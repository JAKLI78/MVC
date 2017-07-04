using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCTask.Core.Interface;

namespace MVCTask.Controllers
{
    public class AsyncImageController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public AsyncImageController(IImageService imageService, IUserService userService)
        {
            if (imageService == null)
                throw new ArgumentNullException(nameof(imageService), $"{nameof(imageService)} cannot be null.");
            if (userService == null)
                throw new ArgumentNullException(nameof(userService), $"{nameof(userService)} cannot be null.");
            _imageService = imageService;
            _userService = userService;
        }

        public async Task<FileResult> RenderImage(int id)
        {
            var fileUrl = await _userService.AsyncGetFileUrlById(id);
            var someByte = await _imageService.GetImage(fileUrl);
            return File(someByte, "image/jpg");
        }
    }
}