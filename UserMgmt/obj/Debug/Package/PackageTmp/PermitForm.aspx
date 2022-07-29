<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PermitForm.aspx.cs" Inherits="UserMgmt.PermitForm" %>



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
                                <title>permit</title>
                                <script type="text/javascript">
                                    function validationMsg() {
                                       <%-- if (document.getElementById('<%=txtpermitno.ClientID%>').value == '') {
                                            alert("Enter Permit No");
                                            document.getElementById("<% =txtpermitno.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        debugger;;
                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter  Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=ddlpermitType.ClientID%>').value == 'Select') {
                                            alert("Select Permit Type");
                                            document.getElementById("<% =ddlpermitType.ClientID%>").focus();
                                            return false;
                                        }
                                        <%--if (document.getElementById('<%=ddlLicnse.ClientID%>').value == 'Select') {
                                            alert("Select License");
                                            document.getElementById("<% =ddlLicnse.ClientID%>").focus();
                                            return false;
                                        }--%>
                                        if (document.getElementById('<%=ddlallotmentno.ClientID%>').value == 'Select') {
                                            alert("Select Allotment No");
                                            document.getElementById("<% =ddlallotmentno.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtpermitqty.ClientID%>').value == '') {
                                            alert("Enter Permit Qty");
                                            document.getElementById("<% =txtpermitqty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtagentname.ClientID%>').value == '') {
                                            alert("Enter Agent Name");
                                            document.getElementById("<% =txtagentname.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlDutyType.ClientID%>').value == 'Select') {
                                            alert("Select Duty Type");
                                            document.getElementById("<% =ddlDutyType.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlDutyType.ClientID%>').value == 'Y') {
                                            if (document.getElementById('<%=txtchallanno.ClientID%>').value == '') {
                                                alert("Enter Challan No");
                                                document.getElementById("<% =txtchallanno.ClientID%>").focus();
                                                return false;
                                            }

                                            if (document.getElementById('<%=txtreceiptdate.ClientID%>').value == '') {
                                                alert("Enter Challan Date");
                                                document.getElementById("<% =txtreceiptdate.ClientID%>").focus();
                                                return false;
                                            } 
                                            if (document.getElementById('<%=txtdutyamount.ClientID%>').value == '') {
                                                alert("Enter Duty Amount");
                                            document.getElementById("<% =txtdutyamount.ClientID%>").focus();
                                            return false;

                                            }
                                            if (document.getElementById('<%=txtdutyrate.ClientID%>').value == '') {
                                                alert("Enter Duty Rate");
                                            document.getElementById("<% =txtdutyrate.ClientID%>").focus();
                                            return false;

                                        }
                                            if (document.getElementById('<%=txttreasury.ClientID%>').value == '') {
                                                alert("Enter Treasury");
                                                document.getElementById("<% =txttreasury.ClientID%>").focus();
                                                return false;

                                            }

                                            if (document.getElementById('<%=txtroutechart.ClientID%>').value == '') {
                                                alert("Enter Route Chart");
                                                document.getElementById("<% =txtroutechart.ClientID%>").focus();
                                                return false;
                                            }

                                        }

                                        <%--if (document.getElementById('<%=txtreceiptqty.ClientID%>').value == '') {
                                            alert("Enter Receipt QTY");
                                            document.getElementById("<% =txtreceiptqty.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        <%--if (document.getElementById('<%=ddlUom.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                            document.getElementById("<% =ddlUom.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                            alert("Enter Vehicle No");
                                            document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                            return false;

                                        }

                                    }
                                </script>
                                <script type="text/javascript">
                                    function validationMsg2() {
                                        if (document.getElementById('<%=ddlproduct.ClientID%>').value == 'Select') {
                                             alert("Select  Product");
                                             document.getElementById("<% =ddlproduct.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtstrength.ClientID%>').value == '') {
                                             alert("Enter Strength");
                                             document.getElementById("<% =txtstrength.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtquantity.ClientID%>').value == '') {
                                             alert("Enter Quantity");
                                             document.getElementById("<% =txtquantity.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=ddluom.ClientID%>').value == 'Select') {
                                             alert("Select UOM ");
                                             document.getElementById("<% =ddluom.ClientID%>").focus();
                                             return false;

                                         }

                                     }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%=txtpass.ClientID%>').value == '') {
                                            alert("Enter Permit Validty");
                                            document.getElementById("<% =txtpass.ClientID%>").focus();
                                            return false;

                                        }
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
                                    function onlyDotsAndAlphabet(txt, event) {

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
                                        if ((charCode >= 65 && charCode <= 90) || charCode == 8)
                                            return true;

                                        return false;
                                    }
                                    function Validate(e) {
                                        var keyCode = e.keyCode || e.which;
                                        var regex = /^[a-zA-Z ]+$/;
                                        var isValid = regex.test(String.fromCharCode(keyCode));
                                        return isValid;
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
                                        $('#BodyContent_txtpass').val(todayDate);
                                        $('#BodyContent_txtdob1').val(todayDate);
                                    }
                                </script>

                                <script>
                                    $(document).ready(function () {

                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtgpd').val());
                                        }

                                        debugger;
                                        if ($('#BodyContent_txtpass').val() == "") {
                                            $('#BodyContent_txtpass').val($('#BodyContent_txtdob1').val());
                                        }
                                        debugger;
                                        if ($('#BodyContent_txtreceiptdate').val() == "") {
                                            $('#BodyContent_txtreceiptdate').val($('#BodyContent_receipt').val());
                                        }
                                        Calcutate();
                                        //if ($('#BodyContent_txtNetWeight').val() == "" || $('#BodyContent_txtNetWeight').val() == "0") {
                                        //    $('#BodyContent_txtTransitWastage').val("");
                                        //}
                                    });
                                </script>

                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <%--                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>

                                        <%--<li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>

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
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>--%>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="lnkShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>

                                <div class="x_title">
                                    <h2>Permit</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

                                    <div id="Div1" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit No</label><br />
                                        <asp:TextBox ID="txtpermitno" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" onkeypress="return onlyDotsAndNumbers(this,event);" title="Permit No"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate3" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" data-toggle="tooltip" data-placement="right" title="Date" class="form-control" AutoComplete="off" ReadOnly="true" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtgpd" runat="server" />

                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit Type </label>
                                        <br />
                                        <asp:DropDownList ID="ddlpermitType" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Supplier Type" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Out of State" Value="O"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="within state" Value="w"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>License Application</label><br />
                                        <asp:DropDownList ID="ddlLicnse" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlLicnse_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="Div3" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>License </label>
                                        <br />
                                        <asp:TextBox ID="txtlicense" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div id="Div11" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Fee Amount</label><br />
                                        <asp:TextBox ID="txtFeeAmount" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div id="Div4" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Start Date</label><br />
                                        <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title="" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div id="Div5" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>End Date</label><br />
                                        <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Allotment No</label><br />
                                        <asp:DropDownList ID="ddlallotmentno" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlallotmentno_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="releaserequestid" runat="server" />

                                    </div>
                                    <div id="Div2" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Purchase From Party</label><br />
                                        <%-- <asp:TextBox ID="txtpurchaseparty" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title=" Supplier Name"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlpurchasedparty" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Supplier Name" Width="60%" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlpurchasedparty_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div runat="server" class="col-md-3 col-sm-2 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Purchase District</label><br />
                                        <asp:TextBox ID="txtdistrict" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" ReadOnly="true"></asp:TextBox>
                                        <%-- <asp:DropDownList ID="ddlpurchasedistrict" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Supplier Name" Width="60%" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                    </div>
                                    <div runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Product</label><br />
                                        <asp:TextBox ID="txtproduct" class="calculate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Available Allocation Quantity</label><br />
                                        <asp:TextBox ID="txtaaqty" Style="text-align: right" class="calculate" runat="server" CssClass="form-control" ReadOnly="true" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    <div runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit Quantity</label><br />
                                        <asp:TextBox ID="txtpermitqty" Style="text-align: right" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtpermitqty_TextChanged"></asp:TextBox>
                                    </div>



                                    <div id="Div6" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Agent Name</label><br />
                                        <asp:TextBox ID="txtagentname" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return Validate(event);" data-placement="right" Width="60%" title="Agent Name"></asp:TextBox>
                                    </div>
                                    <div runat="server" id="validity" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Permit Validity</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image5" TargetControlID="txtpass" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectSetupDate" ID="CalendarExtender4"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtpass" data-toggle="tooltip" data-placement="right" title="Permit Validity" class="form-control validate[required]" AutoComplete="off" ReadOnly="true" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image5" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob1" runat="server" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Duty Type</label><br />
                                            <asp:DropDownList ID="ddlDutyType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDutyType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Duty Paid" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="Duty Not Paid" Value="N"></asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>

                                    <div id="Div10" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Challan No</label><br />
                                        <asp:TextBox ID="txtchallanno" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" Width="60%" title="Challan No"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Challan Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtreceiptdate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectRemovalDate" ID="CalendarExtender3"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtreceiptdate" data-toggle="tooltip" data-placement="right" title="Receipt Date" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="receipt" runat="server" />

                                    </div>

                                    <div id="Div7" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Duty Amount</label><br />
                                        <asp:TextBox ID="txtdutyamount" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" Width="60%" title=" Supplier Name"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="Div8" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Duty Rate</label><br />
                                        <asp:TextBox ID="txtdutyrate" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" Width="60%" title="Duty Rate" MaxLength="3"></asp:TextBox>
                                    </div>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Treasury</label><br />

                                        <asp:TextBox ID="txttreasury" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return Validate(event);" data-toggle="tooltip" data-placement="right" title="Treasury"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="Div12" runat="server" class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Route Chart</label><br />
                                        <asp:TextBox ID="txtroutechart" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Height="50px" Width="90%" title="Route Chart" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div id="Div13" visible="false" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Supplier Name</label><br />
                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title=" Supplier Name"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Supplier Name</label><br />
                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" Width="60%" title=" Supplier Name"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" runat="server" class="form-control" name="size" data-toggle="tooltip" data-placement="right" title="Remarks"></textarea>
                                    </div>
                                    <%--  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Receipt No</label><br />
                                        <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Receipt No"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Receipt Qty</label><br />

                                        <asp:TextBox ID="txtreceiptqty" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Receipt Qty" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>UOM</label><br />
                                        <asp:DropDownList ID="ddlUom" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="UOM"></asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Vehicle No( In case of multiple vehicles please mention "NA")</label><br />

                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Vehicle No"></asp:TextBox>
                                    </div>--%>

                                    <div runat="server" id="permititme">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Product</label><br />
                                            <asp:DropDownList ID="ddlproduct" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Strength</label><br />

                                            <asp:TextBox ID="txtstrength" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Bulk Liters" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Quantity</label><br />
                                            <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="LP Liters"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>UOM</label><br />
                                            <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>



                                        <%-- <div class="clearfix"></div>
                                            <p>&nbsp;</p>--%>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span></label>
                                            <br />
                                            <asp:Button ID="btnadd1" runat="server" CssClass="btn btn-upload" Width="50%" OnClientClick="javascript:return validationMsg2();" Text="ADD" OnClick="Add" />
                                        </div>
                                    </div>

                                    <div id="table" runat="server">
                                        <div id="dummy" runat="server" style="height: auto; width: 70%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                            <table class="table table-striped responsive-utilities jambo_table" id="Dat">
                                                <thead>
                                                    <tr>
                                                        <th>Product</th>
                                                        <th>Strength</th>
                                                        <th>Quantity</th>
                                                        <th>UOM</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="Datatabl">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline" runat="server" visible="false">
                                        <asp:GridView ID="grdpermit" runat="server" AutoGenerateColumns="false" Width="100%"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct" runat="server" Visible="false" Text='<%#Eval("product_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductname" runat="server" Visible="true" Text='<%#Eval("product_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Strength" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStrength" runat="server" Visible="true" Text='<%#Eval("strength") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%#Eval("req_qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOMname" runat="server" Visible="true" Text='<%#Eval("uom_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Visible="true" Text='<%#Eval("uom_code") %>'></asp:Label>
                                                    </ItemTemplate>
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
                                                        <asp:ImageButton ID="ImageButton1" CommandName="Remove" CommandArgument='<%#Eval("Doc_id") %>' ImageUrl="~/img/delete.gif" runat="server" OnClick="ImageButton1_Click" />&nbsp;
                                                     <%--     <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton> --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                            <FooterStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" />
                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>

                                        </asp:GridView>
                                    </div>
                                    <%--<div style="height: 8%; background-color: #26b8b8;">
                                      <div class="x_title"> 
                                           <div class="clearfix"></div>
                                      </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <span style="font-size: small; color: white; margin-left: 40%"></span>
                                    </div>--%>
                                    &nbsp;
                                    <div class="clearfix"></div>
                                    <div runat="server" visible="false">
                                        <asp:GridView ID="grdrawmaterial" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" ShowFooter="true" OnRowDataBound="grdrawmaterial_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" ItemStyle-Width="1px" Visible="true">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblVatCode" runat="server" Visible="false" Text='<%#Eval("vat_code") %>'></asp:Label>--%>
                                                        <asp:DropDownList ID="ddlproduct" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div>
                                                            <asp:Label Text="Total" Style="text-align: right" ItemStyle-Font-Bold="true" Font-Bold="true" runat="server" />
                                                        </div>

                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="storageid" ItemStyle-Font-Bold="true" ItemStyle-Width="1px" Visible="false">
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
                                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Style="text-align: right" onchange="Calcutate()" class="calculate" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div>
                                                            <asp:Label ID="lblTotal" ItemStyle-Font-Bold="true" Font-Bold="true" Width="100px" runat="server" />
                                                        </div>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="1px" Visible="true">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblVatCode" runat="server" Visible="false" Text='<%#Eval("vat_code") %>'></asp:Label>--%>
                                                        <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Strength" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtStrength" Style="text-align: right" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dips in CM" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Width="15%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtdips" runat="server" Style="text-align: right" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
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
                                    <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Approver Comments</label><br />
                                        <textarea type="text" id="txtapproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                             <asp:HiddenField ID="hdfinancialyear" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                                CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveAs_Click">
                                                       Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        </div>
                                    </div>
                                    <p>&nbsp;</p>
                                    <div id="approverid" runat="server">

                                        <div class="x_title">
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
