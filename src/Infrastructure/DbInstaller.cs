using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DbInstaller
    {
        public static void AddDbServices<Context>(this IServiceCollection services, string connectionString) where Context: DbContext {
            services.AddDbContext<Context>(o => {
                o.UseNpgsql(connectionString);
            });
        }


    }
}