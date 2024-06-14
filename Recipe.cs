using System;
using System.Collections.Generic;

// Recipe class representing a recipe with name, ingredients, and instructions
namespace PROG6221_FINAL
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }

        // Constructor for the Recipe class.
        public Recipe(string name, List<Ingredient> ingredients, List<Step> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }
    }
}