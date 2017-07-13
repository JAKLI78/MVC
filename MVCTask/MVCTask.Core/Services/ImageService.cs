using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using MVCTask.Core.Interface;

namespace MVCTask.Core.Services
{
    public class ImageService : IImageService
    {
        public async Task<byte[]> GetResizedImageAsync(string path)
        {
            if (path!=null)
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var img = new Bitmap(fileStream);
                fileStream.Close();
                return ImageResizeAsync(img);
            }
            return new byte[]{};
        }

        private static  byte[] ImageResizeAsync(Image image)
        {            
            image = new Bitmap(image, 50, 50);
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);

                return stream.ToArray();
            }            
        }
    }
}