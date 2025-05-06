using System;
using System.Collections.Generic;
using System.Globalization; // Decimal parse için
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class DersKayit : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtDersAdi;
        protected global::System.Web.UI.WebControls.TextBox TxtMinKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtMaxKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtDersUcret;
        protected global::System.Web.UI.WebControls.TextBox TxtDersAciklama;
        protected global::System.Web.UI.WebControls.DropDownList dDLOgretmenID;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected global::System.Web.UI.WebControls.Label lblMesaj;
        protected global::System.Web.UI.WebControls.ValidationSummary ValidationSummary1;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
            }
            if ((int)Session["KullaniciTipi"] != 1)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
            }

            lblMesaj.Text = "";
            lblMesaj.CssClass = "";

            if (!IsPostBack)
            {
                OgretmenListesiDoldur();
            }
        }

        private void OgretmenListesiDoldur()
        {
            try
            {
                List<EntityOgretmen> ogrtList = BLLOgretmen.BllListele();
                dDLOgretmenID.DataSource = ogrtList;
                dDLOgretmenID.DataTextField = "OGRTADSOYAD";
                dDLOgretmenID.DataValueField = "OGRTID";
                dDLOgretmenID.DataBind();
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Öğretmen listesi yüklenirken hata oluştu.";
                lblMesaj.CssClass = "text-warning";
                Console.WriteLine("DersKayit OgretmenListesiDoldur Hata: " + ex.Message);
            }
        }


        protected void ButtonD_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    EntityDers ent = new EntityDers();
                    ent.DERSAD = TxtDersAdi.Text;
                    ent.MIN = int.Parse(TxtMinKontenjan.Text);
                    ent.MAX = int.Parse(TxtMaxKontenjan.Text);
                    ent.OGRETMENID = int.Parse(dDLOgretmenID.SelectedValue);
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


                    int sonuc = BLLDers.DersEkleBLL(ent);

                    if (sonuc > 0)
                    {
                        Session["KayitMesaj"] = "Ders başarıyla kaydedildi.";
                        Response.Redirect("DersListesi.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else if (sonuc == -1)
                    {
                        lblMesaj.Text = "Ders bilgileri geçersiz. Lütfen kontrol edip tekrar deneyin (Ücret negatif olamaz).";
                        lblMesaj.CssClass = "text-danger";
                    }
                    else
                    {
                        lblMesaj.Text = "Ders kaydedilirken bir hata oluştu.";
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
                    lblMesaj.Text = "Beklenmedik bir hata oluştu: " + ex.Message;
                    lblMesaj.CssClass = "text-danger";
                    Console.WriteLine("DersKayit ButtonD_Click Hata: " + ex.ToString());
                }
            }
        }
    }
}