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
    /// Interaction logic for ListPeoplePage.xaml
    /// </summary>
    public partial class ListPeoplePage : PersonFramedPage
    {
        public ListPeoplePage(PersonViewModel personViewModel) : base(personViewModel)
        {
            InitializeComponent();
            LvUsers.ItemsSource = personViewModel.People;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditPersonPage editPersonPage = new EditPersonPage(PersonViewModel) { Frame = Frame };
            editPersonPage.BtnVax.Visibility = Visibility.Hidden;
            Frame.Navigate(editPersonPage);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                Frame.Navigate(new EditPersonPage(PersonViewModel, LvUsers.SelectedItem as Person) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                PersonViewModel.People.Remove(LvUsers.SelectedItem as Person);
            }
        }
    }
}
