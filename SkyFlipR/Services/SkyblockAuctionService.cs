using SkyFlipR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyFlipR.Services;

public class SkyblockAuctionService
{
	private readonly HttpClient _httpClient;
	private const string BaseUrl = "https://api.hypixel.net/v2/skyblock/auctions";

	public SkyblockAuctionService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async IAsyncEnumerable<Auction> GetBuyItNowAuctionsAsync(string? apiKey = null, CancellationToken cancellation = default)
	{
		int page = 49;

		while (true)
		{
			var url = $"{BaseUrl}?page={page}";
			if (!string.IsNullOrWhiteSpace(apiKey))
			{
				url += $"&key={apiKey}";
			}

			using var resp = await _httpClient.GetAsync(url, cancellation);

			if (!resp.IsSuccessStatusCode)
			{
				yield break;
			}

			resp.EnsureSuccessStatusCode();

			using var stream = await resp.Content.ReadAsStreamAsync(cancellation);
			using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellation);

			if (!json.RootElement.TryGetProperty("auctions", out var auctionsNode))
				break;

			if (auctionsNode.GetArrayLength() == 0)
				break;

			foreach (var auc in auctionsNode.EnumerateArray())
			{
				if (auc.TryGetProperty("bin", out var isBuyItNow) &&
					isBuyItNow.GetBoolean())
				{
					yield return new Auction
					{
						Uuid = auc.GetProperty("uuid").GetString(),
						ItemName = auc.GetProperty("item_name").GetString(),
						BuyPrice = auc.GetProperty("starting_bid").GetInt64(),
						EndTime = DateTimeOffset.FromUnixTimeMilliseconds(auc.GetProperty("end").GetInt64()),
						Auctioneer = auc.GetProperty("auctioneer").GetString()
					};
				}
			}

			page++;
		}
	}
}