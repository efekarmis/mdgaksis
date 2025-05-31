using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls; // Repeater ve diğer kontroller için
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgretmenDerslerim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
                return; // Yönlendirme sonrası kodun devam etmemesi için
            }
            if ((int)Session["KullaniciTipi"] != 1) // Sadece Öğretmen
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
                return;
            }

            if (!IsPostBack)
            {
                LoadOgretmenDersleri();
            }
        }

        private void LoadOgretmenDersleri()
        {
            try
            {
                int ogretmenId = Convert.ToInt32(Session["KullaniciID"]);
                List<EntityDers> dersler = BLLDers.GetDerslerByOgretmenIdBll(ogretmenId);

                rptOgretmenDersleri.DataSource = dersler;
                rptOgretmenDersleri.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("OgretmenDerslerim.LoadOgretmenDersleri Hata: " + ex.Message);
            }
        }

        protected void rptOgretmenDersleri_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    EntityDers currentDers = (EntityDers)e.Item.DataItem;
                    int dersId = currentDers.ID;

                    Repeater rptDersOgrencileri = (Repeater)e.Item.FindControl("rptDersOgrencileri");
                    PlaceHolder phOgrenciYok = (PlaceHolder)e.Item.FindControl("phOgrenciYok");

                    if (rptDersOgrencileri != null && phOgrenciYok != null) // İki kontrol de bulunduysa devam et
                    {
                        List<EntityOgrenci> ogrenciler = BLLOgrenci.GetOgrencilerByDersIdBll(dersId);

                        bool ogrenciVar = ogrenciler != null && ogrenciler.Count > 0;

                        rptDersOgrencileri.Visible = ogrenciVar; // Öğrenci varsa tabloyu/repeater'ı göster
                        phOgrenciYok.Visible = !ogrenciVar;    // Öğrenci yoksa mesajı göster

                        if (ogrenciVar)
                        {
                            rptDersOgrencileri.DataSource = ogrenciler;
                            rptDersOgrencileri.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"OgretmenDerslerim ItemDataBound Hata (DersID: {((EntityDers)e.Item.DataItem).ID}): " + ex.Message);

                    Repeater rpt = e.Item.FindControl("rptDersOgrencileri") as Repeater;
                    PlaceHolder ph = e.Item.FindControl("phOgrenciYok") as PlaceHolder;
                    if (rpt != null) rpt.Visible = false;
                    if (ph != null)
                    {
                        ph.Controls.Clear(); // Önceki içeriği temizle (varsa)
                        ph.Controls.Add(new LiteralControl("<p class='text-danger text-center mt-3'>Öğrenci listesi yüklenirken hata oluştu.</p>"));
                        ph.Visible = true;
                    }
                }
            }
        }

        protected void rptOgretmenDersleri_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}