using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace UserMgmt
{
    public partial class LabInchargeLogin : System.Web.UI.Page
    {
        public class LabInchargeList
        {
            public string form_no { get; set; }
            public int total { get; set; }
            public int status_code { get; set; }
            public int untested { get; set; }
            public int tested { get; set; }
            public int retest { get; set; }
            public int verified { get; set; }
            public string status_string { get; set; }

            public string GetStatusString(int x)
            {
                if (x == 1) return "All samples tested";
                if (x == 2) return "All samples verified";
                if (x == 3) return "All samples not tested";
                return null;
            }

            public LabInchargeList(string FormNo, int Total, int Status_code, int Untested, int Tested, int Retest, int Verified)
            {
                this.form_no = FormNo;
                this.total = Total;
                this.status_code = Status_code;
                this.untested = Untested;
                this.tested = Tested;
                this.retest = Retest;
                this.verified = Verified;
                this.status_string = GetStatusString(Status_code);
            }

            public LabInchargeList()
            {

            }

        }

        public string ConnectionString => ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;

        public NpgsqlConnection GetSqlConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public NpgsqlCommand GetSqlCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, GetSqlConnection())
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        string fullname = "";
        NpgsqlConnection conn;
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
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString());
                try
                {
                    string query = $@"SELECT * FROM exciseautomation.tab_form_no_status WHERE form_no != ''";
                    LoadGrid(query);
                }

                catch (Exception ex)
                {
                    Response.Write("Contact Admin : " + ex.Message.ToString());
                }
            }
        }

        public string get_query()
        {
            string query = $@"";
            string fn = txtForm.Text.ToString();
            int type = int.Parse(ddStatus.SelectedValue.ToString());
            if (fn == "")
            {
                if (type != 0)
                {
                    query = $@"SELECT * FROM exciseautomation.tab_form_no_status WHERE quantity_status_overall = {type}";
                }
                else
                {
                    query = $@"SELECT * FROM exciseautomation.tab_form_no_status";
                }
            }
            else
            {
                if (type != 0)
                {
                    query = $@"SELECT * FROM exciseautomation.tab_form_no_status WHERE form_no = '{fn}' AND quantity_status_overall = {type}";
                }
                else
                {
                    query = $@"SELECT * FROM exciseautomation.tab_form_no_status WHERE form_no = '{fn}'";
                }
            }
            return query;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string query = get_query();
            LoadGrid(query);
        }

        protected void grdLabInchargeLogin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                var inputParams = e.CommandArgument.ToString().Split(',');
                var form_no = inputParams.Length > 0 ? inputParams[0] : default(string);
                Response.Redirect($"~/LabInchargeView.aspx?form_no={form_no}");
            }
            if (e.CommandName == "SMS")
            {
                //avani
                String username = "BIHAREDISTRICT-excise";
                String password = "Excise@123";
                String senderid = "BRGOVT";
                //String mobileNos = "9712915870";
                String mobileNos = "9714797592,9712915870";
                String message = "This is test message";
                String secureKey = "584fdb0f-6287-490f-a071-6102880a9327";
                String templateid = "1307161012046394150";
                string result = btnSMSCol_Click(username, password, senderid, mobileNos, message, secureKey, templateid);
            }
            if (e.CommandName == "Email")
            {
                //Bhavin
                SendEmail();
            }
        }

        //Bhavin
        public void SendEmail()
        {

            ServicePointManager.ServerCertificateValidationCallback =
           delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
           {
               return true;
           };

            using (var message = new MailMessage())
            {
                string toEmail = Session["email_Id"].ToString();
                //message.To.Add(new MailAddress("bhavin@rifesoftware.com"));
                message.To.Add(toEmail);
                message.Subject = "This is IEMS EMAIL";
                message.Body = "This EMAIL from IEMS LAB";
                message.IsBodyHtml = true;

                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        smtp.Send(message);

                    }

                }
                catch (Exception ex)
                {
                    // Logger.Log("11). added CC email >>" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<LabInchargeList> GetList(string query)
        {
            List<LabInchargeList> items = new List<LabInchargeList>();
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string t_form_no = dr.GetString(dr.GetOrdinal("form_no"));
                        int t_total = dr.GetInt32(dr.GetOrdinal("quantity_count"));
                        int t_status_code = dr.GetInt32(dr.GetOrdinal("quantity_status_overall"));
                        int t_untested = dr.GetInt32(dr.GetOrdinal("quantity_untested"));
                        int t_tested = dr.GetInt32(dr.GetOrdinal("quantity_tested"));
                        int t_retest = dr.GetInt32(dr.GetOrdinal("quantity_retest"));
                        int t_verified = dr.GetInt32(dr.GetOrdinal("quantity_verified"));
                        items.Add(new LabInchargeList(t_form_no, t_total, t_status_code, t_untested, t_tested, t_retest, t_verified));
                    }
                }
                command.Connection.Close();
            }
            return items;
        }

        protected void grdLabInchargeLogin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLabInchargeLogin.PageIndex = e.NewPageIndex;
            string query = get_query();
            LoadGrid(query);
        }

        private void LoadGrid(string query)
        {
            grdLabInchargeLogin.DataSource = GetList(query);
            grdLabInchargeLogin.DataBind();
        }

        //avani
        public String btnSMSCol_Click(String username, String password, String senderid, String mobileNos, String message, String secureKey, String templateid)
        {
            try
            {
                Stream dataStream;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequestDLT");
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;

                request.ServicePoint.ConnectionLimit = 1;

                //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";

                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                request.Method = "POST";
                System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
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
                "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
                "&templateid=" + HttpUtility.UrlEncode(templateid.Trim());

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
            catch (Exception ex)
            {
                throw ex;
            }
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

    }

}


class MyPolicy : ICertificatePolicy
{
    public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
    {
        return true;
    }
}




//end


