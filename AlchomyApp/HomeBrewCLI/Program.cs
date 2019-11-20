using HomeBrew;
using System;

namespace HomeBrewCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeBrewCalculator hBCalc = new HomeBrewCalculator();
            Menu menu = new Menu(hBCalc);
            menu.MainMenu();

        }
    }
}
