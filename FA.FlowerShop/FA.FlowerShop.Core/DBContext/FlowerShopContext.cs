using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.FlowerShop.Core
{
    public class FlowerShopContext : DbContext
    {

        public FlowerShopContext() : base("FlowerShopDb")
        {
            Database.SetInitializer(new FlowerShopInitializer());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Flower> Flowers { get; set; }
    }
}
