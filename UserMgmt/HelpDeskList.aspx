﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HelpDeskList.aspx.cs" Inherits="UserMgmt.HelpDeskList" %>

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
                            <title>HelpDesk Ticket List</title>
                        </head>
                        <body>
                            <%--<ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="Ticket" OnClick="Ticket_Click"  runat="server"><span style="color:#fff;font-size:14px;">Ticket Status Master</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton ID="Priority" OnClick="Priority_Click"  runat="server"><span style="color:#fff;font-size:14px;">Priority Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="Helpdesk" OnClick="Helpdesk_Click"  runat="server"><span style="color:#fff;font-size:14px;">Help Desk Ticket</span></asp:LinkButton></li>
                                       
                                    </ul>--%>
                                    <br />
                        <%--    <div class="row">--%>

                               <%-- <div class="col-md-12 col-sm-12 col-xs-12">--%>
                                   <%-- <div class="x_panel">--%>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="x_title">
                                            <h2>HelpDesk List</h2>
                                            <%-- <div  style="float:right">
                                                         <asp:TextBox ID="txtSearch" runat="server" Width="250px" AutoComplete="off"   Height="30px" placeholder="Search Access Type Name"  AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" ></asp:TextBox>
                                                       <span><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></span> 
                                                    </div>--%>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <%--    <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>--%>
                                            <asp:GridView ID="grdHelpdeskList" OnDataBound="grdHelpdeskList_DataBound" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdHelpdeskList_PageIndexChanging"
                                                HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                
                                                <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />

                                                        <PagerTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page"  CommandArgument="First" CssClass="myButton1"><i class="fa fa-step-backward"> </i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="myButton1"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                <asp:ImageButton ID="btnFirst" runat="server" Width="30px" Height="20px" CommandArgument="First" Visible="false" CommandName="Page"  BackColor="Blue" ForeColor="White"
                    ImageUrl="~/img/icons8-first-50.png" /> <asp:ImageButton ID="btnPrev" runat="server" Visible="false"
                        CommandArgument="Prev" CommandName="Page" Width="30px" Height="20px" BackColor="Blue" ImageUrl="~/img/icons8-previous-50.png" /> <asp:DropDownList
                            ID="DDLPage" runat="server" AutoPostBack="True"  Visible="false"  Width="250px" ForeColor="Black" Font-Bold="true">
                        </asp:DropDownList>&nbsp;&nbsp; <asp:TextBox ID="txtpage" runat="server" Height="20px" AutoPostBack="true" TextMode="Number" ForeColor="Black" Width="50px" Font-Bold="true" OnTextChanged="txtpage_TextChanged"></asp:TextBox> <asp:Label ID="lblCurrent" Visible="false" runat="server"></asp:Label>
                of
              <asp:Label ID="lblPages" runat="server" Height="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page"  CommandArgument="Next" CssClass="myButton1"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page"  CommandArgument="Last" CssClass="myButton1"><i class="fa fa-step-forward"> </i></asp:LinkButton>
                                                            
                <asp:ImageButton ID="btnNext" Visible="false"
                    runat="server" CommandArgument="Next" Width="30px" Height="20px" CommandName="Page" ForeColor="Blue" BackColor="Blue" ImageUrl="~/img/icons8-next-50.png"  /> <asp:ImageButton
                        ID="btnLast" runat="server" CommandArgument="Last" Width="30px" Visible="false" Height="20px" BackColor="Blue" CommandName="Page" ImageUrl="~/img/icons8-last-50.png" />
            </PagerTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Ticket Raised Date"  ItemStyle-Font-Bold="true" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTicketRaisedDate" runat="server" Visible="true" Text='<%#Eval("creation_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticket"  ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTicket" runat="server" Visible="true" Text='<%#Eval("ticketno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticket Raised By"  ItemStyle-Font-Bold="true" ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTicketRaisedby" runat="server" Visible="true" Text='<%#Eval("party_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Form Name" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormName" runat="server" Visible="true" Text='<%#Eval("ticket_formname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Developer"  ItemStyle-Font-Bold="true" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeveloper" runat="server" Visible="true" Text='<%#Eval("developer") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tester"  ItemStyle-Font-Bold="true" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTester" runat="server" Visible="true" Text='<%#Eval("tester") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Priority"  ItemStyle-Font-Bold="true" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPriority" runat="server" Visible="true" Text='<%#Eval("priority") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticket Query" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblticketquery" runat="server" Visible="true" Text='<%#Eval("ticket_query") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticketid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblticketid" runat="server" Visible="true" Text='<%#Eval("helpdesk_ticket_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "T" ? "Re-Test" :Eval("record_status").ToString() == "O" ? "Open" :Eval("record_status").ToString() == "N" ? "In Progress" :Eval("record_status").ToString()=="R"? "Resolved":(Eval("record_status").ToString()=="C"? "Complete":"Pending") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10px"  >
                                                        <ItemTemplate>
                                                          <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick="btnView_Click" CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                            </asp:LinkButton>
                                                            <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" Visible='<%# Eval("record_status").ToString() =="T" ? true:Eval("record_status").ToString() =="N" ? true:Eval("record_status").ToString() =="O" ? true:Eval("record_status").ToString() =="R" ? true:Eval("record_status").ToString() =="P"? true:false%>' OnClick="btnEdit_Click" CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"  />

                                                <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                            </asp:GridView>
                                        </div>
                                        <%--</ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                    <%--</div>--%>
                              <%--  </div>--%>
                           <%-- </div>--%>
                        </body>
                        </html>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
