<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="AppealList.aspx.cs" Inherits="UserMgmt.AppealList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NestedBodyContent" runat="server">
    
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
                                <title>Appeal Against Acquitta/Conviction List</title>
                            </head>
                            <body>
                               
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Appeal Against Acquitta/Conviction List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                </div>
                                <div class="x_content">
                                   
                                    <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdAppeal_PageIndexChanging">
                                        <Columns>
                                           <asp:TemplateField HeaderText="seizureno" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="seizureno" runat="server" Visible="true" Text='<%#Eval("seizureno") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TableId" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="TableId" runat="server" Visible="true" Text='<%#Eval("seizure_appealdetails_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Name of Court" ItemStyle-Font-Bold="true" Visible="false"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNameofCourt" runat="server" Visible="false" Text='<%#Eval("court_master_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                              <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccusedName" runat="server" Visible="true" Text='<%#Eval("accusedstatus_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accused Status" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccusedStatus" runat="server" Visible="false" Text='<%#Eval("accusedstatus_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Appeal By" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAppealBy" runat="server" Visible="true" Text='<%#Eval("appealby") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Result" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderDate" runat="server" Visible="true" Text='<%#Eval("appealresult") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  <asp:TemplateField HeaderText="id" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Visible="true" Text='<%#Eval("seizure_appealdetails_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Draft") %>'></asp:Label>
                                                    </ItemTemplate>
                                             </asp:TemplateField>
                                              <asp:TemplateField HeaderText="userid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluserid" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick="btnView_Click" CommandName="View"><i class="fa fa-search-plus"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click" Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>' CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                    
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
