using PPPK_Delivery_2_Dominik_Hruza_3IP1.Dal;
using PPPK_Delivery_2_Dominik_Hruza_3IP1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1.ViewModel
{
    public class VaccinationViewModel
    {
        public ObservableCollection<Vaccination> Vaccinations { get; }
        public int PersonId { get; set; }
        public VaccinationViewModel(int personId)
        {
            PersonId = personId;
            Vaccinations = new ObservableCollection<Vaccination>(RepositoryFactory.GetRepository().GetVaccinationsForPerson(personId));
            Vaccinations.CollectionChanged += People_CollectionChanged;
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddVaccination(Vaccinations[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteVaccination(e.OldItems.OfType<Vaccination>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateVaccination(e.NewItems.OfType<Vaccination>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Vaccination vaccination) => Vaccinations[Vaccinations.IndexOf(vaccination)] = vaccination;
    }
}

