using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using SkyFlipR.Features.BazaarFlip;

namespace SkyFlipR.Services;

public interface ISkyblockBazaarService
{
    Task<List<BazaarItem>> FetchBazaarAsync();
}

public class SkyblockBazaarService : ISkyblockBazaarService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.hypixel.net/v2/skyblock/bazaar";

    public SkyblockBazaarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BazaarItem>> FetchBazaarAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<BazaarResponse>(BaseUrl);
        if (response?.Products is null || !response.Success)
        {
            throw new Exception("Failed to fetch bazaar data.");
        }

        var list = new List<BazaarItem>();
        foreach (var kvp in response.Products)
        {
            list.Add(new BazaarItem
            {
                Name = kvp.Key,
                QuickStatus = kvp.Value.QuickStatus
            });
        }
        return list;
    }
}
