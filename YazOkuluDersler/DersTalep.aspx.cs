using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class DersTalep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["KullaniciTipi"] == null)
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true);
            }
            if ((int)Session["KullaniciTipi"] != 0)
            {
                Response.Redirect("~/Login.aspx?Error=Yetkisiz", true);
            }

            phMesaj.Visible = false;

            if (!IsPostBack)
            {
                DersleriListele();
            }
        }

        private void DersleriListele()
        {
            try
            {
                List<EntityDers> dersListesi = BLLDers.BllListele();
                rptDersListesi.DataSource = dersListesi;
                rptDersListesi.DataBind();
            }
            catch (Exception ex)
            {
                litMesaj.Text = "Ders listesi yüklenirken bir hata oluştu.";
                divMesaj.Attributes["class"] = "alert alert-danger";
                phMesaj.Visible = true;
                Console.WriteLine("DersTalep DersleriListele Hata: " + ex.ToString());
            }
        }

        protected void rptDersListesi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Basvur")
            {
                phMesaj.Visible = false;
                litMesaj.Text = "";
                divMesaj.Attributes["class"] = "alert";

                try
                {
                    int dersId = Convert.ToInt32(e.CommandArgument);
                    int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

                    EntityBasvuruForm ent = new EntityBasvuruForm
                    {
                        BASVURUOGRID = ogrenciId,
                        BASVURUDERSID = dersId
                    };

                    int sonuc = BLLBasvuru.TalepEkleBll(ent);

                    switch (sonuc)
                    {
                        case -1:
                            litMesaj.Text = "Bu ders için zaten beklemede olan bir başvurunuz var!";
                            divMesaj.Attributes["class"] = "alert alert-warning";
                            break;
                        case -2:
                            litMesaj.Text = "Seçtiğiniz dersin kontenjanı dolmuştur!";
                            divMesaj.Attributes["class"] = "alert alert-danger";
                            break;
                        case -3:
                            litMesaj.Text = "Talep edilen ders sistemde bulunamadı!";
                            divMesaj.Attributes["class"] = "alert alert-danger";
                            break;
                        case -4:
                            litMesaj.Text = "Başvuru için gerekli bilgiler eksik veya geçersiz.";
                            divMesaj.Attributes["class"] = "alert alert-danger";
                            break;
                        case -5:
                            litMesaj.Text = "Bu derse zaten kayıtlısınız!";
                            divMesaj.Attributes["class"] = "alert alert-info";
                            break;
                        case -7:
                            litMesaj.Text = "Bakiyeniz bu ders için yetersiz! Lütfen bakiye yükleyin.";
                            divMesaj.Attributes["class"] = "alert alert-danger";
                            break;

                        default:
                            if (sonuc > 0)
                            {
                                litMesaj.Text = "Ders talebiniz başarıyla alınmıştır. Onaylandıktan sonra 'Derslerim' sayfasında görünecektir.";
                                divMesaj.Attributes["class"] = "alert alert-success";
                            }
                            else
                            {
                                litMesaj.Text = "Başvuru sırasında beklenmedik bir hata oluştu.";
                                divMesaj.Attributes["class"] = "alert alert-danger";
                            }
                            break;
                    }
                    phMesaj.Visible = true;
                }
                catch (Exception ex)
                {
                    litMesaj.Text = "İşlem sırasında bir hata oluştu.";
                    divMesaj.Attributes["class"] = "alert alert-danger";
                    phMesaj.Visible = true;
                    Console.WriteLine("DersTalep Başvur Hata: " + ex.ToString());
                }
            }
        }
    }
}