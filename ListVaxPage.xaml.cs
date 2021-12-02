using PPPK_Delivery_2_Dominik_Hruza_3IP1.Models;
using PPPK_Delivery_2_Dominik_Hruza_3IP1.ViewModel;
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

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1
{
    /// <summary>
    /// Interaction logic for ListVaxPage.xaml
    /// </summary>
    public partial class ListVaxPage : VaxFramedPage
    {
        public ListVaxPage(VaccinationViewModel vaccinationViewModel) : base(vaccinationViewModel)
        {
            InitializeComponent();
            LvVaccinations.ItemsSource = vaccinationViewModel.Vaccinations;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditVaxPage(VaccinationViewModel) { Frame = Frame });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvVaccinations.SelectedItem != null)
            {
                Frame.Navigate(new EditVaxPage(VaccinationViewModel, LvVaccinations.SelectedItem as Vaccination) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvVaccinations.SelectedItem != null)
            {
                VaccinationViewModel.Vaccinations.Remove(LvVaccinations.SelectedItem as Vaccination);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
