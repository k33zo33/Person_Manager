using Microsoft.Win32;
using PersonManager.Models;
using PersonManager.Utils;
using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonManager
{
    /// <summary>
    /// Interaction logic for EditPersonPage.xaml
    /// </summary>
    public partial class EditPersonPage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";

        private readonly Person person;
        private DogViewModel dogViewModel;

        public EditPersonPage(PersonViewModel personViewModel,
            Person? person = null
            )
            :base(personViewModel)
        {
            InitializeComponent();
            if (person==null)
            {
                dogViewModel = new DogViewModel(0);
            }
            else
            {
                dogViewModel = new DogViewModel(person.IDPerson);
            }
            this.person = person ?? new Person();
            DataContext = person;
            lvDogs.ItemsSource = dogViewModel!.Dogs;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame?.NavigationService.GoBack();
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                person!.FirstName = tbFirstName.Text.Trim();
                person!.LastName = tbLastName.Text.Trim();
                person!.Email = tbEmail.Text.Trim();
                person!.Age = int.Parse(tbAge.Text.Trim());
                person!.Picture = ImageUtils.BitmapImageToByteArray((picture.Source as BitmapImage)!);

                if (person.IDPerson == 0)
                {
                    PersonViewModel.People.Add(person);
                }
                else
                {
                    PersonViewModel.UpdatePerson(person);
                }

                Frame?.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool ok = true;
            grid.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                e.Background = Brushes.White;
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || "Int".Equals(e.Tag) && !int.TryParse(e.Text, out int r)
                    || "Email".Equals(e.Tag) && !ValidationUtils.IsValidEmail(e.Text)
                )
                {
                    ok = false;
                    e.Background = Brushes.LightCoral;
                }

            });

            pictureBorder.BorderBrush = Brushes.White;
            if (picture.Source == null)
            {
                ok = false;
                pictureBorder.BorderBrush = Brushes.LightCoral;
            }

            return ok;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = Filter };
            if (dialog.ShowDialog()==true)
            {
                picture.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void BtnAddDog_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditDogPage(dogViewModel)
            {
                Frame=Frame
            });
        }


        private void BtnEditDog_Click(object sender, RoutedEventArgs e)
        {
            if (lvDogs.SelectedItem != null) 
            {
                Frame.Navigate(new EditDogPage(dogViewModel, lvDogs.SelectedItem as Dog) { Frame=Frame });
            }
        }

        private void BtnDeleteDog_Click(object sender, RoutedEventArgs e)
        {
            if (lvDogs.SelectedItem != null)
            {
                dogViewModel.Dogs.Remove(lvDogs.SelectedItem as Dog);
            }
        }
    }
}
