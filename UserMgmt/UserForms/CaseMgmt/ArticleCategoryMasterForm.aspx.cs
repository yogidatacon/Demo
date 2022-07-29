using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.Entities;
using Usermngt.BL;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace UserMgmt
{
    public partial class ArticleCategoryMasterForm : System.Web.UI.Page 
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

                if (rtype != "0")
                {
                    string bail_id = Session["ArticleId"].ToString();
                    txtid.Value = bail_id;
                    List<cm_article_subcategory> articlesub = new List<cm_article_subcategory>();
                    articlesub = BL_cm_article_subcategory.GetList();
                    var list = from s in articlesub
                               where s.article_category_code == Session["ArticleCode"].ToString()
                    select s;
                    if (list.ToList().Count > 0)
                    {
                        txtName.Text = Session["ArticleName"].ToString();
                        txtCode.Text = Session["ArticleCode"].ToString();
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["ArticleName"].ToString();
                        txtCode.Text = Session["ArticleCode"].ToString();
                        txtid.Value = Session["ArticleId"].ToString();
                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }
                    if (rtype == "2")
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Text = Session["ArticleName"].ToString();
                        txtCode.Text = Session["ArticleCode"].ToString();
                        txtid.Value = Session["ArticleId"].ToString();

                    }

                }
                else
                {
                    txtCode.Attributes.Add("disabled", "disabled");
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("article_category_master"));
                    if(n<10)
                        txtid.Value = "C0" + (n + 1).ToString();
                    else
                    txtCode.Text ="C"+ (n + 1).ToString();
                    Session["ArticleId"] ="0";
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                  
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ArticleCategoryMasterList");
        }

        //protected void partymaster_Click(object sender, EventArgs e)
        //{
        //    Session["UserID"] = Session["UserID"];
        //    Response.Redirect("ArticleCategoryMasterList");
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            cm_article_category article_category = new cm_article_category();
            article_category.article_category_code = txtCode.Text;
            article_category.article_category_name = txtName.Text;
            article_category.user_id = Session["UserID"].ToString();
            article_category.lastmodified_date = DateTime.Now.ToShortDateString();
            article_category.user_id = Session["UserID"].ToString();
            article_category.record_status = "Y";
            article_category.record_deleted = false;
            List<cm_article_category> article_category1 = BL_cm_article_category.GetList();
            var list = from s in article_category1
                       where s.article_category_name == txtName.Text.Trim()
                       select s;
           
            if (list.ToList().Count>0 )
            {
                if ((list.ToList()[0].article_category_code != txtCode.Text))
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
                        article_category.article_category_master_id = Convert.ToInt32(Session["ArticleId"].ToString());
                        if (BL_cm_article_category.UpdateArticle(article_category))
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
                            Response.Redirect("~/ArticleCategoryMasterList");
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
                    article_category.article_category_master_id = Convert.ToInt32(Session["ArticleId"].ToString());
                    if (BL_cm_article_category.UpdateArticle(article_category))
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
                        Response.Redirect("~/ArticleCategoryMasterList");
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
                    if (BL_cm_article_category.InsertArticleCategory(article_category))
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
                        Response.Redirect("~/ArticleCategoryMasterList");
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
            //}

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ArticleCategoryMasterList");
        }
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text !="")
            {
                int value = BL_User_Mgnt.GetExistsData("article_category_master", "article_category_code", txtCode.Text);
            if (value > 0)
            {
                string message = "Article Category Code  is Already Exists.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                txtCode.Text = "";
                txtCode.Focus();
            }
            }
        }
    }
}