<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PartyTypeMasterForm.aspx.cs" Inherits="UserMgmt.PartyTypeMasterForm" %>

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
                                        if (document.getElementById('<%=txtPartyTypeCode.ClientID%>').value == '') {
                                              alert("Enter Party Type Code");
                                               document.getElementById("<% =txtPartyTypeCode.ClientID%>").focus();
                                            return false;
                                           
                                          }
                                        if (document.getElementById('<%=txtpartytypename.ClientID%>').value == '') {
                                            alert("Enter Party Type Name");
                                             document.getElementById("<% =txtpartytypename.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                          
                                        debugger;
                                        if (document.getElementById('<%=ddlactive.ClientID%>').value == 'Select') {
                                            alert("select Active Type");
                                             document.getElementById("<% =ddlactive.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                    }
                                </script>
                                <script>
                                    function chkDuplicatePartyTypeName() {

                                        var email = $('#BodyContent_txtpartytypename').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtpartytypename').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyTypeMasterForm.aspx/chkDuplicatepartytypename",
                                            data: '{Partytypename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Party Type Name is already exists");
                                                    $('#BodyContent_txtpartytypename').val('');
                                                    $('#BodyContent_txtpartytypename').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicatePartyTypeCode() {

                                        var email = $('#BodyContent_txtPartyTypeCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtPartyTypeCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PartyTypeMasterForm.aspx/chkDuplicatepartytypecode",
                                            data: '{Partytypecode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Party Type Name is already exists");
                                                    $('#BodyContent_txtPartyTypeCode').val('');
                                                    $('#BodyContent_txtPartyTypeCode').focus();
                                                }

                                            }
                                        });
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                     <ul class="nav nav-tabs">
                                <li class="active">
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
                                          <li >  <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                       <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                          <li >
                                            <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                          <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                       </ul> 

                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Party Type Master Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                          
                                              <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                             <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party Type Code</label> <br />
                                              
                                                 <asp:TextBox ID="txtPartyTypeCode" AutoComplete="off" Style="text-transform:uppercase;" class="form-control" onchange="chkDuplicatePartyTypeCode();" Height="30px" Width="250px" MaxLength="3" data-toggle="tooltip" data-placement="right" title="Party Type Code"  runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party Type Name</label>   <br />
                                                <asp:TextBox ID="txtpartytypename" AutoComplete="off" Style="text-transform:capitalize;" class="form-control" onchange="chkDuplicatePartyTypeName();" Height="30px" Width="250px" MaxLength="50" data-toggle="tooltip" data-placement="right" title="Party Type Name"  runat="server"></asp:TextBox>
                                            </div>
                                           
                                           
                                              
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><a style="color: red;">*</a>Active</label>
                                                <br />
                                                            <asp:DropDownList ID="ddlactive" Height="30px" Width="250px" Cssclass="form-control" data-toggle="tooltip" data-placement="right" title="Active"  runat="server">
                                                                <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Yes" Value="True"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="No" Value="False"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                            <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div >
                                                    <asp:HiddenField ID="orgid" runat="server" />
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

