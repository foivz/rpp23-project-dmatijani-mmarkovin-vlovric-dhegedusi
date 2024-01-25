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

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for UcBorrowNewBook.xaml
    /// </summary>
    public partial class UcBorrowNewBook : UserControl {
        private EmployeePanel mainWindow { get; set; }
        private UserControl parentUserControl { get; set; }

        public UcBorrowNewBook(EmployeePanel _mainWindow, UserControl _parentUserControl) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
            this.parentUserControl = _parentUserControl;
        }

        private void btnAddNewBorrow_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            mainWindow.contentPanel.Content = parentUserControl;
        }
    }
}
