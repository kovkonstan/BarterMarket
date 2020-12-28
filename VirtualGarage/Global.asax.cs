﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BarterMarket.Logic;
using System.Reflection;
//using BarterMarket.Logic.Mappings;

namespace BarterMarket
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Garage", // Route name
                "Garage/{action}", // URL with parameters
                new { controller = "Garage", action = "CarsInGarage" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Default", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

			//new CheckValueNumericOnSideClient().SetMessageForType(typeof(double), "Введите корректное значение");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DefaultModelBinder.ResourceClassKey = "BarterMarketResource";
        }

    }
}