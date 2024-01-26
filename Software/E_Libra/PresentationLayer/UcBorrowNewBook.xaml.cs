using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private UcEmployeeBorrows parentUserControl { get; set; }

        public UcBorrowNewBook(EmployeePanel _mainWindow, UcEmployeeBorrows _parentUserControl) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
            this.parentUserControl = _parentUserControl;
        }

        private void btnAddNewBorrow_Click(object sender, RoutedEventArgs e) {
            BorrowBook();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            returnParentUserControl();
        }

        private void returnParentUserControl() {
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

            var borrowService = new BorrowService();

            List<Borrow> existingBorrows = borrowService.GetBorrowsForMemberAndBook(enteredMember.id, enteredBook.id, LoggedUser.LibraryId);

            Borrow alreadyExistingBorrow = existingBorrows.FindAll(b => (b.borrow_status == (int)BorrowStatus.Borrowed || b.borrow_status == (int)BorrowStatus.Late)).FirstOrDefault();
            if (alreadyExistingBorrow != null) {
                MessageBox.Show("Član je već posudio tu knjigu!");
                return;
            }

            Employee thisEmployee = GetEmployee();

            Borrow waitingBorrow = existingBorrows.Find(b => b.borrow_status == (int)BorrowStatus.Waiting);
            if (waitingBorrow == null) {
                Borrow newBorrow = new Borrow {
                    Book = enteredBook,
                    Member = enteredMember,
                    borrow_status = (int)BorrowStatus.Borrowed,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(int.Parse(tbBorrowDuration.Text)),
                    Employee = thisEmployee
                };

                try {
                    borrowService.AddNewBorrow(newBorrow);
                } catch (BookException ex) {
                    MessageBox.Show(ex.Message);
                }

                UpdateParentBorrows();
                returnParentUserControl();
            } else {
                waitingBorrow.borrow_status = (int)BorrowStatus.Borrowed;
                waitingBorrow.borrow_date = DateTime.Now;
                waitingBorrow.return_date = DateTime.Now.AddDays(int.Parse(tbBorrowDuration.Text));
                waitingBorrow.Employee = thisEmployee;

                try {
                    borrowService.UpdateBorrow(waitingBorrow);
                } catch (BookException ex) {
                    MessageBox.Show(ex.Message);
                }

                UpdateParentBorrows();
                returnParentUserControl();
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
                Member enteredMember = memberService.GetMemberByBarcodeId(LoggedUser.LibraryId, tbMemberBarcode.Text);
                return enteredMember;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private Book GetEnteredBook() {
            BookServices bookService = new BookServices();
            try {
                Book enteredBook = bookService.GetBookByBarcodeId(LoggedUser.LibraryId, tbBookBarcode.Text);
                return enteredBook;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private Employee GetEmployee() {
            EmployeeService employeeService = new EmployeeService();
            return employeeService.GetEmployeeByUsername(LoggedUser.Username);
        }

        private void UpdateParentBorrows() {
            parentUserControl.GetAllBorrowsForLibrary(LoggedUser.LibraryId);
            parentUserControl.GetBorrowsForEachStatus(LoggedUser.LibraryId);
            parentUserControl.tbcTabs.SelectedIndex = 2;
        }
    }
}
