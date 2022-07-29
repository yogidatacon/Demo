<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DisposalofPropertyMasterList.aspx.cs" Inherits="UserMgmt.DisposalofPropertyMasterList" %>
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
                                <title>Disposal Property  Master List</title>
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
                                    <li >
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
                                    <%--<li class="active">
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
                                    <h2>Disposal Property  Master List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                     <asp:GridView  ID="DisposalPropertyMasterView"  runat="server" AutoGenerateColumns="false"   AllowPaging="true" 
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="DisposalPropertyMasterView_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCode" runat="server" Visible="true" Text='<%#Eval("disposal_of_property_code") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Visible="true"  Text='<%#Eval("disposal_of_property_name") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Id" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Visible="true"  Text='<%#Eval("disposal_of_property_id") %>' ></asp:Label>
                                                                   
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

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" CssClass="paginationClass" />

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
