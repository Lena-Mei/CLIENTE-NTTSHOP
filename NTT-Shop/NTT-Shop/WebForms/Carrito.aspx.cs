using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
	public partial class WebForm2 : System.Web.UI.Page
	{
		private string generalUrl = "http://localhost:5000/api/";
	
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["session-carrito"] != null)
                    {
                        mostrarProducto();
                        mostrarUsuario();
                        totalPecio();
                    }
                    else
                    {
                        Response.Redirect("CarritoVacio.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
		}
        private void mostrarUsuario()
        {
            Usuario us = getUsuario();
            if (us != null)
            {
                lblCiu.Text = us.Ciudad;
                lblCorreo.Text = us.Email;
                lblCp.Text = us.CodigoPostal;
                lblDir.Text = us.Direccion;
                lblNombre.Text = us.Inicio;
                lblPrv.Text = us.Provincia;
                lblTel.Text = us.Telefono;
            }

        }

        private Usuario getUsuario()
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

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            
            Pedido pedido = crearClasePedido();

            if (crearPedido(pedido))
            {
                Session["session-carrito"] = null;
                Response.Redirect("Escaparate.aspx");

            }
            else
            {
                Response.Redirect("CerrarSesion.aspx");
            }
        }

		private void mostrarProducto()
		{
            List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;
            if (listaCarrito != null && listaCarrito.Count > 0)
            {
                rptProductos.DataSource = listaCarrito;
                rptProductos.DataBind();
            }
        }
       
        protected void btnMenos_Command(object sender, CommandEventArgs e)
        {
			int id = int.Parse(e.CommandArgument.ToString());
			List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;

            //Creamos un objeto Producto  y lo igualamos al resutlado de la búsqueda en la lista Carrito donde devuelve el producto
            //cuyo idProducto es la misma que el id del botón que le hemos pasado. 
            ProductoCarrito producto = listaCarrito.FirstOrDefault(p => p.idProducto == id);

            if (producto.idProducto == id && producto.cantidad > 1)
				{
					producto.cantidad--;
                    //producto.total = producto.producto[0].rate[0].precio * producto.cantidad;
            }
			
            //Actualizamos la variable de sesion y el Repeater. 
            Session["session-carrito"] = listaCarrito;
            rptProductos.DataSource = listaCarrito;
            rptProductos.DataBind();
            totalPecio();

        }

        protected void btnMas_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;

            
            ProductoCarrito producto = listaCarrito.FirstOrDefault(p => p.idProducto == id);


            if (producto.idProducto == id && producto.cantidad < producto.stock )
                {
                    producto.cantidad++;
                    //producto.total = producto.producto[0].rate[0].precio * producto.cantidad;

            }

            Session["session-carrito"] = listaCarrito;
            rptProductos.DataSource = listaCarrito;
            rptProductos.DataBind();
            totalPecio();

        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;
            ProductoCarrito producto = listaCarrito.FirstOrDefault(p => p.idProducto == id);
            listaCarrito.Remove(producto);
            if(listaCarrito.Count==0)
            {
                Session["session-carrito"] = null;
                Response.Redirect("CarritoVacio.aspx");
            }
            else
            {
                Session["session-carrito"] = listaCarrito;
                rptProductos.DataSource = listaCarrito;
                rptProductos.DataBind();
            }
            totalPecio();
           
        }


        private bool crearPedido(Pedido pedido)
        {
            bool correcto = false;
            var pedidoData = new {pedido = pedido};
            string url = generalUrl + "Pedido/insertPedido";
            string jsonDatos = JsonConvert.SerializeObject(pedidoData);

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {

                    streamWriter.Write(jsonDatos);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string errorMensaje = streamReader.ReadToEnd();
                        Console.WriteLine(errorMensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return correcto;
        }

        private Pedido crearClasePedido()
        {
            List<DetallePedido> detallesPedido = new List<DetallePedido>();
            List<ProductoCarrito> listaCarrito = Session["session-carrito"] as List<ProductoCarrito>;

            decimal precioTotal = 0;
            //Recorremos la lista de productos que hay en el carrito
           //Cada vez que va a un porductoCarrito, crea un nuevo detalle pedido y  lo añade a la lista.
            foreach (ProductoCarrito productoCarrito in listaCarrito)
            {
                DetallePedido detallePedido = new DetallePedido()
                {
                    idPedido = 0,
                    idProducto = productoCarrito.idProducto,
                    precio = productoCarrito.total,
                    unidades = productoCarrito.cantidad
                };
                detallesPedido.Add(detallePedido);
                precioTotal += detallePedido.precio;
            }
            Pedido pedido = new Pedido()
            {
                idEstado = 5, //Estado pedido: pendiente
                fechaPedido = DateTime.UtcNow,
                idPedido = 0,
                totalPrecio = precioTotal,
                idUsuario = int.Parse(Session["session-id"].ToString()),
                detallePedido = detallesPedido //añadimos la lsita a detallePedido
            };

            return pedido;

        }

        private void totalPecio()
        {
            List<ProductoCarrito> productos = Session["session-carrito"] as List<ProductoCarrito>;
            decimal total = productos.Sum(p => p.total);
            lblTotal.Text = total.ToString();
        }

        protected void btnVovler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Escaparate.aspx");
        }
    }
}
