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
    /// Interaction logic for UcNewEmployee.xaml
    /// </summary>
    public partial class UcNewEmployee : UserControl {
        public UcNewEmployee() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            AdminGuiControl.LoadPreviousControl();
        }
    }
}
