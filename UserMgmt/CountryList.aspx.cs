using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class CountryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("CountryForm.aspx");
        }


        protected void Country_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CountryList.aspx");
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList.aspx");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DivisionList.aspx");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Districtlist.aspx");
        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleLevelList.aspx");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessTypeList.aspx");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleMasterList.aspx");
        }
    }
}