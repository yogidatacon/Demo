<%@ Page Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SHO_DashBoard.aspx.cs" Inherits="UserMgmt.SHO_DashBoard" %>
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
                                <title>Apparatus List</title>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li>
                                        <asp:LinkButton ID="newComplaints" OnClick="newComplaints_Click"  runat="server"><span style="color:#fff;font-size:14px;">New Complaints </span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="SezierAdded" OnClick="SezierAdded_Click"  runat="server"><span style="color:#fff;font-size:14px;">Seizure Added Complaints</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="FIRfiled" OnClick="FIRfiled_Click"  runat="server"><span style="color:#fff;font-size:14px;">FIR Filed Complaints</span></asp:LinkButton></li>
                                    </ul>
                              <%--  <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>--%>
                                <div class="x_title">
                                    <h2>Call Center Compliant List </h2>
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
                                    </asp:UpdatePanel>
                                --%>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                </div>
                                <div class="x_content">
                                    
                                    <asp:GridView ID="grdComplintList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnRowDataBound="grdComplintList_RowDataBound" OnSelectedIndexChanged="grdComplintList_SelectedIndexChanged" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Complaint No" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomid" runat="server" Visible="true" Text='<%#Eval("comid") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Issue" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblv_issue" runat="server" Visible="true" Text='<%#Eval("v_issue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complaint Source" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomsource" runat="server" Visible="true" Text='<%#Eval("comsource") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complaint Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomname" runat="server" Visible="true" Text='<%#Eval("comname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblphone" runat="server" Visible="true" Text='<%#Eval("phone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Accused Person" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaccperson" runat="server" Visible="true" Text='<%#Eval("accperson") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Raid Location" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnearby" runat="server" Visible="true" Text='<%#Eval("nearby") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="thanacode" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblthanacode" runat="server" Visible="true" Text='<%#Eval("thana_mst_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complaint Status" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomplaintstatus" runat="server" Visible="true" Text='<%#Eval("complaintstatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <%--   <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="Add Sezire" ID="btnView" CssClass="myButton" runat="server"  CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" Visible='<%# Eval("record_status").ToString() == "Y" ? false : true %>' runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                    
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
