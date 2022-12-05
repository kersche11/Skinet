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
            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1",new OpenApiInfo{Title="Skinet API", Version="v1"});
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumentaion(this IApplicationBuilder app)
        {
             app.UseSwagger();
             app.UseSwaggerUI();

             return app;
        }
       
    }
}