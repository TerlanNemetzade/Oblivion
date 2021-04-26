using Entity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
 

namespace Entity.DataAccess.Context
{
    public class NorthwindContext:DbContext
    {
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=OBLIVION;initial catalog=Oblivion;integrated security=true; MultipleActiveResultSets=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDT { get; set; }

    }
}
