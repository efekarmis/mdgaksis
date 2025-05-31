using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class DuyuruEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null) { Response.Redirect("~/Login.aspx?Error=OturumYok", true); return; }
            if ((int)Session["KullaniciTipi"] != 1) { Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return; }

            lblMesaj.Text = "";
            lblMesaj.CssClass = "";
        }

        protected void btnDuyuruEkle_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                EntityDuyuru yeniDuyuru = new EntityDuyuru();
                yeniDuyuru.Baslik = txtBaslik.Text.Trim();
                yeniDuyuru.Icerik = txtIcerik.Text.Trim();
                yeniDuyuru.HedefKitle = Convert.ToInt32(ddlHedefKitle.SelectedValue);
                yeniDuyuru.OnemDerecesi = rbOnemli.Checked ? (short)1 : (short)0;

                int sonuc = BLLDuyuru.DuyuruEkleBLL(yeniDuyuru);

                if (sonuc > 0)
                {
                    Session["DuyuruMesaj"] = "Duyuru başarıyla eklendi.";
                    Session["DuyuruMesajTur"] = true;
                    Response.Redirect("DuyuruYonetimi.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else if (sonuc == -2)
                {
                    MesajGoster("Duyuru eklenemedi. Başlık ve İçerik boş olamaz.", false);
                }
                else
                {
                    MesajGoster("Duyuru eklenirken bir veritabanı hatası oluştu.", false);
                }
            }
            catch (Exception ex)
            {
                MesajGoster("Duyuru eklenirken beklenmedik bir hata oluştu.", false);
                System.Diagnostics.Debug.WriteLine("Duyuru Ekleme Hatası: " + ex.ToString());
            }
        }

        private void MesajGoster(string mesaj, bool basariliMi)
        {
            lblMesaj.Text = mesaj;
            lblMesaj.CssClass = basariliMi ? "text-success" : "text-danger";
        }
    }
}