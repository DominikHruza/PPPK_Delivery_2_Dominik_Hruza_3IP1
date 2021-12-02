using Microsoft.Win32;
using PPPK_Delivery_2_Dominik_Hruza_3IP1.Models;
using PPPK_Delivery_2_Dominik_Hruza_3IP1.Utils;
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
    /// Interaction logic for EditVaxPage.xaml
    /// </summary>
    public partial class EditVaxPage : VaxFramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Vaccination vaccination;
        public EditVaxPage(VaccinationViewModel vaccinationViewModel, Vaccination vaccination = null) : base(vaccinationViewModel)
        {
            InitializeComponent();
            this.vaccination = vaccination ?? new Vaccination();
            DataContext = vaccination;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                vaccination.VaccinationDate = (DateTime) DpDate.SelectedDate;
                vaccination.Manufacturer = TbManufacturer.Text.Trim();
                vaccination.PersonID = VaccinationViewModel.PersonId;
                vaccination.ManufacturerPicture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (vaccination.IDVaccination == 0)
                {
                    VaccinationViewModel.Vaccinations.Add(vaccination);
                }
                else
                {
                    VaccinationViewModel.Update(vaccination);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(DpDate.SelectedDate.Value.ToString().Trim()))
            {
                valid = false;
            }

            DpDate.SelectedDate.Value.ToString().Trim();
            if (string.IsNullOrEmpty(TbManufacturer.Text.Trim())){
                valid = false;
            }

            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }

            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
