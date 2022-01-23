using RateLimitAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        private static readonly string ERROR_MESSAGE = "You have exeeded your call limits. Remining time until removed is {0}";

        private static readonly MemoryCache _memoryCache = new(new MemoryCacheOptions());

        public int Minutes { get; set; }
        public int CallLimits { get; set; }
        public int RateLimitScopeInMinutes { get; set; }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var remoteIpAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (_memoryCache.TryGetValue(remoteIpAddress, out RateLimitItem rateLimitItem))
            {
                if(rateLimitItem.ReleseDateTime != null)
                {
                    var reminingTime = (rateLimitItem.ReleseDateTime - DateTime.Now);

                    context.Result = new JsonResult(new RateLimitResponse()
                    {
                        ErrorMessage = string.Format(ERROR_MESSAGE, reminingTime.Value.ToString("c")),
                        HasError = true
                    })
                    {
                        StatusCode = StatusCodes.Status429TooManyRequests
                    };
                }
                else 
                {
                    rateLimitItem.NumberOfCalls++;
                    if (rateLimitItem.NumberOfCalls == CallLimits)
                    {
                        _memoryCache.Remove(remoteIpAddress);

                        rateLimitItem.ReleseDateTime = DateTime.Now.AddMinutes(Minutes);

                        _memoryCache.Set(rateLimitItem.IpAddress, rateLimitItem, DateTimeOffset.Now.AddMinutes(Minutes));
                    }
                }
            }
            else
            {
                var newRateLimitItem = new RateLimitItem()
                {
                    IpAddress = remoteIpAddress.ToString(),
                    NumberOfCalls = 1
                };

                _memoryCache.Set(newRateLimitItem.IpAddress, newRateLimitItem, DateTimeOffset.Now.AddMinutes(RateLimitScopeInMinutes));
            }

            base.OnResultExecuting(context);
        }
    }
}
