using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c=>
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorisation",
                    In=ParameterLocation.Header,
                    Type= SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer",securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement{
                    {
                        securitySchema, new[]{"Bearer"}
                    }
                };
                c.AddSecurityRequirement(securityRequirement);

                c.SwaggerDoc("v1",new OpenApiInfo{Title="Skinet API", Version="v1"});
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumentaion(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // app.UseSwaggerUI(options =>
            // {
            //     //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //     //options.RoutePrefix = string.Empty;
            // });
             return app;
        }
       
    }
}