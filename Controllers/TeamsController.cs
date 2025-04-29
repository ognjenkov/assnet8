using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Teams.Request;
using assnet8.Dtos.Teams.Response;
using assnet8.Services.Images;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("teams")]
public class TeamsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ICloudImageService _imageService;

    public TeamsController(AppDbContext dbContext, ICloudImageService imageService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromForm] CreateTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                                    .Where(u => u.Id == Guid.Parse(userId))
                                    .Include(u => u.Membership)
                                    .Include(u => u.Organization)
                                    .FirstOrDefaultAsync();

        if (user == null) return NotFound("User not found");

        if (user.Organization != null || user.Membership != null) return BadRequest("User already in team/organization");

        var roles = await _dbContext.Roles
                                .Where(r => r.Name == Roles.Creator)
                                .ToListAsync();

        var team = new Team
        {
            Name = request.Name,
            CreatorId = user.Id,
        };
        await _dbContext.Teams.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        var membership = new Membership
        {
            TeamId = team.Id,
            UserId = user.Id,
            Roles = roles,
        };
        await _dbContext.Memberships.AddAsync(membership);//TODO mozda AddAsync
        await _dbContext.SaveChangesAsync();

        if (request.TeamImage != null)
        {
            try
            {
                var image = await _imageService.UploadImage(user, request.TeamImage);
                await _dbContext.Images.AddAsync(image);

                team.LogoImageId = image.Id;

                await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Failed to upload profile image");
                // throw;
            }

        }


        return StatusCode(201, team.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _dbContext.Teams
                                        .Include(t => t.LogoImage)
                                        .Include(t => t.Memberships)
                                        .Include(t => t.LogoImage)
                                        .ToListAsync();

        return Ok(teams.Select(t => new GetTeamsResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            LogoImage = t.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(t.LogoImage.Id)
            },
            Location = t.Location == null ? null : new LocationSimpleDto
            {
                Id = t.Location.Id,
                Region = t.Location.Region
            }
        }));
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateTeam()
    {
        return Ok("Update team");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpDelete]
    public IActionResult DeleteTeam()
    {
        return Ok("delete team");
    }

    [HttpGet("{TeamId}")]
    public async Task<IActionResult> GetTeam([FromRoute] GetTeamRequestDto request)
    {
        var team = await _dbContext.Teams
                                .Where(t => t.Id == request.TeamId)
                                .Include(t => t.Galleries)
                                    .ThenInclude(g => g!.Images)
                                .Include(t => t.Galleries)
                                    .ThenInclude(g => g!.User)
                                        .ThenInclude(u => u!.ProfileImage)
                                .Include(t => t.Organization)
                                    .ThenInclude(o => o!.LogoImage)
                                .Include(t => t.Location)
                                .Include(t => t.LogoImage)
                                .Include(t => t.Memberships)
                                    .ThenInclude(m => m.User)
                                        .ThenInclude(u => u!.ProfileImage)
                                .Include(t => t.Creator)
                                    .ThenInclude(u => u!.ProfileImage)
                                .FirstOrDefaultAsync();

        if (team == null) return NotFound("Team not found");

        return Ok(new GetTeamResponseDto
        {
            Id = team.Id,
            Name = team.Name,
            Galleries = team.Galleries.Select(g => new GallerySimpleDto
            {
                Title = g.Title,
                Images = g.Images.Select(i => new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(i.Id)
                }).ToList(),
                CreateDateTime = g.CreateDateTime,
                User = g.User == null ? null : new UserSimpleDto
                {
                    Id = g.User.Id,
                    Username = g.User.Username,
                    ProfileImage = g.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = Utils.Utils.GenerateImageFrontendLink(g.User.ProfileImage.Id)
                    }
                }
            }).ToList(),
            Organization = team.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = team.Organization.Id,
                Name = team.Organization.Name,
                LogoImage = team.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(team.Organization.LogoImage.Id)
                }
            },
            Creator = team.Creator == null ? null : new UserSimpleDto
            {
                Id = team.Creator.Id,
                Username = team.Creator.Username,
                ProfileImage = team.Creator.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(team.Creator.ProfileImage.Id)
                }
            },
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(team.LogoImage.Id)
            },
            Location = team.Location == null ? null : new LocationSimpleDto
            {
                Id = team.Location.Id,
                Region = team.Location.Region
            },
            Memberships = team.Memberships.Select(m => new MembershipSimpleDto
            {
                Id = m.Id,
                CreateDateTime = m.CreateDateTime,
                Roles = [],
                User = new UserSimpleDto
                {
                    Id = m.UserId,
                    Username = m.User!.Username,
                    ProfileImage = m.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = Utils.Utils.GenerateImageFrontendLink(m.User.ProfileImage.Id)
                    }
                }
            }).ToList()
        });
    }

    [HttpGet("{TeamId}/card")]
    public Task<IActionResult> GetTeamCard([FromRoute] GetTeamRequestDto request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{TeamId}/simple")]
    public async Task<IActionResult> GetTeamSimple([FromRoute] GetTeamSimpleRequestDto request)
    {
        var team = await _dbContext.Teams
                                .Where(t => t.Id == request.TeamId)
                                .Include(t => t.LogoImage)
                                .FirstOrDefaultAsync();

        if (team == null) return NotFound("Team not found");

        return Ok(new TeamSimpleDto
        {
            Id = team.Id,
            Name = team.Name,
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(team.LogoImage.Id)
            }
        });
    }

}