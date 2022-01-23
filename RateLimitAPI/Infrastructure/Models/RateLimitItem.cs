using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Models
{
    public class RateLimitItem
    {
        public string IpAddress { get; set; }
        public DateTime? ReleseDateTime { get; set; }
        public int MinutesToBlock { get; set; }
        public int NumberOfCalls { get; set; }
    }
}
