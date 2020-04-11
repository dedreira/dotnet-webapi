using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Hellang.Middleware.ProblemDetails;
namespace dedreira.samples.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
                        IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        private IConfiguration configuration { get; }
        private IWebHostEnvironment environment {get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)        
        {
            services.AddCustomProblemDetails(environment);
            services.AddControllers();            
            services.AddCustomApiVersioning();                      
            services.AddOpenApi(configuration);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
        IWebHostEnvironment env, 
        IApiVersionDescriptionProvider provider)
        {
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */
            app.UseProblemDetails(); 
            app.UseHttpsRedirection();            
            app.UseOpenApi(provider);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {                               
                endpoints.MapControllers();
                endpoints.MapGet("/api/health",context => {
                    return Task.FromResult(new OkResult());
                });
            });  
                     
        }
    }
}
