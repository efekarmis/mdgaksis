using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class BasvuruListesi : System.Web.UI.Page
    {
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
                BasvurulariListele();
            }
        }

        private void BasvurulariListele()
        {
            try
            {
                List<EntityBasvuruForm> basvurular = BLLBasvuru.BasvuruListeleBll();
                Repeater.DataSource = basvurular;
                Repeater.DataBind();
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Başvurular yüklenirken bir hata oluştu.";
                lblMesaj.CssClass = "alert alert-danger";
                Console.WriteLine("BasvuruListesi BasvurulariListele Hata: " + ex.ToString());
            }
        }


        protected void Repeater_Command(object source, RepeaterCommandEventArgs e)
        {
            lblMesaj.Text = "";
            lblMesaj.CssClass = "";

            int basvuruId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Onayla")
            {
                int sonuc = BLLBasvuru.OnaylaBasvuru(basvuruId);

                switch (sonuc)
                {
                    case 1:
                        lblMesaj.Text = $"Başvuru (ID:{basvuruId}) başarıyla onaylandı ve öğrenci derse kaydedildi.";
                        lblMesaj.CssClass = "alert alert-success";
                        break;
                    case -10:
                        lblMesaj.Text = "Hata: Onaylanacak başvuru bulunamadı.";
                        lblMesaj.CssClass = "alert alert-danger";
                        break;
                    case -11:
                        lblMesaj.Text = "Hata: Başvurudaki ders sistemde bulunamadı.";
                        lblMesaj.CssClass = "alert alert-danger";
                        break;
                    case -12:
                        lblMesaj.Text = $"Hata: Öğrencinin bakiyesi bu ders için yetersiz! (Başvuru ID:{basvuruId})";
                        lblMesaj.CssClass = "alert alert-warning";
                        break;
                    case -99:
                    default:
                        lblMesaj.Text = $"Onaylama sırasında beklenmedik bir hata oluştu (Başvuru ID:{basvuruId}). Lütfen logları kontrol edin.";
                        lblMesaj.CssClass = "alert alert-danger";
                        break;
                }
            }
            else if (e.CommandName == "Red")
            {
                try
                {
                    BLLBasvuru.RedBasvuru(basvuruId);
                    lblMesaj.Text = $"Başvuru (ID:{basvuruId}) başarıyla reddedildi.";
                    lblMesaj.CssClass = "alert alert-info";
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = $"Başvuru reddedilirken hata oluştu (Başvuru ID:{basvuruId}).";
                    lblMesaj.CssClass = "alert alert-danger";
                    Console.WriteLine($"BasvuruListesi Red Hata (ID:{basvuruId}): " + ex.ToString());
                }
            }

            BasvurulariListele();
        }
    }
}