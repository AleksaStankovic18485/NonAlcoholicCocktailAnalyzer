using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    public class IngredientCounter
    {
        private readonly CocktailService _cocktailService;

        public IngredientCounter()
        {
            _cocktailService = new CocktailService();
        }

        public async Task<Dictionary<string, int>> CountIngredientsAsync(IEnumerable<JToken> cocktails)
        {
            var ingredientCounts = new Dictionary<string, int>();

            var ingredientStreams = cocktails.Select(cocktail =>
            {
                var cocktailId = cocktail["idDrink"].ToString();
                return Observable.FromAsync(() => _cocktailService.FetchIngredientsForCocktailAsync(cocktailId));
            });

            await Observable.Merge(ingredientStreams)
                .ForEachAsync(ingredients =>
                {
                    foreach (var ingredient in ingredients)
                    {
                        if (ingredientCounts.ContainsKey(ingredient))
                        {
                            ingredientCounts[ingredient]++;
                        }
                        else
                        {
                            ingredientCounts[ingredient] = 1;
                        }
                    }
                });

            return ingredientCounts;
        }
    }
}
