using System.Web.Http;
using System.Web.Http.Cors;

namespace QuanLyThuVien
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.UseDataContractJsonSerializer = true;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            formatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            // formatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            formatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
