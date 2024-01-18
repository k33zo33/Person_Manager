using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PersonManager.Utils
{
    public static class ImageUtils
    {
        internal static BitmapImage ByteArrayToBitmapImage(byte[] picture)
        {
            using var memoryStream = new MemoryStream(picture);

            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.StreamSource = memoryStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
        internal static byte[] BitmapImageToByteArray(BitmapImage image)
        {
            var jpegEncdoer = new JpegBitmapEncoder();
            jpegEncdoer.Frames.Add(BitmapFrame.Create(image));  
            using var memoryStream = new MemoryStream();
            jpegEncdoer.Save(memoryStream);

            return memoryStream.ToArray();
        }

        internal static byte[] ByteArrayFromReader(SqlDataReader dr, string column)
        {
            var memoryStream = new MemoryStream();
            var binaryWritter = new BinaryWriter(memoryStream);

            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];

            int currentBytes = 0;
            int readBytes;

            do
            {
               readBytes = (int)dr.GetBytes(
                    dr.GetOrdinal(column),
                    currentBytes,
                    buffer,
                    0,
                    bufferSize
                    );
                binaryWritter.Write(buffer, 0, readBytes);

                currentBytes += readBytes;
            } while (readBytes == bufferSize);




            return memoryStream.ToArray();
        }
    }
}
