using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.HtmlControls; // Panel için

namespace YazOkulu
{
    public partial class OgretmenDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null)
            {
                ShowError("Bu sayfayı görüntülemek için giriş yapmalısınız.");
                return;
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["OgretmenID"] != null && int.TryParse(Request.QueryString["OgretmenID"], out int ogretmenId))
                {
                    LoadOgretmenDetay(ogretmenId);
                }
                else
                {
                    ShowError("Geçersiz veya eksik öğretmen ID.");
                }
            }
        }

        private void LoadOgretmenDetay(int ogretmenId)
        {
            try
            {
                List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(ogretmenId);
                if (ogrtList == null || ogrtList.Count == 0)
                {
                    ShowError("Belirtilen öğretmen bulunamadı.");
                    return;
                }
                EntityOgretmen ogretmen = ogrtList[0];

                litOgretmenAdSoyad.Text = ogretmen.OGRTADSOYAD;
                if (!string.IsNullOrEmpty(ogretmen.OGRTFOTOGRAF))
                {
                    imgOgretmenFoto.ImageUrl = ResolveUrl($"~/OgretmenFotograflari/{ogretmen.OGRTFOTOGRAF}");
                }
                else
                {
                    imgOgretmenFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/default_teacher.png"); // Varsayılan öğretmen fotosu
                }

                List<EntityDers> verilenDersler = BLLDers.GetDerslerByOgretmenIdBll(ogretmenId);

                rptVerilenDersler.DataSource = verilenDersler;
                rptVerilenDersler.DataBind();

                pnlDetay.Visible = true;
                phHata.Visible = false;

            }
            catch (Exception ex)
            {
                ShowError("Öğretmen bilgileri yüklenirken bir hata oluştu.");
                Console.WriteLine($"OgretmenDetay LoadOgretmenDetay Hata (ID: {ogretmenId}): " + ex.ToString());
            }
        }

        private void ShowError(string message)
        {
            pnlDetay.Visible = false;
            litHata.Text = message;
            phHata.Visible = true;
        }
    }
}