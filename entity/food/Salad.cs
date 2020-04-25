using ConsoleApp1.entity.food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity
{
    public class Salad : Food
    {
        public const string CATEGORY_NAME = "салата";
        public Salad(string name, int grams ,decimal price) : base(name, grams, price)
        {
        }
        public override string GetGategoryName()
        {
            return CATEGORY_NAME;
        }
    }
}
