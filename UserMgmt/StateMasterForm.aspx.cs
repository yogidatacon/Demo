using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class StateMasterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
                if(rtype=="1")
                {
                    List<Division> div = new List<Division>();
                    div = BL_User_Mgnt.GetDivisions("");
                    var list = from s in div
                                     where s.state_Code == Session["Sate_Code"].ToString()
                               select s;
                    if(list.ToList().Count>0)
                    {
                       txtState.Attributes.Add("Disabled", "Disabled");
                    }
                    txtSateCode.Attributes.Add("Disabled", "Disabled");
                    txtState.Text = Session["State_name"].ToString();
                    txtSateCode.Value = Session["Sate_Code"].ToString();
                    txtcountryname.Value = Session["country"].ToString();
                  //  txtState.Enabled = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
               
            }
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["Type"] ="0";
            Response.Redirect("~/StateList");
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
            Response.Redirect("~/RoleMasterList1");
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            State state = new State();
            state.state_Code = txtSateCode.Value.ToUpper();
            string s = txtState.Text;
            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            state.state_name =s;
            state.country_name = txtcountryname.Value;
            state.user_id = user_id;
            string val = BL_User_Mgnt.InsertState(state);
            if (val=="1")
            {
                Session["UserID"] = user_id;
                Response.Redirect("StateList");
            }
            else
            {
                if (val.Contains("duplicate"))
                {
                    btnSave.Enabled = true;
                    string message = "State name is Already Exist in Database...!!!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }
        [WebMethod]
        public static string chkDuplicateState(Object statename)
        {
            // IEnumerable myList = email_id as IEnumerable;
            string s = statename.ToString();
            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            int value = BL_User_Mgnt.GetExistsData("state_master", "state_name", s);
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateStateCode(Object statecode)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("state_master", "state_code", statecode.ToString().ToUpper());
            return value.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/StateList");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
    }
}