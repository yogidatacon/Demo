<%@ page language="C#" autoeventwireup="true" codebehind="ParameterList.aspx.cs" inherits="UserMgmt.ParameterList" masterpagefile="~/Admin.Master" %>


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
                                <title>Parameter Management</title>
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
                                                    <h2>Parameter List</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdParameterList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdParameterList_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="GridViews_RowCommand">
                                                        <columns> 

                                                             <%--Bhavin--%>
                                                             <asp:TemplateField HeaderText="Parameter Id"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParameterId" runat="server" Visible="true" Text='<%#Eval("parameter_master_id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--End--%>

                                                            <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParameterName" runat="server" Visible="true" Text='<%#Eval("parameter_master_name") %>'></asp:Label>

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

                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%--Bhavin--%>
                                                                    <%--<asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server"
                                                                        CommandArgument='<%#Eval("parameter_master_id") %>'  CommandName="Edit"><i class="fa fa-pencil-square-o">
                                                                                    </i> 
                                                                    </asp:LinkButton> --%>

                                                                    <asp:LinkButton Text="" ID="btnEdit" CssClass="myButton1" runat="server"
                                                                        CommandArgument='<%#Eval("parameter_master_id") %>'  CommandName="Edit">
                                                                         <i class="fa fa-pencil-square-o"></i>
                                                                                 
                                                                    </asp:LinkButton> 
                                                                    
                                                                    <asp:LinkButton Text="" ID="btnDel" CssClass="deleteButton" runat="server"
                                                                        CommandArgument='<%#Eval("parameter_master_id") %>'  CommandName="Delete_Record">
                                                                         <i class="fa fa-trash"></i>
                                                                    </asp:LinkButton> 
                                                                    <%--End--%>
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
