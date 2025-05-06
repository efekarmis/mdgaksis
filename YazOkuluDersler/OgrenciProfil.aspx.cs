using System;
using System.Collections.Generic;
using System.Globalization; // CultureInfo için
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgrenciProfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
                return;
            }

            if ((int)Session["KullaniciTipi"] != 0)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
                return;
            }

            if (!IsPostBack)
            {
                ProfilBilgileriniYukle();
            }
        }

        private void ProfilBilgileriniYukle()
        {
            try
            {
                int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
                List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(ogrenciId);

                if (ogrList != null && ogrList.Count > 0)
                {
                    EntityOgrenci ogr = ogrList[0];

                    lblAdSoyad.Text = $"{ogr.AD} {ogr.SOYAD}"; // String interpolation
                    lblNumara.Text = ogr.NUMARA;
                    lblBakiye.Text = ogr.BAKIYE.ToString("C", CultureInfo.GetCultureInfo("tr-TR"));

                    if (!string.IsNullOrEmpty(ogr.FOTOGRAF))
                    {
                        imgProfilFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/" + ogr.FOTOGRAF);
                    }
                    else
                    {
                        imgProfilFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/default.png");
                    }
                }
                else
                {
                    lblAdSoyad.Text = "Profil bilgisi bulunamadı.";
                    lblNumara.Text = "-";
                    lblBakiye.Text = "-";
                    btnDuzenle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblAdSoyad.Text = "Bilgiler yüklenirken hata oluştu.";
                lblNumara.Text = "-";
                lblBakiye.Text = "-";
                btnDuzenle.Visible = false;
                Console.WriteLine("OgrenciProfil ProfilBilgileriniYukle Hata: " + ex.Message);
            }
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] != null)
            {
                string ogrenciId = Session["KullaniciID"].ToString();
                Response.Redirect($"~/OgrenciProfilDuzenle.aspx?OGRID={ogrenciId}");
            }
            else
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok");
            }
        }
    }
}