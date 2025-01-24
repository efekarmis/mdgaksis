using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using EntityLayer;

namespace YazOkuluDersler
{
    public partial class DersAta : Page
    {
        protected global::System.Web.UI.WebControls.DropDownList DropDownList1;
        protected global::System.Web.UI.WebControls.TextBox TextBox1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                List<EntityDersler> entDers = BLLDersler.BllListele();
                DropDownList1.DataSource = BLLDersler.BllListele();
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
            BLLDersler.TalepEkleBll(ent);
        }
    }
}