using Microsoft.Extensions.DependencyInjection;

namespace PaymentMicroservices.Api.Extensions
{
    public static class CorsExtensions
    {

        /// <summary>
        /// Set Custom Cors, you need to pass the policy Name and origin
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policyName"></param>
        /// <param name="origin"></param>
        public static void SetCors(this IServiceCollection services, string policyName, string origin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                builder =>
                {
                    builder.WithOrigins(origin
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    ;
                });

            });


        }

        /// <summary>
        /// Set Default Cors
        /// </summary>
        /// <param name="services"></param>
        public static void SetCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    ;
                });

            });

        }


    }
}
