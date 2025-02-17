using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assnet8.Dtos.Images.Request;
using assnet8.Services.Images;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class ImagesController : BaseController
{
    private readonly IImageService _imageService;
    private readonly string _awsUrl;

    public ImagesController(IImageService imageService, IConfiguration configuration)
    {
        this._imageService = imageService;
        this._awsUrl = configuration["AWS:S3:url"] ?? "string";
    }
    [HttpGet("{ImageId}")]
    public async Task<IActionResult> GetImage([FromRoute] GetImageRequestDto request)
    {
        string imageUrl = _awsUrl;
        try
        {
            var image = await _imageService.GetImage(request.ImageId);

            imageUrl += image.Url;
        }
        catch (System.Exception)
        {
            return NotFound("Image not found.");
        }
        System.Console.WriteLine("Image URL: " + imageUrl);
        using (var httpClient = new HttpClient())
        {
            // Fetch the image from the custom address
            var response = await httpClient.GetAsync(imageUrl);

            // If the request was successful, return the image
            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/jpeg";
                System.Console.WriteLine("Content type: " + contentType);
                return File(imageBytes, "image/png");
            }
            else
            {
                return NotFound("Image not found.");
            }
        }
    }
}