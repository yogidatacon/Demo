<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Molasses_LandingPage.aspx.cs" Inherits="UserMgmt.Molasses_LandingPage1" %>

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
                            </head>
                            <body>
                               <ul class="nav nav-tabs">
                                <li>
                                    <asp:LinkButton ID="btnMF1" OnClick="btnMF1_Click" runat="server"><span style="color:#fff;font-size:14px;">Indent for Molasses</span></asp:LinkButton></li>
                                <li >
                                    <asp:LinkButton ID="btnAllocation" OnClick="btnAllocation_Click" runat="server"><span style="color:#fff;font-size:14px;">Allocation Request for Molasses</span></asp:LinkButton></li>
                                <li >
                                    <asp:LinkButton ID="btnMF2" OnClick="btnMF2_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Provisional (MF2)</span></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnMF3" OnClick="btnMF3_Click" runat="server"><span style="color:#fff;font-size:14px;">Molasses Production Actual (MF3)</span></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnMolassesAllocation" OnClick="btnMolassesAllocation_Click"  runat="server"><span style="color:#fff;font-size:14px;">Molasses Allocation</span></asp:LinkButton></li>
                              </ul>

                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
