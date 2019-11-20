using System;

namespace HomeBrewCalculators
{
    class Program
    {
        static void Main(string[] args)
        {

            AVBCalc abvCalc = new AVBCalc();
            DMECalc dmeCalc = new DMECalc();
            bool mainMenuTrue = true;
            while (mainMenuTrue == true)
            {
                Console.Clear();
                Console.WriteLine("\nWhich Calculation would you like to do?");
                Console.WriteLine("=======================================\n");
                Console.WriteLine("1.) ABV Calculator");
                Console.WriteLine("2.) DME Calculator");
                Console.WriteLine("3.) Sparge Water Calculator");
                Console.WriteLine("4.) Boil Off Rate Calculator");
                Console.WriteLine("5.) IBU Calculator");
                Console.WriteLine("6.) Yeast Pitch Calculator\n");
                Console.Write("Selection: ");
                string selection = Console.ReadLine();


                if (selection == "1")
                {

                    abvCalc.ABVMenu();
                }
                if (selection == "2")
                {
                    dmeCalc.DMEMenu();
                }


            }

           
        }
    }
}
