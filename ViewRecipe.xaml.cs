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
    }
}
