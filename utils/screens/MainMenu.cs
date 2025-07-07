using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.utils.screens
{
    internal static class MainMenu
    {
        //All options in the main menu, this is the first screen 
        public static void Display()
        {
            Console.Write($"Options\n" +
                          $"1-Add a user\n" +
                          $"2-Choose a active user\n" +
                          $"3-Modify active user (need active user)\n" +
                          $"4-Add currency to compare (need active user)\n" +
                          $"5-Check all favorite currency (need active user)\n" +
                          $"6-Check another currency (need active user)\n" +
                          $"7-Exit\n" +
                          $"Choose: ");
        }
    }
}
