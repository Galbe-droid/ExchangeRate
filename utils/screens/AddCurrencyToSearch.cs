using ExchangeRate.middleware;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class AddCurrencyToSearch
    {
        //Show to user all avaliable currencies is a list and page format
        private static void CurrencyDisplay(List<CurrencyWithName> currencies, int page, decimal limitPage, int itemsPerPage)
        {
            Console.WriteLine($"////{page} - {limitPage}////");
            for (int i = 0; i < itemsPerPage; i++)
            {
                int actualPage = (page - 1) * itemsPerPage;
                actualPage += i;
                if (actualPage > currencies.Count - 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(currencies[actualPage].ToString());
                }
            }
        }

        //Options inside this menu
        private static void ChoiceMenu()
        {
            Console.Write($"=======================\n" +
                          $"1 - Pick currency\n" +
                          $"2 - Remove currency\n" +
                          $"3 - Exit\n" +
                          $"Choose: ");
        }

        //This is the part that the user interacts with 
        public static string Menu(List<CurrencyWithName> currencies)
        {
            int page = 1;
            int itemsPerPage = 15;
            decimal limitPage = (currencies.Count < itemsPerPage) ? 1 : Math.Ceiling(Convert.ToDecimal(currencies.Count) / itemsPerPage);
            int choose = 0;
            string chooseStr;
            string choosenCurrencies = ""; 
            do
            {
                Console.Clear();
                ChoiceMenu();
                chooseStr = Console.ReadLine();

                if (CheckForValidInput.CheckForInt(chooseStr))
                {
                    choose = Convert.ToInt32((string)chooseStr);

                    if (choose == 1)
                    {
                        bool isAccepted = false;
                        do
                        {
                            Console.Clear();
                            CurrencyDisplay(currencies, page, limitPage, itemsPerPage);
                            Console.Write($"====Currency Code====\n" +
                                          $"+ - Next Page\n" +
                                          $"- - Previous Page\n" +
                                          $"(after press enter the system automatically Capitalize the letters)\n" +
                                          $"(Use ',' to separate de currencies)" +
                                          $"New Currency Code: ");
                            //using + and - the user move between pages as to choose the currency it write it down separating by ","
                            string choiceCurrency = Console.ReadLine();

                            if (choiceCurrency.Length >= 3)
                            {
                                //if user didnt choose + or - and has 3 or more letters the entire string is uppercase 
                                choiceCurrency = choiceCurrency.ToUpper();
                                //The values are split and place on a list so the checkers however it need to check if the currency is valid 
                                List<string> currencySeparated = choiceCurrency.Split(',').ToList();
                                //checks is the currency is valid and if isnt the index are send back in a list form 
                                List<int> indexesToDelete = CheckForValidInput.CheckForValidCurrencyCodeArray(currencies, ref currencySeparated);

                                //Delete all invalid currencies
                                if (indexesToDelete.Count != 0)
                                {
                                    foreach (int index in indexesToDelete)
                                    {
                                        currencySeparated.RemoveAt(index);
                                    }
                                }

                                //Make the valis currency a single string
                                foreach(string currency in currencySeparated)
                                {
                                    choosenCurrencies += currency + ",";
                                }

                                //After all the process the user will leave the loop 
                                isAccepted = true;
                            }

                            //+ add +1 to the page but is limited by the max pages
                            if (choiceCurrency == "+")
                            {
                                if (page == limitPage) { page = int.Parse(limitPage.ToString()); } else { page++; }
                            }

                            //- add -1 to the page but is limited to not go below 1 
                            if (choiceCurrency == "-")
                            {
                                if (page == 1) { page = 1; } else { page--; }
                            }

                        } while (!isAccepted);
                    }
                }
                //If user choose 3 it exit the loop returning all the valid currencies it want to search 
            } while (choose != 3);
            return choosenCurrencies;
        }
    }
}
