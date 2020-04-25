using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.entity
{
    public abstract class Product
    {
        protected Product(string name, decimal price)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.price = price;
        }

        public String name { get; set; }

        public decimal price { get; set; }

        public abstract string GetGategoryName();
    }
}
