using DrinksInfo.Ledana.Models;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Text.Json;


namespace DrinksInfo.Ledana.Services
{
    internal class DrinkService
    {
        internal async Task<List<Category>?> GetCategoriesByHttpClient()
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();

                    var serialize = await JsonSerializer.DeserializeAsync<Categories>(stream,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (serialize is null) return null;

                    return serialize.CategoriesList;
                }
                else return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is not working!" + e.Message);
                return null;
            }
        }

        internal async Task<List<Drink>?> GetDrinksByCategory(string? category)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={HttpUtility.UrlEncode(category)}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();

                    var serialize = await JsonSerializer.DeserializeAsync<Drinks>(stream,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return serialize!.DrinksList;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is not working!" + e.Message);
                return null;
            }
        }

        internal async Task<List<(string Key, string Value)>?> GetDrink(string? drink)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();

                    var serializer = await JsonSerializer.DeserializeAsync<DrinkDetailObject>(stream,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (serializer is null) return null;

                    List<DrinkDetail> returnedList = serializer.DrinkDetailList;
                    if (returnedList is null || returnedList.Count == 0)
                        return null;

                    DrinkDetail drinkDetail = returnedList[0];

                    List<(string Key, string Value)> prepList = [];

                    foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
                    {
                        var value = prop.GetValue(drinkDetail)?.ToString();

                        if (!string.IsNullOrEmpty(value))
                        {
                            string formattedName = prop.Name.StartsWith("str")
                                ? prop.Name.Substring(3)
                                : prop.Name;

                            prepList.Add((formattedName, value));
                        }
                    }
                    return prepList;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is not working!" + e.Message);
                return null;
            }
        }
        public async Task<DrinkDetail?> GetDrinkDetail(string? drink)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();

                    var serializer = await JsonSerializer.DeserializeAsync<DrinkDetailObject>(stream);

                    if (serializer is null) return null;

                    List<DrinkDetail> returnedList = serializer.DrinkDetailList;
                    if (returnedList is null || returnedList.Count == 0)
                        return null;

                    return returnedList[0];
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is not working!" + e.Message);
                return null;

            }
        }
    }
}
