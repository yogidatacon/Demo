<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RoleLevelMasterForm.aspx.cs" Inherits="UserMgmt.RoleLevelMasterForm" %>

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

                                        if (document.getElementById('<%=txtLevelName.ClientID%>').value == '') {
                                            alert("Enter  Level Name");
                                            return false;
                                            document.getElementById("<% =txtLevelName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtDescription.ClientID%>').value == '') {
                                            alert("Enter  Description");
                                            return false;
                                            document.getElementById("<% =txtDescription.ClientID%>").focus();
                                        }
                                    }
                                </script>
                                 <script>
                                     function chkDuplicateRoleLevelName() {
                                        debugger;
                                        var email = $('#BodyContent_txtLevelName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtLevelName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RoleLevelMasterForm.aspx/chkDuplicateRoleLevelName",
                                            data: '{rolelevelname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Role Level  Name is already exists");
                                                    $('#BodyContent_txtLevelName').val('');
                                                    $('#BodyContent_txtLevelName').focus();
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
                                        <li class="active">
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleMaster" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Role Level Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>Level Name</label><br />
                                                  
                                                        <asp:TextBox ID="txtLevelName" AutoComplete="off" Style="text-transform:capitalize" CssClass="form-control" data-toggle="tooltip" data-placement="right" onchange="chkDuplicateRoleLevelName();"  title="Level Name" Width="350px" Height="30px" runat="server"></asp:TextBox>
                                                 </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline ">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Description</label><br />
                                                   
                                                        <asp:TextBox ID="txtDescription" AutoComplete="off" Style="text-transform:uppercase" Width="350px" Height="30px" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Description"  runat="server" ></asp:TextBox>

                                                  </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <%-- <div class="col-md-9 col-sm-9 col-xs-9">--%>
                                                <asp:HiddenField ID="txtid" runat="server" />
                                                <br />
                                               <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                       <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />

                                                <%--</div>--%>
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
