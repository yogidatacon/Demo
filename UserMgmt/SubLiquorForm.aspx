<%@ page language="C#" autoeventwireup="true" codebehind="SubLiquorForm.aspx.cs" inherits="UserMgmt.SubLiquorForm" masterpagefile="~/Admin.Master" %>

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
                                        if ($.trim(document.getElementById('<%=txtSubLiquorName.ClientID%>').value) == '') {
                                            alert("Enter Liquor Sub Type Name");
                                            return false;
                                            document.getElementById("<% =txtSubLiquorName.ClientID%>").focus();
                                        } 
                                    } 
                                </script>  
                            </head>
                            <body>
                                <div>
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click">
                                            <i class="fa fa-list ">SHOW RECORD LIST</i>
                                        </asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Sub Liquor Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>Liquor Type</label><br /> 
                                                <asp:DropDownList ID="ddlLiquorNames" Height="30px" Width="250px" runat="server" data-toggle="tooltip" data-placement="right"
                                                    title="Liquor Type" CssClass="form-control" Style="" DataTextField="type_of_liquor_name" DataValueField="type_of_liquor_id">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>  
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Sub Liquor Name</label><br />
                                                <asp:TextBox ID="txtSubLiquorName" style="width: 250px" AutoComplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Sub Liquor Name" class="form-control capitalize" runat="server">
                                                </asp:TextBox>

                                            </div>
                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>
                                            <p>&nbsp;</p>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline"> 
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="javascript:return validationMsg()" />
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
