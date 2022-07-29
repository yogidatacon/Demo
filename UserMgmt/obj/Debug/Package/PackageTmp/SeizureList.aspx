<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="SeizureList.aspx.cs" Inherits="UserMgmt.SeizureList" %>

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
                                <title>Seizure List</title>
                                  <script type="text/javascript" src="js/jquery1.min.js"></script>
<script type="text/javascript">
                                      $(function () {
                                          var dtToday = new Date();
                                          debugger;;
                                          var month = dtToday.getMonth() + 1;
                                          var day = dtToday.getDate();
                                          var year = dtToday.getFullYear();
                                          if (month < 10)
                                              month = '0' + month.toString();
                                          if (day < 10)
                                              day = '0' + day.toString();

                                          var minDate = year + '-' + month + '-' + day;

                                          $('#BodyContent_NestedBodyContent_txtraiddate').attr('max', minDate);
                                      });
                                      </script>
                            </head>
                            <body>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecord_Click" Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Seizure List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                                <div>
                                     <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Division</label><br />
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" title="State"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>District</label><br />
                                        <asp:DropDownList ID="ddDistrict" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="District" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                  <div  class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Thana</label><br />
                                         <asp:DropDownList ID="ddlsubthana" runat="server" Width="60%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Thana"></asp:DropDownList>
                                     </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label">SeizureNo</label>
                                    <asp:TextBox ID="txtSearch" runat="server"  CssClass="form-control"></asp:TextBox>
                                  </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label">PR/FIR No</label>
                                    <asp:TextBox ID="txtprfirno" runat="server"  CssClass="form-control"></asp:TextBox>
                                  </div>
                                     <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                             <%-- <label class="control-label">PR/FIR No</label><br />--%>   <br />
                                    <asp:Button ID="btnSearchSeizureNo" runat="server" Text="Search" CssClass="btn btn primary" OnClick="btnSearchSeizureNo_Click" />
                                    <asp:Button ID="btnrefresh" runat="server" OnClick="Button2_Click" Text="Refresh"  CssClass="btn btn-refresh"></asp:Button>
                             </div>
                                 </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                            <div class="x_content">
                                                
                                                    <asp:GridView ID="grdSeizureView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdSeizureView_PageIndexChanging" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seizure No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSeizureNo" runat="server" Visible="true" Text='<%#Eval("seizureno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PR/FIR No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprfirno" runat="server" Visible="true" Text='<%#Eval("prfirno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidDate" runat="server" Visible="true" Text='<%#Eval("raid_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Time" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidTime" runat="server" Visible="true" Text='<%#Eval("raid_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Location" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidLocation" runat="server" Visible="true" Text='<%#Eval("raid_location") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Thana" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblThana" runat="server" Visible="true" Text='<%#Eval("thanaName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server"  OnClick ="btnSubView_Click"  CommandName="View"><i class="fa fa-search-plus"></i>
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
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div style="height: 8%; background-color: #26b8b8;">
                                            <span style="font-size: small; color: white; margin-left: 40%">Unsubmitted  Records</span>
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                         <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                             <label class="control-label" style="font-size:small"><span style="color: red"></span>Search Criteria</label><br />
                                            <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" CssClass="form-control" Width="60%" OnSelectedIndexChanged="ddlSelect_SelectedIndexChange">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>By Thana</asp:ListItem>
                                                <asp:ListItem>By Thana & RaidDate</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                         <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Thana</label><br />
                                         <asp:DropDownList ID="ddlThana" runat="server" Width="60%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Thana"></asp:DropDownList>
                                        </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label">SeizureNo</label>
                                      <asp:TextBox ID="txtunseizureno" runat="server"  CssClass="form-control"></asp:TextBox>
                                      </div>
                                         <div  class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Raid Date</label><br />
                                         <asp:DropDownList ID="ddlRaidDate" runat="server" Width="90%" Visible="false" CssClass="form-control"></asp:DropDownList>
                                             <asp:TextBox ID="txtraiddate" runat="server" TextMode="Date" name="datepicker" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span> </label><br />
                                            <asp:Button ID="btnSearch" runat="server" Width="70%" Text="Search Unsubmitted  Records" CssClass="btn btn primary" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                           <%-- <div class="x_content">--%>
                                                
                                                    <asp:GridView ID="grdUnSubmittedList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdUnSubmittedList_PageIndexChanging" >

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seizure No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lblSeizureNo" runat="server" Visible="true" Text='<%#Eval("seizureno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidDate" runat="server" Visible="true" Text='<%#Eval("raid_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Time" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidTime" runat="server" Visible="true" Text='<%#Eval("raid_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Raid Location" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidLocation" runat="server" Visible="true" Text='<%#Eval("raid_location") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Thana" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblThana" runat="server" Visible="true" Text='<%#Eval("thanaName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="userid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluserid" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick ="btnView_Click" CommandName="View"><i class="fa fa-search-plus"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click" CommandName="Edit"><i class="fa fa-pencil-square-o">
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
                                               <div class="x_title"></div>
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
