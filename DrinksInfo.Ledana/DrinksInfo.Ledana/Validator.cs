using DrinksInfo.Ledana.Models;

namespace DrinksInfo.Ledana
{
    internal class Validator
    {
        internal static bool IsIdValid(string? drink, List<Drink> drinks)
        {
            if (string.IsNullOrEmpty(drink) || !drinks.Any(d => d.idDrink == drink)) return false;

            foreach (char c in drink)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        internal static bool IsIdValid(string? drink, List<DrinkDetail> drinks)
        {
            if (string.IsNullOrEmpty(drink) || !drinks.Any(d => d.idDrink == drink)) return false;

            foreach (char c in drink)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        internal static bool IsCategoryValid(string? category, List<Category> categories)
        {
            if (string.IsNullOrEmpty(category)) return false;

            foreach (char c in category)
            {
                if ((!char.IsLetter(c) && c != '/' && c != ' ') || (!categories.Any(c => c.strCategory.ToLower() == category.ToLower())))
                    return false;
            }

            return true;
        }

    }
}
