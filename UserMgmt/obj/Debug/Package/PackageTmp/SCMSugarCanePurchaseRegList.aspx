<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SCMSugarCanePurchaseRegList.aspx.cs" Inherits="UserMgmt.SCMSugarCanePurchaseRegList" %>

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
                                <title>Form R.G-4</title>
                                 <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_grdSugarCanePurchaseList_txtSearch2').val();
                                        var jsondata = JSON.stringify($('#BodyContent_grdSugarCanePurchaseList_txtSearch2').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "SCMSugarCanePurchaseRegList.aspx/chkDuplicateAccessTypeName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    //alert("AccessType  Name is already exists");
                                                    //$('#BodyContent_txtAccessTypeName').val("");
                                                    //$('#BodyContent_txtAccessTypeName').focus();
                                                }

                                            }
                                        });
                                    }
                                    $(document).ready(function () {
                                        $('#BodyContent_grdSugarCanePurchaseList_ddlsearch1').change(function () {
                                            $('#BodyContent_grdSugarCanePurchaseList_txtSearch2').val('');
                                        });
                                    });

                                   
                                    </script>
                                   
                            </head>
                            <body>
                             
                                <!DOCTYPE html>

                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click"  >
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click"  >
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click"  >
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click" >
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                 <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="AddRecords" OnClick="AddRecords_Click"  style="float:right"  ><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton></a>
                                    <div class="x_title">
                                <h2>Form R.G-4</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div style="float: right">
                                </div>
                                
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="x_content">
                                        <asp:GridView ID="grdSugarCanePurchaseList" OnDataBound="grdSugarCanePurchaseList_DataBound" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EmptyDataText="No Records" OnPageIndexChanging="grdSugarCanePurchaseList_PageIndexChanging" 
                                              PagerStyle-ForeColor="Black" PagerStyle-Font-Underline="true"  PagerStyle-HorizontalAlign="Center"
                                            class="table table-striped responsive-utilities jambo_table" Style="overflow-y: scroll;"
                                            HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1"  HeaderStyle-HorizontalAlign="Center"      >
                                            <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />

                                                        <PagerTemplate>
                                                                 <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Party Name" Value="party_name"></asp:ListItem>
                                                                <asp:ListItem Text="Purchase Date" Value="entrydate"></asp:ListItem>
                                                                  <asp:ListItem Text="Total cane purchased" Value="total_purchase"></asp:ListItem>
                                                                  <asp:ListItem Text="Total Cane Crushed" Value="total_canecrushed"></asp:ListItem>
                                                                 
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
                                                <asp:TemplateField HeaderText="Finscal Year" Visible="false"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="30px"   HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblformrg4_id"  runat="server" Text='<%#Eval("sugarcanepurchase_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="30px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblparty_name" runat="server" Text='<%#Eval("party_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party code" Visible="false"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblparty_code" runat="server" Text='<%#Eval("party_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchase Date" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblentrydate" runat="server" Text='<%#Eval("entrydate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total cane purchased" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurchasedToday" runat="server" Text='<%#Eval("total_purchase") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Cane Crushed" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaneCrushedToday" runat="server" Text='<%#Eval("total_canecrushed") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financialyear") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Draft") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true"
                                            ItemStyle-Width="90px" HeaderStyle-Font-Underline="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" runat="server" CssClass="myButton" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                </i>       
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="myButton1" Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>' OnClick="btnEdit_Click"  ><i class="fa fa-pencil-square-o">
                                                                                </i>       
                                                </asp:LinkButton>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            </Columns>

                                          <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"/>

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>

                                        
                                        </asp:GridView>



                                    <hr />
                                  
                                </div> </body></html>
                        </div>

                    </div>

                </div>
            </div>
           
        </div>
    </div>
    
</asp:Content>
