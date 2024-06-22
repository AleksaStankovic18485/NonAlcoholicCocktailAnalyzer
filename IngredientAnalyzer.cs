using System;
using System.Threading.Tasks;

namespace NonAlcoholicCocktailAnalyzer
{
    public class IngredientAnalyzer
    {
        private readonly CocktailService _cocktailService;
        private readonly IngredientCounter _ingredientCounter;
        private readonly DisplayService _displayService;

        public IngredientAnalyzer()
        {
            _cocktailService = new CocktailService();
            _ingredientCounter = new IngredientCounter();
            _displayService = new DisplayService();
        }

        public async Task AnalyzeIngredientsAsync()
        {
            try
            {
                var cocktails = await _cocktailService.FetchCocktailsAsync();
                var ingredientCounts = await _ingredientCounter.CountIngredientsAsync(cocktails);
                _displayService.DisplayResults(ingredientCounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
