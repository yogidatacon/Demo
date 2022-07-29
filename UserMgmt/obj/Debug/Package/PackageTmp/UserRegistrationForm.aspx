<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserRegistrationForm.aspx.cs" Inherits="UserMgmt.UserRegistrationForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div role="main">
        <br />
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">
                            <html xmlns="http://www.w3.org/1999/xhtml">
                            <head>
                                <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                                <title>User Management</title>

                                <%--<script src="../common/theme/js/custom.js"></script>--%>

                                <!-- flot js -->
                                <!--[if lte IE 8]><script type="text/javascript" src="../common/theme/js/excanvas.min.js"></script><![endif]-->
                                <script type="text/javascript" src="/common/theme/js/flot/date.js"></script>

                                <style>
                                    .nowrap {
                                        white-space: nowrap;
                                    }

                                    .field-icon {
                                        /*float: right;*/
                                        margin-left: -25px;
                                        margin-top: 8px;
                                        position: relative;
                                        z-index: 2;
                                    }
                                </style>
                                <style>
                                    .formErrorArrow {
                                        display: none !important;
                                    }

                                    .formErrorContent {
                                        display: none !important;
                                    }
                                </style>




                                <script type="text/javascript">
                                    function ShowHidePassword() {
                                        debugger;
                                        var txt = $('#<%=txtpassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                            $("label[for='cbShowHidePassword1']").text("Hide Password");
                                        }
                                        else {
                                            txt.prop("type", "password");
                                            $("label[for='cbShowHidePassword1']").text("Show Password");
                                        }
                                    }
                                    function ShowHidePassword1() {
                                        debugger;
                                        var txt = $('#<%=txtrepassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                            $("label[for='cbShowHidePassword2']").text("Hide Password");
                                        }
                                        else {
                                            txt.prop("type", "password");
                                            $("label[for='cbShowHidePassword2']").text("Show Password");
                                        }
                                    }
                                </script>

                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;
                                        if (document.getElementById('<%=txtuserName.ClientID%>').value == '') {
                                            alert("Enter  Name");
                                            return false;
                                            document.getElementById("<% =txtuserName.ClientID%>").focus();
                                        }

                                        <%--if (document.getElementById('<%=txtDATE_OF_BIRTH.ClientID%>').value == '') {
                                            alert("Select  DOB");
                                            return false;
                                            document.getElementById("<% =txtDATE_OF_BIRTH.ClientID%>").focus();
                                        }--%>

                                        if (document.getElementById('<%=txtuserid.ClientID%>').value == '') {
                                            alert("Enter  User Name");
                                            return false;
                                            document.getElementById("<% =txtuserid.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtmobile.ClientID%>').value == '') {
                                            alert("Enter  Mobile Nuber");
                                            return false;
                                            document.getElementById("<% =txtmobile.ClientID%>").focus();
                                        }
                                   <%--     if (document.getElementById('<%=txtpassword.ClientID%>').value == '' || document.getElementById('<%=txtpassword.ClientID%>').value == '..........') {--%>
                                            if (document.getElementById('<%=txtpassword.ClientID%>').value == '') {
                                                alert("Enter  Password");
                                                return false;
                                                document.getElementById("<% =txtpassword.ClientID%>").focus();
                                            }
                                        //}
                                        if (document.getElementById('<%=txtrepassword.ClientID%>').value == '') {
                                            alert("Enter  Repassword");
                                            return false;
                                            document.getElementById("<% =txtrepassword.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtemail.ClientID%>').value == '') {
                                            alert("Enter  Email ID");
                                            return false;
                                            document.getElementById("<% =txtemail.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=address.ClientID%>').value == '') {
                                            alert("Enter  Address");
                                            return false;
                                            document.getElementById("<% =address.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddOrgnames.ClientID%>').value == 'Select') {
                                            alert("Select  Organization");
                                            return false;
                                            document.getElementById("<% =ddOrgnames.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlDepartment.ClientID%>').value == 'Select') {
                                            alert("Select  Department");
                                            return false;
                                            document.getElementById("<% =ddlDepartment.ClientID%>").focus();
                                        } 
                                          if (document.getElementById('<%=ddlDesignation.ClientID%>').value == 'Select') {
                                              alert("Select  Designation");
                                            return false;
                                            document.getElementById("<% =ddlDesignation.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlparty.ClientID%>').value == 'Select') {
                                            alert("Select  Party name");
                                            return false;
                                            document.getElementById("<% =ddlparty.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddRole.ClientID%>').value == 'Select') {
                                            alert("Select  Role");
                                            return false;
                                            document.getElementById("<% =ddRole.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddAccessType.ClientID%>').value == 'Select') {
                                            alert("Select  Access Type");
                                            return false;
                                            document.getElementById("<% =ddAccessType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddRoleLevel.ClientID%>').value == 'Select') {
                                            alert("Select  Role Level");
                                            return false;
                                            document.getElementById("<% =ddRoleLevel.ClientID%>").focus();
                                        }

                                        if (document.getElementById('<%=ddStates.ClientID%>').value == 'Select') {
                                            alert("Select State");
                                            return false;
                                            document.getElementById("<% =ddStates.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddDivisions.ClientID%>').value == 'Select') {
                                            alert("Select Division");
                                            return false;
                                            document.getElementById("<% =ddDivisions.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddDistricts.ClientID%>').value == 'Select') {
                                            alert("Select District");
                                            return false;
                                            document.getElementById("<% =ddDistricts.ClientID%>").focus();
                                        }

                                    }
                                </script>
                                <script type="text/javascript">

                                    function validateExtraDocuments() {

                                        var fileInput = document.getElementById('<%= idproofimage.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg only.');
                                            fileInput.value = '';
                                            return false;
                                        }

                                        var uploadControl = document.getElementById('<%= idproofimage.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idproofimage.ClientID %>').value = "";

                                            return false;
                                        }
                                        else {

                                            return true;
                                        }
                                    }
                                </script>
                                <script>
                                    function onlyDotsAndNumbers(txt, event) {
                                        debugger;
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
                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_txtmobile').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid phone number.");
                                            $('#' + BodyContent_txtmobile).val("");
                                            $('#' + BodyContent_txtmobile).focus();
                                        }
                                    }
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtemail').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtemail').val("");
                                            $('#BodyContent_txtemail').focus();
                                            return false;
                                        }

                                    }
                                    function CheckName() {
                                        debugger;
                                        var name = $('#BodyContent_txtuserName').val().length;

                                        if (name < 4) {
                                            alert("Incorrect! Name should be morethan 3 characters.");
                                            $('#BodyContent_txtuserName').val("");
                                            $('#BodyContent_txtuserName').focus();
                                        }
                                    }
                                    function chkDuplicateEmail() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtemail').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtemail').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "UserRegistrationForm.aspx/chkDuplicateEmailData",
                                            data: '{email_id:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Email id is already exists");
                                                    $('#BodyContent_txtemail').val("");
                                                    $('#BodyContent_txtemail').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateUserID() {
                                        debugger;
                                        if ($('#BodyContent_txtuserid').val().length < 5) {
                                            alert("User Name should have minimum 5 characters");
                                            $('#BodyContent_txtuserid').val("");
                                            $('#BodyContent_txtuserid').focus();
                                            return false;
                                        }
                                        else {
                                            var email = $('#BodyContent_txtuserid').val();
                                            var jsondata = JSON.stringify($('#BodyContent_txtuserid').val());
                                            $.ajax({
                                                type: "POST",
                                                //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                                url: "UserRegistrationForm.aspx/chkDuplicateUserIDData",
                                                data: '{User_id:' + jsondata + '}',
                                                datatype: "application/json",
                                                contentType: "application/json; charset=utf-8",
                                                cache: false,
                                                async: false,
                                                success: function (msg) {

                                                    if (parseInt(msg.d) > 0) {
                                                        alert("User Name is already exists");
                                                        $('#BodyContent_txtuserid').val("");
                                                        $('#BodyContent_txtuserid').focus();
                                                    }

                                                }
                                            });
                                        }
                                    }
                                    function imageUpload(image, imageLbl) {
                                        debugger;
                                        var imgText = $('#' + image).val();
                                        //var filename = imgText.replace(/^.*[\\\/]/, '');
                                        //var imgTextArr = filename.split(".");
                                        //var imgTxtFTb = imgTextArr[0];
                                        //if (parseInt(imgTxtFTb.length) > 45) {
                                        //    imgTxtFTb = imgTxtFTb.substring(0, 45);
                                        //}
                                        $('#' + imageLbl).val(imgText);
                                    }

                                    function browseImage(image) {

                                        $('#' + image).click();
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">

                                    function CheckPassword(inputtxt) {
                                        debugger;
                                        var passw = $('#' + inputtxt).val();
                                        var format = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,15}$/;
                                        if (passw.match(format)) {
                                            //alert('Correct, try another...')
                                            return true;
                                        }
                                        else {
                                            alert('Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!')
                                            $('#' + inputtxt).val("");
                                            $('#' + inputtxt).focus();
                                            return false;
                                        }
                                    }
                                    function compare() {

                                        var string1 = document.getElementById('<%=txtpassword.ClientID%>').value;
                                        var string2 = document.getElementById('<%=txtrepassword.ClientID%>').value;

                                        if (string1 == string2) {
                                            $('#BodyContent_txtpass').val(string1);
                                        }
                                        else {
                                            alert("Password not Match.");
                                            $('#BodyContent_txtrepassword').val("");
                                            $('#BodyContent_txtpassword').val("");
                                            $('#BodyContent_txtpassword').focus();
                                            return false;

                                        }
                                    }


                                </script>

                            </head>
                            <body>

                                <div>

                                    <ul class="nav nav-tabs">

                                        <li>
                                            <asp:LinkButton ID="Designation_1" OnClick="Designation_1_Click" runat="server"><span style="color:#fff;font-size:14px;">Department Master</span></asp:LinkButton></li>

                                        <li class="active">
                                            <asp:LinkButton ID="UserRegistration" OnClick="UserRegistration_Click" runat="server"><span style="color:#fff;font-size:14px;">User Registration</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="Employee_Details" OnClick="Employee_Details_Click" runat="server"><span style="color:#fff;font-size:14px;">Employee Details</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>User Registration Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">

                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Emp Code</label><br />
                                                <asp:DropDownList ID="ddlEmp_code" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Emp Code" OnSelectedIndexChanged="ddlEmp_code_SelectedIndexChanged" class="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Name</label><br />
                                                <asp:TextBox ID="txtuserName" onchange="CheckName();" runat="server" data-toggle="tooltip" AutoComplete="off" data-placement="right" title="Name" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                            </div>

                                            <%--<div id="dob" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>DOB</label><br />

                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE_OF_BIRTH" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                                <script type="text/javascript">

                                                    function SelectDate(e) {
                                                        
                                                        var PresentDay = new Date();
                                                        var dateOfBirth = e.get_selectedDate();
                                                        var months = (PresentDay.getMonth() - dateOfBirth.getMonth() + (12 * (PresentDay.getFullYear() - dateOfBirth.getFullYear())));
                                                        var age11 = Math.round(months / 12);
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
                                                        $('#BodyContent_txtDATE_OF_BIRTH').val(todayDate);
                                                        var dat1e = $('#BodyContent_txtDATE_OF_BIRTH').val();
                                                        $('#BodyContent_txtdob').val(dat1e);


                                                        if (Math.round(months / 12) < 21) {
                                                            alert("Age should be greater than 21 Years");
                                                            document.getElementById("<% =txtDATE_OF_BIRTH.ClientID%>").value = "";
                                                            document.getElementById("<% =txtDATE_OF_BIRTH.ClientID%>").focus();
                                                        }
                                                    }

                                                </script>
                                                <asp:TextBox ID="txtDATE_OF_BIRTH" Height="30px" width="60%" ReadOnly="true" data-toggle="tooltip" data-placement="right" title="DOB" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" runat="server" />


                                            </div>--%>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Username</label><br />
                                                <asp:TextBox Height="30px" AutoComplete="off" Width="60%" ID="txtuserid" class="form-control" onchange="chkDuplicateUserID();" data-toggle="tooltip" data-placement="right" title="Username" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Mobile Number</label><br />

                                                <asp:TextBox ID="txtmobile" AutoComplete="off" Height="30px" Width="60%" runat="server" name="mobile" MaxLength="10" data-toggle="tooltip" data-placement="right" title="Mobile Number" class="form-control validate[custom[phone],required]" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                            </div>
                                            <p>&nbsp;</p>

                                            <div class="clearfix"></div>

                                            <div id="pass" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <br />
                                                <label class="control-label"><span style="color: red">*</span>Password</label><br />
                                                <%--  <input type="password" id="password" runat="server" name="password" autocomplete="off" class="form-control validate[required]" maxlength="20" onchange="return chkPassword();" data-toggle="tooltip" data-placement="bottom" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!">--%>

                                                <asp:TextBox ID="txtpassword" Height="30px" onchange="CheckPassword(this.id)" Width="60%" TextMode="Password" class="form-control validate[required]" runat="server" data-toggle="tooltip" data-placement="right" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!"></asp:TextBox>
                                                <%-- <asp:ImageButton ID="Image1" Cssclass="control-label" runat="server" OnClientClick="ShowHidePassword();" Height="10%" Width="10%" ImageUrl="img/eye.jpg" />--%>
                                                <%-- <i class="far fa-eye" id="togglePassword"></i>--%>
                                                <input id="cbShowHidePassword1" name="Show" type="checkbox" onclick="ShowHidePassword();" /><label cssclass="control-label"><span style="color: red"></span>Show</label>
                                            </div>


                                            <div id="repass" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <br />
                                                <label style="font-size: small"><span style="color: red">*</span> Conform-Password</label><br />
                                                <%-- <input type="password" id="repassword" runat="server" autocomplete="off" name="repassword" class="form-control validate[required]" maxlength="20" onchange="return compare()" data-toggle="tooltip" data-placement="bottom" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!">--%>
                                                <asp:TextBox ID="txtrepassword" Height="30px" Width="60%" CssClass="form-control" TextMode="Password" MaxLength="20" data-toggle="tooltip" data-placement="right" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!" onchange=" return compare();" runat="server"></asp:TextBox>
                                                <input id="cbShowHidePassword2" type="checkbox" onclick="ShowHidePassword1();" /><label class="control-label"><span style="color: red"></span>Show</label>
                                                <asp:HiddenField ID="txtpass" runat="server" />
                                            </div>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <br />
                                                <label class="control-label"><span style="color: red">*</span>Email ID</label><br />
                                                <asp:TextBox ID="txtemail" AutoComplete="off" Height="30px" Width="60%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Email ID" runat="server" onchange="emailValidate(this);chkDuplicateEmail();"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small"><span style="color: red">*</span>Address</label><br />

                                                <asp:TextBox ID="address" TextMode="MultiLine" Height="70px" Width="60%" Style="resize: none;" data-toggle="tooltip" runat="server" data-placement="right" title="Address" CssClass="form-control" Rows="3" name="address" autocomplete="off" MaxLength="300"></asp:TextBox>
                                            </div>
                                            <p>&nbsp;</p>

                                            <div class="clearfix"></div>
                                            <asp:UpdatePanel ID="up123" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Organization</label><br />
                                                        <asp:DropDownList ID="ddOrgnames" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Role Name" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Department </label>
                                                        <br />

                                                        <asp:DropDownList ID="ddlDepartment" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Department" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation</label><br />
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Party Name </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlparty" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Party Name" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <div class="clearfix"></div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Role</label><br />

                                                        <asp:DropDownList ID="ddRole" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Role Name" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Access Type</label><br />

                                                        <asp:DropDownList ID="ddAccessType" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Access Type" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>




                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Role Level</label><br />
                                                        <asp:DropDownList ID="ddRoleLevel" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Role Level" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <div class="clearfix"></div>

                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>State</label><br />
                                                        <asp:DropDownList ID="ddStates" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="State " OnSelectedIndexChanged="ddStates_SelectedIndexChanged" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="divexid" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span> Division</label><br />


                                                        <asp:DropDownList ID="ddDivisions" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Division " OnSelectedIndexChanged="ddDivisions_SelectedIndexChanged" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                            <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div id="distexid" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span> District</label><br />

                                                        <asp:DropDownList ID="ddDistricts" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="District " CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                            <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <p>&nbsp;</p>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>


                                            <%-- <asp:UpdatePanel ID="up1" runat="server">
                                                <Triggers>

                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div id="photo" runat="server" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label">Upload Passport Size Photo</label>
                                                        <asp:FileUpload ID="UploadPhoto" runat="server" CssClass="form-control" />
                                                        <input type="hidden" runat="server" id="txtUploadHiden" name="state" style="text-transform:capitalize">
                                                        <asp:LinkButton ID="btnDownload" CssClass="myButton" OnClick="btnDownload_Click" runat="server">Download</asp:LinkButton>

                                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="False" OnClick="btnUpload_Click" OnClientClick="javascript:return validateExtraDocuments()" CssClass="btn btn-primary" />
                                                        <p style="font-size: 9px; font-weight: 600;">(.jpg, .jpeg  upto 2 MB max)</p>

                                                    </div>
                                                </ContentTemplate>

                                            </asp:UpdatePanel>--%>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <%-- <label class="control-label"><span style="margin-top: 5px;">Attachment<span style="color: red">*</span></span>--%>
                                                <label class="control-label"><span style="color: red">*</span>User Photo</label><br />


                                                <asp:FileUpload runat="server" Width="250px" Style="display: none;" ID="idproofimage" class="file-name" name="idproofimage" value="" onchange="validateExtraDocuments();
                                                    imageUpload('BodyContent_idproofimage', 'idproofimageLbl');" />
                                                <input class="form-control" width="250px" readonly style="margin-top: 5px;" onkeypress="return attachMand('BodyContent_idproofimage',this.id)" data-toggle="tooltip" data-placement="right" title="Attachment" type="text" id="idproofimageLbl" name="idproofimageLbl" maxlength="250" placeholder="Attachment Name">
                                                <p id="pattachment" style="font-size: 9px; font-weight: 600;">(.jpg, .jpeg  upto 2 MB max)</p>
                                                <input type="button" id="btndownloadattachment" style="width: 250px; display: none; margin-top: -5px;" class="btn btn-primary" value="Download file" onclick="downloadattachment();" />
                                                <input type="button" id="btnppup" style="width: 85px; margin-bottom: -1px;" value="Browse.." class="btn btn-primary" onclick="browseImage('BodyContent_idproofimage');" />
                                                <span style="display: none">
                                                    <asp:Button runat="server" ID="btnUpload" />
                                                </span>
                                                <span style="display: none">
                                                    <asp:Button runat="server" ID="btnDownloadMf1Attachment" />
                                                </span>
                                                <asp:Button runat="server" ID="btnDownload" CssClass="myButton" Text="Download" OnClick="btnDownloadmf1_Click" />
                                            </div>


                                            <div style="clear: both">
                                                <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                            </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="orgid" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>

                                        </div>
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

