using API_NTT_SHOP.Models;
using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Escaparate : System.Web.UI.Page
    {
		private string generalUrl = "http://localhost:5000/api/";

		protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    ProductoEscaparate();

                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
            
		}

		private void ProductoEscaparate()
		{
			try
			{
				List<Producto> productos = GetAllProducto();
				List<Producto> productosHabilitados = new List<Producto>();
                foreach (var producto in productos)
                {
                    if(producto.habilitado is true)
					{
						productosHabilitados.Add(producto);
					}
                }

                rptProductos.DataSource = productosHabilitados;
				rptProductos.DataBind();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}

        private List<Producto> GetAllProducto()
        {
			Usuario usuario = GetUsuario();
            List<Producto>  productos = new List<Producto>();
            try
            {
                string url = generalUrl + "Producto/getAllProductos?idioma=" + Session["session-iso"].ToString()+"&idRate=" +usuario.IdRate;
				var httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Method = "GET";

				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var resultado = streamReader.ReadToEnd();
					var json = JObject.Parse(resultado);
					var productoArray = json["productoLista"].ToObject<JArray>();
					productos = productoArray.ToObject<List<Producto>>();
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return productos;
		}


		protected void btnAnyadir_Command(object sender, CommandEventArgs e)
		{
			string id = e.CommandArgument.ToString();

			Session["session-producto"] = id;
			Response.Redirect("DetalleProducto.aspx");
		}

        protected void Button1_Command(object sender, CommandEventArgs e)
        {

        }
		protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
		}
		protected void btnAnyadir_Click(object sender, EventArgs e)
		{
			
		}

        private Usuario GetUsuario()
        {
            Usuario usuario = new Usuario();
            try
            {
                int id = int.Parse(Session["session-id"].ToString());
                string url = generalUrl + "Usuario/getUsuario/" + id;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    usuario = json["idUsuario"].ToObject<Usuario>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return usuario;
        }

    }
}