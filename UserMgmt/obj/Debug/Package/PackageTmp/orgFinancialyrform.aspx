<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="orgFinancialyrform.aspx.cs" Inherits="UserMgmt.orgFinancialyrform" %>

<asp:Content ID="Home" ContentPlaceHolderID="BodyContent" runat="server">
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

                                        if (document.getElementById('<%=ddOrgnames.ClientID%>').value == 'Select') {
                                            alert("Select  Organization Name");
                                             document.getElementById("<% =ddOrgnames.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                   <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton ID="OrganisationDetails" OnClick="OrganisationDetails_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Details</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="OrganisationFinancialYear" OnClick="OrganisationFinancialYear_Click" runat="server"><span style="color:#fff;font-size:14px;">Organisation Financial Year</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="Module_Master1" OnClick="Module_Master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Module Master</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton ID="Submodule_master1" OnClick="Submodule_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">SubModule Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="tab_master1" OnClick="tab_master1_Click" runat="server"><span style="color:#fff;font-size:14px;">Tab Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Organisation Financial Year</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <br />
                                       
                                           <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Organisation Name</label><br />
                                                   
                                                    <asp:TextBox TextMode="MultiLine" Width="200px" title="Organisation Name" ID="txtOrgname" runat="server"></asp:TextBox>
                                                   
                                                        <asp:DropDownList ID="ddOrgnames" Height="30px" Width="350px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Organisation Name" CssClass="form-control"   Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>

                                                        </asp:DropDownList>
                                                </div>
                                         <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Financial Year</label><br />
                                                    
                                                        <input type="text" readonly id="txtyear" runat="server" placeholder="YYYY-YYYY" data-toggle="tooltip" data-placement="right" title="Organisation Type" class="form-control" maxlength="10" style="height: 28px; width: 106px; margin-left: 8px">
                                             </div>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <asp:HiddenField ID="orgid" runat="server" />
                                               <p>&nbsp;</p>
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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

