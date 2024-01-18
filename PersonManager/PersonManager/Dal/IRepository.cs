using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.Dal
{
    public interface IRepository
    {
        //person
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        Person GetPerson(int idPerson);
        IList<Person> GetPeople();

        //dog
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(Dog dog);
        Dog GetDog(int idDog);
        IList<Dog> GetDogsForPerson(int personId);
        IList<Dog> GetDogs();


    }
}
