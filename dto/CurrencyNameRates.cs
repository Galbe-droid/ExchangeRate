using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.dto
{
    internal class CurrencyNameRates
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyRate { get; set; }

        public CurrencyNameRates(string code, string name, decimal rate)
        {
            this.CurrencyCode = code;
            this.CurrencyName = name;
            this.CurrencyRate = rate;
        }

        public override string ToString()
        {
            return $"{CurrencyCode} ----- {CurrencyName} ----- Currency Rate: {CurrencyRate}";
        }
    }
}
