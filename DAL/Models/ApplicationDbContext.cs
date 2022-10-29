using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreFilter> GenreFilters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Move into settings
        //    //var connectionString = ConfigurationManager.GetSection("connectionStrings");
        //    //string connectionString = "Server=.;Database=Watchify;Trusted_Connection=True;MultipleActiveResultSets=true";

        //    //optionsBuilder.UseSqlServer(_conn);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GenreFilter>().HasKey(u => new
            {
                u.UserId,
                u.GenreId,
                u.ShowType
            });
        }
    }
}
