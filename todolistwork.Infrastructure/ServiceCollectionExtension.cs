using todolistwork.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using todolistwork.Application.Repository;
using todolistwork.Application.IService;
using todolistwork.Application.Service;
using todolistwork.Application.ICache;
using todolistwork.Infrastructure.database.Redis;

namespace todolistwork.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Repository
            services.AddTransient<ITaskUserRepository, TaskUserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //service
            services.AddTransient<IRedisService, RedisService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITaskUserService, TaskUserService>();

        }
    }
}
