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

namespace PresentationLayer.EmployeePanels
{
    /// <summary>
    /// Interaction logic for UcEditMember.xaml
    /// </summary>
    public partial class UcEditMember : UserControl
    {
        Member editMember;
        public UcEditMember(Member member)
        {
            InitializeComponent();
            editMember = member;

            txtName.Text = member.name;
            txtSurname.Text = member.surname;
            txtOIB.Text = member.OIB;
            txtUsername.Text = member.username;
            txtPassword.Text = member.password;
            txtBarcode.Text = member.barcode_id;
            txtDate.Text = (member.membership_date).ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
        }
    }
}
