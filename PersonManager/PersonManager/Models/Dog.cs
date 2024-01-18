using PersonManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PersonManager.Models
{
    public class Dog
    {


        public int IDDog { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Breed { get; set; }
        public byte[]? DogPicture { get; set; }
        public int PersonID { get; set; }

        public BitmapImage Image 
        { 
            get => ImageUtils.ByteArrayToBitmapImage(DogPicture);
        }


    }
}
