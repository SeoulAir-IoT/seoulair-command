using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Command.Domain.Interfaces.Repositories;

namespace SeoulAir.Command.Repositories.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SeoulAirCommandDbContext>(
                builder => builder.UseSqlServer(configuration.GetConnectionString("CommandDatabase")));
            
            services.AddScoped<ICommandRepository, CommandRepository>();
            return services;
        }
    }
}