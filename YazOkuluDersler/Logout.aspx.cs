using System;
using System.Web; // HttpCookie için eklendi
using System.Web.UI;

namespace YazOkulu
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                HttpCookie sessionCookie = new HttpCookie("ASP.NET_SessionId");
                sessionCookie.Value = string.Empty; // Değeri boşalt
                sessionCookie.Expires = DateTime.Now.AddYears(-1); // Geçmiş bir tarih ver
                Response.Cookies.Add(sessionCookie);
            }



            Response.Redirect("Login.aspx", true);
        }
    }
}