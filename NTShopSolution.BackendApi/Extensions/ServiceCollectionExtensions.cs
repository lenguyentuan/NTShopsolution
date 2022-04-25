using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTShopSolution.Data.EF;

namespace NTShopSolution.BackendApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<NTShopDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NTShopSolutionDb"), b => b.MigrationsAssembly("NTShopSolution.BackendApi")));
            return services;
        }
    }
}