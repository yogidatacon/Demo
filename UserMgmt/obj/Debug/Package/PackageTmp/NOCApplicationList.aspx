<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="NOCApplicationList.aspx.cs" Inherits="UserMgmt.NOCApplicationList" %>


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
                                <title>NOC Application List</title>
                                 <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdNOCApplicationList_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                                   <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnNOCApplication" runat="server" OnClick="btnNOCApplication_Click" ><span style="color:#fff;font-size:14px;">NOC Application</span></asp:LinkButton></li>
                                       
                                    </ul>
                                </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>NOC Application List</h2>
                                    <div class="clearfix"></div>
                                </div>
                              <div class="col-md-4 col-sm-12 col-xs-12 form-inline" id="passfor" runat="server">
               &nbsp; <label class="control-label" style="display:inline"><span style="color:red">*</span>Product Name</label> <br />                                          
                                          &nbsp;  <asp:DropDownList ID="ddproduct" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="product" Width="230px" OnTextChanged="ddproduct_TextChanged" CssClass="form-control" style="">

                                              </asp:DropDownList>
                                        </div>
                                                
                                 <div class="clearfix"></div>
                                 <p>&nbsp;</p><br />
                               
                                <div class="x_content">

                                    <div >
                                        <asp:HiddenField ID="districtcode" runat="server" />
                                        <asp:HiddenField ID="rolename" runat="server" />
                                    <asp:GridView ID="grdNOCApplicationList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdNOCApplicationList_PageIndexChanging" OnDataBound="grdNOCApplicationList_DataBound"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                      <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                            
                                                             <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" CssClass="ddlsearch1s" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Suplier Name" Value="party_name"></asp:ListItem>
                                                                <asp:ListItem Text="Product" Value="product_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Application No" Value="req_nocno"></asp:ListItem>
                                                                           <asp:ListItem Text="NOC Issued No" Value="issue_nocno"></asp:ListItem>
                                                                   <asp:ListItem Text="Customer Name" Value="Cust_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Required NOC QTY" Value="g.req_qty"></asp:ListItem>
                                                                  <asp:ListItem Text="Alloted NOC QTY" Value="g.totalqty"></asp:ListItem>
                                                                  <asp:ListItem Text="Valid Upto" Value="valid_upto"></asp:ListItem>
                                                                 
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
                                            <asp:TemplateField HeaderText="Suplier Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSuplier"  Font-Size="Smaller"  runat="server" Visible="true"  Text='<%#Eval("party_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Suplier Name" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblparty_code" runat="server" Visible="true" Text='<%#Eval("party_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Product" ItemStyle-Font-Bold="true"  ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNocfor" Font-Size="Smaller"  runat="server"  Text='<%#Eval("product_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application No" ItemStyle-Font-Bold="true"  ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreq_nocno" Font-Size="Smaller"  runat="server"  Text='<%#Eval("req_nocno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="NOC Issued No" ItemStyle-Font-Bold="true"  ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssued_nocno" Font-Size="Smaller"  runat="server"  Text='<%#Eval("issue_nocno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application No" Visible="false" ItemStyle-Font-Bold="true"  ItemStyle-Width="1px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplicationNo" Font-Size="Smaller" runat="server"  Text='<%#Eval("noc_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Customer Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="18px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("Cust_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              
                                            <asp:TemplateField HeaderText="Required NOC QTY" ItemStyle-Font-Bold="true"  ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRQTY" Font-Size="Smaller"  runat="server"  Text='<%#Eval("req_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alloted NOC QTY" ItemStyle-Font-Bold="true"  ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQTY" Font-Size="Smaller"  runat="server"  Text='<%#Eval("Approved_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Valid Upto" ItemStyle-Font-Bold="true"  ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidUpto" Font-Size="Smaller"  runat="server"  Text='<%#Eval("valid_upto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"  ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Font-Size="Smaller"  runat="server" Visible="true" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? Eval("noc_status").ToString():Eval("record_status").ToString()=="Y"? Eval("noc_status").ToString()==""?"Pending" :Eval("noc_status").ToString():Eval("record_status").ToString()=="I"? "Issued":"Draft" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovalStatus" Font-Size="Smaller"   runat="server" Visible="true" Text='<%#Eval("noc_status").ToString()=="" ?"Approval Pending":Eval("noc_status").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" id="btnView"  CssClass="myButton" OnClick="btnView_Click"  runat="server"  CommandName="View"  ><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                   <asp:LinkButton Text="Edit" id="btnEdit"  CssClass="myButton1" OnClick="btnEdit_Click"  runat="server"  Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false :Eval("record_status").ToString() == "I" ? false:Eval("record_status").ToString() == "R" ? false: true %>' CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                    <asp:LinkButton Text="I" id="Issued"  runat="server" OnClick="Issued_Click" CssClass="myButton2"  Visible='<%# Eval("record_status").ToString() == "A" ? true:false %>' CommandName="Issue" >
                                                                            </asp:LinkButton>
                                                                    <asp:LinkButton Text="Print" id="LinkButton1"  CssClass="myButton11" OnClick="btnprint_Click"   runat="server"  Visible='<%# Eval("record_status").ToString() == "I" ? true:false %>'  CommandName="Issue" ><i class="fa fa-print"></i>
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
