using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Skybrud.PropertyEditors.NotifyPage;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.UI.JavaScript;

namespace Skybrud.PropertyEditors
{
    public class Startup : IApplicationEventHandler
    {
        private static object _lock = new object();
        private static bool _started = false;

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //throw new System.NotImplementedException();
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //throw new System.NotImplementedException();
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            if (_started)
                return;

            lock (_lock)
            {
                if (!_started)
                {
                    _started = true;

                    // Add server variables for the Intranet
                    ServerVariablesParser.Parsing += ServerVariablesParserParsing;
                }
            }
        }

        static void ServerVariablesParserParsing(object sender, Dictionary<string, object> e)
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            e.Add("DCIntranet", new Dictionary<string, object> {
                { "NotifyPageServiceUrl", url.GetUmbracoApiService<NotifyPageApiController>("ActionName").TrimEnd("ActionName")}
            });
        }
    }
}
