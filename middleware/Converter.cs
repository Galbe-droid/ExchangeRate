using ExchangeRate.dto;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.middleware
{
    internal class Converter
    {
        //Convert a dto into a model currency with name
       public static List<CurrencyWithName> ConvertDictionaryToCurrency(ExchangeRateAllCurrencies keyValuePairsCurrency)
       {
            List<CurrencyWithName> allCurrencies = new();
            foreach (KeyValuePair<string, string> currency in keyValuePairsCurrency.currencies)
            {
                CurrencyWithName newItem = new CurrencyWithName(currency.Key.ToString(), currency.Value.ToString());
                allCurrencies.Add(newItem);
            }
            return allCurrencies;
       }

        //Convert a dto into a model currency with rates
       public static List<CurrencyWithRates> ConvertDictionaryToCurrencyWithRates(ExchangeRateFavoriteCurrencyRates keyValuePairsCurrency)
       {
            List<CurrencyWithRates> allRates = new();

            foreach(KeyValuePair<string, decimal> currency in keyValuePairsCurrency.quotes)
            {
                CurrencyWithRates newItem = new CurrencyWithRates(currency.Key.ToString(), currency.Value);
                allRates.Add(newItem);
            }
            return allRates;
       }
    }
}
