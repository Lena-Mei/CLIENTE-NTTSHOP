﻿using Newtonsoft.Json.Linq;
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
    public partial class ActCorreo : System.Web.UI.Page
    {
        private string generalUrl = "http://localhost:5000/api/";

        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarAlerts(false, false);
            MostrarDatos();

        }


        private void MostrarAlerts(bool error, bool correcto)
        {
            if (!correcto)
            {
                lblCorrecto.Text = string.Empty;
            }
            if (!error)
            {
                lblError.Text = string.Empty;
            }
            lblCorrecto.Enabled = correcto;
            lblError.Enabled = error;
        }

        private void MostrarDatos()
        {
            Usuario usuario = GetUsuario();
            txtUser.Text = usuario.Inicio.ToString();
        }


        private Usuario GetUsuario()
        {
            Usuario usuario = new Usuario();
            string url = generalUrl + "Usuario/getUsuario/" + Session["session-id"].ToString();
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

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            bool correcto = Cambiar();
            if (correcto)
            {
                MostrarAlerts(false, true);
                lblCorrecto.Text = "Se ha actualizado correctamente el correo.";
            }
            else
            {
                MostrarAlerts(true, false);
                lblError.Text = "El correo introducido ya existe. Debes de introducir otro.";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerfilUsuario.aspx");
        }

        private bool Cambiar()
        {
            bool correcto = false;
            try
            {

                string url = generalUrl + "Usuario/updateEmail?idUsuario=" + Session["session-id"].ToString() + "&correo=" + txtCorreo.Text;

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
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            return correcto;
        }
    }
}