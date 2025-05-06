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
                MesajGoster("��renci listesi y�klenirken hata olu�tu.", false);
                System.Diagnostics.Debug.WriteLine("OgrenciListesi Listeleme Hatas�: " + ex.ToString());
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
                        MesajGoster("��renci ba�ar�yla silindi.", true);
                        OgrencileriListele(); // Listeyi yenile
                    }
                    else
                    {
                        MesajGoster("��renci silinirken bir hata olu�tu veya silme i�lemi ger�ekle�tirilemedi (�rn: ili�kili kay�tlar var).", false);
                    }
                }
                catch (Exception ex)
                {
                    MesajGoster("��renci silinirken beklenmedik bir hata olu�tu.", false);
                    System.Diagnostics.Debug.WriteLine("��renci Silme Hatas�: " + ex.ToString());
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