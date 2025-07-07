using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.model
{
    internal class CurrencyWithName(string code, string name)
    {
        public string CurrencyCode { get; set; } = code;
        public string CurrencyName { get; set; } = name;

        public override string ToString()
        {
            return $"{CurrencyCode} ======= {CurrencyName}";
        }
    }
}
