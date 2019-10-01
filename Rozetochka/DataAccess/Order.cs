using DataAccess.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
