using DrinksInfo.Ledana.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DrinksInfo.Ledana.Data
{
    internal class FavouritesDrinkContext : DbContext
    {
        public DbSet<DrinkDetail> FavouriteDrinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("FavouriteDrinksDb");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
