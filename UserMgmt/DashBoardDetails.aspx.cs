using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class DashBoardDetails : System.Web.UI.Page
    {
        string query = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string district = Request.QueryString["label"].ToString();
                string viewno = Request.QueryString["viewno"].ToString();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Country"), new DataColumn("Salary") });
                //dt.Rows.Add(1, "John Hammond", "United States", 70000);
                //dt.Rows.Add(2, "Mudassar Khan", "India", 40000);
                //dt.Rows.Add(3, "Suzanne Mathews", "France", 30000);
                //dt.Rows.Add(4, "Robert Schidner", "Russia", 50000);

                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
                NpgsqlConnection cn = new NpgsqlConnection(connstring);

                if (viewno == "4" || viewno == "OVER STOPPAGE ( > 30 MINS )")
                {
                    if (district == "For Clarification/Referred Back")
                    {
                        query = "SELECT a.*,b.party_name FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='R' ";
                    }

                    else if (district == "Issued")
                    {
                        query = "SELECT a.*,b.party_name FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='I' ";
                    }

                    else if (district == "Pending")
                    {
                        query = "SELECT a.*,b.party_name FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='N' ";
                    }

                    else if (district == "Cancelled")
                    {
                        query = "SELECT ar.req_allotmentdate,ar.req_allotmentno,ar.final_allotmentno FROM exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id=ar.party_code WHERE ar.record_status = 'P'";
                    }
                    else /*if (district == "Recommended by Deputy Commissioner")*/
                    {
                        query = "SELECT a.*,b.party_name FROM exciseautomation.molasses_allotment_request a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.record_status='Y' ";
                    }
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    GridView2.Visible = false;
                    GridView3.Visible = false;
                    GridView4.Visible = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    lblHeading.Text = "List of Molasses Allocation  : " + district;
                    //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                }

                else if (viewno == "101")
                {

                    lblEI.Text = "Allotment Approval Pending at HO Deputy Commissioner";
                    query = "select req_allotmentno,req_allotmentdate,b.district_name ,ur.user_name as UnitName,qty_allotted_till_date from exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id =ar.user_id inner join exciseautomation.district_master b on ur.district_code= b.district_code where allotment_status  isnull ";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    query = "select req_allotmentno,req_allotmentdate,b.district_name ,ur.user_name as UnitName,qty_allotted_till_date from exciseautomation.molasses_allotment_request ar  inner join exciseautomation.user_registration ur on ur.user_id =ar.user_id inner join exciseautomation.district_master b on ur.district_code= b.district_code where allotment_status='Recommended By Deputy Commissioner'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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

                }

                else if (viewno == "1" || viewno == "LOCKS CLOSED AT DESTINATION")
                {
                    query = "select ur.user_name,entrydate,lag((ClosingBalance))  over(partition by partycode order by entrydate) as OpeningBalance,totalproduction,totalissued,closingbalance from exciseautomation.test t inner join exciseautomation.user_registration ur on ur.user_id=t.partycode where  ur.user_name='" + district + "'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    GridView2.Visible = true;
                    GridView3.Visible = false;
                    GridView4.Visible = false;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    lblHeading.Text = "Molasses Production Vs Dispatch - " + district;


                }
                else if (viewno == "18")
                {

                    query = "select *,totalnocquantity-totalliftedquantity as totalpendingquantity from exciseautomation.scm_viewethanoldispatchStatus";





                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    GridView2.Visible = false;
                    GridView3.Visible = false;
                    GridView4.Visible = false;
                    GridView10.Visible = true;
                    GridView10.DataSource = dt;
                    GridView10.DataBind();
                    lblHeading.Text = "Ethanol - NOC Dispatch Status";


                }

                else if (viewno == "2" || viewno == "LOCKS TAMPERED")
                {

                    if (district == "For Clarification/Referred Back")
                    {
                        query = "SELECT apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.customer_name," +
                            "  n.customer_address,n.customer_district,n.state ,n.valid_date,n.customer_tender_or_po,n.total_qty as nocquantity,n.issued_date FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id WHERE n.noc_status::text ~~ 'Referred%'::text AND n.noc_for::text = 'Molasses'::text and noc_number like '%EXM%'";
                    }

                    else if (district == "Issued")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.customer_name,n.customer_address,n.customer_district,n.state ,n.valid_date,n.customer_tender_or_po,n.total_qty as nocquantity,n.issued_date FROM exciseautomation.noc n JOIN exciseautomation.pass p ON p.rr_no::text = n.noc_number::text inner join exciseautomation.user_registration ur on ur.user_id=n.user_id" +
                            " WHERE n.issued  = '" + district + "' AND n.noc_for = 'Molasses' and noc_number like '%EXM%'";
                    }

                    else if (district == "Pending")
                    {
                        query = "SELECT apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.customer_name," +
                           "  n.customer_address,n.customer_district,n.state ,n.valid_date,n.customer_tender_or_po,n.total_qty as nocquantity,n.issued_date FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id WHERE n.noc_status::text = 'Created'::text AND n.noc_for::text = 'Molasses'::text and noc_number like '%EXM%'";
                    }

                    else if (district == "Cancelled")
                    {
                        query = "SELECT apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.customer_name," +
                           "  n.customer_address,n.customer_district,n.state ,n.valid_date,n.customer_tender_or_po,n.total_qty as nocquantity,n.issued_date FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id WHERE n.noc_status::text = 'Rejected'::text AND n.noc_for::text = 'Molasses'::text and noc_number like '%EXM%'";
                    }
                    else if (district == "Approved")
                    {
                        query = "SELECT apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.customer_name," +
                           "  n.customer_address,n.customer_district,n.state ,n.valid_date,n.customer_tender_or_po,n.total_qty as nocquantity,n.issued_date FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id WHERE n.noc_status::text = 'Approved by Commissioner '::text AND n.noc_for::text = 'Molasses'::text and noc_number like '%EXM%'";
                    }
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    GridView2.Visible = false;
                    GridView3.Visible = true;
                    GridView4.Visible = false;
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                    lblHeading.Text = "List of NOC Details of Molasses Export - " + district;

                    //Attribute to show the Plus Minus Button.





                    //Adds THEAD and TBODY to GridView.
                    //GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                else if (viewno == "103")
                {
                    lblEth.Text = "NOC Approval Pending at Assistant Commissioner";

                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Created' and noc_for ='Molasses'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Recommended By Assistant Commissioner' and noc_for ='Molasses'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Recommended By Deputy Commissioner' and noc_for ='Molasses'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                }
                else if (viewno == "3" || viewno == "OVER STOPPAGE ( > 30 MINS )")
                {
                    if (district == "For Clarification/Referred Back")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.issued_date,n.Customer_name,n.customer_address,n.customer_district,n.state,n.total_qty as nocquantity,valid_date,n.customer_tender_or_po,case when n.noc_status='Created' then 'Pending at Assistant Commissioner' when n.noc_status='Recommended By Assistant Commissioner' then 'Pending at Deputy Commissioner' when n.noc_status='Recommended By Deputy Commissioner' then 'Pending at Commissioner' when n.noc_status='Approved by Commissioner ' then 'Approved by Commissioner' end as ApplicationStatus,Issued FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id" +
                           " WHERE n.noc_status::text ~~ 'Referred%'::text AND n.noc_for::text = 'Ethanol'::text and noc_number like '%ETH%'";
                    }

                    else if (district == "Issued")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.issued_date,n.Customer_name,n.customer_address,n.customer_district,n.state,n.total_qty as nocquantity,valid_date,n.customer_tender_or_po,case when n.noc_status='Created' then 'Pending at Assistant Commissioner' when n.noc_status='Recommended By Assistant Commissioner' then 'Pending at Deputy Commissioner' when n.noc_status='Recommended By Deputy Commissioner' then 'Pending at Commissioner' when n.noc_status='Approved by Commissioner ' then 'Approved by Commissioner' end as ApplicationStatus,Issued FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id" +
                            " WHERE n.issued::text = 'Issued'::text AND n.noc_for::text = 'Ethanol'::text and noc_number like '%ETH%'";
                    }

                    else if (district == "Pending")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.issued_date,n.Customer_name,n.customer_address,n.customer_district,n.state,n.total_qty as nocquantity,valid_date,n.customer_tender_or_po,case when n.noc_status='Created' then 'Pending at Assistant Commissioner' when n.noc_status='Recommended by Assistant Commissioner' then 'Pending at Deputy Commissioner' when n.noc_status='Recommended By Deputy Commissioner' then 'Pending at Commissioner' when n.noc_status='Approved by Commissioner ' then 'Approved by Commissioner' end as ApplicationStatus,Issued FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id " +
                            " WHERE (n.noc_status::text = 'Created'::text or n.noc_status::text ~~ 'Recommended%'::text) AND n.noc_for::text = 'Ethanol'::text and noc_number like '%ETH%'";
                    }

                    else if (district == "Cancelled")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.issued_date,n.Customer_name,n.customer_address,n.customer_district,n.state,n.total_qty as nocquantity,valid_date,n.customer_tender_or_po,case when n.noc_status='Created' then 'Pending at Assistant Commissioner' when n.noc_status='Recommended By Assistant Commissioner' then 'Pending at Deputy Commissioner' when n.noc_status='Recommended By Deputy Commissioner' then 'Pending at Commissioner' when n.noc_status='Approved by Commissioner ' then 'Approved by Commissioner' when n.noc_status='Rejected' then 'Cancelled' end as ApplicationStatus,Issued FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id" +
                            " WHERE n.noc_status::text = 'Rejected'::text AND n.noc_for::text = 'Ethanol'::text and noc_number like '%ETH%'";
                    }
                    else if (district == "Approved")
                    {
                        query = "SELECT n.apply_date,n.noc_number,n.issued_nocno,ur.user_name as UnitName,n.issued_date,n.Customer_name,n.customer_address,n.customer_district,n.state,n.total_qty as nocquantity,valid_date,n.customer_tender_or_po,case when n.noc_status='Created' then 'Pending at Assistant Commissioner' when n.noc_status='Recommended By Assistant Commissioner' then 'Pending at Deputy Commissioner' when n.noc_status='Recommended By Deputy Commissioner' then 'Pending at Commissioner' when n.noc_status='Approved by Commissioner ' then 'Approved by Commissioner' end as ApplicationStatus,Issued FROM exciseautomation.noc n inner join exciseautomation.user_registration ur on ur.user_id=n.user_id" +
                            " WHERE n.noc_status::text ~~ 'Approved by Commissioner '::text AND n.noc_for::text = 'Ethanol'::text and noc_number like '%ETH%'";
                    }
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    GridView2.Visible = false;
                    GridView3.Visible = false;
                    GridView4.Visible = true;
                    GridView4.DataSource = dt;
                    GridView4.DataBind();
                    lblHeading.Text = "List of NOC Details of Ethanol - " + district;
                    //Attribute to show the Plus Minus Button.
                    //GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //Adds THEAD and TBODY to GridView.






                }
                else if (viewno == "102")
                {
                    lblEth.Text = "NOC Approval Pending at Assistant Commissioner";

                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Created' and noc_for ='Ethanol'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Recommended By Assistant Commissioner' and noc_for ='Ethanol'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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
                    query = "select noc_number,apply_date,customer_district,customer_name,noc_for,total_qty  from exciseautomation.noc n  where noc_status ='Recommended By Deputy Commissioner' and noc_for ='Ethanol'";
                    using (NpgsqlConnection con = new NpgsqlConnection(connstring))
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

                }

            }
        }
    }
}
