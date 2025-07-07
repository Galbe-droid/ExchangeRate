// See https://aka.ms/new-console-template for more iformation
using ExchangeRate.controllers;
using ExchangeRate.dto;
using ExchangeRate.middleware;
using ExchangeRate.model;
using ExchangeRate.repositories;
using ExchangeRate.services;
using ExchangeRate.utils.screens;

HttpClient _httpClient = new();

//Checking for JSON for saving and loading 
CheckJSONFilesForCurrencies _checkJSONFilesForCurrencies = new();
CheckJSONFilesForUser _checkJSONFilesForUser = new();

//ExchangeRate external API
ExchangeRateService _exchangeRateService = new(_httpClient);

//Currency API
CurrencyRepository _currencyRepository = new(_exchangeRateService, _checkJSONFilesForCurrencies);
CurrencyService _currencyService = new(_currencyRepository);
CurrencyControler _currencyController = new(_currencyService);

//User API
UserRepository _userRepository = new(_checkJSONFilesForUser);
UserService _userService = new(_userRepository);
UserController _userController = new(_userService);

//Check for save file for User
_userController.CheckPath();
_userController.Load();

//Check for save file for Currency
_currencyController.InitialLoading();

//Main Menu atributes 
int choice = 0;
string choiceStr = "";
User activeUser = null;

//Program is active until the user chooses the number 7
do
{
    Console.Clear();
    Console.WriteLine($"User Register {_userController.GetQuantity()}");
    Console.WriteLine("===========================================");
    UserInfo.Display(activeUser, _currencyRepository._currencyList);
    Console.WriteLine("===========================================");
    MainMenu.Display();
    choiceStr = Console.ReadLine();

    //checks if user input is a singular interger, if not the program loops
    if (CheckForValidInput.CheckForInt(choiceStr))
    {
        //if choiceStr is a valid int the parse is made 
        choice = int.Parse(choiceStr); 

        //option 1 is to add a user 
        if (choice == 1) 
        {
            User newUser = AddUser.Menu(_userController.GetQuantity(), _currencyController.GetAllCurrencies());
            if (newUser != null)
            {
                _userController.Post(newUser);
                Console.Write($"User {newUser.Name} was added");
            }
        }

        //Option 2 is to make a user active 
        if (choice == 2) 
        {
            activeUser = ChooseUser.Menu(_userController.Get(), _currencyController.GetAllCurrencies());
        }

        //Option 3 is to update and active user 
        if (choice == 3) 
        {
            if (activeUser != null)
            {
                User updateUser = UpdateUser.Menu(activeUser, _currencyController.GetAllCurrencies());
                if (updateUser != activeUser)
                {
                    _userController.Put(updateUser, updateUser.Id);
                    activeUser = updateUser;
                }
            }
            else
            {
                Console.WriteLine("No active user. (Press any key.)");
                Console.ReadLine();
            }
        }

        //Option 4 is for add active user it favorite currencies
        if (choice == 4)
        {
            if (activeUser != null)
            {
                List<string> oldFavoriteCurrencies = activeUser.FavoriteCurrencyCode;
                User updateUser = new(AddFavoriteCurrency.Menu(activeUser, _currencyController.GetAllCurrencies()));
                _userController.Put(updateUser, updateUser.Id);
                activeUser = updateUser;
            }
            else
            {
                Console.WriteLine("No active user. (Press any key.)");
                Console.ReadLine();
            }
        }

        //Option 5 is to show user currency converted in other countries currency
        if (choice == 5) 
        {
            if (activeUser != null)
            {
                string favoriteCurrency = "";
                foreach (string cur in activeUser.FavoriteCurrencyCode)
                {
                    favoriteCurrency += $"{cur},";
                }
                ExchangeRateFavoriteCurrencyRates listRates = _exchangeRateService.GetRatesAsync(favoriteCurrency, activeUser.CurrencyCode).Result;
                List<CurrencyWithRates> currencyWithRates = Converter.ConvertDictionaryToCurrencyWithRates(listRates);

                ShowUserFavoriteCurrencyRates.Menu(activeUser, _currencyService.GetAllCurrency(), currencyWithRates);
            }
            else
            {
                Console.WriteLine("No active user. (Press any key.)");
                Console.ReadLine();
            }
        }

        //Option 6 is for the user choose to search currencies and show it values converted
        if (choice == 6) 
        {
            if (activeUser != null)
            {
                string currencyToSearch = "";
                currencyToSearch = AddCurrencyToSearch.Menu(_currencyService.GetAllCurrency());

                ExchangeRateFavoriteCurrencyRates listRates = _exchangeRateService.GetRatesAsync(currencyToSearch, activeUser.CurrencyCode).Result;
                List<CurrencyWithRates> currencyWithRates = Converter.ConvertDictionaryToCurrencyWithRates(listRates);

                ShowUserSearchedCurrencyRates.Menu(activeUser, _currencyService.GetAllCurrency(), currencyWithRates, currencyToSearch);
            }
            else
            {
                Console.WriteLine("No active user. (Press any key.)");
                Console.ReadLine();
            }
        }

        //Exit programa
        if (choice == 7) { break; }
    }
} while(true);



