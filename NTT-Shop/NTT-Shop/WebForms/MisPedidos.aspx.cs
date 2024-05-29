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
    public partial class MisPedidos : System.Web.UI.Page
    {
        private string generalUrl = "http://localhost:5000/api/";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    List<ListaPedido> listaPedido = GetPedidosIdUsuario();
                    if (listaPedido.Count > 0)
                    {
                        MostrarPedido();
                    }
                    else
                    {

                        Response.Redirect("SinPedidos.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
        }

        private void MostrarPedido()
        {
            List<ListaPedido> lista = GetPedidosIdUsuario();
            List<Estado> estado = GetAllEstados();
            foreach (var pedido in lista)
            {
                foreach(var estados in estado)
                {
                    if(pedido.idEstado == estados.idEstado)
                    {
                        pedido.estado=estados;
                    }
                }
            }
            var pedidosOrdenados = lista.OrderByDescending(p => p.fechaPedido).ToList();
            try
            {
                rptPedidos.DataSource = pedidosOrdenados;
                rptPedidos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private List<ListaPedido> GetPedidosIdUsuario()
        {
            int idUsuario = int.Parse(Session["session-id"].ToString());
            List<ListaPedido> listaPedidos = new List<ListaPedido>();
            try
            {
                string url = generalUrl + "Pedido/getPedidoIdUser/" + idUsuario;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    var pedidoArray = json["pedidoLista"].ToObject<JArray>();
                    listaPedidos = pedidoArray.ToObject<List<ListaPedido>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaPedidos;
        }

        protected void btnVer_Command(object sender, CommandEventArgs e)
        {
            int idPedido = int.Parse(e.CommandArgument.ToString());
            Session["session-idPedido"] = idPedido;
            Pedido pedido = getPedido(idPedido);
            List<DPedido> detallesPedido = new List<DPedido>();

            if(pedido != null)
            {
                foreach (var detalle in pedido.detallePedido)
                {
                    DPedido dPedido = new DPedido
                    {
                        idPedido = pedido.idPedido,
                        idProducto = detalle.idProducto,
                        precio = detalle.precio,
                        unidades = detalle.unidades,
                        descripcion = detalle.producto.descripcion,
                        rate = detalle.producto.rate
                    };
                    
                    detallesPedido.Add(dPedido);
                }
                Session["session-dPedido"] = detallesPedido;
                Response.Redirect("DetallePedidoVista.aspx");
            }
            else
            {
                Response.Redirect("Escaparate.aspx");
            }
            
        }

        private Pedido getPedido(int idPedido)
        {
            int idRate = int.Parse(Session["session-rate"].ToString());
            Pedido pedido = new Pedido();
            try
            {
                string idioma = Session["session-iso"].ToString();
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


    }
}