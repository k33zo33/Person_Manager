using PersonManager.Models;
using PersonManager.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonManager.Dal
{
    internal class SqlRepository : IRepository
    {

        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public void AddPerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
            cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
            cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
            cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
            cmd.Parameters.Add(
                new SqlParameter(nameof(Person.Picture), System.Data.SqlDbType.Binary, person.Picture!.Length)
                {
                    Value = person.Picture
                });
            var id = new SqlParameter(nameof(Person.IDPerson), System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
            person.IDPerson = (int)id.Value;
        }

        public void DeletePerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
            cmd.ExecuteNonQuery();
        }

        public IList<Person> GetPeople()
        {
            IList<Person> list = new List<Person>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;        
            cmd.ExecuteNonQuery();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadPerson(dr));
            }
            return list;
        }

        private Person ReadPerson(SqlDataReader dr) => new Person
        {
            IDPerson = (int)dr[nameof(Person.IDPerson)],
            FirstName = dr[nameof(Person.IDPerson)].ToString(),
            LastName = dr[nameof(Person.IDPerson)].ToString(),
            Age = (int)dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromReader(dr, nameof(Person.Picture))
        };

        public Person GetPerson(int idPerson)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), idPerson);
            cmd.ExecuteNonQuery();

            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadPerson(dr);
            }
            throw new ArgumentException("Wrong id, no such person");

        }

        public void UpdatePerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
            cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
            cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
            cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
            cmd.Parameters.Add(
                new SqlParameter(nameof(Person.Picture), System.Data.SqlDbType.Binary, person.Picture!.Length)
                {
                    Value = person.Picture
                });
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);

            cmd.ExecuteNonQuery();
            
        }


        public void AddDog(Dog dog)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(nameof(Dog.Name), dog.Name);
            cmd.Parameters.AddWithValue(nameof(Dog.Age), dog.Age);
            cmd.Parameters.AddWithValue(nameof(Dog.Breed), dog.Breed);

            cmd.Parameters.Add(
                new SqlParameter(nameof(Dog.DogPicture), System.Data.SqlDbType.Binary, dog.DogPicture!.Length)
                {
                    Value = dog.DogPicture
                });
            cmd.Parameters.AddWithValue(nameof(Dog.PersonID), dog.PersonID);

            var id = new SqlParameter(nameof(Dog.IDDog), System.Data.SqlDbType.Int)
            {
               Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(id);
            
            cmd.ExecuteNonQuery();
            dog.IDDog = (int)id.Value;

        }

        public void UpdateDog(Dog dog)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Dog.Name), dog.Name);
            cmd.Parameters.AddWithValue(nameof(Dog.Age), dog.Age);
            cmd.Parameters.AddWithValue(nameof(Dog.Breed), dog.Breed);
            cmd.Parameters.Add(
                new SqlParameter(nameof(Dog.DogPicture), System.Data.SqlDbType.Binary, dog.DogPicture!.Length)
                {
                    Value = dog.DogPicture
                });
            cmd.Parameters.AddWithValue(nameof(Dog.PersonID), dog.PersonID);
            var id = new SqlParameter(nameof(Dog.DogPicture), System.Data.SqlDbType.Int)
            {
                Value= dog.IDDog
            };
            cmd.Parameters.AddWithValue(nameof(Dog.IDDog), dog.IDDog);
            cmd.ExecuteNonQuery();
        }

        public void DeleteDog(Dog dog)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var id = new SqlParameter(nameof(Dog.DogPicture), System.Data.SqlDbType.Int)
            {
                Value = dog.IDDog
            };
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
        }

        public Dog GetDog(int idDog)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue(nameof(Dog.IDDog), idDog);

            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) 
            {
                return ReadDog(dr);
            }
            throw new ArgumentException("Wrong dog id");
        }

        private Dog ReadDog(SqlDataReader dr) => new Dog
        {
            IDDog = (int)dr[nameof(Dog.IDDog)],
            Name = dr[nameof(Dog.Name)].ToString(),
            Age = (int)dr[nameof(Dog.Age)],
            Breed = dr[nameof(Dog.Breed)].ToString(),
            DogPicture = ImageUtils.ByteArrayFromReader(dr, nameof(Dog.DogPicture))
        };

        public IList<Dog> GetDogsForPerson(int personId)
        {
            IList<Dog> dogs = new List<Dog>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod().Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Dog.PersonID), personId);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dogs.Add(ReadDog(dr));
            }
            return dogs;
        }

        public IList<Dog> GetDogs()
        {
            IList<Dog> list = new List<Dog>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadDog(dr));
            }
            return list;
        }
    }
}
