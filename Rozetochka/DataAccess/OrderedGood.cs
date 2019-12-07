using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class OrderedGood
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Goods")]
        public int GoodsID { get; set; }

        public int Amount { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public int BuyerID { get; set; }

        public decimal CurrentPrice { get; set; }

        public virtual Goods Goods { get; set; }

        public virtual Order Order { get; set; }
    }
}
