using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Entries.Request;
using assnet8.Dtos.Entries.Response;
using assnet8.Services.Entries;
using assnet8.Services.SignalR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace assnet8.Controllers;
[Authorize]
[Route("entries")]
public class EntriesController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IHubContext<EntriesHub> _entriesHub;

    public EntriesController(AppDbContext dbContext, IHubContext<EntriesHub> entriesHub)
    {
        this._dbContext = dbContext;
        this._entriesHub = entriesHub;
    }
    [HttpGet("game/{GameId}")]
    public async Task<ActionResult<IEnumerable<GetGameEntriesResponseDto>>> GetGameEntries([FromRoute] GetGameEntriesRequestDto request)
    {
        var entries = await _dbContext.Entries.Where(e => e.GameId == request.GameId)
                                        .AsSplitQuery()
                                        .Include(e => e.User!)
                                            .ThenInclude(u => u.ProfileImage)
                                        .ToListAsync();
        //TODO da li mi ovde fali neka provera, mislim da nema potrebe
        return Ok(entries.Select(e => new GetGameEntriesResponseDto
        {
            Id = e.Id,
            CreateDateTime = e.CreateDateTime,
            OpNumber = e.OpNumber,
            RentNumber = e.RentNumber,
            Message = e.Message,
            User = new UserSimpleDto
            {
                Id = e.User!.Id,
                Username = e.User.Username,
                ProfileImage = e.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(e.User.ProfileImage.Id)
                }
            }
        }).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == request.GameId);

        if (game == null) return NotFound("Game not found");

        var entry = new Entry
        {
            OpNumber = request.OpNumber,
            RentNumber = request.RentNumber,
            Message = request.Message,
            UserId = Guid.Parse(userId),
            GameId = request.GameId
        };
        await _dbContext.Entries.AddAsync(entry);
        await _dbContext.SaveChangesAsync();

        await _entriesHub.Clients.Groups(game.Id.ToString()).SendAsync("Refetch");

        return Created();
    }

    [HttpDelete("{EntryId}")]
    public async Task<IActionResult> DeleteEntry([FromRoute] DeleteEntryRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var entry = await _dbContext.Entries
                                .Where(e => e.Id == request.EntryId)
                                .Where(e => e.UserId == userGuid)
                                .FirstOrDefaultAsync();

        if (entry == null) return NotFound("Entry not found");

        _dbContext.Entries.Remove(entry);
        await _dbContext.SaveChangesAsync();

        await _entriesHub.Clients.Groups(entry.GameId.ToString()).SendAsync("Refetch");

        return Ok();
    }
}