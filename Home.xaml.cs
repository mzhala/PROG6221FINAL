using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG6221_FINAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Home : Window
    {
        private RecipeApp recipeApp;
        public Home(RecipeApp existingRecipeApp)
        {
            InitializeComponent();

            recipeApp = existingRecipeApp; // Use the passed-in RecipeApp instance
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {

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
            FoodGroupPieChart obj = new FoodGroupPieChart(recipeApp);
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }
    }
}