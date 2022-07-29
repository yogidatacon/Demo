<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MTPReleaseRequestForm.aspx.cs" Inherits="UserMgmt.MTPReleaseRequestForm" %>
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
                                <title>Release Request of Molasses</title>
                                <script>
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
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtNewRequestedQty.ClientID%>').value == '') {
                                            alert("Enter NewRequested Qty");
                                            document.getElementById("<% =txtNewRequestedQty.ClientID%>").focus();
                                            return false;

                                        }
                                        var qty =parseFloat($('#BodyContent_txtNewRequestedQty').val());
                                        if(qty<=0)
                                        {
                                            alert("NewRequested Qty Zero Not Allowed");
                                            $('#BodyContent_txtNewRequestedQty').val("");
                                            document.getElementById("<% =txtNewRequestedQty.ClientID%>").focus();
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
                                            alert("Enter Description");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                    }
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
                                    function CheckQTY() {
                                        if ($('#BodyContent_txtNewRequestedQty').val() == "") {
                                            alert("Enter Request Qty!!!")
                                           
                                            $('#BodyContent_txtNewRequestedQty').focus();
                                        }
                                        else {
                                            if ($('#BodyContent_txtBalanceQTY').val() == "0")
                                                $('#BodyContent_txtBalanceQTY').val(parseFloat($('#BodyContent_txtAllotmentQty').val()))
                                            var balance = parseFloat($('#BodyContent_txtBalanceQTY').val());
                                            var alloted = parseFloat($('#BodyContent_txtAllotmentQty').val());
                                            var RRqty = parseFloat($('#BodyContent_txtReleaseRequestQTY').val());
                                         
                                            var qty = parseFloat($('#BodyContent_txtNewRequestedQty').val());
                                            var total = parseFloat(RRqty) + parseFloat(qty);
                                            if (qty > balance) {
                                                debugger;
                                                alert("Request Qty should be less than or eqaul to Balance Qty!!!")
                                                $('#BodyContent_txtNewRequestedQty').val('');
                                                $('#BodyContent_txtNewRequestedQty').focus();
                                                return false;
                                            }
                                            if (total > alloted) {
                                                debugger;
                                                alert("Requested Qty and new Request Qty should be less than or eqaul to Balance Qty!!!")
                                                $('#BodyContent_txtNewRequestedQty').val('');
                                                $('#BodyContent_txtNewRequestedQty').focus();
                                                return false;
                                            }
                                        }
                                    }
                                    function SelectValiedDate(e) {

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
                                        $('#BodyContent_txtValiedDate').val(todayDate);
                                        $('#BodyContent_txtvalieddate1').val(todayDate);
                                    }
                                    function validationMsg1() {

                                        if (document.getElementById('<%=txtValiedDate.ClientID%>').value == '') {
                                            alert("Select Valid Date");
                                            document.getElementById("<% =txtValiedDate.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver Remarks");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;
                                        }
                                        //if (GetConformation() === false) {
                                        //    alert("Please Check Digilock Password");
                                        //    return false;
                                        //}
                                    }
                                     function validationMsg12() {

                                       
                                        if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                            alert("Enter Approver Remarks");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;
                                        }
                                        //if (GetConformation() === false) {
                                        //    alert("Please Check Digilock Password");
                                        //    return false;
                                        //}
                                    }
                                </script>
                                
                            </head>
                            <body>
<%--                                  <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnReleaseRequestMolasses"  runat="server" OnClick="btnReleaseRequestMolasses_Click"><span style="color:#fff;font-size:14px;">Release Request of Molasses</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="btnIssuedReleaseRequestLetter" runat="server" OnClick="btnIssuedReleaseRequestLetter_Click"><span style="color:#fff;font-size:14px;">Release Request Applied List</span></asp:LinkButton></li>
                                          <li >
                                            <asp:LinkButton ID="PassRequest" runat="server" OnClick="PassRequest_Click"><span style="color:#fff;font-size:14px;">Request For Pass List</span></asp:LinkButton></li>
                                    </ul>
                                      </div>--%>
                                 <div runat="server" id="MTB">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnIssuedReleaseRequestLetter"  runat="server" OnClick="btnReleaseRequestMolasses_Click"><span style="color:#fff;font-size:14px;">Release Request of Molasses</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnReleaseRequestMolasses" runat="server" OnClick="btnIssuedReleaseRequestLetter_Click"><span style="color:#fff;font-size:14px;">Release Request Applied List</span></asp:LinkButton></li>
                                          <%--<li >
                                            <asp:LinkButton ID="PassRequest" runat="server" OnClick="PassRequest_Click"><span style="color:#fff;font-size:14px;">Request For Pass List</span></asp:LinkButton></li>--%>
                                    </ul>
                                      </div>
                                <div runat="server" id="ETB">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="btnReleaseRequestMolasses_Click"><span style="color:#fff;font-size:14px;">Release Request of ENA/Spirits</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnIssuedReleaseRequestLetter_Click"><span style="color:#fff;font-size:14px;">Release Request Applied List</span></asp:LinkButton></li>
                                          <%--<li >
                                            <asp:LinkButton ID="PassRequest" runat="server" OnClick="PassRequest_Click"><span style="color:#fff;font-size:14px;">Request For Pass List</span></asp:LinkButton></li>--%>
                                    </ul>
                                      </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <%--<div class="x_title">
                                    <h2>Release Request of Molasses</h2>
                                    <div class="clearfix"></div>
                                </div>--%>
                                <div runat="server" id="molasses" class="x_title">
                                    <h2>Molasses Release Request</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div runat="server" id="ENA"  class="x_title">
                                    <h2>ENA/Spirits  Release Request</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:HiddenField ID="rrid" runat="server" />
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                          <asp:HiddenField ID="RRRequestid" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Release Request No</label>
                                        <br />
                                        <asp:TextBox ID="txtReleaseRequestNo" Width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right"  ReadOnly="true" title="Release Request No" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Release Request Date</label>
                                        <br />
                                        <asp:TextBox ID="txtReleaseRequestDate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Release Request Date" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Financial_year</label>
                                        <br />
                                        <asp:TextBox ID="txtFiscalYear" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Financial_year"  ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <span style="color: red"></span> <asp:Label ID="lblDepot" runat="server" Font-Bold="true" CssClass="control-label"></asp:Label>
                                        <br />
                                         <asp:HiddenField ID="releaserequestid" runat="server" />
                                        <asp:TextBox ID="txtMolassesFinalAllotmentNo" Width="70%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Molasses Final Allotment No" ></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Allotment Valid Upto</label>
                                        <br />
                                        <asp:TextBox ID="txtAllotmentValidUpto" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Allotment Valid Upto" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Unit Name</label>
                                        <br />
                                        <asp:TextBox ID="txtUnitName" CssClass="form-control" runat="server" Width="50%" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Unit Name" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Product Name</label>
                                        <br />
                                        <asp:TextBox ID="txtMaterialType" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" title="Product Name" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Allotment Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtAllotmentQty" CssClass="form-control" runat="server" data-toggle="tooltip" title="Allotment Qty" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                  
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Total Released QTY</label>
                                        <br />
                                        <asp:TextBox ID="txtApprovedQTY" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Approved QTY" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Balance QTY</label>
                                        <br />             
                                        <asp:TextBox ID="txtBalanceQTY" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" title="Balance QTY" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Release Requested QTY</label>
                                        <br />
                                        <asp:TextBox ID="txtReleaseRequestQTY" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Release Requested qty till now for this Allotment Number"  ReadOnly="true" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Permit</label>
                                        <br />
                                        <asp:DropDownList ID="ddlpermit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlpermit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                         <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Permit Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtPermit" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Permit"  ReadOnly="true" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>New Request Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtNewRequestedQty" onchange="CheckQTY()" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="New Requested Qty" ></asp:TextBox>
                                    </div>
                                 
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" title="Documents" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="valieddate12" runat="server">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Valid Date Upto</label>
                                                <br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtValiedDate" OnClientDateSelectionChanged="SelectValiedDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtValiedDate"  data-toggle="tooltip" data-placement="right" title="Valid Date Upto" Cssclass="form-control" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtvalieddate1" runat="server" />
                                            </div>
                                            </div>
                                     <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                                <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                                <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                            </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                    <asp:HiddenField ID="partycode" runat="server" />
                                     <asp:HiddenField ID="fromparty" runat="server" />
                                    <asp:HiddenField ID="productcode" runat="server" />
                                    
                                        <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                             <asp:LinkButton ID="btnApprove" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />

                                                <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg12()" class="fa fa-cut" OnClick="btnReject_Click" />
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                     <div class="clearfix"></div>
                                         <p>&nbsp;</p>
                                            <div id="approver" runat="server">
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
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                        </Columns>
                                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                    </asp:GridView>
                                                </div>
                                        </div>
                                        <p>&nbsp;</p>
                                         <div >

                                              <div class="col-md-12 col-sm-12 col-xs-12 form-inline"> 
                                             <asp:Gridview id="grdReleaseRequest" runat="server" autogeneratecolumns="false" class="table table-striped responsive-utilities jambo_table" headerstyle-backcolor="#26b8b8" rowstyle-backcolor="window" 
                                                  headerstyle-forecolor="#ecf0f1" emptydatatext="no records" >
                                                                <Columns>
                                                                    <asp:Templatefield headertext="Financial Year" itemstyle-font-bold="true" itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblfinancial_year" runat="server" text='<%#Eval("financial_year") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    <asp:Templatefield headertext="Allotment No" itemstyle-font-bold="true" itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblAllotedNo" runat="server" text='<%#Eval("final_allotment_no") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    <asp:Templatefield headertext="Allotted QTY" itemstyle-font-bold="true" itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblAllotedQty" runat="server" text='<%#Eval("allocation_qty") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    <asp:Templatefield headertext="RR Issued No" itemstyle-font-bold="true" itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblRRNo" runat="server" text='<%#Eval("rr_issueno") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>                                                                                            
                                                                    <asp:Templatefield headertext="RR Date" itemstyle-font-bold="true" itemstyle-width="20px">
                                                                        <Itemtemplate>
                                                                             <asp:label id="lblRRDate" runat="server" text='<%#Eval("rr_date") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    <asp:Templatefield headertext="RR Quantity" itemstyle-font-bold="true"   itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblRRQuantity" runat="server" text='<%#Eval("rr_quantity") %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    <asp:Templatefield headertext="Status" itemstyle-font-bold="true" itemstyle-width="40px">
                                                                        <Itemtemplate>
                                                                            <asp:label id="lblStatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":Eval("record_status").ToString()=="Y"? "Approval Pending":Eval("record_status").ToString()=="I"? "Issued":"Draft" %>'></asp:label>
                                                                        </Itemtemplate>
                                                                    </asp:Templatefield>
                                                                    
                                                                </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" CssClass="paginationClass" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                            </asp:Gridview></div>
                                           </div>
                                    </div></body>
                            </html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


</asp:Content>
