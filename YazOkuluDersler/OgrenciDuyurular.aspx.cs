using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class OgrenciDuyurular : System.Web.UI.Page
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
                LoadTumDuyurular();
            }
        }

        private void LoadTumDuyurular()
        {
            try
            {
                List<EntityDuyuru> duyurular = BLLDuyuru.TumOgrenciDuyurulariGetirBLL();

                if (duyurular != null && duyurular.Count > 0)
                {
                    rptTumDuyurular.DataSource = duyurular;
                    rptTumDuyurular.DataBind();
                    phDuyuruYok.Visible = false;
                }
                else
                {
                    phDuyuruYok.Visible = true;
                    rptTumDuyurular.DataSource = null;
                    rptTumDuyurular.DataBind();
                }
            }
            catch (Exception ex)
            {
                phDuyuruYok.Visible = true;
                System.Diagnostics.Debug.WriteLine("OgrenciDuyurular LoadTumDuyurular Hata: " + ex.ToString());
            }
        }

        protected string GetHedefKitleText(object hedefKitleObj)
        {
            int hedefKitle = Convert.ToInt32(hedefKitleObj);
            switch (hedefKitle)
            {
                case 0: return "Öğrenci";
                case 1: return "Öğretmen";
                case 2: return "Herkes";
                default: return "Bilinmeyen";
            }
        }

        protected string FormatContent(object content)
        {
            string text = content?.ToString() ?? "";
            return Server.HtmlEncode(text).Replace("\n", "<br />");
        }
    }
}