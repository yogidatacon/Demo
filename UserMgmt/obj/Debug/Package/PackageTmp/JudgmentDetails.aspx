<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="JudgmentDetails.aspx.cs" Inherits="UserMgmt.JudgmentDetails" %>
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
                                <title>Judgment Details</title>
                            </head>
                            <body>
                                    <div>
                                    <ul class="nav nav-tabs">
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnTrailList" OnClick="btnTrailList_Click" >
                                        <span style="color: #fff; font-size: 14px;">Trail List</span></asp:LinkButton></li>
                                     
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnTrial" OnClick="btnTrial_Click">
                                        <span style="color: #fff; font-size: 14px;">Trail Stages</span></asp:LinkButton></li>
                                          <li>
                                            <asp:LinkButton runat="server" ID="btnProsecutionEvidence" OnClick="btnProsecutionEvidence_Click">
                                        <span style="color: #fff; font-size: 14px;">Prosecution Evidence</span></asp:LinkButton></li>
                                         <li >
                                            <asp:LinkButton runat="server" ID="btnAccusedStatement" OnClick="btnAccusedStatement_Click">
                                        <span style="color: #fff; font-size: 14px;">Accused Statement</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnDefenceStatement" OnClick="btnDefenceStatement_Click">
                                        <span style="color: #fff; font-size: 14px;">Defence Statement</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnFinalArgument" OnClick="btnFinalArgument_Click">
                                        <span style="color: #fff; font-size: 14px;">Final Argument</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnJudgement" OnClick="btnJudgement_Click">
                                        <span style="color: #fff; font-size: 14px;">Judgement</span></asp:LinkButton></li>
                                             <li>
                                            <asp:LinkButton runat="server" ID="btnTrailCaseHistory" OnClick="btnTrailCaseHistory_Click">
                                        <span style="color: #fff; font-size: 14px;"> Trail Case History </span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                           </div> 

                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <div>
                                    <ul class="nav nav-tabs">

                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnJudD" OnClick="btnJudD_Click" >
                                        <span style="color: #fff; font-size: 14px;">Judgement Details</span></asp:LinkButton></li>
                                        </ul></div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"> PREVIEW</asp:LinkButton>
                                <div class="x_title">
                                    <h2>Judgment Details</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                </div>
                                <div class="x_content">
                                    <div style =" width:100px; overflow:auto;"> 
                                    <asp:GridView ID="grdJudgementList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                                                    <asp:TemplateField HeaderText="Accused Id" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAccusedid" runat="server" Text='<%#Eval("accused_id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAccused" runat="server" Text='<%#Eval("accu_name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Judgement" ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlJudgement" runat="server" >
                                                                                <asp:ListItem Value="0"  >Select</asp:ListItem>
                                                                                <asp:ListItem Value="1"  >Convicted</asp:ListItem>
                                                                                <asp:ListItem Value="2"  >Acquitted</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
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
