using System.Text.Json.Serialization;

namespace DrinksInfo.Ledana.Models
{
    internal class Categories
    {
        [JsonPropertyName("drinks")]
        public List<Category> CategoriesList { get; set; } = [];
    }
}
