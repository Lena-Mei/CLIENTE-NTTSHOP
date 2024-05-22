using API_NTT_SHOP.Models;
using Newtonsoft.Json.Linq;
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
    public partial class WebForm1 : System.Web.UI.Page
    {

        private string generalUrl = "http://localhost:5000/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {

            int errorMensaje;
            bool error = InicioSesion(out errorMensaje);

            if (error)
            {
                lblValidacion.Text = string.Empty;
                string url = generalUrl + "Usuario/getIdUsuario/" + txtUsuario.Text;
                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var resultado = streamReader.ReadToEnd();
                        var json = JObject.Parse(resultado);

                        string id = json["idUsuario"].ToString();
                        string iso = json["idiomaIso"].ToString();
                        string idRate = json["idRate"].ToString();

                        Session["session-iso"] = iso;
                        Session["session-id"] = id;
                        Session["session-rate"] = idRate;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Response.Redirect("Escaparate.aspx");
            }
            else
            {
                if (errorMensaje == 1)
                {
                    lblValidacion.Text = "Debes de completar los campos.";
                }
                else
                {

                lblValidacion.Text = "Usuario o contraseña incorrecto.";
                }
            }
        }

        private bool InicioSesion(out int errorMensaje)
        {
            errorMensaje = 0;
            bool correcto = false;
            var url = generalUrl + "Usuario/getLogin?inicio=" + txtUsuario.Text + "&contrasenya=" + txtContrasenya.Value;
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

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
                        string error = streamReader.ReadToEnd();
                        Console.WriteLine(error);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("400"))
                {
                    errorMensaje = 1;

                }
                else if (ex.Message.Contains("404"))
                {

                    errorMensaje = 2;
                }
            }

            return correcto;
        }
    }
}