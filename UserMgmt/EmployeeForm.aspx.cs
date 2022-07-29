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
    public partial class EmployeeForm : System.Web.UI.Page
    {
        List<Division> divisions = new List<Division>();
        List<District> Districts = new List<District>();
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
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                GetAllDropDownValues();
                if (Session["rtype"].ToString() != "0")
                {
                    Employee_Details emp = new Employee_Details();
                    emp = BL_Employee_Details.GetDetails(Session["EmpID"].ToString());
                    txtid.Value = emp.employee_master_id;
                    txtempid.Text = emp.emp_code;
                    txtfName.Text = emp.emp_name;
                    txtDOB.Text = emp.dob;
                    txtdob1.Value = emp.dob;
                    txtage.Text = emp.age;
                    txtPresentAddress.Text = emp.present_address;
                    txtpermanentaddress.Text = emp.permanent_address;
                    ddlState.SelectedValue = emp.state_code;
                    ddlState_SelectedIndexChanged(sender, null);
                    ddlDivision.SelectedValue = emp.division_code;
                    ddlDivision_SelectedIndexChanged(sender, null);
                    ddDistrict.SelectedValue = emp.district_code;
                    ddlBank.SelectedValue = emp.bank;
                    txtBranch.Text = emp.branch;
                    txtiefc.Text = emp.ifsc;
                    txtAccountNo.Text = emp.account_no;
                    txtpancard.Text = emp.pancard;
                    txtadharcard.Text = emp.aadharcard;
                    txtmobile.Text = emp.mobile;
                    txtEmail.Text = emp.email_id;
                    ddlDesignation.SelectedValue = emp.designation_code;
                    dddeprtment.SelectedValue = emp.department_code;
                    //txtPincode.Text = emp.pincode;
                    txtDOJ.Text = emp.doj;
                    txtdoj1.Value= emp.doj;
                    txtStartDate.Text = emp.start_date;
                    txtstart1.Value = emp.start_date;
                    if (Session["rtype"].ToString()=="1")
                    {

                        txtempid.ReadOnly = true;
                        txtfName.ReadOnly = true;
                        txtDOB.ReadOnly = true;
                        txtage.ReadOnly = true;
                        txtPresentAddress.ReadOnly = true;
                        txtpermanentaddress.ReadOnly = true;
                        ddlState.Enabled = false;
                        ddlDivision.Enabled = false;
                        ddDistrict.Enabled = false;
                        ddlBank.Enabled = false;
                        txtBranch.ReadOnly = true;
                        txtiefc.ReadOnly = true;
                        txtAccountNo.ReadOnly = true;
                        txtpancard.ReadOnly = true;
                        txtadharcard.ReadOnly = true;
                        txtmobile.ReadOnly = true;
                        txtEmail.ReadOnly = true;
                        ddlDesignation.Enabled = false;
                        dddeprtment.Enabled = false;
                        //txtPincode.ReadOnly = true;
                        txtDOJ.ReadOnly = true;
                        txtStartDate.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        Image1.Visible = false;
                        Image2.Visible = false;
                        Image3.Visible = false;
                    }
                }
            }
        }

        private void GetAllDropDownValues()
        {
            List<Designation_Details> des = new List<Designation_Details>();
            des = BL_Designation_Details.GetDList();
            ddlDesignation.DataSource = des;
            ddlDesignation.DataTextField = "designation_name";
            ddlDesignation.DataValueField = "designation_code";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "Select");
            List<State> statelist = new List<State>();
            statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
            ddlState.DataSource = statelist;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Code";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Select");
            List<Department> Department = new List<Department>();
            Department = BL_User_Mgnt.GetDeptList(Session["UserID"].ToString());
            dddeprtment.DataSource = Department;
            dddeprtment.DataTextField = "Dept_Name";
            dddeprtment.DataValueField = "Dept_Code";
            dddeprtment.DataBind();
            dddeprtment.Items.Insert(0, "Select");
            List<Bank_Master> bank = new List<Bank_Master>();
            bank = BL_Employee_Details.GetBankList();
            ddlBank.DataSource = bank;
            ddlBank.DataTextField = "bank_name";
            ddlBank.DataValueField = "bank_Code";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, "Select");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                Employee_Details emp = new Employee_Details();
                emp.employee_master_id = txtid.Value;
                emp.emp_code = txtempid.Text;
                emp.emp_name = txtfName.Text;
                emp.dob = txtdob1.Value;
                emp.age = txtage.Text;
                emp.present_address = txtPresentAddress.Text;
                emp.permanent_address = txtpermanentaddress.Text;
                emp.state_code = ddlState.SelectedValue;
                emp.division_code = ddlDivision.SelectedValue;
                emp.district_code = ddDistrict.SelectedValue;
                emp.bank = ddlBank.SelectedValue;
                emp.branch = txtBranch.Text;
                emp.ifsc = txtiefc.Text;
                emp.account_no = txtAccountNo.Text;
                emp.pancard = txtpancard.Text;
                emp.aadharcard = txtadharcard.Text;
                emp.mobile = txtmobile.Text;
                emp.email_id = txtEmail.Text;
                emp.designation_code = ddlDesignation.SelectedValue;
                emp.department_code = dddeprtment.SelectedValue;
               // emp.pincode = txtPincode.Text;
                emp.doj = txtdoj1.Value;
                emp.start_date = txtstart1.Value;
                emp.user_id = Session["UserID"].ToString();
                emp.org_id = "1";
                emp.record_status = "Y";
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Employee_Details.Insert(emp);
                else
                    val = BL_Employee_Details.Update(emp);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("EmployeeList");
                }
                else
                {
                    string message =val;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }
        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }
        protected void Employee_Details_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }

        protected void Designation_1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DepartmentMasterList.aspx");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(ddlState.SelectedValue);
            var division = from s in divisions
                           where s.state_Code == ddlState.SelectedValue
                              select s;
            ddlDivision.DataSource = division;
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, "Select");
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            Districts = new List<District>();
            Districts = BL_User_Mgnt.GetDistricts(ddlDivision.SelectedValue);
            var org_master1 = from s in Districts
                              where s.division_Code == ddlDivision.SelectedValue
                              select s;
            ddDistrict.DataSource = org_master1.ToList();
            ddDistrict.DataTextField = "District_Name";
            ddDistrict.DataValueField = "District_Code";
            ddDistrict.DataBind();
            ddDistrict.Items.Insert(0, "Select");
        }
    }
}