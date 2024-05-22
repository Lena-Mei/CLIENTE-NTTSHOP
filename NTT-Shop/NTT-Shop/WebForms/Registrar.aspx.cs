using API_NTT_SHOP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Registrar : System.Web.UI.Page
    {
        private string generalUrl = "http://localhost:5000/api/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int error;
            Usuario usuario = new Usuario()
            {
                IdUsuario = 0,
                Inicio = txtnomUsuario.Text,
                Contrasenya = txtContrasenya.Text,
                Nombre = txtNombre.Text,
                Apellido1 = txtApellido.Text,
                Apellido2 = "",
                Direccion = "",
                Provincia = "",
                Ciudad = "",
                CodigoPostal = "",
                Telefono = "",
                Email = txtCorreo.Text,
                IsoIdioma = "es",
                IdRate = 1
            };

            bool correcto = Registrarse(usuario, out error);
            if (correcto)
            {
                lblValidacion.Text = string.Empty;
                string url = generalUrl + "Usuario/getIdUsuario/" + txtnomUsuario.Text;
                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    //httpRequest.Method = "GET";
                    //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    //{
                    //    var resultado = streamReader.ReadToEnd();
                    //    var json = JObject.Parse(resultado);

                    //    string id = json["idUsuario"].ToString();
                    //    string iso = json["idiomaIso"].ToString();

                    //    Session["session-iso"] = iso;
                    //    Session["session-id"] = id;


                    //}
                    Response.Redirect("IniciarSesion.aspx");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                if (error == 1)
                {
                    lblValidacion.Text = "Debes de completar los campos.";
                }
                else
                {

                    lblValidacion.Text = "Algún dato ya está registrado (correo, nombre de Usuario...).";
                }
            }
        }

        private bool Registrarse(Usuario usuario, out int error)
        {
            error = 0;
            bool correcto = false;

            var usuarioData = new { usuario = usuario };

            string jsonDatos = JsonConvert.SerializeObject(usuarioData);
            string url = generalUrl + "Usuario/insertUsuario";
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
                if (ex.Message.Contains("400"))
                {
                    error = 1;

                }
                else if (ex.Message.Contains("404"))
                {

                    error = 2;
                }
            }

            return correcto;
        }
    }
}