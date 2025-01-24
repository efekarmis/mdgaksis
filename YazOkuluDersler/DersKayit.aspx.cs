using System;
using System.Web.UI;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class DersKayit : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtDersAdi;
        protected global::System.Web.UI.WebControls.TextBox TxtMinKontenjan;
        protected global::System.Web.UI.WebControls.TextBox TxtMaxKontenjan;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected int DersIdDeger;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonD_Click(object sender, EventArgs e)
        {
            // Güncellenecek ders bilgilerini al
            EntityDersler ent = new EntityDersler();
            ent.DERSAD = TxtDersAdi.Text;
            ent.MIN = Convert.ToInt32(TxtMinKontenjan.Text);
            ent.MAX = Convert.ToInt32(TxtMaxKontenjan.Text);

            // Güncelleme işlemini gerçekleştir
            BLLDersler.DersGuncelleBll(ent);

            // Güncelleme sonrası listeye yönlendir
            Response.Redirect("Dersler.aspx");
        }
    }
}