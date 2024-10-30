<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Update Users.aspx.cs" Inherits="WebAppVacaciones.Pages.Update_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Modificar Usuario
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Añadir CSS personalizado aquí -->
    <style>


        .custom-button:hover {
            background-color: #08a0de; /* Color de fondo al pasar el mouse */
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <section class="hero custom-hero">
        <div class="hero-body">
            <div class="container">
                <h1 class="title">Modificar Usuario</h1>
                <br />
                <h2 class="subtitle">Completa el formulario para modificar la información del usuario.</h2>
            </div>
        </div>
    </section>

    <section class="section">
        <div class="container">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="has-text-danger" />

            <div class="field">
                <label class="label">Nombre</label>
                <div class="control">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input" Placeholder="Nombre" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="field">
                <label class="label">Usuario</label>
                <div class="control">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="input" Placeholder="Usuario" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="field">
                <label class="label">Clave</label>
                <div class="control">
                    <asp:TextBox ID="txtClave" runat="server" CssClass="input" TextMode="Password" Placeholder="Clave" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="field">
                <label class="label">Rol</label>
                <div class="control">
                    <div class="select">
                        <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
                            <asp:ListItem Value="1">Administrador</asp:ListItem>
                            <asp:ListItem Value="2">Empleado</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="field" id="empleadoField">
                <label class="label">ID de Empleado (si aplica)</label>
                <div class="control">
                    <div class="select">
                        <asp:DropDownList CssClass="input" ID="ddlIDEmpleado" runat="server"
                            Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlIDEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="field">
                <div class="control">
                    <asp:Button ID="btnModificar" runat="server" CssClass="button custom-button" Text="Modificar Usuario" OnClick="btnModificar_Click" />
                </div>
            </div>

            <div class="field">
                <div class="control">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="has-text-danger"></asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
