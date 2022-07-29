<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ServerConfigs.aspx.cs" Inherits="UserMgmt.ServerConfigs" %>

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
                                        debugger;
                                        if (document.getElementById('<%=txtCode.ClientID%>').value == '') {
                                            alert("Enter  Code");
                                            return false;
                                            document.getElementById("<% =txtCode.ClientID%>").focus();
                                        }

                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter Name");
                                            return false;
                                            document.getElementById("<%=txtName.ClientID%>").focus();

                                        }

                                        if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
                                            alert("Enter Password");
                                            return false;
                                            document.getElementById("<%=txtPassword.ClientID%>").focus();

                                        }
                                        if (document.getElementById('<%=txtdomain.ClientID%>').value == '') {
                                            alert("Enter Domain");
                                            return false;
                                            document.getElementById("<%=txtdomain.ClientID%>").focus();

                                        }
                                        if (document.getElementById('<%=txturl.ClientID%>').value == '') {
                                            alert("Enter URL");
                                            return false;
                                            document.getElementById("<%=txturl.ClientID%>").focus();

                                        }

                                    }
                                </script>
                                <script type="text/javascript">
                                    function ShowHidePassword() {
                                        debugger;
                                        var txt = $('#<%=txtPassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                            $("label[for='cbShowHidePassword1']").text("Hide Password");
                                        }
                                        else {
                                            txt.prop("type", "password");
                                            $("label[for='cbShowHidePassword1']").text("Show Password");
                                        }
                                    }


                                </script>
                            </head>
                            <body>
                                <div>
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>

                                    <div class="x_title">
                                        <h2>Server Configuration Form</h2>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Server Code</label><br />
                                            <asp:TextBox ID="txtCode" runat="server" AutoComplete="off" CssClass="form-control" Height="30px" Width="350" data-toggle="tooltip" data-placement="right" title="Server Code"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> User Name</label><br />
                                            <asp:TextBox ID="txtName" runat="server" AutoComplete="off" CssClass="form-control" AutoCompleteType="Disabled" Height="30px" Width="350" data-toggle="tooltip" data-placement="right" title="User Name" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Password</label><br />
                                            <asp:TextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" CssClass="form-control" Height="30px" Width="350" data-toggle="tooltip" data-placement="right" title="Password"  MaxLength="100" TextMode="Password"></asp:TextBox>
                                            <input id="cbShowHidePassword1" name="Show" runat="server" type="checkbox" onclick="ShowHidePassword();" /><label class="control-label"><span style="color: red"></span>Show</label>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Domain</label><br />
                                            <asp:TextBox ID="txtdomain" runat="server" AutoComplete="off" AutoCompleteType="Disabled" CssClass="form-control" Height="30px" Width="350" data-toggle="tooltip" data-placement="right" title="Domain" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5 col-sm-12 col-xs-12 ">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>URL</label><br />
                                            <asp:TextBox ID="txturl" runat="server" AutoComplete="off" AutoCompleteType="Disabled" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="URL" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" />
                                            <%--</div>--%>
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
