using ConsoleApp1.entity;
using ConsoleApp1.entity.beverage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CommandDispatcher
    {
        private static IDictionary tables;

        private static IDictionary categoryProducts; 

        private static readonly CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
        public CommandDispatcher()
        {
            tables = new Dictionary<int, Table>();
            //CategoryName - ProductName - Product class
            categoryProducts = new Dictionary<string, Dictionary<string, Product>>
            {
                { Salad.CATEGORY_NAME, new Dictionary<string, Product>() },
                { Soup.CATEGORY_NAME, new Dictionary<string, Product>() },
                { MainDish.CATEGORY_NAME, new Dictionary<string, Product>() },
                { Dessert.CATEGORY_NAME, new Dictionary<string, Product>() },
                { Beverage.CATEGORY_NAME, new Dictionary<string, Product>() }
            };
        }

        public void ProcessCommand(string input)
        {
            string[] inputArgs = input.Split(new[] { ", " }, StringSplitOptions.None);

            string command = inputArgs[0];
            if ("продажби".Equals(command))
            {
                PrintSales();
                return;
            }
            else if ("изход".Equals(command))
            {
                PrintSales();
                Thread.Sleep(60000);
                Environment.Exit(0);
            }

            if (!Regex.IsMatch(command, @"^\d+$"))
            {
                //Creation of product
                string category = command;
                string productName = inputArgs[1];
                int quantity = int.Parse(inputArgs[2]);
                decimal price = decimal.Parse(inputArgs[3], cultureInfo);

                if (!((Dictionary<string, Product>)categoryProducts[category]).ContainsKey(productName))
                {
                    switch (category)
                    {
                      case Salad.CATEGORY_NAME: ((Dictionary<string, Product>)categoryProducts[category]).Add(productName, new Salad(productName, quantity ,price));break;
                      case Soup.CATEGORY_NAME: ((Dictionary<string, Product>)categoryProducts[category]).Add(productName, new Soup(productName, quantity, price)); break;
                      case MainDish.CATEGORY_NAME: ((Dictionary<string, Product>)categoryProducts[category]).Add(productName, new MainDish(productName, quantity, price)); break;
                      case Dessert.CATEGORY_NAME: ((Dictionary<string, Product>)categoryProducts[category]).Add(productName, new Dessert(productName, quantity, price)); break;
                      case Beverage.CATEGORY_NAME: ((Dictionary<string, Product>)categoryProducts[category]).Add(productName, new Beverage(productName, quantity, price)); break;
                    }               
                }


            }
            else
            {
                //Order for table
                int tableNumber = int.Parse(command);
                Table table;
                if (!tables.Contains(tableNumber))
                {
                    table = new Table(tableNumber);
                    tables.Add(tableNumber, table);
                }
                else
                {
                    table = ((Table)tables[tableNumber]);
                }

                for (int i = 1; i < inputArgs.Length; i++)
                {
                    table.AddOrder(GetProductByName(inputArgs[i]));
                }
            }
        }

        private void PrintSales()
        {
            Console.WriteLine("Общо заети маси през деня: {0}", tables.Count);
            Console.WriteLine("Общо продажби: {0} - {1}", CalcTablesSalesCount(), CalcTablesSalesSum());
            this.PrintInfoByCategories();
        }


        private decimal CalcTablesSalesCount()
        {
            decimal numberOfSales = 0;
            foreach (DictionaryEntry table in tables)
            {
                numberOfSales += ((Table)table.Value).NumberOfSales();
            }

            return numberOfSales;
        }
        private decimal CalcTablesSalesSum()
        {
            decimal sum = 0;
            foreach (DictionaryEntry table in tables) 
            { 
                sum += ((Table)table.Value).CalcSales();
            }

            return sum;
        }


        private void PrintInfoByCategories()
        {
            Console.WriteLine("По категории:");
            Console.WriteLine(" - Салата: {0} - {1}", CalcProductCountByCategory(Salad.CATEGORY_NAME), CalcProductSumByCategory(Salad.CATEGORY_NAME));
            Console.WriteLine(" - Супа: {0} - {1}", CalcProductCountByCategory(Soup.CATEGORY_NAME), CalcProductSumByCategory(Soup.CATEGORY_NAME));
            Console.WriteLine(" - Осново ястие: {0} - {1}", CalcProductCountByCategory(MainDish.CATEGORY_NAME), CalcProductSumByCategory(MainDish.CATEGORY_NAME));
            Console.WriteLine(" - Десерт: {0} - {1}", CalcProductCountByCategory(Dessert.CATEGORY_NAME), CalcProductSumByCategory(Dessert.CATEGORY_NAME));
            Console.WriteLine(" - Напитка: {0} - {1}", CalcProductCountByCategory(Beverage.CATEGORY_NAME), CalcProductSumByCategory(Beverage.CATEGORY_NAME));
        }

        private int CalcProductCountByCategory(String category)
        {
            int totalCount = 0;
            foreach (DictionaryEntry table in tables)
            {
                totalCount += ((Table)table.Value).salesCountByGategory[category];
            }

            return totalCount;
        }

        private decimal CalcProductSumByCategory(String category)
        {
            decimal totalSum = 0;
            foreach (DictionaryEntry table in tables)
            {
                totalSum += ((Table)table.Value).salesSumByGategory[category];
            }

            return totalSum;
        }

        private Product GetProductByName(String productName)
        {//new Dictionary<string, Dictionary<string, Product>>();
            foreach (DictionaryEntry category in categoryProducts) 
            {
                if (((Dictionary<string, Product>)category.Value).TryGetValue(productName, out Product product))
                {
                    return product;
                }
            }

            return null;
        }
        public Product GetProductByGategory(String productName)
        {//new Dictionary<string, Dictionary<string, Product>>();
            foreach (DictionaryEntry category in categoryProducts)
            {
                if (((Dictionary<string, Product>)category.Value).TryGetValue(productName, out Product product))
                {
                    return product;
                }
            }

            return null;
        }

    }
}
