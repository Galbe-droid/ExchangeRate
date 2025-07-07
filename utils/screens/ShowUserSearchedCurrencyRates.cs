using ExchangeRate.dto;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class ShowUserSearchedCurrencyRates
    {
        //The display that shows the currency and it quantity coverted with user searcheble currencies
        private static void DisplayStatic(User user, List<CurrencyWithName> listNames, List<CurrencyWithRates> listRates, string seachedCurrecies)
        {
            List<string> searched = seachedCurrecies.Split(',').ToList();
            searched.RemoveAt(searched.Count-1);
            CurrencyWithName currency = listNames.FirstOrDefault(coin => coin.CurrencyCode == user.CurrencyCode)!;
            List<CurrencyNameRates> currencyNameRates = new();

            //Create the objects of Currency with name code and current rate against the main currency 
            foreach (string searchedCurrency in searched)
            {
                string name = listNames.FirstOrDefault(coin => coin.CurrencyCode == searchedCurrency)!.CurrencyName;
                decimal rate = listRates.FirstOrDefault(coin => coin.CurrencyName.Contains(searchedCurrency))!.CurrencyRate;
                currencyNameRates.Add(new(searchedCurrency, name, rate));
            }

            Console.WriteLine($"Name: {user.Name}\n" +
                              $"Main Currency: {user.CurrencyCode} - {currency.CurrencyName}\n" +
                              $"Quantity: {user.Quantity}");
            Console.WriteLine("=============Currency Converted=============");
            foreach (CurrencyNameRates searchedCurrency in currencyNameRates)
            {
                Console.WriteLine(searchedCurrency.ToString() + $" ----- Qty Coverted: {searchedCurrency.CurrencyRate * user.Quantity}");
            }
        }

        //The user interact with this part
        public static void Menu(User user, List<CurrencyWithName> listName, List<CurrencyWithRates> listRates, string searchedCurrecies)
        {
            Console.Clear();
            DisplayStatic(user, listName, listRates, searchedCurrecies);
            Console.WriteLine("(Click Any key to continue)");
            Console.ReadLine();
        }
    }
}
