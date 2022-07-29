<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DistillationSetupForm.aspx.cs" Inherits="UserMgmt.DistillationSetupForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>


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
                                <title>Distillation Setup Form</title>
                                <script type="text/javascript">
                                    
                                     function validationMsg1() {

                                         if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Remarks Name");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                         CheckIsRepeat();
                                     }
                                        </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddlDateofSetup.ClientID%>').value == 'Select') {
                                            alert("Select Date of Setup");
                                            document.getElementById("<% =ddlDateofSetup.ClientID%>").focus();
                                            return false;

                                        }
                                    if (document.getElementById('<%=ddlFermenter.ClientID%>').value == 'Select') {
                                        alert("Select Fermenter");
                                            document.getElementById("<% =ddlFermenter.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDateofDistillation.ClientID%>').value == '') {
                                            alert("Enter Distillation Date");
                                            document.getElementById("<% =txtDateofDistillation.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtFinalSpecificGravity.ClientID%>').value == '') {
                                            alert("Enter Final Specific Gravity");
                                            document.getElementById("<% =txtFinalSpecificGravity.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtDegreeofAttenuation.ClientID%>').value == '') {
                                             alert("Enter Degree of Attenuation");
                                            document.getElementById("<% =txtDegreeofAttenuation.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtVatorCasks.ClientID%>').value == '') {
                                            alert("Enter no of Vat or Cask");
                                            document.getElementById("<% =txtVatorCasks.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtStartDat.ClientID%>').value == '') {
                                            alert("Enter StartDate");
                                            document.getElementById("<% =txtStartDat.ClientID%>").focus();
                                            return false;
                                        }
                                        
                                         if (document.getElementById('<%=txtStartTim.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter StartTime");
                                            document.getElementById("<% =txtStartTim.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                        if (document.getElementById('<%=txtEndDat.ClientID%>').value == '') {
                                            alert("Enter End Date");
                                            document.getElementById("<% =txtEndDat.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtEndTim.ClientID%>').value == '') {
                                             alert("Enter End Time");
                                            document.getElementById("<% =txtEndTim.ClientID%>").focus();
                                            return false;
                                         }

                                          if (document.getElementById('<%=txtBulkLitres1.ClientID%>').value == '') {
                                              alert("Enter Bulk Litres");
                                            document.getElementById("<% =txtBulkLitres1.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtToWhicStill.ClientID%>').value == '') {
                                            alert("Enter To Which Still");
                                            document.getElementById("<% =txtToWhicStill.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtTotalBLRemoved.ClientID%>').value == '') {
                                            debugger;
                                            alert("Enter Total BL Removed");
                                            document.getElementById("<% =txtTotalBLRemoved.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtTotalLPLitresRemoved.ClientID%>').value == '') {
                                            alert("Enter Total LP Litres Removed");
                                            document.getElementById("<% =txtTotalLPLitresRemoved.ClientID%>").focus();
                                            return false;

                                        }
                                      <%-- if (document.getElementById('<%=.ClientID%>').value == '') {
                                             alert("Enter End Time");
                                            document.getElementById("<% =txtEndTim.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        CheckIsRepeat();
                                    }
                                </script>
                                   <%--CheckIsRepeat();--%>    
                            <script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            debugger;
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
                                 </script>
                                <script language="javascript" type="text/javascript">
                                    function CheckDepot() {
                                        debugger;
                                       if (document.getElementById('<%=ddlReceiverVAT.ClientID%>').value == 'Select') {
                                           alert("Select ReceiverVAT");
                                            document.getElementById("<% =ddlReceiverVAT.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtBulkLiters.ClientID%>').value == '') {
                                            alert("Enter Bulk Liters");
                                            document.getElementById("<% =txtBulkLiters.ClientID%>").focus();
                                            return false;

                                        }

                                         if (document.getElementById('<%=txtLPLiters.ClientID%>').value == '') {
                                             alert("Enter LP Liters");
                                            document.getElementById("<% =txtLPLiters.ClientID%>").focus();
                                            return false;

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
                                    function onlyDotsAndNumbers3(txt, event) {
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
                                            if ((txtlen - dotpos) > 3)
                                                return false;
                                        }
                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;
                                        return true;
                                    }

                                    function onlyDotsAndNumbers1(txt, event) {
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
                                            if ((txtlen - dotpos) > 1)
                                                return false;
                                        }
                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;
                                        return true;
                                    }
                                    </script>
                                <script language="javascript" type="text/javascript">
                                    function Selectdate(e) {
                                        debugger;;
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
                                        $('#BodyContent_txtDateofDistillation').val(todayDate);
                                        $('#BodyContent_txtdob1').val(todayDate);
                                          
                                       
                                    }

                                    function SelectStartDate(e) {
                                        debugger;;
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
                                      
                                        $('#BodyContent_txtStartDat').val(todayDate);
                                        $('#BodyContent_txtgpd').val(todayDate);
                                    }

                                    function SelectEndDate(e) {
                                        debugger;;
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
                                        $('#BodyContent_txtEndDat').val(todayDate);
                                        $('#BodyContent_txtgpd1').val(todayDate);
                                    }
                                   
                                  </script> 
                                 <script>
                                    $(document).ready(function () {
                                        debugger;;
                                        if ($('#BodyContent_txtDateofDistillation').val() == "") {
                                            $('#BodyContent_txtDateofDistillation').val($('#BodyContent_txtdob1').val());
                                            //$find("start").set_startDate.val($('#BodyContent_txtdob1').val());
                                        }
                                        if ($('#BodyContent_txtStartDat').val() == "") {
                                            $('#BodyContent_txtStartDat').val($('#BodyContent_txtgpd').val());
                                           // $find("end").set_startDate.val($('#BodyContent_txtgpd').val());
                                        }
                                        if ($('#BodyContent_txtEndDat').val() == "") {
                                            $('#BodyContent_txtEndDat').val($('#BodyContent_txtgpd1').val()); 
                                        }
                                        //if ($('#BodyContent_txtStartTim').val() == "") {
                                        //    $('#BodyContent_txtStartTim').val($('#BodyContent_txttime').val());
                                        //}
                                    });
                                    </script> 
                              <%--  <script type="text/javascript">


        function TextDateChanged(newDate) {
            try {
                debugger;;
                var d = Date.parse(newDate);
                $find("start").set_SelectStartDate = d;
               // $get("HiddenField1").value = $find("calDateBehaviour")._selectedDate;
            }
            catch (Exception) {
            }
        }
onchange="TextDateChanged(this.value)"

    </script>--%>

                               
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">

                                         <li>
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click"  >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" Visible="false">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">From Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage & Adjustment</span></asp:LinkButton></li>
                                        <li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" Text="Opening Balance" OnClick="lnkOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>

                                 <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div>
                                    <ul class="nav nav-tabs">
                                         <li >
                                            <asp:LinkButton runat="server" ID="btnfermenter" Text="Seizure List" OnClick="btnfermenter_Click">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup</span></asp:LinkButton></li>

                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnDistillation" Text="Seizure List" OnClick="btnDistillation_Click">
                                        <span style="color: #fff; font-size: 14px;">Distillation Process</span></asp:LinkButton></li>
                                          
                                    </ul>
                                    <br />
                                           </div>  
                                 <a>
                                   <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                               <div class="x_title">
                                      <h2>Distillation (Wash removed to still and spirit produced to store)</h2>
                                       <div class="clearfix"></div>
                                  </div>
                                <div style="float: right">
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Date of Setup</label><br />
                                        <asp:DropDownList ID="ddlDateofSetup" CssClass="form-control" data-toggle="tooltip" data-placement="right"  Width="60%" title="Date of Setup" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDateofSetup_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                 <asp:HiddenField ID="ddsetup" runat="server"/>
                                    </div>
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>To Fermenter VAT </label>
                                        <br />
                                        <asp:DropDownList ID="ddlFermenter" CssClass="form-control" data-toggle="tooltip"  Width="60%" data-placement="right" title="To Fermenter VAT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFermenter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                            
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"  runat="server"  style="display: inline"><span style="color: red"></span> Rawmaterial Used</label><br />
                                        <input type="text" id="txtMolassesUsed" runat="server" data-toggle="tooltip"  style="Width:60%" data-placement="right" title="Molasses" class="form-control" name="size" readonly="readonly">
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Total BL of Wash Setup</label><br />
                                        <input type="text" id="txtTotalBL" runat="server" data-toggle="tooltip"  style="Width:60%" data-placement="right" title="Total BL of Wash Setup" class="form-control" name="size" readonly="readonly">
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                           <label class="control-label" style="font-size: small;display:inline""><span style="color: red"></span> Wash / Avg. in each Vat or Cask(SG)</label><br />
                                            <input type="text" id="txtwashcask" runat="server" data-toggle="tooltip"  style="Width:60%" data-placement="right" title="Wash / Avg. in each Vat or Cask" class="form-control" readonly="readonly">
                                        </div>
                                     
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="x_title"></div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Distillation Date </label>
                                        <br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2"  TargetControlID="txtDateofDistillation"  BehaviorID="dd" OnClientDateSelectionChanged="Selectdate"  Format="dd-MM-yyyy" ID="CalendarExtender4"></cc1:CalendarExtender>

                                          <%--  <script type="text/javascript">

                                                    function SelectDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDateofDistillation').val();
                                                        $('#BodyContent_txtdob1').val(dat1e);
                                                    }

                                                </script>--%>
                                        <asp:TextBox ID="txtDateofDistillation"  Width="60%"  data-toggle="tooltip" data-placement="right"  title="Date Reserved for Form 83" class="form-control validate[required]"   AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob1" runat="server" OnValueChanged="txtDateofDistillation_TextChanged"   />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Final Specific Gravity</label><br />
                                            <input type="text" id="txtFinalSpecificGravity"  autocomplete="off" style="Width:60%" runat="server" onkeypress="return onlyDotsAndNumbers3(this,event);" data-toggle="tooltip" data-placement="right" title="Final Specific Gravity" class="form-control" name="size">
                                        </div>
                                    
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Number of Degree of Attenuation</label><br />
                                            <input type="text" id="txtDegreeofAttenuation" autocomplete="off"  style="Width:60%" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip"  data-placement="right" title="Degree of Attenuation" class="form-control" name="size">
                                        </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>No of Vats or Casks</label><br />
                                        <input type="text" id="txtVatorCasks" runat="server" autocomplete="off"  style="Width:60%" class="form-control" data-toggle="tooltip" data-placement="right"  title="No of Vats or Casks" name="size">
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> Start Date </label>
                                        <br />
                                          <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtStartDat" BehaviorID="start" OnClientDateSelectionChanged="SelectStartDate"   Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                       
                                        <asp:TextBox ID="txtStartDat"  data-toggle="tooltip"   Width="60%" data-placement="right" title="StartDate" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px"></asp:TextBox>
                                        <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtgpd" runat="server" OnValueChanged="txtStartDat_TextChanged" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> Start Time </label><br />
                                        <input type="time" id="txtStartTim" data-toggle="tooltip"  style="Width:60%" data-placement="right" title="Start Time" runat="server" class="form-control" >
                                        <asp:HiddenField ID="txttime" runat="server"/>
                                    </div>

                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> End Date</label><br />
                                          <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txtEndDat" BehaviorID="end" OnClientDateSelectionChanged="SelectEndDate"    Format="dd-MM-yyyy" ID="CalendarExtender3"></cc1:CalendarExtender>
                                         <%-- <script type="text/javascript">

                                                    function SelectDate2(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtEndDat').val();
                                                        $('#BodyContent_txtgpd1').val(dat1e);
                                                    }

                                                </script>--%>
                                          <asp:TextBox ID="txtEndDat"  data-toggle="tooltip"  Width="60%"  data-placement="right" title="End Date"  class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px" ></asp:TextBox>
                                        <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtgpd1" runat="server" OnValueChanged="txtEndDat_TextChanged" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> End Time</label><br />
                                        <input type="time" id="txtEndTim" runat="server"  style="Width:60%" data-toggle="tooltip" data-placement="right" title="End Time" class="form-control" >
                                    </div>

                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Bulk Liters</label><br />
                                        <input type="text" id="txtBulkLitres1" runat="server"  style="Width:60%" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Bulk Litres" class="form-control">
                                    </div>
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>To Which Still</label><br />
                                        <input type="text" id="txtToWhicStill" runat="server" style="Width:60%" data-toggle="tooltip" autocomplete="off"  data-placement="right" title="To Which Still" class="form-control" >
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Total Rawmaterial Removed (Qtls)</label>
                                        <br />
                                        <input type="text" id="txtTotalBLRemoved" runat="server" data-toggle="tooltip"  style="Width:60%" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Total BL Removed" class="form-control" >
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Total Wash Removed </label>
                                        <br />
                                        <input type="text" id="txtTotalLPLitresRemoved" runat="server" data-toggle="tooltip" style="Width:60%" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Total LP Litres Removed" class="form-control" name="size">
                                    </div>
                                   
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="x_title">
                                          <h2>Spirit produced to store</h2>
                                          <div class="clearfix"></div>
                                  </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Receiver VAT </label>
                                        <br />
                                           <asp:DropDownList ID="ddlReceiverVAT" CssClass="form-control"  data-toggle="tooltip" Width="60%" data-placement="right" title="Receiver VAT " runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiverVAT_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Available Qty</label><br />
                                        <input type="text" id="txtAvailableQty" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right"   style="Width:60%" title="Available" class="form-control" readonly="readonly" name="size">
                                    </div>

                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Bulk Liters</label><br />
                                           <asp:TextBox ID="txtBulkLiters"  data-toggle="tooltip"  Width="60%"  CssClass="form-control" data-placement="right" title="Bulk Liters" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"  runat="server"  ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>LP Liters</label><br />
                                        <input type="text" id="txtLPLiters" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers1(this,event);"  style="Width:60%" title="LP Liters" class="form-control" name="size">
                                    </div>
                                 
                                              <div class="clearfix"></div> 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span></label><br />
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-upload" Width="50%"  OnClientClick="javascript:return CheckDepot()" OnClick="Add" Text="ADD" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div id="dummytable" runat="server" style="height: auto; width: 95%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Data">
                                            <thead>
                                                <tr>
                                                    <th>Receiver VAT</th>
                                                    <th>Bulk Liters</th>
                                                    <th>LP Liters</th>
                                                     <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="Datatable">
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                           <div> 
                                        <asp:GridView ID="grdDistillation" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server"  AutoGenerateColumns="false">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvatcode" runat="server" Visible="true" Text='<%#Eval("vat_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceiverVAT" runat="server" Visible="true" Text='<%#Eval("Receiver VAT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bulk Liters" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBulkLiters" runat="server" Visible="true" Text='<%#Eval("Bulk Liters") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="LP Liters" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLPLiters" runat="server" Visible="true" Text='<%#Eval("LP Liters") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1"  CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="ImageButton1_Click"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"  runat="server"  style="display: inline"><span style="color: red"></span>Is distillation complete for the setup date </label>
                                          &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RadioButton1" runat="server" GroupName="radio" />Yes &nbsp;&nbsp;&nbsp;&nbsp;
                                         <span><asp:RadioButton ID="RadioButton2" runat="server" GroupName="radio" />No</span>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <div class="x_title">
                                      <h2>Re-Distillation</h2>
                                       <div class="clearfix"></div>
                                  </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Bulk Litres</label><br />
                                        <input type="text" id="txtBulk" runat="server"  style="Width:60%" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Bulk Litres" onkeypress="return onlyDotsAndNumbers(this,event);" value="0" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>From What Vessel</label><br />
                                        <input type="text" id="txtVessel" runat="server"  autocomplete="off" style="Width:60%" data-toggle="tooltip" data-placement="right" title="From What Vessel" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>To which Still Removed</label><br />
                                        <input type="text" id="txtStillRemoved" runat="server"  style="Width:60%" data-toggle="tooltip" autocomplete="off" data-placement="right" title="To which Still Removed" class="form-control" name="size">
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red"></span>Bulk Litres(Produced for Re-distillation)</label><br />
                                        <input type="text" id="txtBulkLitresp" runat="server" style="Width:60%" data-toggle="tooltip" autocomplete="off" data-placement="right" value="0" onkeypress="return onlyDotsAndNumbers(this,event);" title="Bulk Litres" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red"></span>LP Litres(Produced for Re-distillation)</label><br />
                                        <input type="text" id="txtLPLitresp" runat="server" style="Width:60%" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="LP Litres" value="0" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>BL Litres per Qtl of Materials </label><br />
                                        <input type="text" id="txtBulkOfMaterials" runat="server"  style="Width:60%" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="BL Litres per Qtl of Materials " value="0" class="form-control" name="size">
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Percentage of LP sprit in the wash </label><br />
                                        <input type="text" id="txtLPspritinwash" runat="server"  style="Width:60%" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Percentage of LP sprit in the wash" value="0" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Deg. of Atten per 100 Lt of Wash</label><br />
                                        <input type="text" id="txtDeg10Attenuation" runat="server" style="Width:60%" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Deg. of Attenuation per 100 Litre of Wash" class="form-control" value="0" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Spirit Register Pages</label><br />
                                        <input type="text" id="txtSpiritPages" runat="server" style="Width:60%" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Spirit charge Register Pages" class="form-control" name="size">
                                    </div> 
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-11 col-sm-12 col-xs-12">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Remarks</label><br />
                                        <textarea id="txtRemarks" class="form-control" data-toggle="tooltip" autocomplete="off"  data-placement="right" title="Remarks" runat="server"></textarea>
                                    </div>
                                   
                                      <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                
                                         
                                             <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <asp:LinkButton ID="btnSaveAsDraft" runat="server"
                                            CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click">
                                                        Save as Draft</asp:LinkButton>
                                          <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click" >
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                         <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        <asp:LinkButton ID="btnupdate" runat="server" Visible="false" OnClientClick="javascript:return validationMsg2();" CssClass="btn btn-primary" OnClick="btnupdate_Click"><span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                    </div>
                                </div>
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
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Documents_id") %>' ForeColor="Black" runat="server" OnClick="DeleteFiles" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                </div></div>
                                </body>
                                </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
