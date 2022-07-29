<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="MoneyForm.aspx.cs" Inherits="UserMgmt.MoneyForm" %>

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
                                <title>Money Form</title>
                                <script>
                                    function onlyDotsAndNumbers(txt, event) {
                                        debugger;
                                        var charCode = (event.which) ? event.which : event.keyCode
                                        if (charCode == 46) {
                                            if (txt.value.indexOf(".") < 0)
                                                return true;
                                            else
                                                return false;
                                        }
                                        if (txt.value.indexOf(".") > 0) {
                                            var txtlen = txt.value.length;
                                            var dotpos = txt.value.indexOf(".");
                                            //Change the number here to allow more decimal points than 2
                                            if ((txtlen - dotpos) > 2)
                                                return false;
                                        }
                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;

                                        return true;
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtAmountSeized.ClientID%>').value == '') {
                                            alert("Enter Amount Seized");
                                            document.getElementById("<% =txtAmountSeized.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                        if ((document.getElementById('<%=txttotalCoins.ClientID%>').value == '') && (document.getElementById('<%=txttotalCurrency.ClientID%>').value == '')){
                                            alert("Enter Currency/Coin List details!");
                                            document.getElementById("<% =txttotalCoins.ClientID%>").focus();
                                            return false;

                                        }

                                       <%--  if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;

                                        }--%>

                                    }
                                </script>

                                    <script language="javascript" type="text/javascript">
                                        function validationMsgAdd() {


                                            if (document.getElementById('<%=ddlAmountSeized.ClientID%>').value == 'Select') {
                                                alert("Select Amount Seized");
                                                document.getElementById("<% =ddlAmountSeized.ClientID%>").focus();
                                                return false;

                                            }
                                            if (document.getElementById('<%=txtCurrency.ClientID%>').value == '') {
                                                alert("Enter Currency");
                                                document.getElementById("<% =txtCurrency.ClientID%>").focus();
                                                return false;

                                            }
                                            if (document.getElementById('<%=txtNoofPieces.ClientID%>').value == '') {
                                                alert("Enter No of Pieces");
                                                document.getElementById("<% =txtNoofPieces.ClientID%>").focus();
                                                return false;

                                            }

                                            if (document.getElementById('<%=txtCoins.ClientID%>').value == '') {
                                                alert("Enter Coin details");
                                                document.getElementById("<% =txtCoins.ClientID%>").focus();
                                                return false;

                                            }
                                        }
                                </script>

                                   <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_txttotalCurrency').val() == "") {
                                            $('#BodyContent_txttotalCurrency').val($('#BodyContent_txtCurTotal').val());
                                        }
                                        if ($('#BodyContent_txttotalCoins').val() == "") {
                                            $('#BodyContent_txttotalCoins').val($('#BodyContent_txtCoiTotal').val());
                                        }
                                    });
                                    </script>

                            </head>
                            <body>

                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Money Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span> Amount Seized </label>
                                        <br />
                                         <asp:DropDownList ID="ddlAmountSeized" runat="server" CssClass="form-control" AutoPostBack="true" data-toggle="tooltip" data-placement="right" title="Amount Seized" OnSelectedIndexChanged="ddlAmountSeized_SelectedIndexChange" >
                                             <asp:ListItem>Select</asp:ListItem>
                                              <asp:ListItem>Rupee Note</asp:ListItem>
                                              <asp:ListItem>Rupee Coin</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    <div runat="server" id="Currency" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label  class="control-label"><span style="color: red">*</span>Rupee Note </label>
                                        <br />
                                        <asp:TextBox ID="txtCurrency" autocomplete="off" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" Width="50%" runat="server" data-toggle="tooltip" data-placement="right" title="Currency"></asp:TextBox>
                                          <asp:HiddenField ID="txtCurTotal" runat="server" />
                                    </div>
                                     <div runat="server" id="Coins" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label  class="control-label"><span style="color: red">*</span>Rupee Coin </label>
                                        <br />
                                        <asp:TextBox ID="txtCoins" autocomplete="off" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" Width="50%" runat="server" data-toggle="tooltip" data-placement="right" title="Coins"></asp:TextBox>
                                           <asp:HiddenField ID="txtCoiTotal" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="lblNoofnotes" runat="server"><span style="color: red">*</span>No of Notes</label>
                                         <label class="control-label" id="lblNoofcoins" runat="server"><span style="color: red">*</span>No of Coins</label>
                                        <br />
                                        <asp:TextBox ID="txtNoofPieces" autocomplete="off" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" runat="server" data-toggle="tooltip" data-placement="right" title="No of Pieces"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="totalid" runat="server" >
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Total Amount Seized </label>
                                        <br />
                                        <asp:TextBox ID="txtAmountSeized" autocomplete="off" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" readonly  runat="server" data-toggle="tooltip" data-placement="right" title="Amount Seized"></asp:TextBox>
                                    </div>
                                      <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label  class="control-label"><span style="color: red"></span>Total Currency </label>
                                        <br />
                                        <asp:TextBox ID="txttotalCurrency" autocomplete="off" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label  class="control-label"><span style="color: red"></span> Total Coins </label>
                                        <br />
                                        <asp:TextBox ID="txttotalCoins" autocomplete="off" CssClass="form-control"   runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="btn btn-primary" ID="LinkButton1" Style="float: left" OnClientClick="javascript:return validationMsgAdd()" OnClick="LinkButton1_Click">ADD</asp:LinkButton></a>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div style="height: 8%; background-color: #26b8b8;">
                                        <span style="font-size: small; color: white; margin-left: 40%">Currency/Coins List</span>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                            <asp:GridView ID="grdMoney" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                                PagerStyle-ForeColor="Black" PagerStyle-Font-Underline="true" EmptyDataText="No Records"
                                                PagerStyle-HorizontalAlign="Center"
                                                class="table table-striped responsive-utilities jambo_table" Style="overflow-y: scroll;"
                                                HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                HeaderStyle-ForeColor="#ECF0F1" HeaderStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Money Type"
                                                        ItemStyle-Font-Bold="true" ItemStyle-Width="100px" HeaderStyle-Font-Underline="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMoneyType" runat="server" Visible="true" Text='<%#Eval("Money Type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                        <ItemStyle Font-Bold="True" Width="100px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currency" ItemStyle-Font-Bold="true"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCurrency" runat="server" Visible="true" Text='<%#Eval("Currency") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                        <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Notes/Coins" ItemStyle-Font-Bold="true"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoofPieces" runat="server" Visible="true" Text='<%#Eval("No of Pieces") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                        <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-Font-Bold="true"
                                                        ItemStyle-Width="90px" HeaderStyle-Font-Underline="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Visible="true" Text='<%#Eval("Total") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Doc No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoc" runat="server" Visible="true" Text='<%#Eval("Doc_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="ImageButton1"   CommandName="Remove" OnClick="ImageButton1_Click" CommandArgument='<%#Eval("Doc_id") %>'  ImageUrl="~/img/delete.gif" runat="server" />&nbsp;
                                                     <%--     <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton> --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                <RowStyle BackColor="Window"></RowStyle>
                                            </asp:GridView>
                                        
                                  
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Remarks" Height="8%" Width="92%" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                    <%--    <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
