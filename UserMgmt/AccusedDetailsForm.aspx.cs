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
    public partial class AccusedDetailsForm : System.Web.UI.Page
    {
      static  List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
        List<District> Districts = new List<District>();
        List<ThanaMaster> Thana = new List<ThanaMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["seizureno"] = Session["seizureno"];
                Session["UserID"] = Session["UserID"];
                //if (Session["rtype"].ToString() == "0")
                //{
                //    ads = BL_cm_seiz_AccusedDetails.GetDetails(Session["seizureno"].ToString() + "&" + Session["RaidBy"].ToString());
                //}
                //else
                //{
                //    ads = BL_cm_seiz_AccusedDetails.GetDetails(Session["seizureno"].ToString() + "&" + Session["RaidBy"].ToString());
                //}
                List<State> statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
                ddlState.DataSource = statelist;
                ddlState.DataTextField = "State_Name";
                ddlState.DataValueField = "State_Code";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Select");
                List<cm_gender> _gender = new List<cm_gender>();
                _gender = BL_cm_gender.GetList();
                ddlGender.DataSource = _gender;
                ddlGender.DataTextField = "gender_name";
                ddlGender.DataValueField = "gender_code";
                ddlGender.DataBind();
                ddlGender.Items.Insert(0, "Select");
                List<cm_offence> _offence = new List<cm_offence>();
                _offence = BL_cm_offence.GetList();
                ddlDescriptionofoffence.DataSource = _offence;
                ddlDescriptionofoffence.DataTextField = "offence_name";
                ddlDescriptionofoffence.DataValueField = "offence_code";
                ddlDescriptionofoffence.DataBind();
                ddlDescriptionofoffence.Items.Insert(0, "Select");
                List<cm_caste> _caste = new List<cm_caste>();
               
                _caste = BL_cm_caste.GetList();
                ddlCaste.DataSource = _caste;
                ddlCaste.DataTextField = "caste_name";
                ddlCaste.DataValueField = "caste_code";
                ddlCaste.DataBind();
                ddlCaste.Items.Insert(0, "Select");
                var category_code1 = _caste.GroupBy(x => x.category_code).Select(y => y.First()).Distinct();

                ddlCasteCategory.DataSource = category_code1;
                ddlCasteCategory.DataTextField = "category_code";
                ddlCasteCategory.DataValueField = "category_code";
                ddlCasteCategory.DataBind();
                ddlCasteCategory.Items.Insert(0, "Select");
                List<cm_religion> _religion = new List<cm_religion>();
                _religion = BL_cm_religion.GetList();
                ddReligion.DataSource = _religion;
                ddReligion.DataTextField = "religion_name";
                ddReligion.DataValueField = "religion_code";
                ddReligion.DataBind();
                ddReligion.Items.Insert(0, "Select");
                List<cm_idproof> _idproof = new List<cm_idproof>();
                _idproof = BL_cm_idproof.GetList();
                ddlAccusedIdProof.DataSource = _idproof;
                ddlAccusedIdProof.DataTextField = "idproof_name";
                ddlAccusedIdProof.DataValueField = "idproof_code";
                ddlAccusedIdProof.DataBind();
                ddlAccusedIdProof.Items.Insert(0, "Select");
                List<cm_seiz_Accused_Status> accused_status = new List<cm_seiz_Accused_Status>();
                accused_status = BL_cm_seiz_Accused_Status.GetList();
                ddlAccusedStatus.DataSource = accused_status;
                ddlAccusedStatus.DataTextField = "accusedstatus_name";
                ddlAccusedStatus.DataValueField = "accusedstatus_code";
                ddlAccusedStatus.DataBind();
                ddlAccusedStatus.Items.Insert(0, "Select");
                //Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString()!="0")
                {
                    ads = BL_cm_seiz_AccusedDetails.GetDetails(Session["seizureno"].ToString() + "&" + Session["RaidBy"].ToString());
                    string tableId = Session["TableId"].ToString();
                    var ad = (from s in ads
                                where s.seizure_accused_details_id == Convert.ToInt32(Session["TableId"].ToString())
                              select s);
                    if(ad.ToList()[0].accusedname!="")
                    txtAccusedName.Text= ad.ToList()[0].accusedname;
                    ddlAccusedStatus.SelectedValue = ad.ToList()[0].accusedstatus_code;
                    if (ddlAccusedStatus.SelectedValue == "UNR")
                    {
                        d0.Visible = false;
                        d00.Visible = false;
                        d000.Visible = false;
                        d1.Visible = false;
                    }
                    //  ddlDescriptionofoffence.SelectedValue = ad.ToList()[0].offence_code;
                    //  ddlDescriptionofoffence.SelectedValue = "1";
                    ddlDescriptionofoffence.SelectedValue = ad.ToList()[0].offence_code;
                   
                    txtAge.Text= ad.ToList()[0].accused_age;
                    ddlGender.SelectedValue= ad.ToList()[0].gender_code;
                    txtSpouseName.Text= ad.ToList()[0].relativename;
                    ddReligion.SelectedValue= ad.ToList()[0].religion_code;
                   ddlCaste.SelectedValue = ad.ToList()[0].caste_code;
                   ddlCasteCategory.SelectedValue= ad.ToList()[0].category_code;
                    txtCasteDetails.Text= ad.ToList()[0].caste_details;
                    ddlAccusedIdProof.SelectedValue= ad.ToList()[0].idproof_code;
                    txtIDNo.Text = ad.ToList()[0].idno;
                    txtMarksOfIdentification.Text= ad.ToList()[0].identificaton_mark ;
                     txtOccupation.Text= ad.ToList()[0].occupation;
                    if(ad.ToList()[0].mobileno!="0")
                    txtMobileNo.Text= ad.ToList()[0].mobileno;
                   txtPermanentAddress.Text = ad.ToList()[0].permanentaddress;
                   txtPresentAddress.Text = ad.ToList()[0].presentaddress;
                    txtAlternate.Text = ad.ToList()[0].mobileno1;
                   // txtstate.Text= ad.ToList()[0].state_code;
                    txtthana.Text= ad.ToList()[0].thana_code1;
                    txtdistrict.Text = ad.ToList()[0].district_code1;
                    ddlState.SelectedValue = ad.ToList()[0].state_code;
                    ddlState_SelectedIndexChanged(sender, e);
                    ddlDistrict.SelectedValue = ad.ToList()[0].district_code;
                    ddlDistrict_SelectedIndexChanged(sender, e);
                    ddlThana.SelectedValue = ad.ToList()[0].thana_code;
                    if (ad.ToList()[0].breathtest == "P")
                        rdPositive.Checked = true;
                    else
                        rdNegative.Checked = true;
                    if (ad.ToList()[0].bloodtest == "Y")
                        RadioButton1.Checked = true;
                    else
                        RadioButton2.Checked = true;
                    txtBreathAnalyzer.Text = ad.ToList()[0].breathtest_result;
                    txtBloodTestResult.Text= ad.ToList()[0].bloodtest_result;
                    if (ad.ToList()[0].SDR_CAF != "")
                        ddlSDR_CAF.SelectedValue = ad.ToList()[0].SDR_CAF;
                    if (Session["rtype"].ToString()!="0")
                    {
                        search1.Visible = false;
                    }
                    if(Session["rtype"].ToString()=="1" || ad.ToList()[0].record_status=="Y")
                    {
                        txtAccusedName.ReadOnly = true;
                        ddlDescriptionofoffence.Enabled = false;
                        ddlDescriptionofoffence.Enabled = false;
                        rdPositive.Enabled = false;
                        RadioButton1.Enabled = false;
                        txtAge.ReadOnly = true;
                        ddlGender.Enabled = false;
                        txtSpouseName.Enabled = false;
                        ddReligion.Enabled = false;
                        ddlCasteCategory.Enabled = false;
                        ddlCaste.Enabled = false;
                        txtCasteDetails.Enabled = false;
                        ddlAccusedIdProof.Enabled = false;
                        txtIDNo.ReadOnly = true;
                        ddlSDR_CAF.Enabled = false;
                        txtMarksOfIdentification.ReadOnly = true;
                        txtOccupation.ReadOnly = true;
                        txtMobileNo.ReadOnly = true;
                        txtPermanentAddress.ReadOnly = true;
                        txtPresentAddress.ReadOnly = true;
                        txtBreathAnalyzer.ReadOnly = true;
                        txtBloodTestResult.ReadOnly = true;
                        txtdistrict.ReadOnly = true;
                       // txtstate.ReadOnly = true;
                        txtthana.ReadOnly = true;
                        ddlThana.Enabled = false;
                        ddlState.Enabled = false;
                        ddlDistrict.Enabled = false;
                        btnSaveasDraft.Visible = false;
                        btnCancel.Visible = false;
                        ddlAccusedStatus.Enabled = false;
                        rdNegative.Enabled = false;
                        rdPositive.Enabled = false;
                        RadioButton1.Enabled = false;
                        RadioButton2.Enabled = false;
                        txtAlternate.Enabled = false;
                    }
                }
            }
        }


      
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("AccusedDetailsList");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("BasicIformationForm");

        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnAccusedDetail_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("AccusedDetailsList");
        }
        protected void btnOffencesCommitted_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("OffencesCommittedList");

        }
        protected void btnCaseHistory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("CaseHistoryList");
        }

        protected void Chaddress_Click(object sender, EventArgs e)
        {
           txtPresentAddress.Text = txtPermanentAddress.Text;
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_AccusedDetails ad = new cm_seiz_AccusedDetails();
                ad.accusedstatus_code = ddlAccusedStatus.SelectedValue;
                ad.accusedname = txtAccusedName.Text;
                ad.offence_code = ddlDescriptionofoffence.SelectedValue;
                ad.seizureno=Convert.ToInt32(Session["seizureno"].ToString());
                ad.accused_age = txtAge.Text;
                ad.gender_code = ddlGender.SelectedValue;
                if (ad.accusedstatus_code == "UNR")
                    ad.gender_code = "";
                ad.relativename = txtSpouseName.Text;
                ad.religion_code = ddReligion.SelectedValue;
                if (ad.religion_code == "Select")
                    ad.religion_code = "";
                ad.category_code = ddlCasteCategory.SelectedValue;
                if (ad.category_code == "Select")
                    ad.category_code = "";
                ad.caste_code = ddlCaste.SelectedValue;
                if (ad.caste_code == "Select")
                    ad.caste_code = "";
                ad.caste_details = txtCasteDetails.Text;
                ad.idproof_code = ddlAccusedIdProof.SelectedValue;
                if (ad.idproof_code == "Select")
                    ad.idproof_code = "";
                ad.idno = txtIDNo.Text;
                ad.identificaton_mark = txtMarksOfIdentification.Text;
                ad.occupation = txtOccupation.Text;
                ad.mobileno = txtMobileNo.Text;
                if (ad.mobileno == null || ad.mobileno == "")
                    ad.mobileno = "0";
                ad.permanentaddress = txtPermanentAddress.Text;
                ad.presentaddress = txtPresentAddress.Text;
                ad.user_id = Session["UserID"].ToString();
                ad.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                ad.ipaddress = clientIPAddress.ToString();
                ad.mobileno1 = txtAlternate.Text;
                if (rdPositive.Checked)
                    ad.breathtest ="P";
                else
                    ad.breathtest ="N";
                ad.breathtest_result = txtBreathAnalyzer.Text;
                if (RadioButton1.Checked)
                    ad.bloodtest = "Y";
                else
                    ad.bloodtest = "N";
                ad.bloodtest_result = txtBloodTestResult.Text;
                ad.record_status = "N";
                if (ddlSDR_CAF.SelectedValue == "Select")
                    ad.SDR_CAF = "No";
                else
                    ad.SDR_CAF = ddlSDR_CAF.SelectedValue;
                ad.seizureno =Convert.ToInt32( Session["seizureno"].ToString());
                ad.state_code = ddlState.SelectedValue;
                ad.district_code = ddlDistrict.SelectedValue;
                ad.thana_code = ddlThana.SelectedValue;
                if(ddlState.SelectedValue !="BH")
                {
                    ad.district_code1 = txtdistrict.Text;
                    ad.thana_code1 = txtthana.Text;
                }
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype1"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_AccusedDetails.Insert(ad);
                else
                {
                    ad.seizure_accused_details_id =Convert.ToInt32( Session["TableId"].ToString());
                    val = BL_cm_seiz_AccusedDetails.Update(ad);
                }

                if(val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Session["seizureno"] = Session["seizureno"];
                    Response.Redirect("AccusedDetailsList");
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
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string raidby = "";
            if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                raidby = "E";
            else
                raidby = "P";
            string qery = "raidby='"+raidby+"' and ";
             if(txtAccusName.Text != "")
            {
                qery = "accusedname ilike'%" + txtAccusName.Text+"%'";
            }
            //if (txtIDProof.Text != "")
            //{
            //    if(qery=="")
            //    qery =qery+ " idno='" + txtIDProof.Text + "'";
            //    else
            //        qery = qery + " and idno='" + txtIDProof.Text+"'";
            //}
            if (txtFatherSpouseName.Text != "")
            {
                if (qery == "")
                    qery = qery+ " relativename ilike '%" + txtFatherSpouseName.Text + "%'";
                else
                    qery = qery + " and relativename ilike '%" + txtFatherSpouseName.Text + "%'";
            }
            if (txtMobiNo.Text != "")
            {
                if (qery == "")
                    qery = qery+ " mobileno ilike '%" + txtMobiNo.Text + "%'";
                else
                    qery = qery + " and mobileno ilike '%" + txtMobiNo.Text + "%'";
            }
            if (qery != "raidby='E' and ")
            {
                ads = new List<cm_seiz_AccusedDetails>();
                ads = BL_cm_seiz_AccusedDetails.GetSearchDetails(qery.ToString());
                 grdAccusedDetailsView.DataSource = ads;
                grdAccusedDetailsView.DataBind();
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Please Enter Atleast One Value");
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
            //if (txtAccusName.Text != "" && txtIDProof.Text == "" && txtFatherSpouseName.Text == "" && txtMobiNo.Text == "")
            //{
            //    var ad = (from s in ads
            //              where s.accusedname == txtAccusName.Text 
            //              select s);
            //    grdAccusedDetailsView.DataSource = ad.ToList();
            //    grdAccusedDetailsView.DataBind();
            //}

            //if (txtAccusName.Text != "" && txtIDProof.Text != "" && txtFatherSpouseName.Text == "" && txtMobiNo.Text == "")
            //{
            //    var ad = (from s in ads
            //              where s.accusedname == txtAccusName.Text && s.idno == txtIDProof.Text 
            //              select s);
            //    grdAccusedDetailsView.DataSource = ad.ToList();
            //    grdAccusedDetailsView.DataBind();
            //}
            //if (txtAccusName.Text != "" && txtIDProof.Text != "" && txtFatherSpouseName.Text != "" && txtMobiNo.Text=="" )
            //{
            //    var ad = (from s in ads
            //              where s.accusedname == txtAccusName.Text && s.idno == txtIDProof.Text && s.relativename == txtFatherSpouseName.Text 
            //              select s);
            //    grdAccusedDetailsView.DataSource = ad.ToList();
            //    grdAccusedDetailsView.DataBind();
            //}
            //if (txtAccusName.Text != "" && txtIDProof.Text != "" && txtFatherSpouseName.Text != "" && txtMobiNo.Text != "")
            //{
            //    var ad = (from s in ads
            //              where s.accusedname == txtAccusName.Text && s.idno == txtIDProof.Text && s.relativename == txtFatherSpouseName.Text && s.mobileno == txtMobiNo.Text
            //              select s);
            //    grdAccusedDetailsView.DataSource = ad.ToList();
            //    grdAccusedDetailsView.DataBind();
            //}
        }

        protected void chselect_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                foreach (GridViewRow row in grdAccusedDetailsView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[row.Cells.Count - 1].FindControl("chselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string adid = (row.Cells[row.Cells.Count - 2].FindControl("lbladid") as Label).Text;
                            var ad = (from s in ads
                                      where s.seizure_accused_details_id == Convert.ToInt32(adid)
                                      select s);
                            ddlDescriptionofoffence.SelectedValue = ad.ToList()[0].offence_code;
                            //ddlDescriptionofoffence.SelectedValue = "1";
                            //rdArrested.Checked = true;
                            rdPositive.Checked = true;
                            RadioButton1.Checked = true;
                            txtAge.Text = ad.ToList()[0].accused_age;
                            txtAccusedName.Text = ad.ToList()[0].accusedname;
                            ddlGender.SelectedValue = ad.ToList()[0].gender_code;
                            ddlAccusedStatus.SelectedValue = ad.ToList()[0].accusedstatus_code;
                            txtSpouseName.Text = ad.ToList()[0].relativename;
                            ddReligion.SelectedValue = ad.ToList()[0].religion_code;
                            ddlCaste.SelectedValue = ad.ToList()[0].caste_code;
                            ddlCasteCategory.SelectedValue= ad.ToList()[0].category_code;
                            txtCasteDetails.Text= ad.ToList()[0].caste_details;
                            ddlAccusedIdProof.SelectedValue = ad.ToList()[0].idproof_code;
                            txtIDNo.Text = ad.ToList()[0].idno;
                            txtMarksOfIdentification.Text = ad.ToList()[0].identificaton_mark;
                            txtOccupation.Text = ad.ToList()[0].occupation;
                            txtMobileNo.Text = ad.ToList()[0].mobileno;
                            txtPermanentAddress.Text = ad.ToList()[0].permanentaddress;
                            txtPresentAddress.Text = ad.ToList()[0].presentaddress;
                            txtBreathAnalyzer.Text = ad.ToList()[0].breathtest_result;
                            txtBloodTestResult.Text = ad.ToList()[0].bloodtest_result;

                            break;
                        }
                        else
                        {
                           
                            ddlDescriptionofoffence.SelectedValue = "Select";
                            //rdArrested.Checked = true;
                            rdPositive.Checked = false;
                            RadioButton1.Checked = false;
                            txtAge.Text ="";
                            txtAccusedName.Text ="";
                            ddlGender.SelectedValue = "Select";
                            ddlAccusedStatus.SelectedValue = "Select";
                            txtSpouseName.Text ="";
                            ddReligion.SelectedValue = "Select";
                            ddlCasteCategory.SelectedValue = "Select";
                            ddlCaste.SelectedValue = "Select";
                            txtCasteDetails.Text = "";
                            ddlAccusedIdProof.SelectedValue = "Select"; 
                            txtIDNo.Text ="";
                            txtMarksOfIdentification.Text = "Select"; 
                            txtOccupation.Text = "";
                            txtMobileNo.Text = ""; ;
                            txtPermanentAddress.Text ="";
                            txtPresentAddress.Text = "";
                            txtBreathAnalyzer.Text = "";
                            txtBloodTestResult.Text = "";
                        }
                    }
                }

            }
           
        }

        protected void grdAccusedDetailsView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAccusedDetailsView.PageIndex = e.NewPageIndex;
            string raidby = "";
            if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                raidby = "E";
            else
                raidby = "P";
            string qery = "raidby='" + raidby + "' and ";
            if (txtAccusName.Text != "")
            {
                qery = "accusedname ilike'%" + txtAccusName.Text + "%'";
            }
            //if (txtIDProof.Text != "")
            //{
            //    if(qery=="")
            //    qery =qery+ " idno='" + txtIDProof.Text + "'";
            //    else
            //        qery = qery + " and idno='" + txtIDProof.Text+"'";
            //}
            if (txtFatherSpouseName.Text != "")
            {
                if (qery == "")
                    qery = qery + " relativename ilike '%" + txtFatherSpouseName.Text + "%'";
                else
                    qery = qery + " and relativename ilike '%" + txtFatherSpouseName.Text + "%'";
            }
            if (txtMobiNo.Text != "")
            {
                if (qery == "")
                    qery = qery + " mobileno ilike '%" + txtMobiNo.Text + "%'";
                else
                    qery = qery + " and mobileno ilike '%" + txtMobiNo.Text + "%'";
            }
            if (qery != "")
            {
                ads = new List<cm_seiz_AccusedDetails>();
                ads = BL_cm_seiz_AccusedDetails.GetSearchDetails(qery.ToString());
                grdAccusedDetailsView.DataSource = ads;
                grdAccusedDetailsView.DataBind();
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Please Enter Atleast One Value");
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
        }


        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue =="BH")
            {
                Districts = new List<District>();
                Districts = BL_User_Mgnt.GetDistricts("");
                var org_master1 = from s in Districts
                                  where s.state_Code == ddlState.SelectedValue
                                  select s;
                ddlDistrict.DataSource = org_master1.ToList();
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                txtdistrict.Visible = false;
                txtthana.Visible = false;
                ddlDistrict.Visible =true;
                ddlThana.Visible = true;
            }
            else
            {
                txtthana.Visible = true;
                txtdistrict.Visible = true;
                ddlDistrict.Visible = false;
                ddlThana.Visible = false;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thana = new List<ThanaMaster>();
            Thana = BL_Thana.GetThanaList(ddlDistrict.SelectedValue);
            var org_master1 = from s in Thana
                              where s.district_code == ddlDistrict.SelectedValue
                              select s;
            ddlThana.DataSource = org_master1.ToList();
            ddlThana.DataTextField = "thana_name";
            ddlThana.DataValueField = "thana_code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, "Select");
        }
    }
}