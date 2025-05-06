using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;
using System.Web.UI.HtmlControls;

namespace YazOkulu
{
    public partial class DersDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((int)Session["KullaniciTipi"] == 0 || (int)Session["KullaniciTipi"] == 1))
            {
                Response.Redirect("~/Login.aspx?Error=OturumYok", true); return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null && int.TryParse(Request.QueryString["ID"], out int dersId))
                {
                    LoadDersDetay(dersId);
                }
                else
                {
                    ShowError("Geçersiz veya eksik Ders ID.");
                }

                if (Request.UrlReferrer != null)
                {
                    hlGeri.NavigateUrl = Request.UrlReferrer.ToString();
                    hlHataGeri.NavigateUrl = Request.UrlReferrer.ToString();
                }
                else
                {
                    if (Session["KullaniciTipi"] != null && (int)Session["KullaniciTipi"] == 0)
                        hlGeri.NavigateUrl = ResolveUrl("~/DersTalep.aspx"); // Öğrenci için
                    else if (Session["KullaniciTipi"] != null && (int)Session["KullaniciTipi"] == 1)
                        hlGeri.NavigateUrl = ResolveUrl("~/DersListesi.aspx"); // Öğretmen için
                    else
                        hlGeri.NavigateUrl = ResolveUrl("~/Login.aspx"); // Diğer durumlar

                    hlHataGeri.NavigateUrl = hlGeri.NavigateUrl; // Hata durumu için de aynı
                }
            }
        }

        private void LoadDersDetay(int dersId)
        {
            try
            {
                List<EntityDers> dersList = BLLDers.BllDersDetay(dersId);
                if (dersList == null || dersList.Count == 0)
                {
                    ShowError($"ID={dersId} olan ders bulunamadı.");
                    return;
                }

                EntityDers ders = dersList[0];

                Page.Title = ders.DERSAD + " - Ders Detayı";
                hDersAdi.InnerText = ders.DERSAD;
                litMinKontenjan.Text = ders.MIN.ToString();
                litMaxKontenjan.Text = ders.MAX.ToString();
                litUcret.Text = ders.DERSUCRET.ToString("C", CultureInfo.GetCultureInfo("tr-TR"));

                if (ders.OGRETMEN != null && !string.IsNullOrEmpty(ders.OGRETMEN.OGRTADSOYAD))
                {
                    litOgretmen.Text = ders.OGRETMEN.OGRTADSOYAD;
                }
                else
                {
                    litOgretmen.Text = "<span class='text-muted fst-italic'>Atanmamış</span>";
                }

                if (!string.IsNullOrEmpty(ders.DERSACIKLAMA))
                {
                    litAciklama.Text = Server.HtmlEncode(ders.DERSACIKLAMA).Replace("\n", "<br />");
                }
                else
                {
                    litAciklama.Text = "<span class='text-muted'>Bu ders için bir açıklama girilmemiştir.</span>";
                }

                pnlDersBilgi.Visible = true;
                pnlHata.Visible = false;

            }
            catch (Exception ex)
            {
                ShowError("Ders detayları yüklenirken bir hata oluştu.");
                Console.WriteLine($"OgrenciDersDetay LoadDersDetay Hata (DersID:{dersId}): " + ex.ToString());
            }
        }

        private void ShowError(string message)
        {
            pnlDersBilgi.Visible = false;
            pnlHata.Visible = true;
            litHata.Text = message;
        }
    }
}