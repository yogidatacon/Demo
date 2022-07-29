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
    public partial class UOMMasterForm : System.Web.UI.Page
    {
        static string uom1;
        
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
                // rtype = Session["rtype"].ToString();
                Session["UserID"] = Session["UserID"];
                if (Session["rtype"].ToString()!= "0")
                {
                    txtcode.Text = Session["uom_code"].ToString();
                    txtName.Text = Session["uom_name"].ToString();
                    uom1= Session["uom_name"].ToString();
                    txtcode.Enabled = false;
                    List<UOM_Master> tabs = new List<UOM_Master>();
                    tabs = BL_UOM.GetList("");
                    var list = from s in tabs
                               where s.uom_code == Session["uom_code"].ToString()
                               select s;
                    List<VAT_Master> party = new List<VAT_Master>();
                    party = BL_VATMaster.GetvatmasterList("");
                    var list1 = from s1 in party
                               where s1.uom_code == Session["uom_code"].ToString()
                               select s1;
                    if (Session["rtype"].ToString()== "1" || list1.ToList().Count>0)
                    {
                       
                        txtName.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }

                    
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UOM_Master uom = new UOM_Master();
                uom.uom_code = txtcode.Text.ToUpper();
                uom.uom_name = txtName.Text;
                uom.user_id = Session["UserID"].ToString();
               
                string val ;
                if (Session["rtype"].ToString() == "0")
                    val = BL_UOM.Insert(uom);
                else
                    val = BL_UOM.Update(uom);
                if(val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("UOMMasterList");
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
                    return;
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
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
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }
        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");

        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
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
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }
        [WebMethod]
        public static string chkDuplicateUOMCode(Object uomcode)
        {
            int value = BL_User_Mgnt.GetExistsData("UOM_Master", "UOM_Code", uomcode.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateUOMName(Object uomname)
        {
            int value = 0;
           if(uom1!=uomname.ToString())
                value = BL_User_Mgnt.GetExistsData("UOM_Master", "UOM_Name", uomname.ToString());
          
            return value.ToString();
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void RawMaterialTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }

        protected void RawMaterial_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList");
        }
    }
}