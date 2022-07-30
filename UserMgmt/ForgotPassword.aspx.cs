using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string userid = "";
       // string district = "";
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

                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                //conn.Open();

                if (userid == "admin")
                {


                }
                if (userid != "admin")
                {

                }
                //conn.Close();
            }
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
        }

        protected void ddlArticleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lblSeizureNo_Click(object sender, EventArgs e)
        {


        }

        protected void sortByRecent_Click(object sender, EventArgs e)
        {

        }

        protected void lblSeizureNo_Click1(object sender, EventArgs e)
        {

        }

        protected void grdArticleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdArticleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void lnkDashBoard_Click(object sender, EventArgs e)
        {

        }

         

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://prohibitionbihar.in/iemss1/LOGINIEMS.ASPX#!");
        }


        protected void lnkSeizureList_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCaseMaster_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Demo of github working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Encrypts the password to enhance confidentiality of passswords
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        //protected void btnOtp_Click(object sender, EventArgs e)
        //{
        //    string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
        //    NpgsqlConnection conn = new NpgsqlConnection(connstring);
        //    conn.Open();

        //    string mobileno = "";
        //    string emailid = "";

        //    string userid = Encrypt(txtUserId.Value,true);

        //    var userid_check_cmd = new NpgsqlCommand("SELECT user_id,email_id,mobile from exciseautomation.user_registration where user_id='"+txtUserId.Value+"'", conn);
        //    NpgsqlDataReader userid_pgreader = userid_check_cmd.ExecuteReader();
        //    DataTable dt_userid = new DataTable();
        //    dt_userid.Load(userid_pgreader);

        //    mobileno = dt_userid.Rows[0]["mobile"].ToString();
        //    emailid= dt_userid.Rows[0]["email_id"].ToString();

        //    string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        //    string numbers = "1234567890";

        //    string characters = numbers;

        //    characters += alphabets + small_alphabets + numbers;

        //    int length = 5;
        //    string otp = string.Empty;
        //    for (int i = 0; i < length; i++)
        //    {
        //        string character = string.Empty;
        //        do
        //        {
        //            int index = new Random().Next(0, characters.Length);
        //            character = characters.ToCharArray()[index].ToString();
        //        } while (otp.IndexOf(character) != -1);
        //        otp += character;
        //    }

        //    if (1 == 1)
        //    {

        //        sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BHRGOV", mobileno , otp, "584fdb0f-6287-490f-a071-6102880a9327");
        //    }

        //    //if (1 == 1)
        //    //{
        //    //    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BHRGOV", dt.Rows[0]["mobilenos"].ToString() + ",9939192110" + via_mobileno, messgae, "584fdb0f-6287-490f-a071-6102880a9327");
        //    //}

        //    string email_otp = string.Empty;
        //    for (int i = 0; i < length; i++)
        //    {
        //        string character = string.Empty;
        //        do
        //        {
        //            int index = new Random().Next(0, characters.Length);
        //            character = characters.ToCharArray()[index].ToString();
        //        } while (email_otp.IndexOf(character) != -1);
        //        email_otp += character;
        //    }

        //    string from_email_id = "controlroom@prohibitionbihar.in";
        //    using (MailMessage mm = new MailMessage(from_email_id, emailid))
        //    {
        //        mm.Subject = "This is an email from the Prohibition and Excise Department : Forgot Password Alert : ";
        //        mm.Body = "Dear Sir/Madam,"+"\n"+"Please use this OTP to reset your password : "+ email_otp;

        //        mm.IsBodyHtml = false;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtpout.asia.secureserver.net";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential("controlroom@prohibitionbihar.in", "IEMS@123");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;

        //        smtp.Send(mm);
        //    }




        //    var command = new NpgsqlCommand("SELECT (NOW()::timestamp+interval '10 minutes') as ExpirationTime;", conn);
        //    NpgsqlDataReader pgreader = command.ExecuteReader();

        //    DataTable dt = new DataTable();
        //    dt.Load(pgreader);

        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(dt);

        //    string d = Convert.ToDateTime(dt.Rows[0]["ExpirationTime"]).ToString("yyyy-MM-dd hh:mm:ss");
        //    var command_update_expirationtime = new NpgsqlCommand("update exciseautomation.user_registration set linkexpirationtime='" + d + "',emailotp='" + email_otp + "',smsotp='" + otp + "' where user_id='" + txtUserId.Value + "'", conn);
        //    command_update_expirationtime.ExecuteScalar();

        //    conn.Close();

        //    txtNewPassword.Visible = true;
        //    txtConfirmPassword.Visible = true;
        //    btnSubmit.Visible = true;
        //    btnCancel.Visible = true;

        //    txtUserId.Disabled = true;
        //    btnGenerateOtp.Enabled = false;
        //    txtEmailOtp.Visible = true;
        //    txtSMSOtp.Visible = true;

        //    lblConfirmPassword.Visible = true;
        //    lblNewPassword.Visible = true;
        //    lblEmailOtp.Visible = true;
        //    lblSMSOtp.Visible = true;
        //    btnGenerateOtp.Visible = false;

        //    string script = "alert(\"OTP sent to mail and mobile!!!!!\");";
        //    ScriptManager.RegisterStartupScript(this, GetType(),
        //                          "ServerControlScript", script, true);



        //}

        protected void btnUserCheck_Click(object sender, EventArgs e)
        {

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            var userid_check_cmd = new NpgsqlCommand("SELECT user_id from exciseautomation.user_registration where user_id='" + txtUserId.Value + "'", conn);
            NpgsqlDataReader userid_pgreader = userid_check_cmd.ExecuteReader();
            if (userid_pgreader.HasRows)
            {
                txtNewPassword.Visible = true;
                txtConfirmPassword.Visible = true;
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                txtCaptcha.Visible = true;
                Captcha1.Visible = true;
                RequiredFieldValidatorNewpwd.Visible = true;
                RequiredFieldValidatorConpwd.Visible = true;
                RequiredFieldValidatorCaptcha.Visible = true;
                btnBack.Visible = false;         
                txtUserId.Disabled = true;
                btnCheckUser.Enabled = false;
                txtEmailOtp.Visible = false;
                txtSMSOtp.Visible = false;

                lblConfirmPassword.Visible = true;
                lblNewPassword.Visible = true;
                lblEmailOtp.Visible = false;
                lblSMSOtp.Visible = false;
                btnCheckUser.Visible = false;
                LblEnterCaptcha.Visible = true;
            }
            else
            {
                string script = "alert(\"User Not Exist!!!!!\");";
                  ScriptManager.RegisterStartupScript(this, GetType(),"ServerControlScript", script, true);

            }
            conn.Close();



        }

        public String sendBulkSMS(String username, String password, String senderid, String mobileNos, String message, String secureKey)
        {
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequest");
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            // System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            Console.Write(NewsecureKey);
            Console.Write(encryptedPassword);

            String smsservicetype = "bulkmsg"; // for bulk msg

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +

             "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

             "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

             "&content=" + HttpUtility.UrlEncode(message.Trim()) +

             "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) +

             "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +

            "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim());
            Console.Write(query);

            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            String Status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            String responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;



        }

        protected String encryptedPasswod(String password)
        {

            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
            byte[] pp = sha1.ComputeHash(encPwd);
            // static string result = System.Text.Encoding.UTF8.GetString(pp);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {

                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();

        }
        
        /// <summary>
        /// Method to Generate hash code  
        /// </summary>
        /// <param name= "secure_key">your last generated Secure_key </param>

        protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
            byte[] sec_key = sha1.ComputeHash(genkey);

            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < sec_key.Length; i++)
            {
                sb1.Append(sec_key[i].ToString("x2"));
            }
            return sb1.ToString();
        }

        public String sendUnicodeOTPSMS(String username, String password, String senderid, String mobileNos, String UnicodeOTPmsg, String secureKey)
        {
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequest");
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";
          //  System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String U_Convertedmessage = "";

            foreach (char c in UnicodeOTPmsg)
            {
                int j = (int)c;
                String sss = "&#" + j + ";";
                U_Convertedmessage = U_Convertedmessage + sss;
            }
            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), U_Convertedmessage.Trim(), secureKey.Trim());


            String smsservicetype = "unicodeotpmsg"; // for unicode msg
            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(U_Convertedmessage.Trim()) +
                "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) +
                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
                "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim());


            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            String Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            String responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
       {

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            if (txtConfirmPassword.Text == txtNewPassword.Text)
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (Captcha1.UserValidated)
                {
                    string pwd = encryption(txtNewPassword.Text);
                    var passwordupdate = new NpgsqlCommand("update exciseautomation.user_registration set user_password='" + pwd + "' where user_id='" + txtUserId.Value + "'", conn);
                    passwordupdate.ExecuteScalar();
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "alert", "alert('Password reset successfully!!!');window.location ='LoginPage.aspx';", true);
                }
                else
                {

                    string script = "alert(\"Invalid captcha!!!!!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                }
            }
                      
            conn.Close();


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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginIems");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginIems");
        }
    }
}