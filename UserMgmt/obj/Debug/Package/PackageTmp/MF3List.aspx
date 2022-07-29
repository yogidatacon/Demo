<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MF3List.aspx.cs" Inherits="UserMgmt.MF3List" %>

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
                                <title>Molasses Production Actual List</title>
                                 <script>
                                    $(function () {
                                        $(".ddlsearch1").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdMolassesProvisionalProduction_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li >
                                        <asp:LinkButton ID="MF2" OnClick="MF2_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Provisional (MF2)</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="MF3" OnClick="MF3_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Actual (MF3)</span></asp:LinkButton></li>
                                </ul>
                                <br />

                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecords_Click" Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Molasses Production Actual List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="x_content">
                                    <div >
                                        <asp:GridView ID="grdMolassesProductionActualList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdMolassesProductionActualList_PageIndexChanging" OnDataBound="grdMolassesProductionActualList_DataBound"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                           
                                               <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>

                                                                <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Fiancial Year" Value="financial_year"></asp:ListItem>
                                                                <asp:ListItem Text="Closing Date" Value="crushing_closedate"></asp:ListItem>
                                                                  <asp:ListItem Text="Cane Crushing Qty" Value="cane_crushed_total"></asp:ListItem>
                                                                           <asp:ListItem Text="Production Qty" Value="molasses_plan_next_season"></asp:ListItem>
                                                                 <%--  <asp:ListItem Text="Pass Requested Qty" Value="req_qty"></asp:ListItem>
                                                                  <asp:ListItem Text="Approved Qty" Value="req_qty"></asp:ListItem>
                                                                  <asp:ListItem Text="Pass Generated Qty" Value="lifted_qty"></asp:ListItem>
                                                                      <asp:ListItem Text="Customer/Supplier" Value="name"></asp:ListItem>
                                                                 --%>
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black" onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></span> 
      <span><asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span> 
     

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
                                                <asp:TemplateField HeaderText="Financial Year" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFiancialYear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Closing Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClosing" runat="server" Visible="true" Text='<%#Eval("crushing_closedate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cane Crushing Qty" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCrushing" runat="server" Visible="true" Text='<%#Eval("cane_crushed_total") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Molasses Produced QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProduced" runat="server" Visible="true" Text='<%#Eval("molasses_produced_total") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sugar Produced QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSugar" runat="server" Visible="true" Text='<%#Eval("sugar_produced_total") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                              
                                                            <asp:TemplateField HeaderText="Total Alloted QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAlloted" runat="server" Visible="true" Text='<%#Eval("qty_lifted_total") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="prodid" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Visible="true" Text='<%#Eval("molasses_actual_prod_id") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Visible="true" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Submitted":"Draft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                           
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View" OnClick="btnView_Click"><i class="fa fa-search-plus">  </i> 
                                                        </asp:LinkButton>
                                                        <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click" Visible='<%# Eval("record_status").ToString() == "Y" ? false :true %>' CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                        </asp:LinkButton>
                                                         <asp:LinkButton Text="Print" ID="btnprint" CssClass="myButton11" runat="server" Visible='<%# Eval("record_status").ToString() == "Y" ? true: false %>' OnClick="btnprint_Click" CommandName="Print" ><i class="fa fa-print"> 
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
