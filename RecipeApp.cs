using System;
using System.Collections.Generic;
using System.Linq;
using PROG6221_FINAL;

namespace PROG6221_FINAL
{
    public delegate void CalorieExceededHandler();

    public class RecipeApp
    {
        private List<Recipe> recipes;
        private double ratio = 1;
        FoodGroups foodGroups = new FoodGroups();
        public RecipeApp()
        {
            recipes = new List<Recipe>();

            // Example: Adding a default recipe
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 1, "large", 140 , 0),
                new Ingredient("Butter", 1, "Teaspoon", 100 , 1),
                new Ingredient("Pepper", 1, "Teaspoon", 200 , 5),
                new Ingredient("Salt", 1, "Pinch", 100, 4)
            };

            List<Step> steps = new List<Step>
            {
                new Step("Preheat pan"),
                new Step("In a bowl combine and mix eggs, salt and pepper."),
                new Step("Pour batter into a greased pan."),
                new Step("Cook for 3 minutes, or until cooked.")
            };
            Recipe defaultRecipe = new Recipe("Scrambled Egg", ingredients, steps);
            recipes.Add(defaultRecipe);

            // Example: Adding a default recipe
            List<Ingredient> ingredients2 = new List<Ingredient>
            {
                new Ingredient("Egg", 1, "large", 0, 0),
                new Ingredient("Butter", 1, "Teaspoon", 0, 0),
                new Ingredient("Pepper", 1, "Teaspoon", 0, 0),
                new Ingredient("Salt", 1, "Pinch", 0, 0),
                new Ingredient("Onion", 1, "large", 0, 0)
            };

            List<Step> steps2 = new List<Step>
            {
                new Step("Preheat pan"),
                new Step("In a bowl combine and mix eggs, salt and pepper."),
                new Step("Pour chopped onion into a greased pan."),
                new Step("Pour batter into into the fried onion."),
                new Step("Cook for 3 minutes, or until cooked.")
            };
            Recipe defaultRecipe2 = new Recipe("Egg and Onion Fiesta", ingredients2, steps2);
            recipes.Add(defaultRecipe2);

        }

        public void AddRecipe()
        {
            Console.WriteLine("Enter Recipe Details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            int numIngredients;
            while (true)
            {
                Console.Write("Number of Ingredients: ");
                if (int.TryParse(Console.ReadLine(), out numIngredients) && numIngredients > 0)
                    break;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"\nEnter details for Ingredient {i + 1}");

                Console.Write("Name: ");
                string ingredientName = Console.ReadLine();

                float quantity;
                while (true)
                {
                    Console.Write("Quantity: ");
                    if (float.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.Write("Unit: ");
                string unit = Console.ReadLine();

                int calorie;
                while (true)
                {
                    Console.Write("Calorie Count: ");
                    if (int.TryParse(Console.ReadLine(), out calorie) && calorie > 0)
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                int foodGroupIndex;
                while (true)
                {
                    Console.WriteLine("\nFood Groups:\n");
                    foodGroups.DisplayFoodGroupNames();
                    Console.WriteLine("8. Learn More");
                    Console.Write("\nFood Group No. : ");
                    if (int.TryParse(Console.ReadLine(), out foodGroupIndex) && foodGroupIndex > 0 && foodGroupIndex < 9)
                        if (foodGroupIndex == 8)
                            foodGroups.DisplayAllFoodGroupDetails();
                        else
                            break;
                    else
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                ingredients.Add(new Ingredient(ingredientName, quantity, unit, calorie, foodGroupIndex));
            }

            int numSteps;
            while (true)
            {
                Console.Write("\nNumber of Steps: ");
                if (int.TryParse(Console.ReadLine(), out numSteps) && numSteps > 0)
                    break;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            List<Step> steps = new List<Step>();
            Console.WriteLine("Enter the instruction steps");
            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"{i + 1}: ");
                string description = Console.ReadLine();
                steps.Add(new Step(description));
            }

            Recipe newRecipe = new Recipe(name, ingredients, steps);
            recipes.Add(newRecipe);

            Console.WriteLine("Recipe added successfully!");
        }

        public void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available");
                return;
            }

            // Sort recipes by name
            SortRecipesByName();

            Console.WriteLine("Recipes:");
            for (int k = 0; k < recipes.Count; k++)
            {
                Console.WriteLine($"{k + 1}. {recipes[k].Name}");
            }

            bool inputOk = false;
            int i = 0;
            while (!inputOk)
            {
                try
                {
                    Console.Write("\nEnter the number corresponding to the recipe to view: ");
                    i = int.Parse(Console.ReadLine());
                    if (i > 0 && i <= recipes.Count)
                        inputOk = true;
                    else
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            i -= 1;
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("Recipe Details\n");

            Console.WriteLine("Name:" + recipes[i].Name);

            Console.WriteLine("\nIngredients:");
            Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -15} {5, -15}", "No.", "Name", "Quantity", "Unit", "Calorie Count", "Food Group");
            foreach (var ingredient in recipes[i].Ingredients)
            {
                Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -15 } {5, -15}", (recipes[i].Ingredients.IndexOf(ingredient) + 1), ingredient.Name, ingredient.Quantity * ratio, ingredient.Unit, ingredient.CalorieCount, foodGroups.GetFoodGroupName(ingredient.FoodGroupIndex));
            }

            int totalCalories = CalculateTotalCalories(recipes[i].Ingredients, HandleCalorieExceeded);
            Console.WriteLine($"\nTotal Calories: {totalCalories}");


            Console.WriteLine("\nSteps:");
            foreach (var step in recipes[i].Steps)
            {
                Console.WriteLine($"{recipes[i].Steps.IndexOf(step) + 1}. {step.Instruction}");
            }
            Console.WriteLine("----------------------------------------------");
        }

        private void SortRecipesByName()
        {
            recipes.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        // Method to return
        public string[] GetSortedRecipeNames()
        {
            SortRecipesByName();
            return recipes.Select(r => r.Name).OrderBy(name => name).ToArray();
        }

        public Recipe GetRecipeByName(string recipeName)
        {
            return recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
        }

        public double getRatio()
        {
            return ratio;
        }

        public String getFoodGroup(int index)
        {
            return foodGroups.GetFoodGroupName(index);
        }

        

        // Method to calculate the sum of calories from a list of ingredients
        public int CalculateTotalCalories(List<Ingredient> ingredients, CalorieExceededHandler calorieExceededHandler)
        {
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += (int)(ingredient.CalorieCount * ratio);
            }

            if (totalCalories > 300)
            {
                // Invoke the delegate to notify the user
                calorieExceededHandler?.Invoke();
            }

            return totalCalories;
        }

        // Define a method to handle the notification
        public void HandleCalorieExceeded()
        {
            Console.WriteLine("Warning: Calorie count exceeds 300");
        }

        public void ScaleRecipe()
        {
            Console.WriteLine("Scale Menu:");
            Console.WriteLine("A. 1/2 Half");
            Console.WriteLine("B. 2 Double");
            Console.WriteLine("C. 3 Triple");
            Console.WriteLine("D. Reset Scale");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine("");
            switch (choice.ToUpper())
            {
                case "A":
                    ratio = 0.5;
                    break;
                case "B":
                    ratio = 2;
                    break;
                case "C":
                    ratio = 3;
                    break;
                case "D":
                    ratio = 1;
                    break;
                default:
                    Console.WriteLine("Invalid Choice!");
                    return;
            }
            Console.WriteLine("Recipe scale updated!");

        }

        public void SetRecipeScale(double i)
        {
            ratio = i;
        }
        public void RemoveRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available to remove.");
                return;
            }

            Console.WriteLine("Select a recipe to remove:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int index;
            while (true)
            {
                Console.Write("Enter the number corresponding to the recipe to remove: ");
                if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= recipes.Count)
                    break;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            Recipe removedRecipe = recipes[index - 1];

            // Confirm before removing the recipe
            Console.Write($"Are you sure you want to remove recipe '{removedRecipe.Name}'? (Y/N): ");
            string confirmation = Console.ReadLine().Trim().ToUpper();

            if (confirmation == "Y")
            {
                recipes.RemoveAt(index - 1);
                Console.WriteLine($"Recipe '{removedRecipe.Name}' removed successfully!");
            }
            else
            {
                Console.WriteLine("Removal cancelled.");
            }
        }

        // Method to remove a recipe
        public void RemoveRecipe(Recipe recipe)
        {
            // Ensure recipe exists in the collection
            if (recipes.Contains(recipe))
            {
                recipes.Remove(recipe);
            }
        }

        // Method to get all recipes
        public List<Recipe> GetAllRecipes()
        {
            return recipes;
        }
    }
}
