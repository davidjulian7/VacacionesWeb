using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppVacaciones
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        string Patron = "VacacionesGNTTel";

        protected void ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario.Text;
                cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = clave.Text;
                cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    Session["Id_rol"] = rd[4].ToString();
                    Session["usuario"] = rd[2].ToString();
                    Session["Nombre"] = rd[1].ToString();

                    // Redirigir según el rol del usuario
                    if (Session["Id_rol"].ToString() == "1")
                    {
                        Response.Redirect("Bienvenido.aspx");
                    }
                    else if (Session["Id_rol"].ToString() == "2")
                    {
                        Response.Redirect("ConsultaEmpleado.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rol no reconocido');", true);
                    }
                }
                else
                {
                    // Mostrar la pantalla emergente
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Usuario o contraseña incorrectos');", true);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // En caso de error, cerrar conexión y mostrar el mensaje
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }


    }
}