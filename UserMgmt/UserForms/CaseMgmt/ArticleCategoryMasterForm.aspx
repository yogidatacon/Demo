<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ArticleCategoryMasterForm.aspx.cs" Inherits="UserMgmt.ArticleCategoryMasterForm" %>

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
                                <title>Article Category Master</title>
                               
                                 <script language="javascript" type="text/javascript">

                                    function validationMsg() {
                                      
                                        if (document.getElementById('<%=txtCode.ClientID%>').value == "") {
                                            alert("Enter  Article Category Code");
                                              document.getElementById("<% =txtCode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter  Article Category Name");
                                             document.getElementById("<% =txtName.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>

                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Article Category Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Code</label><br />
                                        <asp:TextBox ID="txtCode" AutoPostBack="true"  class="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title=" Code" runat="server" MaxLength="3" isvalidate="true" OnTextChanged="txtCode_TextChanged" errMsg="Enter code "></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Name</label><br />
                                        <asp:TextBox ID="txtName"  class="form-control" Height="30px" Width="250px" data-toggle="tooltip" AutoComplete="off" data-placement="right" title=" Name" runat="server"   MaxLength="50" isvalidate="true" errMsg="Enter Name "></asp:TextBox>
                                    </div>

                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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
