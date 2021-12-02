using PPPK_Delivery_2_Dominik_Hruza_3IP1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1.Models
{
    public class Vaccination
    {
        public int IDVaccination { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string Manufacturer { get; set; }
        public byte[] ManufacturerPicture { get; set; }
        public BitmapImage Image
        {
            get => ImageUtils.ByteArrayToBitmapImage(ManufacturerPicture);
        }
        public int PersonID { get; set; }
    }
}
