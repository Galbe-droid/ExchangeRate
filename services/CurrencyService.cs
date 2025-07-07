using ExchangeRate.Interface;
using ExchangeRate.model;
using ExchangeRate.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.services
{
    internal class CurrencyService : ICurrencyService
    {
        private CurrencyRepository _currencyRepository;

        public CurrencyService(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public List<CurrencyWithName> GetAllCurrency()
        {
            return _currencyRepository._currencyList;
        }

        public List<CurrencyWithName> GetCurrenciesByCode(string[] codes)
        {
            return _currencyRepository._currencyList.FindAll(cur => codes.Contains(cur.CurrencyCode));
        }

        public CurrencyWithName GetCurrencyByCode(string code)
        {
            return _currencyRepository._currencyList.FirstOrDefault(cur => cur.CurrencyCode == code);
        }

        public async void InitialLoading()
        {
            await _currencyRepository.InitialLoading();
        }

        public async Task UpdateList()
        {
            await _currencyRepository.GetAllCurrenciesFromAPI();
        }
    }
}
