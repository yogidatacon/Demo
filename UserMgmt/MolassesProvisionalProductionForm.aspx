<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MolassesProvisionalProductionForm.aspx.cs" Inherits="UserMgmt.MolassesProvisionalProductionForm1" %>

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
                                <title>Molasses Production Provisional Form</title>
                                 <script type="text/javascript" src="/common/theme/js/flot/date.js"></script>
                                <script>
                                    function onlyDotsAndNumbers(txt, event) {

                                        var charCode = (event.which) ? event.which : event.keyCode

                                        if (charCode == 46)
                                        {
                                            if (txt.value.indexOf(".") < 0)
                                                return true;
                                            else
                                                return false;
                                        }
                                        if (txt.value.indexOf(".") > 0)
                                        {
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
                                    function Validate0() {
                                        debugger;
                                        if (document.getElementById('<%=ddlCaptive.ClientID%>').value == 'Select') {
                                            alert("Select  Captive/Non Captive");
                                            document.getElementById("<% =ddlCaptive.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=ddlMaterial.ClientID%>').value == 'Select') {
                                            alert("Select  Material");
                                            document.getElementById("<% =ddlMaterial.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Select cane Crushing Date (sl:1)");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtmolassesproduction.ClientID%>').value == '') {
                                            alert("enter cane production of molasses Date (sl:2(a))");
                                            document.getElementById("<% =txtmolassesproduction.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtsugarproduction.ClientID%>').value == '') {
                                            alert("enter plan sugar season (sl:2(b))");
                                            document.getElementById("<%=txtsugarproduction.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyMproduceddaily.ClientID%>').value == '') {
                                            alert("enter plan molasses Daily (sl:2(c))");
                                            document.getElementById("<% =txtQtyMproduceddaily.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtySproduceddaily.ClientID%>').value == '') {
                                            alert("enter plan sugar Daily (sl:2(d))");
                                            document.getElementById("<% =txtQtySproduceddaily.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtproductionstoredin.ClientID%>').value == '') {
                                            alert("enter value of (sl:4)");
                                            document.getElementById("<% =txtproductionstoredin.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtLoadingofMolasses.ClientID%>').value == '') {
                                            alert("enter value of (sl:5)");
                                            document.getElementById("<% =txtLoadingofMolasses.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtYear21.ClientID%>').value == '') {
                                            alert("enter Year of (sl:6(1))");
                                            document.getElementById("<% =txtYear21.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtYear22.ClientID%>').value == '') {
                                            alert("enter Year of (sl:6(2))");
                                            document.getElementById("<% =txtYear22.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtYear23.ClientID%>').value == '') {
                                            alert("enter Year of (sl:6(3))");
                                            document.getElementById("<% =txtYear23.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtQtyYear1.ClientID%>').value == '') {
                                            alert("enter Year of (sl:8(1))");
                                            document.getElementById("<% =txtQtyYear1.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyYear2.ClientID%>').value == '') {
                                            alert("enter Year of (sl:8(2)");
                                            document.getElementById("<% =txtQtyYear2.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyYear3.ClientID%>').value == '') {
                                            alert("enter Year of (sl:8(3)");
                                            document.getElementById("<% =txtQtyYear3.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtQtyYear1.ClientID%>').value != '0'||document.getElementById('<%=txtQtyYear1.ClientID%>').value != '') {
                                             alert("Available QTY of year 1 still peding to storage vats");
                                            document.getElementById("<% =txtQtyYear1.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtQtyYear2.ClientID%>').value != '0' ||document.getElementById('<%=txtQtyYear1.ClientID%>').value != '') {
                                            alert("Available QTY of year 2 still peding to storage vats");
                                            document.getElementById("<% =txtQtyYear1.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyYear3.ClientID%>').value != '0' document.getElementById('<%=txtQtyYear1.ClientID%>').value != '') {
                                            alert("Available QTY of year 3 still peding to storage vats");
                                            document.getElementById("<% =txtQtyYear1.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt10.ClientID%>').value == '') {
                                            alert("enter value of (sl:10)");
                                            document.getElementById("<% =txt10.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt11.ClientID%>').value == '') {
                                            alert("enter value of (sl:11)");
                                            document.getElementById("<% =txt11.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt12.ClientID%>').value == '') {
                                            alert("enter value of (sl:12)");
                                            document.getElementById("<% =txt12.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt13.ClientID%>').value == '') {
                                            alert("enter value of (sl:13)");
                                            document.getElementById("<% =txt13.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txt14a.ClientID%>').value == '') {
                                            alert("enter Year of (sl:14(A))");
                                            document.getElementById("<% =txt14a.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt14b.ClientID%>').value == '') {
                                            alert("enter value of (sl:14(B))");
                                            document.getElementById("<% =txt14b.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txt15.ClientID%>').value == '') {
                                            alert("enter value of (sl:15)");
                                            document.getElementById("<% =txt15.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                    function DistMsg() {

                                        if (document.getElementById('<%=ddDistilleryName.ClientID%>').value == 'Select') {
                                            alert("Select Distillery Name");
                                            document.getElementById("<% =ddDistilleryName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddYear.ClientID%>').value == 'Select') {
                                            alert("Select Year");
                                            document.getElementById("<% =ddYear.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtQuantity.ClientID%>').value == '') {
                                            alert("Enter QTY");
                                            document.getElementById("<% =txtQuantity.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                    function OtherMsg() {

                                        if (document.getElementById('<%=txtDistilleryName.ClientID%>').value == '') {
                                            alert("Enter Distillery Name");
                                            document.getElementById("<% =txtDistilleryName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=OtherYear.ClientID%>').value == 'Select') {
                                            alert("Select Year");
                                            document.getElementById("<% =OtherYear.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtQuantity2.ClientID%>').value == '') {
                                            alert("Enter QTY");
                                            document.getElementById("<% =txtQuantity2.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                    function StorageMsg() {

                                        if (document.getElementById('<%=ddlTankName.ClientID%>').value == 'Select') {
                                            alert("Select TankName");
                                            document.getElementById("<% =ddlTankName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddStorageYear.ClientID%>').value == 'Select') {
                                            alert("Select Year");
                                            document.getElementById("<% =ddStorageYear.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtStorageQuantity.ClientID%>').value == '') {
                                            alert("Enter QTY");
                                            document.getElementById("<% =txtStorageQuantity.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                    function CheckDuplicates() {
                                        debugger;
                                        var jsondata = JSON.stringify($('#BodyContent_partycode').val() + "_" + $('#BodyContent_ddlMaterial').val() + "_" + $('#BodyContent_txtFinancialYear').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "MolassesProvisionalProductionForm.aspx/CheckDuplicates",
                                            data: '{value:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (msg.d == "DataExist") {

                                                    alert("Data exist fror this " + $('#BodyContent_txtFinancialYear').val() + " fiscal year, please select different material");
                                                    $('#BodyContent_ddlMaterial').val("");
                                                    $('#BodyContent_ddlMaterial').focus();
                                                }

                                            }
                                        });
                                    }
                                    
                                </script>
                                <script type="text/javascript">
                                    function SelectDate(sender, args) {
                                       
                                       // var dateOfBirth = e.get_selectedDate();
                                        var todayDate = sender.get_selectedDate();
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
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                    </script>
                                    <script type="text/javascript">
                                    function Calcutate() {
                                        debugger;
                                        var total = 0.0;

                                        var gv = document.getElementById("<%= GridView1.ClientID %>");
                                        var tb = gv.getElementsByTagName("input");
                                        var sub = 0;
                                      
                                        var indexQ = 1;
                                        var indexP = 0;

                                        for (var i = 0; i < tb.length; i++) {
                                            if (tb[i].type == "text") {
                                                sub = parseFloat(tb[i].value);
                                                if (isNaN(sub)) {
                                                    // lb[i + indexQ].innerHTML = "";
                                                    sub = 0;
                                                }
                                                total =parseFloat(total)+sub;
                                            }
                                        }

                                        $('#BodyContent_GridView1_lblTotal').text(parseFloat(total).toFixed(2));
                                        $('#BodyContent_storagetotalcapacity').val(parseFloat(total).toFixed(2));
                                    }
                                    $(document).ready(function () {
                                        Calcutate();
                                        debugger;
                                        if( $('#BodyContent_txtDATE').val()=="");
                                        $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                    });
                                    function Get8Values() {

                                        var year1 = $('#BodyContent_txtYear21').val();
                                        if ($('#BodyContent_txtYear21').val() == "") {

                                            $('#BodyContent_txtYear21').focus();
                                        }
                                        var year2 = $('#BodyContent_txtYear22').val();
                                        if ($('#BodyContent_txtYear22').val() == "") {

                                            $('#BodyContent_txtYear22').focus();
                                        }
                                        var year3 = $('#BodyContent_txtYear23').val();
                                        if ($('#BodyContent_txtYear23').val() == "") {

                                            $('#BodyContent_txtYear23').focus();
                                        }
                                       
                                        $('#BodyContent_txtQtyYear1').val(year1);
                                        $('#BodyContent_txtQtyYear2').val(year2);
                                        $('#BodyContent_txtQtyYear3').val(year3);
                                    }
                                    function GetBalnceQTY() {

                                        if ($('#BodyContent_lbl8Year1').text() == $('#BodyContent_ddStorageYear').val()) {
                                            $('#BodyContent_txtStorageQuantity').val($('#BodyContent_txtQtyYear1').val());
                                        }
                                        if ($('#BodyContent_lbl8Year2').text() == $('#BodyContent_ddStorageYear').val()) {
                                            $('#BodyContent_txtStorageQuantity').val($('#BodyContent_txtQtyYear2').val());
                                        }
                                        if ($('#BodyContent_lbl8Year3').text() == $('#BodyContent_ddStorageYear').val()) {
                                            $('#BodyContent_txtStorageQuantity').val($('#BodyContent_txtQtyYear3').val());
                                        }
                                        $('#BodyContent_StorageQuantity').val($('#BodyContent_txtStorageQuantity').val());
                                        if ($('#BodyContent_txtStorageQuantity').val() == "") {
                                            alert("Check QTY");
                                            $('#BodyContent_ddStorageYear').focus();
                                        }
                                    }
                                    function CheckNumber(number) {
                                       
                                       
                                        x = number.value.split('.');
                                        x1 = x[0];
                                      
                                        if(x[0].length > 7) 
                                        {
                                            alert("Invalid Number");
                                            number.value="";
                                            number.focus();

                                        }
                                        else
                                        {
                                            if(x.length>1)
                                                {
                                                if(x[0].length > 7) 
                                                {
                                                    alert("Invalid Number");
                                                    number.value="";
                                                    number.focus();
                                                }
                                                    }

                                        }
                                        
                                       
                                    }
                                </script>

                            </head>
                            <body>

                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <asp:LinkButton ID="MF2" OnClick="MF2_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Provisional (MF2)</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="MF3" OnClick="MF3_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Actual (MF3)</span></asp:LinkButton></li>
                                </ul>
                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Molasses Provisional Production Form</h2>
                                    <div class="clearfix"></div>
                                   
                                </div>

                                <div class="x_content">
                                    <div>
                                    <asp:HiddenField ID="partycode" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                  
                                            <table>
                                                <tr>

                                                    <td>Financial Year<span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinancialYear" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="FinancialYear" ReadOnly="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Date<span style="color: red;">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtDate1" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Date" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Type<span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtType" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Type" ReadOnly="true"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Captive/Non Captive <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCaptive" runat="server" data-toggle="tooltip" CssClass="form-control" onchange="GetUnitName()" data-placement="right" title="Captive">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td>Material<span style="color: red;">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMaterial" onchange="CheckDuplicates()" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Material">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>1.On what date your factory will start cane crushing for the next season. <span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                                       
                                        <asp:TextBox ID="txtDATE" onchage="SelectDate"  data-toggle="tooltip" data-placement="right" title="Indent Date" Cssclass="form-control validate[required]" AutoComplete="off" ReadOnly="true" runat="server" >
                                        </asp:TextBox>
                                                </td>
                                                <td>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                             </td>
                                                <td>
                                                     <asp:HiddenField ID="txtdob" runat="server" />
												</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>2.(a)What is your anticipated production of molasses for the next season. <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtmolassesproduction" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="CheckNumber(this)" data-toggle="tooltip" data-placement="right" MaxLength="10" title="Molasses production for next season"></asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">(b)What is the anticipated production of sugar for next season.<span style="color: red">*</span><br />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtsugarproduction" CssClass="form-control" runat="server" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="CheckNumber(this)" data-toggle="tooltip" data-placement="right" title="Sugar production for next season"></asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">(c)Quantity of molasses which may be produced daily. <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtQtyMproduceddaily" CssClass="form-control" runat="server" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="CheckNumber(this)" data-toggle="tooltip" data-placement="right" title="Quantity of molasses which may be produced daily"></asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">(d)Quantity of sugar to be produced daily. <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtQtySproduceddaily" CssClass="form-control" runat="server" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="CheckNumber(this)" data-toggle="tooltip" data-placement="right" title="Quantity of sugar to be produced daily"></asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                            </table>
                                        
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div>
                                        <h2 style="font: bolder; font-size: medium;">3.What is Your Total Storage Capacity</h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div style="width: 100%">
                                        <asp:GridView ID="GridView1" runat="server"
                                            HeaderStyle-BackColor="#26b8b8" ShowFooter="true"
                                            HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Data Captured" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="vat code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvat_code" runat="server" Text='<%# Eval("vat_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 
                                                <asp:TemplateField HeaderText="Tank Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvat_name" runat="server" Text='<%# Eval("vat_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div>
                                                            <asp:Label Text="Total" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" />
                                                        </div>

                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tank Capacity" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Style="text-align: right" onchange="Calcutate()" class="calculate" Text='<%# Eval("vat_totalcapacity") %>' runat="server" MaxLength="12" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <div>
                                                            <asp:Label ID="lblTotal" ItemStyle-Font-Bold="true" Font-Bold="true" Width="100px" runat="server" />
                                                        </div>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="10px"></FooterStyle>

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="storagetotalcapacity" runat="server" />
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <table>
                                        <tr>
                                            <td class="auto-style1">4.How much of the new production will be stored in    <span style="color: red">*</span></td>
                                            <td>
                                                <asp:TextBox ID="txtproductionstoredin" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="How much of the new production will be stored in" MaxLength="10" onchange="CheckNumber(this)"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox></td>
                                            <td>Qtls
                                            </td>
                                        </tr>
                                    </table>
                                    <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                    <table>
                                        <tr>
                                            <td class="auto-style1">5.Can Loading of Molasses be taken up while cane crushing is going on in your Factory?if so how many tank wagons can be loading daily within free time allowed? if from what date loading of Molasses can be taken up at your factory?    <span style="color: red">*</span></td>
                                            <td style="width:40%">
                                                <asp:TextBox ID="txtLoadingofMolasses" CssClass="form-control" runat="server" data-toggle="tooltip" Width="100%" data-placement="right" title="Molasses loading at factory" TextMode="MultiLine" MaxLength="100" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <h2 style="font: bolder; font-size: medium;">6.What is the actual and final Production of Molasses of previous three Season(Qtls)</h2>
                                    <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:Label runat="server" Text="Year1" ID="Year1" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                        <asp:TextBox ID="txtYear21" CssClass="form-control" runat="server"  onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="10" onchange="CheckNumber(this);Get8Values()" data-toggle="tooltip" data-placement="right" title="Year1"></asp:TextBox>Qtls
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:Label runat="server" Text="Year2" ID="Year2" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                        <asp:TextBox ID="txtYear22" CssClass="form-control" runat="server"  onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="10" onchange="CheckNumber(this);Get8Values()"  data-toggle="tooltip" data-placement="right" title="Year2"></asp:TextBox>Qtls
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:Label runat="server" Text="Year3" ID="Year3" Font-Bold="true" CssClass="control-label" />
                                        <br />
                                        <asp:TextBox ID="txtYear23" CssClass="form-control" runat="server"  onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="10" onchange="CheckNumber(this);Get8Values()" data-toggle="tooltip" data-placement="right" title="Year2"></asp:TextBox>Qtls
                                    </div>

                                    <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                       <div>
                                        <h2 style="font: bolder; font-size: medium;">7.(a) What quantity of Molasses out of the quantity quoted in column 6 has been delivered(figure for each year to be noted separately)</h2>
                                        <div class="clearfix"></div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <asp:Label runat="server" Text="Distillery Name" ID="lblDistillery" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                            <asp:DropDownList ID="ddDistilleryName" runat="server" data-toggle="tooltip" data-placement="right" title="DistilleryName" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <asp:Label runat="server" Text="Year" ID="lblYear" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                            <asp:DropDownList ID="ddYear" runat="server" data-toggle="tooltip" data-placement="right" title="Year" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <asp:Label runat="server" Text="Quantity" ID="lblQTY" Font-Bold="true" CssClass="control-label" /><br />
                                            <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" MaxLength="10" onchange="CheckNumber(this)" onkeypress="return onlyDotsAndNumbers(this,event);" Text="0" title="QTY "></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Total </label>
                                            <br />

                                            <asp:TextBox ID="txtDistillery" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Total"></asp:TextBox>
                                        </div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>

                                        <div class="col-md-9 col-sm-9 col-xs-12">

                                            <asp:LinkButton runat="server" ID="btnDistAdd" OnClientClick="javascript:return DistMsg();" CssClass="btn btn-primary" OnClick="AddDist">  
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >ADD</i></asp:LinkButton>
                                        </div>

                                        <p>&nbsp;</p>

                                        <div class="clearfix"></div>

                                        <div id="dummygrdDistlleries" runat="server" style="height: auto; width: 100%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                            <table class="table table-striped responsive-utilities jambo_table" id="dummytable12">
                                                <thead>
                                                    <tr>
                                                        <th>Distillery Name</th>
                                                        <th>Year</th>
                                                        <th>QTY</th>
                                                        <th>Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="resourcetable2">
                                                </tbody>

                                            </table>
                                        </div>
                                        <div>
                                            <asp:GridView ID="grdDistlleries" runat="server"
                                                HeaderStyle-BackColor="#26b8b8"
                                                HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="party_code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParty_code" runat="server" Text='<%# Eval("party_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Distillery Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistilleryname" runat="server" Text='<%# Eval("party_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("delivered_year") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("delivered_qty") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("molasses_deliverydetail_id") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("molasses_deliverydetail_id") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnDistRemove_Click" />
                                                            <%--<asp:LinkButton ID="btnDistRemove" Text="Remove" CommandArgument='<%# Eval("party_code") %>' ForeColor="Black" runat="server" OnClick="btnDistRemove_Click" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                        </div>

                                       </div>
                                        <p>&nbsp;</p>
                                       <div class="clearfix"></div>
                                        <div>
                                            <h2 style="font: bolder; font-size: medium;">7.(b) Any Other Person  or Persons to whom alotment has been made</h2>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <asp:Label runat="server" Text="Distillery Name" ID="Label1" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                                <asp:TextBox ID="txtDistilleryName" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Distillery Name "></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">

                                                <asp:Label runat="server" Text="Year" ID="Label2" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                                <asp:DropDownList ID="OtherYear" runat="server" data-toggle="tooltip" data-placement="right" title="Year" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <asp:Label runat="server" Text="Quantity" ID="Label3" Font-Bold="true" CssClass="control-label" /><br />
                                                <asp:TextBox ID="txtQuantity2" CssClass="form-control" MaxLength="10" onchange="CheckNumber(this)" onkeypress="return onlyDotsAndNumbers(this,event);" runat="server" data-toggle="tooltip" data-placement="right" title="QTY "></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Total </label>
                                                <br />

                                                <asp:TextBox ID="txtOtherTotal" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Total"></asp:TextBox>
                                            </div>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>

                                            <div class="col-md-9 col-sm-9 col-xs-12">

                                                <asp:LinkButton runat="server" ID="btnOther" OnClientClick="javascript:return OtherMsg();" CssClass="btn btn-primary" OnClick="AddOther">  
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >ADD</i></asp:LinkButton>

                                            </div>

                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div id="dummyOthertable" runat="server" style="height: auto; width: 100%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                                <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                                    <thead>
                                                        <tr>
                                                            <th>Distillery Name</th>
                                                            <th>Year</th>
                                                            <th>QTY</th>
                                                            <th>Actions</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="resourcetable">
                                                    </tbody>

                                                </table>
                                            </div>
                                            <div>
                                                <asp:GridView ID="grdOther" runat="server"
                                                    HeaderStyle-BackColor="#26b8b8"
                                                    HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" Width="100%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Distillery Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistilleryname" runat="server" Text='<%# Eval("other_party") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%# Eval("delivered_year") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTY(BL)" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("delivered_qty") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("molasses_other_deliverydetail_id") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("molasses_other_deliverydetail_id") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnOtherRemove_Click" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                    <div>
                                        <h2 style="font: bolder; font-size: medium;">8.What quantity of Molasses of previous three season is still available with you</h2>

                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">

                                            <asp:Label runat="server" Text="Year1" ID="lbl8Year1" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                            <asp:TextBox ID="txtQtyYear1" CssClass="form-control" onchange="CheckNumber(this)" runat="server" data-toggle="tooltip" data-placement="right" title=""></asp:TextBox>Qtls
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <asp:Label runat="server" Text="Year2" ID="lbl8Year2"  Font-Bold="true" CssClass="control-label"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtQtyYear2" CssClass="form-control" onchange="CheckNumber(this)" runat="server" data-toggle="tooltip" data-placement="right" title=""></asp:TextBox>Qtls
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <asp:Label runat="server" Text="Year3" ID="lbl8Year3" Font-Bold="true" CssClass="control-label" />
                                            <br />
                                            <asp:TextBox ID="txtQtyYear3" CssClass="form-control" onchange="CheckNumber(this)" runat="server" data-toggle="tooltip" data-placement="right" title=""></asp:TextBox>Qtls
                                        </div>
                                      </div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <div>
                                            <h2 style="font: bolder; font-size: medium;">9.Where has the above stock stored in</h2>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <asp:Label runat="server" Text="Tank Name" ID="Label6" Font-Bold="true" CssClass="control-label" />
                                                <br />

                                                <asp:DropDownList ID="ddlTankName" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Tank Name"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <asp:Label runat="server" Text="Year" ID="lblStarageyear" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                                <asp:DropDownList ID="ddStorageYear" onchange="GetBalnceQTY()" runat="server" data-toggle="tooltip" data-placement="right" title="Year" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <asp:Label runat="server" Text="QTY" ID="Label5" Font-Bold="true" CssClass="control-label" /><br />
                                                <asp:TextBox ID="txtStorageQuantity" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="QTY "></asp:TextBox>
                                            </div>
                                            <asp:HiddenField ID="StorageQuantity" runat="server" />
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Total </label>
                                                <br />

                                                <asp:TextBox ID="txtStorageTotal" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Total"></asp:TextBox>
                                            </div>
                                        </div>
                                      
                                        <p>&nbsp;</p>
                                          <div class="clearfix"></div>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:LinkButton runat="server" ID="btnStorageAdd" OnClientClick="javascript:return StorageMsg();" CssClass="btn btn-primary" OnClick="AddStorage">  
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >ADD</i></asp:LinkButton>
                                        </div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <div id="dummyStoragetable" runat="server" style="height: auto; width: 100%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                            <table class="table table-striped responsive-utilities jambo_table" id="dummytable14">
                                                <thead>
                                                    <tr>
                                                        <th>Tank Name</th>
                                                        <th>Year</th>
                                                        <th>QTY</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="resourcetable4">
                                                </tbody>

                                            </table>
                                        </div>
                                        <div>
                                            <asp:GridView ID="grdStorage" runat="server"
                                                HeaderStyle-BackColor="#26b8b8"
                                                HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tank Name" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvat_code" runat="server" Text='<%# Eval("vat_code") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tank Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvatname" runat="server" Text='<%# Eval("vat_name") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("financial_year") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY(BL)" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("bal_capacity") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("molasses_prod_tank_storage_id") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("molasses_prod_tank_storage_id") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnStorageRemove_Click" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>10.Why has the Above stock not been cleared as key   <span style="color: red">*</span></td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="txt10" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="Why has the Above stock not been cleared as key" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>11.What arrangment are you going to make for storage of new crop if your tanks are full with old Molasses <span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt11" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="What arrangment are you going to make for storage of new crop if your tanks are full with old Molasses" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>12.Will you feel any storage difficulty if lifting of the new crop of molsses is deffered till the crushing season <span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt12" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="Will you feel any storage difficulty if lifting of the new crop of molsses is deffered till the crushing season" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>13.Have you repaired,cleaned your storage tanks which are empty <span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt13" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="50" title="Have repaired,cleaned your storage tanks which are empty " ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>14.What is the name and  address of yours (name individual who are resposible should be writen) <span style="color: red"></span></td>
                                                <td>
                                                   <asp:TextBox ID="txt14" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="What is the name and  address of yours (name individual who are resposible should be writen)" TextMode="MultiLine"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 2.8%">A)Name and Address of Occupier <span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt14a" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="Occupier" ></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td style="padding-left: 2.8%">B)Name and Address of Manager<span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt14b" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="Manager" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>15.Have you got a mechanical pump for loading of molasses <span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txt15" CssClass="form-control" runat="server" Height="10%" Width="95.5%" data-toggle="tooltip" data-placement="right" MaxLength="100" title="Have you got a mechanical pump for loading of molasses" ></asp:TextBox></td>
                                            </tr>
                                        </table>
                                        </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="Total7b" runat="server" />
                                        <asp:HiddenField ID="Total7a" runat="server" />
                                        <asp:HiddenField ID="storagetotal" runat="server" />
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return Validate0();" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return Validate0();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
