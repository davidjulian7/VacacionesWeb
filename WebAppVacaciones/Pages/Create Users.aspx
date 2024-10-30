<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Create Users.aspx.cs" Inherits="WebAppVacaciones.Pages.Registro_de_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Registro de Usuario
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
                <h1 class="title">Registro de Usuario</h1>
                <br />
                <h2 class="subtitle">Completa el formulario para registrar un nuevo usuario.</h2>
            </div>
        </div>
    </section>

    <section class="section">
        <div class="container">
            <div class="field">
                <label class="label">Nombre Completo</label>
                <div class="control">
                    <asp:TextBox CssClass="input" ID="txtNombre" runat="server" Placeholder="Nombre completo" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="field">
                <label class="label">Nombre de Usuario</label>
                <div class="control">
                    <asp:TextBox CssClass="input" ID="txtUsuario" runat="server" Placeholder="Nombre de usuario" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="field">
                <label class="label">Contraseña</label>
                <div class="control">
                    <asp:TextBox CssClass="input" ID="txtClave" runat="server" TextMode="Password" Placeholder="Contraseña" Required="true"></asp:TextBox>
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
                    <asp:Button CssClass="button custom-button" ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
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
