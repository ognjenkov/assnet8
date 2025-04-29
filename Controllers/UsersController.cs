using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using assnet8.Dtos.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Route("users")]
public class UsersController : BaseController
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpGet("{UserId}/simple")]
    public async Task<IActionResult> GetUserSimple([FromRoute] GetUserSimpleRequestDto request)
    {
        var user = await _dbContext.Users
                                     .Where(u => u.Id == request.UserId)
                                     .Include(u => u.ProfileImage)
                                     .FirstOrDefaultAsync();


        if (user == null) return NotFound("User not found");

        return Ok(new UserSimpleDto
        {
            Id = user.Id,
            Username = user.Username,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
            }
        });
    }

    [Authorize]
    [HttpGet("{UserId}/card")]
    public async Task<IActionResult> GetUserCard([FromRoute] GetUserCardRequestDto request)
    {
        var user = await _dbContext.Users
                                     .Where(u => u.Id == request.UserId)
                                     .Include(u => u.ProfileImage)
                                     .FirstOrDefaultAsync();


        if (user == null) return NotFound("User not found");

        throw new NotImplementedException();
    }
}