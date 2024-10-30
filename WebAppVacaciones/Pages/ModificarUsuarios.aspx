
<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="ModificarUsuarios.aspx.cs" Inherits="WebAppVacaciones.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <br />
    <asp:Button runat="server" CssClass="button is-light" ID="btncreate" Visible="false" Text="Create"/>
    <asp:GridView CssClass="table center mx-auto" runat="server" ID="datos" OnRowDataBound="datos_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Opciones">
                <ItemTemplate>
                    <asp:Button runat="server" CssClass="button is-primary" ID="btnread" Text="Read" />
                    <asp:Button runat="server" CssClass="button is-warning" ID="btnupdate" Text="Update" />
                    <asp:Button runat="server" CssClass="button is-danger" ID="btndelete" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>