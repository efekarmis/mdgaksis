using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnOgrenciGiris_Click(object sender, EventArgs e)
        {
            string id = txtOgrenciId.Text.Trim();
            string sifre = txtOgrenciSifre.Text;

            litOgrenciError.Text = "";
            hfPanelState.Value = "sign-in";

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(sifre))
            {
                litOgrenciError.Text = "<span class='error-message'>ID ve Şifre alanları gereklidir.</span>";
                return;
            }

            try
            {
                EntityKullanici user = BLLKullanici.KullaniciKontrol(id, sifre, 0);

                if (user != null)
                {
                    Session["KullaniciTipi"] = 0;
                    Session["KullaniciID"] = user.KULLANICIID;
                    Session["KullaniciAdSoyad"] = $"{user.KULLANICIAD} {user.KULLANICISOYAD}";

                    Response.Redirect("OgrenciAnaSayfa.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    litOgrenciError.Text = "<span class='error-message'>Geçersiz ID veya şifre.</span>";
                }
            }
            catch (Exception ex)
            {
                litOgrenciError.Text = "<span class='error-message'>Giriş işlemi sırasında bir hata oluştu.</span>";
                System.Diagnostics.Debug.WriteLine("Öğrenci Giriş Hatası: " + ex.ToString());
            }
        }

        protected void btnOgretmenGiris_Click(object sender, EventArgs e)
        {
            string id = txtOgretmenId.Text.Trim();
            string sifre = txtOgretmenSifre.Text;

            litOgretmenError.Text = "";
            hfPanelState.Value = "sign-up";

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(sifre))
            {
                litOgretmenError.Text = "<span class='error-message'>ID ve Şifre alanları gereklidir.</span>";
                return;
            }

            try
            {
                EntityKullanici user = BLLKullanici.KullaniciKontrol(id, sifre, 1);

                if (user != null)
                {
                    Session["KullaniciTipi"] = 1;
                    Session["KullaniciID"] = user.KULLANICIID;
                    Session["KullaniciAdSoyad"] = $"{user.KULLANICIAD} {user.KULLANICISOYAD}";

                    Response.Redirect("OgretmenAnaSayfa.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    litOgretmenError.Text = "<span class='error-message'>Geçersiz ID veya şifre.</span>";
                }
            }
            catch (Exception ex)
            {
                litOgretmenError.Text = "<span class='error-message'>Giriş işlemi sırasında bir hata oluştu.</span>";
                System.Diagnostics.Debug.WriteLine("Öğretmen Giriş Hatası: " + ex.ToString());
            }
        }
    }
}