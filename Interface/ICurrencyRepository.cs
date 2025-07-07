using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Interface
{
    internal interface ICurrencyRepository
    {
        Task GetAllCurrenciesFromAPI();
        void GetAllCurrenciesFromJSON();
        Task InitialLoading();
    }
}
