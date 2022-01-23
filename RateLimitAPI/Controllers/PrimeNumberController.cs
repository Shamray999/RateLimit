using RateLimitAPI.Infrastructure;
using RateLimitAPI.Models;
using RateLimitAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimeNumberController : ControllerBase
    {
        private readonly ILogger<PrimeNumberController> _logger;
        private readonly IPrimeNumberService _primeNumberService;

        public PrimeNumberController(ILogger<PrimeNumberController> logger,
            IPrimeNumberService primeNumberService)
        {
            _logger = logger;
            _primeNumberService = primeNumberService;
        }

        [HttpPost]
        [Route("IsPrimeNumber")]
        [RateLimit(Minutes = 1, CallLimits = 2, RateLimitScopeInMinutes = 1)]
        public PrimeNumberResponse IsPrimeNumber(PrimeNumberRequest request)
        {
            try
            {
                var isPrimeNumber = _primeNumberService.IsPrimeNumber(request.Number);
                return new PrimeNumberResponse() { IsPrime = isPrimeNumber, Number = request.Number };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
