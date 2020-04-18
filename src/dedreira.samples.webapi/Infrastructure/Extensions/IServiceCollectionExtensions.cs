using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using dedreira.samples.webapi.Infrastructure.OpenApi;
using dedreira.samples.webapi.Infrastructure.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Hosting;
using Hellang.Middleware.ProblemDetails;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration configuration) =>
        services
        .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
        .AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.CustomSchemaIds(t => t.FullName);
            options.OperationFilter<AuthorizeCheckOperationFilter>();            
            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionInRoute>();
            var jwtBearerOptions = configuration.GetSection<JwtBearer>();
            var oAuth2Scheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new System.Uri($"{jwtBearerOptions.AuthorizeEndpoint}"),
                        TokenUrl = new System.Uri($"{jwtBearerOptions.TokenEndpoint}"),
                        Scopes = new Dictionary<string, string>()
                {
                            { "scope", "Scope Description" }
                }
                    }

                },
            };
            options.AddSecurityDefinition("oauth2", oAuth2Scheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                        {oAuth2Scheme, new string[]{}}
                    });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>        
        services
        .AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true)
        .AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
        });

        public static IServiceCollection AddCustomProblemDetails(
            this IServiceCollection services,
             IWebHostEnvironment environment
            ) => 
                services
                .AddProblemDetails(configure =>
                {
                    configure.IncludeExceptionDetails = (ctx,ex) => environment.EnvironmentName == "Development";
                    configure.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
                });
    }    
}