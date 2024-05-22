<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="NTT_Shop.WebForms.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Agregar al carrito</h1>
    <div class="row">
        <div class="col-3">
            <p class="fw-semibold">Nombre del producto:</p>
        </div>
        <div class="col-9">
            <asp:Label runat="server" ID="lblNombre" />
        </div>
        <div class="col-3">
            <p class="fw-semibold">Descripción del producto:</p>
        </div>
        <div class="col-9">
            <asp:Label runat="server" ID="lblDes"  />
        </div>
        <div class="col-3">
            <p class="fw-semibold">ID producto:</p>
        </div>
        <div class="col-9">
            <asp:Label runat="server" ID="lblId" />
        </div>
        <div class="col-3">
            <p class="fw-semibold">Precio producto:</p>
        </div>
        <div class="col-9">
            <asp:Label runat="server" ID="lblPrecio" />
        </div>
    </div>
    <asp:Button runat="server" Text="Volver a escaparate" ID="btnVolver" class="btn btn-secondary" OnClick="btnVolver_Click" />
    <asp:Button runat="server" Text="Añadir al Carrito" ID="btnAnyadir" OnClick="btnAnyadir_Click" class="btn btn-success" />
</asp:Content>
