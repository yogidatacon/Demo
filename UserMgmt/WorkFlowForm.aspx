<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="workflowForm.aspx.cs" Inherits="UserMgmt.workflowForm" %>

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
                                <style>
                                    .cht th {
                                        text-align: center;
                                    }
                                </style>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlsubmodules.ClientID%>').value == 'Select') {
                                            alert("Select  SubModule Name");
                                            return false;
                                            document.getElementById("<% =ddlsubmodules.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlTabnames.ClientID%>').value == 'Select') {
                                            alert("Select Tab Name");
                                            return false;
                                            document.getElementById("<% =ddlTabnames.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlRoleName.ClientID%>').value == 'Select') {
                                            alert("Select  Role");
                                            return false;
                                            document.getElementById("<% =ddlRoleName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlDistrict.ClientID%>').value == 'Select') {
                                            alert("Select  District");
                                            return false;
                                            document.getElementById("<% =ddlDistrict.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlUsers.ClientID%>').value == 'Select') {
                                            alert("Select  User Name");
                                            return false;
                                            document.getElementById("<% =ddlUsers.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddlApproverlevel.ClientID%>').value == 'Select') {
                                            alert("Select  Approver Level");
                                            return false;
                                            document.getElementById("<% =ddlApproverlevel.ClientID%>").focus();
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="WorkFlowDetails" runat="server" OnClick="WorkFlowDetails_Click"><span style="color:#fff;font-size:14px;">Work Flow Details</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Work Flow Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Sub Module Name</label><br />
                                                <asp:DropDownList ID="ddlsubmodules" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="SubModule Name" name="ddlsubmodules" OnSelectedIndexChanged="ddlsubmodules_SelectedIndexChanged" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Tab Name</label><br />
                                                <asp:DropDownList ID="ddlTabnames" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" name="ddlTabnames" data-placement="right" title="Tab Name" OnSelectedIndexChanged="ddlTabnames_SelectedIndexChanged" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Role Name</label><br />
                                                <asp:DropDownList ID="ddlRoleName" Height="30px" Width="250px" AutoPostBack="true" Font-Names="ddlRoleName" runat="server" data-toggle="tooltip" data-placement="right" title="Role Name" OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>District Name</label><br />

                                                <asp:DropDownList ID="ddlDistrict" name="ddlDistrict" Height="30px" Width="250px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"  AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="District Name" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>User Name</label><br />

                                                <asp:DropDownList ID="ddlUsers" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="User Name" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Approvel Level Code</label><br />

                                                <asp:DropDownList ID="ddlApproverlevel" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Approvel Level Code" CssClass="form-control" Style="">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="5" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="lblStatecode" runat="server" />
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                                <div class="col-md-9 col-sm-9 col-xs-12">
                                                    &nbsp; 
                                                    <asp:LinkButton Text="Add" ID="btnAdd" CssClass="btn btn-succ" runat="server" OnClick="btnAdd_Click" OnClientClick="javascript:return validationMsg()"><i class="fa fa-plus-circle" style="font-size: 15px"></i> ADD
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div id="dummytable" runat="server" style="height: auto; width: 98%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                                <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                                    <thead>
                                                        <tr>
                                                            <th>Sub Module Name</th>
                                                            <th>Tab Name</th>
                                                            <th>Role Name</th>
                                                            <th>District Name</th>
                                                            <th>User Name</th>
                                                            <th>Approver Level</th>
                                                            <th><i class="fa fa-trash"></i></th>
                                                        </tr>

                                                    </thead>
                                                    <tbody id="resourcetable">
                                                    </tbody>

                                                </table>
                                            </div>
                                            <div style="height: auto; width: 98%; border: 0px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7; grid-column-align: center" runat="server">
                                                <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" AllowPaging="true" HeaderStyle-CssClass="cht" OnPageIndexChanged="grdAdd_PageIndexChanged" PageSize="5"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sub Module Name" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <%-- <input ID="lblSubModuleName" runat="server" Visible="true" Text='<%#Eval("SubModule_Name") %>'  ></input>--%>
                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblSubModuleName" type="text" value='<%#Eval("SubModule_Name") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Module Name" ItemStyle-Font-Bold="true" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblSubModuleCode" runat="server" Visible="false" Text='<%#Eval("SubModule_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tab Name" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblTab_Name" type="text" value='<%#Eval("Tab_Name") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tab Name" ItemStyle-Font-Bold="true" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lbltab_id" runat="server" Visible="false" Text='<%#Eval("tab_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role Name" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblRole_Name" type="text" value='<%#Eval("Role_Name") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role Name" ItemStyle-Font-Bold="true" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblRole_name_code" runat="server" Visible="false" Text='<%#Eval("Role_name_code") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblDistrictName" type="text" value='<%#Eval("District_Name") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name" Visible="false" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblDistrict_Code" runat="server" Visible="false" Text='<%#Eval("District_Code") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblUserName" type="text" value='<%#Eval("User_Name") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" ItemStyle-Font-Bold="true" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUser_Registration_id" runat="server" Visible="false" Text='<%#Eval("user_registration_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approver Level" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <input class="form-control validate[required]" style="width: 100%; text-align: center" readonly="" autocomplete="off" name="Role" id="lblApproverLevel" type="text" value='<%#Eval("Approver_Level") %>'>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approver Level" Visible="false" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblApproverLevel1" runat="server" Visible="false" Text='<%#Eval("Approver_Level1") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-BackColor="#f5f6f7" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                              
                                                                    <asp:ImageButton ID="btnRemove" OnClick="btnRemove_Click" CommandName="Remove" ImageUrl="~/img/delete.gif"  runat="server" />             
                                                               

                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                    <PagerStyle BackColor="#26B8B8" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Right" ForeColor="#ECF0F1" />

                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></RowStyle>
                                                </asp:GridView>
                                            </div>

                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        </div>
                                        <p>&nbsp;</p>

                                        <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                            <%-- <div class="col-md-9 col-sm-9 col-xs-9">--%>
                                            <asp:HiddenField ID="txtid" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />

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

