using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class UserReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UserReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserReport.aspx");
        }
    }
}