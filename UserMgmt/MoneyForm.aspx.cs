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
    public partial class MoneyForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        double currencytotal = 0;
        double coinstotal = 0;
        static cm_seiz_Money Money = new cm_seiz_Money();

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                Currency.Visible = false;
              //  Coins.Visible = false;
                lblNoofnotes.Visible = false;
                lblNoofcoins.Visible = true;
                string strPreviousPage = "";
              //  ddlAmountSeized_SelectedIndexChange(sender, null);
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                if(txtCoins.Text !="")
                {
                    coinstotal += Convert.ToDouble(txtCoins.Text);
                    txttotalCoins.Text = coinstotal.ToString();
                }
                if (txtCurrency.Text != "")
                {
                    currencytotal += Convert.ToDouble(txtCurrency.Text);
                    txttotalCoins.Text = currencytotal.ToString();
                }
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                //party_code.Value= user.party_code.ToString();
                // _party_code = party_code.Value;
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Money Type");
                    dt.Columns.Add("Currency");
                    dt.Columns.Add("No of Pieces");
                    dt.Columns.Add("Total");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }
                if (Session["UserID"].ToString() == "Admin")
                {
                    btnSaveasDraft.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    btnSaveasDraft.Visible = true;
                    btnCancel.Visible = true;
                }

                if (Session["rtype"].ToString() != "0")
                {
                    int a = Convert.ToInt32(Session["MoneyId"].ToString());
                    string b = Session["UserID"].ToString();

                    Money = new cm_seiz_Money();
                    Money = BL_Money.GetDetails(Session["UserID"].ToString(), a);
                    txtAmountSeized.Text = Money.total_amount.ToString();
                    txttotalCoins.Text = Money.coins.ToString();
                    txttotalCurrency.Text = Money.currency.ToString();
                    txtRemarks.Text = Money.remarks;


                    Doc_id = 0;
                    for (int i = 0; i < Money.currencyCoins.Count; i++)
                    {
                        //if (i == 0)
                            //dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(Money.currencyCoins[i].money_type, Money.currencyCoins[i].currency, Money.currencyCoins[i].noofpieces, Money.currencyCoins[i].amount);
                        grdMoney.DataSource = dt;
                        grdMoney.DataBind();
                        Doc_id++;
                    }

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        LinkButton1.Visible = false;
                        Currency.Visible = false;
                       // Coins.Visible = false;
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        ddlAmountSeized.Enabled = false;
                        txtAmountSeized.ReadOnly = true;
                        txtCoins.ReadOnly = true;
                        txtCurrency.ReadOnly = true;
                        txtNoofPieces.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txttotalCoins.ReadOnly = true;
                        txttotalCurrency.ReadOnly = true;
                        txtAmountSeized.Visible = false;
                        txttotalCurrency.Visible = false;
                        txttotalCoins.Visible = false;
                        totalid.Visible = false;
                        grdMoney.Columns[grdMoney.Columns.Count - 1].Visible = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MoneyList");
        }
        protected void ddlAmountSeized_SelectedIndexChange(object sender, EventArgs e)
        {
          if(ddlAmountSeized.SelectedItem.ToString()== "Rupee Note" || ddlAmountSeized.SelectedItem.ToString() == "Select")
            {
                lblNoofnotes.Visible = true;
                lblNoofcoins.Visible = false;
                Currency.Visible = true;
                Coins.Visible = false;
            }
          else
            {
                lblNoofnotes.Visible = false;
                lblNoofcoins.Visible = true;
                Currency.Visible = false;
                Coins.Visible = true;
            }
            
        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");
        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnVehicle_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");
        }
        protected void btnApparatus_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ApparatusList");
        }
        protected void btnProperty_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PropertyList");
        }
        protected void btnMoney_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");
        }
        int Doc_id = 0;
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
               double a = 0;
                double total = 0;
                double total1 = 0;
             
                if (txtCoins.Text !="")
                {
                    coinstotal += Convert.ToDouble(txtCoins.Text);
                    txttotalCoins.Text = coinstotal.ToString();
                    a =Convert.ToDouble(txtCoins.Text);
                    total = Convert.ToDouble(txtCoins.Text) * Convert.ToDouble(txtNoofPieces.Text);
                }
                if(txtCurrency.Text !="")
                {
                    currencytotal += Convert.ToDouble(txtCurrency.Text);
                   txttotalCurrency.Text = currencytotal.ToString();
                    a = Convert.ToInt32(txtCurrency.Text);
                    if (txtNoofPieces.Text == "")
                        txtNoofPieces.Text = "0";
                    total = Convert.ToDouble(txtCurrency.Text) * Convert.ToDouble(txtNoofPieces.Text);
                }


                string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                dt = (DataTable)ViewState["Records"];
                dt.Rows.Add(ddlAmountSeized.SelectedItem.ToString(),a, txtNoofPieces.Text,total, Doc_id);
                grdMoney.DataSource = dt;
                grdMoney.DataBind();
                Doc_id++;
                ddlAmountSeized.ClearSelection();
                grdMoney.Visible = true;
                txtNoofPieces.Text = "";
               txtCurrency.Text = "";
               txtCoins.Text = "";
                for (int i = 0; i < grdMoney.Rows.Count; i++)
                {
                    GridViewRow row1 = grdMoney.Rows[i];
                    string type = (row1.Cells[1].FindControl("lblMoneyType") as Label).Text;
                    //if(type== "Currency")
                    //{
                    //    string Qty1 = (row1.Cells[1].FindControl("lblCurrency") as Label).Text;
                    //    currencytotal += Convert.ToDouble(Qty1);
                    //    txttotalCurrency.Text = currencytotal.ToString();
                    //}
                    //else
                    //{
                    //    string Qty1 = (row1.Cells[1].FindControl("lblCurrency") as Label).Text;
                    //    coinstotal += Convert.ToDouble(Qty1);
                    //    txttotalCoins.Text = currencytotal.ToString();
                    //}
                    string Qty2 = (row1.Cells[1].FindControl("lblTotal") as Label).Text;
                    total1 += Convert.ToDouble(Qty2);
                    txtAmountSeized.Text = total1.ToString();
                    txtAmountSeized.ReadOnly = true;
                }

            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
               
                double total1 = 0;
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                //string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdMoney.DataSource = dt1;
               grdMoney.DataBind();
                //if (dt1.Rows.Count < 1)
                //    dummyDatatable.Visible = true;
                txtAmountSeized.Text = "0";
                txttotalCurrency.Text = "0";
                txttotalCoins.Text = "0";
                for (int i = 0; i < grdMoney.Rows.Count; i++)
                {
                    GridViewRow row1 = grdMoney.Rows[i];
                    string type = (row1.Cells[1].FindControl("lblMoneyType") as Label).Text;
                    if (type == "Rupee Note")
                    {
                        string Qty1 = (row1.Cells[1].FindControl("lblCurrency") as Label).Text;
                        string Qty11 = (row1.Cells[1].FindControl("lblNoofPieces") as Label).Text;
                        currencytotal += Convert.ToDouble(Qty1);
                        txttotalCurrency.Text = Qty11.ToString();
                    }
                    else
                    {
                        string Qty1 = (row1.Cells[1].FindControl("lblCurrency") as Label).Text;
                        string Qty11 = (row1.Cells[1].FindControl("lblNoofPieces") as Label).Text;
                        coinstotal += Convert.ToDouble(Qty1);
                        txttotalCoins.Text = Qty11.ToString();
                    }
                    string Qty2 = (row1.Cells[1].FindControl("lblTotal") as Label).Text;
                    total1 += Convert.ToDouble(Qty2);
                    txtAmountSeized.Text = total1.ToString();
                    txtAmountSeized.ReadOnly = true;
                }
                if(grdMoney.Rows.Count==0)
                {
                    txtAmountSeized.Text ="0";
                    txttotalCurrency.Text="0";
                    txttotalCoins.Text = "0";
                    txtAmountSeized.ReadOnly = true;
                }
              
            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                Money = new cm_seiz_Money();
                Money.total_amount = Convert.ToDouble(txtAmountSeized.Text);
                if(txttotalCurrency.Text =="")
                {
                   txttotalCurrency.Text = txtCurTotal.Value;
                   
                }
                if (txttotalCurrency.Text != "")
                {
                    Money.currency = Convert.ToDouble(txttotalCurrency.Text);
                    //var value= Convert.ToDouble(txttotalCurrency.Text).ToString("N2").Replace("$", "").Replace(",", "'");
                }
                if (txttotalCoins.Text == "")
                {
                    txttotalCoins.Text = txtCoiTotal.Value;
                }
                if (!string.IsNullOrEmpty(txttotalCoins.Text))
                {
                    Money.coins = Convert.ToDouble(txttotalCoins.Text);
                }
                else
                {
                    Money.coins = 0;
                }
                Money.user_id = Session["UserID"].ToString();

                Money.remarks = txtRemarks.Text;
                Money.record_status = "N";
                Money.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                Money.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                int i = 0;
                Money.currencyCoins = new List<cm_seiz_CurrencyCoins>();
                dt = ViewState["Records"] as DataTable;
                cm_seiz_CurrencyCoins currCoins = new cm_seiz_CurrencyCoins();
                foreach (DataRow dr in dt.Rows)
                {
                    currCoins = new cm_seiz_CurrencyCoins();
                    currCoins.seizureno = Money.seizureno.ToString();
                    currCoins.money_type= (grdMoney.Rows[i].FindControl("lblMoneyType") as Label).Text;
                    currCoins.currency = (grdMoney.Rows[i].FindControl("lblCurrency") as Label).Text;
                    currCoins.noofpieces = (grdMoney.Rows[i].FindControl("lblNoofPieces") as Label).Text;
                    currCoins.amount = (grdMoney.Rows[i].FindControl("lblTotal") as Label).Text;
                    currCoins.user_id = Session["UserID"].ToString();
                    
                    //doc.doc_name = dr["Doc_Name"].ToString();
                    //doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    //doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    Money.currencyCoins.Add(currCoins);
                    i++;
                }
 
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_Money.Insert(Money);
                else
                {
                    Money.seizure_moneydetails_id= Convert.ToInt32(Session["MoneyId"].ToString());
                    Money.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    val = BL_Money.Update(Money);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MoneyList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MoneyList");
        }





    }
}