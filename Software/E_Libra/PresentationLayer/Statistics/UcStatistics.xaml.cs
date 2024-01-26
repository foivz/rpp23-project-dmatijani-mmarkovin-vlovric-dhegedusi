using BussinessLogicLayer.services;
using EntitiesLayer;
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
    public partial class UcStatistics : UserControl {
        StatisticsService statisticsService = new StatisticsService();
        public UcStatistics() {
            InitializeComponent();
            cmbStats.SelectionChanged += StatsComboBoxControl_SelectionChanged;
            dgMostPopularBooks.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void StatsComboBoxControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbStats.SelectedItem != null) {

                int selectedOption = cmbStats.SelectedIndex;

                EmployeeService employeeService = new EmployeeService();
                var Library_id = employeeService.GetEmployeeLibraryId(LoggedUser.Username);

                switch (selectedOption) {
                    case 0: // Najposuđenije knjige

                        dgMostPopularBooks.ItemsSource = statisticsService.GetMostPopularBooks(Library_id);
                        dgMostPopularBooks.Visibility = System.Windows.Visibility.Visible;

                        break;

                    case 1: // Broj ukupnih posudbi po žanru

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


    }
}