<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="ProsecutionEvidenceList.aspx.cs" Inherits="UserMgmt.ProsecutionEvidenceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedBodyContent" runat="server">

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
                                <title>Prosecution Evidence List</title>
                            </head>
                            <body>
                                 <%-- <div>
                                    <ul class="nav nav-tabs">

                                             <li>
                                            <asp:LinkButton runat="server" ID="btnTrailList" Visible="false" OnClick="btnTrailList_Click" >
                                        <span style="color: #fff; font-size: 14px;">Trail List</span></asp:LinkButton></li>
                                       
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnTrial" OnClick="btnTrial_Click">
                                        <span style="color: #fff; font-size: 14px;"> Trial Stages </span></asp:LinkButton></li>
                                          <li class="active">
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
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnJudgement" OnClick="btnJudgement_Click">
                                        <span style="color: #fff; font-size: 14px;">Judgement</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnTrailCaseHistory" OnClick="btnTrailCaseHistory_Click">
                                        <span style="color: #fff; font-size: 14px;"> Trail Case History </span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                           </div> --%>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <asp:LinkButton runat="server" CssClass="myButton3 " ID="btnAddRecord"  OnClick="AddRecord_Click"  Style="float: right"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>
                                <div class="x_title">
                                    <h2>Prosecution Evidence List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                 <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                <div style="float:right" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn primary" />
                                </div>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                --%>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red"></span></label>
                                </div>
                                <div class="x_content">
                                    
                                    <asp:GridView ID="grdProsecutionEvidenceView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" OnPageIndexChanging="grdProsecutionEvidenceView_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="TableId" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="TableId" runat="server" Visible="true" Text='<%#Eval("seizure_trial_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prosecution Evidence  Date" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProsecutionEvidenceDate" runat="server" Visible="true" Text='<%#Eval("currentstagedate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Hearing Date" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNextHearingDate" runat="server" Visible="true" Text='<%#Eval("nexthearingdate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="userid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluserid" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server"  OnClick="btnView_Click" CommandName ="View"><i class="fa fa-search-plus"></i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click"  Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>' CommandName="Edit"><i class="fa fa-pencil-square-o"> 
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
