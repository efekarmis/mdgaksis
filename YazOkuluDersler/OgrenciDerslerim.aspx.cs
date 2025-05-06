using System;
using System.Collections.Generic;
using System.Web.UI;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgrenciDerslerim : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
            }

            if ((int)Session["KullaniciTipi"] != 0)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
            }

            if (!IsPostBack)
            {
                if (Session["KullaniciID"] != null)
                {
                    int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

                    List<EntityDersKayit> dersListesi = BLLOgrenci.OgrenciOnayliDersListesiBll(ogrenciId);

                    RepeaterDerslerim.DataSource = dersListesi;
                    RepeaterDerslerim.DataBind();
                }
                else
                {
                    Response.Redirect("StudentLogin.aspx");
                }
            }
        }
    }
}