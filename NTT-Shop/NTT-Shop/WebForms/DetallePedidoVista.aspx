<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="DetallePedidoVista.aspx.cs" Inherits="NTT_Shop.WebForms.DetallePedidoVista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-5">
            <div class="row">
                <div class="col-12">
                    <h3>Pedido Detalle</h3>
                    <hr />
                </div>
                <div class="col-3">
                    <p class="fw-bold">Nº Pedido</p>
                </div>
                <div class="col-9">
                    <asp:Label ID="lblidPedido" runat="server" />
                </div>
                <div class="col-3">
                    <p class="fw-bold">Fecha: </p>
                </div>
                <div class="col-9">
                    <asp:Label ID="lblFecha" runat="server" />
                </div>
                <div class="col-3">
                    <p class="fw-bold">Estado:</p>
                </div>
                <div class="col-9">
                    <asp:Label ID="lblEstado" runat="server" />
                </div>
            </div>
            <hr />

        </div>
        <div class="col-7">
            <div class="row">
                <div class="col-12">
                    <h3>Productos Seleccionados</h3>
                    <hr />
                    <table class="table">
                        <thead>
                            <tr>
                                <th scoope="col">Id</th>
                                <th scoope="col">Nombre</th>
                                <th scoope="col">Cantidad</th>
                                <th scoope="col">Precio</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptpedido">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("idProducto") %></td>
                                        <asp:Repeater runat="server" ID="Repeater1" DataSource='<%# Eval("Descripcion") %>'>
                                            <ItemTemplate>
                                                <td><%# Eval("nombre") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td><%# Eval("unidades") %></td>
                                        <asp:Repeater runat="server" ID="rptRate" DataSource='<%# Eval("Rate") %>'>
                                            <ItemTemplate>
                                                <td><%# Eval("precio") %>€</td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div class="col-6">
                        <p class="fw-bold">
                            Total Precio:
                            <asp:Label runat="server" ID="lblTotal" />€
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12" style="margin-top: 15px;">
            <% int idEstado = int.Parse(Session["session-estado"].ToString());
                if (idEstado == 1)
                {%>
            <asp:Label runat="server" ID="alerta1" class="alert alert-info">El Pedido está <strong>CONFIRMDO</strong> por el Administrador. A la espera de ser <underline>Enviado</underline></asp:Label>
            <% }
                if (idEstado == 2)
                {%>
            <asp:Label runat="server" ID="alerta2" class="alert alert-success">El Pedido está <strong>ANULADO</strong> por el Administrador.</asp:Label>
            <% }
                if (idEstado == 3)
                {%>
            <asp:Label runat="server" ID="alerta3" class="alert alert-primary">El Pedido ha sido <strong>ENVIADO</strong>. A la espera de ser <underline>Entregado</underline></asp:Label>
            <% }
                if (idEstado == 4)
                {%>
            <asp:Label runat="server" ID="alerta4" class="alert alert-success">El Pedido está <strong>ENTREGADO</strong></asp:Label>
            <% }
                if (idEstado == 5)
                {%>
            <asp:Label runat="server" ID="alerta5" class="alert alert-secondary">El Pedido está <strong>PENDIENTE</strong>. A la espera de ser <underline>Confirmado</underline> por el Administrador</asp:Label>
            <% }
                if (idEstado == 6)
                {%>
            <asp:Label runat="server" ID="alerta6" class="alert alert-warning">El Pedido ha sido <strong>DEVUELTO</strong> por el usuario.</asp:Label>
            <% } %>
        </div>

        <div class="col-12" style="margin-top: 25px;">
            <asp:Button runat="server" ID="btnVovler" class="btn btn-secondary" Text="Volver lista" OnClick="btnVovler_Click" />
             <% 
                    if (idEstado == 4)
                    {%>
                        <asp:Button runat="server" ID="Button1" class="btn btn-warning" Text="Devolver pedido" OnClick="btnDevolver_Click" />
 <% }%>
        </div>
    </div>
</asp:Content>
