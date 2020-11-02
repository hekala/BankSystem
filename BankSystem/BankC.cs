using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    public class BankC : BankB 
    {      
        public string CalculateCurrency(int amount, string startingCurrency, string destinationCurrency) 
        {           
            if (IsCountryNameValid(startingCurrency) && IsCountryNameValid(destinationCurrency)) 
            {                
                GetExchangeRateByName(startingCurrency);
                int index = _currencyCodes.IndexOf(destinationCurrency); 
                string destinationRate = _exchangeRates[index]; //searches for the same index in rates list
                double totalInDestinationCurrency = amount / double.Parse(_exchangeRate) * double.Parse(destinationRate);
                totalInDestinationCurrency = Math.Round(totalInDestinationCurrency * 100) / 100; //rounds down to 2 dec places               
                Console.WriteLine("{0} {1} gives you {2} {3}.", amount, startingCurrency, totalInDestinationCurrency, destinationCurrency);
                return totalInDestinationCurrency + destinationCurrency;
            }     
            return "";
        }        
        public override double ExchangeCurrency(double amountToExchange, string country)
        {
            if (IsCountryNameValid(country))
            {
                DayOfWeek today = DateTime.Today.DayOfWeek;
                if (today == DayOfWeek.Tuesday) //no fees applied
                {
                    GetExchangeRateByName(country);
                    double exchangeMoney = amountToExchange * double.Parse(_exchangeRate);
                    exchangeMoney = Math.Floor(exchangeMoney / 1.0) * 1.0; //rounds down 1.0 
                    Console.WriteLine("Money exchanged! Today is {0}, no fees applied! {1} EUR gives you {2} {3}", today, amountToExchange, exchangeMoney, _currencyCode);
                }
                else
                {
                    base.ExchangeCurrency(amountToExchange, country); //same as bank B                    
                }               
            }
            return 0;
        }   
    }
}
