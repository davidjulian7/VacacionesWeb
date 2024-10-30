using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppVacaciones.Pages
{
    public partial class Registro_de_Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización al cargar la página
                ddlRol_SelectedIndexChanged(null, EventArgs.Empty);
                CargarEmpleadosSinUsuario(); // Cargar empleados al iniciar
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Habilita o deshabilita el campo ID de Empleado basado en la selección
            if (ddlRol.SelectedValue == "1") // Administrador
            {
                ddlIDEmpleado.Enabled = false;
                ddlIDEmpleado.Items.Clear(); // Limpiar si es necesario
                txtNombre.Text = string.Empty; // Limpiar el campo Nombre Completo
                CargarEmpleadosSinUsuario(); // Cargar empleados al iniciar
            }
            else if (ddlRol.SelectedValue == "2") // Empleado
            {
                ddlIDEmpleado.Enabled = true;
                CargarEmpleadosSinUsuario(); // Cargar empleados cuando se selecciona "Empleado"
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

        protected void ddlIDEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Transcribir el nombre del empleado al TextBox de Nombre Completo
            if (ddlIDEmpleado.SelectedValue != "0")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Empleado WHERE ID_Empleado = @ID_Empleado", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Empleado", ddlIDEmpleado.SelectedValue);

                        try
                        {
                            conn.Open();
                            string nombre = cmd.ExecuteScalar()?.ToString();

                            if (!string.IsNullOrEmpty(nombre))
                            {
                                txtNombre.Text = nombre; // Asigna el nombre al TextBox
                            }
                        }
                        catch (Exception ex)
                        {
                            // Manejar el error
                            ScriptManager.RegisterStartupScript(this, GetType(), "errorCargarNombre", $"alert('Error al cargar el nombre: {ex.Message}');", true);
                        }
                    }
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_registrar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@Clave", txtClave.Text);
                        cmd.Parameters.AddWithValue("@Id_rol", ddlRol.SelectedValue);
                        cmd.Parameters.AddWithValue("@Patron", "VacacionesGNTTel");

                        if (ddlRol.SelectedValue == "2" && ddlIDEmpleado.SelectedValue != "0")
                        {
                            cmd.Parameters.AddWithValue("@ID_Empleado", ddlIDEmpleado.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ID_Empleado", DBNull.Value);
                        }

                        // Parámetro de salida para obtener el resultado del procedimiento almacenado
                        SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int);
                        resultadoParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(resultadoParam);

                        cmd.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(resultadoParam.Value);

                        if (resultado == 1)
                        {
                            // Usuario ya existe
                            ScriptManager.RegisterStartupScript(this, GetType(), "usuarioExiste", "alert('El nombre de usuario ya existe.');", true);
                        }
                        else
                        {
                            // Usuario registrado con éxito
                            ScriptManager.RegisterStartupScript(this, GetType(), "registroExitoso", "alert('Usuario registrado con éxito');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "registroError", $"alert('Error al registrar el usuario: {ex.Message}');", true);
                }
            }
        }
    }
}
 