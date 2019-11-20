using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBrewCalculators
{
    class DMECalc
    {
        public void DMEMenu()
        {
            bool repeatMethod = true;
            double originalDME = 0.0;
            double resultDME = 0.0;
            double resultDMEOunces = 0.0;
            double resultDMEGrams = 0.0;
            string oGravity = "";
            string gallons = "";
            string targetGravity = "";
            double targetDME = 0.0; 

            while (repeatMethod)
            {
                Console.Clear();
                Console.Write("Welcome to the DME Calculator!!\n" +
                            "Use This Calculator to determine the Dry Malt Extract needed for your brew. \n" +
                           "===============================\n" +
                          "Please enter Original Gravity (1.020 - 1.130): ");
                oGravity = Console.ReadLine();
                Console.Write("Please Enter volume of Gallons: ");
                gallons = Console.ReadLine();
                Console.Write("Please Enter Target Gravity (1.020 - 1.150): ");
                targetGravity = Console.ReadLine();

                //Convert user string inputs to doubles
                double oGDouble = Convert.ToDouble(oGravity);
                double gallonDouble = Convert.ToDouble(gallons);
                double targetGravityDouble = Convert.ToDouble(targetGravity);
                //Calculates DME

                originalDME = oGDouble * gallonDouble;
                targetDME = targetGravityDouble * gallonDouble;
                resultDME =  (originalDME / targetDME) ;
                resultDMEOunces = resultDME * 16;
                resultDMEGrams = resultDME * 453.592;

                Console.WriteLine($"Your Brew's Required DME is {String.Format("{0:0.00}", resultDME)} Lbs \n" +
                                                           $"or {String.Format("{0:0.00}", resultDMEOunces)} Ounces \n" +
                                                           $"or {String.Format("{0:0.00}", resultDMEGrams)} Grams \n" +
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

