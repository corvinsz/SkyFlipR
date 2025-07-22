using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Extensions;


public class Rootobject
{
    public string uuid { get; set; }
    public string auctioneer { get; set; }
    public string profile_id { get; set; }
    public string[] coop { get; set; }
    public long start { get; set; }
    public long end { get; set; }
    public string item_name { get; set; }
    public string item_lore { get; set; }
    public string extra { get; set; }
    public string[] categories { get; set; }
    public string category { get; set; }
    public string tier { get; set; }
    public int starting_bid { get; set; }
    public string item_bytes { get; set; }
    public bool claimed { get; set; }
    public object[] claimed_bidders { get; set; }
    public int highest_bid_amount { get; set; }
    public long last_updated { get; set; }
    public bool bin { get; set; }
    public object[] bids { get; set; }
    public string item_uuid { get; set; }
}
