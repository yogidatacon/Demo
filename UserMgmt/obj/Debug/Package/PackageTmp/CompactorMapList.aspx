<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CompactorMapList.aspx.cs" Inherits="UserMgmt.CompactorMapList" %>

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
                                <title>Compactor Management</title>
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
                                                    <h2>Compactor List</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdCompactorList" runat="server" AutoGenerateColumns="false" PageSize="50" AllowPaging="true" 
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnPageIndexChanging="grdCompactorList_PageIndexChanging" OnRowCommand="GridViews_RowCommand">
                                                        <columns>

                                                             <asp:TemplateField HeaderText="Compactor Id" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompactorId" runat="server" Visible="true" Text='<%#Eval("compactor_id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Compactor Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompactorName" runat="server" Visible="true" Text='<%#Eval("comp_name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                           
                                                            <asp:TemplateField HeaderText="User Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                
                                                                      <asp:LinkButton Text=""  ID="btnEdit" CssClass="myButton1" runat="server"
                                                                        CommandArgument='<%#Eval("compactor_id") %>'  CommandName="Edit_Record"> 
                                                                          <i class="fa fa-pencil-square-o"></i>
                                                                    </asp:LinkButton> 
                                                                    <asp:LinkButton Text="" ID="btnDel" CssClass="deleteButton" runat="server"
                                                                        CommandArgument='<%#Eval("compactor_id") %>'  CommandName="Delete_Record"> 
                                                                          <i class="fa fa-trash"></i>
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
