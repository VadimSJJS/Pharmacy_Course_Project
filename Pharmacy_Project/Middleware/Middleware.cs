using Pharmacy_Project.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        public Middleware(RequestDelegate next) => _next = next;
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, PharmacyContext dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DbInitializer.Initialize(dbContext);
                context.Session.SetString("starting", "Yes");
            }
            return _next.Invoke(context);
            }
        }
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }

    }
}
