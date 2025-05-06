using System;
using System.Web.UI;
using System.Collections.Generic;
using EntityLayer;
using BusinessLogicLayer;
using System.Web.UI.WebControls;
using System.Globalization;

namespace YazOkulu
{
    public partial class DersGuncelle : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtDersAdi;
        protected global::System.Web.UI.WebControls.DropDownList DrpOgretmenList;
        protected global::System.Web.UI.WebControls.TextBox TxtMinKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtMaxKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtDersUcret;
        protected global::System.Web.UI.WebControls.TextBox TxtDersAciklama;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected global::System.Web.UI.WebControls.Label lblMesaj;
        protected global::System.Web.UI.WebControls.HiddenField hdnDersID;
        protected global::System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
        protected global::System.Web.UI.WebControls.HyperLink hlGeri;


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMesaj.Text = "";
            lblMesaj.CssClass = "";

            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
            }
            if ((int)Session["KullaniciTipi"] != 1)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null && int.TryParse(Request.QueryString["ID"], out int dersId))
                {
                    hdnDersID.Value = dersId.ToString();
                    LoadDersData(dersId);
                }
                else
                {
                    lblMesaj.Text = "Geçersiz Ders ID.";
                    lblMesaj.CssClass = "text-danger";
                    DisableForm();
                }
            }
        }

        private void DisableForm()
        {
            TxtDersAdi.Enabled = false;
            DrpOgretmenList.Enabled = false;
            TxtMinKontenjan.Enabled = false;
            TxtMaxKontenjan.Enabled = false;
            TxtDersUcret.Enabled = false;
            Button1.Enabled = false;
        }


        private void LoadDersData(int dersId)
        {
            try
            {
                List<EntityDers> dersList = BLLDers.BllDersDetay(dersId);
                if (dersList == null || dersList.Count == 0)
                {
                    throw new Exception("Ders bulunamadı.");
                }
                EntityDers ders = dersList[0];

                TxtDersAdi.Text = ders.DERSAD;
                TxtMinKontenjan.Text = ders.MIN.ToString();
                TxtMaxKontenjan.Text = ders.MAX.ToString();
                TxtDersUcret.Text = ders.DERSUCRET.ToString("F2", CultureInfo.InvariantCulture);
                TxtDersAciklama.Text = ders.DERSACIKLAMA;

                List<EntityOgretmen> ogrtList = BLLOgretmen.BllListele();
                DrpOgretmenList.DataSource = ogrtList;
                DrpOgretmenList.DataTextField = "OGRTADSOYAD";
                DrpOgretmenList.DataValueField = "OGRTID";
                DrpOgretmenList.DataBind();

                if (ders.OGRETMENID != 0 && DrpOgretmenList.Items.FindByValue(ders.OGRETMENID.ToString()) != null)
                {
                    DrpOgretmenList.SelectedValue = ders.OGRETMENID.ToString();
                }
                else
                {
                    DrpOgretmenList.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Ders bilgileri yüklenirken hata: " + ex.Message;
                lblMesaj.CssClass = "text-danger";
                DisableForm();
                Console.WriteLine("DersGuncelle LoadDersData Hata: " + ex.ToString());
            }
        }


        protected void ButtonG_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (!int.TryParse(hdnDersID.Value, out int dersId))
            {
                lblMesaj.Text = "Geçersiz Ders ID.";
                lblMesaj.CssClass = "text-danger";
                return;
            }

            try
            {
                EntityDers ent = new EntityDers();
                ent.ID = dersId;
                ent.DERSAD = TxtDersAdi.Text;
                ent.MIN = Convert.ToInt32(TxtMinKontenjan.Text);
                ent.MAX = Convert.ToInt32(TxtMaxKontenjan.Text);
                ent.OGRETMENID = Convert.ToInt32(DrpOgretmenList.SelectedValue);
                ent.DERSACIKLAMA = TxtDersAciklama.Text.Trim();

                if (decimal.TryParse(TxtDersUcret.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dersUcreti))
                {
                    ent.DERSUCRET = dersUcreti;
                }
                else
                {
                    lblMesaj.Text = "Geçersiz ücret formatı.";
                    lblMesaj.CssClass = "text-danger";
                    return;
                }

                bool sonuc = BLLDers.DersGuncelleBll(ent);

                if (sonuc)
                {
                    Session["GuncellemeMesaj"] = "Ders başarıyla güncellendi.";
                    Response.Redirect("DersListesi.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMesaj.Text = "Ders güncellenirken bir hata oluştu veya girilen bilgiler geçersiz (örn: ücret negatif).";
                    lblMesaj.CssClass = "text-danger";
                }
            }
            catch (FormatException)
            {
                lblMesaj.Text = "Lütfen kontenjan ve ücret alanlarına geçerli sayılar giriniz.";
                lblMesaj.CssClass = "text-danger";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Güncelleme sırasında beklenmedik bir hata oluştu: " + ex.Message;
                lblMesaj.CssClass = "text-danger";
                Console.WriteLine("DersGuncelle ButtonG_Click Hata: " + ex.ToString());
            }
        }
    }
}