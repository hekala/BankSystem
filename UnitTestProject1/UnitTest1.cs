using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankSystem;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private BankA a = new BankA();
        private BankB b = new BankB();
        private BankC c = new BankC();
        
        [TestMethod]
        public void Test_IsCountryNameValid_A()
        {
            bool result = a.IsCountryNameValid("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_IsCountryNameValid_AUD()
        {
            bool result = a.IsCountryNameValid("aUd");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_IsCountryNameValid_Austraalia()
        {
            bool result = a.IsCountryNameValid("austraalia");
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void Test_GetExchageRate_Country_Aud()
        {
            string result = a.GetExchangeRate("aud");
            Assert.AreEqual("1,6275AUD", result);
        }

        [TestMethod]
        public void Test_GetExchageRate_GBP()
        {
            string result = a.GetExchangeRate("gbp");
            Assert.AreEqual("0,8407GBP", result);
        } 
        
        [TestMethod]
        public void Test_CalculateCurrency_AUD()
        {
            string result = a.CalculateCurrency(5, "AUD");
            Assert.AreEqual("8,1375AUD", result);
        }

        [TestMethod]
        public void Test_CalculateCurrency_Austraalia()
        {
            string result = a.CalculateCurrency(5, "Austraalia");
            Assert.AreEqual("8,1375AUD", result);
        }

        [TestMethod]
        public void Test_ExchangeCurrency_5()
        {
            double result = a.ExchangeCurrency(5, "AUD");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_IsDataEntered_NoData()
        {
            bool result = a.IsCustomerDataEntered();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_IsDataEntered()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            bool result = a.IsCustomerDataEntered();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CanGetLoan_32600()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            a.FindMaxLoanAmount();
            bool result = a.CanGetLoan(32600);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CanGetLoan_2600()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            a.FindMaxLoanAmount();
            bool result = a.CanGetLoan(2600);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CanGetLoan_2500()
        {
            a.AddCustomerData("Kati", 40, 500, 200);            
            bool result = a.CanGetLoan(2500);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_FindMaxLoanAmount_32400()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            double result = a.FindMaxLoanAmount();
            Assert.AreEqual(32400, result);
        }

        [TestMethod]
        public void Test_FindMaxLoanAmount_32400_B()
        {
            b.AddCustomerData("Mari", 40, 500, 200);
            double result = b.FindMaxLoanAmount();
            Assert.AreEqual(22500, result);
        }

        [TestMethod]
        public void Test_GetLoan_2600()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            a.FindMaxLoanAmount();
            double result = a.GetLoan(2600);
            Assert.AreEqual(2600, result);
        }

        [TestMethod]
        public void Test_GetSmallLoan_400()
        {
            a.AddCustomerData("Mari", 40, 500, 200);                       
            double result = a.GetSmallLoan(400);
            Assert.AreEqual(400, result);
        }

        [TestMethod]
        public void Test_CalculateCurrency_C()
        {
            string result = c.CalculateCurrency(10, "AUD", "TRY");
            Assert.AreEqual("39,51TRY", result);
        }       

        [TestMethod]
        public void Test_GetUserData_Mari()
        {
            a.AddCustomerData("Mari", 40, 500, 200);
            a.WriteToFile();
            string result = a.GetUserData("Mari");
            Assert.AreEqual("Mari, 40, 500, 200, 0", result);
        }  
        
        [TestMethod]
        public void Test_FindSmallLoanAmount_2200()
        {
            double result = b.FindSmallLoanAmount(2200, 2);
            Assert.AreEqual(2293,3702, result);
        }

        [TestMethod]
        public void Test_FindSmallLoanAmount_0()
        {
            double result = b.FindSmallLoanAmount(2200, 21);
            Assert.AreEqual(0, result);
        }        
    }
}
