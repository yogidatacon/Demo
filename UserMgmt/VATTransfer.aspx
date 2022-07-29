<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="VATTransfer.aspx.cs" Inherits="UserMgmt.VATTransfer"  %>
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
                                <title>VAT Transfer </title>
                                  <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <script type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddVatType.ClientID%>').value == 'Select') {
                                            alert("Select VAT Type");
                                            document.getElementById("<% =ddVatType.ClientID%>").focus();
                                            return false;

                                        } 
                                          if (document.getElementById('<%=txtTransferDate.ClientID%>').value == '') {
                                              alert("Enter vat transfer date");
                                            document.getElementById("<% =txtTransferDate.ClientID%>").focus();
                                            return false;

                                        }
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
                                            alert("Please enter a strength value");
                                            document.getElementById("<% =txtStrength.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlFromVATName.ClientID%>').value == 'Select') {
                                            alert("Select From VAT Name");
                                            document.getElementById("<% =ddlFromVATName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlToVATName.ClientID%>').value == 'Select') {
                                            alert("Select To VAT Name");
                                            document.getElementById("<% =ddlToVATName.ClientID%>").focus();
                                            return false;

                                        }

                                    }
                                    function validationMsg1() {

                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver remarks");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
 <script>
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
                                        $('#BodyContent_txtTransferDate').val(todayDate);
                                        $('#BodyContent_txttrdate').val(todayDate);
                                    }
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
                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                       
                                        if ($('#BodyContent_txtTransferDate').val() == "") {
                                            $('#BodyContent_txtTransferDate').val($('#BodyContent_txttrdate').val());
                                        }
                                    });
                                    </script>

                            </head>
                            <body>
                               <div runat="server" id="SCM">
                                    <ul class="nav nav-tabs" id="sgr" runat="server">
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                     <ul class="nav nav-tabs" id="dst" runat="server">
                                       <li >
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>

                                       <%-- <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>--%>

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
                                        <span style="color: #fff; font-size: 14px;">From Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage & Adjustment</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>

                                    </ul>
                                    <br />
                                </div>
                                 <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="row">
                                    <div class="x_title">
                                        <h2>VAT Transfer  Form</h2>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="x_content">
                                         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">* </span>VAT Type</label>
                                            <br />
                                            <asp:DropDownList ID="ddVatType" runat="server" data-toggle="tooltip" AutoPostBack="true" OnSelectedIndexChanged="ddVatType_SelectedIndexChanged" data-placement="right" title="VAT Type"  CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Transfer Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtTransferDate" Format="dd-MM-yyyy"  OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtTransferDate"  data-toggle="tooltip" data-placement="right" title="Date Reserved for Form 84D" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txttrdate" runat="server" />
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Dips in Wet CM</label><br />
                                            <asp:TextBox ID="txtDipsinWetInches" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Dips in Wet CM" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Temperature</label><br />
                                            <asp:TextBox ID="txtTemperature" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Temperature" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Indication</label><br />

                                            <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Indication"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Strength</label><br />
                                            <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Strength"></asp:TextBox>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">* </span>From VAT Name</label><br />
                                            <asp:DropDownList ID="ddlFromVATName" runat="server" data-toggle="tooltip" data-placement="right" title="From VAT Name" OnSelectedIndexChanged="ddlFromVATName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                          <%--  <asp:HiddenField ID="FromVATName1" runat="server" />--%>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <%--<label class="control-label" style="display: inline"><span style="color: red"></span>Available Qty (BL)</label>--%><asp:Label ID="AvalBL" runat="server" CssClass="control-label" Font-Bold="true" style="display: inline" ></asp:Label><br />
                                            <asp:TextBox ID="txtAvailableQtyBL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(BL)" ></asp:TextBox>
                                        </div>
                                       <%-- <asp:HiddenField ID="AvailableQtyBL1" runat="server" />--%>
                                        <div runat="server" id="ALP" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Available Qty (LP)</label><br />
                                            <asp:TextBox ID="txtAvailableQtyLPL1" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)"></asp:TextBox>
                                        </div>
                                       <%-- <asp:HiddenField ID="AvailableQtyLPL" runat="server" />--%>
                                         <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">* </span>To VAT Name</label><br />
                                            <asp:DropDownList ID="ddlToVATName" runat="server" data-toggle="tooltip" data-placement="right" title="From VAT Name"  CssClass="form-control">
                                            </asp:DropDownList>
                                         <%--  // <asp:HiddenField ID="ToVATName1" runat="server" />--%>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <%--<label class="control-label" style="display: inline"><span style="color: red"></span>Transfer Qty (BL)</label><br />--%>
                                            <asp:Label ID="TBL" runat="server" Font-Bold="true" CssClass="control-label" style="display: inline"></asp:Label><br />
                                            <asp:TextBox ID="txtTransferQTY" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Transfer Qty (BL)" AutoPostBack="true" OnTextChanged="txtTransferQTY_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="TransferQTY1" runat="server" />
                                        <div runat="server" id="TLP" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                          <label class="control-label" style="display: inline"><span style="color: red"></span>Transfer Qty (LP)</label><br />
                                             
                                            <asp:TextBox ID="txtTransferQTYLPL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Transfer Qty(LPL)"></asp:TextBox>
                                        </div>
                                      <%--//  <asp:HiddenField ID="TransferQTYLPL1" runat="server" />--%>

                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-10 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Remarks</label><br />
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Remarks" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12">
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                            <<asp:TextBox TextMode="MultiLine" ID="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" Height="50px" Width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>

                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <asp:HiddenField ID="txtid1" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft"  runat="server" Text="Save as Draft"  OnClientClick="javascript:return validationMsg()"  class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click"/>
                                            <asp:LinkButton ID="btnSave1" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave1_Click" />
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                        </div>
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


