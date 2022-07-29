<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RawMaterialTypeMaster.aspx.cs" Inherits="UserMgmt.RawMaterialTypeMaster" %>
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
                                <title>Raw Material Type Master</title>
                                 <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                       
                                         if (document.getElementById('<%=txtRawMaterialTypecode.ClientID%>').value == '') {
                                             alert("Enter  Raw Material Type Code");
                                              document.getElementById("<% =txtRawMaterialTypecode.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         if (document.getElementById('<%=txtRawname.ClientID%>').value == '') {
                                             alert("Enter  Raw Material Type Name");
                                              document.getElementById("<% =txtRawname.ClientID%>").focus();
                                            return false;
                                           
                                         }
                                         
                                        
                                    }
                                </script>
                                <script>
                                    function CheckDuplicatesName() {

                                        var email = $('#BodyContent_txtRawname').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtRawname').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RawMaterialTypeMaster.aspx/CheckDuplicatesName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Raw Meterial Type Name is already exists");
                                                    $('#BodyContent_txtRawname').val('');
                                                    $('#BodyContent_txtRawname').focus();
                                                }

                                            }
                                        });
                                    }
                                    function CheckDuplicatesCode() {

                                        var email = $('#BodyContent_txtRawMaterialTypecode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtRawMaterialTypecode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RawMaterialTypeMaster.aspx/CheckDuplicatesCode",
                                            data: '{code:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Raw Meterial Type Code is already exists");
                                                    $('#BodyContent_txtRawMaterialTypecode').val('');
                                                    $('#BodyContent_txtRawMaterialTypecode').focus();
                                                }

                                            }
                                        });
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
                                    <li >
                                        <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                     <li >  <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                     <li class="active">  <asp:LinkButton ID="RawMaterialTypeMaster1" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial1" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                   <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                     <li>
                                        <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                    <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                </ul>
                                    <br />
                                 <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="btnShowRecords" OnClick="ShowRecords_Click" style="float:right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Raw Material Type Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Raw Material Type  code</label><br />
                                        <asp:TextBox ID="txtRawMaterialTypecode" onchange="CheckDuplicatesCode();" Style="text-transform:uppercase" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Raw Material Type code" runat="server" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Raw Material Type Name</label><br />
                                       <%-- <asp:TextBox ID="txtRawMaterialTypeName"  CssClass="form-control" AutoPostBack="false"  Height="3%" Width="60%" data-toggle="tooltip" data-placement="right" title=" Raw Material Type Name" runat="server" MaxLength="50"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtRawname" onchange="CheckDuplicatesName();" Style="text-transform:capitalize" Width="60%" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Raw Material Type Name"  MaxLength="50" ></asp:TextBox>
                                    </div>

                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave"  runat="server" Text="Submit"  CssClass="btn btn-primary"  OnClientClick="javascript:return validationMsg()"  OnClick="btnSave_Click"  />
                                        <asp:Button ID="btnCancel"  runat="server" Text="Cancel"  CssClass="btn btn-danger" OnClick="btnCancel_Click"  />
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
