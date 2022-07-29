<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DailyMolassesProductionList.aspx.cs" Inherits="UserMgmt.SCMDailyMolassesProductionList" %>
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
                                <title> Daily Molasses Production List</title>

                                  <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_grdSCMDailyMolassesView_txtSearch2').val();
                                        var jsondata = JSON.stringify($('#BodyContent_grdSCMDailyMolassesView_txtSearch2').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DailyMolassesProductionList.aspx/chkDuplicateAccessTypeName",
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
                                        $('#BodyContent_grdSCMDailyMolassesView_ddlsearch1').change(function () {
                                            $('#BodyContent_grdSCMDailyMolassesView_txtSearch2').val('');
                                        });
                                    });

                                   
                                    </script>
                               
                            </head>
                            <body>
                                  <div>
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                 <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecord_Click"  style="float:right" ><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Daily Molasses Production List</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                    <%--<asp:DropDownList ID="ddlPartyNameSCM" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"  AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="x_content">
                                    <asp:GridView ID="grdSCMDailyMolassesView" OnDataBound="grdSCMDailyMolassesView_DataBound" runat="server"  AutoGenerateColumns="false" PageSize="10" AllowPaging="true"  EmptyDataText="No Records" OnPageIndexChanging="grdSCMDailyMolassesView_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                    <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />

                                                        <PagerTemplate>
                                                             <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Party Name" Value="party_name"></asp:ListItem>
                                                                <asp:ListItem Text="Production Date" Value="entrydate"></asp:ListItem>
                                                                  <asp:ListItem Text="Production QTY" Value="dailyproduction"></asp:ListItem>
                                                                  <asp:ListItem Text="UOM" Value="uom_name"></asp:ListItem>
                                                                 
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
                                                          <asp:TemplateField HeaderText="Party Code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartyCode" runat="server" Visible="false" Text='<%#Eval("party_code") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartyName" runat="server" Visible="true" Text='<%#Eval("party_name") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Production Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryDate" runat="server" Visible="true" Text='<%#Eval("entrydate") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Production QTY" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodaysProduction" runat="server" Visible="true" Text='<%#Eval("dailyproduction") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                             <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM" runat="server" Visible="true" Text='<%#Eval("uom_name") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'  ></asp:Label>
                                                                   
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
                                                    <asp:TemplateField HeaderText="Action"     ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" >
                                                                <ItemTemplate>
                                                                        <asp:LinkButton Text="View" id="btnView"  CssClass="myButton" OnClick="btnView_Click"  runat="server"     CommandName="View" ><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                   <asp:LinkButton Text="Edit" id="btnEdit"  CssClass="myButton1" OnClick="btnEdite_Click"   runat="server" Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>'   CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
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
                            </body>
                            </html>
                        </div>
                    </div>

                </div>
            </div>
        </div></div>
</asp:Content>
