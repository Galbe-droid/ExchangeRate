using ExchangeRate.dto;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Interface
{
    internal interface IExchangeRateService
    {
        Task<ExchangeRateAllCurrencies> GetAllAsync();
        Task<ExchangeRateFavoriteCurrencyRates> GetRatesAsync(string favoriteCurrencies, string mainCurrency);
    }
}
