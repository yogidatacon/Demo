<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RMR_GrainBased1.aspx.cs" Inherits="UserMgmt.RMR_GrainBased1" %>

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
                                <script type="text/javascript" src="../common/theme/js/flot/date.js"></script>
                                <title>Raw Material Receipt</title>
                                <script type="text/javascript" src="jquery.timepicker.js"></script>
                               <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                     if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter  Date");
                                             document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;
                                           
                                     }
                                         if (document.getElementById('<%=ddtypeofmeterial.ClientID%>').value == 'Select') {
                                            alert("Select Rawmaterial");
                                             document.getElementById("<% =ddtypeofmeterial.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtPassNo.ClientID%>').value == '') {
                                            alert("Enter Receipt No");
                                             document.getElementById("<% =txtPassNo.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtRecieptDate.ClientID%>').value == '') {
                                            alert("Enter Receipt Date");
                                             document.getElementById("<% =txtRecieptDate.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtOtyDispatch.ClientID%>').value == '') {
                                             alert("Enter Receipt QTY");
                                             document.getElementById("<% =txtOtyDispatch.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=ddlUOM.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                             document.getElementById("<% =ddlUOM.ClientID%>").focus();
                                            return false;
                                           
                                         } 
                                         if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                            alert("Enter Vehicle No");
                                             document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                           if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                               alert("Enter Remarks");
                                             document.getElementById("<% =txtRemarks.ClientID%>").focus();
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
                                    var hd = 0;
                                    function Calcutate() {
                                        debugger;;
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
                                        $('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                      
                                      
                                       
                                    }
                                       </script>
                                 <script type="text/javascript">
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
                                        $('#BodyContent_txtDATE').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                        $('#BodyContent_txtdob').val(todayDate);
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
                                        $('#BodyContent_txtRecieptDate').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                        $('#BodyContent_reciept').val(todayDate);
                                    }
                                      </script>

                                  <script>
                                    $(document).ready(function () {
                                  
                                       
                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == ""){
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                        }
                                        Calcutate();
                                        debugger;
                                        if ($('#BodyContent_txtRecieptDate').val() == "") {
                                            $('#BodyContent_txtRecieptDate').val($('#BodyContent_reciept').val());
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
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="lnkShowRecords" OnClick="lnkShowRecords_Click" Style="float: right" Text="ShowRecords"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Raw Material Receipt Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span> Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" MaxLength="10" data-toggle="tooltip" data-placement="right"  CssClass="form-control" title="Date" class="form-control" AutoComplete="off" runat="server">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob" runat="server" />
                                    </div>
                                   
                                    <div class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline;font-size:smaller"><span style="color: red">*</span>Raw Material</label><br />
                                       <%-- <asp:TextBox ID="txtUnitName" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Name" ReadOnly="true"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddtypeofmeterial"  Height="30px" width="60%" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddtypeofmeterial_SelectedIndexChanged" title="Party Name" Cssclass="form-control" Style="">
                                                       <%--     <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline;font-size:smaller"><span style="color: red">*</span>Supplier Type</label><br />
                                       <%-- <asp:TextBox ID="txtUnitName" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Name" ReadOnly="true"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlSupplierType"  Height="30px" width="60%" AutoPostBack="true"  runat="server" data-toggle="tooltip" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged" data-placement="right" title="Party Name" Cssclass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Sugar Mill" Value="Sugar Mill"></asp:ListItem>
                                             <asp:ListItem Enabled="true" Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>
                                     <div id="ddp" runat="server" visible="false" class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline;font-size:smaller"><span style="color: red">*</span>Supplier Name</label><br />
                                       <%-- <asp:TextBox ID="txtUnitName" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Name" ReadOnly="true"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlparty"  Height="30px" width="85%" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" title="Party Name" Cssclass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                    </div>
                                    <div id="txtp" runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Supplier Name</label><br />
                                        <asp:TextBox ID="txtfromparty" runat="server" CssClass="form-control" Width="60%" data-toggle="tooltip" data-placement="right" title="Pass No" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="rtype" runat="server" />
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Receipt No</label><br />
                                        <asp:TextBox ID="txtPassNo" runat="server" CssClass="form-control" Width="60%" data-toggle="tooltip" data-placement="right" title="Pass No" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Receipt Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtRecieptDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate1" ID="CalendarExtender1"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtRecieptDate" MaxLength="10" data-toggle="tooltip" data-placement="right"  CssClass="form-control" title="Date" class="form-control" AutoComplete="off" runat="server">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="reciept" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Receipt Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtOtyDispatch" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Oty Dispatch" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline;font-size:smaller"><span style="color: red">*</span>UOM</label><br />
                                       <%-- <asp:TextBox ID="txtUnitName" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Name" ReadOnly="true"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlUOM"  Height="30px" width="50%" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right"  title="UOM" Cssclass="form-control" Style="">
                                                        </asp:DropDownList>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Vehicle No</label><br />
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Vehicle No" ></asp:TextBox>
                                    </div>
                                       </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                
                                    <asp:HiddenField ID="gridtotal" runat="server" />
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div style="height: 8%; background-color: #26b8b8;">
                                        <span style="font-size: small; color: white; margin-left: 40%">Raw Material Stored in VAT</span>
                                    </div>
                                    &nbsp;
                                    <div class="clearfix"></div>
                                  
                                        <div runat="server" id="div1">
                                            <div>
                                                <asp:GridView ID="grdRawMaterial" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Records"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" ShowFooter="true" class="table table-striped responsive-utilities jambo_table" >
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
                                                    <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="10px"></FooterStyle>

                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                </asp:GridView>
                                            </div>

                                        </div>

                                        <div class="clearfix"></div>

                                        <div class="col-md-9 col-sm-12 col-xs-12">
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="85.5%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div id="approvalremarks" runat="server" class="col-md-9 col-sm-12 col-xs-12">
                                            <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Remarks</label><br />
                                            <asp:TextBox ID="txtApproverremarks" Width="85.5%"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                               <asp:HiddenField ID="hdtotal" runat="server" />
                                            <asp:LinkButton ID="btnSaveAs" runat="server" class="btn btn-info pull-left" OnClientClick="javascript:return validationMsg()" Style="height: 30px" OnClick="btnSaveAs_Click">
                                                    <span aria-hidden="true" > </span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" Style="height: 30px" OnClick="btnSubmit_Click" OnClientClick="javascript:return validationMsg()" >
                                                    <span aria-hidden="true" > </span>Submit </asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" OnClick="btnApprove_Click" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" OnClick="btnReject_Click" class="fa fa-cut" />
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
                             
                                </body>
                                </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
