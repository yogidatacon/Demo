using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.Entities;
using Usermngt.BL;

namespace UserMgmt
{
    public partial class ArticleNameMasterForm : System.Web.UI.Page
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
                List<cm_article_category> articlecategory = new List<cm_article_category>();
                articlecategory = BL_cm_article_category.GetList();
                var list = (from s in articlecategory
                           select s).OrderBy(o => o.article_category_name);
                ddlArticleCategory.DataSource = list.ToList();
                ddlArticleCategory.DataTextField = "article_category_name";
                ddlArticleCategory.DataValueField = "article_category_code";
                ddlArticleCategory.DataBind();
                ddlArticleCategory.Items.Insert(0, "Select");
                List<cm_article_subcategory> cm_article_subcategory = new List<cm_article_subcategory>();
                cm_article_subcategory = BL_cm_article_subcategory.GetList();
                var list1 = (from s in cm_article_subcategory
                             select s).OrderBy(o => o.article_sub_category_name);
                ddlArticleSubCategory.DataSource = list1.ToList();
                ddlArticleSubCategory.DataTextField = "article_sub_category_name";
                ddlArticleSubCategory.DataValueField = "article_sub_category_code";
                ddlArticleSubCategory.DataBind();
                ddlArticleSubCategory.Items.Insert(0, "Select");




                if (rtype != "0")
                {
                    string bail_id = Session["ArticleNId"].ToString();
                    txtid.Value = bail_id;
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("article&"+ Session["ArticleNCode"].ToString()));
                   
                    if (n> 0)
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        ddlArticleCategory.Attributes.Add("disabled", "disabled");
                        ddlArticleSubCategory.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "1")
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        ddlArticleSubCategory.SelectedValue = Session["ArticleNsubCode"].ToString();
                        ddlArticleCategory.SelectedValue = Session["Article1code"].ToString();
                        txtName.Text = Session["ArticleNName"].ToString();
                        txtcode.Text = Session["ArticleNCode"].ToString();
                        txtid.Value = Session["ArticleNId"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                        ddlArticleCategory.Enabled = false;
                        ddlArticleSubCategory.Enabled = false;
                    }
                    if (rtype == "2")
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        ddlArticleSubCategory.SelectedValue = Session["ArticleNsubCode"].ToString();
                        ddlArticleCategory.SelectedValue = Session["Article1code"].ToString();
                        txtName.Text = Session["ArticleNName"].ToString();
                        txtcode.Text = Session["ArticleNCode"].ToString();
                        txtid.Value = Session["ArticleNId"].ToString();

                    }

                }
                else
                {
                    txtcode.Attributes.Add("disabled", "disabled");
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("article_name_master"));
                    txtcode.Text = "A" + string.Format("{0:0000}",(n + 1));
                    Session["ArticleNId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

       
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ArticleNameMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_article_name article_name = new cm_article_name();
            article_name.article_name_code = txtcode.Text;
            article_name.article_name = txtName.Text;
            article_name.article_category_code = ddlArticleCategory.SelectedValue;
            article_name.article_sub_category_code = ddlArticleSubCategory.SelectedValue;
            article_name.lastmodified_date = DateTime.Now.ToShortDateString();
            article_name.user_id = Session["UserID"].ToString();
            article_name.record_status = "Y";
            article_name.record_deleted = false;
            List<cm_article_name> _article_name = new List<cm_article_name>();
            _article_name = BL_cm_article_name.GetList();
            var list = (from s in _article_name where s.article_name== txtName.Text.Trim() && s.article_category_code==ddlArticleCategory.SelectedValue && s.article_sub_category_code==ddlArticleSubCategory.SelectedValue
            select s);
            if (list.ToList().Count > 0)
            {
                if ((list.ToList()[0].article_category_code != txtcode.Text))
                {
                    string message = "Artical name i already exists";
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
                        article_name.article_name_master_id = Convert.ToInt32(Session["ArticleNId"].ToString());
                        if (BL_cm_article_name.UpdateArticle(article_name))
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
                            Response.Redirect("~/ArticleNameMasterList");
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
                if (Session["rtype"].ToString() != "0")
                {
                    article_name.article_name_master_id = Convert.ToInt32(Session["ArticleNId"].ToString());
                    if (BL_cm_article_name.UpdateArticle(article_name))
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
                        Response.Redirect("~/ArticleNameMasterList");
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
                else
                {
                    if (BL_cm_article_name.InsertArticleName(article_name))
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
                        Response.Redirect("~/ArticleNameMasterList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ArticleNameMasterList");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("article_name_master", "article_name_code", txtcode.Text);
                if (value > 0)
                {
                    string message = "Article Name Code  is Already Exists.";
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
        protected void ddlArticleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                List<cm_article_subcategory> articlesudcategory = new List<cm_article_subcategory>();
                articlesudcategory = BL_cm_article_subcategory.GetList();
                var list1 = from s in articlesudcategory
                            where s.article_category_code == ddlArticleCategory.SelectedValue
                            select s;
                ddlArticleSubCategory.DataSource = list1.ToList();
                ddlArticleSubCategory.DataTextField = "article_sub_category_name";
                ddlArticleSubCategory.DataValueField = "article_sub_category_code";
                ddlArticleSubCategory.DataBind();
                ddlArticleSubCategory.Items.Insert(0, "Select");
            }
        }
    }
}