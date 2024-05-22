<%@ Page Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Title="MasterPage Example" CodeBehind="Example2.aspx.cs" Inherits="NTT_Shop.WebForms.Example2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>
			        <header>
				        <h1>Ejemplo 2</h1>
			        </header>
							<asp:Label runat="server" ID="lblID"></asp:Label>
							<asp:Label runat="server" ID="lblIso"></asp:Label>
                   			
		        </article>
	
	        </section>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
