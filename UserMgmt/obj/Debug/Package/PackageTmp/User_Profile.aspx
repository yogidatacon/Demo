<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="User_Profile.aspx.cs" Inherits="UserMgmt.User_Profile" %>
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
                                <title>User Profile</title>
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

                                       <%--if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
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
                                       }--%>
                                   }
                                </script>
                                <script type="text/javascript">

                                    function validateExtraDocuments() {

                                        var fileInput = document.getElementById('<%= idproofimage.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg only.');
                                            fileInput.value = '';
                                            return false;
                                        }

                                        var uploadControl = document.getElementById('<%= idproofimage.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idproofimage.ClientID %>').value = "";

                                            return false;
                                        }
                                        else {

                                            return true;
                                        }
                                    }
                                    function imageUpload(image, imageLbl) {
                                        debugger;
                                        var imgText = $('#' + image).val();
                                        //var filename = imgText.replace(/^.*[\\\/]/, '');
                                        //var imgTextArr = filename.split(".");
                                        //var imgTxtFTb = imgTextArr[0];
                                        //if (parseInt(imgTxtFTb.length) > 45) {
                                        //    imgTxtFTb = imgTxtFTb.substring(0, 45);
                                        //}
                                        $('#' + imageLbl).val(imgText);
                                    }

                                    function browseImage(image) {

                                        $('#' + image).click();
                                    }
                                </script>
                            </head>
                            <body>
                                   
                                <div class="x_title">
                                    <h2>User Profile</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="clearfix"></div>
                                   
                                      </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span>Name of Officer</label>
                                                <br />                                                
                                                <asp:TextBox ID="txtnameofofficer" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right"  Text='<%#Eval("user_name") %>'  title="Name of Officer"></asp:TextBox>
                                            </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Designation</label>
                                        <br />
                                      <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                    </div>
                                 
                                   
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>District</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="District"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Mobile No</label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Mobile No"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Date of Birth </label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDOB" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDOB" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date of Birth" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                           <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Date of Joining</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtdateofjoining" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtdateofjoining" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date of Joining" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div> 
                                          <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Date of Present Posting</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtdateofpresent" Format="dd-MM-yyyy" ID="CalendarExtender3"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtdateofpresent"  data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date of Present Posting" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                            </div> 
                                          <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Date of Retairement</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txtretairement" Format="dd-MM-yyyy" ID="CalendarExtender4"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtretairement" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date of Retairement" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                            </div> 
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>PRAN No/GPF No</label>
                                        <br />
                                        <asp:TextBox ID="txtPranNo" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="PRAN No/GPF No"></asp:TextBox>
                                    </div>

                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Email Id</label>
                                        <br />
                                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Email Id"></asp:TextBox>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Blood Group</label>
                                        <br />
                                        <asp:TextBox ID="txtbloodgroup" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Blood Group"></asp:TextBox>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Emergency Contact No</label>
                                        <br />
                                        <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Emergency Contact No"></asp:TextBox>
                                    </div>
                                 <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <%-- <label class="control-label"><span style="margin-top: 5px;">Attachment<span style="color: red">*</span></span>--%>
                                                <label class="control-label"><span style="color: red">*</span>User Photo</label><br />


                                                <asp:FileUpload runat="server" Width="250px" Style="display: none;" ID="idproofimage" class="file-name" name="idproofimage" value="" onchange="validateExtraDocuments();
                                                    imageUpload('BodyContent_idproofimage', 'idproofimageLbl');" />
                                                <input class="form-control" width="250px" readonly style="margin-top: 5px;" onkeypress="return attachMand('BodyContent_idproofimage',this.id)" data-toggle="tooltip" data-placement="right" title="Attachment" type="text" id="idproofimageLbl" name="idproofimageLbl" maxlength="250" placeholder="Attachment Name">
                                                <p id="pattachment" style="font-size: 9px; font-weight: 600;">(.jpg, .jpeg  upto 2 MB max)</p>
                                                <input type="button" id="btndownloadattachment" style="width: 250px; display: none; margin-top: -5px;" class="btn btn-primary" value="Download file" onclick="downloadattachment();" />
                                                <input type="button" id="btnppup" style="width: 85px; margin-bottom: -1px;" Value="Browse.." class="btn btn-primary" onclick="browseImage('BodyContent_idproofimage');" />
                                                <span style="display: none">
                                                    <asp:Button runat="server" ID="btnUpload" />
                                                </span>
                                                <span style="display: none">
                                                    <asp:Button runat="server" ID="btnDownloadMf1Attachment" />
                                                </span>
                                                <asp:Button runat="server" ID="btnDownload" CssClass="myButton" Text="Download" OnClick="btnDownload_Click" />
                                            </div>  
                                 
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left" >
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save</asp:LinkButton>
                                       
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
