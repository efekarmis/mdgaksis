using System;
using System.Web.UI;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class DersGuncelle : Page
    {
        protected global::System.Web.UI.WebControls.TextBox TxtDersAdi;
        protected global::System.Web.UI.WebControls.DropDownList DrpOgretmenList;
        protected global::System.Web.UI.WebControls.Button Button1;
        protected int DersIdDeger;

        protected void Page_Load(object sender, EventArgs e)
        {
            DersIdDeger = Convert.ToInt32(Request.QueryString["ID"]);;
            if (Page.IsPostBack == false)
            {
                // Ders bilgilerini al
                EntityDersler ders = BLLDersler.BllDersDetay(DersIdDeger)[0];
                TxtDersAdi.Text = ders.DERSAD;
                
                List<EntityOgretmen> ogrtList = BLLOgretmen.BllListele();
                DrpOgretmenList.DataSource = ogrtList;
                DrpOgretmenList.DataTextField = "OGRTADSOYAD";
                DrpOgretmenList.DataValueField = "OGRTID";
                DrpOgretmenList.DataBind();
                if (ders.OGRETMENID != 0)
                {
                    DrpOgretmenList.SelectedValue = ders.OGRETMENID.ToString();
                }
            }
        }

        protected void ButtonG_Click(object sender, EventArgs e)
        {
            // Güncellenecek ders bilgilerini al
            EntityDersler ent = new EntityDersler
            {
                ID = DersIdDeger,
                DERSAD = TxtDersAdi.Text,
                OGRETMENID = Convert.ToInt32(DrpOgretmenList.SelectedValue)
            };

            // Güncelleme işlemini gerçekleştir
            BLLDersler.DersGuncelleBll(ent);

            // Güncelleme sonrası listeye yönlendir
            Response.Redirect("DersListesi.aspx");
        }
    }
}