using Owin;
using System.Linq;
using System.Web.Http;

namespace MedbotService.Configuration
{
    public class ApiConfiguration
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            // Habilita roteamento por atributos.
            config.MapHttpAttributeRoutes();

            // Exibe o conteúdo apenas em REST.
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            appBuilder.UseWebApi(config);
        }
    }
}
