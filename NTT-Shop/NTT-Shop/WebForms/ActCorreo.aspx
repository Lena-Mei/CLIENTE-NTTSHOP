<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="ActCorreo.aspx.cs" Inherits="NTT_Shop.WebForms.ActCorreo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="row justify-content-center">
    <div class="col-md-4">
        <h2 class="text-center">Actualizar correo</h2>
        <hr />
        <div class="form-group">
             <asp:Label runat="server" ID="lblusuario" Text="Nombre Usuario:" Font-Bold="True" class="form-label" />
             <asp:TextBox runat="server" ID="txtUser" class="form-control" disabled="true" />
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="lblCorreo" Text="Nueva contraseña" class="form-label fw-bold"/><br />
            <asp:TextBox runat="server" ID="txtCorreo" class="form-control" />
            <asp:RegularExpressionValidator ID="revCorreo" runat="server" Display="Dynamic" ControlToValidate="txtCorreo" ForeColor="Red" ErrorMessage="Dirección de correo inválido. Debe tener '@' y terminar en '.com' o '.es'." ValidationExpression="^[^\s@]+@[^\s@]+\.(com|es)$" ></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvC" runat="server" Display="Dynamic" ControlToValidate="txtCorreo" ForeColor="Red"  ErrorMessage="Debes de introducir el nuevo correo." />
        </div>
        
        <div class="col-12 d-grid" style="margin-top:10px;">
           <asp:Button runat="server" ID="btnCambiar" Text="Actualizar correo" class="btn btn-outline-dark" OnClick="btnCambiar_Click" />
        </div>
        <div class="col-12 d-grid" style="margin-top:10px;">
            <asp:Button runat="server" ID="btnVolver" Text="Volver" class="btn btn-outline-secondary" OnClick="btnVolver_Click" />
        </div>

    </div>
            <asp:Label runat="server" ID="lblCorrecto"  class="alert alert-success" style="margin-top:20px;"/><br />
            <asp:Label runat="server" ID="lblError"  class="alert alert-danger"/><br />
  
</div>
</asp:Content>
