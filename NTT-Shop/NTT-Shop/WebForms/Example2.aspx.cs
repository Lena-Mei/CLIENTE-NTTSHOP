using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Example2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["session-id"]== null)
            {
				Response.Redirect("IniciarSesion.aspx");
			}

            lblID.Text = Session["session-id"].ToString();
            lblIso.Text = Session["session-iso"].ToString();
        }

       

      
    }
}