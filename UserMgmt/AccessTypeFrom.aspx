<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AccessTypeFrom.aspx.cs" Inherits="UserMgmt.AccessTypeFrom" %>

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
                                           if (document.getElementById('<%=txtAccessTypeCode.ClientID%>').value == '') {
                                            alert("Enter  AccessType Code");
                                              document.getElementById("<% =txtAccessTypeCode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtAccessTypeName.ClientID%>').value == '') {
                                            alert("Enter  AccessType Name");
                                              document.getElementById("<% =txtAccessTypeName.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtDescription.ClientID%>').value == '') {
                                            alert("Enter  Description");
                                             document.getElementById("<% =txtDescription.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                                <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_txtAccessTypeName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtAccessTypeName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "AccessTypeFrom.aspx/chkDuplicateAccessTypeName",
                                            data: '{accesstypename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("AccessType  Name is already exists");
                                                    $('#BodyContent_txtAccessTypeName').val("");
                                                    $('#BodyContent_txtAccessTypeName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateAccessTypecode() {
                                        debugger;
                                        var email = $('#BodyContent_txtAccessTypeCode').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtAccessTypeCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "AccessTypeFrom.aspx/chkDuplicateAccessTypecode",
                                            data: '{accesstypecode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("AccessType  Code is already exists");
                                                    $('#BodyContent_txtAccessTypeCode').val("");
                                                    $('#BodyContent_txtAccessTypeCode').focus();
                                                }

                                            }
                                        });
                                    }

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
                                            if ((txtlen - dotpos) > 3)
                                                return false;
                                        }
                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                            return false;
                                        return true;
                                    }
                                   
                                    </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="StateMaster" OnClick="StateMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton ID="DivisionMaster" OnClick="DivisionMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="DistrictMaster" OnClick="DistrictMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="ThanaMaster" OnClick="ThanaMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Thana Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleMaster" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Access Type Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">* </span>Access Type Code</label> <br />
                                            <asp:TextBox ID="txtAccessTypeCode" AutoComplete="off" CssClass="form-control" style="text-transform:capitalize" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="chkDuplicateAccessTypecode();" data-placement="right"  title="Access Type Code" Width="350px" Height="30px" runat="server"></asp:TextBox>
                                                   </div>
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">* </span>Access Type Name</label> <br />
                                            <asp:TextBox ID="txtAccessTypeName" AutoComplete="off" CssClass="form-control" style="text-transform:capitalize" data-toggle="tooltip" data-placement="right"  onchange="chkDuplicateAccessTypeName();" title="Access Type Name" Width="350px" Height="30px" runat="server"></asp:TextBox>
                                                   </div>
                                              <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Description</label>   <br />
                                                        <asp:TextBox ID="txtDescription" AutoComplete="off" data-toggle="tooltip" CssClass="form-control" data-placement="right"  Width="350px" Height="30px" title="Description"  runat="server"></asp:TextBox>
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

