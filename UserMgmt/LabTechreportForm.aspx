<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabTechreportForm.aspx.cs" Inherits="UserMgmt.LabTechreportForm" MasterPageFile="~/LabTechMaster.Master" %>




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
                                <style type='text/css'>
                                    .error-text {
                                        color: #cc0033;
                                        font-family: Helvetica, Arial, sans-serif;
                                        font-size: 13px;
                                        font-weight: bold;
                                        line-height: 20px;
                                        text-shadow: 1px 1px rgba(250,250,250,.3);
                                    }

                                    th, td {
                                        border: 1px solid rgba(246, 251, 252, 0.46);
                                    }

                                    .bordered {
                                        border: 1px solid black;
                                    }

                                    .tabletech {
                                        width: 100%;
                                        border-collapse: collapse;
                                        border: 1px solid black;
                                        margin-top: 20px;
                                        padding: 5px;
                                    }

                                    .frmtech {
                                        border: 1px solid black;
                                        border-radius: 5px;
                                        margin: 20px 5px;
                                        padding: 10px;
                                    }

                                    .tabletech td {
                                        padding: 5px;
                                    }
                                </style>

                                <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>--%>


                                <script type="text/javascript">
                                    //$(document).ready(function () {
                                    //    alert("hello");
                                    //    //$('#labdeviceradiohydro').change(function () {
                                    //    //    alert("hydro");
                                    //    //});
                                    //    $('input[type=radio][id="labdeviceradiohydro"]').change(function () {
                                    //        alert($(this).val()); // or, use `this.value`
                                    //    });
                                    //});




                                    //var isChecked = document.getElementById("labdeviceradiohydro").checked;
                                    //if (ischecked) {
                                    //       alert("hydro");
                                    //}

                                    function CheckHydroEvent() {
                                        //alert("fun");
                                        var chkhydro = document.getElementById("<%=labdeviceradiohydro.ClientID %>").checked;
                                        if (chkhydro == true) {
                                            // alert(chkhydro);  

                                            document.getElementById("<%=txtIndication.ClientID %>").disabled = false;
                                            document.getElementById("<%=txtHydrotemp.ClientID %>").disabled = false;

                                            document.getElementById("<%=txtPyknometerempty.ClientID %>").value = '';
                                            document.getElementById("<%=txtPyknometerdmwater.ClientID %>").value = '';
                                            document.getElementById("<%=txtPyknometersample.ClientID %>").value = '';
                                            document.getElementById("<%=txtPyknotemperature.ClientID %>").value = '';

                                            document.getElementById("<%=txtPyknometerempty.ClientID %>").disabled = true;
                                            document.getElementById("<%=txtPyknometerdmwater.ClientID %>").disabled = true;
                                            document.getElementById("<%=txtPyknometersample.ClientID %>").disabled = true;
                                            document.getElementById("<%=txtPyknotemperature.ClientID %>").disabled = true;
                                        }

                                    }

                                    function CheckPyknoEvent() {
                                        //alert("fun");
                                        var chkpykno = document.getElementById("<%=labdeviceradiopykno.ClientID %>").checked;
                                        if (chkpykno == true) {
                                            //alert(chkpykno);

                                            document.getElementById("<%=txtPyknometerempty.ClientID %>").disabled = false;
                                            document.getElementById("<%=txtPyknometerdmwater.ClientID %>").disabled = false;
                                            document.getElementById("<%=txtPyknometersample.ClientID %>").disabled = false;
                                            document.getElementById("<%=txtPyknotemperature.ClientID %>").disabled = false;

                                            document.getElementById("<%=txtIndication.ClientID %>").value = '';
                                            document.getElementById("<%=txtHydrotemp.ClientID %>").value = '';
                                            document.getElementById("<%=txtIndication.ClientID %>").disabled = true;
                                            document.getElementById("<%=txtHydrotemp.ClientID %>").disabled = true;
                                        }
                                    }


                                    function PassTestEvent() {
                                        var passtest = document.getElementById("<%=parameterpasses.ClientID %>").checked;
                                        if (passtest == true) {
                                            //alert(passtest);

                                        }
                                    }
                                    //Maharshi Start
                                    //function confirmation() {
                                    //    alert("Confirmation");
                                    //}

                                    function validationMsg() {
                                        var agree = confirm("Are you sure, you want to save this page, Once submitted this is not editable?");
                                        if (agree)
                                            return true;
                                        else
                                            return false;
                                    
                                    }


                                </script>



                                <title>User Management</title>
                            </head>
                            <body>
                                <div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <asp:Label runat="server" ID="lblError" CssClass="error-text" Visible="false" />
                                                <div class="bordered">
                                                    <table class="tabletech">
                                                        <tr>
                                                            <td style="width: 15%" align="right">Form No. / Date</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=formanddate %></td>

                                                            <td style="width: 14%" align="right">Liquor Type</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=liq_type %></td>

                                                            <td style="width: 14%" align="right">Liquor Sub Type</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=liq_sub_type_name%></td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 15%" align="right">Size (ml/ltr)</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=size_name %></td>

                                                            <td style="width: 14%" align="right">Quantity</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=quantity %></td>

                                                            <td style="width: 14%" align="right">Brand</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=brand_name %></td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 15%" align="right">Batch No</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=batch_no %></td>

                                                            <td style="width: 14%" align="right">Address</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=address %></td>

                                                            <td style="width: 14%" align="right">Status</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=status %></td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 15%" align="right">Compactor ID</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left"><%=compactor_id %></td>

                                                            <td style="width: 14%" align="right">Proof Strength</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left">
                                                                <asp:DropDownList ID="proofDropdown" runat="server">
                                                                    <asp:ListItem Value="UP">UP</asp:ListItem>
                                                                    <asp:ListItem Value="OP">OP</asp:ListItem>
                                                                </asp:DropDownList><asp:TextBox ID="txtProofstr" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 14%" align="right">Color</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left">
                                                                <asp:DropDownList ID="colorDropdown" runat="server"></asp:DropDownList></td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 15%" align="right">Smell</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left">
                                                                <asp:TextBox ID="txtSmell" runat="server"></asp:TextBox></td>

                                                            <td style="width: 14%" align="right">Remarks</td>
                                                            <td style="width: 2%">:</td>
                                                            <td style="width: 17%" align="left">
                                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="70"></asp:TextBox></td>

                                                            <td style="width: 14%" align="right">
                                                                <!--Temperature-->
                                                            </td>
                                                            <td style="width: 2%">
                                                                <!--Temperature-->
                                                            </td>
                                                            <td style="width: 17%" align="left">
                                                                <!--<input type="text" name="temperature" value="">-->
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <br />

                                                <div class="bordered">
                                                    <table class="tabletech">
                                                        <tr>
                                                            <td colspan="6">Laboratory Device used for Testing Alcohol Strength</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                <asp:RadioButton ID="labdeviceradiohydro" runat="server" GroupName="labdeviceradio" onclick="CheckHydroEvent()" />Hydrometer</td>
                                                            <td colspan="3" align="center">
                                                                <asp:RadioButton ID="labdeviceradiopykno" runat="server" GroupName="labdeviceradio" onclick="CheckPyknoEvent()" />Pyknometer</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" align="right">Indication</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtIndication" runat="server"></asp:TextBox></td>

                                                            <td style="width: 25%" align="left">Empty pyknometer weight</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtPyknometerempty" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" align="right">Temperature</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtHydrotemp" runat="server"></asp:TextBox></td>

                                                            <td style="width: 25%" align="left">D.M water with pyknometer weight</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtPyknometerdmwater" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" align="left">&nbsp;</td>
                                                            <td style="width: 3%"></td>
                                                            <td style="width: 22%" align="left">&nbsp;</td>

                                                            <td style="width: 25%" align="left">Sample with pyknometer weight</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtPyknometersample" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" align="left">&nbsp;</td>
                                                            <td style="width: 3%"></td>
                                                            <td style="width: 22%" align="left">&nbsp;</td>

                                                            <td style="width: 25%" align="left">Temperature</td>
                                                            <td style="width: 3%">:</td>
                                                            <td style="width: 22%" align="left">
                                                                <asp:TextBox ID="txtPyknotemperature" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <br />

                                                <div class="bordered">
                                                    <table class="tabletech">
                                                        <tr>
                                                            <td colspan="8">Parameter Details</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%" align="center">
                                                                <asp:RadioButton ID="parameterpasses" runat="server" GroupName="parametertestradio" onclick="PassTestEvent()" AutoPostBack="true" OnCheckedChanged="parameterpasses_CheckedChanged" />Passes the Test</td>
                                                            <td style="width: 50%" align="center">
                                                                <asp:RadioButton ID="parameterpassesbyvalue" runat="server" GroupName="parametertestradio" OnCheckedChanged="parameterpassesbyvalue_CheckedChanged" AutoPostBack="true" />Passes the Test by value</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 48%">
                                                                <asp:GridView ID="parametergrid1" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                    OnRowDataBound="parametergrid1_RowDataBound" HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="false" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltxt1" runat="server" Visible="true" Text='<%# Eval("txt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="false" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="parameter_checkbox" runat="server" Checked='<%# Eval("tick") %>' />

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                                    <PagerStyle BackColor="#26B8B8" BorderWidth="1px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1"
                                                                        VerticalAlign="Middle" Font-Size="Medium" Font-Bold="False" />
                                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="1px" Height="25px"></RowStyle>
                                                                </asp:GridView>
                                                            </td>
                                                            <td style="width: 48%">
                                                                <asp:GridView ID="parametergrid2" runat="server" AutoGenerateColumns="false"
                                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table" Width="90%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="false" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltxt2" runat="server" Visible="true" Text='<%# Eval("txt") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="false" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="parameter_textbox" runat="server" Text='<%# Eval("input") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                                    <PagerStyle BackColor="#26B8B8" BorderWidth="1px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1"
                                                                        VerticalAlign="Middle" Font-Size="Medium" Font-Bold="False" />
                                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="1px" Height="25px"></RowStyle>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <br />

                                                <div class="bordered">
                                                    <table class="tabletech">
                                                        <tr>
                                                            <td colspan="4">Test Result</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:RadioButton ID="testresultpassed" runat="server" GroupName="testresultradio" /></td>
                                                            <td>Passed</td>
                                                            <td align="left"></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:RadioButton ID="testresultnotpassed" runat="server" GroupName="testresultradio" /></td>
                                                            <td>Not Passed</td>
                                                            <td align="left">Remark</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTestRemarks" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br />
                                                <%--Bhavin--%>
                                                <asp:LinkButton runat="server" CssClass="myButton3" ID="btnCancel" Style="float: right" OnClick="btnCancel_Click" >
                                                        Cancel
                                                </asp:LinkButton>
                                                <%--End--%>
                                                <asp:LinkButton runat="server" CssClass="myButton3" ID="btnSave" Style="float: right" OnClick="btnEdit_Click" OnClientClick="javascript:return validationMsg()">
                                                
                                                        Save
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" CssClass="myButton3" ID="btnVerify" Style="float: right" OnClick="btnEdit_Click" OnClientClick="javascript:return validationMsg()">
                                                        Update
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" CssClass="myButton3" ID="btnReset" Style="float: right" OnClick="btnReset_Click">
                                                        Reset
                                                </asp:LinkButton>

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


