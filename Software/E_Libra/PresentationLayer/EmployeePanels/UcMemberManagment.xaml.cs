using BussinessLogicLayer.services;
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
    }
}
