<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="UserMgmt.EmployeeForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div role="main">
        <br />
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">
                            <html>
                            <head>
                                <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                                <title>Employee Form</title>
                                <script>
                                    function onlyDotsAndNumbers(txt, event) {

                                        var charCode = (event.which) ? event.which : event.keyCode
                                        if (charCode == 46) {
                                            if (txt.value.indexOf(".") < 0)
                                                return true;
                                            else
                                                return false;
                                        }

                                        if (txt.value.indexOf(".") > 0) {
                                            var txtlen = txt.value.length;
                                            var dotpos = txt.value.indexOf(".");
                                            //Change the number here to allow more decimal points than 2
                                            if ((txtlen - dotpos) > 2)
                                                return false;
                                        }

                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;

                                        return true;
                                    }
                                    function validationMsg() {


                                       <%-- if (document.getElementById('<%=txtDOB.ClientID%>').value == '') {
                                            alert("Enter DOB");
                                            document.getElementById("<% =txtDOB.ClientID%>").focus();
                                            return false;--%>


                                        //}
                                    }
                                    function SelectDate(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!
                                        var PresentDay = new Date();
                                        var yy = PresentDay.getFullYear();
                                        var m = PresentDay.getMonth() + 1;
                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        var months = (m - mm + (12 * (yy - yyyy)));
                                        var age = Math.round(months / 12);
                                        $('#BodyContent_txtDOB').val(todayDate);

                                        $('#BodyContent_txtdob1').val(todayDate);
                                        $('#BodyContent_txtage').val(age);
                                    }
                                    function SelectDateDOJ(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtDOJ').val(todayDate);

                                        $('#BodyContent_txtdoj1').val(todayDate);
                                    }
                                    function SelectDateStartDate(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtStartDate').val(todayDate);

                                        $('#BodyContent_txtstart1').val(todayDate);
                                    }
                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_txtMobile').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid phone number.");
                                            $('#BodyContent_txtMobile').val("");
                                            $('#BodyContent_txtMobile').focus();
                                        }
                                    }
                                    function pancardValidate() {
                                        debugger;
                                        var txtPANCard = $('#BodyContent_txtpancard');
                                        var regex = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_txtpancard').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid PAN number.");
                                            $('#BodyContent_txtpancard').val("");
                                            $('#BodyContent_txtpancard').focus();
                                            return false;
                                        }
                                    }
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtEmail').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtEmail').val('');
                                            $('#BodyContent_txtEmail').focus();
                                            return false;
                                        }

                                    }
                                    function IFSCValidate() {
                                        debugger;
                                        var txtPANCard = $('#BodyContent_txtiefc');
                                        var regex = /^[A-Z]{4}0[A-Z0-9]{6}$/;
                                        if (regex.test($('#BodyContent_txtiefc').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid IFSC Code.");
                                            $('#BodyContent_txtiefc').val("");
                                            $('#BodyContent_txtiefc').focus();
                                            return false;
                                        }
                                    }
                                </script>
                                 <script>
                                     $(document).ready(function () {
                                         if ($('#BodyContent_txtDOB').val() == "")
                                             $('#BodyContent_txtDOB').val($('#BodyContent_txtdob1').val());
                                         if ($('#BodyContent_txtDOJ').val() == "")
                                             $('#BodyContent_txtDOJ').val($('#BodyContent_txtdoj1').val());
                                         if ($('#BodyContent_txtStartDate').val() == "")
                                             $('#BodyContent_txtStartDate').val($('#BodyContent_txtstart1').val());
                                     });
                                      </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">

                                    <li>
                                        <asp:LinkButton ID="Designation_1" OnClick="Designation_1_Click" runat="server"><span style="color:#fff;font-size:14px;">Department Master</span></asp:LinkButton></li>
                                 <%--   <li>
                                        <asp:LinkButton ID="Designation_2" OnClick="Designation_2_Click" runat="server"><span style="color:#fff;font-size:14px;">Designations</span></asp:LinkButton></li>--%>
                                    <li>
                                        <asp:LinkButton ID="UserRegistration" OnClick="UserRegistration_Click" runat="server"><span style="color:#fff;font-size:14px;">User Registration</span></asp:LinkButton></li>
                                      <li class="active">
                                        <asp:LinkButton ID="Employee_Details" OnClick="Employee_Details_Click" runat="server"><span style="color:#fff;font-size:14px;">Employee Details</span></asp:LinkButton></li>
                                </ul>
                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Employee Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Employee Code</label><br />
                                        <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Employee Code"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Employee Name</label><br />
                                        <asp:TextBox ID="txtfName" runat="server" Width="80%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Employee Name"></asp:TextBox>
                                    </div>
                                   
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>DOB</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDOB" OnClientDateSelectionChanged="SelectDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDOB" data-toggle="tooltip" Enabled="false" ReadOnly="true" data-placement="right" title="DOB" CssClass="form-control validate[required]" AutoComplete="off" runat="server">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob1" runat="server" />
                                            </div>
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Age</label><br />
                                        <asp:TextBox ID="txtage" ReadOnly="true" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Age"></asp:TextBox>
                                    </div>
                                    <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                    <div class="col-md-5 col-sm-2 col-xs-2 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Present Address</label><br />
                                        <asp:TextBox ID="txtPresentAddress" TextMode="MultiLine" Width="85%" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5 col-sm-2 col-xs-2 form-inline" style="padding-left: 8.4%">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Permanent Address</label><br />
                                        <asp:TextBox ID="txtpermanentaddress" TextMode="MultiLine" Width="107%" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)"></asp:TextBox>
                                    </div>
                                    <p>&nbsp;</p>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>State</label><br />
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" title="Molasses Type"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Division</label><br />
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" title="Molasses Type"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>District</label><br />
                                        <asp:DropDownList ID="ddDistrict" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Molasses Type"></asp:DropDownList>
                                    </div>

                                    <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Pincode</label><br />
                                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Pincode"></asp:TextBox>
                                    </div>--%>
                                   </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="bank_div" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Name Of Bank</label><br />
                                            <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Name Of Bank"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Branch</label><br />
                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Branch"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>IFSC Code</label><br />
                                            <asp:TextBox ID="txtiefc" runat="server" Style="text-transform:uppercase" CssClass="form-control" data-toggle="tooltip" data-placement="right" MaxLength="11" onchange="IFSCValidate()" title="IFC Code"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Account Number</label><br />
                                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" MaxLength="18" title="Account Number" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Pancard No</label><br />
                                        <asp:TextBox ID="txtpancard" runat="server" Style="text-transform:uppercase" CssClass="form-control" data-toggle="tooltip" data-placement="right" MaxLength="10" onchange="pancardValidate();" title=" Pancard No"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Aadharcard No</label><br />
                                        <asp:TextBox ID="txtadharcard" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" MaxLength="12" title="Aadharcard No" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Mobile No</label><br />
                                        <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mobile No" MaxLength="10" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Email ID</label><br />
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" onchange="emailValidate()" title="Email ID"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Date of Join</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtDOJ" OnClientDateSelectionChanged="SelectDateDOJ" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDOJ" data-toggle="tooltip" Enabled="false" ReadOnly="true" data-placement="right" title="Date of Join" CssClass="form-control validate[required]" AutoComplete="off" runat="server">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdoj1" runat="server" />

                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Start Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtStartDate" OnClientDateSelectionChanged="SelectDateStartDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtStartDate" data-toggle="tooltip" ReadOnly="true" Enabled="false" data-placement="right" title="Start Date" CssClass="form-control validate[required]" AutoComplete="off" runat="server">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                          <asp:HiddenField ID="txtstart1" runat="server" />

                                    </div>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Department</label><br />
                                        <asp:DropDownList ID="dddeprtment" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Department"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation</label><br />
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                    </div>
                              


                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
