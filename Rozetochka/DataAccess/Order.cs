using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
        [ForeignKey("User")]
        public int BuyerID { get; set; }
        public List<OrderedGood> OrderedGood { get; set; }
        public virtual User User { get; set; }
    }
}
