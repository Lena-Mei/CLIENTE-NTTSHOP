<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="NTT_Shop.WebForms.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <div class="row">
                <div class="col-12">
                    <h2>Datos usuario</h2>
                                    <hr />

                </div>
                <div class="col-3 fw-bold">
                    <p>Usuario: </p>
                </div>
                <div class="col-9">
                    <asp:Label ID="lblNombre" runat="server" />
                </div>
                <div class="col-3 fw-bold">
                    <p>Email: </p>
                </div>
                <div class="col-9" >
                    <asp:Label ID="lblCorreo" runat="server" />
                </div>
                <div class="col-3 fw-bold">
                    <p>Teléfono:</p>
                </div>
                <div class="col-9">
                    <asp:Label ID="lblTel" runat="server" />
                </div>
            </div>
        </div>
        <div class="col-8">
            <div class="row">
                <div class="col-12">
                    <h2>Datos dirección</h2>
                    <hr />
                </div>
                <div class="col-6">
                    <div class="row">
                        <div class="col-3 fw-bold">
                            <p>Dirección: </p>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblDir" runat="server" />
                        </div>
                        <div class="col-3 fw-bold">
                            <p>Provincia: </p>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblPrv" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="row">
                        <div class="col-3 fw-bold">
                            <p>Ciudad:</p>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblCiu" runat="server" />
                        </div>
                        <div class="col-3  fw-bold">
                            <p>CP:</p>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblCp" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <h3>Productos</h3>
        </div>
        <div class="col-12">
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">idProducto</th>
                        <th scope="col">Nombre</th>
                        <!--Producto.descripcion -->
                        <%--                        <th scope="col"> Descripcion</th> <!--Producto.descripcion -->--%>
                        <th scope="col">Precio</th>
                        <!--Producto.rate -->
                        <th scope="col">Cantidad</th>
                        <th scope="col">Total</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptProductos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("idProducto") %></th>
                                <asp:Repeater ID="rptProducto" runat="server" DataSource='<%# Eval("producto") %>'>
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptDes" runat="server" DataSource='<%# Eval("Descripcion") %>'>
                                            <ItemTemplate>
                                                <td><%# Eval("nombre") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="rptRate" runat="server" DataSource='<%# Eval("Rate") %>'>
                                            <ItemTemplate>
                                                <td><%# Eval("precio") %>€</td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td>
                                    <asp:Button runat="server" Text="<" ID="btnMenos" CommandArgument='<%# Eval("idProducto") %>' CommandName="RestarCant" OnCommand="btnMenos_Command" CssClass="btn btn-secondary btn-sm" />
                                    <%# Eval("cantidad") %>
                                    <asp:Button runat="server" Text=">" ID="btnMas" CommandArgument='<%# Eval("idProducto") %>' CommandName="SumarCant" OnCommand="btnMas_Command" CssClass="btn btn-secondary btn-sm" />
                                </td>
                                <td><%# Eval("total") %>€</td>
                                <td>
                                    <asp:Button runat="server" Text="Eliminar" ID="btnEliminar" CommandArgument='<%# Eval("idProducto") %>' CommandName="EliminarPr" OnCommand="btnEliminar_Command" CssClass="btn btn-danger btn-sm" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="col-10">
                <p class="text-end fw-bold">
                    Precio total:
                    <asp:Label runat="server" ID="lblTotal"/>€
                </p>
            </div>

        </div>
    </div>
    <asp:Button runat="server" ID="btnVovler" Text="Seguir comprando"  class="btn btn-secondary" OnClick="btnVovler_Click" />
    <asp:Button runat="server" class="btn btn-success" ID="btnConfirmar" Text="Confirmar Pedido" OnClick="btnConfirmar_Click" />

</asp:Content>
