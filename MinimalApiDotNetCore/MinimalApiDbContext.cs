using Microsoft.EntityFrameworkCore;
using MinimalApiDotNetCore.Entities;

namespace MinimalApiDotNetCore
{
    public class MinimalApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public MinimalApiDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Users.AddAsync(new User() { Id = 1, IsComplete = false, Name = "javad bayat" });
            //base.SaveChanges();
            base.OnModelCreating(modelBuilder);
        }
    }
}
