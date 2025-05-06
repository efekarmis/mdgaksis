using System;
using System.Collections.Generic;
using System.IO; // Eklendi
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using DataAccessLayer; // BLLDetay direkt DAL'dan deðil BLL'den gelmeliydi, düzeltelim
using BusinessLogicLayer;

namespace YazOkulu
{
    public partial class OgrenciGuncelle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["OGRID"] != null && int.TryParse(Request.QueryString["OGRID"], out int ogrenciId))
                {
                    hdnOgrenciID.Value = ogrenciId.ToString(); // ID'yi sakla
                    LoadOgrenciData(ogrenciId);
                }
                else
                {
                    Response.Write("<script>alert('Geçersiz Öðrenci ID.'); window.location='OgrenciListesi.aspx';</script>");
                }
            }
        }

        private void LoadOgrenciData(int ogrenciId)
        {
            try
            {
                List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(ogrenciId); // BLL kullan
                if (ogrList != null && ogrList.Count > 0)
                {
                    EntityOgrenci ogr = ogrList[0];
                    TxtAd.Text = ogr.AD;
                    TxtSoyad.Text = ogr.SOYAD;
                    TxtNumara.Text = ogr.NUMARA;
                    hdnOgrenciID.Value = ogr.ID.ToString(); // ID'yi tekrar teyit et

                    if (!string.IsNullOrEmpty(ogr.FOTOGRAF))
                    {
                        imgMevcutFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/" + ogr.FOTOGRAF);
                    }
                    else
                    {
                         imgMevcutFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/default.png"); // Varsayýlan resim
                    }
                }
                else
                {
                    Response.Write("<script>alert('Öðrenci bulunamadý.'); window.location='OgrenciListesi.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Öðrenci bilgileri yüklenirken hata: {ex.Message.Replace("'", "\\'")}');</script>");
            }
        }

        protected void ButtonG_Click(object sender, EventArgs e)
        {
             if (!int.TryParse(hdnOgrenciID.Value, out int ogrenciId))
             {
                 Response.Write("<script>alert('Öðrenci ID alýnamadý.');</script>");
                 return;
             }

             string yeniFotografAdi = null;
             bool dosyaYuklendi = false;
             bool hataOlustu = false;

             if (FileUploadControl.HasFile)
             {
                 dosyaYuklendi = true;
                 try
                 {
                     string fileExtension = Path.GetExtension(FileUploadControl.FileName).ToLower();
                     if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".gif")
                     {
                         if (FileUploadControl.PostedFile.ContentLength < 5 * 1024 * 1024) // 5 MB limit
                         {
                             yeniFotografAdi = Guid.NewGuid().ToString() + fileExtension; // Yeni ad
                             string savePath = Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi;
                             FileUploadControl.SaveAs(savePath);
                         } else { throw new Exception("Dosya boyutu çok büyük (Maks 5MB)."); }
                     } else { throw new Exception("Geçersiz dosya türü (JPG, PNG, GIF)."); }
                 }
                 catch (Exception ex)
                 {
                     Response.Write($"<script>alert('Fotoðraf yüklenirken hata: {ex.Message.Replace("'", "\\'")}');</script>");
                     hataOlustu = true;
                 }
             }

             if(hataOlustu) return; // Dosya yüklemede hata varsa devam etme

             try
             {
                List<EntityOgrenci> mevcutOgrList = BLLOgrenci.BllDetay(ogrenciId);
                if(mevcutOgrList == null || mevcutOgrList.Count == 0)
                {
                     Response.Write("<script>alert('Güncellenecek öðrenci bulunamadý.');</script>");
                    return;
                }
                EntityOgrenci ent = mevcutOgrList[0]; // Mevcut bilgileri al

                ent.AD = TxtAd.Text;
                ent.SOYAD = TxtSoyad.Text;
                ent.NUMARA = TxtNumara.Text;
                if (!string.IsNullOrWhiteSpace(TxtSifre.Text))
                {
                    ent.SIFRE = TxtSifre.Text; // Hashlenmeli!
                }

                 string eskiFotografAdi = ent.FOTOGRAF; // Silmek gerekirse diye sakla

                 if (chkFotoKaldir.Checked) // Fotoðrafý kaldýr iþaretliyse
                 {
                     ent.FOTOGRAF = null; // Veritabanýnda null yap
                     yeniFotografAdi = null; // Yeni yüklenen dosyayý kullanma
                 }
                 else if (dosyaYuklendi) // Yeni dosya yüklendiyse ve kaldýr iþaretli deðilse
                 {
                     ent.FOTOGRAF = yeniFotografAdi; // Veritabanýna yeni adý yaz
                 }


                bool sonuc = BLLOgrenci.OgrenciGuncelleBll(ent);

                if (sonuc)
                {
                     if ((chkFotoKaldir.Checked || dosyaYuklendi) && !string.IsNullOrEmpty(eskiFotografAdi))
                     {
                         try
                         {
                             string eskiDosyaYolu = Server.MapPath("~/OgrenciFotograflari/") + eskiFotografAdi;
                             if (File.Exists(eskiDosyaYolu))
                             {
                                 File.Delete(eskiDosyaYolu);
                             }
                         } catch (Exception ex) {
                              Console.WriteLine("Eski fotoðraf silme hatasý: " + ex.Message);
                         }
                     }

                    Response.Write("<script>alert('Öðrenci bilgileri baþarýyla güncellendi.'); window.location='OgrenciListesi.aspx';</script>");
                }
                else
                {
                     Response.Write("<script>alert('Öðrenci güncellenirken bir hata oluþtu.');</script>");
                      if (dosyaYuklendi && !string.IsNullOrEmpty(yeniFotografAdi))
                      {
                          try { File.Delete(Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi); } catch {}
                      }
                }
             }
              catch (Exception ex)
             {
                   Response.Write($"<script>alert('Güncelleme sýrasýnda hata: {ex.Message.Replace("'", "\\'")}');</script>");
                      if (dosyaYuklendi && !string.IsNullOrEmpty(yeniFotografAdi))
                      {
                          try { File.Delete(Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi); } catch {}
                      }
             }
        }
    }
}