﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="AccusedDetailsList.aspx.cs" Inherits="UserMgmt.AccusedDetailsList" %>
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
                                <title>Accused Details List</title>
                            </head>
                            <body>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Accused Details List </h2>
                                    <div class="clearfix"></div>
                                </div>
                               <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                <div style="float:right" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn primary" />
                                </div>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                
                                <div class="x_content">
                                    <div > 
                                    <asp:GridView ID="grdAccusedDetailsView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdAccusedDetailsView_PageIndexChanging">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Description of offence" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescriptionofoffence" runat="server" Visible="false"  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="SeizureNo" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="seizureno" runat="server" Visible="true" Text='<%#Eval("seizureno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TableId" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="TableId" runat="server" Visible="true" Text='<%#Eval("seizure_accused_details_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccusedName" runat="server" Visible="true" Text='<%#Eval("accusedname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGender" runat="server" Visible="true" Text='<%#Eval("gender_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Age" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAge" runat="server" Visible="true" Text='<%#Eval("accused_age") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permanent Address" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPermanentAddress" runat="server" Visible="true" Text='<%#Eval("permanentaddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accused Status" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Visible="true" Text='<%#Eval("accusedstatus_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="userid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluserid" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick="btnView_Click" CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  Visible='<%# Eval("record_status").ToString() == "Y" ? false : true %>'  OnClick="btnEdit_Click"  CommandName="Edit"><i class="fa fa-pencil-square-o"> 
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
