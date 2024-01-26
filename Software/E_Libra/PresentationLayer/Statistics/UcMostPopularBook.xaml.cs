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
    public partial class UcMostPopularBook : UserControl {
        StatisticsService statisticsService = new StatisticsService();
        public UcMostPopularBook() {
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

                        CreateLayoutMostPopularBooks(Library_id);
                       
                        break;

                    case 1: // Broj ukupnih posudbi po žanru

                        CreateLayoutMostPopularGenres(Library_id);
                        break;

                    case 2: // Broj registriranih članova
                        MessageBox.Show("Showing total registered members");
                        break;

                    case 3: // Broj napisanih recenzija
                        MessageBox.Show("Showing total registered members");
                        break;

                    case 4: // Prihodi
                        MessageBox.Show("Showing total registered members");
                        break;

                    default:
                        break;
                }
            }
        }

        private void CreateLayoutMostPopularGenres(int library_id) {
            throw new NotImplementedException();
        }

        private void CreateLayoutMostPopularBooks(int Library_id) {
            DataGrid dgMostPopularBooks = new DataGrid {
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
                Binding = new Binding("Book_Name")
            };
            DataGridTextColumn column2 = new DataGridTextColumn {
                Header = "Autor",
                Binding = new Binding("Author_Name")
            };
            DataGridTextColumn column3 = new DataGridTextColumn {
                Header = "Broj posudbi",
                Binding = new Binding("Times_Borrowed")
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
