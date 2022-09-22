using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progi_test.Models
{
    public class Bid
    {
        public decimal Amount { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }

        public Bid(decimal amount, string user)
        {
            Amount = amount;
            User = user;
            CreatedAt = DateTime.Now;
        }
    }
}
