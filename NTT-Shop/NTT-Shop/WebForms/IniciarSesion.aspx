<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="NTT_Shop.WebForms.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="row justify-content-center">
    <div class="col-md-4">
            <h1 class="text-center">Iniciar Sesión</h1>
            <hr />
        <div class="form-group" style="margin-top:5px;">
            <asp:Label Text="Usuario:" runat="server" Font-Bold="true" />
            <asp:TextBox ID="txtUsuario" runat="server" class="form-control" />
        </div>
        <div class="form-group" style="margin-top:5px;">
            <asp:Label Text="Contraseña:" runat="server"  Font-Bold="true" />
            <input type="password" id="txtContrasenya" runat="server" class="form-control" />
        </div>
        <div class="col-12 text-center">
            <asp:Label ID="lblValidacion" runat="server" Style="color: red;" />
        </div>
        <div class="col-12 d-grid" style="margin-top:15px;">
            <asp:Button ID="btnIniciar" runat="server" Text="Iniciar Sesión" OnClick="btnIniciar_Click" class="btn btn-outline-success"/>
        </div>
        <div class="col-12 text-center" style="margin-top:5px;">
            <a href="Registrar.aspx">Crear una cuenta</a>
        </div>
            
    </div>
      </div>
</asp:Content>
