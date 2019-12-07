using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CartedGoodDTO
    {
        public int Amount { get; set; }
        public virtual Goods Goods { get; set; }
    }
}
