<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RoleMasterForm.aspx.cs" Inherits="UserMgmt.RoleMasterForm" %>

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

                                        if (document.getElementById('<%=txtRoleName.ClientID%>').value == '') {
                                            alert("Enter  Role Name");
                                            return false;
                                            document.getElementById("<% =txtRoleName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddRolelevels.ClientID%>').value == 'Select') {
                                            alert("Select  Role Levels");
                                            return false;
                                            document.getElementById("<% =ddRolelevels.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddAccessType.ClientID%>').value == 'Select') {
                                            alert("Select  Access Type");
                                            return false;
                                            document.getElementById("<% =ddAccessType.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddNextrole.ClientID%>').value == 'Select') {
                                            alert("Select Parent Role");
                                            return false;
                                            document.getElementById("<% =ddNextrole.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddorgnnames.ClientID%>').value == 'Select') {
                                            alert("Select Orgnization");
                                            return false;
                                            document.getElementById("<% =ddorgnnames.ClientID%>").focus();
                                        }
                                   <%--   if (document.getElementById('<%=txtSanctionStrength.ClientID%>').value == '') {
                                            alert("Enter Sanction Strength");
                                            return false;
                                            document.getElementById("<% =txtSanctionStrength.ClientID%>").focus();
                                        }--%>

                                    }


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
                                <script>
                                     function chkDuplicateRoleName() {
                                        debugger;
                                        var email = $('#BodyContent_txtRoleName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtRoleName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RoleMasterForm.aspx/chkDuplicateRoleName",
                                            data: '{rolename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Role Name is already exists");
                                                    $('#BodyContent_txtRoleName').val('');
                                                    $('#BodyContent_txtRoleName').focus();
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
                                            <asp:LinkButton ID="StateMaster" OnClick="StateMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton ID="DivisionMaster" OnClick="DivisionMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="DistrictMaster" OnClick="DistrictMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="ThanaMaster" OnClick="ThanaMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Thana Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="RoleMaster1" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Role Master Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">

                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Role Name</label><br />


                                                <asp:TextBox ID="txtRoleName" Style="text-transform:capitalize" AutoComplete="off" CssClass="form-control" data-toggle="tooltip" data-placement="right" onchange="chkDuplicateRoleName();" title="Role Name" Width="350px" Height="30px" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Role Level</label><br />
                                                <asp:DropDownList ID="ddRolelevels" Height="30px" Width="65%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Role Level" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Access Type</label><br />



                                                <asp:DropDownList ID="ddAccessType" Height="30px" Width="65%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Access Type" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>

                                                </asp:DropDownList>

                                            </div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Parent Role</label><br />

                                                <asp:DropDownList ID="ddNextrole" Height="30px" Width="65%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Parent Name" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="NA" Value=""></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Organization</label><br />
                                                <asp:DropDownList ID="ddorgnnames" Height="30px" Width="65%" AutoPostBack="true"  runat="server" data-toggle="tooltip" data-placement="right" title="Organisation Name" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>

                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Sanction Strength</label><br />



                                                <asp:TextBox ID="txtSanctionStrength" AutoComplete="off" Style="text-transform:uppercase"  data-toggle="tooltip" CssClass="form-control" data-placement="right" title="Sanction Strength" AutoCompleteType="Disabled" Width="350px" Height="30px" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>

                                            </div>


                                            <div>
                                                <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>
                                            <asp:HiddenField ID="lblid" runat="server" />
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                  <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
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
