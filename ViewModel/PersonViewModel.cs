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
    public class PersonViewModel
    {
        public ObservableCollection<Person> People { get; }
        public PersonViewModel()
        {
            People = new ObservableCollection<Person>(RepositoryFactory.GetRepository().GetPeople());
            People.CollectionChanged += People_CollectionChanged;
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddPerson(People[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeletePerson(e.OldItems.OfType<Person>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdatePerson(e.NewItems.OfType<Person>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Person person) => People[People.IndexOf(person)] = person;
    }
}
