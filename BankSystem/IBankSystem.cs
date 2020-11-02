using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    interface IBankSystem
    {

        string GetExchangeRate(string country);
        string CalculateCurrency(double amountToExchange, string country);
        double ExchangeCurrency(double amountToExchange, string country);
        void AddCustomerData(string name, int age, int monthlyIncome, int monthlyObligations);
        double FindMaxLoanAmount();
        bool CanGetLoan(int sum);
        double GetLoan(int loan);
        double FindSmallLoanAmount(int loanAmount, int years);
        double GetSmallLoan(int loanAmount);
        void WriteToFile();
        string GetUserData(string userName);
    }
}
