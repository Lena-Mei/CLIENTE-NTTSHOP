<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="NTT_Shop.WebForms.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-12 text-center">
            <h1>Mis datos</h1>
            <hr />
        </div>
        <div class="col-6">
            <div class="row">
                <div class="form-group">
                    <asp:Label runat="server" ID="lblusuario" Text="Nombre:" Font-Bold="True" class="form-label" />
                    <asp:TextBox runat="server" ID="txtUser" class="form-control" />
                    <asp:RegularExpressionValidator ID="revNombre" runat="server" Display="Dynamic" ControlToValidate="txtUser"  ErrorMessage="El campo Nombre no puede contener números." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$" ForeColor="Red" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" Display="Dynamic" ControlToValidate="txtUser" ForeColor="Red"  ErrorMessage="El campo Nombre es obligatorio." />
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblAp" Text="Apellidos:" Font-Bold="True" />
                    <div class="row">
                        <div class="col-6">
                            <asp:TextBox runat="server" ID="txtAp1" class="form-control" />
                            <asp:RegularExpressionValidator ID="revAp1" runat="server" Display="Dynamic" ControlToValidate="txtAp1"   ErrorMessage="El campo Apellido no puede contener números." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$" ForeColor="Red" />
                            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" Display="Dynamic" ControlToValidate="txtAp1" ForeColor="Red"  ErrorMessage="El primer apellido es obligatorio." />
                        </div>
                        <div class="col-6">
                            <asp:TextBox runat="server" ID="txtAp2" class="form-control" />
                            <asp:RegularExpressionValidator ID="revAp2" runat="server" Display="Dynamic" ControlToValidate="txtAp2"  ErrorMessage="El campo Apellido no puede contener números." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$" ForeColor="Red" />

                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblDir" Text="Dirección:" Font-Bold="True" />
                    <asp:TextBox runat="server" ID="txtDir" class="form-control" />
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblPrv" Text="Provincia:" Font-Bold="True" /><asp:TextBox runat="server" ID="txtPrv" class="form-control" />
                    <asp:RegularExpressionValidator ID="revProv" runat="server" Display="Dynamic" ControlToValidate="txtPrv"  ErrorMessage="El campo Provincia no puede contener números." ValidationExpression="[a-zA-Z]+" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="row">
                <div class="form-group">
                    <asp:Label runat="server" ID="lblCi" Text="Ciudad:" Font-Bold="True" />
                    <asp:TextBox runat="server" ID="txtCiudad" class="form-control" />
                    <asp:RegularExpressionValidator ID="revCiudad" runat="server" Display="Dynamic" ControlToValidate="txtCiudad" ErrorMessage="El campo Ciudad no puede contener números." ValidationExpression="[a-zA-Z]+" ForeColor="Red" />

                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblCp" Text="Código Postal:" Font-Bold="True"></asp:Label><asp:TextBox runat="server" ID="txtCP" MaxLength="5" class="form-control" />
                    <asp:RegularExpressionValidator ID="revCp" runat="server" Display="Dynamic" ControlToValidate="txtCP"  ErrorMessage="El campo Código Postal solo puede contener números." ValidationExpression="^\d+$" ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revCp2" runat="server" Display="Dynamic" ControlToValidate="txtCP"  ErrorMessage="El campo Código Postal debe contener 5 dígitos." ValidationExpression="\b\d{5}\b" ForeColor="Red" />
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblTel" Text="teléfono:" Font-Bold="True"> </asp:Label><asp:TextBox runat="server" ID="txtTel" MaxLength="9" TextMode="Phone" class="form-control" />
                    <asp:RegularExpressionValidator ID="revTel" runat="server" Display="Dynamic" ControlToValidate="txtTel"  ErrorMessage="El campo Teléfono solo puede contener números." ValidationExpression="^\d+$" ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revTel2" runat="server" Display="Dynamic" ControlToValidate="txtTel"  ErrorMessage="El campo Teléfono debe contener 9 dígitos." ValidationExpression="\b\d{9}\b" ForeColor="Red" />
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lblEmail" Text="Correo electrónico:" Font-Bold="True" />
                    <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" class="form-control" disabled="True" />

                </div>
            </div>
        </div>
        <div class="col-4 d-grid gap-2" style="margin-top: 10px;">
            <asp:Button runat="server" ID="btnActualizar" Text="Guardar" OnClick="btnActualizar_Click" class="btn btn-outline-success" />
        </div>
        <div class="col-4 d-grid gap-2" style="margin-top: 10px;">
            <asp:Button runat="server" ID="btnCambiarC" Text="Cambiar Contraseña"  OnClick="btnCambiarC_Click1"  class="btn btn-outline-secondary"/>
        </div>
          <div class="col-4 d-grid gap-2" style="margin-top: 10px;">
      <asp:Button runat="server" ID="btnActCorreo" Text="Cambiar Correo"  OnClick="btnActCorreo_Click"  class="btn btn-outline-secondary"/>
  </div>
    </div>


    <asp:Label runat="server" ID="lblCorrecto" Text="" Font-Bold="True" />
    <asp:Label runat="server" ID="lblValidacion" Text="" Font-Bold="True" />
</asp:Content>
