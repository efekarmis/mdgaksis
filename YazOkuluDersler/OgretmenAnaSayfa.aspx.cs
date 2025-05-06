using System;
using System.Collections.Generic;
using System.Web.UI;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class OgretmenAnaSayfa : Page
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
                try
                {
                    int ogretmenId = Convert.ToInt32(Session["KullaniciID"]);
                    List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(ogretmenId); // Öğretmen detayını çek

                    if (ogrtList != null && ogrtList.Count > 0)
                    {
                        EntityOgretmen ogretmen = ogrtList[0];

                        string profileHTML = $@"
                            <h4>{ogretmen.OGRTAD} {ogretmen.OGRTSOYAD}</h4>
                            <p><strong>Öğretmen ID:</strong> {ogretmen.OGRTID}</p>
                        ";
                        profileDetails.InnerHtml = profileHTML;

                        string fotografDosyaAdi = ogretmen.OGRTFOTOGRAF;
                        if (!string.IsNullOrEmpty(fotografDosyaAdi))
                        {
                            string imageUrl = "~/OgretmenFotograflari/" + fotografDosyaAdi;
                            imgOgretmenFoto.ImageUrl = ResolveUrl(imageUrl);
                        }
                        else
                        {
                            imgOgretmenFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/default_teacher.png");
                        }
                    }
                    else
                    {
                        profileDetails.InnerHtml = "<p>Profil bilgileri bulunamadı.</p>";
                    }
                }
                catch (Exception ex)
                {
                    profileDetails.InnerHtml = "<p>Profil bilgileri yüklenirken bir hata oluştu.</p>";
                    Console.WriteLine("OgretmenAnaSayfa Page_Load Hata: " + ex.Message);
                }

                LoadDuyurular();
            }
        }

        private void LoadDuyurular()
        {
            try
            {
                int kullaniciTipi = Session["KullaniciTipi"] != null ? Convert.ToInt32(Session["KullaniciTipi"]) : 0;

                List<EntityDuyuru> duyurular = BLLDuyuru.AktifDuyurulariGetirBLL(kullaniciTipi);

                if (rptDuyurular != null)
                {
                    rptDuyurular.DataSource = duyurular;
                    rptDuyurular.DataBind();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("HATA: rptDuyurular kontrolü sayfada bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Duyuru Paneli Yükleme Hatası: " + ex.ToString());
            }
        }

        protected string LimitContent(object content, int maxLength)
        {
            string text = content?.ToString() ?? "";
            if (text.Length <= maxLength)
            {
                return Server.HtmlEncode(text).Replace("\n", "<br />");
            }
            else
            {
                int lastSpace = text.LastIndexOf(' ', maxLength);
                string shortText = text.Substring(0, (lastSpace > 0) ? lastSpace : maxLength);
                return Server.HtmlEncode(shortText).Replace("\n", "<br />") + "...";
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
    }
}