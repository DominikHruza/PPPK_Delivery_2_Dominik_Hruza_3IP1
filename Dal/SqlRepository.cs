using PPPK_Delivery_2_Dominik_Hruza_3IP1.Models;
using PPPK_Delivery_2_Dominik_Hruza_3IP1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1.Dal
{
    class SqlRepository : IRepository
    {
        private const string FirstNameParam = "@firstname";
        private const string LastNameParam = "@lastname";
        private const string AgeParam = "@age";
        private const string EmailParam = "@email";
        private const string PictureParam = "@picture";
        private const string IdPersonParam = "@idPerson";

        private const string VaccinationDateParam = "@vaccinationDate";
        private const string ManufacturerParam = "@manufacturer";
        private const string ManufacturerPictureParam = "@manufacturerPicture";
        private const string IDVaccinationParam = "@idVaccination";
        private const string PersonIdParam = "@personID";

        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public void AddPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    SqlParameter idPerson = new SqlParameter(IdPersonParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPerson);
                    cmd.ExecuteNonQuery();
                    person.IDPerson = (int)idPerson.Value;
                }
            }
        }

        public void DeletePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Person> GetPeople()
        {
            IList<Person> people = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            people.Add(ReadPerson(dr));
                        }
                    }
                }
            }
            return people;
        }

        public Person GetPerson(int idPerson)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, idPerson);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadPerson(dr);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        private Person ReadPerson(SqlDataReader dr) => new Person
        {
            IDPerson = (int)dr[nameof(Person.IDPerson)],
            FirstName = dr[nameof(Person.FirstName)].ToString(),
            LastName = dr[nameof(Person.LastName)].ToString(),
            Age = (int)dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)
        };

        public void UpdatePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddVaccination(Vaccination vaccination)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(VaccinationDateParam, vaccination.VaccinationDate.Date);
                    cmd.Parameters.AddWithValue(ManufacturerParam, vaccination.Manufacturer);
                    cmd.Parameters.AddWithValue(PersonIdParam, vaccination.PersonID);
                    cmd.Parameters.Add(new SqlParameter(ManufacturerPictureParam, SqlDbType.Binary, vaccination.ManufacturerPicture.Length)
                    {
                        Value = vaccination.ManufacturerPicture
                    });
                    SqlParameter idVaccination = new SqlParameter(IDVaccinationParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idVaccination);
                    cmd.ExecuteNonQuery();
                    vaccination.IDVaccination = (int)idVaccination.Value;
                }
            }
        }

        public void DeleteVaccination(Vaccination vaccination)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDVaccinationParam, vaccination.IDVaccination);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Vaccination> GetVaccinationsForPerson(int personId)
        {
            IList<Vaccination> vaccinations = new List<Vaccination>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(PersonIdParam, personId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vaccinations.Add(ReadVaccination(dr));
                        }
                    }
                }
            }
            return vaccinations;
        }

        public Vaccination GetVaccination(int idVaccinartion)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDVaccinationParam, IDVaccinationParam);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadVaccination(dr);
                        }
                    }
                }
            }
            throw new Exception("Vaccination does not exist");
        }

        public void UpdateVaccination(Vaccination vaccination)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDVaccinationParam, vaccination.IDVaccination);
                    cmd.Parameters.AddWithValue(VaccinationDateParam, vaccination.VaccinationDate.Date);
                    cmd.Parameters.AddWithValue(ManufacturerParam, vaccination.Manufacturer);
                    cmd.Parameters.AddWithValue(PersonIdParam, vaccination.PersonID);
                    cmd.Parameters.Add(new SqlParameter(ManufacturerPictureParam, SqlDbType.Binary, vaccination.ManufacturerPicture.Length)
                    {
                        Value = vaccination.ManufacturerPicture
                    });
       
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Vaccination ReadVaccination(SqlDataReader dr) => new Vaccination
        {
            IDVaccination = (int)dr[nameof(Vaccination.IDVaccination)],
            VaccinationDate = (DateTime)dr[nameof(Vaccination.VaccinationDate)],
            Manufacturer = dr[nameof(Vaccination.Manufacturer)].ToString(),
            ManufacturerPicture = ImageUtils.ByteArrayFromSqlDataReader(dr, 3),
            PersonID = (int)dr[nameof(Vaccination.PersonID)]
        };
    }
}
