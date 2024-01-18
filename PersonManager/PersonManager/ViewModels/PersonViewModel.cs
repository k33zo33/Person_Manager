using PersonManager.Dal;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.ViewModels
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> People { get; }

        public PersonViewModel()
        {
            People = new ObservableCollection<Person>(
                RepositoryFactory.GetRepository().GetPeople()
                );

            People.CollectionChanged += People_CollectionChanged;
    }

        private void People_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddPerson(
                        People[e.NewStartingIndex]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeletePerson(
                       e.OldItems!.OfType<Person>().ToList()[0]);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdatePerson(
                      e.NewItems!.OfType<Person>().ToList()[0]);
                    break;
                
            }
        }
        public void UpdatePerson(Person person) => People[People.IndexOf(person)] = person;
    }
}
