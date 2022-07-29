using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ServerConfigs : System.Web.UI.Page
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
                //  string user_id = "admin";
                if (rtype != "0")
                {

                    if (rtype == "1")
                    {
                        txtCode.Text = Session["server_code"].ToString();
                        txtName.Text = Session["server_user"].ToString();
                        txtPassword.Text = Session["server_password"].ToString();
                        txtdomain.Text = Session["server_domain"].ToString();
                        txturl.Text = Session["server_url"].ToString();
                        txtPassword.Enabled = false;
                        cbShowHidePassword1.Visible = false;
                        txtCode.Enabled = false;
                        txtName.Enabled = false;
                        txtdomain.Enabled = false;
                        txturl.Enabled = false;
                        
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }

                }
                else
                {

                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("SeverConfigList.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeverConfigList.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                string password = encryption(txtPassword.Text);
                Server_Configs server = new Server_Configs();
                server.server_code = txtCode.Text;
                server.server_user = txtName.Text;
                server.server_password =password ;
                server.server_domain = txtdomain.Text;
                server.server_url = txturl.Text;
                server.user_id = user_id;
                if (BL_SeverConfig.InsertSever(server))
                {

                    string message = "Record is  Sucessfuly Submitted.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());


                    Session["UserID"] = user_id;
                    Response.Redirect("SeverConfigList.aspx");
                }
                else
                {
                    string message = "Server Error";
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
                string password = encryption(txtPassword.Text);
                Server_Configs server = new Server_Configs();
                server.server_code = txtCode.Text;
                server.server_user = txtName.Text;
                server.server_password =password;
                server.server_domain = txtdomain.Text;
                server.server_url = txturl.Text;
                server.user_id = user_id;
                //server.config_master_id=
                if (BL_SeverConfig.UpdateServer(server))
                {
                    string message = "Successfully Updated";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();

                    Response.Redirect("SeverConfigList.aspx");
                }
                else
                {
                    string message = "Server Side Error";
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

        public string encryption(String password)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
    }
}