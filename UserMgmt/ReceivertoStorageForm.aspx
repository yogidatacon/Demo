<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReceivertoStorageForm.aspx.cs" Inherits="UserMgmt.ReceivertoStorageForm" %>

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
                                <title>Receiver to Storage</title>
                                 <script type="text/javascript" src="../common/theme/js/flot/date.js"></script>
                                <script  type="text/javascript">
                                     function validationMsg1() {
                                         if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Comments ");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                     }
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtReceiptDate.ClientID%>').value == '') {
                                            alert("Select Receipt Date");
                                             document.getElementById("<% =txtReceiptDate.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                            if (document.getElementById('<%=txtReceiptTime.ClientID%>').value == '') {
                                                alert("Enter Receipt Time");
                                             document.getElementById("<% =txtReceiptTime.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                         if (document.getElementById('<%=ddlProductionDate.ClientID%>').value == 'Select') {
                                             alert("Select Production Date");
                                             document.getElementById("<% =ddlProductionDate.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                        if (document.getElementById('<%=ddlToStoragevat.ClientID%>').value == 'Select') {
                                            alert("Select  Storagevat VAT");
                                             document.getElementById("<% =ddlToStoragevat.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                    
                                        if (document.getElementById('<%=txtDipInWetCms.ClientID%>').value == '') {
                                            alert("Enter DipsinWet Inches");
                                             document.getElementById("<% =txtDipInWetCms.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        
                                        if (document.getElementById('<%=txtTemprature.ClientID%>').value == '') {
                                            alert("Enter Temperature ");
                                             document.getElementById("<% =txtTemprature.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=txtIndication.ClientID%>').value == '') {
                                            alert("Enter Indication ");
                                             document.getElementById("<% =txtIndication.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=txtStrength.ClientID%>').value == '') {
                                            alert("Enter Strength");
                                             document.getElementById("<% =txtStrength.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        debugger;
                                    <%--  if (document.getElementById('<%=ddTransferToStorageVat.ClientID%>').value == 'Select') {
                                          alert("Select Transfer To Storage VAT");
                                             document.getElementById("<% =ddTransferToStorageVat.ClientID%>").focus();
                                            return false;
                                           
                                        } --%>
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
                                <script type="text/javascript">
                                    
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
                                 
                                    function SelectDate(e) {
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
                                        $('#BodyContent_txtReceiptDate').val(todayDate);
                                        $('#BodyContent_txtrdate').val(todayDate);
                                    }
                                 
                                    
                                 
                                </script>

                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_textTotal').val() == "") {
                                            $('#BodyContent_textTotal').val($('#BodyContent_txtTotal').val());
                                        }
                                        if ($('#BodyContent_txtReceiptDate').val() == "") {
                                            $('#BodyContent_txtReceiptDate').val($('#BodyContent_txttrdate').val());
                                        }
                                    });
                                    </script>
                            </head>
                            <body>
                                <div>

                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt </span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" Visible="false">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Storage to Dispatch</span></asp:LinkButton></li>
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
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnReceipts" Text="Receipts" OnClick="btnReceipts_Click"  >
                                        <span style="color: #fff; font-size: 14px;">Receipts</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="btnTransfer" Text="Transfer" OnClick="btnTransfer_Click" >
                                        <span style="color: #fff; font-size: 14px;">Transfer</span></asp:LinkButton></li>
                                          
                                    </ul>
                                    <br />
                                           </div>  
                                <a>
                                    <asp:HiddenField ID="trqty" runat="server" />
                                    <asp:HiddenField ID="rtype" runat="server" />
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Receipt</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Receipt Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtReceiptDate" Format="dd-MM-yyyy"  OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <script type="text/javascript">
                                                    function SelectDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtReceiptDate').val();
                                                        $('#BodyContent_txttrdate').val(dat1e);
                                                    }
                                                </script>
                                        <asp:TextBox ID="txtReceiptDate"  data-toggle="tooltip" data-placement="right" title="Receipt Date" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txttrdate" runat="server" />
                                    </div>

                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> Receipt Time </label><br />
                                        <input type="time" id="txtReceiptTime" data-toggle="tooltip" data-placement="right" title="Receipt Time " runat="server" class="form-control" >
                                    </div>

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Production Date</label><br />
                                         <asp:DropDownList ID="ddlProductionDate"  runat="server" CssClass="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title=" Production Date" AutoPostBack="true" OnSelectedIndexChanged="ddlProductionDate_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> Storage Vat</label><br />
                                         <asp:DropDownList ID="ddlToStoragevat"  runat="server" CssClass="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title=" ToStoragevat" AutoPostBack="true" OnSelectedIndexChanged="ddlToStoragevat_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                                                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                              <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span> Storage Vat Received Qty</label><br />
                                        <asp:TextBox ID="txtreceived" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Storage Vat Received Qty" ReadOnly="true" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div id="dummytable" runat="server" style="height: auto; width: 95%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Data">
                                            <thead>
                                                <tr>
                                                    <th>Receiver VAT</th>
                                                    <th>Receiver Qty(BL)</th>
                                                    <th>Receiver Qty(LP)</th>
                                                </tr>
                                            </thead>
                                            <tbody id="Datatable">
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdReceiverVat" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server"  AutoGenerateColumns="false">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvatcode" runat="server" Visible="true" Text='<%#Eval("receivervat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceiverVAT" runat="server" Visible="true" Text='<%#Eval("vat_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Qty(BL)" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBulkLiters" runat="server" Visible="true" Text='<%#Eval("bl_received") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Receiver Qty(LP)" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLPLiters" runat="server" Visible="true" Text='<%#Eval("lp_received") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="removal_hour" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblremovalhour" runat="server" Visible="true" Text='<%#Eval("removal_hour") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                           <%--  <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1"  CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                        <div class="clearfix"></div>
                                     <div class="x_title"></div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   </ContentTemplate>
                                    </asp:UpdatePanel>
                                      <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                             
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Dip In Wet Cms</label><br />
                                        <asp:TextBox ID="txtDipInWetCms" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Dip In Wet Cms" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Temperature</label><br />
                                        <asp:TextBox ID="txtTemprature" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Temprature" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Indication</label><br />
                                        <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Indication" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Strength</label><br />
                                        <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Strength" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Increase In Operation (BL)</label><br />
                                        <asp:TextBox ID="txtIncreaseBLLitresInOperation" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Increase In Operation (BL)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Increase By Groging (BL)</label><br />
                                        <asp:TextBox ID="txtIncreaseBLLitresByGroging" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Increase By Groging (BL)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Decrease By Reduction (BL)</label><br />
                                        <asp:TextBox ID="txtDecreasByReduction" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Decreasing By Reduction" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Decrease By Blending (BL)</label><br />
                                        <asp:TextBox ID="txtDecreasByBlending" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Decreasing By Blending" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Decrease By Racking (BL)</label><br />
                                        <asp:TextBox ID="txtDecreasByRacking" onchange="GetBalance()" CssClass="form-control" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" title=" Decreasing By Racking" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Decrease By Wastage Storage (BL)</label><br />
                                        <asp:TextBox ID="txtDecreasByWastageStroage" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Decreasing By Wastage Stroage" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                         </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <%--   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Balance Qty (BL)</label><br />
                                        <asp:TextBox ID="txtBalanceQtyBL" runat="server" onchange="GetValueINLPL2()" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoComplete="off" ReadOnly="true" title=" Balance Qty(BL)" ></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Balance Qty (LP)</label><br />
                                        <asp:TextBox ID="txtBalanceQtyLP"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" ReadOnly="true" AutoComplete="off" title=" Balance Qty(LP)"></asp:TextBox>
                                    </div>--%>
                                   <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Transfer To Storage VAT</label><br />
                                        <asp:DropDownList ID="ddTransferToStorageVat" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Transfer To Dispatch VAT" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:DropDownList>
                                    </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                   <div class="col-md-10 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Remarks</label><br />
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Remarks" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-9 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                                CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg(); " OnClick="btnSaveasDraft_Click" UseSubmitBehavior="false">
                                                       Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click1" UseSubmitBehavior="false">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
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
                                     <div class="col-md-6 col-sm-12 col-xs-12 form-inline" id="receivervalue" runat="server">
                                        <asp:GridView ID="grdvalue" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server"  AutoGenerateColumns="false">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvatcode" runat="server" Visible="true" Text='<%#Eval("receivervat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceiverVAT" runat="server" Visible="true" Text='<%#Eval("vat_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt Qty(BL)" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBulkLiters" runat="server" Visible="true" Text='<%#Eval("bl_received") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Receipt Qty(LP)" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLPLiters" runat="server" Visible="true" Text='<%#Eval("lp_received") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                           <%--  <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1"  CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
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
