using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class PartyTypeMasterForm : System.Web.UI.Page
    {
        public static string Partytypename1 = "";
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
                if (Session["rtype"].ToString() != "0")
                {
                    List<Party_Master> party = new List<Party_Master>();
                    party = BL_Party_Master.GetList();
                    var list = from s1 in party
                               where s1.party_type_code == Session["Party_Type_Code"].ToString()
                               select s1;
                    txtPartyTypeCode.Text = Session["Party_Type_Code"].ToString();
                    txtpartytypename.Text = Session["Party_Type_Name"].ToString();
                    Partytypename1 = Session["Party_Type_Name"].ToString();
                    string val = Session["Status"].ToString();
                
                    if (val=="Active")
                    ddlactive.SelectedValue= "True";
                    else
                        ddlactive.SelectedValue = "False";
                    if (Session["rtype"].ToString() == "1" || list.ToList().Count>0)
                    {
                        txtPartyTypeCode.ReadOnly = true;
                        txtpartytypename.ReadOnly = true;
                       // ddlactive.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        ddlactive.Enabled = false;
                    }
                    else
                    {
                        txtPartyTypeCode.ReadOnly = true;
                    }

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                Party_Type_Master partytype = new Party_Type_Master();
                partytype.party_type_name = txtpartytypename.Text;
                partytype.party_type_code = txtPartyTypeCode.Text.ToUpper();
                if (ddlactive.SelectedItem.ToString()=="Yes")
                partytype.party_active ="true";
                else
                partytype.party_active = "false";
                partytype.org_id = 1;
                partytype.user_id = Session["UserID"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                 val = BL_Party_Type_Master.Insert(partytype);
                else
                    val = BL_Party_Type_Master.Update(partytype);
                if (val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PartyTypeMasterList");
                }
                else
                {
                   
                    string message =val;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }

        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }

        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }

        protected void productmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList");
        }

        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");

        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }
        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }

        protected void producttypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductTypeList");
        }
        protected void RawMaterialTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void RawMaterial_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList");
        }
        [WebMethod]
        public static string chkDuplicatePartyTypeCode(Object Partytypecode)
        {
            int value = BL_User_Mgnt.GetExistsData("party_type_master", "Party_type_code", Partytypecode.ToString());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicatePartyTypeName(Object Partytypename)
        {
            int value=0;
            if(Partytypename1!= Partytypename.ToString())
                value = BL_User_Mgnt.GetExistsData("party_type_master", "Party_type_name", Partytypename.ToString());
           
            return value.ToString();
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
    }
}