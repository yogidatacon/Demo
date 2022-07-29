<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master"  AutoEventWireup="true" CodeBehind="VehicleList.aspx.cs" Inherits="UserMgmt.VehicleList" %>
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
                                <title>Vehicle List</title>
                            </head>
                            <body>
                            
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Vehicle List </h2>
                                    <div class="clearfix"></div>
                                </div>
                               <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
                                   
                                    <asp:GridView ID="grdExcisableArticlesView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdExcisableArticlesView_PageIndexChanging">

                                        <Columns>
                                             <asp:TemplateField HeaderText="SeizureNo" ItemStyle-Font-Bold="true"  ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="seizureno" runat="server" Visible="true" Text='<%#Eval("seizureno") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TableId" ItemStyle-Font-Bold="true"  ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="TableId" runat="server" Visible="true" Text='<%#Eval("seizure_vehicledetails_id") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle Type" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleType" runat="server" Visible="true" Text='<%#Eval("vehicle_type_code") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleName" runat="server" Visible="true" Text='<%#Eval("vehiclename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chassis No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNo" runat="server" Visible="true" Text='<%#Eval("chasisno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration Number" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="18px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegistrationNumber" runat="server" Visible="true" Text='<%#Eval("registrationno") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerName" runat="server" Visible="true" Text='<%#Eval("ownername") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Owner Permanent Address" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerPermanentAddress" runat="server" Visible="true" Text='<%#Eval("permanentaddress") %>'></asp:Label>

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
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" Visible='<%# Eval("record_status").ToString() == "Y" ? false : true %>' OnClick="btnEdit_Click" CommandName="Edit"><i class="fa fa-pencil-square-o"> 
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
