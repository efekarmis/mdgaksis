using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class Default : Page
    {
        protected global::System.Web.UI.WebControls.Repeater Repeater1;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<EntityOgrenci> ogrList = BLLOgrenci.BllListele();
            Repeater1.DataSource = ogrList;
            Repeater1.DataBind();
        }
    }
}