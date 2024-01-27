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

namespace PresentationLayer.EmployeePanels
{
    /// <summary>
    /// Interaction logic for UcEditMember.xaml
    /// </summary>
    public partial class UcEditMember : UserControl
    {
        Member editMember;
        MemberService memberService;
        public UcEditMember(Member member)
        {
            InitializeComponent();
            editMember = member;
            memberService = new MemberService();

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            editMember.name = txtName.Text;
            editMember.surname = txtSurname.Text;
            editMember.OIB = txtOIB.Text;
            editMember.password = txtPassword.Text;
            bool edited = memberService.UpdateMember(editMember);
            if (edited)
            {
                (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
            }
        }
    }
}
