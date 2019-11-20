using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using System.IO;

namespace Capstone.Classes
{
    class VendingMachineCLI
    {
        public void GenerateDisplayItemsMenu(Dictionary<string, SnackItem> Snacks, VendingMachine Vend, bool bMainMenu = true)
        {
            Console.Clear();
            List<string> sListOfKeys = new List<string>();
            foreach (string s in Snacks.Keys)
            {
                sListOfKeys.Add(s);
            }
            string[] saKeys = sListOfKeys.ToArray();
            decimal dBalanceOverride = Vend.Balance;
            if (dBalanceOverride > 999.99m)
            {
                dBalanceOverride = 999.99m;
            }
            string sLine = "_______________________________________________\n" +
"||   _   _   _   _   _   _   _   _   _   _   ||\n" +
"||  / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\  ||\n" +
"|| ( V | E | N | D | O | M | A | T | I | C ) ||\n" +
"||  \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/  ||\n" +
"||___________________________________________||\n" +
"||      ||      ||      ||      ||           ||\n" +
$"||  A1  ||  A2  ||  A3  ||  A4  ||  Balance  ||\n" +
$"||      ||      ||      ||      ||  ${string.Format("{0:000.00}", dBalanceOverride)}  ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||  [A] [B]  ||\n" +
$"||  B1  ||  B2  ||  B3  ||  B4  ||  [C] [D]  ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||  [1] [2]  ||\n" +
$"||      ||      ||      ||      ||  [3] [4]  ||\n" +
$"||  C1  ||  C2  ||  C3  ||  C4  ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||  D1  ||  D2  ||  D3  ||  D4  ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
$"||      ||      ||      ||      ||           ||\n" +
"||______________________________||           ||\n" +
"||                              ||           ||\n" +
"||                              ||           ||\n" +
"||______________________________||___________||\n\n";
            string[] newStringArray = sLine.Split('\n');
            foreach (string s in newStringArray)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(s);
                Console.ResetColor();
            }
            int n = 0;
            sLine = "";
            foreach (string s in saKeys)
            {
                if (Snacks[saKeys[n]].nAmount <= 0)
                {
                    sLine += "**SOLD OUT**\n";
                }
                else
                {
                    sLine += $"{ Snacks[saKeys[n]].sLocation} - { Snacks[saKeys[n]].sName} - ${ Snacks[saKeys[n]].dPrice}\n";
                }
                n++;
            }
            Console.WriteLine(sLine);
            Console.ForegroundColor = ConsoleColor.White;
            if (bMainMenu)
            {
                Console.WriteLine("1). Insert Money");
                Console.WriteLine("2). Select Product");
                Console.WriteLine("3). Finish Transaction");
                Console.WriteLine("4). Exit");
            }
        }

        public void PromptInsertMoney(VendingMachine VendoMatic600, string sLogPath)
        {
            Console.WriteLine("How many Dollars do you want to insert into the machine?");
            bool bIsValidAmount = int.TryParse(Console.ReadLine(), out int nAmount);
            while (!bIsValidAmount)
            {
                Console.WriteLine("Please enter a valid number of Dollar Bills.");
                bIsValidAmount = int.TryParse(Console.ReadLine(), out nAmount);
            }
            VendoMatic600.InsertMoney(nAmount);
            GenerateDisplayItemsMenu(VendoMatic600.GetSnackItems(), VendoMatic600, false);
            using (StreamWriter newSW = new StreamWriter(sLogPath, true))
            {
                //write Date & Time
                newSW.Write(DateTime.Now.ToString() + " ");
                //write amount entered
                newSW.Write($"FEED MONEY: ${string.Format("{0:0.00}", (double)nAmount)} ");
                //write balance                                
                newSW.Write($"${string.Format("{0:0.00}", VendoMatic600.Balance)}\n");
            }
        }

        public void RunTransaction(VendingMachine VendoMatic600, string sLogPath)
        {
            Console.WriteLine("Ending Transaction... Returning Change...");
            string sLine = VendoMatic600.GetChangeMessage(VendoMatic600.Balance);
            Console.WriteLine(sLine);
            using (StreamWriter newSW = new StreamWriter(sLogPath, true))
            {
                //write Date & Time
                newSW.Write(DateTime.Now.ToString() + " ");
                //write transaction amount 
                newSW.Write("GIVE CHANGE: $" + VendoMatic600.Balance + " ");
                VendoMatic600.Balance = 0;
                //write balance                                
                newSW.Write($"${string.Format("{0:0.00}", VendoMatic600.Balance)}\n");
            }
            Console.ReadKey();
        }

        public void RunPurchase(VendingMachine VendoMatic600, string sLogPath)
        {

            Console.Write("Please Select Your Product by ID: ");
            bool bIsValidProductNumber = false;
            bool bIsValidProductLetter = false;
            string sInput = Console.ReadLine();
            bIsValidProductNumber = int.TryParse(sInput.Substring(1, 1), out int nNumber);
            bIsValidProductLetter = sInput.Substring(0, 1).ToUpper() == "A" ||
                                    sInput.Substring(0, 1).ToUpper() == "B" ||
                                    sInput.Substring(0, 1).ToUpper() == "C" ||
                                    sInput.Substring(0, 1).ToUpper() == "D";

            while (!bIsValidProductNumber || !bIsValidProductLetter || nNumber < 1 || nNumber > 4)
            {
                Console.WriteLine("Please enter a valid Product ID...   Format : LetterNumber");
                sInput = Console.ReadLine();
                bIsValidProductNumber = int.TryParse(sInput.Substring(1, 1), out nNumber);
                bIsValidProductLetter = sInput.Substring(0, 1).ToUpper() == "A" ||
                                        sInput.Substring(0, 1).ToUpper() == "B" ||
                                        sInput.Substring(0, 1).ToUpper() == "C" ||
                                        sInput.Substring(0, 1).ToUpper() == "D";
            }
            sInput = sInput.Substring(0, 1).ToUpper() + sInput.Substring(1, 1);

            if (VendoMatic600.GetSnackItems()[sInput].nAmount > 0)
            {
                VendoMatic600.Balance -= VendoMatic600.GetSnackItems()[sInput].dPrice;
                VendoMatic600.DecrementAmountOfSnack(VendoMatic600.GetSnackItems()[sInput], 1);
                Console.WriteLine(VendoMatic600.DispenseItem(VendoMatic600.GetSnackItems()[sInput]));
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("We Don't Have Any More, Go Away.");
            }
            using (StreamWriter newSW = new StreamWriter(sLogPath, true))
            {
                //write Date & Time
                newSW.Write(DateTime.Now.ToString() + " ");
                //write amount entered
                newSW.Write(VendoMatic600.GetSnackItems()[sInput].sName + " ");
                newSW.Write(VendoMatic600.GetSnackItems()[sInput].sLocation + " ");
                newSW.Write("$" + string.Format("{0:0.00}", VendoMatic600.GetSnackItems()[sInput].dPrice) + " ");
                //write balance                                
                newSW.Write($"${string.Format("{0:0.00}", VendoMatic600.Balance)}\n");
            }

        }

        public void Start(VendingMachine VendoMatic600, string sLogPath)
        {
            bool bFinished = false;
            while (!bFinished)
            {
                GenerateDisplayItemsMenu(VendoMatic600.GetSnackItems(), VendoMatic600);
                bool bValidSelection = int.TryParse(Console.ReadLine(), out int nSelection);
                while (!bValidSelection || nSelection > 4 || nSelection < 1)
                {
                    GenerateDisplayItemsMenu(VendoMatic600.GetSnackItems(), VendoMatic600);
                    Console.WriteLine("Please Enter a valid selection.");
                    bValidSelection = int.TryParse(Console.ReadLine(), out nSelection);
                }
                switch (nSelection)
                {
                    case 1:
                        {
                            Console.Clear();
                            GenerateDisplayItemsMenu(VendoMatic600.GetSnackItems(), VendoMatic600, false);
                            PromptInsertMoney(VendoMatic600, sLogPath);
                        }
                        break;
                    case 2:
                        {
                            if (!VendoMatic600.IsAllowedToPurchase())
                            {
                                Console.WriteLine("You cannot afford anything.");
                                Console.ReadKey();
                                break;
                            }
                            Console.Clear();
                            GenerateDisplayItemsMenu(VendoMatic600.GetSnackItems(), VendoMatic600, false);
                            RunPurchase(VendoMatic600, sLogPath);

                        }
                        break;
                    case 3:
                        {
                            Console.Clear();
                            RunTransaction(VendoMatic600, sLogPath);
                        }
                        break;
                    case 4:
                        {
                            bFinished = true;
                        }
                        break;
                }
            }
        }

        public void DisplayMainMenuOptions()
        {
            Console.WriteLine("1). Insert Money");
            Console.WriteLine("2). Select Product");
            Console.WriteLine("3). Finish Transaction");
            Console.WriteLine("4). Exit");
        }
    }
}
