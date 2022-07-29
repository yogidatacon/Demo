<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistrictUserForm.aspx.cs" Inherits="UserMgmt.DistrictUserForm" MasterPageFile="~/Admin.Master" %>

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
                                        if ($.trim(document.getElementById('<%=txtUserName.ClientID%>').value) == '') {
                                            alert("Enter User Name");
                                            return false;
                                            document.getElementById("<% =txtUserName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
                                            alert("Enter Password");
                                            return false;
                                            document.getElementById("<%=txtPassword.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtFullName.ClientID%>').value == '') {
                                            alert("Enter Full Name");
                                            return false;
                                            document.getElementById("<%=txtFullName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtEmailId1.ClientID%>').value == '') {
                                            alert("District Office Email Can not be blank");
                                            return false;
                                            document.getElementById("<%=txtEmailId1.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtMobileNo1.ClientID%>').value == '') {
                                            alert("District Mobile Can not be blank");
                                            return false;
                                            document.getElementById("<%=txtMobileNo1.ClientID%>").focus();
                                        }
                                        //Bhavin
                                        if (document.getElementById('<%=txtMobileNo1.ClientID%>').value.length != 10) {
                                            alert("District Excise mobile no not valid");
                                            return false;
                                            document.getElementById("<%=txtMobileNo1.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtMobileNo2.ClientID%>').value.length != 10) {
                                            alert("District magistrate mobile no not valid");
                                            return false;
                                            document.getElementById("<%=txtMobileNo2.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtMobileNo3.ClientID%>').value.length != 10) {
                                            alert("Excise Commisioner mobile no not valid");
                                            return false;
                                            document.getElementById("<%=txtMobileNo3.ClientID%>").focus();
                                        }
                                        //End 
                                    }

                                    function updateLabels() {
                                        var mobileNo = "spnMobileNo";
                                        var emailId = "spnEmailId";
                                        var textEmailId = '<%=txtEmailId1.ClientID%>';
                                        var textMobileNo = '<%=txtMobileNo1.ClientID%>';
                                        var currentDepartment = $("#" +'<%=ddlDepartmentName.ClientID%>').val();
                                        var mobileNoText = "";
                                        var emailIdText = "";
                                        switch (currentDepartment) {
                                            case "02":
                                                mobileNoText = "Superintendent of police(SP) Mobile No";
                                                emailIdText = "Superintendent of police(SP) Email Id ";
                                                break;
                                            case "03":
                                                mobileNoText = "Excise Officer Incharge Mobile No";
                                                emailIdText = "Excise Officer Incharge Email Id";
                                                break;
                                            default:
                                                mobileNoText = "District Excise Officer Mobile No";
                                                emailIdText = "District Excise Officer Email Id";
                                                break;
                                        }

                                        $("#" + mobileNo).text(mobileNoText);
                                        $("#" + emailId).text(emailIdText);
                                        $("#" + textMobileNo).attr("data-original-title", mobileNoText);
                                        $("#" + textEmailId).attr("data-original-title", emailIdText);
                                    }

                                    $(function () {
                                        setTimeout(function () {
                                            $("#" + '<%=ddlDepartmentName.ClientID%>').on("change", function () {
                                                updateLabels();
                                            });
                                            updateLabels();
                                        }, 200);
                                    });
                                </script>

                            </head>
                            <body>
                                <div>
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click">
                                            <i class="fa fa-list ">SHOW RECORD LIST</i>
                                        </asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>District Users Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>District Name</label><br />

                                                <asp:DropDownList ID="ddlDistrictNames" Height="30px" Width="250px" runat="server" data-toggle="tooltip" data-placement="right"
                                                    title="District Name" CssClass="form-control" Style="" DataTextField="Value" DataValueField="Key">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                                <input type="hidden" id="hdnDistrictName">
                                                <span id="errorDistrictName" style="color: red; font: bold; display: none; text-align: right; margin-left: 160px;" data-toggle="tooltip"
                                                    data-placement="right" title="State Name">Please Select District Name...</span>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;">
                                                    <span style="color: red">*       
                                                    </span>Department Name</label>
                                                <br />
                                                <asp:DropDownList ID="ddlDepartmentName" Height="30px" Width="250px" runat="server" data-toggle="tooltip" data-placement="right"
                                                    title="Department Name" CssClass="form-control" Style="" DataTextField="Value" DataValueField="Key">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                </asp:DropDownList>

                                                <input type="hidden" id="hdnDepartmentName">
                                                <span id="errorDepartmentName" style="color: red; font: bold; display: none; text-align: right; margin-left: 160px;" data-toggle="tooltip"
                                                    data-placement="right" title="State Name">Please Select Department Name...</span>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Full Name</label><br />
                                                <asp:TextBox ID="txtFullName" Style="width: 250px" AutoComplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Full Name" class="form-control capitalize" runat="server">
                                                </asp:TextBox>

                                            </div>

                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> User Name</label><br />
                                                <asp:TextBox ID="txtUserName" Style="width: 250px" AutoComplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="User Name" class="form-control" runat="server">
                                                </asp:TextBox>

                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Password</label><br />
                                                <asp:TextBox ID="txtPassword" Style="width: 250px" AutoComplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Password" class="form-control" runat="server">
                                                </asp:TextBox>

                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;">
                                                    <span style="color: red">*</span>
                                                    <span id="spnEmailId">District Excise Officer Email Id  </span>
                                                </label>
                                                <br />
                                                <input type="email" id="txtEmailId1" maxlength="200" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="District Excise Officer Email Id " class="form-control" runat="server" />

                                            </div>

                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;">
                                                    <span style="color: red">*</span>
                                                    District Magistrate Email Id
                                                </label>
                                                <br />
                                                <input type="email" id="txtEmailId2" maxlength="200" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="District Magistrate Email Id" class="form-control" runat="server" />
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;">
                                                    <span style="color: red">*</span>
                                                    Exercise Commissioner Email Id
                                                </label>
                                                <br />
                                                <input type="email" id="txtEmailId3" maxlength="200" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Exercise Commissioner Email Id" class="form-control" runat="server" />
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;">
                                                    <span style="color: red">*</span>
                                                    <span id="spnMobileNo">District Excise Officer Mobile No</span>
                                                </label>
                                                <br />
                                                <input type="number" max="9999999999" id="txtMobileNo1" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="District Excise Officer Mobile No" class="form-control" runat="server" />

                                            </div>

                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> District magistrate Mobile No</label><br />
                                                <input type="number" max="9999999999" id="txtMobileNo2" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="District magistrate Mobile No" class="form-control" runat="server" />

                                            </div>

                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Excise Commisioner mobile No</label><br />
                                                <input type="number" max="9999999999" id="txtMobileNo3" style="width: 250px" autocomplete="off" data-toggle="tooltip" data-placement="right"
                                                    title="Excise Commisioner mobile No" class="form-control" runat="server" />

                                            </div>

                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="javascript:return validationMsg()" />
                                                <input ID="btnCancel"  type="reset" value="Cancel" class="btn btn-danger" OnClick="location.href='DistrictUserList.aspx';" />
                                                <%--<asp:Button ID="Button1" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />--%>
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
