<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="FermentertoReceiverForm.aspx.cs" Inherits="UserMgmt.FermentertoReceiverForm" %>

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
                            <head>     <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                                   <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title>Fermenter To Receiver</title>
                                 <script  type="text/javascript">
                                     function validationMsg1() {
                                         if (document.getElementById('<%=txtapproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Comments ");
                                             document.getElementById("<% =txtapproverremarks.ClientID%>").focus();
                                             return false;
                                         }
                                     }
                                     function validationMsg() {
                                         if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                             alert("Select Gauged & Proved Date ");
                                             document.getElementById("<% =txtDATE.ClientID%>").focus();
                                             return false;
                                           
                                         }
                                         if (document.getElementById('<%=ddlDistillationDate.ClientID%>').value == 'Select') {
                                             alert("Select Distillatio Date ");
                                             document.getElementById("<% =ddlDistillationDate.ClientID%>").focus();
                                             return false;
                                           
                                         }
                                         CheckIsRepeat();
                                     }
                                     </script>
                                     <script  type="text/javascript">
                                     function validationMsg2()
                                     {
                                         if (document.getElementById('<%=ddlFermenter.ClientID%>').value == 'Select') {
                                             alert("Select  From Fermenter");
                                             document.getElementById("<% =ddlFermenter.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtDipsinWetInches.ClientID%>').value == '') {
                                             alert("Enter dips  wet in cm");
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

                                         if (document.getElementById('<%=ddlReceiver.ClientID%>').value == 'Select') {
                                             alert("Select to Which Receiever ");
                                             document.getElementById("<% =ddlReceiver.ClientID%>").focus();
                                             return false;
                                           
                                         }
                                        <%-- if (document.getElementById('<%=txtBulkLiters.ClientID%>').value == '') {
                                             alert("Enter BulkLiters");
                                             document.getElementById("<% =txtBulkLiters.ClientID%>").focus();
                                             return false;
                                         }--%>

                                     }
                                     </script>
                                        <script  type="text/javascript">
                                     function validationMsg3() {

                                         if (document.getElementById('<%=ddlStorageVat.ClientID%>').value == 'Select') {
                                              alert("Select StorageVat");
                                             document.getElementById("<% =ddlStorageVat.ClientID%>").focus();
                                             return false;
                                         }
                                         if (document.getElementById('<%=txtDateofRemoval.ClientID%>').value == '') {
                                             alert("Enter Date of Removal ");
                                             document.getElementById("<% =txtDateofRemoval.ClientID%>").focus();
                                             return false;
                                         }
                                         if (document.getElementById('<%=txtHoursofRemoval.ClientID%>').value == '') {
                                             alert("Enter Hours of Removal ");
                                             document.getElementById("<% =txtHoursofRemoval.ClientID%>").focus();
                                             return false;
                                         }
                                           if (document.getElementById('<%=BulkLiters.ClientID%>').value == '') {
                                             alert("Enter BulkLiters");
                                             document.getElementById("<% =BulkLiters.ClientID%>").focus();
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
                                        $('#BodyContent_txtDateofRemoval').val(todayDate);
                                        $('#BodyContent_txtdor').val(todayDate);
                                    }

                                    function Selectdate(e) {
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
                                        $('#BodyContent_txtDate3').val(todayDate);
                                        $('#BodyContent_txtdor1').val(todayDate);
                                    }

                                </script>
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
                              <script>
                                  //$(document).ready(function () {
                                  //    debugger;
                                  //    if ($('#BodyContent_ddlFermenter').val() != "Select") {
                                  //        GetValuesofVAT();
                                  //        GetValueINLPL2();
                                  //        GetValueINLPL1();
                                  //    }
                                  //  });
                                    </script> 
                                
                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                       
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtgpd').val());
                                        }

                                        if ($('#BodyContent_txtDateofRemoval').val() == "") {
                                            $('#BodyContent_txtDateofRemoval').val($('#BodyContent_txtdor').val());
                                        }
                                        if ($('#BodyContent_txtDate3').val() == "") {
                                            $('#BodyContent_txtDate3').val($('#BodyContent_txtdor1').val());
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
                                        <li class="active">
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
                                    <h2>Fermenter To Receiver</h2>
                                    <div class="clearfix"></div>
                                </div>
                         
                                <div class="x_content">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Gauged & Proved Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate3" ID="CalendarExtender"></cc1:CalendarExtender>
                                              <%--  <script type="text/javascript">

                                                    function SelectDate3(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDATE').val();
                                                        $('#BodyContent_txtgpd').val(dat1e);
                                                    }

                                                </script>--%>
                                                <asp:TextBox ID="txtDATE" data-toggle="tooltip" data-placement="right" title="Guaged & Proved Date" class="form-control" AutoComplete="off" ReadOnly="true"  runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtgpd" runat="server" />

                                            </div>
                               
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red">*</span>Distillation Date </label><br />
                                    <asp:DropDownList ID="ddlDistillationDate"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Distillation Date" AutoPostBack="true" OnSelectedIndexChanged="ddlDistillationDate_SelectedIndexChanged"   runat="server"></asp:DropDownList>
                                     </div>

                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red">*</span>Setup Date </label><br />
                                    <asp:DropDownList ID="ddlSetupDate"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Setup Date" AutoPostBack="true" OnSelectedIndexChanged="ddlSetupDate_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                     </div>
                                    <div class="x_title">
                                    <div class="clearfix"></div>
                                </div>
                                     
                                    <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Number Of Receiver</label><br />
                                                <asp:TextBox ID="txtNumberOfReceiver" runat="server" data-toggle="tooltip" data-placement="right" title="Number Of Receiver"  CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                      <div class="clearfix"></div>
                                      <div class="x_title">--%>
                                  
                               
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>From Fermenter</label><br />
                                                <asp:DropDownList ID="ddlFermenter"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="From Fermenter" AutoPostBack="true" OnSelectedIndexChanged="ddlFermenter_SelectedIndexChanged"  ></asp:DropDownList>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red"></span>Available Qty (BL)</label><br />
                                     <asp:TextBox ID="txtAvailableQtyBL" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title=" Available Qty(BL)" ReadOnly="true"></asp:TextBox>
                                </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Dips in Wet CM</label><br />

                                                <asp:TextBox ID="txtDipsinWetInches" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Dips in wet in cm" onkeypress="return onlyDotsAndNumbers(this,event);"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            
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
                                        <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Strength"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                              <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>To Which Receiver</label><br />
                                                <asp:DropDownList ID="ddlReceiver" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="No Of Receiver" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiver_SelectedIndexChanged" ></asp:DropDownList>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Bulk Liters</label><br />

                                        <asp:TextBox ID="txtBulkLiters" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" onchange="GetValueINLPL1()" data-placement="right" title="Bulk Liters" onkeypress="return onlyDotsAndNumbers(this,event);" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>LP Liters</label><br />
                                        <asp:TextBox ID="txtLPLiters" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="LP Liters"></asp:TextBox>
                                    </div>
                                       
                                     <%-- <div class="clearfix"></div>
                                            <p>&nbsp;</p>--%>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span></label><br />
                                        <asp:Button ID="btnadd1" runat="server" CssClass="btn btn-upload" Width="50%" OnClientClick="javascript:return validationMsg2();" Text="ADD" OnClick="Add"  />
                                    </div>
                                  
                                    <div class="x_title"> 
                                           <div class="clearfix"></div>
                                      </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="table" runat="server">
                                     <div id="dummy" runat="server"  style="height: auto; width: 70%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Dat">
                                            <thead>
                                                <tr>
                                                    <th>Dips in Wet CM</th>
                                                     <th>Temperature</th>
                                                    <th>Indication</th>
                                                    <th>Strength</th>
                                                     <th>To Which Receiver </th>
                                                    <th>Bulk Liters</th>
                                                    <th>LP Liters </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Datatabl">
                                            </tbody>
                                        </table>
                                    </div>
                                        </div>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdToReceiver" runat="server" AutoGenerateColumns="false" Width="100%" 
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" ShowFooter="true" OnRowDataBound="grdToReceiver_RowDataBound">
                                            <Columns>
                                                  <asp:TemplateField HeaderText="Fermenter Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px"  Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFermenterCode" runat="server" Visible="false" Text='<%#Eval("FermenterCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px"  Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatCoder" runat="server" Visible="false" Text='<%#Eval("vatcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Dips in Wet CM" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDipsinWetCM" runat="server" Visible="true" Text='<%#Eval("Dips") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <FooterTemplate>
                                                          </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Temperature" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTemperature" runat="server" Visible="true" Text='<%#Eval("Temperature") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Indication" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndication" runat="server" Visible="true" Text='<%#Eval("Indication") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Strength" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStrength" runat="server" Visible="true" Text='<%#Eval("Strength") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="To Which Receiver" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToWhichReceiver" runat="server" Visible="true" Text='<%#Eval("Receiver") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <FooterTemplate>
                                                         <b>Total</b>:
                                                          </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bulk Liters" ItemStyle-Font-Bold="true"  ItemStyle-Width="200px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBulkLiters" runat="server" Visible="true" Text='<%#Eval("Bulk Liters") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                         
                                                        <asp:Label ID="lblTotal" runat="server" ForeColor="White" Font-Bold="true" Text="0.00"></asp:Label>  
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="LP Liters" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LPLiters" runat="server" Visible="true" Text='<%#Eval("LP Liters") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <FooterTemplate>
                                                         
                                                        <asp:Label ID="lblLPTotal" runat="server" ForeColor="White" Font-Bold="true" Text="0.00"></asp:Label>  
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="No of vat" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                   <ItemTemplate>
                                                        <asp:Label ID="lblvat" runat="server" Visible="true" Text='<%#Eval("No of vats") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                   <asp:TemplateField HeaderText="ID" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc" runat="server" Visible="true" Text='<%#Eval("Doc_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="ImageButton1"   CommandName="Remove"  CommandArgument='<%#Eval("Doc_id") %>'  ImageUrl="~/img/delete.gif" runat="server" OnClick="ImageButton1_Click" />&nbsp;
                                                     <%--     <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton> --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                               <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px"/>
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                            
                                        </asp:GridView></div>
                                      <div class="x_title"> 
                                           <div class="clearfix"></div>
                                      </div>
                                   <div class="x_title">
                                    <h2>To Store</h2>
                                    <div class="clearfix"></div>
                                </div>
                                      
                                      <p>&nbsp;</p>
                                  
                              
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>To Which Storage Vat</label><br />
                                     <asp:DropDownList ID="ddlStorageVat" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Storage Vat" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlStorageVat_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Available Qty</label><br />
                                        <input type="text" id="txtAvailableQty" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right"    title="Available" class="form-control" readonly="readonly" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Date of Removal</label><br />
                                      <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtDateofRemoval" Format="dd-MM-yyyy"  OnClientDateSelectionChanged="SelectRemovalDate" ID="CalendarExtender1"></cc1:CalendarExtender>
                                        <%-- <script type="text/javascript">

                                             function SelectDate(e) {
                                                 debugger;
                                                 var dat1e = $('#BodyContent_txtDateofRemoval').val();
                                                        $('#BodyContent_txtdor').val(dat1e);
                                                    }

                                                </script>--%>
                                        <asp:TextBox ID="txtDateofRemoval"  data-toggle="tooltip" data-placement="right" title="Date Reserved for Form 84" class="form-control validate[required]" ReadOnly="true" AutoComplete="off"  runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                         <asp:HiddenField ID="txtdor" runat="server" />

                                    </div>
                                 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Hours of Removal</label><br />
                                    <input type="time" id="txtHoursofRemoval" class="form-control" data-toggle="tooltip" runat="server" data-placement="right" title="Hours of Removal"/>
                                    </div>
                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Bulk Liters</label><br />
                                        <asp:TextBox ID="BulkLiters" runat="server" CssClass="form-control" data-toggle="tooltip"  autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Bulk Liters" AutoPostBack="true" OnTextChanged="BulkLiters_TextChanged"></asp:TextBox>
                                    </div>
                                          
                                  
                              
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>LP Liters</label><br />
                                        <asp:TextBox ID="LPLiters" runat="server" CssClass="form-control" ReadOnly="true" autocomplete="off" data-toggle="tooltip" data-placement="right" title="LP Liters"></asp:TextBox>
                                    </div>
                                      
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span></label><br />
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-upload" Width="50%"  OnClientClick="javascript:return validationMsg3();" Text="ADD" OnClick="Add1" />
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div id="dummyDatatable" runat="server" style="height: auto; width: 95%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Data">
                                            <thead>
                                                <tr>
                                                    <th>To Which Storage Vat</th>
                                                    <th>Date of Removal</th>
                                                     <th>Hours of Removal</th>
                                                    <th>Bulk Liters</th>
                                                    <th>LP Liters</th>
                                                     
                                                </tr>
                                            </thead>
                                            <tbody id="Datatable">
                                            </tbody>
                                        </table>
                                    </div> 
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <asp:UpdatePanel runat="server">
                                                    <ContentTemplate> 
                                           <div>
                                                 
                                        <asp:GridView ID="gridToStore" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server" ShowFooter="true"  AutoGenerateColumns="false" OnRowDataBound="gridToStore_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" ItemStyle-Width="50px"  Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatCode" runat="server" Visible="false" Text='<%#Eval("vat_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Which Storage VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStorageVAT" runat="server" Visible="true" Text='<%#Eval("StorageVat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Date of Removal" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateofRemoval" runat="server" Visible="true" Text='<%#Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hours of Removal" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHoursofRemoval" runat="server" Visible="true" Text='<%#Eval("Hours") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <FooterTemplate>
                                                         <b>Total</b>:
                                                           </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bulk Liters" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBulkLiters" runat="server" Visible="true" Text='<%#Eval("BulkLiter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <FooterTemplate>
                                                        
                                                        <asp:Label ID="lblTotal" runat="server" ForeColor="White" Font-Bold="true" Text="0.00"></asp:Label>  
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LP Liters" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLPLiters" runat="server" Visible="true" Text='<%#Eval("LPLiter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                         
                                                        <asp:Label ID="lblLPTotal" runat="server" ForeColor="White" Font-Bold="true" Text="0.00"></asp:Label>  
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ID" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc" runat="server" Visible="true" Text='<%#Eval("Docid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" CommandArgument='<%#Eval("Docid") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="ImageButton3_Click" />

                                                          <%-- <asp:LinkButton Text="Edit" id="btnEdit"  CssClass="myButton1"   runat="server"  CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                            </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                               <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px"/>
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                           
                                        </asp:GridView></div></ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="LPLiters" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      
                                        <div class="x_title">
                                      <h2>For Re-Distillation</h2>
                                       <div class="clearfix"></div>
                                  </div>

                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                          <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red"></span>To Which Still</label><br />
                                      <asp:TextBox ID="txtToWhichStill" runat="server" CssClass="form-control" data-toggle="tooltip"  data-placement="right" title="To Which Still"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Date of Removal</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtDate3" OnClientDateSelectionChanged="Selectdate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                      <%--   <script type="text/javascript">

                                             function SelectDate2(e) {
                                                 debugger;
                                                        var dat1e = $('#BodyContent_txtDate3').val();
                                                        $('#BodyContent_txtdor1').val(dat1e);
                                                    }

                                                </script>--%>
                                        <asp:TextBox ID="txtDate3"  data-toggle="tooltip" data-placement="right" title="Date" class="form-control validate[required]" ReadOnly="true" AutoComplete="off"  runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                         <asp:HiddenField ID="txtdor1" runat="server" />

                                    </div>
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Bulk Liters</label><br />
                                        <asp:TextBox ID="txtRBulkLiters" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" data-placement="right" title="Bulk Liters" OnTextChanged="txtRBulkLiters_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>LP Liters</label><br />
                                        <asp:TextBox ID="txtRLpLiters" runat="server" CssClass="form-control"  ReadOnly="true"  data-toggle="tooltip" data-placement="right" title="LP Liters"></asp:TextBox>
                                    </div>
                                             
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" runat="server" class="form-control" name="size" data-toggle="tooltip" data-placement="right" title="Remarks"></textarea>
                                    </div>
                                  <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id ="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtapproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                        </div> 
                             
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                            <asp:HiddenField ID="party" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                                CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click">
                                                       Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                            <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
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
                                    </div></div></div></body></html>
                                        </div> 
                                </div>
                                   </div>
                           
                        </div>
                    </div>
                </div>
           
    

</asp:Content>
