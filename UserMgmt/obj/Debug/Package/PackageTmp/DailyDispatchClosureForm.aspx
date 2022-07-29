<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DailyDispatchClosureForm.aspx.cs" Inherits="UserMgmt.DailyDispatchClosureForm" %>

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
                                <title>Daily Dispatch Closure</title>

                                
                                 <script  type="text/javascript">
                                     function validationMsg1() {
                                      if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Comments ");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                     }
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtDateofRemoval.ClientID%>').value == '') {
                                            alert("Enter Date of Removal");
                                             document.getElementById("<% =txtDateofRemoval.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        if (document.getElementById('<%=ddDispatchVAT.ClientID%>').value == 'Select') {
                                             alert("Select Dispatch VAT");
                                             document.getElementById("<% =ddDispatchVAT.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                     <%--   if (document.getElementById('<%=ddlTransferDate.ClientID%>').value == 'Select') {
                                            alert("Select  Transfer Date");
                                             document.getElementById("<% =ddlTransferDate.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                          if (document.getElementById('<%=ddDispatchVAT.ClientID%>').value == 'Select') {
                                              alert("Select Dispatch Vat");
                                             document.getElementById("<% =ddDispatchVAT.ClientID%>").focus();
                                            return false;
                                           
                                      }--%>

                                         <%-- if (document.getElementById('<%=txtTransferQtyBL.ClientID%>').value == '') {
                                            alert("Enter TransferQtyBL");
                                             document.getElementById("<% =txtTransferQtyBL.ClientID%>").focus();
                                            return false;
                                           
                                        }--%>

                                    
                                        if (document.getElementById('<%=txtDipsinWetInches.ClientID%>').value == '') {
                                            alert("Enter DipsinWet Inches");
                                             document.getElementById("<% =txtDipsinWetInches.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                        
                                        if (document.getElementById('<%=txtTemperature.ClientID%>').value == '') {
                                            alert("Enter Temperature ");
                                             document.getElementById("<% =txtTemperature.ClientID%>").focus();
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

                                    function SelectDate1(e) {
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
                                        $('#BodyContent_txtDateofRemoval').val(todayDate);
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                </script>

                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                       
                                        if ($('#BodyContent_txtDateofRemoval').val() == "") {
                                            $('#BodyContent_txtDateofRemoval').val($('#BodyContent_txtdob').val());
                                        }
                                    });
                                    </script>
                            </head>
                            <body>
                                <div>

                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" Visible="false">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Storage to Dispatch</span></asp:LinkButton></li>
                                        <li class="active">
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
                                <a>
                                   <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Daily Dispatch Closure</h2>
                                    <div class="clearfix"></div>
                                </div>


                                <div style="float: right">
                                    
                                </div>
                                <div class="x_content">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                
                                 
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Date of Removal</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDateofRemoval" OnClientDateSelectionChanged="SelectDate1" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                                  <script type="text/javascript">

                                                    function SelectDate1(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDateofRemoval').val();
                                                        $('#BodyContent_txtdob').val(dat1e);
                                                    }

                                                </script>

                                                <asp:TextBox ID="txtDateofRemoval" data-toggle="tooltip" data-placement="right" title="Date of Removal" class="form-control validate[required]" AutoComplete="off"  runat="server" Font-Size="14px" AutoPostBack="true">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" OnValueChanged="txtDateofRemoval_TextChanged" runat="server" />

                                            </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                      
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Dispatch VAT</label><br />
                                                <asp:DropDownList ID="ddDispatchVAT" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch VAT"  runat="server" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="ddDispatchVAT_SelectedIndexChanged"></asp:DropDownList>
                                                  <asp:HiddenField ID="hidvat" runat="server" />

                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red">*</span>Dispatch Qty</label><br />
                                     <asp:TextBox ID="txtDispatchQty" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title=" Dispatch Qty" ReadOnly="true" ></asp:TextBox>
                                </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red"></span>Available Qty (BL)</label><br />
                                     <asp:TextBox ID="txtAvailableQtyBL" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title=" Available Qty(BL)" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red"></span>Available Qty (LP)</label><br />
                                    <asp:TextBox ID="txtAvailableQtyLPL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)"  ReadOnly="true"></asp:TextBox>
                                     </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Dips in Wet CM</label><br />
                                                <asp:TextBox ID="txtDipsinWetInches" runat="server" data-toggle="tooltip" data-placement="right" title="Dips in Wet CM" AutoComplete="off"  CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </div>
                                            
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Temperature</label><br />

                                                <asp:TextBox ID="txtTemperature" runat="server" data-toggle="tooltip" data-placement="right" title="Temperature" AutoComplete="off"  CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </div>
                                 

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Indication</label><br />

                                        <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title="Indication" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Strength</label><br />
                                        <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title="Strength" AutoPostBack="true" OnTextChanged="txtStrength_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Decrease by Reduction (BL)</label><br />
                                                <asp:TextBox ID="txtDecreasbyReduction" runat="server" data-toggle="tooltip" data-placement="right" title="Decrease by Reduction (BL)" AutoComplete="off"  CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDecreasbyReduction_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            <asp:HiddenField ID="DBRED" runat="server" />
                                                  </div>
                                            
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Decrease by Blending (BL)</label><br />

                                                <asp:TextBox ID="txtDecreasbyBlending" runat="server" data-toggle="tooltip" data-placement="right" title="Decrease by Blending (BL)" AutoComplete="off"  CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDecreasbyBlending_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                             <asp:HiddenField ID="DBB" runat="server" />
                                                 </div>
                                 

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Decrease by Racking (BL)</label><br />

                                        <asp:TextBox ID="txtDecreasRacking" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoComplete="off" title="Decrease by Racking (BL)" AutoPostBack="true" OnTextChanged="txtDecreasRacking_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                   <asp:HiddenField ID="DBR" runat="server" />
                                           </div>
                                    

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Decrease by Wastage (BL)</label><br />
                                        <asp:TextBox ID="txtDecreasbyWastage" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Decrease by Wastage (BL)" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtDecreasbyWastage_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:HiddenField ID="DBW" runat="server" />
                                          </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Increase BL In Operation</label><br />

                                        <asp:TextBox ID="txtIncreaseBLInOperation" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoComplete="off" title="Increase BL In Operation" AutoPostBack="true" OnTextChanged="txtIncreaseBLInOperation_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:HiddenField ID="IBO" runat="server" />
                                                  </div>
                                    

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Increase BL By Groging</label><br />
                                        <asp:TextBox ID="txtIncreaseBLByGroging" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Increase BL By Groging" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtIncreaseBLByGroging_TextChanged" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    <asp:HiddenField ID="IBG" runat="server" />
                                          </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Actual Dispatch (BL)</label><br />

                                        <asp:TextBox ID="txtBalanceBLQty" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Balance BL Qty" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Actual Dispatch (LP)</label><br />
                                        <asp:TextBox ID="txtBalanceLPQty" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Balance LP Qty"></asp:TextBox>
                                    </div>
                              
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                  <div class="col-md-10 col-sm-12 col-xs-12 ">
                       <label class="control-label" style="font-size:small"><span style="color: red"></span>Remarks</label><br />
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right"  title=" Remarks" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id ="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                        </div> 
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                                CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click">
                                                       Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click" >
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                            <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click"  />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut"  OnClick="btnReject_Click" />
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
                                        </asp:GridView></div>
                                    </div>
                                    <div id="dispatch" runat="server"> 
                                     <div class="clearfix"></div>
                                      <p>&nbsp;</p>
                                         <div class="x_title">
                                    <h2>Daily Dispatches</h2>
                                    <div class="clearfix"></div>
                               </div>
                                         <asp:GridView ID="grdDispatchvat" runat="server" Visible="true"   AutoGenerateColumns="false" PageSize="10" AllowPaging="true"  EmptyDataText="No Records" OnPageIndexChanging="grdDispatchvat_PageIndexChanging" 
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                      <Columns>
                                                            <asp:TemplateField HeaderText="Dispatch Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblreceiptDate" runat="server" Visible="true" Text='<%#Eval("receipt_date") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Party Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartyName" runat="server" Visible="true" Text='<%#Eval("party_name") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpartycode" runat="server" Visible="true" Text='<%#Eval("party_code") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="dispatchvat"  ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldispatchvat" runat="server" Visible="true" Text='<%#Eval("to_dispatchvat") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldispatch" runat="server" Visible="true" Text='<%#Eval("dispatchvat") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Dispatch Qty" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBL" runat="server" Visible="true" Text='<%#Eval("total_bl_receipt") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         <%--    <asp:TemplateField HeaderText="LP Qty " ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLP" runat="server" Visible="true" Text='<%#Eval("total_lp_receipt") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <%-- <asp:TemplateField HeaderText="form84id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblform84Did" runat="server" Visible="true" Text='<%#Eval("storage_dispatch_id") %>'  ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                   <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Issued") %>'></asp:Label>
                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                   </Columns>
                                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                                <PagerStyle BackColor="#26B8B8" HorizontalAlign="Right" ForeColor="#ECF0F1" />

                                                                <RowStyle BackColor="Window"></RowStyle>
                                                             </asp:GridView>

                                        <asp:GridView ID="Vat" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server"  AutoGenerateColumns="false">
                                       <Columns>
                                                            <asp:TemplateField HeaderText="Closure Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblclosureDate" runat="server" Visible="true" Text='<%#Eval("closure_date") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Party Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartyName" runat="server" Visible="true" Text='<%#Eval("party_name") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpartycode" runat="server" Visible="true" Text='<%#Eval("party_code") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="dispatchvat"  ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldispatchvat" runat="server" Visible="true" Text='<%#Eval("from_dispatchvat") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch Vat" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldispatch" runat="server" Visible="true" Text='<%#Eval("vat_name") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="dailydispatchclosure_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDDLid" runat="server" Visible="true" Text='<%#Eval("dailydispatchclosure_id") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Draft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" >
                                                                <ItemTemplate>
                                                                        <asp:LinkButton Text="View" id="btnView"  CssClass="myButton"  runat="server"   CommandName="View" ><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                   <asp:LinkButton Text="Edit" id="btnEdit"  CssClass="myButton1"   runat="server"  Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>'   CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                </ItemTemplate>
                                                                  <ItemStyle Width="10px" />
                                                            </asp:TemplateField>
                                                   </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
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
