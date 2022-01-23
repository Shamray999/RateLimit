using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Models
{
    public class RateLimitResponse
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
