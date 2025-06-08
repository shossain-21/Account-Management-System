using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementSystem.Services
{
    public class AppDbContext : IdentityDbContext<AccountUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole
            {
                Id = "1",
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var accountant = new IdentityRole
            {
                Id = "2",
                Name = "accountant",
                NormalizedName = "ACCOUNTANT"
            };

            var viewer = new IdentityRole
            {
                Id = "3",
                Name = "viewer",
                NormalizedName = "VIEWER"
            };

            builder.Entity<IdentityRole>().HasData(admin, accountant, viewer);
        }

        public DbSet<Control> AspNetControls { get; set; }

        public DbSet<SubSidiary> AspNetSubSidiaries { get; set; }
    }
}
