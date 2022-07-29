<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RawMaterialtoFermenterForm.aspx.cs" Inherits="UserMgmt.RawMaterialtoFermenterForm" %>

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
                                <title>Raw Material To Fermenter</title>
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
                                        debugger;

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
                                        alert("Select Fermenter");
                                            document.getElementById("<% =ddlFermenter.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                        if (document.getElementById('<%=txtVatNumber.ClientID%>').value == '') {
                                            alert("Enter Vat Number");
                                            document.getElementById("<% =txtVatNumber.ClientID%>").focus();
                                            return false;

                                        }
                                        var gridView = document.getElementById("<%=gridToStore.ClientID %>");
                                        if (gridView == null)
                                        {
                                            if (document.getElementById('<%=ddlMolassesStorageVAT.ClientID%>').value == 'Select') {
                                            alert("Select Molasses Storage VAT");
                                            document.getElementById("<% =ddlMolassesStorageVAT.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtMolassesused.ClientID%>').value == '') {
                                            alert("Enter Molasses");
                                            document.getElementById("<% =txtMolassesused.ClientID%>").focus();
                                            return false;
                                        }
                                        }
                                         if (document.getElementById('<%=txtBLofWashSetup.ClientID%>').value == '') {
                                             alert("Enter BL of WashSetup");
                                            document.getElementById("<% =txtBLofWashSetup.ClientID%>").focus();
                                            return false;

                                         }
                                       
                                        
                                        if (document.getElementById('<%=txtSpentWash.ClientID%>').value == '') {
                                            alert("Enter SpentWash");
                                            document.getElementById("<% =txtSpentWash.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtSGVatorCask.ClientID%>').value == '') {
                                            alert("Enter SG Vat or Cask");
                                            document.getElementById("<% =txtSGVatorCask.ClientID%>").focus();
                                            return false;
                                        }
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
                                        if (document.getElementById('<%=ddlMolassesStorageVAT.ClientID%>').value == 'Select') {
                                            alert("Select Molasses Storage VAT");
                                            document.getElementById("<% =ddlMolassesStorageVAT.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtMolassesused.ClientID%>').value == '') {
                                            alert("Enter Molasses");
                                            document.getElementById("<% =txtMolassesused.ClientID%>").focus();
                                            return false;

                                        }
                                        CheckIsRepeat();
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
                                     
                                    function Calcutate() {

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
                                       
                                    }

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

                                
                                 <a>
                                   <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Fermenter Setup</h2>
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
                                        <label class="control-label"><span style="color: red">*</span>Date of Setup</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE"  OnClientDateSelectionChanged="SelectSetupDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                          <script type="text/javascript">
                                                    function SelectSetupDate(e) {
                                                        debugger;
                                                        var dat1e = $('#BodyContent_txtDATE').val();
                                                        $('#BodyContent_txtdob1').val(dat1e);
                                                    }

                                                </script>
                                        <asp:TextBox ID="txtDATE"  data-toggle="tooltip" data-placement="right" title="Date Reserved for Distillation" ReadOnly="true" CssClass="form-control validate[required]"  AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob1" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span> Setup Time </label><br />
                                        <input type="time" id="txtSetuptime" data-toggle="tooltip" data-placement="right" title="Start Time" runat="server" class="form-control" >
                                    </div>
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>To Fermenter VAT </label>
                                        <br />
                                        <asp:DropDownList ID="ddlFermenter" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="To Fermenter VAT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFermenter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Number of Vats </label>
                                        <br />
                                        <asp:TextBox ID="txtVatNumber" data-toggle="tooltip" data-placement="right" title="Vat Number" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control" Style="text-align: left" runat="server"></asp:TextBox>
                                    </div>
                                            
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="x_title"></div>
                                 <%-- <div class="x_title">
                                      <h2>Fermentation Setup</h2>
                                        <div class="clearfix"></div>
                                  </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                
                                             
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="Label1" runat="server" style="display:inline"><span style="color: red">*</span> Storage VAT</label><br />
                                      <asp:DropDownList ID="ddlMolassesStorageVAT"  CssClass="form-control" data-toggle="tooltip" Width="60%" data-placement="right" title="Storage VAT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMolassesStorageVAT_Click">
                                        </asp:DropDownList>
                                             <asp:HiddenField ID="SVat" runat="server" />
                                    </div>
                                  
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="Label2" runat="server"><span style="color: red"></span>Available Stock</label><br />
                                        <input type="text" id="txtAvailableStock" runat="server" data-toggle="tooltip" data-placement="right" title="Available Stock"  class="form-control" name="size" readonly="readonly">
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="Label3" runat="server" style="display:inline"><span style="color: red"></span>Product</label><br />
                                     <%-- <asp:DropDownList ID="ddlRawmaterialType"  CssClass="form-control" data-toggle="tooltip"  data-placement="right" title="Rawmaterial Type" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>--%>
                                           <input type="text" id="txtRawMaterial" runat="server" data-toggle="tooltip" data-placement="right" title=""  class="form-control" name="size" readonly="readonly">
                                             <asp:HiddenField ID="hdnst" runat="server" />
                                    </div>
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                          <span style="color: red">*</span><label class="control-label" id="lblRawMaterialUsed" runat="server" style="display:inline"><asp:Label ID="lblstar" runat="server" Text="Label"></asp:Label></label><br />
                                        <asp:TextBox ID="txtMolassesused" runat="server" data-toggle="tooltip" data-placement="right" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" title="Molasses" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMolassesused_TextChanged"></asp:TextBox>
                                        <%--<input type="text" id="" runat="server" data-toggle="tooltip" data-placement="right" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" title="Molasses" class="form-control" name="size">--%>
                                    </div>
                                          <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Mahua</label><br />
                                        <input type="text" id="txtMahua" runat="server" class="form-control" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Mahua" name="size">
                                    </div>
                                 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Gur</label><br />
                                        <input type="text" id="txtGur" runat="server" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Gur" class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span> Wash</label><br />
                                        <input type="text" id="txtSpentWash1" runat="server" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Spent Wash" class="form-control">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Active Wash or Liquid Yeast</label><br />
                                        <input type="text" id="txtActiveWash" data-toggle="tooltip" data-placement="right" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" title="Active Wash or Liquid Yeast" runat="server" class="form-control" name="size">
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Water/Juice</label><br />
                                        <input type="text" id="txtWater"  runat="server" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Water/Juice"
                                            class="form-control">
                                    </div>
                                       
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Other materials </label>
                                        <br />
                                        <input type="text" id="txtOtherMaterials" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Other materials"
                                            class="form-control" name="size">
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span></label><br />
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-upload" Width="50%" OnClientClick="javascript:return CheckDepot()"  Text="ADD" OnClick="Add" />
                                    </div>
                                    </div>
                                 
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <%--<div id="table" runat="server">--%>
                                     <div id="dummyDatatable" runat="server"  style="height: auto; width: 95%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Data">
                                            <thead>
                                                <tr>
                                                    <th>Storage VAT</th>
                                                     <th>Product</th>
                                                    <th>Raw Material Used</th>
                                                     <th>Mahua</th>
                                                    <th>Gur</th>
                                                    <th> Wash</th>
                                                     <th>Active Wash </th>
                                                    <th>Water</th>
                                                    <th>Other materials </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Datatable">
                                            </tbody>
                                        </table>
                                    </div>
                                        <%--</div>--%>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                           <div> 
                                        <asp:GridView ID="gridToStore" runat="server" AutoGenerateColumns="false"  ShowFooter="true" GridLines="None"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnRowDataBound="gridToStore_RowDataBound" OnPageIndexChanging="gridToStore_PageIndexChanging">
                                            <Columns>
                                                  <asp:TemplateField HeaderText="Gur" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVatcode" runat="server" Visible="false" Text='<%#Eval("vat_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Storage VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="200px"  Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMolassesStorageVAT" runat="server" Visible="true" Text='<%#Eval("StorageVat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div>
                                                            <asp:Label Text="Total" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" ItemStyle-Width="200px"  Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct" runat="server" Visible="true" Text='<%#Eval("Product") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%-- <asp:TemplateField HeaderText="Raw Material Type" ItemStyle-Font-Bold="true" ItemStyle-Width="200px"  Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRawMaterialType" runat="server" Visible="true" Text='<%#Eval("RawMaterialType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="Raw Material Used" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMolassesUsed" runat="server" Visible="true"   Text='<%#Eval("Molasses") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="0.00"></asp:Label>  
                                                      <%--  <div>
                                                            <asp:Label ID="lblTotal" ItemStyle-Font-Bold="true" Font-Bold="true" Width="100px" runat="server" />
                                                        </div>--%>
                                                          
                                                    </FooterTemplate>
                                                    <FooterStyle  />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mahua" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMahua" runat="server" Visible="true" Text='<%#Eval("Mahua") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%-- <FooterStyle HorizontalAlign="Right" />
                                                      <FooterTemplate>
                                                      <asp:Button ID="ButtonAdd"  CssClass="btn btn-primary" runat="server" style="width:50px;"  Text="Add New Row"  />
                                                    </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Gur" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGur" runat="server" Visible="true" Text='<%#Eval("Gur") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Wash" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpentWash" runat="server" Visible="true" Text='<%#Eval("Spent Wash") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Active Wash" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActiveWash" runat="server" Visible="true" Text='<%#Eval("Active Wash") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Water" ItemStyle-Font-Bold="true"  ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWater" runat="server" Visible="true" Text='<%#Eval("Water") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Other materials" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOthermaterials" runat="server" Visible="true" Text='<%#Eval("Other materials") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="No of vat" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvat" runat="server" Visible="true" Text='<%#Eval("No of vats") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Doc No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc" runat="server" Visible="true" Text='<%#Eval("Doc_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="ImageButton1"   CommandName="Remove" OnClick="ImageButton1_Click" CommandArgument='<%#Eval("Doc_id") %>'  ImageUrl="~/img/delete.gif" runat="server" />&nbsp;
                                                     <%--     <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton> --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                            
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                              <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px"/>
                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                    </div><br />

                                 <%--  <div id="molassestotal" runat="server" style="height: 25px; width:1200px; background-color: #26b8b8;">
                                        <span style="font-size: small; color: white; margin-left: 40%">Molasses Used:-</span> <span> <asp:Label ID="lbltotal" Font-Bold="true" runat="server" Text="0"></asp:Label></span>
                                    </div>--%>   <div class="clearfix"></div>
                                
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                   
                               <%--  <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                     <asp:Label ID="lblMolasses" Font-Bold="true" runat="server" Text="Molasses Used"></asp:Label>:
                                     </div>--%>
                                    <%--  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"  runat="server"  style="display: inline"><span style="color: red">*</span>Total Molasses Used</label><br />
                                        <input type="text" id="Text4" runat="server" data-toggle="tooltip" data-placement="right" title="Molasses" class="form-control" name="size" readonly="readonly">
                                    </div>--%>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Total BL of Wash Setup</label><br />
                                        <input type="text" id="txtBLofWashSetup" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Total BL of Wash Setup" class="form-control" name="size">
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Wash</label><br />
                                            <input type="text" id="txtSpentWash" data-toggle="tooltip" data-placement="right" autocomplete="off" onkeypress="return onlyDotsAndNumbers1(this,event);" title="Spent Wash" runat="server" class="form-control" name="size">
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                           <label class="control-label" style="font-size: small;display:inline""><span style="color: red">*</span> Wash / Avg. in each Vat or Cask(SG)</label><br />
                                            <input type="text" id="txtSGVatorCask" runat="server" data-toggle="tooltip" autocomplete="off" onkeypress="return onlyDotsAndNumbers1(this,event);" data-placement="right" title="Wash / Avg. in each Vat or Cask" class="form-control">
                                        </div>
                                    
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                
                                           <%--  <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>--%>
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
