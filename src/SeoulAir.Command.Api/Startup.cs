using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDomainServices();

            services.AddRepositories(Configuration);
            
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

            app.UseHttpsRedirection();

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
