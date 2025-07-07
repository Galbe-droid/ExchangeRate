using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.model
{
    internal class CurrencyWithRates
    {
        public string CurrencyName { get; set; }
        public decimal CurrencyRate { get; set; }

        public CurrencyWithRates(string name, decimal rate)
        {
            CurrencyName = name;
            CurrencyRate = rate;
        }
    }
}
