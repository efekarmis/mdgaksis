using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using BusinessLogicLayer;
using System.Web.UI.HtmlControls;

namespace YazOkuluDersler
{
    public partial class DersListesi : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.Repeater Repeater;
        protected global::System.Web.UI.WebControls.HyperLink hlDersEkle;
        protected global::System.Web.UI.WebControls.PlaceHolder phMesaj;
        protected global::System.Web.UI.WebControls.Literal litMesaj;
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl divMesaj;
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
                DersleriListele();
            }
        }

        private void ShowSessionMessage()
        {
            if (phMesaj == null) return;

            if (Session["KayitMesaj"] != null)
            {
                MesajGoster(Session["KayitMesaj"].ToString(), true);
                Session.Remove("KayitMesaj");
            }
            else if (Session["GuncellemeMesaj"] != null)
            {
                MesajGoster(Session["GuncellemeMesaj"].ToString(), true);
                Session.Remove("GuncellemeMesaj");
            }
            else
            {
                phMesaj.Visible = false;
            }
        }

        private void DersleriListele()
        {
            try
            {
                List<EntityDers> drsList = BLLDers.BllListele();
                Repeater.DataSource = drsList;
                Repeater.DataBind();
            }
            catch (Exception ex)
            {
                MesajGoster("Ders listesi y�klenirken hata olu�tu.", false);
                System.Diagnostics.Debug.WriteLine("DersListesi DersleriListele Hata: " + ex.ToString());
            }
        }

        protected void Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                try
                {
                    int dersId = Convert.ToInt32(e.CommandArgument);
                    bool sonuc = BLLDers.DersSilBll(dersId);

                    if (sonuc)
                    {
                        MesajGoster("Ders ba�ar�yla silindi.", true);
                        DersleriListele();
                    }
                    else
                    {
                        MesajGoster("Ders silinirken bir hata olu�tu veya silme i�lemi ger�ekle�tirilemedi (�rn: derse kay�tl� ��renci var).", false);
                    }
                }
                catch (Exception ex)
                {
                    MesajGoster("Ders silinirken beklenmedik bir hata olu�tu.", false);
                    System.Diagnostics.Debug.WriteLine("Ders Silme Hatas�: " + ex.ToString());
                }
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