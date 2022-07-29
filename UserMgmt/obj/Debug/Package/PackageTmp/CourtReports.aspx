<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CourtReports.aspx.cs" Inherits="UserMgmt.CourtReports" %>

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
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger
                                      <%--  if (document.getElementById('<%=ddlparty.ClientID%>').value == 'Select') {
                                            alert("Select  Party Name");
                                            document.getElementById("<% =ddlparty.ClientID%>").focus();
                                            return false;
                                        }--%>
                                        if (document.getElementById('<%=ddlReportName.ClientID%>').value == 'Select') {
                                            alert("Select  Report Name");
                                           
                                            document.getElementById("<% =ddlReportName.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlReportName.ClientID%>').value != '84' && document.getElementById('<%=ddlReportName.ClientID%>').value != '86' && document.getElementById('<%=ddlReportName.ClientID%>').value != '87'&& document.getElementById('<%=ddlReportName.ClientID%>').value != '88') {
                                            if (document.getElementById('<%=ddlDEPTType.ClientID%>').value == 'Select') {
                                                alert("Select Department Type");
                                                document.getElementById("<% =ddlDEPTType.ClientID%>").focus();
                                                return false;
                                            }
                                        }
                                        debugger;;
                                        if (document.getElementById('<%=ddlReportName.ClientID%>').value != '75' && document.getElementById('<%=ddlReportName.ClientID%>').value != '77' && document.getElementById('<%=ddlReportName.ClientID%>').value != '79' && document.getElementById('<%=ddlReportName.ClientID%>').value != '76'&& document.getElementById('<%=ddlReportName.ClientID%>').value != '78'&& document.getElementById('<%=ddlReportName.ClientID%>').value != '80' && document.getElementById('<%=ddlReportName.ClientID%>').value != '81' && document.getElementById('<%=ddlReportName.ClientID%>').value != '87'&& document.getElementById('<%=ddlReportName.ClientID%>').value != '88') {
                                            if (document.getElementById('<%=ddldistrict.ClientID%>').value == 'Select') {
                                                alert("Select  District");
                                                document.getElementById("<% =ddldistrict.ClientID%>").focus();
                                                return false;

                                            }
                                        }
                                          if (document.getElementById('<%=ddlReportName.ClientID%>').value == '84' || document.getElementById('<%=ddlReportName.ClientID%>').value == '86' ) {
                                            if (document.getElementById('<%=ddlCourt.ClientID%>').value == 'Select') {
                                                alert("Select  Court Name");
                                                document.getElementById("<% =ddlCourt.ClientID%>").focus();
                                                return false;
                                            }
                                        }
                                        <%--   if (document.getElementById('<%=ddlfinancialyear.ClientID%>').value == 'Select') {
                                            alert("Select  Financial Year");
                                           
                                            document.getElementById("<% =ddlfinancialyear.ClientID%>").focus();
                                            return false;
                                        }
                                         if ((document.getElementById('<%=ddlReportName.ClientID%>').value == '21'||document.getElementById('<%=ddlReportName.ClientID%>').value == '22' ||document.getElementById('<%=ddlReportName.ClientID%>').value == '23'||document.getElementById('<%=ddlReportName.ClientID%>').value == '24'||document.getElementById('<%=ddlReportName.ClientID%>').value == '27')  && document.getElementById('<%=ddVats.ClientID%>').value == 'Select') {
                                            alert("Select  VAT Name");
                                           
                                            document.getElementById("<% =ddVats.ClientID%>").focus();
                                            return false;
                                         }--%>
                                        debugger
                                              if (document.getElementById('<%=ddlReportName.ClientID%>').value != '75' && document.getElementById('<%=ddlReportName.ClientID%>').value != '77' && document.getElementById('<%=ddlReportName.ClientID%>').value != '79' && document.getElementById('<%=ddlReportName.ClientID%>').value != '81') {
                                                if (document.getElementById('<%=txtFromDATE.ClientID%>').value == '') {
                                                    alert("Select  From Date");
                                                    document.getElementById("<% =txtFromDATE.ClientID%>").focus();
                                                    return false;
                                                }
                                                if (document.getElementById('<%=txtToDATE.ClientID%>').value == '') {
                                                    alert("Select To Date ");

                                                    document.getElementById("<% =txtToDATE.ClientID%>").focus();
                                                    return false;
                                                }
                                            }
                                        
                                       
                                     }
                                </script>
                                <script type="text/javascript">
            function popup(url) {
                debugger;;
                var width = 1000;
                var height = 500;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ', height=' + height;
                params += ', top=' + top + ', left=' + left;
                params += ', toolbar=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', directories=no';
                params += ', scrollbars=no';
                params += ', status=no';
                params += ', location=no';
                newwin = window.open(url, 'd', params);
                if (window.focus) {
                    newwin.focus()
                }
                return false;
                 
                 
            }
            function SelectFromDate(e) {
                debugger
                var todayDate = e.get_selectedDate();
                var dd = todayDate.getDate();
                var mm = todayDate.getMonth() + 1; //January is 0!
              //  $('#BodyContent_txtFromDate').val(todayDate);
                var yyyy = todayDate.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                todayDate = dd + '-' + mm + '-' + yyyy;
                $('#BodyContent_txtFromDate').val(todayDate);
                $('#BodyContent_txtfrom').val(todayDate);
              
            }
           
            function SelectToDate(e) {
                debugger
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
                $('#BodyContent_txtToDate').val(todayDate);
                $('#BodyContent_txtto').val(todayDate);
              //  $('#BodyContent_CalendarExtender1').first(todayDate);
                
            }
            $(document).ready(function () {

                if ($('#BodyContent_txtFromDATE').val() == "")
                    $('#BodyContent_txtFromDATE').val($('#BodyContent_txtfrom').val());
                if ($('#BodyContent_txtToDate').val() == "")
                    $('#BodyContent_txtToDate').val($('#BodyContent_txtto').val());
            });
        </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">

                                        <li class="active">
                                            <asp:LinkButton ID="UserReport" OnClick="UserReports_Click" runat="server"><span style="color:#fff;font-size:14px;">User Reports</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <h2>User Reports</h2>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_content">
                                                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                <%--    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red">*</span>Party Name </label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlparty"  Height="30px" width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlparty_SelectedIndexChanged" runat="server" data-toggle="tooltip" data-placement="right" title="Party Name" Cssclass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Report Name</label><br />
                                                        <asp:DropDownList ID="ddlReportName" Height="30px" width="95%" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="right" title="Reports Name" name="ddlReportNames" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged" CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="--Select--" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                      <div id="DEPT" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red">*</span>Department Type</label><br />
                                                        <asp:DropDownList ID="ddlDEPTType" Height="30px" width="95%" runat="server" data-toggle="tooltip" data-placement="right" title="Sale Type" name="ddlReportNames"  CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="--Select--" Value="Select"></asp:ListItem>
                                                             <asp:ListItem Text="Excise" Value="E"></asp:ListItem>
                                                             <asp:ListItem Text="Police" Value="P"></asp:ListItem> 
                                                        </asp:DropDownList>
                                                    </div>
                                              <%--  <div id="financialyear"  runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Financial Year</label><br />
                                                        <asp:DropDownList ID="ddlfinancialyear" Height="30px" width="95%" runat="server" data-toggle="tooltip" data-placement="right" title="Financial Year" AutoPostBack="true" OnSelectedIndexChanged="ddlfinancialyear_SelectedIndexChanged"  CssClass="form-control" Style="">
                                                        </asp:DropDownList>
                                                     <asp:HiddenField ID="enddate" runat="server" />    
                                                     <asp:HiddenField ID="startdate" runat="server" />    
                                                    </div>--%>
                                                    <div id="DST" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>District</label><br />
                                                        <asp:DropDownList ID="ddldistrict" Height="30px" width="95%" runat="server" data-toggle="tooltip" data-placement="right" title="District Name" name="ddlReportNames"  CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="--Select--" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                        <%--<div  runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Thana</label><br />
                                                        <asp:DropDownList ID="ddlThana" Height="30px" width="95%" runat="server" data-toggle="tooltip" data-placement="right" title="Reports Name" name="ddlReportNames"  CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="--Select--" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                   --%>
                                                    <div   id="Court" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Court Name</label><br />
                                                        <asp:DropDownList ID="ddlCourt" Height="30px" width="95%" runat="server" data-toggle="tooltip" data-placement="right" title="Court Name" name="ddlCourt"  CssClass="form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="--Select--" Value="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                     <div id="fromdate" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>From Date </label>
                                                <br />
                                               <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtFromDATE" OnClientDateSelectionChanged="SelectFromDate" ClientIDMode="Static" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtFromDATE" ReadOnly="true" width="60%"  data-toggle="tooltip" data-placement="right" title="From Date " Cssclass="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                               <asp:HiddenField ID="txtfrom" runat="server" />    
                                            </div>
                                               <div id="todate1" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>To Date </label>
                                                <br />
                                               <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtToDATE" OnClientDateSelectionChanged="SelectToDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtToDATE" ReadOnly="true" width="60%" data-toggle="tooltip" data-placement="right" title="To Date" Cssclass="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                               <asp:HiddenField ID="txtto" runat="server" />    
                                            </div>
                                                    
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                    <div class="clearfix">
                                                        <div>
                                                            <div style="margin-left: 125px">
                                                                <asp:Button ID="btnGenerate" runat="server" class="btn btn-primary" OnClientClick="javascript:return validationMsg();"  Text="Generate" OnClick="btnGenerate_Click"  />
                                                                <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
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
