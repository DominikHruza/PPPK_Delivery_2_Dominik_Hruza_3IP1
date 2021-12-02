using PPPK_Delivery_2_Dominik_Hruza_3IP1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1
{
    public class VaxFramedPage : Page
    {
        public VaxFramedPage(VaccinationViewModel vaccinationViewModel)
        {
            VaccinationViewModel = vaccinationViewModel;
        }
        public VaccinationViewModel VaccinationViewModel { get; }
        public Frame Frame { get; set; }
    }
}
