<%@ Page Language="C#" MasterPageFile="~/Admin.Master"  AutoEventWireup="true" CodeBehind="MolassesIssueRegisterForm.aspx.cs" Inherits="UserMgmt.MolassesIssueRegisterForm" %>

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
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title> Molasses Issue Register List</title>
                                <script type="text/javascript">
                                    function validationMsg() {
                                      
                                        if (document.getElementById('<%=ddpassno.ClientID%>').value == 'Select') {
                                            alert("Select PassNo");
                                             document.getElementById("<% =ddpassno.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         <%--if (document.getElementById('<%=ddlPartyName.ClientID%>').value == 'Select') {
                                            alert("Select Party Name");
                                            return false;
                                            document.getElementById("<% =ddlPartyName.ClientID%>").focus();
                                         }--%>
                                          if (document.getElementById('<%=txtDate1.ClientID%>').value == '') {
                                            alert("Select Date");
                                             document.getElementById("<% =txtDate1.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                       
                                      if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value < 0) {
                                          alert("OpeningBalance value must be a greater than 0 ");
                                              document.getElementById("<% =txtOpeningBalance.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        <%--   if (document.getElementById('<%=txtTPQ.ClientID%>').value == '') {
                                            alert("Enter Total Production Qty");
                                             document.getElementById("<% =txtTPQ.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=txtTIQ.ClientID%>').value == '') {
                                            alert("Enter Total Issued QTY");
                                              document.getElementById("<% =txtTIQ.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                         if (document.getElementById('<%=txtAvailable.ClientID%>').value == '') {
                                            alert("Enter Total Available QTY");
                                              document.getElementById("<% =txtAvailable.ClientID%>").focus();
                                            return false;
                                          
                                         }--%>
                                        
                                        <%-- if (document.getElementById('<%=txtClosingDips.ClientID%>').value == '') {
                                            alert("Enter Closing Dips");
                                              document.getElementById("<% =txtClosingDips.ClientID%>").focus();
                                            return false;
                                          
                                        }--%>
                                      
                                        if (document.getElementById('<%=ddpassno.ClientID%>').value == 'Select') {
                                            alert("Select PassNo");
                                             document.getElementById("<% =ddpassno.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=txtissuedqut.ClientID%>').value == '') {
                                            alert("Enter IssueQTY");
                                              document.getElementById("<% =txtissuedqut.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                      
                                        if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                                document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                        
                                        }
                                       // ValidateQTY();
                                    }
                                </script>
                                <script type="text/javascript">
                                    function validationMsg1() {
                                        // GetPassDetails();
                                        debugger;
                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver Remarks Name");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;

                                        }

                                    }
                                </script>
                                <script>
                                    function GetOpeningBalance() {
                                        debugger;
                                        var vat_type = $('#BodyContent_ddlVatName').val();
                                      
                                        var jsondata = JSON.stringify($('#BodyContent_partycode').val() + "_" + $('#BodyContent_ddlVatName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "MolassesIssueRegisterForm.aspx/GetOpeningBalance",
                                            data: '{partycode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (data) {
                                               
                                                if (data.d != "") {
                                                    var res = data.d.split("_");
                                                    $('#BodyContent_txtOpeningBalance').val(parseFloat(res[0]).toFixed(2));
                                                    $('#BodyContent_txtTPQ').val(parseFloat(res[1]).toFixed(2));
                                                    $('#BodyContent_txtTIQ').val(parseFloat(res[2]).toFixed(2));
                                                    $('#BodyContent_txtClosingDips').val(parseFloat(res[3]).toFixed(2))
                                                    $('#BodyContent_uom').val(res[4]);
                                                    var available = (parseFloat($('#BodyContent_txtOpeningBalance').val()) + parseFloat($('#BodyContent_txtTPQ').val()) - $('#BodyContent_txtTIQ').val());
                                                    $('#BodyContent_txtAvailable').val(available.toFixed(2));
                                                    if ($('#BodyContent_ddlVatName').val() != "Select" || $('#BodyContent_ddlVatName').val() != "" || $('#BodyContent_ddlVatName').val() != null) {
                                                        $('#BodyContent_opening').val($('#BodyContent_txtOpeningBalance').val());
                                                        $('#BodyContent_production').val($('#BodyContent_txtTPQ').val());
                                                        $('#BodyContent_issued').val($('#BodyContent_txtTIQ').val());
                                                        $('#BodyContent_available').val($('#BodyContent_txtAvailable').val());
                                                        $('#BodyContent_ClosingDips').val($('#BodyContent_txtClosingDips').val());

                                                        $('#BodyContent_vatcode').val($('#BodyContent_ddlVatName').val());
                                                        if ($('#BodyContent_txtOpeningBalance').val() == "NaN") {
                                                            $('#BodyContent_txtOpeningBalance').val("");
                                                        }
                                                        if ($('#BodyContent_txtTPQ').val() == "NaN") {
                                                            $('#BodyContent_txtTPQ').val("");
                                                        }
                                                        if ($('#BodyContent_txtTIQ').val() == "NaN") {
                                                            $('#BodyContent_txtTIQ').val("");
                                                        }
                                                        if ($('#BodyContent_txtAvailable').val() == "NaN") {
                                                            $('#BodyContent_txtAvailable').val("");
                                                        }
                                                        if ($('#BodyContent_txtClosingDips').val() == "NaN") {
                                                            $('#BodyContent_txtClosingDips').val("");
                                                        }
                                                       
                                                    }
                                                }
                                               
                                            }
                                        });
                                    }
                                    </script>
                                
                                
                                 <script>
                                    function ValidateQTY() {
                                       
                                       
                                        var issued = parseFloat($('#BodyContent_txtissuedqut').val());
                                        var rem = parseFloat($('#BodyContent_remqty').val());
                                        var TIQ = parseFloat($('#BodyContent_txtAvailable').val());
                                        $('#BodyContent_issuedqut').val(issued);
                                        if (issued < rem) {
                                            alert("Issued QTY Not Morethan of Pass QTY");
                                            $('#BodyContent_txtissuedqut').val("");
                                            $('#BodyContent_txtissuedqut').focus();
                                            return false;
                                        }
                                        if (issued > TIQ) {
                                            alert("Issued QTY Not Morethan of Available QTY");
                                            $('#BodyContent_txtissuedqut').val("");
                                            $('#BodyContent_txtissuedqut').focus();
                                            return false;
                                        }
                                    }
                                     </script>
                                 <script>
                                     function SelectDate(e) {
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
                                         $('#BodyContent_txtDate1').val(todayDate);
                                         $('#BodyContent_txtdob').val(todayDate);
                                       
                                    }
                                   

                                </script>
                                <script>
                                    $(document).ready(function ()
                                    {
                                        debugger;
                                        if ($('#BodyContent_topartycode').val() == "") {
                                            GetOpeningBalance();
                                           // GetPassDetails();
                                        }
                                        if ($('#BodyContent_ddlPartyName').val() == "Select" || $('#BodyContent_ddlPartyName').val() == "" || $('#BodyContent_ddlPartyName').val() == null) {
                                            $('#BodyContent_ddlPartyName').val($('#BodyContent_topartycode').val());
                                        }

                                        if ($('#BodyContent_ddlVatName').val() == "Select" || $('#BodyContent_ddlVatName').val() == "" || $('#BodyContent_ddlVatName').val() == null) {
                                          //  LoadVatNames();
                                            $('#BodyContent_ddlVatName').val($('#BodyContent_vatcode').val());
                                        }

                                        if ($('#BodyContent_txtOpeningBalance').val() == "" || $('#BodyContent_txtOpeningBalance').val() == null) {
                                            GetOpeningBalance();
                                            $('#BodyContent_txtOpeningBalance').val($('#BodyContent_opening').val());
                                        }
                                        if ($('#BodyContent_txtTPQ').val() == "" || $('#BodyContent_txtTPQ').val() == "")
                                            $('#BodyContent_txtTPQ').val($('#BodyContent_production').val());
                                        if ($('#BodyContent_txtTIQ').val() == "" || $('#BodyContent_txtTIQ').val() == null)
                                            $('#BodyContent_txtTIQ').val($('#BodyContent_issued').val());
                                        if ($('#BodyContent_txtAvailable').val() == "" || $('#BodyContent_txtAvailable').val() == null)
                                            $('#BodyContent_txtAvailable').val($('#BodyContent_available').val());

                                        if ($('#BodyContent_txtOpeningBalance').val() == "NaN") {
                                            $('#BodyContent_txtOpeningBalance').val("");
                                        }
                                        if ($('#BodyContent_txtTPQ').val() == "NaN") {
                                            $('#BodyContent_txtTPQ').val("");
                                        }
                                        if ($('#BodyContent_txtTIQ').val() == "NaN") {
                                            $('#BodyContent_txtTIQ').val("");
                                        }
                                        if ($('#BodyContent_txtAvailable').val() == "NaN") {
                                            $('#BodyContent_txtAvailable').val("");
                                        } 
                                        if ($('#BodyContent_txtClosingDips').val() == "NaN") {
                                            $('#BodyContent_txtClosingDips').val("");
                                        }
                                        //if ($('#BodyContent_ddpassno').val() == "Select" || $('#BodyContent_ddpassno').val() == "" || $('#BodyContent_ddpassno').val() == null) {

                                        //    $('#BodyContent_ddpassno').val($('#BodyContent_passno').val());
                                        //}
                                        //if ($('#BodyContent_txtPassDate').val() == "" || $('#BodyContent_txtPassDate').val() == null) {
                                        //    GetPassDetails();
                                        //    $('#BodyContent_txtPassDate').val($('#BodyContent_passdate').val());
                                        //}
                                        //if ($('#BodyContent_txtissuetype').val() == "" || $('#BodyContent_txtissuetype').val() == null) {
                                        //    $('#BodyContent_txtissuetype').val($('#BodyContent_issuetype').val());
                                        //}
                                        //if ($('#BodyContent_txtDLN').val() == "" || $('#BodyContent_txtDLN').val() == null) {
                                        //    $('#BodyContent_txtDLN').val($('#BodyContent_digilock').val());
                                        //}
                                        if ($('#BodyContent_txtDate1').val() == "") {
                                            $('#BodyContent_txtDate1').val($('#BodyContent_txtdob').val());
                                        }
                                      
                                    });
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
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    </div>
                                    <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                       <div>
                                     <div class="x_title">
                                            <h2>Molasses Issue Register Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                 <asp:HiddenField ID="uom" runat="server" />
                                                <label class="control-label"><span style="color: red">*</span>Pass No</label><br />
                                                <asp:DropDownList ID="ddpassno" AutoPostBack="true" OnSelectedIndexChanged="ddpassno_SelectedIndexChanged" Width="90%" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Pass No">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="passno" runat="server" />
                                                <asp:HiddenField ID="passqty" runat="server" />
                                                <asp:HiddenField ID="vehicleno" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>VAT Name</label><br />
                                                <asp:DropDownList ID="ddlVatName" onchange="GetOpeningBalance()" runat="server" data-toggle="tooltip" Width="80%" data-placement="right" title="VAT Name" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="vatcode" runat="server" />
                                            </div>

                                           <%-- <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>To Party Name</label><br />
                                                <asp:DropDownList ID="ddlPartyName" onchange="GetTopartyCode()" runat="server" Width="80%" data-toggle="tooltip" data-placement="right" title="Party Name"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                                               
                                            </div>--%>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                 <asp:HiddenField ID="partycode" runat="server" />
                                                <asp:HiddenField ID="topartycode" runat="server" />
                                                <label class="control-label"><span style="color: red">*</span>Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" OnClientDateSelectionChanged="SelectDate"  TargetControlID="txtDate1" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                              
                                                <asp:TextBox ID="txtDate1" data-toggle="tooltip" data-placement="right"  title="Date" CssClass="form-control" AutoComplete="off" runat="server" >
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" CssClass="control-label" runat="server" Height="10%" Width="10%"  ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" runat="server" />

                                            </div>
                                            
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Opening Balance</label><br />
                                                <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Opening Balance" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="opening" runat="server" />
                                            </div>                                                      
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Total Production Quantity </label>
                                                <br />
                                                <asp:TextBox ID="txtTPQ" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Total Production Quantity" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="production" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Total Issued Quantity </label>
                                                <br />
                                                <asp:TextBox ID="txtTIQ" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Total Issued Quantity" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="issued" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Total Available Quantity</label>
                                                <br />
                                                <asp:TextBox ID="txtAvailable" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Total Available Quantity" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="available" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Dips in CM</label><br />
                                                <asp:TextBox ID="txtClosingDips" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Dips in CM" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="ClosingDips" runat="server" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            
                                              <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>To Party Name</label><br />
                                                 <asp:TextBox ID="txttoparty" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="To Party Name" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Pass Date</label><br />
                                                <asp:TextBox ID="txtPassDate" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Pass Date" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="passdate" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                
                                                <label class="control-label"><span style="color: red">*</span>Issue Type</label><br />
                                                <asp:TextBox ID="txtissuetype" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Issue Type" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="issuetype" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Digi-Lock No</label><br />
                                                <asp:TextBox ID="txtDLN" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Digi-Lock No" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="digilock" runat="server" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Issued Quantity</label><br />
                                                <asp:TextBox ID="txtissuedqut" onchange="ValidateQTY();" ReadOnly="true" runat="server" CssClass="form-control" Width="80%" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Issued Quantity"></asp:TextBox>
                                                <asp:HiddenField ID="issuedqut" runat="server" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Closing Dips in CM</label><br />
                                                <asp:TextBox ID="txtDipsinCM" onchange="ValidateQTY();" runat="server" CssClass="form-control" Width="80%" data-toggle="tooltip"  onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Closing Dips in CM"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Value(@RS)</label><br />
                                                <asp:TextBox ID="txtvalue" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Value(@RS)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Basic(@RS)</label><br />
                                                <asp:TextBox ID="txtBasic" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Basic(@RS)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>SPL(@RL)</label><br />
                                                <asp:TextBox ID="txtSPL" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="SPL(@RL)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Destroyed/Wasted(Qts)</label><br />
                                                <asp:TextBox ID="txtwaste" runat="server" CssClass="form-control" data-toggle="tooltip" Width="80%" data-placement="right" title="Destroyed/Wasted(Qts)"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-11 col-sm-12 col-xs-12">
                                                <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="85%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                                <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                                <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Approver Comments" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <asp:HiddenField ID="mirid" runat="server" />
                                                <asp:HiddenField ID="remqty" runat="server" />
                                                <asp:LinkButton ID="btnSaveasDraft" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-info" OnClick="btnSaveasDraft_Click">
                                                    <span class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                                <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSave_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnApprove" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                                <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                                <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                    CssClass="btn btn-danger" OnClick="btnCancel_Click1">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                            </div>
                                            <p>&nbsp;</p>
                                            <div id="approver" runat="server">
                                                <div class="x_title">
                                                    <h4>Approval Summary</h4>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_title">
                                                    <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                        HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                        HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" Width="1218px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Transaction_Date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approver Role" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                        </Columns>
                                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                    </asp:GridView>
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
