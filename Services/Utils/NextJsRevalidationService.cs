using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace assnet8.Services.Utils;

public class NextJsRevalidationService : INextJsRevalidationService
{
    private readonly HttpClient _httpClient;
    private readonly string _revalidateSecret;
    private readonly string _revalidateUrl = "api/revalidate";

    public NextJsRevalidationService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _revalidateSecret = config["Frontend:Revalidate:Secret"]!;

        // Validate configuration
        if (string.IsNullOrEmpty(_revalidateSecret))
            throw new ArgumentNullException("Frontend:Revalidate:Secret must be configured");
    }

    public async Task<bool> RevalidatePathAsync(string path)
    {
        try
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                _revalidateUrl); // Relative path uses BaseAddress

            request.Headers.Add("x-revalidate-secret", _revalidateSecret);

            var content = new StringContent(
                JsonSerializer.Serialize(new { path }),
                Encoding.UTF8,
                "application/json");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RevalidateTagAsync(string tag)
    {
        try
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                _revalidateUrl); // Relative path uses BaseAddress

            request.Headers.Add("x-revalidate-secret", _revalidateSecret);

            var content = new StringContent(
                JsonSerializer.Serialize(new { tag }),
                Encoding.UTF8,
                "application/json");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return true;
        }
        catch
        {
            return false;
        }
    }
}