using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    public class CocktailService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string BaseUrl = "https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic";

        public async Task<IEnumerable<JToken>> FetchCocktailsAsync()
        {
            var response = await httpClient.GetStringAsync(BaseUrl);
            var json = JObject.Parse(response);
            return json["drinks"];
        }

        public async Task<IEnumerable<string>> FetchIngredientsForCocktailAsync(string cocktailId)
        {
            var url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={cocktailId}";
            var response = await httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            var ingredients = new List<string>();

            for (int i = 1; i <= 15; i++)
            {
                var ingredient = json["drinks"][0][$"strIngredient{i}"]?.ToString();
                if (!string.IsNullOrEmpty(ingredient))
                {
                    ingredients.Add(ingredient);
                }
            }

            return ingredients;
        }
    }
}
