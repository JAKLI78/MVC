using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCTask.Core.Interface;

namespace MVCTask.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public ImageController(IImageService imageService, IUserService userService)
        {
            if (imageService == null)
                throw new ArgumentNullException(nameof(imageService), $"{nameof(imageService)} cannot be null.");
            if (userService == null)
                throw new ArgumentNullException(nameof(userService), $"{nameof(userService)} cannot be null.");
            _imageService = imageService;
            _userService = userService;
        }

        public async Task<FileResult> GetUserImage(int id)
        {
            var fileUrl = await _userService.GetFileUriByIdAsync(id);
            if (!string.IsNullOrEmpty(fileUrl))
            {
                fileUrl = Server.MapPath(fileUrl);
            }
            var imageByteArray = await _imageService.GetResizedImageAsync(fileUrl);
            return File(imageByteArray, "image/jpg");
        }
    }
}