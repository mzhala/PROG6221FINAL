using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PROG6221_FINAL
{
    /// <summary>
    /// Interaction logic for ViewRecipe.xaml
    /// </summary>
    public partial class ViewRecipe : Window
    {
        public ViewRecipe()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow obj = new MainWindow();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_add_recipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_update_recipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_remove_recipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_food_group_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_viewSelected_Recipe_Click(object sender, RoutedEventArgs e)
        {
            btn_viewSelected_Recipe.Background = Brushes.Green;
            await Delayed1SecMethod(); // Delay for 1000 milliseconds (1 second)
            btn_viewSelected_Recipe.Background = Brushes.Transparent;
        }

        static async Task Delayed1SecMethod()
        {
            await Task.Delay(100); // Delay for 1000 milliseconds (1 second)
        }

        private void btn_recipeScale_reset_Click(object sender, RoutedEventArgs e)
        {
            btn_recipeScale_reset.Background = Brushes.SeaGreen;
            btn_recipeScale_half.Background = Brushes.White;
            btn_recipeScale_double.Background = Brushes.White;
            btn_recipeScale_triple.Background = Brushes.White;
        }

        private void btn_recipeScale_half_Click(object sender, RoutedEventArgs e)
        {
            btn_recipeScale_reset.Background = Brushes.White;
            btn_recipeScale_half.Background = Brushes.SeaGreen;
            btn_recipeScale_double.Background = Brushes.White;
            btn_recipeScale_triple.Background = Brushes.White;
        }

        private void btn_recipeScale_double_Click(object sender, RoutedEventArgs e)
        {
            btn_recipeScale_reset.Background = Brushes.White;
            btn_recipeScale_half.Background = Brushes.White;
            btn_recipeScale_double.Background = Brushes.SeaGreen;
            btn_recipeScale_triple.Background = Brushes.White;
        }

        private void btn_recipeScale_triple_Click(object sender, RoutedEventArgs e)
        {
            btn_recipeScale_reset.Background = Brushes.White;
            btn_recipeScale_half.Background = Brushes.White;
            btn_recipeScale_double.Background = Brushes.White;
            btn_recipeScale_triple.Background = Brushes.SeaGreen;
        }
    }
}
