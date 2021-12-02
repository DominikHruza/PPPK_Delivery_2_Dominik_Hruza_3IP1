using PPPK_Delivery_2_Dominik_Hruza_3IP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1.Dal
{
    public interface IRepository
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        IList<Person> GetPeople();
        Person GetPerson(int idPerson);
        void UpdatePerson(Person person);

        void AddVaccination(Vaccination vaccination);
        void DeleteVaccination(Vaccination vaccination);
        IList<Vaccination> GetVaccinationsForPerson(int personId);
        Vaccination GetVaccination(int idVaccination);
        void UpdateVaccination(Vaccination vaccination);
    }
}
