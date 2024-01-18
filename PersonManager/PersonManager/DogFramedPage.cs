using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonManager
{
    public class DogFramedPage : Page
    {
        public DogFramedPage(DogViewModel dogViewModel)
        {
            DogViewModel = dogViewModel;
        }
        public DogViewModel DogViewModel { get; }
        public Frame? Frame { get; set; }
    }
}
