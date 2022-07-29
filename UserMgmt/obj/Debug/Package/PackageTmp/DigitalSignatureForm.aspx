<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DigitalSignatureForm.aspx.cs" Inherits="UserMgmt.DigitalSignatureForm" %>


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
                                <title>Digital Signature Form</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                      
                                         if (document.getElementById('<%=ddlName.ClientID%>').value == 'Select') {
                                        alert("Select Name");
                                            document.getElementById("<% =ddlName.ClientID%>").focus();
                                            return false;

                                        }
                                         
                                        if (document.getElementById('<%=txtDigitalSignatureID.ClientID%>').value == '') {
                                            alert("Enter Digital Signature ID");
                                            document.getElementById("<% =txtDigitalSignatureID.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                        if (document.getElementById('<%=txtpassword.ClientID%>').value == '') {
                                            alert("Enter Password");
                                            document.getElementById("<% =txtpassword.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                                 <script type="text/javascript">
                                    function ShowHidePassword() {
                                        debugger;
                                        var txt = $('#<%=txtpassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                            $("label[for='cbShowHidePassword1']").text("Hide Password");
                                        }
                                        else
                                        {
                                            txt.prop("type", "password");
                                            $("label[for='cbShowHidePassword1']").text("Show Password");
                                        }
                                    }
                                    function ShowHidePassword1() {
                                        debugger;
                                        var txt = $('#<%=txtrepassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                            $("label[for='cbShowHidePassword2']").text("Hide Password");
                                        }
                                        else {
                                            txt.prop("type", "password");
                                            $("label[for='cbShowHidePassword2']").text("Show Password");
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">

                                    function CheckPassword(inputtxt) {
                                        debugger;
                                        var passw = $('#' + inputtxt).val();
                                        var format = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,15}$/;
                                        if (passw.match(format)) {
                                            //alert('Correct, try another...')
                                            return true;
                                        }
                                        else {
                                            alert('Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!')
                                            $('#' + inputtxt).val("");
                                            $('#' + inputtxt).focus();
                                            return false;
                                        }
                                    }
                                    function compare() {
                                       
                                            var string1 = document.getElementById('<%=txtpassword.ClientID%>').value;
                                            var string2 = document.getElementById('<%=txtrepassword.ClientID%>').value;

                                            if (string1 == string2) {
                                                $('#BodyContent_txtpass').val(string1);
                                            }
                                            else {
                                                alert("Password not Match.");
                                                $('#BodyContent_txtrepassword').val("");
                                                $('#BodyContent_txtpassword').val("");
                                                $('#BodyContent_txtpassword').focus();
                                                return false;

                                            }
                                        }

                                   
                                </script>
                            </head>
                            <body>
                             
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Digital Signature Form</h2>
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
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Role Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlRoleName" width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" title="Role Name"></asp:DropDownList>
                                     
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>User Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlName" width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoPostBack="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged" title="User Name"></asp:DropDownList>
                                     
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Emp ID</label>
                                        <br />
                                        <asp:TextBox ID="txtEmpID" width="60%" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Emp ID"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>User ID</label>
                                        <br />
                                        <asp:TextBox ID="txtUserID" width="60%" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="User ID"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Digital Signature Dongle SLNO</label>
                                        <br />
                                        <asp:TextBox ID="txtDigitalSignatureID" width="60%" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Digital Signature Dongle SLNO"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Certifying Authority</label>
                                        <br />
                                          <asp:TextBox ID="txtSA" runat="server" width="60%" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Certifying Authority"></asp:TextBox>
                                     
                                    </div>
                                      <div id="pass" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Password</label><br />
                                                <%--  <input type="password" id="password" runat="server" name="password" autocomplete="off" class="form-control validate[required]" maxlength="20" onchange="return chkPassword();" data-toggle="tooltip" data-placement="bottom" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!">--%>

                                                <asp:TextBox ID="txtpassword" Height="30px" onchange="CheckPassword(this.id)"  width="60%" TextMode="Password" class="form-control validate[required]" runat="server" data-toggle="tooltip" data-placement="right" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!"></asp:TextBox>
                                               <%-- <asp:ImageButton ID="Image1" Cssclass="control-label" runat="server" OnClientClick="ShowHidePassword();" Height="10%" Width="10%" ImageUrl="img/eye.jpg" />--%>
                                                <%-- <i class="far fa-eye" id="togglePassword"></i>--%>
                                                <input id="cbShowHidePassword1" name="Show" type="checkbox" onclick="ShowHidePassword();" /><label Cssclass="control-label"><span style="color: red"></span>Show</label>
                                            </div>


                                            <div id="repass" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">   
                                                <label class="control-label" style="display: inline" ><span style="color: red">*</span> Conform-Password</label><br />
                                                <%-- <input type="password" id="repassword" runat="server" autocomplete="off" name="repassword" class="form-control validate[required]" maxlength="20" onchange="return compare()" data-toggle="tooltip" data-placement="bottom" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!">--%>
                                                <asp:TextBox ID="txtrepassword" Height="30px"   width="60%" Cssclass="form-control" TextMode="Password" MaxLength="20" data-toggle="tooltip" data-placement="right" title="Password should have minimum 8 characters in length. At least one character in upper case, lower case & Numeric and special characters!" onchange=" return compare();" runat="server"></asp:TextBox>
                                                <input id="cbShowHidePassword2" type="checkbox" onclick="ShowHidePassword1();" /><label class="control-label"><span style="color: red"></span>Show</label>
                                                <asp:HiddenField ID="txtpass" runat="server" />
                                            </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Digital Signature Status</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDSS" width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" AutoPostBack="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged" title="User Name">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="txtid" runat="server" />
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
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
