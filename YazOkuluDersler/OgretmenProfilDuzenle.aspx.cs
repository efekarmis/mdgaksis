using System;
using System.Collections.Generic;
using System.IO; // Eklendi
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgretmenProfilDuzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["OGRTID"] != null && Request.QueryString["OGRTID"] == Session["KullaniciID"].ToString())
                {
                    int ogretmenId = Convert.ToInt32(Request.QueryString["OGRTID"]);
                    hdnOgretmenID.Value = ogretmenId.ToString();
                    ProfilDoldur(ogretmenId);
                }
                else { /* Hata */ }
            }
            lblMesaj.Text = ""; lblMesaj.CssClass = "";
        }

        private void ProfilDoldur(int ogretmenId)
        {
            try
            {
                List<EntityOgretmen> ogrtList = BLLOgretmen.BllDetay(ogretmenId);
                if (ogrtList != null && ogrtList.Count > 0)
                {
                    EntityOgretmen ogrt = ogrtList[0];
                    TxtAd.Text = ogrt.OGRTAD;
                    TxtSoyad.Text = ogrt.OGRTSOYAD;
                    if (!string.IsNullOrEmpty(ogrt.OGRTFOTOGRAF))
                    {
                        imgMevcutFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/" + ogrt.OGRTFOTOGRAF);
                    }
                    else
                    {
                        imgMevcutFoto.ImageUrl = ResolveUrl("~/OgretmenFotograflari/default_teacher.png");
                    }
                }
                else { throw new Exception("Öğretmen bulunamadı."); }
            }
            catch (Exception ex) { /* Hata */ }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) { return; }
            if (!int.TryParse(hdnOgretmenID.Value, out int ogretmenId)) { /* Hata */ return; }

            string yeniFotografAdi = null;
            bool dosyaYuklendi = false;
            string eskiFotografAdi = null;

            if (chkFotoKaldir.Checked)
            {
                yeniFotografAdi = null;
            }
            else if (FileUploadControl.HasFile)
            {
                dosyaYuklendi = true;
                try
                {
                    string fileExtension = Path.GetExtension(FileUploadControl.FileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".gif")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 5 * 1024 * 1024)
                        {
                            yeniFotografAdi = Guid.NewGuid().ToString() + fileExtension;
                            string savePath = Server.MapPath("~/OgretmenFotograflari/") + yeniFotografAdi; // Doğru klasör
                            FileUploadControl.SaveAs(savePath);
                        }
                        else { throw new Exception("Dosya boyutu çok büyük (Maks 5MB)."); }
                    }
                    else { throw new Exception("Geçersiz dosya türü (JPG, PNG, GIF)."); }
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = $"Fotoğraf yüklenirken hata: {ex.Message}"; lblMesaj.CssClass = "text-danger"; return;
                }
            }

            try
            {
                List<EntityOgretmen> mevcutOgrtList = BLLOgretmen.BllDetay(ogretmenId);
                if (mevcutOgrtList == null || mevcutOgrtList.Count == 0) { /* Hata */ return; }
                EntityOgretmen ent = mevcutOgrtList[0];
                eskiFotografAdi = ent.OGRTFOTOGRAF;

                ent.OGRTAD = TxtAd.Text;
                ent.OGRTSOYAD = TxtSoyad.Text;
                if (!string.IsNullOrWhiteSpace(TxtSifre.Text))
                {
                    ent.OGRTSIFRE = TxtSifre.Text; // Hash'le!
                }

                if (chkFotoKaldir.Checked)
                {
                    ent.OGRTFOTOGRAF = null;
                }
                else if (dosyaYuklendi)
                {
                    ent.OGRTFOTOGRAF = yeniFotografAdi;
                }

                bool sonuc = BLLOgretmen.OgretmenGuncelleBll(ent);

                if (sonuc)
                {
                    if ((chkFotoKaldir.Checked || dosyaYuklendi) && !string.IsNullOrEmpty(eskiFotografAdi) && eskiFotografAdi != yeniFotografAdi)
                    {
                        try
                        {
                            string eskiDosyaYolu = Server.MapPath("~/OgretmenFotograflari/") + eskiFotografAdi; // Doğru klasör
                            if (File.Exists(eskiDosyaYolu)) { File.Delete(eskiDosyaYolu); }
                        }
                        catch (Exception ex) { Console.WriteLine("Eski öğretmen fotoğrafı silme hatası: " + ex.Message); }
                    }
                    Session["ProfilGuncellendiMesaj"] = "Profiliniz başarıyla güncellendi.";
                    Response.Redirect("~/OgretmenProfil.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else { /* Güncelleme hatası + Yeni yüklenen dosyayı silme (varsa) */ }
            }
            catch (Exception ex) { /* Genel güncelleme hatası + Yeni yüklenen dosyayı silme (varsa) */ }
        }

        protected void btnIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OgretmenProfil.aspx");
        }
    }
}