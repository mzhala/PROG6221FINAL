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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PROG6221_FINAL
{
    /// <summary>
    /// Interaction logic for FoodGroupPieChart.xaml
    /// </summary>
    public partial class FoodGroupPieChart : Window
    {
        public FoodGroupPieChart()
        {
            InitializeComponent();

            // Define series collection
            SeriesCollection seriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Group 1",
                    Values = new ChartValues<double> { 10 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 2",
                    Values = new ChartValues<double> { 15 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 3",
                    Values = new ChartValues<double> { 20 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 4",
                    Values = new ChartValues<double> { 5 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 4",
                    Values = new ChartValues<double> { 5 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 6",
                    Values = new ChartValues<double> { 5 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                },
                new PieSeries
                {
                    Title = "Group 7",
                    Values = new ChartValues<double> { 5 },
                    DataLabels = true,
                    LabelPoint = PieLabelPoint
                }
            };


            // Assign the series collection to the PieChart
            pieChart.Series = seriesCollection;
        }

        // Custom label formatting function
        private Func<ChartPoint, string> PieLabelPoint => chartPoint =>
        {
            // Get the value of the data point
            double value = chartPoint.Y;

            // Get the title of the series
            string title = chartPoint.SeriesView.Title;

            // Format the label to include both title and value
            return $"{title}: {value}";
        };



        private void btn_updateSelected_Recipe_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow obj = new MainWindow();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_add_recipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_update_recipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_remove_recipe_Click(object sender, RoutedEventArgs e)
        {
            RemoveRecipe obj = new RemoveRecipe();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_food_group_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_view_recipes_Click(object sender, RoutedEventArgs e)
        {
            ViewRecipe obj = new ViewRecipe();
            this.Visibility = Visibility.Hidden;
            obj.Show();
        }

        private void btn_updateSelected_Recipe__Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
