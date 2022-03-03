using System;
using System.Collections.Generic;

namespace DrinkVendingMachine.Models
{
    public partial class Coins
    {
        public int Id { get; set; }
        public int? One { get; set; }
        public int? Two { get; set; }
        public int? Five { get; set; }
        public int? Ten { get; set; }
    }
}
