using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt) { }

        public DbSet<LoveBerfin> LoveBerfin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LoveBerfin>().HasData(new LoveBerfin() { Id = 1 ,LoveName="Canberk",Name = "Berfin"});

            base.OnModelCreating(builder);
        }
    }
}
