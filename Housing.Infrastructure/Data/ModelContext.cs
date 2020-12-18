using Housing.Core.Models;
using Microsoft.EntityFrameworkCore;
using WebMaze.DbStuff.Model;

namespace Housing.Infrastructure.Data
{
    public class ModelContext : DbContext
    {
        public DbSet<CitizenUser> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HousingUser> HouseResidents { get; set; }
        public DbSet<HousingOwner> HouseOwners { get; set; }
        public DbSet<Comment> HouseAdvertisementComments { get; set; }
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasOne(h => h.Owner).WithMany(o => o.Houses).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.
            base.OnModelCreating(modelBuilder);
        }
    }
}