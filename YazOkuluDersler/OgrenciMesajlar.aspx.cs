// OgrenciMesajlar.aspx.cs
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class OgrenciMesajlar : System.Web.UI.Page
    {
        // OgretmenMesajlasma.aspx.cs

        protected void Page_Load(object sender, EventArgs e)
        {
            // Erişim Kontrolü
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null) { Response.Redirect("~/Login.aspx?Error=OturumYok", true); return; }
            if ((int)Session["KullaniciTipi"] != 0) { Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return; }

            phMesaj.Visible = false; // Başlangıçta mesajı gizle

            if (!IsPostBack)
            {
                // 1. ÖNCE SOL MENÜYÜ DOLDUR
                OgrencininDersleriniDoldur();

                // 2. QueryString'i kontrol et
                int dersIdFromQuery = 0;
                if (Request.QueryString["DersID"] != null && int.TryParse(Request.QueryString["DersID"], out dersIdFromQuery))
                {
                    // 3. HiddenField'ı ayarla
                    hfSeciliDersId.Value = dersIdFromQuery.ToString();

                    // 5. Panelleri ayarla ve mesajları yükle
                    SetPanelVisibility(dersIdFromQuery);
                    if (dersIdFromQuery > 0) // Sadece geçerli bir ders ID'si varsa yükle
                    {
                        MesajlariYukle(dersIdFromQuery);
                        // Sol menüdeki aktif sınıfı güncellemek için tekrar bağlamaya gerek yok,
                        // çünkü DersListesiniDoldurSolMenu zaten çağrıldı ve SelectedValue ayarlandı.
                        // ASPX tarafındaki CssClass ataması yeterli olmalı.
                        // DersListesiniDoldurSolMenu(); // Bu satırı kaldırabiliriz.
                    }
                }
                else
                {
                    // QueryString yoksa, başlangıç durumu
                    SetPanelVisibility(0);
                    hfSeciliDersId.Value = "0";
                }
            }
            else // Postback ise
            {
                // Sadece panellerin görünürlüğünü ayarla (mevcut hfSeciliDersId değerine göre)
                SetPanelVisibility(Convert.ToInt32(hfSeciliDersId.Value));
                // Postback'te mesajları tekrar yüklemeye gerek yok,
                // sadece ItemCommand veya ButtonClick gibi olaylarda yüklenmeli.
            }
        }

        private void OgrencininDersleriniDoldur()
        {
            try
            {
                int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
                List<EntityDersKayit> kayitliDersler = BLLOgrenci.OgrenciOnayliDersListesiBll(ogrenciId);
                // Dropdown için sadece ID ve Ad gerekiyor
                var dropdownDataSource = kayitliDersler.Select(d => new { DersId = d.DersId, DersAd = d.DersAd }).ToList();

                rptDersListesiSolMenuOgr.DataSource = dropdownDataSource; // Doğru Repeater'a bağla
                rptDersListesiSolMenuOgr.DataBind();
            }
            catch (Exception ex)
            {
                MesajGoster("Kayıtlı dersleriniz yüklenirken hata oluştu.", false);
                System.Diagnostics.Debug.WriteLine("OgrenciMesajlar DersleriDoldur Hata: " + ex.ToString());
            }
        }

        // Sol menüdeki ders seçimi olayı
        protected void rptDersListesiSolMenuOgr_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DersSec")
            {
                int secilenDersId = Convert.ToInt32(e.CommandArgument);
                hfSeciliDersId.Value = secilenDersId.ToString();
                phMesaj.Visible = false;
                SetPanelVisibility(secilenDersId);
                MesajlariYukle(secilenDersId);
                OgrencininDersleriniDoldur(); // Aktif dersi işaretlemek için tekrar bağla
            }
        }

        // Panellerin görünürlüğünü ayarlar
        private void SetPanelVisibility(int seciliDersId)
        {
            bool dersSecili = seciliDersId > 0;
            pnlMesajlasmaAlaniSag.Visible = dersSecili;
            pnlDersSecimiBekleniyor.Visible = !dersSecili;
        }

        // Mesajları yükler
        private void MesajlariYukle(int dersId)
        {
            try
            {
                if (dersId <= 0) { /*...*/ return; } // Aynı

                int kullaniciId = Convert.ToInt32(Session["KullaniciID"]);
                int kullaniciTipi = Convert.ToInt32(Session["KullaniciTipi"]);

                // Seçili dersin adını al (Öğrenci ders listesinden)
                List<EntityDersKayit> kayitliDersler = BLLOgrenci.OgrenciOnayliDersListesiBll(kullaniciId);
                var seciliDers = kayitliDersler.FirstOrDefault(d => d.DersId == dersId);
                litSeciliDersAdi.Text = seciliDers?.DersAd ?? "Bilinmeyen Ders";


                List<EntityMesaj> mesajlar = BLLMesaj.GetMesajlarByDersIdBLL(dersId, kullaniciId, kullaniciTipi);
                rptMesajAkisi.DataSource = mesajlar;
                rptMesajAkisi.DataBind();
                pnlMesajYok.Visible = (mesajlar == null || mesajlar.Count == 0);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ScrollToBottom_" + rptMesajAkisi.ClientID, "setTimeout(function(){ scrollToBottom('mesajAkisiContainer'); }, 150);", true);
            }
            catch (Exception ex) { /*...*/ } // Aynı
        }

        // Mesaj gönderme
        protected void btnMesajGonder_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            int dersId = Convert.ToInt32(hfSeciliDersId.Value);
            string mesajIcerik = txtMesajIcerik.Text.Trim();
            int kullaniciId = Convert.ToInt32(Session["KullaniciID"]);
            int kullaniciTipi = Convert.ToInt32(Session["KullaniciTipi"]); // 0 veya 1

            if (dersId <= 0) { MesajGoster("Lütfen mesaj göndermek için soldaki menüden bir ders seçin.", false); return; }

            try
            {
                EntityMesaj yeniMesaj = new EntityMesaj
                {
                    DersID = dersId,
                    GonderenKullaniciID = kullaniciId,
                    GonderenKullaniciTipi = kullaniciTipi,
                    MesajIcerik = mesajIcerik
                };

                int sonuc = BLLMesaj.MesajEkleBLL(yeniMesaj, kullaniciId, kullaniciTipi);

                if (sonuc > 0)
                {
                    Response.Redirect(Request.Url.AbsolutePath + "?DersID=" + dersId.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else if (sonuc == -3) { MesajGoster("Bu derse mesaj gönderme yetkiniz yok.", false); }
                else if (sonuc == -2) { MesajGoster("Mesaj gönderilemedi. İçerik boş olamaz.", false); }
                else { MesajGoster("Mesaj gönderilirken bir veritabanı hatası oluştu.", false); }
            }
            catch (Exception ex)
            {
                MesajGoster("Mesaj gönderilirken beklenmedik bir hata oluştu.", false);
                System.Diagnostics.Debug.WriteLine("Mesaj Gönderme Hatası: " + ex.ToString());
            }
        }

        // Gönderen Adını Almak İçin Yardımcı Metot
        protected string GetGonderenAdi(object gonderenIDObj, object gonderenTipiObj)
        {
            string adSoyad = "Bilinmeyen Kullanıcı";
            try
            {
                if (gonderenIDObj != null && gonderenTipiObj != null &&
                    int.TryParse(gonderenIDObj.ToString(), out int gonderenID) &&
                    int.TryParse(gonderenTipiObj.ToString(), out int gonderenTipi))
                {
                    if (gonderenTipi == 0) // Öğrenci
                    {
                        // Kendi mesajı ise "Siz" yaz
                        if (gonderenID == Convert.ToInt32(Session["KullaniciID"])) return "Siz";
                        List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(gonderenID);
                        if (ogrList?.Count > 0) adSoyad = $"{ogrList[0].AD} {ogrList[0].SOYAD}";
                    }
                    else if (gonderenTipi == 1) // Öğretmen
                    {
                        List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(gonderenID);
                        if (ogrtList?.Count > 0) adSoyad = ogrtList[0].OGRTADSOYAD + " (Öğretmen)"; // Öğretmen olduğunu belirt
                    }
                }
            }
            catch (Exception ex) { /*...*/ adSoyad = "Hata"; }
            return adSoyad;
        }

        // Mesaj İçeriğini Formatlamak İçin Yardımcı Metot
        protected string FormatMesajIcerik(object icerikObj)
        {
            try
            {
                string text = icerikObj?.ToString() ?? string.Empty;
                return Server.HtmlEncode(text).Replace("\n", "<br />");
            }
            catch { return "[Mesaj içeriği görüntülenemiyor]"; }
        }

        protected string GetGonderenFotoUrl(object gonderenIDObj, object gonderenTipiObj)
        {
            // Varsayılan resimler (bunların projende olduğundan emin ol)
            string defaultOgrenciFoto = "~/OgrenciFotograflari/default.png";
            string defaultOgretmenFoto = "~/OgretmenFotograflari/default_teacher.png";
            string fotoUrl = defaultOgrenciFoto; // Başlangıçta varsayılan öğrenci

            try
            {
                if (gonderenIDObj != null && gonderenTipiObj != null &&
                    int.TryParse(gonderenIDObj.ToString(), out int gonderenID) &&
                    int.TryParse(gonderenTipiObj.ToString(), out int gonderenTipi))
                {
                    if (gonderenTipi == 0) // Öğrenci
                    {
                        fotoUrl = defaultOgrenciFoto; // Varsayılanı ayarla
                        List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(gonderenID);
                        if (ogrList?.Count > 0 && !string.IsNullOrEmpty(ogrList[0].FOTOGRAF))
                        {
                            fotoUrl = $"~/OgrenciFotograflari/{ogrList[0].FOTOGRAF}";
                        }
                    }
                    else if (gonderenTipi == 1) // Öğretmen
                    {
                        fotoUrl = defaultOgretmenFoto; // Varsayılanı ayarla
                        List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(gonderenID);
                        if (ogrtList?.Count > 0 && !string.IsNullOrEmpty(ogrtList[0].OGRTFOTOGRAF))
                        {
                            fotoUrl = $"~/OgretmenFotograflari/{ogrtList[0].OGRTFOTOGRAF}";
                        }
                    }
                }
                else
                {
                    // ID veya Tip null veya geçersizse, tipe göre varsayılanı döndür (tip biliniyorsa)
                    if (int.TryParse(gonderenTipiObj?.ToString() ?? "-1", out int tip))
                    {
                        fotoUrl = (tip == 1) ? defaultOgretmenFoto : defaultOgrenciFoto;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetGonderenFotoUrl Hata (ID:{gonderenIDObj}, Tip:{gonderenTipiObj}): " + ex.ToString());
                // Hata durumunda da varsayılanı döndür (tipi tahmin etmeye çalış)
                if (int.TryParse(gonderenTipiObj?.ToString() ?? "-1", out int tip))
                {
                    fotoUrl = (tip == 1) ? defaultOgretmenFoto : defaultOgrenciFoto;
                }
            }
            // Metot sadece ~/ ile başlayan yolu döndürür, ResolveUrl ASPX'te kullanılır.
            return fotoUrl;
        }

        // Mesaj Gösterme Yardımcı Metodu
        private void MesajGoster(string mesaj, bool basariliMi)
        {
            if (phMesaj == null || litMesaj == null || divMesaj == null) return;
            litMesaj.Text = mesaj;
            divMesaj.Attributes["class"] = basariliMi ? "alert alert-success alert-dismissible m-3" : "alert alert-danger alert-dismissible m-3"; // m-3 eklendi
            phMesaj.Visible = true;
        }
    }
}