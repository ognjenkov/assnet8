using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace assnet8.Services.Utils;

public class GoogleMapsService : IGoogleMapsService
{
    private readonly HttpClient _httpClient;

    public GoogleMapsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(double? Latitude, double? Longitude)> GetCoordinatesFromUrlAsync(string url)
    {
        // First try to extract directly from input URL
        var coords = TryExtractCoordinates(url);
        if (coords.Latitude.HasValue && coords.Longitude.HasValue)
            return coords;

        // If not found, try to expand the URL
        var expandedUrl = await ExpandUrlAsync(url);
        if (expandedUrl != null)
        {
            // Try extracting from expanded URL
            coords = TryExtractCoordinates(expandedUrl);
            if (coords.Latitude.HasValue && coords.Longitude.HasValue)
                return coords;

            // Try extracting from the path pattern
            coords = TryExtractCoordinatesFromPath(expandedUrl);
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

            // Handle redirect responses
            if (response.StatusCode >= HttpStatusCode.Moved && response.StatusCode < HttpStatusCode.BadRequest)
            {
                return response.Headers.Location?.ToString();
            }

            // Handle successful responses that might contain the coordinates in the final URL
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Check if the final request URI has the coordinates in the path
                var finalUri = response.RequestMessage?.RequestUri;
                if (finalUri != null)
                {
                    // Try extracting from the path pattern first
                    var pathCoords = TryExtractCoordinatesFromPath(finalUri.AbsoluteUri);
                    if (pathCoords.Latitude.HasValue && pathCoords.Longitude.HasValue)
                    {
                        return finalUri.AbsoluteUri;
                    }

                    // Fall back to returning the final URL for other extraction methods
                    return finalUri.AbsoluteUri;
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to expand URL: {ex.Message}");
        }

        return null;
    }

    private static (double? Latitude, double? Longitude) TryExtractCoordinates(string url)
    {
        // Original extraction patterns
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

        // Also try the new path pattern
        var pathCoords = TryExtractCoordinatesFromPath(url);
        if (pathCoords.Latitude.HasValue && pathCoords.Longitude.HasValue)
        {
            return pathCoords;
        }

        return (null, null);
    }

    private static (double? Latitude, double? Longitude) TryExtractCoordinatesFromPath(string url)
    {
        // Handle patterns like:
        // /maps/search/45.959907,+19.971800
        // /maps/place/45.959907,+19.971800
        var match = Regex.Match(url, @"/maps/(?:search|place)/([+-]?\d+\.\d+),([+-]?\d+\.\d+)");
        if (match.Success)
        {
            return (double.Parse(match.Groups[1].Value), double.Parse(match.Groups[2].Value));
        }

        return (null, null);
    }
}