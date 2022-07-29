<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HelpDeskForm.aspx.cs" Inherits="UserMgmt.HelpDeskForm" %>

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
                                <%--  <script language="javascript" type="text/javascript">
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
                                        if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
                                            alert("Enter Password");
                                            document.getElementById("<% =txtPassword.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>--%>
                                <script language="javascript" type="text/javascript">
                                    <%-- function ShowHidePassword() {
                                        debugger;
                                        var txt = $('#<%=txtPassword.ClientID%>');
                                        if (txt.prop("type") == "password") {
                                            txt.prop("type", "text");
                                           // $("label[for='cbShowHidePassword1']").text("Hide Password");
                                        }
                                        else {
                                            txt.prop("type", "password");
                                          //  $("label[for='cbShowHidePassword1']").text("Show Password");
                                        }
                                 }


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
                                    }--%>
                                </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="Ticket" OnClick="Ticket_Click"  runat="server"><span style="color:#fff;font-size:14px;">Ticket Status Master</span></asp:LinkButton></li>

                                        <li >
                                            <asp:LinkButton ID="Priority" OnClick="Priority_Click"  runat="server"><span style="color:#fff;font-size:14px;">Priority Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="Helpdesk" OnClick="Helpdesk_Click"  runat="server"><span style="color:#fff;font-size:14px;">Help Desk</span></asp:LinkButton></li>
                                       
                                    </ul>
                                    <br />
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <%--<asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton>--%></a>
                                <div class="x_title">
                                    <h2>HelpDesk Form</h2>
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
                           <%--    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Page Name</label>
                                        <br />
                                        <asp:TextBox ID="txtpagename" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ReadOnly="true"></asp:TextBox>
                                    </div>--%>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" style="display: inline"><span style="color: red"></span>P</label><br />
                                           <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" style="display: inline"><span style="color: red"></span>T</label><br />
                                           <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" style="display: inline"><span style="color: red"></span>T</label><br />
                                           <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="col-md-12 col-sm-12 col-xs-12" style="Width:100%" >
                                        <%-- <div style="display:inline">
                                         <label class="control-label" style="display: inline"><span style="color: red"></span>Contact Number </label>
                                      <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ReadOnly="true"></asp:TextBox>
                                     <label class="control-label" style="display: inline"><span style="color: red"></span>Email</label>
                                      
                                        Email:<asp:TextBox ID="txtEmail" CssClass="form-control" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true"></asp:TextBox>

                                         </div> <br/>--%>
                                        <asp:TextBox ID="txtticketQuery"  autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" cols="60" rows="16" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                   <%-- <div class="col-md-1 col-sm-12 col-xs-12"></div>
                                     <div class="col-md-4 col-sm-12 col-xs-12">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>A</label>
                                           <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                                          
                                           </div>--%>
                                   
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div style="width:100%;height:100%" >
                                         <asp:TextBox ID="TextBox1"  autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>--%>
                                    </div>
                                     <div class="clearfix"></div>
                                    <div style="width:100%">
                                         <asp:TextBox ID="TextBox2"  autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <p>&nbsp;</p>
                                      <div class="x_title">
                                    <div class="clearfix"></div>
                                </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                          </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        </div>  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         
                                           </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        
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
