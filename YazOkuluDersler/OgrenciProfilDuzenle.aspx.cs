using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;


namespace YazOkulu
{
    public partial class OgrenciProfilDuzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true); return;
            }
            if ((int)Session["KullaniciTipi"] != 0)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["OGRID"] != null && Request.QueryString["OGRID"] == Session["KullaniciID"].ToString())
                {
                    int ogrenciId = Convert.ToInt32(Request.QueryString["OGRID"]);
                    hdnOgrenciID.Value = ogrenciId.ToString();
                    ProfilDoldur(ogrenciId);
                }
                else
                {
                    lblMesaj.Text = "Profil bilgileri yüklenemedi veya yetkiniz yok.";
                    lblMesaj.CssClass = "text-danger";
                    pnlFormInputs.Visible = false; // Formu gizle (Panel eklenirse)
                    btnKaydet.Visible = false;
                }
            }
            lblMesaj.Text = "";
            lblMesaj.CssClass = "";
        }

        private void ProfilDoldur(int ogrenciId)
        {
            try
            {
                List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(ogrenciId);
                if (ogrList != null && ogrList.Count > 0)
                {
                    EntityOgrenci ogr = ogrList[0];
                    TxtAd.Text = ogr.AD;
                    TxtSoyad.Text = ogr.SOYAD;
                    TxtNumara.Text = ogr.NUMARA;

                    if (!string.IsNullOrEmpty(ogr.FOTOGRAF))
                    {
                        imgMevcutFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/" + ogr.FOTOGRAF);
                    }
                    else
                    {
                        imgMevcutFoto.ImageUrl = ResolveUrl("~/OgrenciFotograflari/default.png");
                    }
                }
                else { throw new Exception("Öğrenci bulunamadı."); }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Profil bilgileri yüklenirken hata: " + ex.Message;
                lblMesaj.CssClass = "text-danger";
                btnKaydet.Visible = false;
            }
        }


        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (!int.TryParse(hdnOgrenciID.Value, out int ogrenciId))
            {
                lblMesaj.Text = "Geçersiz Öğrenci ID."; lblMesaj.CssClass = "text-danger"; return;
            }

            string yeniFotografAdi = null;
            bool dosyaYuklendi = false;
            string eskiFotografAdi = null;

            if (chkFotoKaldir.Checked)
            {
                yeniFotografAdi = null; // Kaldırılacaksa null olacak
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
                        { // 5 MB limit
                            yeniFotografAdi = Guid.NewGuid().ToString() + fileExtension;
                            string savePath = Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi;
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
            } // else: ne kaldır ne yeni dosya var, fotoğraf değişmeyecek


            try
            {
                List<EntityOgrenci> mevcutOgrList = BLLOgrenci.BllDetay(ogrenciId);
                if (mevcutOgrList == null || mevcutOgrList.Count == 0)
                {
                    lblMesaj.Text = "Güncellenecek öğrenci bulunamadı."; lblMesaj.CssClass = "text-danger"; return;
                }
                EntityOgrenci ent = mevcutOgrList[0];
                eskiFotografAdi = ent.FOTOGRAF; // Eski adı al

                ent.AD = TxtAd.Text;
                ent.SOYAD = TxtSoyad.Text;
                ent.NUMARA = TxtNumara.Text; // Değiştirilmesine izin veriliyor mu?
                if (!string.IsNullOrWhiteSpace(TxtSifre.Text))
                {
                    ent.SIFRE = TxtSifre.Text; // Hash'le!
                }

                if (chkFotoKaldir.Checked)
                {
                    ent.FOTOGRAF = null;
                }
                else if (dosyaYuklendi)
                {
                    ent.FOTOGRAF = yeniFotografAdi;
                }

                bool sonuc = BLLOgrenci.OgrenciGuncelleBll(ent);

                if (sonuc)
                {
                    if ((chkFotoKaldir.Checked || dosyaYuklendi) && !string.IsNullOrEmpty(eskiFotografAdi) && eskiFotografAdi != yeniFotografAdi)
                    {
                        try
                        {
                            string eskiDosyaYolu = Server.MapPath("~/OgrenciFotograflari/") + eskiFotografAdi;
                            if (File.Exists(eskiDosyaYolu)) { File.Delete(eskiDosyaYolu); }
                        }
                        catch (Exception ex) { Console.WriteLine("Eski fotoğraf silme hatası: " + ex.Message); }
                    }
                    Session["ProfilGuncellendiMesaj"] = "Profiliniz başarıyla güncellendi."; // Mesajı Session ile taşı
                    Response.Redirect("~/OgrenciProfil.aspx", false); // False: kod devam etsin
                    Context.ApplicationInstance.CompleteRequest(); // Yönlendirmeyi tamamla
                }
                else
                {
                    lblMesaj.Text = "Profil güncellenirken bir hata oluştu."; lblMesaj.CssClass = "text-danger";
                    if (dosyaYuklendi && !string.IsNullOrEmpty(yeniFotografAdi))
                    {
                        try { File.Delete(Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Güncelleme sırasında hata: " + ex.Message; lblMesaj.CssClass = "text-danger";
                if (dosyaYuklendi && !string.IsNullOrEmpty(yeniFotografAdi))
                {
                    try { File.Delete(Server.MapPath("~/OgrenciFotograflari/") + yeniFotografAdi); } catch { }
                }
            }
        }

        protected void btnIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OgrenciProfil.aspx");
        }
    }
}