using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;

namespace RoboBank.Account.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
        }
    }
}
