<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SubModuleMasterForm.aspx.cs" Inherits="UserMgmt.SubModuleMasterForm" %>
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
                                       

                                         if (document.getElementById('<%=txtSubModuleCode.ClientID%>').value == '') {
                                            alert("Enter SubModule Code");
                                            return false;
                                            document.getElementById("<% =txtSubModuleCode.ClientID%>").focus();
                                         }

                                         if (document.getElementById('<%=txtSubModuleName.ClientID%>').value == '') {
                                            alert("Enter SubModule Name");
                                            return false;
                                            document.getElementById("<% =txtSubModuleName.ClientID%>").focus();
                                         }

                                         if (document.getElementById('<%=ddModuleNme.ClientID%>').value == 'Select') {
                                             alert("Enter Module Name");
                                            return false;
                                            document.getElementById("<% =ddModuleNme.ClientID%>").focus();
                                         }

                                         if (document.getElementById('<%=ddOrgNmae.ClientID%>').value == 'Select') {
                                             alert("Enter Organisation Name");
                                            return false;
                                            document.getElementById("<% =ddOrgNmae.ClientID%>").focus();
                                         }

                                      <%--  

                                          if (document.getElementById('<%=txtDescription.ClientID%>').value == 'Select') {
                                              alert("Enter Description");
                                            return false;
                                            document.getElementById("<% =txtDescription.ClientID%>").focus();
                                        }--%>
                                    }
                                </script>
                                <script>
                                    function chkDuplicateSubmodulename() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtSubModuleName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtSubModuleName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "SubModuleMasterForm.aspx/chkDuplicateSubmodulename",
                                            data: '{submodulename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("SubModule Name is already exists");
                                                    $('#BodyContent_txtSubModuleName').val('');
                                                    $('#BodyContent_txtSubModuleName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateSubmodulecode() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtSubModuleCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtSubModuleCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "SubModuleMasterForm.aspx/chkDuplicateSubmodulecode",
                                            data: '{submodulecode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("SubModule Code is already exists");
                                                    $('#BodyContent_txtSubModuleCode').val('');
                                                    $('#BodyContent_txtSubModuleCode').focus();
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
                                        <li>
                                            <asp:LinkButton ID="Module_Master1" OnClick="Module_Master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Module Master</span></asp:LinkButton></li>
                                          <li class="active">
                                            <asp:LinkButton ID="Submodule_master1" OnClick="Submodule_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">SubModule Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="tab_master1" OnClick="tab_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Tab Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                         <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                        <div class="x_title">
                                            <h2>Sub Module Master Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Organisation Name</label>  <br />
                                                    <asp:DropDownList ID="ddOrgNmae" runat="server" AutoPostBack="true"  data-toggle="tooltip" data-placement="right" title="Organisation Name" OnSelectedIndexChanged="ddOrgNmae_SelectedIndexChanged" CssClass="form-control" ></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Module Name</label> <br />
                                                 <asp:DropDownList ID="ddModuleNme" runat="server"  data-toggle="tooltip" data-placement="right" title="Module Name" CssClass="form-control" ></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Sub Module Code</label><br />
                                                <asp:TextBox ID="txtSubModuleCode" AutoComplete="off" onchange="chkDuplicateSubmodulecode();" Style="text-transform:uppercase" runat="server"  data-toggle="tooltip" data-placement="right" title="Sub Module Code" CssClass="form-control" MaxLength="3"  ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Sub Module Name</label><br />
                                                <asp:TextBox ID="txtSubModuleName" AutoComplete="off" onchange="chkDuplicateSubmodulename()" Style="text-transform:capitalize" runat="server"  data-toggle="tooltip" data-placement="right" title="Sub Module Name" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                                            </div> 
                                           
                                                <%--<div class="clearfix"></div>
                                               <p>&nbsp;</p>
                                            <div class="col-md-11 col-sm-6 col-xs-12 ">
                                                <label class="control-label" style="font-size:small"><span style="color: red">*</span>Description</label> <br />
                                                <asp:TextBox ID="txtDescription" runat="server" data-toggle="tooltip"  data-placement="right" title="Description" CssClass="form-control" MaxLength="100" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                               <p>&nbsp;</p>--%>
                                           
                                            
                                            <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="orgid" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div></body></html>
                                </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div>
</asp:Content>
