using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for UcStatistics.xaml
    /// </summary>
    public partial class UcStatistics : UserControl {
        StatisticsService statisticsService = new StatisticsService();
        private DataGrid dgMostPopularBooks;
        private ItemsControl icMostPopularGenres;
        private ItemsControl icReviewCount;
        public UcStatistics() {
            InitializeComponent();
            cmbStats.SelectionChanged += StatsComboBoxControl_SelectionChanged;
            cmbStats.SelectedIndex = 0;
        }

        private void StatsComboBoxControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbStats.SelectedItem != null) {

                int selectedOption = cmbStats.SelectedIndex;

                EmployeeService employeeService = new EmployeeService();
                var Library_id = employeeService.GetEmployeeLibraryId(LoggedUser.Username);

                switch (selectedOption) {
                    case 0: // Najposuđenije knjige
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }

                        CreateLayoutMostPopularBooks(Library_id);
                       
                        break;

                    case 1: // Broj ukupnih posudbi po žanru

                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }

                        CreateLayoutMostPopularGenres(Library_id);
                        break;

                    case 2: // Broj registriranih članova
                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }
                        break;

                    case 3: // Broj napisanih recenzija
                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }

                        CreateLayoutReviewCount(Library_id);

                        break;

                    case 4: // Prihodi
                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void CreateLayoutReviewCount(int Library_id) {
            var ReviewStatistics = statisticsService.GetReviewCount(Library_id);

            icReviewCount = new ItemsControl {
                Name = "icReviewCount",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = ReviewStatistics
            };

            DataTemplate dataTemplate = new DataTemplate(typeof(ReviewStatistics));

            // Create a FrameworkElementFactory for a Border
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(5));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(1));

            // Create a StackPanel within the Border
            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // Create TextBlock for Grade
            FrameworkElementFactory textBlock1Factory = new FrameworkElementFactory(typeof(TextBlock));
            MultiBinding multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(new Binding("Grade"));
            multiBinding.StringFormat = "Ocjena ({0})";
            textBlock1Factory.SetBinding(TextBlock.TextProperty, multiBinding);
            textBlock1Factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 60, 0));
            textBlock1Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            // Create TextBlock for Number_Count
            FrameworkElementFactory textBlock2Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2Factory.SetBinding(TextBlock.TextProperty, new Binding("Number_Count"));
            textBlock2Factory.SetValue(TextBlock.MarginProperty, new Thickness(60, 0, 0, 0));
            textBlock2Factory.SetValue(TextBlock.FontSizeProperty, 18.0);
            textBlock2Factory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            // Add TextBlocks to StackPanel
            stackPanelFactory.AppendChild(textBlock1Factory);
            stackPanelFactory.AppendChild(textBlock2Factory);

            // Add StackPanel to the Border
            borderFactory.AppendChild(stackPanelFactory);

            // Set Border as the VisualTree for the DataTemplate
            dataTemplate.VisualTree = borderFactory;

            icReviewCount.ItemTemplate = dataTemplate;

            Grid.SetRow(icReviewCount, 1);
            Grid.SetColumn(icReviewCount, 0);
            grid.Children.Add(icReviewCount);
        }



        public void CreateLayoutMostPopularGenres(int Library_id) {
            var mostPopularGenres = statisticsService.GetMostPopularGenres(Library_id);

            icMostPopularGenres = new ItemsControl {
                Name = "icMostPopularGenres",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = mostPopularGenres
            };

            DataTemplate dataTemplate = new DataTemplate(typeof(MostPopularGenres));

            // Create a FrameworkElementFactory for a Border
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(5));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(1));

            // Create a StackPanel within the Border
            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // Create TextBlock for Genre_name
            FrameworkElementFactory textBlock1Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock1Factory.SetBinding(TextBlock.TextProperty, new Binding("Genre_name"));
            textBlock1Factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0));
            textBlock1Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            // Create TextBlock for Times_Borrowed
            FrameworkElementFactory textBlock2Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2Factory.SetBinding(TextBlock.TextProperty, new Binding("Times_Borrowed"));
            textBlock2Factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 0, 0));
            textBlock2Factory.SetValue(TextBlock.FontSizeProperty, 18.0);
            textBlock2Factory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            // Add TextBlocks to StackPanel
            stackPanelFactory.AppendChild(textBlock1Factory);
            stackPanelFactory.AppendChild(textBlock2Factory);

            // Add StackPanel to the Border
            borderFactory.AppendChild(stackPanelFactory);

            // Set Border as the VisualTree for the DataTemplate
            dataTemplate.VisualTree = borderFactory;

            icMostPopularGenres.ItemTemplate = dataTemplate;

            Grid.SetRow(icMostPopularGenres, 1);
            grid.Children.Add(icMostPopularGenres);
        }

        private void CreateLayoutMostPopularBooks(int Library_id) {
            dgMostPopularBooks = new DataGrid {
                Name = "dgMostPopularBooks",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                AutoGenerateColumns = false,
                IsReadOnly = true
            };

            DataGridTextColumn column1 = new DataGridTextColumn {
                Header = "Ime knjige",
                Binding = new Binding("Book_Name"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            DataGridTextColumn column2 = new DataGridTextColumn {
                Header = "Autor",
                Binding = new Binding("Author_Name"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            DataGridTextColumn column3 = new DataGridTextColumn {
                Header = "Broj posudbi",
                Binding = new Binding("Times_Borrowed"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star) 
            };

            dgMostPopularBooks.Columns.Add(column1);
            dgMostPopularBooks.Columns.Add(column2);
            dgMostPopularBooks.Columns.Add(column3);

            Grid.SetRow(dgMostPopularBooks, 1);
            Grid.SetColumn(dgMostPopularBooks, 0);
            grid.Children.Add(dgMostPopularBooks);

            dgMostPopularBooks.ItemsSource = statisticsService.GetMostPopularBooks(Library_id);
        }

    }
}
