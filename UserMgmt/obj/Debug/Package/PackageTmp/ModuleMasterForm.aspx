<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ModuleMasterForm.aspx.cs" Inherits="UserMgmt.ModuleMasterForm" %>

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

                                        if (document.getElementById('<%=ddOrgNmae.ClientID%>').value == 'Select') {
                                            alert("Enter Organisation Name");
                                            return false;
                                            document.getElementById("<% =ddOrgNmae.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtModuleCode.ClientID%>').value == '') {
                                            alert("Enter Module Code");
                                            return false;
                                            document.getElementById("<% =txtModuleCode.ClientID%>").focus();
                                        }

                                        if (document.getElementById('<%=txtModuleName.ClientID%>').value == '') {
                                            alert("Enter Module Name");
                                            return false;
                                            document.getElementById("<% =txtModuleName.ClientID%>").focus();
                                        }

                                    }
                                </script>
                                <script>
                                    function chkDuplicateModuleName() {
                                        debugger;
                                        var email = $('#BodyContent_txtModuleName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtModuleName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "ModuleMasterForm.aspx/chkDuplicatemodulename",
                                            data: '{modulename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Module  Name is already exists");
                                                    $('#BodyContent_txtModuleName').val('');
                                                    $('#BodyContent_txtModuleName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateModuleCode() {

                                        var email = $('#BodyContent_txtModuleCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtModuleCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "ModuleMasterForm.aspx/chkDuplicatemodulecode",
                                            data: '{modulecode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Module Code is already exists");
                                                    $('#BodyContent_txtModuleCode').val('');
                                                    $('#BodyContent_txtModuleCode').focus();
                                                }

                                            }
                                        });
                                    }
                                    </script>
                            </head>
                            <body>
                                <div class="row">
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton ID="OrganisationDetails" OnClick="OrganisationDetails_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Details</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="OrganisationFinancialYear" OnClick="OrganisationFinancialYear_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Financial Year</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="Module_Master1" OnClick="Module_Master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Module Master</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton ID="Submodule_master1" OnClick="Submodule_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">SubModule Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="tab_master1" OnClick="tab_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Tab Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                        <div class="x_title">
                                            <h2>Module Master Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                    <div class="x_content">

                                         <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Module Code</label><br />
                                            <asp:TextBox ID="txtModuleCode" onchange="chkDuplicateModuleCode();"  Style="text-transform:uppercase" AutoComplete="off" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Module Code" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Module Name</label><br />
                                            <asp:TextBox ID="txtModuleName" onchange="chkDuplicateModuleName();" Style="text-transform:capitalize" AutoComplete="off" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Module Name" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        </div>
                                       
                                         <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Organisation Name</label>
                                            <br />
                                            <asp:DropDownList ID="ddOrgNmae" runat="server" Height="30px" Width="450px" data-toggle="tooltip" data-placement="right" title="Organisation Name" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                       <p>&nbsp;</p> 
                                         <p>&nbsp;</p>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:HiddenField ID="moduleid" runat="server" />
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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
