using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
namespace dedreira.samples.webapi.Infrastructure.OpenApi
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiVersion = context.ApiDescription.GetApiVersion();

            if (apiVersion == null)
            {
                return;
            }

            var parameters = operation.Parameters;

            if (parameters == null)
            {
                operation.Parameters = parameters = new List<OpenApiParameter>();
            }

            // Note: In most applications, service authors will choose a single, consistent approach to how API
            // versioning is applied. this sample uses:
            // 1. Query string parameter method with the name "api-version".
            // 2. URL path segment with the route parameter name "api-version".
            // Unless you allow multiple API versioning methods in your app, your implementation could be simpler.

            // consider the url path segment parameter first
            var parameter = parameters.FirstOrDefault(p => p.Name == "api-version");
            if (parameter == null)
            {
                // the only other method in this sample is by query string
                parameter = new OpenApiParameter()
                {
                    Name = "api-version",
                    Required = true,
                    In = ParameterLocation.Query                                       
                };
                parameters.Add(parameter);
            }
            parameter.Description = "The requested API version";
        }
    }
}
