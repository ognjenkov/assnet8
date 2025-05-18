using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Pagination;
using assnet8.Dtos.Teams.Request;
using assnet8.Dtos.Teams.Response;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Route("teams")]
public class TeamsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ICloudImageService _imageService;
    private readonly INextJsRevalidationService _nextJsRevalidationService;

    public TeamsController(AppDbContext dbContext, ICloudImageService imageService, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
        this._nextJsRevalidationService = nextJsRevalidationService;
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
                                    .AsSplitQuery()
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
            LocationId = request.LocationId,
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

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/teams/{team.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("teams"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


        return StatusCode(201, team.Id);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<GetTeamsResponseDto>>> GetTeams([FromQuery] GetTeamsRequestDto request)
    {
        var query = _dbContext.Teams
                                        .AsSplitQuery()
                                        .Include(t => t.LogoImage)
                                        .Include(t => t.Memberships)
                                        .Include(t => t.LogoImage)
                                        .Include(t => t.Location)
                                        .OrderByDescending(s => s.CreateDateTime)
                                        .AsQueryable();

        var totalCount = await query.CountAsync();

        var teams = await query
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync();

        var teamDtos = teams.Select(t => new GetTeamsResponseDto
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
        });

        return Ok(new PaginatedResponseDto<GetTeamsResponseDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = teamDtos
        });
    }

    [HttpGet("ids")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetTeamIds()
    {
        var ids = await _dbContext.Teams
        .AsNoTracking()
        .Select(p => p.Id)
        .ToListAsync();

        return Ok(ids);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateTeam()
    {
        // try
        // {
        //     await Task.WhenAll(
        //             _nextJsRevalidationService.RevalidatePathAsync($"/teams/{team.Id}"),
        //             _nextJsRevalidationService.RevalidateTagAsync("teams"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}-simple"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}") 
        //         );
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex);
        // }

        return Ok("Update team");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeam()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var team = await _dbContext.Teams
            .AsSplitQuery()
            .Include(t => t.Galleries)
                .ThenInclude(g => g.Images)
            .Include(t => t.Memberships)
            .Include(t => t.Invites)
            .Include(t => t.LogoImage)
            .Include(t => t.Organization)
            .FirstOrDefaultAsync(t => t.CreatorId == userGuid);
        if (team == null) return NotFound("Team not found");

        if (team.Organization != null)
        {
            return BadRequest("You cannot disband a team that owns an organization, you must delete the organization first.");
        }

        _dbContext.Memberships.RemoveRange(team.Memberships);
        _dbContext.RemoveRange(team.Invites);
        await _dbContext.SaveChangesAsync();

        try
        {
            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Team cannot be deleted due to existing references.");
        }

        if (team.LogoImage != null)
        {
            await _imageService.DeleteImage(team.LogoImage);
        }
        foreach (var gallery in team.Galleries)
        {
            foreach (var image in gallery.Images)
            {
                await _imageService.DeleteImage(image);
            }

            _dbContext.Galleries.Remove(gallery);

            try
            {
                await Task.WhenAll(
                    _nextJsRevalidationService.RevalidateTagAsync($"gallery-{gallery.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"gallery-{gallery.Id}-simple")
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        await _dbContext.SaveChangesAsync();

        foreach (var membership in team.Memberships)
        {
            try // TODO za svaki membership
            {
                await Task.WhenAll(
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{membership.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{membership.Id}-simple")
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/teams/{team.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("teams"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{team.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Ok("delete team");
    }

    [HttpGet("{TeamId}")]
    public async Task<ActionResult<GetTeamResponseDto>> GetTeam([FromRoute] GetTeamRequestDto request)
    {
        var team = await _dbContext.Teams
                                .Where(t => t.Id == request.TeamId)
                                .AsSplitQuery()
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
            }).ToList(),
            CreateDateTime = team.CreateDateTime
        });
    }

    [HttpGet("{TeamId}/card")]
    public Task<IActionResult> GetTeamCard([FromRoute] GetTeamRequestDto request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{TeamId}/simple")]
    public async Task<ActionResult<TeamSimpleDto>> GetTeamSimple([FromRoute] GetTeamSimpleRequestDto request)
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
