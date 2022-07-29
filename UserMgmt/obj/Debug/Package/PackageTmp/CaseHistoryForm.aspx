<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="CaseHistoryForm.aspx.cs" Inherits="UserMgmt.CaseHistoryForm" %>
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
                                <title>Case History Form</title>
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
                                        if (document.getElementById('<%=ddlAccusedName.ClientID%>').value == 'Select') {
                                            alert("Select AccusedName");
                                            document.getElementById("<% =ddlAccusedName.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=ddlAccusedIdProof.ClientID%>').value == 'Select') {
                                             alert("Select Accused Id Proof");
                                            document.getElementById("<% =ddlAccusedIdProof.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtIDNo.ClientID%>').value == '') {
                                             alert("Enter ID No");
                                            document.getElementById("<% =txtIDNo.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtCaseID.ClientID%>').value == '') {
                                            alert("Enter Case ID");
                                            document.getElementById("<% =txtCaseID.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtCaseDetails.ClientID%>').value == '') {
                                             alert("Enter Case Details");
                                            document.getElementById("<% =txtCaseDetails.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Case History Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Accused Name  </label>
                                        <br />
                                         <asp:DropDownList ID="ddlAccusedName" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Name"></asp:DropDownList>
                                       
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Accused Id Proof </label>
                                        <br />
                                         <asp:DropDownList ID="ddlAccusedIdProof" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Id Proof"></asp:DropDownList>                                       
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>ID No</label>
                                        <br />
                                        <asp:TextBox ID="txtIDNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="ID No" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Case ID </label>
                                        <br />
                                        <asp:TextBox ID="txtCaseID" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Case ID " ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                    <div  class="col-md-6 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red">*</span>Case Details</label>
                                        <br />
                                        <asp:TextBox ID="txtCaseDetails" CssClass="form-control" runat="server" data-toggle="tooltip" Width="84%" data-placement="right" title="Case Details" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                           <%-- <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
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
