<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RawmaterialWastageForm.aspx.cs" Inherits="UserMgmt.RawmaterialWastageForm" %>


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
                                <title>Raw Material Wastage & Adjustment</title>
                               <script type="text/javascript">
                                    
                                     function validationMsg1() {

                                         if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Remarks Name");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                     }
                                        </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;

                                        }
                                        <%-- if (document.getElementById('<%=txtSetuptime.ClientID%>').value == '') {
                                             alert("Enter Setuptime");
                                            document.getElementById("<% =txtSetuptime.ClientID%>").focus();
                                            return false;

                                        }--%>
                                    if (document.getElementById('<%=ddlFermenter.ClientID%>').value == 'Select') {
                                        alert("Select Vat");
                                            document.getElementById("<% =ddlFermenter.ClientID%>").focus();
                                            return false;

                                        }
                                       <%-- if (document.getElementById('<%=txtTransitWastage.ClientID%>').value == '') {
                                            alert("Enter Transit Wastage");
                                            document.getElementById("<% =txtTransitWastage.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtStorageWastage.ClientID%>').value == '') {
                                             alert("Enter Storage Wastage");
                                            document.getElementById("<% =txtStorageWastage.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtHandlingWastage.ClientID%>').value == '') {
                                            alert("Enter Handling Wastage");
                                            document.getElementById("<% =txtHandlingWastage.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remark");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                            return false;

                                        }

                                    }

                                      function validationMsg2() {

                                       <%-- if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;

                                        }
                                        <%-- if (document.getElementById('<%=txtSetuptime.ClientID%>').value == '') {
                                             alert("Enter Setuptime");
                                            document.getElementById("<% =txtSetuptime.ClientID%>").focus();
                                            return false;

                                        }
                                    if (document.getElementById('<%=ddlFermenter.ClientID%>').value == 'Select') {
                                        alert("Select Vat");
                                            document.getElementById("<% =ddlFermenter.ClientID%>").focus();
                                            return false;

                                        }
                                       --%>
                                      <%--  if (document.getElementById('<%=txtTransitWastage.ClientID%>').value == '') {
                                            alert("Enter Transit Wastage");
                                            document.getElementById("<% =txtTransitWastage.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtStorageWastage.ClientID%>').value == '') {
                                             alert("Enter Storage Wastage");
                                            document.getElementById("<% =txtStorageWastage.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtHandlingWastage.ClientID%>').value == '') {
                                            alert("Enter Handling Wastage");
                                            document.getElementById("<% =txtHandlingWastage.ClientID%>").focus();
                                            return false;

                                        }

                                           if (document.getElementById('<%=txtIncreaseInOperation.ClientID%>').value == '') {
                                               alert("Enter Increase In Operation");
                                            document.getElementById("<% =txtIncreaseInOperation.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if (document.getElementById('<%=txtDecreaseInWastage.ClientID%>').value == '') {
                                            alert("Enter Decrease In Wastage");
                                            document.getElementById("<% =txtDecreaseInWastage.ClientID%>").focus();
                                            return false;

                                        }--%>
                                      <%--  if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remark");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                            return false;

                                        }--%>

                                    }
                                </script>
                              
                                <%-- <script> $(document).ready(function () {
                                        debugger;
                                     if ($('#BodyContent_txtDATE').val() == "") {
                                         $('#BodyContent_txtDATE').val($('#BodyContent_txtdob1').val());
                                     }
                                            });
                                     </script>--%>

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
                                            if ((txtlen - dotpos) > 3)
                                                return false;
                                        }
                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;
                                        return true;
                                    }


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
                                     
                                 <%--   function Calcutate() {

                                        var total = 0;
                                      
                                        var gv = document.getElementById("<%= gridToStore.ClientID %>");
                                        var tb = gv.getElementsByTagName("input");
                                        var sub = 0;
                                        var total = 0;
                                        var indexQ = 1;
                                        var indexP = 0;

                                        for (var i = 0; i < tb.length; i++)
                                        {
                                            if (tb[i].type == "text" )
                                            {
                                                sub = parseFloat(tb[i].value);
                                                if (isNaN(sub)) {
                                                    // lb[i + indexQ].innerHTML = "";
                                                    sub = 0;
                                                }
                                               
                                                total += parseFloat(sub);
                                                debugger;
                                                //var NetWeight = parseFloat($('#BodyContent_NetWeight').val()).toFixed(2);
                                                //if (NetWeight < total)
                                                //{
                                                //    total -= parseFloat(sub);
                                                //    alert("Qty not Matched with Net Weight");
                                                //    $('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                                //    tb[i].value = "";
                                                //    tb[i].focus();
                                                //    return false;
                                                //}
                                                //i++;
                                            }
                                        }

                                        $('#BodyContent_gridToStore_lblTotal').text(total);
                                       
                                    }--%>

                                </script>

                                 <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob1').val());
                                        }
                                    });
                                    </script>
                               
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">

                                          <li >
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click"  >
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
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage & Adjustment</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" Text="Opening Balance" OnClick="lnkOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>

                                    </ul>
                                    <br />
                                </div>

                                
                                 <a>
                                   <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Raw Material Wastage & Adjustment</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>  
                                     <asp:UpdatePanel runat="server">
                                        <ContentTemplate></ContentTemplate>
                                    </asp:UpdatePanel>
                                               
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE"  OnClientDateSelectionChanged="SelectSetupDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                          <script type="text/javascript">
                                                    function SelectSetupDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDATE').val();
                                                        $('#BodyContent_txtdob1').val(dat1e);
                                                    }
                                                </script>
                                        <asp:TextBox ID="txtDATE"  data-toggle="tooltip" data-placement="right" title="Date" ReadOnly="true" CssClass="form-control validate[required]"  AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob1" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span> VAT </label>
                                        <br />
                                        <asp:DropDownList ID="ddlFermenter" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" VAT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFermenter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Transit Wastage </label>
                                        <br />
                                        <asp:TextBox ID="txtTransitWastage" data-toggle="tooltip" data-placement="right" title="Transit Wastage" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control" Style="text-align: left" runat="server"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Storage Wastage </label>
                                        <br />
                                        <asp:TextBox ID="txtStorageWastage" data-toggle="tooltip" data-placement="right" title="Storage Wastage" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control" Style="text-align: left" runat="server"></asp:TextBox>
                                    </div>
                                                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Handling Wastage</label>
                                        <br />
                                        <asp:TextBox ID="txtHandlingWastage" data-toggle="tooltip" data-placement="right" title="Handling Wastage" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control" Style="text-align: left" runat="server"></asp:TextBox>
                                    </div>
                                     <%--<div class="clearfix"></div>
                                    <p>&nbsp;</p>--%>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Increase In Operation <%--(BL)--%></label><br />
                                        <asp:TextBox ID="txtIncreaseInOperation" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Increase In Operation(BL)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Decrease In Wastage <%--(BL)--%></label><br />
                                        <asp:TextBox ID="txtDecreaseInWastage" onchange="GetBalance()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoComplete="off" title=" Decrease  In Wastage(BL)" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                  <%--  <div class="clearfix"></div>
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
                                    </div>--%>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                             <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                         <asp:HiddenField ID="party_code" runat="server" />
                                        <asp:LinkButton ID="btnSaveAsDraft" runat="server"
                                            CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click">
                                                        Save as Draft</asp:LinkButton>
                                          <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true"> </span>Submit</asp:LinkButton>
                                           <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        <asp:LinkButton ID="btnupdate" runat="server" Visible="false" OnClientClick="javascript:return validationMsg2();" CssClass="btn btn-primary" OnClick="btnupdate_Click"><span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                         <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
