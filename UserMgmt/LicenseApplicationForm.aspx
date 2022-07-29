<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="LicenseApplicationForm.aspx.cs" Inherits="UserMgmt.LicenseApplicationForm" %>

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
                                <title>License Application From </title>
                                   <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;
                                        if (document.getElementById('<%=txtApplicantname.ClientID%>').value == '') {
                                            alert("Enter Applicant Name");
                                            document.getElementById("<% =txtApplicantname.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtunitname.ClientID%>').value == '') {
                                            alert("Enter Unit Name");
                                            document.getElementById("<% =txtunitname.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtApplicationDate.ClientID%>').value == '') {
                                            alert("Enter Application Date");
                                            document.getElementById("<% =txtApplicationDate.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtAddress.ClientID%>').value == '') {
                                            alert("Enter Address");
                                            document.getElementById("<% =txtAddress.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddStates.ClientID%>').value =='Select') {
                                            alert("select State");
                                            document.getElementById("<% =ddStates.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddDivisions.ClientID%>').value == 'Select') {
                                            alert("select Division");
                                            document.getElementById("<% =ddDivisions.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddDistricts.ClientID%>').value == 'Select') {
                                            alert("select District");
                                            document.getElementById("<% =ddDistricts.ClientID%>").focus();
                                            return false;

                                        }
                                      
                                        if (document.getElementById('<%=txttaluk.ClientID%>').value == '') {
                                            alert("Enter Taluk");
                                            document.getElementById("<% =txttaluk.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlLicense.ClientID%>').value == 'Select') {
                                            alert("select license type name");
                                            document.getElementById("<% =ddlLicense.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlfee.ClientID%>').value == 'Select') {
                                            alert("select license Fee");
                                            document.getElementById("<% =ddlfee.ClientID%>").focus();
                                            return false;

                                        }

                                        if (document.getElementById('<%=txtMobile.ClientID%>').value == '') {
                                            alert("Enter Mobile");
                                            document.getElementById("<% =txtMobile.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtMobile.ClientID%>').value.length <10) {
                                            alert("Invalid Mobile number");
                                            document.getElementById("<%=txtMobile.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtemail.ClientID%>').value == '') {
                                            alert("Enter Email");
                                            document.getElementById("<%=txtemail.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtphotoname.ClientID%>').value == '') {
                                            alert("Enter Photo Description");
                                            document.getElementById("<%=txtphotoname.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlidproof.ClientID%>').value == 'Select') {
                                            alert("select Idproof");
                                            document.getElementById("<%=ddlidproof.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtGST.ClientID%>').value == '') {
                                            alert("Enter GST");
                                            document.getElementById("<%=txtGST.ClientID%>").focus();
                                            return false;
                                        }
                                        var allotedqty = document.getElementById('<%=txtGST.ClientID%>');
                                        // if (allotedqty.innerHTML.length < 15) {
                                          if (document.getElementById('<%=txtGST.ClientID%>').value.length <15) {
                                            alert("Invalid GST number");
                                            document.getElementById("<%=txtGST.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remark");
                                            document.getElementById("<%=txtRemarks.ClientID%>").focus();
                                            return false;
                                        }

                                    }
                                      </script>
                                  <script language="javascript" type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver Comments ");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;
                                        }

                                    }
                                    function validationMsg12() {
                                        if (document.getElementById('<%=txtReceiptDate.ClientID%>').value == '') {
                                            alert("Enter Start Date ");
                                            document.getElementById("<% =txtReceiptDate.ClientID%>").focus();
                                             return false;
                                         }
                                        if (document.getElementById('<%=txtDateofRemoval.ClientID%>').value == '') {
                                            alert("Enter End Date");
                                            document.getElementById("<% =txtDateofRemoval.ClientID%>").focus();
                                             return false;

                                         }
                                         if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver Comments ");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;
                                         }
                                     }
                                </script>
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
                                        if (charCode > 8 && (charCode < 65 || charCode > 90))
                                            return false;

                                        return true;
                                    }
                                    function Validate(e) {
                                        var keyCode = e.keyCode || e.which;
                                        var regex = /^[a-zA-Z ]+$/; //^[A-Za-z]*$/;
                                        var isValid = regex.test(String.fromCharCode(keyCode));
                                        return isValid;
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
                                        $('#BodyContent_txtApplicationDate').val(todayDate);
                                        $('#BodyContent_txttrdate').val(todayDate);
                                    }
                                    function SelectDate5(e) {
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
                                        $('#BodyContent_txtReceiptDate').val(todayDate);
                                        $('#BodyContent_txtdate').val(todayDate);
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
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtemail').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtemail').val("");
                                            $('#BodyContent_txtemail').focus();
                                            return false;
                                        }
                                    }
                                    function pancardValidate() {
                                        debugger;
                                        var txtPANCard = $('#BodyContent_textPan');
                                        var regex = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_textPan').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid PAN number.");
                                            $('#BodyContent_textPan').val("");
                                            $('#BodyContent_textPan').focus();
                                            return false;
                                        }
                                    }
                                    function ValidateTAN() {
                                        debugger;
                                        var txtTAN = $('#BodyContent_textTan');
                                        var regex = /([A-Z]){4}([0-9]){5}([A-Z]){1}$/;
                                        if (regex.test($('#BodyContent_textTan').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid TAN number.");
                                            $('#BodyContent_textTan').val("");
                                            $('#BodyContent_textTan').focus();
                                            return false;
                                        }
                                    }
                                    function validateGST() {
                                        debugger;
                                        var txtTIN = $('#BodyContent_txtGST');
                                        var regex = /([0-9]){2}([A-Z]){5}([0-9]){4}([A-Z]){1}([0-9]){1}([Z]){1}([0-9]){1}$/;
                                        if (regex.test($('#BodyContent_txtGST').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid GST number.");
                                            $('#BodyContent_txtGST').val("");
                                            $('#BodyContent_txtGST').focus();
                                            return false;
                                        }
                                    }
                                    function validateTIN() {
                                        debugger;
                                        var txtTIN = $('#BodyContent_textTin');
                                        var regex = /^\d{11}$/;
                                        if (regex.test($('#BodyContent_textTin').val().toUpperCase())) {

                                            return true;
                                        } else {
                                            alert("Invalid TIN number.");
                                            $('#BodyContent_textTin').val("");
                                            $('#BodyContent_textTin').focus();
                                            return false;
                                        }
                                    }


                                </script>
                                <script>
                                    $(document).ready(function () {

                                        debugger;
                                        if ($('#BodyContent_txtApplicationDate').val() == "") {
                                            $('#BodyContent_txtApplicationDate').val($('#BodyContent_txttrdate').val());
                                        }

                                        debugger;
                                        if ($('#BodyContent_txtReceiptDate').val() == "") {
                                            $('#BodyContent_txtReceiptDate').val($('#BodyContent_txtdate').val());
                                        }
                                        debugger;
                                        if ($('#BodyContent_txtDateofRemoval').val() == "") {
                                            $('#BodyContent_txtDateofRemoval').val($('#BodyContent_txtdor').val());
                                        }
                                        debugger;
                                        if ($('#BodyContent_btnDownloadMf1Attachment').val() == "") {
                                            $('#BodyContent_btnDownloadMf1Attachment').val($('#BodyContent_Hdphoto').val());
                                        }
                                        if ($('#BodyContent_btnDownloadMf1Attachment1').val() == "") {
                                            $('#BodyContent_btnDownloadMf1Attachment1').val($('#BodyContent_Hdidproof').val());
                                        }
                                        // Calcutate();
                                        //if ($('#BodyContent_txtNetWeight').val() == "" || $('#BodyContent_txtNetWeight').val() == "0") {
                                        //    $('#BodyContent_txtTransitWastage').val("");
                                        //}
                                    });
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function CheckDiscription() {
                                        debugger;
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


                                </script>
                                <script type="text/javascript">


                                    function validateExtraDocuments() {

                                        var fileInput = document.getElementById('<%=idupDocument.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }


                                        var uploadControl = document.getElementById('<%=idupDocument.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%=idupDocument.ClientID %>').value = "";

                                            return false;
                                        }
                                        else {

                                            return true;
                                        }
                                    }
                                    function validateExtraDocuments1() {
                                        debugger;
                                        var fileInput = document.getElementById('<%=FileIDproof.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }
                                         var b = document.getElementById('<%=FileIDproof.ClientID %>').files[0].name;
                                        $('#BodyContent_Hdidproof').val(b)
                                        var uploadControl = document.getElementById('<%=FileIDproof.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%=FileIDproof.ClientID %>').value = "";

                                               return false;
                                           }
                                           else {

                                            return true;
                                            
                                           }
                                       }
                                       function validateExtraDocuments2() {
                                           debugger;;
                                           $('#BodyContent_Hdphoto').val($('#BodyContent_FilePhoto').val())
                                           var fileInput = document.getElementById('<%=FilePhoto.ClientID %>');
                                           var filePath = fileInput.value;
                                           var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                           if (!allowedExtensions.exec(filePath)) {
                                               alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                               fileInput.value = '';
                                               return false;
                                           }
                                           var b = document.getElementById('<%=FilePhoto.ClientID %>').files[0].name;
                                           $('#BodyContent_Hdphoto').val(b)

                                           var uploadControl = document.getElementById('<%=FilePhoto.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%=FilePhoto.ClientID %>').value = "";

                                               return false;
                                           }
                                           else {

                                               return true;
                                           }
                                       }
                                       function phoneValidate() {
                                           debugger;
                                           var mobileN = $('#BodyContent_txtMobile').val().length;

                                           if (mobileN != 10) {
                                               alert("Invalid phone number.");
                                               $('#BodyContent_txtMobile').val("");
                                               $('#BodyContent_txtMobile').focus();
                                           }
                                       }
                                       function imageUpload(image, imageLbl) {
                                           debugger;
                                           var imgText = $('#' + image).val();
                                           //var filename = imgText.replace(/^.*[\\\/]/, '');
                                           //var imgTextArr = filename.split(".");
                                           //var imgTxtFTb = imgTextArr[0];
                                           //if (parseInt(imgTxtFTb.length) > 45) {
                                           //    imgTxtFTb = imgTxtFTb.substring(0, 45);
                                           //}
                                           $('#' + imageLbl).val(imgText);
                                       }

                                       function browseImage(image) {

                                           $('#' + image).click();
                                       }
                                </script>

                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <%-- <li>
                                        <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                      <li>
                                            <asp:LinkButton ID="partyfinancialyears" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party type Financial Years</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                    <li >
                                        <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                     <li >  <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                    <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                     <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>--%>
                                </ul>

                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>License Application Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <div runat="server" visible="false" id="applicationno" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Application No</label><br />
                                    <asp:TextBox ID="txtapplicationno" runat="server" AutoComplete="off" Height="30px" Width="60%" class="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Application Number"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Applicant  Name</label>
                                    <br />
                                    <asp:TextBox ID="txtApplicantname" AutoComplete="off" class="form-control" Height="30px" onkeypress="return Validate(event);" Width="250px" data-toggle="tooltip" data-placement="right" title="Applicant  Name" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label""><span style="color: red">*</span> Unit Name</label><br />
                                    <asp:TextBox ID="txtunitname" class="form-control" AutoComplete="off" Height="30px" Width="250px" onkeypress="return Validate(event);" data-toggle="tooltip" data-placement="right" title="Unit Name" runat="server"></asp:TextBox>
                                </div>
                                <div runat="server" id="appicationdate" visible="true" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Established On</label><br />
                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtApplicationDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate1" ID="CalendarExtender"></cc1:CalendarExtender>
                                    <script type="text/javascript">
                                        function SelectDate(e) {
                                            debugger;
                                            var dat1e = $('#BodyContent_txtReceiptDate').val();
                                            $('#BodyContent_txttrdate').val(dat1e);
                                        }
                                    </script>
                                    <asp:TextBox ID="txtApplicationDate" data-toggle="tooltip" data-placement="right" title="Application Date" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                    <asp:HiddenField ID="txttrdate" runat="server" />
                                </div>

                                <div id="dob" visible="false" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>DOB</label><br />
                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image12" TargetControlID="txtDATE_OF_BIRTH" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender1"></cc1:CalendarExtender>
                                    <script type="text/javascript">

                                        function SelectDate(e) {

                                            var PresentDay = new Date();
                                            var dateOfBirth = e.get_selectedDate();
                                            var months = (PresentDay.getMonth() - dateOfBirth.getMonth() + (12 * (PresentDay.getFullYear() - dateOfBirth.getFullYear())));
                                            var age11 = Math.round(months / 12);
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
                                            $('#BodyContent_txtDATE_OF_BIRTH').val(todayDate);
                                            var dat1e = $('#BodyContent_txtDATE_OF_BIRTH').val();
                                            $('#BodyContent_txtdob').val(dat1e);


                                            if (Math.round(months / 12) < 21) {
                                                alert("Age should be greater than 21 Years");
                                                document.getElementById("<% =txtDATE_OF_BIRTH.ClientID%>").value = "";
                                                document.getElementById("<% =txtDATE_OF_BIRTH.ClientID%>").focus();
                                            }
                                        }

                                    </script>
                                    <asp:TextBox ID="txtDATE_OF_BIRTH" Height="30px" Width="60%" ReadOnly="true" data-toggle="tooltip" data-placement="right" title="DOB" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    <asp:ImageButton ID="Image12" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                    <asp:HiddenField ID="txtdob" runat="server" />
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Address</label><br />

                                    <asp:TextBox ID="txtAddress" class="form-control" Width="250px" data-toggle="tooltip" Style="text-transform: capitalize;" data-placement="right" title="Address" MaxLength="250" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>State</label><br />
                                    <asp:DropDownList ID="ddStates" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="State" OnSelectedIndexChanged="ddStates_SelectedIndexChanged1" CssClass="form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span> Division</label><br />
                                    <asp:DropDownList ID="ddDivisions" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Division" OnSelectedIndexChanged="ddDivisions_SelectedIndexChanged" CssClass="form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span> District</label><br />
                                    <asp:DropDownList ID="ddDistricts" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddDistricts_SelectedIndexChanged" title="District " CssClass="form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Taluk Town</label><br />
                                    <asp:TextBox ID="txttaluk" class="form-control" data-toggle="tooltip" data-placement="right" title="Taluk Town" onkeypress="return Validate(event);" runat="server" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Thana</label><br />
                                    <asp:DropDownList ID="ddlthana" Height="30px" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Thana" CssClass="form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display:inline"><span style="color: red">*</span>License Applied For</label><br />
                                    <asp:DropDownList ID="ddlLicense" runat="server" CssClass="form-control" data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="License Type Name" OnSelectedIndexChanged="ddlLicense_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span>License Type</label><br />
                                    <asp:CheckBoxList ID="Chsub" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                </div>
                                <%--   <div runat="server" id="check" class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red"></span>License Type</label><br />
                                          <asp:CheckBoxList ID="Ch1" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    </div>--%>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>License Fee</label><br />
                                    <asp:DropDownList ID="ddlfee" runat="server" CssClass="form-control" data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="License Fee" OnSelectedIndexChanged="ddlfee_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                          <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Unit Name</label> <br />
                                                            <asp:DropDownList ID="ddlUnitname"  AutoPostBack="true" class="form-control"   Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Unit Name"  runat="server">
                                                               
                                                            </asp:DropDownList>
                                                        </div>--%>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Fee Amount</label><br />
                                    <asp:TextBox ID="txtfeeamount" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Fee Amount" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                       <asp:HiddenField ID="Hdrenewal" runat="server" />
                                </div>

                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Advertisement Reference</label><br />

                                    <asp:TextBox ID="txtrank" class="form-control" AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Applicant Rank" runat="server"></asp:TextBox>
                                </div>


                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>PIN </label>
                                    <br />
                                    <asp:TextBox ID="txtPin" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="PIN" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                                <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline"> <br />
                                           <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>District</label> <br />
                                                            <asp:DropDownList ID="ddlDistrict" Height="30px" Width="250px" data-toggle="tooltip" class="form-control" data-placement="right" title="District"  runat="server">
                                                               
                                                            </asp:DropDownList>
                                                        </div>--%>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Start Date</label><br />
                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtReceiptDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate5" ID="CalendarExtender4"></cc1:CalendarExtender>
                                    <asp:TextBox ID="txtReceiptDate" data-toggle="tooltip" data-placement="right" title="Receipt Date" class="form-control validate[required]" AutoComplete="off" ReadOnly="true" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                    <asp:HiddenField ID="txtdate" runat="server" />
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span> End Date</label><br />
                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtDateofRemoval" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectRemovalDate" ID="CalendarExtender5"></cc1:CalendarExtender>
                                    <asp:TextBox ID="txtDateofRemoval" data-toggle="tooltip" data-placement="right" title="Date Reserved for Form 84" class="form-control validate[required]" ReadOnly="true" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                    <asp:HiddenField ID="txtdor" runat="server" />

                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Mobile No</label><br />
                                    <asp:TextBox ID="txtMobile" class="form-control" data-toggle="tooltip" onchange="phoneValidate()" data-placement="right" title="Mobile No" MaxLength="10" runat="server" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Email </label>
                                    <br />
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email" AutoPostBack="true" onchange="emailValidate(this);"></asp:TextBox>
                                </div>
                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Aadhaar No </label>
                                    <br />
                                    <asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Aadhaar" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Photo  Description</label><br />
                                    <asp:TextBox ID="txtphotoname" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Photo" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Photo </label>
                                    <br />
                                    <asp:FileUpload ID="FilePhoto" runat="server" CssClass="form-control" onchange="validateExtraDocuments2();" o />
                                    <asp:HiddenField ID="Hdphoto" runat="server" />
                                    <span style="display: none">
                                        <asp:Button runat="server" ID="btnDownloadMf1Attachment" />
                                    </span>
                                    <asp:Button runat="server" ID="btnDownload" CssClass="myButton" Text="Download" OnClick="btnDownloadmf1_Click" />

                                </div>
                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <%-- <label class="control-label"><span style="margin-top: 5px;">Attachment<span style="color: red">*</span></span>--%>
                                    <label class="control-label"><span style="color: red">*</span>User Photo</label><br />
                                    <asp:FileUpload runat="server" Width="250px" Style="display: none;" ID="idproofimage" class="file-name" name="idproofimage" value="" onchange="validateExtraDocuments();
                                                    imageUpload('BodyContent_idproofimage', 'idproofimageLbl');" />
                                    <input class="form-control" width="250px" readonly style="margin-top: 5px;" onkeypress="return attachMand('BodyContent_idproofimage',this.id)" data-toggle="tooltip" data-placement="right" title="Attachment" type="text" id="idproofimageLbl" name="idproofimageLbl" maxlength="250" placeholder="Attachment Name">
                                    <p id="pattachment" style="font-size: 9px; font-weight: 600;">(.jpg, .jpeg  upto 2 MB max)</p>
                                    <input type="button" id="btndownloadattachment" style="width: 250px; display: none; margin-top: -5px;" class="btn btn-primary" value="Download file" onclick="downloadattachment();" />
                                    <input type="button" id="btnppup" style="width: 85px; margin-bottom: -1px;" value="Browse.." class="btn btn-primary" onclick="browseImage('BodyContent_idproofimage');" />
                                    <span style="display: none">
                                        <asp:Button runat="server" ID="btnUpload" />
                                    </span>
                                </div>


                                <%--                                --%>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><a style="color: red;">*</a>Id Proof Type</label>
                                    <br />
                                    <asp:DropDownList ID="ddlidproof" data-toggle="tooltip" class="form-control" data-placement="right" title="ID" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Id Proof</label><br />
                                    <asp:FileUpload ID="FileIDproof" runat="server" CssClass="form-control" onchange="validateExtraDocuments1();" />
                                    <asp:HiddenField ID="Hdidproof" runat="server" />
                                    <span style="display: none">
                                        <asp:Button runat="server" ID="btnDownloadMf1Attachment1" />
                                    </span>
                                    <asp:Button runat="server" ID="btnDownload1" CssClass="myButton" Text="Download" OnClick="btnDownloadmf2_Click" />

                                </div>
                                <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <%-- <label class="control-label"><span style="margin-top: 5px;">Attachment<span style="color: red">*</span></span>--%>
                                    <label class="control-label"><span style="color: red">*</span>Id Proof</label><br />
                                    <asp:FileUpload runat="server" Width="250px" Style="display: none;" ID="idproofimage1" class="file-name" name="idproofimage" value="" onchange="validateExtraDocuments();
                                                    imageUpload('BodyContent_idproofimage1', 'idproofimage1Lbl');" />
                                    <input class="form-control" width="250px" readonly style="margin-top: 5px;" onkeypress="return attachMand('BodyContent_idproofimage1',this.id)" data-toggle="tooltip" data-placement="right" title="Attachment" type="text" id="idproofimage1Lbl" name="idproofimageLbl" maxlength="250" placeholder="Attachment Name">
                                    <p id="pattachment1" style="font-size: 9px; font-weight: 600;">(.jpg, .jpeg  upto 2 MB max)</p>
                                    <input type="button" id="btndownloadattachment1" style="width: 250px; display: none; margin-top: -5px;" class="btn btn-primary" value="Download file" onclick="downloadattachment();" />
                                    <input type="button" id="btnppup1" style="width: 85px; margin-bottom: -1px;" value="Browse.." class="btn btn-primary" onclick="browseImage('BodyContent_idproofimage1');" />
                                    <span style="display: none">
                                        <asp:Button runat="server" ID="btnUpload1" />
                                    </span>
                                </div>



                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <%-- <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Photo Name</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>

                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Id Proof</label><br />
                                                                                       <asp:FileUpload ID="idupDocument1" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                            <span>
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                            <thead>
                                                <tr>
                                                    <th>User Photo</th>
                                                    <th>Idproof Type</th>
                                                      <th>Idproof Image</th>
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
                                                  <asp:TemplateField HeaderText="User Photo" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphoto" runat="server" Visible="true" Text='<%#Eval("photoname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Download"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" Width="30px" Height="20px" CommandArgument='<%#Eval("photoname") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Idproof Type" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" runat="server" Visible="true" Text='<%#Eval("idproof_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Idproof Type" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscriptione" runat="server" Visible="true" Text='<%#Eval("idproof_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FilePath" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFilePath" runat="server" Visible="true" Text='<%#Eval("idproof_image") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoc_id" runat="server" Visible="true" Text='<%#Eval("doc_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Download" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("idproof_image") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp; 
<%--                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                     <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("idproof_image") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                --%><div class="clearfix"></div>
                                <p>&nbsp;</p>
                               <%--  onchange="validateGST();"--%>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>GST</label><br />
                                    <asp:TextBox ID="txtGST" runat="server" AutoComplete="off" Height="30px" Width="60%" Style="text-transform: uppercase;" class="form-control" data-toggle="tooltip" MaxLength="15" data-placement="right" AutoPostBack="true" title="GST" OnTextChanged="txtGST_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span>TIN</label><br />
                                    <asp:TextBox ID="txtTin" runat="server" AutoComplete="off" Height="30px" Width="60%" Style="text-transform: uppercase;" class="form-control" data-toggle="tooltip" MaxLength="11" data-placement="right" onchange="validateTIN();" onkeypress="return onlyDotsAndNumbers(this,event);" title="TIN"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span>TAN</label>
                                    <br />
                                    <asp:TextBox ID="txtTan" runat="server" AutoComplete="off" Height="30px" Width="60%" Style="text-transform: uppercase;" class="form-control" data-toggle="tooltip" MaxLength="10" data-placement="right" onchange="ValidateTAN();" title="TAN"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span>PAN</label>
                                    <br />
                                    <asp:TextBox ID="txtPan" Style="text-transform: uppercase" AutoComplete="off" runat="server" Height="30px" Width="60%" class="form-control" data-toggle="tooltip" data-placement="right" title="PAN" MaxLength="10" onchange="pancardValidate();"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div id="docs" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                        <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" title="Documents" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                        <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                        <span>
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
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
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" Visible="true" HeaderStyle-HorizontalAlign="Center">
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
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-10 col-sm-12 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remark</label><br />
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Remarks" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Security Amount </label><br />
                                    <asp:TextBox ID="txtsecurity" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Security Amount" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Advance Fee </label><br />
                                    <asp:TextBox ID="txtadvance" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Denatured Qty(BL)" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                                  </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Proc Fee </label><br />
                                    <asp:TextBox ID="txtProc" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Denatured Qty(BL)" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>--%>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div runat="server" id="start">
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                    <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <p>&nbsp;</p>
                                <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                     <asp:HiddenField ID="currentlevel" runat="server" />
                                    <asp:HiddenField ID="org_id" runat="server" />
                                    <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                        CssClass="btn btn-info pull-left" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click">
                                                       Save as Draft</asp:LinkButton>
                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click1">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                    <asp:LinkButton ID="btnhodapprover" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg12();" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                    <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                    <asp:LinkButton ID="btnReferBack" Text="Refer Back" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return validationMsg12();" OnClick="btnReferBack_Click" class="fa fa-backward" />
                                    <asp:LinkButton ID="btnIssue" Text="Issue" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return validationMsg12();" OnClick="btnIssue_Click" class="fa fa-backward" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
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
