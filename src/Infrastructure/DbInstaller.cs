using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DbInstaller
    {
        public static void AddDbServices(this IServiceCollection services, string connectionString) {
            services.AddDbContext<ApplicationDbContext>(o => {
                o.UseNpgsql(connectionString);
            });
        }

    }
}