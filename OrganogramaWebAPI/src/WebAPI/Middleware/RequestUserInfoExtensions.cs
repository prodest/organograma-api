using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Middleware
{
    public static class RequestUserInfoExtensions
    {
        public static IApplicationBuilder UseRequestUserInfo(this IApplicationBuilder builder, RequestUserInfoOptions options)
        {
            return builder.UseMiddleware<RequestUserInfoMiddleware>(options);
        }
    }
}
