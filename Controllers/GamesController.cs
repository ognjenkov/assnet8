using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Games.Request;
using assnet8.Dtos.Games.Response;
using assnet8.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("games")]
public class GamesController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;

    public GamesController(AppDbContext dbContext, IAccountService accountService)
    {
        this._dbContext = dbContext;
        this._accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        var games = await _dbContext.Games
                                        .Include(g => g.Organization)
                                        .ThenInclude(o => o!.LogoImage)
                                        .Include(g => g.Field)
                                        .ThenInclude(f => f!.ThumbnailImage)
                                        .Include(g => g.Field)
                                        .ThenInclude(f => f!.Location)
                                        .Include(g => g.Tags)
                                        .ToListAsync();

        return Ok(games.Select(g => new GetGamesResponseDto
        {
            Id = g.Id,
            Title = g.Title,
            CreateDateTime = g.CreateDateTime,
            StartDateTime = g.StartDateTime,
            Organization = g.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = g.Organization.Id,
                Name = g.Organization.Name,
                LogoImage = g.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(g.Organization.LogoImage.Id)
                }
            },
            Tags = g.Tags,
            Field = g.Field == null ? null : new FieldSimpleDto
            {
                Id = g.Field.Id,
                Name = g.Field.Name,
                GoogleMapsLink = g.Field.GoogleMapsLink,
                ThumbnailImage = g.Field.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(g.Field.ThumbnailImage.Id)
                },
                Location = g.Field.Location == null ? null : new LocationSimpleDto
                {
                    Id = g.Field.Location.Id,
                    Region = g.Field.Location.Region
                }
            }
        }));
    }

    [HttpGet("{GameId}")]
    public async Task<IActionResult> GetGame([FromRoute] GetGameRequestDto request)
    {
        //ako je logged in dobija i prijave, ako ne samo game
        var game = await _dbContext.Games
                                .Where(g => g.Id == request.GameId)
                                .Include(g => g.Organization)
                                .ThenInclude(o => o!.LogoImage)
                                .Include(g => g.Field)
                                .ThenInclude(f => f!.ThumbnailImage)
                                .Include(g => g.Field)
                                .ThenInclude(f => f!.Location)
                                .Include(g => g.Tags)
                                .FirstOrDefaultAsync();

        if (game == null) return NotFound("Game not found");

        return Ok(new GetGameResponseDto
        {
            Id = game.Id,
            Title = game.Title,
            CreateDateTime = game.CreateDateTime,
            StartDateTime = game.StartDateTime,
            Description = game.Description,
            Organization = game.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = game.Organization.Id,
                Name = game.Organization.Name,
                LogoImage = game.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(game.Organization.LogoImage.Id)
                }
            },
            Tags = game.Tags,
            Field = game.Field == null ? null : new FieldSimpleDto
            {
                Id = game.Field.Id,
                Name = game.Field.Name,
                GoogleMapsLink = game.Field.GoogleMapsLink,
                ThumbnailImage = game.Field.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(game.Field.ThumbnailImage.Id)
                },
                Location = game.Field.Location == null ? null : new LocationSimpleDto
                {
                    Id = game.Field.Location.Id,
                    Region = game.Field.Location.Region
                }
            }
        });
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (userId != Guid.Parse(userId).ToString()) return Unauthorized(); //TODO zasto uopste proveravam ova 3

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        //ovo se jedino desava ako tim nema organizaciju, zbog autorizacije
        if (organizationId == null) return Unauthorized("Your team does not have an organization");

        List<Tag> tags = (List<Tag>?)HttpContext.Items["ValidatedTags"] ?? [];

        var organizationOwnsField = await _dbContext.Fields
                                        .AnyAsync(f => f.Id == request.FieldId && f.OrganizationId == organizationId);

        if (!organizationOwnsField) return NotFound("Field not found");// mozda neki drugi error?

        var game = new Game
        {
            Title = request.Title,
            StartDateTime = request.StartDateTime,
            Description = request.Description,
            FieldId = request.FieldId,
            OrganizationId = (Guid)organizationId,
            Tags = tags
        };

        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();

        return StatusCode(201, game.Id);
    }

    [HttpDelete]
    public IActionResult DeleteGame()
    {
        return Ok("Create game");
    }


}