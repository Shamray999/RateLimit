using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Models
{
    public class PrimeNumberRequest
    {
        [Required]
        public int Number { get; set; }
        
        [MaxLength(100)]
        public string Token { get; set; }
    }
}
