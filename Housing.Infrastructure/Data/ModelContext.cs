using Housing.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.Infrastructure.Data
{
    public class ModelContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<HouseResident> HouseResidents { get; set; }
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }
    }
}