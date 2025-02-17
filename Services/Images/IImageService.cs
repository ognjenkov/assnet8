using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Images
{
    public interface IImageService
    {
        public Task<Image> UploadImage(User user, IFormFile imageFile);
        public Task<Image> GetImage(Guid imageId);
        public Task DeleteImage(Guid imageId);
    }
}