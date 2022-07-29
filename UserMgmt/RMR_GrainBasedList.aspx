﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RMR_GrainBasedList.aspx.cs" Inherits="UserMgmt.RMR_GrainBasedList" %>
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
                                <title>Raw Material Receipt List</title>
                                <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_grdRawMaterialReceiptList_txtSearch2').val();
                                        var jsondata = JSON.stringify($('#BodyContent_grdRawMaterialReceiptList_txtSearch2').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "RMR_GrainBasedList.aspx/chkDuplicateAccessTypeName",
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
                                        $('#BodyContent_grdRawMaterialReceiptList_ddlsearch1').change(function () {
                                            $('#BodyContent_grdRawMaterialReceiptList_txtSearch2').val('');
                                        });
                                    });

                                   
                                    </script>
 
                                   
                            </head>
                            <body>
                             
                                <!DOCTYPE html>

                                <div>
                                    <ul class="nav nav-tabs">
                                       <li class="active">
                                            <asp:LinkButton runat="server" ID="LinkButton1" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                       <%--  <li >
                                            <asp:LinkButton runat="server" ID="OtherRMR" Text="Seizure List" OnClick="OtherRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Other Than Molasses Material Receipt</span></asp:LinkButton></li>--%>

                                       <%-- <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>--%>

                                         <li >
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                                                                    <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>

                                    </ul>
                                    <br />
                                    <br />
                                </div>
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div id="drawid" runat="server">
                                     <ul class="nav nav-tabs">
                                      <li>
                                            <asp:LinkButton runat="server" ID="lnkRawReceipt" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="OtherRMR1" Text="Seizure List" OnClick="OtherRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt(Other Than Molasses) </span></asp:LinkButton></li>

                                       <%-- <li>
                                            <asp:LinkButton runat="server" ID="LinkButton2" Text="Seizure List" OnClick="btnDistillation_Click">
                                        <span style="color: #fff; font-size: 14px;">Distillation Process</span></asp:LinkButton></li>--%>
                                          
                                    </ul>
                                    <br />
                                           </div>  
                                 <div id="SGR" runat="server">
                                      <ul class="nav nav-tabs">
                                          <li >
                                            <asp:LinkButton runat="server" ID="btnsgr" Text="Seizure List" OnClick="btnsgr_Click"  >
                                        <span style="color: #fff; font-size: 14px;">From Sugar Mills</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnoth" Text="Seizure List" OnClick="btnot_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses From Other Sources</span></asp:LinkButton></li>

                             <%--    <li>
                                            <asp:LinkButton runat="server" ID="LinkButton2" Text="Seizure List" OnClick="btnDistillation_Click">
                                        <span style="color: #fff; font-size: 14px;">Distillation Process</span></asp:LinkButton></li>--%>
                                          
                                    </ul>
                                    <br />
                                           </div>  
                                 <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="AddRecords" OnClick="AddRecord_Click"     style="float:right"  ><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton></a>
                                    <div  runat="server" id="ena" class="x_title">
                                <h2>Raw Material Receipt(Other Than Molasses) List</h2>
                                <div class="clearfix"></div>
                            </div>
                                  <div id="dis" runat="server" class="x_title">
                                <h2>Raw Material Receipt List</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div style="float: right">
                                </div>
                                
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="x_content">
                                        <asp:GridView ID="grdRawMaterialReceiptList" OnDataBound="grdRawMaterialReceiptList_DataBound" runat="server"  AutoGenerateColumns="false" PageSize="10" AllowPaging="true"  EmptyDataText="No Records" OnPageIndexChanging="grdRawMaterialReceiptList_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"      >
                                            <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />

                                                        <PagerTemplate>
                <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Date Of Recieved" Value="rmr_entrydate"></asp:ListItem>
                                                                <asp:ListItem Text="From Unit" Value="party_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Other Unit" Value="suppliername"></asp:ListItem>
                                                                           <asp:ListItem Text="Pass No" Value="pass_issueno"></asp:ListItem>
                                                                  <asp:ListItem Text="Receipt Quantity" Value="passqty"></asp:ListItem>
                                                                 
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
                                                <asp:TemplateField HeaderText="Receipt ID" Visible="false"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="30px"   HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID"  runat="server" Text='<%#Eval("rawmaterial_receipt_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="30px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Receipt Date " Visible="true"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="30px"   HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateOfDispatch"  runat="server" Text='<%#Eval("rmr_entrydate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="30px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Unit"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpartyname" runat="server" Text='<%#Eval("party_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Other Unit"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther" runat="server" Text='<%#Eval("suppliername") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Party Code"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpartycode" runat="server" Text='<%#Eval("party_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Pass No"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="100px"   HeaderStyle-Font-Underline="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpss" runat="server" Text='<%#Eval("passno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt Quantity " ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantityOfDispatch" runat="server" Text='<%#Eval("passqty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No" Visible="false" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%#Eval("vehicleno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="financial year" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinancialyear" runat="server" Visible="true" Text='<%#Eval("financial_year") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" 
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":Eval("record_status").ToString()=="Y"? "Pending":Eval("record_status").ToString()=="N"? "Draft":"Entry Pending" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                               

                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true"
                                            ItemStyle-Width="90px" HeaderStyle-Font-Underline="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" runat="server" CssClass="myButton" OnClick="btnView_Click" ><i class="fa fa-search-plus">
                                                </i>       
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="myButton1"  Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>' OnClick="btnEdite_Click"  ><i class="fa fa-pencil-square-o">
                                                                                </i>       
                                                </asp:LinkButton>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                           <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

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
