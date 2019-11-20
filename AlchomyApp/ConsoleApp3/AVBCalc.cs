using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBrewCalculators
{
    class AVBCalc
    {
        public void ABVMenu()
        {
            bool repeatMethod = true;
            double resultABV = 0.0;
            string oGravity = "";
            string fGravity = "";

            while (repeatMethod)
            {
                Console.Clear();
                Console.Write("Welcome to the ABV Calculator!!\n" +
                            "Use This Calculator to determine the Alcohol By Volome in your brew. \n" +
                           "===============================\n" +
                          "Please enter Original Gravity (1.000 - 1.130): ");
                oGravity = Console.ReadLine();
                Console.Write("Please Enter Final Gravity (1.000 - 1.130): ");
                fGravity = Console.ReadLine();

                //Convert user string inputs to doubles
                double oGDouble = Convert.ToDouble(oGravity);
                double oFDouble = Convert.ToDouble(fGravity);

                //Calculates ABV
                resultABV = (oGDouble - oFDouble) * 131.25;

                Console.WriteLine($"Your Brew's ABV is {string.Format("{0:0.00}", resultABV)} \n" +
                                    "===============================\n" +
                                    "New Calculation or Return to Main Menu?\n" +
                                    "1.) New Calculation\n" +
                                    "2.) Main Menu");
                Console.Write("Selection: ");
                string selection = Console.ReadLine();

                if (selection == "1")
                {
                    repeatMethod = true;
                }
                else if (selection == "2")
                {
                    repeatMethod = false;
                }
            }
        }
    }
}
