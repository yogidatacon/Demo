<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CasteMasterForm.aspx.cs" Inherits="UserMgmt.CasteMasterForm" %>
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
                                <title>Caste Master</title>
                                   <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtcode.ClientID%>').value == '') {
                                            alert("Enter  Caste Code");
                                              document.getElementById("<% =txtcode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter  Caste Name");
                                             document.getElementById("<% =txtName.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=DDReligion.ClientID%>').value == 'Select') {
                                             alert("Select  Religion");
                                             document.getElementById("<% =DDReligion.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                                <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" style="float:right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Caste Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Code</label><br />
                                        <asp:TextBox ID="txtcode" ReadOnly="true" AutoPostBack="true" class="form-control" data-toggle="tooltip" AutoComplete="off" data-placement="right" title="Code" runat="server" OnTextChanged="txtcode_TextChanged" MaxLength="3"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Name</label><br />
                                        <asp:TextBox ID="txtName"  AutoPostBack="true" class="form-control"  Height="30px"  Width="250px" data-toggle="tooltip" data-placement="right" title=" Name" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label style="font-size: small"><span style="color: red">*</span>Caste Category</label><br />
                                        <asp:DropDownList ID="ddlCastecategory" runat="server" class="form-control" data-toggle="tooltip"  data-placement="right" title="Religion" MaxLength="3">
                                            <asp:ListItem Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label style="font-size: small"><span style="color: red">*</span>Religion</label><br />
                                        <asp:DropDownList ID="DDReligion" runat="server" class="form-control" data-toggle="tooltip"  data-placement="right" title="Religion" MaxLength="3">
                                            <asp:ListItem Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()"  OnClick="btnSave_Click"  />
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
