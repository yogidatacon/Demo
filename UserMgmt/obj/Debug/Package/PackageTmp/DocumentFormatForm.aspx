<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DocumentFormatForm.aspx.cs" Inherits="UserMgmt.DocumentFormatForm" %>

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
                                <title>Document Format Form</title>
                                <script type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlPartyName.ClientID%>').value == '') {
                                            alert("Select  PartyName");
                                            return false;
                                            document.getElementById("<% =ddlPartyName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtNoc.ClientID%>').value == '') {
                                            alert("Enter  Noc");
                                            return false;
                                            document.getElementById("<% =txtNoc.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtPass.ClientID%>').value == '') {
                                            alert("Enter  Pass");
                                            return false;
                                            document.getElementById("<% =txtPass.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtReleaseRequest.ClientID%>').value == '') {
                                            alert("Enter  Release Request");
                                            return false;
                                            document.getElementById("<% =txtReleaseRequest.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtMolassesAllotment.ClientID%>').value == '') {
                                            alert("Enter  Molasses Allotment");
                                            return false;
                                            document.getElementById("<% =txtMolassesAllotment.ClientID%>").focus();
                                        }

                                        if (document.getElementById('<%=txtpermit.ClientID%>').value == '') {
                                            alert("Enter  Permit");
                                            return false;
                                            document.getElementById("<% =txtpermit.ClientID%>").focus();
                                        }

                                    }
                                </script>

                            </head>
                            <body>
                                <div>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Document Format Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Party Name</label><br />
                                                <asp:DropDownList ID="ddlPartyName" runat="server" data-toggle="tooltip" Height="30px" Width="250px" data-placement="right" title="Party Name" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Noc</label><br />
                                                <asp:TextBox ID="txtNoc" AutoComplete="off" Style="text-transform: uppercase" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Noc" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Pass</label><br />
                                                <asp:TextBox ID="txtPass" AutoComplete="off" Style="text-transform: uppercase" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Pass" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Release Request</label><br />
                                                <asp:TextBox ID="txtReleaseRequest" AutoComplete="off" Style="text-transform: uppercase" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Release Request" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Molasses Allotment</label><br />
                                                <asp:TextBox ID="txtMolassesAllotment" AutoComplete="off" Style="text-transform: uppercase" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Molasses Allotment" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit</label><br />
                                                <asp:TextBox ID="txtpermit" AutoComplete="off" Style="text-transform: uppercase" runat="server" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Permit" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <div class="col-md-9 col-sm-9 col-xs-9">
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
