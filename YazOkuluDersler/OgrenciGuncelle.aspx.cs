using System;
using System.Web.UI;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class OgrenciGuncelle : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtId;
        protected global::System.Web.UI.WebControls.TextBox TxtAd;
        protected global::System.Web.UI.WebControls.TextBox TxtSoyad;
        protected global::System.Web.UI.WebControls.TextBox TxtNumara;
        protected global::System.Web.UI.WebControls.TextBox TxtSifre;
        protected global::System.Web.UI.WebControls.TextBox TxtFoto;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected int IdDeger;
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(Request.QueryString["OGRID"].ToString());
            IdDeger = x;
            if(Page.IsPostBack == false){ 
                List<EntityOgrenci> ogrList = BLLOgrenci.BllDetay(x);
                TxtAd.Text = ogrList[0].AD;
                TxtSoyad.Text = ogrList[0].SOYAD;
                TxtNumara.Text = ogrList[0].NUMARA;
                TxtSifre.Text = ogrList[0].SIFRE;
                TxtFoto.Text = ogrList[0].FOTOGRAF;
            }
        }
        
        protected void ButtonG_Click(object sender, EventArgs e)
        {
            EntityOgrenci ent = new EntityOgrenci();
            ent.ID = IdDeger;
            ent.AD = TxtAd.Text;
            ent.SOYAD = TxtSoyad.Text;
            ent.NUMARA = TxtNumara.Text;
            ent.SIFRE = TxtSifre.Text;
            ent.FOTOGRAF = TxtFoto.Text;
            BLLOgrenci.OgrenciGuncelleBll(ent);
            Response.Redirect("OgrenciListesi.aspx");
        }
    }
}