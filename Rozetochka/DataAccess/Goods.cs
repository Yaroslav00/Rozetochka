using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class Goods // товар
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public Goods(int ID, string Name, decimal Price, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
        }
    }
}
