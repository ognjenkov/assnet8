using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace assnet8.Services.Images;

public class ImageService : IImageService
{
    private readonly AppDbContext _dbContext;
    private readonly string _awsUrl;
    public ImageService(AppDbContext dbContext, IConfiguration configuration)
    {
        this._dbContext = dbContext;
        this._awsUrl = configuration["AWS:S3:url"] ?? "string";
    }
    public Task DeleteImage(Image image)
    {
        throw new NotImplementedException();
    }

    public async Task<(byte[] ImageData, string ContentType)> GetImage(Image image)
    {
        using (var httpClient = new HttpClient())
        {
            var imageAwsURL = _awsUrl + image.S3Id;
            var response = await httpClient.GetAsync(imageAwsURL);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Image not found.");
            }

            var imageBytes = await response.Content.ReadAsByteArrayAsync();
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/jpeg";

            return (imageBytes, contentType);
        }
    }
    public async Task<Image> UploadImage(User user, IFormFile imageFile)
    {
        var S3Id = Guid.NewGuid();
        var imageUrl = _awsUrl + S3Id;

        var image = new Image
        {
            Title = imageFile.FileName,
            S3Id = S3Id,
            Extension = Path.GetExtension(imageFile.FileName).ToLower(),
            UserId = user.Id,
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