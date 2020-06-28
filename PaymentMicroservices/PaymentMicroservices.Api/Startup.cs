using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using DBserver.CrossCutting;
using Microsoft.OpenApi.Models;
using PaymentMicroservices.Api.DI;
using PaymentMicroservices.Domain.Extensoes;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PaymentMicroservices.Api.Extensions;

namespace PaymentMicroservices.Api
{
    public class Startup 
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected virtual IContainer InitializeContainer(ContainerBuilder builder, params IModule[] modules)
        {
            var crossCuttingAssembly = typeof(ContaCorrenteModule).GetTypeInfo().Assembly;

            var options = new InitializeOptions(new[] { crossCuttingAssembly }, modules, ContainerBuildOptions.None);
            return InitializeContainer(options, builder);
        }

        public IContainer InitializeContainer(InitializeOptions options, ContainerBuilder builder)
        {
            RegisterModulesFromAssemblies(options, builder);
            RegisterIndividualModules(options, builder);
            return builder.Build(options.BuildOptions);
        }

        private static void RegisterModulesFromAssemblies(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.AssembliesToScan.NuloOuVazio())
                builder.RegisterAssemblyModules(options.AssembliesToScan);
        }

        private static void RegisterIndividualModules(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.Modules.NuloOuVazio())
                foreach (var module in options.Modules)
                    builder.RegisterModule(module);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddExtentions(Configuration);
            services.AddControllers();
                   

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var appContainer = InitializeContainer(builder);

            return appContainer.Resolve<IServiceProvider>();
        }
            
        public void Configure(IApplicationBuilder app,
         IWebHostEnvironment env,
         IApiVersionDescriptionProvider versionProvider)
        {
            app.AddExtensions(versionProvider);


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
     
    }
}
