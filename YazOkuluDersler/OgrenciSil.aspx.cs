using System;
using System.Web.UI;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class OgrenciSil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(Request.QueryString["OGRID"]);
            Response.Write(x);
            EntityOgrenci ent = new EntityOgrenci();
            ent.ID = x;
            BLLOgrenci.OgrenciSilBll(ent.ID);
            Response.Redirect("OgrenciListesi.aspx");
        }
    }
}