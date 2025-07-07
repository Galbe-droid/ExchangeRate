using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.middleware
{
    internal static class CheckForValidInput
    {
        //Check if the user select a single digit 
        public static bool CheckForInt(string check)
        {
            bool isValid;
            isValid = !String.IsNullOrEmpty(check);
            if (isValid) { isValid = Char.IsDigit(check[0]); }
            if (isValid) { isValid = check.Length == 1; }
            return isValid;
        }

        //Check if a single currency exists
        public static bool CheckForValidCurrencyCode(List<CurrencyWithName> currencies, string code)
        {
            bool isValid;
            isValid = !String.IsNullOrEmpty(code);
            if (isValid) { isValid = currencies.Exists(cont => cont.CurrencyCode == code); }
            return isValid;
        }
        
        //checks many codes exists and send back the position of the wrong codes 
        public static List<int> CheckForValidCurrencyCodeArray(List<CurrencyWithName> currencies, ref List<string> codes)
        {
            int validCodes = 0;
            List<int> indexesToDelete = [];
            if(codes.Count != 0)
            {
                foreach (string code in codes)
                {
                    if (currencies.Exists(cur => cur.CurrencyCode == code)) { validCodes++; }
                    else { indexesToDelete.Add(codes.IndexOf(code)); }
                }
            }           

            if (validCodes == codes.Count)
            {
                Console.WriteLine("All codes accepted !(press any key)");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"{validCodes} from {codes.Count} codes where accepted !(press any key)");
                Console.ReadLine();
            }

            return indexesToDelete;
        }

        //Check if a decimal is valid
        public static bool CheckForValidDecimal(string  check)
        {
            bool isValid;            
            isValid = !String.IsNullOrEmpty(check);
            decimal checkDecimal = decimal.Parse(check);
            if (isValid) { isValid = Decimal.IsCanonical(checkDecimal); }
            return isValid;
        }

        //Check if the user is valid
        public static bool CheckForValidUser(User user)
        {
            bool isValid;
            isValid = !String.IsNullOrEmpty(user.Name);
            if (isValid) { isValid = !String.IsNullOrEmpty(user.CurrencyCode);  }
            return isValid;
        }
    }
}
