using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class VATTransferList : System.Web.UI.Page
    {
        List<VATTransfers_> vattransfer = new List<VATTransfers_>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                            sgr.Visible = false;
                        else
                            dst.Visible = false;
                        vattransfer = new List<VATTransfers_>();
                        vattransfer = BL_VATTransfers_.GetList(user.party_code,"");

                        if (Session["UserID"].ToString() == "Admin")
                        {
                           
                            SCM.Visible = true;
                            if (Session["party_type"].ToString() == "SGR")
                            {
                                dst.Visible = false;
                                sgr.Visible = true;
                                string a = "Sugar Mill";
                                vattransfer = BL_VATTransfers_.GetList(user.party_code,a);
                            }
                            else
                            {
                                string a = "Distillery Unit";
                                vattransfer = BL_VATTransfers_.GetList(user.party_code, a);
                                dst.Visible = true;
                                sgr.Visible = false;
                            }
                            var Vat_types = from s in vattransfer
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                            btnAddRecord.Visible = false;
                        }
                        else if (user.role_name == "Bond Officer")
                        {

                            var Vat_types = from s in vattransfer
                                            where s.record_status != "N" && s.financial_year==user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                            btnAddRecord.Visible = false;
                            foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                            {
                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                            }

                        }
                        else
                        {
                            var Vat_types = from s in vattransfer
                                            where  s.financial_year == user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                            //  btnAddRecord.Visible = false;
                        }
                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void btnRG4_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }

        protected void btnDMP_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }

        protected void btnMIR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("VATTransfer.aspx");
        }
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
        protected void lnkDailyDispatchClosure_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyDispatchClosureList");
        }
        protected void lnkRawMaterialToFermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList");
        }

        protected void lnkFermentertoReceiver_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FermentertoReceiverList");
        }

        protected void lnkReceivertoStorage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList");
        }

        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FromStoragetoDispatchList");
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string transferid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblvat_transfer_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["VTfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["transferid"] = transferid;
            Session["rtype"] = "2";
            Response.Redirect("VATTransfer.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string transferid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblvat_transfer_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["VTfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["transferid"] = transferid;
            Session["rtype"] = "1";
            Response.Redirect("VATTransfer.aspx");
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdVATTransfers.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                    sgr.Visible = false;
                else
                    dst.Visible = false;
                vattransfer = new List<VATTransfers_>();
                vattransfer = BL_VATTransfers_.GetList(user.party_code, "");

                if (Session["UserID"].ToString() == "Admin")
                {

                    SCM.Visible = true;
                    if (Session["party_type"].ToString() == "SGR")
                    {
                        dst.Visible = false;
                        sgr.Visible = true;
                        string a = "Sugar Mill";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, a);
                    }
                    else
                    {
                        string a = "Distillery Unit";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, a);
                        dst.Visible = true;
                        sgr.Visible = false;
                    }
                    var Vat_types = from s in vattransfer
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Bond Officer")
                {

                    var Vat_types = from s in vattransfer
                                    where s.record_status == "Y" && s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                    foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                }
                else
                {
                    var Vat_types = from s in vattransfer
                                    where s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                    //  btnAddRecord.Visible = false;
                }
            }
            return vattransfer.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdVATTransfers.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["vatfsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["vatftext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                            sgr.Visible = false;
                        else
                            dst.Visible = false;
                        if (ddsearch.SelectedValue == "transfered_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                vattransfer = new List<VATTransfers_>();
                                vattransfer = BL_VATTransfers_.GetList(user.party_code, "");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                vattransfer = new List<VATTransfers_>();
                                vattransfer = BL_VATTransfers_.GetList(user.party_code, "");

                            }
                        }
                        else
                        {
                            vattransfer = new List<VATTransfers_>();
                            vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, "");
                        }

                        if (Session["UserID"].ToString() == "Admin")
                        {

                            SCM.Visible = true;
                            if (Session["party_type"].ToString() == "SGR")
                            {
                                dst.Visible = false;
                                sgr.Visible = true;
                                string a = "Sugar Mill";
                                vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, a);
                            }
                            else
                            {
                                string a = "Distillery Unit";
                                vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, a);
                                dst.Visible = true;
                                sgr.Visible = false;
                            }
                            if (ddsearch.SelectedValue == "transfered_date")
                            {
                                var Vat_types = from s in vattransfer
                                                where s.transfered_date==txtpage.Text
                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                            }
                            else
                            {
                                var Vat_types = from s in vattransfer

                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                            }

                            btnAddRecord.Visible = false;
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "transfered_date")
                            {
                                var Vat_types = from s in vattransfer
                                                where s.transfered_date == txtpage.Text && s.record_status == "Y" && s.financial_year == user.financial_year
                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                                foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                                {
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }
                            }
                            else
                            {

                                var Vat_types = from s in vattransfer
                                                where s.record_status == "Y" && s.financial_year == user.financial_year
                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                                foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                                {
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }
                            }

                            btnAddRecord.Visible = false;
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "transfered_date")
                            {
                                var Vat_types = from s in vattransfer
                                                where s.transfered_date == txtpage.Text && s.financial_year == user.financial_year
                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                            }
                            else
                            {
                                var Vat_types = from s in vattransfer
                                                where s.financial_year == user.financial_year
                                                select s;
                                grdVATTransfers.DataSource = Vat_types.ToList();
                                grdVATTransfers.DataBind();
                            }
                            //  btnAddRecord.Visible = false;
                        }
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



        }



        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdVATTransfers.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdVATTransfers.PageIndex = a - 1;
            }
            else
            {
                grdVATTransfers.PageIndex = a;
            }
            

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                    sgr.Visible = false;
                else
                    dst.Visible = false;
                vattransfer = new List<VATTransfers_>();
                vattransfer = BL_VATTransfers_.GetList(user.party_code, "");

                if (Session["UserID"].ToString() == "Admin")
                {

                    SCM.Visible = true;
                    if (Session["party_type"].ToString() == "SGR")
                    {
                        dst.Visible = false;
                        sgr.Visible = true;
                        string b = "Sugar Mill";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, b);
                    }
                    else
                    {
                        string b = "Distillery Unit";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, b);
                        dst.Visible = true;
                        sgr.Visible = false;
                    }
                    var Vat_types = from s in vattransfer
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Bond Officer")
                {

                    var Vat_types = from s in vattransfer
                                    where s.record_status == "Y" && s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                    foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                }
                else
                {
                    var Vat_types = from s in vattransfer
                                    where s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    //  btnAddRecord.Visible = false;
                }
            }


            }

        protected void grdVATTransfers_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdVATTransfers.TopPagerRow;
            if (grdVATTransfers.PageCount != 0)
            {
                grdVATTransfers.TopPagerRow.Visible = true;
            }

            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");
            TextBox txtpages = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["vatfsearch"] != null && Session["vatftext"] != null)
            {
                ddsearch.SelectedValue = Session["vatfsearch"].ToString();
                txtpages.Text = Session["vatftext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdVATTransfers.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdVATTransfers.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdVATTransfers.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdVATTransfers.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdVATTransfers.PageIndex == 0)
            {
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdVATTransfers.PageIndex + 1 == grdVATTransfers.PageCount)
            {
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdVATTransfers.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void grdVATTransfers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(!IsPostBack)
            {
                if (e.NewPageIndex < 0)
                {
                    grdVATTransfers.PageIndex = 0;
                }
                else
                {

                    grdVATTransfers.PageIndex = e.NewPageIndex;
                }
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user != null)
                {
                    if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                        sgr.Visible = false;
                    else
                        dst.Visible = false;
                    GridViewRow row = grdVATTransfers.TopPagerRow;
                    TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                    if (Session["vatfsearch"] != null && Session["vatftext"] != null)
                    {
                        ddsearch.SelectedValue = Session["vatfsearch"].ToString();
                        txtpage.Text = Session["vatftext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "transfered_date")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {

                                        vattransfer = new List<VATTransfers_>();
                                        vattransfer = BL_VATTransfers_.GetList(user.party_code, "");
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        vattransfer = new List<VATTransfers_>();
                                        vattransfer = BL_VATTransfers_.GetList(user.party_code, "");

                                    }
                                }
                                else
                                {

                                    vattransfer = new List<VATTransfers_>();
                                    vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, "");
                                }
                            }
                        }
                    }
                    else
                    {
                        vattransfer = new List<VATTransfers_>();
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, "");
                    }

                    if (Session["UserID"].ToString() == "Admin")
                    {

                        SCM.Visible = true;
                        if (Session["party_type"].ToString() == "SGR")
                        {
                            dst.Visible = false;
                            sgr.Visible = true;
                            string a = "Sugar Mill";
                            vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, a);
                        }
                        else
                        {
                            string a = "Distillery Unit";
                            vattransfer = BL_VATTransfers_.Search("vat_transfer", ddsearch.SelectedValue, txtpage.Text, user.party_code, a);
                            dst.Visible = true;
                            sgr.Visible = false;
                        }
                        if (ddsearch.SelectedValue == "transfered_date")
                        {
                            var Vat_types = from s in vattransfer
                                            where s.transfered_date == txtpage.Text
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                        }
                        else
                        {
                            var Vat_types = from s in vattransfer
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                        }

                        btnAddRecord.Visible = false;
                    }
                    else if (user.role_name == "Bond Officer")
                    {
                        if (ddsearch.SelectedValue == "transfered_date")
                        {
                            var Vat_types = from s in vattransfer
                                            where s.transfered_date == txtpage.Text && s.record_status == "Y" && s.financial_year == user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                            foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                            {
                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                            }
                        }
                        else
                        {

                            var Vat_types = from s in vattransfer
                                            where s.record_status == "Y" && s.financial_year == user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                            foreach (GridViewRow dr1 in grdVATTransfers.Rows)
                            {
                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                            }
                        }

                        btnAddRecord.Visible = false;
                    }
                    else
                    {
                        if (ddsearch.SelectedValue == "transfered_date")
                        {
                            var Vat_types = from s in vattransfer
                                            where s.transfered_date == txtpage.Text && s.financial_year == user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                        }
                        else
                        {
                            var Vat_types = from s in vattransfer
                                            where s.financial_year == user.financial_year
                                            select s;
                            grdVATTransfers.DataSource = Vat_types.ToList();
                            grdVATTransfers.DataBind();
                        }
                        //  btnAddRecord.Visible = false;
                    }
                }
                }
            }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["vatfsearch"] = null;
            Session["vatftext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                    sgr.Visible = false;
                else
                    dst.Visible = false;
                vattransfer = new List<VATTransfers_>();
                vattransfer = BL_VATTransfers_.GetList(user.party_code, "");

                if (Session["UserID"].ToString() == "Admin")
                {

                    SCM.Visible = true;
                    if (Session["party_type"].ToString() == "SGR")
                    {
                        dst.Visible = false;
                        sgr.Visible = true;
                        string b = "Sugar Mill";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, b);
                    }
                    else
                    {
                        string b = "Distillery Unit";
                        vattransfer = BL_VATTransfers_.GetList(user.party_code, b);
                        dst.Visible = true;
                        sgr.Visible = false;
                    }
                    var Vat_types = from s in vattransfer
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Bond Officer")
                {

                    var Vat_types = from s in vattransfer
                                    where s.record_status == "Y" && s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    btnAddRecord.Visible = false;
                }
                else
                {
                    var Vat_types = from s in vattransfer
                                    where s.financial_year == user.financial_year
                                    select s;
                    grdVATTransfers.DataSource = Vat_types.ToList();
                    grdVATTransfers.DataBind();
                    //  btnAddRecord.Visible = false;
                }
            }


        }
    }
}