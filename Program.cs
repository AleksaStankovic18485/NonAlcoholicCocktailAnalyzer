using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ingredientAnalyzer = new IngredientAnalyzer();
            await ingredientAnalyzer.AnalyzeIngredientsAsync();
        }
    }
}
