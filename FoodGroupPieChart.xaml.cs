using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace PROG6221_FINAL
{
    public partial class FoodGroupPieChart : Window
    {
        private RecipeApp recipeApp;
        private int totalIngredients;
        private List<Recipe> allRecipes;
        private List<Recipe> selectedRecipes;
        private ICollectionView allRecipesView;

        public FoodGroupPieChart(RecipeApp existingRecipeApp)
        {
            InitializeComponent();

            recipeApp = existingRecipeApp; // Use the passed-in RecipeApp instance
            allRecipes = recipeApp.GetAllRecipes();
            selectedRecipes = new List<Recipe>();

            // Create a CollectionView for allRecipes and set the filter
            allRecipesView = CollectionViewSource.GetDefaultView(allRecipes);
            allRecipesView.Filter = FilterRecipes;

            DisplaySortedRecipeNames();
        }

        private void LoadFoodGroupDistribution()
        {
            // Initialize the series collection
            SeriesCollection series = new SeriesCollection();

            // Update pie chart
            UpdatePieChart(series);

            // Set the series to the pie chart
            pieChart.Series = series;
        }

        private void UpdatePieChart(SeriesCollection series)
        {
            // Calculate food group distribution
            var foodGroupCounts = new Dictionary<string, int>();
            totalIngredients = 0;

            foreach (var recipe in selectedRecipes)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    string foodGroup = recipeApp.getFoodGroup(ingredient.FoodGroupIndex);

                    if (foodGroupCounts.ContainsKey(foodGroup))
                        foodGroupCounts[foodGroup]++;
                    else
                        foodGroupCounts.Add(foodGroup, 1);

                    totalIngredients++;
                }
            }

            foreach (var kv in foodGroupCounts)
            {
                series.Add(new PieSeries
                {
                    Title = kv.Key,
                    Values = new ChartValues<int> { kv.Value },
                    DataLabels = true,
                    LabelPoint = point => $"{point.Participation:P}",
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Foreground = Brushes.Black
                });
            }
        }

        private void DisplaySortedRecipeNames()
        {
            lst_allRecipeList.ItemsSource = allRecipesView;
            lst_allRecipeList.DisplayMemberPath = "Name";
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            Home obj = new Home(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            ViewRecipe obj = new ViewRecipe(recipeApp);
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
            // Already on the Food Group Chart page
        }

        private void btn_addRecipeToPieChart_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lst_allRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null && !selectedRecipes.Contains(selectedRecipe))
            {
                selectedRecipes.Add(selectedRecipe);
                // Refresh the CollectionView to update the display
                allRecipesView.Refresh();

                // Refresh the selected recipe list
                UpdateRecipeLists();
            }
        }

        private void btn_removeSelectedRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lst_selectedRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                selectedRecipes.Remove(selectedRecipe);
                // Refresh the CollectionView to update the display
                allRecipesView.Refresh();

                // Refresh the selected recipe list
                UpdateRecipeLists();
            }
        }

        private void btn_clearRecipeList_Click(object sender, RoutedEventArgs e)
        {
            selectedRecipes.Clear();
            // Refresh the CollectionView to update the display
            allRecipesView.Refresh();

            // Refresh the selected recipe list
            UpdateRecipeLists();
        }

        private void UpdateRecipeLists()
        {
            lst_selectedRecipeList.ItemsSource = selectedRecipes.OrderBy(r => r.Name).ToList();
            // Update the pie chart with the new selection
            LoadFoodGroupDistribution();
        }

        private bool FilterRecipes(object item)
        {
            Recipe recipe = item as Recipe;
            return recipe != null && !selectedRecipes.Contains(recipe);
        }
    }
}
