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
    public async Task<ActionResult<UserSimpleDto>> GetUserSimple([FromRoute] GetUserSimpleRequestDto request)
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


        // var user = await _accountService.GetAccountFromUserId(request.UserId);
        // if (user == null) return NotFound("User not found");

        // return Ok(new GetAccountCardResponseDto
        // {
        //     Id = user.Id,
        //     Username = user.Username,
        //     ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
        //     {
        //         Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
        //     },
        //     Services = user.Services?.Select(s => new ServiceSimpleDto
        //     {
        //         Id = s.Id,
        //         Title = s.Title,
        //         CreatedDateTime = s.CreatedDateTime,
        //         ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
        //         {
        //             Url = Utils.Utils.GenerateImageFrontendLink(s.ThumbnailImage.Id)
        //         }
        //     }).ToList() ?? [],
        //     Organization = user.Organization == null ? null : new OrganizationSimpleDto
        //     {
        //         Id = user.Organization.Id,
        //         Name = user.Organization.Name,
        //         LogoImage = user.Organization.LogoImage == null ? null : new ImageSimpleDto
        //         {
        //             Url = Utils.Utils.GenerateImageFrontendLink(user.Organization.LogoImage.Id)
        //         }
        //     },
        //     Membership = user.Membership == null ? null : new MembershipSimpleDto
        //     {
        //         Id = user.Membership.Id,
        //         CreateDateTime = user.Membership.CreateDateTime,
        //         Roles = user.Membership.Roles,
        //         Team = user.Membership.Team == null ? null : new TeamSimpleDto
        //         {
        //             Id = user.Membership.Team.Id,
        //             Name = user.Membership.Team.Name,
        //             LogoImage = user.Membership.Team.LogoImage == null ? null : new ImageSimpleDto
        //             {
        //                 Url = Utils.Utils.GenerateImageFrontendLink(user.Membership.Team.LogoImage.Id)
        //             }
        //         },
        //     }
        // });
    }
}