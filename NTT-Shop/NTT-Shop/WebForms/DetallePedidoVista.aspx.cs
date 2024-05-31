using System;
using System.Collections.Generic;
using NTT_Shop.Models.Entities;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace NTT_Shop.WebForms
{
    public partial class DetallePedidoVista : System.Web.UI.Page
    {
        private string generalUrl = "http://localhost:5000/api/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["session-dPedido"] != null)
                    {
                        mostrarDatos();
                    }
                    else
                    {
                        Response.Redirect("Escaparate.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
        }

        private void mostrarDatos()
        {
            ListaPedido pedido = getPedido();
            List<Estado> estados = GetAllEstados();
            foreach (var estado in estados)
            {
                if (estado.idEstado == pedido.idEstado)
                {
                    pedido.estado = estado;
                }
            }
            Session["session-estado"] = pedido.estado.idEstado;
            lblEstado.Text = pedido.estado.descripcion.ToString();
            lblFecha.Text = pedido.fechaPedido.ToString();
            lblidPedido.Text = pedido.idPedido.ToString();
            lblTotal.Text = pedido.totalPrecio.ToString();

            List<DPedido> listaProductos = Session["session-dPedido"] as List<DPedido>;
            if(listaProductos != null && listaProductos.Count > 0)
            {
                rptpedido.DataSource = listaProductos;
                rptpedido.DataBind();
            }
        }

        private ListaPedido getPedido()
        {
            ListaPedido pedido = new ListaPedido();
            try
            {
                string idioma = Session["session-iso"].ToString();
                int idPedido = int.Parse(Session["session-idPedido"].ToString());
                int idRate = int.Parse(Session["session-rate"].ToString());
                string url = generalUrl + "Pedido/getPedido?id="+idPedido+"&idioma="+idioma+"&idRate="+idRate;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    pedido = json["pedido"].ToObject<ListaPedido>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pedido;
        }

        private List<Estado> GetAllEstados()
        {
            List<Estado> estados = new List<Estado>();
            try
            {
                string url = generalUrl + "Pedido/getAllEstados";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var estadoArray = json["estadoLista"].ToObject<JArray>();
                    estados = estadoArray.ToObject<List<Estado>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return estados;
        }


        protected void btnVovler_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisPedidos.aspx");
        }

        private bool DevolverPedido()
        {
            ListaPedido pedido = getPedido();
            bool correcto = false;
            try
            {
                string url = generalUrl + "Pedido/updateEstadoPedido/" + pedido.idPedido + "/" + 6;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return correcto;

        }

        protected void btnDevolver_Click(object sender, EventArgs e)
        {
            bool correcto = DevolverPedido();
            SumarStock(int.Parse(lblidPedido.Text));
            if (correcto)
            {
                Response.Redirect("DetallePedidoVista.aspx");
            }
        }


        private void SumarStock(int idPedido)
        {
            Usuario user = GetUsuario(int.Parse(Session["session-id"].ToString()));
            Pedido ped = obtenerPedido(idPedido, user.IsoIdioma, user.IdRate);
            Producto prod;
            string error;
            foreach (DetallePedido item in ped.detallePedido)
            {
                prod = item.producto;
                prod.stock = prod.stock + item.unidades;
                ActualizarProducto(prod, out error);
            }
        }

        private Usuario GetUsuario(int? id)
        {
            Usuario usuario = new Usuario();
            string url = generalUrl + "Usuario/getUsuario/" + id;
            try
            {
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
                usuario = null;
            }
            return usuario;
        }
        private bool ActualizarProducto(Producto producto, out string error)
        {
            error = "";
            bool correcto = false;
            var adminData = new { producto = producto };

            string jsonDatos = JsonConvert.SerializeObject(adminData);
            string url = generalUrl + "Producto/updateProducto";
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";
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

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400")) //BadRequest
                {
                    error = "Algún dato está vacío o es nulo.";

                }
                else if (ex.Message.Contains("404")) //NotFound
                {

                    error = "Algún dato introducido ya existe o es inválido.";
                }
            }
            return correcto;
        }
        private Pedido obtenerPedido(int idPedido, string idioma, int idRate)
        {
            Pedido pedido = new Pedido();
            try
            {
                string url = generalUrl + "Pedido/getPedido?id=" + idPedido + "&idioma=" + idioma + "&idRate=" + idRate;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    pedido = json["pedido"].ToObject<Pedido>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pedido;
        }
    }
}