using System;
using System.Web;
using System.Web.UI;

namespace PizzaApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e) 
        {
            #if NET_4_5
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
            new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-1.10.2.min.js",
                DebugPath = "~/Scripts/jquery-1.10.2.min.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
            });
            #endif
        }

        void Application_End(object sender, EventArgs e) { }

        void Application_Error(object sender, EventArgs e)
        {
            Message.LastException = Server.GetLastError().GetBaseException();
            string message = "Error Caught in Application_Error event\n" +
                "Error in: " + Request.Url.ToString() +
                "\nError Message:" + Message.LastException.Message.ToString() +
                "\nStack Trace:" + Message.LastException.StackTrace.ToString();

        }
    }
}
