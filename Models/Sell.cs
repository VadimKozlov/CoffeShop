using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop2.Models
{
    public class Sell
    {
        public Guid SellId { get; set; } = Guid.NewGuid();

        public Guid CoffeeId { get; set; }

        public int Count { get; set; } = 0;

        public Double Price { get; set; }

        public DateTime Date { get; set; }
    }
}