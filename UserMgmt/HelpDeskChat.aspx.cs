using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class HelpDeskChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userid = Session["UserID"].ToString();
                // string userid = "Admin";
                Session["UserID"] = userid;
                labUsername.Text = Session["Username"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                Session["ID"] = user.id;
                txtContactNumber.Text = user.mobile;
                txtEmail.Text = user.email_id;
                string previousPageName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                string currentpage = Request.Url.Segments[Request.Url.Segments.Length - 1];
                Session["pagename"] = previousPageName;
                txtpagename.Text = previousPageName;
                txtpagename.Visible = false;
            }
          
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
         
                string userid = Session["UserID"].ToString();
                string previousPageName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                string currentpage = Request.Url.Segments[Request.Url.Segments.Length - 1];
                Helpdesk Helpdesk = new Helpdesk();
                Helpdesk.ticket_formname = txtpagename.Text;
                Helpdesk.user_email = txtEmail.Text;
                Helpdesk.user_contact =Convert.ToDouble( txtContactNumber.Text);
                Helpdesk.user_id = Session["UserID"].ToString();
            Helpdesk.user_registration_id = Convert.ToInt32(Session["ID"].ToString());
                Helpdesk.ticket_query = txtmessagebody.Text;
                Helpdesk.ticketstatus_code = "P";
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                Helpdesk.ticket_raisedby = Convert.ToInt32(user.id);
                Helpdesk.ticketno = user.user_id;
            if (idupDocument.HasFile)
            {

                string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                string[] filetype = fileName.Split('.');
                string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                idupDocument.PostedFile.SaveAs(Server.MapPath("~/HD_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                string path = Server.MapPath("~/HD_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                Helpdesk.path = path;
            }
            Helpdesk.record_status = "P";
                string val;
            if (Helpdesk.record_status == "P")
            {
                val = BL_HelpDesk.Insert(Helpdesk);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("controlroom@prohibitionbihar.in");
                msg.To.Add("charan.ccsd@gmail.com");
                msg.Subject =  "Issues";
                //if (idupDocument.HasFile)
                //{
                //    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                //    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
                //}
                msg.Body = "Resolve Time" ;
                SmtpClient smt = new SmtpClient();
                smt.Host = "smtpout.asia.secureserver.net";
                // smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = "controlroom@prohibitionbihar.in"; //Your Email ID  
                ntwd.Password = "IEMS@123"; // Your Password  
                smt.UseDefaultCredentials = true;
                smt.Credentials = ntwd;
                smt.Port = 587;
                smt.EnableSsl = true;
                try
                {
                    smt.Send(msg);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email sent.');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email not sent.');", true);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
            else
            {
                string message = "Sending message failed";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
           

    
            //try
            //{
            //    //Create the msg object to be sent
            //    MailMessage msg = new MailMessage();
            //    //Add your email address to the recipients
            //    msg.To.Add("yogeshbaluragi96@gmail.com");
            //    //Configure the address we are sending the mail from **- NOT SURE IF I NEED THIS OR NOT?**
            //    MailAddress address = new MailAddress("charan.ccsd@gmail.com");
            //    msg.From = address;
            //    //Append their name in the beginning of the subject
            //    msg.Subject = txtpagename.Text + " :  " +"Issues";
            //    msg.Body = txtmessagebody.Text;

            //    //Configure an SmtpClient to send the mail.
            //    SmtpClient client = new SmtpClient("smtp.live.com", 465);
            //    client.EnableSsl = true; //only enable this if your provider requires it
            //                             //Setup credentials to login to our sender email address ("UserName", "Password")
            //    NetworkCredential credentials = new NetworkCredential("", "charan.ccsd@gmail.com", "charansd1996");
            //    client.Credentials = credentials;

            //    //Send the msg
            //    client.Send(msg);

            //    //Display some feedback to the user to let them know it was sent
            //    lblResult.Text = "Your message was sent!";

            //    //Clear the form
            //    txtpagename.Text = "";
            //    txtEmail.Text = "";
            //    txtContactNumber.Text = "";
            //    txtmessagebody.Text = "";
            //}
            //catch
            //{
            //    //If the message failed at some point, let the user know
            //    lblResult.Text = "Your message failed to send, please try again.";
            //}
      




        //string to = "sivadatacon2@gmail.com"; //To address    
        //string from = "charan.ccsd@gmail.com"; //From address    
        //MailMessage message = new MailMessage(from, to);

        //string mailbody = txtmessagebody.Text;
        //message.Subject = txtpagename.Text +"Issues";
        //message.Body = mailbody;
        //message.BodyEncoding = Encoding.UTF8;
        //message.IsBodyHtml = true;
        ////SmtpClient client = new SmtpClient("smtp.server.address", 2525)
        ////{
        ////    Credentials = new NetworkCredential("charan.ccsd@gmail.com", "charansd1996"),
        ////    EnableSsl = true
        ////};
        //SmtpClient client = new SmtpClient("SMTP.gmail.com", 587); //Gmail smtp    
        //System.Net.NetworkCredential basicCredential1 = new
        //System.Net.NetworkCredential("charan.ccsd@gmail.com", "charansd1996");
        //client.EnableSsl = true;
        //client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //client.UseDefaultCredentials = false;
        //client.Credentials = basicCredential1;
        //try
        //{
        //    client.Send(message);
        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    }
}