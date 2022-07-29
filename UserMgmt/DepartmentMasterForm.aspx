<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DepartmentMasterForm.aspx.cs" Inherits="UserMgmt.DepartmentMasterForm" %>
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
                                <title>Vat Type Master</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                       
                                        if (document.getElementById('<%=txtname1.ClientID%>').value == '') {
                                            alert("Enter Department Name");
                                             document.getElementById("<% =txtname1.ClientID%>").focus();
                                            return false;
                                           
                                        }
                                         if (document.getElementById('<%=txtcode.ClientID%>').value == '') {
                                             alert("Enter Department Code");
                                               document.getElementById("<% =txtcode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                    }
                                </script>
                                <script>
                                    function chkDuplicateProductTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_txtName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DepartmentMasterForm.aspx/chkDuplicateProductTypeName",
                                            data: '{producttypename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("Product Type  Name is already exists");
                                                    $('#BodyContent_txtName').val('');
                                                    $('#BodyContent_txtName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateProductTypeCode() {

                                        var email = $('#BodyContent_txtcode').val();
                                        if ($('#BodyContent_txtcode').val().length != 3) {
                                            alert("Product Product Type Code length sholud be 3");
                                            $('#BodyContent_txtcode').val('');
                                            return;
                                        }
                                        else {
                                            var jsondata = JSON.stringify($('#BodyContent_txtcode').val());
                                            $.ajax({
                                                type: "POST",
                                                //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                                url: "DepartmentMasterForm.aspx/chkDuplicateProductTypeCode",
                                                data: '{producttypecode:' + jsondata + '}',
                                                datatype: "application/json",
                                                contentType: "application/json; charset=utf-8",
                                                cache: false,
                                                async: false,
                                                success: function (msg) {

                                                    if (parseInt(msg.d) > 0) {
                                                        alert("Product Type Code is already exists");
                                                        $('#BodyContent_txtcode').val('');
                                                        $('#BodyContent_txtcode').focus();
                                                    }

                                                }
                                            });

                                        }
                                    }
                                     </script>

                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                            <asp:LinkButton ID="LinkButton1" OnClick="Designation_1_Click"  runat="server"><span style="color:#fff;font-size:14px;">Depertment Master</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="UserRegistration" OnClick="UserRegistration_Click" runat="server"><span style="color:#fff;font-size:14px;">User Registration</span></asp:LinkButton></li>
                                           <li >
                                            <asp:LinkButton ID="Employee_Details" OnClick="Employee_Details_Click" runat="server"><span style="color:#fff;font-size:14px;">Employee Details</span></asp:LinkButton></li>
                                       </ul> 
                                

                                <br />
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Department Master</h2>
                                    <div class="clearfix"></div>
                                </div>
                                
                                <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Department code</label><br />
                                        <asp:TextBox ID="txtcode" AutoComplete="off" Style="text-transform:uppercase" onchange="chkDuplicateProductTypeCode();"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Department Code" runat="server" MaxLength="3"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label style="font-size: small"><span style="color: red">*</span>Department Name</label><br />
                                        <asp:TextBox ID="txtname1" AutoPostBack="true" AutoComplete="off" Style="text-transform:capitalize" onchange="chkDuplicateProductTypeName();"   CssClass="form-control" Height="30px" Width="250px" data-toggle="tooltip" data-placement="right" title="Department Name" runat="server" MaxLength="50"></asp:TextBox>
                                    </div>
                                 
                                    
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                        <asp:HiddenField ID="org_id" runat="server" />
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg();" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
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
