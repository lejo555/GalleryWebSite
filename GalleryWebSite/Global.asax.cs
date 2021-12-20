using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using GalleryWebSite.Models.DAL;
namespace GalleryWebSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var a = 0;
            var b = 1 / a;
            Database.SetInitializer<GalleryDBContext>(new CreateDatabaseIfNotExists<GalleryDBContext>());

            using (var context = new GalleryDBContext())
            {
                context.Database.Initialize(false);
            }
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                string path = HttpRuntime.AppDomainAppPath + "bin\\Trace.txt";
                string[] contents = {
                    "----------------------",
                    "Error Details:",
                    string.Format("Time: {0}",DateTime.Now.ToString()),
                    string.Format("Message: {0}",exception.Message),
                    string.Format("Stack: {0}",exception.StackTrace),
                    "----------------------"
                };
                File.WriteAllLines(path, contents);
            }
        }

    }
}