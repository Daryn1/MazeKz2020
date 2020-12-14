using Housing.Core.Models;
using Microsoft.EntityFrameworkCore;
using WebMaze.DbStuff.Model;

namespace Housing.Infrastructure.Data
{
    public class ModelContext : DbContext
    {
        public DbSet<CitizenUser> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HousingUser> HousingUsers { get; set; }
        public DbSet<Comment> HouseAdvertisementComments { get; set; }
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }
    }
}