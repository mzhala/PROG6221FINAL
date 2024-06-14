using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace PROG6221_FINAL
{
    public partial class ViewRecipe : Window
    {
        private RecipeApp recipeApp; // Ensure this field is declared at the class level

        public ViewRecipe()
        {
            InitializeComponent();

            recipeApp = new RecipeApp(); // Instantiate RecipeApp

            LoadRecipes(); // Call LoadRecipes method to load data into UI
        }

        private void LoadRecipes()
        {
            lst_recipeList.ItemsSource = recipeApp.GetSortedRecipeNames();
        }

        private void Lst_recipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst_recipeList.SelectedItem != null)
            {
                string selectedRecipeName = lst_recipeList.SelectedItem.ToString();
                DisplayRecipeDetails(selectedRecipeName);
            }
        }

        private void DisplayRecipeDetails(string recipeName)
        {
            Recipe selectedRecipe = recipeApp.GetRecipeByName(recipeName);

            if (selectedRecipe != null)
            {
                // Calculate scaled ingredients
                var scaledIngredients = selectedRecipe.Ingredients
                    .Select(ing => new Ingredient(ing.Name, ing.Quantity * recipeApp.getRatio(), ing.Unit, ing.CalorieCount, ing.FoodGroupIndex))
                    .ToList();

                // Display scaled ingredients
                lst_ingredients.ItemsSource = scaledIngredients
                    .Select(ing => $"{ing.Quantity} {ing.Unit} {ing.Name}");

                // Calculate total calories with scaled ingredients
                double totalCalories = recipeApp.CalculateTotalCalories(scaledIngredients, recipeApp.HandleCalorieExceeded);

                // Display recipe details
                lst_steps.ItemsSource = selectedRecipe.Steps.Select(step => step.Instruction);
                lbl_selectedRecipe.Content = selectedRecipe.Name;
                lbl_calories.Content = $"Calories: {totalCalories}";
            }
        }




        private void btn_viewSelected_Recipe_Click(object sender, RoutedEventArgs e)
        {
            if (lst_recipeList.SelectedItem != null)
            {
                string selectedRecipeName = lst_recipeList.SelectedItem.ToString();
                DisplayRecipeDetails(selectedRecipeName);
            }

            btn_viewSelected_Recipe.Background = Brushes.Green;
            Delayed1SecMethod().ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    btn_viewSelected_Recipe.Background = Brushes.White;
                });
            });
        }

        private static async Task Delayed1SecMethod()
        {
            await Task.Delay(1000); // Delay for 1000 milliseconds (1 second)
        }

        private void ResetRecipeScale()
        {
            btn_recipeScale_reset.Background = Brushes.White;
            btn_recipeScale_half.Background = Brushes.White;
            btn_recipeScale_double.Background = Brushes.White;
            btn_recipeScale_triple.Background = Brushes.White;
        }

        private void btn_recipeScale_reset_Click(object sender, RoutedEventArgs e)
        {
            ResetRecipeScale();
            btn_recipeScale_reset.Background = Brushes.SeaGreen;
            recipeApp.SetRecipeScale(1); // Scale recipes to original
            RefreshRecipeDetails(); // Refresh displayed recipe details
        }

        private void btn_recipeScale_half_Click(object sender, RoutedEventArgs e)
        {
            ResetRecipeScale();
            btn_recipeScale_half.Background = Brushes.SeaGreen;
            recipeApp.SetRecipeScale(0.5); // Scale recipes to half
            RefreshRecipeDetails(); // Refresh displayed recipe details
        }

        private void btn_recipeScale_double_Click(object sender, RoutedEventArgs e)
        {
            ResetRecipeScale();
            btn_recipeScale_double.Background = Brushes.SeaGreen;
            recipeApp.SetRecipeScale(2); // Scale recipes to double
            RefreshRecipeDetails(); // Refresh displayed recipe details
        }

        private void btn_recipeScale_triple_Click(object sender, RoutedEventArgs e)
        {
            ResetRecipeScale();
            btn_recipeScale_triple.Background = Brushes.SeaGreen;
            recipeApp.SetRecipeScale(3); // Scale recipes to triple
            RefreshRecipeDetails(); // Refresh displayed recipe details
        }

        private void RefreshRecipeDetails()
        {
            if (lst_recipeList.SelectedItem != null)
            {
                string selectedRecipeName = lst_recipeList.SelectedItem.ToString();
                DisplayRecipeDetails(selectedRecipeName);
            }
        }


        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow obj = new MainWindow();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_add_recipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe obj = new AddRecipe();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_update_recipe_Click(object sender, RoutedEventArgs e)
        {
            UpdateRecipe obj = new UpdateRecipe();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_remove_recipe_Click(object sender, RoutedEventArgs e)
        {
            RemoveRecipe obj = new RemoveRecipe();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_food_group_Click(object sender, RoutedEventArgs e)
        {
            FoodGroupPieChart obj = new FoodGroupPieChart();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            // Refresh the recipes list or switch to another view
        }
    }
}
