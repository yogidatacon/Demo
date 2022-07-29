<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ArticleCategoryMasterList.aspx.cs" Inherits="UserMgmt.ArticleCategoryMasterList" %>

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
                                <title>Article Category Master List</title>
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
                                <ul class="nav nav-tabs">
                                    <li>
                                        <asp:LinkButton ID="Gender" OnClick="Gender_Click" runat="server"><span style="color:#fff;font-size:14px;">Gender</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="Religion" OnClick="Religion_Click" runat="server"><span style="color:#fff;font-size:14px;">Religion</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="Caste" OnClick="Caste_Click" runat="server"><span style="color:#fff;font-size:14px;">Caste</span></asp:LinkButton></li>
                                        <li>
                                        <asp:LinkButton ID="Idproof" OnClick="Idproof_Click" runat="server"><span style="color:#fff;font-size:14px;">Idproof</span></asp:LinkButton></li>
                                      <li>
                                        <asp:LinkButton ID="DesignationType" OnClick="DesignationType_Click" runat="server"><span style="color:#fff;font-size:14px;">Department</span></asp:LinkButton></li>
                                      <li>
                                        <asp:LinkButton ID="Designation" OnClick="Designation_Click" runat="server"><span style="color:#fff;font-size:14px;">Designation</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="ArticleCategoryMaster" OnClick="ArticleCategoryMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Article Category</span></asp:LinkButton></li>
                                     <li>
                                        <asp:LinkButton ID="ArticleSubCategory" OnClick="ArticleSubCategory_Click" runat="server"><span style="color:#fff;font-size:14px;">Article SubCategory</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="article_name" OnClick="Articlename_Click" runat="server"><span style="color:#fff;font-size:14px;">Article Name</span></asp:LinkButton></li>
                                      <li>
                                        <asp:LinkButton ID="propertytype" OnClick="propertytype_Click" runat="server"><span style="color:#fff;font-size:14px;">Property Type</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="Vehicle" OnClick="Vehicle_Click" runat="server"><span style="color:#fff;font-size:14px;">Vehicle</span></asp:LinkButton></li>
                                     <li>
                                        <asp:LinkButton ID="Court" OnClick="Court_Click" runat="server"><span style="color:#fff;font-size:14px;">Court</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="Bail" OnClick="Bail_Click" runat="server"><span style="color:#fff;font-size:14px;">Bail</span></asp:LinkButton></li>
                                    <%--<li>
                                        <asp:LinkButton ID="DisposalofProperty" OnClick="DisposalofProperty_Click" runat="server"><span style="color:#fff;font-size:14px;">Disposal of Property</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="OffenceType" OnClick="OffenceType_Click" runat="server"><span style="color:#fff;font-size:14px;">Offence Type</span></asp:LinkButton></li>--%>
                                    <li>
                                        <asp:LinkButton ID="Offence" OnClick="Offence_Click" runat="server"><span style="color:#fff;font-size:14px;">Offence</span></asp:LinkButton></li>
                                      <%--<li>
                                        <asp:LinkButton ID="SeizurStage" OnClick="SeizurStage_Click" runat="server"><span style="color:#fff;font-size:14px;">Seizure Stage</span></asp:LinkButton></li>--%>
                                  <%--  <li>
                                        <asp:LinkButton ID="SeizurStatus" OnClick="SeizurStatus_Click" runat="server"><span style="color:#fff;font-size:14px;">SeizurStatus</span></asp:LinkButton></li>--%>
                                </ul>

                                 <asp:LinkButton runat="server" CssClass="myButton3 " ID="AddRecord" style="float:right" OnClick="AddRecord_Click"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Article Category Master List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                     <asp:GridView  ID="ArticleCategoryView"  runat="server" AutoGenerateColumns="false"   AllowPaging="true" OnDataBound="ArticleCategoryView_DataBound" 
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="ArticleCategoryView_PageIndexChanging">
                                        <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                            <asp:DropDownList ID="ddlsearch1" CssClass="ddlsearch1s" runat="server" Width="150px" Font-Bold="true" Height="25px" 
                                                                ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Article Category Code" Value="article_category_code"></asp:ListItem>
                                                <asp:ListItem Text="Article Category Name" Value="article_category_name"></asp:ListItem>
                                                  
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                            <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black"  AutoPostBack="true" onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary" OnClick="Button2_Click" OnClientClick="javascript:return validationMsg()" /></span>
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
                                                            <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCode" runat="server" Visible="true" Text='<%# Bind("article_category_code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Visible="true"  Text='<%# Bind("article_category_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Id" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Visible="true"  Text='<%# Bind("article_category_master_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action"     ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" >
                                                                <ItemTemplate>
                                                                        <asp:LinkButton Text="View" id="btnView"  CssClass="myButton"  runat="server"    CommandName="View" OnClick="btnView_Click" ><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                    <asp:LinkButton Text="Edit" id="LinkButton1"  CssClass="myButton1"   runat="server"   CommandName="Edit" OnClick="btnEdit_Click" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
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
