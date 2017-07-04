using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using MVCTask.Core.Interface;

namespace MVCTask.Core.Services
{
    public class ImageService : IImageService
    {
        public async Task<byte[]> GetImage(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var img = new Bitmap(fileStream);
            fileStream.Close();

            return await ImageResize(img);
        }


        private static async Task<byte[]> ImageResize(Image image)
        {
            return await Task.Run(() =>
            {
                image = new Bitmap(image, 50, 50);
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);

                    return stream.ToArray();
                }
            });
        }
    }
}