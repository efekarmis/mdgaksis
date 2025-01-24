using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using DataAccessLayer;
using BusinessLogicLayer;

namespace YazOkuluDersler
{
    public partial class DersListesi : Page
    {
        protected global::System.Web.UI.WebControls.Repeater Repeater;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<EntityDersler> drsList = BLLDersler.BllListele();
            Repeater.DataSource = drsList;
            Repeater.DataBind();
        }
    }
}