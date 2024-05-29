using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class CarritoVacio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    
                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
        }
    }
}