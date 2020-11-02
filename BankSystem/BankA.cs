using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BankSystem
{
    public class BankA : IBankSystem
    {
        internal List<string> _currencyCodes = new List<string>();
        internal List<string> _exchangeRates = new List<string>();
        internal List<string> _countryNames = new List<string>();
        internal List<string> _currencyNames = new List<string>();
        internal string _currencyCode;
        internal string _exchangeRate;
        internal string _countryName;
        internal string _currencyName;

        internal string _customerName;
        internal int _customerAge;
        internal int _monthlySalary;
        internal int _monthlyObligations;

        internal double _serviceFee;
        internal int _maxLoanTime;
        internal double _maxLoanAmountPercent;
        internal double _interestRate;
        internal double _maxLoanAmount;
        internal double _loanTotal;      

        public BankA()
        {
            ReadValuesFromFile();
            _serviceFee = 4;
            _maxLoanTime = 30;
            _maxLoanAmountPercent = 0.3; //30%
            _interestRate = 1.2;
        }
        public void ReadValuesFromFile()
        {
            using (StreamReader reader = new StreamReader("currency.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    _currencyCode = line.Split(' ')[0];
                    _countryName = line.Split(' ')[1];
                    _currencyName = line.Split(' ')[2];
                    _exchangeRate = line.Split(' ').Last();
                    _exchangeRate = _exchangeRate.Replace('.', ',');
                    _currencyCodes.Add(_currencyCode);
                    _countryNames.Add(_countryName);
                    _currencyNames.Add(_currencyName);
                    _exchangeRates.Add(_exchangeRate);
                }
            }
        }
        public bool IsCountryNameValid(string country)
        {
            if (country.Length >= 3) //has to be at least 3 letters
            {
                if (_countryNames.Contains(FirstCharToUpper(country)))
                {
                    return true;
                }
                if (_currencyCodes.Contains(country.ToUpper()))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Name is not on the list!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid. Country name or currency code must be at least 3 letters!");
                return false;
            }
        }
        public string FirstCharToUpper(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1);
        }
        public void GetExchangeRateByName(string country)
        {
            if (IsCountryNameValid(country))
            {
                if (country.Length == 3) //entered currencycode
                {
                    int index = _currencyCodes.IndexOf(country.ToUpper());
                    _currencyCode = _currencyCodes[index];
                    _exchangeRate = _exchangeRates[index];
                }
                else //entered countryname
                {
                    int index = _countryNames.IndexOf(FirstCharToUpper(country));
                    _currencyCode = _currencyCodes[index];
                    _exchangeRate = _exchangeRates[index];
                }
            }
        }
        public string GetExchangeRate(string country)
        {
            if (IsCountryNameValid(country))
            {
                GetExchangeRateByName(country);
                Console.WriteLine("1 EUR is {0} {1}", _exchangeRate, _currencyCode);
                return _exchangeRate + _currencyCode;
            }
            return "";
        }
        public string CalculateCurrency(double amountToExchange, string country)
        {
            if (IsCountryNameValid(country))
            {
                GetExchangeRateByName(country);
                double total = amountToExchange * double.Parse(_exchangeRate);
                Console.WriteLine("{0} EUR gives you {1} {2}", amountToExchange, total, _currencyCode);
                return total + _currencyCode;
            }
            return "";
        }
        public virtual double ExchangeCurrency(double amountToExchange, string country)
        {
            if (IsCountryNameValid(country))
            {
                GetExchangeRateByName(country);
                double exchangeMoney = (amountToExchange - _serviceFee) * double.Parse(_exchangeRate);
                exchangeMoney = Math.Floor(exchangeMoney / 1.0) * 1.0; //rounds down 1.0 
                Console.WriteLine("Money exchanged! Service fee is {0} EUR. {1} EUR gives you {2} {3}", _serviceFee, amountToExchange, exchangeMoney, _currencyCode);
                return exchangeMoney;
            }
            return 0;
        }
        public void AddCustomerData(string name, int age, int monthlyIncome, int monthlyObligations)
        {
            if (name.Length >= 4)
            {
                _customerName = name;
                _customerAge = age;
                _monthlySalary = monthlyIncome;
                _monthlyObligations = monthlyObligations;
            }
            else
            {
                Console.WriteLine("Minimum length for name is 4!");
            }
        }
        public bool IsCustomerDataEntered() 
        {
            if (_customerName != null && _customerAge != 0 && _monthlySalary != 0 && _monthlyObligations != 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("No customer data entered! Please add data!");
                return false;
            }
        }
        public virtual double FindMaxLoanAmount()
        {
            if (IsCustomerDataEntered())
            {
                double maxMonthlyPayment = (_monthlySalary - _monthlyObligations) * _maxLoanAmountPercent;
                if ((_monthlySalary - _monthlyObligations - maxMonthlyPayment) >= 200) //at least 200euros on account after monthly payment
                {
                    _maxLoanAmount = maxMonthlyPayment * 12 * _maxLoanTime;
                    Console.WriteLine("Max loan amount is {0}", _maxLoanAmount);
                    return _maxLoanAmount;
                }
                else
                {
                    Console.WriteLine("Cannot get loan, less than 200 on account after monthly payment!");
                }
            }
            return 0;
        }
        public bool CanGetLoan(int loanAmount)
        {
            if (IsCustomerDataEntered())
            {
                if (_maxLoanAmount != 0) //checks if max loan amount has been calculated w/previous method
                {
                    if (loanAmount <= _maxLoanAmount)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Too big amount, maximum is {0}", _maxLoanAmount);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Please calculate your maximum loan amount!");
                    return false;
                }                
            }
            return false;
        }
        public double GetLoan(int loanAmount)
        {
            if (IsCustomerDataEntered())
            {
                if (CanGetLoan(loanAmount))
                {
                    _loanTotal += loanAmount;
                    _maxLoanAmount -= loanAmount;
                    Console.WriteLine("Gave loan in sum {0}, loan total is {1}", loanAmount, _loanTotal);
                    return _loanTotal;
                }
            }
            return 0;
        }
        public double FindSmallLoanAmount(int loanAmount, int years)
        {
            if (years <= 20)
            {
                double smallLoanTotal = 0;
                for (int i = 0; i <= years; i++)
                {
                    smallLoanTotal = loanAmount * Math.Pow((1 + _interestRate / 100), years);
                }
                Console.WriteLine("Total amount is {0}", smallLoanTotal);
                return smallLoanTotal;
            }
            else
            {
                Console.WriteLine("Upper limit for years is 20!");
                return 0;
            }
        }
        public double GetSmallLoan(int loanAmount)
        {
            if (IsCustomerDataEntered())
            {
                _loanTotal += loanAmount;
                Console.WriteLine("Gave loan in sum {0}, loan total is {1}", loanAmount, _loanTotal);
                return _loanTotal;
            }
            return 0;
        }
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter("usersData.txt", true))
            {
                writer.WriteLine("{0}, {1}, {2}, {3}, {4}", _customerName, _customerAge, _monthlySalary, _monthlyObligations, _loanTotal);
            }
        }
        public string GetUserData(string userName)
        {
            string userInfo = "";
            using (StreamReader reader = new StreamReader("usersData.txt"))
            {
                while (!reader.EndOfStream)
                {
                    userInfo = reader.ReadLine();                    
                }
                if (userInfo.Contains(userName))
                {
                    Console.WriteLine(userInfo);
                    return userInfo;
                }
                else
                {
                    Console.WriteLine("No user with name {0}!", userName);
                }
            }            
            return "";
        }
    }
}


