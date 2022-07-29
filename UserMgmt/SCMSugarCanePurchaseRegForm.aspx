<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SCMSugarCanePurchaseRegForm.aspx.cs" Inherits="UserMgmt.SCMSugarCanePurchaseRegForm" %>
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
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title>SCM</title>
                                 <script language="javascript" type="text/javascript">
                                     function validationMsg1() {

                                         if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                             alert("Enter Approver Remarks");
                                             document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                             return false;

                                         }
                                     }
                                        </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddlPartyName.ClientID%>').value == 'Select') {
                                            alert("Select Party Name");
                                            document.getElementById("<% =ddlPartyName.ClientID%>").focus();
                                            return false;

                                        }
                                      <%--  if (document.getElementById('<%=ddlFinancialYear.ClientID%>').value == 'Select') {
                                            alert("Select Financial Year");
                                            document.getElementById("<% =ddlFinancialYear.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        if (document.getElementById('<%=txtDate.ClientID%>').value == '') {
                                            alert("Select Date");
                                            document.getElementById("<% =txtDate.ClientID%>").focus();
                                            return false;

                                        }
                                        <%--if (document.getElementById('<%=textpfg.ClientID%>').value == '') {
                                            alert("Enter Purchased at Factory Gate");
                                            document.getElementById("<% =textpfg.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=textpfs.ClientID%>').value == '') {
                                            alert("Enter Purchased From Out Station");
                                            document.getElementById("<% =textpfs.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=textFOE.ClientID%>').value == '') {
                                            alert("Enter From Own Estate");
                                            document.getElementById("<% =textFOE.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        
                                        <%--if (parseFloat(document.getElementById('<%=textTotal.ClientID%>').value)<1) {
                                            debugger;
                                            alert("Cane Crushed Zero Values are Not Allowed");
                                            document.getElementById("<% =textcanecrushed.ClientID%>").focus();
                                            return false;

                                        }--%>
                                       <%-- var crushed = $('#BodyContent_textcanecrushed').val();
                                       
                                            if (document.getElementById('<%=textcanecrushed.ClientID%>').value == '' || parseFloat(crushed)<=0) {
                                            alert("Enter Cane Crushed");
                                            document.getElementById("<% =textcanecrushed.ClientID%>").focus();
                                            return false;

                                        }--%>
                                        //CheckWithTotal();
                                         if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                            return false;

                                        }

                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function CheckDiscription() {
                                        debugger;
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                            alert("Please Attach file");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                            alert("Enter Document Description");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
                                <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_textTotal').val() == "") {
                                            $('#BodyContent_textTotal').val($('#BodyContent_txtTotal').val());
                                        }
                                        if ($('#BodyContent_txtDate').val() == "") {
                                            $('#BodyContent_txtDate').val($('#BodyContent_txtdob').val());
                                        }
                                        if ($('#BodyContent_ddlPartyName').val() == "Select" ||$('#BodyContent_ddlPartyName').val() == "") {
                                            $('#BodyContent_ddlPartyName').val($('#BodyContent_txtdob').val());
                                        }
                                        GetTotal();
                                    });
                                    </script>
                                <script type="text/javascript">

                                    function validateExtraDocuments() {
                                       
                                        var fileInput = document.getElementById('<%= idupDocument.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }

                                        var uploadControl = document.getElementById('<%= idupDocument.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idupDocument.ClientID %>').value = "";

                                            return false;
                                        }
                                        else {
                                            return true;
                                        }

                                    }
                                    function GetTotal() {
                                       
                                        var pfg = $('#BodyContent_textpfg').val();
                                        var pfs = $('#BodyContent_textpfs').val();
                                        var foe = $('#BodyContent_textFOE').val();
                                        var total = $('#BodyContent_textTotal').val();
                                        if (pfg == "")
                                            pfg = 0;
                                        if (pfs == "")
                                            pfs = 0;
                                        if (foe == "")
                                            foe = 0;
                                        if (total == "")
                                            total = 0;
                                        var result = parseFloat(parseFloat(pfg) + parseFloat(pfs) + parseFloat(foe)).toFixed(2);;
                                        //  $('#texttotal').val(result);
                                        $('#BodyContent_textTotal').val(result);
                                        $('#BodyContent_txtTotal').val(result);
                                        
                                        var total2 = $('#BodyContent_txtpending1').val();
                                        var t = parseFloat(result) + parseFloat(total2);
                                        $('#BodyContent_txtPending').val(t.toFixed(2));
                                    }
                                    function CheckWithTotal() {
                                        debugger;
                                       
                                        var total1 = $('#BodyContent_txtPending').val();
                                        if ($('#BodyContent_canecrushed').val() == "")
                                            $('#BodyContent_canecrushed').val('0');
                                        var total2 = $('#BodyContent_canecrushed').val();
                                        var total = parseFloat(total1) + parseFloat(total2);
                                        if ($('#BodyContent_textcanecrushed').val() == "")
                                            $('#BodyContent_textcanecrushed').val('0');
                                        var crushed = $('#BodyContent_textcanecrushed').val();
                                        if (crushed == "")
                                            crushed = 0;
                                        if (parseFloat(total) <parseFloat(crushed))
                                        {
                                            alert("Cane Crushed Total should be less than or eqaul to Total!!!")
                                            $('#BodyContent_textcanecrushed').val("");
                                            $('#BodyContent_textcanecrushed').focus();
                                            return false;
                                        }
                                        else
                                        {
                                           
                                        }

                                    }
                                </script>
                                
                                <script>
                                    function chkDuplicateDates() {
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
                                        $('#BodyContent_txtDate').val(todayDate);
                                        $('#BodyContent_txtdob').val(todayDate);
                                         if (document.getElementById('<%=ddlPartyName.ClientID%>').value == 'Select') {
                                            alert("Please Select Party Name");
                                            document.getElementById('<%=ddlPartyName.ClientID%>').focus();
                                            $('#BodyContent_txtDate').val('');
                                            return false;
                                        }
                                        else {
                                            var etrydate = $('#BodyContent_txtDate').val();
                                            $('#BodyContent_txtdob').val(etrydate);
                                            var party_code = $('#BodyContent_ddlPartyName').val();
                                            var jsondata = JSON.stringify($('#BodyContent_txtDate').val() + "_" + $('#BodyContent_ddlPartyName').val());
                                            $.ajax({
                                                type: "POST",
                                                //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                                url: "SCMSugarCanePurchaseRegForm.aspx/chkDuplicateDates",
                                                data: '{scpdate:' + jsondata + '}',
                                                datatype: "application/json",
                                                contentType: "application/json; charset=utf-8",
                                                cache: false,
                                                async: false,
                                                success: function (msg) {
                                                    if (parseInt(msg.d) > 0) {
                                                        debugger;
                                                        alert("Purchase Date is already exists");
                                                        $('#BodyContent_txtDate').val('');
                                                        $('#BodyContent_txtdob').val('');
                                                       
                                                        $('#BodyContent_txtDate').focus();
                                                        return false;
                                                    }

                                                }
                                            });
                                        }
                                    }


                                    function onlyDotsAndNumbers(txt, event) {

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
                            </head>
                            <body>

                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="btnShowRecords" OnClick="btnShowRecords_Click" Style="float: right" Text="Seizure List"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Form R.G.4</h2>
                                    <div class="clearfix"></div>
                                </div>
                               
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="dd" runat="server" />
                                            <asp:HiddenField ID="txtpending1" runat="server" />
                                            <asp:HiddenField ID="rtype" runat="server" />
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Party Name</label><br />
                                                <asp:DropDownList ID="ddlPartyName" CssClass="form-control" Width="80%" runat="server" data-toggle="tooltip" data-placement="right" title="Party Name"
                                                   
                                                    class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                          
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Date</label><br />
                                               <cc1:CalendarExtender runat="server" PopupButtonID="Image1"  TargetControlID="txtDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                             
                                                 <asp:TextBox ID="txtDate" onchange="chkDuplicateDates()"  data-toggle="tooltip" data-placement="right" title="Sugar Cane purchase/crush  date" class="form-control"  AutoComplete="off" runat="server" >
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" runat="server" />
                                               <%-- <input type="date" id="txtDate"  runat="server" class="form-control" />--%>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Purchased at Factory Gate</label><br />
                                                <asp:TextBox ID="textpfg" runat="server" onchange="GetTotal();" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Purchased at Factory Gate"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Out Station Purchase </label><br />
                                                <asp:TextBox ID="textpfs" runat="server" onchange="GetTotal();" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Out Station Purchase "></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>From Own Estate</label><br />
                                                <asp:TextBox ID="textFOE" runat="server" onchange="GetTotal();" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="From Own Estate"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Today's cane purchased Total</label><br />
                                                 <asp:TextBox ID="textTotal" ReadOnly="true" ContentEditable="false" runat="server"  CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Today's cane purchased Total   "></asp:TextBox>
                                               
                                            </div>
                                            <asp:HiddenField ID="txtTotal" runat="server" />
                                            <div class="clearfix"></div>
                                            
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                 <br />
                                                <asp:HiddenField ID="canecrushed" runat="server" />
                                                <label class="control-label"><span style="color: red"></span>Cane Crushed</label><br />
                                                <asp:TextBox ID="textcanecrushed" onchange="CheckWithTotal();" runat="server" CssClass="form-control" data-toggle="tooltip" AutoComplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Cane Crushed"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                 <br />
                                                <label class="control-label" style="display: inline;width:100%"><span style="color: red"></span>Current Available Cane Quantity</label><br />
                                                <asp:TextBox ID="txtPending" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Total Cane purchase Stock-Crushed"></asp:TextBox>
                                            </div> 
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <p>&nbsp;</p> 
                                    <p>&nbsp;</p>
                                     <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                    <div id="docs" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                        <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" class="control-label" style="display: inline"><span style="color: red"></span>Name of the Document</label><br />
                                        <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Name of the Document"></asp:TextBox>
                                        <span>
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile"/>
                                        </span>
                                    </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                            <thead>
                                                <tr>
                                                    <th>File Name</th>
                                                    <th>Description</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="resourcetable">
                                            </tbody>

                                        </table>
                                    </div>
                                    <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" 
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="File Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" runat="server" Visible="true" Text='<%#Eval("Doc_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscriptione" runat="server" Visible="true" Text='<%#Eval("Discription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FilePath" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFilePath" runat="server" Visible="true" Text='<%#Eval("Doc_path") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoc_id" runat="server" Visible="true" Text='<%#Eval("doc_id") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" OnClick="DownloadFile" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" OnClick="btnRemove_Click" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                           <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>

                                           
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <asp:TextBox id="txtRemarks1" TextMode="MultiLine" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <asp:TextBox id="txtApproverremarks" TextMode="MultiLine" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg();" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                               <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                    </div>
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
                            </body>
                            </html>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

