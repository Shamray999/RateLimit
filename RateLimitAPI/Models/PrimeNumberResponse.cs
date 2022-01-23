using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Models
{
    public class PrimeNumberResponse
    {
        public int Number { get; set; }
        public bool IsPrime { get; set; }
    }
}
