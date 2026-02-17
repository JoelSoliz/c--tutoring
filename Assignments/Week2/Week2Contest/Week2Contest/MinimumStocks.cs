using System;
using System.Collections.Generic;

public class MinimumStocks
{
    public static void Main(string[] args) 
    {
        string days = Console.ReadLine();
        int N = int.Parse(days);
        // key: name, value: price
        Dictionary<string, int> stockMarket = new Dictionary<string, int>();

        for(int i = 0; i < N; i++)
        {
            string input = Console.ReadLine();
            string[] parsedInput = input.Split(" ");

            int instructionType = int.Parse(parsedInput[0]);

            if (instructionType == 1)
            {
                string stockName = parsedInput[1];
                int stockPrice = int.Parse(parsedInput[2]);

                stockMarket.Add(stockName, stockPrice);
            }
            else if (instructionType == 2)
            {
                string stockName = parsedInput[1];
                int stockPrice = int.Parse(parsedInput[2]);
                stockMarket[stockName] = stockPrice;
            }
            else 
            {
                string cheapestName = "";
                int cheapestPrice = int.MaxValue; // the infinit max value

                foreach (KeyValuePair<string, int> stock in stockMarket)
                {
                    if (stock.Value < cheapestPrice)
                    { 
                        cheapestName = stock.Key;
                        cheapestPrice = stock.Value;
                        
                    }
                }
                stockMarket.Remove(cheapestName);
                Console.WriteLine($"{cheapestName} {i + 1}");
            }
        }

    }
}