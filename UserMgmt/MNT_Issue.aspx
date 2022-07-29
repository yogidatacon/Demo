<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MNT_Issue.aspx.cs" Inherits="UserMgmt.MNT_Issue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

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
                                <title>ISSUE OF ENA / SDS</title>
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
                                <script type="text/javascript" language="javascript">
                                    function subjectChanged() {
                                        debugger;;
                                        var b = 0;
                                        b = document.getElementById('<%=txtStrength.ClientID%>').value;
            var vSubmitButton = document.getElementById('<%=txtIssueQtyBL.ClientID%>').value;
                                        if (vSubmitButton == "") {
                                            vSubmitButton = 0;
                                        }

                                        if (b == "") {
                                             b= 0;
                                        }

                                        else if (parseFloat(vSubmitButton) > 0) {
                                            var a = (parseFloat(vSubmitButton) * ((100 + parseFloat(b)) / 100));
                                            document.getElementById('<%=txtIssueQtyLPL.ClientID %>').value = a.toFixed(2);;

                                        }
        };
    </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtApplicationRequestNo.ClientID%>').value == '') {
                                            alert("Enter Application Request No");
                                            $('#BodyContent_txtApplicationRequestNo').val("");
                                            document.getElementById("<% =txtApplicationRequestNo.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtIssueDate.ClientID%>').value == '') {
                                            alert("Enter Issue Date");
                                            $('#BodyContent_txtIssueDate').val("");
                                            document.getElementById("<% =txtIssueDate.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=ddlProduct.ClientID%>').value == 'Select') {
                                            alert("Select Product");
                                            document.getElementById("<% =ddlProduct.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            $('#BodyContent_txtRemarks').val("");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                        }
                                        
                                    }

                                    function validationMsg1() {

                                        if (document.getElementById('<%=txtBORemarks.ClientID%>').value == '') {
                                            alert("Enter Approver Remark");
                                            document.getElementById("<% =txtBORemarks.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsgAdd() {
                                        debugger;;
                                        if (document.getElementById('<%=ddlStorageVAT.ClientID%>').value == 'Select') {
                                            alert("Select Storage Vat");
                                            
                                            document.getElementById("<% =ddlStorageVAT.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlIssuedVat.ClientID%>').value == 'Select') {
                                            alert("Select Issue Vat");
                                            
                                            document.getElementById("<% =ddlIssuedVat.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtIssueQtyBL.ClientID%>').value == '') {
                                            alert("Enter Issue Qty(BL)");
                                            $('#BodyContent_txtIssueQtyBL').val("");
                                            document.getElementById("<% =txtIssueQtyBL.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=txtIssueQtyBL.ClientID%>').value <= 0) {
                                              alert("Zero or Less Than Zero Issue Qty(BL) not allowed");
                                            $('#BodyContent_txtIssueQtyBL').val("");
                                            document.getElementById("<% =txtIssueQtyBL.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtIssueQtyLPL.ClientID%>').value == '') {
                                            alert("Enter Issue Qty(LPL)");
                                            $('#BodyContent_txtIssueQtyLPL').val("");
                                            document.getElementById("<% =txtIssueQtyLPL.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtIssueQtyLPL.ClientID%>').value <=0) {
                                             alert("Zero or Less Than Zero Issue Qty(LPL) Not Allowed ");
                                            $('#BodyContent_txtIssueQtyLPL').val("");
                                            document.getElementById("<% =txtIssueQtyLPL.ClientID%>").focus();
                                            return false;
                                        }
                                        if (parseInt(document.getElementById('<%=txtIssueQtyBL.ClientID%>').value) > parseInt(document.getElementById('<%=txtAvailableQtyBL.ClientID%>').value)) {
                                            alert("Issue Qty (BL) cannot be greater than Available Qty (BL)");
                                            $('#BodyContent_txtIssueQtyBL').val("");
                                            $('#BodyContent_txtIssueQtyLPL').val("");
                                            document.getElementById("<% =txtIssueQtyBL.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtStrength.ClientID%>').value == '') {
                                            alert("Enter Strength");
                                            $('#BodyContent_txtStrength').val("");
                                            document.getElementById("<% =txtStrength.ClientID%>").focus();
                                            return false;
                                        }
                                        

                                                                                
                                        
                                    }
                                </script>
                            </head>
                            <body>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" OnClick="ShowRecords_Click" ID="ShowRecords" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>ISSUE OF ENA / SDS</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Application Request No</label><br />
                                        <asp:TextBox ID="txtApplicationRequestNo" Visible="true" class="form-control" Height="30px" Width="250px"  
                                            AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Application Request No" runat="server" MaxLength="25"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Issue Date</label><br />

                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtIssueDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtIssueDate"  data-toggle="tooltip" data-placement="right" ReadOnly="true"
                                            title="Date of Issue " class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob" runat="server" />
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label style="width: 60%"><span style="color: red">*</span> Product</label><br />

                                                <asp:DropDownList ID="ddlProduct" AutoPostBack="true" ToolTip="Product" class="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>


                                    <div class="x_title">

                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                    </div>
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Storage VAT</label><br />

                                        <asp:DropDownList ID="ddlStorageVAT" AutoPostBack="true" OnSelectedIndexChanged="ddlStorageVAT_SelectedIndexChanged" 
                                            class="form-control" runat="server" ToolTip="Storage VAT"></asp:DropDownList>
                                    </div>
                                            
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Available Qty (BL) </label>
                                        <br />

                                        <asp:TextBox ID="txtAvailableQtyBL" disabled Visible="true" AutoPostBack="true"
                                             class="form-control" Height="30px" Width="250px" AutoComplete="off" title="Available Qty (BL)"
                                            data-toggle="tooltip" data-placement="right"   runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                            </ContentTemplate>
                                         <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlStorageVAT" EventName="SelectedIndexChanged" />
    </Triggers>
                                         </asp:UpdatePanel>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="diav00" runat="server" visible="false">
                                        <label style="width: 60%"><span style="color: red">*</span>Available Qty (LPL) </label>
                                        <br />

                                        <asp:TextBox ID="txtAvailableQtyLPL" disabled Visible="false" AutoPostBack="true" class="form-control" 
                                            Height="30px" Width="250px" AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Available Qty (LPL)" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Issued VAT</label><br />

                                        <asp:DropDownList ID="ddlIssuedVat" AutoPostBack="true" class="form-control" ToolTip="Issued VAT" runat="server"></asp:DropDownList>
                                    </div>
                                            </ContentTemplate>
                                         </asp:UpdatePanel>
                                    
                                    


                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Strength </label>
                                        <br />

                                        <asp:TextBox ID="txtStrength" Visible="true" class="form-control" 
                                            onkeypress="return onlyDotsAndNumbers(this,event);" Height="30px" Width="250px"  onChange="subjectChanged();"
                                            AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Strength" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Issue Qty (BL) </label>
                                        <br />

                                        <asp:TextBox ID="txtIssueQtyBL" Visible="true" class="form-control" 
                                            onkeypress="return onlyDotsAndNumbers(this,event);" 
                                            Height="30px" Width="250px" AutoComplete="off" data-toggle="tooltip" 
                                            data-placement="right" onChange="subjectChanged();" title="Issue Qty (BL)" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label style="width: 60%"><span style="color: red">*</span>Issue Qty (LPL) </label>
                                        <br />

                                        <asp:TextBox ID="txtIssueQtyLPL" Visible="true" 
                                            onkeypress="return onlyDotsAndNumbers(this,event);" 
                                            class="form-control" Height="30px" Width="250px"  
                                            AutoComplete="off" data-toggle="tooltip" data-placement="right" title="Issue Qty (LPL)" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <br />
                                        <asp:Button ID="btnAdd" runat="server" Text="ADD"  OnClick="btnAdd_Click" class="btn btn-primary"  ToolTip="Add"
                                            OnClientClick="javascript:return validationMsgAdd();"/>

                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="dummytable" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                            <thead>
                                                <tr>
                                                    <th>Storage VAT</th>
                                                    <th>Issued VAT</th>
                                                    <th>Strength</th>
                                                    <th>Issue Qty (BL)</th>
                                                    <th>Issue Qty (LPL)</th>
                                                    
                                                </tr>
                                            </thead>
                                            <tbody id="resourcetable">
                                            </tbody>

                                        </table>
                                    </div>
                                    <div class="col-md-10 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Storage VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmedicine_name" runat="server" Visible="true" Text='<%#Eval("storage_vat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Vat" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblissue_vat" runat="server" Visible="true" Text='<%#Eval("issue_vat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Strength" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstrength" runat="server" Visible="true" Text='<%#Eval("strength") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Qty (BL)" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbatch_no" runat="server" Visible="true" Text='<%#Eval("issued_qty_bl") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Qty (LPL)" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblconsumption_qty" DataFormatString="{0:0.00}" runat="server" Visible="true" Text='<%#Eval("issued_qty_lpl") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="ImageButton1"   CommandName="Remove" OnClick="ImageButton1_Click" CommandArgument='<%#Eval("storage_vat") %>'  ImageUrl="~/img/delete.gif" runat="server" />&nbsp;
                                                     <%--     <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton> --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                


                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 ">
                                        <label style="display: inline; font-size: small"><span style="color: red">*</span>Remarks</label>
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="250" runat="server" Width="90%" data-toggle="tooltip" 
                                            data-placement="right" title="Remarks" onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 ">
                                        <label style="display: inline; font-size: small" runat="server" id="lblApproverRemarks" visible="false"><span style="color: red"></span>Approver Remarks</label>
                                        <asp:TextBox ID="txtBORemarks" CssClass="form-control" Visible="false" MaxLength="250" 
                                            runat="server" Width="90%" data-toggle="tooltip" data-placement="right" title="BO Remarks" onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>

                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save as Draft" OnClick="btnSave_Click" ToolTip="Save as Draft"  class="btn btn-info" OnClientClick="javascript:return validationMsg();" />
                                        <asp:Button ID="btnSumbit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ToolTip="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg();" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancel_Click" class="btn btn-danger" />
                                    <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" ToolTip="Approve" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" ToolTip="Reject" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="approverid" runat="server">
                                           
                                        <div class="x_title">
                                                
                                                <h4>Approval Summary</h4>
                                                <div class="clearfix"></div>
                                            </div>
                                        <div class="x_title">
                                        <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                    HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                    HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" Width="1218px">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Transaction_Date") %>'></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approver Role" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Documents_id") %>' ForeColor="Black" runat="server" OnClick="DeleteFiles" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
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
