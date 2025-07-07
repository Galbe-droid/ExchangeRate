using ExchangeRate.middleware;
using ExchangeRate.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal class AddUser
    {
        //Atributes for adding a user
        private static string name = "";
        private static string currencyCode = "";
        private static decimal quantity = 0;

        //A static display for the user to follow
        private static void StaticDisplay(int idPreview)
        { 
            Console.WriteLine($"====New User====\n" +
                              $"Name: {name}\n" +
                              $"Currency Code: {currencyCode}\n" +
                              $"Quantity: {quantity}");            
        }

        //User options
        private static void MenuDisplay()
        {
            Console.Write($"====Options====\n" +
                          $"1 - Change Name\n" +
                          $"2 - Change Currency Code\n" +
                          $"3 - Quantity\n" +
                          $"4 - Add User\n" +
                          $"Choose: ");
        }

        //Show all the currencies avaliable
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

        //User acts inside this method for a user creation
        public static User Menu(int idPreview, List<CurrencyWithName> currencies)
        {
            string choiceStr = "";
            int choice = 0;
            do
            {
                Console.Clear();
                StaticDisplay(idPreview);
                MenuDisplay();
                choiceStr = Console.ReadLine();
                if (CheckForValidInput.CheckForInt(choiceStr))
                {
                    choice = int.Parse(choiceStr);
                }

                //Change name and consult if the name is not null or empty
                if(choice == 1)
                {
                    do
                    {
                        Console.Clear();
                        StaticDisplay(idPreview);
                        Console.Write($"====Name====\n" +
                                      $"New Name: ");
                        name = Console.ReadLine();
                    } while (String.IsNullOrEmpty(name));
                }

                //Change the main currency of the user
                if(choice == 2)
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
                        //using + and - the user move between pages as to choose the currency it write it down
                        string choiceCurrency = Console.ReadLine();

                        //Only one currency is accepted this is the reason the Lenght is aways 3
                        if(choiceCurrency.Length == 3)
                        {
                            currencyCode = choiceCurrency.ToUpper();
                            isAccepted = CheckForValidInput.CheckForValidCurrencyCode(currencies, currencyCode);
                        }

                        //+ add +1 to the page but is limited by the max pages
                        if (choiceCurrency == "+")
                        {
                            if(page == limitPage) { page = int.Parse(limitPage.ToString()); } else { page++; }
                        }

                        //- add -1 to the page but is limited to not go below 1 
                        if (choiceCurrency == "-")
                        {
                            if (page == 1) { page = 1; } else { page--; }
                        }

                        Console.Write(isAccepted);
                        Console.ReadLine();

                    } while (String.IsNullOrEmpty(currencyCode) && !isAccepted);
                }

                //Change the quantity 
                if(choice == 3)
                {
                    bool isAccepted = false;
                    string quantityStr = "";
                    do
                    {
                        Console.Clear();
                        StaticDisplay(idPreview);
                        Console.Write($"====Quantity====\n" +
                                      $"use ',' to separate" +
                                      $"Quantity: ");
                        quantityStr = Console.ReadLine();
                        isAccepted = CheckForValidInput.CheckForValidDecimal(quantityStr);
                        quantity = decimal.Parse(quantityStr);
                    } while (String.IsNullOrEmpty(quantityStr) && isAccepted);
                }

                //Finish the user creation and here occur the verification 
                if(choice == 4)
                {
                    bool isAccepted = false;
                    string validation = "";
                    User user = new(idPreview, name, currencyCode, quantity);
                    do
                    {
                        //Check if the user is Valid
                        isAccepted = CheckForValidInput.CheckForValidUser(user);
                        //if isnt the user receive a warning to try again or continue without saving
                        if (!isAccepted)
                        {
                            Console.WriteLine("Error. Character not created.");
                            Console.Write("Do you wish to edit again Y/N: ");
                            validation = Console.ReadLine().ToLower();
                            //if the user choose to try again the choice is change so it not break the while loop
                            if(validation == "y")
                            {
                                choice = 0;
                            }
                            break;
                        }
                        else
                        {
                            //If everything is ok the user is added and returned
                            Console.WriteLine($"User {user.Name} Added");
                            return user;
                        }
                    } while (true);
                }
            } while (choice != 4);
            return null;
        }
    }
}
