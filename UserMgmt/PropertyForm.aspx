<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="PropertyForm.aspx.cs" Inherits="UserMgmt.PropertyForm" %>
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
                                <title>Property Form</title>
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
                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_NestedBodyContent_txtMobileNo').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Mobile No.");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').val("");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').focus();
                                        }
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
                                        if (document.getElementById('<%=ddlPropertyType.ClientID%>').value == 'Select') {
                                            alert("Select Property Type");
                                            document.getElementById("<% =ddlPropertyType.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtPropertyAddress.ClientID%>').value == '') {
                                             alert("Enter Property Address");
                                            document.getElementById("<% =txtPropertyAddress.ClientID%>").focus();
                                            return false;

                                         }
                                          else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtPropertyAddress").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <10)
                                             {
                                                 alert("Property Address should be minimum 10 character");
                                                 document.getElementById("<% =txtPropertyAddress.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                          <%--else if (document.getElementById('<%=txtLocation.ClientID%>').value == '') {
                                             alert("Enter Location");
                                            document.getElementById("<% =txtLocation.ClientID%>").focus();
                                            return false;
                                         }
                                       
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtLocation").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Location should be minimum 3 character");
                                                 document.getElementById("<% =txtLocation.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtLandmark.ClientID%>').value == '') {
                                             alert("Enter Landmark");
                                            document.getElementById("<% =txtLandmark.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtLandmark").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Landmark should be minimum 3 character");
                                                 document.getElementById("<% =txtLandmark.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtCircle.ClientID%>').value == '') {
                                             alert("Enter Circle");
                                            document.getElementById("<% =txtCircle.ClientID%>").focus();
                                            return false;
                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtCircle").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Circle should be minimum 3 character");
                                                 document.getElementById("<% =txtCircle.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtMouzaName.ClientID%>').value == '') {
                                             alert("Enter Mauza Name");
                                            document.getElementById("<% =txtMouzaName.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtMouzaName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Mauza Name should be minimum 3 character");
                                                 document.getElementById("<% =txtMouzaName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                        if (document.getElementById('<%=txtKhataNo.ClientID%>').value == '') {
                                            alert("Enter KhataNo");
                                            document.getElementById("<% =txtKhataNo.ClientID%>").focus();
                                            return false;
                                        }
                                        else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtKhataNo").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("KhataNo should be minimum 3 character");
                                                 document.getElementById("<% =txtKhataNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtKhasraNo.ClientID%>').value == '') {
                                             alert("Enter KhasraNo");
                                            document.getElementById("<% =txtKhasraNo.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtKhasraNo").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("KhasraNo should be minimum 3 character");
                                                 document.getElementById("<% =txtKhasraNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtThana.ClientID%>').value == '') {
                                            alert("Enter Thana");
                                            document.getElementById("<% =txtThana.ClientID%>").focus();
                                            return false;
                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtThana").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Thana should be minimum 3 character");
                                                 document.getElementById("<% =txtThana.ClientID%>").focus();
                                                 return false;
                                             }
                                         }

                                         if (document.getElementById('<%=txtOwnerName.ClientID%>').value == '') {
                                             alert("Enter Owner Name");
                                            document.getElementById("<% =txtOwnerName.ClientID%>").focus();
                                            return false;
                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtOwnerName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Owner Name should be minimum 3 character");
                                                 document.getElementById("<% =txtOwnerName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if ((document.getElementById('<%=txtMobileNo.ClientID%>').value == '')|| (document.getElementById('<%=txtMobileNo.ClientID%>').value.length<"10")){
                                             alert("Enter valid Mobile No");
                                            document.getElementById("<% =txtMobileNo.ClientID%>").focus();
                                            return false;
                                         } --%>                               
                                        <%--if (document.getElementById('<%=txtOwnerPermanentAddress.ClientID%>').value == '') {
                                             alert("Enter Owner Permanent Address");
                                            document.getElementById("<% =txtOwnerPermanentAddress.ClientID%>").focus();
                                            return false;
                                         }
                                          else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtOwnerPermanentAddress").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Owner Permanent should be minimum 3 character");
                                                 document.getElementById("<% =txtOwnerPermanentAddress.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                        if (document.getElementById('<%=txtOwnerPresentAddress.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter Owner Present Address");
                                            document.getElementById("<% =txtOwnerPresentAddress.ClientID%>").focus();
                                            return false;

                                        }--%>
                                         
                                    }
                                </script>

                                   <script type="text/javascript">
                                    function copyValue(Chk) {
                                        if(Chk.checked)
                                        {
                                             document.getElementById('<%=txtOwnerPresentAddress.ClientID%>').value  = document.getElementById('<%=txtOwnerPermanentAddress.ClientID%>').value;
                                        }
                                       
                                    }
                                </script>
                            </head>
                            <body>
                            
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Property Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Property Type </label>
                                        <br />
                                        <asp:DropDownList ID="ddlPropertyType" runat="server" CssClass="form-control" Height="5%" Width="50%" data-toggle="tooltip"  data-placement="right" title="Property Type"></asp:DropDownList>
                                      
                                    </div>
                                  
                                      <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red">*</span>Property Address/Description  </label>
                                        <br />
                                         <asp:TextBox ID="txtPropertyAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Property Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Location</label>
                                        <br />
                                        <asp:TextBox ID="txtLocation" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Location" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Landmark</label>
                                        <br />
                                        <asp:TextBox ID="txtLandmark" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Landmark" ></asp:TextBox>
                                    </div>
                                   
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Circle</label>
                                        <br />
                                        <asp:TextBox ID="txtCircle" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Circle Name" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Mouza Name </label>
                                        <br />
                                        <asp:TextBox ID="txtMouzaName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Mauza Name" onkeypress="return onlyAlphabets(this,event);" ></asp:TextBox>
                                    </div>

                                      <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Khata No</label>
                                        <br />
                                        <asp:TextBox ID="txtKhataNo" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Khata No" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Khasra No</label>
                                        <br />
                                        <asp:TextBox ID="txtKhasraNo" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Khasra No" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Thana </label>
                                        <br />
                                        <asp:DropDownList ID="ddlThana" runat="server" Visible="false" CssClass="form-control" Height="5%" Width="50%" data-toggle="tooltip" data-placement="right" title="Thana"></asp:DropDownList>
                                          <asp:TextBox ID="txtThana" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Owner Name" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Owner Name</label>
                                        <br />
                                        <asp:TextBox ID="txtOwnerName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Owner Name" onkeypress="return onlyAlphabets(this,event);" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Mobile No </label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" onchange="phoneValidate()" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" MaxLength="10" title="Mobile No" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Permanent Address </label>
                                          &nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="chk" runat="server" onclick="copyValue(this)" /> Copy Address
                                        <br />
                                        <asp:TextBox ID="txtOwnerPermanentAddress" autocomplete="off" CssClass="form-control" runat="server" Height="10%" Width="77.5%" data-toggle="tooltip" data-placement="right" title="Owner Permanent Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Present Address</label>
                                        <br />
                                         <asp:TextBox ID="txtOwnerPresentAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Height="10%" Width="77.5%" data-placement="right" title="Owner Present Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>

                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                              <asp:HiddenField ID="party_code" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                         <%--   <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
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
