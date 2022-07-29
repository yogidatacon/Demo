<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MF3Form.aspx.cs" Inherits="UserMgmt.MF3Form" %>

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
                                 <script type="text/javascript" src="/common/theme/js/flot/date.js"></script>
                                <title>Molasses Production Actual</title>
                                <script>
                                    function onlyDotsAndNumbers(txt, event) {
                                        debugger;
                                        var charCode = (event.which) ? event.which : event.keyCode              <a href="MF3Form.aspx">MF3Form.aspx</a>
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
                                    function CheckNumber(number) {


                                        x = number.value.split('.');
                                        x1 = x[0];

                                        if (x[0].length > 7) {
                                            alert("Invalid Number");
                                            number.value = "";
                                            number.focus();

                                        }
                                        else {
                                            if (x.length > 1) {
                                                if (x[0].length > 7) {
                                                    alert("Invalid Number");
                                                    number.value = "";
                                                    number.focus();
                                                }
                                            }

                                        }


                                    }
                                </script>
                                 <script type="text/javascript">
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
                                        $('#BodyContent_txtDate').val(todayDate);
                                        var date1 = $('#BodyContent_txtDate').val();
                                        $('#BodyContent_txtdob').val(date1);
                                    }
                                    </script>
                                <script type="text/javascript">
                                    function validationMsg()
                                    {
                                        if (document.getElementById('<%=ddlMaterial.ClientID%>').value == 'Select')
                                        {
                                            alert("Select  Material");
                                            document.getElementById("<% =ddlMaterial.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtDate.ClientID%>').value == '')
                                        {
                                            alert("Enter Date");
                                            document.getElementById("<% =txtDate.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtTotalCrushingqty.ClientID%>').value == '')
                                        {
                                            alert("Enter  Total Crushing Qty ");
                                            document.getElementById("<% =txtTotalCrushingqty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtTotalSugarqty.ClientID%>').value == '')
                                        {
                                            alert("Enter  Total Crushing Qty of Sugar ");
                                            document.getElementById("<% =txtTotalSugarqty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtproducedqty.ClientID%>').value == '')
                                        {
                                            alert("Enter Produced Qty of Molasses");
                                            document.getElementById("<% =txtproducedqty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtTotalLifted.ClientID%>').value == '')
                                        {
                                            alert("Enter Total Lifted Qty");
                                            document.getElementById("<% =txtTotalLifted.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtRow6.ClientID%>').value == '')
                                        {
                                            alert("Enter row6");
                                            document.getElementById("<% =txtRow6.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtrow7.ClientID%>').value == '')
                                        {
                                            alert("Enter  row7");
                                            document.getElementById("<% =txtrow7.ClientID%>").focus();
                                            return false;
                                        }

                                    }
                                </script>
                                <script  type="text/javascript">
                                    function Validate0() {

                                        if (document.getElementById('<%=ddlStorageTankname.ClientID%>').value == '')
                                        {
                                            alert("Select   Tanker name");
                                            document.getElementById("<% =ddlStorageTankname.ClientID%>").focus();
                                            return false;
                                        }
                                        var 
                                        if (document.getElementById('<%=txtStorageQTY.ClientID%>').value == '') {
                                            alert("Enter  QTY");
                                            document.getElementById("<% =txtStorageQTY.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                    
                                </script>
                                <script>
                                    $(document).ready(function () {
                                        Calcutate();
                                    });
                                </script>
                                <script type="text/javascript">
         
    function getConfirmation() {

        //var retVal = prompt("Record cannot be changed after submission \n \n Confirm Password to Submit : ", "your Password here");
        //if (retVal != null) {
            if (document.getElementById('<%=ddlMaterial.ClientID%>').value == 'Select') {
                alert("Select  Material");
                document.getElementById("<% =ddlMaterial.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtDate.ClientID%>').value == '') {
                alert("Enter Date");
                document.getElementById("<% =txtDate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById('<%=txtTotalCrushingqty.ClientID%>').value == '') {
                alert("Enter  Total Crushing Qty ");
                document.getElementById("<% =txtTotalCrushingqty.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtTotalSugarqty.ClientID%>').value == '') {
                alert("Enter  Total Crushing Qty of Sugar ");
                document.getElementById("<% =txtTotalSugarqty.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtproducedqty.ClientID%>').value == '') {
                alert("Enter Produced Qty of Molasses");
                document.getElementById("<% =txtproducedqty.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtTotalLifted.ClientID%>').value == '') {
                alert("Enter Total Lifted Qty");
                document.getElementById("<% =txtTotalLifted.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtRow6.ClientID%>').value == '') {
                alert("Enter row6");
                document.getElementById("<% =txtRow6.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=txtrow7.ClientID%>').value == '') {
                alert("Enter  row7");
                document.getElementById("<% =txtrow7.ClientID%>").focus();
                return false;
            }
            return true;
        //}
        //else {
           
        //    return false;
        //}
    }
   
                                </script>
                                <script type="text/javascript">

                                    function Calcutate() {
                                        debugger;
                                        var total = 0;

                                        var gv = document.getElementById("<%= grdstorage.ClientID %>");
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


                                            }
                                        }

                                        $('#BodyContent_grdstorage_lblTotal').text(total);
                                        $('#BodyContent_actualstoragetotal').val(total);
                                        //var producedqty= parseFloat(  $('#BodyContent_txtproducedqty').val());
                                        //var TotalLifted= parseFloat(  $('#BodyContent_txtTotalLifted').val());
                                        //var total1=parseFloat(producedqty)- parseFloat(TotalLifted); 
                                        //debugger;
                                        //if(total1<total)
                                        //{
                                        //    alert("Not Mothan of (sl:3 - sl:4) Available Quantity!");
                                        //}
                                    }
                                    function CheckDuplicates() {
                                        var jsondata = JSON.stringify($('#BodyContent_partycode').val() + "_" + $('#BodyContent_ddlMaterial').val() + "_" + $('#BodyContent_txtFinancialYear').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "MF3Form.aspx/CheckDuplicates",
                                            data: '{value:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (msg.d == "DataExist") {
                                                    debugger;
                                                    alert("Data exist fror this " + $('#BodyContent_txtFinancialYear').val() + " fiscal year, please select different material");
                                                    $('#BodyContent_ddlMaterial').val("");
                                                    $('#BodyContent_ddlMaterial').focus();
                                                }

                                            }
                                        });
                                    }
                                </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li>
                                        <asp:LinkButton ID="MF2" OnClick="MF2_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Provisional (MF2)</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="MF3" OnClick="MF3_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Actual (MF3)</span></asp:LinkButton></li>
                                </ul>
                                <br />

                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Molasses Production Actual</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   
                                            <asp:HiddenField ID="total1" runat="server" />
                                            <asp:HiddenField ID="actualstoragetotal" runat="server" />
                                            <asp:HiddenField ID="partycode" runat="server" />
                                            <table>
                                                <tr>
                                                    <td>Financial Year<span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinancialYear" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="FinancialYear" ReadOnly="false"></asp:TextBox>
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
                                                    <td>1.Date of closing of the crushing operation
                                                <asp:Label runat="server" ID="lbldateyear" class="control-label"></asp:Label><span style="color: red;">*</span></td>
                                                    <td>
                                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                        <asp:TextBox ID="txtDate" data-toggle="tooltip" data-placement="right" title="Date of closing of the crushing operation" class="form-control validate[required]" AutoComplete="off" runat="server" >
                                                        </asp:TextBox></td>
                                                    <td>
                                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                        <asp:HiddenField ID="txtdob" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>2.Total cane crushing during the season
                                                <asp:Label runat="server" ID="lblSeasonyear" class="control-label" />
                                                        <span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalCrushingqty" runat="server" data-toggle="tooltip" data-placement="right" title="Total cane crushing during the season" onchange="CheckNumber(this)" onkeypress="return isNumberKeyy(event,this)" MaxLength="10" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>2a.Final Quantity of Sugar produced during the season<asp:Label runat="server" ID="lblSugarYear" class="control-label" />
                                                        <span style="color: red">*</span><br />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalSugarqty" runat="server" data-toggle="tooltip" data-placement="right" title="Final Quantity of Sugar produced during the season" onchange="CheckNumber(this)" onkeypress="return isNumberKeyy(event,this)" MaxLength="10" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>3.Final Quantity of Molasses produced during the season
                                                <asp:Label runat="server" ID="lblSeasonyear1" class="control-label" />
                                                        <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtproducedqty" runat="server" data-toggle="tooltip" data-placement="right" title="Final Quantity of Molasses produced during the season" onchange="CheckNumber(this)" onkeypress="return isNumberKeyy(event,this)" MaxLength="10" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>4.Quantity lifted after closure of crushing up to
                                                <asp:Label runat="server" ID="lblliftedDate" class="control-label" />
                                                        of
                                                <asp:Label runat="server" ID="lblLiftedyear1" class="control-label" />
                                                        production <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalLifted" runat="server" title="Quantity lifted after closure of crushing up to of production" data-toggle="tooltip" data-placement="right" onchange="CheckNumber(this)" onkeypress="return isNumberKeyy(event,this)" MaxLength="10" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>Qtls</td>
                                                </tr>
                                            </table>
                                     
                                            <h2 style="font: bolder; font-size: 14px;">5.Balance quantity still available and stored as detailed below as on
                                        <label id="lblavailableyear" class="control-label" />
                                            </h2>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div id="tankers">
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <asp:Label runat="server" Text="Tanker Name" ID="Label6" Font-Bold="true" CssClass="control-label" /><br />
                                                    <asp:DropDownList ID="ddlStorageTankname" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="StorageTankname" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">

                                                <asp:Label runat="server" Text="Year" ID="lblStarageyear" Font-Bold="true" CssClass="control-label"></asp:Label><br />
                                            <asp:DropDownList ID="ddStorageYear" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Year" CssClass="form-control">
                                            </asp:DropDownList>
                                            </div>--%>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <asp:Label runat="server" Text="QTY" ID="Label5" Font-Bold="true" CssClass="control-label" /><br />
                                                    <asp:TextBox ID="txtStorageQTY" runat="server" data-toggle="tooltip" data-placement="right" title="QTY" CssClass="form-control" MaxLength="10" onchange="CheckNumber(this)" onkeypress="return onlyDotsAndNumbers(this,event);">
                                                    </asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red"></span>Total </label>
                                                    <br />
                                                    <asp:TextBox ID="txtToalCapacity" runat="server" data-toggle="tooltip" title="Total" data-placement="right" CssClass="form-control" ReadOnly="true">
                                                    </asp:TextBox>
                                                    <asp:HiddenField ID="storagetotal" runat="server" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <asp:LinkButton runat="server" ID="btnAdd" OnClientClick="javascript:return Validate0();" CssClass="btn btn-primary" OnClick="btnAdd_Click">  
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >ADD</i></asp:LinkButton>
                                            </div>
                                            <p>&nbsp;</p>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
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
                                                <asp:GridView ID="grdstorage" runat="server"
                                                    HeaderStyle-BackColor="#26b8b8"
                                                    HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Data Captured" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tank Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvatname" runat="server" Text='<%# Eval("vat_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <FooterTemplate>
                                                                <div>
                                                                    <asp:Label Text="Total" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                             <footerstyle horizontalalign="left" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblQTY" Style="text-align: right" title="QTY" onchange="Calcutate()" class="calculate" Text='<%# Eval("bal_capacity") %>' runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%-- <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblTotal" ItemStyle-Font-Bold="true" Font-Bold="true" Width="100px" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="vat_code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvatcode" runat="server" Text='<%# Eval("vat_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("molasses_prod_tank_storage_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("molasses_prod_tank_storage_id") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="lnkRemove_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                    <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="10px"></FooterStyle>

                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                </asp:GridView>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <table>
                                                <tr>
                                                    <td>6.Have you got arrangement for loading of molasses stored in uncovered tank and pits into tank wagons? if so ,how many wagons may be loaded for a day  <span style="color: red">*</span><br />
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:TextBox ID="txtRow6" runat="server" data-toggle="tooltip" title="how many wagons may be loaded for a day" data-placement="right" Width="90%" MaxLength="100" CssClass="form-control" TextMode="MultiLine">
                                                        </asp:TextBox>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>7.What arrangements are being made to prevent Molasses stored in open pits from mixing with rain water and deterioration due to natural causes <span style="color: red">*</span></td>
                                                    <td style="width: 50%">
                                                        <asp:TextBox ID="txtrow7" runat="server" data-toggle="tooltip" data-placement="right" Width="90%" tittle="What arrangements are being made" MaxLength="100" CssClass="form-control" TextMode="MultiLine">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return getConfirmation();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
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
