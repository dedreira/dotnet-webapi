using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.OpenApi.Models;
namespace dedreira.samples.webapi.Infrastructure.OpenApi
{
    public class RemoveVersionFromParameter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
            if(null != versionParameter)
            {
                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}