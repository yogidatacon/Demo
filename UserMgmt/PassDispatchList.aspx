<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PassDispatchList.aspx.cs" Inherits="UserMgmt.PassDispatchList" %>
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
                                <title>Pass Applied List</title>
                                 <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdPassApplicationList_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                         <li >
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Transport Pass</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click" ><span style="color:#fff;font-size:14px;">Dispatch</span></asp:LinkButton></li>
                                        
                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                               <%-- <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecord_Click" Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>--%>
                                <div class="x_title">
                                    <h2>Dispatch  List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                
                                   
                                 <div class="col-md-4 col-sm-12 col-xs-12 form-inline" id="passfor" runat="server">
                <label class="control-label" style="display:inline"><span style="color:red">*</span>Type of Pass</label> <br />                                          
                                            <asp:DropDownList ID="ddPassFor" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="" OnSelectedIndexChanged="ddPassFor_SelectedIndexChanged" CssClass="form-control" style="">
                                                <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Export" Value="EXP"></asp:ListItem>
                                                <asp:ListItem Text="Domestic " Value="DOM"></asp:ListItem> 
                                              </asp:DropDownList>
                                        </div>
                                            
                 <div id="Pass">
                                <div class="x_content">
                                    <div >
                                        <asp:GridView ID="grdPassApplicationList"  runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdPassApplicationList_PageIndexChanging" OnDataBound="grdPassApplicationList_DataBound"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                         
                                            
                                               <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>

                                                             <asp:DropDownList ID="ddlsearch1" runat="server" CssClass="ddlsearch1s" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="PassNo" Value="pass_reqno"></asp:ListItem>
                                                                <asp:ListItem Text="Issued PassNo" Value="pass_issueno"></asp:ListItem>
                                                                  <asp:ListItem Text="Pass Type" Value="Pass_type"></asp:ListItem>
                                                                           <asp:ListItem Text="RR/NOC No" Value="rr_noc_issuedno"></asp:ListItem>
                                                                   <asp:ListItem Text="Suplier/Customer Name" Value="b.party_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Date of Dispatch" Value="dispatch_date"></asp:ListItem>
                                                                  <asp:ListItem Text="Dispatch QTY" Value="dispatch_qty"></asp:ListItem>
                                                                  <%--<asp:ListItem Text="Brix" Value="brix"></asp:ListItem>--%>
                                                                 
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
                                                <asp:TemplateField HeaderText="PassNo" ItemStyle-Font-Bold="true" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassNo1" runat="server" Visible="true" Text='<%#Eval("pass_reqno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued PassNo" ItemStyle-Font-Bold="true" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssuedPassNo" Font-Size="Smaller" runat="server" Visible="true" Text='<%#Eval("pass_issuedno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PassNo" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassNo" runat="server" Visible="true" Text='<%#Eval("Pass_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Pass Type" ItemStyle-Font-Bold="true" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpass_type" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("Pass_type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NOC No" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrrnoc_record_request_id1" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rr_noc_issuedno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NOC No1" ItemStyle-Font-Bold="true" Visible="false"  ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrrnoc_record_request_id" Font-Size="Smaller"   runat="server" Visible="true" Text='<%#Eval("request_for_pass_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" runat="server" Font-Size="Smaller"  Visible="true" Text='<%#Eval("supplier_unit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                  <%-- <asp:TemplateField HeaderText="Customer id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcid" runat="server" Font-Size="Smaller"  Visible="true" Text='<%#Eval("customer_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Date of Dispatch " ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldateofDispatch" runat="server" Font-Size="Smaller"  Visible="true" Text='<%#Eval("dispatch_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dispatch QTY" ItemStyle-Font-Bold="true" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDispatchqty" runat="server" Font-Size="Smaller"  Visible="true" Text='<%#Eval("dispatch_qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderText="Brix" ItemStyle-Font-Bold="true" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbrix" runat="server" Visible="true" Font-Size="Smaller"  Text='<%#Eval("Brix") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>--%>
                                                   
                                            <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":Eval("record_status").ToString()=="I"? "Issued" :Eval("record_status").ToString()=="M"? "Issued" :Eval("record_status").ToString()=="P"? "Issued":Eval("record_status").ToString()=="D"? "Issued": "Draft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true"  ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                         <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "N" ? true : false %>' OnClick="btnEdit_Click"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="I" ID="btnApply" CssClass="myButton8" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "Y" ? true : false %>'  OnClick="btnApply_Click" ><i class="fa">I
                                                                                    </i> 
                                                   </asp:LinkButton>
                                                         <asp:LinkButton Text="Print" id="LinkButton1"  CssClass="myButton11"   runat="server"  Visible='<%# Eval("record_status").ToString() == "I" ? true:Eval("record_status").ToString() == "M" ? true:Eval("record_status").ToString() == "P" ? true:Eval("record_status").ToString() == "D" ? true:false %>' OnClick="LinkButton1_Click"   CommandName="Issue" ><i class="fa fa-print"></i>
                                                                            </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"  />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                        
                                    </div>
                                    <hr />
<asp:Literal ID="ltEmbed" runat="server" />
                                </div></div>

                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
