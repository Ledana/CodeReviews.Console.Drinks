using System.Text.Json.Serialization;

namespace DrinksInfo.Ledana.Models
{
    internal class Drinks
    {
        [JsonPropertyName("drinks")]
        public List<Drink> DrinksList { get; set; } = [];
    }
}
