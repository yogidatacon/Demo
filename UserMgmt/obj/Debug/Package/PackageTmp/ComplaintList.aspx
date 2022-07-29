<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ComplaintList.aspx.cs" Inherits="UserMgmt.ComplaintList" %>
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
                                <title>Complaint List </title>
                                    <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_grdAllocationRequestView_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                            </head>
                            <body>
                               <%-- <div>
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnIndent" OnClick="btnIndent_Click">
                                        <span style="color: #fff; font-size: 14px;">Indent for Molasses</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnARM" OnClick="btnARM_Click">
                                        <span style="color: #fff; font-size: 14px;">Allocation Request for Molasses</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>--%>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Complaint List </h2>
                                    <div class="clearfix"></div>
                                </div>
                               
                                <div class="x_content">
                                    <div > 
                                    <asp:GridView ID="grdAllocationRequestView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdAllocationRequestView_PageIndexChanging" OnDataBound="grdAllocationRequestView_DataBound"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                         <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                         <asp:DropDownList ID="ddlsearch1" CssClass="ddlsearch1s" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Financial Year" Value="financial_year"></asp:ListItem>
                                                <asp:ListItem Text="Product Name" Value="product_name"></asp:ListItem>
                                                  <asp:ListItem Text="Allotment No" Value="final_allotmentno"></asp:ListItem>
                                               <asp:ListItem Text="Request Date" Value="req_allotmentdate"></asp:ListItem>
                                                                    <asp:ListItem Text="Indenter Unit Name" Value="party_name"></asp:ListItem>
                                                                           <asp:ListItem Text="From Sugar Mill" Value="requested_fromunit"></asp:ListItem>
                                                                  <asp:ListItem Text="Requested Qty" Value="reqd_qty"></asp:ListItem>
                                                                  <asp:ListItem Text="Alloted Qty" Value="allotted_qty"></asp:ListItem>
                                                                 
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
                                            <asp:TemplateField HeaderText="Complainant Name" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplainantName" runat="server"  Text='<%#Eval("complainant_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Complaint Details" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplaintDetails" runat="server" Visible="true" Text='<%#Eval("complaint_details") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile Number" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobileNumber" runat="server" Visible="true" Text='<%#Eval("mobile_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="complaint_id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentId" runat="server" Visible="true" Text='<%#Eval("complaint_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? Eval("allotment_status").ToString() :Eval("record_status").ToString()=="R"? Eval("allotment_status").ToString():(Eval("record_status").ToString()=="Y"?Eval("allotment_status").ToString()=="N"?"Pending":Eval("allotment_status").ToString() :Eval("record_status").ToString()=="I"? "Alloted" : Eval("record_status").ToString()=="B"? Eval("allotment_status").ToString(): "Draft") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>--%>
                                         <%--   <asp:TemplateField HeaderText="Approval Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovalstatus" runat="server" Text='<%# Eval("allotment_status").ToString()== "N" ? "Pending":Eval("allotment_status").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" Visible="false" CommandName="Edit" OnClick="btnEdit_Click"><i class="fa fa-pencil-square-o"> 
                                                    </i> 
                                                    </asp:LinkButton>
                                                     <asp:LinkButton Text="Issue" ID="btnEssue" CssClass="myButton2" runat="server" Visible=' false' CommandName="Issue" >I<i > 
                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Print" ID="btnprint" CssClass="myButton11" runat="server" Visible='false'  CommandName="Print" ><i class="fa fa-print"> 
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
