using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Entries.Request;
using assnet8.Dtos.Entries.Response;
using assnet8.Services.Entries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
public class EntriesController : BaseController
{
    private readonly AppDbContext _dbContext;

    public EntriesController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    [HttpGet("game/{GameId}")]
    public IActionResult GetGameEntries([FromRoute] GetGameEntriesRequestDto request)
    {
        var entries = _dbContext.Entries.Where(e => e.GameId == request.GameId)
                                        .Include(e => e.User!)
                                            .ThenInclude(u => u.ProfileImage)
                                        .ToList();
        //TODO da li mi ovde fali neka provera, mislim da nema potrebe
        return Ok(entries.Select(e => new GetGameEntriesResponseDto
        {
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
        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == request.GameId);

        if (game == null) return NotFound("Game not found");

        var entryExists = await _dbContext.Entries
                                            .Where(e => e.GameId == request.GameId)
                                            .Where(e => e.UserId == Guid.Parse(userId))
                                            .FirstOrDefaultAsync();
        if (entryExists != null) return BadRequest("Entry already exists");

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

        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEntry([FromBody] DeleteEntryRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var entry = await _dbContext.Entries
                                .Where(e => e.Id == request.EntryId)
                                .Where(e => e.UserId == Guid.Parse(userId))
                                .FirstOrDefaultAsync();

        if (entry == null) return NotFound("Entry not found");

        _dbContext.Entries.Remove(entry);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}