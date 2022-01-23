using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Services
{
    public interface IPrimeNumberService
    {
        bool IsPrimeNumber(int number);
    }
}
