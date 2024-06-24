using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    public class IngredientCounter
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://www.thecocktaildb.com/api/json/v1/1/filter.php";
        private readonly HashSet<string> _validParams = new HashSet<string> { "Non_Alcoholic" };

        public IObservable<KeyValuePair<string, int>> GetIngredientCountsObservable(string drinkType)
        {
            if (!_validParams.Contains(drinkType))
            {
                throw new ArgumentException("Invalid drink type parameter. Allowed values: Non_Alcoholic");
            }

            string url = $"{_baseUrl}?a={drinkType}";

            return _httpClient.GetStringAsync(url)
                .ToObservable()
                .SelectMany(cocktailData =>
                {
                    var drinks = JObject.Parse(cocktailData)["drinks"];
                    if (drinks == null)
                    {
                        throw new Exception("No drinks found for the specified type.");
                    }

                    var drinkObservables = drinks
                        .Select(drink => GetDrinkIngredientsObservable((string)drink["idDrink"]))
                        .ToArray();

                    return Observable.Merge(drinkObservables)
                                     .SelectMany(ingredients => ingredients)
                                     .GroupBy(ingredient => ingredient)
                                     .SelectMany(group => group.Count().Select(count => new KeyValuePair<string, int>(group.Key, count)));
                });
        }

        private IObservable<IEnumerable<string>> GetDrinkIngredientsObservable(string drinkId)
        {
            string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkId}";

            return _httpClient.GetStringAsync(url)
                .ToObservable()
                .Select(drinkDetails =>
                {
                    var drink = JObject.Parse(drinkDetails)["drinks"]?.FirstOrDefault();
                    if (drink == null)
                    {
                        throw new Exception($"No details found for drink ID {drinkId}");
                    }

                    var ingredients = new List<string>();
                    for (int i = 1; i <= 15; i++)
                    {
                        var ingredient = (string)drink[$"strIngredient{i}"];
                        if (!string.IsNullOrEmpty(ingredient))
                        {
                            ingredients.Add(ingredient);
                        }
                    }

                    return ingredients.AsEnumerable();
                });
        }
    }
}
