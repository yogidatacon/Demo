<%@ page language="C#" autoeventwireup="true" codebehind="ThanaForm.aspx.cs" inherits="UserMgmt.ThanaForm" masterpagefile="~/Admin.Master" %>

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
                                        if ($.trim(document.getElementById('<%=txtThanaName.ClientID%>').value) == '') {
                                            alert("Enter Thana Name");
                                            return false;
                                            document.getElementById("<% =txtThanaName.ClientID%>").focus();
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
                                            <h2>Thana Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>District Name</label><br /> 
                                                <asp:DropDownList ID="ddlDistrictNames" Height="30px" Width="250px" runat="server" data-toggle="tooltip" data-placement="right"
                                                    title="District Name" CssClass="form-control" Style="" DataTextField="Value" DataValueField="Key">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList> 
                                                <span id="errorDistrictName" style="color: red; font: bold; display: none; text-align: right; margin-left: 160px;" data-toggle="tooltip"
                                                    data-placement="right" title="State Name">Please Select District Name...</span>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Thana Name</label><br />
                                                <asp:TextBox ID="txtThanaName" style="width: 250px" AutoComplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Thana Name" class="form-control capitalize" runat="server">
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
