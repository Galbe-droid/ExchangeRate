using ExchangeRate.middleware;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class ChooseUser
    {
        //A static display showing all the user 
        private static void DisplayStatic(List<User> users, List<CurrencyWithName> currencies, int page, decimal limitPage, int itemsPerPage)
        {
            Console.WriteLine($"==========Users:{users.Count}===========");
            Console.WriteLine($"=========={page} - {limitPage}==========");
            for (int i = 0; i < itemsPerPage; i++)
            {
                int actualPage = (page - 1) * itemsPerPage;
                actualPage += i;
                if (actualPage > users.Count - 1)
                {
                    break;
                }
                else
                {
                    CurrencyWithName currency = currencies.FirstOrDefault(coin => coin.CurrencyCode == users[actualPage].CurrencyCode)!;
                    Console.WriteLine($"Id: {users[actualPage].Id} | Name: {users[actualPage].Name} | Main Currency: {users[actualPage].CurrencyCode} ---- {currency.CurrencyName} | Quantity: {users[actualPage].Quantity}");
                }
            }
            Console.WriteLine("================================");
        }

        //User acts inside this method 
        public static User Menu(List<User> users, List<CurrencyWithName> currencies)
        {
            int page = 1;
            int itemsPerPage = 5;
            decimal limitPage = (users.Count < itemsPerPage) ? 1 : Math.Ceiling(Convert.ToDecimal(users.Count) / itemsPerPage);
            User choosenUser = null;
            string choiceStr = "";
            do
            {
                Console.Clear();
                DisplayStatic(users, currencies, page, limitPage, itemsPerPage);
                Console.Write("Choose by id ('e' to exit): ");
                choiceStr = Console.ReadLine();

                //Exit this menu 
                if(choiceStr == "e")
                {
                    return null;
                }

                bool isValid = CheckForValidInput.CheckForInt(choiceStr);

                //If the choice is a valid int them it is check if the user exits
                if (isValid)
                {
                    int choice = Convert.ToInt32(choiceStr);   

                    choosenUser = users.Find(user => user.Id == choice)!;
                    //if null the method loop
                    if(choosenUser != null)
                    {
                        return choosenUser; 
                    }                    
                } 
            } while (true);
        }
    }
}
