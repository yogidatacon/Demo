<%@ page language="C#" masterpagefile="~/ReceivingSectionMaster.Master" autoeventwireup="true" codebehind="ReceivingSectionForm.aspx.cs" inherits="UserMgmt.ReceivingSectionForm" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        .rbl input[type="radio"] {
            margin-left: 20px;
            margin-right: 1px;
        }
    </style>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>  
   <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>  
   <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script> 
  
    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };


        function showAlert() {
          alert("Record already Exist.");
       }

        function quantityReceivedCommonValidation() {
            debugger;
            if (document.getElementById('<%=ddlSealed.ClientID%>').value == 'True' &&
                $.trim(document.getElementById('<%=txtSealed.ClientID%>').value) == '') {
                alert("Enter Sealed Status");
                return false;
                document.getElementById("<%=txtCourtOrder.ClientID%>").focus();
            }
            return true;
        }
        function validationMsg() {
            //alert("Test");
            //debugger;
            //Bhavin
            <%--if ($.trim(document.getElementById('<%=txtlettername.ClientID%>').value) == '') {
                alert("Enter Letter No");
                return false;
                document.getElementById("<% =txtlettername.ClientID%>").focus();
            }
            if (document.getElementById('<%=txtLetterDate.ClientID%>').value == '') {
                alert("Select Letter Date");
                return false;
                document.getElementById("<%=txtLetterDate.ClientID%>").focus();
            }--%>
            //End 
            if (document.getElementById('<%=rdType.ClientID%>' + "_0").checked) {
                if (document.getElementById('<%=ddTypePolice.ClientID%>').value == 'Select') {
                    alert("Select Type");
                    return false;
                    document.getElementById("<%=ddTypePolice.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlDistrictPolice.ClientID%>').value == 'Select') {
                    alert("Select District");
                    return false;
                    document.getElementById("<%=ddlDistrictPolice.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddThana.ClientID%>').value == 'Select') {
                    alert("Select Thana");
                    return false;
                    document.getElementById("<%=ddThana.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlPoliceOfficerDesignation.ClientID%>').value == 'Select') {
                    alert("Select Designation");
                    return false;
                    document.getElementById("<%=ddlPoliceOfficerDesignation.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtFIRNo.ClientID%>').value == '') {
                    alert("Enter FIR No");
                    return false;
                    document.getElementById("<%=txtFIRNo.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtDateOfInstitutionOfFIR.ClientID%>').value == '') {
                    alert("Select Date of Institution of FIR");
                    return false;
                    document.getElementById("<%=txtDateOfInstitutionOfFIR.ClientID%>").focus();
                }

                if (document.getElementById('<%=ddlCourtOrder.ClientID%>').value == 'True' &&
                    $.trim(document.getElementById('<%=txtCourtOrder.ClientID%>').value) == '') {
                    alert("Enter Court Order");
                    return false;
                    document.getElementById("<%=txtCourtOrder.ClientID%>").focus();
                }

                if (document.getElementById('<%=ddlSeizureList.ClientID%>').value == 'True' &&
                    $.trim(document.getElementById('<%=txtSeizureList.ClientID%>').value) == '') {
                    alert("Enter Seizure List");
                    return false;
                    document.getElementById("<%=txtSeizureList.ClientID%>").focus();
                }
            }
            else if (document.getElementById('<%=rdType.ClientID%>' + "_1").checked) {
                debugger;
                if (document.getElementById('<%=ddlTypeExcise.ClientID%>').value == 'Select') {
                    alert("Select Type");
                    return false;
                    document.getElementById("<%=ddlTypeExcise.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlExciseDistrict.ClientID%>').value == 'Select') {
                    alert("Select District");
                    return false;
                    document.getElementById("<%=ddlExciseDistrict.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlExciseDesignation.ClientID%>').value == 'Select') {
                    alert("Select Designation");
                    return false;
                    document.getElementById("<%=ddlExciseDesignation.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtCaseNO.ClientID%>').value == '') {
                    alert("Enter Case No");
                    return false;
                    document.getElementById("<%=txtCaseNO.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtExciseDateOfInstitutionOfFIR.ClientID%>').value == '') {
                    alert("Select Date of Institution of FIR");
                    return false;
                    document.getElementById("<%=txtExciseDateOfInstitutionOfFIR.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtExiseRemark.ClientID%>').value == '') {
                    alert("Enter Remark");
                    return false;
                    document.getElementById("<%=txtExiseRemark.ClientID%>").focus();
                }
                //Bhavin
                <%--if (document.getElementById('<%=txtAddressOfMan.ClientID%>').value == '') {
                    alert("Enter Address of the Manufacturer");
                    return false;
                    document.getElementById("<%=txtAddressOfMan.ClientID%>").focus();
                }--%>
                //End
                if (document.getElementById('<%=txtPNRNO.ClientID%>').value == '') {
                    alert("Enter PNR No");
                    return false;
                    document.getElementById("<%=txtPNRNO.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtState.ClientID%>').value == '') {
                    alert("Enter State V/S");
                    return false;
                    document.getElementById("<%=txtState.ClientID%>").focus();
                }
            }
            else {
                if (document.getElementById('<%=dllDistilleryName.ClientID%>').value == 'Select') {
                    alert("Select Distillery");
                    return false;
                    document.getElementById("<%=dllDistilleryName.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlDistillerDistrict.ClientID%>').value == 'Select') {
                    alert("Select District");
                    return false;
                    document.getElementById("<%=ddlDistillerDistrict.ClientID%>").focus();
                }
                if (document.getElementById('<%=ddlOfficerDesignation.ClientID%>').value == 'Select') {
                    alert("Select Designation");
                    return false;
                    document.getElementById("<%=ddlOfficerDesignation.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtVatNo.ClientID%>').value == '') {
                    alert("Enter VAT No");
                    return false;
                    document.getElementById("<%=txtVatNo.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtDenaturedDate.ClientID%>').value == '') {
                    alert("Select De Natured Date");
                    return false;
                    document.getElementById("<%=txtDenaturedDate.ClientID%>").focus();
                }
                if (document.getElementById('<%=txtRemark.ClientID%>').value == '') {
                    alert("Enter Remark");
                    return false;
                    document.getElementById("<%=txtRemark.ClientID%>").focus();
                }
            }

            var result = quantityReceivedCommonValidation();
            if (result) {
                debugger;
                //Bhavin
                <%--if (document.getElementById("<%=hdnIsSavedAlready.ClientID%>").value != "True") {
                    var isSaved = confirm("If Yes, Record will be saved in 'Saved' status else 'Draft' status");
                    //alert(isSaved);
                    document.getElementById("<%=hdnIsSaveOrDraft.ClientID%>").value = isSaved;
                }--%>
                return true;
            }
            else {
                return false;
            }
            return true;
        }

        function quantReceivedValidation() {
            if (document.getElementById('<%=ddlTypeOfLiquor.ClientID%>').value == 'Select') {
                alert("Select Type");
                return false;
                document.getElementById("<%=ddlTypeOfLiquor.ClientID%>").focus();
            }
            if (document.getElementById('<%=ddlQuantitySubType.ClientID%>').value == 'Select') {
                alert("Select Subtype");
                return false;
                document.getElementById("<%=ddlQuantitySubType.ClientID%>").focus();
            }
            if (document.getElementById('<%=ddlQuantitySize.ClientID%>').value == 'Select') {
                alert("Select Size");
                return false;
                document.getElementById("<%=ddlQuantitySize.ClientID%>").focus();
            }
            if (document.getElementById('<%=txtQuantity.ClientID%>').value == '') {
                alert("Enter Quantity");
                return false;
                document.getElementById("<%=txtQuantity.ClientID%>").focus();
            }
            if (document.getElementById('<%=ddlBrandName.ClientID%>').value == 'Select') {
                alert("Select Brand");
                return false;
                document.getElementById("<%=ddlBrandName.ClientID%>").focus();
            }
            if (document.getElementById('<%=txtBatchNo.ClientID%>').value == '') {
                alert("Enter Batch No.");
                return false;
                document.getElementById("<%=txtBatchNo.ClientID%>").focus();
            }
            if (document.getElementById('<%=txtAddressOfMan.ClientID%>').value == '') {
                alert("Enter Address");
                return false;
                document.getElementById("<%=txtAddressOfMan.ClientID%>").focus();
            }
            if (document.getElementById('<%=ddlCompactor.ClientID%>').value == 'Select') {
                alert("Select CompactorID");
                return false;
                document.getElementById("<%=ddlCompactor.ClientID%>").focus();
            }
        }
    </script>
    <script>
        $(function() {
            $("#<%= txtLetterDate.ClientID %>").datepicker({
                format: "dd-mm-yy"
            });
            $("#<%= txtDateOfInstitutionOfFIR.ClientID %>").datepicker({
                format: "dd-mm-yy"
            });
            $("#<%= txtExciseDateOfInstitutionOfFIR.ClientID %>").datepicker({
                format: "dd-mm-yy"
            });
            $("#<%= txtDenaturedDate.ClientID %>").datepicker({
                format: "dd-mm-yy"
            });
        });
    </script>
    <div role="main">
        <br />
        <div>
        <div class="">
            <div class="row top_tiles">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="col-md-3 col-sm-3 col-xs-3 form-inline">
                    <label class="control-label" style="display: inline">Date:</label>
                    <asp:Label ID="lblTodayDate" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-4 form-inline">
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Letter No:</label>
                    <asp:TextBox ID="txtlettername" AutoComplete="off" runat="server" Height="30px" Width="100px" data-toggle="tooltip" data-placement="right"
                        title="Letter Number" CssClass="form-control" MaxLength="50">
                    </asp:TextBox>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-4 form-inline">
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Letter Date:</label>
                    <asp:TextBox ID="txtLetterDate" Width="50%" data-placement="right" title="Letter Date"
                        class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                    </asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-2 col-xs-2 form-inline" runat="server" id="dvStatus">
                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Status :</label>
                    <label class="control-label" id="lblStatus" runat="server" style="display: inline"></label>
                </div>
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="">
                            <label>Exibit received from:</label>
                            <asp:RadioButtonList ID="rdType" AutoPostBack="true" OnSelectedIndexChanged="rdType_OnSelectedIndexChange"
                                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="rbl">
                                <asp:ListItem Text="Police" Value="police" />
                                <asp:ListItem Text="Excise" Value="excise" />
                                <asp:ListItem Text="Distillery" Value="distillery" />
                            </asp:RadioButtonList>
                        </div>
                        <div>
                            <asp:Panel ID="panPolice" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Police" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Type</label>
                                    <br />
                                    <asp:DropDownList ID="ddTypePolice" runat="server" Height="30px" Width="250px" data-toggle="tooltip" DataValueField="pol_type_id" DataTextField="pol_type_name"
                                        data-placement="right" title="Type" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>District</label>
                                    <br />
                                    <asp:DropDownList ID="ddlDistrictPolice" AutoPostBack="true" runat="server" Height="30px" DataValueField="Key" DataTextField="Value"
                                        Width="250px" data-toggle="tooltip"
                                        data-placement="right" title="District" CssClass="form-control" OnSelectedIndexChanged="ddDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Thana</label><br />
                                    <asp:DropDownList ID="ddThana" runat="server" Height="30px" Width="250px" DataValueField="thana_master_id" DataTextField="thana_name"
                                        data-toggle="tooltip" data-placement="right" title="Thana" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Officer Designation</label>
                                    <br />
                                    <asp:DropDownList ID="ddlPoliceOfficerDesignation" runat="server" Height="30px" Width="250px" DataValueField="pol_desg_id" DataTextField="pol_desg_name"
                                        data-toggle="tooltip" data-placement="right" title="Officer Designation" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>FIR No</label><br />
                                    <asp:TextBox ID="txtFIRNo" AutoComplete="off" runat="server" Height="30px" Width="250px" data-toggle="tooltip"
                                        data-placement="right" title="FIR No" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Date Of Institution Of FIR</label>
                                    <br />
                                    <asp:TextBox ID="txtDateOfInstitutionOfFIR" data-toggle="tooltip" Width="60%" d
                                        ata-placement="right" title="Date Of Institution Of FIR" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panDocumentReceivingChecklist" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Document Receiving Check list" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Court Order</label>
                                    <br />
                                    <asp:DropDownList ID="ddlCourtOrder" runat="server" AutoPostBack="true" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Court Order" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCourtOrder_SelectedIndexChanged">
                                        <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"></label>
                                    <br />
                                    <asp:TextBox ID="txtCourtOrder" AutoComplete="off" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Court Order" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp</p>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>FIR Copy</label>
                                    <br />
                                    <asp:DropDownList ID="ddlFirCopy" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="FIR copy" CssClass="form-control">
                                        <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Seizure List</label>
                                    <br />
                                    <asp:DropDownList ID="ddlSeizureList" runat="server" AutoPostBack="true" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Seizure List" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlSeizureList_SelectedIndexChanged">
                                        <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span></label>
                                    <br />
                                    <asp:TextBox ID="txtSeizureList" AutoComplete="off" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Seizure List" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="panExcise" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Excise" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Type</label>
                                    <br />
                                    <asp:DropDownList ID="ddlTypeExcise" runat="server" Height="30px" Width="250px" DataValueField="excise_type_id" DataTextField="excise_type_name"
                                        data-toggle="tooltip" data-placement="right" title="Type" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>District</label>
                                    <br />
                                    <asp:DropDownList ID="ddlExciseDistrict" AutoPostBack="true" runat="server" Height="30px" Width="250px" DataValueField="Key" DataTextField="Value"
                                        data-toggle="tooltip" data-placement="right" title="District" CssClass="form-control"
                                        OnSelectedIndexChanged="ddDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation</label><br />
                                    <asp:DropDownList ID="ddlExciseDesignation" runat="server" Height="30px" Width="250px" DataValueField="excise_desg_id" DataTextField="excise_desg_name"
                                        data-toggle="tooltip" data-placement="right" title="Designation" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Case No</label><br />
                                    <asp:TextBox ID="txtCaseNO" AutoComplete="off" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Case No" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Date of Institution of case</label>
                                    <br />
                                 
                                    <asp:TextBox ID="txtExciseDateOfInstitutionOfFIR"
                                        data-toggle="tooltip" Width="60%" data-placement="right" title="Date of Institution of case"
                                        class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    
                                </div>
                                <div class="col-md-8 col-sm-6 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remark</label>
                                    <br />
                                    <asp:TextBox ID="txtExiseRemark" AutoComplete="off" runat="server" data-toggle="tooltip" data-placement="right"
                                        title="Remark" CssClass="form-control" MaxLength="100" TextMode="MultiLine">
                                    </asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="panDistillery" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Distillery" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Distillery Name</label>
                                    <br />
                                    <asp:DropDownList ID="dllDistilleryName" runat="server" Height="30px" DataValueField="distillery_code" DataTextField="distillery_name"
                                        Width="250px" data-toggle="tooltip"
                                        data-placement="right" title="Distillery" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>District</label>
                                    <br />
                                    <asp:DropDownList ID="ddlDistillerDistrict" runat="server" Height="30px" Width="250px" DataValueField="Key" DataTextField="Value"
                                        data-toggle="tooltip" data-placement="right" title="District" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label"><span style="color: red">*</span>Officer Designation</label>
                                    <br />
                                    <asp:DropDownList ID="ddlOfficerDesignation" runat="server"
                                        Height="30px" Width="250px" DataValueField="dist_desg_id" DataTextField="dist_desg_name"
                                        data-toggle="tooltip" data-placement="right" title="Officer Designation" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Vat No:</label>
                                    <br />
                                    <asp:TextBox ID="txtVatNo" AutoComplete="off" runat="server" Height="30px" Width="150px"
                                        data-toggle="tooltip" data-placement="right" title="FIR No" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Denatured Date:</label>
                                    <br />
                                    
                                    <asp:TextBox ID="txtDenaturedDate" data-toggle="tooltip" Width="50%" data-placement="right"
                                        title="Denatured Date" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                    </asp:TextBox>
                                    
                                </div>
                                <div class="col-md-8 col-sm-6 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remark</label>
                                    <br />
                                    <asp:TextBox ID="txtRemark" AutoComplete="off" runat="server" data-toggle="tooltip" data-placement="right" title="Remark"
                                        CssClass="form-control" MaxLength="100" TextMode="MultiLine">
                                    </asp:TextBox>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="panSealedStatus" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Sealed Status" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span>Sealed</label>
                                    <br />
                                    <asp:DropDownList ID="ddlSealed" runat="server" AutoPostBack="true" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Sealed" CssClass="form-control" OnSelectedIndexChanged="ddlSealed_SelectedIndexChanged">
                                        <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red"></span></label>
                                    <br />
                                    <asp:TextBox ID="txtSealed" AutoComplete="off" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" data-placement="right" title="Sealed" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <br />
                            </asp:Panel>


                            <asp:Panel ID="exciseDocumentReceivingChecklist" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Document Receiving Check list" style="margin-top: 25px; padding: 10px 17px;">

                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>P.R.No. :</label>
                                    <br />
                                    <asp:TextBox ID="txtPNRNO" AutoComplete="off" runat="server" Height="30px" Width="200px"
                                        data-toggle="tooltip" data-placement="right" title="P.R.No" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>State V/S:</label>
                                    <br />
                                    <asp:TextBox ID="txtState" AutoComplete="off" runat="server" Height="30px" Width="200px"
                                        data-toggle="tooltip" data-placement="right" title="State V/S" CssClass="form-control" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="panQuantityReceived" runat="server" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px"
                                GroupingText="Quantity Received" style="margin-top: 25px; padding: 10px 17px;">
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Type Of Liquor:</label>
                                    <asp:DropDownList ID="ddlTypeOfLiquor" runat="server" Height="30px" Width="250px" DataValueField="type_of_liquor_id" DataTextField="type_of_liquor_name"
                                        OnSelectedIndexChanged="ddlTypofLiquor_Changed" AutoPostBack="true"
                                        data-toggle="tooltip" data-placement="right" title="Type Of Liquor" CssClass="form-control" placeholder="Select">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">

                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Sub Type:</label>
                                    <asp:DropDownList ID="ddlQuantitySubType" runat="server" Height="30px" DataValueField="liquor_sub_type_id" DataTextField="liquor_sub_name"
                                        Width="250px" data-toggle="tooltip"
                                        data-placement="right" title="Sub Type" CssClass="form-control" placeholder="Select">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Size: &nbsp&nbsp&nbsp&nbsp</label>
                                    <asp:DropDownList ID="ddlQuantitySize" runat="server" Height="30px" Width="250px" DataTextField="size_master_name" DataValueField="size_master_id"
                                        data-toggle="tooltip"
                                        data-placement="right" title="Size" CssClass="form-control" placeholder="Select">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-3 col-sm-6 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Quantity:</label>
                                    <asp:TextBox ID="txtQuantity" AutoComplete="off" runat="server" Style="text-transform: capitalize" data-toggle="tooltip"
                                        data-placement="right" title="Quantity" CssClass="form-control" MaxLength="10">
                                    </asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>


                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Brand Name:</label>
                                    <asp:DropDownList ID="ddlBrandName" runat="server" Height="30px" Width="250px" DataTextField="brand_master_name" DataValueField="brand_master_id"
                                        data-toggle="tooltip"
                                        data-placement="right" title="Brand Name" CssClass="form-control" placeholder="--Select One--">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3 col-sm-6 col-xs-12  ">

                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Batch No:</label>
                                    <%--Bhavin--%>
                                    <%--<asp:TextBox ID="txtBatchNo" AutoComplete="off" runat="server" Style="text-transform: capitalize"
                                        data-toggle="tooltip" data-placement="right" title="Batch No" CssClass="form-control" MaxLength="10">
                                    </asp:TextBox>--%>
                                    <asp:TextBox ID="txtBatchNo" AutoComplete="off" runat="server" Style="text-transform: capitalize"
                                        data-toggle="tooltip" data-placement="right" title="Batch No" CssClass="form-control" MaxLength="39">
                                    </asp:TextBox>
                                    <%--End--%>
                                </div>

                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div class="col-md-7 col-sm-6 col-xs-12 ">
                                    <label class="control-label" style="font-size: small"><span style="color: red">*</span>Address of the manufacturer:</label>
                                    <br />
                                    <asp:TextBox ID="txtAddressOfMan" AutoComplete="off" runat="server" data-toggle="tooltip"
                                        data-placement="right" title="Address of the manufacturer" CssClass="form-control" MaxLength="100" TextMode="MultiLine">
                                    </asp:TextBox>
                                    
                                </div>

                                <div class="col-md-1 col-sm-1 col-xs-12 ">
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                    <label class="control-label" style="display: inline"><span style="color: red">*</span>Compactor:</label>
                                    <asp:DropDownList ID="ddlCompactor" runat="server" Height="30px" Width="250px"
                                        data-toggle="tooltip" dataValueField="Key" DataTextField="Value"
                                        data-placement="right" title="Compactor" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12 form-inline text-center" style="padding-top: 1em">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" AutoPostBack="false" OnClick="btnAdd_Click" OnClientClick="javascript:return quantReceivedValidation()" />
                                </div>

                                <asp:GridView ID="grdQuantList" runat="server" AutoGenerateColumns="false"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table"
                                                        OnRowCommand="grdQuantList_RowCommand">
                                                        <columns> 
                                                       <asp:TemplateField HeaderText="Type of liquor" ItemStyle-Font-Bold="true" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeOfLiquor" runat="server" Visible="true" Text='<%#Eval("type_of_liquor_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubType" runat="server" Visible="true" Text='<%#Eval("liquor_sub_name") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Width="160px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSize" runat="server" Visible="true" Text='<%#Eval("size_master_name") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%#Eval("quantity") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Brand Name" ItemStyle-Font-Bold="true" ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrandName" runat="server" Visible="true" Text='<%#Eval("brand_master_name") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Batch No"  ItemStyle-Font-Bold="true" ItemStyle-Width="120px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBatchNo" runat="server" Visible="true" Text='<%#Eval("batch_no") %>'></asp:Label>
                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Address of the manufacturer"  ItemStyle-Font-Bold="true" ItemStyle-Width="220px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Visible="true" Text='<%#Eval("address") %>'></asp:Label> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compactor"  ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompactor" runat="server" Visible="true" Text='<%#Eval("compactor_name") %>'></asp:Label> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                         <ItemTemplate>
                                                             <asp:LinkButton Text="Edit" ID="btnQuantEdit" CssClass="btn btn-primary" runat="server"
                                                                 CommandArgument='<%# Eval("id") %>'  
                                                                 CommandName="Edit_Record">
                                                             </asp:LinkButton> 
                                                             <asp:LinkButton Text="Delete" ID="btnQuantDelete" CssClass="btn btn-danger" runat="server"
                                                                 CommandArgument='<%# Eval("id") %>'  
                                                                 CommandName="Delete_Record">
                                                             </asp:LinkButton> 
                                                         </ItemTemplate> 
                                                      </asp:TemplateField>
                                                     </columns>
                                                        <headerstyle backcolor="#26B8B8" forecolor="#ECF0F1" borderstyle="Solid" borderwidth="2px" height="25px" horizontalalign="Center"></headerstyle>
                                                        <pagerstyle backcolor="#26B8B8" borderwidth="2px" height="5px" horizontalalign="Right" forecolor="#ECF0F1"
                                                            verticalalign="Middle" font-size="Medium" font-bold="True" />
                                                        <rowstyle backcolor="Window" borderstyle="Solid" borderwidth="2px" height="25px"></rowstyle>
                                                    </asp:GridView>

                            </asp:Panel>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline text-center" style="padding-top: 1em; left: 0px; top: 0px; height: 84px;">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="javascript:return validationMsg()" />
                        <asp:Button ID="btnCancel" runat="server" Text="Clear" class="btn btn-danger" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
            </div>
    </div>
    <asp:HiddenField ID="hdnIsSaveOrDraft" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnIsSavedAlready" runat="server"></asp:HiddenField>
</asp:Content>
