<%@ Page Language="C#" AutoEventWireup="true" masterpagefile="~/DistrictOfficerMaster.Master" CodeBehind="DistrictOfficerView.aspx.cs" Inherits="UserMgmt.DistrictOfficerView" %>

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
                                                            <td><asp:label id="lblForm" runat="server"></asp:label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Letter No. : </b></td>
                                                            <td><asp:label id="lblLetter" runat="server"></asp:label></td>
                                                        </tr>
                                                        <%if (depart_name=="excise") {%>
                                                            <tr>
                                                                <td><b>Case No. : </b></td>
                                                                <td><asp:label id="lblCase" runat="server"></asp:label></td>
                                                            </tr>
                                                        <%} %>
                                                        <%if (depart_name=="police") {%>
                                                            <tr>
                                                                <td><b>FIR No. : </b></td>
                                                                <td><asp:label id="lblFir" runat="server"></asp:label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Thana Name : </b></td>
                                                                <td><asp:label id="lblThana" runat="server"></asp:label></td>
                                                            </tr>
                                                        <%} %>
                                                        <%if (depart_name=="distillery") {%>
                                                            <tr>
                                                                <td><b>Reference No. : </b></td>
                                                                <td><asp:label id="lblRef" runat="server"></asp:label></td>
                                                            </tr>
                                                        <%} %>
                                                        <tr>
                                                            <td><b>District : </b></td>
                                                            <td><asp:label id="lblDIst" runat="server"></asp:label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Date : </b></td>
                                                            <td><asp:label id="lblDate" runat="server"></asp:label></td>
                                                        </tr>
                                                    </table>
                                                    
                                                  </div>


                                                <div class="x_content">
                                                    <asp:GridView ID="grdDistrictOfficerView" runat="server" AutoGenerateColumns="false" PageSize="50" AllowPaging="true"
                                                        OnPageIndexChanging="grdDistrictOfficerView_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                        <columns> 
                                                        <asp:TemplateField HeaderText="Sample" ItemStyle-Font-Bold="false" ItemStyle-Width="700px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <table style="border: 1px solid black">
                                                                    <tr style="border: 1px solid black">
                                                                        <td>
                                                                            <b>Liquor Sample Type :</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblType" runat="server" Visible="true" Text='<%# Eval("liq_type") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Indication :
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInd" runat="server" Visible="true" Text='<%# Eval("indication") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Temperature :
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblTemp" runat="server" Visible="true" Text='<%# Eval("temperature") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Proof Strength:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblProof" runat="server" Visible="true" Text='<%# Eval("proof") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                     <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Color:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblColor" runat="server" Visible="true" Text='<%# Eval("color") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                     <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Smell:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSmell" runat="server" Visible="true" Text='<%# Eval("smell") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border: 1px solid black">
                                                                        <td>
                                                                            Remarks :
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblRemarks" runat="server" Visible="true" Text='<%# Eval("remarks") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Visible="true" Text='<%# Eval("status") %>'></asp:Label>
                                                                </br>
                                                                <asp:Label ID="lblFinalStatus" runat="server" Visible="true" Text='<%# Eval("final_status") %>'></asp:Label>
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
