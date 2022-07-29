<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RequestForTransportList.aspx.cs" Inherits="UserMgmt.RequestForTransportList" %>
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
                                <title>Request For Pass List</title>
                                  <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdMolassesReleaseRequest_txtSearch2').val('');
                                                $('#BodyContent_grdNOCList_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                                   <div>
                                    <ul class="nav nav-tabs">
                                         <li class="active">
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Transport Pass</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click"><span style="color:#fff;font-size:14px;">Dispatch</span></asp:LinkButton></li>
                                    
                                       
                                    </ul>
                                </div>
                                
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2> Request For Transport Pass List</h2>
                                    <div class="clearfix"></div>
                                </div>
                               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                  
                                   
                                 <div class="col-md-4 col-sm-12 col-xs-12 form-inline" id="passfor" runat="server">
               &nbsp; <label class="control-label" style="display:inline"><span style="color:red">*</span> Transport Pass / Dispatch For</label> <br />                                          
                                          &nbsp;  <asp:DropDownList ID="ddPassFor" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Pass / Dispatch For" OnSelectedIndexChanged="ddPassFor_SelectedIndexChanged" CssClass="form-control" style="">
                                                <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Export" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Domestic" Value="N"></asp:ListItem> 
                                              </asp:DropDownList>
                                        </div>
                                                
                                 <div class="clearfix"></div>
                                 <p>&nbsp;</p><br />
                               
                                    <div > 
                                    <asp:GridView ID="grdMolassesReleaseRequest" runat="server"  AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdMolassesReleaseRequest_PageIndexChanging" OnDataBound="grdMolassesReleaseRequest_DataBound"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                         <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                              <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" CssClass="ddlsearch1s" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="RR No" Value="b.rr_issueno"></asp:ListItem>
                                                                <asp:ListItem Text="Product" Value="product_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Valid Upto" Value="valied_date"></asp:ListItem>
                                                                           <asp:ListItem Text="RR Approved Qty" Value="b.rr_approved_qty"></asp:ListItem>
                                                                   <asp:ListItem Text="Pass Requested Qty" Value="req_qty"></asp:ListItem>
                                                                  <%--<asp:ListItem Text="Approved Qty" Value="req_qty"></asp:ListItem>--%>
                                                                  <asp:ListItem Text="Customer/Supplier" Value="p.party_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Pass Generated Qty" Value="lifted_qty"></asp:ListItem>
                                                                 
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black" onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary"  /></span> 
      <span><asp:LinkButton ID="LinkButton5" runat="server"  CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span> 

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
                                               <asp:TemplateField HeaderText="RR No"  ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleaseRequestNo1" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rr_allotmentno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RR No"  ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleaseRequestNo" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rrnoc_request_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Release Request No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequest_for_pass_id" Font-Size="Smaller"   runat="server" Visible="true" Text='<%#Eval("request_for_pass_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product " ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("product_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valid Upto" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidUpto" Font-Size="Smaller"  runat="server"  Text='<%#Eval("valied_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="RR Approved Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRRQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("approvedqty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Pass Requested Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPassRequestedQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("req_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Approved Qty" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="PassApprovedQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("req_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Customer/Supplier" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomer_Supplier" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pass Generated Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="PassLiftedQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("lifted_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="party_code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartycode" runat="server" Visible="true" Text='<%#Eval("party_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"
                                                 ItemStyle-Width="40px" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Requested" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Requested":Eval("record_status").ToString()=="I"? "Alloted" : "Draft") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "N" ? true : false %>' OnClick="btnEdit_Click"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Apply Pass" ID="btnApply" CssClass="myButton7" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "Y" ? true :Eval("record_status").ToString() == "A" ? true: false %>' OnClick="btnApply_Click"><i class="fa">G 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                       
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"  />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px"  Height="25px"></RowStyle>
                                    </asp:GridView>
                                        <asp:GridView ID="grdNOCList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdNOCList_PageIndexChanging" OnDataBound="grdNOCList_DataBound"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                      <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                               <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" CssClass="ddlsearch1s" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="NOC IssuedNo" Value="issue_nocno"></asp:ListItem>
                                                                <asp:ListItem Text="Product" Value="product_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Valid Upto" Value="pass_valid_upto"></asp:ListItem>
                                                                           <asp:ListItem Text="NOC Issued Qty" Value="b.noc_total_qty"></asp:ListItem>
                                                                   <asp:ListItem Text="Pass Requested Qty" Value="req_qty"></asp:ListItem>
                                                                 <%-- <asp:ListItem Text="Approved Qty" Value="req_qty"></asp:ListItem>--%>
                                                                  <asp:ListItem Text="Pass Generated Qty" Value="lifted_qty"></asp:ListItem>
                                                                      <asp:ListItem Text="Customer/Supplier" Value="b1.cust_name"></asp:ListItem>
                                                                 
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black" onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click1" /></span> 
      <span><asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span> 
     
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page"  CommandArgument="First" CssClass="myButton1"><i class="fa fa-step-backward"> </i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="myButton1"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                <asp:ImageButton ID="btnFirst" runat="server" Width="30px" Height="20px" CommandArgument="First" Visible="false" CommandName="Page"  BackColor="Blue" ForeColor="White"
                    ImageUrl="~/img/icons8-first-50.png" /> <asp:ImageButton ID="btnPrev" runat="server" Visible="false"
                        CommandArgument="Prev" CommandName="Page" Width="30px" Height="20px" BackColor="Blue" ImageUrl="~/img/icons8-previous-50.png" /> <asp:DropDownList
                            ID="DDLPage" runat="server" AutoPostBack="True"  Visible="false"  Width="250px" ForeColor="Black" Font-Bold="true">
                        </asp:DropDownList>&nbsp;&nbsp; <asp:TextBox ID="txtpage" runat="server" Height="20px" AutoPostBack="true" TextMode="Number" ForeColor="Black" Width="50px" Font-Bold="true" OnTextChanged="txtpage_TextChanged1"></asp:TextBox> <asp:Label ID="lblCurrent" Visible="false" runat="server"></asp:Label>
                of
              <asp:Label ID="lblPages" runat="server" Height="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page"  CommandArgument="Next" CssClass="myButton1"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page"  CommandArgument="Last" CssClass="myButton1"><i class="fa fa-step-forward"> </i></asp:LinkButton>
                                                            
                <asp:ImageButton ID="btnNext" Visible="false"
                    runat="server" CommandArgument="Next" Width="30px" Height="20px" CommandName="Page" ForeColor="Blue" BackColor="Blue" ImageUrl="~/img/icons8-next-50.png"  /> <asp:ImageButton
                        ID="btnLast" runat="server" CommandArgument="Last" Width="30px" Visible="false" Height="20px" BackColor="Blue" CommandName="Page" ImageUrl="~/img/icons8-last-50.png" />
            </PagerTemplate>        
                                                <Columns>
                                          <asp:TemplateField HeaderText="NOC IssuedNo"  ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNOCIssedNo" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("issue_nocno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="NOC No"  ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleaseRequestNo" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("rrnoc_request_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Release Request No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="2px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrequest_for_pass_id" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("request_for_pass_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product " ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("product_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valid Upto" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidUpto" Font-Size="Smaller"  runat="server"  Text='<%#Eval("pass_valid_upto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="NOC Issued Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRRQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("approvedqty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Pass Requested Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPassRequestedQty" Font-Size="Smaller"   runat="server" Visible="true" Text='<%#Eval("req_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Approved Qty" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="PassApprovedQty" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("req_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="passtype" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpasstype" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("pass_type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pass Generated Qty" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="PassLiftedQty" runat="server" Font-Size="Smaller"  Visible="true" Text='<%#Eval("lifted_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="party_code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartycode" runat="server" Visible="true" Text='<%#Eval("party_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="record_status" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrecordstatus" runat="server" Visible="true" Text='<%#Eval("record_status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomer_Supplier" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval" ItemStyle-Font-Bold="true"
                                                 ItemStyle-Width="40px" >
                                                <ItemTemplate>
                                                     <asp:Label ID="lblstatus" Font-Size="Smaller"  runat="server" Visible="true" Text='<%#Eval("approval_status") %>'></asp:Label>
<%--                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Recommended by Bond Officer" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":Eval("record_status").ToString()=="I"? "Issued by Bond Officer" :Eval("record_status").ToString()=="B"? "Refer Back" :"Draft") %>'></asp:Label>
                                               --%>
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View"  OnClick="btnNOCView_Click"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "N" ? true :Eval("record_status").ToString() == "R" ? true : false %>' OnClick="btnNOCEdit_Click"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                      <asp:LinkButton Text="I" ID="btnissue" CssClass="myButton8" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "A" ? true : false %>' OnClick="btnNOCView_Click" ><i class="fa">I
                                                                                    </i> 
                                                   </asp:LinkButton>
                                                    <asp:LinkButton Text="Apply Pass" ID="btnApply" CssClass="myButton7" runat="server"  CommandName="Edit" Visible='<%# Eval("record_status").ToString() == "I" ? true : false %>' OnClick="btnNOCApply_Click"><i class="fa">G
                                                                                    </i> 
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
                                   
                                 
                             
                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
