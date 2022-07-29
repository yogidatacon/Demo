using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class PartyFinancialYear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            string userid = Session["UserID"].ToString();
            List < Party_Type_Master> partytypes = new List<Party_Type_Master>();
            partytypes = BL_Party_Type_Master.GetList();
            ddpartytype.DataSource = partytypes;
            ddpartytype.DataTextField = "Party_Type_Name";
            ddpartytype.DataValueField = "Party_Type_Code";
            ddpartytype.DataBind();
            ddpartytype.Items.Insert(0, "Select");
           
            if (Session["rtype"].ToString()!="0")
            {
                Financial_Years fin = new Financial_Years();
                fin = BL_Financial_Years.GetDetails(Session["fin_id"].ToString());
                ddpartytype.SelectedValue = fin.party_type_code;
                partytype.Value = fin.party_type_code;
                txtstart.Value = fin.start_date.Substring(0,10).Replace("/","-");
                txtEndDate.Text = fin.end_date.Substring(0, 10).Replace("/", "-"); ;
                txtend.Value = fin.end_date.Substring(0, 10).Replace("/", "-"); ;
                txtStartDate.Text = fin.start_date.Substring(0, 10).Replace("/", "-"); ;
                txtfinyear.Text = fin.financial_year;


                lblwarning.InnerText = fin.financial_year;
                //   lblyear.Text = fin.financial_year.ToString();
                if (Session["rtype"].ToString() == "1")
                {
                    ddpartytype.Enabled = false;
                    Image1.Visible = false;
                    Image2.Visible = false;
                    txtEndDate.ReadOnly = true;
                    txtStartDate.ReadOnly = true;
                    txtfinyear.ReadOnly = true;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                   
                }
            }
        }

        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void producttypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductTypeList");
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

        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }
      
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Financial_Years fin = new Financial_Years();
            fin.party_type_code = partytype.Value;
            fin.start_date = txtstart.Value;
            fin.end_date = txtend.Value;
            fin.financial_year = txtstart.Value.Substring(6, 4)+"-"+txtend.Value.Substring(6,4);
          //  fin.status = ddActive.SelectedValue;
            string val;
            if (Session["rtype"].ToString() == "0")
                val = BL_Financial_Years.Insert(fin);
            else
                val = BL_Financial_Years.Update(fin);
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }
    }
}