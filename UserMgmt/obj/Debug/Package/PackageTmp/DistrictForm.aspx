<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DistrictForm.aspx.cs" Inherits="UserMgmt.DistrictForm" %>

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
                                <%--   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">--%>
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddStatenames.ClientID%>').value == 'Select') {
                                            alert("Select  State Name");
                                            return false;
                                            document.getElementById("<% =ddStatenames.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=ddDivisions.ClientID%>').value == 'Select') {
                                            alert("Select  Division Name");
                                            return false;
                                            document.getElementById("<% =ddDivisions.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtDistrictName.ClientID%>').value == '') {
                                            alert("Enter  District Name");
                                            return false;
                                            document.getElementById("<% =txtDistrictName.ClientID%>").focus();
                                        }
                                        if (document.getElementById('<%=txtDistrictCode.ClientID%>').value == '') {
                                            alert("Enter  District Code");
                                            return false;
                                            document.getElementById("<% =txtDistrictCode.ClientID%>").focus();
                                        }
                                    }
                                </script>
                                <script>
                                    function chkDuplicateDistrictName() {
                                        debugger;
                                        var email = $('#BodyContent_txtDistrictName').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtDistrictName').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DistrictForm.aspx/chkDuplicateDistrictName",
                                            data: '{districtname:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("District Name is already exists");
                                                    $('#BodyContent_txtDistrictName').val("");
                                                    $('#BodyContent_txtDistrictName').focus();
                                                }

                                            }
                                        });
                                    }
                                    function chkDuplicateDistrictCode() {

                                        var email = $('#BodyContent_txtDistrictCode').val();
                                        if ($('#BodyContent_txtDistrictCode').val().length != 2) {
                                            alert("District Code Lenth Should be 2");
                                            $('#BodyContent_txtDistrictCode').val("");
                                            $('#BodyContent_txtDistrictCode').focus();
                                        }
                                        else
                                            var jsondata = JSON.stringify($('#BodyContent_txtDistrictCode').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DistrictForm.aspx/chkDuplicateDistrictCode",
                                            data: '{districtcode:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("District Code is already exists");
                                                    $('#BodyContent_txtDistrictCode').val("");
                                                    $('#BodyContent_txtDistrictCode').focus();
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
                                        <li class="active">
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
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>District Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                              <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>State Name</label> <br />
                                                    
                                                        <asp:DropDownList ID="ddStatenames" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="State Name" OnSelectedIndexChanged="ddStatenames_SelectedIndexChanged" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                            <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                        <%--<input type="hidden" id="stateName">
                                                    <span id="error2" style="color: red; font: bold; display: none; text-align: right;margin-left: 160px;" data-toggle="tooltip" data-placement="right" title="State Name">Please enter State Name...</span>--%>
                                                   </div>
                                             <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*       </span>Division Name</label><br />
                                                  
                                                        <asp:DropDownList ID="ddDivisions" Height="30px" Width="250px" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Division Name" CssClass="form-control" Style="">
                                                            <%-- <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>--%>
                                                            <%-- <asp:ListItem Enabled="true" Text="Bihar" Value="1"></asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                        <%-- <input type="hidden" id="stateName">
                                                    <span id="error2" style="color: red; font: bold; display: none; text-align: right;margin-left: 160px;" data-toggle="tooltip" data-placement="right" title="State Name">Please enter State Name...</span>--%>
                                                   </div>
                                             <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> District Name</label>    <br />
                                                  
                                                        <asp:TextBox ID="txtDistrictName" AutoComplete="off" onchange="chkDuplicateDistrictName();" style="text-transform:capitalize" runat="server" Width="250px" Height="30px" class="form-control capitalize" 
                                                            data-toggle="tooltip" data-placement="right" title="District Name"></asp:TextBox>
</div>
                                             <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> District Code</label><br />
                                                  
                                                        <asp:TextBox ID="txtDistrictCode" onchange="chkDuplicateDistrictCode();" AutoComplete="off"  runat="server" style="text-transform:uppercase" class="form-control capitalize" MaxLength="2"
                                                            data-toggle="tooltip" data-placement="right" title="District Code"></asp:TextBox>
                                                        <%-- <input type="text" id="txtDistrictCode" AutoPostBack="true" runat="server" onserverchange="txtDistrictCode_ServerChange"
                                                             data-toggle="tooltip" data-placement="right" 
                                                             title="District Code" class="form-control capitalize"  >--%>
                                                    
                                                 </div>
                                                  <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                               <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Chemical Lab District Id</label>    <br />
                                                  
                                                        <asp:TextBox ID="txtlabid" AutoComplete="off"  style="text-transform:capitalize" runat="server" Width="250px" Height="30px" class="form-control capitalize" onkeypress="return onlyDotsAndNumbers(this,event);"
                                                            data-toggle="tooltip" data-placement="right" title="Chemical Lab District Id"></asp:TextBox>
</div>

                                            <asp:HiddenField ID="lblStatecode" runat="server" />
                                            <asp:HiddenField ID="lblDivisionCode" runat="server" />
                                            
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <%--<div class="col-md-9 col-sm-9 col-xs-12">--%>
                                                 <br />
                                                    <asp:LinkButton Text="Add" ID="btnAdd" CssClass="btn btn-primary"  runat="server" OnClick="btnAdd_Click" OnClientClick="javascript:return validationMsg()"><i class="fa fa-plus-circle" style="font-size: 15px"></i> ADD
                                                                                    
                                                    </asp:LinkButton>
                                                <%--</div>--%>
                                            </div>

                                            <p>&nbsp;</p>
                                               <p>&nbsp;</p>
                                               <p>&nbsp;</p>
                                               <p>&nbsp;</p>  
                                             <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <div >
                                                <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="District Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistrictName" runat="server" Visible="true" Text='<%#Eval("DistrictName") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distric Code" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistricCode" runat="server" Visible="true" Text='<%#Eval("District_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Division Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisionName" runat="server" Visible="true" Text='<%#Eval("Division_Name") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStateName" runat="server" Visible="true" Text='<%#Eval("State_Name") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Chemical Lab District Id" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllabid" runat="server" Visible="true" Text='<%#Eval("tab_district_id") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStateCode" runat="server" Visible="true" Text='<%#Eval("State_Code") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Division Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisionCode" runat="server" Visible="true" Text='<%#Eval("Division_Code") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="Remove" ID="btnRemove" CssClass="myButton2" runat="server" OnClick="btnRemove_Click" CommandName="Remove"><i class="fa fa-remove">
                                                                                    </i> 
                                                                </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                    <PagerStyle BackColor="#26B8B8" HorizontalAlign="Right" ForeColor="#ECF0F1" />

                                                    <RowStyle BackColor="Window"></RowStyle>
                                                </asp:GridView>
                                            </div>

                                            <div>
                                                <p>&nbsp;</p>
                                                <div class="clearfix"></div>
                                            </div>
                                            <p>&nbsp;</p>

                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <%-- <div class="col-md-9 col-sm-9 col-xs-9">--%>
                                                <asp:HiddenField ID="txtid" runat="server" />
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" />
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
