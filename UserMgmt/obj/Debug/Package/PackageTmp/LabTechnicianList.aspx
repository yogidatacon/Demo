<%@ page language="C#" masterpagefile="~/LabTechMaster.Master" autoeventwireup="true" codebehind="LabTechnicianList.aspx.cs" inherits="UserMgmt.LabTechnicianList" %>

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
                                <div class="table-responsive">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <h2>Lab Technician List</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdReceivingSectionList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                                        OnPageIndexChanging="grdReceivingSectionList_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="GridReceivingSectionList_RowCommand">
                                                        <columns> 
                                                            <asp:TemplateField HeaderText="Liquor Type" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLiqType" runat="server" Visible="true" Text='<%#Eval("LiqType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Type" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%#Eval("LiqSubType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSize" runat="server" Visible="true" Text='<%#Eval("LiqSize") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%#Eval("LiqQuant") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brand" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBrand" runat="server" Visible="true" Text='<%#Eval("LiqBrand") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Batch No." ItemStyle-Font-Bold="true" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBatch" runat="server" Visible="true" Text='<%#Eval("LiqBatch") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address" ItemStyle-Font-Bold="true" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Visible="true" Text='<%#Eval("LiqAddr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Visible="true" Text='<%#Eval("LiqStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Form No" ItemStyle-Font-Bold="true" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFormNo" runat="server" Visible="true" Text='<%#Eval("FormNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFormDate" runat="server" Visible="true" Text='<%#Eval("FormDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Compactor ID" ItemStyle-Font-Bold="true" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompID" runat="server" Visible="true" Text='<%#Eval("CompId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="Report" ID="LinkButton1" CssClass="btn btn-primary" runat="server"
                                                                        CommandArgument='<%# Eval("QuantId") + "," + Eval("FormNo") + "," + Eval("LiqStatus") %>'  
                                                                        CommandName="Edit">
                                                                    </asp:LinkButton> 
                                                                </ItemTemplate> 
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
