using System.Threading.Tasks;

namespace MVCTask.Core.Interface
{
    public interface IImageService
    {
        Task<byte[]> GetResizedImageAsync(string path);
    }
}