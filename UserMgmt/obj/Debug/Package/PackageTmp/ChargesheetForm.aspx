<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="ChargesheetForm.aspx.cs" Inherits="UserMgmt.ChargesheetForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title>Chargesheet Form</title>
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
    function blockAllChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false; 
        }
    </script>
                                 <script type="text/javascript">
    function blockSpecialChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    function ShowHideDiv() {
        var ddlModeDisposal = document.getElementById("BodyContent_NestedBodyContent_ddlModeDisposal");

        debugger;;
        if (ddlModeDisposal.value == "4") {
            $("#BodyContent_NestedBodyContent_court").show();


        }
        else {
            $("#BodyContent_NestedBodyContent_court").hide();

        }


    }
    function ShowHideDiv1() {
        var ddvehicle = document.getElementById("BodyContent_NestedBodyContent_ddvehicle");
        debugger;;
        if (ddvehicle.value == "Yes") {
            $("#BodyContent_NestedBodyContent_fsldd").show();

        }
        else {
            $("#BodyContent_NestedBodyContent_fsldd").hide();

        }


    }
    function ShowHideDiv2() {
        var ddliquore = document.getElementById("BodyContent_NestedBodyContent_ddliquore");
        debugger;;
        if (ddliquore.value == "Yes") {
            $("#BodyContent_NestedBodyContent_liqur").show();

        }
        else {
            $("#BodyContent_NestedBodyContent_liqur").hide();

        }


    }
    </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;;
                                        if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                            alert("Enter FIR details!");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtMemoofEvidence.ClientID%>').value == '') {
                                            alert("Enter Memo of Evidence");
                                            document.getElementById("<% =txtMemoofEvidence.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtSeizedPropertyKept.ClientID%>').value == '') {
                                            alert("Enter Place where kept");
                                            document.getElementById("<% =txtSeizedPropertyKept.ClientID%>").focus();
                                             return false;
                                         }
                                         if (document.getElementById('<%=txtDateProductionSeizerProperty.ClientID%>').value == '') {
                                            alert("Enter Date of  Seizer");
                                            document.getElementById("<% =txtDateProductionSeizerProperty.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlModeDisposal.ClientID%>').value == 'Select') {
                                            alert("Select Mode Disposal");
                                            document.getElementById("<% =ddlModeDisposal.ClientID%>").focus();
                                             return false;
                                        }
                                      
                                        if(document.getElementById('<%=ddlModeDisposal.ClientID%>').value == '4')
                                        {
                                           
                                            if (document.getElementById('<%=txtCourtorderDate.ClientID%>').value == '')
                                            {
                                                alert("Select Court order Date");
                                                document.getElementById("<% =txtCourtorderDate.ClientID%>").focus();
                                                return false;
                                            }
                                         
                                        }
                                         if (document.getElementById('<%=ddvehicle.ClientID%>').value == 'Select') {
                                             alert("Select Vehicle Verification");
                                            document.getElementById("<% =ddlModeDisposal.ClientID%>").focus();
                                             return false;
                                        }
                                        if(document.getElementById('<%=ddvehicle.ClientID%>').value == 'Yes')
                                        {
                                            
                                            if (document.getElementById('<%=ddvehiclefsl.ClientID%>').value == 'Select')
                                            {
                                                alert("Select FSL Place");
                                                document.getElementById("<% =ddvehiclefsl.ClientID%>").focus();
                                                return false;
                                            }
                                            
                                        }
                                       
                                        if (document.getElementById('<%=ddliquore.ClientID%>').value == 'Select') {
                                            alert("Select Liquore Test");
                                            document.getElementById("<% =ddliquore.ClientID%>").focus();
                                             return false;
                                        }
                                        if(document.getElementById('<%=ddliquore.ClientID%>').value == 'Yes')
                                        {
                                            
                                            if (document.getElementById('<%=ddliquorefsl.ClientID%>').value == 'Select')
                                            {
                                                alert("Select Liquor Test Type");
                                                document.getElementById("<% =ddliquorefsl.ClientID%>").focus();
                                                return false;
                                            }
                                           
                                        }
                                      
                                    }
                                    $(document).ready(function () {
                                        debugger;;

                                        ShowHideDiv();
                                        ShowHideDiv1();
                                        ShowHideDiv2();

                                    });
                                   
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function CheckDiscription() {
                                        debugger;
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                            alert("Please Attach file");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                            alert("Enter Discription");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                                <script type="text/javascript">

                                    function validateExtraDocuments() {
                                        var fileInput = document.getElementById('<%= idupDocument.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }
                                        var uploadControl = document.getElementById('<%= idupDocument.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idupDocument.ClientID %>').value = "";
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                </script>
                                           
                           
                            </head>
                            <body>
                                <%--<div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnChargesheetFiling" OnClick="btnChargesheetFiling_Click">
                                        <span style="color: #fff; font-size: 14px;">Chargesheet</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnADDAccusedDetails" OnClick="btnADDAccusedDetails_Click">
                                        <span style="color: #fff; font-size: 14px;">Add Accused Details</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>--%>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Chargesheet Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                         </ContentTemplate>
                                    </asp:UpdatePanel>     
                                <a>
                                 <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="btnSeizure_Click"  BorderStyle="Outset"> Seizure</asp:LinkButton>
                                </a>
                               <%-- <a>
                                 <asp:LinkButton runat="server" ID="btnFIR" Height="100%" Width="12%" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View FIR" OnClick="btnFIR_Click"  BorderStyle="Outset"> FIR</asp:LinkButton>
                                </a>--%>
                                        <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                   <%-- <asp:UpdatePanel runat="server">--%>
                                      <%--  <ContentTemplate>--%>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red">*</span>PR / FIR No</label>
                                                <br />                                                
                                                <asp:TextBox ID="txtPRFIRNo" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                                <asp:TextBox ID="txtfirdate"  AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>' Visible="false"  title="PR / FIR No"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline ">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Memo of Evidence</label>
                                                <br />
                                                <asp:TextBox ID="txtMemoofEvidence" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" title="Memo of Evidence"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12  form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red">*</span>Place where kept</label>
                                                <br />
                                                <asp:TextBox ID="txtSeizedPropertyKept" AutoComplete="off" CssClass="form-control" runat="server" Width="80%" data-toggle="tooltip" data-placement="right" title="Seized Property Kept"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtDateProductionSeizerProperty" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDateProductionSeizerProperty" onchange="chkDuplicateDates();"  data-toggle="tooltip" data-placement="right" title="Date of Production of Seized Property" ReadOnly="false" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Mode of disposal</label>
                                                <br />
                                                <asp:DropDownList ID="ddlModeDisposal" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mode of Disposal of Seized Property" onchange ="ShowHideDiv()">
                                                    <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Text="Confiscated and Kept at Malkhana" Value="1">Confiscated and Kept at Malkhana</asp:ListItem>
                                                    <asp:ListItem Text="Confiscated and destroyed" Value="5">Confiscated and destroyed</asp:ListItem>
                                                    <asp:ListItem Text="Kept at Malkhana" Value="3">Kept at Malkhana</asp:ListItem>
                                                    <asp:ListItem Text="Released by Court Order" Value="4">Released by Court Order</asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="2">N/A</asp:ListItem>
                                                   <%-- <asp:ListItem Text="Confiscated and destroyed" Value="6">Confiscated and destroyed</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div runat="server" id="court" style="display:none" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Court Order Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtCourtorderDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtCourtorderDate" onchange="chkDuplicateDates();" data-toggle="tooltip"  data-placement="right" title="Court Order Date" class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                       <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Vehicle Verification</label>
                                                <br />
                                                <asp:DropDownList ID="ddvehicle" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mode of Disposal of Seized Property" onchange ="ShowHideDiv1()">
                                                    <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    <div id="fsldd" runat="server" style="display: none" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>FSL Place</label>
                                                <br />
                                                <asp:DropDownList ID="ddvehiclefsl" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mode of Disposal of Seized Property" >
                                                    <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Text="FSL Patna" Value="PA">FSL Patna</asp:ListItem>
                                                    <asp:ListItem Text="FSL Muzzafarpur" Value="MU">FSL Muzzafarpur</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                      <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Liquor Test</label>
                                                <br />
                                                <asp:DropDownList ID="ddliquore" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mode of Disposal of Seized Property" onchange ="ShowHideDiv2()">
                                                    <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    <div id="liqur" runat="server" style="display: none" class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Liquor Test Type</label>
                                                <br />
                                                <asp:DropDownList ID="ddliquorefsl" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Mode of Disposal of Seized Property" >
                                                    <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Text="Chemist" Value="CH">Chemist</asp:ListItem>
                                                    <asp:ListItem Text="FSL" Value="FSL">FSL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-6 col-sm-12 col-xs-12 ">
                                                <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Remarks</label>
                                                <br />
                                                <asp:TextBox ID="txtRemarks" AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Remarks" Width="90.8%" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div id="docs" runat="server">
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                                    <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                    <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                                    <asp:TextBox ID="txtDiscription" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                                    <span>
                                                        <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
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
                                                <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
                                                    HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                    <Columns>
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
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' OnClick="DownloadFile" CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                                                &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" OnClick="btnRemove_Click" ImageUrl="~/img/delete.gif" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                    <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                                    <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                </asp:GridView>
                                                <div class="clearfix"></div>
                                                <p>&nbsp;</p>
                                               <%-- <div style="height: 8%; background-color: #26b8b8;">
                                                    <span style="font-size: small; color: white; margin-left: 40%">Accused List</span>
                                                </div>
                                                <div class="clearfix"></div>
                                                <p>&nbsp;</p>--%>
                                               <%-- <div class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                                    <div style="width: 600px; overflow: auto;">
                                                        <asp:GridView ID="grdAccusedDetailsListView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records"
                                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chSelect" runat="server" Checked='<%# bool.Parse(Eval("chargesheet_status").ToString() == "Y" ? "True": "False" )%>'  />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Accused ID" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccusedID" runat="server" Visible="true" Text='<%#Eval("seizure_accused_details_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAppearanceName" runat="server" Visible="true" Text='<%#Eval("accusedname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:TemplateField HeaderText="FIR Filed" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFIRFiled" runat="server" Visible="true" Text='<%#Eval("") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                        </asp:GridView>
                                                    </div>
                                                </div>--%>
                                       
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
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
        </div></div> 
    
</asp:Content>
