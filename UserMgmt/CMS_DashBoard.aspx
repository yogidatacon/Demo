<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin.master" CodeBehind="CMS_DashBoard.aspx.cs" Inherits="UserMgmt.CMS_DashBoard" %>

<asp:Content ID="Home" ContentPlaceHolderID="BodyContent" runat="server">
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
                                <title>User Management</title>
                                <style>
                                    .btn {
                                        border: none;
                                        outline: none;
                                        padding: 10px 16px;
                                        background-color: skyblue;
                                        cursor: pointer;
                                        font-size: 18px;
                                    }
                                </style>
                                <style>
                                    .scontainer {
                                        display: flex;
                                    }

                                    .fixed {
                                        width: 700px;
                                    }

                                    .flex-item {
                                        flex-grow: 1;
                                    }
                                </style>

                                <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.js" type="text/javascript"></script>
                                <script type="text/javascript" src="//cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
                                <script src=" https://cdn.fusioncharts.com/fusioncharts/latest/maps/fusioncharts.bihar.js"></script>
                                <script src=" https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.maps.js"></script>
                                <script type="text/javascript" src="//cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>

                            </head>
                            <body>
                                <div class="x_title">
                                    <h2>Dash Board - CMS</h2>
                                    <div class="clearfix"></div>
                                    <%--   
                                    <asp:RadioButtonList ID="rdbUnitType"  CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitType_SelectedIndexChanged">
                                        <asp:ListItem Value="S" Text=" SUGAR MILL"></asp:ListItem>
                                        <asp:ListItem Value="D" Text=" DISTILLERY"></asp:ListItem>
                                    </asp:RadioButtonList>--%>

                                    <asp:RadioButtonList ID="rdbUnitType" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitType_SelectedIndexChanged">
                                        <asp:ListItem Value="S" Text="SUGAR MILL"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="DISTILLERY"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="CASE MANAGEMENT"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="x_content">
                                    <div>
                                        <br />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal10" runat="server"></asp:Literal>
                                            </div>
                                            <hr />
                                        </div>
                                        <hr />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L3" runat="server"></asp:Literal>
                                            </div>

                                            <div class="flex-item">
                                                <asp:Literal ID="L4" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L5" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L6" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <hr />

                                        <div class="container">
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                            </div>

                                        </div>
                                        <hr />

                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L7" runat="server"></asp:Literal>
                                            </div>
                                            <div class="flex-item">
                                                <asp:Literal ID="L8" runat="server"></asp:Literal>
                                            </div>
                                            <div class="flex-item">
                                                <asp:Literal ID="L9" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L10" runat="server"></asp:Literal>
                                            </div>

                                        </div>
                                        <hr />
                                        <div class="scontainer">
                                            <div class="flex-item">
                                                <asp:Literal ID="L11" runat="server"></asp:Literal>

                                            </div>
                                        </div>
                                        <hr />
                                        <div class="scontainer" runat="server" visible="false">

                                            <div class="flex-item">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div>

                                            <div class="flex-item">
                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
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
