using System;
using System.IO;
using System.Collections.Generic;
using Capstone.Classes;
using System.Text;
namespace Capstone
{
    class Program
    {        
        static void Main(string[] args)
        {
            VendingMachine VendoMatic600 = new VendingMachine();
            VendingMachineCLI VCLI = new VendingMachineCLI();
            VendoMatic600.FillMyMachine(@"..\..\..\..\VendingMachine.txt");
            VCLI.Start(VendoMatic600, @"..\..\..\..\Log.txt");
        }
    }
}
