using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Usermngt.BL.ReceivingSection;
using Usermngt.BL.DataUtility;
using Usermngt.Entities;
using Npgsql;

namespace UserMgmt
{
    public partial class ReceivingSectionList : System.Web.UI.Page
    {
        private readonly IReceivingSectionService _receivingSectionService = new ReceivingSectionProvider();
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

                LoadGrid();
            }
        }
        protected void AddEntryForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReceivingSectionForm.aspx");
        }

        protected void GridReceivingSectionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //Bhavin-03072022
                List<ReceivingSectionList1> receivingSectionList = new List<ReceivingSectionList1>();
                receivingSectionList = _receivingSectionService.LoadReceivingSectionDetails();
                var inputParams = e.CommandArgument.ToString().Split(',');
                //var receivingSectionId = inputParams.Length > 0 ? inputParams[0] : default(string);
                var exhibit_from = inputParams.Length > 1 ? inputParams[1] : default(string);
                
                var quant_received_id = inputParams.Length > 0 ? inputParams[0] : default(string);
                var form_no = inputParams.Length > 1 ? inputParams[1] : default(string);
                //Response.Redirect($"~/DistrictOfficerView.aspx?id={id}&district={district_name}");
                //Response.Redirect($"/ReceivingSectionForm.aspx?QMode=Edit&QId={44852}");
                //Bhavin-20220304
                Response.Redirect($"/ReceivingSectionForm.aspx?QMode=Edit&QId={quant_received_id}&form_no={form_no}");
                //Response.Redirect($"~/LabTechreportForm.aspx?form_no={form_no}&qid={quant_id}&status={status}");
                //Response.Redirect($"/ReceivingSectionForm.aspx?QMode=Edit&QId={receivingSectionId}&QExhibitfrom={exhibit_from}");
                //End
            }
        }

        protected void grdReceivingSectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReceivingSectionList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        #region Private Methods
        private void LoadGrid()
        {
            var receivingList = _receivingSectionService.LoadReceivingSectionDetails();
            grdReceivingSectionList.DataSource = receivingList;
            grdReceivingSectionList.DataBind();
        }
        #endregion
    }
}