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
    public partial class RawMaterialReceiptList : System.Web.UI.Page
    {
        List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();
        public  UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string userid = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {

                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["party_type"] = "DIS";
                    grdRawMaterialReceiptList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    if (user != null)
                    {

                        Session["rolename"] = user.role_name;
                        Session["partycode"] = user.party_code;
                        Session["party_type"] = user.party_type;
                        if (user.party_type == "M & tP" || user.party_code == "MTP" || user.party_code == "MTR" || user.party_code == "MTW")
                        {
                            MTP.Visible = true;
                            SCM.Visible = false;
                            SGR.Visible = false;
                            if(user.party_code == "MTR" || user.party_code == "MTW")
                            {
                                btnConsumption.Visible = false;
                            }
                            rawmaterial = new List<RawMaterialReceipt>();
                           //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                           rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                        }
                        else
                        {
                            MTP.Visible =false;
                            SCM.Visible =true;
                            SGR.Visible = true;
                            rawmaterial = new List<RawMaterialReceipt>();
                            rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                        }
                            int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                        if(value==1)
                        {
                            Response.Redirect("RMR_GrainBasedList");
                            //drawid.Visible = true;
                            //lnkRawReceipt.Visible =false;
                            //OtherRMR.Visible = true;
                        }
                        else if(value !=1)
                        {
                            int value1 = BL_RawMaterialReceipt.GetData(Session["partycode"].ToString());
                            if (value1 == 1)
                            {
                                if (user.party_type.Trim() == "ENA Distillery Unit")
                                {
                                    drawid.Visible = true;
                                    lnkRawReceipt.Visible = true;
                                    OtherRMR.Visible = true;
                                    SGR.Visible = false;
                                }
                                else
                                {
                                    drawid.Visible = false;
                                    if (user.party_type == "M & tP" || user.party_code == "MTP" || user.party_code == "MTR" || user.party_code == "MTW")
                                    {
                                        SGR.Visible = false;
                                    }
                                    else
                                    {
                                        SGR.Visible = true;
                                    }
                                }
                                
                            }
                            else
                            {
                                drawid.Visible = false;
                                if (user.party_type == "M & tP" || user.party_code == "MTP" || user.party_code == "MTR" || user.party_code == "MTW")
                                {
                                    SGR.Visible = false;
                                }
                                else
                                {
                                    SGR.Visible = true;
                                }
                            }

                        }
                     //   List<OpeningBalance> openingbalance = new List<OpeningBalance>();
                     //   openingbalance = BL_OpeningBalance.GetOpeningBalanceList(user.party_code, "");
                     //var partynames = from s in openingbalance
                     //                     where s.party_code == user.party_code && s.financial_year == user.financial_year
                     //                     select s;
                      
                     //   if (partynames.ToList()[0].record_status!="A")
                     //   {
                     //       //lnkRMR.Visible = false;
                     //       //lnkRawMaterialToFermenter.Visible = false;
                     //       //lnkFermentertoReceiver.Visible = false;
                     //       //lnkReceivertoStorage.Visible = false;
                     //       //lnkFromStoragetoDispatch.Visible = false;
                     //       //lnkDailyDispatchClosure.Visible = false;
                     //       //btnVATtansfers.Visible = false;
                     //       //lnkRawMaterialWastage.Visible = false;
                     //       //LinkButton1.Visible = false;
                     //       //btnIssue.Visible = false;
                     //       //btnConsumption.Visible = false;
                     //       //drawid.Visible = false;
                     //       //SGR.Visible = false;
                     //       Session["UserID"] = Session["UserID"];
                     //       Response.Redirect("OpeningBalanceList");
                     //   }
                        
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            grdRawMaterialReceiptList.DataSource = rawmaterial;
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                string status = (dr1.FindControl("lblstatus") as Label).Text;
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                if (status == "Issued" || status == "Approved")
                                {
                                   
                                    btn.Visible = true;
                                }
                                else
                                {
                                    btn.Visible = false;
                                }
                            }
                        }
                        else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
                        {
                            var list = from s in rawmaterial
                                       where s.party_code == user.party_code && s.record_status == "Y" && s.financial_year==user.financial_year
                                      // orderby s.rmr_entrydate descending
                                       select s;
                            grdRawMaterialReceiptList.DataSource = list.ToList();
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        else if (user.role_name == "Bond Officer" /*&& user.party_type == "M & tP"*/)
                        {
                            var list = from s in rawmaterial
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                       // orderby s.rmr_entrydate descending
                                       select s;
                            grdRawMaterialReceiptList.DataSource = list.ToList();
                            grdRawMaterialReceiptList.DataBind();
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        //else if(user.party_type == "M & tP")
                        //{
                        //    var list = from s in rawmaterial
                        //              where s.party_code == user.party_code
                        //               // orderby s.rmr_entrydate descending
                        //               select s;
                        //    grdRawMaterialReceiptList.DataSource = list.ToList();
                        //    grdRawMaterialReceiptList.DataBind();
                        //    SGR.Visible = false;

                        //}
                        else
                        {
                            var list = from a in rawmaterial
                                       where a.party_code == user.party_code && a.financial_year == user.financial_year
                                       // orderby s.rmr_entrydate descending
                                       select a;
                          
                      
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
        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if (Session["partycode"].ToString() == "MTR" || Session["partycode"].ToString() == "MTW")
            {
                Response.Redirect("MNTW_IssueList.aspx");
            }
            else
            {
                Response.Redirect("MNT_IssueList.aspx");
            }
           
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void btnConsumption_Click(object sender, EventArgs e)
        {
            Response.Redirect("MNT_ConsumptionList.aspx");
        }



        protected void lnkOB_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningBalance.aspx");
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("RMR_GrainBased");
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rmrfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["receipt_id"] = Convert.ToInt32(id);
            Session["rType"] = 1;
            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
            rawmaterials = BL_RawMaterialReceipt.GetList(Convert.ToInt32(Session["receipt_id"].ToString()), Session["rmrfinancial_year"].ToString());
           
                Response.Redirect("RawmaterialReceiptForm");
            
        }


        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rmrfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["receipt_id"] = Convert.ToInt32(id);
            Session["rType"] = 2;
            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
            rawmaterials = BL_RawMaterialReceipt.GetList(Convert.ToInt32(Session["receipt_id"].ToString()), Session["rmrfinancial_year"].ToString());
          
                Response.Redirect("RawmaterialReceiptForm");
           
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
            if (e.NewPageIndex == -1)
            {
                grdRawMaterialReceiptList.PageIndex = 0;
            }
            else
            {
                grdRawMaterialReceiptList.PageIndex = e.NewPageIndex;
            }
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["rwmrsearch"] != null && Session["rwmrtext"] != null)
            {
                ddsearch.SelectedValue = Session["rwmrsearch"].ToString();
                txtpage.Text = Session["rwmrtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        if (user.party_type == "M & tP")
                        {
                            MTP.Visible = true;
                            SCM.Visible = false;
                            SGR.Visible = false;
                            rawmaterial = new List<RawMaterialReceipt>();
                            //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    rawmaterial = BL_RawMaterialReceipt.Search1("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/", "-"));

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                                  //  rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());

                                }
                            }
                            else
                            {
                                rawmaterial = BL_RawMaterialReceipt.Search1("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                        else
                        {
                            MTP.Visible = false;
                            SCM.Visible = true;
                            SGR.Visible = true;
                            rawmaterial = new List<RawMaterialReceipt>();
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/", "-"));

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());

                                }

                            }
                            else
                            {
                                rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }

                    }
                }
            }
            else
            {

                if (user.party_type == "M & tP")
                {

                    rawmaterial = new List<RawMaterialReceipt>();
                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                }
                else
                {
                    rawmaterial = new List<RawMaterialReceipt>();
                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                }
            }

            if (Session["UserID"].ToString() == "Admin")
            {
                if (ddsearch.SelectedValue == "rmr_entrydate")
                {
                    var list = from s in rawmaterial
                               where s.rmr_entrydate == txtpage.Text
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                }
                else
                {

                    grdRawMaterialReceiptList.DataSource = rawmaterial;
                    grdRawMaterialReceiptList.DataBind();
                }

                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
            {
                if (ddsearch.SelectedValue == "rmr_entrydate")
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.rmr_entrydate == txtpage.Text && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                }
                else
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();

                }

                foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
            }
            else
            {
                if (ddsearch.SelectedValue == "rmr_entrydate")
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.rmr_entrydate == txtpage.Text && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                }
                else
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                }

            }
        }

        protected void OtherRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RMR_GrainBasedList"); 
        }
        protected void RMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void btnsgr_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void btnot_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RMR_GrainBasedList");
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user.party_type == "M & tP")
                {
                    MTP.Visible = true;
                    SCM.Visible = false;
                    SGR.Visible = false;
                    rawmaterial = new List<RawMaterialReceipt>();
                    //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                }
                else
                {
                    MTP.Visible = false;
                    SCM.Visible = true;
                    SGR.Visible = true;
                    rawmaterial = new List<RawMaterialReceipt>();
                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                }

                if (Session["UserID"].ToString() == "Admin")
                {
                    grdRawMaterialReceiptList.DataSource = rawmaterial;
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N"
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type == "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N"
                               // orderby s.rmr_entrydate descending
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
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();

                }


            }
            return rawmaterial.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                

                Session["rwmrsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                   
                    Session["rwmrtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (user.party_type == "M & tP")
                        {
                            MTP.Visible = true;
                            SCM.Visible = false;
                            SGR.Visible = false;
                            rawmaterial = new List<RawMaterialReceipt>();
                            //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    // rawmaterial = BL_RawMaterialReceipt.Search1("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/", "-"));
                                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());

                                }
                            }
                            else
                            {
                                rawmaterial = BL_RawMaterialReceipt.Search1("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                            }
                           
                        }
                        else
                        {
                            MTP.Visible = false;
                            SCM.Visible = true;
                            SGR.Visible = true;
                            rawmaterial = new List<RawMaterialReceipt>();
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                if(txtpage.Text.ToString().Length==10)
                                {
                                    //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());

                                }

                            }
                            else
                            {
                                rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                            }
                                
                        }
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            if(ddsearch.SelectedValue== "rmr_entrydate")
                            {
                                var list = from s in rawmaterial
                                           where s.rmr_entrydate == txtpage.Text
                                           // orderby s.rmr_entrydate descending
                                           select s;
                                grdRawMaterialReceiptList.DataSource = list.ToList();
                                grdRawMaterialReceiptList.DataBind();
                            }
                            else
                            {
                               
                                grdRawMaterialReceiptList.DataSource = rawmaterial;
                                grdRawMaterialReceiptList.DataBind();
                            }
                            
                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
                        {
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                var list = from s in rawmaterial
                                           where s.party_code == user.party_code && s.record_status != "N" && s.rmr_entrydate==txtpage.Text && s.financial_year == user.financial_year
                                           // orderby s.rmr_entrydate descending
                                           select s;
                                grdRawMaterialReceiptList.DataSource = list.ToList();
                                grdRawMaterialReceiptList.DataBind();
                            }
                            else
                            {
                                var list = from s in rawmaterial
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                           // orderby s.rmr_entrydate descending
                                           select s;
                                grdRawMaterialReceiptList.DataSource = list.ToList();
                                grdRawMaterialReceiptList.DataBind();

                            }

                            foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {
                                var list = from s in rawmaterial
                                           where s.party_code == user.party_code && s.rmr_entrydate==txtpage.Text && s.financial_year == user.financial_year
                                           // orderby s.rmr_entrydate descending
                                           select s;
                                grdRawMaterialReceiptList.DataSource = list.ToList();
                                grdRawMaterialReceiptList.DataBind();
                            }
                            else
                            {
                                var list = from s in rawmaterial
                                           where s.party_code == user.party_code && s.financial_year == user.financial_year
                                           // orderby s.rmr_entrydate descending
                                           select s;
                                grdRawMaterialReceiptList.DataSource = list.ToList();
                                grdRawMaterialReceiptList.DataBind();
                            }

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
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }

            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdRawMaterialReceiptList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdRawMaterialReceiptList.PageIndex = a - 1;
            }

            rawmaterial = new List<RawMaterialReceipt>();
            rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

            


            if (user != null)
            {

                Session["rolename"] = user.role_name;
                Session["partycode"] = user.party_code;
                if (user.party_type == "M & tP")
                {
                    MTP.Visible = true;
                    SCM.Visible = false;
                    SGR.Visible = false;
                    rawmaterial = new List<RawMaterialReceipt>();
                    //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                }
                else
                {
                    MTP.Visible = false;
                    SCM.Visible = true;
                    SGR.Visible = true;
                    rawmaterial = new List<RawMaterialReceipt>();
                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                }
                int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                if (value == 1)
                {
                    Response.Redirect("RMR_GrainBasedList");
                    //drawid.Visible = true;
                    //lnkRawReceipt.Visible =false;
                    //OtherRMR.Visible = true;
                }
                else if (value != 1)
                {
                    int value1 = BL_RawMaterialReceipt.GetData(Session["partycode"].ToString());
                    if (value1 == 1)
                    {
                        if (user.party_type.Trim() == "ENA Distillery Unit")
                        {
                            drawid.Visible = true;
                            lnkRawReceipt.Visible = true;
                            OtherRMR.Visible = true;
                            SGR.Visible = false;
                        }
                        else
                        {
                            drawid.Visible = false;
                            if (user.party_type == "M & tP")
                            {
                                SGR.Visible = false;
                            }
                            else
                            {
                                SGR.Visible = true;
                            }
                        }

                    }
                    else
                    {
                        drawid.Visible = false;
                        if (user.party_type == "M & tP")
                        {
                            SGR.Visible = false;
                        }
                        else
                        {
                            SGR.Visible = true;
                        }
                    }

                }


                if (Session["UserID"].ToString() == "Admin")
                {
                    grdRawMaterialReceiptList.DataSource = rawmaterial;
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type == "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
                //else if(user.party_type == "M & tP")
                //{
                //    var list = from s in rawmaterial
                //              where s.party_code == user.party_code
                //               // orderby s.rmr_entrydate descending
                //               select s;
                //    grdRawMaterialReceiptList.DataSource = list.ToList();
                //    grdRawMaterialReceiptList.DataBind();
                //    SGR.Visible = false;

                //}
                else
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();

                }
            }


        }

        protected void grdRawMaterialReceiptList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRawMaterialReceiptList.TopPagerRow;
            if (grdRawMaterialReceiptList.Rows.Count > 0)
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
            TextBox txtpages = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["rwmrsearch"] != null && Session["rwmrtext"] != null)
            {
                ddsearch.SelectedValue = Session["rwmrsearch"].ToString();
                txtpages.Text = Session["rwmrtext"].ToString();
            }

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

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rwmrsearch"] = null;
            Session["rwmrtext"] = null;
            rawmaterial = new List<RawMaterialReceipt>();
            rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());




            if (user != null)
            {

                Session["rolename"] = user.role_name;
                Session["partycode"] = user.party_code;
                if (user.party_type == "M & tP")
                {
                    MTP.Visible = true;
                    SCM.Visible = false;
                    SGR.Visible = false;
                    rawmaterial = new List<RawMaterialReceipt>();
                    //rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                    rawmaterial = BL_RawMaterialReceipt.GetMTPRaw(Session["UserID"].ToString());
                }
                else
                {
                    MTP.Visible = false;
                    SCM.Visible = true;
                    SGR.Visible = true;
                    rawmaterial = new List<RawMaterialReceipt>();
                    rawmaterial = BL_RawMaterialReceipt.GetRawmaterialList(Session["UserID"].ToString());
                }
                int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                if (value == 1)
                {
                    Response.Redirect("RMR_GrainBasedList");
                    //drawid.Visible = true;
                    //lnkRawReceipt.Visible =false;
                    //OtherRMR.Visible = true;
                }
                else if (value != 1)
                {
                    int value1 = BL_RawMaterialReceipt.GetData(Session["partycode"].ToString());
                    if (value1 == 1)
                    {
                        if (user.party_type.Trim() == "ENA Distillery Unit")
                        {
                            drawid.Visible = true;
                            lnkRawReceipt.Visible = true;
                            OtherRMR.Visible = true;
                            SGR.Visible = false;
                        }
                        else
                        {
                            drawid.Visible = false;
                            if (user.party_type == "M & tP")
                            {
                                SGR.Visible = false;
                            }
                            else
                            {
                                SGR.Visible = true;
                            }
                        }

                    }
                    else
                    {
                        drawid.Visible = false;
                        if (user.party_type == "M & tP")
                        {
                            SGR.Visible = false;
                        }
                        else
                        {
                            SGR.Visible = true;
                        }
                    }

                }


                if (Session["UserID"].ToString() == "Admin")
                {
                    grdRawMaterialReceiptList.DataSource = rawmaterial;
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type != "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else if (user.role_name == "Bond Officer" /*&& user.party_type == "M & tP"*/)
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();
                    foreach (GridViewRow dr1 in grdRawMaterialReceiptList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
                //else if(user.party_type == "M & tP")
                //{
                //    var list = from s in rawmaterial
                //              where s.party_code == user.party_code
                //               // orderby s.rmr_entrydate descending
                //               select s;
                //    grdRawMaterialReceiptList.DataSource = list.ToList();
                //    grdRawMaterialReceiptList.DataBind();
                //    SGR.Visible = false;

                //}
                else
                {
                    var list = from s in rawmaterial
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdRawMaterialReceiptList.DataSource = list.ToList();
                    grdRawMaterialReceiptList.DataBind();

                }
            }

        }
    }
}