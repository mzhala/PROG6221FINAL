using System;
using System.Collections.Generic;
using System.Windows;

namespace PROG6221_FINAL
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        private RecipeApp recipeApp;
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<Step> steps = new List<Step>();

        public AddRecipe(RecipeApp existingRecipeApp)
        {
            InitializeComponent();

            recipeApp = existingRecipeApp; // Use the passed-in RecipeApp instance
            LoadRecipes(); // Call LoadRecipes method to load data into UI
            LoadFoodGroups();
        }

        private void LoadRecipes()
        {
            lst_recipeList.ItemsSource = recipeApp.GetSortedRecipeNames();
        }

        private void LoadFoodGroups()
        {
            // Add food groups to the ComboBox
            cmbBx_foodGroup.Items.Add("Starchy foods");
            cmbBx_foodGroup.Items.Add("Vegetables and fruits");
            cmbBx_foodGroup.Items.Add("Dry beans, peas, lentils and soya");
            cmbBx_foodGroup.Items.Add("Chicken, fish, meat and eggs");
            cmbBx_foodGroup.Items.Add("Milk and dairy products");
            cmbBx_foodGroup.Items.Add("Fats and oil");
            cmbBx_foodGroup.Items.Add("Water");
            cmbBx_foodGroup.SelectedIndex = 0;
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

        private void btn_addIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Read ingredient details from text boxes
            string ingredientName = txtBx_ingredientName.Text.Trim();
            string unit = txtBx_unit.Text.Trim();
            int foodGroupIndex = cmbBx_foodGroup.SelectedIndex;

            // Validate and parse quantity
            double quantity;
            if (!double.TryParse(txtBx_quantity.Text.Trim(), out quantity) || quantity <= 0)
            {
                error_msg.Content = "Invalid value for quantity. Quantity must be a positive number.";
                return;
            }

            // Validate and parse calorieCount
            int calorieCount;
            if (!int.TryParse(txtBx_calorieCount.Text.Trim(), out calorieCount) || calorieCount < 0)
            {
                error_msg.Content = "Invalid value for calorie count. Calorie count must be a non-negative integer.";
                return;
            }

            // Validate ingredientName
            if (string.IsNullOrWhiteSpace(ingredientName) || ContainsOnlyNumbers(ingredientName))
            {
                error_msg.Content = "Invalid value for ingredient name. Ingredient name cannot be empty or consist only of numeric characters.";
                return;
            }

            // Validate unit
            if (string.IsNullOrWhiteSpace(unit) || ContainsOnlyNumbers(unit))
            {
                error_msg.Content = "Invalid value for unit. Unit cannot be empty or consist only of numeric characters.";
                return;
            }

            // Validate food group selection
            if (foodGroupIndex == -1)
            {
                error_msg.Content = "Please select a food group.";
                return;
            }

            // Clear previous error messages
            error_msg.Content = "";

            // Create Ingredient object
            Ingredient newIngredient = new Ingredient(ingredientName, quantity, unit, calorieCount, foodGroupIndex+1);
            // Add Ingredient to list
            ingredients.Add(newIngredient);
            // Add Ingredient details to ListBox
            lst_ingredients.Items.Add(newIngredient.Name);

            // Clear input fields after adding
            ClearIngredientFields();
        }

        private bool ContainsOnlyNumbers(string input)
        {
            return input.All(char.IsDigit);
        }



        private void ClearIngredientFields()
        {
            txtBx_ingredientName.Text = "";
            txtBx_quantity.Text = "";
            txtBx_unit.Text = "";
            txtBx_calorieCount.Text = "";
            cmbBx_foodGroup.SelectedItem = null; // Clear selected food group
            error_msg.Content = "";
        }

        private void btn_addStep_Click(object sender, RoutedEventArgs e)
        {
            // Read step details from text block
            string stepDescription = txtBx_step.Text.Trim();

            // Validate input (you can add your validation logic here)
            if (string.IsNullOrWhiteSpace(stepDescription) || ContainsOnlyNumbers(stepDescription))
            {
                error_msg.Content = "Invalid value for step. Step description cannot be empty or consist only of numeric characters.";
                return;
            }

            // Create Step object
            Step newStep = new Step(stepDescription);

            // Add Step to list
            steps.Add(newStep);

            // Add Step details to ListBox
            lst_steps.Items.Add(newStep.Instruction);

            // Clear input fields after adding
            txtBx_step.Text = "";
            error_msg.Content = "";
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

        private void btn_add_recipe_Click(object sender, RoutedEventArgs e)
        {
            // Validate if all necessary fields are filled before adding the recipe
            if (string.IsNullOrEmpty(txtBx_recipeName.Text.Trim()) || ingredients.Count == 0 || steps.Count == 0)
            {
                MessageBox.Show("Please fill in all fields (Recipe Name, Ingredients, Steps) before adding the recipe.", "Incomplete Recipe", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Read recipe name from text box
            string recipeName = txtBx_recipeName.Text.Trim();

            // Log the ingredients for debugging
            foreach (var ing in ingredients)
            {
                Console.WriteLine($"Ingredient: {ing.Name}, Quantity: {ing.Quantity}, Unit: {ing.Unit}, Calories: {ing.CalorieCount}");
            }

            // Create Recipe object
            Recipe newRecipe = new Recipe(recipeName, new List<Ingredient>(ingredients), new List<Step>(steps));

            // Add Recipe to RecipeApp
            recipeApp.AddRecipe(newRecipe);

            // Log the recipe for debugging
            Console.WriteLine($"Recipe added: {newRecipe.Name}, Ingredients count: {newRecipe.Ingredients.Count}, Steps count: {newRecipe.Steps.Count}");

            // Clear UI fields after adding recipe
            ClearRecipeFields();

            // Optionally, provide feedback or navigate to another window
            error_msg.Content = "Recipe added successfully!";
            LoadRecipes();
        }


        private void ClearRecipeFields()
        {
            txtBx_recipeName.Text = "";
            lst_ingredients.Items.Clear();
            lst_steps.Items.Clear();
            ingredients.Clear();
            steps.Clear();
            error_msg.Content = "";
        }

    }
}
