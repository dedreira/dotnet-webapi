using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder application, IApiVersionDescriptionProvider provider) =>
            application
                .UseSwagger()
                .UseSwaggerUI(options => 
                {
                    foreach(var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        options.OAuthClientId("swaggerui");
                        options.RoutePrefix = "";
                    }
                });
    }
}