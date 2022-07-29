<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="StateMasterForm.aspx.cs" Inherits="UserMgmt.StateMasterForm" %>

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
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtState.ClientID%>').value == '') {
                                            alert("Enter  State Name");
                                            return false;
                                            document.getElementById("<% =txtState.ClientID%>").focus();
                                        }

                                          if (document.getElementById('<%=txtSateCode.ClientID%>').value == '') {
                                            alert("Enter  State Code");
                                            return false;
                                            document.getElementById("<% =txtSateCode.ClientID%>").focus();
                                        }
                                    }
                                </script>

                                <script>
                                    function chkDuplicateState() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtState').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtState').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "StateMasterForm.aspx/chkDuplicateState",
                                            data: '{statename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("State Name is already exists");
                                                    $('#BodyContent_txtState').val('');
                                                    $('#BodyContent_txtState').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateStateCode() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtSateCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtSateCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "StateMasterForm.aspx/chkDuplicateStateCode",
                                            data: '{statecode:' + $('#BodyContent_txtSateCode').val() + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("State Code is already exists");
                                                    $('#BodyContent_txtSateCode').val('');
                                                    $('#BodyContent_txtSateCode').focus();
                                                }

                                            }
                                        });
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="StateMaster" OnClick="StateMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton ID="DivisionMaster" OnClick="DivisionMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="DistrictMaster" OnClick="DistrictMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="ThanaMaster" OnClick="ThanaMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Thana Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleMaster" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>


                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>State Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <br />
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span> State Name</label><br />
                                                <asp:TextBox ID="txtState" AutoComplete="off" runat="server" Height="30px" Width="250px" style="text-transform:capitalize" data-toggle="tooltip" data-placement="right" title="State Name" onchange="chkDuplicateState();"  class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>State Code</label><br />
                                                <input type="text" runat="server" id="txtSateCode" AutoComplete="off" style="text-transform:uppercase" data-toggle="tooltip" data-placement="right" title="State Code" onchange="chkDuplicateStateCode();"  class="form-control">
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Country Name</label><br />
                                                <input type="text" runat="server" id="txtcountryname" AutoComplete="off" value="IND" data-toggle="tooltip" data-placement="right" title="country name" readonly class="form-control">
                                            </div>
                                            <div>

                                                <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <asp:HiddenField ID="orgid" runat="server" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                                </div>
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
