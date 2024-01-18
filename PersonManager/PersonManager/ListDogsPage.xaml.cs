using PersonManager.Models;
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
    /// Interaction logic for ListDogsPage.xaml
    /// </summary>
    public partial class ListDogsPage : DogFramedPage
    {
        public ListDogsPage(DogViewModel dogViewModel) : base(dogViewModel)
        {
            InitializeComponent();
            lvDogs.ItemsSource = DogViewModel.Dogs;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame?.Navigate(new EditDogPage(DogViewModel)
            {
                Frame = Frame
            });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvDogs.SelectedItem!=null)
            {
                Frame?.Navigate(new EditDogPage(
                    DogViewModel, 
                    lvDogs.SelectedItem as Dog)
                {
                    Frame = Frame
                });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvDogs.SelectedItem != null)
            {
                DogViewModel.Dogs.Remove((lvDogs.SelectedItem as Dog)!);
            }
        }
    }
}
