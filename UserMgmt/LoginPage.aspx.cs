using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class LoginPage : System.Web.UI.Page
    {
        //static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              

            }

        }
        /// <summary>
        /// verification of UserId and password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            //AntiForgery.Validate();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString());
            try
            {
                conn.Open();
                string UserName =txtusername.Value;
                string password = txtuserpassword.Value;

                password = encryption(password);

                //var command12 = new NpgsqlCommand("select user_id as LoginStatus from exciseautomation.user_registration where user_id='" + UserName + "' and password='" + password + "'", conn);
                //var command12 = new NpgsqlCommand("select user_id as LoginStatus,user_name,role_name_code from exciseautomation.user_registration where user_id=@UserName and user_password=@password", conn);
                var command12 = new NpgsqlCommand("select a.*,c.department_name, b.district_name,c.department_master_id,b.district_master_id from exciseautomation.user_registration  a left join exciseautomation.district_master  b on a.district_code=b.district_code left join exciseautomation.department_master  c on a.department_code=c.department_code where a.user_id=@UserName and a.user_password=@password", conn);

                NpgsqlParameter UseriD = new NpgsqlParameter("@UserName", NpgsqlTypes.NpgsqlDbType.Text, 50);
                UseriD.Value = UserName;

                NpgsqlParameter Pwd = new NpgsqlParameter("@password", NpgsqlTypes.NpgsqlDbType.Text, 50);
                Pwd.Value = password;
                command12.Parameters.Add(UseriD);
                command12.Parameters.Add(Pwd);



                NpgsqlDataReader pgreader12 = command12.ExecuteReader();

                DataTable dt12 = new DataTable();
                dt12.Load(pgreader12);
                if (dt12.Rows.Count > 0)
                {
                    loginerror.Visible = false;
                    Session["UserID"] = UserName.ToString();
                    Session["Username"] = dt12.Rows[0]["User_Name"].ToString();
                    Session["role_name_code"]=dt12.Rows[0]["role_name_code"].ToString();
                    Session["user_Reg_Id"] = dt12.Rows[0]["user_registration_id"].ToString(); //dr.GetInt32(dr.GetOrdinal("user_registration_id"))
                    Session["Dist_id"] = dt12.Rows[0]["district_master_id"].ToString();
                    Session["Dept_id"] = dt12.Rows[0]["department_master_id"].ToString(); 
                    Session["district_name"] =dt12.Rows[0]["district_name"].ToString();
                    Session["depart_name"] = dt12.Rows[0]["department_name"].ToString();
                    Session["email_Id"] = dt12.Rows[0]["email_id"].ToString();
                    //    Response.Redirect("~/RMR_GrainBased.aspx?userid=" + Session["UserID"].ToString());
                    if (Session["role_name_code"].ToString() == "101"|| Session["role_name_code"].ToString() == "60")
                    {
                        Response.Redirect("~/ReceivingSectionForm.aspx");
                    }
                    else if (Session["role_name_code"].ToString() == "102"  || Session["role_name_code"].ToString() == "63")
                    {
                        Response.Redirect("~/LabTechnicianList.aspx");
                    }
                    else if (Session["role_name_code"].ToString() == "103" || Session["role_name_code"].ToString() == "62")
                    {
                        Response.Redirect("~/LabInchargeLogin.aspx");
                    }
                    else if (Session["role_name_code"].ToString() == "104" || Session["role_name_code"].ToString() == "61")
                    {
                        Response.Redirect("~/DistrictOfficerLogin.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/User_Mgmt?userid=" + Session["UserID"].ToString());
                    }

                }

                else
                {

                    //loginerror.Visible = true;
                    //loginerror.Text = "* Invalid Username or Password !!!!";
                    string script = "alert(\"* Invalid Username or Password !!!!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }

            }

            catch (Exception ex)
            {
                //shows error message
                Response.Write("Contact Admin : " + ex.Message.ToString());
            }

            finally { conn.Close(); conn.Dispose(); }


        }
        /// <summary>
        /// this method encrypts the Password to enhance the confidentiality of credentials
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>

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

        /// <summary>
        /// Redirects to Forgot password Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnForgotPassword_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword");
        }
    }
}