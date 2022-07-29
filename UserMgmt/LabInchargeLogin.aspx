<%@ Page Language="C#" AutoEventWireup="true" masterpagefile="~/LabInchargeMaster.Master" CodeBehind="LabInchargeLogin.aspx.cs" Inherits="UserMgmt.LabInchargeLogin" %>

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
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                

                                                  <div>
                                                    <table style="padding: 15px">
                                                        <tr>
                                                            <td><b>Form No. : </b></td>
                                                            <td><asp:textbox runat="server" id="txtForm" /></td>
                                                            <td><b>Status : </b></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddStatus" runat="server">
                                                                     <asp:ListItem Text="All" Value="0" Selected="True"/>
                                                                    <asp:ListItem Text="All Samples Tested" Value="1" />
                                                                    <asp:ListItem Text="All Samples Verified" Value="2" />
                                                                    <asp:ListItem Text="All Samples not Tested" Value="3" />
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnShow" Style="float: right" OnClick="btnShow_Click">
                                                                Show
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                  </div>


                                                <div class="x_content">
                                                    <asp:GridView ID="grdLabInchargeLogin" runat="server" AutoGenerateColumns="false" PageSize="50" AllowPaging="true"
                                                        OnPageIndexChanging="grdLabInchargeLogin_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="grdLabInchargeLogin_RowCommand">
                                                        <columns> 
                                                        <asp:TemplateField HeaderText="Form No" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblForm" runat="server" Visible="true" Text='<%# Eval("form_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Total Sample" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Visible="true" Text='<%# Eval("total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Untested" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUntested" runat="server" Visible="true" Text='<%# Eval("untested") %>'></asp:Label>
                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tested" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTested" runat="server" Visible="true" Text='<%# Eval("tested") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Retest" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRetest" runat="server" Visible="true" Text='<%# Eval("retest") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Verified" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVerified" runat="server" Visible="true" Text='<%# Eval("verified") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="225px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusString" runat="server" Visible="true" Text='<%# Eval("status_string") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                         <ItemTemplate>
                                                             <asp:LinkButton Text="View" ID="btnViewCol" CssClass="myButton1" runat="server"
                                                                 CommandArgument='<%# Eval("form_no") %>'  
                                                                 CommandName="View">
                                                             </asp:LinkButton> 
                                                         </ItemTemplate> 
                                                      </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Notified/Download" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                         <ItemTemplate>
                                                               <asp:LinkButton Text="Email" ID="btnEmailCol" CssClass="myButton1" runat="server"                                                                 
                                                                 CommandName="Email">
                                                             </asp:LinkButton>
                                                             &nbsp;
                                                               <asp:LinkButton Text="SMS" ID="btnSMSCol" CssClass="myButton1" runat="server"                                                                  
                                                                 CommandName="SMS">
                                                             </asp:LinkButton>
                                                             &nbsp;
                                                               <asp:LinkButton Text="Download" ID="btnDownloadCol" CssClass="myButton1" runat="server"                                                                 
                                                                 CommandName="Download">
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
