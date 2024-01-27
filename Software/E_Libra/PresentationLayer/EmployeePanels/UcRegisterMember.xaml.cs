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
using ZXing;

namespace PresentationLayer.EmployeePanels
{
    /// <summary>
    /// Interaction logic for UcRegisterMember.xaml
    /// </summary>
    public partial class UcRegisterMember : UserControl
    {
        MemberService memberService;
        EmployeeService employeeService;
        public UcRegisterMember()
        {
            InitializeComponent();
            memberService = new MemberService();
            employeeService = new EmployeeService();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int LibraryId = employeeService.GetEmployeeLibraryId(LoggedUser.Username);
            Member newMember = new Member()
            {
                name = txtName.Text,
                surname = txtSurname.Text,
                OIB = txtOIB.Text,
                username = txtUsername.Text, 
                password = txtPassword.Text,
                membership_date = txtDate.SelectedDate,
                barcode_id = txtBarcode.Text,
                Library_id = LibraryId
            };
            memberService.AddNewMember(newMember);
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
        }
        private void btnGenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            txtBarcode.Text = memberService.RandomCodeGenerator().ToString();

            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128};
            System.Drawing.Bitmap barcodeBitmap = writer.Write(txtBarcode.Text);
            imgBarcode.Source = ConvertBitmapToImageSource(barcodeBitmap);
        }

        private ImageSource ConvertBitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
