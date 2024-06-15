using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace PROG6221_FINAL
{
    public partial class RemoveRecipe : Window
    {
        private RecipeApp recipeApp; // Ensure this field is declared at the class level

        public RemoveRecipe(RecipeApp existingRecipeApp)
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

            if (selectedRecipe != null)
            {
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

                // Display scaled ingredients in DataGrid
                dataGrid_ingredients.ItemsSource = scaledIngredients;

                // Display steps with numbering
                lst_steps.ItemsSource = selectedRecipe.Steps
                    .Select((step, index) => $"{index + 1}. {step.Instruction}");

                // Calculate total calories with scaled ingredients
                double totalCalories = recipeApp.CalculateTotalCalories(
                    scaledIngredients.Select(ing => new Ingredient(ing.Name, ing.Quantity, ing.Unit, ing.CalorieCount, selectedRecipe.Ingredients.First(i => i.Name == ing.Name).FoodGroupIndex)).ToList(),
                    recipeApp.HandleCalorieExceeded
                );

                // Display recipe details
                lbl_selectedRecipe.Content = selectedRecipe.Name;
                lbl_calories.Content = $"Calories: {totalCalories}";

            }
            else
            {

                // Clear UI elements related to recipe details
                dataGrid_ingredients.ItemsSource = null;
                lst_steps.ItemsSource = null;
                lbl_selectedRecipe.Content = "No Selected Recipe";
                lbl_calories.Content = "Calories";
            }
        }



        private void btn_viewSelected_Recipe_Click(object sender, RoutedEventArgs e)
        {
            if (lst_recipeList.SelectedItem != null)
            {
                string selectedRecipeName = lst_recipeList.SelectedItem.ToString();
                DisplayRecipeDetails(selectedRecipeName);
            }

            // Change button background color temporarily
            btn_viewSelected_Recipe.Background = Brushes.Green;

            // Reset button background after a delay
            Delayed1SecMethod().ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    btn_viewSelected_Recipe.Background = Brushes.White;
                });
            });
        }


        private void ClearRecipeDetails()
        {
            lbl_selectedRecipe.Content = "No Selected Recipe";
            lbl_calories.Content = "Calories: ";
            dataGrid_ingredients.ItemsSource = null;
            lst_steps.ItemsSource = null;
        }

        private static async Task Delayed1SecMethod()
        {
            await Task.Delay(1000); // Delay for 1000 milliseconds (1 second)
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
            //
        }

        private void btn_food_group_Click(object sender, RoutedEventArgs e)
        {
            FoodGroupPieChart obj = new FoodGroupPieChart(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            ViewRecipe obj = new ViewRecipe(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_removeSelected_Recipe_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (lst_recipeList.SelectedItem != null)
            {
                string selectedRecipeName = lst_recipeList.SelectedItem.ToString();
                Recipe selectedRecipe = recipeApp.GetRecipeByName(selectedRecipeName);

                if (selectedRecipe != null)
                {
                    // Remove the recipe from the app
                    recipeApp.RemoveRecipe(selectedRecipe);

                    // Refresh recipe list
                    LoadRecipes();

                    // Clear details
                    ClearRecipeDetails();
                }
            }

            // Hide confirm button
            btn_removeSelected_Recipe_Confirm.Visibility = Visibility.Hidden;
            lb_Confirm.Visibility = Visibility.Hidden;
        }

        private void btn_removeSelected_Recipe_Click(object sender, RoutedEventArgs e)
        {
            if (lst_recipeList.SelectedItem != null)
            {
                // Show confirm button and label
                btn_removeSelected_Recipe_Confirm.Visibility = Visibility.Visible;
                lb_Confirm.Visibility = Visibility.Visible;

                // Hide no recipe selected label
                lb_NoRecipeSelected.Visibility = Visibility.Hidden;
            }
            else
            {
                // Show NoRecipeSelected label
                btn_removeSelected_Recipe_Confirm.Visibility = Visibility.Hidden;
                lb_Confirm.Visibility = Visibility.Hidden;
                lb_NoRecipeSelected.Visibility = Visibility.Visible;
            }
        }

    }
}
