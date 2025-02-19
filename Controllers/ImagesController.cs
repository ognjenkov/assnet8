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
    private readonly AppDbContext _dbContext;

    public ImagesController(IImageService imageService, IConfiguration configuration, AppDbContext dbContext)
    {
        this._imageService = imageService;
        this._dbContext = dbContext;
    }
    [HttpGet("{ImageId}")]
    public async Task<IActionResult> GetImage([FromRoute] GetImageRequestDto request)
    {
        var image = await _dbContext.Images.FirstOrDefaultAsync(i => i.Id == request.ImageId);

        if (image == null)
        {
            return NotFound("Image not found.");
        }

        var (imageData, contentType) = await _imageService.GetImage(image);

        return File(imageData, contentType);
    }
}