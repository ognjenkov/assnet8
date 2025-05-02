using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using assnet8.Dtos.Locations;

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
    public async Task<ActionResult<IEnumerable<GetLocationsResponseDto>>> GetLocations()
    {
        var locations = await _dbContext.Locations
                                            .Include(l => l.Municipalities)
                                            .ToListAsync();

        return Ok(locations.Select(l => new GetLocationsResponseDto
        {
            Id = l.Id,
            Region = l.Region,
            Registration = l.Registration,
            Municipalities = l.Municipalities.Select(m => new MunicipalitySimpleDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToList()
        }));
    }
}