using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataAccess.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Goods> Merchandise { get; set; }

        public DbSet<OrderedGood> PurchaseGoods { get; set; }

        public DbSet<Order> Purchases { get; set; }

        public DbSet<User> ShopUsers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}