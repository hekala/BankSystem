using System;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BankA a = new BankA();
            BankB b = new BankB();
            BankC c = new BankC();

            a.GetExchangeRate("ad"); 
            a.GetExchangeRate("aud");
            a.GetExchangeRate("AUD");
            a.GetExchangeRate("austraalia");
            a.GetExchangeRate("gBp");          
            a.CalculateCurrency(5, "aud");           
            a.CalculateCurrency(5, "Austraalia"); 
            a.ExchangeCurrency(5, "aud");  
            a.FindMaxLoanAmount(); //no data entered
            a.AddCustomerData("Mari", 40, 500, 200); 
            a.FindMaxLoanAmount();       
            a.GetLoan(32600);
            a.GetLoan(2600);
            a.GetLoan(32400);
            a.GetLoan(400);            
            a.GetSmallLoan(400);
            a.WriteToFile(); //adds Mari's data to file            
            a.GetUserData("Jüri"); //no such user
            a.GetUserData("Mari");
            Console.WriteLine(); 
            
            b.FindCurrencies("peeso");
            b.FindCurrencies("Kroon");
            b.AddCustomerData("Kati", 40, 500, 200);
            b.FindMaxLoanAmount();
            b.FindSmallLoanAmount(2200, 2);         
            b.WriteToFile(); //adds Kati's data to file                   
            b.ExchangeCurrency(5, "AUD"); 
            b.ExchangeCurrency(25, "AUD");
            b.GetUserData("Kati");            
            Console.WriteLine(); 
            
            c.CalculateCurrency(10, "AUD", "TRY");
            c.ExchangeCurrency(25, "AUD");
        }
    }
}
