<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SeverConfigList.aspx.cs" Inherits="UserMgmt.SeverConfigList" %>

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
                                <title>Server Configuration List</title>
                            </head>
                            <body>


                                <div class="row">

                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="x_panel">
                                            <a>
                                                <asp:LinkButton runat="server" CssClass="myButton3" ID="AddRecords" OnClick="AddRecords_Click" Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton></a>
                                            <div class="x_title">
                                                <h2>Server Configuration List</h2>
                                                <div class="clearfix"></div>

                                            </div>
                                            <div class="x_content">
                                                <asp:GridView ID="ServerConfigurationList" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCode" runat="server" Visible="true" Text='<%#Eval("server_code") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Visible="true" Text='<%#Eval("server_user") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Domain" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDomain" runat="server" Visible="true" Text='<%#Eval("server_domain") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="URL" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblURL" runat="server" Visible="true" Text='<%#Eval("server_url") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Password" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPassword" runat="server" Visible="false" Text='<%#Eval("server_password") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick="btnView_Click" CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                </asp:LinkButton>
                                                                <%--   <asp:LinkButton Text="Edit" id="LinkButton1"  CssClass="myButton1"   runat="server" OnClick="btnEdite_Click" CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                            </asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                  <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" CssClass="paginationClass" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                </asp:GridView>

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
