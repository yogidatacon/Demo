<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SCMDashBoard.aspx.cs" Inherits="UserMgmt.SCMDashBoard" %>
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
                                <title>User Management</title>
                                <style>
                                   
                                </style>

                                <script type="text/javascript">
                                    function Func() {
                                        $("#MolassesProductionList").slideDown(1000);
                                        $("#SugarMolasseslist").slideDown(1000);
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">

                                    function printDiv(divID) {
                                        debugger;
                                        var contents = document.getElementById(divID).innerHTML;
                                        var frame1 = document.createElement('iframe');
                                        frame1.name = "frame1";
                                        frame1.style.position = "absolute";
                                        frame1.style.top = "-1000000px";
                                        document.body.appendChild(frame1);
                                        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                                        frameDoc.document.open();
                                        frameDoc.document.write('<html><head><title></title>');
                                        frameDoc.document.write('</head><body>');
                                        frameDoc.document.write(contents);
                                        frameDoc.document.write('</body></html>');
                                        frameDoc.document.close();
                                        setTimeout(function () {
                                            window.frames["frame1"].focus();
                                            window.frames["frame1"].print();
                                            document.body.removeChild(frame1);
                                        }, 500);
                                        return false;
                                    }
                                </script>
                                <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
                                <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
                            </head>
                            <body>
                                <div id="dasboard" runat="server">
                                <div class="x_title">
                                    <%--<h2>Dash Board:SCM </h2>--%>
                                    <div class="clearfix"></div>
                                    <asp:RadioButtonList ID="rdbUnitType" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitType_SelectedIndexChanged">
                                        <asp:ListItem Value="S" Text=" SUGAR MILL"></asp:ListItem>
                                        <asp:ListItem Value="D" Text=" DISTILLERY"></asp:ListItem>
                                         <asp:ListItem Value="C" Text="CASE MANAGEMENT"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div>
                                    <ul class="nav nav-tabs">
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnSCM" Text="Seizure List" Visible="true" OnClick="btnSCM_Click">
                                        <span style="color: #fff; font-size: 14px;">SCM Dash Board</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnEA" Text="Seizure List" Visible="true" OnClick="btnEA_Click">
                                        <span style="color: #fff; font-size: 14px;">EA Dash Board</span></asp:LinkButton></li>
                                       
                                    </ul>
                                </div>
                                   <div class="clearfix"></div>
                                <p>&nbsp</p>
                                <div id="distdiv" runat="server">
                                    <div class="container">
                                    <div id="DWMolassesPurchasecharts" runat="server">
                                        
                                              <div class=" col-sm-12">
                                            <h5 style="align-content: center" id="purchase" runat="server">Molasses Purchase Vs Consumption</h5>
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                            </div>
                                                  </div>
                                        </div>
                                    </div>
                                    <div id="DWMolassesPurchaselist" runat="server">
                                        <asp:LinkButton ID="btnback5" runat="server" CssClass="btn btn-primary" OnClick="btnback5_Click">Back</asp:LinkButton>
                                        <%--  <asp:LinkButton Text="Print" ID="btnprint5" CssClass="btn btn-danger" runat="server" OnClientClick="printDiv('DWMolassesPurchaseprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>
                                                <button onclick="printDiv('DWMolassesPurchaseprint')"><i class="fa fa-print"></i></button>--%>

                                        <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('DWMolassesPurchaseprint')" />

                                        <div style="height: 250px; overflow: auto;">
                                            <div id="DWMolassesPurchaseprint">
                                                <h3>
                                                    <asp:Label ID="lblHeading5" runat="server" Style="font-family: Cambria; font-size: 16px"></asp:Label>
                                                    <span style="margin-left: 100px"></span></h3>
                                                <asp:GridView ID="GridView5" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false" Width="800px"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                    <Columns>
                                                        <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                                                        <asp:BoundField DataField="totalPurchase" HeaderText="Receipt" />
                                                        <asp:BoundField DataField="totalConsumption" HeaderText="Consumption" />
                                                        <%-- <asp:BoundField DataField="closingbalance" HeaderText="Closingbalance" />--%>
                                                        <%--  <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>


                                      <div class="container">
                                    <div id="DWAbsoluteAlcoholChart" runat="server">
                                      
                                             <div class=" col-sm-12">
                                            <h5 style="align-content: center" id="AAPC" runat="server">Absolute Alcohol Production Vs Consumption</h5>
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                            </div></div>
                                        </div>
                                    </div>

                                    <div id="DWAbsoluteAlcohollist" runat="server">
                                        <asp:LinkButton ID="btnback4" runat="server" CssClass="btn btn-primary" OnClick="btnback4_Click">Back</asp:LinkButton>
                                        <%--  <asp:LinkButton Text="Print" ID="btnprint4" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('DWAbsoluteAlcoholprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>--%>
                                        <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('DWAbsoluteAlcoholprint')" />
                                        <div style="height: 250px; overflow: auto;">
                                            <div id="DWAbsoluteAlcoholprint">
                                                <h3>
                                                    <asp:Label ID="lblHeading4" runat="server" Style="font-family: Cambria; font-size: 16px"></asp:Label>
                                                    <span style="margin-left: 100px"></span></h3>
                                                <asp:GridView ID="GridView4" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false" Width="800px"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                    <Columns>
                                                        <%--  <asp:BoundField DataField="entrydate" HeaderText="Date" />--%>
                                                        <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                                                        <asp:BoundField DataField="totalPurchase" HeaderText="Receipt" />
                                                        <asp:BoundField DataField="totalConsumption" HeaderText="Consumption" />
                                                        <%-- <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                      <div class="container">
                                    <div id="DWEthanolCharts" runat="server">
                                      
                                              <div class=" col-sm-12">
                                            <h5 style="align-content: center" id="EPS" runat="server">Ethanol Production Vs Sale</h5>
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                                            </div></div>
                                        </div>
                                    </div>
                                    <div id="DWEthanolList" runat="server">
                                        <asp:LinkButton ID="btnback3" runat="server" CssClass="btn btn-primary" OnClick="btnback3_Click">Back</asp:LinkButton>
                                        <%--   <asp:LinkButton Text="Print" ID="btnprint3" CssClass="btn btn-danger"  runat="server" OnClientClick="javascript:printDiv('DWEthanolprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>--%>
                                        <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('DWEthanolprint')" />
                                        <div style="height: 250px; overflow: auto;">
                                            <div id="DWEthanolprint">
                                                <h3>
                                                    <asp:Label ID="lblHeading3" runat="server" Style="font-family: Cambria; font-size: 16px"></asp:Label>
                                                    <span style="margin-left: 100px"></span></h3>
                                                <asp:GridView ID="GridView3" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false" Width="800px"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                    <Columns>
                                                        <%--  <asp:BoundField DataField="rmr_entrydate" HeaderText="Date" />--%>
                                                        <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                                                        <asp:BoundField DataField="totalPurchase" HeaderText="Production" />
                                                        <asp:BoundField DataField="totalConsumption" HeaderText="Sale" />
                                                        <%--  <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <div class="container">
                                                    <div class="flex-item">
                                                        <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                                                    </div></div>
                                                    &nbsp;&nbsp;
                        <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                        <div class="flex-item">
                            <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                        </div></div>--%>
                                </div>
                                <div id="sugarmill" runat="server">
                                     <div class="container">
                                    <div id="sugarMolassescharts" runat="server">
                                       
                                             <div class=" col-sm-12">
                                            <h5 style="margin-left: 400px">Sugar Cane Purchase Vs Consumption</h5>
                                            <div class="flex-item">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div></div>
                                        </div>
                                    </div>
                                    <div id="SugarMolasseslist" runat="server">
                                        <asp:LinkButton ID="btnback" runat="server" CssClass="btn btn-primary" OnClick="btnback_Click">Back</asp:LinkButton>
                                        <%--  <input type="button" value="click" onclick="printdiv('Sugarprint');" class="btn btn-danger" ><i class="fa fa-print"></i>  --%>
                                        <%-- <asp:LinkButton Text="Print" ID="btnprint" CssClass="btn btn-danger" runat="server" CommandName="Print" OnClientClick="javascript:printDiv('Sugarprint')" ><i class="fa fa-print"> 
                                                                                    </i> 
                                        </asp:LinkButton>--%>
                                        <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('Sugarprint')" />
                                        <div style="height: 250px; overflow: auto;" id="Sugar" runat="server">
                                            <div id="Sugarprint">
                                                <h3>
                                                    <asp:Label ID="lblHeading" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                    <span style="margin-left: 100px"></span></h3>
                                                <asp:GridView ID="GridView1" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false" Width="800px"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                    <Columns>
                                                        <asp:BoundField DataField="entrydate" HeaderText="Date" />
                                                        <asp:BoundField DataField="total_purchase" HeaderText="Receipt" />
                                                        <asp:BoundField DataField="total_canecrushed" HeaderText="Consumption" />
                                                        <%--  <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <%--   <div id="Sugarprint" style="height: auto; width: auto;" >
                                              <asp:GridView ID="GridView6" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false" Width="800px"
                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                <Columns>
                                                    <asp:BoundField DataField="entrydate" HeaderText="Date" />
                                                    <asp:BoundField DataField="total_purchase" HeaderText="Receipt" />
                                                    <asp:BoundField DataField="total_canecrushed" HeaderText="Consumption" />
                                                   <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>--%>
                                    </div>
                                     <div class="container">
                                    <div id="MolassesProduction" runat="server">
                                       
                                             <div class=" col-sm-12">
                                            <h5 style="margin-left: 400px">Molasses Production Vs Dispatch</h5>
                                            <div>
                                                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                            </div></div>
                                        </div>
                                    </div>
                                    <div id="MolassesProductionList" runat="server">
                                        <%--  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger"   OnClick="btnBack_Click"  />--%>
                                        <asp:LinkButton ID="btnback2" runat="server" CssClass="btn btn-primary" OnClick="btnback2_Click">Back</asp:LinkButton>
                                        <%-- <asp:LinkButton Text="Print" ID="btnprint2" CssClass="btn btn-danger" runat="server" CommandName="Print" OnClientClick="javascript:printDiv('Molassesprint')"><i class="fa fa-print"> 
                                                                                    </i> 
                                        </asp:LinkButton>--%>
                                        <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('Molassesprint')" />
                                        <div style="height: 250px; overflow: auto;" id="Molasses" runat="server">
                                            <div id="Molassesprint">
                                                <h3>
                                                    <asp:Label ID="lblHeading1" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                    <span style="margin-left: 100px"></span></h3>
                                                <asp:GridView ID="GridView2" CssClass="footable" runat="server" AutoGenerateColumns="false" Width="800px">
                                                    <Columns>
                                                        <asp:BoundField DataField="entrydate" HeaderText="Date" />
                                                        <asp:BoundField DataField="openingbalancevalue" HeaderText="Opening Balance" />
                                                        <asp:BoundField DataField="dailyproduction" HeaderText="Production" />
                                                        <asp:BoundField DataField="issuedqty" HeaderText="Dispatch" />
                                                        <%-- <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn primary" OnClick="btnBack_Click"  />--%>
                                    </div>

                                </div></div>
                            </body>
                                <%--<script type="text/javascript">var $zoho=$zoho || {};$zoho.salesiq = $zoho.salesiq || {widgetcode:"da1fb4e629fc53376116bae1d2c5adad9511cd93f15005f11f9aad94bb8e0d783ee3e2c9d877e93a6ea2c1297580d759", values:{},ready:function(){}};var d=document;s=d.createElement("script");s.type="text/javascript";s.id="zsiqscript";s.defer=true;s.src="https://salesiq.zoho.in/widget";t=d.getElementsByTagName("script")[0];t.parentNode.insertBefore(s,t);d.write("<div id='zsiqwidget'></div>");</script>--%>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
