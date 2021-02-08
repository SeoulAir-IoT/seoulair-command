using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                var xmlDocumentationFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentationFileName));
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(OpenApiInfoProjectVersion, new OpenApiInfo
                {
                    Title = OpenApiInfoTitle,
                    Description = OpenApiInfoDescription,
                    Version = OpenApiInfoProjectVersion,
                    Contact = new OpenApiContact
                    {
                        Name = GitlabContactName,
                        Url = new Uri(GitlabRepoUri)
                    }
                });
            });

            return services;
        }
    }
}
