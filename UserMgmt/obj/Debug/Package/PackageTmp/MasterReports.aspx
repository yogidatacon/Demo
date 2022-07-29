<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MasterReports.aspx.cs" Inherits="UserMgmt.MasterReports" %>

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
                                        if (document.getElementById('<%=txtReportName.ClientID%>').value == '') {
                                            alert("Enter  Report Name");
                                            return false;
                                            document.getElementById("<% =txtReportName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlModuleName.ClientID%>').value == '--Select--') {
                                            alert("Select Module Name");
                                            return false;
                                            document.getElementById("<% =ddlModuleName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlactive.ClientID%>').value == '--Select--') {
                                            alert("Select  Active");
                                            return false;
                                            document.getElementById("<% =ddlactive.ClientID%>").focus();
                                        }

                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">

                                        <li class="active">
                                            <asp:LinkButton ID="UserReport" OnClick="UserReport_Click" runat="server"><span style="color:#fff;font-size:14px;">Master Reports</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">

                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <h2>Master Reports</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><a style="color: red;">*</a>Report Name</label><br />

                                                        <asp:TextBox ID="txtReportName" CssClass="form-control" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Report Name"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><a style="color: red;">*</a>Report File Name</label><br />

                                                        <asp:TextBox ID="txtreportfilename" CssClass="form-control" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Report Name"></asp:TextBox>
                                                    </div>
                                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><a style="color: red;">*</a>Party Type</label><br />
                                                        <asp:DropDownList ID="ddpartytype" CssClass="form-control" Height="30px" Width="250px" runat="server" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                   
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><a style="color: red;">*</a>Module Name</label><br />
                                                        <asp:DropDownList ID="ddlModuleName" CssClass="form-control" Height="30px" Width="250px" runat="server" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <p>&nbsp;</p>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                            <label class="control-label"><a style="color: red;">*</a>Active</label><br />
                                                            <asp:DropDownList ID="ddlactive" Height="30px" Width="250px" CssClass="form-control" runat="server">
                                                                <asp:ListItem Enabled="true" Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Yes" Value="Active"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="No" Value="InActive"></asp:ListItem>

                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                        <%-- <div>
                                                        <label class="control-label"><a style="color: red;">*</a>Report Path</label><br />

                                                        <asp:TextBox ID="TextReportPath" runat="server" Height="35px" data-toggle="tooltip" data-placement="right" title="Report Path" width="250px"></asp:TextBox>
                                                    </div>--%>
                                                        <p>&nbsp;</p>
                                                        <p>&nbsp;</p>
                                                        <p>&nbsp;</p>
                                                        <div>
                                                            <asp:HiddenField ID="txtid" runat="server" />
                                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                                <asp:Button ID="btnsave" runat="server" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" Text="Save" OnClick="btnsave_Click" />
                                                                <asp:Button ID="tbnCancel" runat="server" class="btn btn-danger" Text="Cancel" OnClick="tbnCancel_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
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
