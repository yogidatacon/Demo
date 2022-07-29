<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductMasterForm.aspx.cs" Inherits="UserMgmt.ProductMasterForm" %>

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
                                <title>Vat Type Master</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {


                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter Product  Name");
                                            document.getElementById("<% =txtName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtcode.ClientID%>').value == '') {
                                            alert("Enter Product  Code");
                                            document.getElementById("<% =txtcode.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=DDProduct.ClientID%>').value == 'Select') {
                                            alert("Enter  Product Type Name ");
                                            document.getElementById("<% =DDProduct.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
                                       <script>
                                    function chkDuplicateProductName() {
                                        debugger;
                                        var email = $('#BodyContent_txtName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "ProductMasterForm.aspx/chkDuplicateProductName",
                                            data: '{productname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Product Name is already exists");
                                                    $('#BodyContent_txtName').val('');
                                                    $('#BodyContent_txtName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateProductCode() {

                                        var email = $('#BodyContent_txtcode').val();
                                        //if ($('#BodyContent_txtcode').val().length != 3) {
                                        //    alert("Product Product Code length sholud be 3");
                                        //    $('#BodyContent_txtcode').val('');
                                        //    return;
                                        //}
                                        //else {
                                            var jsondata = JSON.stringify($('#BodyContent_txtcode').val());
                                            $.ajax({
                                                type: "POST",
                                                //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                                url: "ProductMasterForm.aspx/chkDuplicateProductCode",
                                                data: '{productcode:' + jsondata + '}',
                                                datatype: "application/json",
                                                contentType: "application/json; charset=utf-8",
                                                cache: false,
                                                async: false,
                                                success: function (msg) {

                                                    if (parseInt(msg.d) > 0) {
                                                        alert("Product Code is already exists");
                                                        $('#BodyContent_txtcode').val('');
                                                        $('#BodyContent_txtcode').focus();
                                                    }

                                                }
                                            });
                                        //}
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

                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li>
                                        <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                    <li>
                                            <asp:LinkButton ID="partyfinancialyears" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Financial Year</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                     <li >  <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                     <li >  <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                   <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                     <li>
                                        <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                     <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>
                                </ul>
                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Product  Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Product Code</label><br />
                                        <asp:TextBox ID="txtcode" AutoComplete="off" onchange="chkDuplicateProductCode();" Style="text-transform:uppercase" onkeypress="return onlyDotsAndNumbers(this,event);"  class="form-control" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" code" runat="server" MaxLength="3"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Product Name</label><br />
                                        <asp:TextBox ID="txtName" AutoComplete="off" onchange="chkDuplicateProductName();" Style="text-transform:capitalize"  class="form-control" CssClass="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title=" Name" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Product Type Name</label><br />
                                        <asp:DropDownList ID="DDProduct" class="form-control" runat="server" CssClass="form-control" Style="text-transform:capitalize" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Product"></asp:DropDownList>
                                    </div>  
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="org_id" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
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
