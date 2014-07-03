using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ToDo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formaatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formaatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
