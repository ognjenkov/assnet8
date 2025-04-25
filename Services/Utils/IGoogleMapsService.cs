using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Utils;
public interface IGoogleMapsService
{
    Task<(double? Latitude, double? Longitude)> GetCoordinatesFromUrlAsync(string url);
}