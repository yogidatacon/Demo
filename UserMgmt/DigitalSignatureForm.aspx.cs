using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class DigitalSignatureForm : System.Web.UI.Page
    {
        List<DigitalSignature> DigitalSignature = new List<DigitalSignature>();
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
                List<DigitalSignature> f82 = new List<DigitalSignature>();
                f82 = BL_DigitalSignature.GetEmpList();
               
                var distinctKeys = f82.Select(s => new { s.role_name, s.role_name_code })
                            .Distinct();
                ddlRoleName.DataSource = distinctKeys.ToList();
                ddlRoleName.DataTextField = "role_name";
                ddlRoleName.DataValueField = "role_name_code";
                ddlRoleName.DataBind();
                ddlRoleName.Items.Insert(0, "Select");
                if (rtype != "0")
                {
                    DigitalSignature digi = new DigitalSignature();
                    digi= BL_DigitalSignature.GetDetails(Session["Digi_id"].ToString());

                    if (rtype == "1")
                    {
                     
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        ddlRoleName.SelectedValue = digi.role_name;
                        txtDigitalSignatureID.Text = digi.dongle_id;
                        string valu = Dycrypt(digi.dongle_password);
                        txtpassword.Attributes.Add("value", valu);
                        txtpassword.Attributes.Add("value", valu);
                        ddlName.SelectedValue =digi.digi_user_id;
                        txtid.Value =digi.digi_user_id;
                        ddlRoleName.Enabled = false;
                        txtpassword.ReadOnly = true;
                        txtDigitalSignatureID.ReadOnly = true;
                        ddlName.Enabled = false;
                    }
                    if (rtype == "2")
                    {
                        ddlRoleName.SelectedValue = digi.role_name_code;
                        ddlRoleName_SelectedIndexChanged(sender, null);
                        txtDigitalSignatureID.Text = digi.dongle_id;
                        string valu = Dycrypt(digi.dongle_password);
                        txtpassword.Attributes.Add("value", valu);
                        txtrepassword.Attributes.Add("value", valu);
                        ddlName.SelectedValue = digi.dongle_userid.ToString();
                        ddlName_SelectedIndexChanged(sender, null);
                        txtid.Value = digi.digi_user_id;
                        txtSA.Text = digi.certyfing_authority;
                      //  ddlRoleName.Enabled = false;
                      //  txtpassword.ReadOnly = true;
                      //  txtDigitalSignatureID.ReadOnly = true;
                     //   ddlName.Enabled = false;
                    }
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    //txtid.Value = (n + 1).ToString();
                   // Session["Dsid"] = txtid.Value;
                    btnSubmit.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        private string Dycrypt(string dongle_password)
        {
            
          //  string val = encryption(dongle_password);
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(dongle_password.ToString());
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }


        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("DigitalSignatureList");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                DigitalSignature obj = new DigitalSignature();
                obj.dongle_userid = Convert.ToInt32(ddlName.SelectedValue);
                obj.dongle_id = txtDigitalSignatureID.Text;
                obj.role_name_code = ddlRoleName.SelectedValue;
                obj.empid = txtEmpID.Text;
                obj.certyfing_authority = txtSA.Text;
                obj.user_id = Session["UserID"].ToString();
                obj.dongle_password = encryption(txtpassword.Text); 
                if (Session["rtype"].ToString() != "0")
                {
                    obj.digital_signature_id = Convert.ToInt32(Session["Digi_id"].ToString());
                    if (BL_DigitalSignature.UpdateDigitalSignature(obj))
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
                        Response.Redirect("DigitalSignatureList");
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
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

                    if (BL_DigitalSignature.InserDigitalSignature(obj))
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
                        Response.Redirect("DigitalSignatureList");
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
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
        public string encryption(String password)
        {

            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(password);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
          
        }
        protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (ddlName.SelectedValue != "Select")
                {
                    f82 = BL_DigitalSignature.GetEmpList();
                    var list1 = from s in f82
                                where s.user_registration_id == Convert.ToInt32(ddlName.SelectedValue.ToString())
                                select s;
                 
                    txtUserID.Text = list1.ToList()[0].user_id;
                    if (list1.ToList()[0].empid != "0")
                        txtEmpID.Text= list1.ToList()[0].empid;


                }

           
        }
       static public  List<DigitalSignature> f82 = new List<DigitalSignature>();
        protected void ddlRoleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (ddlRoleName.SelectedValue != "Select")
                {
                    f82 = BL_DigitalSignature.GetEmpList();
                    var list1 = from s in f82 where s.role_name_code==ddlRoleName.SelectedValue
                                select s;
                    ddlName.DataSource = list1.ToList();
                    ddlName.DataTextField = "emp_name";
                    ddlName.DataValueField = "user_registration_id";
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, "Select");
                }
           
        }
    }
}