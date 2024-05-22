<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="NTT_Shop.WebForms.Registrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="row">

                <div class="col-12 text-center">
                    <h1>Registrarse</h1>
                    <hr />
                </div>
                <div class="col-6"  style="margin-top:5px;">

                    <div class="form-group">
                        <asp:Label Text="Nombre:" runat="server" Font-Bold="True" />
                        <asp:TextBox ID="txtNombre" runat="server" class="form-control" />
                        <asp:RegularExpressionValidator ID="revNombre" runat="server" Display="Dynamic" ControlToValidate="txtNombre" ErrorMessage="El campo Nombre no puede contener números." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$" ForeColor="Red" />

                    </div>
                </div>
                <div class="col-6  style="margin-top:5px;"">

                    <div class="form-group">
                        <asp:Label Text="Primer apelldio:" runat="server" Font-Bold="True" />
                        <asp:TextBox ID="txtApellido" runat="server" class="form-control" />
                        <asp:RegularExpressionValidator ID="revAp1" runat="server" Display="Dynamic" ControlToValidate="txtApellido" ErrorMessage="El campo Apellido no puede contener números." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$" ForeColor="Red" />

                    </div>
                </div>
                <div class="col-6"  style="margin-top:5px;">

                    <div class="form-group">
                        <asp:Label Text="Nombre Usuario:" runat="server" Font-Bold="True" />
                        <asp:TextBox ID="txtnomUsuario" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="col-6"  style="margin-top:5px;">

                    <div class="form-group">
                        <asp:Label Text="Contraseña:" runat="server" Font-Bold="True" />
                        <asp:TextBox ID="txtContrasenya" runat="server" TextMode="Password" class="form-control" />
                        <asp:RegularExpressionValidator ID="revContr" runat="server" Display="Dynamic" ControlToValidate="txtContrasenya" ErrorMessage="La contraseña debe tener mínimo 10 caracteres, un número y una mayúscula." ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*\d).{10,}$" />
                    </div>
                </div>

                <div class="col-12" style="margin-top:5px;">

                    <div class="form-group">
                        <asp:Label Text="Correo electrónico:" runat="server" Font-Bold="True" />
                        <asp:TextBox ID="txtCorreo" runat="server" class="form-control" />
                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" Display="Dynamic" ControlToValidate="txtCorreo" ErrorMessage="Dirección de correo inválido. Debe tener '@' y terminar en '.com' o '.es'." ValidationExpression="^[^\s@]+@[^\s@]+\.(com|es)$" />

                    </div>
                </div>
                <div class="col-12 text-center">
                    <asp:Label ID="lblValidacion" runat="server" Style="color: red;" class="text-center" />
                </div>
                <div class="col-12 d-grid" style="margin-top: 15px;">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" OnClick="btnRegistrar_Click" class="btn btn-outline-success" />
                </div>
                <div class="col-12 text-center" style="margin-top:10px;">
                       <a href="IniciarSesion.aspx" >Ya tengo una cuenta</a>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
