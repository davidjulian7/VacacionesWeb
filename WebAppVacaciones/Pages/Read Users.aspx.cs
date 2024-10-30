using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppVacaciones.Pages
{
    public partial class ConsultarUsuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        // Método para cargar la lista de usuarios
        private void CargarUsuarios(string filtro = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Usuarios";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += " WHERE Nombre LIKE @Filtro OR Usuario LIKE @Filtro";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(filtro))
                        {
                            cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvUsuarios.DataSource = dt;
                        gvUsuarios.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    MostrarNotificacion("Error al cargar los usuarios: " + ex.Message, "error");
                }
            }
        }

        // Método para manejar el evento de cambio de texto en el TextBox de búsqueda
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtSearch.Text.Trim();
            CargarUsuarios(filtro);
        }

        // Método para eliminar un usuario
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string idUsuario = (sender as LinkButton).CommandArgument;

            // Mostrar confirmación antes de eliminar
            ScriptManager.RegisterStartupScript(this, GetType(), "confirmDelete",
                "Swal.fire({ title: '¿Estás seguro?', text: '¡No podrás recuperar este usuario!', icon: 'warning', showCancelButton: true, confirmButtonColor: '#3085d6', cancelButtonColor: '#d33', confirmButtonText: 'Sí, eliminarlo' }).then((result) => { if (result.isConfirmed) { __doPostBack('btnEliminar', '" + idUsuario + "'); } });", true);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "btnEliminar")
            {
                string idUsuario = Request.Form["__EVENTARGUMENT"];
                EliminarUsuario(idUsuario);
            }
        }

        // Método para eliminar un usuario
        private void EliminarUsuario(string idUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE Id_Usuario = @Id_Usuario", con))
                    {
                        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MostrarNotificacion("Usuario eliminado correctamente.", "success");
                            CargarUsuarios(); // Recargar la lista de usuarios
                        }
                        else
                        {
                            MostrarNotificacion("No se encontró el usuario para eliminar.", "warning");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MostrarNotificacion("Error al eliminar el usuario: " + ex.Message, "error");
                }
            }
        }

        // Método para mostrar notificaciones
        private void MostrarNotificacion(string mensaje, string tipo)
        {
            string script = $"Swal.fire({{ text: '{mensaje}', icon: '{tipo}', timer: 3000, showConfirmButton: false }});";
            ScriptManager.RegisterStartupScript(this, GetType(), "showNotification", script, true);
        }
    }
}
