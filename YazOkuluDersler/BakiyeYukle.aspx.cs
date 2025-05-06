using System;
using System.Collections.Generic; // List için
using System.Globalization; // CultureInfo için
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer; // BLL kullanmak için
using EntityLayer; // EntityOgrenci için (opsiyonel, sadece mevcut bakiye göstermek için)


namespace YazOkulu
{
    public partial class BakiyeYukle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("Login.aspx?Error=OturumYok", true);
                return;
            }
            if ((int)Session["KullaniciTipi"] != 0) // Sadece Öğrenci
            {
                Response.Redirect("Login.aspx?Error=Yetkisiz", true);
                return;
            }

            if (!IsPostBack)
            {
                MevcutBakiyeyiGoster();
            }
            lblMesaj.Text = "";
            lblMesaj.CssClass = "";
        }

        private void MevcutBakiyeyiGoster()
        {
            try
            {
                int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
                List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(ogrenciId);
                if (ogrList != null && ogrList.Count > 0)
                {
                    lblMevcutBakiye.Text = ogrList[0].BAKIYE.ToString("C", CultureInfo.GetCultureInfo("tr-TR"));
                }
                else
                {
                    lblMevcutBakiye.Text = "Bilgi alınamadı.";
                }
            }
            catch (Exception ex)
            {
                lblMevcutBakiye.Text = "Hata oluştu.";
                Console.WriteLine("BakiyeYukle MevcutBakiyeyiGoster Hata: " + ex.Message);
            }
        }


        protected void btnBakiyeYukle_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    double yuklenecekMiktar;
                    if (double.TryParse(txtYuklenecekMiktar.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out yuklenecekMiktar))
                    {
                        if (yuklenecekMiktar > 0)
                        {
                            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

                            bool sonuc = BLLOgrenci.BakiyeEkleBll(ogrenciId, yuklenecekMiktar);

                            if (sonuc)
                            {
                                lblMesaj.Text = $"Başarılı! {yuklenecekMiktar:C2} bakiyenize eklendi.";
                                lblMesaj.CssClass = "text-success"; // Başarı mesajı stili
                                txtYuklenecekMiktar.Text = ""; // Input'u temizle
                                MevcutBakiyeyiGoster(); // Güncel bakiyeyi tekrar göster
                            }
                            else
                            {
                                lblMesaj.Text = "Bakiye güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                                lblMesaj.CssClass = "text-danger"; // Hata mesajı stili
                            }
                        }
                        else
                        {
                            lblMesaj.Text = "Yüklenecek miktar 0'dan büyük olmalıdır.";
                            lblMesaj.CssClass = "text-danger";
                        }
                    }
                    else
                    {
                        lblMesaj.Text = "Lütfen geçerli bir sayısal miktar giriniz.";
                        lblMesaj.CssClass = "text-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "İşlem sırasında beklenmedik bir hata oluştu.";
                    lblMesaj.CssClass = "text-danger";
                    Console.WriteLine("BakiyeYukle btnBakiyeYukle_Click Hata: " + ex.Message);
                }
            }
        }
    }
}