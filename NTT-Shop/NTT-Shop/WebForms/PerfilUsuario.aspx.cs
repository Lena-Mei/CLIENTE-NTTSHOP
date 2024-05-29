using API_NTT_SHOP.Models;
using Newtonsoft.Json;
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
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        private string generalUrl = "http://localhost:5000/api/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    Usuario usuario = GetUsuario();
                    if (usuario == null)
                    {
                        txtUser.Text = "no funciona crack";
                    }
                    else
                    {
                        
							txtUser.Text = usuario.Nombre.ToString();
							txtAp2.Text = usuario.Apellido2.ToString();
						
							txtDir.Text = usuario.Direccion.ToString();

							txtPrv.Text = usuario.Provincia.ToString();
						
							txtCiudad.Text = usuario.Ciudad.ToString();
						
							txtCP.Text = usuario.CodigoPostal.ToString();

							txtTel.Text = usuario.Telefono.ToString();

						
						txtAp1.Text = usuario.Apellido1.ToString();
                        txtEmail.Text = usuario.Email.ToString();
                    }
                }
               
            }
            else
            {
                Response.Redirect("IniciarSesion.aspx");
            }
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int error;
            Usuario usuario = GetUsuario();
            Usuario usAct = new Usuario()
            {
                IdUsuario = usuario.IdUsuario,
                Inicio = usuario.Inicio,
                Contrasenya = usuario.Contrasenya,
                Nombre = txtUser.Text,
                Apellido1 = txtAp1.Text,
                Apellido2 = txtAp2.Text,
                Direccion = txtDir.Text,
                Provincia = txtPrv.Text,
                Ciudad = txtCiudad.Text,
                CodigoPostal = txtCP.Text,
                Telefono = txtTel.Text,
                Email = usuario.Email,
                IsoIdioma = usuario.IsoIdioma,
                IdRate = usuario.IdRate
            };

            bool correcto = ActualizarUsuario(usAct, out error);
            if (correcto)
            {
                lblCorrecto.Text = "Se ha actualizado correctamente";
                Response.Redirect("PerfilUsuario.aspx");
            }
            else
            {
                if (error == 2)
                {
                    lblValidacion.Text = "Debes de completar los campos.";
                }
                else
                {

                    lblValidacion.Text = "Algún dato ya está registrado (correo, nombre de Usuario...).";
                }
            }
            
        }

        private bool ActualizarUsuario(Usuario usuario, out int error)
        {
            error = 0;
            bool correcto = false;
            var usuarioData = new { usuario = usuario };

            string jsonDatos = JsonConvert.SerializeObject(usuarioData);
            string url = generalUrl + "Usuario/updateUsuario";
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
                    error = 1;

                }
                else if (ex.Message.Contains("404")) //NotFound
                {

                    error = 2;
                }
            }

            return correcto;


        }

        protected void btnCambiarC_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCambiarC_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CambiarContrasenya.aspx");
        }

        protected void btnActCorreo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActCorreo.aspx");
        }
    }
}