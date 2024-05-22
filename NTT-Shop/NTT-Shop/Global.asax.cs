using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace NTT_Shop
{
    public class Global : System.Web.HttpApplication
    {

        //Variables de aplciación que está disponible para 
        //toda la aplicación y para todos los ususarios
        protected void Application_Start(object sender, EventArgs e)
        {
        }



        //Variables de sesión para cada usuario
        protected void Session_Start(object sender, EventArgs e)
        {
            //Session["session-id"] = "2";
            //Session["session-iso"] = "es";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //el usuario solicita 
        }
    }
}