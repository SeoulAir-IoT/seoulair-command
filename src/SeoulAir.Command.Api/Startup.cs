using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeoulAir.Command.Api.Configuration;
using SeoulAir.Command.Api.Configuration.Extensions;
using SeoulAir.Command.Domain.Services.Extensions;
using SeoulAir.Command.Repositories;
using SeoulAir.Command.Repositories.Extensions;

namespace SeoulAir.Command.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<SeoulAirCommandDbContext>(
                builder => builder.UseSqlServer(Configuration.GetConnectionString("CommandDatabase")));

            services.AddDomainServices();

            services.AddRepositories();
            
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.EnsureMigrationOfContext<SeoulAirCommandDbContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
