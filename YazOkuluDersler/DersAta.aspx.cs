using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkulu
{
    public partial class DersAta : Page
    {
        protected global::System.Web.UI.WebControls.DropDownList DropDownList1;
        protected global::System.Web.UI.WebControls.TextBox TextBox1;
        
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

            if (Page.IsPostBack == false)
            {
                List<EntityDers> entDers = BLLDers.BllListele();
                DropDownList1.DataSource = entDers;
                DropDownList1.DataTextField = "DERSAD";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
            }
        }
        protected void ButtonTalep_Click(object sender, EventArgs e)
        {
            EntityBasvuruForm ent = new EntityBasvuruForm();
            ent.BASVURUOGRID = int.Parse(TextBox1.Text.ToString());
            ent.BASVURUDERSID = int.Parse(DropDownList1.SelectedValue.ToString());
            BLLBasvuru.TalepEkleBll(ent);
        }
    }
}