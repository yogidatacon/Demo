using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MntpAlloctionRequestFrom : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        List<Product_Master> indents = new List<Product_Master>();
        List<WorkFlow> workflow = new List<WorkFlow>();
        // UserDetails user = new UserDetails();
        List<Party_Master> partymasters = new List<Party_Master>();
        //  partymasters = BL_Party_Master.GetList();
        int iddoc = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
                CalendarExtender1.StartDate = DateTime.Now;
                string strPreviousPage = "";
                btnReferBack.Visible = false;
                //  txtDATE.Enabled = false;

                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Doc_Name");
                        dt.Columns.Add("Discription");
                        dt.Columns.Add("Doc_Path");
                        dt.Columns.Add("Doc_id");
                        ViewState["Records"] = dt;
                    }
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {

                        if (user.party_type == "All" || user.party_type == "ALL")
                            //btnIndent.Visible = false;
                        // conformation.Value =Dycrypt( user.digi_password);
                        approverremaks.Visible = false;
                        captivecode.Value = user.party_captive_unit_name;
                        partycode.Value = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["usertype"] = user.party_type;
                        Session["party_code"] = user.party_code;
                        //ddlCaptive.SelectedIndex = 0;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        if (user.role_name == "Applicant")
                        {
                            valieddate11.Visible = false;
                            txtPartyname.Text = user.party_name;
                        }
                        if (user.role_name == "Applicant" || user.party_type == "ALL" || user.party_type == "All")
                        {
                            string[] values = (user.digi_id + "_470,120_220,70").ToString().Split('_');

                            dtime.Value = DateTimeOffset.UtcNow.ToString("o");
                            dkey.Value = values[0].ToString();
                            xy.Value = values[1].ToString();
                            HW.Value = values[2].ToString();
                            //List<Party_Master> partymasters = new List<Party_Master>();
                            partymasters = BL_Party_Master.GetProduct_Party();
                            txtfinancialyear.Text = user.financial_year;
                            indents = new List<Product_Master>();
                            indents = BL_ProductMaster.GetProductMasterList("");
                            if (user.role_name == "Applicant")
                            {
                                var ind = (from s in indents
                                           where s.product_type_code=="7"
                                           select s);
                                ddlMolassestype.DataSource = ind.ToList();
                                ddlMolassestype.DataTextField = "product_name";
                                ddlMolassestype.DataValueField = "product_code";
                                ddlMolassestype.DataBind();
                                ddlMolassestype.Items.Insert(0, "Select");
                                var ind1 = (from s in partymasters
                                            where s.party_code == user.party_code
                                            select s);
                                txtPartyname.Text = ind1.ToList()[0].party_name;
                                approverremaks.Visible = false;
                            }

                            if (Session["rtype"].ToString() != "0")
                            {
                                Molasses_Allocation allotment = new Molasses_Allocation();
                                allotment = BL_Molasses_Allocation.GetDetails(Session["Allotment_ID"].ToString(),Session["MNfinancial_year"].ToString());
                                allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                                partycode.Value = allotment.party_code;
                                txtfinancialyear.Text = allotment.financial_year;
                                txtDATE.Text = allotment.req_allotmentdate;
                                var ind = (from s in indents
                                           where s.product_type_code=="7"
                                           select s);
                                ddlMolassestype.DataSource = ind.ToList();
                                ddlMolassestype.DataTextField = "product_name";
                                ddlMolassestype.DataValueField = "product_code";
                                ddlMolassestype.DataBind();
                                ddlMolassestype.Items.Insert(0, "Select");
                                CalendarExtender.SelectedDate = Convert.ToDateTime(allotment.req_allotmentdate);
                                // if (allotment.req_allotmentdate!="")
                                txtdob.Value = allotment.req_allotmentdate;
                                ddlMolassestype.SelectedValue = allotment.product_code;
                                ddlMolassestype_SelectedIndexChanged(sender, null);
                                iscaptive.Value = allotment.iscaptive;
                                product.Value = allotment.product_code;
                               // ddlCaptive.SelectedValue = allotment.iscaptive;
                                var ind1 = (from s in partymasters
                                            where s.party_code == allotment.party_code
                                            select s);
                                txtPartyname.Text = ind1.ToList()[0].party_name;
                                captivecode.Value = allotment.requested_fromunit;
                                ddlCaptive_SelectedIndexChanged(sender, null);
                                ddlSugarmill.SelectedValue = allotment.requested_fromunit;
                                ddlSugarmill_SelectedIndexChanged(sender, null);
                                rtype.Value = allotment.record_status;
                                //txtProvisionalIndent.Text = allotment.prov_indent_qty.ToString();
                               // txtQuantityallottedtilldate.Text = allotment.approved_qty.ToString();
                                Session["qtilldate"] = allotment.qty_allotted_till_date.ToString();
                                txtMolassesRequiredQty.Text = allotment.reqd_qty.ToString();
                                txtAllotedQty.Text = allotment.approved_qty.ToString();
                                appqty.Value = allotment.approved_qty.ToString();
                                reqqty.Value = allotment.reqd_qty.ToString();
                                txtremarks.Text = allotment.remarks;
                                indentqty.Value = allotment.reqd_qty.ToString();
                                allotedqty.Value = allotment.qty_allotted_till_date.ToString();
                                txtValiedDate.Text = allotment.allotment_validdate;
                                if (allotment.allotment_validdate != null)
                                    CalendarExtender1.SelectedDate = Convert.ToDateTime(allotment.allotment_validdate);
                                //else
                                //    CalendarExtender1.SelectedDate = DateTime.Now;
                                txtvalieddate1.Value = allotment.allotment_validdate;
                                approverremaks.Visible = false;
                              
                                iddoc = 0;
                                Doc_id = 0;
                                for (int i = 0; i < allotment.docs.Count; i++)
                                {
                                    if (i == 0)
                                        dummytable.Visible = false;
                                    dt = (DataTable)ViewState["Records"];
                                    dt.Rows.Add("1", allotment.docs[i].doc_name, allotment.docs[i].description, allotment.docs[i].doc_path, allotment.docs[i].id);
                                    grdAdd.DataSource = dt;
                                    grdAdd.DataBind();

                                    Doc_id++;

                                }
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{
                                //    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                                //    {
                                //        //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                                //        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                                //    }
                                //}

                                if ((Session["rtype"].ToString() == "1"))
                                {
                                    foreach (GridViewRow dr1 in grdAdd.Rows)
                                    {
                                        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                        btn.Visible = false;
                                    }
                                    workflow = new List<WorkFlow>();
                                    workflow = BL_WorkFlow.ApprovelLevels("EIA", "198");
                                    //workflow = BL_WorkFlow.Checkworkflow("EIA", "198", user.role_name_code.ToString(), "", user.id.ToString());
                                    var aplevel = (from s in workflow
                                                   where s.user_registration_id == user.id.ToString()
                                                   select s);
                                    Session["wcount"] = workflow.Count;
                                    if (aplevel.ToList().Count > 0 || user.role_name == "Applicant")
                                    {
                                        if (user.role_name != "Applicant")
                                            currentlevel.Value = aplevel.ToList()[0].approver_level;
                                        //   Image1.Visible = false;
                                        btnCancel.Visible = false;
                                        btnSaveasDraft.Visible = false;
                                        btnSubmit.Visible = false;
                                        docs.Visible = false;
                                        ddlMolassestype.Enabled = false;
                                        btnReferBack.Visible = false;
                                      //  ddlCaptive.Enabled = false;
                                        ddlSugarmill.Enabled = false;
                                        txtMolassesRequiredQty.ReadOnly = true;
                                        txtDATE.Enabled = false;
                                        txtValiedDate.ReadOnly = true;
                                        txtremarks.ReadOnly = true;
                                        Image1.Visible = false;
                                        //  grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                                        List<All_Approvals> approvals = new List<All_Approvals>();
                                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["Allotment_ID"].ToString(), "MALT");
                                        var list4 = (from s in approvals
                                                     where s.financial_year == Session["MNfinancial_year"].ToString()
                                                     select s);
                                        grdApprovalDetails.DataSource = list4.ToList();
                                        approv.Visible = false;
                                        grdApprovalDetails.DataBind();
                                        valieddate11.Visible = false;
                                        //if (txtAllotedQty.Text == "0" || txtAllotedQty.Text == "")
                                        //    txtAllotedQty.Text = txtMolassesRequiredQty.Text;
                                        if (approvals.Count > 0)
                                        {
                                            approv.Visible = true;
                                            // valieddate.Visible = true;
                                        }
                                        if (allotment.record_status == "I")
                                        {
                                            valieddate11.Visible = true;
                                            ddlSugarmill.Enabled = false;
                                        }
                                        if (allotment.allotment_status != "N" && (allotment.record_status != "N"))
                                        {
                                            txtValiedDate.ReadOnly = true;
                                            txtAllotedQty.ReadOnly = true;
                                            Image2.Visible = false;
                                        }
                                        if (allotment.record_status == "I")
                                        {
                                            // grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                                        }
                                        else
                                        {
                                            if ((user.party_type == "All" || user.party_type == "ALL"))
                                            {
                                                if (currentlevel.Value == "2")
                                                 { 
                                                    valieddate11.Visible = true;
                                                    txtAllotedQty.ReadOnly = false;
                                                    txtValiedDate.ReadOnly = true;
                                                    Image1.Visible = false;
                                                    Image2.Visible = true;

                                                }
                                                if (currentlevel.Value == "3")
                                                {
                                                    valieddate11.Visible = true;
                                                    txtAllotedQty.ReadOnly = true;
                                                    txtValiedDate.ReadOnly = true;
                                                    Image1.Visible = false;
                                                    Image2.Visible = false;
                                                }
                                                if (currentlevel.Value == "1")
                                                {
                                                    valieddate11.Visible = false;
                                                    txtAllotedQty.ReadOnly = true;
                                                    txtValiedDate.ReadOnly = true;
                                                    Image1.Visible = false;
                                                    Image2.Visible = false;
                                                }
                                                    //btnIndent.Visible = false;


                                                    int workal = Convert.ToInt32(workflow[0].approver_level);
                                                //if ((allotment.approverlevel == (workal - 1) && allotment.record_status != "N") || (allotment.approverlevel ==0 && allotment.record_status == "B") || (allotment.approverlevel == 1 && allotment.record_status == "B"))
                                                //{
                                                    if (allotment.approverlevel == 0 || allotment.record_status == "B")
                                                    {
                                                        docs.Visible = true;
                                                        //if (ddlCaptive.SelectedValue != "Y")
                                                            ddlSugarmill.Enabled = true;
                                                        Image2.Visible = true;
                                                        Image1.Visible = false;
                                                        if (allotment.record_status != "A")
                                                        {
                                                            txtAllotedQty.ReadOnly = false;
                                                            //txtValiedDate.ReadOnly = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Image1.Visible = false;
                                                        if (allotment.record_status == "A")
                                                            docs.Visible = false;
                                                        else
                                                            docs.Visible = true;
                                                        ddlSugarmill.Enabled = false;
                                                    }

                                                    btnCancel.Visible = false;
                                                    btnSaveasDraft.Visible = false;
                                                    btnSubmit.Visible = false;
                                                    approverremaks.Visible = true;
                                                ddlSugarmill.Enabled = false;
                                                btnReferBack.Visible = false;
                                                    btnReject.Visible = false;
                                                    btnApprove.Visible = false;
                                                if(allotment.record_status == "Y" && allotment.allotment_status == "N"|| (currentlevel.Value == "1" && allotment.record_status == "B"))
                                                {
                                                    btnReject.Visible = true;
                                                    btnApprove.Visible = true;
                                                }
                                                if ((currentlevel.Value == "2" && allotment.allotment_status.Trim() == "Recommended  by Assistant Commissioner")|| (currentlevel.Value == "3" && allotment.allotment_status.Trim() == "Recommended  by Deputy Commissioner"))
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReferBack.Visible = true;
                                                    btnReject.Visible = false;
                                                    approverremaks.Visible = true;
                                                }
                                                if (user.role_name == "Commissioner" && allotment.record_status == "B" || (currentlevel.Value != "1" && allotment.allotment_status.Trim() == "Refer Back by Deputy Commissioner"))
                                                {
                                                    btnApprove.Visible = false;
                                                    btnReferBack.Visible = false;
                                                    btnReject.Visible = false;
                                                    approverremaks.Visible = false;
                                                    txtAllotedQty.ReadOnly = true;
                                                      docs.Visible = false;
                                                    txtValiedDate.ReadOnly = true;
                                                }
                                                if (currentlevel.Value == "2" && allotment.record_status == "B" &&( allotment.allotment_status.Trim() == "Refer Back by Commissioner"|| allotment.allotment_status.Trim() == "Refer Back by Joint Commissioner As Commissioner" || allotment.allotment_status.Trim() == "Refer Back by DY As Commissioner"))
                                                    {
                                                        btnApprove.Visible = true;
                                                        btnReferBack.Visible = true;
                                                        btnReject.Visible = false;
                                                        approverremaks.Visible = true;
                                                        txtAllotedQty.ReadOnly = false;
                                                        //  docs.Visible = false;
                                                        txtValiedDate.ReadOnly = true;
                                                    }

                                                    if (user.role_name == "Commissioner" && allotment.record_status != "B")
                                                    {
                                                        btnReferBack.Visible = true;
                                                        btnReject.Visible = false;
                                                        //  docs.Visible = false;
                                                    }
                                                    if (allotment.record_status == "A" || allotment.record_status == "I"||(allotment.record_status == "Y" && allotment.allotment_status.Trim() == "Recommended  by Commissioner"))
                                                    {
                                                        btnCancel.Visible = false;
                                                        btnSaveasDraft.Visible = false;
                                                        btnSubmit.Visible = false;
                                                        btnApprove.Visible = false;
                                                        btnReferBack.Visible = false;
                                                        btnReject.Visible = false;
                                                        approverremaks.Visible = false;
                                                        txtAllotedQty.ReadOnly = true;
                                                        docs.Visible = false;
                                                        txtValiedDate.ReadOnly = true;
                                                    }
                                                    if (allotment.record_status == "R")
                                                    {
                                                        btnApprove.Visible = false;
                                                        btnReferBack.Visible = false;
                                                        btnReject.Visible = false;
                                                        approverremaks.Visible = false;
                                                        txtAllotedQty.ReadOnly = true;
                                                        docs.Visible = false;
                                                        txtValiedDate.ReadOnly = true;
                                                    }


                                                    }
                                           // }
                                        }
                                    }
                                    else
                                    {

                                        Response.Redirect("~/User_Mgmt");
                                    }

                                }

                            }
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
                    Response.Redirect("~/User_Mgmt");
                }


            }

        }
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        private string Dycrypt(string dongle_password)
        {

            //  string val = encryption(dongle_password);
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(dongle_password.ToString());
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }


        }
        static int Doc_id = 1;
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add("", fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                        {
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                    Doc_id++;
                    txtDiscription.Text = "";


                }
            }

        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["Allotment_ID"].ToString(), v, Session["amparty_code"].ToString());
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                    FileInfo fInfoEvent;
                    fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                    fInfoEvent.Delete();

                }
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdAdd.DataSource = dt1;
                grdAdd.DataBind();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                    {
                        //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;


            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
                if (File.Exists(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                    Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
                else
                    Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }

        //[WebMethod]
        //public static string GetIndentQTY(Object product)
        //{
        //    string value = "";
        //    List<Party_Master> list = new List<Party_Master>();
        //    if (product != null)
        //    {
        //        string[] par = product.ToString().Split('_');
        //        var ind = (from s in indents
        //                   where s.product_code == par[0].ToString() && s.record_active == null && s.party_code == par[1].ToString()
        //                   select s);
        //        var allotedtilldate = from s1 in indents
        //                              where s1.product_code == par[0].ToString() && s1.record_active == null && s1.party_code == par[1].ToString()
        //                              group s1 by s1.product_code into playerGroup
        //                              select new
        //                              {
        //                                  product_code = playerGroup.Key,
        //                                  Totalalloted = playerGroup.Sum(x => x.molasses_allocated_qty),
        //                              };
        //        //List<Party_Master> partymasters = new List<Party_Master>();
        //        //partymasters = BL_Party_Master.GetProduct_Party();
        //        //list = (from s in partymasters
        //        //        where s.product_code==par[0]
        //        //        select s).ToList();

        //        if (ind.ToList().Count > 0)
        //            value = ind.ToList()[0].indent_qty.ToString();
        //        else
        //            value = "0";
        //        if (allotedtilldate.ToList().Count > 0)
        //            value = value + "_" + allotedtilldate.ToList()[0].Totalalloted.ToString();
        //        else
        //            value = value + "_0";
        //    }

        //    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        //    return jsonString;
        //}
        [WebMethod]
        public static string GetProductionQTY(Object partycode)
        {
            DailyMolassesProduction_e production = new DailyMolassesProduction_e();
            production = BL_DailyMolassesProduction.GetProductionQTY(partycode.ToString());
            return production.dailyproduction.ToString();
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {


            if (Session["rolename"].ToString() == "Applicant")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("MntpAlloctionRequestList.aspx");
            }
            else
            {
                if (Session["formid"].ToString() == "P")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MTPAllocation_P.aspx");
                }
                if (Session["formid"].ToString() == "A")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MTPAllocation_A.aspx");
                }
                if (Session["formid"].ToString() == "B")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MTPAllocation_B.aspx");
                }
                if (Session["formid"].ToString() == "I")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MTPAllocation_I.aspx");
                }
            }
        }
        [WebMethod]
        public static string CheckDuplicates(Object value)
        {
            string val = "";
            val = BL_Molasses_Allocation.GetValues(value.ToString());
            return val;
        }
        protected void btnIndent_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("IndentList");
        }


        protected void btnARM_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AllocationRequestList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.financial_year = txtfinancialyear.Text;
                allotment.req_allotmentdate = txtdob.Value;
                allotment.product_code = ddlMolassestype.SelectedValue;
                product.Value = ddlMolassestype.SelectedValue;
                //allotment.iscaptive = ddlCaptive.SelectedValue;

                allotment.requested_fromunit = ddlSugarmill.SelectedValue;
              //  allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);
                allotment.qty_allotted_till_date = Convert.ToDouble(txtQuantityallottedtilldate.Text);
                allotment.reqd_qty = Convert.ToDouble(txtMolassesRequiredQty.Text);
                allotment.record_status = "N";
                allotment.party_code = partycode.Value;
                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtremarks.Text;
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = dr["Doc_path"].ToString();
                    doc.description = dr["Discription"].ToString();
                    allotment.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Molasses_Allocation.Insert(allotment);
                else
                {
                    allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                    val = BL_Molasses_Allocation.Update(allotment);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MntpAlloctionRequestList.aspx");
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.financial_year = txtfinancialyear.Text;
                allotment.req_allotmentdate = txtdob.Value;
                allotment.product_code = ddlMolassestype.SelectedValue;
                product.Value = ddlMolassestype.SelectedValue;
               // allotment.iscaptive = ddlCaptive.SelectedValue;

                allotment.requested_fromunit = ddlSugarmill.SelectedValue;
                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.TexttxtQuantityallottedtilldate.Text
               if( txtQuantityallottedtilldate.Text !="")
                {
                    allotment.qty_allotted_till_date = Convert.ToDouble(txtQuantityallottedtilldate.Text);
                }
               else
                {
                    allotment.qty_allotted_till_date = 0;
                }
               
                allotment.reqd_qty = Convert.ToDouble(txtMolassesRequiredQty.Text);
                allotment.record_status = "Y";
                allotment.party_code = partycode.Value;
                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtremarks.Text;
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    allotment.docs.Add(doc);
                    i++;
                }
                string val;

                if (Session["rtype"].ToString() == "0")
                    val = BL_Molasses_Allocation.Insert(allotment);
                else
                {
                    allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                    val = BL_Molasses_Allocation.Update(allotment);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MntpAlloctionRequestList.aspx");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MntpAlloctionRequestList.aspx");
        }
        protected string GetValiedUser()
        {
            string valieduser = "";
            X509Certificate2 certClient = null;
            X509Store st = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            st.Open(OpenFlags.MaxAllowed);
            X509Certificate2Collection collection = st.Certificates;
            for (int i1 = 0; i1 < collection.Count; i1++)
            {
                foreach (X509Certificate2 cert in collection)
                {
                    certClient = cert;
                    string username = certClient.Subject;
                    string startdate = certClient.GetEffectiveDateString();
                    string enddate = certClient.GetExpirationDateString();
                    if (collection[i1].Subject.Contains("Name"))
                    {
                        certClient = collection[i1];
                    }
                }
            }
            st.Close();
            return valieduser;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtApproverComment.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Approver remarks');", true);
                    txtApproverComment.Focus();
                    txtApproverComment.Text = "";
                }
                else
                {
                    Molasses_Allocation allotment = new Molasses_Allocation();
                    allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                    if (ddlSugarmill.SelectedValue == "" || ddlSugarmill.SelectedValue == "Select")
                        ddlSugarmill.SelectedValue = captivecode.Value;
                    allotment.requested_fromunit = ddlSugarmill.SelectedValue;
                    allotment.financial_year = txtfinancialyear.Text;
                    allotment.qty_allotted_till_date = Convert.ToDouble(txtAllotedQty.Text);
                    // allotment.qty_allotted_till_date=Convert.ToDouble(appqty.Value);
                    allotment.allotment_validdate = txtvalieddate1.Value;
                    allotment.remarks = txtApproverComment.Text;

                    allotment.product_name = Session["wcount"] + "_" + currentlevel.Value + "_" + Session["rolename"];

                    allotment.record_status = "Y";
                    allotment.user_id = Session["UserID"].ToString();
                    allotment.party_code = Session["amparty_code"].ToString();
                    int i = 0;
                    dt = ViewState["Records"] as DataTable;
                    allotment.docs = new List<EASCM_DOCS>();
                    Session["griddata"] = dt;
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["Status"].ToString() == "")
                        {
                            EASCM_DOCS doc = new EASCM_DOCS();
                            doc.doc_name = dr["Doc_Name"].ToString();
                            doc.doc_path = dr["Doc_path"].ToString();
                            doc.description = dr["Discription"].ToString();
                            // approversummary = approversummary + "{!}" + doc.doc_name + "{!}" + doc.doc_path + "{!}" + doc.description;
                            allotment.docs.Add(doc);
                        }
                        i++;
                    }
                    //  approversummary= encryption(approversummary);
                    int value = BL_Molasses_Allocation.GetDigitalsignature(Session["UserID"].ToString());
                    if (value == 1)
                    //  if (Session["UserID"].ToString() == "com")
                    {
                        Session["ReportId"] = "allotment_letter";
                        allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                        Session["AllotmentNo"] = Session["Allotment_ID"].ToString();
                        Session["product_name"] = allotment.product_name;
                        Session["allotmentqty"] = allotment.qty_allotted_till_date;
                        Session["remarks"] = allotment.remarks;
                        string script = string.Format("sessionStorage.userId= '{0}';", "com");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('HtmlPage1.html' ,'');", true);
                        Response.Redirect("HtmlPage1.html");
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                    }
                    else
                    {
                        string val;

                        val = BL_Molasses_Allocation.ApproveMtpAllocation(allotment);

                        if (val == "0")
                        {
                            if (Session["formid"].ToString() == "P")
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("MTPAllocation_P.aspx");
                            }
                            if (Session["formid"].ToString() == "A")
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("MTPAllocation_A.aspx");
                            }
                            if (Session["formid"].ToString() == "B")
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("MTPAllocation_B.aspx");
                            }
                            if (Session["formid"].ToString() == "I")
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("MTPAllocation_I.aspx");
                            }
                        }
                        else
                        {
                            string message = "Server Error";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        }
                    }
                }
            }
        }
        public string encryption(String password)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (IsPostBack)
                {
                    Molasses_Allocation allotment = new Molasses_Allocation();
                    allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                    allotment.financial_year = txtfinancialyear.Text;

                    if (ddlSugarmill.SelectedValue == "" || ddlSugarmill.SelectedValue == "Select")
                        ddlSugarmill.SelectedValue = captivecode.Value;
                    allotment.requested_fromunit = ddlSugarmill.SelectedValue;
                    allotment.qty_allotted_till_date = Convert.ToDouble(txtAllotedQty.Text);
                  //  allotment.qty_allotted_till_date=Convert.ToDouble(appqty.Value);
                    allotment.allotment_validdate = txtvalieddate1.Value;
                    allotment.remarks = txtApproverComment.Text;
                    allotment.product_name = Session["wcount"] + "_" + currentlevel.Value + "_" + Session["rolename"];
                    allotment.record_status = "R";
                    allotment.user_id = Session["UserID"].ToString();
                    allotment.party_code = Session["amparty_code"].ToString();
                    int i = 0;
                    dt = ViewState["Records"] as DataTable;
                    allotment.docs = new List<EASCM_DOCS>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Status"].ToString() == "")
                        {
                            EASCM_DOCS doc = new EASCM_DOCS();
                            doc.doc_name = dr["Doc_Name"].ToString();
                            doc.doc_path = dr["Doc_path"].ToString();
                            doc.description = dr["Discription"].ToString();
                            allotment.docs.Add(doc);
                        }
                        i++;
                    }
                    string val;
                    val = BL_Molasses_Allocation.ApproveMtpAllocation(allotment);
                    if (val == "0")
                    {
                        if (Session["formid"].ToString() == "P")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_P.aspx");
                        }
                        if (Session["formid"].ToString() == "A")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_A.aspx");
                        }
                        if (Session["formid"].ToString() == "B")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_B.aspx");
                        }
                        if (Session["formid"].ToString() == "I")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_I.aspx");
                        }
                    }
                    else
                    {
                        string message = "Server Error";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }

        protected void btnReferBack_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (IsPostBack)
                {
                    Molasses_Allocation allotment = new Molasses_Allocation();
                    allotment.molasses_allotment_request_id = Session["Allotment_ID"].ToString();
                    if (ddlSugarmill.SelectedValue == "" || ddlSugarmill.SelectedValue == "Select")
                        ddlSugarmill.SelectedValue = captivecode.Value;
                    allotment.requested_fromunit = ddlSugarmill.SelectedValue;
                    allotment.qty_allotted_till_date = Convert.ToDouble(txtAllotedQty.Text);
                    allotment.financial_year = txtfinancialyear.Text;
                    // allotment.qty_allotted_till_date=Convert.ToDouble(appqty.Value);
                    allotment.allotment_validdate = txtvalieddate1.Value;
                    allotment.remarks = txtApproverComment.Text;
                    allotment.product_name = Session["wcount"] + "_" + currentlevel.Value + "_" + Session["rolename"];
                    allotment.record_status = "B";
                    allotment.user_id = Session["UserID"].ToString();
                    allotment.party_code = Session["amparty_code"].ToString();
                    int i = 0;
                    dt = ViewState["Records"] as DataTable;
                    allotment.docs = new List<EASCM_DOCS>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Status"].ToString() == "")
                        {
                            EASCM_DOCS doc = new EASCM_DOCS();
                            doc.doc_name = dr["Doc_Name"].ToString();
                            doc.doc_path = dr["Doc_path"].ToString();
                            doc.description = dr["Discription"].ToString();
                            allotment.docs.Add(doc);
                        }
                        i++;
                    }
                    string val;
                    val = BL_Molasses_Allocation.ApproveMtpAllocation(allotment);

                    if (val == "0")
                    {
                        if (Session["formid"].ToString() == "P")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_P.aspx");
                        }
                        if (Session["formid"].ToString() == "A")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_A.aspx");
                        }
                        if (Session["formid"].ToString() == "B")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_B.aspx");
                        }
                        if (Session["formid"].ToString() == "I")
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("MTPAllocation_I.aspx");
                        }
                    }
                    else
                    {
                        string message = "Server Error";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }

        protected void ddlMolassestype_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlMolassestype.SelectedValue != "Select")
            {
                //indents = new List<Indent_Form>();
                //indents = BL_IndentForm.GetList();
                //var ind = (from s in indents
                //           where s.product_code == ddlMolassestype.SelectedValue && s.record_active == null && s.party_code == partycode.Value
                //           select s);
                //var allotedtilldate = from s1 in indents
                //                      where s1.product_code == ddlMolassestype.SelectedValue && s1.record_active == null && s1.party_code == partycode.Value
                //                      group s1 by s1.product_code into playerGroup
                //                      select new
                //                      {
                //                          product_code = playerGroup.Key,
                //                          Totalalloted = playerGroup.Sum(x => x.molasses_allocated_qty),
                //                      };
                List<Party_Master> partymasters = new List<Party_Master>();
                partymasters = BL_Party_Master.GetList();
                var list = (from s in partymasters
                            where s.party_type_code == "ENA"
                            select s).ToList();
                ddlSugarmill.DataSource = list.ToList();
                ddlSugarmill.DataTextField = "Party_name";
                ddlSugarmill.DataValueField = "party_code";
                ddlSugarmill.DataBind();
                ddlSugarmill.Items.Insert(0, "Select");
                //txtProvisionalIndent.Text = ind.ToList()[0].indent_qty.ToString();
               Molasses_Allocation production = new Molasses_Allocation();
                production = BL_Molasses_Allocation.GetQTY(partycode.Value, ddlMolassestype.SelectedValue);
                txtQuantityallottedtilldate.Text = production.qty_allotted_till_date.ToString();
                CalendarExtender.EndDate = DateTime.Now;
            }
        }

        protected void ddlCaptive_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ddlCaptive.SelectedValue == "Y")
            //    {
            //        if (captivecode.Value != "")
            //        {

            //            ddlSugarmill.SelectedValue = captivecode.Value;
            //            ddlSugarmill.Enabled = false;
            //        }
            //        else
            //            ddlSugarmill.Enabled = true;
            //    }
            //    else
            //    {
            //        if (ddlSugarmill.SelectedValue != "" && Session["rtype"].ToString() == "0")
            //            ddlSugarmill.SelectedValue = "Select";
            //        //else
            //        //    ddlSugarmill.SelectedValue = captivecode.Value;
            //        ddlSugarmill.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ddlCaptive.SelectedValue = "Select";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append("In Captive Unit Selected Molasses Type is Not Available");
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //}

        }

        protected void ddlSugarmill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMolassestype.SelectedValue != "Select" && ddlSugarmill.SelectedValue != "Select")
            {
                string party = ddlSugarmill.SelectedValue + "_" + ddlMolassestype.SelectedValue;
                DailyMolassesProduction_e production = new DailyMolassesProduction_e();
                production = BL_DailyMolassesProduction.GetProductionQTY(party.ToString());
                txtProduction.Text = production.dailyproduction.ToString();
            }
        }
    }
}