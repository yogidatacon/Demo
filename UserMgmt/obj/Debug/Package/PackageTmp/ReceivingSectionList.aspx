<%@ page language="C#" masterpagefile="~/ReceivingSectionMaster.Master" autoeventwireup="true" codebehind="ReceivingSectionList.aspx.cs" inherits="UserMgmt.ReceivingSectionList" %>

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
                                    <asp:LinkButton runat="server" CssClass="myButton3 " ID="AddRecord" Style="float: right" OnClick="AddEntryForm_Click">
                                        <i class="fa fa-plus-circle">ADD NEW RECORD</i>
                                    </asp:LinkButton>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <h2>Receiving Section List</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdReceivingSectionList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                                        OnPageIndexChanging="grdReceivingSectionList_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="GridReceivingSectionList_RowCommand">
                                                        <columns> 
                                                            <%--Jay20220228--%>
                                                        <asp:TemplateField HeaderText="Form No" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label2" runat="server" Visible="true" Text='<%#Eval("receiving_section_id") %>'></asp:Label>--%>
                                                                <asp:Label ID="Label2" runat="server" Visible="true" Text='<%#Eval("form_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Type of liquor" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblTypeOfLiquor" runat="server" Visible="true" Text='<%#Eval("type_of_liquor_name") %>'></asp:Label>--%>
                                                                <asp:Label ID="lblTypeOfLiquor" runat="server" Visible="true" Text='<%#Eval("liq_type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%#Eval("liquor_sub_name") %>'></asp:Label>--%>
                                                                <%--<asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%#Eval("liq_sub_type_name") %>'></asp:Label>--%>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="160px">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblSize" runat="server" Visible="true" Text='<%#Eval("size_master_name") %>'></asp:Label>--%>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%#Eval("quantity") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Brand Name" ItemStyle-Font-Bold="true" ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblBrandName" runat="server" Visible="true" Text='<%#Eval("brand_master_name") %>'></asp:Label>--%>
                                                                <%--<asp:Label ID="lblBrandName" runat="server" Visible="true" Text='<%#Eval("brand_name") %>'></asp:Label>--%>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Batch No"  ItemStyle-Font-Bold="true" ItemStyle-Width="120px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBatchNo" runat="server" Visible="true" Text='<%#Eval("batch_no") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Address of the manufacturer"  ItemStyle-Font-Bold="true" ItemStyle-Width="220px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Visible="true" Text='<%#Eval("address") %>'></asp:Label> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compactor"  ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblCompactor" runat="server" Visible="true" Text='<%#Eval("compactor_name") %>'></asp:Label>--%> 
                                                                <%--<asp:Label ID="lblCompactor" runat="server" Visible="true" Text='<%#Eval("comp_id") %>'></asp:Label>--%> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Save Status"  ItemStyle-Font-Bold="true" ItemStyle-Width="60px">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="Label1" runat="server" Visible="true" Text='<%#Eval("issaved_text") %>'></asp:Label>--%> 
                                                                <asp:Label ID="Label1" runat="server" Visible="true" Text='<%#Eval("status") %>'></asp:Label> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Action22" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                         <ItemTemplate>
                                                             <%--Bhavin-03072022--%>
                                                             <asp:LinkButton Text="View" ID="LinkButton1" CssClass="myButton1" runat="server"
                                                                 
                                                                 CommandArgument='<%# Eval("quant_received_id") + "," + Eval("form_no") %>'
                                                                 CommandName="Edit">
                                                                 <i class="fa fa-pencil-square-o"> </i> 
                                                             </asp:LinkButton>
                                                             <%--End--%>
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
