using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class IndentForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
                string strPreviousPage = "";
              //  txtDATE.Enabled = false;
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        captiveunit.Value = user.party_captive_unit_name;
                        partycode.Value = user.party_code;
                        txtfinancialyear.Text = user.financial_year;
                        if (user.party_type == "Distillery Unit")
                        {
                            List<Party_Master> partymasters = new List<Party_Master>();
                            partymasters = BL_Party_Master.GetList();
                            var list = (from s in partymasters
                                        where s.party_type_code != "DIS" && s.party_type_code != "d01" && s.party_active == "Active" && s.party_code != "All" && s.party_code != "ALL"
                                        select s);
                            ddUnitName.DataSource = list.ToList();
                            ddUnitName.DataTextField = "Party_name";
                            ddUnitName.DataValueField = "party_code";
                            ddUnitName.DataBind();
                            ddUnitName.Items.Insert(0, "Select");
                            // captiveunit.Value = list.ToList()[list.ToList().Count - 1].financialyear;
                            // ddUnitName.Items.Insert(1, "");

                            List<Product_Master> products = new List<Product_Master>();
                            products = BL_ProductMaster.GetProductMasterList("");
                            var product = (from s in products
                                           where s.product_type_code == "1" || s.product_type_code == "1"
                                           select s);
                            ddMaterial.DataSource = product.ToList();
                            ddMaterial.DataTextField = "product_name";
                            ddMaterial.DataValueField = "product_code";
                            ddMaterial.DataBind();
                            ddMaterial.Items.Insert(0, "Select");
                            if (Session["rtype"].ToString() != "0")
                            {
                                Indent_Form indent = new Indent_Form();
                                indent = BL_IndentForm.GetDetails(Session["Indent_id"].ToString(), Session["Ifinancial_year"].ToString());
                                txtfinancialyear.Text = indent.financial_year;
                                CalendarExtender.SelectedDate = Convert.ToDateTime(indent.indent_date);
                                txtDATE.Text = indent.indent_date;
                                txtdob.Value = indent.indent_date;
                                //   ddUnitName.SelectedValue = indent.captive_unit_name;
                                var list1 = (from s in partymasters
                                             where s.party_code == indent.party_code
                                             select s);
                                txtIndentQty.Text = indent.indent_qty.ToString();
                                ddlCaptive.SelectedValue = indent.is_captive;

                                captiveunit.Value = list1.ToList()[0].party_captive_unit_name;
                                ddUnitName.SelectedValue = indent.captive_unit_name;
                                ddMaterial.SelectedValue = indent.product_code;
                                //////////////Quantity of molasses required///////////////
                                txtCountrySperit.Text = indent.req_cs.ToString();
                                txtRectifiedSpirit.Text = indent.req_rs.ToString();
                                txtPowerAlcohol.Text = indent.req_pa.ToString();
                                txtDistilledSperit.Text = indent.req_ds.ToString();
                                //////////////Quantity of molasses Recieved///////////////
                                txtCountrySperit1.Text = indent.recd_pycs.ToString();
                                txtRectifiedSpirit1.Text = indent.recd_pyrs.ToString();
                                txtPowerAlcohol1.Text = indent.recd_pypa.ToString();
                                txtDistilledSperit1.Text = indent.recd_pyds.ToString();
                                //////////////Quantity of molasses PR Used///////////////
                                txtCountrySperit2.Text = indent.used_pycs.ToString();
                                txtRectifiedSpirit2.Text = indent.used_pyrs.ToString();
                                txtPowerAlcohol2.Text = indent.used_pypa.ToString();
                                txtDistilledSperit2.Text = indent.used_pyds.ToString();
                                //////////////Quantity of molasses cr Used///////////////
                                txtCountrySperit3.Text = indent.used_cycs.ToString();
                                txtRectifiedSpirit3.Text = indent.used_cyrs.ToString();
                                txtPowerAlcohol3.Text = indent.used_cypa.ToString();
                                txtDistilledSperit3.Text = indent.used_cyds.ToString();
                                ////////////Last Menu///////////////
                                txtMolassesDistilled.Text = indent.molasses_distilled.ToString();
                                txtWorkingwastage.Text = indent.working_wastage.ToString();
                                txtTransitwastage.Text = indent.transit_wastage.ToString();
                                txtQtyfirstday.Text = indent.molasses_instock_cy.ToString();
                                txtqty1of31.Text = indent.molasses_recd_cy.ToString();
                                txtQtyMM.Text = indent.molasses_used_cy.ToString();
                                txtQtymolassesstill.Text = indent.molasses_to_be_lifted.ToString();
                                txtqtynov1todec31.Text = indent.molasses_to_consume.ToString();
                                txtInStock.Text = indent.molasses_instock.ToString();
                                txtQtyLifted.Text = indent.molasses_lifted_qty.ToString();
                                txtBalanceallotment.Text = indent.molasses_bal_allotment.ToString();
                                txtStorage.Text = indent.molasses_bal_storage.ToString();
                                indent.party_code = partycode.Value;
                                indent.molasses_indent_reqno = "1";
                                indent.record_id_format = "1";
                                for (int i = 0; i < indent.docs.Count; i++)
                                {
                                    if (i == 0)
                                        dummytable.Visible = false;
                                    dt = (DataTable)ViewState["Records"];
                                    dt.Rows.Add(indent.docs[i].doc_name, indent.docs[i].description, indent.docs[i].doc_path, indent.docs[i].id);
                                    grdAdd.DataSource = dt;
                                    grdAdd.DataBind();
                                }
                                if (Session["rtype"].ToString() == "1")
                                {
                                    txtfinancialyear.ReadOnly = true;
                                    txtDATE.ReadOnly = true;
                                    txtIndentQty.ReadOnly = true;
                                    ddlCaptive.Enabled = false;
                                    ddUnitName.Enabled = false;
                                    ddMaterial.Enabled = false;
                                    //////////////Quantity of molasses required///////////////
                                    txtCountrySperit.ReadOnly = true;
                                    txtDistilledSperit.ReadOnly = true;
                                    txtPowerAlcohol.ReadOnly = true;
                                    txtRectifiedSpirit.ReadOnly = true;
                                    //////////////Quantity of molasses Recieved///////////////
                                    txtCountrySperit1.ReadOnly = true;
                                    txtDistilledSperit1.ReadOnly = true;
                                    txtPowerAlcohol1.ReadOnly = true;
                                    txtRectifiedSpirit1.ReadOnly = true;
                                    //////////////Quantity of molasses PR Used///////////////
                                    txtCountrySperit2.ReadOnly = true;
                                    txtDistilledSperit2.ReadOnly = true;
                                    txtPowerAlcohol2.ReadOnly = true;
                                    txtRectifiedSpirit2.ReadOnly = true;
                                    //////////////Quantity of molasses cr Used///////////////
                                    txtCountrySperit3.ReadOnly = true;
                                    txtDistilledSperit3.ReadOnly = true;
                                    txtPowerAlcohol3.ReadOnly = true;
                                    txtRectifiedSpirit3.ReadOnly = true;
                                    ////////////Last Menu///////////////
                                    txtMolassesDistilled.ReadOnly = true;
                                    txtWorkingwastage.ReadOnly = true;
                                    txtTransitwastage.ReadOnly = true;
                                    txtQtyfirstday.ReadOnly = true;
                                    txtqty1of31.ReadOnly = true;
                                    txtQtyMM.ReadOnly = true;
                                    txtQtymolassesstill.ReadOnly = true;
                                    txtqtynov1todec31.ReadOnly = true;
                                    txtInStock.ReadOnly = true;
                                    txtQtyLifted.ReadOnly = true;
                                    txtBalanceallotment.ReadOnly = true;
                                    txtStorage.ReadOnly = true;
                                    Image1.Visible = false;
                                    btnCancel.Visible = false;
                                    btnSaveasDraft.Visible = false;
                                    btnSubmit.Visible = false;
                                    docs.Visible = false;
                                    foreach (GridViewRow dr1 in grdAdd.Rows)
                                    {
                                        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                        btn.Visible = false;
                                    }
                                }
                            }

                        }
                        else if (user.party_type == "Sugar Mill")
                        {

                        }
                        else
                        {
                            Response.Redirect("~/LoginPage");
                        }
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Database Server Not Connecting");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }

        }
        int Doc_id = 1;
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",","_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";


                }
            }

        }
        [WebMethod]
        public static string CheckDuplicates(Object value)
        {
            string val = "";
            val = BL_IndentForm.GetValues(value.ToString());
            return val;
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["Indent_id"].ToString(), v, partycode.Value);
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                    FileInfo fInfoEvent;
                    fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                    fInfoEvent.Delete();

                }
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdAdd.DataSource = dt1;
                grdAdd.DataBind();
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
           
            Response.Redirect("IndentList.aspx"); 
        }

        protected void btnIndent_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("IndentList.aspx"); 
        }

        protected void btnARM_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AllocationRequestList.aspx");  
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            btnSaveasDraft.Enabled = false;
            if(IsPostBack)
            {
                Indent_Form indent = new Indent_Form();
                indent.financial_year = txtfinancialyear.Text;
                if (txtDATE.Text == "")
                    txtDATE.Text = txtdob.Value;
                indent.indent_date = txtdob.Value;
                indent.indent_qty =Convert.ToDouble( txtIndentQty.Text);
                indent.is_captive = ddlCaptive.SelectedValue;
                if (ddUnitName.SelectedValue == "Select" && ddlCaptive.SelectedValue == "N")
                    indent.captive_unit_name = "NA";
                else
                {
                    ddUnitName.SelectedValue = captiveunit.Value;
                    indent.captive_unit_name = ddUnitName.SelectedValue;
                }
                indent.product_code = ddMaterial.SelectedValue;
                //////////////Quantity of molasses required///////////////
                if (txtCountrySperit.Text == "")
                    txtCountrySperit.Text = "0";
                if (txtRectifiedSpirit.Text == "")
                    txtRectifiedSpirit.Text = "0";
                if (txtPowerAlcohol.Text == "")
                    txtPowerAlcohol.Text = "0";
                if (txtDistilledSperit.Text == "")
                    txtDistilledSperit.Text = "0";
                ////////////////////////////////////
                if (txtCountrySperit1.Text == "")
                    txtCountrySperit1.Text = "0";
                if (txtRectifiedSpirit1.Text == "")
                    txtRectifiedSpirit1.Text = "0";
                if (txtPowerAlcohol1.Text == "")
                    txtPowerAlcohol1.Text = "0";
                if (txtDistilledSperit1.Text == "")
                    txtDistilledSperit1.Text = "0";
                ////////////////////////////////////
                if (txtCountrySperit2.Text == "")
                    txtCountrySperit2.Text = "0";
                if (txtRectifiedSpirit2.Text == "")
                    txtRectifiedSpirit2.Text = "0";
                if (txtPowerAlcohol2.Text == "")
                    txtPowerAlcohol2.Text = "0";
                if (txtDistilledSperit2.Text == "")
                    txtDistilledSperit2.Text = "0";
                ////////////////////////////////////
                if (txtCountrySperit3.Text == "")
                    txtCountrySperit3.Text = "0";
                if (txtRectifiedSpirit3.Text == "")
                    txtRectifiedSpirit3.Text = "0";
                if (txtPowerAlcohol3.Text == "")
                    txtPowerAlcohol3.Text = "0";
                if (txtDistilledSperit3.Text == "")
                    txtDistilledSperit3.Text = "0";
                indent.req_cs= Convert.ToDouble(txtCountrySperit.Text);
                indent.req_rs = Convert.ToDouble(txtRectifiedSpirit.Text);
                indent.req_pa = Convert.ToDouble(txtPowerAlcohol.Text);
                indent.req_ds = Convert.ToDouble(txtDistilledSperit.Text);
                //////////////Quantity of molasses Recieved///////////////
                indent.recd_pycs = Convert.ToDouble(txtCountrySperit1.Text);
                indent.recd_pyrs = Convert.ToDouble(txtRectifiedSpirit1.Text);
                indent.recd_pypa = Convert.ToDouble(txtPowerAlcohol1.Text);
                indent.recd_pyds = Convert.ToDouble(txtDistilledSperit1.Text);
                //////////////Quantity of molasses PR Used///////////////
                indent.used_pycs = Convert.ToDouble(txtCountrySperit2.Text);
                indent.used_pyrs = Convert.ToDouble(txtRectifiedSpirit2.Text);
                indent.used_pypa = Convert.ToDouble(txtPowerAlcohol2.Text);
                indent.used_pyds = Convert.ToDouble(txtDistilledSperit2.Text);
                //////////////Quantity of molasses cr Used///////////////
                indent.used_cycs = Convert.ToDouble(txtCountrySperit3.Text);
                indent.used_cyrs = Convert.ToDouble(txtRectifiedSpirit3.Text);
                indent.used_cypa = Convert.ToDouble(txtPowerAlcohol3.Text);
                indent.used_cyds = Convert.ToDouble(txtDistilledSperit3.Text);
                ////////////Last Menu///////////////
                indent.molasses_distilled = Convert.ToDouble(txtMolassesDistilled.Text);
                indent.working_wastage = Convert.ToDouble(txtWorkingwastage.Text);
                indent.transit_wastage = Convert.ToDouble(txtTransitwastage.Text);
                indent.molasses_instock_cy = Convert.ToDouble(txtQtyfirstday.Text);
                indent.molasses_recd_cy= Convert.ToDouble(txtqty1of31.Text);
                indent.molasses_used_cy= Convert.ToDouble(txtQtyMM.Text);
                indent.molasses_to_be_lifted = Convert.ToDouble(txtQtymolassesstill.Text);
                indent.molasses_to_consume= Convert.ToDouble(txtqtynov1todec31.Text);
                indent.molasses_instock = Convert.ToDouble(txtInStock.Text);
                indent.molasses_lifted_qty = Convert.ToDouble(txtQtyLifted.Text);
                indent.molasses_bal_allotment= Convert.ToDouble(txtBalanceallotment.Text);
                indent.molasses_bal_storage= Convert.ToDouble(txtStorage.Text);
                indent.party_code = partycode.Value;
                indent.molasses_indent_reqno = "1";
                indent.record_id_format = "1";
                indent.record_status = "N";
                indent.user_id = Session["UserID"].ToString();
                int i = 0;
                indent.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    indent.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_IndentForm.Insert(indent);
                else
                {
                    indent.molasses_indent_id = Session["indent_id"].ToString();
                    val = BL_IndentForm.Update(indent);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("IndentList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                Indent_Form indent = new Indent_Form();
                indent.financial_year = txtfinancialyear.Text;
                if (txtDATE.Text == "")
                    txtDATE.Text = txtdob.Value;
                indent.indent_date = txtdob.Value;
                indent.indent_qty = Convert.ToDouble(txtIndentQty.Text);
                indent.is_captive = ddlCaptive.SelectedValue;
                if (ddUnitName.SelectedValue == "Select" && ddlCaptive.SelectedValue == "N")
                    indent.captive_unit_name = "NA";
                else
                {
                    ddUnitName.SelectedValue = captiveunit.Value;
                    indent.captive_unit_name = ddUnitName.SelectedValue;
                }
                indent.product_code = ddMaterial.SelectedValue;
                //////////////Quantity of molasses required///////////////
                indent.req_cs = Convert.ToDouble(txtCountrySperit.Text);
                indent.req_rs = Convert.ToDouble(txtRectifiedSpirit.Text);
                indent.req_pa = Convert.ToDouble(txtPowerAlcohol.Text);
                indent.req_ds = Convert.ToDouble(txtDistilledSperit.Text);
                //////////////Quantity of molasses Recieved///////////////
                indent.recd_pycs = Convert.ToDouble(txtCountrySperit1.Text);
                indent.recd_pyrs = Convert.ToDouble(txtRectifiedSpirit1.Text);
                indent.recd_pypa = Convert.ToDouble(txtPowerAlcohol1.Text);
                indent.recd_pyds = Convert.ToDouble(txtDistilledSperit1.Text);
                //////////////Quantity of molasses PR Used///////////////
                indent.used_pycs = Convert.ToDouble(txtCountrySperit2.Text);
                indent.used_pyrs = Convert.ToDouble(txtRectifiedSpirit2.Text);
                indent.used_pypa = Convert.ToDouble(txtPowerAlcohol2.Text);
                indent.used_pyds = Convert.ToDouble(txtDistilledSperit2.Text);
                //////////////Quantity of molasses cr Used///////////////
                indent.used_cycs = Convert.ToDouble(txtCountrySperit3.Text);
                indent.used_cyrs = Convert.ToDouble(txtRectifiedSpirit3.Text);
                indent.used_cypa = Convert.ToDouble(txtPowerAlcohol3.Text);
                indent.used_cyds = Convert.ToDouble(txtDistilledSperit3.Text);
                ////////////Last Menu///////////////
                indent.molasses_distilled = Convert.ToDouble(txtMolassesDistilled.Text);
                indent.working_wastage = Convert.ToDouble(txtWorkingwastage.Text);
                indent.transit_wastage = Convert.ToDouble(txtTransitwastage.Text);
                indent.molasses_instock_cy = Convert.ToDouble(txtQtyfirstday.Text);
                indent.molasses_recd_cy = Convert.ToDouble(txtqty1of31.Text);
                indent.molasses_used_cy = Convert.ToDouble(txtQtyMM.Text);
                indent.molasses_to_be_lifted = Convert.ToDouble(txtQtymolassesstill.Text);
                indent.molasses_to_consume = Convert.ToDouble(txtqtynov1todec31.Text);
                indent.molasses_instock = Convert.ToDouble(txtInStock.Text);
                indent.molasses_lifted_qty = Convert.ToDouble(txtQtyLifted.Text);
                indent.molasses_bal_allotment = Convert.ToDouble(txtBalanceallotment.Text);
                indent.molasses_bal_storage = Convert.ToDouble(txtStorage.Text);
                indent.party_code = partycode.Value;
              //  indent.molasses_indent_reqno = "1";
                indent.record_id_format = "1";
                indent.record_status = "Y";
                indent.user_id = Session["UserID"].ToString();
                int i = 0;
                indent.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    indent.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_IndentForm.Insert(indent);
                else
                {
                    indent.molasses_indent_id = Session["indent_id"].ToString();
                    val = BL_IndentForm.Update(indent);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("IndentList");
                }
                else
                {
                    btnSubmit.Enabled = true;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];

            Response.Redirect("IndentList.aspx");
        }
    }
}