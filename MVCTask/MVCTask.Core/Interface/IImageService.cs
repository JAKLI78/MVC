using System.Threading.Tasks;

namespace MVCTask.Core.Interface
{
    public interface IImageService
    {
        Task<byte[]> GetImageAsync(string path);
    }
}