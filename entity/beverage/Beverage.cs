using ConsoleApp1.appinterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity.beverage
{
    public class Beverage : Product, ICalories
    {
        public const string CATEGORY_NAME = "напитка";
        public Beverage(string name, int milliliters, decimal price) : base(name, price)
        {
            this.milliliters = milliliters;
            this.callories = 1.5 * callories;
        }

        public int milliliters { get; set; }
        public double callories { get; set; }

        public override string GetGategoryName()
        {
            return CATEGORY_NAME;
        }
    }
}
