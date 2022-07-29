<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DigiLocker.aspx.cs" Inherits="UserMgmt.DigiLocker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">


    <div class="row top_tiles">
        <div class="">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel">
                    <html>
                    <head>
                        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                        <title>Dashboard</title>

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

                       


                         <script language="javascript" type="text/javascript">
                                        function changeColor(btn) {

                                            if (btn.style.backgroundColor.toLowerCase() == 'skyblue')
                                                btn.style.backgroundColor = 'sandybrown';
                                            else if (btn.style.backgroundColor.toLowerCase() == 'sandybrown')
                                                btn.style.backgroundColor = 'skyblue';

                                            return false;
                                        }
                                    </script>

                      
                      
                    </head>
                    <body>
                        <div class="">
                            <div class="x_title">
                                <h2>Dashboard

                                </h2>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                                           
            <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
	<script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>

                        <div>
                            <div class="container">
                                <div>
                                    <asp:Literal ID="overall" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <hr />
                            <%-- <button onclick="scrolldiv()">Scroll</button>--%>
                        </div>
                        <div id="parent">
                            <div id="chaild1">
                                <div id="myDIV">
                                    <asp:Button ID="btnByThana" CssClass="btn"  runat="server" Text="By Thana" OnClick="btnByThana_Click" />
                                    <asp:Button ID="btnByDate" CssClass="btn"  runat="server" Text="By Date" OnClick="btnByDate_Click" />

                                   
                                </div>

                                <div class="container">
                                    <div class="flex-item">

                                        <asp:Literal ID="DistrictCreated" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div id="chaild2">
                                <div class="container" id="district">
                                    <div class="flex-item">

                                        <asp:Literal ID="DistrictClosed" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div id="chaild3">
                                <hr />
                                <di class="container">
                        <div class="flex-item">
                           
                             <asp:Literal ID="DistrictTampered" runat="server"></asp:Literal>
                        </div>
                            </div>
                            <hr />
                            <div id="chaild4">
                                <div class="container">
                                    <div class="flex-item">

                                        <asp:Literal ID="DistrictOverStop" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="container">
                                <div class="flex-item">
                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="container">
                                <div class="flex-item">
                                    <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                                </div>

                                <div class="container">
                                    <div class="flex-item">
                                        <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="container">
                                    <div class="flex-item">
                                        <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                                    </div>
                                </div>--%>
                        <hr />
                        <hr />

                    </body>
                    </html>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
