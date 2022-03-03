using System;
using System.Collections.Generic;
using DrinkVendingMachine.Models;

namespace DrinkVendingMachine.Models
{
    public partial class Memory
    {
        public int Id { get; set; }
        public int One { get; set; }
        public int Two { get; set; }
        public int Five { get; set; }
        public int Ten { get; set; }
        public int Sum { get; set; }

        public int ProductsIdProduct { get; set; }

        public virtual Products IdProductsNavigation { get; set; }

    }
}
