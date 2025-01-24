using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkuluDersler
{
    public class OgrenciKayit : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TextAd;
        protected global::System.Web.UI.WebControls.TextBox TxtSoyad;
        protected global::System.Web.UI.WebControls.TextBox TxtNumara;
        protected global::System.Web.UI.WebControls.TextBox TxtSifre;
        protected global::System.Web.UI.WebControls.TextBox TxtFoto;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            EntityOgrenci ent = new EntityOgrenci();
            ent.AD = TextAd.Text;
            ent.SOYAD = TxtSoyad.Text;
            ent.FOTOGRAF = TxtFoto.Text;
            ent.NUMARA = TxtNumara.Text;
            ent.SIFRE = TxtSifre.Text;

            BLLOgrenci.OgrenciEkleBLL(ent);
        }
    }
}