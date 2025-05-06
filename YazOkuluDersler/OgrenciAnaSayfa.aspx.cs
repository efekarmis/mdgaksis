using System;
using System.Collections.Generic;
using System.Web.UI;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.WebControls;

namespace YazOkulu
{
    public partial class OgrenciAnaSayfa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("Login.aspx?Error=OturumYok", true);
                return;
            }
            if ((int)Session["KullaniciTipi"] != 0)
            {
                Response.Redirect("Login.aspx?Error=Yetkisiz", true);
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

                    List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(ogrenciId);

                    if (ogrList != null && ogrList.Count > 0)
                    {
                        EntityOgrenci ogrenci = ogrList[0];

                        string profileHTML = $@"
                            <h4>{ogrenci.AD} {ogrenci.SOYAD}</h4>
                            <p><strong>Öğrenci No:</strong> {ogrenci.NUMARA}</p>
                            <p><strong>Bakiye:</strong> {ogrenci.BAKIYE:C2}</p>
                        ";
                        profileDetails.InnerHtml = profileHTML;

                        string fotografDosyaAdi = ogrenci.FOTOGRAF;
                        if (!string.IsNullOrEmpty(fotografDosyaAdi))
                        {
                            string imageUrl = "~/OgrenciFotograflari/" + fotografDosyaAdi;
                            imgOgrenciFoto.ImageUrl = ResolveUrl(imageUrl);
                        }
                        else
                        {
                            imgOgrenciFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/default.png");
                        }

                        hlProfilDuzenle.NavigateUrl = $"~/OgrenciGuncelle.aspx?OGRID={ogrenciId}";
                    }
                    else
                    {
                        profileDetails.InnerHtml = "<p>Profil bilgileri bulunamadı.</p>";
                        hlProfilDuzenle.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    profileDetails.InnerHtml = "<p>Profil bilgileri yüklenirken bir hata oluştu.</p>";
                    hlProfilDuzenle.Visible = false;
                    Console.WriteLine("OgrenciAnaSayfa Page_Load Hata: " + ex.Message);
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