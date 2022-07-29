<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DME_List.aspx.cs" Inherits="UserMgmt.DME_List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
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
                                <title>DM Case Registration List</title>
                                <script language="javascript" type="text/javascript">
                                //function Selectdate(e) {
                                //        debugger;
                                //        var todayDate = e.get_selectedDate();
                                //        var dd = todayDate.getDate();
                                //        var mm = todayDate.getMonth() + 1; //January is 0!

                                //        var yyyy = todayDate.getFullYear();
                                //        if (dd < 10) {
                                //            dd = '0' + dd;
                                //        }
                                //        if (mm < 10) {
                                //            mm = '0' + mm;
                                //        }
                                //        todayDate = dd + '-' + mm + '-' + yyyy;
                                     
                                //        $('#BodyContent_txtDATE').val(todayDate);
                                //        $('#BodyContent_txtdob').val(todayDate);
                                //}
                                    function Selectdate(e) {
                                        debugger;
                                    var todayDate = e.get_selectedDate();
                                    var dd = todayDate.getDate();
                                    var mm = todayDate.getMonth() + 1; //January is 0!

                                    var yyyy = todayDate.getFullYear();
                                    if (dd < 10) {
                                        dd = '0' + dd;
                                    }
                                    if (mm < 10) {
                                        mm = '0' + mm;
                                    }
                                    todayDate = dd + '-' + mm + '-' + yyyy;
                                    $('#BodyContent_txtDATE').val(todayDate);
                                    //var date1 = $('#BodyContent_txtDATE').val();
                                    $('#BodyContent_txtdob').val(todayDate);
                                }
                                 </script>
                                <script>
                                    $(function () {
                                        $(".ddlsearch1s").change(function () {
                                            if ($(this).find('option:selected').text() == "1") {
                                                alert('1');
                                            } else {
                                                $('#BodyContent_ArticleCategoryView_txtSearch2').val('');
                                            }
                                        });
                                    });
                                   
                                    </script>
                                 <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;;
                                        if ($("#BodyContent_ArticleCategoryView_ddlsearch1").val() == 'Select') {
                                            alert("select search column!");
                                            $("#BodyContent_ArticleCategoryView_ddlsearch1").focus();
                                            return false;
                                        }
                                        if ($("#BodyContent_ArticleCategoryView_txtSearch2").val() == '') {
                                            alert("Enter search column Value!");
                                            $("#BodyContent_ArticleCategoryView_txtSearch2").focus();
                                            return false;
                                        }
                                    }
                                    </script> 
                            </head>
                            <body>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord" OnClick="AddRecord_Click" Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>DM Case Registration List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                         <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                             <label class="control-label" style="font-size:small"><span style="color: red"></span>Search Criteria</label><br />
                                            <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" CssClass="form-control" Width="60%" OnSelectedIndexChanged="ddlSelect_SelectedIndexChange">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>By Thana</asp:ListItem>
                                                <asp:ListItem>By Thana & RaidDate</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                           
                                 <div class="x_title">
                                    
                                    <div class="clearfix"></div>
                                </div>   
                                <%--  <div class="x_title"></div>--%>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                           <%-- <div class="x_content">--%>
                                                
                                                    <asp:GridView ID="grdUnSubmittedList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnDataBound="grdUnSubmittedList_DataBound"
                                                        HeaderStyle-BackColor="#26b8b8" DataKeyNames="dmcase_registration_id" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdUnSubmittedList_PageIndexChanging" >
                                                       <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                            <asp:DropDownList ID="ddlsearch1" CssClass="ddlsearch1s" runat="server" Width="150px" Font-Bold="true" Height="25px" 
                                                                ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Case No" Value="caseno"></asp:ListItem>
                                                <asp:ListItem Text="PR/FIR No" Value="prfirno"></asp:ListItem>
                                                <asp:ListItem Text="Vehicle/Property" Value="confiscation_code"></asp:ListItem>
                                                <asp:ListItem Text="District" Value="district_code"></asp:ListItem>
                                                 <asp:ListItem Text="Date of hearing" Value="date_of_hearing"></asp:ListItem>
                                                                  <asp:ListItem Text="thana" Value="thana_name"></asp:ListItem>
                                                  
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                            <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black"  AutoPostBack="true" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px" OnClick="Button2_Click"  Text="Search" CssClass="btn btn-primary"  OnClientClick="javascript:return validationMsg()" /></span>
     <span><asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"  CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span>
                                                          
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page"  CommandArgument="First" CssClass="myButton1"><i class="fa fa-step-backward"> </i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="myButton1"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                <asp:ImageButton ID="btnFirst" runat="server" Width="30px" Height="20px" CommandArgument="First" Visible="false" CommandName="Page"  BackColor="Blue" ForeColor="White"
                    ImageUrl="~/img/icons8-first-50.png" /> <asp:ImageButton ID="btnPrev" runat="server" Visible="false"
                        CommandArgument="Prev" CommandName="Page" Width="30px" Height="20px" BackColor="Blue" ImageUrl="~/img/icons8-previous-50.png" /> <asp:DropDownList
                            ID="DDLPage" runat="server" AutoPostBack="True"  Visible="false"  Width="250px" ForeColor="Black" Font-Bold="true">
                        </asp:DropDownList>&nbsp;&nbsp; <asp:TextBox ID="txtpage" runat="server" Height="20px" AutoPostBack="true" TextMode="Number" ForeColor="Black" Width="50px" Font-Bold="true" OnTextChanged="txtpage_TextChanged" ></asp:TextBox> <asp:Label ID="lblCurrent" Visible="false" runat="server"></asp:Label>
                of
              <asp:Label ID="lblPages" runat="server" Height="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page"  CommandArgument="Next" CssClass="myButton1"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page"  CommandArgument="Last" CssClass="myButton1"><i class="fa fa-step-forward"> </i></asp:LinkButton>
                                                            
                <asp:ImageButton ID="btnNext" Visible="false"
                    runat="server" CommandArgument="Next" Width="30px" Height="20px" CommandName="Page" ForeColor="Blue" BackColor="Blue" ImageUrl="~/img/icons8-next-50.png"  /> <asp:ImageButton
                        ID="btnLast" runat="server" CommandArgument="Last" Width="30px" Visible="false" Height="20px" BackColor="Blue" CommandName="Page" ImageUrl="~/img/icons8-last-50.png" />
                                                              
            </PagerTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="District" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lblSeizureNo" runat="server" Visible="true" Text='<%#Eval("district_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Thana" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lblthana11" runat="server" Visible="true" Text='<%#Eval("thana_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Court Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lblCourtName" runat="server" Visible="true" Text='<%#Eval("court_master_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Department" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidTime" runat="server" Visible="true" Text='<%# Eval("raidby").ToString() == "E" ? "Excise": "Police" %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Case No" ItemStyle-Font-Bold="true" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidDate" runat="server" Visible="true" Text='<%#Eval("caseno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="PR/FIR No" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="30px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTPR" runat="server" Width="100%" Visible="true" Text='<%#Eval("prfirno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vehicle/Property" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="10px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaidLocation" runat="server" Visible="true" Text='<%# Eval("confiscation_code").ToString() == "VH" ? "Vehicle": "Property" %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Date of Hearing" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateofHearing" runat="server" Visible="true" Text='<%# Eval("case_hearingdate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstatus" runat="server" Visible="true" Text='<%# Eval("hearing_status").ToString() == "N" ? "Draft":Eval("hearing_status").ToString() == "Y"? "Submitted":Eval("hearing_status").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="regid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblregid" runat="server" Visible="true" Text='<%# Eval("dmcase_registration_id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick ="btnView_Click" CommandName="View"><i class="fa fa-search-plus"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click" Visible='<%# Eval("record_status").ToString() == "N" ? true: false %>' CommandName="Edit"><i class="fa fa-pencil-square-o">
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
                                                
                                           <%--// </div>--%>
                                              <%-- <div class="x_title"></div>--%>
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
