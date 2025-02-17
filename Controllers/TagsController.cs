using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class TagsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public TagsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _dbContext.Tags.ToListAsync();
        return Ok(tags);
    }
    [HttpGet("service")]
    public async Task<IActionResult> GetServiceTags()
    {
        var tags = await _dbContext.Tags.Where(t => t.Type == TagType.Service).ToListAsync();
        return Ok(tags);
    }
    [HttpGet("game")]
    public async Task<IActionResult> GetGameTags()
    {
        var tags = await _dbContext.Tags.Where(t => t.Type == TagType.Game).ToListAsync();
        return Ok(tags);
    }
    [HttpGet("listing")]
    public async Task<IActionResult> GetListingTags()
    {
        var tags = await _dbContext.Tags.Where(t => t.Type == TagType.Listing).ToListAsync();
        return Ok(tags);
    }
}