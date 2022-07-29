<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OpeningBalanceForm.aspx.cs" Inherits="UserMgmt.OpeningBalanceForm" %>

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
                                <title>Opening Balance</title>
                                 <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_grdOpeningBalanceView_txtSearch2').val();
                                        var jsondata = JSON.stringify($('#BodyContent_grdOpeningBalanceView_txtSearch2').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "OpeningBalanceForm.aspx/chkDuplicateAccessTypeName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    //alert("AccessType  Name is already exists");
                                                    //$('#BodyContent_txtAccessTypeName').val("");
                                                    //$('#BodyContent_txtAccessTypeName').focus();
                                                }

                                            }
                                        });
                                    }
                                    $(document).ready(function () {
                                        $('#BodyContent_grdOpeningBalanceView_ddlsearch1').change(function () {
                                            $('#BodyContent_grdOpeningBalanceView_txtSearch2').val('');
                                        });
                                    });

                                   
                                    </script>
 
                                  <script>
                                    function onlyDotsAndNumbers(txt, event) {
                                      
                                        var charCode = (event.which) ? event.which : event.keyCode
                                        if (charCode == 46)
                                        {
                                            if (txt.value.indexOf(".") < 0)
                                                return true;
                                            else
                                                return false;
                                        }

                                        if (txt.value.indexOf(".") > 0)
                                        {
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
                                        if (document.getElementById('<%= txtRemarks1.ClientID%>').value == '')
                                        {
                                            alert("Enter  Remarks ");
                                            return false;
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%= txtapproverremarks.ClientID%>').value == '')
                                        {
                                            alert("Enter  Approval Comments ");
                                            return false;
                                            document.getElementById("<% =txtapproverremarks.ClientID%>").focus();
                                        }
                                    }
                                    function CheckValues(obj) {
                                        debugger;
                                        //  var id = document.getElementById(obj.id);
                                        var row = obj.parentNode.parentNode;
                                        var n = (row.rowIndex) - 1;
                                        var today = $('#BodyContent_grdOpeningBalanceView_txtOpeningBalance_' + n).val();
                                        var total = $('#BodyContent_grdOpeningBalanceView_txttotalcapacity_' + n).val();
                                       
                                       // $('#BodyContent_vattotal').val(today);
                                        //vattotal
                                      //  $('#BodyContent_val1').val("1");
                                        if (parseFloat(total) < parseFloat(today)) {
                                            alert("Opening cannot be greater than VAT storage capacity");
                                            $('#BodyContent_grdOpeningBalanceView_txtOpeningBalance_' + n).val("");
                                            $('#BodyContent_grdOpeningBalanceView_txtOpeningBalance_' + n).focus();
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                </script>

                            </head>
                            <body>
                                <div runat="server" id="SCM">
                                    <ul class="nav nav-tabs" id="sgr" runat="server">
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                     <ul class="nav nav-tabs" id="dst" runat="server">
                                       <li >
                                            <asp:LinkButton runat="server" ID="lnkRMR" Text="Seizure List" OnClick="lnkRMR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>

                                       <%-- <li>
                                            <asp:LinkButton runat="server" ID="lnkGR" Text="Seizure List" OnClick="lnkGR_Click" >
                                        <span style="color: #fff; font-size: 14px;">Grain Purchase Register</span></asp:LinkButton></li>--%>

                                         <li >
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialToFermenter" OnClick="lnkRawMaterialToFermenter_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter Setup and Distillation</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFermentertoReceiver" OnClick="lnkFermentertoReceiver_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Fermenter to Receiver</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkReceivertoStorage" OnClick="lnkReceivertoStorage_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Receiver to Storage</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="lnkFromStoragetoDispatch" OnClick="lnkFromStoragetoDispatch_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Storage to Dispatch</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDailyDispatchClosure" OnClick="lnkDailyDispatchClosure_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Daily Dispatch Closure</span></asp:LinkButton></li>
                                         <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="lnkRawMaterialWastage" OnClick="lnkRawMaterialWastage_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Wastage</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkForm65" Visible="false" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">Form 65</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkOpeningBalance" OnClick="btnOpeningBalance_Click" Text="Opening Balance">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>

                                    </ul>
                                    <br />
                                </div>
                                  <div runat="server" id="MTP">
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkRMR_Click">
                                        <span style="color: #fff; font-size: 14px;">Raw Material Receipt</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnIssue" OnClick="btnIssue_Click">
                                        <span style="color: #fff; font-size: 14px;">Issue</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnConsumption" OnClick="btnConsumption_Click">
                                        <span style="color: #fff; font-size: 14px;">Consumption</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="lnkOB" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                     <asp:LinkButton runat="server" CssClass="myButton3" ID="btnShowRecords" OnClick="ShowRecords_Click" Style="float: right" Text="ShowRecords"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="x_title">
                                        <h2>Opening Balance Entry Form</h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="x_content">
                                      
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <asp:GridView ID="grdOpeningBalanceView" OnDataBound="grdOpeningBalanceView_DataBound" runat="server"  AutoGenerateColumns="false" EmptyDataText="No Records" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdOpeningBalanceView_PageIndexChanging"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                    <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" 
                                                             Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" 
                                                             Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" 
                                                             PreviousPageImageUrl="~/img/icons8-previous-50.png" />

                                                        <PagerTemplate>
                                                              <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                               <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Storage/Content" Value="storage_content"></asp:ListItem>
                                                                <asp:ListItem Text="Vat Type" Value="vat_type_name"></asp:ListItem>
                                                                  <asp:ListItem Text="Vat Name" Value="vat_name"></asp:ListItem>
                                                                           <%--<asp:ListItem Text="To Vat Name" Value="to_vatname"></asp:ListItem>--%>
                                                                  <asp:ListItem Text="UOM" Value="uom_name"></asp:ListItem>
                                                                 
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black" onchange="chkDuplicateAccessTypeName();" ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></span> 
      <span><asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span> 
     


                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page"  CommandArgument="First" CssClass="myButton1"><i class="fa fa-step-backward"> </i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="myButton1"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                <asp:ImageButton ID="btnFirst" runat="server" Width="30px" Height="20px" CommandArgument="First" Visible="false" CommandName="Page"  BackColor="Blue" ForeColor="White"
                    ImageUrl="~/img/icons8-first-50.png" /> <asp:ImageButton ID="btnPrev" runat="server" Visible="false"
                        CommandArgument="Prev" CommandName="Page" Width="30px" Height="20px" BackColor="Blue" ImageUrl="~/img/icons8-previous-50.png" /> <asp:DropDownList
                            ID="DDLPage" runat="server" AutoPostBack="True"  Visible="false"  Width="250px" ForeColor="Black" Font-Bold="true">
                        </asp:DropDownList>&nbsp;&nbsp; <asp:TextBox ID="txtpage" runat="server" Height="20px" AutoPostBack="true" TextMode="Number" ForeColor="Black" Width="50px" Font-Bold="true" OnTextChanged="txtpage_TextChanged"></asp:TextBox> <asp:Label ID="lblCurrent" Visible="false" runat="server"></asp:Label>
                of
              <asp:Label ID="lblPages" runat="server" Height="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page"  CommandArgument="Next" CssClass="myButton1"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page"  CommandArgument="Last" CssClass="myButton1"><i class="fa fa-step-forward"> </i></asp:LinkButton>
                                                            
                <asp:ImageButton ID="btnNext" Visible="false"
                    runat="server" CommandArgument="Next" Width="30px" Height="20px" CommandName="Page" ForeColor="Blue" BackColor="Blue" ImageUrl="~/img/icons8-next-50.png"  /> <asp:ImageButton
                        ID="btnLast" runat="server" CommandArgument="Last" Width="30px" Visible="false" Height="20px" BackColor="Blue" CommandName="Page" ImageUrl="~/img/icons8-last-50.png" />
            </PagerTemplate>
                                                        <Columns>
                                                          <asp:TemplateField HeaderText="Storage/Content"  ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstoragecontent" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("storage_content") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblid" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("openingbalance_id") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vat Type Code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVatTypeCode" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("vat_type_code") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Vat Type" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVatType" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("vat_type_name") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Vat Code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVatCode" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("vat_code") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Vat Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVatName" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("vat_name") %>'  ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             
                                                             <asp:TemplateField HeaderText="Total Capacity" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                    <ItemTemplate>
                                                      
                                                        <asp:TextBox ID="txttotalcapacity" CssClass="form-control" runat="server" Text='<%#Eval("Total_Capacity") %>' ReadOnly="true">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                            
                                                              <asp:TemplateField HeaderText="Opening Balance" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                   <%-- <asp:Label ID="lblOpeningBalance" runat="server" Visible="true" Text='<%#Eval("Opening Balance") %>' ></asp:Label>--%>
                                                                    <asp:TextBox ID="txtOpeningBalance" runat="server"  Text='<%# Eval("openingbalancevalue") %>' onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtOpeningBalance_TextChanged" ForeColor="Black" CssClass="form-control" ></asp:TextBox>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="UOMCode" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOMCode" runat="server" ForeColor="Black" Visible="false" Text='<%#Eval("uom_code") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="20px" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM" runat="server" ForeColor="Black" Visible="true" Text='<%#Eval("uom_name") %>' ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="status"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Draft") %>'></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                   </Columns>
                                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"/>

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>

                                          
                                                             </asp:GridView>
                                         

                               
                                <div class="clearfix"></div>
                                         
                                             <div id="remark" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <textarea type="text" id="txtRemarks1" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                         <div class="clearfix"></div>
                                         
                                             <div id="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red"></span>Approver Comments</label><br />
                                        <textarea type="text" id="txtapproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                    </div>
                                         <div class="clearfix"></div>
                                         <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                              
                                               
                                                  <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> </span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSave_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                                <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                    </div>
                                        <div id="approverid" runat="server">
                                           <p>&nbsp;</p>

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
        </div></div>


</asp:Content>
