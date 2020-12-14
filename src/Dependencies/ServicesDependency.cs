
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace src.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IHackerNewsService, HackerNewsService>();
        }
    }
}