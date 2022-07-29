<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RawmaterialReceiptForm.aspx.cs" Inherits="UserMgmt.RawmaterialReceiptForm" %>

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
                                 <script language="javascript" type="text/javascript">
                                     function validationMsg() {
                                         debugger;
                                        if (document.getElementById('<%=txtdob.ClientID%>').value == '') {
                                            alert("Select  Date");
                                             document.getElementById("<% =txtdob.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                            
                                        if (document.getElementById('<%=txtGrossWeight.ClientID%>').value == '') {
                                            alert("Enter  GrossWeight");
                                            document.getElementById("<%=txtGrossWeight.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=txtTankerWeight.ClientID%>').value == '') {
                                              alert("Enter TankerWeight");
                                            document.getElementById("<%=txtTankerWeight.ClientID%>").focus();
                                            return false;
                                          }
                                        debugger;
                                        var netweight =parseFloat( parseFloat($('#BodyContent_txtNetWeight').val()) + parseFloat($('#BodyContent_txtTransitWastage').val())).toFixed(2);
                                        var supplierweight = parseFloat($('#BodyContent_txtOtyDispatch').val()).toFixed(2);
                                        if (netweight != supplierweight)
                                        {
                                            alert("Net weight can be lesser or equal to Dispatch Qty")
                                            //  $('#BodyContent_txtGrossWeight').val("");
                                            document.getElementById("<%=txtGrossWeight.ClientID%>").focus();
                                            return false;

                                        }
                                      
                                        var NetWeight = parseFloat($('#BodyContent_txtNetWeight').val()).toFixed(2);
                                        var total = parseFloat($('#BodyContent_grdRawMaterial_lblTotal').text()).toFixed(2);
                                        $('#BodyContent_gridtotal').val(total);
                                        if (NetWeight != total)
                                        {
                                            alert("Vat wise Total Qty Not Matched With Dispatch Qty")
                                            $('#BodyContent_gridtotal').val("");
                                            $('#BodyContent_grdRawMaterial_lblTotal').focus();
                                            return false;
                                        }
                                       
                                         if (document.getElementById('<%=gridtotal.ClientID%>').value == '') {
                                             alert("Vat wise Total Qty Not Matched With Dispatch Qty");
                                            document.getElementById("<%=gridtotal.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                             alert("Enter Remarks");
                                            document.getElementById("<%=txtRemarks.ClientID%>").focus();
                                            return false;
                                          }
                                       
                                    }

                                      function validationMsg2() {
                                        if (document.getElementById('<%=txtdob.ClientID%>').value == '') {
                                            alert("Select  Date");
                                             document.getElementById("<% =txtdob.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                            
                                        if (document.getElementById('<%=txtGrossWeight.ClientID%>').value == '') {
                                            alert("Enter  GrossWeight");
                                            document.getElementById("<%=txtGrossWeight.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=txtTankerWeight.ClientID%>').value == '') {
                                              alert("Enter TankerWeight");
                                            document.getElementById("<%=txtTankerWeight.ClientID%>").focus();
                                            return false;
                                          }
                                        debugger;
                                        var netweight =parseFloat( parseFloat($('#BodyContent_txtNetWeight').val()) + parseFloat($('#BodyContent_txtTransitWastage').val())).toFixed(2);
                                        var supplierweight = parseFloat($('#BodyContent_txtOtyDispatch').val()).toFixed(2);
                                        if (netweight != supplierweight)
                                        {
                                            alert("Net weight can be lesser or equal to Dispatch Qty")
                                            //  $('#BodyContent_txtGrossWeight').val("");
                                            document.getElementById("<%=txtGrossWeight.ClientID%>").focus();
                                            return false;

                                        }
                                      
                                        var NetWeight = parseFloat($('#BodyContent_txtNetWeight').val()).toFixed(2);
                                        var total = parseFloat($('#BodyContent_grdRawMaterial_lblTotal').text()).toFixed(2);
                                        $('#BodyContent_gridtotal').val(total);
                                        if (NetWeight != total)
                                        {
                                            alert("Vat wise Total Qty Not Matched With Dispatch Qty")
                                            $('#BodyContent_gridtotal').val("");
                                            $('#BodyContent_grdRawMaterial_lblTotal').focus();
                                            return false;
                                        }
                                       
                                         if (document.getElementById('<%=gridtotal.ClientID%>').value == '') {
                                             alert("Vat wise Total Qty Not Matched With Dispatch Qty");
                                            document.getElementById("<%=gridtotal.ClientID%>").focus();
                                            return false;
                                         }
                                       
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%= txtApproverremarks.ClientID%>').value == '')
                                        {
                                            alert("Enter  Approval Comments ");
                                            return false;
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
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
                                    function Calcutate() {
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
                                       
                                    }
                                    function GetValues()
                                    {
                                        debugger;
                                        var grosswait = parseFloat($('#BodyContent_txtGrossWeight').val()).toFixed(2);
                                        var tankerweight = parseFloat($('#BodyContent_txtTankerWeight').val()).toFixed(2);
                                        $('#BodyContent_txtNetWeight').val(parseFloat(parseFloat(grosswait) - parseFloat(tankerweight)).toFixed(2));
                                        $('#BodyContent_NetWeight').val(parseFloat(parseFloat(grosswait) - parseFloat(tankerweight)).toFixed(2));
                                       
                                        var netweight = parseFloat($('#BodyContent_txtNetWeight').val()).toFixed(2);
                                        var supplierweight = parseFloat($('#BodyContent_txtOtyDispatch').val()).toFixed(2);
                                        if (netweight > 0)
                                            var waste = parseFloat(parseFloat(supplierweight) - parseFloat(netweight)).toFixed(2);
                                        $('#BodyContent_txtTransitWastage').val(waste);
                                        var TransitWastage = waste;
                                        debugger;
                                        if (parseFloat(TransitWastage) < 0 && tankerweight>=0)
                                        {
                                            alert("Net weight can be lesser or equal to Dispatch Qty");
                                            $('#BodyContent_txtNetWeight').val($('#BodyContent_txtGrossWeight').val());
                                            $('#BodyContent_NetWeight').val($('#BodyContent_txtGrossWeight').val());
                                            $('#BodyContent_txtTransitWastage').val("");
                                            $('#BodyContent_txtTankerWeight').val("");
                                            $('#BodyContent_txtTankerWeight').focus();
                                           
                                                return false;
                                        }
                                       
                                    }
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
                                        $('#BodyContent_txtDATE').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                    
                                </script>
                                <script>
                                    $(document).ready(function () {
                                        GetValues();
                                        Calcutate();
                                        if ($('#BodyContent_txtDATE').val() == "")
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                        if ($('#BodyContent_txtNetWeight').val() == "" || $('#BodyContent_txtNetWeight').val() == "0") {
                                            $('#BodyContent_txtNetWeight').val($('#BodyContent_NetWeight').val());
                                        }
                                        if ($('#BodyContent_txtNetWeight').val() == "" || $('#BodyContent_txtNetWeight').val() == "0") {
                                            $('#BodyContent_txtTransitWastage').val("");
                                        }
                                    });
                                </script>
                            </head>
                            <body>
                                <!DOCTYPE html>
                                <div runat="server" id="SCM">
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
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage & Adjustment</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                 <div runat="server" id="MTP">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnIssue" OnClick="btnIssue_Click">
                                        <span style="color: #fff; font-size: 14px;">Issue</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnConsumption" OnClick="btnConsumption_Click">
                                        <span style="color: #fff; font-size: 14px;">Consumption</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkOB" OnClick="lnkOB_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="lnkShowRecords" OnClick="ShowRecords_Click" Style="float: right" Text="ShowRecords"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Raw Material Receipt Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    
                                    <div >
                                        <label class="control-label" style="display: inline;font-size:smaller"><span style="color: red">*</span> From Which Unit Name</label><br />
                                        <asp:TextBox ID="txtUnitName" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Name" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="rtype" runat="server" />
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Pass No</label><br />
                                        <asp:TextBox ID="txtPassNo" runat="server" CssClass="form-control" Width="60%" data-toggle="tooltip" data-placement="right" title="Pass No" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Date Of Issue</label><br />
                                        <asp:TextBox ID="txtDateOfIssue" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Date Of Issue" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Vehicle No</label><br />
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Vehicle No" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Dispatch Qty/Supplier Weight </label>
                                        <br />
                                        <asp:TextBox ID="txtOtyDispatch" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Oty Dispatch" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" MaxLength="10" data-toggle="tooltip" data-placement="right"  CssClass="form-control" title="Date" class="form-control" AutoComplete="off" runat="server">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob" runat="server" />
                                    </div> 
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Gross Weight </label>
                                        <br />
                                        <asp:TextBox ID="txtGrossWeight" onchange="GetValues()" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Gross Weight"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Tanker Weight</label><br />
                                        <asp:TextBox ID="txtTankerWeight" onchange="GetValues()" runat="server" data-toggle="tooltip" data-placement="right" title="Tanker Weight" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Net Weight</label><br />
                                        <asp:TextBox ID="txtNetWeight" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Net Weight" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="NetWeight" runat="server" />
                                    </div>
                                   
                                    <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Supplier Weight</label><br />
                                        <asp:TextBox ID="txtSupplierWeight" onchange="GetValues()" ReadOnly="true" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Supplier Weight"></asp:TextBox>
                                    </div>--%>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Transit Wastage </label>
                                        <br />
                                        <asp:TextBox ID="txtTransitWastage" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Transit Wastage" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:HiddenField ID="gridtotal" runat="server" />
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div style="height: 8%; background-color: #26b8b8;">
                                        <span style="font-size: small; color: white; margin-left: 40%">Raw Material/Molasses Stored in VAT</span>
                                    </div>
                                    &nbsp;
                                    <div class="clearfix"></div>
                                    <div>
                                        <div runat="server" id="div1">
                                            <div>
                                                <asp:GridView ID="grdRawMaterial" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Records"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" ShowFooter="true" class="table table-striped responsive-utilities jambo_table">
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
                                                                    <asp:Label Text="Total" style="text-align:right" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" /></div>

                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                         <%--<asp:TemplateField HeaderText="Total Capacity" ItemStyle-Font-Bold="true" ItemStyle-Width="15%" >
                                                    <ItemTemplate>
                                                      
                                                        <asp:TextBox ID="txttotalcapacity" style="text-align:right"  runat="server" CssClass="form-control" runat="server" Text='<%#Eval("vat_totalcapacity") %>' ReadOnly="true">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                      <FooterTemplate>

                                                                <div>
                                                                    <asp:Label Text="Total" style="text-align:right" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" /></div>

                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="left" />
                                                    
                                                </asp:TemplateField>--%>
                                                       

                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" style="text-align:right" onchange="Calcutate()" class="calculate" runat="server" CssClass="form-control" Text='<%#Eval("storedqty") %>' onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
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
                                                    <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="10px"></FooterStyle>

                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                </asp:GridView>
                                            </div>

                                        </div>

                                                                               <div class="clearfix"></div>
                                        <p>&nbsp;</p>

                                        <div >
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>

                                        <div id="approvalremarks" runat="server" class="col-md-9 col-sm-12 col-xs-12">
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                            <asp:TextBox ID="txtApproverremarks" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                                                                <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveAs" runat="server" class="btn btn-info pull-left" OnClientClick="javascript:return validationMsg()" Style="height: 30px" OnClick="btnSaveAs_Click">
                                                    <span aria-hidden="true" > </span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg()" Style="height: 30px" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit </asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" OnClick="btnApprove_Click" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" OnClick="btnReject_Click" class="fa fa-cut" />
                                             <asp:LinkButton ID="btnupdate" runat="server" Visible="false" OnClientClick="javascript:return validationMsg2();" CssClass="btn btn-primary" OnClick="btnupdate_Click"><span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true"> </span></asp:LinkButton>
                                        </div>
                                        <p>&nbsp;</p>
                                        <div id="approv" runat="server">
                                        <div  class="x_title">
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
