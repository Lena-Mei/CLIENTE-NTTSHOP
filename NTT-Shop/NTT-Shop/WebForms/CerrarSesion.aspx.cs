using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class CerrarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                // Cerrar sesión
                Session["session-id"] = null;
                Session["session-iso"] = null;
                Session["session-producto"] = null;
                Session["session-carrito"] = null;

                // Redirigir a la página de inicio o a donde desees
                Response.Redirect("Example.aspx");
            
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
          
        }
    }
}