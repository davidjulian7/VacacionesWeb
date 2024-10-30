using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppVacaciones.Pages
{
    public partial class MP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                // Redirigir al login si no hay sesión activa
                Response.Redirect("Login.aspx");
            }
            Response.AppendHeader("Cache-Control", "no-store");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Eliminar toda la información de sesión
            Session.Clear();
            Session.Abandon();

            // Opcional: Eliminar cookies de autenticación si las estás utilizando
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            }

            // Redirigir a la página de login
            Response.Redirect("Login.aspx");
        }



    }
}