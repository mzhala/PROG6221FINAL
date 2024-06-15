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
        private RecipeApp recipeApp; // field is declared at the class level

        public ViewRecipe(RecipeApp existingRecipeApp)
        {
            InitializeComponent();

            recipeApp = existingRecipeApp; // Use the passed-in RecipeApp instance

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
           // MessageBox.Show(selectedRecipe.Name + " - 0", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);

            if (selectedRecipe != null)
            {
                //MessageBox.Show(selectedRecipe.Name + " - 1", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                // Calculate scaled ingredients
                var scaledIngredients = selectedRecipe.Ingredients
                    .Select((ing, index) => new
                    {
                        Number = index + 1,
                        ing.Name,
                        Quantity = ing.Quantity * recipeApp.getRatio(),
                        ing.Unit,
                        ing.CalorieCount,
                        FoodGroup = recipeApp.getFoodGroup(ing.FoodGroupIndex)
                    })
                    .ToList();
                //MessageBox.Show(selectedRecipe.Name + " - 2", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                // Display scaled ingredients in DataGrid
                dataGrid_ingredients.ItemsSource = scaledIngredients;
                //MessageBox.Show(selectedRecipe.Name + " - 3", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                // Display steps with numbering
                lst_steps.ItemsSource = selectedRecipe.Steps
                    .Select((step, index) => $"{index + 1}. {step.Instruction}");
                //MessageBox.Show(selectedRecipe.Name + " - 4", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                // Calculate total calories with scaled ingredients
                double totalCalories = recipeApp.CalculateTotalCalories(
                    scaledIngredients.Select(ing => new Ingredient(ing.Name, ing.Quantity, ing.Unit, ing.CalorieCount, selectedRecipe.Ingredients.First(i => i.Name == ing.Name).FoodGroupIndex)).ToList(),
                    recipeApp.HandleCalorieExceeded
                );
                //MessageBox.Show(selectedRecipe.Name + " - 5", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                // Display recipe details
                lbl_selectedRecipe.Content = selectedRecipe.Name;
               // MessageBox.Show(selectedRecipe.Name + " - 6", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
                lbl_calories.Content = $"Calories: {totalCalories}";
                //MessageBox.Show(selectedRecipe.Name + " - 7", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //MessageBox.Show(selectedRecipe.Name + " - 8", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Home obj = new Home(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_add_recipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe obj = new AddRecipe(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_remove_recipe_Click(object sender, RoutedEventArgs e)
        {
            RemoveRecipe obj = new RemoveRecipe(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_food_group_Click(object sender, RoutedEventArgs e)
        {
            FoodGroupPieChart obj = new FoodGroupPieChart(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            // Refresh the recipes list or switch to another view
        }
    }
}
