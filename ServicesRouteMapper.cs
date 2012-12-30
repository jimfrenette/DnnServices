﻿using System;
using DotNetNuke.Web.Api;

namespace DnnServices
{
    public class ServicesRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Services", "default", "{controller}/{action}", new[] { "DnnServices" });
        }
    }
}
