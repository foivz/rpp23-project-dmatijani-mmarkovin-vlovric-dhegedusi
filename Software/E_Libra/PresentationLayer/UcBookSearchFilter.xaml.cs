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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for UcBookSearchFilter.xaml
    /// </summary>
    public partial class UcBookSearchFilter : UserControl
    {
        BookServices bookServices;
        public UcBookSearchFilter()
        {
            InitializeComponent();
            bookServices = new BookServices();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //0-sve, 1-žanr, 2-pisac, 3-godina
            switch(cmbFilter.SelectedIndex)
            {
                case 0:
                    dgvBookSearch.ItemsSource = bookServices.SearchBooks(txtSearch.Text);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
