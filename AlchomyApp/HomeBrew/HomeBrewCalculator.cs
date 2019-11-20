using System;

namespace HomeBrew
{
    public class HomeBrewCalculator
    {
        const string ABV_MENU = "Welcome to the ABV Calculator!!\n" +
                            "Use This Calculator to determine the Alcohol By Volome in your brew. \n" +
                           "===============================\n";
        const string CONST_REQUEST_ORIGINALGRAVITY = "Please enter Original Gravity (1.000 - 1.130): ";
        const string CONST_REQUEST_FINALGRAVITY = "Please Enter Final Gravity (1.000 - 1.130): ";


        public void AbvCalcOpenMenu()
        { 

            bool repeatMethod = true;

            while (repeatMethod)
            {
                Console.Clear();

                Console.Write(ABV_MENU);
                Console.Write(CONST_REQUEST_ORIGINALGRAVITY); 
                string oGravity = Console.ReadLine();

                Console.Write(CONST_REQUEST_FINALGRAVITY);
                string fGravity = Console.ReadLine();

                double resultABV = RunABVCalc(oGravity, fGravity);

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

        private double RunABVCalc(string oGravity, string fGravity)
        {
            double resultABV = 0.0;
            
            //Convert user string inputs to doubles

            double oGDouble = Convert.ToDouble(oGravity);
            double oFDouble = Convert.ToDouble(fGravity);


            //Calculates ABV
            resultABV = (oGDouble - oFDouble) * 131.25;
            return resultABV;
        }
    }
}
