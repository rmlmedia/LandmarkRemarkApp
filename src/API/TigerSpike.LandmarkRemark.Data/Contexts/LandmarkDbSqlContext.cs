using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TigerSpike.LandmarkRemark.Domain.Models;

namespace TigerSpike.LandmarkRemark.Data.Contexts
{
    public class LandmarkDbSqlContext : DbContext
    {
        private readonly bool _isInMemory;

        public DbSet<Landmark> Landmarks { get; set; }
        public DbSet<User> Users { get; set; }

        public LandmarkDbSqlContext(bool isInMemory = false) : base() {
            _isInMemory = isInMemory;
        }

        protected string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            return config.GetValue<string>("connectionString");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isInMemory)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "LandmarkRemarks");
            }
            else 
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
    }
}