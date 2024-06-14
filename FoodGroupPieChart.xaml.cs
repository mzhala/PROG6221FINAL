using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace PROG6221_FINAL
{
    public partial class FoodGroupPieChart : Window
    {
        private RecipeApp recipeApp;
        private int totalIngredients; // Declare totalIngredients at class level

        public FoodGroupPieChart()
        {
            InitializeComponent();
            recipeApp = new RecipeApp(); // Instantiate RecipeApp
            LoadFoodGroupDistribution();
            DisplaySortedRecipeNames();
        }

        private void LoadFoodGroupDistribution()
        {
            // Get all recipes
            List<Recipe> recipes = recipeApp.GetAllRecipes();

            // Calculate food group distribution
            var foodGroupCounts = new Dictionary<string, int>();
            totalIngredients = 0; // Initialize totalIngredients here

            foreach (var recipe in recipes)
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

            // Prepare data for pie chart
            SeriesCollection series = new SeriesCollection();
            foreach (var kv in foodGroupCounts)
            {
                double percentage = (double)kv.Value / totalIngredients * 100;
                series.Add(new PieSeries
                {
                    Title = kv.Key,
                    Values = new ChartValues<int> { kv.Value },

                    DataLabels = true, // Display data labels
                    LabelPoint = point => $"{point.Participation:P}", // Customize label to show percentage
                    FontWeight = FontWeights.Bold, // Bold font for labels
                    FontSize = 14, // Adjust font size as needed

                    Foreground = Brushes.Black, // Set text color to black

                });
            }

            // Set the Series of the pie chart
            pieChart.LegendLocation = LegendLocation.Right;

            pieChart.Series = series;

        }





        private void DisplaySortedRecipeNames()
        {
            // Get all recipes
            List<Recipe> recipes = recipeApp.GetAllRecipes();

            // Sort recipes by name
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

            // Bind sorted recipes to the ListBox
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
    }
}
