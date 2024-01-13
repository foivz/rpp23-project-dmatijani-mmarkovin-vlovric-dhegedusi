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

namespace PresentationLayer.AdminPanels {
    /// <summary>
    /// Klasa AdminPanel omogućuje administratoru sve administratorske kontrole
    /// David Matijanić
    /// </summary>
    public partial class AdminPanel : Window {
        public AdminPanel() {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda "btnAllLibraries_Click" otvara novi UserControl koji ima popis svih knjižnica
        /// David Matijanić
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllLibraries_Click(object sender, RoutedEventArgs e) {
            UcAllLibraries ucAllLibraries = new UcAllLibraries();
            contentPanel.Content = ucAllLibraries;
        }
    }
}
