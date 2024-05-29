using API_NTT_SHOP.Models;
using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
	public partial class DetalleProducto : System.Web.UI.Page
	{
		private string generalUrl = "http://localhost:5000/api/";


		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    MostrarProducto();
                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
		}

		protected void btnAnyadir_Click(object sender, EventArgs e)
		{
			//Aquí declaramos el idProducto con el producto que se muestra en la vista.
            int idProducto = int.Parse(Session["session-producto"].ToString());
            Producto producto = GetProducto();
			List<Producto> productos = new List<Producto>();
            productos.Add(producto);

			//Vamos a crear un nuevo objeto ProductoCarrito //Donde vamos a declarar su id y su cantidad (predeterminada 1)
			ProductoCarrito productoCarrito = new ProductoCarrito()
			{
				cantidad = 1,
				idProducto = idProducto,
				stock = producto.stock,
				producto = productos,
				//total = producto.rate[0].precio
            };
			 //Ponemos el idProducto al mismo que el idProducto de la vista
            try
            {


				//Áquí se crea una lista de ProductoCarrito y se iguala a la lista de la variable 
				List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;


				//Se comprueba si está vacía
                if (listaCarrito == null)
                {
					//Si está vacía, se crea una nueva lista 
                    listaCarrito = new List<ProductoCarrito>
                    {
                        productoCarrito
                    };

                }
				else
				{
                    ProductoCarrito productoExistente = listaCarrito.FirstOrDefault(p => p.idProducto == idProducto);
                    if (productoExistente != null)
                    {
                        // Si el producto está en el carrito, incrementamos la cantidad.
						if(productoExistente.cantidad < productoExistente.stock)
						{
							productoExistente.cantidad++;
							//productoExistente.total = producto.rate[0].precio * productoExistente.cantidad;
                        }
                    }
                    else
                    {
                        // Si el producto no está en el carrito, lo añadimos a la lista.
                        listaCarrito.Add(productoCarrito);
                    }
                }
				//Se guarda en la variable de sesión la lista con los productosCarrito
                Session["session-carrito"] = listaCarrito;


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            Response.Redirect("Carrito.aspx");

        }

        private void MostrarProducto()
		{
		
			Producto producto = GetProducto();
			
			lblId.Text = producto.idProducto.ToString();
			lblNombre.Text = producto.descripcion[0].nombre.ToString();
			lblDes.Text = producto.descripcion[0].descripcion.ToString();
			lblPrecio.Text = producto.rate[0].precio.ToString() + "€";
		}

		private Producto GetProducto()
		{
			Producto producto = new Producto();
			int id = int.Parse(Session["session-producto"].ToString());
            int idRate = int.Parse(Session["session-rate"].ToString());
            try
			{
				string url = generalUrl + "Producto/getProducto?id=" + id + "&idioma=" + Session["session-iso"].ToString() +"&idRate=" +idRate;
				var httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Method = "GET";

				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var resultado = streamReader.ReadToEnd();
					var json = JObject.Parse(resultado);
					producto = json["producto"].ToObject<Producto>();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return producto;
		}

        protected void btnVolver_Click(object sender, EventArgs e)
        {
			Response.Redirect("Escaparate.aspx");
        }
    }
}