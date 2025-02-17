using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace assnet8.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _dbContext;
        private readonly string _awsUrl;
        public ImageService(AppDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._awsUrl = configuration["AWS:S3:url"] ?? "string";
        }
        public Task DeleteImage(Guid imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetImage(Guid imageId)
        {
            var image = await _dbContext.Images.FirstOrDefaultAsync(i => i.Id == imageId);

            if (image == null)
            {
                throw new Exception("Image not found.");
            }
            return image;
        }

        public async Task<Image> UploadImage(User user, IFormFile imageFile)
        {
            var urlId = Guid.NewGuid();
            var imageUrl = _awsUrl + urlId;

            var image = new Image
            {
                Title = imageFile.FileName,
                Url = urlId.ToString(),
                Extension = Path.GetExtension(imageFile.FileName).ToLower(),
                UserId = user.Id,
                ProfileImageUser = user
            };

            using (var client = new HttpClient())
            {
                using (var stream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    using (var content = new StreamContent(stream))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                        var response = await client.PutAsync(imageUrl, content);

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Failed to upload image. Status Code: {response.StatusCode}");
                        }
                    }
                }
            }

            return image;
        }
    }
}