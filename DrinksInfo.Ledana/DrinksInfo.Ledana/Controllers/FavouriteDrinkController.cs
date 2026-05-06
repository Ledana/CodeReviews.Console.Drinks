using DrinksInfo.Ledana.Data;
using DrinksInfo.Ledana.Models;

namespace DrinksInfo.Ledana.Controllers
{
    internal class FavouriteDrinkController
    {
        internal static void AddDrinkToFavourites(DrinkDetail favouriteDrink)
        {
            try
            {
                using var context = new FavouritesDrinkContext();
                context.FavouriteDrinks.Add(favouriteDrink);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong! " + e.Message);
            }
        }

        internal static async Task DeleteAllFavourites()
        {
            try
            { 
            using var context = new FavouritesDrinkContext();
            context.FavouriteDrinks.RemoveRange(context.FavouriteDrinks);
            await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! " + e.Message);
            }
        }

        internal static void DeleteFavourite(string? drink)
        {
            try
            {
                using var context = new FavouritesDrinkContext();
                var drinkToDelete = context.FavouriteDrinks.Where(d => d.idDrink == drink).First();
                context.FavouriteDrinks.Remove(drinkToDelete);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong! " + e.Message);
            }
}

        internal static List<DrinkDetail> GetFavouriteDrinks()
        {
            try
            {
                using var context = new FavouritesDrinkContext();
                return context.FavouriteDrinks.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! " + e.Message);
            }
        }

        internal static bool HasDrink(string? drink)
        {
            try
            {
                using var context = new FavouritesDrinkContext();
                return context.FavouriteDrinks.Any(d => d.idDrink == drink);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! " + e.Message);
            }
        }
    }
}
