using ExchangeRate.dto;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class ShowUserFavoriteCurrencyRates
    {
        //The display that shows the currency and it quantity coverted with user favorite currencies
        private static void DisplayStatic(User user, List<CurrencyWithName> listNames, List<CurrencyWithRates> listRates)
        {
            CurrencyWithName currency = listNames.FirstOrDefault(coin => coin.CurrencyCode == user.CurrencyCode)!;
            List<CurrencyNameRates> currencyNameRates = new();

            //Create the objects of Currency with name code and current rate against the main currency 
            foreach(string favoriteCurrency in user.FavoriteCurrencyCode)
            {
                string name = listNames.FirstOrDefault(coin => coin.CurrencyCode == favoriteCurrency)!.CurrencyName;
                decimal rate = listRates.FirstOrDefault(coin => coin.CurrencyName.Contains(favoriteCurrency))!.CurrencyRate;
                currencyNameRates.Add(new(favoriteCurrency, name, rate));
            }

            Console.WriteLine($"Name: {user.Name}\n" +
                              $"Main Currency: {user.CurrencyCode} - {currency.CurrencyName}\n" +
                              $"Quantity: {user.Quantity}");
            Console.WriteLine("=============Currency Converted=============");
            foreach(CurrencyNameRates favCurrency in  currencyNameRates)
            {
                Console.WriteLine(favCurrency.ToString() + $" ----- Qty Coverted: {favCurrency.CurrencyRate * user.Quantity}");
            }            
        }

        //The user interact with this part
        public static void Menu(User user, List<CurrencyWithName> listName, List<CurrencyWithRates> listRates)
        {
            Console.Clear();
            DisplayStatic(user, listName, listRates);
            Console.WriteLine("(Click Any key to continue)");
            Console.ReadLine();
        }
    }
}
