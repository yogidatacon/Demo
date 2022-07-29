<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PartyFinancialYear.aspx.cs" Inherits="UserMgmt.PartyFinancialYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Home" ContentPlaceHolderID="BodyContent" runat="server">
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
                                  <script type="text/javascript" src="../common/theme/js/flot/date.js"></script>
                                <title>User Management</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddpartytype.ClientID%>').value == 'Select') {
                                            alert("Select  Organization Name");
                                             document.getElementById("<% =ddpartytype.ClientID%>").focus();
                                            return false;
                                           
                                        }

                                    }
                                    function SelectStartDate(e) {
                                      
                                        var date1 = $('#BodyContent_txtStartDate').val();
                                        $('#BodyContent_txtstart').val(date1);
                                    }
                                    function SelectEndDate(e) {
                                        debugger;
                                        var date2 = $('#BodyContent_txtEndDate').val();
                                        $('#BodyContent_txtend').val(date2);
                                        var start = $('#BodyContent_txtStartDate').val();
                                        var end = $('#BodyContent_txtEndDate').val();
                                        var fin = start.substr(6, 4) + "-" + end.substr(6, 4);
                                        $('#BodyContent_txtfinyear').val(fin);
                                        $('#BodyContent_txtyear').val(fin);
                                       
                                        $('#BodyContent_lblwarning').text(fin);
                                    }
                                    function GetValues() {
                                        debugger;
                                        var partytype = $('#BodyContent_ddpartytype').val();
                                        $('#BodyContent_partytype').val(partytype);
                                       
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                   <ul class="nav nav-tabs">
                                      
                                         <li >
                                         <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                          <li class="active">
                                            <asp:LinkButton ID="LinkButton1" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Financial Year</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                                                                                                                                                            <li >
                                            <asp:LinkButton ID="producttypemaster" OnClick="producttypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Type Master</span></asp:LinkButton></li>
                                        <li >
                                            <asp:LinkButton ID="productmaster" OnClick="productmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Product Master</span></asp:LinkButton></li>
                                         <li >  <asp:LinkButton ID="uommaster" OnClick="uommaster_Click" runat="server"><span style="color:#fff;font-size:14px;">UOM Master</span></asp:LinkButton></li>
                                         <li >  <asp:LinkButton ID="RawMaterialTypeMaster" OnClick="RawMaterialTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material Type Master</span></asp:LinkButton></li>
                                        <li >  <asp:LinkButton ID="RawMaterial" OnClick="RawMaterial_Click" runat="server"><span style="color:#fff;font-size:14px;">Raw Material</span></asp:LinkButton></li>
                                       <li>
                                            <asp:LinkButton ID="vattypemaster1" OnClick="vattypemaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Type Master</span></asp:LinkButton></li>
                                         <li >
                                            <asp:LinkButton ID="vatmaster" OnClick="vatmaster_Click" runat="server"><span style="color:#fff;font-size:14px;">VAT Master</span></asp:LinkButton></li>
                                         <li >
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                        
                                    </ul>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Party Financial Year</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                      
                                           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>   
                                           <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                           <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Party Type Name</label><br />
                                            <asp:DropDownList ID="ddpartytype" Height="30px"  Width="200px" onchange="GetValues()" runat="server" data-toggle="tooltip" data-placement="right" title="Party Type Name" CssClass="form-control"   Style="">
                                                        </asp:DropDownList>
                                                 <asp:HiddenField ID="partytype" runat="server" />
                                                </div>
                                         <div class="clearfix"></div>
                                         <p>&nbsp;</p>
                                        
                                         <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                           <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Start Date</label><br />
                                            <cc1:CalendarExtender runat="server" PopupButtonID="Image1" OnClientDateSelectionChanged="SelectStartDate" TargetControlID="txtStartDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtStartDate" data-toggle="tooltip" data-placement="right" title="Date" CssClass="form-control" AutoComplete="off" runat="server" >
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" CssClass="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtstart" runat="server" />
                                             
                                             </div>

                                         <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>End Date</label><br />
                                                    
                                                       <cc1:CalendarExtender runat="server" PopupButtonID="Image2" OnClientDateSelectionChanged="SelectEndDate" TargetControlID="txtEndDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                              
                                                <asp:TextBox ID="txtEndDate" data-toggle="tooltip" data-placement="right" title="Date" CssClass="form-control" AutoComplete="off" runat="server">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" CssClass="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtend" runat="server" />
                                             </div>

                                         <div class="col-md-3 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Financial Year</label><br />
                                                    
                                                        <asp:TextBox  readonly id="txtfinyear" runat="server"  placeholder="YYYY-YYYY" data-toggle="tooltip" data-placement="right" title="Organisation Type" class="form-control" maxlength="10" style="height: 28px; width: 106px; margin-left: 8px"></asp:TextBox>
                                             </div>
                                         <div class="clearfix"></div>
                                         <p>&nbsp;</p>
                                            <div style="border:solid" class="col-md-16 col-sm-16 col-xs-12 form-inline" >
                                          <label style="font-size: small; font-weight: bold;" runat="server" >Warning : By submitting the form, the system will compute new opening balances and make the  </label> <label style="font-size: small; font-weight: bold;color:red" runat="server" id="lblwarning"></label><label style="font-size: small; font-weight: bold;" runat="server"  id="Label2"> as the current financial year.You will not be able to add new entries for the previous year anymore and all old records will be made in-active.</label>
                                                       
                                                </div>
                                         <div class="clearfix"></div>
                                         <p>&nbsp;</p>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <asp:HiddenField ID="orgid" runat="server" />
                                               <p>&nbsp;</p>
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click"  />
                                            </div>

                                        </div>
                                    </div></body></html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
</asp:Content>
