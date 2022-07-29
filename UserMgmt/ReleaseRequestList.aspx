<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReleaseRequestList.aspx.cs" Inherits="UserMgmt.ReleaseRequestList" %>


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
                                <title>Molasses Release Request List</title>
                                  <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdMolassesReleaseRequest_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                                  <div runat="server" id="MTB">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="ReleaseRequestMolasses"  runat="server" OnClick="btnReleaseRequestMolasses_Click"><span style="color:#fff;font-size:14px;">Release Request of Molasses</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="IssuedReleaseRequestLetter" runat="server" OnClick="btnIssuedReleaseRequestLetter_Click"><span style="color:#fff;font-size:14px;">Release Request Applied List</span></asp:LinkButton></li>
                                          <%--<li >
                                            <asp:LinkButton ID="PassRequest" runat="server" OnClick="PassRequest_Click"><span style="color:#fff;font-size:14px;">Request For Pass List</span></asp:LinkButton></li>--%>
                                    </ul>
                                      </div>
                                <div runat="server" id="ETB">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="btnReleaseRequestMolasses_Click"><span style="color:#fff;font-size:14px;">Release Request of ENA/Spirit</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnIssuedReleaseRequestLetter_Click"><span style="color:#fff;font-size:14px;">Release Request Applied List</span></asp:LinkButton></li>
                                          <%--<li >
                                            <asp:LinkButton ID="PassRequest" runat="server" OnClick="PassRequest_Click"><span style="color:#fff;font-size:14px;">Request For Pass List</span></asp:LinkButton></li>--%>
                                    </ul>
                                      </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  
                              
                                <div runat="server" id="molasses" class="x_title">
                                    <h2>Molasses Release Request List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div runat="server" id="ENA"  class="x_title">
                                    <h2>ENA/Spirits  Release Request List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                </div>
                                <div class="x_content">
                                    <div > 
                                    <asp:GridView ID="grdMolassesReleaseRequest" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdMolassesReleaseRequest_PageIndexChanging"  OnDataBound="grdMolassesReleaseRequest_DataBound"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                            
                                                            
                                      <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" CssClass="ddlsearch1s" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Financial Year" Value="financial_year"></asp:ListItem>
                                                  <asp:ListItem Text="Allotment No" Value="final_allotmentno"></asp:ListItem>
                                             <asp:ListItem Text="Product Name" Value="product_name"></asp:ListItem>
                                               <asp:ListItem Text="From Unit Name" Value="party_name"></asp:ListItem>
                                                                    <asp:ListItem Text="Valid Upto" Value="valid_date"></asp:ListItem>
                                                                           <asp:ListItem Text="Indent QTY" Value="prov_indent_qty"></asp:ListItem>
                                                                  <asp:ListItem Text="Alloted Qty" Value="allotted_qty"></asp:ListItem>
                                               <asp:ListItem Text="Approved QTY" Value="b.rrapqty"></asp:ListItem>
                                               <asp:ListItem Text="Balance QTY" Value="rr_balance_qty"></asp:ListItem>
                                               <asp:ListItem Text="RR QTY" Value="rr_quantity"></asp:ListItem>
                                                                 
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black"  onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
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
                                              <asp:Templatefield headertext="Financial Year" itemstyle-font-bold="true" itemstyle-width="10px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblfinancial_year" Font-Size="Smaller"  runat="server" text='<%#Eval("financial_year") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                            
                                             <asp:TemplateField HeaderText="Allotment No" ItemStyle-Font-Bold="true"  ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllotmentNo1" Font-Size="Smaller"  runat="server"  Text='<%#Eval("final_allotment_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allotment No" ItemStyle-Font-Bold="true" Visible="false"  ItemStyle-Width="1px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllotmentNo" Font-Size="Smaller"  runat="server"  Text='<%#Eval("rr_allotmentno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("product_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="product_code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproduct_code" runat="server" Visible="true" Text='<%#Eval("product_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Unit Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfromUnitName" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("party_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valid Upto" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidUpto" Font-Size="Smaller"  runat="server"  Text='<%#Eval("valid_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Indent QTY" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvisionalIndentQTY" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("prov_indent_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="MA Alloted QTY" ItemStyle-Font-Bold="true"  ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllotedQTY" Font-Size="Smaller"  runat="server"  Text='<%#Eval("allocation_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RR Approved QTY" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovedQTY" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rr_approved_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Balance QTY" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBalanceQTY" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rr_balance_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RR QTY" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestedQTY" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rr_quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Request" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="New Request" ID="btnNewRequest" CssClass="btn btn-round" runat="server" OnClick="btnNewRequest_Click" CommandName="New Request">
                                                    </asp:LinkButton>
                                                   <%--  <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" Visible="false" CommandName="Edit" OnClick="btnNewRequest_Click" ><i class="fa fa-pencil-square-o"> 
                                                    </i> 
                                                    </asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"  />

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
