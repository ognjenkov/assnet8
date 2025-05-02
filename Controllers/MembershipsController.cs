using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using assnet8.Dtos.Memberships;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Authorize]
[Route("memberships")]
public class MembershipsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly INextJsRevalidationService _nextJsRevalidationService;

    public MembershipsController(AppDbContext dbContext, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._nextJsRevalidationService = nextJsRevalidationService;
    }

    [HttpGet("{TeamId}")]
    public IActionResult GetTeamMemberships([FromRoute] string TeamId)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpGet("{TeamId}/simple")]
    public async Task<IActionResult> GetTeamMembershipsSimple([FromRoute] GetTeamMembershipsSimpleRequestDto request)
    {
        var memberships = await _dbContext.Memberships
                            .Where(m => m.TeamId == request.TeamId)
                            .Include(m => m.Roles)
                            .Include(m => m.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .ToListAsync();

        if (memberships == null) return NotFound("Memberships not found");

        return Ok(memberships.Select(m => new MembershipSimpleDto
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
        }));
    }

    [Authorize]
    [HttpGet("{TeamId}/{MembershipId}")]
    public IActionResult GetTeamMembership([FromRoute] string TeamId, [FromRoute] string MembershipId)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpGet("{TeamId}/{MembershipId}/simple")]
    public async Task<IActionResult> GetTeamMembershipSimple([FromRoute] GetTeamMembershipSimpleRequestDto request)
    {
        var membership = await _dbContext.Memberships
                            .Where(m => m.Id == request.MembershipId)
                            .Include(m => m.Roles)
                            .Include(m => m.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

        return Ok(new MembershipSimpleDto
        {
            Id = membership.Id,
            CreateDateTime = membership.CreateDateTime,
            Roles = membership.Roles,
            User = new UserSimpleDto
            {
                Id = membership.UserId,
                Username = membership.User!.Username,
                ProfileImage = membership.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(membership.User.ProfileImage.Id)
                }
            }
        });
    }

    [HttpPost("invite")]
    public IActionResult InviteUserToTeam()
    {
        throw new NotImplementedException();
    }



}