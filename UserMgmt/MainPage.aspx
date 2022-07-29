<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="UserMgmt.MainPage" %>

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
                                <div class="x_title">
                                    <%--<h2>Dash Board - EA</h2>--%>
                                    <div class="clearfix"></div>

                                    <asp:RadioButtonList ID="rdbUnitType" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitType_SelectedIndexChanged">
                                        <asp:ListItem Value="S" Text=" SUGAR MILL" ></asp:ListItem>
                                        <asp:ListItem Value="D" Text=" DISTILLERY"></asp:ListItem>
                                       <asp:ListItem Value="C" Text="CASE MANAGEMENT"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                  <div>
                                    <ul class="nav nav-tabs">
                                        
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnSCM" Text="Seizure List" Visible="true" OnClick="btnSCM_Click">
                                        <span style="color: #fff; font-size: 14px;">SCM Dash Board</span></asp:LinkButton></li>
                                         <li class="active">
                                            <asp:LinkButton runat="server" ID="btnEA" Text="Seizure List" Visible="true" OnClick="btnEA_Click">
                                        <span style="color: #fff; font-size: 14px;">EA Dash Board</span></asp:LinkButton></li>
                                        
                                        </ul>
                                </div>

                                <div class="x_content">
                                    <div>
                                        <br />
                                        <div id="distdiv" runat="server">
                                            <div class="container">
                                                <div id="MolassesAllocation" runat="server">
                                                    <div class=" col-sm-5">
                                                        <h5 style="align-content: center" runat="server" id="molass">Molasses Allocation Status</h5>
                                                        <div class="flex-item">
                                                            <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1  form-inline">
                                                         </div>

                                                    <div class="col-sm-5 sidenav">
                                                        <h5 style="align-content: center" runat="server" id="MASD">Molasses Allocation Status At Different Stake Holders</h5>
                                                        <div class="flex-item">
                                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="MolassesAllocationStatulist" runat="server">
                                                <asp:LinkButton ID="btnback" runat="server" CssClass="btn btn-primary"
                                                    OnClick="btnBack_Click">Back</asp:LinkButton>
                                             <%--   <asp:LinkButton Text="Print" ID="btnprint" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('MolassesAllocationprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                           </i> 
                                                </asp:LinkButton>--%>
                                                  <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('MolassesAllocationprint')" />
                                                <div style="height: 200px; overflow: auto;">
                                                    <div id="MolassesAllocationprint">
                                                        <h3>
                                                            <asp:Label ID="lblHeading" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                        </h3>

                                                        <asp:GridView ID="GridView1" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false"
                                                            Style="max-width: 1000px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                            <Columns>
                                                                <asp:BoundField DataField="allotmentdate" HeaderText="Allocation Request Date" />
                                                                <asp:BoundField DataField="req_allotmentno" HeaderText="Molasses Allocation Request No" />
                                                                <asp:BoundField DataField="finalallotmentdate" HeaderText="Allotted Date" />
                                                                <asp:BoundField DataField="final_allotmentno" HeaderText="Molasses Final Allocation  No" />
                                                                <%--  <asp:BoundField DataField="a_end_date" HeaderText="Allotment Valid Upto" />--%>
                                                                <asp:BoundField DataField="party_name" HeaderText="Sugar Mill Unit" />
                                                                <%--  <asp:BoundField DataField="DistilleryUnit" HeaderText="Distillery Unit" />--%>
                                                                <asp:BoundField DataField="reqd_qty" HeaderText="Requestd Quantity" />
                                                                <asp:BoundField DataField="qty_allotted_till_date" HeaderText="Allotted Quantity" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="MolassesAllocationStatuDifferentStakeHolders" runat="server">
                                             <asp:LinkButton ID="btnback5" runat="server" CssClass="btn btn-primary" OnClick="btnback5_Click">Back</asp:LinkButton>
                                              <%--  <asp:LinkButton Text="Print" ID="btnprint5" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('DifferentStakeprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>--%>
                                                  <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('DifferentStakeprint')" />
                                                  <div id="DifferentStakeprint">
                                                <div style="height: 200px; overflow: auto;">
                                                  
                                                        <h3>
                                                            <asp:Label ID="lblEI" runat="server" Style="font-family: Cambria;"></asp:Label></h3>

                                                        <asp:GridView ID="GridView5" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                            Style="max-width: 1000px">
                                                            <Columns>
                                                                <asp:BoundField DataField="req_allotmentno" HeaderText="Molasses Allotment Request No" />
                                                                <asp:BoundField DataField="req_allotmentdate" HeaderText="Allotment Request Date" />
                                                                <%--<asp:BoundField DataField="district" HeaderText="District Name" />--%>
                                                                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                                                                <asp:BoundField DataField="reqd_qty" HeaderText="Allotment Request Qty (Qtls)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                               
                                                <br />


                                                <div style="height: 200px; overflow: auto;">
                                                    <div id="">
                                                        <h3>
                                                            <asp:Label ID="lblCom" runat="server" Style="font-family: Cambria;"></asp:Label></h3>
                                                        <asp:GridView ID="GridView6" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                            Style="max-width: 1000px">
                                                            <Columns>
                                                               <asp:BoundField DataField="req_allotmentno" HeaderText="Molasses Allotment Request No" />
                                                                <asp:BoundField DataField="req_allotmentdate" HeaderText="Allotment Request Date" />
                                                                <%--<asp:BoundField DataField="district" HeaderText="District Name" />--%>
                                                                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                                                                <asp:BoundField DataField="reqd_qty" HeaderText="Allotment Request Qty (Qtls)" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <br />
                                                </div>
                                                       </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class=" col-sm-12 ">
                                                <h5 style="margin-left: 150px" id="Distillery" runat="server">Distillery Wise Molasses Allotted Vs Lifted</h5>
                                                <div class="flex-item">
                                                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                              <div class="container">
                                            <div id="NOCApprovalcharts" runat="server">
                                                <div class="col-sm-5 ">
                                                    <h5 style="align-content: center">NOC Approval Status</h5>
                                                    <div class="flex-item">
                                                        <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-1  form-inline">
                                                         </div>
                                                <div class="col-sm-5 sidenav">
                                                    <h5 style="align-content: center">NOC Dispatch Status</h5>
                                                    <div class="flex-item">
                                                        <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div></div>
                                            <div id="NOCApprovallist" runat="server" class="col-md-6 col-sm-6 col-xs-12 form-inline">
                                                <asp:LinkButton ID="btnback4" runat="server" CssClass="btn btn-primary" OnClick="btnBack4_Click">Back</asp:LinkButton>
                                             <%--   <asp:LinkButton Text="Print" ID="btnprint4" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('NOCApprovalprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>--%>
                                                  <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('NOCApprovalprint')" />
                                                <div style="height: 500px; overflow: auto;">
                                                    <div id="NOCApprovalprint">
                                                        <h3>
                                                            <asp:Label ID="lblHeading4" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                            <span style="margin-left: 300px"></span></h3>
                                                        <asp:GridView ID="GridView4" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false"
                                                            Style="max-width: 1000px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                            <Columns>
                                                                <asp:BoundField DataField="nocdate" HeaderText="NOC Date" />
                                                                <asp:BoundField DataField="req_nocno" HeaderText="NOC Request Number" />
                                                                <asp:BoundField DataField="issue_nocno" HeaderText="NOC Number" />
                                                                <asp:BoundField DataField="party_name" HeaderText="Unit Name" />
                                                                <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
                                                                <asp:BoundField DataField="cust_address" HeaderText="Customer Address" />
                                                                <%--   <asp:BoundField DataField="customer_district" HeaderText="Customer District" />
                                                                <asp:BoundField DataField="state" HeaderText="State" />--%>
                                                                <asp:BoundField DataField="issue_date" HeaderText="NOC issued date" />
                                                                <asp:BoundField DataField="valid_upto" HeaderText="NOC valid date" />
                                                                <asp:BoundField DataField="tenderno" HeaderText="Tender or PO" />
                                                                <asp:BoundField DataField="noc_total_qty" HeaderText="NOC Quantity" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="NOCDispatchStatus" runat="server" class="col-md-6 col-sm-6 col-xs-12 form-inline">
                                                <asp:LinkButton ID="btnback10" runat="server" CssClass="btn btn-primary" OnClick="btnback10_Click">Back</asp:LinkButton>
                                              <%--  <asp:LinkButton Text="Print" ID="btnprint10" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('NOCDispatchprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                                </asp:LinkButton>--%>
                                                 <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('NOCDispatchprint')" />
                                                <div style="height: 500px; overflow: auto;">
                                                    <div id="NOCDispatchprint">
                                                        <h3>
                                                            <asp:Label ID="lblHeading10" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                            <span style="margin-left: 300px"></span></h3>
                                                        <asp:GridView ID="GridView10" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                            Style="max-width: 1000px" Visible="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="user_name" HeaderText="Unit Name" />
                                                                <asp:BoundField DataField="totalnocquantity" HeaderText="NOC Approved Quantity" />
                                                                <asp:BoundField DataField="totalliftedquantity" HeaderText="Dispatch Qauantity" />
                                                                <asp:BoundField DataField="totalpendingquantity" HeaderText="Pending Quantity" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <hr />
                                </div>
                                <div id="sugarmill" runat="server">
                                    <div class="container">

                                        <div id="NOCApprovalStatusSugar" runat="server">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <h5 style="align-content: center">NOC Approval Status For Export Of Molasses(Sugar Mills)</h5>
                                                <div>
                                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <h5 style="margin-left: 150px">NOC Status At Different Stake Holders(Sugar Mills)</h5>
                                                <div>
                                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="NOCApprovalStatusSugarList" runat="server">
                                            <asp:LinkButton ID="btnback3" runat="server" CssClass="btn btn-primary" OnClick="btnBack3_Click">Back</asp:LinkButton>
                                          <%--  <asp:LinkButton Text="Print" ID="btnprint3" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('Sugarprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                            </asp:LinkButton>--%>
                                               <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('Sugarprint')" />
                                            <div style="height: 500px; width: 1000px; overflow: auto;">
                                                <div id="Sugarprint">
                                                    <h3>
                                                        <asp:Label ID="lblHeading3" runat="server" Style="font-family: Cambria;"></asp:Label>
                                                        <span style="margin-left: 300px"></span></h3>
                                                    <asp:GridView ID="GridView3" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false"
                                                        Style="max-width: 1000px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                        <Columns>
                                                            <asp:BoundField DataField="nocdate" HeaderText="NOC Date" />
                                                            <asp:BoundField DataField="req_nocno" HeaderText="NOC Request Number" />
                                                            <asp:BoundField DataField="issue_nocno" HeaderText="NOC Number" />
                                                            <asp:BoundField DataField="party_name" HeaderText="Unit Name" />
                                                            <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
                                                            <asp:BoundField DataField="cust_address" HeaderText="Customer Address" />
                                                            <asp:BoundField DataField="valid_upto" HeaderText="NOC issued date" />
                                                            <asp:BoundField DataField="valid_upto" HeaderText="NOC valid date" />
                                                            <asp:BoundField DataField="tenderno" HeaderText="Tender or PO" />
                                                            <asp:BoundField DataField="noc_total_qty" HeaderText="NOC Quantity" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="NOCStatusAtDifferentStakeHolders" runat="server">
                                            <asp:LinkButton ID="btnback7" runat="server" CssClass="btn btn-primary" OnClick="btnBack7_Click">Back</asp:LinkButton>
                                           <%-- <asp:LinkButton Text="Print" ID="btnprint7" CssClass="btn btn-danger" runat="server" OnClientClick="javascript:printDiv('StakeHoldersprint')" CommandName="Print"><i class="fa fa-print"> 
                                                                                    </i> 
                                            </asp:LinkButton>--%>
                                             <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('StakeHoldersprint')" />
                                            <h3>
                                                <asp:Label ID="lblETHCOM" runat="server" Style="font-family: Cambria;"></asp:Label></h3>
                                            <br />
                                            <div style="height: 500px; width: 1000px; overflow: auto;">
                                                <div id="StakeHoldersprint">
                                                    <asp:GridView ID="GridView9" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                        Style="max-width: 1000px">
                                                        <Columns>
                                                            <asp:BoundField DataField="nocdate" HeaderText="NOC Date" />
                                                            <asp:BoundField DataField="req_nocno" HeaderText="NOC Request Number" />
                                                            <asp:BoundField DataField="issue_nocno" HeaderText="NOC Number" />
                                                            <asp:BoundField DataField="party_name" HeaderText="Unit Name" />
                                                            <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
                                                            <asp:BoundField DataField="cust_address" HeaderText="Customer Address" />
                                                            <asp:BoundField DataField="valid_upto" HeaderText="NOC issued date" />
                                                            <asp:BoundField DataField="valid_upto" HeaderText="NOC valid date" />
                                                            <asp:BoundField DataField="tenderno" HeaderText="Tender or PO" />
                                                            <asp:BoundField DataField="noc_total_qty" HeaderText="NOC Quantity" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div></div>
                                            
                                            <br />
                                            <h3>
                                                <asp:Label ID="lblETHHODYCO" runat="server" Style="font-family: Cambria;"></asp:Label></h3>
                                            <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('StakeHolderETHHODYCO')" />
                                            <div style="height: 500px; width: 1000px; overflow: auto;">
                                                   <div id="StakeHolderETHHODYCO">
                                                <asp:GridView ID="GridView8" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                    Style="max-width: 1000px">
                                                    <Columns>
                                                        <asp:BoundField DataField="nocdate" HeaderText="NOC Date" />
                                                        <asp:BoundField DataField="req_nocno" HeaderText="NOC Request Number" />
                                                        <asp:BoundField DataField="issue_nocno" HeaderText="NOC Number" />
                                                        <asp:BoundField DataField="party_name" HeaderText="Unit Name" />
                                                        <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="cust_address" HeaderText="Customer Address" />
                                                        <asp:BoundField DataField="valid_upto" HeaderText="NOC issued date" />
                                                        <asp:BoundField DataField="valid_upto" HeaderText="NOC valid date" />
                                                        <asp:BoundField DataField="tenderno" HeaderText="Tender or PO" />
                                                        <asp:BoundField DataField="noc_total_qty" HeaderText="NOC Quantity" />
                                                    </Columns>
                                                </asp:GridView></div>
                                            </div>
                                       
                                            <h3>
                                                <asp:Label ID="lblEth" runat="server" Style="font-family: Cambria;"></asp:Label></h3>
                                                <input type="button" title="click" class="btn btn-danger" value="Print" onclick="printDiv('StakeHolderEth')" />
                                            <div style="height: 500px; width: 1000px; overflow: auto;">
                                                <div id="StakeHolderEth">
                                                <asp:GridView ID="GridView7" CssClass="footable" runat="server" AutoGenerateColumns="false"
                                                    Style="max-width: 1000px">
                                                    <Columns>
                                                        <asp:BoundField DataField="nocdate" HeaderText="NOC Date" />
                                                        <asp:BoundField DataField="req_nocno" HeaderText="NOC Request Number" />
                                                        <asp:BoundField DataField="issue_nocno" HeaderText="NOC Number" />
                                                        <asp:BoundField DataField="party_name" HeaderText="Unit Name" />
                                                        <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="cust_address" HeaderText="Customer Address" />
                                                        <asp:BoundField DataField="valid_upto" HeaderText="NOC issued date" />
                                                        <asp:BoundField DataField="valid_upto" HeaderText="NOC valid date" />
                                                        <asp:BoundField DataField="tenderno" HeaderText="Tender or PO" />
                                                        <asp:BoundField DataField="noc_total_qty" HeaderText="NOC Quantity" />
                                                    </Columns>
                                                </asp:GridView></div>
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
