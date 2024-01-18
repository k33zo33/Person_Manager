using PersonManager.Dal;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.ViewModels
{
    public class DogViewModel
    {
        public ObservableCollection<Dog> Dogs { get; }
        public int PersonId { get; set; }
        public DogViewModel(int personId)
        {
            PersonId = personId;
            Dogs = new ObservableCollection<Dog>(
                RepositoryFactory.GetRepository().GetDogs());
            Dogs.CollectionChanged += Dogs_CollectionChanged;
        }

        private void Dogs_CollectionChanged(object? sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository()
                        .AddDog(Dogs[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository()
                        .DeleteDog(e.OldItems!.OfType<Dog>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository()
                        .UpdateDog(e.NewItems!.OfType<Dog>().ToList()[0]);
                    break;
               
            }
        }
        public void UpdateDog(Dog dog)=> Dogs[Dogs.IndexOf(dog)] = dog;
    }
}
