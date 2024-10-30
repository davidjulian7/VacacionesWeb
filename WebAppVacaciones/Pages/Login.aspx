<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppVacaciones.IniciarSesion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@1.0.2/css/bulma.min.css" />
    <title>Iniciar Sesión</title>

    <style>
        .full-height {
            height: 100vh; /* Altura completa de la ventana */
        }

        .centered {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
        }
            
        .password-container {
            position: relative;
        }

        .toggle-password {
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
        }

        /* Agrega esta clase para el enfoque del input */
        .input:focus {
            border-color: #08a0de; /* Cambia el color del borde al enfoque */
            box-shadow: 0 0 0 2px rgba(8, 160, 222, 0.2); /* Agrega un efecto de sombra para mayor visibilidad */
            outline: none; /* Elimina el contorno por defecto */
        }
    </style>

    <script type="text/javascript">
        function togglePassword() {
            var passwordField = document.getElementById('<%= clave.ClientID %>');
            var toggleIcon = document.getElementById('togglePasswordIcon');
            if (passwordField.type === "password") {
                passwordField.type = "text";
                toggleIcon.classList.remove('fa-eye');
                toggleIcon.classList.add('fa-eye-slash');
            } else {
                passwordField.type = "password";
                toggleIcon.classList.remove('fa-eye-slash');
                toggleIcon.classList.add('fa-eye');
            }
        }
    </script>

    <!-- Incluye FontAwesome para el ícono del ojo -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container full-height centered">
            <div class="box" style="max-width: 400px; width: 100%;">
                <div class="has-text-centered">
                    <img src="../Img/Logo.png" alt="Logo" class="img-fluid mb-4" style="max-width: 200px;" />
                    <p class="small-text">Sistema Control de Vacaciones</p>
                </div>

                <br />
                <br />
                <h1 class="title has-text-centered has-text-weight-normal">Iniciar Sesión</h1>

                <div class="field">
                    <label class="label">Usuario</label>
                    <div class="control">
                        <asp:TextBox runat="server" ID="usuario" class="input" type="text" placeholder="ej. master"></asp:TextBox>
                    </div>
                </div>

                <div class="field password-container">
                    <label class="label">Contraseña</label>
                    <div class="control">
                        <asp:TextBox runat="server" ID="clave" class="input" type="password" placeholder="ej. ****"></asp:TextBox>
                        <!-- Icono de ojo para mostrar/ocultar contraseña -->
                        <i id="togglePasswordIcon" class="fas fa-eye toggle-password" onclick="togglePassword()"></i>
                    </div>
                </div>

                <div class="has-text-centered">
                    <asp:Button runat="server" ID="ingresar" class="button" Text="Ingresar" OnClick="ingresar_Click" Style="background-color: #08a0de; color: white;" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
