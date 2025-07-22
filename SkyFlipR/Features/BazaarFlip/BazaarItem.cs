using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkyFlipR.Features.BazaarFlip;

public class BazaarResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("lastUpdated")]
    public long LastUpdated { get; set; }

    [JsonPropertyName("products")]
    public Dictionary<string, ProductWrapper> Products { get; set; }
}

public class ProductWrapper
{
    [JsonPropertyName("quick_status")]
    public QuickStatus QuickStatus { get; set; }
}

public class BazaarItem
{
    public string Name { get; set; }
    public QuickStatus QuickStatus { get; set; }

    public double BuyPriceForLevelFive
    {
        get
        {
            if (QuickStatus?.ProductId?.Length > 0)
            {
                char ending = QuickStatus.ProductId[^1]; // last character
                return ending switch
                {
                    '1' => QuickStatus.SellPrice * 16,
                    '2' => QuickStatus.SellPrice * 8,
                    '3' => QuickStatus.SellPrice * 4,
                    '4' => QuickStatus.SellPrice * 2,
                    '5' => QuickStatus.SellPrice * 1,
                    _ => -1
                };
            }
            return -1;
        }
    }

}

public class QuickStatus
{
    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

    [JsonPropertyName("sellPrice")]
    public double SellPrice { get; set; }
    [JsonPropertyName("sellVolume")]
    public long SellVolume { get; set; }
    [JsonPropertyName("sellMovingWeek")]
    public long SellMovingWeek { get; set; }
    [JsonPropertyName("sellOrders")]
    public int SellOrders { get; set; }

    [JsonPropertyName("buyPrice")]
    public double BuyPrice { get; set; }
    [JsonPropertyName("buyVolume")]
    public long BuyVolume { get; set; }
    [JsonPropertyName("buyMovingWeek")]
    public long BuyMovingWeek { get; set; }
    [JsonPropertyName("buyOrders")]
    public int BuyOrders { get; set; }
}