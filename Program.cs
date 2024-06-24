using NonAlcoholicCocktailAnalyzer;
using System;

namespace NonAlcoholicCocktailAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the drink type (e.g., Non_Alcoholic): ");
            string drinkType = Console.ReadLine();

            var ingredientCounter = new IngredientCounter();
            var ingredientAnalyzer = new IngredientPrikaz(ingredientCounter);

            ingredientAnalyzer.AnalyzeIngredients(drinkType);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
