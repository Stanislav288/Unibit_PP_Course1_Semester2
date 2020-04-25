using ConsoleApp1.entity.beverage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity
{
   public class Table
    {
        public Table(int number)
        {
            this.number = number;
            orders = new List<Product>();
            salesSumByGategory = new Dictionary<string, decimal>
            {
                { Salad.CATEGORY_NAME, 0 },
                { Soup.CATEGORY_NAME, 0 },
                { MainDish.CATEGORY_NAME, 0 },
                { Dessert.CATEGORY_NAME, 0 },
                { Beverage.CATEGORY_NAME, 0 }
            };

            salesCountByGategory = new Dictionary<string, int>
            {
                { Salad.CATEGORY_NAME, 0 },
                { Soup.CATEGORY_NAME, 0 },
                { MainDish.CATEGORY_NAME, 0 },
                { Dessert.CATEGORY_NAME, 0 },
                { Beverage.CATEGORY_NAME, 0 }
            };
        }

        public int number { get; set; }

        protected List<Product> orders { get;}

        public Dictionary<string, decimal> salesSumByGategory { get; protected set; }
        public Dictionary<string, int> salesCountByGategory { get; protected set; }
 
        public int NumberOfSales()
        {
            return orders.Count;
        }

        public decimal CalcSales()
        {
            decimal sum = 0;
            for (int i=0; i< orders.Count; i++)
            {
                sum += ((Product)orders.ElementAt(i)).price;
            }

            return sum;
        }

        public void AddOrder(Product product)
        {
            orders.Add(product);

            string category = product.GetGategoryName();

            salesSumByGategory[category] += product.price;
            salesCountByGategory[category] ++;
        }
    }
}
