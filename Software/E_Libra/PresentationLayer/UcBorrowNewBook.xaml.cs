using BussinessLogicLayer.Exceptions;
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
            BorrowBook();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            mainWindow.contentPanel.Content = parentUserControl;
        }

        private void BorrowBook() {
            if (!CheckInputFields()) {
                return;
            }

            Book enteredBook = GetEnteredBook();
            if (enteredBook == null) {
                return;
            }

            Member enteredMember = GetEnteredMember();
            if (enteredMember == null) {
                return;
            }
        }

        private bool CheckInputFields() {
            if (tbBookBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod knjige!");
                return false;
            }

            if (tbMemberBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod sa članske iskaznice člana!");
                return false;
            }

            if (tbBorrowDuration.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti uneseno trajanje posudbe!");
                return false;
            }

            int numberOfDays = 0;
            if (!int.TryParse(tbBorrowDuration.Text, out numberOfDays)) {
                MessageBox.Show("Nije unesen ispravan broj dana!");
                return false;
            }

            if (numberOfDays < 1) {
                MessageBox.Show("Trajanje posudbe mora biti barem 1 dan!");
                return false;
            }

            return true;
        }

        private Member GetEnteredMember() {
            MemberService memberService = new MemberService();
            try {
                Member enteredMember = memberService.GetMemberByBarcodeId(tbMemberBarcode.Text);
                return enteredMember;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private Book GetEnteredBook() {
            BookServices bookService = new BookServices();
            try {
                Book enteredBook = bookService.GetBookByBarcodeId(tbBookBarcode.Text);
                return enteredBook;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
