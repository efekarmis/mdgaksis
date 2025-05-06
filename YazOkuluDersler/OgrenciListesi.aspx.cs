using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkulu
{
    public partial class OgrenciListesi : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
                return;
            }

            if ((int)Session["KullaniciTipi"] != 1)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
                return;
            }
            ShowSessionMessage();

            if (!IsPostBack)
            {
                OgrencileriListele();
            }
        }

        private void OgrencileriListele()
        {
            try
            {
                List<EntityOgrenci> ogrList = BLLOgrenci.BllListele();
                Repeater1.DataSource = ogrList;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                MesajGoster("Öðrenci listesi yüklenirken hata oluþtu.", false);
                System.Diagnostics.Debug.WriteLine("OgrenciListesi Listeleme Hatasý: " + ex.ToString());
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                try
                {
                    int ogrenciId = Convert.ToInt32(e.CommandArgument);
                    bool sonuc = BLLOgrenci.OgrenciSilBll(ogrenciId);

                    if (sonuc)
                    {
                        MesajGoster("Öðrenci baþarýyla silindi.", true);
                        OgrencileriListele(); // Listeyi yenile
                    }
                    else
                    {
                        MesajGoster("Öðrenci silinirken bir hata oluþtu veya silme iþlemi gerçekleþtirilemedi (örn: iliþkili kayýtlar var).", false);
                    }
                }
                catch (Exception ex)
                {
                    MesajGoster("Öðrenci silinirken beklenmedik bir hata oluþtu.", false);
                    System.Diagnostics.Debug.WriteLine("Öðrenci Silme Hatasý: " + ex.ToString());
                }
            }
        }


        private void ShowSessionMessage()
        {
            if (Session["OgrenciMesaj"] != null && Session["OgrenciMesajTur"] != null)
            {
                MesajGoster(Session["OgrenciMesaj"].ToString(), (bool)Session["OgrenciMesajTur"]);
                Session.Remove("OgrenciMesaj");
                Session.Remove("OgrenciMesajTur");
            }
            else
            {
                phMesaj.Visible = false;
            }
        }

        private void MesajGoster(string mesaj, bool basariliMi)
        {
            if (phMesaj == null || litMesaj == null || divMesaj == null) return;
            litMesaj.Text = mesaj;
            divMesaj.Attributes["class"] = basariliMi ? "alert alert-success alert-dismissible" : "alert alert-danger alert-dismissible";
            phMesaj.Visible = true;
        }
    }
}