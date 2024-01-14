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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.AdminPanels {
    /// <summary>
    /// Interaction logic for UcAllEmployees.xaml
    /// </summary>
    public partial class UcAllEmployees : UserControl {
        public UcAllEmployees() {
            InitializeComponent();
        }

        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e) {
            UcNewEmployee ucNewEmployee = new UcNewEmployee();
            AdminGuiControl.LoadNewControl(ucNewEmployee);
        }
    }
}
