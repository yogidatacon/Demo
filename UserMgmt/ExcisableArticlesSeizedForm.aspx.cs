using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using System.Data;
using System.Text;
using AjaxControlToolkit;
using System.Web.Services;

namespace UserMgmt
{
    public partial class ExcisableArticlesSeizedForm : System.Web.UI.Page
    {
        #region Variables

        List<cm_seiz_ExcisableArticlesSeized> _ExcisableArticlesSeized = new List<cm_seiz_ExcisableArticlesSeized>();
        DataTable dt = new DataTable();
        int Doc_id = 1;

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<cm_article_category> articleCategoryDetails = new List<cm_article_category>();
                articleCategoryDetails = BL_cm_article_category.GetList();

                ddlArticleCategory.DataSource = articleCategoryDetails;
                ddlArticleCategory.DataTextField = "article_category_name";
                ddlArticleCategory.DataValueField = "article_category_code";
                ddlArticleCategory.DataBind();
                ddlArticleCategory.Items.Insert(0, "Select");

                List<cm_article_subcategory> articleSubCategoryDetails = new List<cm_article_subcategory>();
                articleSubCategoryDetails = BL_cm_article_subcategory.GetList();

                ddlArticleSubCategory.DataSource = articleSubCategoryDetails;
                ddlArticleSubCategory.DataTextField = "article_sub_category_name";
                ddlArticleSubCategory.DataValueField = "article_sub_category_code";
                ddlArticleSubCategory.DataBind();
                ddlArticleSubCategory.Items.Insert(0, "Select");


                List<cm_article_name> articlename= new List<cm_article_name>();
               articlename = BL_cm_article_name.GetList();
                ddlarticlename.DataSource = articlename;
                ddlarticlename.DataTextField = "article_name";
                ddlarticlename.DataValueField = "article_name_code";
                ddlarticlename.DataBind();
                ddlarticlename.Items.Insert(0, "Select");

                List<UOM_Master> UOM_MasterDetails = new List<UOM_Master>();
                UOM_MasterDetails = BL_UOM.GetList(string.Empty);

                ddlUnitOfMeasurement.DataSource = UOM_MasterDetails;
                ddlUnitOfMeasurement.DataTextField = "uom_name";
                ddlUnitOfMeasurement.DataValueField = "uom_code";
                ddlUnitOfMeasurement.DataBind();
                ddlUnitOfMeasurement.Items.Insert(0, "Select");

                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                serchid.Visible = true;
                Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString() != "0")
                {
                    string seizureNo = Session["seizureno"].ToString();
                    string tableId = Session["tableId"].ToString();
                    serchid.Visible = false;
                    cm_seiz_ExcisableArticlesSeized obj = new cm_seiz_ExcisableArticlesSeized();
                    obj = BL_cm_seiz_ExcisableArticlesSeized.GetDetails(tableId);
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.manufacturing_date);
                    // txtArticleName.Text = obj.article_name;
                    ddlarticlename.SelectedValue = obj.article_name_code;
                    ddlArticleCategory.SelectedValue = obj.article_category_code?.ToString() ?? string.Empty;
                    ddlArticleSubCategory.SelectedValue = obj.article_sub_category_code;
                    txtManufacturer.Text = obj.manufacturer_code.ToString();
                    ddlUnitOfMeasurement.SelectedValue = obj.uom_code?.ToString() ?? string.Empty;
                    txtQuantity.Text = obj.quantity.ToString();
                    txtPackingSize.Text = obj.packingsize_code.ToString();
                    txtAreaSize.Text = obj.farmingsize;
                    txtRemarks.Text = obj.remarks;
                    txtSaleStateCode.Text = obj.sale_state_code;
                    txtProdStateCode.Text = obj.prod_state_code;
                    txtManufacturingDate.Text = obj.manufacturing_date;
                    txtDifferentLiquor.Text = obj.Different_Liquor;
                    txtBatch.Text = obj.batchno;
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs.Count; i++)
                    {
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(obj.docs[i].doc_name, obj.docs[i].description, obj.docs[i].doc_path, obj.docs[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;

                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (obj.record_status == "Y")
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                            //btnSubmit.Visible = false;
                            idupDocument.Enabled = false;
                            txtDiscription.Enabled = false;
                            btnUpload.Enabled = false;
                        }
                        if ((Session["rtype"].ToString() == "1"))
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        idupDocument.Enabled = false;
                        txtDiscription.Enabled = false;
                        btnUpload.Enabled = false;
                        docs.Visible = false;
                        //btnSubmit.Visible = false;
                        dummytable.Visible = false;
                        ddlarticlename.Enabled = false;
                      //  txtArticleName.Attributes.Add("disabled", "disabled");
                        ddlArticleCategory.Attributes.Add("disabled", "disabled");
                        ddlArticleSubCategory.Attributes.Add("disabled", "disabled");
                        txtManufacturer.Attributes.Add("disabled", "disabled");
                        ddlUnitOfMeasurement.Attributes.Add("disabled", "disabled");
                        txtQuantity.Attributes.Add("disabled", "disabled"); 
                        txtPackingSize.Attributes.Add("disabled", "disabled");
                        txtAreaSize.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("disabled", "disabled");
                        Image1.Visible = false;
                        txtBatch.Attributes.Add("disabled", "disabled");
                        txtSaleStateCode.Attributes.Add("disabled", "disabled");
                        txtProdStateCode.Attributes.Add("disabled", "disabled");
                        txtManufacturingDate.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcisableArticlesSeizedList");
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                cm_seiz_ExcisableArticlesSeized cm_obj = new cm_seiz_ExcisableArticlesSeized();
                cm_obj.article_name_code = ddlarticlename.SelectedValue; //txtArticleName.Text;
                cm_obj.article_category_code = ddlArticleCategory.SelectedValue;
                cm_obj.article_sub_category_code = ddlArticleSubCategory.SelectedValue;
                cm_obj.manufacturer_code = (txtManufacturer.Text);
                cm_obj.uom_code = ddlUnitOfMeasurement.SelectedValue;
                cm_obj.quantity = (txtQuantity.Text);
                cm_obj.packingsize_code = (txtPackingSize.Text);
                cm_obj.farmingsize = txtAreaSize.Text;
                cm_obj.remarks = txtRemarks.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.record_status = "Y";
                cm_obj.batchno = txtBatch.Text;
                cm_obj.Different_Liquor = txtDifferentLiquor.Text;
                cm_obj.manufacturing_date = txtManufacturingDate.Text;
                cm_obj.prod_state_code = txtProdStateCode.Text;
                cm_obj.sale_state_code = txtSaleStateCode.Text;
                cm_obj.record_deleted = false;
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                bool val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_cm_seiz_ExcisableArticlesSeized.InsertExcisableArticlesSeized(cm_obj);
                else
                {
                    cm_obj.seizure_excisable_articles_id = Convert.ToInt32(Session["tableId"].ToString());
                    val = BL_cm_seiz_ExcisableArticlesSeized.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ExcisableArticlesSeizedList");
                }
                else
                {
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ExcisableArticlesSeizedList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_ExcisableArticlesSeized cm_obj = new cm_seiz_ExcisableArticlesSeized();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.article_name_code = ddlarticlename.SelectedValue; //txtArticleName.Text;
                cm_obj.article_category_code = ddlArticleCategory.SelectedValue;
                cm_obj.article_sub_category_code = ddlArticleSubCategory.SelectedValue;
                cm_obj.manufacturer_code = (txtManufacturer.Text);
               
                cm_obj.uom_code = ddlUnitOfMeasurement.SelectedValue;
                cm_obj.quantity = (txtQuantity.Text);
                cm_obj.Different_Liquor = txtDifferentLiquor.Text;
                cm_obj.packingsize_code = (txtPackingSize.Text);
                cm_obj.farmingsize = txtAreaSize.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                cm_obj.remarks = txtRemarks.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.batchno = txtBatch.Text;
                cm_obj.manufacturing_date = txtManufacturingDate.Text;
                if (cm_obj.manufacturing_date == "")
                    cm_obj.manufacturing_date = null;
                cm_obj.prod_state_code = txtProdStateCode.Text;
                cm_obj.sale_state_code = txtSaleStateCode.Text;
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                int i = 0;
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }

                bool val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))                    
                    val = BL_cm_seiz_ExcisableArticlesSeized.InsertExcisableArticlesSeized(cm_obj);
                else
                {
                    cm_obj.seizure_excisable_articles_id = Convert.ToInt32(Session["tableId"].ToString());
                    val = BL_cm_seiz_ExcisableArticlesSeized.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("~/ExcisableArticlesSeizedList");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
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
                Session["UserID"] = Session["UserID"];
                // string seizureNo=
                string _Article = txtArticle.Text;
                if (_Article.Trim() != "")
                {
                    _ExcisableArticlesSeized = new List<cm_seiz_ExcisableArticlesSeized>();
                    _ExcisableArticlesSeized = BL_cm_seiz_ExcisableArticlesSeized.ArticleSearch(_Article);
                    grdExcisableArticlesView.DataSource = _ExcisableArticlesSeized.ToList();
                    grdExcisableArticlesView.DataBind();
                    grdExcisableArticlesView.Visible = true;
                }
                
                else
                {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Enter Artical Name");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    
                }
            }
        }

        
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //if (idupDocument.HasFile)
                //{
                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    string[] filetype = fileName.Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                    string path = Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";
               // }
            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                string a = Session["rtype1"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype1"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["seizureNo"].ToString(), v);
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
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
                Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void chselect_CheckedChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                
                foreach (GridViewRow row in grdExcisableArticlesView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string catcode = (row.Cells[0].FindControl("lblArticleCategory") as Label).Text;
                            string scatcode = (row.Cells[1].FindControl("lblArticleSubCategory") as Label).Text;
                            string acode = (row.Cells[2].FindControl("lblArticleNameCode") as Label).Text;
                            ddlArticleCategory.SelectedValue = catcode;
                            ddlArticleSubCategory.SelectedValue = scatcode;
                            ddlarticlename.SelectedValue = acode;
                            break;
                        }
                        else
                        {
                            ddlArticleCategory.SelectedValue = "Select";
                            ddlArticleSubCategory.SelectedValue = "Select";
                            ddlarticlename.SelectedValue = "Select";
                        }
                    }
                }
               
            }
        }

        protected void grdExcisableArticlesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExcisableArticlesView.PageIndex = e.NewPageIndex;
            string _Article = txtArticle.Text;
            _ExcisableArticlesSeized = new List<cm_seiz_ExcisableArticlesSeized>();
            _ExcisableArticlesSeized = BL_cm_seiz_ExcisableArticlesSeized.ArticleSearch(_Article);
            grdExcisableArticlesView.DataSource = _ExcisableArticlesSeized.ToList();
            grdExcisableArticlesView.DataBind();
            grdExcisableArticlesView.Visible = true;
        }
    }
}