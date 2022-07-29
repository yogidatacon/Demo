<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PartyMasterForm.aspx.cs" Inherits="UserMgmt.PartyMasterForm" %>
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
                                <title>User Management</title>
                               
                                <script type="text/javascript">
                                    function validationMsg() {
                                        debugger;
                                         if (document.getElementById('<%=ddlPartyType.ClientID%>').value == 'Select') {
                                             alert("select Party Type");
                                             document.getElementById("<% =ddlPartyType.ClientID%>").focus();
                                            return false;
                                            
                                        }
                                        if (document.getElementById('<%=txtpartyname.ClientID%>').value == '') {
                                            alert("Enter Party  Name");
                                             document.getElementById("<% =txtpartyname.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                          if (document.getElementById('<%=txtPartyCode.ClientID%>').value == '') {
                                              alert("Enter Party  Code");
                                               document.getElementById("<% =txtPartyCode.ClientID%>").focus();
                                            return false;
                                           
                                          }
                                        if (document.getElementById('<%=txtLicenseNo.ClientID%>').value == '') {
                                            alert("Enter License No");
                                              document.getElementById("<% =txtLicenseNo.ClientID%>").focus();
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
                                        if (document.getElementById('<%=txtPartyAddress.ClientID%>').value == '') {
                                            alert("Enter Party Address");
                                              document.getElementById("<% =txtPartyAddress.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                         if (document.getElementById('<%=ddlDistrict.ClientID%>').value == 'Select') {
                                             alert("select District");
                                              document.getElementById("<% =ddlDistrict.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                        if (document.getElementById('<%=txtMobile.ClientID%>').value == '') {
                                            alert("Enter mobile No");
                                              document.getElementById("<% =txtMobile.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtEmail.ClientID%>').value == '') {
                                             alert("Enter  Email");
                                              document.getElementById("<% =txtEmail.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=ddlCaptive.ClientID%>').value == 'Select') {
                                             alert("select Captive");
                                              document.getElementById("<% =ddlCaptive.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                      
                                        if (document.getElementById('<%=ddlCaptive.ClientID%>').value == 'Yes' && document.getElementById('<%=ddlCaptiveunitname.ClientID%>').value == 'Select') {
                                           
                                            
                                                alert("select Captive unit name");
                                                document.getElementById("<% =ddlCaptiveunitname.ClientID%>").focus();
                                                return false;
                                           
                                        }
                                         
                                    }
                                </script>
                                <script>
                                    function chkDuplicatePartyName() {
                                        debugger;
                                        var email = $('#BodyContent_txtpartyename').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtpartyname').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyMasterForm.aspx/chkDuplicatepartyname",
                                            data: '{partyname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Party  Name is already exists");
                                                    $('#BodyContent_txtpartyname').val('');
                                                    $('#BodyContent_txtpartyname').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicatePartyCode() {
                                        debugger;
                                        var email = $('#BodyContent_txtPartyCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtPartyCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyMasterForm.aspx/chkDuplicatepartycode",
                                            data: '{partycode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Party  Code is already exists");
                                                    $('#BodyContent_txtPartyCode').val('');
                                                    $('#BodyContent_txtPartyCode').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateLicenseNo() {

                                        var email = $('#BodyContent_txtLicenseNo').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtLicenseNo').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyMasterForm.aspx/chkDuplicateLicenseNo",
                                            data: '{licenseno:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("LicenseNo is already exists");
                                                    $('#BodyContent_txtLicenseNo').val('');
                                                    $('#BodyContent_txtLicenseNo').focus();
                                                }

                                            }
                                        });
                                    }
                                   
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
                                        var mobileN = $('#BodyContent_txtMobile').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid phone number.");
                                            $('#BodyContent_txtMobile').val("");
                                            $('#BodyContent_txtMobile').focus();
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
                                    function ValidateTAN() {
                                        debugger;
                                        var txtTAN = $('#BodyContent_textTan');
                                        var regex = /([A-Z]){4}([0-9]){5}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_textTan').val().toUpperCase())) {

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
                                   
                                    function chkDuplicateEmail() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtEmail').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtEmail').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyMasterForm.aspx/chkDuplicateEmailData",
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
                                    function GetFinancialYear()
                                    {
                                        debugger;
                                        var User_id = $('#BodyContent_ddlPartyType').val();
                                        var jsondata = JSON.stringify($('#BodyContent_ddlPartyType').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyMasterForm.aspx/GetFinancialYear",
                                            data: '{partytype:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                
                                                $('#BodyContent_txtFinanceyear').val(msg.d);
                                                }

                                            
                                        });
                                    }
                                </script>
                            </head>
                            <body>
                                
                                    <ul class="nav nav-tabs">
                                <li >
                                         <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton ID="partyfinancialyears" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Financial Year</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                                                                                                                                                            <li >
                                            <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                         <li >  <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                         <li >  <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                       <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                         <li >
                                            <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                         <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                       </ul> 

                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Party Master Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        
                                        <div class="x_content">
                                            
                                        
                                             <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                              <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party  Code</label><br />
                                              
                                                 <asp:TextBox ID="txtPartyCode" onchange="chkDuplicatePartyCode();"  Style="text-transform:uppercase;" AutoComplete="off" class="form-control" Height="30px" Width="250px" MaxLength="3" data-toggle="tooltip" data-placement="right" title="Party  Code"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline"> 
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party  Name</label> <br />
                                                <asp:TextBox ID="txtpartyname" onchange="chkDuplicatePartyName();"  Style="text-transform:capitalize;" AutoComplete="off" class="form-control" Height="30px" Width="250px" MaxLength="50" data-toggle="tooltip" data-placement="right" title="Party  Name"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                          
                                          <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Party Type Name</label> <br />
                                                            <asp:DropDownList ID="ddlPartyType" onchange="GetFinancialYear()" AutoPostBack="true" class="form-control"  OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Party Type Name"  runat="server">
                                                               
                                                            </asp:DropDownList>
                                                        </div>
                                           
                                             
                                             <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                              <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party License No</label><br />
                                              
                                                 <asp:TextBox ID="txtLicenseNo" onchange="chkDuplicateLicenseNo();" Style="text-transform:capitalize;" class="form-control" AutoComplete="off" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Party License No"  runat="server"></asp:TextBox>
                                            </div>
                                              <div class="clearfix"></div>
                                              <p>&nbsp;</p>
                                              <%-- onchange="validateGST();"--%>
                                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>GST</label><br />
                                                     <asp:TextBox ID="txtGST" runat="server" AutoComplete="off" height="30px" width="60%" Style="text-transform:uppercase;" class="form-control" data-toggle="tooltip" MaxLength="15" data-placement="right"   title="GST"></asp:TextBox>
                                                    
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>TIN</label><br />
                                                     <asp:TextBox ID="textTin" runat="server" AutoComplete="off" height="30px" width="60%" Style="text-transform:uppercase;" class="form-control" data-toggle="tooltip" MaxLength="11" data-placement="right" onchange="validateTIN();" onkeypress="return onlyDotsAndNumbers(this,event);" title="TIN"></asp:TextBox>
                                                    
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>TAN</label>  <br />
                                                   <asp:TextBox ID="textTan" runat="server" AutoComplete="off" height="30px" width="60%" Style="text-transform:uppercase;" class="form-control" data-toggle="tooltip" MaxLength="10" data-placement="right" onchange="ValidateTAN();" title="TAN"></asp:TextBox>
                                                  
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red">*</span>PAN</label> <br />
                                                   
                                                     <asp:TextBox ID="textPan" style="text-transform:uppercase" AutoComplete="off"  runat="server" height="30px" width="60%" class="form-control" data-toggle="tooltip" data-placement="right" title="PAN" MaxLength="10" onchange="pancardValidate();"></asp:TextBox>
                                                
                                                </div>
                                                <div class="clearfix"></div>
                                              <p>&nbsp;</p>
                                           
                                              
                                                <div class="clearfix"></div>
                                             <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                              <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party  Address</label><br />
                                              
                                                 <asp:TextBox ID="txtPartyAddress" class="form-control"  Width="250px" data-toggle="tooltip" Style="text-transform:capitalize;" data-placement="right" title="Party Address" MaxLength="250" TextMode="MultiLine"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline"> <br />
                                           <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>District</label> <br />
                                                            <asp:DropDownList ID="ddlDistrict" Height="30px" Width="250px" data-toggle="tooltip" class="form-control" data-placement="right" title="District"  runat="server">
                                                               
                                                            </asp:DropDownList>
                                                        </div>
                                            
                                            <div  class="col-md-3 col-sm-12 col-xs-12 form-inline"> <br />
                                              <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Mobile No</label><br />
                                                 <asp:TextBox ID="txtMobile"  class="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Mobile No" MaxLength="10"  runat="server" AutoComplete="off" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline"><br />
                                                <label style="font-size: small;"><span style="color: red">*</span>Email</label><br />
                                                <asp:TextBox ID="txtEmail" runat="server"  AutoComplete="off" height="30px" width="60%" class="form-control" data-toggle="tooltip" data-placement="right" MaxLength="50"  onchange="emailValidate(this);chkDuplicateEmail();"  title="Email"></asp:TextBox>
                                                </div> 
                                             <div class="clearfix"></div>
                                              <p>&nbsp;</p>
                                             <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                              <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Current Financial Year</label><br />
                                              
                                                 <asp:TextBox ID="txtFinanceyear" AutoPostBack="true"  Style="text-transform:uppercase;" height="30px" width="60%"  ReadOnly="true" AutoComplete="off" class="form-control"  MaxLength="3" data-toggle="tooltip" data-placement="right" title="Current Financial Year"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline"> 
                                           <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Active</label> <br />
                                                            <asp:DropDownList ID="ddlactive" Height="30px" width="60%"  data-toggle="tooltip" class="form-control" data-placement="right" title="Active"  runat="server">
                                                                <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Yes" Value="True"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="No" Value="False"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline"> 
                                           <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Captive</label> <br />
                                                            <asp:DropDownList ID="ddlCaptive" Height="30px" AutoPostBack="true" width="60%"  data-toggle="tooltip" class="form-control" OnSelectedIndexChanged="ddlCaptive_SelectedIndexChanged" data-placement="right" title="Captive"  runat="server">
                                                                <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Yes" Value="True"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="No" Value="False"></asp:ListItem>
                                                                 </asp:DropDownList>
                                                        </div>
                                            <div id="captive"  runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline"> 
                                           <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Captive Unit Name</label> <br />
                                                            <asp:DropDownList ID="ddlCaptiveunitname" width="60%" AutoPostBack="true" data-toggle="tooltip" class="form-control" data-placement="right" title="Captive Unit Name"  runat="server">
                                                               <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                                  </asp:DropDownList>
                                                        </div>
                                            <div class="clearfix"></div> 
                                             <p>&nbsp;</p>
                                            <div id="isgrain" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                              <br />  
                                                <asp:CheckBox Style="font-size:medium;font:bold" ID="CheckBox1" runat="server" /> <label class="control-label">Is Grain Based</label>
                                            </div>
                                              <div class="clearfix"></div> 
                                              <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div >
                                                    <asp:HiddenField ID="orgid" runat="server" />
                                               <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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

