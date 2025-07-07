using ExchangeRate.dto;
using ExchangeRate.Interface;
using ExchangeRate.middleware;
using ExchangeRate.model;
using ExchangeRate.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.repositories
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        public List<CurrencyWithName> _currencyList = [];
        public readonly string path = @"../../../SaveInfo/allCurrencies.json";

        private ExchangeRateService _exchangeRateService;
        private CheckJSONFilesForCurrencies _checkJSONFilesForCurrencies;

        public CurrencyRepository(ExchangeRateService exchangeRateService, CheckJSONFilesForCurrencies checkJSONFilesForCurrencies)
        {
            _exchangeRateService = exchangeRateService;
            _checkJSONFilesForCurrencies = checkJSONFilesForCurrencies;
        }

        public async Task GetAllCurrenciesFromAPI()
        {
            ExchangeRateAllCurrencies exchange = await _exchangeRateService.GetAllAsync();
            _currencyList = Converter.ConvertDictionaryToCurrency(exchange);
            _checkJSONFilesForCurrencies.SaveJSONCurrency(exchange, path); 
        }

        public void GetAllCurrenciesFromJSON()
        {
            ExchangeRateAllCurrencies currencyDictionary = _checkJSONFilesForCurrencies.LoadJSONCurrency(path);
            _currencyList = Converter.ConvertDictionaryToCurrency(currencyDictionary);
        }

        public async Task InitialLoading()
        {
            //Check for save file for Currency 
            if (!File.Exists(path))
            {
                _checkJSONFilesForCurrencies.CheckFileForCurrencies(path);
                await GetAllCurrenciesFromAPI();
            }
            else
            {
                GetAllCurrenciesFromJSON();
            }

        }
    }
}
