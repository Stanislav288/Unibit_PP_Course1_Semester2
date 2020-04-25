using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity.food
{
    public abstract class Food : Product
    {
        protected Food(string name,int grams, decimal price) : base(name, price)
        {
            this.grams = grams;
        }

        public int grams { get; set; }
    }
}
