using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Interface
{
    internal interface ICurrencyService
    {
        void InitialLoading();
        Task UpdateList();
        List<CurrencyWithName> GetAllCurrency();
        CurrencyWithName GetCurrencyByCode(string code);
        List<CurrencyWithName> GetCurrenciesByCode(string[] codes);
    }
}
