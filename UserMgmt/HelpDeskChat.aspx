<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDeskChat.aspx.cs" Inherits="UserMgmt.HelpDeskChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script  type="text/javascript">
          function validationMsg() {
              if (document.getElementById('<%=txtContactNumber.ClientID%>').value == '') {
                  alert("Enter Contact Number ");
                  document.getElementById("<% =txtContactNumber.ClientID%>").focus();
                  return false;
              }

              if (document.getElementById('<%=txtEmail.ClientID%>').value == '') {
                  alert("Enter Email ");
                  document.getElementById("<% =txtEmail.ClientID%>").focus();
                  return false;

              }


          }
                                    </script>
    <style>
        .button {
  background-color:royalblue;
  border: none;
  color: white;
  padding: 10px 22px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
  border-radius: 2px;
}
    </style>
     <script>
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
    </script>
</head>
    
<body>
    <form id="form1" runat="server">
                                <div class="x_title">
                                    <div style="height: 8%; background-color: #26b8b8;">
                                                    <span style="font-size: small; color: white; margin-left: 40%">Create HelpDesk Ticket</span>
                                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                      &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                    <div style="align-content:center">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline">Welcome<span style="color: red">:</span></label>
                                        <asp:Label ID="labUsername" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                                    </div>
                                         &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                           <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                     <%--   <label class="control-label" style="display: inline"><span style="color: red"></span>Page Name</label>--%>
                                        <asp:TextBox ID="txtpagename" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right"></asp:TextBox>
                                    </div>
                                       
                                         <div class="col-md-4 col-sm-12 col-xs-12">
                                             <asp:TextBox ID="txtmessagebody" runat="server" TextMode="MultiLine" Width="400px" placeholder="Ticket Query" Height="300px" MaxLength="2000"></asp:TextBox>
                                             </div>
                                         <asp:FileUpload ID="idupDocument" runat="server" onchange="validateExtraDocuments();" />
                                  &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Contact Number </label>
                                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number"></asp:TextBox>
                                    </div>    &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Email</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" title="Email"></asp:TextBox>
                                    </div>
                                    &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;
                                   <div style="align-content:center;margin-left:30%" class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                       <asp:Label ID="lblResult" runat="server" ></asp:Label>
                                        <asp:HiddenField ID="txtid" runat="server" />
                                       <asp:Button ID="btnsend" runat="server" Text="Send" OnClientClick="javascript:return validationMsg()" CssClass="button" OnClick="btnsend_Click" />
                                       <%--  <asp:Button ID="btncancle" runat="server" Text="Cancel"  CssClass="btn btn-danger" OnClientClick="javascript:window.close();" />--%>
                                       <%-- <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Send</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>--%>
                                    </div>
                                </div></div></div>
    </form>
</body>
</html>
