using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CasteMasterForm : System.Web.UI.Page
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
                List<cm_religion> religion = new List<cm_religion>();
                religion = BL_cm_religion.GetList();
                var list = from s in religion
                           select s;
                DDReligion.DataSource = list.ToList();
                DDReligion.DataTextField = "religion_name";
                DDReligion.DataValueField = "religion_code";
                DDReligion.DataBind();
                DDReligion.Items.Insert(0, "Select");
                List<cm_caste> _caste = new List<cm_caste>();
                _caste = BL_cm_caste.GetList();
                var list1 = _caste.GroupBy(x => x.category_code).Select(y => y.First()).Distinct();
                ddlCastecategory.DataSource = list1.ToList();
                ddlCastecategory.DataTextField = "category_code";
                ddlCastecategory.DataValueField = "category_code";
                ddlCastecategory.DataBind();
                ddlCastecategory.Items.Insert(0, "Select");

                if (rtype != "0")
                {
                    string CasteId = Session["CasteId"].ToString();
                    txtid.Value = CasteId;
                    txtcode.Text = Session["CasteCode"].ToString();
                    List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
                    ads = BL_cm_seiz_AccusedDetails.GetDetails("");
                    var list11 = from s in ads
                               where s.caste_code ==txtcode.Text.Trim()
                               select s;
                    if (list11.ToList().Count > 0)
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        ddlCastecategory.Attributes.Add("disabled", "disabled");
                        DDReligion.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["CasteName"].ToString();
                        DDReligion.SelectedValue = Session["Religion"].ToString();
                        ddlCastecategory.SelectedValue = Session["Category_code"].ToString();
                        txtcode.Text = Session["CasteCode"].ToString();
                        txtid.Value = Session["CasteId"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                        DDReligion.Attributes.Add("disabled", "disabled");
                        ddlCastecategory.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["CasteName"].ToString();
                        txtcode.Text = Session["CasteCode"].ToString();                        
                        DDReligion.SelectedValue = Session["Religion"].ToString(); 
                        txtid.Value = Session["CasteId"].ToString();
                        ddlCastecategory.SelectedValue = Session["Category_code"].ToString();
                    }
                }
                else
                {
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("caste_master"));
                    txtcode.Text = "C" + string.Format("{0:0000}", n+1);
                    Session["CasteId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/CasteMasterList");
        }

     
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            
            cm_caste cm_obj = new cm_caste();
            cm_obj.caste_code = txtcode.Text;
            cm_obj.caste_name = txtName.Text;
            cm_obj.religion_code = DDReligion.SelectedItem.Value;
            cm_obj.user_id = Session["UserID"].ToString();
            cm_obj.category_code = ddlCastecategory.SelectedValue;
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            List<cm_caste> _caste = new List<cm_caste>();
            _caste = BL_cm_caste.GetList();
            var list = from s in _caste
                       where s.caste_name == txtName.Text.Trim()
                       select s;

            if (list.ToList().Count > 0)
            {
                if ((list.ToList()[0].caste_code != txtcode.Text))
                {
                    string message = "Category Name is Already Exists!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                else
                {
                    if (Session["rtype"].ToString() != "0")
                    {
                        cm_obj.caste_master_id = Convert.ToInt32(Session["CasteId"].ToString());
                        if (BL_cm_caste.UpdateCaste(cm_obj))
                        {
                            string message = "Record is Successfully Updated.";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                            Session["UserID"] = Session["UserID"].ToString();
                            Response.Redirect("~/CasteMasterList");
                        }

                        else
                        {
                            btnSave.Enabled = true;
                            string message = "Server Side Error.";
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
            }

            else
            {

                if (BL_cm_caste.InsertCaste(cm_obj))
                {

                    string message = "Record is Successfully Submited.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("~/CasteMasterList");
                }
                else
                {
                    btnSave.Enabled = true;
                    string message = "Server Side Error.";
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/CasteMasterList");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("caste_master", "caste_code", txtcode.Text);
                if (value > 0)
                {
                    string message = "Caste Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtcode.Text = "";
                    txtcode.Focus();
                }
            }
        }
    }
}