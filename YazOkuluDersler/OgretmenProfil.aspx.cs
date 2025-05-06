using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgretmenProfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true); return;
            }
            if ((int)Session["KullaniciTipi"] != 1)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return;
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
                int ogretmenId = Convert.ToInt32(Session["KullaniciID"]);
                List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(ogretmenId);

                if (ogrtList != null && ogrtList.Count > 0)
                {
                    EntityOgretmen ogrt = ogrtList[0];
                    lblAdSoyad.Text = $"{ogrt.OGRTAD} {ogrt.OGRTSOYAD}";
                    lblOgretmenID.Text = ogrt.OGRTID.ToString();

                    if (!string.IsNullOrEmpty(ogrt.OGRTFOTOGRAF))
                    {
                        imgProfilFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/" + ogrt.OGRTFOTOGRAF);
                    }
                    else
                    {
                        imgProfilFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/default_teacher.png");
                    }
                }
                else
                {
                    lblAdSoyad.Text = "Profil bilgisi bulunamadı.";
                    lblOgretmenID.Text = "-";
                    btnDuzenle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblAdSoyad.Text = "Bilgiler yüklenirken hata oluştu.";
                lblOgretmenID.Text = "-";
                btnDuzenle.Visible = false;
                Console.WriteLine("OgretmenProfil ProfilBilgileriniYukle Hata: " + ex.Message);
            }
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] != null)
            {
                string ogretmenId = Session["KullaniciID"].ToString();
                Response.Redirect($"~/OgretmenProfilDuzenle.aspx?OGRTID={ogretmenId}"); // OGRTID kullandık
            }
            else
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok");
            }
        }
    }
}