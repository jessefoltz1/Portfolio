using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using Capstone;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class CapstoneTests
    {
        [TestMethod]
        public void TestSnackSlot() //Test Snack slot is set correctly.
        {
            SnackItem Snack = new SnackItem(new string[] { "Z2", "Foobar", "7.89", "Ass" });
            Assert.AreEqual("Z2", Snack.sLocation);
        }

        [TestMethod]
        public void TestSnackName() //Test Snack Name is set correctly.
        {
            SnackItem Snack = new SnackItem(new string[] { "Z2", "Foodbar", "7.89", "Pretzel" });
            Assert.AreEqual("Foodbar", Snack.sName);
        }

        [TestMethod]
        public void TestSnackPrice() //Test Snack Cost is set correctly.
        {
            SnackItem Snack = new SnackItem(new string[] { "Z2", "Foobar", "7.89", "Horesdeovaries" });
            Assert.AreEqual(7.89m, Snack.dPrice);
        }

        [TestMethod]
        public void TestSnackType() //Test Snack Type is set correctly.
        {
            SnackItem Snack = new SnackItem(new string[] { "Z2", "Foobar", "7.89", "Hat" });
            Assert.AreEqual("Hat", Snack.sType);
        }

        [TestMethod]
        public void TestSnackAmountEqualsFive() //Test default amount is 5
        {
            SnackItem Snack = new SnackItem(new string[] { "Z2", "Foobar", "7.89", "Beer" });
            Assert.AreEqual(5, Snack.nAmount);
        }

        [TestMethod]
        public void TestSnackChangeIsCorrect1() //Test Quarters Multiple and Dime/Nickel Singular, and Change is Correct.
        {
            VendingMachine VendeezNuts = new VendingMachine();
            Assert.AreEqual("Change = 12 Quarters, 1 Dime, and 1 Nickel for a total of $3.15", VendeezNuts.GetChangeMessage(3.15m));
        }

        [TestMethod]
        public void TestSnackChangeIsCorrect2() //Test Quarter Singular and Dimes is 0 and Change is Correct.
        {
            VendingMachine VendeezNuts = new VendingMachine();
            Assert.AreEqual("Change = 1 Quarter, 0 Dimes, and 1 Nickel for a total of $0.30", VendeezNuts.GetChangeMessage(0.30m));
        }

        [TestMethod]
        public void TestSnackChangeIsCorrect3() //Additional Change is Correct with larger input.
        {
            VendingMachine TestVendor = new VendingMachine();
            Assert.AreEqual("Change = 67 Quarters, 1 Dime, and 0 Nickels for a total of $16.85", TestVendor.GetChangeMessage(16.85m));
        }

        [TestMethod]
        public void TestDispenseItemChip() // Test if correct dispense message is sent.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak = new SnackItem(new string[] { "d4", "CheeseCar", "2.00", "Chip" });
            Assert.AreEqual("Crunch Crunch, Yum!", TestVendor.DispenseItem(snak));
            ; }
        [TestMethod]
        public void TestDispenseItemGum() // Test if correct dispense message is sent.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak = new SnackItem(new string[] { "d4", "CheeseCar", "2.00", "Gum" });
            Assert.AreEqual("Chew Chew, Yum!", TestVendor.DispenseItem(snak));
        }

        [TestMethod]
        public void TestDispenseItemDrink() // Test if correct dispense message is sent.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak = new SnackItem(new string[] { "d4", "CheeseCar", "2.00", "Drink" });
            Assert.AreEqual("Glug Glug, Yum!", TestVendor.DispenseItem(snak));
        }

        [TestMethod]
        public void TestDispenseItemCandy() // Test if correct dispense message is sent.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak = new SnackItem(new string[] { "d4", "CheeseCar", "2.00", "Candy" });
            Assert.AreEqual("Munch Munch, Yum!", TestVendor.DispenseItem(snak));
        }

        [TestMethod]
        public void TestInsertMoney()// Test if valid entry for values inserted to machine
        {
            VendingMachine TestVendor = new VendingMachine();
            TestVendor.InsertMoney(12);
            Assert.AreEqual(12.00M, TestVendor.Balance);
        }

        [TestMethod]
        public void TestInventoryAddSnack()// Test if item exists in dictionary after adding 
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak = new SnackItem(new string[] { "d4", "CheeseCar", "2.00", "Candy" });
            TestVendor.AddItemToMachine(snak);
            Assert.AreEqual(snak, TestVendor.GetSnackItems()["d4"]);
        }


        [TestMethod]
        public void TestCheapestSnack()// Tests if correct cheapest price is returned.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak1 = new SnackItem(new string[] { "d1", "Dinger", "2.50", "Candy" });
            SnackItem snak2 = new SnackItem(new string[] { "b3", "HooHoo", "1.35", "Chip" });
            SnackItem snak3 = new SnackItem(new string[] { "a2", "Dilly Willy", "0.45", "Soda" });
            TestVendor.AddItemToMachine(snak1);
            TestVendor.AddItemToMachine(snak2);
            TestVendor.AddItemToMachine(snak3);
            Assert.AreEqual(0.45m, TestVendor.GetCheapestPrice(TestVendor.GetSnackItems()));
        }

        [TestMethod]
        public void TestGetSnackItems()//Test if corrected dictionary is retrieved.
        {
            VendingMachine TestVendor = new VendingMachine();
            SnackItem snak1 = new SnackItem(new string[] { "d1", "Dinger", "2.50", "Candy" });
            SnackItem snak2 = new SnackItem(new string[] { "b3", "HooHoo", "1.35", "Chip" });
            SnackItem snak3 = new SnackItem(new string[] { "a2", "Dilly Willy", "0.45", "Soda" });
            TestVendor.AddItemToMachine(snak1);
            TestVendor.AddItemToMachine(snak2);
            TestVendor.AddItemToMachine(snak3);
            Dictionary<string, SnackItem> TestDict = new Dictionary<string, SnackItem>()
            {
                {snak1.sLocation, snak1},
                {snak2.sLocation, snak2},
                {snak3.sLocation, snak3}
            };
            CollectionAssert.AreEquivalent(TestDict, TestVendor.GetSnackItems());
        }

        [TestMethod]
        public void TestBalance() // Tests if Balance is correct for entered values.
        {
            VendingMachine TestVendor = new VendingMachine();
            TestVendor.InsertMoney(12);
            TestVendor.InsertMoney(15);
            Assert.AreEqual(27.00M, TestVendor.Balance);
        }

        [TestMethod]
        public void TestIsAllowedToPurchaseFalse()// Test if balance is enough to make a purchase.
        {
            VendingMachine TestVendor = new VendingMachine();
            TestVendor.InsertMoney(1);
            SnackItem snak1 = new SnackItem(new string[] { "d1", "Dinger", "2.50", "Candy" });
            SnackItem snak2 = new SnackItem(new string[] { "b3", "HooHoo", "1.35", "Chip" });
            TestVendor.AddItemToMachine(snak1);
            TestVendor.AddItemToMachine(snak2);
            Assert.IsFalse(TestVendor.IsAllowedToPurchase());
           
        }

        [TestMethod]
        public void TestIsAllowedToPurchaseTrue()// Test if balance is enough to make a purchase.
        {
            VendingMachine TestVendor = new VendingMachine();
            TestVendor.InsertMoney(2);
            SnackItem snak1 = new SnackItem(new string[] { "d1", "Dinger", "2.50", "Candy" });
            SnackItem snak2 = new SnackItem(new string[] { "b3", "HooHoo", "1.35", "Chip" });
            TestVendor.AddItemToMachine(snak1);
            TestVendor.AddItemToMachine(snak2);
            Assert.IsTrue(TestVendor.IsAllowedToPurchase());
        }

    }
}
