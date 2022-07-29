<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="LicenseFeeMaster.aspx.cs" Inherits="UserMgmt.LicenseFeeMaster" %>
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
                                <title>License Fee Master</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                       
                                          if (document.getElementById('<%=txtcode.ClientID%>').value == '') {
                                              alert("Enter License Fee Code");
                                               document.getElementById("<% =txtcode.ClientID%>").focus();
                                            return false;
                                           
                                          }
                                         if (document.getElementById('<%=ddlLicense.ClientID%>').value == 'Select') {
                                              alert("Select License Type Name");
                                               document.getElementById("<% =ddlLicense.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                        if (document.getElementById('<%=ddlsubtype.ClientID%>').value == 'Select') {
                                            alert("Select License Sub Type Name");
                                               document.getElementById("<% =ddlsubtype.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                         if (document.getElementById('<%=txtFee.ClientID%>').value == '') {
                                             alert("Enter Fee Amount");
                                             document.getElementById("<% =txtFee.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtregn.ClientID%>').value == '') {
                                             alert("Enter Regn Amount");
                                             document.getElementById("<% =txtregn.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtsecurity.ClientID%>').value == '') {
                                             alert("Enter Security Amount");
                                             document.getElementById("<% =txtsecurity.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtadvance.ClientID%>').value == '') {
                                             alert("Enter Advance Fee");
                                             document.getElementById("<% =txtadvance.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtProc.ClientID%>').value == '') {
                                             alert("Enter Proc Fee");
                                             document.getElementById("<% =txtProc.ClientID%>").focus();
                                            return false;
                                         }

                                         if (document.getElementById('<%=txtrenewal.ClientID%>').value == '') {
                                             alert("Enter Renewal Fee");
                                             document.getElementById("<% =txtrenewal.ClientID%>").focus();
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

                                    function chkDuplicateUOMName() {
                                        debugger;
                                        var uomname = $('#BodyContent_txtName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "LicenseFeeMaster.aspx/chkDuplicateUOMName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("License SubType Name is already exists");
                                                    $('#BodyContent_txtName').val('');
                                                    $('#BodyContent_txtName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateUOMCode() {
                                        debugger;
                                        var uomode = $('#BodyContent_txtcode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtcode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "LicenseFeeMaster.aspx/chkDuplicateUOMCode",
                                            data: '{code:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Liense Fee Code is already exists");
                                                    $('#BodyContent_txtcode').val('');
                                                    $('#BodyContent_txtcode').focus();
                                                }

                                            }
                                        });
                                    }
                                </script>

                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                     <li>
                                         <asp:LinkButton ID="license" OnClick="license_Click" runat="server"><span style="color:#fff;font-size:14px;">License Master</span></asp:LinkButton></li>
                                       <li>
                                            <asp:LinkButton ID="licensesub" OnClick="licensesub_Click"  runat="server"><span style="color:#fff;font-size:14px;">License Sub Type</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="licensefee" OnClick="licensefee_Click" runat="server"><span style="color:#fff;font-size:14px;">License Fee</span></asp:LinkButton></li>
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
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>License Fee Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>License Fee Code</label><br />
                                        <asp:TextBox ID="txtcode" AutoComplete="off" onchange="chkDuplicateUOMCode();" Style="text-transform:uppercase"  class="form-control" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="License Fee Code" runat="server" MaxLength="10"></asp:TextBox>
                                    </div>
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>License Type Name</label><br />
                                      <asp:DropDownList ID="ddlLicense" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="License Type Name"  ></asp:DropDownList>
                                    </div>
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>License Sub Type Name</label><br />
                                      <asp:DropDownList ID="ddlsubtype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="License Sub Type Name"  ></asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Fee Amount </label><br />
                                    <asp:TextBox ID="txtFee" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Fee Amount" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Regn Amount </label><br />
                                    <asp:TextBox ID="txtregn" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Regn Amount" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Security Amount </label><br />
                                    <asp:TextBox ID="txtsecurity" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Security Amount" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Advance Fee </label><br />
                                    <asp:TextBox ID="txtadvance" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Advance Fee" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                                  </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Proc Fee </label><br />
                                    <asp:TextBox ID="txtProc" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Proc Fee" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                     <label class="control-label" style="display:inline"><span style="color: red">*</span>Renewal Fee</label><br />
                                    <asp:TextBox ID="txtrenewal" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Renewal Fee" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"  ></asp:TextBox>
                                  </div>
                                 <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="org_id" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
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
