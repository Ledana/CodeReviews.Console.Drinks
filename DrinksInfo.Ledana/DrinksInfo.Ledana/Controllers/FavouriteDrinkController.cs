using DrinksInfo.Ledana.Data;
using DrinksInfo.Ledana.Models;

namespace DrinksInfo.Ledana.Controllers
{
    internal class FavouriteDrinkController
    {
        internal static void AddDrinkToFavourites(DrinkDetail favouriteDrink)
        {
            using var context = new FavouritesDrinkContext();

            context.FavouriteDrinks.Add(favouriteDrink);
            context.SaveChanges();
        }

        internal static async Task DeleteAllFavourites()
        {
            using var context = new FavouritesDrinkContext();
            context.FavouriteDrinks.RemoveRange(context.FavouriteDrinks);
            await context.SaveChangesAsync();
        }

        internal static void DeleteFavourite(string? drink)
        {
            using var context = new FavouritesDrinkContext();
            var drinkToDelete = context.FavouriteDrinks.Where(d => d.idDrink == drink).First();
            context.FavouriteDrinks.Remove(drinkToDelete);
            context.SaveChanges();
        }

        internal static List<DrinkDetail> GetFavouriteDrinks()
        {
            using var context = new FavouritesDrinkContext();
            return context.FavouriteDrinks.ToList();
        }

        internal static bool HasDrink(string? drink)
        {
            using var context = new FavouritesDrinkContext();

            return context.FavouriteDrinks.Any(d => d.idDrink == drink);
        }
    }
}
