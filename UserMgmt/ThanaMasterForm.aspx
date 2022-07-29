<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ThanaMasterForm.aspx.cs" Inherits="UserMgmt.ThanaMasterForm" %>
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
                                <title>Thana Master</title>

                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                         if (document.getElementById('<%=ddDistrict.ClientID%>').value == 'Select'){
                                            alert("Enter District");
                                        return false;
                                        document.getElementById("<%=ddDistrict.ClientID%>").focus();
                                         }
                                         if (document.getElementById('<%=DDDivision.ClientID%>').value == 'Select'){
                                            alert("Enter Division");
                                        return false;
                                        document.getElementById("<%=DDDivision.ClientID%>").focus();
                                         }
                                         if (document.getElementById('<%=DDState.ClientID%>').value == 'Select'){
                                            alert("Enter State");
                                        return false;
                                        document.getElementById("<%=DDState.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtcode.ClientID%>').value == ' ') {
                                            alert("Enter Code");
                                            return false;
                                            document.getElementById("<% =txtcode.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtName.ClientID%>').value == ' '){
                                            alert("Enter Name");
                                        return false;
                                        document.getElementById("<%=txtName.ClientID%>").focus();
                                        }

                                        
                                    }
                                </script>

                            </head>
                            <body>

                                 <ul class="nav nav-tabs">
                                 <li >
                                            <asp:LinkButton ID="StateMaster" OnClick="StateMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton ID="DivisionMaster" OnClick="DivisionMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="DistrictMaster" OnClick="DistrictMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="ThanaMaster" OnClick="ThanaMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Thana Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleMaster" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>
                                    </ul>
                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click"  Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Thana Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline ">
                                        <label style="font-size: small"><span style="color: red">*</span>State </label>
                                        <br />
                                        <asp:DropDownList ID="DDState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDState_SelectedIndexChanged" CssClass="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="State">
                                            <asp:ListItem Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline ">
                                        <label style="font-size: small"><span style="color: red">*</span>Division </label>
                                        <br />
                                        <asp:DropDownList ID="DDDivision" AutoPostBack="true" OnSelectedIndexChanged="DDDivision_SelectedIndexChanged" runat="server" CssClass="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Division">
                                            <asp:ListItem Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline ">
                                        <label style="font-size: small"><span style="color: red">*</span>District </label>
                                        <br />
                                        <asp:DropDownList ID="ddDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddDistrict_SelectedIndexChanged" runat="server" CssClass="form-control" Height="30px" Width="250px"  data-toggle="tooltip" data-placement="right" title="District ">
                                            <asp:ListItem Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                    </div>
                                    
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Thana Code</label><br />
                                        <asp:TextBox ID="txtcode" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Code" runat="server" MaxLength="3"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span> Name</label><br />
                                        <asp:TextBox ID="txtName" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title=" Name" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <p>&nbsp;</p>
                                <p>&nbsp;</p>
                                <p>&nbsp;</p>
                                <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                    <asp:HiddenField ID="txtid" runat="server" />
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"   OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click"  />
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
