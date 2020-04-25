using ConsoleApp1.appinterface;
using ConsoleApp1.entity.food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity
{
    public class Dessert : Food, ICalories
    {
        public const string CATEGORY_NAME = "десерт";
        public Dessert(string name, int grams, decimal price) : base(name, grams, price)
        {
            this.callories = 3 * grams; 
        }

        public double callories { get; set; }

        public override string GetGategoryName()
        {
            return CATEGORY_NAME;
        }
    }
}
