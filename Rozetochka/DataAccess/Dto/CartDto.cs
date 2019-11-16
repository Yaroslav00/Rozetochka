using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class CartDto
    {
        public DateTime Data { get; set; }

        public decimal TotalPrice { get; set; }

        public bool PaymentStatus { get; set; }

        public List<OrderedGoodDto> OrderedGoods { get; set; }
    }
}
