<%@ page language="C#" masterpagefile="~/DistrictOfficerMaster.Master" autoeventwireup="true" codebehind="DistrictOfficerLogin.aspx.cs" inherits="UserMgmt.DistrictOfficerLogin" %>

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
                                                <script>
                                               <%-- $(function() {
                                                    $("#<%= fromDate.ClientID %>").datepicker({
                                                        format: "yyyy-mm-dd"
                                                    });
                                                     $("#<%= toDate.ClientID %>").datepicker({
                                                        format: "yyyy-mm-dd"
                                                    });
                                                });--%>
                                                </script>

                                                  <div>
                                                    <table style="padding: 15px">
                                                        <tr>
                                                            <td><b>District : </b></td>
                                                            <td><asp:Label ID="dolDistrictLabel" runat="server" Visible="true" Text='district'></asp:Label></td>
                                                            <td><b>Department : </b></td>
                                                            <td><asp:Label ID="dolDepartLabel" runat="server" Visible="true" Text='department'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>From : </td>
                                                            <td><asp:TextBox ID="fromDate" runat="server" TextMode="Date" /></td>
                                                            <td>To : </td>
                                                            <td><asp:TextBox ID="toDate" runat="server" TextMode="Date" /></td>  
                                                            <td>
                                                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="DOLShow" Style="float: right" OnClick="DOLShow_Click">
                                                                Show
                                                                </asp:LinkButton>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                    
                                                  </div>


                                                <div class="x_content">
                                                    <asp:GridView ID="grdDistrictOfficerLogin" runat="server" AutoGenerateColumns="false" PageSize="50" AllowPaging="true"
                                                        OnPageIndexChanging="grdDistrictOfficerLogin_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="grdDistrictOfficerLogin_RowCommand">
                                                        <columns> 
                                                        <asp:TemplateField HeaderText="District" ItemStyle-Font-Bold="true" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Visible="true" Text='<%# Eval("district_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Form No." ItemStyle-Font-Bold="true" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeOfLiquor" runat="server" Visible="true" Text='<%# Eval("form_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Form Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%# Eval("form_date") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSize" runat="server" Visible="true" Text='<%# Eval("status") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                         <ItemTemplate>
                                                             <asp:LinkButton Text="View" ID="LinkButton1" CssClass="myButton1" runat="server"
                                                                 CommandArgument='<%# Eval("form_no") + "," + Eval("district_name") %>'  
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
