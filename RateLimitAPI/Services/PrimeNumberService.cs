using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Services
{
    public class PrimeNumberService : IPrimeNumberService
    {
        public bool IsPrimeNumber(int number)
        {
            int i;

            for (i = 2; i <= number - 1; i++)
            {
                if (number % i == 0)
                    return false;
            }

            if (i == number)
                return true;

            return false;
        }
    }
}
