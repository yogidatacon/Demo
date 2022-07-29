<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OrgFroms.aspx.cs" Inherits="UserMgmt.OrgFroms" %>

<asp:Content ID="Home" ContentPlaceHolderID="BodyContent" runat="server">
    <div role="main">
        <br />
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">
                            <html>
                            <head>

                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtOrgname.ClientID%>').value == '') {
                                            alert("Enter  Organization Name");
                                              document.getElementById("<% =txtOrgname.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtorgtyp.ClientID%>').value == '') {
                                            alert("Enter  Organization Type");
                                              document.getElementById("<% =txtorgtyp.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                         if (document.getElementById('<%=txtContactNumber.ClientID%>').value == '') {
                                             alert("Enter  Mobile No");
                                              document.getElementById("<% =txtContactNumber.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         if (document.getElementById('<%=txtEmail.ClientID%>').value == '') {
                                             alert("Enter  Email");
                                              document.getElementById("<% =txtEmail.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtGST.ClientID%>').value == '') {
                                             alert("Enter  GST");
                                              document.getElementById("<% =txtGST.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         if (document.getElementById('<%=textTin.ClientID%>').value == '') {
                                             alert("Enter  TIN");
                                              document.getElementById("<% =textTin.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=textTan.ClientID%>').value == '') {
                                            alert("Enter  TAN");
                                             document.getElementById("<% =textTan.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=textPan.ClientID%>').value == '') {
                                            alert("Enter  PAN");
                                             document.getElementById("<% =textPan.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         if (document.getElementById('<%=txtaddress.ClientID%>').value == '') {
                                             alert("Enter  Address");
                                              document.getElementById("<% =txtaddress.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=txtDescr.ClientID%>').value == '') {
                                            alert("Enter  Description");
                                             document.getElementById("<% =txtDescr.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                    }
                                    

                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtemail').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtemail').val('');
                                            return false;
                                        }

                                    }
                                    function chkDuplicateEmail() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtEmail').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtEmail').val());
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
                                                    $('#BodyContent_txtEmail').val('');
                                                    $('#BodyContent_txtEmail').focus();
                                                }

                                            }
                                        });
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
                                        var mobileN = $('#BodyContent_txtContactNumber').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid phone number.");
                                            $('#BodyContent_txtContactNumber').val("");
                                            $('#BodyContent_txtContactNumber').focus();
                                        }
                                    }
                                    function pancardValidate() {
                                        debugger;
                                        var txtPANCard = $('#BodyContent_textPan');
                                        var regex = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_textPan').val().toUpperCase())) {
                                          
                                            return true;
                                        } else {
                                            alert("Invalid PAN number.");
                                            $('#BodyContent_textPan').val("");
                                            $('#BodyContent_textPan').focus();
                                            return false;
                                        }
                                    }
                                    function ValidateTAN() {
                                        debugger;
                                        var txtTAN = $('#BodyContent_textTan');
                                        var regex = /([A-Z]){4}([0-9]){5}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_textTan').val().toUpperCase()))
                                        {

                                            return true;
                                        } else {
                                            alert("Invalid TAN number.");
                                            $('#BodyContent_textTan').val("");
                                            $('#BodyContent_textTan').focus();
                                            return false;
                                        }
                                    }
                                    function validateGST() {
                                        debugger;
                                        var txtTIN = $('#BodyContent_txtGST');
                                        var regex = /([0-9]){2}([A-Z]){5}([0-9]){4}([A-Z]){1}([0-9]){1}([Z]){1}([0-9]){1}$/;
                                        if (regex.test($('#BodyContent_txtGST').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid GST number.");
                                           $('#BodyContent_txtGST').val("");
                                            $('#BodyContent_txtGST').focus();
                                            return false;
                                        }
                                    }
                                        function validateTIN() {
                                            debugger;
                                            var txtTIN = $('#BodyContent_textTin');
                                            var regex = /^\d{11}$/;
                                            if (regex.test($('#BodyContent_textTin').val().toUpperCase())) {

                                                return true;
                                            } else {
                                                alert("Invalid TIN number.");
                                                $('#BodyContent_textTin').val("");
                                                $('#BodyContent_textTin').focus();
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
                                    function chkDuplicateEmail() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtEmail').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtEmail').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "OrgFroms.aspx/chkDuplicateEmailData",
                                            data: '{email_id:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Email id is already exists");
                                                    $('#BodyContent_txtEmail').val("");
                                                    $('#BodyContent_txtEmail').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateOrgnization() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtOrgname').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtOrgname').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "OrgFroms.aspx/chkDuplicaOrgname",
                                            data: '{orgname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Organisation Name is already exists");
                                                    $('#BodyContent_txtOrgname').val("");
                                                    $('#BodyContent_txtOrgname').focus();
                                                }

                                            }
                                        });
                                    }
                                    </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="OrganisationDetails" OnClick="OrganisationDetails_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Details</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="OrganisationFinancialYear" OnClick="OrganisationFinancialYear_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Financial Year</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="Module_Master1" OnClick="Module_Master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Module Master</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton ID="Submodule_master1" OnClick="Submodule_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">SubModule Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="tab_master1" OnClick="tab_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Tab Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Organisation Master</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <br />

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Organisation Name</label><br />
                                                <asp:TextBox ID="txtOrgname" AutoComplete="off" Style="text-transform:uppercase" height="30px" width="60%" runat="server" class="form-control" data-toggle="tooltip" data-placement="right" title="Organisation Name" MaxLength="100" onchange="chkDuplicateOrgnization();"></asp:TextBox>
                                                <%--<input type="text"  id="txtOrg" runat="server" height="30px" width="350px" class="form-control" data-toggle="tooltip" data-placement="right" title="Organisation Name" maxlength="100">--%>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Organisation Type</label>
                                                <br />
                                                 <asp:TextBox ID="txtorgtyp" AutoComplete="off" height="30px" width="60%" runat="server" Cssclass="form-control"  data-toggle="tooltip" data-placement="right" title="Organisation Type" MaxLength="10"></asp:TextBox>
                                           
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label style="font-size: small;"><span style="color: red">*</span>Mobile No</label><br />
                                                <asp:TextBox ID="txtContactNumber" AutoComplete="off" height="30px" width="60%" runat="server" class="form-control" data-toggle="tooltip" data-placement="right" title="Contact Number" MaxLength="10" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label style="font-size: small;"><span style="color: red">*</span>Email</label><br />
                                                <asp:TextBox ID="txtEmail" runat="server"  AutoComplete="off" height="30px" width="60%" class="form-control" data-toggle="tooltip" data-placement="right" onchange="emailValidate(this);chkDuplicateEmail();"  title="Email"></asp:TextBox>
                                                </div>
                                                <div class="clearfix"></div>
                                              <p>&nbsp;</p>
                                         <%--   onchange="validateGST();"--%>
                                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>GST</label><br />
                                                     <asp:TextBox ID="txtGST" runat="server" style="text-transform:uppercase" AutoComplete="off" height="30px" width="60%" class="form-control" data-toggle="tooltip" MaxLength="15" data-placement="right"   title="GST"></asp:TextBox>
                                                    
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>TIN</label><br />
                                                     <asp:TextBox ID="textTin" runat="server" style="text-transform:uppercase" AutoComplete="off" height="30px" width="60%" class="form-control" data-toggle="tooltip" MaxLength="11" data-placement="right" onchange="validateTIN();" onkeypress="return onlyDotsAndNumbers(this,event);" title="TIN"></asp:TextBox>
                                                    
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>TAN</label>  <br />
                                                   <asp:TextBox ID="textTan" runat="server" style="text-transform:uppercase" AutoComplete="off" height="30px" width="60%" class="form-control" data-toggle="tooltip" MaxLength="10" data-placement="right" onchange="ValidateTAN();" title="TAN"></asp:TextBox>
                                                  
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>PAN</label> <br />
                                                   
                                                     <asp:TextBox ID="textPan" style="text-transform:uppercase" AutoComplete="off" runat="server" height="30px" width="60%" class="form-control" data-toggle="tooltip" data-placement="right" title="PAN" onchange="pancardValidate();"></asp:TextBox>
                                                
                                                </div>
                                                <div class="clearfix"></div>
                                              <p>&nbsp;</p>

                                            <div class="col-md-11 col-sm-12 col-xs-12">
                                                    <label class="control-label" style="font-size:small"><span style="color: red">*</span>Address</label><br />
                                                    
                                                    <textarea id="txtaddress" runat="server"  class="form-control" AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Address" style="resize: none"></textarea>
                                                </div>

                                               
                                             <div class="clearfix"></div>
                                                <p>&nbsp;</p>
                                                
                                             <div class="col-md-11 col-sm-12 col-xs-12 ">
                                                    <label class="control-label" style="font-size:small"><span style="color: red">*</span>Description</label><br />
                                                    
                                                    <textarea data-toggle="tooltip" id="txtDescr"  runat="server" AutoComplete="off" data-placement="right" maxlength="100" title="Description" class="form-control" style="resize: none"></textarea>
                                                </div>

                                            </div>


                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="orgid" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                    <%-- <button type="submit" id="btnSave" runat="server" onclick="Save" class="btn btn-primary" >Submit</button>                                                   
                                                <a href="../login/orgFinancialyrlist.htm" class="btn btn-danger">Cancel</a>   --%>
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
