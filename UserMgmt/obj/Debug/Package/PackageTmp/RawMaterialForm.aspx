<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RawMaterialForm.aspx.cs" Inherits="UserMgmt.RawMaterialForm" %>
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
                                       
                                         if (document.getElementById('<%=txtRawMaterialCod.ClientID%>').value == '') {
                                            alert("Enter Raw Material Code");
                                            return false;
                                            document.getElementById("<% =txtRawMaterialCod.ClientID%>").focus();
                                         }
                                         if (document.getElementById('<%=txtRawMaterialNam.ClientID%>').value == '') {
                                             alert("Enter Raw Material Name");
                                            return false;
                                            document.getElementById("<% =txtRawMaterialNam.ClientID%>").focus();
                                         }
                                         if (document.getElementById('<%=ddRawMaterialType.ClientID%>').value == 'Select') {
                                            alert("Select Raw Material Type");
                                            return false;
                                            document.getElementById("<% =ddRawMaterialType.ClientID%>").focus();
                                         }
                                         if (document.getElementById('<%=DDProduct.ClientID%>').value == 'Select') {
                                            alert("Select Product Type");
                                            return false;
                                            document.getElementById("<% =DDProduct.ClientID%>").focus();
                                        }
                                         if (document.getElementById('<%=ddUOM.ClientID%>').value == 'Select') {
                                            alert("Select UOM");
                                            return false;
                                            document.getElementById("<% =ddUOM.ClientID%>").focus();
                                         }
                                        
                                    }
                                </script>
                                <script>
                                    function CheckDuplicatesName() {

                                        var email = $('#BodyContent_txtRawMaterialNam').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtRawMaterialNam').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RawMaterialForm.aspx/CheckDuplicatesName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Raw Meterial Name is already exists");
                                                    $('#BodyContent_txtRawMaterialNam').val('');
                                                    $('#BodyContent_txtRawMaterialNam').focus();
                                                }

                                            }
                                        });
                                    }
                                    function CheckDuplicatesCode() {

                                        var email = $('#BodyContent_txtRawMaterialCod').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtRawMaterialCod').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RawMaterialForm.aspx/CheckDuplicatesCode",
                                            data: '{code:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Raw Meterial Code is already exists");
                                                    $('#BodyContent_txtRawMaterialCod').val('');
                                                    $('#BodyContent_txtRawMaterialCod').focus();
                                                }

                                            }
                                        });
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
                                                                                                                                                                            <li >
                                            <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                         <li >  <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                      <li >  <asp:LinkButton ID="RawMaterialTypeMaster1" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li class="active">  <asp:LinkButton ID="RawMaterial1" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                       <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                      <li >
                                            <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                         <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                       </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="btnShowRecords" OnClick="ShowRecords_Click" Style="float: right" ><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Raw Material Master Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <br />
                                            
                                             <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Raw Material Code</label><br />
                                                <%--<input type="text" runat="server" id="txtRawMaterialCode" style="height:3%; width:60%" data-toggle="tooltip" data-placement="right" title="Raw Material Code" maxlength="10"  class="form-control">--%>
                                                 <asp:TextBox ID="txtRawMaterialCod" Style="text-transform:uppercase" onchange="CheckDuplicatesCode();" runat="server" height="3%" width="60%" data-toggle="tooltip" data-placement="right" title="Raw Material Code" maxlength="10" CssClass="form-control"  ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span> Raw Material Name</label><br />
                                             
                                                 <asp:TextBox ID="txtRawMaterialNam" Style="text-transform:capitalize" onchange="CheckDuplicatesName();" runat="server" height="3%" width="60%" data-toggle="tooltip" data-placement="right" title="Raw Material Code" maxlength="50" CssClass="form-control"  ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Raw Material Type</label><br />
                                                <asp:DropDownList ID="ddRawMaterialType" Height="3%" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Raw Material Type" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Product Type Name</label><br />
                                        <asp:DropDownList ID="DDProduct" class="form-control" runat="server" CssClass="form-control" Style="text-transform:capitalize" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Product"></asp:DropDownList>
                                    </div>  
                                             <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>UOM</label>  <br />
                                                <asp:DropDownList ID="ddUOM" Height="3%" Width="60%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="UOM" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                          
                                                <div class="clearfix"></div>

                                             <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="Rawid" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" OnClientClick="javascript:return validationMsg()" class="btn btn-primary" OnClick="btnSave_Click"  />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"  class="btn btn-danger" OnClick="btnCancel_Click"  />
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
