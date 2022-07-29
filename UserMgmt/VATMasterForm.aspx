<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="VATMasterForm.aspx.cs" Inherits="UserMgmt.VATMasterForm" EnableEventValidation="true" %>


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
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlPartyType.ClientID%>').value == 'Select') {
                                            alert("Select  Party Type");
                                            return false;
                                            document.getElementById("<% =ddlPartyType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlParty.ClientID%>').value == 'Select') {
                                            alert("Select Party Name");
                                            return false;
                                            document.getElementById("<% =ddlParty.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlProductType.ClientID%>').value == 'Select') {
                                            alert("Select Product Type");
                                            return false;
                                            document.getElementById("<% =ddlProductType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlVatType.ClientID%>').value == 'Select') {
                                            alert("Select  VAT Type");
                                            return false;
                                            document.getElementById("<% =ddlVatType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtVatName.ClientID%>').value == '') {
                                            alert("Enter  VAT Name");
                                            return false;
                                            document.getElementById("<% =txtVatName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtVatCapacity.ClientID%>').value == '') {
                                            alert("Enter  VAT Capacity");
                                            return false;
                                            document.getElementById("<% =txtVatCapacity.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlUOM.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                            return false;
                                            document.getElementById("<% =ddlVatType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlContent.ClientID%>').value == 'Select') {
                                            alert("Select Content");
                                            return false;
                                            document.getElementById("<% =ddlContent.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtDepth.ClientID%>').value == '') {
                                            alert("Enter  Depth");
                                            return false;
                                            document.getElementById("<% =txtDepth.ClientID%>").focus();
                                        }
                                    }
                                </script>
                                <script>
                                    function chkDuplicateVATName() {
                                        debugger; 
                                        var uomname = $('#BodyContent_txtVatName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtVatName').val() + "_" + $('#BodyContent_ddlParty').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "VATMasterForm.aspx/chkDuplicateVATName",
                                            data: '{vatname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("VAT Name is already exists");
                                                    $('#BodyContent_txtVatName').val('');
                                                    $('#BodyContent_txtVatName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function GetValues() {
                                        $('#BodyContent_txtVatName').val("");
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="partyfinancialyears" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Financial Year</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                         <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                    </ul>

                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>VAT Master Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Party Type </label>
                                                <br />
                                                <asp:DropDownList ID="ddlPartyType" Height="30px" Width="250px" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" title="Party Type" OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged" class="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Party Name</label><br />
                                                <asp:DropDownList ID="ddlParty" onchange="GetValues()"  Height="30px" Width="250px"  runat="server" data-toggle="tooltip" data-placement="right" title="Party Name" class="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Product Type</label><br />
                                                <asp:DropDownList ID="ddlProductType"  Height="30px" Width="250px" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" title="Party Type" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" class="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>VAT Type</label><br />
                                                <asp:DropDownList ID="ddlVatType" Height="30px" Width="250px" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" title="VAT Type" OnSelectedIndexChanged="ddlVatType_SelectedIndexChanged" class="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>VAT Name</label><br />
                                                <asp:TextBox ID="txtVatName" AutoComplete="off" onchange="chkDuplicateVATName();" Style="text-transform: capitalize;" class="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="VAT Name" runat="server"></asp:TextBox>
                                                <%-- <input type="text" runat="server" style="background-color:white"  id="txtVatName" Width="170px" data-toggle="tooltip" data-placement="right" title="VAT Name"  class="form-control">--%>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>VAT Capacity</label><br />
                                                <asp:TextBox ID="txtVatCapacity" class="form-control" onchange="chkDuplicatePartyTypeCode();" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="VAT Capacity" runat="server"></asp:TextBox>

                                            </div>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>UOM</label>
                                                <br />
                                                <asp:DropDownList ID="ddlUOM" Height="30px" Width="250px" AutoPostBack="true" class="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="UOM" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Content</label>
                                                <br />
                                                <asp:DropDownList ID="ddlContent" Height="30px" Width="250px" AutoPostBack="true" class="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Content" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Depth in CM</label>
                                                <br />
                                                <asp:TextBox ID="txtDepth" class="form-control" Height="30px" onchange="chkDuplicatePartyTypeCode();" Width="250px" data-toggle="tooltip" data-placement="right" title="Depth in CM" runat="server"></asp:TextBox>
                                            </div>
                                             
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="vatcode" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
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
