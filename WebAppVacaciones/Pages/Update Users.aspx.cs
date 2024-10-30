using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppVacaciones.Pages
{
    public partial class Update_Users : System.Web.UI.Page
    {
        private string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización al cargar la página
                ddlRol_SelectedIndexChanged(null, EventArgs.Empty);
                CargarEmpleadosSinUsuario(); // Cargar empleados al iniciar
            }
        }
    }



        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtiene el valor seleccionado del DropDownList
            string selectedValue = ddlRol.SelectedValue;

            // Habilita o deshabilita el campo ID de Empleado basado en la selección
            if (selectedValue == "1") // Administrador
            {
                ddlIDEmpleado.Enabled = false;
                ddlIDEmpleado.Text = string.Empty; // Limpiar el valor si es necesario
            }
            else if (selectedValue == "2") // Empleado
            {
                ddlIDEmpleado.Enabled = true;
            }
        }

        private void CargarDatosUsuario(int idUsuario)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Nombre, Usuario, Id_Rol, ID_Empleado FROM Usuarios WHERE Id_Usuario = @Id_Usuario";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtUsuario.Text = reader["Usuario"].ToString();
                    ddlRol.SelectedValue = reader["Id_Rol"].ToString();
                    ddlIDEmpleado.Text = reader["ID_Empleado"] != DBNull.Value ? reader["ID_Empleado"].ToString() : "";
                    ddlIDEmpleado.Enabled = ddlRol.SelectedValue == "2";
                }

                conn.Close();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int idUsuario = int.Parse(Request.QueryString["id"]);
                ModificarUsuarioEnDB(idUsuario);
            }
        }

        private void ModificarUsuarioEnDB(int idUsuario)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_modificar_usuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@Clave", !string.IsNullOrEmpty(txtClave.Text) ? txtClave.Text : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Rol", int.Parse(ddlRol.SelectedValue));

                if (ddlRol.SelectedValue == "2" && !string.IsNullOrEmpty(ddlIDEmpleado.Text))
                {
                    cmd.Parameters.AddWithValue("@ID_Empleado", int.Parse(ddlIDEmpleado.Text));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ID_Empleado", DBNull.Value);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                // Redireccionar de vuelta a la página de consulta de usuarios
                Response.Redirect("ConsultarUsuarios.aspx");
            }
        }



        private void CargarEmpleadosSinUsuario()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EmpleadosSinUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlIDEmpleado.DataSource = reader;
                        ddlIDEmpleado.DataTextField = "Nombre";  // Nombre del empleado
                        ddlIDEmpleado.DataValueField = "ID_Empleado"; // ID del empleado
                        ddlIDEmpleado.DataBind();

                        // Añadir un elemento predeterminado
                        ddlIDEmpleado.Items.Insert(0, new ListItem("Seleccione un Empleado", "0"));
                    }
                    catch (Exception ex)
                    {
                        // Manejar el error
                        ScriptManager.RegisterStartupScript(this, GetType(), "errorCargaEmpleados", $"alert('Error al cargar empleados: {ex.Message}');", true);
                    }
                }
            }
        }



    }
}