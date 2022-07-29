<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Daily_Dairy_Otherthan_Raid.aspx.cs" Inherits="UserMgmt.Dairy_Daily_Otherthan_Raid" %>

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
                                <title>Daily Dairy Entry Otherthan Raid</title>
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
                                    function blockAllChar(e) {
                                        var k;
                                        document.all ? k = e.keyCode : k = e.which;
                                        return false;
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtraiddate.ClientID%>').value == '') {
                                           alert("Select Raid Date!");
                                           document.getElementById("<% =txtraiddate.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlIntelligence.ClientID%>').value == 'Select') {
                                           alert("Select Intelligence!");
                                           document.getElementById("<% =ddlIntelligence.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddPetrolling.ClientID%>').value == 'Select') {
                                           alert("Select Patrolling!");
                                           document.getElementById("<% =ddPetrolling.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlVehiclecheck.ClientID%>').value == 'Select') {
                                           alert("Select Vehicle Check!");
                                           document.getElementById("<% =ddlVehiclecheck.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlliquordestruction.ClientID%>').value == 'Select') {
                                           alert("Select Liquordestruction!");
                                           document.getElementById("<% =ddlliquordestruction.ClientID%>").focus();
                                           return false;
                                       }

                                       if (document.getElementById('<%=ddlliquordestruction.ClientID%>').value == 'Yes') {

                                           if (document.getElementById('<%=ddlUOM.ClientID%>').value == 'Select') {
                                               alert("select UOM");
                                               document.getElementById("<%=ddlUOM.ClientID%>").focus();
                                                   return false;
                                               }
                                               if (document.getElementById('<%=txtQuantity.ClientID%>').value == '') {
                                               alert("Enter Quantity");
                                               document.getElementById("<% =txtQuantity.ClientID%>").focus();
                                                       return false;
                                                   }
                                               }
                                               if (document.getElementById('<%=ddlwitnessappearance.ClientID%>').value == 'Select') {
                                           alert("Select Witness Appearance!");
                                           document.getElementById("<% =ddlwitnessappearance.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddOthers.ClientID%>').value == 'Select') {
                                           alert("Select Others");
                                           document.getElementById("<% =ddOthers.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddOthers.ClientID%>').value == 'Yes') {

                                           if (document.getElementById('<%=txtrecovery.ClientID%>').value == '') {
                                               alert("Enter Other Recovery");
                                               document.getElementById("<% =txtrecovery.ClientID%>").focus();
                                               return false;
                                           }
                                       }

                                       <%-- if (document.getElementById('<%=txtMeeting.ClientID%>').value == '') {
                                               alert("Enter Meeting");
                                               document.getElementById("<% =txtMeeting.ClientID%>").focus();
                                                       return false;
                                                   }--%>

                                   }
                                </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li>
                                        <asp:LinkButton ID="DDER" OnClick="DDER_Click" runat="server"><span style="color:#fff;font-size:14px;">Raid Entry</span></asp:LinkButton></li>
                                    <li class="active">
                                        <asp:LinkButton ID="DDEOR" OnClick="DDEOR_Click" runat="server"><span style="color:#fff;font-size:14px;">Other than Raid Entry</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="EVT" OnClick="EVT_Click" Visible="false" runat="server"><span style="color:#fff;font-size:14px;">Events</span></asp:LinkButton></li>
                                </ul>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Daily Dairy Entry Otherthan Raid</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div class="clearfix"></div>

                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span>Name of Officer</label>
                                    <br />
                                    <asp:TextBox ID="txtnameofofficer" CssClass="form-control" Enabled="false" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("user_name") %>' title="Name of Officer"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Designation</label>
                                    <br />
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" Enabled="false" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                </div>


                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span>District</label>
                                    <br />
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false" data-toggle="tooltip" data-placement="right" title="District"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                    <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Mobile No</label>
                                    <br />
                                    <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" Enabled="false" data-toggle="tooltip" data-placement="right" title="Mobile No"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Raid Date</label><br />
                                    <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtraiddate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                    <asp:TextBox ID="txtraiddate" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Raid Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Intelligence Gathering</label>
                                    <br />
                                    <asp:DropDownList ID="ddlIntelligence" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Recovery">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Patrolling</label>
                                    <br />
                                    <asp:DropDownList ID="ddPetrolling" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Recovery">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Vehicle Check</label>
                                    <br />
                                    <asp:DropDownList ID="ddlVehiclecheck" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Recovery">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Liquor Destruction</label>
                                    <br />
                                    <asp:DropDownList ID="ddlliquordestruction" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Liquor Destruction" AutoPostBack="true" OnSelectedIndexChanged="ddlliquordestruction_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div id="liqdiv" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>UOM</label>
                                        <br />
                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="UOM">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" id="Arrst" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Quantity"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Witness Appearance in Court</label>
                                    <br />
                                    <asp:DropDownList ID="ddlwitnessappearance" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Recovery">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Others</label>
                                    <br />
                                    <asp:DropDownList ID="ddOthers" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Recovery">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span>Other Recovery</label>
                                    <br />

                                    <asp:TextBox ID="txtrecovery" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Volumes in No"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span>Meeting</label>
                                    <br />

                                    <asp:TextBox ID="txtMeeting" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Meeting"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div id="docs" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                        <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline" id="dn" runat="server"><span style="color: red"></span>Document Name</label><br />
                                        <asp:TextBox ID="txtDiscription" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Name"></asp:TextBox>
                                        <span>
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                        </span>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div id="dummytable" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                    <table class="table table-striped responsive-utilities jambo_table" id="membertable">
                                        <thead>
                                            <tr>
                                                <th>File Name</th>
                                                <th>Description</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="resourcetable">
                                        </tbody>

                                    </table>
                                </div>

                                <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                    <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" EmptyDataText="No Attached Files"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Visible="true" Text='<%#Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName" runat="server" Visible="true" Text='<%#Eval("Doc_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscriptione" runat="server" Visible="true" Text='<%#Eval("Discription") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FilePath" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFilePath" runat="server" Visible="true" Text='<%#Eval("Doc_path") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldoc_id" runat="server" Visible="true" Text='<%#Eval("doc_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluser_id" runat="server" Visible="true" Text='<%#Eval("user_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_name") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                    &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_name") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                    <asp:HiddenField ID="hdocsfrom" runat="server" />
                                    <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
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

