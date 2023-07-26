using todolistwork.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using todolistwork.Application.Repository;
using todolistwork.Application.ICache;
using todolistwork.Infrastructure.database.Redis;

namespace todolistwork.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<ITaskUserRepository, TaskUserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRedisService, RedisService>();
        }
    }
}
