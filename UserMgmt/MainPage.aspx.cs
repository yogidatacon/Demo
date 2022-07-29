using FusionCharts.Charts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MainPage : System.Web.UI.Page
    {
        string query = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPreviousPage = "";
            if (Request.UrlReferrer != null)
            {
                Session["UserID"] = Session["UserID"];
                // Session["UserName"] = Session["UserID"];
                strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

            }
            if (strPreviousPage == "")
            {
                Response.Redirect("~/LoginPage");
            }
            Session["UserID"] = Session["UserID"];
            string userid = Session["UserID"].ToString();
            // string userid = "Admin";
            Session["UserID"] = userid;
            string username = Session["Username"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(userid);
            Session["party_code"] = user.party_code;
            Session["party_type"] = user.party_type;
            int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
            if (value == 1)
            {
                btnSCM.Visible = true;
            }
            //if (!this.IsPostBack)
            //{
            RenderChartD2();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string district = Request.QueryString["label"]?.ToString() ?? string.Empty;
            string viewno = Request.QueryString["viewno"]?.ToString() ?? string.Empty;
            // btnprint.Visible = false;
            btnback.Visible = false;
            // btnprint3.Visible = false;
            btnback3.Visible = false;
            // btnprint4.Visible = false;
            btnback4.Visible = false;
            // btnprint7.Visible = false;
            btnback7.Visible = false;
            //btnprint10.Visible = false;
            btnback10.Visible = false;
            //btnprint5.Visible = false;
            btnback5.Visible = false;

            if (userid == "com" || userid == "hodyco")
            {
                rdbUnitType.Visible = true;
                if (rdbUnitType.SelectedValue == "C")
                {
                    Response.Redirect("CMS_DashBoard.aspx");
                }

            }

            if (rdbUnitType.SelectedValue.ToString() == "" && viewno == "")
            {
                rdbUnitType.SelectedValue = "S";
            }
            if ((rdbUnitType.SelectedValue.ToString() == "") && (viewno == "2" || viewno == "102"))
            {
                rdbUnitType.SelectedValue = "S";
            }
            else if ((rdbUnitType.SelectedValue.ToString() == "") && (viewno == "4" || viewno == "3" || viewno == "18" || viewno == "101"))
            {
                rdbUnitType.SelectedValue = "D";
            }
            Unittypeselction();


            if (user.party_type == "Sugar Mill")
            {
                rdbUnitType.Visible = false;
                sugarmill.Visible = true;
                distdiv.Visible = false;
                NOCApprovalStatusSugar.Visible = true;
                NOCApprovalStatusSugarList.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;

            }
            if (user.party_type == "Distillery Unit"/*|| user.party_type == "ENA Distillery Unit"*/)
            {
                rdbUnitType.Visible = false;
                NOCApprovalcharts.Visible = true;
                NOCApprovallist.Visible = false;
                MolassesAllocation.Visible = true;
                MolassesAllocationStatulist.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
                NOCDispatchStatus.Visible = false;
                sugarmill.Visible = false;
                distdiv.Visible = true;

            }
            if (Session["party_type"].ToString() == "M & tP" || Session["party_type"].ToString().Trim() == "ENA Distillery Unit")
            {
                rdbUnitType.Visible = false;
                NOCApprovalcharts.Visible =false;
                NOCApprovallist.Visible = false;
                MolassesAllocation.Visible =true;
                Distillery.Visible = false;
                MolassesAllocationStatulist.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible =false;
                NOCDispatchStatus.Visible = false;
                sugarmill.Visible = false;
                distdiv.Visible = true;
            }
            if (Session["party_type"].ToString() == "M & tP" )
            {
                molass.InnerText = "ENA Impure Spirit Allocation Status";
                MASD.InnerText = "ENA Impure Spirit Allocation Status At Different Stake Holders";
            }

                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            NpgsqlConnection cn = new NpgsqlConnection(conn);

            if (viewno == "4" || viewno == "OVER STOPPAGE ( > 30 MINS )")
            {
                if (district.Trim() == "Cancelled")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='R'  ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status = 'R'  and b.party_name = '" + username + "'";
                    }

                }

                else if (district.Trim() == "Issued")
                {


                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='I' ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status = 'I'  and a.user_id  = '" + userid + "'";
                    }
                }

                else if (district.Trim() == "Pending")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where  a.allotment_status Ilike 'Recommended%' ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.allotment_status Ilike 'Recommended%'  and a.user_id  = '" + userid + "'";
                    }
                }

                else if (district.Trim() == "Pending For Issue")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='A' or a.allotment_status Ilike 'Approved by Commissioner'";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status='A' or a.allotment_status Ilike 'Approved by Commissioner' and a.user_id  = '" + userid + "'";
                    }

                }
                else if (district.Trim() == "Recommended  by Deputy Commissioner")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='Y' and a.approver_level=1 ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status = 'Y' and a.approver_level=1 and a.user_id  = '" + userid + "'";
                    }
                }
                else if (district.Trim() == "For Clarification/Referred Back")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='B' ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status = 'B'  and a.user_id  = '" + userid + "'";
                    }
                }
                else if (district.Trim() == "Approved")
                {
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='A'  ";
                    }
                    else
                    {
                        query = " SELECT a.*,b.party_name, to_char(a.req_allotmentdate, 'DD/MM/YYYY') as allotmentdate, to_char(a.final_allotmentdate, 'DD/MM/YYYY') as finalallotmentdate  FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code = b.party_code where a.record_status = 'A'  and a.user_id  = '" + userid + "'";
                    }
                }

                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);

                    }

                }
                GridView1.Visible = true;
                // GridView2.Visible = false;
                //  GridView3.Visible = false;
                //  GridView4.Visible = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblHeading.Text =  "Molasses Allocation  -"+district;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                MolassesAllocation.Visible = false;
                MolassesAllocationStatulist.Visible = true;
                MolassesAllocationStatulist.Focus();
                //  btnprint.Visible = true;
                btnback.Visible = true;
                sugarmill.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
            }


            else if (viewno == "2" || viewno == "LOCKS TAMPERED")
            {

                if (district.Trim() == "Rejected by Deputy Commissioner")
                {

                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='R'";
                    }
                    else
                    {
                        query = " select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='R' and n.user_id ='" + userid + "'";
                    }

                }

                else if (district.Trim() == "Issued")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='I'";
                    }
                    else
                    {
                        query = " select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='I' and n.user_id='" + userid + "'";
                    }
                }

                else if (district.Trim() == "Pending")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='N'";
                    }
                    else
                    {
                        query = " select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='N' and n.user_id='" + userid + "'";
                    }
                }

                else if (district.Trim() == "Cancelled")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='C'";
                    }
                    else
                    {
                        query = " select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='C' and n.user_id='" + userid + "'";
                    }
                }
                else if (district.Trim() == "Refer Back to Deputy Commissioner")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='Y'";
                    }
                    else
                    {
                        query = " select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,p.party_name,n.issue_date,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'M%'and n.record_status='Y' and n.user_id='" + userid + "'";
                    }
                }
                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);

                    }

                }
                GridView1.Visible = false;
                //GridView2.Visible = false;
                GridView3.Visible = true;
                // GridView4.Visible = false;
                GridView3.DataSource = dt;
                GridView3.DataBind();
                lblHeading3.Text = "List of NOC Details of Molasses Export - " + district;

                //Attribute to show the Plus Minus Button.
                NOCApprovalStatusSugar.Visible = false;
                NOCApprovalStatusSugarList.Visible = true;
                NOCApprovalStatusSugarList.Focus();
                NOCStatusAtDifferentStakeHolders.Visible = false;
                // btnprint3.Visible = true;
                btnback3.Visible = true;
                distdiv.Visible = false;



                //Adds THEAD and TBODY to GridView.
                //GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


            else if (viewno == "3" || viewno == "OVER STOPPAGE ( > 30 MINS )")
            {
                if (district.Trim() == "Cancelled")
                {

                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='R'  ";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='R'  and n.user_id='" + userid + "'";
                    }


                }

                else if (district.Trim() == "Issued")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='I' ";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='I'  and n.user_id='" + userid + "'";
                    }
                }

                else if (district.Trim() == "Pending")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='Y' or n.noc_status Ilike 'Recommended%'";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='Y' or n.noc_status Ilike 'Recommended%' and n.user_id='" + userid + "'";
                    }
                }

                else if (district.Trim() == "Recommended  by Deputy Commissioner")
                {
                    if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%' and n.record_status='Y' and n.approver_level=1";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='Y' and n.approver_level=1 and n.user_id='" + userid + "'";
                    }
                }
                else if (district.Trim() == "For Clarification/Referred Back") 
                {
                    if (Session["UserID"].ToString() == "com" || userid == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='B' ";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='B' and n.user_id='" + userid + "'";
                    }
                }

                 else if (district.Trim() == "Approved") 
                {
                    if (Session["UserID"].ToString() == "com" || userid == "hodyco")
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='A' or n.noc_status Ilike 'Approved by Commissioner'";
                    }
                    else
                    {
                        query = "select to_char(n.nocdate, 'DD/MM/YYYY') as nocdate ,b.product_name,n.req_nocno,n.issue_nocno,to_char(n.issue_date, 'DD/MM/YYYY') as issue_date ,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,to_char(n.valid_upto, 'DD/MM/YYYY') as valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='A'  or n.noc_status Ilike 'Approved by Commissioner' and n.user_id='" + userid + "'";
                    }
                }
                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);

                    }

                }
                GridView1.Visible = false;
                //  GridView2.Visible = false;
                GridView3.Visible = false;
                GridView4.Visible = true;
                GridView4.DataSource = dt;
                GridView4.DataBind();
                lblHeading4.Text = "List of NOC Details of Ethanol - " + district;
                //Attribute to show the Plus Minus Button.
                //GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;

                //Adds THEAD and TBODY to GridView.
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
                MolassesAllocationStatulist.Visible = false;
                NOCApprovalcharts.Visible = false;
                NOCDispatchStatus.Visible = false;
                NOCApprovallist.Visible = true;
                NOCApprovallist.Focus();
                // btnprint4.Visible = true;
                btnback4.Visible = true;
                sugarmill.Visible = false;
            }
            else if (viewno == "18")
            {
                if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                {
                    query = "SELECT a.*,(totalnocquantity - totalliftedquantity)as totalpendingquantity,a.user_name FROM exciseautomation.nocqtydispatchdashboard a ";
                }
                else
                {
                    query = "SELECT a.*,(totalnocquantity - totalliftedquantity)as totalpendingquantity,a.user_name FROM exciseautomation.nocqtydispatchdashboard a where a.party_code='" + Session["party_code"].ToString() + "'";
                }


                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);

                    }

                }
                GridView1.Visible = false;
                // GridView2.Visible = false;
                GridView3.Visible = false;
                GridView4.Visible = false;
                GridView10.Visible = true;
                GridView10.DataSource = dt;
                GridView10.DataBind();
                lblHeading10.Text = "Ethanol - NOC Dispatch Status";
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
                MolassesAllocationStatulist.Visible = false;
                NOCApprovalcharts.Visible = false;
                NOCDispatchStatus.Visible = true;
                NOCDispatchStatus.Focus();
                NOCApprovallist.Visible = false;
                // btnprint4.Visible = true;
                btnback10.Visible = true;
                sugarmill.Visible = false;

            }

            else if (viewno == "101")
            {

                lblEI.Text = "Allotment Approval Pending at HO Deputy Commissioner";
                if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                {
                    query = "select ar.req_allotmentno,to_char(ar.req_allotmentdate, 'DD/MM/YYYY') as req_allotmentdate,ur.user_name as UnitName,reqd_qty from  exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id =ar.user_id where ar.record_status='Y' and ar.approver_level='0'";
                }
                else
                {
                    query = "select ar.req_allotmentno,to_char(ar.req_allotmentdate, 'DD/MM/YYYY') as req_allotmentdate,ur.user_name as UnitName,reqd_qty from  exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id =ar.user_id where ar.record_status='Y' and ar.approver_level='0' and ur.user_id='" + userid + "'";
                }

                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da1.Fill(dt1);

                    }

                }

                GridView5.Visible = true;
                GridView5.DataSource = dt1;
                GridView5.DataBind();

                lblCom.Text = "Allotment Approval Pending at Excise Commissioner";
                query = "select ar.req_allotmentno,to_char(ar.req_allotmentdate, 'DD/MM/YYYY') as req_allotmentdate,ur.user_name as UnitName,reqd_qty from  exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id =ar.user_id where ar.record_status='Y' and ar.approver_level='1'";
                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da2.Fill(dt2);

                    }

                }

                GridView5.Visible = true;
                GridView6.Visible = true;
                GridView6.DataSource = dt2;
                GridView6.DataBind();
                MolassesAllocation.Visible = false;
                MolassesAllocationStatulist.Visible = false;
                //  btnprint.Visible = true;
              btnback5.Visible = true;
                sugarmill.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = true;
                MolassesAllocationStatuDifferentStakeHolders.Focus();

            }
            else if (viewno == "102")
            {
                lblEth.Text = "NOC Approval Pending at Assistant Commissioner";
                if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                {
                    query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,n.issue_date,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.record_status='Y' and n.approver_level=0";
                }
                else
                {
                    query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,n.issue_date,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code inner join exciseautomation.user_registration f on n.user_id=f.user_id where b.product_name like'E%'and n.record_status='Y' and n.approver_level=0 and f.user_name='" + userid + "'";
                }


                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da2.Fill(dt2);

                    }

                }

                GridView7.Visible = true;
                GridView7.Visible = true;
                GridView7.DataSource = dt2;
                GridView7.DataBind();

                lblETHHODYCO.Text = "NOC Approval Pending at Deputy Commissioner";
                DataTable dt99 = new DataTable();
                query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,n.issue_date,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.noc_status Ilike 'Recommended By Assistant Commissione%' ";
                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da99 = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da99.Fill(dt99);

                    }

                }

                GridView8.Visible = true;
                GridView8.Visible = true;
                GridView8.DataSource = dt99;
                GridView8.DataBind();

                lblETHCOM.Text = "NOC Approval Pending at Excise Commissioner";
                DataTable dt98 = new DataTable();
                query = "select n.nocdate,b.product_name,n.req_nocno,n.issue_nocno,n.issue_date,p.party_name,n.noc_total_qty,c.cust_name,c.cust_address,n.valid_upto,n.tenderno from  exciseautomation.noc n inner join exciseautomation.party_master p on n.party_code=p.party_code inner join exciseautomation.customer_master c on n.customer_id = c.customer_id inner join exciseautomation.product_master b on n.noc_for=b.product_code where b.product_name like'E%'and n.noc_status Ilike 'Recommended By Deputy Commission%'";
                using (NpgsqlConnection con = new NpgsqlConnection(conn))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da98 = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da98.Fill(dt98);
                    }

                }

                GridView9.Visible = true;
                GridView9.Visible = true;
                GridView9.DataSource = dt98;
                GridView9.DataBind();
                NOCApprovalStatusSugar.Visible = false;
                NOCApprovalStatusSugarList.Visible = false;
                btnback7.Visible = true;
                NOCStatusAtDifferentStakeHolders.Visible = true;
                NOCStatusAtDifferentStakeHolders.Focus();
            }

            DistilleryWiseMolassesAllottedVsLifted(userid, username, user);
        }

        private void DistilleryWiseMolassesAllottedVsLifted(string userid, string username, UserDetails user)
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            DataTable dt6 = new DataTable();
            using (NpgsqlConnection con = new NpgsqlConnection(connstring))
            {
                //Distillery Wise Molasses Allotted Vs Lifted
                con.Open();
                if (Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
                {
                    //    query = "SELECT (ur.party_name) as user_name,case when a.allocatedquantity isnull then 0 else a.allocatedquantity end as allocatedquantity"
                    //+ " ,case when l.liftedquantity isnull then 0 else l.liftedquantity end as liftedquantity, case when(a.allocatedquantity - l.liftedquantity) isnull then 0 else  a.allocatedquantity - l.liftedquantity end as BalanceQuantity FROM exciseautomation.viewmolassesallocatedqty a inner join exciseautomation.viewmolassesliftedqty l on l.party_name = a.party_name right join exciseautomation.party_master ur on a.party_name = ur.party_name"
                    //+ " inner join exciseautomation.party_type_master b on ur.party_type_code = b.party_type_code where b.party_type_name = 'Distillery Unit'  order by ur.party_name";
                    query = "SELECT * FROM exciseautomation.viewmolassesallottedvslifted";
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, con))
                    {
                        // fill data table
                        da.Fill(dt6);

                    }
                }
                else if (user.party_type == "Distillery Unit")
                {
                    //      query = "SELECT (ur.party_name) as user_name,case when a.allocatedquantity isnull then 0 else a.allocatedquantity end as allocatedquantity"
                    //+ " ,case when l.liftedquantity isnull then 0 else l.liftedquantity end as liftedquantity, case when(a.allocatedquantity - l.liftedquantity) isnull then 0 else  a.allocatedquantity - l.liftedquantity end as BalanceQuantity FROM exciseautomation.viewmolassesallocatedqty a inner join exciseautomation.viewmolassesliftedqty l on l.party_name = a.party_name right join exciseautomation.party_master ur on a.party_name = ur.party_name"
                    //+ " inner join exciseautomation.party_type_master b on ur.party_type_code = b.party_type_code where b.party_type_name = 'Distillery Unit' and ur.party_name='" + username + "'  order by ur.party_name";
                    query = "SELECT * FROM exciseautomation.viewmolassesallottedvslifted where party_code='"+ Session["party_code"].ToString()+"'";
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, con))
                    {
                        // fill data table
                        da.Fill(dt6);

                    }

                }
            }


            if (dt6.Rows.Count > 0)
            {
                string sss__1 = string.Empty,
                    sss__2 = string.Empty,
                    sss__3 = string.Empty,
                    sss__4 = string.Empty,
                    sss__5 = string.Empty,
                    sss__6 = string.Empty,
                    sss__7 = string.Empty;
                string ppp__1 = string.Empty,
                  ppp__2 = string.Empty,
                  ppp__3 = string.Empty,
                  ppp__4 = string.Empty,
                  ppp__5 = string.Empty,
                  ppp__6 = string.Empty,
                  ppp__7 = string.Empty;
                string uuu__1 = string.Empty,
                      uuu__2 = string.Empty,
                      uuu__3 = string.Empty,
                      uuu__4 = string.Empty,
                      uuu__5 = string.Empty,
                      uuu__6 = string.Empty,
                      uuu__7 = string.Empty;
                string bbb__1 = string.Empty,
                 bbb__2 = string.Empty,
                 bbb__3 = string.Empty,
                 bbb__4 = string.Empty,
                 bbb__5 = string.Empty,
                 bbb__6 = string.Empty,
                 bbb__7 = string.Empty;
                string oo__1 = string.Empty,
                oo__2 = string.Empty,
                oo__3 = string.Empty,
                oo__4 = string.Empty,
                oo__5 = string.Empty,
                oo__6 = string.Empty,
                oo__7 = string.Empty;
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sss__1 = dt6.Rows[i]["user_name"].ToString();
                        ppp__1 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__1 = dt6.Rows[i]["BalanceQuantity"].ToString();
                         bbb__1 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__1 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 1)
                    {
                        sss__2 = dt6.Rows[i]["user_name"].ToString();
                        ppp__2 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__2 = dt6.Rows[i]["BalanceQuantity"].ToString();
                         bbb__2 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__2 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 2)
                    {
                        sss__3 = dt6.Rows[i]["user_name"].ToString();
                        ppp__3 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__3 = dt6.Rows[i]["BalanceQuantity"].ToString();
                         bbb__3 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__3 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 3)
                    {
                        sss__4 = dt6.Rows[i]["user_name"].ToString();
                        ppp__4 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__4 = dt6.Rows[i]["BalanceQuantity"].ToString();
                         bbb__4 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__4 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 4)
                    {
                        sss__5 = dt6.Rows[i]["user_name"].ToString();
                        ppp__5 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__5 = dt6.Rows[i]["BalanceQuantity"].ToString();
                         bbb__5 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__5 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 5)
                    {
                        sss__6 = dt6.Rows[i]["user_name"].ToString();
                        ppp__6 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__6 = dt6.Rows[i]["BalanceQuantity"].ToString();
                          bbb__6 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__6 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }
                    if (i == 6)
                    {
                        sss__7 = dt6.Rows[i]["user_name"].ToString();
                        ppp__7 = dt6.Rows[i]["liftedquantity"].ToString();
                        uuu__7 = dt6.Rows[i]["BalanceQuantity"].ToString();
                        bbb__7 = dt6.Rows[i]["Otherliftedquantity"].ToString();
                        oo__7 = dt6.Rows[i]["allocatedquantity"].ToString();
                    }


                }

                Dictionary<string, string> chartConfig5 = new Dictionary<string, string>();
                // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                Chart sales6 = new Chart();

                // Setting chart id
                sales6.SetChartParameter(Chart.ChartParameter.chartId, "myChart6");

                // Setting chart type to mscolumn2d chart
                sales6.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                // Setting chart width to 600px
                sales6.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                // Setting chart height to 350px
                sales6.SetChartParameter(Chart.ChartParameter.chartHeight, "500");

                // Setting chart data as JSON String (Uncomment below line
                sales6.SetData("{\n  \"chart\": {\n   \"exportEnabled\":\"1\", \n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"Distilleries\",\n    \"yaxisname\": \"Quintals\"," +
                   "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> Quintals of <b>$seriesName</b> in $label\",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                   "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + sss__1 + "\"\n        },\n        {\n          \"label\": \"" + sss__2 + "\"\n        },\n        {\n          \"label\": \"" + sss__3 + "\"\n        },\n        {\n          \"label\": \"" + sss__4 + "\"\n        },\n        {\n          \"label\": \"" + sss__5 + "\"\n        },\n        {\n          \"label\": \"" + sss__6 + "\"\n        }," +
                   "\n        {\n          \"label\": \"" + sss__7 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Allocated Quantity\",\n      " +
                   "\"data\": [\n        {\n          \"value\": \"" + oo__1 + "\"\n        },\n        {\n          \"value\": \"" + oo__2 + "\"\n        },\n        {\n          \"value\": \"" + oo__3 + "\"\n        },\n       " +
                   " {\n          \"value\": \"" + oo__4 + "\"\n        },{\n          \"value\": \"" + oo__5 + "\"\n        },{\n          \"value\": \"" + oo__6 + "\"\n        },{\n          \"value\": \"" + oo__7 + "\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Total Lifted Quantity\",\n    " +
                   "  \"data\": [\n        {\n          \"value\": \"" + ppp__1 + "\"\n        },\n        {\n          \"value\": \"" + ppp__2 + "\"\n        },\n        {\n          \"value\": \"" + ppp__3 + "\"\n        },\n        {\n          \"value\": \"" + ppp__4 + "\"\n        },\n        {\n          \"value\": \"" + ppp__5 + "\"\n        },\n        {\n          \"value\": \"" + ppp__6 + "\"\n        },\n    " +
                   " {\n          \"value\": \"" + ppp__7 + "\"\n        }\n      ]\n    },\n {\n      \"seriesname\": \"Molasses From Other Sources Total Lifted Quantity\",\n    " +
                   "  \"data\": [\n        {\n          \"value\": \"" + bbb__1 + "\"\n        },\n        {\n          \"value\": \"" + bbb__2 + "\"\n        },\n        {\n          \"value\": \"" + bbb__3 + "\"\n        },\n        {\n          \"value\": \"" + bbb__4 + "\"\n        },\n        {\n          \"value\": \"" + bbb__5 + "\"\n        },\n        {\n          \"value\": \"" + bbb__6 + "\"\n        },\n    " +
                   "    {\n          \"value\": \"" + bbb__7 + "\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Balance Quantity To Be Lifted\",\n   " +
                   "   \"data\": [\n        {\n          \"value\": \"" + uuu__1 + "\"\n        },\n        {\n          \"value\": \"" + uuu__2 + "\"\n        },\n        {\n          \"value\": \"" + uuu__3 + "\"\n        },\n        {\n          \"value\": \"" + uuu__4 + "\"\n        },\n        {\n          \"value\": \"" + uuu__5 + "\"\n        },\n        {\n          \"value\": \"" + uuu__6 + "\"\n        },\n     " +
                   "   {\n          \"value\": \"" + uuu__7 + "\"\n        }\n      ]\n    },\n    {\n      \"\": \"\",\n   " +
                   "   \"\": [\n        {\n          \"\": \"\"\n        },\n        {\n          \"\": \"\"\n        },\n        {\n          \"\": \"\"\n        },\n        {\n          \"\": \"\"\n        },\n        {\n          \"\": \"\"\n        },\n        {\n          \"\": \"\"\n        },\n     " +
                   "   {\n          \"\": \"\"\n        }\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                Literal4.Text = sales6.Render();

            }
        }

        private void RenderChartD2()
        {
            Chart chart4 = new Chart("pie2d", "fourth_chart", "520", "500", "jsonurl", "Handler/Allotment.ashx");
            Chart chart6 = new Chart("pie2d", "sixth_chart", "530", "500", "jsonurl", "Handler/NOCDashboard.ashx");
            Chart chart7 = new Chart("pie2d", "seventh_chart", "530", "500", "jsonurl", "Handler/NOCQtyDashboard.ashx");
            Chart chart12 = new Chart("pie2d", "eight_chart", "520", "500", "jsonurl", "Handler/StakeHolderMA.ashx");
            Chart chart13 = new Chart("pie2d", "ninth_chart", "520", "500", "jsonurl", "Handler/StakeHolderETHNOC.ashx");
            Chart chart10 = new Chart("pie2d", "tenth_chart", "520", "500", "jsonurl", "Handler/NOCDashboardSugarMill.ashx");
            Literal1.Text = chart10.Render();
            Literal3.Text = chart4.Render();
            Literal2.Text = chart12.Render();
            Literal5.Text = chart13.Render();
            Literal6.Text = chart6.Render();
            Literal7.Text = chart7.Render(); 
        }

        protected void rdbUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Unittypeselction();
        }

        private void Unittypeselction()
        {
            if (rdbUnitType.SelectedValue.ToString() == "S")
            {
                sugarmill.Visible = true;
                distdiv.Visible = false;
                NOCApprovalStatusSugar.Visible = true;
                NOCApprovalStatusSugarList.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
                NOCStatusAtDifferentStakeHolders.Visible = false;
              //  btnprint3.Visible = false;
                btnback3.Visible = false;
               // btnprint7.Visible = false;
                btnback7.Visible = false;
            }
            if (rdbUnitType.SelectedValue.ToString() == "D")
            {

                NOCApprovalcharts.Visible = true;
                NOCApprovallist.Visible = false;
                MolassesAllocation.Visible = true;
                MolassesAllocationStatulist.Visible = false;
                MolassesAllocationStatuDifferentStakeHolders.Visible = false;
                NOCDispatchStatus.Visible = false;
                sugarmill.Visible = false;
                distdiv.Visible = true;
               // btnprint4.Visible = false;
                btnback4.Visible = false;

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

            MolassesAllocation.Visible = true;
            NOCApprovallist.Visible = false;
            MolassesAllocationStatuDifferentStakeHolders.Visible = false;
            MolassesAllocationStatulist.Visible = false;
            sugarmill.Visible = false;
           
        }

        protected void btnBack3_Click(object sender, EventArgs e)
        {

            NOCApprovalStatusSugar.Visible = true;
           NOCApprovalStatusSugarList.Visible = false;
            distdiv.Visible = false;
        }

        protected void btnBack7_Click(object sender, EventArgs e)
        {

            NOCApprovalStatusSugar.Visible = true;
            NOCApprovalStatusSugarList.Visible = false;
            NOCStatusAtDifferentStakeHolders.Visible = false;
            distdiv.Visible = false;
        }
        protected void btnBack4_Click(object sender, EventArgs e)
        {
            NOCApprovalcharts.Visible = true;
            MolassesAllocationStatuDifferentStakeHolders.Visible = false;
            NOCApprovallist.Visible = false;
            sugarmill.Visible = false;
        }

        protected void btnback5_Click(object sender, EventArgs e)
        {
            MolassesAllocation.Visible = true;
            NOCApprovallist.Visible = false;
            MolassesAllocationStatulist.Visible = false;
            MolassesAllocationStatuDifferentStakeHolders.Visible = false;
            sugarmill.Visible = false;
        }

        protected void btnback10_Click(object sender, EventArgs e)
        {
            NOCApprovalcharts.Visible = true;
            NOCApprovallist.Visible = false;
            MolassesAllocationStatulist.Visible = false;
            MolassesAllocationStatuDifferentStakeHolders.Visible = false;
            NOCDispatchStatus.Visible = false;
            sugarmill.Visible = false;
        }

        protected void btnSCM_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("SCMDashBoard.aspx");
        }

        protected void btnEA_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage");
        }
    }
}