using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("locations")]
public class LocationsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public LocationsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _dbContext.Locations
                                            .Include(l => l.Municipalities)
                                            .ToListAsync();

        return Ok(locations);
    }
}