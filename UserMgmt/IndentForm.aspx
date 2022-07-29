<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="IndentForm.aspx.cs" Inherits="UserMgmt.IndentForm" %>

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
                                <title>Indent For Molasses Form</title>
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
                                    function validationMsg() {


                                        if (document.getElementById('<%=txtIndentQty.ClientID%>').value == '') {
                                            alert("Enter Indent Quantity");
                                            document.getElementById("<% =txtIndentQty.ClientID%>").focus();
                                            return false;


                                        }
                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Select Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;


                                        }
                                        if (document.getElementById('<%=ddlCaptive.ClientID%>').value == 'Select') {
                                            alert("Select  Captive");
                                            document.getElementById("<% =ddlCaptive.ClientID%>").focus();
                                            return false;

                                        }
                                        debugger;
                                        if (document.getElementById('<%=ddUnitName.ClientID%>').value == 'Select' && document.getElementById('<%=ddlCaptive.ClientID%>').value == 'Y') {
                                            alert("Select UnitName");
                                            document.getElementById("<% =ddUnitName.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddMaterial.ClientID%>').value == 'Select') {
                                            alert("Select Material");
                                            document.getElementById("<% =ddMaterial.ClientID%>").focus();
                                            return false;
                                        }
                                        var Total1 = $('#BodyContent_total1').val();
                                        var indentqty = parseFloat($('#BodyContent_txtIndentQty').val());
                                        if (Total1 != indentqty) {
                                            alert("Molasses Required Qty Should be Match with Indent QTY");
                                            $('#BodyContent_txtTotal1').focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtMolassesDistilled.ClientID%>').value == '') {
                                            alert("Enter Molasses Distilled");
                                            document.getElementById("<% =txtMolassesDistilled.ClientID%>").focus();
                                            return false;
                                        } 
                                        if (document.getElementById('<%=txtWorkingwastage.ClientID%>').value == '') {
                                            alert("Enter Workingwastage");
                                            document.getElementById("<% =txtWorkingwastage.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtTransitwastage.ClientID%>').value == '') {
                                            alert("Select Transitwastage");
                                            document.getElementById("<% =txtTransitwastage.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyfirstday.ClientID%>').value == '') {
                                            alert("Enter 1st day of January");
                                            document.getElementById("<% =txtQtyfirstday.ClientID%>").focus();
                                            return false;
                                        }  
                                        if (document.getElementById('<%=txtqty1of31.ClientID%>').value == '') {
                                            alert("Enter 31st October This Year");
                                            document.getElementById("<% =txtqty1of31.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtTotal5.ClientID%>').value == '') {
                                            alert("Enter Total");
                                            document.getElementById("<% =txtTotal5.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyMM.ClientID%>').value == '') {
                                            alert("Enter Quantity of molasses Used in the Manufacture");
                                            document.getElementById("<% =txtQtyMM.ClientID%>").focus();
                                            return false;
                                        }  
                                        if (document.getElementById('<%=txtQtymolassesstill.ClientID%>').value == '') {
                                            alert("Enter Quantity of molasses still to be lifted according to existing allotment");
                                            document.getElementById("<% =txtQtymolassesstill.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtqtynov1todec31.ClientID%>').value == '') {
                                            alert("Enter Quantity likely to be consumed during the period 1st November to 31st December.");
                                            document.getElementById("<% =txtqtynov1todec31.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtInStock.ClientID%>').value == '') {
                                            alert("Enter In stock as on ");
                                            document.getElementById("<% =txtInStock.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtQtyLifted.ClientID%>').value == '') {
                                            alert("Enter  Quantity to be lifted");
                                            document.getElementById("<% =txtQtyLifted.ClientID%>").focus();
                                            return false;
                                        }   
                                        if (document.getElementById('<%=txtTotal6.ClientID%>').value == '') {
                                            alert("Enter Total");
                                            document.getElementById("<% =txtTotal6.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtBalanceallotment.ClientID%>').value == '') {
                                            alert("Enter Balance on allotment");
                                            document.getElementById("<% =txtBalanceallotment.ClientID%>").focus();
                                            return false;k
                                        }
                                        if (document.getElementById('<%=txtGrandTotal.ClientID%>').value == '') {
                                            alert("Enter Grand Total");
                                            document.getElementById("<% =txtGrandTotal.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtStorage.ClientID%>').value == '') {
                                            alert("Enter Balance quantity of likely to be remain in storage.");
                                            document.getElementById("<% =txtStorage.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value != '') {
                                            alert("please click add button to add attached files.");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;
                                        }
                                        
                                    }

                                    function CheckDiscription() {

                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                             alert("Please Attach file");
                                             document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                             alert("Enter Description");
                                             document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }

                                    }
                                       function validateExtraDocuments() {
                                       
                                        var fileInput = document.getElementById('<%= idupDocument.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }

                                        var uploadControl = document.getElementById('<%= idupDocument.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idupDocument.ClientID %>').value = "";

                                            return false;                                           
                                        }
                                        else {
                                            return true;
                                        }
                                    }
                                    function GetUnitName() {

                                        debugger;
                                        if ($('#BodyContent_ddlCaptive').val() == "Y")
                                        {
                                            if ($('#BodyContent_captiveunit').val() != "") {
                                                $('#BodyContent_ddUnitName').val($('#BodyContent_captiveunit').val());
                                                document.getElementById('<%=ddUnitName.ClientID%>').disabled = true;
                                                document.getElementById('<%=ddUnitName.ClientID%>').style.display = "block";
                                                document.getElementById('capt').style.display = "inline";
                                            }
                                            //if ($('#BodyContent_ddUnitName').val() == "") {
                                            //    alert("Unit is not a Captive please Check it....");
                                            //    $('#BodyContent_ddlCaptive').val('Select');
                                            //    $('#BodyContent_ddlCaptive').focus();
                                            //}
                                        }
                                        else
                                        {
                                           
                                            document.getElementById('<%=ddUnitName.ClientID%>').style.display = "none";
                                            document.getElementById('BodyContent_ddUnitName').disabled = false;
                                            document.getElementById('capt').style.display = "none";
                                            $('#BodyContent_ddUnitName').val('Select');
                                            $('#BodyContent_ddUnitName').focus();
                                           // $('#BodyContent_ddUnitName').val("");
                                        }
                                    }
                                    function GetTotal1() {
                                       
                                        var val1 = parseFloat($('#BodyContent_txtCountrySperit').val());
                                        var val2 = parseFloat($('#BodyContent_txtRectifiedSpirit').val());
                                        var val3 = parseFloat($('#BodyContent_txtPowerAlcohol').val());
                                        var val4 = parseFloat($('#BodyContent_txtDistilledSperit').val());
                                        if ($('#BodyContent_txtCountrySperit').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtRectifiedSpirit').val() == '')
                                            val2 = 0;
                                        if ($('#BodyContent_txtPowerAlcohol').val() == '')
                                            val3 = 0;
                                        if ($('#BodyContent_txtDistilledSperit').val() == '')
                                            val4 = 0;

                                        var Total1 = parseFloat(val1) + parseFloat(val2) + parseFloat(val3) + parseFloat(val4);
                                        $('#BodyContent_txtTotal1').val(parseFloat(Total1).toFixed(2));
                                        $('#BodyContent_total1').val(parseFloat(Total1).toFixed(2))
                                        var indentqty = parseFloat($('#BodyContent_total1').val());
                                        if (Total1 != indentqty) {
                                            alert("Molasses Required Qty Should be Match with Indent QTY");
                                            $('#BodyContent_txtTotal1').focus();
                                        }


                                    }
                                    function GetTotal2() {
                                        var val1 = parseFloat($('#BodyContent_txtCountrySperit1').val());
                                        var val2 = parseFloat($('#BodyContent_txtRectifiedSpirit1').val());
                                        var val3 = parseFloat($('#BodyContent_txtPowerAlcohol1').val());
                                        var val4 = parseFloat($('#BodyContent_txtDistilledSperit1').val());
                                        if ($('#BodyContent_txtCountrySperit1').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtRectifiedSpirit1').val() == '')
                                            val2 = 0;
                                        if ($('#BodyContent_txtPowerAlcohol1').val() == '')
                                            val3 = 0;
                                        if ($('#BodyContent_txtDistilledSperit1').val() == '')
                                            val4 = 0;
                                        var Total2 = parseFloat(val1) + parseFloat(val2) + parseFloat(val3) + parseFloat(val4);
                                        $('#BodyContent_txtTotal2').val(parseFloat(Total2).toFixed(2))
                                        $('#BodyContent_total2').val(parseFloat(Total2).toFixed(2))

                                    }
                                    function GetTotal3() {
                                        var val1 = parseFloat($('#BodyContent_txtCountrySperit2').val());
                                        var val2 = parseFloat($('#BodyContent_txtRectifiedSpirit2').val());
                                        var val3 = parseFloat($('#BodyContent_txtPowerAlcohol2').val());
                                        var val4 = parseFloat($('#BodyContent_txtDistilledSperit2').val());
                                        if ($('#BodyContent_txtCountrySperit2').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtRectifiedSpirit2').val() == '')
                                            val2 = 0;
                                        if ($('#BodyContent_txtPowerAlcohol2').val() == '')
                                            val3 = 0;
                                        if ($('#BodyContent_txtDistilledSperit2').val() == '')
                                            val4 = 0;
                                        var Total3 = parseFloat(val1) + parseFloat(val2) + parseFloat(val3) + parseFloat(val4);
                                        $('#BodyContent_txtTotal3').val(parseFloat(Total3).toFixed(2))
                                        $('#BodyContent_total3').val(parseFloat(Total3).toFixed(2));

                                    }
                                    function GetTotal4() {
                                        var val1 = parseFloat($('#BodyContent_txtCountrySperit3').val());
                                        var val2 = parseFloat($('#BodyContent_txtRectifiedSpirit3').val());
                                        var val3 = parseFloat($('#BodyContent_txtPowerAlcohol3').val());
                                        var val4 = parseFloat($('#BodyContent_txtDistilledSperit3').val());
                                        if ($('#BodyContent_txtCountrySperit3').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtRectifiedSpirit3').val() == '')
                                            val2 = 0;
                                        if ($('#BodyContent_txtPowerAlcohol3').val() == '')
                                            val3 = 0;
                                        if ($('#BodyContent_txtDistilledSperit3').val() == '')
                                            val4 = 0;
                                        var Total4 = parseFloat(val1) + parseFloat(val2) + parseFloat(val3) + parseFloat(val4);
                                        $('#BodyContent_txtTotal4').val(parseFloat(Total4).toFixed(2));
                                        $('#BodyContent_total4').val(parseFloat(Total4).toFixed(2));

                                    }
                                    function GetTotal5() {
                                       
                                        var val1 = parseFloat($('#BodyContent_txtQtyfirstday').val());
                                        var val2 = parseFloat($('#BodyContent_txtqty1of31').val());

                                        if ($('#BodyContent_txtQtyfirstday').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtqty1of31').val() == '')
                                            val2 = 0;

                                        var Total5 = parseFloat(val1) + parseFloat(val2);
                                        $('#BodyContent_txtTotal5').val(parseFloat(Total5).toFixed(2));
                                        $('#BodyContent_total5').val(parseFloat(Total5).toFixed(2));

                                    }
                                    function GetTotal6() {

                                        var val1 = parseFloat($('#BodyContent_txtInStock').val());
                                        var val2 = parseFloat($('#BodyContent_txtQtyLifted').val());
                                        var val3 = parseFloat($('#BodyContent_txtBalanceallotment').val());

                                        if ($('#BodyContent_txtInStock').val() == '')
                                            val1 = 0;
                                        if ($('#BodyContent_txtQtyLifted').val() == '')
                                            val2 = 0;
                                        if ($('#BodyContent_txtBalanceallotment').val() == '')
                                            val3 = 0;

                                        var Total6 = parseFloat(val1) + parseFloat(val2);
                                       
                                            var gtotal = parseFloat(Total6) + parseFloat(val3);
                                            $('#BodyContent_txtTotal6').val(parseFloat(Total6).toFixed(2));
                                            $('#BodyContent_txtGrandTotal').val(parseFloat(gtotal).toFixed(2));
                                            $('#BodyContent_total6').val(parseFloat(Total6).toFixed(2));

                                            $('#BodyContent_Gtotal').val(parseFloat(gtotal).toFixed(2));
                                       
                                       
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
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                    function CheckDuplicates() {

                                        var jsondata = JSON.stringify($('#BodyContent_partycode').val() + "_" + $('#BodyContent_ddMaterial').val() + "_" + $('#BodyContent_txtfinancialyear').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "IndentForm.aspx/CheckDuplicates",
                                            data: '{value:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (msg.d == "DataExist") {
                                                    alert("Data exist for this " + $('#BodyContent_txtfinancialyear').val() + " fiscal year, please select different material");
                                                    $('#BodyContent_ddMaterial').val("Select");
                                                    $('#BodyContent_ddMaterial').focus();
                                                }

                                            }
                                        });
                                    }

                                </script>
                                <script>
                                    $(document).ready(function () {
                                      
                                        if ($('#BodyContent_txtCountrySperit').val() == "")
                                            $('#BodyContent_txtCountrySperit').val("0");
                                        if ($('#BodyContent_txtRectifiedSpirit').val() == "")
                                            $('#BodyContent_txtRectifiedSpirit').val("0");
                                        if ($('#BodyContent_txtPowerAlcohol').val() == "")
                                            $('#BodyContent_txtPowerAlcohol').val("0");
                                        if ($('#BodyContent_txtDistilledSperit').val() == "")
                                            $('#BodyContent_txtDistilledSperit').val("0");
                                        //1
                                        if ($('#BodyContent_txtCountrySperit1').val() == "")
                                            $('#BodyContent_txtCountrySperit1').val("0");
                                        if ($('#BodyContent_txtRectifiedSpirit1').val() == "")
                                            $('#BodyContent_txtRectifiedSpirit1').val("0");
                                        if ($('#BodyContent_txtPowerAlcohol1').val() == "")
                                            $('#BodyContent_txtPowerAlcohol1').val("0");
                                        if ($('#BodyContent_txtDistilledSperit1').val() == "")
                                            $('#BodyContent_txtDistilledSperit1').val("0");
                                        //2
                                        if ($('#BodyContent_txtCountrySperit2').val() == '')
                                            $('#BodyContent_txtCountrySperit2').val("0");
                                        if ($('#BodyContent_txtRectifiedSpirit2').val() == '')
                                            $('#BodyContent_txtRectifiedSpirit2').val("0");
                                        if ($('#BodyContent_txtPowerAlcohol2').val() == '')
                                            $('#BodyContent_txtPowerAlcohol2').val("0");
                                        if ($('#BodyContent_txtDistilledSperit2').val() == '')
                                            $('#BodyContent_txtDistilledSperit2').val("0");
                                        //3
                                        if ($('#BodyContent_txtCountrySperit3').val() == '')
                                            $('#BodyContent_txtCountrySperit3').val("0");
                                        if ($('#BodyContent_txtRectifiedSpirit3').val() == '')
                                            $('#BodyContent_txtRectifiedSpirit3').val("0");
                                        if ($('#BodyContent_txtPowerAlcohol3').val() == '')
                                            $('#BodyContent_txtPowerAlcohol3').val("0");
                                        if ($('#BodyContent_txtDistilledSperit3').val() == '')
                                            $('#BodyContent_txtDistilledSperit3').val("0");
                                        //                                     

                                        if ($('#BodyContent_txtMolassesDistilled').val() == '')
                                            $('#BodyContent_txtMolassesDistilled').val("0");
                                        if ($('#BodyContent_txtWorkingwastage').val() == '')
                                            $('#BodyContent_txtWorkingwastage').val("0");
                                        if ($('#BodyContent_txtTransitwastage').val() == '')
                                            $('#BodyContent_txtTransitwastage').val("0");
                                        if ($('#BodyContent_txtQtyfirstday').val() == '')
                                            $('#BodyContent_txtQtyfirstday').val("0");
                                        if ($('#BodyContent_txtqty1of31').val() == '')
                                            $('#BodyContent_txtqty1of31').val("0");
                                        //                         
                                        if ($('#BodyContent_txtQtyMM').val() == '')
                                            $('#BodyContent_txtQtyMM').val("0");
                                        if ($('#BodyContent_txtQtymolassesstill').val() == '')
                                            $('#BodyContent_txtQtymolassesstill').val("0");
                                        if ($('#BodyContent_txtqtynov1todec31').val() == '')
                                            $('#BodyContent_txtqtynov1todec31').val("0");
                                        if ($('#BodyContent_txtInStock').val() == '')
                                            $('#BodyContent_txtInStock').val("0");
                                        if ($('#BodyContent_txtQtyLifted').val() == '')
                                            $('#BodyContent_txtQtyLifted').val("0");
                                        if ($('#BodyContent_txtBalanceallotment').val() == '')
                                            $('#BodyContent_txtBalanceallotment').val("0");
                                        if ($('#BodyContent_txtStorage').val() == '')
                                            $('#BodyContent_txtStorage').val("0");
                                        GetTotal1();
                                        GetTotal2();
                                        GetTotal3();
                                        GetTotal4();
                                        GetTotal5();
                                        GetTotal6();
                                        GetUnitName();
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                        }

                                       
                                    });
                                </script>
                                <style>
                                    fieldset {
                                        background-color: #eeeeee;
                                    }

                                    legend {
                                        background-color: gray;
                                        color: white;
                                        padding: 5px 5px;
                                        font-size: small;
                                        font-weight: bold;
                                        text-align: center;
                                    }

                                    input {
                                        margin: 1px;
                                    }
                                </style>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnIndent" OnClick="btnIndent_Click">
                                        <span style="color: #fff; font-size: 14px;">Indent for Molasses</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnARM" OnClick="btnARM_Click">
                                        <span style="color: #fff; font-size: 14px;">Allocation Request for Molasses</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Indent for Molasses Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />

                                    <asp:HiddenField ID="total1" runat="server" />
                                    <asp:HiddenField ID="partycode" runat="server" />
                                    <asp:HiddenField ID="total2" runat="server" />
                                    <asp:HiddenField ID="total3" runat="server" />
                                    <asp:HiddenField ID="total4" runat="server" />
                                    <asp:HiddenField ID="total5" runat="server" />
                                    <asp:HiddenField ID="Gtotal" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <p>&nbsp;</p>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <tr>
                                                            <td>Indent Quantity<span style="color: red;">*</span></td>
                                                            <td>
                                                                <asp:TextBox ID="txtIndentQty" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Indent Quantity"></asp:TextBox>

                                                            </td>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Financial Year<span style="color: red;">*</span></td>
                                                    <%--<td>
															<input type="text" class="form-control" id="ind_year" name="ind_year" value="2019-2020" readonly></td>--%>
                                                    <td>
                                                        <asp:TextBox ID="txtfinancialyear" ReadOnly="true" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Financial Year"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Indent Date<span style="color: red;">*</span>

                                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" OnClientDateSelectionChanged="SelectDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDATE" data-toggle="tooltip" ReadOnly="true" data-placement="right" title="Indent Date" CssClass="form-control validate[required]" AutoComplete="off" runat="server">
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
                                                    <td>Captive <span style="color: red;">*</span></td>
                                                    <td>
                                                        <asp:HiddenField ID="captiveunit" runat="server" />
                                                        <asp:DropDownList ID="ddlCaptive" runat="server" data-toggle="tooltip" CssClass="form-control" onchange="GetUnitName()" data-placement="right" title="Captive">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td id="capt">Captive Unit Name<span style="color: red;">*</span></td>
                                                    <td id="capt1">
                                                        <asp:DropDownList ID="ddUnitName" runat="server" data-toggle="tooltip" CssClass="form-control" data-placement="right" title="Unit Name"></asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Material<span style="color: red;">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddMaterial" onchange="CheckDuplicates()" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Material"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="border: 1px #000 solid">Quantity of molasses required<span style="color: red;">*</span>

                                                    </td>
                                                    <td style="border: 1px #000 solid">Quantity of molasses
													
													received
                                                        <br />
                                                        During previous year
													
													for the
                                                        <br />
                                                        manufacture of <span style="color: red;">*</span>

                                                    </td>
                                                    <td style="border: 1px #000 solid; width: 15%">Quantity of molasses
													used 
                                                        <br />
                                                        During 
													the previous year
													
													for the
                                                        <br />
                                                        manufacture of <span style="color: red;"></span></td>
                                                    <td style="border: 1px #000 solid">Quantity of molasses used
													<br />
                                                        since  January to 31st
													<br />
                                                        - in October This year
													<br />
                                                        For Manufacture of <span style="color: red;"></span></td>
                                                </tr>
                                                <tr>
                                                    <td style="border: 1px #000 solid">C.S</td>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtCountrySperit" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Country Sperit"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtCountrySperit1" CssClass="form-control" onchange="CheckNumber(this);GetTotal2()" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Rectified Spirit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtCountrySperit2" CssClass="form-control" onchange="CheckNumber(this);GetTotal3()" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Power Alcohol" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtCountrySperit3" CssClass="form-control" onchange="CheckNumber(this);GetTotal4()" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Country Sperit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                </tr>

                                                <tr>
                                                    <td style="border: 1px #000 solid">R.S</td>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtRectifiedSpirit" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Country Sperit"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtRectifiedSpirit1" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Power Alcohol" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtRectifiedSpirit2" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Rectified Spirit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtRectifiedSpirit3" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Distilled Sperit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                </tr>

                                                <tr>

                                                    <td style="border: 1px #000 solid">P.A</td>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtPowerAlcohol" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Country Sperit"></asp:TextBox>



                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtPowerAlcohol1" CssClass="form-control" runat="server" onchange="CheckNumber(this);GetTotal2()" MaxLength="10"  data-toggle="tooltip" data-placement="right" title="Power Alcohol" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtPowerAlcohol2" CssClass="form-control" onchange="CheckNumber(this);GetTotal3()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Rectified Spirit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtPowerAlcohol3" CssClass="form-control" onchange="CheckNumber(this);GetTotal4()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Distilled Sperit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                </tr>
                                                <tr>

                                                    <td style="border: 1px #000 solid">D.S</td>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtDistilledSperit" CssClass="form-control" onchange="CheckNumber(this);GetTotal1()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Country Sperit"></asp:TextBox>



                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtDistilledSperit1" CssClass="form-control" runat="server" onchange="CheckNumber(this);GetTotal2()" MaxLength="10"  data-toggle="tooltip" data-placement="right" title="Power Alcohol" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtDistilledSperit2" CssClass="form-control" onchange="CheckNumber(this);GetTotal3()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Rectified Spirit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtDistilledSperit3" CssClass="form-control" onchange="CheckNumber(this);GetTotal4()" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" title="Distilled Sperit" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                </tr>
                                                <tr>

                                                    <td style="border: 1px #000 solid">Total</td>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtTotal1" CssClass="form-control" runat="server" data-toggle="tooltip" onchange="CheckNumber(this)" MaxLength="10" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Total" ReadOnly="true"></asp:TextBox>



                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtTotal2" CssClass="form-control" onchange="CheckNumber(this)" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Total" ReadOnly="true"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtTotal3" CssClass="form-control" onchange="CheckNumber(this)" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Total" ReadOnly="true"></asp:TextBox>
                                                    <td style="border: 1px #000 solid">
                                                        <asp:TextBox ID="txtTotal4" CssClass="form-control" onchange="CheckNumber(this)" MaxLength="10" runat="server" data-toggle="tooltip" data-placement="right" title="Total" ReadOnly="true"></asp:TextBox>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <br />
                                                        <span>Quantity of molasses used since January'</span><span id="spnyearvalue"></span><span> this year for the manufacture of -</span> </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding-left: 1.6%">Molasses Distilled <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMolassesDistilled" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Molasses Distilled" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>                    
                                                    <td style="padding-left: 1.6%">Working wastage <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtWorkingwastage" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Working wastage" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">Transit wastage <span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTransitwastage" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Transit wastage" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>Quantity of molasses in
													stock of 1st day of January.<span style="color: red;">*</span><br />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQtyfirstday" onchange="CheckNumber(this);GetTotal5()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Quantity of molasses in stock of 1st day of January"  MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                                              
                                                <tr>
                                                    <td>Quantity of molasses Received
													from 1st January to 31st													
													October This Year.<span style="color: red;">*</span><br />
                                                        <br />
                                                    </td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtqty1of31" onchange="CheckNumber(this);GetTotal5()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right"  MaxLength="10" title="Quantity of molasses Received from 1st January to 31st October This Year" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>Total<span style="color: red;">*</span><br />
                                                        <br />
                                                    </td>                                         
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtTotal5" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);" title="Total" ReadOnly="true"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>Quantity of molasses													
													Used in the Manufacture.<span style="color: red;">*</span><br />
                                                    </td>
                                                    <td style="margin-left: 4%">      
                                                        <asp:TextBox ID="txtQtyMM" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Quantity of molasses Used in the Manufacture." onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>Quantity of molasses still													
													to be lifted according												
													to existing allotment.<span style="color: red;">*</span><br />

                                                    </td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtQtymolassesstill" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Molasses to be lifted w.r.t existing allotment" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>

                                                </tr>

                                                <tr>
                                                    <td>Quantity likely to be
													consumed during the period
													1st November to 31st December.<span style="color: red;">*</span><br />
                                                        <br />
                                                    </td>                           
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtqtynov1todec31" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Molasses likely to be consumed." onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>

                                                </tr>

                                                <%--	<tr>
												<td>Quantity likely to be consumed during the period 1st Novemeber'18 to 31st December'18
													<span style="color: red;">*</span></td>
												<td style="margin-left: 4%">
													<input type="float" class="form-control" id="mf_1_qty_tobeconsumed" name="mf_1_qty_tobeconsumed" onkeypress="return (isFloatInput(event,this.value));" value="0" maxlength="13"></td>
												<td>Qtls</td>
											</tr>--%>
                                                <tr>
                                                    <td style="padding-left: 1.6%"><span>In stock as on </span><span id="spnStockasonyear"></span><span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtInStock" onchange="CheckNumber(this);GetTotal6()" MaxLength="10" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="In stock as on"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">Quantity to be lifted<span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtQtyLifted" onchange="CheckNumber(this);GetTotal6()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Quantity to be lifted"  MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>                          
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">Total<span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtTotal6" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Total" ReadOnly="true" onchange="CheckNumber(this)" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 1.6%">Balance on allotment<span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtBalanceallotment" onchange="CheckNumber(this);GetTotal6()" MaxLength="10" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Balance on allotment" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>

                                                    <td>Qtls</td>
                                                </tr>
                                                <tr> 
                                                    <td style="padding-left: 1.6%">Grand Total<span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtGrandTotal" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Grand Total" ReadOnly="true"></asp:TextBox>

                                                    <td>Qtls</td>
                                                </tr>
                                                <tr>
                                                    <td>Balance quantity of likely to be remain in storage.<span style="color: red;">*</span></td>
                                                    <td style="margin-left: 4%">
                                                        <asp:TextBox ID="txtStorage" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Balance quantity of likely to be remain in storage" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    <td>Qtls</td>
                                                </tr>

                                            </table>
                                        </ContentTemplate>



                                    </asp:UpdatePanel>
                                </div>


                                <div class="clearfix"></div>

                                <div id="docs" runat="server">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">

                                        <label class="control-label"><span style="color: red"></span>Documents</label><br />

                                        <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                        <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Cane Crushed"></asp:TextBox>
                                        <span>
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                        </span>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                    <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                        <thead>
                                            <tr>
                                                <th>File Name</th>
                                                <th>Description</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="resourcetable">
                                        </tbody>

                                    </table>
                                </div>
                                <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                    <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="File Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName" runat="server" Visible="true" Text='<%#Eval("Doc_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscriptione" runat="server" Visible="true" Text='<%#Eval("Discription") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FilePath" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFilePath" runat="server" Visible="true" Text='<%#Eval("Doc_path") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldoc_id" runat="server" Visible="true" Text='<%#Eval("doc_id") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                    &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>


                                    </asp:GridView>


                                    <%--  <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id="approverremaks" runat="server" class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small; display:inline"><span style="color: red">*</span>Approver Comments</label><br />
                                    <asp:TextBox ID="txtApproverComment" runat="server" CssClass="form-control" data-toggle="tooltip" Height="5%" Width="95%" data-placement="right" title="Approver Comments" TextMode="MultiLine"></asp:TextBox>
                                    </div>--%>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <%--  <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve"  />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut"  />--%>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
