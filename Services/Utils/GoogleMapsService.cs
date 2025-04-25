using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace assnet8.Services.Utils;

public class GoogleMapsService : IGoogleMapsService
{
    private readonly HttpClient _httpClient;

    public GoogleMapsService(HttpClient httpClient) // Direct injection
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
    }
    public async Task<(double? Latitude, double? Longitude)> GetCoordinatesFromUrlAsync(string url)
    {
        var coords = TryExtractCoordinates(url);
        if (coords.Latitude.HasValue && coords.Longitude.HasValue)
            return coords;

        var expandedUrl = await ExpandUrlAsync(url);
        if (expandedUrl != null)
        {
            coords = TryExtractCoordinates(expandedUrl);
            if (coords.Latitude.HasValue && coords.Longitude.HasValue)
                return coords;
        }

        return (null, null);
    }

    private async Task<string?> ExpandUrlAsync(string shortUrl)
    {
        try
        {
            var response = await _httpClient.GetAsync(shortUrl, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode >= HttpStatusCode.Moved && response.StatusCode < HttpStatusCode.BadRequest)
            {
                return response.Headers.Location?.ToString();
            }
        }
        catch (HttpRequestException ex)
        {
            // Log the error (you can inject ILogger<GoogleMapsService>)
            Console.WriteLine($"Failed to expand URL: {ex.Message}");
        }

        return null;
    }

    private static (double? Latitude, double? Longitude) TryExtractCoordinates(string url)
    {
        // Same as before
        var match = Regex.Match(url, @"@(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)");
        if (match.Success)
        {
            return (double.Parse(match.Groups[1].Value), double.Parse(match.Groups[3].Value));
        }

        match = Regex.Match(url, @"/place/(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)");
        if (match.Success)
        {
            return (double.Parse(match.Groups[1].Value), double.Parse(match.Groups[3].Value));
        }

        return (null, null);
    }
}
