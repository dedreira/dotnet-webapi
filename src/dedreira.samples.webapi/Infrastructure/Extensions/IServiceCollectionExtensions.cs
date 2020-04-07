using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using dedreira.samples.webapi.Infrastructure.OpenApi;
using dedreira.samples.webapi.Infrastructure.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
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
            options.OperationFilter<ApiVersionOperationFilter>();

            var jwtBearerOptions = configuration.GetSection<JwtBearer>();
            var oAuth2Scheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new System.Uri($"{jwtBearerOptions.Authority}/connect/authorize"),
                        TokenUrl = new System.Uri($"{jwtBearerOptions.Authority}/connect/token"),
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
        });

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
    services
        .AddVersionedApiExplorer()
        .AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
        });
    }
}