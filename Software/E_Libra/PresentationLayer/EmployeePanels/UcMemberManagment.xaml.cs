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
    /// Interaction logic for UcMemberManagment.xaml
    /// </summary>
    public partial class UcMemberManagment : UserControl
    {
        MemberService memberService;
        public UcMemberManagment()
        {
            InitializeComponent();
            memberService = new MemberService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
        }

        private void btnMemberRegistration_Click(object sender, RoutedEventArgs e)
        {
            UcRegisterMember ucRegisterMember = new UcRegisterMember();
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucRegisterMember;
        }

        private void btnEditMember_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = dgvMembers.SelectedItem as Member;
            if (selectedMember != null)
            {
            UcEditMember ucEditMember = new UcEditMember(selectedMember);
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucEditMember;
            } else
            {
                MessageBox.Show("Odaberite člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = dgvMembers.SelectedItem as Member;
            if (selectedMember != null)
            {
                bool deleted = false;
                MessageBoxResult reuslt =  MessageBox.Show("Jeste li sigurni da želite izbrisati", "Upozorenje", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(reuslt == MessageBoxResult.OK)
                {
                    deleted = memberService.DeleteMember(selectedMember);
                }
                if (deleted)
                {
                    dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
                }
            } else
            {
                MessageBox.Show("Odaberite člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
