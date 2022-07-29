using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;

using System.Security.Cryptography;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using System.Net.Mail;

namespace CaseMgmt
{
    public partial class SMS_Witness : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
        NpgsqlConnection conn = new NpgsqlConnection(connstring);
        protected void Page_Load(object sender, EventArgs e)
        {
            string messgae = "";
            conn.Open();

            //var command12 = new NpgsqlCommand("select distinct b.seizureno,witnessname,contactno,cast (dateofnextstage as timestamp)-9 as Test_Day,cast (dateofnextstage as timestamp)-1 as One_Day ,cast (dateofnextstage as timestamp)-4 as Four_Day ,cast (dateofnextstage as timestamp)-7 as One_Week ,dateofnextstage,b.district,f.prfirno from exciseautomation.casemgmt_smsview c inner join exciseautomation.casemgmt_basicinformation b on b.seizureno =c.seizureno left join exciseautomation.casemgmt_fir f on f.seizureno =b.seizureno where dateofnextstage <>'' ", conn);

            var command12 = new NpgsqlCommand("select distinct b.seizureno,witnessname,contactno,cast (dateofnextstage as timestamp)-9 as Test_Day,"+
                " cast (dateofnextstage as timestamp)-1 as One_Day ,cast (dateofnextstage as timestamp)-4 as Four_Day ,"+
                " cast (dateofnextstage as timestamp)-7 as One_Week ,dateofnextstage,dm.district_name as district,f.prfirno"+
                " from exciseautomation.casemgmt_smsview c inner join exciseautomation.seizure_basicinfo b"+
                " on b.seizureno =c.seizureno and b.raidby = c.raidby inner join exciseautomation.seizure_fir f"+
                " on f.seizureno =b.seizureno and c.raidby =f.raidby  inner join exciseautomation.district_master dm on dm.district_code =b.district_code"+
                " where dateofnextstage is not null  and trialstage_code =4", conn);

            NpgsqlDataReader pgreader12 = command12.ExecuteReader();

            DataTable dt12 = new DataTable();
            dt12.Load(pgreader12);
            pgreader12.Close();
            string smss = "N";
            for (int i = 0; i < dt12.Rows.Count; i++)
            {
                DateTime dt4 = Convert.ToDateTime(dt12.Rows[i]["Test_Day"].ToString());
                DateTime dt1 = Convert.ToDateTime(dt12.Rows[i]["One_Day"].ToString());
                DateTime dt2 = Convert.ToDateTime(dt12.Rows[i]["Four_Day"].ToString());
                DateTime dt3 = Convert.ToDateTime(dt12.Rows[i]["One_Week"].ToString());
                string witnessname = dt12.Rows[i]["witnessname"].ToString();

                string seizure_no = dt12.Rows[i]["seizureno"].ToString();

                if (dt4 == Convert.ToDateTime(DateTime.Now.ToLongDateString()))
                {
                    //sendSingleSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", dt12.Rows[i]["contactno"].ToString(), "Hi", "584fdb0f-6287-490f-a071-6102880a9327");
                    messgae = "You are requested to attend the Additional Session Judge, Special court,Excise " + dt12.Rows[i]["district"].ToString() + " dated " + dt12.Rows[i]["dateofnextstage"].ToString() + " as a witness of case registered against PR No " + dt12.Rows[i]["prfirno"].ToString().Trim() + ". " + "For details please contact Excise District Office " + dt12.Rows[i]["district"].ToString() + ".- Bihar Government.";
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", dt12.Rows[i]["contactno"].ToString(), messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9473029410", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "8095346146", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    smss = "Y";

                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9473029410", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012064977234");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "8095346146", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012064977234");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9972096222", messgae, "584fdb0f-6287-490f-a071-6102880a9327");
                }

                if (dt1 == Convert.ToDateTime(DateTime.Now.ToLongDateString()))
                {
                     
                    messgae = "You are requested to attend the Additional Session Judge, Special court,Excise "+ dt12.Rows[i]["district"].ToString()+" dated "+ dt12.Rows[i]["dateofnextstage"].ToString()+ " as a witness of case registered against PR No "+ dt12.Rows[i]["prfirno"].ToString().Trim()+". "+ "For details please contact Excise District Office "+ dt12.Rows[i]["district"].ToString()+ ".- Bihar Government.";
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", dt12.Rows[i]["contactno"].ToString(), messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9473029410", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "8095346146", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    smss = "Y";

                }

                else if (dt2 == Convert.ToDateTime(DateTime.Now.ToLongDateString()))
                {
                    messgae = "You are requested to attend the Additional Session Judge, Special court,Excise " + dt12.Rows[i]["district"].ToString() + " dated " + dt12.Rows[i]["dateofnextstage"].ToString() + " as a witness of case registered against PR No " + dt12.Rows[i]["prfirno"].ToString().Trim() + ". " + "For details please contact Excise District Office " + dt12.Rows[i]["district"].ToString() + ".- Bihar Government.";
                    sendSingleSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", dt12.Rows[i]["contactno"].ToString(), messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9473029410", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "8095346146", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    smss = "Y";

                }

                else if (dt3 == Convert.ToDateTime(DateTime.Now.ToLongDateString()))
                {
                    messgae = "You are requested to attend the Additional Session Judge, Special court,Excise " + dt12.Rows[i]["district"].ToString() + " dated " + dt12.Rows[i]["dateofnextstage"].ToString() + " as a witness of case registered against PR No " + dt12.Rows[i]["prfirno"].ToString().Trim() + ". " + "For details please contact Excise District Office " + dt12.Rows[i]["district"].ToString() + ".- Bihar Government.";
                    sendSingleSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", dt12.Rows[i]["contactno"].ToString(), messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    //sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "9473029410", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    sendBulkSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", "8095346146", messgae, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
                    smss = "Y";

                }
                int j = 0;
                j = i;
                Response.Write(Environment.NewLine+ j + Environment.NewLine);
                if(smss == "Y")
                {
                    Response.Write("Mobile No : " + dt12.Rows[i]["contactno"].ToString() + Environment.NewLine);
                }
            }

            Response.Write("\n Sent all notifications !!!");
            conn.Close();
        }


       
         







        /// <summary>
        /// Method for sending single SMS.
        /// </summary>
        /// <param name="username"> Registered user name</param>
        /// <param name="password"> Valid login password</param>
        /// <param name="senderid">Sender ID </param>
        /// <param name="mobileNo"> valid Single Mobile Number </param>
        /// <param name="message">Message Content </param>
        /// <param name="secureKey">Department generate key by login to services portal</param>


        // Method for sending single SMS.

        public String sendSingleSMS(String username, String password, String senderid, String mobileNo, String message, String secureKey, String templateid)
        {
            //Latest Generated Secure Key
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(" ");
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
            String smsservicetype = "singlemsg"; //For single message.

            //String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
            //    "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

            //    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

            //    "&content=" + HttpUtility.UrlEncode(message.Trim()) +

            //    "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +

            //    "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
            //  "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim());

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +

             "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

             "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

             "&content=" + HttpUtility.UrlEncode(message.Trim()) +

             "&bulkmobno=" + HttpUtility.UrlEncode(mobileNo) +

             "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +

            "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +

        "&templateid=" + HttpUtility.UrlEncode(templateid.Trim());

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


        /// <summary>
        /// Method for sending bulk SMS.
        /// </summary>
        /// <param name="username"> Registered user name</param>
        /// <param name="password"> Valid login password</param>
        /// <param name="senderid">Sender ID </param>
        /// <param name="mobileNo"> valid Mobile Numbers </param>
        /// <param name="message">Message Content </param>
        /// <param name="secureKey">Department generate key by login to services portal</param>

        // method for sending bulk SMS

        public String sendBulkSMS(String username, String password, String senderid, String mobileNos, String message, String secureKey,String templateid)
        {
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequest");
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

            //String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +

            // "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

            // "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

            // "&content=" + HttpUtility.UrlEncode(message.Trim()) +

            // "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) +

            // "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +

            //"&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim());



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


        /// <summary>
        /// method for Sending unicode..
        /// </summary>
        /// <param name="username"> Registered user name</param>
        /// <param name="password"> Valid login password</param>
        /// <param name="senderid">Sender ID </param>
        /// <param name="mobileNo"> valid Mobile Numbers </param>
        /// <param name="Unicodemessage">Unicodemessage Message Content </param>
        /// <param name="secureKey">Department generate key by login to services portal</param>

        //method for Sending unicode message..

        public String sendUnicodeSMS(String username, String password, String senderid, String mobileNos, String Unicodemessage, String secureKey)
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

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
            String U_Convertedmessage = "";

            foreach (char c in Unicodemessage)
            {
                int j = (int)c;
                String sss = "&#" + j + ";";
                U_Convertedmessage = U_Convertedmessage + sss;
            }
            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), U_Convertedmessage.Trim(), secureKey.Trim());


            String smsservicetype = "unicodemsg"; // for unicode msg
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



        /// <summary>
        /// Method for sending OTP MSG.
        /// </summary>
        /// <param name="username"> Registered user name</param>
        /// <param name="password"> Valid login password</param>
        /// <param name="senderid">Sender ID </param>
        /// <param name="mobileNo"> valid single  Mobile Number </param>
        /// <param name="message">Message Content </param>
        /// <param name="secureKey">Department generate key by login to services portal</param>

        // Method for sending OTP MSG.

        public String sendOTPMSG(String username, String password, String senderid, String mobileNo, String message, String secureKey)
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
            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String key = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());

            String smsservicetype = "otpmsg"; //For OTP message.

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

                "&content=" + HttpUtility.UrlEncode(message.Trim()) +

                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +

                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
              "&key=" + HttpUtility.UrlEncode(key.Trim());



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


        // Method for sending UnicodeOTP MSG.

        /// <summary>
        /// method for Sending unicode..
        /// </summary>
        /// <param name="username"> Registered user name</param>
        /// <param name="password"> Valid login password</param>
        /// <param name="senderid">Sender ID </param>
        /// <param name="mobileNo"> valid Mobile Numbers </param>
        /// <param name="Unicodemessage">Unicodemessage Message Content </param>
        /// <param name="secureKey">Department generate key by login to services portal</param>

        //method for Sending unicode message..

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
            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

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


        /// <summary>
        /// Method to get Encrypted the password 
        /// </summary>
        /// <param name="password"> password as String"</param>

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

    }

}

 

