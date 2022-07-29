using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class AccesstypeMasterform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessTypeList");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Districtlist");
        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleMasterList");
        }
    }
}