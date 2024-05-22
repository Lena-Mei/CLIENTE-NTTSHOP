<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="Escaparate.aspx.cs" Inherits="NTT_Shop.WebForms.Escaparate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Escaparate</h1>
            <div class="row row-cols-3">
    <asp:Repeater ID="rptProductos" runat="server" OnItemCommand="rptProductos_ItemCommand">
        <ItemTemplate>
                <div class="col mb-4">
                    <div class="card h-90">
                        <asp:Label runat="server" ID="lblId" class="card-header" style="color: green"><%# Eval("idProducto") %></asp:Label>
                        <div class="card-body">
                            <asp:Repeater ID="rptDes" runat="server" DataSource='<%# Eval("Descripcion") %>'>
                                <ItemTemplate>
                                    <p class="card-title fw-semibold"><%# Eval("nombre") %></p>
                                    <p class="card-title "><%# Eval("descripcion") %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="row">
                                <div class="col">
                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Rate") %>'>
                                        <ItemTemplate>
                                            <p class=" fw-semibold" style="color:red;"><%# Eval("precio") %>€</p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col">
                                    <asp:Button runat="server" Text="Carrito" ID="btnAnyadir" CommandArgument='<%# Eval("idProducto") %>' CommandName="PasarId" OnCommand="btnAnyadir_Command" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ItemTemplate>
    </asp:Repeater>
            </div>
</asp:Content>
