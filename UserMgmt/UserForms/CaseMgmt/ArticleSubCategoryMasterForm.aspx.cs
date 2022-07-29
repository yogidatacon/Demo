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
    public partial class ArticleSubCategoryMasterForm : System.Web.UI.Page
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
                           select s).OrderBy(o=>o.article_category_name) ;
              ddlArticleCategory .DataSource = list.ToList();
                ddlArticleCategory.DataTextField = "article_category_name";
                ddlArticleCategory.DataValueField = "article_category_code";
                ddlArticleCategory.DataBind();
                ddlArticleCategory.Items.Insert(0, "Select");


                if (rtype != "0")
                {
                    string bail_id = Session["ArticlesubId"].ToString();
                    txtid.Value = bail_id;
                    List<cm_article_name> articlename = new List<cm_article_name>();
                    articlename = BL_cm_article_name.GetList();
                    var list2 = from s in articlename
                                where s.article_sub_category_code == Session["ArticlesubCode"].ToString()
                    select s;
                    if (list2.ToList().Count > 0)
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        ddlArticleCategory.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["ArticlesubName"].ToString();
                        txtcode.Text = Session["ArticlesubCode"].ToString();
                        txtid.Value = Session["ArticlesubId"].ToString();
                        ddlArticleCategory.SelectedValue = Session["Article1code"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                        ddlArticleCategory.Enabled = false;
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["ArticlesubName"].ToString();
                        txtcode.Text = Session["ArticlesubCode"].ToString();
                        txtid.Value = Session["ArticlesubId"].ToString();
                        ddlArticleCategory.SelectedValue = Session["Article1code"].ToString();

                    }

                }
                else
                {
                    txtcode.Attributes.Add("disabled", "disabled");
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("article_sub_category_master"));
                    txtcode.Text = "SC" + string.Format("{0:000}", (n + 1));
                    Session["ArticlesubId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ArticleSubCategoryMasterLIst");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_article_subcategory article_subcategory = new cm_article_subcategory();
            article_subcategory.article_sub_category_code = txtcode.Text;
            article_subcategory.article_sub_category_name = txtName.Text;
            article_subcategory.article_category_code = ddlArticleCategory.SelectedValue;
            article_subcategory.lastmodified_date = DateTime.Now.ToShortDateString();
            article_subcategory.user_id = Session["UserID"].ToString();
            article_subcategory.record_status = "Y";
            article_subcategory.record_deleted = false;
            List<cm_article_subcategory> article_subcategory1 = new List<cm_article_subcategory>();
            article_subcategory1 = BL_cm_article_subcategory.GetList();
            var list2 = from s in article_subcategory1
                        where s.article_sub_category_name == txtName.Text.Trim() && s.article_category_code==ddlArticleCategory.SelectedValue
                        select s;
            if (list2.ToList().Count > 0)
            {
                if ((list2.ToList()[0].article_category_code != txtcode.Text))
                {
                    string message = "Sub Category Name is Already Exists!";
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
                        article_subcategory.article_sub_category_master_id = Convert.ToInt32(Session["ArticlesubId"].ToString());
                        if (BL_cm_article_subcategory.UpdateArticlesub(article_subcategory))
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
                            Response.Redirect("~/ArticleSubCategoryMasterLIst");
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
                    article_subcategory.article_sub_category_master_id = Convert.ToInt32(Session["ArticlesubId"].ToString());
                    if (BL_cm_article_subcategory.UpdateArticlesub(article_subcategory))
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
                        Response.Redirect("~/ArticleSubCategoryMasterLIst");
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
                    if (BL_cm_article_subcategory.InsertArticleSubCategory(article_subcategory))
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
                        Response.Redirect("~/ArticleSubCategoryMasterLIst");
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
            Response.Redirect("~/ArticleSubCategoryMasterLIst");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("article_sub_category_master", "article_sub_category_code", txtcode.Text);
                if (value > 0)
                {
                    string message = "Article Sub Category  Code  is Already Exists.";
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