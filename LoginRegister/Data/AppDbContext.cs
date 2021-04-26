using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oblivion.Models;
using Oblivion.Models.Account;

namespace Oblivion.Context
{
   
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        

        public AppDbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new RoleConfiguration());

        


    }

}
}
