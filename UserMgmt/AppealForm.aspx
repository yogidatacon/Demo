<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="AppealForm.aspx.cs" Inherits="UserMgmt.AppealForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title>Appeal Against Acquitta/Conviction</title>
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
    <script type="text/javascript">
    function blockAllChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false; 
        }
    </script>
                               <script language="javascript" type="text/javascript">
                                   function validationMsg() {

                                       if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                           alert("Enter FIR details!");
                                           document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlNameofCourt.ClientID%>').value == 'Select') {
                                           alert("Select Name of Court");
                                           document.getElementById("<% =ddlNameofCourt.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlAccusedName.ClientID%>').value == 'Select') {
                                           alert("Select AccusedName");
                                           document.getElementById("<% =ddlAccusedName.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlAccusedStatus.ClientID%>').value == 'Select') {
                                           alert("Select Accused Status");
                                           document.getElementById("<% =ddlAccusedStatus.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtAppealNo.ClientID%>').value == '') {
                                           alert("Enter Appeal No");
                                           document.getElementById("<% =txtAppealNo.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtDate.ClientID%>').value == '') {
                                           alert("Enter Date ");
                                           document.getElementById("<% =txtDate.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtAppealBy.ClientID%>').value == '') {
                                           alert("Enter AppealBy");
                                           document.getElementById("<% =txtAppealBy.ClientID%>").focus();
                                           return false;
                                       }

                                       if (document.getElementById('<%=txtResult.ClientID%>').value == '') {
                                           alert("Enter result");
                                           document.getElementById("<% =txtResult.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtResultDate.ClientID%>').value == '') {
                                           alert("Select Result Date");
                                           document.getElementById("<% =txtResultDate.ClientID%>").focus();
                                           return false;
                                       }
                                   }
                                </script>
                            </head>
                            <body>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords"  OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Appeal Against Acquitta/Conviction</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red">*</span>PR / FIR No</label>
                                                <br />                                                
                                                <asp:TextBox ID="txtPRFIRNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                            </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Name of Court</label>
                                        <br />
                                      <asp:DropDownList ID="ddlNameofCourt" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Name of Court"></asp:DropDownList>
                                    </div>
                                 
                                    <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Accused Name</label>
                                        <br />
                                        <asp:TextBox ID="txtAccusedName" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Accused Name"></asp:TextBox>
                                    </div>--%>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Accused Name  </label>
                                        <br />
                                        <asp:DropDownList ID="ddlAccusedName" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Name"></asp:DropDownList>
                                    </div>

                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Accused Status</label>
                                        <br />
                                            <asp:DropDownList ID="ddlAccusedStatus" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Status"></asp:DropDownList>
                                        
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Appeal No</label>
                                        <br />
                                        <asp:TextBox ID="txtAppealNo" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Appeal No"></asp:TextBox>
                                    </div>
                                     
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Date </label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title=" Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>

                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Appeal By</label>
                                        <br />
                                        <asp:TextBox ID="txtAppealBy" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Appeal By"></asp:TextBox>
                                    </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Result</label>
                                        <br />
                                        <asp:TextBox ID="txtResult" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Result"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Result Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtResultDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtResultDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Result Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div> 
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
