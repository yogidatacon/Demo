<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="FIRForm.aspx.cs" Inherits="UserMgmt.FIRForm" %>

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
                                <title>FIR Form</title>
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
    function blockSpecialChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        //return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        return false; 
        }
    </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtFIRDate.ClientID%>').value == '') {
                                            alert("Enter FIR Date");
                                            document.getElementById("<% =txtFIRDate.ClientID%>").focus();
                                            return false;
                                        }
                                       <%-- var FIR_Date = document.getElementById('<%=txtFIRDate.ClientID%>').value;
                                        var Complaint_Date = document.getElementById('<%=txtComplaintDate.ClientID%>').value;
                                        //var cDate = new Date(Complaint_Date);
                                        //var fDate = new Date(FIR_Date);
                                        if (FIR_Date != '' && Complaint_Date != '' && Complaint_Date > FIR_Date) {
                                            debugger;
                                            alert("Please ensure that the FIR Date is greater than or equal to the Complaint Date.");
                                            return false;
                                        }--%>
                                         if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                             alert("Enter PR/FIR No");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtRaidOrderBy.ClientID%>').value == '') {
                                            alert("Enter Raid Order By");
                                            document.getElementById("<% =txtRaidOrderBy.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtManualBookDate.ClientID%>').value == '') {
                                             alert("Enter Manual Book Date");
                                            document.getElementById("<% =txtManualBookDate.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtManualBookPRFIR.ClientID%>').value == '') {
                                            alert("Enter Manual Book PR/FIR");
                                            document.getElementById("<% =txtManualBookPRFIR.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=ddlDesignation.ClientID%>').value == 'Select') {
                                             alert("Select Designation");
                                            document.getElementById("<% =ddlDesignation.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtComplaintDate.ClientID%>').value == '') {
                                            alert("Enter Complaint Date");
                                            document.getElementById("<% =txtComplaintDate.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtOfficialComplaintNo.ClientID%>').value == '') {
                                             alert("Enter Official Complaint No");
                                            document.getElementById("<% =txtOfficialComplaintNo.ClientID%>").focus();
                                            return false;
                                         }
                                        if (document.getElementById('<%=txtcourt.ClientID%>').value == '') {
                                            alert("Enter Information sent to the court date");
                                            document.getElementById("<% =txtcourt.ClientID%>").focus();
                                            return false;
                                        }

                                        
                                        
                                    }
                                </script>


                                 <script type="text/javascript">
                                    function SelectDate3(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtComplaintDate').val(todayDate);
                                        $('#BodyContent_txtgpd').val(todayDate);
                                    }

                                    function SelectDate2(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtManualBookDate').val(todayDate);
                                        $('#BodyContent_txtdor').val(todayDate);
                                    }
                                   
                                    function Selectdate(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtFIRDate').val(todayDate);
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                    function SelectDate4(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtcourt').val(todayDate);
                                        $('#BodyContent_txtinfo').val(todayDate);
                                    }

                                </script>
                                
                                  <script>
                                    $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_txtFIRDate').val() == "") {
                                            $('#BodyContent_txtFIRDate').val($('#BodyContent_txtdob').val());
                                        }

                                        if ($('#BodyContent_txtManualBookDate').val() == "") {
                                            $('#BodyContent_txtManualBookDate').val($('#BodyContent_txtdor').val());
                                        }
                                        if ($('#BodyContent_txtComplaintDate').val() == "") {
                                            $('#BodyContent_txtComplaintDate').val($('#BodyContent_txtdor1').val());
                                        }
                                        if ($('#BodyContent_txtcourt').val() == "") {
                                            $('#BodyContent_txtcourt').val($('#BodyContent_txtinfo').val());
                                        }


                                    });
                                    </script> 
                            </head>
                            <body>
                              
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>FIR Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <%--<asp:UpdatePanel runat="server">--%>
                                        <ContentTemplate>
                                      <a>
                                     <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="LinkButton1_Click"  BorderStyle="Outset"> Seizure</asp:LinkButton>
                                </a>
                                     <div class="clearfix"></div>
                                        <p>&nbsp;</p>

                                     <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small"><span style="color: red">*</span>Seizure No</label>
                                        <br />
                                         <asp:DropDownList ID="ddlSeizureNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Seizure No"></asp:DropDownList>
                                    </div>--%>
                                 <%--    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Raid By </label>
                                        <br />
                                            <asp:TextBox ID="txtRaidBy" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Raid By" ReadOnly="true"></asp:TextBox>
                                    </div>--%>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small"><span style="color: red">*</span>Raid Order By</label>
                                        <br />
                                        <asp:TextBox ID="txtRaidOrderBy" AutoComplete="off"  CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Raid Order By"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small"><span style="color: red">*</span>Designation</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDesignation" AutoComplete="off" Width="60%" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                    </div> 
                                      <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                   
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                        <asp:TextBox ID="txtPRFIRNo" AutoComplete="off" CssClass="form-control"   runat="server" data-toggle="tooltip" data-placement="right" title="PR / FIR No" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>PR /FIR Date</label><br />
                                         <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtFIRDate" Format="dd-MM-yyyy" ID="CalendarExtender" OnClientDateSelectionChanged="Selectdate"  ></cc1:CalendarExtender>                           
                                                <asp:TextBox ID="txtFIRDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="PR /FIR Date" class="form-control validate[required]" onkeypress="return blockSpecialChar(event)" AutoComplete="off" runat="server" ReadOnly="false" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" runat="server" />
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Manual Book PR / FIR</label>
                                        <br />
                                        <asp:TextBox ID="txtManualBookPRFIR"  AutoComplete="off" CssClass="form-control"   runat="server" data-toggle="tooltip" data-placement="right" title="Manual Book PR / FIR" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Manual Book Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtManualBookDate" Format="dd-MM-yyyy" ID="CalendarExtender1" OnClientDateSelectionChanged="SelectDate2" ></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtManualBookDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="Manual Book Date" class="form-control validate[required]" onkeypress="return blockSpecialChar(event)" AutoComplete="off" runat="server"  ReadOnly="false" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdor" runat="server" />
                                            </div>
                                     
                                      <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Official Complaint No</label>
                                        <br />
                                        <asp:TextBox ID="txtOfficialComplaintNo" AutoComplete="off" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Official Complaint No"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Complaint Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtComplaintDate" Format="dd-MM-yyyy" ID="CalendarExtender2" OnClientDateSelectionChanged="SelectDate3"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtComplaintDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="Complaint Date" class="form-control validate[required]" onkeypress="return blockSpecialChar(event)" AutoComplete="off"  ReadOnly="false" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtgpd" runat="server" />
                                            </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Information sent to court</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txtcourt" Format="dd-MM-yyyy" ID="CalendarExtender3" OnClientDateSelectionChanged="SelectDate4"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtcourt" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="Information sent to the court" class="form-control validate[required]" onkeypress="return blockSpecialChar(event)" AutoComplete="off"   ReadOnly="false" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtinfo" runat="server" />
                                            </div>
                                     <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                      <div style="height: 8%; background-color: #26b8b8;">
                                   <span style="font-size: small; color: white; margin-left: 40%">Accused List</span>
                                </div>
                                      <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                
                                           <div class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                    <div style =" width:1000px; overflow:auto;"> 
                                    <asp:GridView ID="grdAccusedDetailsListView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                             <%--<asp:TemplateField HeaderText="Fir Status" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"  >
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chSelect" runat="server" Checked='<%# bool.Parse(Eval("fir_status").ToString() == "Y" ? "True": "False" )%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Accused ID" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccusedID" runat="server" Visible="true" Text='<%#Eval("seizure_accused_details_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAppearanceName" runat="server" Visible="true" Text='<%#Eval("accusedname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%--<asp:TemplateField HeaderText="FIR Filed" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFIRFiled" runat="server" Visible="true" Text='<%#Eval("accusedname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                    </div></div>
                           </ContentTemplate>
                                   <%-- </asp:UpdatePanel>--%>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" CssClass="btn btn-danger"  OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                    </div></body>
                            </html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
