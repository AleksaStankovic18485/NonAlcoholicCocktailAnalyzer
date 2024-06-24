using NonAlcoholicCocktailAnalyzer;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace NonAlcoholicCocktailAnalyzer
{
    public class IngredientPrikaz
    {
        private readonly IngredientCounter _ingredientCounter;

        public IngredientPrikaz(IngredientCounter ingredientCounter)
        {
            _ingredientCounter = ingredientCounter;
        }

        public void AnalyzeIngredients(string drinkType)
        {
            try
            {
                _ingredientCounter.GetIngredientCountsObservable(drinkType)
                    .Subscribe(
                        ingredientCount =>
                        {
                            Console.WriteLine($"{ingredientCount.Key}: {ingredientCount.Value}");
                        },
                        ex =>
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        },
                        () =>
                        {
                            Console.WriteLine("Analysis complete.");
                        });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
