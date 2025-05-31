// DuyuruYonetimi.aspx.cs
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class DuyuruYonetimi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null) { Response.Redirect("~/Login.aspx?Error=OturumYok", true); return; }
            if ((int)Session["KullaniciTipi"] != 1) { Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return; }

            ShowSessionMessage();

            if (!IsPostBack)
            {
                TumDuyurulariListele();
            }
        }

        private void ShowSessionMessage()
        {
            if (Session["DuyuruMesaj"] != null && Session["DuyuruMesajTur"] != null)
            {
                MesajGoster(Session["DuyuruMesaj"].ToString(), (bool)Session["DuyuruMesajTur"]);
                Session.Remove("DuyuruMesaj");
                Session.Remove("DuyuruMesajTur");
            }
            else
            {
                phMesaj.Visible = false;
            }
        }

        private void TumDuyurulariListele()
        {
            try
            {
                List<EntityDuyuru> duyurular = BLLDuyuru.TumDuyurulariGetirBLL();
                rptTumDuyurular.DataSource = duyurular;
                rptTumDuyurular.DataBind();
            }
            catch (Exception ex)
            {
                MesajGoster("Duyurular listelenirken hata oluştu.", false);
                System.Diagnostics.Debug.WriteLine("Duyuru Yönetimi Listeleme Hatası: " + ex.ToString());
            }
        }

        protected void rptTumDuyurular_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                try
                {
                    int duyuruId = Convert.ToInt32(e.CommandArgument);
                    bool sonuc = BLLDuyuru.DuyuruSilBLL(duyuruId);
                    if (sonuc)
                    {
                        MesajGoster("Duyuru başarıyla silindi.", true);
                        TumDuyurulariListele();
                    }
                    else
                    {
                        MesajGoster("Duyuru silinirken bir hata oluştu veya duyuru bulunamadı.", false);
                    }
                }
                catch (Exception ex)
                {
                    MesajGoster("Duyuru silinirken beklenmedik bir hata oluştu.", false);
                    System.Diagnostics.Debug.WriteLine("Duyuru Silme Hatası: " + ex.ToString());
                }
            }
        }

        protected string GetHedefKitleText(object hedefKitleObj)
        {
            int hedefKitle = Convert.ToInt32(hedefKitleObj);
            switch (hedefKitle)
            {
                case 0: return "Öğrenci";
                case 1: return "Öğretmen";
                case 2: return "Herkes";
                default: return "Bilinmeyen";
            }
        }

        private void MesajGoster(string mesaj, bool basariliMi)
        {
            litMesaj.Text = mesaj;
            divMesaj.Attributes["class"] = basariliMi ? "alert alert-success alert-dismissible" : "alert alert-danger alert-dismissible";
            phMesaj.Visible = true;
        }
    }
}