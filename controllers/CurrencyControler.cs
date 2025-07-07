using ExchangeRate.model;
using ExchangeRate.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.controllers
{
    internal class CurrencyControler
    {
        public CurrencyService _currencyService;

        public CurrencyControler(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public List<CurrencyWithName> GetAllCurrencies()
        {
            return _currencyService.GetAllCurrency();
        }

        public CurrencyWithName GetCurrencyByCode(string code)
        {
            return _currencyService.GetCurrencyByCode(code);
        }

        public List<CurrencyWithName> GetCurrenciesByCode(string[] code)
        {
            return _currencyService.GetCurrenciesByCode(code);
        }

        public void InitialLoading()
        {
            _currencyService.InitialLoading();
        }

        public async Task UpdateList()
        {
            await _currencyService.UpdateList();
        }
    }
}
