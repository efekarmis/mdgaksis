using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class DuyuruGuncelle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null) { Response.Redirect("~/Login.aspx?Error=OturumYok", true); return; }
            if ((int)Session["KullaniciTipi"] != 1) { Response.Redirect("~/Login.aspx?Error=Yetkisiz", true); return; }

            lblMesaj.Text = "";
            lblMesaj.CssClass = "";

            if (!IsPostBack)
            {
                if (Request.QueryString["DuyuruID"] != null && int.TryParse(Request.QueryString["DuyuruID"], out int duyuruId))
                {
                    hdnDuyuruID.Value = duyuruId.ToString();
                    LoadDuyuruData(duyuruId);
                }
                else
                {
                    MesajGoster("Geçersiz veya eksik Duyuru ID.", false);
                    DisableForm();
                }
            }
        }

        private void DisableForm()
        {
            txtBaslik.Enabled = false;
            txtIcerik.Enabled = false;
            ddlHedefKitle.Enabled = false;
            rbNormal.Enabled = false;
            rbOnemli.Enabled = false;
            btnDuyuruGuncelle.Enabled = false;
        }

        private void LoadDuyuruData(int duyuruId)
        {
            try
            {
                EntityDuyuru duyuru = BLLDuyuru.DuyuruGetirBLL(duyuruId);
                if (duyuru == null)
                {
                    MesajGoster("Duyuru bulunamadı.", false);
                    DisableForm();
                    return;
                }

                txtBaslik.Text = duyuru.Baslik;
                txtIcerik.Text = duyuru.Icerik;
                // Hedef Kitle seçimi
                ddlHedefKitle.SelectedValue = duyuru.HedefKitle.ToString();
                // Önem Derecesi seçimi
                if (duyuru.OnemDerecesi == 1) { rbOnemli.Checked = true; rbNormal.Checked = false; }
                else { rbNormal.Checked = true; rbOnemli.Checked = false; }
            }
            catch (Exception ex)
            {
                MesajGoster("Duyuru bilgileri yüklenirken hata oluştu.", false);
                DisableForm();
                System.Diagnostics.Debug.WriteLine("Duyuru Güncelleme Yükleme Hatası: " + ex.ToString());
            }
        }


        protected void btnDuyuruGuncelle_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (!int.TryParse(hdnDuyuruID.Value, out int duyuruId))
            {
                MesajGoster("Geçersiz Duyuru ID.", false); return;
            }

            try
            {
                EntityDuyuru guncelDuyuru = new EntityDuyuru();
                guncelDuyuru.DuyuruID = duyuruId;
                guncelDuyuru.Baslik = txtBaslik.Text.Trim();
                guncelDuyuru.Icerik = txtIcerik.Text.Trim();
                guncelDuyuru.HedefKitle = Convert.ToInt32(ddlHedefKitle.SelectedValue);
                guncelDuyuru.OnemDerecesi = rbOnemli.Checked ? (short)1 : (short)0;

                bool sonuc = BLLDuyuru.DuyuruGuncelleBLL(guncelDuyuru);

                if (sonuc)
                {
                    Session["DuyuruMesaj"] = "Duyuru başarıyla güncellendi.";
                    Session["DuyuruMesajTur"] = true;
                    Response.Redirect("DuyuruYonetimi.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    MesajGoster("Duyuru güncellenirken bir hata oluştu veya bilgiler geçersiz.", false);
                }
            }
            catch (Exception ex)
            {
                MesajGoster("Duyuru güncellenirken beklenmedik bir hata oluştu.", false);
                System.Diagnostics.Debug.WriteLine("Duyuru Güncelleme Hatası: " + ex.ToString());
            }
        }

        private void MesajGoster(string mesaj, bool basariliMi)
        {
            lblMesaj.Text = mesaj;
            lblMesaj.CssClass = basariliMi ? "text-success" : "text-danger";
        }
    }
}