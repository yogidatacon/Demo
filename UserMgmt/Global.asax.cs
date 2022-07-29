using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using UserMgmt.Utility;

namespace UserMgmt
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }
        private void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ////Bug 53067 - Remediate - Session Stealing-start
            ////Check If it is a new session or not , if not then do the further checks
            //if (Request.Cookies["UserID"] != null && Request.Cookies["UserID"].Value != null)
            //{
            //    string newSessionID = Request.Cookies["UserID"].Value;
            //    //Check the valid length of your Generated Session ID
            //    //if (newSessionID.Length < 24)
            //    //{
            //    //    //Log the attack details here
            //    //    //Close the session
            //        ExpireAllCookies();
            //    //}
            //    //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
            //    //if (newSessionID.Length > 24)
            //    //{
            //    //    if (Utility.SessionObject.GenerateHashKey(Request.Browser.Browser) != newSessionID.Substring(24))
            //    //    {
            //    //        //Log the attack details here
            //    //        //Close the session
            //    //        ExpireAllCookies();
            //    //    }
            //    //}
            //}
            //else if (Request.Cookies["UserID"] != null && Request.Cookies["UserID"].Value != null)//new session
            //{
            //    Response.Cookies["UserID"].Value = Request.Cookies["UserID"].Value;
            //}
            ////Bug 53067 - Remediate - Session Stealing-end
          
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}