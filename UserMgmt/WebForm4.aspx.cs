using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                byte[] pdfPath = System.IO.File.ReadAllBytes(@"D:\\bopee_final_2.pdf");
                String file = Convert.ToBase64String(pdfPath);
                DataSet ds = new DataSet();
                System.Random random = new System.Random();
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/All_Approved_Docs/request_master.xml"));
                int n = 1;
                foreach (XmlNode item in doc.DocumentElement.ChildNodes)
                {
                    if (n == 2)
                    {
                        item.InnerText = DateTimeOffset.UtcNow.ToString("o");
                    }
                    if (n == 3)
                    {
                        item.InnerText = random.Next(100000).ToString(); 
                    }
                    if (item.Name == "certificate")
                    {

                        foreach (XmlNode item1 in item)
                        {
                            if (item1.Attributes[0].Value == "SN")
                            {
                                item1.InnerText = "03053c41";
                            }
                        }
                    }
                    if (item.Name == "pdf")
                    {
                        foreach (XmlNode item1 in item)
                        {
                            if (item1.NextSibling != null)
                            {
                                if (item1.NextSibling.Name == "cood")
                                {
                                    item1.NextSibling.InnerText = "10,10";
                                }
                                if (item1.NextSibling.Name == "size")
                                {
                                    item1.NextSibling.InnerText = "200,100";
                                }
                            }
                        }
                    }
                    if (item.Name == "data")
                    {

                        item.InnerText = file;
                    }
                    n++;
                }
                doc.Save(Server.MapPath("~/All_Approved_Docs/request.xml"));
                string xmlcontents = doc.InnerXml;
                var dataa = HttpUtility.UrlEncode(xmlcontents);
                TextBox1.Text = dataa;
            }
        }
    }
}