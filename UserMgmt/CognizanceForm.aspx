<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="CognizanceForm.aspx.cs" Inherits="UserMgmt.CognizanceForm" %>

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
                                <title>Cognizance Form</title>
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
                                        if (document.getElementById('<%=txtCognizancetakenundersection.ClientID%>').value == '') {
                                            alert("Enter Cognizance taken under section");
                                            document.getElementById("<% =txtCognizancetakenundersection.ClientID%>").focus();
                                            return false;
                                        }
                                      <%--  var Cognizance_date = document.getElementById('<%=txtDateofCognizance.ClientID%>').value;
                                        var Nexthearing_Date = document.getElementById('<%=txtNexthearingDate.ClientID%>').value;
                                        //var cDate = new Date(Complaint_Date);
                                        //var fDate = new Date(FIR_Date);
                                        if (Cognizance_date != '' && Nexthearing_Date != '' &&  Cognizance_date>Nexthearing_Date) {                                        
                                            alert("Please ensure that the Nexthearing Date  is greater than or equal to the Cognizance Date .");
                                            return false;
                                        }--%>
                                          if (document.getElementById('<%=txtDateofCognizance.ClientID%>').value == '') {
                                              alert("Enter Date of Cognizance");
                                            document.getElementById("<% =txtDateofCognizance.ClientID%>").focus();
                                            return false;
                                          }
                                        if (document.getElementById('<%=txtNexthearingDate.ClientID%>').value == '') {
                                            alert("Enter Next hearing Date");
                                            document.getElementById("<% =txtNexthearingDate.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                                
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Cognizance Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                       
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                   
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                      <asp:TextBox ID="txtPRFIRNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                       <asp:TextBox ID="txtfirdate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>' Visible="false"  title="PR / FIR No"></asp:TextBox>
                                    </div>
                                 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Cognizance Under Section</label>
                                        <br />
                                        <asp:TextBox ID="txtCognizancetakenundersection" AutoComplete="off" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Cognizance Under Section"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Date of Cognizance</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDateofCognizance" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDateofCognizance"  data-toggle="tooltip" data-placement="right" title="Date of Cognizance" class="form-control validate[required]"  ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Next Hearing Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtNexthearingDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtNexthearingDate"  data-toggle="tooltip" data-placement="right" title="Next hearing date" class="form-control validate[required]"  ReadOnly="false"  onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div> 
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();"     OnClick ="btnSubmit_Click"     CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger"  OnClick="btnCancel_Click">Cancel
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
