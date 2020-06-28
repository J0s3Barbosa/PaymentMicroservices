using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PaymentMicroservices.Api.Extensions
{
    public static class SwaggerExtensions
    {
        private static string ApiName => $"{"Payment"} - {"MicroService"}";

        /// <summary>
        /// Extention that makes it easier to Setup Swagger to Api
        /// </summary>
        /// <param name="services"></param>
        public static void SetSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

            services.AddSwaggerGen(config =>
            {
                config.TagActionsBy(api => api.GroupBySwaggerGroupAttribute());
                config.SetXmlDocumentation();
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>(){ "Bearer " }
          }
        });

            });
        }

        public static void SetSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.ConfigureSwaggerUI(versionProvider);
            });
        }

        private static void SetXmlDocumentation(this SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);
            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }

        private static string GetXmlDocumentPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }

        private static IList<string> GroupBySwaggerGroupAttribute(this ApiDescription api)
        {
            var groupName = string.Empty;

            if (api.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                var attribute = methodInfo.DeclaringType.CustomAttributes.SingleOrDefault(o => o.AttributeType.Name.Equals("SwaggerGroupAttribute"));
                groupName = attribute?.ConstructorArguments?.FirstOrDefault().Value?.ToString();
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                return new List<string> { groupName };
            }
            else
            {
                var actionDescriptor = api.GetProperty<ControllerActionDescriptor>();
                if (actionDescriptor == null)
                {
                    actionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    api.SetProperty(actionDescriptor);
                }
                return new List<string> { actionDescriptor?.ControllerName };
            }
        }

        private static void ConfigureSwaggerUI(this SwaggerUIOptions swaggerUIOptions, IApiVersionDescriptionProvider versionProvider)
        {
            var swaggerJsonBasePath = string.IsNullOrWhiteSpace(swaggerUIOptions.RoutePrefix) ? "." : "..";

            foreach (var apiVersion in versionProvider.ApiVersionDescriptions)
            {
                swaggerUIOptions.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v{apiVersion.ApiVersion}/swagger.json", $"{ApiName} - v{apiVersion.ApiVersion}");
            }

            swaggerUIOptions.RoutePrefix = "swagger";
            swaggerUIOptions.DocExpansion(DocExpansion.None);
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var build = Assembly.GetEntryAssembly().GetName().Version.ToString();
            var apiName = $"{"Payment"} - {"MicroService"}";
            var info = new OpenApiInfo()
            {
                Title = apiName,
                Version = description.ApiVersion.ToString(),
                Description = $"{apiName} - build: {build}",
                Contact = new OpenApiContact
                {
                    Name = "José Barbosa",
                    Email = "chto@outlook.com"
                }

            };

            info.Description += description.IsDeprecated ? $" - <strong> {""} </strong>" : string.Empty;

            return info;
        }
    }

    public sealed class SwaggerGroupAttribute : Attribute
    {
        public string GroupName { get; }

        public SwaggerGroupAttribute(string groupName) => GroupName = groupName;
    }
}
