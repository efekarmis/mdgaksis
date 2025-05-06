using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls; // Bu using kalsýn
using BusinessLogicLayer;
using EntityLayer;
using static System.Net.Mime.MediaTypeNames;

namespace YazOkulu
{
    public partial class OgrenciKayit : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fotografDosyaAdi = null;

            if (FileUploadControl.HasFile)
            {
                try
                {
                    string fileExtension = Path.GetExtension(FileUploadControl.FileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".gif")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 5 * 1024 * 1024) // 5 MB
                        {
                            fotografDosyaAdi = Guid.NewGuid().ToString() + fileExtension;
                            string savePath = Server.MapPath("~/OgrenciFotograflari/") + fotografDosyaAdi;
                            FileUploadControl.SaveAs(savePath);
                        }
                        else
                        {
                            Response.Write("<script>alert('Dosya boyutu çok büyük (Maksimum 5MB).');</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Sadece JPG, JPEG, PNG veya GIF dosyalarý yükleyebilirsiniz.');</script>");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Dosya yüklenirken bir hata oluþtu: {ex.Message.Replace("'", "\\'")}');</script>");
                    return;
                }
            }

            EntityOgrenci ent = new EntityOgrenci();
            ent.AD = TextAd.Text;
            ent.SOYAD = TxtSoyad.Text;
            ent.NUMARA = TxtNumara.Text;
            ent.SIFRE = TxtSifre.Text;
            ent.FOTOGRAF = fotografDosyaAdi;

            try
            {
                int sonuc = BLLOgrenci.OgrenciEkleBLL(ent);
                if (sonuc > 0)
                {
                    Response.Write("<script>alert('Öðrenci baþarýyla kaydedildi.'); window.location='OgrenciListesi.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Öðrenci kaydedilirken bir hata oluþtu.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Kaydetme sýrasýnda hata: {ex.Message.Replace("'", "\\'")}');</script>");
            }
        }
    }
}