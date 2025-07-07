using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal static class UserInfo
    {
        public static void Display(User user, List<CurrencyWithName> list)
        {
            if (user == null)
            {
                Console.WriteLine("No active user");
            }
            else
            {
                CurrencyWithName currency = list.FirstOrDefault(coin => coin.CurrencyCode == user.CurrencyCode)!;
                Console.WriteLine($"Name: {user.Name}\n" +
                                  $"Main Currency: {user.CurrencyCode} - {currency.CurrencyName}\n" +
                                  $"Quantity: {user.Quantity}");
                Console.Write($"Favorite Currencies: ");
                foreach (string code in user.FavoriteCurrencyCode)
                {
                    Console.Write(code + ", ");
                }
                Console.WriteLine();
            }
        }   
    }
}
