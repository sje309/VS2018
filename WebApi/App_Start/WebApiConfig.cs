using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register( HttpConfiguration config )
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            // 1、默认路由
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            // 2、自定义路由一: 匹配到action
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new  { id = RouteParameter.Optional }
              );
        }
    }
}
