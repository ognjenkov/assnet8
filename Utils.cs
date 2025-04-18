using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace assnet8.Utils;
public static class Utils
{
    public static string GenerateImageFrontendLink(Guid imageId)
    {
        return "http://localhost:5181/images/" + imageId;
    }
}
public class GoogleMapsHelper
{
    private static readonly HttpClient client = new HttpClient(new HttpClientHandler
    {
        AllowAutoRedirect = false
    });

    public static async Task<(double? Latitude, double? Longitude)> GetCoordinatesFromGoogleMapsUrl(string url)
    {
        // Try extracting coordinates directly
        var coords = TryExtractCoordinates(url);
        if (coords.Latitude.HasValue && coords.Longitude.HasValue)
            return coords;

        // Try expanding the URL if coordinates weren't found
        var expandedUrl = await ExpandUrl(url);
        if (expandedUrl != null)
        {
            coords = TryExtractCoordinates(expandedUrl);
            if (coords.Latitude.HasValue && coords.Longitude.HasValue)
                return coords;
        }

        // Nothing found
        return (null, null);
    }

    private static async Task<string?> ExpandUrl(string shortUrl)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, shortUrl);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await client.SendAsync(request);
            if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400)
            {
                return response.Headers.Location?.ToString();
            }
        }
        catch { }

        return null;
    }

    private static (double? Latitude, double? Longitude) TryExtractCoordinates(string url)
    {
        // Match patterns like: @45.815399,15.966568
        var match = Regex.Match(url, @"@(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)");
        if (match.Success)
        {
            double lat = double.Parse(match.Groups[1].Value);
            double lng = double.Parse(match.Groups[3].Value);
            return (lat, lng);
        }

        // Match patterns like: /place/45.815399,15.966568
        match = Regex.Match(url, @"/place/(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)");
        if (match.Success)
        {
            double lat = double.Parse(match.Groups[1].Value);
            double lng = double.Parse(match.Groups[3].Value);
            return (lat, lng);
        }

        return (null, null);
    }
}