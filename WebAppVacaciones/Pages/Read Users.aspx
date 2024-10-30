<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Read Users.aspx.cs" Inherits="WebAppVacaciones.Pages.ConsultarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Consulta de Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .custom-button:hover {
            background-color: #08a0de; /* Color de fondo al pasar el mouse */
        }

        .centered-container {
            display: flex;
            justify-content: center; /* Centrar horizontalmente */
        }

        .container-custom {
            max-width: 1200px; /* Ajustar el tamaño máximo del contenedor */
            width: 100%; /* Asegurar que el contenedor ocupe el 100% del ancho disponible */
        }

        .notification {
            position: fixed;
            top: 10px;
            right: 10px;
            z-index: 1000;
            max-width: 300px;
        }
    </style>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.4/css/bulma.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script> <!-- Biblioteca para notificaciones -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <section class="hero custom-hero">
        <div class="hero-body">
            <div class="container">
                <h1 class="title">Consulta de Usuarios</h1>
                <br />
                <h2 class="subtitle">Aquí puedes consultar, modificar o eliminar usuarios.</h2>
            </div>
        </div>
    </section>

    <section class="section">
        <div class="centered-container">
            <div class="container container-custom">
                <div class="field">
                    <label class="label">Buscar Usuario</label>
                    <div class="control">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="input" Placeholder="Buscar..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                    </div>
                </div>

                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table is-striped is-bordered is-hoverable">
                    <Columns>
                        <asp:BoundField DataField="Id_Usuario" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Id_Rol" HeaderText="Rol" />
                        <asp:BoundField DataField="ID_Empleado" HeaderText="ID Empleado" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a class="button is-info is-small custom-button" href="Update Users.aspx">Modificar</a>
                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="button is-danger is-small custom-button" Text="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("Id_Usuario") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>

    <!-- Contenedor para notificaciones -->
    <div id="notificationContainer"></div>
</asp:Content>
