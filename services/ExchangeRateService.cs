using ExchangeRate.dto;
using ExchangeRate.Interface;
using ExchangeRate.model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.services
{
    internal class ExchangeRateService : IExchangeRateService
    {
        private HttpClient _httpClient;
        //Get the key here: https://exchangerate.host/
        private readonly string exchangeKey = "PLACE YOUR KEY HERE !!!!!";

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get all avaliable currency, it gets one time them it saved on the Json file so it does not use more tokens
        public async Task<ExchangeRateAllCurrencies> GetAllAsync()
        {            
            ExchangeRateAllCurrencies currencies = new();
            try
            {               
                var result = await _httpClient.GetAsync($"https://api.exchangerate.host/list?access_key={exchangeKey}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();;
                    currencies = JsonConvert.DeserializeObject<ExchangeRateAllCurrencies>(content)!;                  
                }
                return currencies!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Get all favorite or shearched currency that the user wanted 
        public async Task<ExchangeRateFavoriteCurrencyRates> GetRatesAsync(string favoriteCurrencies, string mainCurrency)
        {
            ExchangeRateFavoriteCurrencyRates favorites = new();
            try
            {
                Console.WriteLine($"{favoriteCurrencies} + {mainCurrency}");
                Console.ReadLine();
                var result = await _httpClient.GetAsync($"https://api.exchangerate.host/live?access_key={exchangeKey}&source={mainCurrency}&currencies={favoriteCurrencies}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    Console.ReadLine();
                    favorites = JsonConvert.DeserializeObject<ExchangeRateFavoriteCurrencyRates>(content)!;
                }
                foreach(KeyValuePair<string, decimal> keyValuePair in favorites.quotes)
                {
                    Console.WriteLine($"{keyValuePair.Key} + {keyValuePair.Value}");
                }
                Console.ReadLine();
                return favorites!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
