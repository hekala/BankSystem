using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BankSystem
{
    public class BankB : BankA 
    {
        internal int _maxLoanAge;         
        public BankB()
        {
            _serviceFee = 2;
            _maxLoanAmountPercent = 0.25;
            _maxLoanAge = 65;
            _interestRate = 2.1;
        }
        public override double ExchangeCurrency(double amountToExchange, string country)
        {
            if (IsCountryNameValid(country))
            {
                if (amountToExchange >= 20) //minimal amount 20 eur
                {
                    GetExchangeRateByName(country);
                    _serviceFee = _serviceFee + 0.1 * (amountToExchange - _serviceFee);
                    double exchangeMoney = (amountToExchange - _serviceFee) * double.Parse(_exchangeRate);
                    exchangeMoney = Math.Floor(exchangeMoney / 1.0) * 1.0; //rounds down 1.0 
                    Console.WriteLine("Money exchanged! Service fee is {0}. {1} euros gives you {2} {3}", _serviceFee, amountToExchange, exchangeMoney, _currencyCode);
                    return exchangeMoney;
                }
                else
                {
                    Console.WriteLine("Too small amount! Minimal is 20 EUR!");
                    return 0;
                }
            }
            return 0;
        }
        public void FindCurrencies(string currencyName) 
        {
            if (_currencyNames.Contains(currencyName.ToLower())) //if name is on the list
            {
                List<string> countriesAndCurrencies = new List<string>();
                Console.WriteLine("Currencies with name {0} are: ", currencyName.ToLower());
                using (StreamReader reader = new StreamReader("currency.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string countryAndCurrency = line.Split(' ')[1] + " " + line.Split(' ')[2];                        
                        countriesAndCurrencies.Add(countryAndCurrency);                        
                    }
                }
                for (int i = 0; i < countriesAndCurrencies.Count; i++)
                {
                    if (countriesAndCurrencies[i].Contains(currencyName.ToLower()))
                    {
                        string row = countriesAndCurrencies[i];                        
                        Console.WriteLine("{0}", row);                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Currencyname is not on the list!");                
            }            
        }      
        public override double FindMaxLoanAmount() 
        {
            if (IsCustomerDataEntered())
            {
                if (_customerAge <= _maxLoanAge) //if < 65
                {
                    _maxLoanTime = _maxLoanAge - _customerAge; 
                    double maxMonthlyPayment = (_monthlySalary - _monthlyObligations) * _maxLoanAmountPercent;
                    if ((_monthlySalary - _monthlyObligations - maxMonthlyPayment) >= 200)
                    {
                        _maxLoanAmount  = maxMonthlyPayment * 12 * _maxLoanTime;
                        Console.WriteLine("Max loan amount is {0}", _maxLoanAmount);
                        return _maxLoanAmount;
                    }
                    else
                    {
                        Console.WriteLine("Cannot get loan, less than 200 on account after monthly payment!");
                    }
                }
                else
                {
                    Console.WriteLine("Age limit for loan is {0} years", _maxLoanAge);                    
                }             
            }            
            return 0;
        }
    }
}
