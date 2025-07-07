using ExchangeRate.middleware;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class UpdateUser
    {
        //Static diplay showing user info, update with each render
        private static void DisplayStatic(int id, string name, string currencyCode, decimal quantity)
        {
            Console.WriteLine("=======================");
            Console.Write($"Id: {id}\n" +
                          $"Name: {name}\n" +
                          $"Main Currency: {currencyCode}\n" +
                          $"Quantity: {quantity}\n");
            Console.WriteLine("=======================");
        }

        //User Options
        private static void MenuDisplay()
        {
            Console.Write($"====Options====\n" +
                          $"1 - Change Name\n" +
                          $"2 - Change Currency Code\n" +
                          $"3 - Change Quantity\n" +
                          $"4 - Finalize\n" +
                          $"Choose: ");
        }

        //Display all currencies avaliable
        private static void DisplayCurrenciesPage(List<CurrencyWithName> currencies, int page, decimal limitPage, int itemsPerPage)
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

        //User interacts with this method
        public static User Menu(User user, List<CurrencyWithName> currencies)
        {
            int id = user.Id;
            string name = user.Name;
            string currencyCode  = user.CurrencyCode;
            decimal quantity = user.Quantity;
            int choice = 0;

            do
            {
                Console.Clear();
                DisplayStatic(id, name, currencyCode, quantity);
                MenuDisplay();
                string choiceStr = Console.ReadLine();

                if (CheckForValidInput.CheckForInt(choiceStr))
                {
                    choice = Convert.ToInt32(choiceStr);
                }

                //Change name
                if (choice == 1)
                {
                    do
                    {
                        Console.Clear();
                        DisplayStatic(id, name, currencyCode, quantity);
                        Console.Write($"====Name====\n" +
                                      $"New Name: ");
                        name = Console.ReadLine();
                    } while (String.IsNullOrEmpty(name));
                }

                //Change currency
                if (choice == 2)
                {
                    bool isAccepted = false;
                    int page = 1;
                    int itemsPerPage = 15;
                    decimal limitPage = (currencies.Count < itemsPerPage) ? 1 : Math.Ceiling(Convert.ToDecimal(currencies.Count) / itemsPerPage);
                    do
                    {
                        Console.Clear();
                        DisplayCurrenciesPage(currencies, page, limitPage, itemsPerPage);
                        Console.Write($"====Currency Code====\n" +
                                      $"+ - Next Page\n" +
                                      $"- - Previous Page\n" +
                                      $"(after press enter the system automatically Capitalize the letters)\n" +
                                      $"New Currency Code: ");
                        string choiceCurrency = Console.ReadLine();

                        if (choiceCurrency.Length == 3)
                        {
                            currencyCode = choiceCurrency.ToUpper();
                            isAccepted = CheckForValidInput.CheckForValidCurrencyCode(currencies, currencyCode);
                        }

                        if (choiceCurrency == "+")
                        {
                            if (page == limitPage) { page = int.Parse(limitPage.ToString()); } else { page++; }
                        }

                        if (choiceCurrency == "-")
                        {
                            if (page == 1) { page = 1; } else { page--; }
                        }

                    } while (!isAccepted);

                }

                //Change quantity
                if (choice == 3)
                {
                    bool isAccepted = false;
                    string quantityStr = "";
                    do
                    {
                        Console.Clear();
                        DisplayStatic(id, name, currencyCode, quantity);
                        Console.Write($"====Quantity====\n" +
                                      $"use ',' to separate" +
                                      $"Quantity: ");
                        quantityStr = Console.ReadLine();
                        isAccepted = CheckForValidInput.CheckForValidDecimal(quantityStr);
                        quantity = decimal.Parse(quantityStr);
                    } while (String.IsNullOrEmpty(quantityStr) && isAccepted);
                }

                //Finalize the update
                if (choice == 4)
                {
                    bool isAccepted = false;
                    string validation = "";
                    User updateUser = new(id, name, currencyCode, quantity);
                    do
                    {
                        isAccepted = CheckForValidInput.CheckForValidUser(user);
                        //if isnt the user receive a warning to try again or continue without saving
                        if (!isAccepted)
                        {
                            Console.WriteLine("Error. Character not updated.");
                            Console.Write("Do you wish to edit again Y/N: ");
                            validation = Console.ReadLine().ToLower();
                            //if the user choose to try again the choice is change so it not break the while loop
                            if (validation == "y")
                            {
                                choice = 0;
                            }
                            break;
                        }
                        else
                        {
                            //If everything is ok the user is updated and returned
                            Console.WriteLine($"User {user.Name} Updated");
                            return updateUser;
                        }
                    } while (true);
                }
            } while (choice != 4) ;
            return user;
        }
    }
}
