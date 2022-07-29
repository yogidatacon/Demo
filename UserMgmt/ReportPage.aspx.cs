using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ReportPage : System.Web.UI.Page
    {
        List<Server_Configs> server = new List<Server_Configs>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"]!=null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
                string userid = Session["UserID"].ToString();
                //ReportViewer1.Width =800;
                ReportViewer1.Height =900;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                server = new List<Server_Configs>();
                server = BL_SeverConfig.GetServerList(userid);
                //IReportServerCredentials irsc = new CustomReportCredentials("Admin", "Admin123", "NAVEEN"); // e.g.: ("demo-001", "123456789", "ifc")
                IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password,server[0].server_domain);
                ReportViewer1.Visible = false;
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
                Session["ReportProject1"] = server[0].projectname;
               
                if (Session["ReportId"].ToString() == "view_pass")
                {
                   if(Session["Pass_Type"].ToString()=="RR")
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["" + Session["ReportProject1"].ToString()+""].ToString()+"/rr_pass";
                   else
                        ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/noc_pass";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3]; 
                    Param[0] = new ReportParameter("Parameter1", Session["Pass_No"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["Pass_Type"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Pfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("Pass_" + Session["Pass_No"].ToString()+".pdf");
                    //    ExportToPDF(pdf_file);
                    //}
                    //else
                    //{
                    //    string[] values = Session["Allvalues"].ToString().Split('_');
                    //    SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    //}
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                
             else   if (Session["ReportId"].ToString().Trim() == "ENA Dispatch Report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+ "/noc_pass";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["Pass_No"].ToString()); 
                    Param[1] = new ReportParameter("Parameter2", Session["Pass_Type"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["PDfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("Pass_" + Session["Pass_No"].ToString()+".pdf");
                    //    ExportToPDF(pdf_file);
                    //}
                    //else
                    //{
                    //    string[] values = Session["Allvalues"].ToString().Split('_');
                    //    SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    //}
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "ena_form46_domestic"|| Session["ReportId"].ToString().Trim() == "ena_form46")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+ Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["Pass_ID"].ToString()); 
                    Param[1] = new ReportParameter("Parameter2", Session["Pass_Type"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["rftptfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("Pass_" + Session["Pass_No"].ToString()+".pdf");
                    //    ExportToPDF(pdf_file);
                    //}
                    //else
                    //{
                    //    string[] values = Session["Allvalues"].ToString().Split('_');
                    //    SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    //}
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_ID"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else  if (Session["ReportId"].ToString() == "MF1")
                {
                   
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/molasses_indent_mf1";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2]; 
                    Param[0] = new ReportParameter("Parameter1", Session["IndentNO"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["Ifinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "MF1_" + Session["IndentNO"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "RR")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/release_request_rr";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2]; 
                    Param[0] = new ReportParameter("Parameter1", Session["RR_NO"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["rrfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "RR_" + Session["RR_NO"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "NOC")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/no_objection_certificate_noc";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3]; 
                    Param[0] = new ReportParameter("Parameter1", Session["NOC_ID"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["nocfinancial_year"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["nocparty_code"].ToString());
                  
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "NOC_" + Session["NOC_ID"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                   //ExportToPDF(ReportViewer1.ServerReport.ReportPath+ "/All_Approved_Docs", Param.ToList(), "NOC_"+ Session["NOC_ID"].ToString());
                  //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('ShowPDF.ashx'); alert('OK' );", true);
                  //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('"+ReportViewer1.ServerReport.ReportPath + "/All_Approved_Docs/NOC_" + Session["NOC_ID"].ToString()+"' ,'_blank');", true);
                }

                else if (Session["ReportId"].ToString() == "ENA")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/ena_no_objection_certificate_noc";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["NOC_ID"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["nocfinancial_year"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["nocparty_code"].ToString());

                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "NOC_" + Session["NOC_ID"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                    //ExportToPDF(ReportViewer1.ServerReport.ReportPath+ "/All_Approved_Docs", Param.ToList(), "NOC_"+ Session["NOC_ID"].ToString());
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('ShowPDF.ashx'); alert('OK' );", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('"+ReportViewer1.ServerReport.ReportPath + "/All_Approved_Docs/NOC_" + Session["NOC_ID"].ToString()+"' ,'_blank');", true);
                }
                else if (Session["ReportId"].ToString() == "allotment_letter" || Session["ReportId"].ToString() == "ena_allotment_letter")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2]; 
                    Param[0] = new ReportParameter("Parameter1", Session["AllotmentNo"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["financial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ReportViewer1.LocalReport.Refresh();
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("~/All_Approved_Docs/Allotment_" + Session["AllotmentNo"].ToString() + ".pdf");
                    //    if (File.Exists(pdf_file))
                    //        ExportToPDFfile(pdf_file);
                    //    else
                    //        ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Allotment_" + Session["AllotmentNo"].ToString());
                    //}
                    //else
                    //{
                    //string[] values = Session["Allvalues"].ToString().Split('_');
                    //SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), values, "Allotment_" + Session["AllotmentNo"].ToString());
                    //    Session["UserID"] = Session["UserID"];
                    //    Response.Redirect("AllocationRequestList");
                    //}
                    //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('HtmlPage11.html' ,'_blank');", true);
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Allotment_" + Session["AllotmentNo"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "Form52B"|| Session["ReportId"].ToString() == "Form47")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+ Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["AllotmentNo"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["Perfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ReportViewer1.LocalReport.Refresh();
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("~/All_Approved_Docs/Allotment_" + Session["AllotmentNo"].ToString() + ".pdf");
                    //    if (File.Exists(pdf_file))
                    //        ExportToPDFfile(pdf_file);
                    //    else
                    //        ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Allotment_" + Session["AllotmentNo"].ToString());
                    //}
                    //else
                    //{
                    //string[] values = Session["Allvalues"].ToString().Split('_');
                    //SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), values, "Allotment_" + Session["AllotmentNo"].ToString());
                    //    Session["UserID"] = Session["UserID"];
                    //    Response.Redirect("AllocationRequestList");
                    //}
                    //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('HtmlPage11.html' ,'_blank');", true);
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Allotment_" + Session["AllotmentNo"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }

                else if (Session["ReportId"].ToString() == "MF2")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/molasses_provisional_production_mf2";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["MF2_No"].ToString()); 
                   Param[1] = new ReportParameter("Parameter2", Session["MPPfinancial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "MF2_" + Session["MF2_No"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "MF3")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/molasses_production_actual_mf3";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["MF3_No"].ToString());
                     Param[1] = new ReportParameter("Parameter2", Session["MF3financial_year"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "MF3_" + Session["MF3_No"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
               else if (Session["ReportId"].ToString() == "rm_stock_ena" || Session["ReportId"].ToString() == "molasses_stock_sugarmill_register" || Session["ReportId"].ToString() == "absolute_alcohol_stock_register" || Session["ReportId"].ToString() == "ethanol_stock_register_report" || Session["ReportId"].ToString() == "ena_stock_register" || Session["ReportId"].ToString() == "impure_spirit_stock_register" || Session["ReportId"].ToString().Trim() == "ena_ethanol_stock_register" || Session["ReportId"].ToString().Trim() == "sds_stock_register" || Session["ReportId"].ToString().Trim() == "grain_stock_register")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[9];
                    Param[0] = new ReportParameter("Parameter1", Session["Party_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["vat_code"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["vat_code"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["Fromdate"].ToString());
                    Param[5] = new ReportParameter("Parameter6", Session["ToDate"].ToString());
                    Param[6] = new ReportParameter("Parameter7", Session["Party_code"].ToString());
                    Param[7] = new ReportParameter("Parameter8", Session["vat_code"].ToString()); 
                   Param[8] = new ReportParameter("Parameter9", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; //Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; //Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"]+ "_stock_register");
                    ReportViewer1.LocalReport.Refresh();
                }
                else if  (Session["ReportId"].ToString().Trim() == "molasses_stock_distillery" )
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[11];
                    Param[0] = new ReportParameter("Parameter1", Session["Party_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["vat_code"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Reportfinancialyear"].ToString());

                    Param[3] = new ReportParameter("Parameter4", Session["Party_code"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["vat_code"].ToString());
                    Param[5] = new ReportParameter("Parameter6", Session["Reportfinancialyear"].ToString());

                    Param[6] = new ReportParameter("Parameter7", Session["Fromdate"].ToString());
                    Param[7] = new ReportParameter("Parameter8", Session["ToDate"].ToString());

                    Param[8] = new ReportParameter("Parameter9", Session["Party_code"].ToString());
                    Param[9] = new ReportParameter("Parameter10", Session["vat_code"].ToString());
                    Param[10] = new ReportParameter("Parameter11", Session["Reportfinancialyear"].ToString());

                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; //Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; //Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + "_stock_register");
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "sugarcane_stock_register" )
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[6];
                    Param[0] = new ReportParameter("Parameter1", Session["Party_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["Party_code"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["FromDate"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["ToDate"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["Party_code"].ToString()); 
                   Param[5] = new ReportParameter("Parameter6", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"]+ "_molasses_stock_distillery");
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "CM_DISTRICT _VS_OFFICER_SUMMARY" || Session["ReportId"].ToString().Trim() == "DateWiseCaseCountReport" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_APPARATUS_SUMMARY" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_BAIL_SUMMARY" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_EXCISE_ARTICLE_SUMMARY" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_PROPERTY_SUMMARY" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_SEIZURE_SUMMARY" || Session["ReportId"].ToString() == "CM_DISTRICTWISE_VEHICLE_SUMMARY" || Session["ReportId"].ToString() == "CM_SEIZURE_SUMMARY")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["district_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["FromDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["ToDate"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["RaidBy"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "GenderWiseOffenceSummary" || Session["ReportId"].ToString().Trim() == "DistrictCasteWiseSummary" || Session["ReportId"].ToString().Trim() == "DistrictAgeWiseSummary")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["RaidBy"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if ( Session["ReportId"].ToString().Trim() == "DRD3"|| Session["ReportId"].ToString().Trim() =="DRD_NON_PERFORMER")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }

                else if (Session["ReportId"].ToString().Trim() == "DRD1" || Session["ReportId"].ToString().Trim() =="DRD1_OTR")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["district_code"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }

                else if (Session["ReportId"].ToString().Trim() == "DMR1" || Session["ReportId"].ToString().Trim() == "DMR3")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[3];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["district_code"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "DMR2" || Session["ReportId"].ToString().Trim() == "DMR4")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["district_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["court_code"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "SeizureStatusDateWise" ) 
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["district_code"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "CaseReport1_Police" || Session["ReportId"].ToString().Trim() == "CaseReport1") 
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["district_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["FromDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "CM_DISTRICTWISE_SEIZURE_LIST")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["district_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["RaidBy"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString().Trim() == "CaseCountReport"|| Session["ReportId"].ToString().Trim() == "DistrictCaseStatus" || Session["ReportId"].ToString().Trim() == "DistrictGenderWiseOffenceSummary" || Session["ReportId"].ToString().Trim() == "DistrictWiseAccusedStatus")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[1];
                    Param[0] = new ReportParameter("Parameter1", Session["RaidBy"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["RaidBy"] + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                //else if (Session["ReportId"].ToString() == "molasses_stock_distillery" )
                //{

                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                //    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[7];
                //    Param[0] = new ReportParameter("Parameter1", Session["Party_code"].ToString());
                //    Param[1] = new ReportParameter("Parameter2", Session["vat_code"].ToString());
                //    Param[1] = new ReportParameter("Parameter2", Session["Party_code"].ToString());

                //    Param[3] = new ReportParameter("Parameter4", Session["Party_code"].ToString());
                //    Param[4] = new ReportParameter("Parameter5", Session["FromDate"].ToString());
                //    Param[5] = new ReportParameter("Parameter6", Session["ToDate"].ToString());
                //    Param[6] = new ReportParameter("Parameter7", Session["Party_code"].ToString());
                //    ReportViewer1.ShowParameterPrompts = false;
                //    ReportViewer1.ServerReport.SetParameters(Param);
                //    ReportViewer1.ServerReport.Refresh();
                //    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                //    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                //    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                //    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + "_molasses_stock_distillery");
                //    ReportViewer1.LocalReport.Refresh();
                //}
                //else if (Session["ReportId"].ToString() == "absolute_alcohol_stock_register")
                //{

                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                //    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[6];
                //    Param[0] = new ReportParameter("Parameter1", Session["Party_code"].ToString());
                //    Param[1] = new ReportParameter("Parameter2", Session["Party_code"].ToString());
                //    Param[2] = new ReportParameter("Parameter3", Session["FromDate"].ToString());
                //    Param[3] = new ReportParameter("Parameter4", Session["ToDate"].ToString());
                //    Param[4] = new ReportParameter("Parameter5", Session["Party_code"].ToString());
                //    Param[5] = new ReportParameter("Parameter6", Session["vat_code"].ToString());
                //    ReportViewer1.ShowParameterPrompts = false;
                //    ReportViewer1.ServerReport.SetParameters(Param);
                //    ReportViewer1.ServerReport.Refresh();
                //    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                //    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                //    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                //    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + "_molasses_stock_distillery");
                //    ReportViewer1.LocalReport.Refresh();
                //}

                else if ( Session["ReportId"].ToString() == "form_82report" || Session["ReportId"].ToString() == "ena_form_82report" || Session["ReportId"].ToString().Trim() == "ena_form_83report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/"+ Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[5];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["vat_code"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + "_"+ Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "view_form_84report" || Session["ReportId"].ToString() == "view_form84_d_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[5];
                    Param[0] = new ReportParameter("Parameter1", Session["vat_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["Party_code"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["FromDate"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["ToDate"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"] + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if(Session["ReportId"].ToString() == "ena_dispatch_report"|| Session["ReportId"].ToString() == "ena_ethanol_dispatch_report" || Session["ReportId"].ToString() == "sds_dispatch_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[5];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["Pass_Type"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "vatwise_ena_dispatch_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[6];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["Pass_Type"].ToString());
                    Param[4] = new ReportParameter("Parameter5", Session["vat_code"].ToString());
                    Param[5] = new ReportParameter("Parameter6", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if(Session["ReportId"].ToString() == "Issue_ReportMNT" || Session["ReportId"].ToString() == "Issue_ReportMNT_WHL")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "RG3MTP"|| Session["ReportId"].ToString() == "RG2MTP" || Session["ReportId"].ToString() == "RG2MTP_WHL")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["vat_code"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["vat_code"].ToString());
                   // Param[2] = new ReportParameter("Parameter3", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }

                else if (Session["ReportId"].ToString() == "MTP_ReceiptRegister" || Session["ReportId"].ToString() == "MTW_ReceiptRegister") 
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["UserID"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["FromDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["ToDate"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "Consumption_ReportMNT")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["vat_code"].ToString());
                 //   Param[4] = new ReportParameter("Parameter5", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "CM_SEIZURE_DETAILS")
                {
                    string raidby = Session["SeizureNo"].ToString();
                    if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                        raidby = "E";
                    else
                        raidby = "P";
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/CM_SEIZURE_DETAILS";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["SeizureNo"].ToString());
                    Param[1] = new ReportParameter("Parameter2", raidby);
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    //if (Session["Type"].ToString() == "Print")
                    //{
                    //    string pdf_file = Server.MapPath("Pass_" + Session["Pass_No"].ToString()+".pdf");
                    //    ExportToPDF(pdf_file);
                    //}
                    //else
                    //{
                    //    string[] values = Session["Allvalues"].ToString().Split('_');
                    //    SaveWithDigital(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "Pass_" + Session["Pass_No"].ToString());
                    //}
                    ExportToexcel(ReportViewer1.ServerReport.ReportPath, Param.ToList(), "CM_SEIZURE_DETAILS_" + Session["SeizureNo"].ToString() + "_" + raidby);
                    ReportViewer1.LocalReport.Refresh();
                }

                else if (Session["ReportId"].ToString() == "view_state_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_state_report";
                    ReportViewer1.ServerReport.Refresh();
                }
               else if (Session["ReportId"].ToString() == "view_district_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_district_report";
                    ReportViewer1.ServerReport.Refresh();
                }
               else if (Session["ReportId"].ToString() == "view_module_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_module_report";
                    ReportViewer1.ServerReport.Refresh();
                }
               else if (Session["ReportId"].ToString() == "view_rolename_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_rolename_report";
                    ReportViewer1.ServerReport.Refresh();
                }
              else  if (Session["ReportId"].ToString() == "view_division_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_division_report";
                    ReportViewer1.ServerReport.Refresh();
                }
               else if (Session["ReportId"].ToString() == "view_submodule_report")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_submodule_report";
                    ReportViewer1.ServerReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "SummaryReport")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/SummaryReport";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    // ExportToPDF1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "ReceiptReport")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/ReceiptReport";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    // ExportToPDF1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh(); 

                }
                else if (Session["ReportId"].ToString().Trim() == "CaseReport7" || Session["ReportId"].ToString().Trim() == "CaseReport4")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString() + "/"+ Session["ReportId"].ToString();
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToexcel1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "ParameterRegisterReport")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/ParameterRegisterReport";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    // ExportToPDF1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Session["ReportId"].ToString() == "LabtechReport")
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/LabtechReport";
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    //for without parameter and generate pdf
                    // ExportToPDF1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString() + "_" + Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
                //else if (Session["ReportId"].ToString() == "CaseCountReport")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/CaseCountReport";
                //    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                //    Param[0] = new ReportParameter("Parameter1", Session["Raidby"].ToString());
                //    ReportViewer1.ShowParameterPrompts = false;
                //    ReportViewer1.ServerReport.SetParameters(Param);
                //    ReportViewer1.ServerReport.Refresh();
                //    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                //    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                //    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                //    ExportToPDF1(ReportViewer1.ServerReport.ReportPath, Session["ReportId"].ToString());
                //    ReportViewer1.LocalReport.Refresh();
                //}
                else  if (Session["ReportId"].ToString() == "view_tabname_report")    
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_tabname_report";
                    ReportViewer1.ServerReport.Refresh();
                }
                else
                {
                    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/" + Session["ReportId"].ToString();
                    ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                    Param[0] = new ReportParameter("Parameter1", Session["FromDate"].ToString());
                    Param[1] = new ReportParameter("Parameter2", Session["ToDate"].ToString());
                    Param[2] = new ReportParameter("Parameter3", Session["Party_code"].ToString());
                    Param[3] = new ReportParameter("Parameter4", Session["Reportfinancialyear"].ToString());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ServerReport.SetParameters(Param);
                    ReportViewer1.ServerReport.Refresh();
                    ReportViewer1.AsyncRendering = false; // Force inline/remove iFrame
                    ReportViewer1.SizeToReportContent = true; // Fit report to screen
                    ReportViewer1.ZoomMode = ZoomMode.FullPage;
                    ExportToPDF(ReportViewer1.ServerReport.ReportPath, Param.ToList(), Session["Party_code"].ToString()+ "_"+Session["ReportId"].ToString());
                    ReportViewer1.LocalReport.Refresh();
                }
               
                //if (Session["ReportId"].ToString() == "view_district_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_district_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
                //if (Session["ReportId"].ToString() == "view_module_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_module_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
                //if (Session["ReportId"].ToString() == "view_rolename_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_rolename_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
                //if (Session["ReportId"].ToString() == "view_division_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_division_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
                //if (Session["ReportId"].ToString() == "view_submodule_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_submodule_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
                //if (Session["ReportId"].ToString() == "view_tabname_report")
                //{
                //    ReportViewer1.ServerReport.ReportPath = "/" + Session["ReportProject1"].ToString()+"/view_tabname_report"; 
                //    ReportViewer1.ServerReport.Refresh();
                //}
            }
        }
        [Serializable]
      public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }


        }
        public void ExportToPDFfile(string pdf_file)
        {
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(pdf_file);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }

        }
        public void ExportToPDF(string path, List<ReportParameter> reportParams, string fileName)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password, server[0].server_domain);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
            viewer.ServerReport.ReportPath = "" + path;
            viewer.ServerReport.SetParameters(reportParams);
            byte[] bytes = viewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension,
                out streamIds, out warnings);
            File.WriteAllBytes(Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf"), bytes);
            //  txtpdf.Text = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "GetRequestData();", true);

            string FilePath = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }



        }



        public void ExportToexcel(string path, List<ReportParameter> reportParams, string fileName)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password, server[0].server_domain);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
            viewer.ServerReport.ReportPath = "" + path;
            viewer.ServerReport.SetParameters(reportParams);
            //byte[] bytes = viewer.ServerReport.Render("excel", null, out mimeType, out encoding, out extension,
            //    out streamIds, out warnings);
            //File.WriteAllBytes(Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf"), bytes);
            ////  txtpdf.Text = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            ////  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "GetRequestData();", true);

            //string FilePath = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(FilePath);
            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //}

            byte[] bytes = viewer.ServerReport.Render("excel", null, out mimeType, out encoding, out extension,
               out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download



        }


        public void ExportToexcel1(string path,  string fileName)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password, server[0].server_domain);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
            viewer.ServerReport.ReportPath = "" + path;
            //viewer.ServerReport.SetParameters(reportParams);
            //byte[] bytes = viewer.ServerReport.Render("excel", null, out mimeType, out encoding, out extension,
            //    out streamIds, out warnings);
            //File.WriteAllBytes(Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf"), bytes);
            ////  txtpdf.Text = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            ////  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "GetRequestData();", true);

            //string FilePath = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(FilePath);
            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //}

            byte[] bytes = viewer.ServerReport.Render("excel", null, out mimeType, out encoding, out extension,
               out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download



        }

        public void ExportToPDF1(string path, string fileName)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password, server[0].server_domain);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
            viewer.ServerReport.ReportPath = "" + path;
            //string _format = "";
            //if (Session["party_type_name"].ToString() == "Excise")
            //{
            //    _format = "excel";
            //}
            //else { _format = "PDF"; }
            byte[] bytes = viewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension,
                out streamIds, out warnings);
            File.WriteAllBytes(Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf"), bytes);
            //  txtpdf.Text = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "GetRequestData();", true);
            string FilePath = Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf");
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }



        }
        public void SaveWithDigital(string path, List<ReportParameter> reportParams,string[] values, string fileName)
        {

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(server[0].server_user, server[0].server_password, server[0].server_domain);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ServerReport.ReportServerUrl = new Uri(server[0].server_url);
            viewer.ServerReport.ReportPath = "" + path;
            viewer.ServerReport.SetParameters(reportParams);
            byte[] bytes = viewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension,
                out streamIds, out warnings);
            String file = Convert.ToBase64String(bytes);
            DataSet ds = new DataSet();
        
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/All_Approved_Docs/request_master.xml"));
            int n = 1;
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                if(n==2)
                {
                    item.InnerText= DateTimeOffset.UtcNow.ToString("o");
                }
                if (n == 3)
                {
                    item.InnerText ="123";
                }
                if (item.Name== "certificate")
                {
                   
                    foreach (XmlNode item1 in item)
                    {
                       if(item1.Attributes[0].Value=="SN")
                        {
                            item1.InnerText = values[0].ToString();
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
                                item1.NextSibling.InnerText = values[1].ToString();
                            }
                            if (item1.NextSibling.Name == "size")
                            {
                                item1.NextSibling.InnerText = values[2].ToString();
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
            string pfile = "C:\\Users\\Admin\\Downloads\\request.xml".Replace("\\",",");
            //doc.SelectSingleNode("request/certificate/").InnerText = DateTimeOffset.UtcNow.ToString("o");
            string xmlcontents = doc.InnerXml;
            var dataa = HttpUtility.UrlEncode(xmlcontents);
            Session["Pdf_Data"] = dataa;
            txtpdf.Value = file;
            dtime.Value= DateTimeOffset.UtcNow.ToString("o");
            dkey.Value= values[0].ToString();
            xy.Value= values[1].ToString();
            HW.Value= values[2].ToString();
            // var vvv = Session["Pdf_Data"];
            // Response.Redirect("HtmlPage1.html");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('HtmlPage11.html' ,'_blank');", true);
            // Response.Redirect("HtmlPage1.html");
            //string url = "http://127.0.0.1:1620";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //byte[] requestInFormOfBytes = System.Text.Encoding.UTF8.GetBytes(dataa);
            //request.Method = "POST";
            //request.ContentType = "text/xml;charset=utf-8";
            //request.ContentLength = requestInFormOfBytes.Length;
            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
            //requestStream.Close();
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
            //string receivedResponse = respStream.ReadToEnd();
            //Console.WriteLine(receivedResponse);
            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.LoadXml(receivedResponse);
            //string value = "";
            //foreach (XmlNode item in xmlDocument.DocumentElement.ChildNodes)
            //{
            //    Console.WriteLine(item.LastChild.InnerXml);
            //    value = item.LastChild.InnerText.ToString();
            //}
            //byte[] bytess = System.Convert.FromBase64String(value);
            //File.WriteAllBytes(Server.MapPath("~/All_Approved_Docs/" + fileName + ".pdf"), bytess);


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UserReports");
        }
        [WebMethod]

        public static String GetPDFData(string file)
        {

           byte[] pdfPath = System.IO.File.ReadAllBytes(file);
          
           // testpdf pdf = new testpdf();
          //  pdf.pdfdata = pdfPath;
        //  pdf.pdffiledata = Convert.ToBase64String(pdfPath, 0, pdfPath.Length);
            // return pdfPath;
           // var json = new JavaScriptSerializer().Serialize(pdf.pdffiledata);
            return Convert.ToBase64String(pdfPath, 0, pdfPath.Length); ;
        }

    }
    [Serializable]
    public class testpdf
    {
        public string pdffiledata { get; set; }
        public byte[] pdfdata { get; set; }
    }
}