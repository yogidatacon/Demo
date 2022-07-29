<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MNTW_IssueList.aspx.cs" Inherits="UserMgmt.MNTW_IssueList" %>
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
                                <title>M & TP  Issue List</title>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkRMR" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnIssue" OnClick="btnIssue_Click">
                                        <span style="color: #fff; font-size: 14px;">Issue</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" Visible="false" ID="btnConsumption" OnClick="btnConsumption_Click">
                                        <span style="color: #fff; font-size: 14px;">Consumption</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkOB" OnClick="lnkOB_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  
                                     Style="float: right" OnClick="btnAddRecord_Click"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>M & TP  Issue List</h2>
                                    <div class="clearfix"></div>
                                </div>
                               
                                <div class="x_content">
                                    <div > 
                                        <div id="dummytable" visible="false" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                            <thead>
                                                <tr>
                                                    <th>Issue No.</th>
                                                    <th>Issue Date</th>
                                                    <th>Product Name</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody id="resourcetable">
                                            </tbody>

                                        </table>
                                    </div>
                                    <asp:GridView ID="grdConsumptionList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" 
                                        EmptyDataText="No Records"  
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" 
                                        class="table table-striped responsive-utilities jambo_table" OnRowDataBound="grdConsumptionList_RowDataBound" OnPageIndexChanging="grdConsumptionList_PageIndexChanging">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblconsumption_no" runat="server"  Text='<%#Eval("issue_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application Request No" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblapplication_requestno" runat="server"  Text='<%#Eval("application_requestno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Issue Date" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblconsumption_date" runat="server" Visible="true" Text='<%#Eval("issue_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproduct_name" runat="server" Visible="true" Text='<%#Eval("product_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":Eval("record_status").ToString()=="Y"? "Pending":Eval("record_status").ToString()=="N"? "Draft":"Entry Pending" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View"   CommandName="View" OnClick="btnView_Click"  ID="btnView" 
                                                        CssClass="myButton" runat="server" ><i class="fa fa-search-plus">
                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit"  ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click"  CommandName="Edit" 
                                                        Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>'><i class="fa fa-pencil-square-o"> 
                                                    </i> 
                                                    </asp:LinkButton>
                                                     
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


                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
