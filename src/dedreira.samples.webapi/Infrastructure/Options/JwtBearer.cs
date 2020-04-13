namespace dedreira.samples.webapi.Infrastructure.Options
{
    public class JwtBearer
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string AuthorizeEndpoint {get;set;}
        public string TokenEndpoint {get;set;}
    }
}
