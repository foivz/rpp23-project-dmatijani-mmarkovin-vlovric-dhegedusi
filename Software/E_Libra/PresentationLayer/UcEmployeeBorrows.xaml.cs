using BussinessLogicLayer.services;
using EntitiesLayer;
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
    /// Interaction logic for UcEmployeeBorrows.xaml
    /// </summary>
    public partial class UcEmployeeBorrows : UserControl {
        private BorrowService borrowService = new BorrowService();
        private EmployeePanel mainWindow { get; set; }

        public UcEmployeeBorrows(EmployeePanel _mainWindow) {
            InitializeComponent();

            this.mainWindow = _mainWindow;

            var employeeService = new EmployeeService();
            int libraryId = employeeService.GetEmployeeLibraryId(LoggedUser.Username);

            GetAllBorrowsForLibrary(libraryId);
            GetBorrowsForEachStatus(libraryId);
        }

        private void GetAllBorrowsForLibrary(int libraryId) {
            dgAllBorrows.ItemsSource = borrowService.GetAllBorrowsForLibrary(libraryId);
        }

        private void GetBorrowsForEachStatus(int libraryId) {
            dgPendingBorrows.ItemsSource = borrowService.GetBorrowsForLibraryByStatus(libraryId, BorrowStatus.Waiting);
            dgCurrentBorrows.ItemsSource = borrowService.GetBorrowsForLibraryByStatus(libraryId, BorrowStatus.Borrowed);
            dgLateBorrows.ItemsSource = borrowService.GetBorrowsForLibraryByStatus(libraryId, BorrowStatus.Late);
            dgDoneBorrows.ItemsSource = borrowService.GetBorrowsForLibraryByStatus(libraryId, BorrowStatus.Returned);
        }

        private void btnReturnBook_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Pritisnut gumb za vraćanje knjige!");
        }

        private void btnBorrowBook_Click(object sender, RoutedEventArgs e) {
            mainWindow.contentPanel.Content = new UcBorrowNewBook(mainWindow, this);
        }
    }
}
