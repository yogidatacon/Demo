<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RequestForPassForm.aspx.cs" Inherits="UserMgmt.RequestForPassForm" %>


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
                                <title>Request For Pass</title>
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
                                <script  type="text/javascript">
                                    function validationMsg() {
                                        debugger;
                                           if (document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'Select') {
                                                alert("Select  Request No");
                                                document.getElementById("<% =ddlReleaseRequestNo.ClientID%>").focus();
                                                return false;
                                         }
                                        if(document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'M')
                                        {
                                            if (document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'Select') {
                                                alert("Select Release Request No");
                                                document.getElementById("<% =ddlReleaseRequestNo.ClientID%>").focus();
                                                return false;

                                            }
                                            
                                           
                                            if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value == '' ) {
                                                alert("Enter Pass Requested Qty");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                               if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value < 1 ) {
                                                   alert("Requested Qty Zero or Less Than 1 is Not Allowed");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                               
                                            var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                            if (Request <= 0 && Request < 1)
                                            {
                                                alert("Requested Qty Zero or Less Than 1 is Not Allowed");
                                                $('#BodyContent_txtPassRequestedQty').val("");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                        }
                                        else
                                        {
                                             if (document.getElementById('<%=ddlReleaseRequestNo.ClientID%>').value == 'Select') {
                                                alert("Select NOC No");
                                                document.getElementById("<% =ddlReleaseRequestNo.ClientID%>").focus();
                                                return false;

                                             }
                                            if (document.getElementById('<%=ddlDepot.ClientID%>').value == 'Select') {
                                                alert("Select Depot");
                                                document.getElementById("<% =ddlDepot.ClientID%>").focus();
                                                return false;

                                            }
                                           
                                            var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                            if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value == '' ) {
                                                alert("Enter Pass Requested Qty");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                             if (document.getElementById('<%=txtPassRequestedQty.ClientID%>').value < 1 ) {
                                                   alert("Requested Qty Zero or Less Than 1 is Not Allowed");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                            if (Request <= 0 && Request < 1) {
                                                alert("Requested Qty Zero or Less Than 1 is Not Allowed");
                                                $('#BodyContent_txtPassRequestedQty').val("");
                                                document.getElementById("<% =txtPassRequestedQty.ClientID%>").focus();
                                                return false;

                                            }
                                        }
                                         
                                    }
                                    function validationMsg1()
                                    {
                                        if (document.getElementById('<%=txtPassApprovedQty.ClientID%>').value == '')
                                        {
                                            alert("Enter Pass Approved Qty");
                                            document.getElementById("<% =txtPassApprovedQty.ClientID%>").focus();
                                            return false;
                                        }
                                        var Approved = parseFloat($('#BodyContent_txtPassApprovedQty').val());
                                        if (Approved<=0)
                                        {
                                            alert("Pass Approved Qty Zero Not Allowed");
                                            document.getElementById("<% =txtPassApprovedQty.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                    function QTYvalidationMsg()
                                    {
                                        var Approved = parseFloat($('#BodyContent_txtApprovedQTY').val());
                                        var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                        var balance = parseFloat($('#BodyContent_balanceqty').val()).toFixed(2);
                                        debugger;
                                        if (Request > balance || balance<0) {
                                            alert("Request Qty should be less than or equal to RR/NOC Balance Qty !!!")
                                            $('#BodyContent_txtPassRequestedQty').val("");
                                            $('#BodyContent_txtPassRequestedQty').focus();
                                            return false;
                                        }
                                    }
                                    function QTYvalidationMsg1() {
                                        var Approved = parseFloat($('#BodyContent_txtPassApprovedQty').val());
                                        var Request = parseFloat($('#BodyContent_txtPassRequestedQty').val());
                                       
                                        if (Approved > Request) {
                                            alert("Pass Approved Qty should be less than or equal to Pass Requested QTY!!!")
                                            $('#BodyContent_txtPassApprovedQty').val("");
                                            $('#BodyContent_txtPassApprovedQty').focus();
                                            return false;
                                        }
                                    }
                                </script>
                                
                            </head>
                            <body>
                                   <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Pass</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click"><span style="color:#fff;font-size:14px;">Generated Pass List</span></asp:LinkButton></li>
                                        
                                        
                                    </ul>
                                </div>
                                  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2> Request For  Pass</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                     
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <label class="control-label" id="rrl" runat="server" style="display:inline"><span style="color: red">*</span>Release Request No</label>
                                        <label class="control-label" id="nocl" runat="server" style="display:inline"><span style="color: red">*</span>NOC No</label>
                                        <br />
                                        <asp:DropDownList ID="ddlReleaseRequestNo" runat="server" Width="70%" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlReleaseRequestNo_SelectedIndexChanged" data-toggle="tooltip" data-placement="right" ></asp:DropDownList>
                                      
                                    </div>
                                    <div id="depot" runat="server"  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Depot Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDepot" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" data-toggle="tooltip" data-placement="right" ></asp:DropDownList>
                                      
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Request Date</label>
                                        <br />
                                        <asp:TextBox ID="txtReleaseRequestDate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"  ></asp:TextBox>
                                    </div>
                                      <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Customer/Supplier</label>
                                        <br />
                                        <asp:TextBox ID="txtcutomername" Width="70%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"  ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                     <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Final Allotment No</label>
                                        <br />
                                        <asp:TextBox ID="txtMolassesFinalAllotmentNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"  ></asp:TextBox>
                                    </div>--%>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Valid Upto</label>
                                        <br />
                                        <asp:TextBox ID="txtRRValidUpto" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                    <%-- <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Unit Name</label>
                                        <br />
                                        <asp:TextBox ID="txtUnitName" CssClass="form-control" runat="server" Width="50%" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>--%>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Product Type</label>
                                        <br />
                                        <asp:TextBox ID="txtMaterialType" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Allotment Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtAllotmentQty" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                  
                                   
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Approved QTY</label>
                                        <br />
                                        <asp:HiddenField ID="passtype" runat="server" />
                                        <asp:TextBox ID="txtApprovedQTY" CssClass="form-control" runat="server" data-toggle="tooltip" title="Approved QTY" data-placement="right" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Balance Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" ReadOnly="true" data-toggle="tooltip" data-placement="right"  title="RR Balance Qty" ></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"

ControlToValidate="txtBalance" />
                                    </div>
                                    <%--<div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Pass Requested Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtIssuedqty" CssClass="form-control" runat="server" ReadOnly="true" data-toggle="tooltip" data-placement="right"  title="RR Balance Qty" ></asp:TextBox>
                                    </div>--%>
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="balanceqty" runat="server" />
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Pass Request Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtPassRequestedQty" onchange="QTYvalidationMsg()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Pass Request Qty" ></asp:TextBox>
                                    </div>
                                   
                                     <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="pass" runat="server" style="display:inline"><span style="color: red">*</span>Pass Approved Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtPassApprovedQty" onchange="QTYvalidationMsg1()" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Pass Approved Qty" ></asp:TextBox>
                                    </div>
                                 
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                             <asp:LinkButton ID="btnApprove" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                                <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right"  class="fa fa-cut" OnClick="btnReject_Click" />
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
