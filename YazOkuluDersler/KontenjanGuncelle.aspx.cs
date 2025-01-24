using System;
using System.Web.UI;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class KontenjanGuncelle : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtDersAdi;
        protected global::System.Web.UI.WebControls.TextBox TxtMinKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtMaxKontenjan;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected int DersIdDeger;

        protected void Page_Load(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(Request.QueryString["ID"]);
            DersIdDeger = x;
            if (Page.IsPostBack == false)
            {
                // Ders bilgilerini al
                List<EntityDersler> dersList = BLLKontenjanlar.BllDersDetay(x);
                TxtDersAdi.Text = dersList[0].DERSAD;
                TxtMinKontenjan.Text = dersList[0].MIN.ToString();
                TxtMaxKontenjan.Text = dersList[0].MAX.ToString();
            }
        }

        protected void ButtonG_Click(object sender, EventArgs e)
        {
            // Güncellenecek ders bilgilerini al
            EntityDersler ent = new EntityDersler();
            ent.ID = DersIdDeger;
            ent.DERSAD = TxtDersAdi.Text;
            ent.MIN = Convert.ToInt32(TxtMinKontenjan.Text);
            ent.MAX = Convert.ToInt32(TxtMaxKontenjan.Text);

            // Güncelleme işlemini gerçekleştir
            BLLDersler.DersGuncelleBll(ent);

            // Güncelleme sonrası listeye yönlendir
            Response.Redirect("~/KontenjanListesi.aspx");
        }
    }
}