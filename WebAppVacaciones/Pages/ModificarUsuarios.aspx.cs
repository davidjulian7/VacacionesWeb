using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebAppVacaciones.Pages
{
    public partial class Index : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            // Evitar caché en la página
            Response.AppendHeader("Cache-Control", "no-store");

            // Cargar los datos y permisos si es la primera carga de la página
            if (!IsPostBack && Session["usuario"] != null)
            {
                int id_rol = Convert.ToInt32(Session["id_rol"]);
                CargarDatos();
                AsignarPermisos(id_rol);
            }
        }

        // Método para cargar los datos
        private void CargarDatos()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_datos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datos.DataSource = dt;
                    datos.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: podrías agregar una notificación para el usuario
                MostrarNotificacion("Error al cargar los datos: " + ex.Message, "error");
            }
            finally
            {
                con.Close();
            }
        }

        // Método para asignar permisos basado en el rol
        private void AsignarPermisos(int id_rol)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_permisos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_rol", id_rol);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool canCreate = false, canRead = false, canUpdate = false, canDelete = false;

                        while (reader.Read())
                        {
                            switch (reader[0].ToString())
                            {
                                case "Create":
                                    canCreate = Convert.ToBoolean(reader[1]);
                                    break;
                                case "Read":
                                    canRead = Convert.ToBoolean(reader[1]);
                                    break;
                                case "Update":
                                    canUpdate = Convert.ToBoolean(reader[1]);
                                    break;
                                case "Delete":
                                    canDelete = Convert.ToBoolean(reader[1]);
                                    break;
                            }
                        }

                        // Aplicar permisos a los botones globales
                        btncreate.Visible = canCreate;

                        // Guardar permisos en la sesión para usar en el evento RowDataBound
                        Session["canRead"] = canRead;
                        Session["canUpdate"] = canUpdate;
                        Session["canDelete"] = canDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al asignar permisos: " + ex.Message, "error");
            }
            finally
            {
                con.Close();
            }
        }

        // Evento RowDataBound para aplicar permisos por fila
        protected void datos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnRead = (Button)e.Row.FindControl("btnread");
                Button btnUpdate = (Button)e.Row.FindControl("btnupdate");
                Button btnDelete = (Button)e.Row.FindControl("btndelete");

                // Aplicar permisos por fila
                btnRead.Visible = (bool)Session["canRead"];
                btnUpdate.Visible = (bool)Session["canUpdate"];
                btnDelete.Visible = (bool)Session["canDelete"];
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
