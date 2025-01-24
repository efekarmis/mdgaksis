using System;
using System.Web.UI;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkuluDersler
{
    public partial class KontenjanSil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Write(x);
            EntityDersler ent = new EntityDersler();
            ent.ID = x;
            BLLDersler.DersSilBll(ent.ID);
            Response.Redirect("Dersler.aspx");
        }
    }
}