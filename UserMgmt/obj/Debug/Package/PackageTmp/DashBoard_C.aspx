<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DashBoard_C.aspx.cs" Inherits="UserMgmt.DashBoard_C" %>

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
                                <title>Allocation Request List </title>
                            </head>
                            <body>
                                <div class="x_title">
                                    <h2>Dash Board - EA</h2>
                                    <div class="clearfix"></div>
                            <asp:RadioButtonList ID="rdbUnitType" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitType_SelectedIndexChanged">
                                                              <asp:ListItem Value="S" Text=" SUGAR MILL"></asp:ListItem>
                                                                <asp:ListItem Value="D" Text=" DISTILLERY"></asp:ListItem>
                                                             </asp:RadioButtonList>
                            
                        </div>
                               
                                <div class="x_content">
                                    <div > 
                                    <br />
                   
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div id="distdiv" runat="server" visible="false">
                                    <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                        </div>
                        &nbsp;&nbsp;
                        </div>
                        <hr /> 
                    <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <hr /> 
                    <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <hr /> 
                                        
                    <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                        </div>
                        &nbsp;&nbsp;
                    
                        <div class="flex-item">
                            <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <hr /> 
                                       
                                    <div id="sugarmill" runat="server" visible="false">
                                    <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </div>
                         
                    </div>
                                        <br /><hr />
                                        <div class="container">
                        <div class="flex-item">
                            <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <hr />
                                        </div>
                                </div>	


                            </div>

                      
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
