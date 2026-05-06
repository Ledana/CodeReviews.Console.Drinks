using DrinksInfo.Ledana.Controllers;
using DrinksInfo.Ledana.Models;
using DrinksInfo.Ledana.Services;

namespace DrinksInfo.Ledana
{
    internal class UserInput
    {
        private readonly DrinkService drinksService = new();

        internal async Task GetCategoriesInput()
        {
            var categories = await drinksService.GetCategoriesByHttpClient();

            if (categories is null) return;

            TableVisualisationEngine.ShowCategoriesHttpTable(categories);

            string? category = Console.ReadLine();
            if (category is not null && category.ToLower() == "x") return;

            if (!string.IsNullOrEmpty(category) && category.ToLower() == "favourites")
            {
                FavouritesMenu();
                string? input = Console.ReadLine();
                if (input is null) return;

                switch (input)
                {
                    case "1":
                        await SeeAllFavourites();
                        break;
                    case "2":
                        await DeleteAllFavourites();
                        break;
                    case "3":
                        DeleteOneFavourite();
                        break;
                    default:
                        break;
                }

                return;
            }

            while (!Validator.IsCategoryValid(category, categories))
            {
                Console.WriteLine("\nInvalid category. Type 'x' to exit or try again.");
                category = Console.ReadLine();
                if (category == "x") return;
            }

            await GetDrinksInput(category);
        }

        private void DeleteOneFavourite()
        {
            List<DrinkDetail> favouriteDrinks = FavouriteDrinkController.GetFavouriteDrinks();

            TableVisualisationEngine.ShowFavouriteDrinksHttpTable(favouriteDrinks);

            string? drink = Console.ReadLine();

            while (!Validator.IsIdValid(drink, favouriteDrinks))
            {
                Console.WriteLine("\nInvalid drink. Type 'x' to exit or try again.");
                drink = Console.ReadLine();
                if (drink == "x") return;
            }

            FavouriteDrinkController.DeleteFavourite(drink);
            Console.WriteLine($"Drink with id {drink} has been deleted!");
        }

        private async Task DeleteAllFavourites()
        {
            await FavouriteDrinkController.DeleteAllFavourites();
            Console.WriteLine("Successfully deleted all favourites!");
        }

        public async Task SeeAllFavourites()
        {
            List<DrinkDetail> favouriteDrinks = FavouriteDrinkController.GetFavouriteDrinks();

            TableVisualisationEngine.ShowFavouriteDrinksHttpTable(favouriteDrinks);

            string? drink = Console.ReadLine();
            if (drink is not null && drink.ToLower() == "x") return;

            while (!Validator.IsIdValid(drink, favouriteDrinks))
            {
                Console.WriteLine("\nInvalid drink. Type 'x' to exit or try again.");
                drink = Console.ReadLine();
                if (drink == "x") return;
            }

            var drinkTable = await drinksService.GetDrink(drink);

            if (drinkTable is null) return;

            TableVisualisationEngine.ShowDrinkHttp(drinkTable);
            return;
        }
        private void FavouritesMenu()
        {
            Console.WriteLine("1. See all favourites");
            Console.WriteLine("2. Delete all favourites");
            Console.WriteLine("3. Delete one favourite");
        }

        private async Task GetDrinksInput(string? category)
        {
            var drinks = await drinksService.GetDrinksByCategory(category);

            if (drinks is null) return;

            TableVisualisationEngine.ShowDrinksHttpTable(drinks);

            string? drink = Console.ReadLine();

            if (drink is not null && drink.ToLower() == "x") return;

            while (!Validator.IsIdValid(drink, drinks))
            {
                Console.WriteLine("\nInvalid drink. Type 'x' to exit or try again.");
                drink = Console.ReadLine();
                if (drink == "x") return;
            }

            var drinkTable = await drinksService.GetDrink(drink);

            if (drinkTable is null) return;

            TableVisualisationEngine.ShowDrinkHttp(drinkTable);

            if (!FavouriteDrinkController.HasDrink(drink))
                await AskForFavourite(drink);
        }

        public async Task AskForFavourite(string? drink)
        {
            Console.WriteLine("Do you want to add this category to your favourites? (y/n): ");
            var input = Console.ReadLine();
            if (input is not null && input.ToLower() == "y")
            {
                DrinkDetail? favouriteDrink = await drinksService.GetDrinkDetail(drink);
                if (favouriteDrink is null) return;

                FavouriteDrinkController.AddDrinkToFavourites(favouriteDrink);

                Console.WriteLine("Drink added into favourites successfully!");
            }
        }
    }
}
