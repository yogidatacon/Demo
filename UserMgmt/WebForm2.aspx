<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="UserMgmt.WebForm2" %>

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
                                <title>Request For Pass</title>
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
                                </script>
                                <script  type="text/javascript">
                                    function validationMsg() {
                                        if(document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'M')
                                        {
                                            if (document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'Select') {
                                                alert("Select Release Request No");
                                                document.getElementById("<% =ddlReleaseRequestNo.ClientID%>").focus();
                                                return false;

                                            }
                                            
                                           
                                            if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value == '' ) {
                                                alert("Enter Pass Requested Qty");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                            var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                            if (Request <= 0)
                                            {
                                                alert("Requested Qty Zero Not Allowed");
                                                $('#BodyContent_txtPassRequestedQty').val("");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                        }
                                        else
                                        {
                                             if (document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'Select') {
                                                alert("Select NOC No");
                                                document.getElementById("<% =ddlReleaseRequestNo.ClientID%>").focus();
                                                return false;

                                             }
                                            if (document.getElementById('<%=ddlDepot.ClientID%>').value == 'Select') {
                                                alert("Select Product");
                                                document.getElementById("<% =ddlDepot.ClientID%>").focus();
                                                return false;

                                            }
                                              if (document.getElementById('<%=txtPassDATE.ClientID%>').value == '' ) {
                                                alert("Enter Pass Date");
                                                document.getElementById("<% =txtPassDATE.ClientID%>").focus();
                                                return false;

                                              }
                                              if (document.getElementById('<%=txtDATE.ClientID%>').value == '' ) {
                                                alert("Enter Pass Valid up to");
                                                document.getElementById("<% =txtDATE.ClientID%>").focus();
                                                return false;

                                            }
                                            if (document.getElementById('<%=txtTankerNo.ClientID%>').value == '' ) {
                                                alert("Enter TankerNo");
                                                document.getElementById("<% =txtTankerNo.ClientID%>").focus();
                                                return false;

                                            }
                                             if (document.getElementById('<%=txtTemperature.ClientID%>').value == '' ) {
                                                 alert("Enter Temperature");
                                                document.getElementById("<% =txtTemperature.ClientID%>").focus();
                                                return false;

                                             }
                                             if (document.getElementById('<%=txtIndication.ClientID%>').value == '' ) {
                                                 alert("Enter Indication");
                                                document.getElementById("<% =txtIndication.ClientID%>").focus();
                                                return false;

                                             }
                                             if (document.getElementById('<%=txtStrength.ClientID%>').value == '' ) {
                                                 alert("Enter Strength");
                                                document.getElementById("<% =txtStrength.ClientID%>").focus();
                                                return false;

                                             }
                                             if (document.getElementById('<%=txtDigitalLockNo.ClientID%>').value == '' ) {
                                                 alert("Enter DigitalLockNo");
                                                document.getElementById("<% =txtDigitalLockNo.ClientID%>").focus();
                                                return false;

                                            }
                                            var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                            if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value == '' ) {
                                                alert("Enter Pass Requested Qty");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                            if (Request<=0) {
                                                alert("Requested Qty Zero Not Allowed");
                                                $('#BodyContent_txtPassRequestedQty').val("");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }

                                              if (document.getElementById('<%=txtRouteDetails.ClientID%>').value == '' ) {
                                                  alert("Enter RouteDetails");
                                                document.getElementById("<% =txtRouteDetails.ClientID%>").focus();
                                                return false;

                                             }
                                        }
                                    }
                                    function validationMsg1()
                                    {
                                        if (document.getElementById('<%=txtPassApprovedQty.ClientID%>').value == '')
                                        {
                                            alert("Enter Pass Approved Qty");
                                            document.getElementById("<% =txtPassApprovedQty.ClientID%>").focus();
                                            return false;
                                        }
                                        var Approved = parseFloat($('#BodyContent_txtPassApprovedQty').val());
                                        if (Approved<=0)
                                        {
                                            alert("Pass Approved Qty Zero Not Allowed");
                                            document.getElementById("<% =txtPassApprovedQty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Comments ");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                    }
                                    function QTYvalidationMsg()
                                    {
                                        var Approved = parseFloat($('#BodyContent_txtApprovedQTY').val());
                                        var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                        var balance = parseFloat($('#BodyContent_balanceqty').val()).toFixed(2);
                                        debugger;
                                        if (Request > balance || balance<0) {
                                            alert("Request Qty should be less than or equal to RR/NOC Balance Qty !!!")
                                            $('#BodyContent_txtPassRequestedQty').val("");
                                            $('#BodyContent_txtPassRequestedQty').focus();
                                            return false;
                                        }
                                    }
                                    function QTYvalidationMsg1() {
                                        var Approved = parseFloat($('#BodyContent_txtPassApprovedQty').val());
                                        var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                       
                                        if (Approved > Request) {
                                            alert("Pass Approved Qty should be less than or equal to Pass Requested QTY!!!")
                                            $('#BodyContent_txtPassApprovedQty').val("");
                                            $('#BodyContent_txtPassApprovedQty').focus();
                                            return false;
                                        }
                                    }
                                </script>
                                    <script>
                                 function SelectSetupDate(e) {
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
                                        $('#BodyContent_txtDATE').val(todayDate);
                                        $('#BodyContent_txtdob1').val(todayDate);
                                 }
                                 function SelectpassDate(e) {
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
                                     $('#BodyContent_txtPassDATE').val(todayDate);
                                     $('#BodyContent_txtpass').val(todayDate);
                                 }
                                    </script>
                                 <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob1').val());
                                        }
                                        if ($('#BodyContent_txtPassDATE').val() == "") {
                                            $('#BodyContent_txtPassDATE').val($('#BodyContent_txtpass').val());
                                        }
                                    });
                                    </script>
                            </head>
                            <body>
                                   <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Transport Pass</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click"><span style="color:#fff;font-size:14px;">Dispatch</span></asp:LinkButton></li>
                                        
                                        
                                    </ul>
                                </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2> Request For Transport Pass(Domestic)</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                     
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                            
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <label class="control-label" id="rrl" runat="server" style="display:inline"><span style="color: red">*</span>Release Request No</label>
                                        <label class="control-label" id="nocl" runat="server" style="display:inline"><span style="color: red">*</span>Permit No</label>
                                        <br />
                                        <asp:DropDownList ID="ddlReleaseRequestNo" runat="server" Width="70%" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlReleaseRequestNo_SelectedIndexChanged" data-toggle="tooltip" data-placement="right" ></asp:DropDownList>
                                      
                                    </div>
                                    <div id="depot" runat="server"  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Product Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDepot" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" data-toggle="tooltip" data-placement="right" ></asp:DropDownList>
                                      
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span> Pass Date  </label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtPassDATE"  OnClientDateSelectionChanged="SelectpassDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                          <script type="text/javascript">
                                                    function SelectSetupDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDATE').val();
                                                        $('#BodyContent_txtdob1').val(dat1e);
                                                    }

                                                </script>
                                        <asp:TextBox ID="txtPassDATE"  data-toggle="tooltip" data-placement="right" title="Pass Date" ReadOnly="true" CssClass="form-control validate[required]"  AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtpass" runat="server" />
                                    </div>

                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Pass Valid up to </label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE"  OnClientDateSelectionChanged="SelectSetupDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                          <script type="text/javascript">
                                                    function SelectSetupDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDATE').val();
                                                        $('#BodyContent_txtdob1').val(dat1e);
                                                    }

                                                </script>
                                        <asp:TextBox ID="txtDATE"  data-toggle="tooltip" data-placement="right" title="Pass Valid up to" ReadOnly="true" CssClass="form-control validate[required]"  AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob1" runat="server" />
                                    </div>
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 
                                    
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Product Type</label>
                                        <br />
                                        <asp:TextBox ID="txtMaterialType" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                 
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Allotment Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtAllotmentQty" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Permit Approved QTY</label>
                                        <br />
                                        <asp:HiddenField ID="passtype" runat="server" />
                                        <asp:TextBox ID="txtApprovedQTY" CssClass="form-control" runat="server" data-toggle="tooltip" title="Approved QTY" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Balance Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" ReadOnly="true" data-toggle="tooltip" data-placement="right"  title="RR Balance Qty" ></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"

ControlToValidate="txtBalance" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       
                                <%--  <div class="col-md-1 col-sm-12 col-xs-12">
                                      </div>--%>
                                      <div  class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Customer Name & Address</label>
                                        <br />
                                          <asp:TextBox ID="txtSupplierAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="85%" data-placement="right" title="Supplier Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12">
                                      </div>
                                     <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" id="Label4" runat="server"  style="display: inline; font-size: small"><span style="color: red"></span>Supplier Name & Address</label>
                                        <br />
                                        <asp:TextBox ID="txtNameAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="85%" data-placement="right" title="Name & Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label id="s" runat="server" class="control-label" style="display: inline"><span style="color: red"></span>Customer State</label>
                                        <br />
                                        <asp:TextBox ID="txtstate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="State" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     
                                       <%-- <div class="col-md-5 col-sm-12 col-xs-12">
                                       <%-- <label class="control-label" id="Supplieraddress" runat="server"  style="display: inline; font-size: small"><span style="color: red">*</span>Supplier Address</label>
                                        <label class="control-label" id="caddress" runat="server"  style="display: inline; font-size: small"><span style="color: red"></span>Customer Address</label>
                                        <br />
                                        <asp:TextBox ID="txtSupplierAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="95%" data-placement="right" title="Supplier Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                             </div>
                                 
                                     <div class="col-md-1 col-sm-12 col-xs-12">
                                      </div>
                                    --%>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="Div1" runat="server">
                                        <label id="d" runat="server" class="control-label" style="display: inline"><span style="color: red"></span> Customer District</label>
                                        <br />
                                        <asp:TextBox ID="txtdistrict"  CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="District" ReadOnly="true"></asp:TextBox>
                                    </div>

                                      <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Permit No </label>
                                        <br />
                                        <asp:TextBox ID="txtPermitNo" CssClass="form-control" runat="server" data-toggle="tooltip" Width="70%" data-placement="right" ReadOnly="true"  ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Tanker No</label>
                                        <br />
                                        <asp:TextBox ID="txtTankerNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Tanker No"></asp:TextBox>
                                    </div>
                                     <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Final Allotment No</label>
                                        <br />
                                        <asp:TextBox ID="txtMolassesFinalAllotmentNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"  ></asp:TextBox>
                                    </div>--%>
                                      
                                  <%--  <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Valid Upto</label>
                                        <br />
                                        <asp:TextBox ID="txtRRValidUpto" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>--%>
                                    <%-- <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Unit Name</label>
                                        <br />
                                        <asp:TextBox ID="txtUnitName" CssClass="form-control" runat="server" Width="50%" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>--%>
                                     
                                    <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Pass Requested Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtIssuedqty" CssClass="form-control" runat="server" ReadOnly="true" data-toggle="tooltip" data-placement="right"  title="RR Balance Qty" ></asp:TextBox>
                                    </div>--%>
                                    
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Temperature</label><br />

                                                <asp:TextBox ID="txtTemperature" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Temperature" onkeypress="return onlyDotsAndNumbers(this,event);"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Indication</label><br />

                                        <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Indication"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Strength</label><br />
                                        <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" AutoPostBack="true" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Strength" OnTextChanged="txtStrength_TextChanged"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Digital Lock No</label>
                                        <br />
                                        <asp:TextBox ID="txtDigitalLockNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Digital Lock No"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="balanceqty" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Pass Request Qty(BL)</label>
                                        <br />
                                        <asp:TextBox ID="txtPassRequestedQty" onchange="QTYvalidationMsg()" CssClass="form-control" runat="server" AutoPostBack="true" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Pass Request Qty" OnTextChanged="txtPassRequestedQty_TextChanged" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span> Pass Request Qty(LPL)</label>
                                        <br />
                                        <asp:TextBox ID="txtQtylpl" onchange="CheckQTY()"  CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Pass Approved Qty(LPL)"></asp:TextBox>
                                    </div>
                                        <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display:inline;font-size:12px"><span style="color: red">*</span>Route Details</label>
                                        <br />
                                        <asp:TextBox ID="txtRouteDetails" CssClass="form-control" runat="server" data-toggle="tooltip" Width="85%" data-placement="right" title="Route Details" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="pass" runat="server" style="display:inline"><span style="color: red">*</span>Pass Approved Qty(BL)</label>
                                        <br />
                                        <asp:TextBox ID="txtPassApprovedQty" onchange="QTYvalidationMsg1()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Pass Approved Qty" ></asp:TextBox>
                                    </div>
                                 <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                              <asp:HiddenField ID="rolename" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                             <asp:LinkButton ID="btnApprove" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                             <asp:LinkButton ID="btnissue" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Issue" OnClick="btnissue_Click" />
                                             <asp:LinkButton ID="btnReferBack" Text="Refer Back" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return validationMsg1()"  class="fa fa-backward" OnClick="btnReferBack_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger" OnClientClick="javascript:return validationMsg1()"  class="fa fa-cut" OnClick="btnReject_Click" />
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="approverid" runat="server">
                                        <p>&nbsp;</p>
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
                                   
                        </body>
                            </html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
