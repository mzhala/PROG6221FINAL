using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        public FoodGroupPieChart()
        {
            InitializeComponent();
            recipeApp = new RecipeApp();
            allRecipes = recipeApp.GetAllRecipes();
            selectedRecipes = new List<Recipe>();
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
            var sortedRecipes = allRecipes.OrderBy(r => r.Name).ToList();
            lst_allRecipeList.ItemsSource = sortedRecipes;
            lst_allRecipeList.DisplayMemberPath = "Name";
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow obj = new MainWindow();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            ViewRecipe obj = new ViewRecipe();
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
            // Already on the Food Group Chart page
        }

        private void btn_addRecipeToPieChart_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lst_allRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                selectedRecipes.Add(selectedRecipe);
                allRecipes.Remove(selectedRecipe);

                // Refresh the lists
                lst_allRecipeList.ItemsSource = null;
                lst_allRecipeList.ItemsSource = allRecipes.OrderBy(r => r.Name).ToList();

                lst_selectedRecipeList.ItemsSource = null;
                lst_selectedRecipeList.ItemsSource = selectedRecipes.OrderBy(r => r.Name).ToList();

                // Update the pie chart with the new selection
                LoadFoodGroupDistribution();
            }
        }

        private void btn_removeSelectedRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lst_selectedRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                allRecipes.Add(selectedRecipe);
                selectedRecipes.Remove(selectedRecipe);

                // Refresh the lists
                lst_allRecipeList.ItemsSource = null;
                lst_allRecipeList.ItemsSource = allRecipes.OrderBy(r => r.Name).ToList();

                lst_selectedRecipeList.ItemsSource = null;
                lst_selectedRecipeList.ItemsSource = selectedRecipes.OrderBy(r => r.Name).ToList();

                // Update the pie chart with the new selection
                LoadFoodGroupDistribution();
            }
        }

        private void btn_clearRecipeList_Click(object sender, RoutedEventArgs e)
        {
            allRecipes.AddRange(selectedRecipes);
            selectedRecipes.Clear();

            // Refresh the lists
            lst_allRecipeList.ItemsSource = null;
            lst_allRecipeList.ItemsSource = allRecipes.OrderBy(r => r.Name).ToList();

            lst_selectedRecipeList.ItemsSource = null;

            // Update the pie chart with the new selection
            LoadFoodGroupDistribution();
        }

    }
}
