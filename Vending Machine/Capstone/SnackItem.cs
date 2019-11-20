using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class SnackItem
    {
        public string sLocation { get; }
        public string sName { get; }
        public decimal dPrice { get; }
        public string sType { get; }
        public int nAmount { get; set; }

        public SnackItem(string[] saData)
        {
            sLocation = saData[0];
            sName     = saData[1];
            dPrice    = decimal.Parse(saData[2]);
            sType     = saData[3];
            nAmount = 5;
        }
    }
}
