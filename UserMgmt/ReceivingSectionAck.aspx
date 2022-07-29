<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivingSectionAck.aspx.cs" Inherits="UserMgmt.ReceivingSectionAck" MasterPageFile="~/Acknowledgment.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="server">
    <style type='text/css'>
        @media print{
            #printbtn, #newbtn{
                display:none;
            }
			
			font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
			font-size:8px;
        }

        @page {
            size: auto;   /* auto is the initial value */
            margin: 0;  /* this affects the margin in the printer settings */
        }

        @media print
        {    
            .no-print, .no-print *
            {
                display: none !important;
            }
        }

        .gridcontent{
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            border: 1px solid #ddd;
            padding: 3px;
            font-size: x-small;
        }

        .main_table {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            /*border-collapse: seperated;*/
            border-collapse: collapse;
            border: 1px solid #ddd;
            width: 100%;
        }

        .main_table td, #main_table th {
            border: 1px solid #ddd;
            padding: 3px;
            font-size: x-small;
        }
        
        .main_table th {
            padding-top: 3px;
            padding-bottom: 3px;
            text-align: left;
            background-color: #1c647e;
            color: white;
        }

    </style>
</asp:Content>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <div style='text-align:center;background-color:#1bb3ea;color:white;height:4%'><span style="font-size:22px"><b>&nbsp; </b></span></div>
    <div style='text-align:center;background-color:#1bb3ea;color:black;height:4%'><span style="font-size:22px"><b>Prohibition, Excise & Registration Dept. Govt. of Bihar </b></span></div>
    <div style='text-align:center;background-color:#1bb3ea;color:black;height:4%;padding-bottom: 20px;'><span style="font-size:22px"><b>Acknowledgement slip </b></span></div>

    <asp:Panel ID="panPolice" runat="server" style="margin-top: 25px; padding: 10px 17px;">
        <table width='90%' class='main_table'>
            <tr>
                <td width='25%'>Form no/Date</td>
                <td width='25%'><asp:Label ID="polFormDate" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>State/Department</td>
                <td width='25%'>Bihar/Police</td>
            </tr>

            <tr>
                <td width='25%'>District</td>
                <td width='25%'><asp:Label ID="polDist" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Thana</td>
                <td width='25%'><asp:Label ID="polThana" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>FIR No.</td>
                <td width='25%'><asp:Label ID="polFirNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Date of FIR</td>
                <td width='25%'><asp:Label ID="polFirDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Letter No.</td>
                <td width='25%'><asp:Label ID="polLetterNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Letter Date</td>
                <td width='25%'><asp:Label ID="polLetterDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>FIR Copy</td>
                <td width='25%'><asp:Label ID="polFirCopy" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Court Order</td>
                <td width='25%'><asp:Label ID="polCourtOrder" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Seizure List</td>
                <td width='25%'><asp:Label ID="polSeizureList" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'></td>
                <td width='25%'></td>
            </tr>

            <tr>
                <td width='25%'>Sealed Status</td>
                <td width='25%'><asp:Label ID="polSealedStatus" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Sealed Details</td>
                <td width='25%'><asp:Label ID="polSealedDetails" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="panExcise" runat="server" style="margin-top: 25px; padding: 10px 17px;">
        <table width='90%' class='main_table'>
            <tr>
                <td width='25%'>Form no/Date</td>
                <td width='25%'><asp:Label ID="exFormDate" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>State/Department</td>
                <td width='25%'>Bihar/Excise</td>
            </tr>

            <tr>
                <td width='25%'>District</td>
                <td width='25%'><asp:Label ID="exDist" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Remark</td>
                <td width='25%'><asp:Label ID="exRemark" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Letter No.</td>
                <td width='25%'><asp:Label ID="exLetterNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Letter Date</td>
                <td width='25%'><asp:Label ID="exLetterDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Case No.</td>
                <td width='25%'><asp:Label ID="exCaseNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Date of Case</td>
                <td width='25%'><asp:Label ID="exCaseDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Sealed Status</td>
                <td width='25%'><asp:Label ID="exSealedStatus" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Sealed Details</td>
                <td width='25%'><asp:Label ID="exSealedDetails" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>P.R. No.</td>
                <td width='25%'><asp:Label ID="exPrNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>State V/S</td>
                <td width='25%'><asp:Label ID="exStateVs" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="panDistill" runat="server" style="margin-top: 25px; padding: 10px 17px;">
        <table width='90%' class='main_table'>
            <tr>
                <td width='25%'>Form no/Date</td>
                <td width='25%'><asp:Label ID="disFormDate" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>State/Department</td>
                <td width='25%'>Bihar/Distillery</td>
            </tr>

            <tr>
                <td width='25%'>District</td>
                <td width='25%'><asp:Label ID="disDistrict" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Distillery Name</td>
                <td width='25%'><asp:Label ID="disDistilleryName" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>VAT No.</td>
                <td width='25%'><asp:Label ID="disVatNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Denatured Date</td>
                <td width='25%'><asp:Label ID="disDenaturedDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Remark</td>
                <td width='25%'><asp:Label ID="disRemark" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'></td>
                <td width='25%'></td>
            </tr>

            <tr>
                <td width='25%'>Letter No.</td>
                <td width='25%'><asp:Label ID="disLetterNo" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Letter Date</td>
                <td width='25%'><asp:Label ID="disLetterDate" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>

            <tr>
                <td width='25%'>Sealed Status</td>
                <td width='25%'><asp:Label ID="disSealedStatus" runat="server" Visible="true" Text=''></asp:Label></td>
			    <td width='25%'>Sealed Details</td>
                <td width='25%'><asp:Label ID="disSealedDetails" runat="server" Visible="true" Text=''></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <center>
    <asp:GridView ID="grdQuantList" runat="server" AutoGenerateColumns="false" Width="90%">
        <columns> 
            <asp:TemplateField HeaderText="Reference No" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblQuantId" runat="server" Visible="true" Text='<%#Eval("QuantId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type of Liquor" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblLiqType" runat="server" Visible="true" Text='<%#Eval("LiqType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Liquor Sub Type" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%#Eval("LiqSubType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Size" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSize" runat="server" Visible="true" Text='<%#Eval("LiqSize") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address of Manufacturer" ItemStyle-Width="22%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblAddress" runat="server" Visible="true" Text='<%#Eval("LiqAddr") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%#Eval("LiqQuant") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Brand Name" ItemStyle-Width="22%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblBrand" runat="server" Visible="true" Text='<%#Eval("LiqBrand") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Batch No" ItemStyle-Width="9%" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblBatch" runat="server" Visible="true" Text='<%#Eval("LiqBatch") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </columns>
        <headerstyle borderstyle="Solid" borderwidth="1px" horizontalalign="Left"></headerstyle>
        <rowstyle borderstyle="Solid" borderwidth="1px" horizontalalign="Left"></rowstyle>
    </asp:GridView>
    </center>

    <asp:Panel ID="Panel1" runat="server" style="margin-top: 5px; padding: 10px 17px;">
        <table width='90%' class='main_table'>
            <tr>
			    <td width="50%" height="100px" align="left" valign="bottom"><b>Sign. of Party/ Representative</b></td>
			    <td width="50%" height="100px" align="right" valign="bottom"><b>Officer-in-Charge</b></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <center>
        <asp:Panel ID="Panel2" runat="server" style="margin-top: 25px; padding: 10px 17px;">
            <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary" onclientclick="window.print();" />
            <asp:Button ID="btnNew" runat="server" Text="New Entry" class="btn btn-primary" OnClick="btnNew_Click" />
        </asp:Panel>
    </center>
</asp:Content>