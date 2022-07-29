<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DashBoardDetails.aspx.cs" Inherits="UserMgmt.DashBoardDetails" %>
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
                                <title>Defence Statement Form</title>
                                <script>
                                </script>
                            </head>
                            <body>
                              
                            
                               
                                <div class="x_title">
                                    <h2>Dashboard</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">

                               
        <div><h3>
            <asp:Label ID="lblHeading" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            </div><asp:Button ID="printButton" runat="server" Text="Print" CssClass="btn btn primary" OnClientClick="javascript:window.print();" />
        <br /><hr />
        <asp:GridView ID="GridView1" CssClass="footable footable-detail-show" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
            <Columns>
                <asp:BoundField DataField="req_allotmentdate" HeaderText="Allocation Request Date" />
                <asp:BoundField DataField="req_allotmentno" HeaderText="Molasses Allocation Request No" />
                <asp:BoundField DataField="final_allotmentdate" HeaderText="Allotted Date" />
                <asp:BoundField DataField="final_allotmentno" HeaderText="Molasses Final Allocation  No" />
              <%--  <asp:BoundField DataField="a_end_date" HeaderText="Allotment Valid Upto" />--%>
                <asp:BoundField DataField="party_name" HeaderText="Sugar Mill Unit" />
              <%--  <asp:BoundField DataField="DistilleryUnit" HeaderText="Distillery Unit" />--%>
                <asp:BoundField DataField="reqd_qty" HeaderText="Requestd Quantity" />
                 <asp:BoundField DataField="qty_allotted_till_date" HeaderText="Allotted Quantity" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="entrydate" HeaderText="Date" />
                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                <asp:BoundField DataField="totalproduction" HeaderText="Production" />
                <asp:BoundField DataField="totalissued" HeaderText="Dispatch" />
                <asp:BoundField DataField="closingbalance" HeaderText="Closing Balance" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView10" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px" Visible="false">
            <Columns>
                <asp:BoundField DataField="user_name" HeaderText="Unit Name" />
                <asp:BoundField DataField="totalnocquantity" HeaderText="NOC Approved Quantity" />
                <asp:BoundField DataField="totalliftedquantity" HeaderText="Dispatch Qauantity" />
                <asp:BoundField DataField="totalpendingquantity" HeaderText="Pending Quantity" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView3" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1300px">
            <Columns>
                <asp:BoundField DataField="apply_date" HeaderText="NOC Date" />
                <asp:BoundField DataField="noc_number" HeaderText="NOC Request Number" />
                <asp:BoundField DataField="issued_nocno" HeaderText="NOC Number" />
                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                <asp:BoundField DataField="customer_name" HeaderText="Customer Name" />
                <asp:BoundField DataField="customer_address" HeaderText="Customer Address" />
                <asp:BoundField DataField="customer_district" HeaderText="Customer District" />
                <asp:BoundField DataField="state" HeaderText="State" />
                <asp:BoundField DataField="valid_date" HeaderText="NOC issued date" />
                <asp:BoundField DataField="valid_date" HeaderText="NOC valid date" />
                <asp:BoundField DataField="customer_tender_or_po" HeaderText="Tender or PO" />
                <asp:BoundField DataField="nocquantity" HeaderText="NOC Quantity" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView4" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="apply_date" HeaderText="NOC Date" />
                <asp:BoundField DataField="noc_number" HeaderText="NOC Request Number" />
                <asp:BoundField DataField="issued_nocno" HeaderText="NOC Number" />
                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                <asp:BoundField DataField="customer_name" HeaderText="Customer Name" />
                <asp:BoundField DataField="customer_address" HeaderText="Customer Address" />
                <asp:BoundField DataField="customer_district" HeaderText="Customer District" />
                <asp:BoundField DataField="state" HeaderText="State" />
                <asp:BoundField DataField="valid_date" HeaderText="NOC issued date" />
                <asp:BoundField DataField="issued_date" HeaderText="NOC valid date" />
                <asp:BoundField DataField="customer_tender_or_po" HeaderText="Tender or PO" />
                <asp:BoundField DataField="nocquantity" HeaderText="NOC Quantity" />
                <asp:BoundField DataField="ApplicationStatus" HeaderText="Application Status" />
                <asp:BoundField DataField="Issued" HeaderText="Issued" />
            </Columns>
        </asp:GridView>

        <div>
            <h3>
            <asp:Label ID="lblEI" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            <br /> 
            <asp:GridView ID="GridView5" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="allotmentrequestno" HeaderText="Molasses Allotment Request No" />
                <asp:BoundField DataField="allotmentrequestdate" HeaderText="Allotment Request Date" />
                <asp:BoundField DataField="district" HeaderText="District Name" />
                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                <asp:BoundField DataField="molassesrequiredqty" HeaderText="Allotment Request Qty (Qtls)" />
            </Columns>
        </asp:GridView>
            <br /> 
            <h3>
            <asp:Label ID="lblCom" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            <br /><hr />
            <asp:GridView ID="GridView6" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="allotmentrequestno" HeaderText="Molasses Allotment Request No" />
                <asp:BoundField DataField="allotmentrequestdate" HeaderText="Allotment Request Date" />
                <asp:BoundField DataField="district" HeaderText="District Name" />
                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                <asp:BoundField DataField="molassesrequiredqty" HeaderText="Allotment Request Qty (Qtls)" />
            </Columns>
        </asp:GridView><br /> 

             <br /> 
            <h3>
            <asp:Label ID="lblETHCOM" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            <br /> 
            <asp:GridView ID="GridView9" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="noc_number" HeaderText="NOC Request No" />
                <asp:BoundField DataField="apply_date" HeaderText="NOC Request Date" />
                <asp:BoundField DataField="customer_district" HeaderText="District Name" />
                <asp:BoundField DataField="customer_name" HeaderText="Unit Name" />
                <asp:BoundField DataField="noc_for" HeaderText="Type of NOC" />
                <asp:BoundField DataField="total_qty" HeaderText="NOC Request Qty (Qtls)" />
            </Columns>
        </asp:GridView>
             <br /> 
            <h3>
            <asp:Label ID="lblETHHODYCO" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            <br /> 
            <asp:GridView ID="GridView8" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="noc_number" HeaderText="NOC Request No" />
                <asp:BoundField DataField="apply_date" HeaderText="NOC Request Date" />
                <asp:BoundField DataField="customer_district" HeaderText="District Name" />
                <asp:BoundField DataField="customer_name" HeaderText="Unit Name" />
                <asp:BoundField DataField="noc_for" HeaderText="Type of NOC" />
                <asp:BoundField DataField="total_qty" HeaderText="NOC Request Qty (Qtls)" />
            </Columns>
        </asp:GridView><br /> 
            <h3>
            <asp:Label ID="lblEth" runat="server" style="font-family:Cambria;"></asp:Label></h3>
            <br /><hr />
            <asp:GridView ID="GridView7" CssClass="footable" runat="server" AutoGenerateColumns="false"
            Style="max-width: 1000px">
            <Columns>
                <asp:BoundField DataField="noc_number" HeaderText="NOC Request No" />
                <asp:BoundField DataField="apply_date" HeaderText="NOC Request Date" />
                <asp:BoundField DataField="customer_district" HeaderText="District Name" />
                <asp:BoundField DataField="customer_name" HeaderText="Unit Name" />
                <asp:BoundField DataField="noc_for" HeaderText="Type of NOC" />
                <asp:BoundField DataField="total_qty" HeaderText="NOC Request Qty (Qtls)" />
            </Columns>
        </asp:GridView><br /> 
            <br /> 
        </div>
        <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=GridView1]').footable();
            });
        </script>
                                    </div></body>
                            </html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>







</asp:Content>
