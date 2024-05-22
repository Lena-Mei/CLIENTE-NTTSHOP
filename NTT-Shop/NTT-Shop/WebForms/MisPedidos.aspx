<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="MisPedidos.aspx.cs" Inherits="NTT_Shop.WebForms.MisPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Mis Pedidos</h3>
    <div class="row">
        <div class="col">
            <table class="table">
                <thead>
                    <tr>
                        <th scoope="col">Nº Pedido</th>
                        <th scoope="col">Fecha</th>
                        <th scoope="col">Estado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPedidos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <th scoope="row"><%# Eval("idPedido") %></th>
                                <td><%# Eval("fechaPedido") %></td>
                                
                        <%--         <asp:Repeater ID="rptEstado" runat="server" DataSource='<%# Eval("Estado") %>'>
                                     <ItemTemplate>--%>
                                         <td><%# Eval("estado.descripcion") %></td>
                          <%--           </ItemTemplate>
                                 </asp:Repeater>--%>

                                <td>
                                    <asp:Button ID="btnVer" runat="server" CssClass="btn btn-outline-secondary" CommandArgument='<%# Eval("idPedido") %>' CommandName="VerPedido" OnCommand="btnVer_Command" Text="ver Pedido" />
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
