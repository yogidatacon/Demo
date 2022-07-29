<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PassDispatchForm.aspx.cs" Inherits="UserMgmt.PassDispatchForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
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
                                <title>Pass Application Form</title>
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

                                    function Validate(e) {
                                        var keyCode = e.keyCode || e.which;
                                        var regex = /^[a-zA-Z ]+$/; //^[A-Za-z]*$/;
                                        var isValid = regex.test(String.fromCharCode(keyCode));
                                        return isValid;
                                    }
                                </script>
                                <script  type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=ddlDispatchType.ClientID%>').value == 'Select') {
                                            alert("Select Dispatch Type");
                                            document.getElementById("<% =ddlDispatchType.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=ddlDispatchVAT.ClientID%>').value == 'Select') {
                                            alert("Select Dispatch Vat");
                                            document.getElementById("<% =ddlDispatchVAT.ClientID%>").focus();
                                            return false;
                                        }
                                       if (document.getElementById('<%=txtQtyDispatch.ClientID%>').value == '') {
                                           alert("Enter Dispatch Qty");
                                            document.getElementById("<% =txtQtyDispatch.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=txtTaxInvoiceNo1.ClientID%>').value == '') {
                                            alert("Enter Tax Invoice No");
                                            document.getElementById("<% =txtTaxInvoiceNo1.ClientID%>").focus();
                                            return false;
                                        }

                                         if (document.getElementById('<%=ddlDispatchUnder.ClientID%>').value == 'Select') {
                                             alert("Select Dispatch Under");
                                                document.getElementById("<% =ddlDispatchUnder.ClientID%>").focus();
                                                return false;
                                            }
                                        if (document.getElementById('<%=passtype.ClientID%>').value == 'M')
                                        {
                                            if (document.getElementById('<%=ddlYearofProduction.ClientID%>').value == 'Select') {
                                                alert("Select Year of Production");
                                                document.getElementById("<% =ddlYearofProduction.ClientID%>").focus();
                                                return false;
                                            }

                                            if (document.getElementById('<%=txtBRIX.ClientID%>').value == '') {
                                                alert("Enter BRIX");
                                                document.getElementById("<% =txtBRIX.ClientID%>").focus();
                                                return false;
                                            }
                                            if (document.getElementById('<%=txtSugarContent.ClientID%>').value == '') {
                                                alert("Enter Sugar Content");
                                                document.getElementById("<% =txtSugarContent.ClientID%>").focus();
                                                return false;
                                            }
                                        }

                                     
                                        var balance = parseFloat($('#BodyContent_balance').val());
                                        var dispatch = parseFloat($('#BodyContent_txtQtyDispatch').val());
                                        if (balance < dispatch) {
                                            alert("Dispatch qty cannot be More than of Balance Qty");
                                            $('#BodyContent_txtQtyDispatch').val('');
                                            $('#BodyContent_txtQtyDispatch').focus();
                                            return false;
                                        }
                                        if (dispatch<=0) {
                                            alert(" Zero Dispatch qty is not Allowed");
                                            $('#BodyContent_txtQtyDispatch').val('');
                                            $('#BodyContent_txtQtyDispatch').focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=txtNameOfCarrier.ClientID%>').value != "PipeLine")
                                         {
                                             if (document.getElementById('<%=txtNameOfCarrier.ClientID%>').value == '') {
                                                 alert("Enter Name Of Carrier");
                                                 document.getElementById("<% =txtNameOfCarrier.ClientID%>").focus();
                                                 return false;
                                             }


                                             if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                                 alert("Enter Vehicle No");
                                                 document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                                 return false;
                                             }

                                             if (document.getElementById('<%=txtVehicleType.ClientID%>').value == '') {
                                                 alert("Enter Vehicle Type");
                                                 document.getElementById("<% =txtVehicleType.ClientID%>").focus();
                                                 return false;
                                             }

                                             if (document.getElementById('<%=txtDriverName.ClientID%>').value == '') {
                                                 alert("Enter Driver Name");
                                                 document.getElementById("<% =txtDriverName.ClientID%>").focus();
                                                 return false;
                                             }
                                             if (document.getElementById('<%=txtTransportChallanBiltyNo.ClientID%>').value == '') {
                                                 alert("Enter Transport Challan Bilty No");
                                                 document.getElementById("<% =txtTransportChallanBiltyNo.ClientID%>").focus();
                                                 return false;
                                             }


                                             if (document.getElementById('<%=txtDigitalLockNo.ClientID%>').value == '') {
                                                 alert("Enter Digital Lock No");
                                                 document.getElementById("<% =txtDigitalLockNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtDateOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Date Of Dispatch");
                                            document.getElementById("<% =txtDateOfDispatch.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtTimeOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Time Of Dispatch");
                                            document.getElementById("<% =txtTimeOfDispatch.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtDurationOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Duration Of Dispatch");
                                            document.getElementById("<% =txtDurationOfDispatch.ClientID%>").focus();
                                            return false;
                                         } 
                                        if (document.getElementById('<%=txtRouteDetails.ClientID%>').value == '') {
                                           
                                              alert("Enter Route Details");
                                            document.getElementById("<% =txtRouteDetails.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%= txtDepositedAmount.ClientID%>').value == '') {
                                              alert("Enter  Deposited Amount");
                                            document.getElementById("<% = txtDepositedAmount.ClientID%>").focus();
                                            return false;
                                          }
                                           if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks1.ClientID%>").focus();
                                            return false;
                                           }
                                        CheckIsRepeat();
                                    }
                                     function validationMsg1() {

                                         if (document.getElementById('<%=txtNameOfCarrier.ClientID%>').value != "PipeLine")
                                         {
                                             if (document.getElementById('<%=txtNameOfCarrier.ClientID%>').value == '') {
                                                 alert("Enter Name Of Carrier");
                                                 document.getElementById("<% =txtNameOfCarrier.ClientID%>").focus();
                                                 return false;
                                             }


                                             if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                                 alert("Enter Vehicle No");
                                                 document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                                 return false;
                                             }

                                             if (document.getElementById('<%=txtVehicleType.ClientID%>').value == '') {
                                                 alert("Enter Vehicle Type");
                                                 document.getElementById("<% =txtVehicleType.ClientID%>").focus();
                                                 return false;
                                             }

                                             if (document.getElementById('<%=txtDriverName.ClientID%>').value == '') {
                                                 alert("Enter Driver Name");
                                                 document.getElementById("<% =txtDriverName.ClientID%>").focus();
                                                 return false;
                                             }
                                             if (document.getElementById('<%=txtTransportChallanBiltyNo.ClientID%>').value == '') {
                                                 alert("Enter Transport Challan Bilty No");
                                                 document.getElementById("<% =txtTransportChallanBiltyNo.ClientID%>").focus();
                                                 return false;
                                             }


                                             if (document.getElementById('<%=txtDigitalLockNo.ClientID%>').value == '') {
                                                 alert("Enter Digital Lock No");
                                                 document.getElementById("<% =txtDigitalLockNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtDateOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Date Of Dispatch");
                                            document.getElementById("<% =txtDateOfDispatch.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtTimeOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Time Of Dispatch");
                                            document.getElementById("<% =txtTimeOfDispatch.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=txtDurationOfDispatch.ClientID%>').value == '') {
                                             alert("Enter Duration Of Dispatch");
                                            document.getElementById("<% =txtDurationOfDispatch.ClientID%>").focus();
                                            return false;
                                         } 
                                          if (document.getElementById('<%=txtRouteDetails.ClientID%>').value == '') {
                                              alert("Enter Route Details");
                                            document.getElementById("<% =txtRouteDetails.ClientID%>").focus();
                                            return false;
                                         } 
                                     }
                                       function validationMsg12() {

                                           
                                          if (document.getElementById('<%=txtApproverremarks.ClientID%>').value == '') {
                                              alert("Enter Approver Remarks");
                                            document.getElementById("<% =txtApproverremarks.ClientID%>").focus();
                                            return false;
                                         } 
                                     }
                                    function onTimeChange()
                                    {
                                        debugger;
                                        var inputEle = $('#BodyContent_txtTimeOfDispatch').val();
                                        var timeSplit = inputEle.value.split(':'),  
                                          hours,
                                          minutes,
                                          meridian;
                                        hours = timeSplit[0];
                                        minutes = timeSplit[1];
                                        if (hours > 12) {
                                            meridian = 'PM';
                                            hours -= 12;
                                        } else if (hours < 12) {
                                            meridian = 'AM';
                                            if (hours == 0) {
                                                hours = 12;
                                            }
                                        } else {
                                            meridian = 'PM';
                                        }
                                        $('#BodyContent_txtTimeOfDispatch').val(hours + ':' + minutes + ' ' + meridian);
                                    }
                                    function CheckQTY()
                                    {
                                        debugger;
                                        var available = parseFloat($('#BodyContent_txtAvailableQty').val());
                                        var reserved = parseFloat($('#BodyContent_txtreservedqty').val());
                                   //     var available = parseFloat(available1) - parseFloat(reserved);
                                    var dispatch = parseFloat($('#BodyContent_txtQtyDispatch').val());
                                    if(available<dispatch)
                                    {
                                        alert("Dispatch qty not  Morethan of Available Qty");
                                        $('#BodyContent_ddlDispatchVAT').val("Select");
                                        $('#BodyContent_txtQtyDispatch').val('');
                                        $('#BodyContent_ddlDispatchVAT').focus();
                                        return ;
                                    }
                                    var balance = parseFloat($('#BodyContent_txtBalanceQuantity').val());
                                    var dispatch = parseFloat($('#BodyContent_txtQtyDispatch').val());
                                    if (balance < dispatch)
                                    {
                                        alert("Dispatch qty cannot be more than the Requested Qty");
                                        $('#BodyContent_txtQtyDispatch').val('');
                                        $('#BodyContent_txtQtyDispatch').focus();
                                        return ;
                                    }
                                    }
                                    function SelectD(e) {
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
                                        $('#BodyContent_txtDateOfDispatch').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                        $('#BodyContent_dd1').val(todayDate);
                                    }
                                </script>
                                 <script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            debugger;
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
                                 </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Transport Pass</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click"><span style="color:#fff;font-size:14px;">Dispatch</span></asp:LinkButton></li>
                                    
                                        
                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                               
                                 <div class="x_title">
                                    <h2>Dispatch Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:HiddenField ID="rr_noc_request_id" runat="server" />
                                    <asp:HiddenField ID="userparty" runat="server" />
                                    <asp:HiddenField ID="party_code" runat="server" />
                                    <asp:HiddenField ID="product_code" runat="server" />
                                      <asp:HiddenField ID="uom" runat="server" />
                                    <asp:HiddenField ID="balance" runat="server" />
                                    <asp:HiddenField ID="status" runat="server" />
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div style="display: none" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <asp:HiddenField ID="passtype" runat="server" />
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Request for Pass  No</label>
                                        <br />
                                       <%-- <asp:DropDownList ID="ddlPassRequestNo" re runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Pass Request No"></asp:DropDownList>--%>
                                         <asp:TextBox ID="txtPassRequestNo" Width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Pass Request No" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label id="ReleaseRequest" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span>Permit No</label>
                                        <label id="nocrequest" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span>NOC Application No</label>
                                        <br />
                                  <%--      <asp:DropDownList ID="ddlReleaseRequestNo" runat="server" CssClass="form-control" Width="70%" data-toggle="tooltip" data-placement="right" title="Release Request No"></asp:DropDownList>--%>
                                           <asp:TextBox ID="txtReleaseRequestNo" Width="70%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Release Request No" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Pass Required For</label>
                                        <br />
                                       
                                        <asp:TextBox ID="txtPassRequiredFor" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Pass Required For" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                         <label id="permit" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span> Valid Upto</label>
                                        <label id="ValidUpto" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span> NOC Valid Upto</label>
                                        <br />
                                        <asp:TextBox ID="txtValidUpto" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Valid Upto" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="issued" runat="server">
                                        <label id="Label1" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span>Pass Issued No</label>
                                        <br />
                                        <asp:TextBox ID="txtpassissuedno" Width="60%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Pass Issued No" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"  id="AllNo" runat="server" style="display: inline"><span style="color: red">*</span>Issued Permit No</label>
                                        <label class="control-label"  id="issuedNOC" runat="server" style="display: inline"><span style="color: red">*</span>Issued NOC No</label>
                                        <br />
                                        <asp:TextBox ID="txtAllotmentrequestno" CssClass="form-control" Width="70%" runat="server" data-toggle="tooltip" data-placement="right" title="Allotment Request No" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="Suppliername" runat="server" style="display: inline"><span style="color: red">*</span>Supplier Name</label>
                                          <label class="control-label" id="customer" runat="server" style="display: inline"><span style="color: red">*</span>Customer Name</label>
                                        <br />
                                        <asp:TextBox ID="txtSupplierName" CssClass="form-control"  Width="60%" runat="server" data-toggle="tooltip" data-placement="right" title="Supplier Name" ReadOnly="true"></asp:TextBox>
                                    </div>
                                   
                                    <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" id="Supplieraddress" runat="server"  style="display: inline; font-size: small"><span style="color: red">*</span>Supplier Address</label>
                                        <label class="control-label" id="caddress" runat="server"  style="display: inline; font-size: small"><span style="color: red">*</span>Customer Address</label>
                                        <br />
                                        <asp:TextBox ID="txtSupplierAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="95%" data-placement="right" title="Supplier Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                  <%--  <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label id="Label2" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span>State</label>
                                        <br />
                                        <asp:TextBox ID="txtstate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="State" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="Div1" runat="server">
                                        <label id="Label3" runat="server" class="control-label" style="display: inline"><span style="color: red">*</span>District</label>
                                        <br />
                                        <asp:TextBox ID="txtdistrict" Width="85%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="District" ReadOnly="true"></asp:TextBox>
                                    </div>--%>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="Depot" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Product</label>
                                        <br />
                                        <asp:TextBox ID="txtDepot" CssClass="form-control" Width="50%" runat="server" data-toggle="tooltip" data-placement="right" title="Depot"  ReadOnly="true"></asp:TextBox>
                                      
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" id="Requested" runat="server" style="display: inline"><span style="color: red">*</span>Pass Requested Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtReleaseRequestQuantity" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Approved Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Requested Quantity for Dispatch </label>
                                        <br />
                                        <asp:TextBox ID="txtLiftedQuantity" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Lifted Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Pass Balance Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtBalanceQuantity" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Balance Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>
                                   <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span> Pass Requested Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtPassQty" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="Pass Requested Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Dispatch Type</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDispatchType" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch Type"></asp:DropDownList>
                                    </div>
                                    <div id="Dispatchvat" runat="server">
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>From Dispatch VAT</label>
                                        <br />
                                         <asp:DropDownList ID="ddlDispatchVAT" AutoPostBack="true" OnSelectedIndexChanged="ddlDispatchVAT_SelectedIndexChanged" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch VAT"></asp:DropDownList>
                                    </div>
                                      
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Available Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtAvailableQty" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" title="Available Qty" ReadOnly="true"></asp:TextBox>
                                    </div>
                                        </div>   
                                    <div runat="server" visible="false" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span> Reserved Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtreservedqty" ReadOnly="true" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Reserved Qty"></asp:TextBox>
                                    </div>  
                                    <asp:HiddenField ID="depotid" runat="server" />
                                   <%--  <div class="clearfix"></div>
                                    <p>&nbsp;</p>   
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Temperature</label><br />

                                                <asp:TextBox ID="txtTemperature" runat="server" data-toggle="tooltip" autocomplete="off" data-placement="right" title="Temperature" onkeypress="return onlyDotsAndNumbers(this,event);"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Indication</label><br />

                                        <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Indication"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Strength</label><br />
                                        <asp:TextBox ID="txtStrength" runat="server" CssClass="form-control" data-toggle="tooltip" autocomplete="off" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Strength"></asp:TextBox>
                                    </div>--%>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>     
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span> Dispatch Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtQtyDispatch" onchange="CheckQTY()"  CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Qty Dispatch"></asp:TextBox>
                                    </div>
                                    
                                   
                                   
                                    
                                   
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Tax Invoice No</label>
                                        <br />
                                        <asp:TextBox ID="txtTaxInvoiceNo1" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Tax Invoice No"></asp:TextBox>
                                    </div>
                                  

                                      <div id="Div1" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Dispatch Under </label>
                                        <br />
                                         <asp:DropDownList ID="ddlDispatchUnder" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch Under">
                                             <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="Under Bond" Value="B"></asp:ListItem>
                                                <asp:ListItem Text="On Payment of Duty" Value="D"></asp:ListItem> 
                                         </asp:DropDownList>
                                    </div>
                                         <div id="permitno" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Permit No</label>
                                        <br />
                                        <asp:TextBox ID="txtpermitno" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" Width="70%" data-toggle="tooltip" data-placement="right" title="Permit No"></asp:TextBox>
                                    </div>

                                     <div id="Molasses1" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Year of Production</label>
                                        <br />
                                         <asp:DropDownList ID="ddlYearofProduction" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Year of Production"></asp:DropDownList>
                                    </div>
                                     <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" ><span style="color: red">*</span>UOM</label>
                                        <br />
                                        <asp:DropDownList ID="ddlUOM" onchange="GetUOM()" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="UOM"></asp:DropDownList>
                                    </div>--%>
                                    <div class="clearfix"></div>
                                  
                                         <p>&nbsp;</p>
                                     <div id="Molasses" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>BRIX</label>
                                        <br />
                                        <asp:TextBox ID="txtBRIX" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="BRIX"></asp:TextBox>
                                    </div>
                                          
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Sugar Content</label>
                                        <br />
                                        <asp:TextBox ID="txtSugarContent" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="8" title="Sugar Content"></asp:TextBox>
                                    </div></div>
                                 <%--   <div id="ApprovalB" runat="server">
                                         <div class="x_title">
                                    <h2>Pass Approval</h2>
                                    <div class="clearfix"></div>
                                </div>--%>
                                          <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                         <%--<div id="pipeline" runat="server">--%>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Name Of Carrier</label>
                                        <br />
                                        <asp:TextBox ID="txtNameOfCarrier" onkeypress="return Validate(event);" style="text-transform:capitalize;" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Name Of Carrier"></asp:TextBox>
                                    </div>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Vehicle No</label>
                                        <br />
                                             <asp:TextBox ID="txtVehicleNo" style="text-transform:uppercase;" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Vehicle No"></asp:TextBox>
                                    </div>
                                                               
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Vehicle Type</label>
                                        <br />
                                             <%--<asp:DropDownList ID="ddlVehicleType" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Vehicle Type"></asp:DropDownList>--%>
                                             <asp:TextBox ID="txtVehicleType" CssClass="form-control" style="text-transform:capitalize;" runat="server" data-toggle="tooltip" data-placement="right" title="Vehicle Type"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Driver Name</label>
                                        <br />
                                        <asp:TextBox ID="txtDriverName" CssClass="form-control" style="text-transform:capitalize;" runat="server" data-toggle="tooltip" data-placement="right" title="Driver Name"></asp:TextBox>
                                    </div>
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Transport Challan Bilty No</label>
                                        <br />
                                        <asp:TextBox ID="txtTransportChallanBiltyNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Transport Challan Bilty No"></asp:TextBox>
                                    </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Digital Lock No</label>
                                        <br />
                                        <asp:TextBox ID="txtDigitalLockNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Digital Lock No"></asp:TextBox>
                                    </div>
                                             
                                        <div class="col-md-5 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display: inline;font-size:12px"><span style="color: red">*</span>Route Details</label>
                                        <br />
                                        <asp:TextBox ID="txtRouteDetails" CssClass="form-control" runat="server"  Width="95%" data-toggle="tooltip" data-placement="right" title="Route Details" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                             <%--</div>--%>
                                         <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                           <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span> Date Of Dispatch</label><br />
                                          <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDateOfDispatch" OnClientDateSelectionChanged="SelectD" Format="dd-MM-yyyy" ID="CalendarExtender3"></cc1:CalendarExtender>
                                          <asp:TextBox ID="txtDateOfDispatch"  data-toggle="tooltip"  data-placement="right" onChange="javascript: txtToDateChanged(this);" title="Date Of Dispatch" Cssclass="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px"></asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="dd1" runat="server" />
                                    </div>                                         
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Time Of Dispatch</label><br />
                                        <input type="time" id="txtTimeOfDispatch" onchange="onTimeChange()"   runat="server"   data-toggle="tooltip" data-placement="right" title="Time Of Dispatch" class="form-control" >
                                       
                                    </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Duration Of Dispatch</label>
                                        <br />
                                        <asp:TextBox ID="txtDurationOfDispatch" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Duration Of Dispatch"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Deposited Amount </label>
                                        <br />
                                        <asp:TextBox ID="txtDepositedAmount" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Deposited Amount"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks1" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                                <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                                <textarea type="text" id="txtApproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></textarea>
                                            </div>    

                                  <%--  </div>--%>
                                   
                                   </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                           <asp:HiddenField ID="partycode" runat="server" />
                                        <asp:HiddenField ID="Dparty" runat="server" />
                                  

                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        
                                         <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg12()" class="fa fa-cut" OnClick="btnReject_Click" />
                                         <asp:LinkButton ID="btnApprove" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Issue" OnClick="btnApprove_Click"/>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                    </div>
                                 <div id="approver" runat="server">
                                                <div class="x_title">
                                                    <h4>Approval Summary</h4>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="x_title">
                                                    <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                        HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                        HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" Width="1218px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Transaction_Date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approver Role" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                    </asp:GridView>
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
