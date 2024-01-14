﻿using BussinessLogicLayer.Exceptions;
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

namespace PresentationLayer.AdminPanels {
    /// <summary>
    /// Interaction logic for UcNewLibrary.xaml
    /// </summary>
    public partial class UcNewLibrary : UserControl {
        private bool editing { get; set; }

        public UcNewLibrary() {
            InitializeComponent();
            editing = false;
        }

        public UcNewLibrary(Library libraryToChange) {
            InitializeComponent();
            LoadLibraryDataIntoTextBoxes(libraryToChange);
            editing = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            AdminGuiControl.LoadPreviousControl();
        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            SaveLibrary();
        }

        private void SaveLibrary() {
            int newLibraryID;
            if (int.TryParse(tbLibraryID.Text, out newLibraryID)) {}
            else {
                MessageBox.Show("ID treba biti cijeli broj!");
                return;
            }
            string newLibraryName = tbLibraryName.Text;
            string newLibraryOIB = tbLibraryOIB.Text;
            string newLibraryPhone = tbLibraryPhone.Text;
            string newLibraryEmail = tbLibraryEmail.Text;
            decimal newLibraryPriceDayLate;
            if (decimal.TryParse(tbLibraryPriceDayLate.Text, out newLibraryPriceDayLate)) {}
            else {
                MessageBox.Show("Cijena kašnjenja po danu treba biti decimalan broj!");
                return;
            }
            string newLibraryAddress = tbLibraryAddress.Text;
            int newLibraryMembershipDurationDays;
            if (int.TryParse(tbLibraryMembershipDuration.Text, out newLibraryMembershipDurationDays)) {}
            else {
                MessageBox.Show("Broj dana trajanja članarine treba biti cijeli broj!");
                return;
            }

            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime newLibraryMembershipDuration = startDate.AddDays(newLibraryMembershipDurationDays - 1);

            Library newLibrary = new Library {
                id = newLibraryID,
                name = newLibraryName,
                OIB = newLibraryOIB,
                phone = newLibraryPhone,
                email = newLibraryEmail,
                price_day_late = newLibraryPriceDayLate,
                address = newLibraryAddress,
                membership_duration = newLibraryMembershipDuration
            };

            try {
                LibraryService service = new LibraryService();

                if (!editing) {
                    int result = service.AddLibrary(newLibrary);

                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllLibraries());
                    } else {
                        MessageBox.Show("Knjižnicu nije moguće dodati.");
                    }
                } else {
                    int result = service.UpdateLibrary(newLibrary);

                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllLibraries());
                    } else {
                        MessageBox.Show("Knjižnicu nije moguće urediti.");
                    }
                }
            } catch (LibraryException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadLibraryDataIntoTextBoxes(Library library) {
            tbLibraryID.IsEnabled = false;

            tbLibraryID.Text = library.id.ToString();
            tbLibraryName.Text = library.name;
            tbLibraryOIB.Text = library.OIB;
            tbLibraryPhone.Text = library.phone;
            tbLibraryEmail.Text = library.email;
            tbLibraryPriceDayLate.Text = library.price_day_late.ToString();
            tbLibraryAddress.Text = library.address;
            tbLibraryMembershipDuration.Text = ((library.membership_duration - new DateTime(2024, 1, 1)).Days + 1).ToString();
        }
    }
}
