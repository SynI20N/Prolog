using Orchard.Mvc.Routes;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prolog.Web
{
    public class RouteProvider : IRouteProvider
    {
        public const string AreaName = "Prolog.Web";
        public const string ImagesFolder = "~/Modules/" + AreaName + "/Images/";

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                             new RouteDescriptor {
                                                     Priority = 20,
                                                     Route = new Route(
                                                         "Prolog.Web/{controller}/{action}/{id}",
                                                         new RouteValueDictionary {
                                                                                      {"area", AreaName},
                                                                                      {"controller", "HomeController"},
                                                                                      {"action", "Index"},
                                                                                      {"id", UrlParameter.Optional}
                                                                                  },
                                                         null,
                                                         new RouteValueDictionary
                                                             {
                                                                 {"area", AreaName}
                                                             },
                                                         new MvcRouteHandler())
                                                 }
                         };
        }
    }
}