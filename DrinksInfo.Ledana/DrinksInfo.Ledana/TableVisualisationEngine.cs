using DrinksInfo.Ledana.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace DrinksInfo.Ledana
{
    internal class TableVisualisationEngine
    {
        internal static void ShowCategoriesHttpTable(List<Category>? categories)
        {
            Console.Clear();

            var table = new Table();
            table.AddColumn("Categories: ");

            if (categories is null)
            {
                Console.WriteLine("Table is empty or API is down!");
                return;
            }

            foreach (var item in categories)
            {
                table.AddRow(item.strCategory);
            }
            table.AddRow("Favourites");

            AnsiConsole.Write(table);
            Console.Write("Type 'x' to exit or Choose category : ");
        }

        internal static void ShowDrinkHttp(List<(string Key, string Value)> drinkTable)
        {
            var table = new Table()
                .ShowRowSeparators();
            table.AddColumn("Property");
            table.AddColumn("Value");

            foreach (var item in drinkTable)
            {
                table.AddRow(item.Key, item.Value);
            }
            AnsiConsole.Write(table);
        }

        internal static void ShowDrinksHttpTable(List<Drink>? drinks)
        {
            var table = new Table().Title("Drinks: ");

            table.AddColumn("Id");
            table.AddColumn("Name");

            if (drinks is null)
            {
                Console.WriteLine("Table empty or API is down!");
                return;
            }

            foreach (var item in drinks)
            {
                table.AddRow(item.idDrink.ToString(), item.strDrink);
            }
            AnsiConsole.Write(table);
            Console.Write("Type 'x' to exit or Choose drink id: ");
        }

        internal static void ShowFavouriteDrinksHttpTable(List<DrinkDetail> favouriteDrinks)
        {
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");

            List<Drink> drinks = [];
            foreach(var item in favouriteDrinks)
            {
                drinks.Add(DetailToDrink(item));
            }

            foreach (var item in drinks)
            {
                table.AddRow(item.idDrink.ToString(), item.strDrink);
            }

            AnsiConsole.Write(table);
            Console.Write("Type 'x' to exit or Choose drink id: ");
        }
        private static Drink DetailToDrink(DrinkDetail drink)
        {
            return new Drink()
            {
                idDrink = drink.idDrink,
                strDrink = drink.strDrink
            };
        }
    }
}
