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
    /// Interaction logic for EditDogPage.xaml
    /// </summary>
    public partial class EditDogPage : DogFramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Dog? dog;
        public EditDogPage(DogViewModel dogViewModel,
            Dog? dog = null
            ):base(dogViewModel)
        {
            InitializeComponent();
            this.dog = dog ?? new Dog();
            DataContext = dog;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                dog.Name = tbName.Text.Trim();
                dog.Age = int.Parse(tbAge.Text.Trim());
                dog.Breed = tbBreed.Text.Trim();
                dog.DogPicture = ImageUtils.BitmapImageToByteArray(picture.Source as BitmapImage);
                dog.PersonID = DogViewModel.PersonId;
                if (dog.IDDog==0)
                {
                    DogViewModel.Dogs.Add(dog);
                }
                else
                {
                    DogViewModel.UpdateDog(dog);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool ok = true;
            EditDog.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int size)))
                {
                    e.Background = Brushes.LightCoral;
                    ok = false;
                }
                else
                {
                    e.Background = Brushes.White;
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
            var openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog()== true)
            {
                picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
