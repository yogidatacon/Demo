using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class HelpDeskForm1 : System.Web.UI.Page
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
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string userid = Session["UserID"].ToString();
              Session["ID"]= user.id;
                List<Ticketcategory> Ticketcategorylist = new List<Ticketcategory>();
                Ticketcategorylist = BL_Ticketcategory.GetList();

                var List5 = from s in Ticketcategorylist
                            select s;
                ddlTicketCategory.DataSource = List5.ToList();
                ddlTicketCategory.DataTextField = "ticketcategory_name";
                ddlTicketCategory.DataValueField = "ticketcategory_code";
               ddlTicketCategory.DataBind();
               ddlTicketCategory.Items.Insert(0, "Select");

                List<Priority> Prioritylist = new List<Priority>();
                Prioritylist = BL_Priority.GetList();

                var List1 = from s in Prioritylist
                            select s;
                ddlPriority.DataSource = List1.ToList();
                ddlPriority.DataTextField = "priority_name";
                ddlPriority.DataValueField = "priority_code";
              
                ddlPriority.DataBind();
                ddlPriority.Items.Insert(0, "Select");

                List<UserDetails> details = new List<UserDetails>();
                details = BL_UserDetails.GetUserList(user.user_id);
                var list4 = from s in details
                            where s.role_name_code==57
                            select s;
                ddldeveloper.DataSource = list4.ToList();
                ddldeveloper.DataTextField = "user_name";
                ddldeveloper.DataValueField = "id";
                ddldeveloper.DataBind();
                ddldeveloper.Items.Insert(0, "Select");

                var list5 = from s in details
                            where s.role_name_code ==58
                            select s;
                ddlAssignedTotester.DataSource = list5.ToList();
                ddlAssignedTotester.DataTextField = "user_name";
                ddlAssignedTotester.DataValueField = "id";
                ddlAssignedTotester.DataBind();
                ddlAssignedTotester.Items.Insert(0, "Select");

                List<TicketStatus> TicketStatuslist = new List<TicketStatus>();
                TicketStatuslist = BL_TicketStatus.GetList();
                var list2 = from s in TicketStatuslist
                            select s;
                ddlTicketStatus.DataSource = list2.ToList();
                ddlTicketStatus.DataTextField = "ticketstatus_name";
                ddlTicketStatus.DataValueField = "ticketstatus_code";
                ddlTicketStatus.DataBind();
                ddlTicketStatus.Items.Insert(0, "Select");

                if (Session["rtype"].ToString() != "0")
                {
                    int id = Convert.ToInt32(Session["ticketid"].ToString());
                    Helpdesk help = new Helpdesk();
                    help = BL_HelpDesk.GetDetails(id);
                    txtpagename.Text = help.ticket_formname;
                    txtName.Text = help.party_name;
                  
                    if(user.user_id!="Admin")
                    {
                      ddlPriority.Enabled = false;
                    }
                    txtContactNumber.Text = help.user_contact.ToString();
                    txtEmail.Text = help.user_email;
                    ddlPriority.SelectedValue = help.priority_code;
                    if(help.priority_code !="" && help.priority_code != null && help.priority_code !="Select")
                    {
                        Priority va1 = new Priority();
                        va1 = BL_Priority.Getreslovetime(ddlPriority.SelectedValue);
                        lblresolvetime.Text = "Resolve Time"+" " +va1.priority_resolvetime;
                    }
                    ddlTicketStatus.SelectedValue = help.ticketstatus_code;
                    ddlTicketCategory.SelectedValue = help.ticketcategory_code;
                    txtticketQuery.Text = help.ticket_query;
                    Session["ticket_id"] = help.helpdesk_ticket_id;
                    lblTracking.Text = help.helpdesk_ticket_id.ToString();
                    lblTicketno.Text = help.ticketno.ToString();
                    string exampleTrimmed = String.Concat(help.ticketstatus.Where(c => !Char.IsWhiteSpace(c)));
                    lblTicketstatus.Text = help.party_name;
                    ddlAssignedTotester.SelectedValue = help.tester_code.ToString();
                    ddldeveloper.SelectedValue = help.developer_code.ToString();
                    lblCreated.Text = help.creation_date;
                    //if(help.timetaken_dev.ToString() !="")
                    //{ }
                    lblDeveloper.Text =help.timetaken_dev.ToString()+"" +"Hours";
                    lblTester.Text = help.timetaken_tester.ToString()+"" + "Hours";
                    lblUpdated.Text = help.lastmodified_date;
                    if (user.user_id == "Admin" || user.role_name == "Developer" || user.role_name == "Test Engineer")
                    {
                        List<TicketHistory> he = new List<TicketHistory>();
                        he = BL_HelpDesk.GetDetailsHistory(help.helpdesk_ticket_id);
                        grdHistory.DataSource = he;
                        grdHistory.DataBind();
                    }

                    if (Session["rtype"].ToString() == "1")
                    {
                        txtpagename.Enabled = false;
                        txtName.Enabled = false;
                        txtticketQuery.Enabled = false;
                        txtContactNumber.Enabled = false;
                        txtEmail.Enabled = false;
                        txtComment.Enabled = false;
                        //txthistory.Enabled = false;
                        ddlAssignedTotester.Enabled = false;
                        ddldeveloper.Enabled = false;
                        ddlPriority.Enabled = false;
                        ddlTicketStatus.Enabled = false;
                    }

                    }


            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if(ddlPriority.SelectedValue=="Select" || ddlPriority.SelectedValue=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Select Priority ');", true);
                }
                else if(ddlTicketCategory.SelectedValue=="Select" || ddlTicketCategory.SelectedValue=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Select Ticket Category ');", true);
                }
                else if(txtComment.Text=="" || idupDocument.ToString()=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Enter Your Message or attach file ');", true);
                }
                else
                { 
                int id = Convert.ToInt32(Session["ticketid"].ToString());
                Helpdesk help = new Helpdesk();
                help = BL_HelpDesk.GetDetails(id);
                TicketHistory history = new TicketHistory();
                history.createdby_id = txtName.Text;
                history.remarks = txtComment.Text;
                history.transaction_date = help.creation_date;
                history.transaction_id = help.helpdesk_ticket_id;
                if(ddlAssignedTotester.SelectedValue!="Select")
                {
                    history.tester = ddlAssignedTotester.SelectedValue;
                }
                if(ddldeveloper.SelectedValue !="Select")
                {
                    history.developer = ddldeveloper.SelectedValue;
                }
                if(ddlPriority.SelectedValue!="Select")
                {
                    history.priority_code = ddlPriority.SelectedValue;
                }
               
                if(ddlTicketStatus.SelectedItem.ToString()=="Complete")
                {
                    history.record_status ="C";
                        history.ticketsatus = ddlTicketStatus.SelectedValue;
                    }
              else if(ddlTicketStatus.SelectedItem.ToString() == "In Progress")
                {
                    history.record_status = "N";
                        history.ticketsatus = ddlTicketStatus.SelectedValue;
                    }
               else if (ddlTicketStatus.SelectedItem.ToString() == "Resolved")
                {
                    history.record_status = "R";
                        history.ticketsatus = ddlTicketStatus.SelectedValue;
                    }
                    else if (ddlTicketStatus.SelectedItem.ToString() == "Re-test")
                    {
                        history.record_status = "T";
                        history.ticketsatus = ddlTicketStatus.SelectedValue;
                    }
                    else
                    {
                        history.record_status = "O";
                        history.ticketsatus= "O";
                    }
                if(ddlTicketCategory.SelectedValue!="Select")
                    {
                        history.ticketcategory_code = ddlTicketCategory.SelectedValue;
                    }
             
               
                history.user_id= Session["UserID"].ToString();
                history.user_registration_id = Convert.ToInt32(Session["ID"].ToString());
                history.docs = new List<HD_DOCS>();
                EASCM_DOCS doc = new EASCM_DOCS();
              
                if (idupDocument.HasFile)
                {
                   
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    string[] filetype = fileName.Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/HD_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                    string path = Server.MapPath("~/HD_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                    history.path = path;
                }
               
                string val;
                    val =BL_HelpDesk.InsertHistory(history);
               
                if (val == "0")
                {
                        Helpdesk help1 = new Helpdesk();
                        help1 = BL_HelpDesk.GetDetails(history.hd_ticket_history_id);

                        if (help1.developer !="" && help1.developer !=null && help1.record_status=="N" )
                        {
                          
                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress("controlroom@prohibitionbihar.in");
                            msg.To.Add(help1.user_email);
                            msg.Subject = help1.ticket_formname + " :  " + "Issues";
                            //if (idupDocument.HasFile)
                            //{
                            //    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                            //    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
                            //}
                            msg.Body = "Resolve Time" + " " + help1.priority_resolvetime; ;
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
                            catch(Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email not sent.');", true);
                            }
                          
                        }
                        if (help1.tester != "" && help1.tester != null && help1.record_status == "C")
                        {
                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress("controlroom@prohibitionbihar.in");
                            msg.To.Add(help1.user_email);
                            msg.Subject = help1.ticket_formname + " :  " + "Issues";
                            //if (idupDocument.HasFile)
                            //{
                            //    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                            //    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
                            //}
                            msg.Body = "Resolve Time" + " " + help1.priority_resolvetime; ;
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

                        }
                        Response.Redirect("HelpDeskList.aspx");

                        List<TicketHistory> he = new List<TicketHistory>();
                    he = BL_HelpDesk.GetDetailsHistory(history.hd_ticket_history_id);
                    grdHistory.DataSource = he;
                    grdHistory.DataBind();
                    //Session["UserID"] = Session["UserID"];
                  
                  

                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                }
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/HD_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("HelpDeskList.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-30); //Delete the cookie
            Session.Abandon();
            Response.Redirect("~/LoginPage.aspx");
        }

        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                Priority va1 = new Priority();
                va1 = BL_Priority.Getreslovetime(ddlPriority.SelectedValue);
                lblresolvetime.Text = "Resolve Time" + " " + va1.priority_resolvetime;
            }
        }

        protected void grdHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHistory.PageIndex = e.NewPageIndex;
            List<TicketHistory> he = new List<TicketHistory>();
            he = BL_HelpDesk.GetDetailsHistory(Convert.ToInt32(Session["ticket_id"].ToString()));
            grdHistory.DataSource = he;
            grdHistory.DataBind();
        }
    }
}