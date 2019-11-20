using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public decimal Balance { get; private set; } = 0.0M;
        Dictionary<string, SnackItem> VendItems = new Dictionary<string, SnackItem>();


        public void FillMyMachine(string sFilePath)
        {            
            //List<string> sList = new List<string>();
            using (StreamReader newSR = new StreamReader(sFilePath))
            {
                while (!newSR.EndOfStream)
                {
                    string sLine = newSR.ReadLine();
                    string[] saStringArray = sLine.Split('|');
                    SnackItem siSnack = new SnackItem(saStringArray);
                    AddItemToMachine(siSnack);
                }
            }
        }

        public void AddItemToMachine(SnackItem Snack)
        {
            VendItems.Add(Snack.sLocation, Snack);
        }

        public void IncrementAmountOfSnack(SnackItem Snack, int nAmount)
        {
            Snack.nAmount += nAmount;
        }

        public void DecrementAmountOfSnack(SnackItem Snack, int nAmount)
        {
            Snack.nAmount -= nAmount;
        }

        public Dictionary<string, SnackItem> GetSnackItems()
        {
            return VendItems;
        }

        public void InsertMoney(decimal dAmount)
        {
            Balance += dAmount;
        }

        public void DecreaseMoney(decimal dAmount)
        {
            Balance -= dAmount;
        }

        public bool IsAllowedToPurchase()
        {
            bool result = true;
            
            decimal dCheapest = GetCheapestPrice(VendItems);
            if (Balance < dCheapest)
            {
                result = false;
            }

            return result;
        }

        public decimal GetCheapestPrice(Dictionary<string, SnackItem> Snacks)
        {
            decimal dCheapest = -1.0m;
            foreach (string snack in Snacks.Keys)
            {   
                if (dCheapest == -1.0m)
                {
                    dCheapest = Snacks[snack].dPrice;
                }

                if (Snacks[snack].dPrice <= dCheapest)
                {
                    dCheapest = Snacks[snack].dPrice;
                }                  
            }
            return dCheapest;
        }

        public string GetChangeMessage(decimal balance)
        {
            string result = "";
            decimal iOriginalBalance = balance;
            string sQuarters = "Quarter";
            string sDimes = "Dime";
            string sNickles = "Nickel";
            int iQuarters = (int)(balance / 0.25m);
            if (iQuarters > 0)
            {
                balance -= 0.25m * iQuarters;
            }
            int iDimes = (int)(balance / 0.1m);
            if (iDimes > 0)
            {
                balance -= 0.10m * iDimes;
            }
            int iNickels = (int)(balance / 0.05m);
            if (iNickels > 0)
            {
                balance -= 0.05m * iNickels;
            }
            if (iQuarters != 1)
            {
                sQuarters = "Quarters";
            }
            if (iDimes != 1)
            {
                sDimes = "Dimes";
            }
            if (iNickels != 1)
            {
                sNickles = "Nickels";
            }
            result = $"Change = {iQuarters} {sQuarters}, {iDimes} {sDimes}, and {iNickels} {sNickles} for a total of ${string.Format("{0:0.00}", iOriginalBalance)}";
            return result;
        }

        public string DispenseItem(SnackItem Snack)
        {
            string result = "";
            switch (Snack.sType)
            {
                case "Chip": result = "Crunch Crunch, Yum!\n" + ".~~~~~. \n" +
                                                                "|-----|\n" +
                                                                "|CHIPS|\n" +
                                                                "|-----|\n" +
                                                                "|_____|\n"; 
                                                                
                    break;
                case "Candy": result = "Munch Munch, Yum!\n" +  "       \n" +
                                                                " .---. \n" +
                                                                "(CANDY)\n" +
                                                                " '---' \n" +
                                                                "       ";
                    break;
                case "Drink": result = "Glug Glug, Yum!\n" + " ____ \n"+
                                                            "[____] \n" +
                                                            "|SODA| \n" +
                                                            "|____| \n"+
                                                            "[____] ";
                    break;
                case "Gum": result = "Chew Chew, Yum!\n" + "       \n"+
                                                           " .---. \n"+
                                                           "| GUM |\n"+
                                                           "'-----'\n"+
                                                           "      ";
                    break;
            }
            return result;
        }


    }
}
