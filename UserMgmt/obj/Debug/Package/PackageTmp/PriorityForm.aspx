<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PriorityForm.aspx.cs" Inherits="UserMgmt.PriorityForm" %>

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
                                <title>Ticket Status Master</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtpriorityCode.ClientID%>').value == '') {
                                            alert("Enter  Priority Code");
                                            document.getElementById("<% =txtpriorityCode.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtpriorityName.ClientID%>').value == '') {
                                            alert("Enter  Priority Name");
                                            document.getElementById("<% =txtpriorityName.ClientID%>").focus();
                                            return false;

                                        }

                                        if (document.getElementById('<%=txtResolvetime.ClientID%>').value == '') {
                                            alert("Enter  Resolvetime");
                                            document.getElementById("<% =txtResolvetime.ClientID%>").focus();
                                             return false;

                                         }
                                     }
                                </script>
                                <script>
                                    function chkDuplicate() {
                                        debugger;
                                        var email = $('#BodyContent_txtpriorityName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtpriorityName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "PriorityForm.aspx/chkDuplicate",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Priority Name  is already exists");
                                                    $('#BodyContent_txtpriorityName').val("");
                                                    $('#BodyContent_txtpriorityName').focus();
                                                }

                                            }
                                        });
                                    }

                                </script>
                                <script type="text/javascript">

                                    function isAlphaNumeric(keyCode) {

                                        return (((keyCode >= 48 && keyCode <= 57) && isShift == false) ||

                                               (keyCode >= 65 && keyCode <= 90) || keyCode == 8 ||

                                               (keyCode >= 96 && keyCode <= 105))

                                    }


                                    function isSpecialKey(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode
                                        if ((charCode >= 65 && charCode <= 91) || (charCode >= 97 && charCode <= 123) || (charCode >= 48 || charCode <= 57))
                                            return true;

                                        return false;
                                    }

                                </script>

                                <script type="text/javascript">
    function blockSpecialChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }
    </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="Ticket" OnClick="Ticket_Click" runat="server"><span style="color:#fff;font-size:14px;">Ticket Status Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="Ticketcategory" OnClick="Ticketcategory_Click" runat="server"><span style="color:#fff;font-size:14px;">Ticket Category Master</span></asp:LinkButton></li>

                                        <li class="active">
                                            <asp:LinkButton ID="Priority" OnClick="Priority_Click" runat="server"><span style="color:#fff;font-size:14px;">Priority Master</span></asp:LinkButton></li>
                                        <%--  <li>
                                            <asp:LinkButton ID="Helpdesk" OnClick="Helpdesk_Click"  runat="server"><span style="color:#fff;font-size:14px;">Help Desk Ticket</span></asp:LinkButton></li>
                                        --%>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Priority Master</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Priority Code</label>
                                                <br />
                                                <asp:TextBox ID="txtpriorityCode" AutoComplete="off" CssClass="form-control" Style="text-transform: capitalize" data-toggle="tooltip" data-placement="right" title="Priority Code" onkeypress="return blockSpecialChar(event)" Width="350px" Height="30px" runat="server" MaxLength="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Priority Name</label>
                                                <br />
                                                <asp:TextBox ID="txtpriorityName" AutoComplete="off" data-toggle="tooltip" CssClass="form-control" data-placement="right" Width="350px" Height="30px" onkeypress="return blockSpecialChar(event)"  onchange="chkDuplicate();" title="Priority Name" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Resolvetime</label>
                                                <br />
                                                <asp:TextBox ID="txtResolvetime" runat="server" AutoComplete="off" data-toggle="tooltip" CssClass="form-control" data-placement="right" Width="350px" onkeypress="return blockSpecialChar(event)" Height="30px" title="Resolvetime"></asp:TextBox>
                                            </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <%-- <div class="col-md-9 col-sm-9 col-xs-9">--%>
                                                <asp:HiddenField ID="txtid" runat="server" />
                                                <br />
                                                <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />

                                                <%--</div>--%>
                                            </div>
                                        </div>
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
