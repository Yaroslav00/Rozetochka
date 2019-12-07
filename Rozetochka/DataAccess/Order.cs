using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
        public List<OrderedGood> OrderedGood { get; set; }
    }
}
