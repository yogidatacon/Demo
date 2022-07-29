using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class PermitList : System.Web.UI.Page
    {
        List<Permit> raw = new List<Permit>();
        public UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string userid = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["rolename"] = user.role_name;
                        Session["partycode"] = user.party_code;
                        Session["partytypecode"] = user.party_type_code;

                        grdRawMaterialReceiptList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        raw = new List<Permit>();
                        raw = BL_Permit.GetList();

                        if (Session["UserID"].ToString() == "Admin")
                        {
                            grdRawMaterialReceiptList.DataSource = raw;
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            var list = from s in raw
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financial_year==user.financial_year
                                       // orderby s.rmr_entrydate descending
                                       select s;
                            grdRawMaterialReceiptList.DataSource = list.ToList();
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            AddRecords.Visible = false;
                        }
                        else if ( user.role_name.Trim() == "Assistant Commissioner")
                        {
                            var list = from s in raw
                                       where  s.record_status != "N" && s.district_code==user.district_code
                                       // orderby s.rmr_entrydate descending
                                       select s;
                            grdRawMaterialReceiptList.DataSource = list.ToList();
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            AddRecords.Visible = false;
                        }
                        else
                        {
                            var list = from s in raw
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       //orderby s.rmr_entrydate descending
                                       select s;
                            grdRawMaterialReceiptList.DataSource = list.ToList();
                            grdRawMaterialReceiptList.DataBind();

                        }
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Database Server Not Connecting");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
                else
                {
                    Response.Redirect("~/LoginPage");
                }
            }

        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("PermitForm.aspx");
            //Response.Redirect("WebForm1.aspx");
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Perfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["permit_id"] = Convert.ToInt32(id);
            Session["rType"] = 1;
            Response.Redirect("PermitForm.aspx");

        }


        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Perfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["permit_id"] = Convert.ToInt32(id);
            Session["rType"] = 2;
            Response.Redirect("PermitForm.aspx");

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
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
        protected void lnkGR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
        }

        protected void grdRawMaterialReceiptList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdRawMaterialReceiptList.PageIndex = 0;
            }
            else
            {
                grdRawMaterialReceiptList.PageIndex = e.NewPageIndex;
            }
           
            raw = new List<Permit>();
            raw = BL_Permit.GetList();

            if (Session["UserID"].ToString() == "Admin")
            {
                grdRawMaterialReceiptList.DataSource = raw;
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else if (Session["rolename"].ToString() == "Bond officer")
            {
                var list = from s in raw
                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
            }
            else if ( Session["rolename"].ToString().Trim() == "Assistant Commissioner")
            {
                var list = from s in raw
                           where s.party_code == user.party_code && s.record_status != "N"
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
            }
            else
            {
                var list = from s in raw
                           where s.party_code == Session["partycode"].ToString() && s.financial_year == user.financial_year
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();

            }
        }

        protected void RMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer; 
            string IndentNO = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            string partytype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartytypecode") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Perfinancial_year"] = financial_year;
            if (partytype == "MTP")
            {
                Session["ReportId"] = "Form52B";
            }
            else
            {
                Session["ReportId"] = "Form47";
            }
            
            Session["UserID"] = Session["UserID"].ToString();
            Session["AllotmentNo"] = IndentNO;
            Session["Type"] = "Print";
            //  Session["Allvalues"] = "030515c5_470,120_220,70";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void OtherRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RMR_GrainBasedList");
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if(a!=0)
            {
                grdRawMaterialReceiptList.PageIndex = a - 1;
            }
            else
            {
                grdRawMaterialReceiptList.PageIndex = a;
            }


            string userid = Session["UserID"].ToString();
            raw = new List<Permit>();
            raw = BL_Permit.GetList();

            if (Session["UserID"].ToString() == "Admin")
            {
                grdRawMaterialReceiptList.DataSource = raw;
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else if (Session["rolename"].ToString() == "Bond officer")
            {
                var list = from s in raw
                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
            }
            else if (Session["rolename"].ToString().Trim() == "Assistant Commissioner")
            {
                var list = from s in raw
                           where s.party_code == user.party_code && s.record_status != "N"
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();
                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
            }
            else
            {
                var list = from s in raw
                           where s.party_code == Session["partycode"].ToString() && s.financial_year == user.financial_year
                           // orderby s.rawmaterial_receipt_id descending
                           select s;
                grdRawMaterialReceiptList.DataSource = list.ToList();
                grdRawMaterialReceiptList.DataBind();

            }


        }
        protected void grdRawMaterialReceiptList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            if (grdRawMaterialReceiptList.PageCount != 0)
            {
                grdRawMaterialReceiptList.TopPagerRow.Visible = true;
            }

            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = grdRawMaterialReceiptList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdRawMaterialReceiptList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdRawMaterialReceiptList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRawMaterialReceiptList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdRawMaterialReceiptList.PageIndex == 0)
            {
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdRawMaterialReceiptList.PageIndex + 1 == grdRawMaterialReceiptList.PageCount)
            {
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRawMaterialReceiptList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
    }
}