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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for UcReturnBook.xaml
    /// </summary>
    public partial class UcReturnBook : UserControl {
        private EmployeePanel mainWindow { get; set; }
        private UcEmployeeBorrows parentUserControl { get; set; }

        public UcReturnBook(EmployeePanel _mainWindow, UcEmployeeBorrows _parentUserControl) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
            this.parentUserControl = _parentUserControl;

            btnReturnBorrow.IsEnabled = false;
            btnReturnBorrow.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            ReturnParentUserControl();
        }

        private void ReturnParentUserControl() {
            mainWindow.contentPanel.Content = parentUserControl;
        }

        private void btnCheckBorrow_Click(object sender, RoutedEventArgs e) {
            CheckForBorrows();
        }

        private void CheckForBorrows() {
            string borrowInformation = "";
            tbBorrowInfo.Text = "";

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

            Borrow existingBorrow = GetBorrow(enteredBook, enteredMember);
            if (existingBorrow == null) {
                borrowInformation += "Ne postoji aktualna posudba!";
                tbBorrowInfo.Text = borrowInformation;
                return;
            }

            string dateFormat = "dd.MM.yyyy";
            bool late = existingBorrow.borrow_status == (int)BorrowStatus.Late;

            borrowInformation += "Postoji posudba!" + Environment.NewLine;
            borrowInformation += "Član: " + existingBorrow.Member.ToString() + Environment.NewLine;
            borrowInformation += "Knjiga: " + existingBorrow.Book.ToString() + Environment.NewLine;
            borrowInformation += "Kasni? " + (late ? "DA" : "NE") + Environment.NewLine;
            borrowInformation += "Datum posudbe: " + existingBorrow.borrow_date.ToString(dateFormat) + Environment.NewLine;
            if (existingBorrow.return_date != null) {
                borrowInformation += "Rok za vraćanje: " + ((DateTime)existingBorrow.return_date).ToString(dateFormat);
                if (late) {
                    TimeSpan difference = DateTime.Now - (DateTime)existingBorrow.return_date;
                    int daysLate = Convert.ToInt16(Math.Ceiling(difference.TotalDays));
                    borrowInformation += $" - kasni {daysLate} dana.";
                }
                borrowInformation += Environment.NewLine;
            }

            tbBorrowInfo.Text = borrowInformation;

            btnReturnBorrow.IsEnabled = true;
            btnReturnBorrow.Visibility = Visibility.Visible;
        }

        private void btnReturnBorrow_Click(object sender, RoutedEventArgs e) {

        }

        private bool CheckInputFields() {
            btnReturnBorrow.IsEnabled = false;
            btnReturnBorrow.Visibility = Visibility.Collapsed;

            if (tbBookBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod knjige!");
                return false;
            }

            if (tbMemberBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod sa članske iskaznice člana!");
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

        private Borrow GetBorrow(Book book, Member member) {
            BorrowService borrowService = new BorrowService();
            return borrowService.GetBorrowsForMemberAndBook(member.id, book.id, LoggedUser.LibraryId).Where(b => b.borrow_status != (int)BorrowStatus.Returned
            && b.borrow_status != (int)BorrowStatus.Waiting).FirstOrDefault();
        }
    }
}
