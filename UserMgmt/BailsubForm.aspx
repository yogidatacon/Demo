<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="BailsubForm.aspx.cs" Inherits="UserMgmt.BailsubForm" %>

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
                                <title>Accused Bail  Form</title>
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
                                 <script>
                                    function onlyAlphabets(e, t) {
                                        try {
                                            if (window.event) {
                                                var charCode = window.event.keyCode;
                                            }
                                            else if (e) {
                                                var charCode = e.which;
                                            }
                                            else { return true; }
                                            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32)
                                                return true;
                                            else
                                                return false;
                                        }
                                        catch (err) {
                                            alert(err.Description);
                                        }
                                    }
                                </script>
                               <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                       
                                       if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                            alert("Enter FIR details!");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlNameoftheCourt.ClientID%>').value == 'Select') {
                                            alert("Select Name of the Court");
                                            document.getElementById("<% =ddlNameoftheCourt.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=ddlAccusedName.ClientID%>').value == 'Select') {
                                             alert("Select Accused Name");
                                            document.getElementById("<% =ddlAccusedName.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtBailNo.ClientID%>').value == '') {
                                            alert("Enter Bail No");
                                            document.getElementById("<% =txtBailNo.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=ddlBailType.ClientID%>').value == 'Select') {
                                             alert("Select Bail Type");
                                            document.getElementById("<% =ddlBailType.ClientID%>").focus();
                                            return false;
                                         }
                                          if (document.getElementById('<%=txtBailRequestDate.ClientID%>').value == '') {
                                              alert("Select Bail Request Date");
                                            document.getElementById("<% =ddlBailType.ClientID%>").focus();
                                            return false;
                                          }
                                        if (document.getElementById('<%=txtBailGrantedDate.ClientID%>').value == '') {
                                            alert("Entere Bail Granted Date");
                                            document.getElementById("<% =txtBailGrantedDate%>").focus();
                                            return false;
                                        }
                                      <%--  var Bail_RequestDate = document.getElementById('<%=txtBailRequestDate.ClientID%>').value;
                                        var Bail_GrantedDate = document.getElementById('<%=txtBailGrantedDate.ClientID%>').value;
                                        //var cDate = new Date(Complaint_Date);
                                        //var fDate = new Date(FIR_Date);
                                        if (Bail_RequestDate != '' && Bail_GrantedDate != '' && Bail_RequestDate > Bail_GrantedDate) {
                                            alert("Please ensure that the Bail Granted Date  is greater than or equal to the Bail Request Date .");
                                            return false;
                                        }--%>
                                         <%--if (document.getElementById('<%=txtBailer.ClientID%>').value == '') {
                                             alert("Enter Bailer");
                                            document.getElementById("<% =txtBailer.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtReasonForGrant.ClientID%>').value == '') {
                                            alert("Enter Reason For Grant");
                                            document.getElementById("<% =txtReasonForGrant.ClientID%>").focus();
                                            return false;
                                        }--%>
                                    }
                                </script>
                            </head>
                            <body>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2> Bail  Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                  <%--  <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>--%>

                                <a>
                                     <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="btnSeizure_Click"  BorderStyle="Outset"> Seizure</asp:LinkButton>
                                </a>
                                <%--<a>
                                       <asp:LinkButton runat="server" ID="btnFIR" Height="100%" Width="12%" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View FIR" OnClick="btnFIR_Click"  BorderStyle="Outset"> FIR</asp:LinkButton>
                                </a>
                                <a>
                                       <asp:LinkButton runat="server" ID="btnChargeSheet" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Chargesheet" OnClick="btnChargeSheet_Click"  BorderStyle="Outset">Chargesheet </asp:LinkButton>
                                </a>--%>

                                       <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                  
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                      <asp:TextBox ID="txtPRFIRNo" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                        <asp:TextBox ID="txtfirdate" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"  Visible="false"  title="PR / FIR No"></asp:TextBox>
                                    </div>
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Name of the Court</label>
                                        <br />
                                        <asp:DropDownList ID="ddlNameoftheCourt" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Name of the Court"></asp:DropDownList>
                                    </div> 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Accused Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlAccusedName" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Name"></asp:DropDownList>
                                    </div> 
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Bail Granted </label>
                                        <br />
                                          <asp:RadioButton ID="rdYes" runat="server" GroupName = "radio"    />Yes  &nbsp;&nbsp;&nbsp;
                                          <asp:RadioButton ID="rdNo" runat="server" GroupName = "radio"  />No
                                    </div>
                                     <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Bail No</label>
                                        <br />
                                        <asp:TextBox ID="txtBailNo" AutoComplete="off" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Bail No"></asp:TextBox>
                                    </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Bail Type</label>
                                        <br />
                                        <asp:DropDownList ID="ddlBailType" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Bail Type"></asp:DropDownList>
                                    </div> 
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Bail Request Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtBailRequestDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtBailRequestDate"  data-toggle="tooltip" data-placement="right" title="Bail Request Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" ReadOnly="false" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                      
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Bail Granted Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtBailGrantedDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtBailGrantedDate"  data-toggle="tooltip" data-placement="right" title="Bail Granted Date" class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                       <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Bailer</label>
                                        <br />
                                        <asp:TextBox ID="txtBailer" AutoComplete="off" CssClass="form-control" runat="server" onkeypress="return onlyAlphabets(this,event);"  data-toggle="tooltip" data-placement="right" title="Bailer"></asp:TextBox>
                                    </div>
                                      <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" style="font-size:small; display:inline"><span style="color: red"></span>Reason for Grant</label>
                                        <br />
                                        <asp:TextBox ID="txtReasonForGrant" AutoComplete="off" CssClass="form-control" Width="100%"  runat="server" data-toggle="tooltip" data-placement="right" title="Reason for Grant" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger"  OnClick="btnCancel_Click" >Cancel
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
