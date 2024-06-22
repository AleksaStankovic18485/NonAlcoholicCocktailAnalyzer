using System;
using System.Collections.Generic;
using System.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    public class DisplayService
    {
        public void DisplayResults(Dictionary<string, int> ingredientCounts)
        {
            Console.WriteLine("Ingredient counts:");
            foreach (var kvp in ingredientCounts.OrderByDescending(kvp => kvp.Value))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
