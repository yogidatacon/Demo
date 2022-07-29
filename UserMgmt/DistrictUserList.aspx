<%@ page language="C#" autoeventwireup="true" codebehind="DistrictUserList.aspx.cs" inherits="UserMgmt.DistrictUserList" masterpagefile="~/Admin.Master" %>

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
                            </head>
                            <body>
                                <div>
                                    <asp:LinkButton runat="server" CssClass="myButton3 " ID="AddRecord" Style="float: right" OnClick="AddRecord_Click">
                                        <i class="fa fa-plus-circle">ADD NEW RECORD</i>
                                    </asp:LinkButton>

                                    <div class="row">

                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <h2>District Users List</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdDistrictUserList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdDistrictUserList_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="GridViews_RowCommand">
                                                        <columns>
                                                            <%--Bhavin--%>
                                                            <asp:TemplateField HeaderText="District Id" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDistrictId" runat="server" Visible="true" Text='<%#Eval("dist_id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--END--%>

                                                            <asp:TemplateField HeaderText="District Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionName" runat="server" Visible="true" Text='<%#Eval("district_name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--Bhavin--%>
                                                            <asp:TemplateField HeaderText="District Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDistrictCode" runat="server" Visible="true" Text='<%#Eval("district_code") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--End--%>

                                                            <asp:TemplateField HeaderText="Department Name"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionCode" runat="server" Visible="true" Text='<%#Eval("department_name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--Bhavin--%>
                                                             <asp:TemplateField HeaderText="User Id"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsersId" runat="server" Visible="true" Text='<%#Eval("userid") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--END--%>

                                                              <asp:TemplateField HeaderText="Full Name"   ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbelFullName" runat="server" Visible="true" Text='<%#Eval("full_name") %>'></asp:Label> 
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false" HeaderText="User ID" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Password" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPassword" runat="server" Visible="true" Text='<%#Eval("user_password") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email ID"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmailId" runat="server" Visible="true" Text='<%#Eval("email_id") %>'></asp:Label> 
                                                                    <%--Bhavin--%>
                                                                    <br />
                                                                    <asp:Label ID="lblEmailId2" runat="server" Visible="true" Text='<%#Eval("email_id2") %>'></asp:Label> 
                                                                    <br />
                                                                    <asp:Label ID="lblEmailId3" runat="server" Visible="true" Text='<%#Eval("email_id3") %>'></asp:Label> 
                                                                     <%--end--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Mobile No"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobileNo" runat="server" Visible="true" Text='<%#Eval("mobile_no") %>'></asp:Label>
                                                                    <%--Bhavin--%>
                                                                    <br />
                                                                    <asp:Label ID="lblMobileNo2" runat="server" Visible="true" Text='<%#Eval("mobile_no2") %>'></asp:Label>
                                                                    <%--End--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--Bhavin--%>
                                                             <%--<asp:TemplateField HeaderText="Created On"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedOn" runat="server" Visible="true" Text='<%#Eval("created_on") %>'></asp:Label> 
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Created By"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedBy" runat="server" Visible="true" Text='<%#Eval("created_by") %>'></asp:Label> 
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <%--End--%>

                                                             <%--<asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="LinkButton1" CssClass="myButton" runat="server"
                                                                        CommandArgument='<%#Eval("district_login_id") %>'  CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                    </asp:LinkButton> 
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" />
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton1" runat="server"
                                                                        CommandArgument='<%#Eval("district_login_id") %>'  CommandName="Edit"><i class="fa fa-pencil-square-o">
                                                                                    </i> 
                                                                    </asp:LinkButton> 
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" />
                                                            </asp:TemplateField>
                                                            
                                                        </columns>
                                                        <headerstyle backcolor="#26B8B8" forecolor="#ECF0F1" borderstyle="Solid" borderwidth="2px" height="25px" horizontalalign="Center"></headerstyle>
                                                        <pagerstyle backcolor="#26B8B8" borderwidth="2px" height="5px" horizontalalign="Right" forecolor="#ECF0F1"
                                                            verticalalign="Middle" font-size="Medium" font-bold="True" />
                                                        <rowstyle backcolor="Window" borderstyle="Solid" borderwidth="2px" height="25px"></rowstyle>
                                                    </asp:GridView>
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
