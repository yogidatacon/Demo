using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgmt
{
    /// <summary>
    /// Summary description for SampleHandler
    /// </summary>
    public class SampleHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            context.Response.ContentType = "text/plain";
            context.Response.Write(context.Session["Pdf_Data"]);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}