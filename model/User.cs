using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Quantity { get; set; }
        public List<string> FavoriteCurrencyCode { get; set; }

        [JsonConstructor]
        public User(int id, string name, string currencyCode, decimal quantity)
        {
            Id = id;
            Name = name;
            CurrencyCode = currencyCode;
            Quantity = quantity;
            FavoriteCurrencyCode = [];
        }

        public User(User user)
        {
            Id = user.Id;
            Name = user.Name;
            CurrencyCode = user.CurrencyCode;
            Quantity = user.Quantity;
            FavoriteCurrencyCode = user.FavoriteCurrencyCode;
        }
    }
}
