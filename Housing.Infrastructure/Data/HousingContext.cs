using Housing.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebMaze.DbStuff.Model;

namespace Housing.Infrastructure.Data
{
    public class HousingContext : DbContext
    {
        public DbSet<CitizenUser> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HousingResident> HouseResidents { get; set; }
        public DbSet<HousingOwner> HouseOwners { get; set; }
        public DbSet<Comment> HouseAdvertisementComments { get; set; }
        public DbSet<CartHouse> HouseCarts { get; set; }
        public DbSet<HousingOwnerRequest> HousingOwnerRequests { get; set; }
        public DbSet<HousingResidentRequest> HousingResidentRequests { get; set; }

        public HousingContext(DbContextOptions<HousingContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasOne(h => h.Owner).WithMany(o => o.Houses).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HousingOwner>().HasMany(o => o.Houses).WithOne(h => h.Owner);
            modelBuilder.Entity<HousingResident>().HasOne(u => u.House).WithMany(h => h.HousingUsers).
                OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<House>().HasMany(h => h.Comments).WithOne(c => c.House).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HousingResident>().HasMany(u => u.Comments).WithOne(c => c.User).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HousingOwner>().HasMany(o => o.OwnerRequests).WithOne(r => r.Owner);
            modelBuilder.Entity<HousingResident>().HasMany(o => o.ResidentRequests).WithOne(r => r.Resident);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}