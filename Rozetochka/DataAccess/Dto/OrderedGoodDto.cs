using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class OrderedGoodDto
    {
        public int ID { get; set; }

        public int GoodsID { get; set; }

        public int Amount { get; set; }

        public int OrderID { get; set; }

        public int BuyerID { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}
