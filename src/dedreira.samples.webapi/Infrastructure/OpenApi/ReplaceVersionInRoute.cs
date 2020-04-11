using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
namespace dedreira.samples.webapi.Infrastructure.OpenApi
{
    public class ReplaceVersionInRoute : IDocumentFilter
    {
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {            
            OpenApiPaths paths = new OpenApiPaths();
            foreach (var path in document.Paths)
            {
                paths.Add(path.Key.Replace("v{version}", document.Info.Version),
                            path.Value);
            }
            document.Paths = paths;
        }
    }
}