<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RMR_GrainBased.aspx.cs" Inherits="UserMgmt.RMR_GrainBased" %>

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
                                <title>Raw Material Receipt</title>
                                <script type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter  Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlRawMaterial.ClientID%>').value == 'Select') {
                                            alert("Select Rawmaterial");
                                            document.getElementById("<% =ddlRawMaterial.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlSupplierType.ClientID%>').value == 'Select') {
                                            alert("Select Supplier Type");
                                            document.getElementById("<% =ddlSupplierType.ClientID%>").focus();
                                              return false;

                                          }
                                          if (document.getElementById('<%=txtReceiptNo.ClientID%>').value == '') {
                                            alert("Enter Receipt No");
                                            document.getElementById("<% =txtReceiptNo.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtreceiptdate.ClientID%>').value == '') {
                                            alert("Enter Receipt Date");
                                            document.getElementById("<% =txtreceiptdate.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtreceiptqty.ClientID%>').value == '') {
                                            alert("Enter Receipt QTY");
                                            document.getElementById("<% =txtreceiptqty.ClientID%>").focus();
                                             return false;

                                         }
                                         if (document.getElementById('<%=ddlUom.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                            document.getElementById("<% =ddlUom.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                            alert("Enter Vehicle No");
                                            document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                               return false;

                                           }

                                    }

                                      function validationMsg2() {
                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter  Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlRawMaterial.ClientID%>').value == 'Select') {
                                            alert("Select Rawmaterial");
                                            document.getElementById("<% =ddlRawMaterial.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlSupplierType.ClientID%>').value == 'Select') {
                                            alert("Select Supplier Type");
                                            document.getElementById("<% =ddlSupplierType.ClientID%>").focus();
                                              return false;

                                          }
                                          if (document.getElementById('<%=txtReceiptNo.ClientID%>').value == '') {
                                            alert("Enter Receipt No");
                                            document.getElementById("<% =txtReceiptNo.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtreceiptdate.ClientID%>').value == '') {
                                            alert("Enter Receipt Date");
                                            document.getElementById("<% =txtreceiptdate.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtreceiptqty.ClientID%>').value == '') {
                                            alert("Enter Receipt QTY");
                                            document.getElementById("<% =txtreceiptqty.ClientID%>").focus();
                                             return false;

                                         }
                                         if (document.getElementById('<%=ddlUom.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                            document.getElementById("<% =ddlUom.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                            alert("Enter Vehicle No");
                                            document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                            return false;

                                        }
                                      

                                       }
                                </script>

                                <script language="javascript" type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%= txtapproverremarks.ClientID%>').value == '') {
                                            alert("Enter  Approval Comments ");
                                            return false;
                                            document.getElementById("<% =txtapproverremarks.ClientID%>").focus();
                                        }
                                    }
                                </script>
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


                                <script type="text/javascript">
                                    var hd = 0;
                        function Calcutate() {
                                        debugger;;
                                        var total = 0;

                                        var gv = document.getElementById("<%= grdrawmaterial.ClientID %>");
                                        var tb = gv.getElementsByTagName("input");
                                        var sub = 0;
                                        var total = 0;
                                        var indexQ = 1;
                                        var indexP = 0;

                                        for (var i = 0; i < tb.length; i++) {
                                            if (tb[i].type == "text") {
                                                sub = parseFloat(tb[i].value);
                                                if (isNaN(sub)) {
                                                    // lb[i + indexQ].innerHTML = "";
                                                    sub = 0;
                                                }

                                                total += parseFloat(sub);

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
                                                i++;
                                            }
                                        }
                                        debugger;;
                                        $('#BodyContent_grdrawmaterial_lblTotal').text(total);
                                    }
                                 <%--  function Calcutate() {
                                        debugger;
                                        var total = 0;
                                      
                                        var gv = document.getElementById("<%= grdRawMaterial.ClientID %>");
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
                                               
                                                var NetWeight = parseFloat($('#BodyContent_NetWeight').val()).toFixed(2);
                                                if (NetWeight < total)
                                                {
                                                    total -= parseFloat(sub);
                                                    alert("Qty not Matched with Net Weight");
                                                    $('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                                    tb[i].value = "";
                                                    tb[i].focus();
                                                    return false;
                                                }
                                                i++;
                                            }
                                        }
                                        debugger;
                                        $('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                       
                                    }--%>
                                </script>
                                <script type="text/javascript">
                                    function SelectDate3(e) {
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
                                        $('#BodyContent_txtgpd').val(todayDate);
                                    }

                                    function SelectRemovalDate(e) {
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
                                        $('#BodyContent_txtreceiptdate').val(todayDate);
                                        $('#BodyContent_receipt').val(todayDate);
                                    }

                                </script>

                                <script>
                                    $(document).ready(function () {
                                        Calcutate();
                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtgpd').val());
                                        }
                                        debugger;
                                        if ($('#BodyContent_txtRecieptDate').val() == "") {
                                            $('#BodyContent_txtRecieptDate').val($('#BodyContent_receipt').val());
                                        }
                                        //if ($('#BodyContent_txtNetWeight').val() == "" || $('#BodyContent_txtNetWeight').val() == "0") {
                                        //    $('#BodyContent_txtTransitWastage').val("");
                                        //}
                                    });
                                </script>

                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>

                                        <%--<li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>--%>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li>
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
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="lnkShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>

                                <div runat="server" id="ena" class="x_title">
                                    <h2>Raw Material Receipt Form (Other Than Molasses)</h2>
                                    <div class="clearfix"></div>
                                </div>
                                 <div id="dis" runat="server" class="x_title">
                                <h2>Raw Material Receipt List</h2>
                                <div class="clearfix"></div>
                            </div>
                                <div class="x_content">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span> Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate3" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" data-toggle="tooltip" data-placement="right" title="Date" class="form-control" AutoComplete="off" ReadOnly="true" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtgpd" runat="server" />

                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Raw Material</label><br />
                                        <asp:DropDownList ID="ddlRawMaterial" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Raw Material" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRawMaterial_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Supplier Type </label>
                                        <br />
                                        <asp:DropDownList ID="ddlSupplierType" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Supplier Type" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Sugar Mill" Value="Sugar Mill"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Others" Value="Others"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div id="ddp" runat="server" visible="false" class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Supplier Name</label><br />
                                        <asp:DropDownList ID="ddlSupplierName" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Supplier Name" Width="60%" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </div>
                                    <div id="txtp" runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Supplier Name</label><br />
                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title=" Supplier Name" ></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Receipt No</label><br />
                                        <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Receipt No" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Receipt Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtreceiptdate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectRemovalDate" ID="CalendarExtender3"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtreceiptdate" data-toggle="tooltip" data-placement="right" title="Receipt Date" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="receipt" runat="server" />

                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Receipt Qty</label><br />

                                        <asp:TextBox ID="txtreceiptqty" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Receipt Qty" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                                    </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>UOM</label><br />
                                        <asp:DropDownList ID="ddlUom" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="UOM"  ></asp:DropDownList>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Vehicle No( In case of multiple vehicles please mention "NA")</label><br />

                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Vehicle No"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   <div style="height: 8%; background-color: #26b8b8;">
                                        <span style="font-size: small; color: white; margin-left: 40%">Raw Material Stored in VAT</span>
                                    </div>
                                    &nbsp;
                                    <div class="clearfix"></div>
                                    <div>
                                        <asp:GridView ID="grdrawmaterial" runat="server" AutoGenerateColumns="false" Width="100%"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" ShowFooter="true" >
                                             <Columns>
                                                         <asp:TemplateField HeaderText=" Vat code" ItemStyle-Font-Bold="true" ItemStyle-Width="1px" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVatCode" runat="server" Visible="false" Text='<%#Eval("vat_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="storageid" ItemStyle-Font-Bold="true" ItemStyle-Width="1px" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstorageid" runat="server" Visible="false" Text='<%#Eval("rmstorageid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Vat Name" ItemStyle-Font-Bold="true" ItemStyle-Width="65%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVatName" runat="server" Visible="true" Text='<%#Eval("vat_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label Text="Total"  style="text-align:right" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" /></div>

                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" style="text-align:right" onchange="Calcutate()" class="calculate" runat="server" CssClass="form-control"  Text='<%#Eval("storedqty") %>' onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <div>
                                                            <asp:Label ID="lblTotal" ItemStyle-Font-Bold="true"  Font-Bold="true" Width="100px" runat="server"/></div>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dips in CM" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdips" runat="server" style="text-align:right" CssClass="form-control" Text='<%#Eval("Opening_dips") %>' onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                            <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" />
                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" runat="server" class="form-control" name="size" data-toggle="tooltip" data-placement="right" title="Remarks"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Approver Comments</label><br />
                                        <textarea type="text" id="txtapproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                                CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveAs_Click">
                                                       Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                             <asp:LinkButton ID="btnupdate" runat="server" Visible="false" OnClientClick="javascript:return validationMsg2();" CssClass="btn btn-primary" OnClick="btnupdate_Click"><span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        </div>
                                    </div>
                                       <p>&nbsp;</p>
                                    <div id="approverid" runat="server">
                                       
                                        <div  class="x_title">
                                            <h4>Approval Summary</h4>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div>
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
