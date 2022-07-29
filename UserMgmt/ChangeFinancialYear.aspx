<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ChangeFinancialYear.aspx.cs" Inherits="UserMgmt.ChangeFinancialYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                                <title>Receiver to Storage</title>
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <script  type="text/javascript">
                                    
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlpartyname.ClientID%>').value == 'Select') {
                                            alert("Select Party Name");
                                             document.getElementById("<% =ddlpartyname.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         if (document.getElementById('<%=txtcurrentfinancial.ClientID%>').value == '') {
                                             alert("Select Current Financial Year");
                                             document.getElementById("<% =txtcurrentfinancial.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                    }
                                </script>
                                 <script>
                               
                                    </script> 
                                <script type="text/javascript">
                                
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
                                    function GetValueINLPL1() {
                                        debugger;
                                       
                                    }
                                    
                                    function SelectDate(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtTransferDate').val(todayDate);
                                        $('#BodyContent_txtrdate').val(todayDate);
                                    }
                                 
                                </script>
                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                       
                                        if ($('#BodyContent_txtTransferDate').val() == "") {
                                            $('#BodyContent_txtTransferDate').val($('#BodyContent_txttrdate').val());
                                        }
                                    });
                                    </script>

                            </head>
                            <body>
                                <div>

                                   <%-- <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt </span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" Visible="false">
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;"> Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage & Adjustment</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" Text="Opening Balance" OnClick="lnkOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />--%>
                                </div>
                                
                                <a>
                                    <asp:HiddenField ID="trqty" runat="server" />
                                    <asp:HiddenField ID="rtype" runat="server" />
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="btnCancel_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Change Financial Year</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" style="font-size: small;color:black; display: inline"><span style="color: red">*</span>Party Name</label><br />
                                         <asp:DropDownList ID="ddlpartyname"  runat="server" CssClass="form-control" ForeColor="Black" Width="70%" data-toggle="tooltip" AutoComplete="off" data-placement="right" title="Party Name" AutoPostBack="true" ></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                 <label class="control-label" style="font-size: small;color:black; display: inline"><span style="color: red"></span>New Financial Year </label><br />
                                        <asp:TextBox ID="txtcurrentfinancial"  CssClass="form-control" runat="server" ForeColor="Black" data-toggle="tooltip" data-placement="right" AutoComplete="off" title="Current Financial Year" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                
                                         
                                             <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-9 col-sm-12 col-xs-12 form-inline" style="margin-top: 15px;">
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <asp:HiddenField ID="party_code" runat="server" />
                                               <asp:HiddenField ID="id" runat="server" />
                                             <asp:HiddenField ID="enddate" runat="server" />
                                              <asp:HiddenField ID="startdate" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" runat="server"
                                               CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg();" OnClick="btnSaveasDraft_Click" >
                                                       Submit</asp:LinkButton>
                                          <%--  <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" >
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" OnClick="btnApprove_Click" runat="server" Style="width: 85px;" Text="Approve"   />
                                            <asp:LinkButton ID="btnReject" Text="Reject" Visible="false" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" OnClick="btnReject_Click" class="fa fa-cut" />
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"  CssClass="btn btn-danger" >Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                            <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    </div>
                                       <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                           <div id="divImage" style="display:none">
                    <%-- <asp:Image ID="img1" runat="server" ImageUrl="photos/Spinner-2.gif"/><span>--%> <%--<label class="control-label" runat="server" id="Label1" style="display: inline ;color:black"> </label><br />--%><%--</span>--%>
               <%--    
                </div>               
                                        <label class="control-label" runat="server" id="done" style="display: inline;color:black"></label><br />
                                          
                                    </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                               <%--  <%--   <div id="approverid" runat="server">
                                        <p>&nbsp;</p>
                                        <div class="x_title">
                                            <h4>Approval Summary</h4>
                                            <div class="clearfix"></div>
                                        </div>-
                                        <div class="x_content">
                                            <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" Width="1218px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" runat="server" Font-Bold="true" Text='<%# Eval("ID") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Table Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApproverRole" runat="server" Font-Bold="true" Text='<%# Eval("Table") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApproverComments" runat="server" Font-Bold="true" Text='<%# Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" Text='<%# Eval("User") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                  <%--  </div>
                                    --%>
                                     <div id="approverid" runat="server">
                                     <p>&nbsp;</p>
                                            <div class="x_title">
                                                <h4>Approval Summary</h4>
                                                <div class="clearfix"></div>
                                            </div>
                                      <%--  <div class="x_title">  </div>--%>
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
                            </body>
                            </html>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
