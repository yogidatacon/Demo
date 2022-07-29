<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="VATTransfersFroms.aspx.cs" Inherits="UserMgmt.VATTransfersFroms" %>

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
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddVatType.ClientID%>').value == 'Select') {
                                            alert("Select VAT Type");
                                            document.getElementById("<% =ddVatType.ClientID%>").focus();
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
                                            alert("Enter Strength");
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
                                </script>
                                <script>
                                    function LoadVatNames() {

                                        var vat_type = $('#BodyContent_ddVatType').val();
                                        var jsondata = JSON.stringify($('#BodyContent_ddVatType').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "VATTransfersFroms.aspx/LoadVatNames",
                                            data: '{vattype:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (data) {

                                                $('#BodyContent_ddlFromVATName').find('option').remove();
                                                $('#BodyContent_ddlToVATName').find('option').remove();
                                                $.each(data.d, function (index, value) {

                                                    if (index == 0) {
                                                        $('#BodyContent_ddlFromVATName').append($('<option></option>').val("Select").html("Select"));
                                                        $('#BodyContent_ddlToVATName').append($('<option></option>').val("Select").html("Select"));
                                                    }
                                                    $('#BodyContent_ddlFromVATName').append($('<option></option>').val(value.vat_code).html(value.vat_name));
                                                    $('#BodyContent_ddlToVATName').append($('<option></option>').val(value.vat_code).html(value.vat_name));
                                                })
                                            }
                                        });
                                    }
                                    function GetAvailableQTY() {
                                        debugger;
                                        var strnth = $('#BodyContent_txtStrength').val();
                                        if ($('#BodyContent_txtStrength').val() == "" && $('#BodyContent_ddlFromVATName').val() != null) {
                                            alert("Please Enter Strenth");
                                            $('#BodyContent_ddlFromVATName').val("Select");
                                        }
                                        else {
                                           
                                            $('#BodyContent_ddlToVATName').find('option').remove();
                                            var options = $("#BodyContent_ddlFromVATName").html();
                                            $('#BodyContent_ddlToVATName').html(options);
                                            var vatcode = $('#BodyContent_ddlFromVATName').val();
                                            var x = document.getElementById("BodyContent_ddlFromVATName");
                                            var y = document.getElementById("BodyContent_ddlToVATName");
                                            y.remove(x.selectedIndex);
                                           
                                            if ($('#BodyContent_ddlFromVATName').val() != "Select" || $('#BodyContent_ddlFromVATName').val() != null) {

                                                var jsondata = JSON.stringify($('#BodyContent_ddlFromVATName').val());
                                                $.ajax({
                                                    type: "POST",
                                                    //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                                    url: "VATTransfersFroms.aspx/GetAvailableQTY",
                                                    data: '{vatcode:' + jsondata + '}',
                                                    datatype: "application/json",
                                                    contentType: "application/json; charset=utf-8",
                                                    cache: false,
                                                    async: false,
                                                    success: function (msg) {
                                                        var total = parseFloat(msg.d);

                                                        $('#BodyContent_txtAvailableQtyBL').val(total.toFixed(2));
                                                        var strnth1 = $('#BodyContent_txtStrength').val();
                                                        var lpl11 = (total * (1 + parseFloat(strnth1) / 100));
                                                        $('#BodyContent_txtAvailableQtyLPL').val(lpl11.toFixed(2));
                                                        $('#BodyContent_AvailableQtyBL').val(total.toFixed(2));
                                                        $('#BodyContent_AvailableQtyLPL').val(lpl11.toFixed(2));
                                                        $('#BodyContent_FromVATName').val($('#BodyContent_ddlFromVATName').val());

                                                        $('#BodyContent_ddlToVATName').removeData($('#BodyContent_FromVATName').val());
                                                    }
                                                });
                                            }
                                        }
                                    }

                                    function GetLPLValue() {
                                        debugger;
                                        var total = parseFloat($('#BodyContent_txtTransferQTY').val()).toFixed(2);
                                        var avialble = parseFloat($('#BodyContent_txtAvailableQtyBL').val()).toFixed(2);
                                        if (parseFloat(avialble) < parseFloat(total)) {
                                            alert("Transfer qty cannot be more than available qty");
                                            $('#BodyContent_txtTransferQTY').val("");
                                            $('#BodyContent_TransferQTY').val("");
                                            $('#BodyContent_txtTransferQTYLPL').val("")
                                            $('#BodyContent_TransferQTYLPL').val("")
                                            $('#BodyContent_txtTransferQTY').focus();

                                        }
                                        else {

                                            var strnth1 = $('#BodyContent_txtStrength').val();
                                            var lpl11 = ($('#BodyContent_txtTransferQTY').val() * (1 + parseFloat(strnth1) / 100));
                                            $('#BodyContent_txtTransferQTYLPL').val(lpl11.toFixed(2))
                                            $('#BodyContent_TransferQTYLPL').val(lpl11.toFixed(2))
                                            $('#BodyContent_TransferQTY').val(parseFloat($('#BodyContent_txtTransferQTY').val()).toFixed(2));
                                            $('#BodyContent_ToVATName').val($('#BodyContent_ddlToVATName').val());
                                        }
                                    }
                                </script>
                              
                                <script>
                                    $(document).ready(function () {

                                        LoadVatNames();
                                        if ($('#BodyContent_ddlFromVATName').val() == null || $('#BodyContent_ddlFromVATName').val() == "Select") {
                                            $('#BodyContent_ddlFromVATName').val($('#BodyContent_FromVATName').val());
                                        }
                                        if ($('#BodyContent_ddlToVATName').val() == null || $('#BodyContent_ddlToVATName').val() == "Select") {
                                            $('#BodyContent_ddlToVATName').val($('#BodyContent_ToVATName').val());
                                        }
                                        if ($('#BodyContent_txtAvailableQtyBL').val() == "") {
                                            $('#BodyContent_txtAvailableQtyBL').val($('#BodyContent_AvailableQtyBL').val());
                                        }
                                        if ($('#BodyContent_txtAvailableQtyLPL').val() == "") {
                                            $('#BodyContent_txtAvailableQtyLPL').val($('#BodyContent_AvailableQtyLPL').val());
                                        }
                                        if ($('#BodyContent_txtTransferQTY').val() == "") {
                                            $('#BodyContent_txtTransferQTY').val($('#BodyContent_TransferQTY').val());
                                        }
                                        if ($('#BodyContent_txtTransferQTYLPL').val() == "") {
                                            $('#BodyContent_txtTransferQTYLPL').val($('#BodyContent_TransferQTYLPL').val());
                                        }
                                        if ($('#BodyContent_txtStrength').val() != "") {
                                            GetAvailableQTY();
                                            GetLPLValue();
                                        }
                                        if ($('#BodyContent_txtTransferQTY').val() == "NaN") {
                                            $('#BodyContent_txtTransferQTY').val("");
                                        }
                                        if ($('#BodyContent_txtTransferQTYLPL').val() == "NaN") {
                                            $('#BodyContent_txtTransferQTYLPL').val("");
                                        }
                                        if ($('#BodyContent_txtAvailableQtyBL').val() == "NaN") {
                                            $('#BodyContent_txtAvailableQtyBL').val("");
                                        }
                                        if ($('#BodyContent_txtAvailableQtyLPL').val() == "NaN") {
                                            $('#BodyContent_txtAvailableQtyLPL').val("");
                                        }
                                    });
                                </script>

                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">

                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                       <%-- <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>--%>

                                         <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Raw Material To Fermenter</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li>
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
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage</span></asp:LinkButton></li>
                                        <li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>

                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="row">
                                    <div class="x_title">
                                        <h2>District Form</h2>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="x_content">
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">* </span>VAT Type</label>
                                            <br />
                                            <asp:DropDownList ID="ddVatType" runat="server" data-toggle="tooltip" data-placement="right" title="VAT Type" onchange="LoadVatNames();" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Dips in Wet CM</label><br />
                                            <asp:TextBox ID="txtDipsinWetInches" runat="server" data-toggle="tooltip" data-placement="right" title="Dips in Wet CM" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Temperature</label><br />
                                            <asp:TextBox ID="txtTemperature" runat="server" data-toggle="tooltip" data-placement="right" title="Temperature" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Indication</label><br />

                                            <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Indication"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Strength</label><br />
                                            <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Strength"></asp:TextBox>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">* </span>From VAT Name</label><br />
                                            <asp:DropDownList ID="ddlFromVATName" runat="server" data-toggle="tooltip" data-placement="right" title="From VAT Name" onchange="GetAvailableQTY();" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="FromVATName" runat="server" />
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Available Qty (BL)</label><br />
                                            <asp:TextBox ID="txtAvailableQtyBL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(BL)"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="AvailableQtyBL" runat="server" />
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Available Qty (LP)</label><br />
                                            <asp:TextBox ID="txtAvailableQtyLPL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="AvailableQtyLPL" runat="server" />
                                         <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-2 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">* </span>To VAT Name</label><br />
                                            <asp:DropDownList ID="ddlToVATName" runat="server" data-toggle="tooltip" data-placement="right" title="From VAT Name"  CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="ToVATName" runat="server" />
                                        </div>


                                       
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Transfer Qty (BL)</label><br />
                                            <asp:TextBox ID="txtTransferQTY" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Transfer Qty (BL)" onchange="GetLPLValue()"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="TransferQTY" runat="server" />
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Transfer Qty (LP)</label><br />
                                            <asp:TextBox ID="txtTransferQTYLPL" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Transfer Qty(LPL)"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="TransferQTYLPL" runat="server" />
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                            <<asp:TextBox TextMode="MultiLine" ID="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" Height="50px" Width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>

                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <asp:HiddenField ID="txtid" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                            <asp:Button ID="btnSave1" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave1_Click" />
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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

