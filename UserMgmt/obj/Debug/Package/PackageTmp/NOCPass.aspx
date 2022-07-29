<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="NOCPass.aspx.cs" Inherits="UserMgmt.NOCPass" %>


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
                                <title>Dispatch Form </title>
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
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                         if (document.getElementById('<%=ddlNOCRequestNumber.ClientID%>').value == '') {
                                             alert("Select NOC Request Number");
                                            document.getElementById("<% =ddlNOCRequestNumber.ClientID%>").focus();
                                            return false;
                                         }
                                         if (document.getElementById('<%=ddlDepot.ClientID%>').value == '') {
                                            alert("Select Depot");
                                            document.getElementById("<% =ddlDepot.ClientID%>").focus();
                                            return false;
                                         }

                                  if (document.getElementById('<%=ddlDispatchType.ClientID%>').value == '') {
                                      alert("Select Dispatch Type");
                                            document.getElementById("<% =ddlDispatchType.ClientID%>").focus();
                                            return false;
                                  }

                                        if (document.getElementById('<%=txtTaxInvoiceNo.ClientID%>').value == '') {
                                             alert("Enter Tax Invoice No");
                                            document.getElementById("<% =txtTaxInvoiceNo.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=ddlDispatchVAT.ClientID%>').value == '') {
                                            alert("Select Dispatch VAT");
                                            document.getElementById("<% =ddlDispatchVAT.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtQtyDispatch.ClientID%>').value == '') {
                                            alert("Enter Qty Dispatch");
                                            document.getElementById("<% =txtQtyDispatch.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                             alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                         }
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                          <li >
                                            <asp:LinkButton ID="btnRequestForPass" runat="server" OnClick="btnRequestForPass_Click" ><span style="color:#fff;font-size:14px;">Request For Pass</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="btnApplyForPass" runat="server" OnClick="btnApplyForPass_Click"><span style="color:#fff;font-size:14px;"> Apply For Pass</span></asp:LinkButton></li>
                                      
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
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>NOC Request Number</label>
                                        <br />
                                        <asp:DropDownList ID="ddlNOCRequestNumber" runat="server" CssClass="form-control" Width="70%" data-toggle="tooltip" data-placement="right" title="NOC Request Number"></asp:DropDownList>
                                    </div>

                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Pass Required For</label>
                                        <br />
                                        <asp:TextBox ID="txtPassRequiredFor" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Pass Required For" ReadOnly="true"></asp:TextBox>
                                    </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                  <label class="control-label" style="display: inline"><span style="color: red"></span>Name of Supplier Unit</label>
                                        <br />
                                        <asp:TextBox ID="txtSupplierUnit" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Name of Supplier Unit" ReadOnly="true"></asp:TextBox>
                                            </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                  <label class="control-label" style="display: inline"><span style="color: red"></span>Customer Name</label>
                                        <br />
                                        <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Customer Name" ReadOnly="true"></asp:TextBox>
                                            </div>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display: inline;font-size:small"><span style="color: red"></span>Customer Address</label>
                                        <br />
                                        <asp:TextBox ID="txtCustomerAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right"  title="Customer Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Depot</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDepot" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Depot"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Dispatch Type</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDispatchType" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch Type"></asp:DropDownList>
                                    </div>
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Tax Invoice No</label>
                                        <br />
                                        <asp:TextBox ID="txtTaxInvoiceNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Tax Invoice No" ></asp:TextBox>
                                    </div>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>NOC Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtNOCQuantity" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="NOC Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Lifted Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtLiftedQuantity" CssClass="form-control" runat="server"   data-toggle="tooltip" data-placement="right"  title="Lifted Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>NOC Balance Quantity</label>
                                        <br />
                                        
                                        <asp:TextBox ID="txtBalanceQuantity" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="NOC Balance Quantity" ReadOnly="true"></asp:TextBox>
                                    </div>

                                      
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                  
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>From Dispatch VAT</label>
                                        <br />
                                         <asp:DropDownList ID="ddlDispatchVAT" runat="server"  CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Dispatch VAT"></asp:DropDownList>
                                    </div>
                                      
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Available Qty</label>
                                        <br />
                                        <asp:TextBox ID="txtAvailableQty" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" title="Available Qty" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Qty Dispatch</label>
                                        <br />
                                     
                                        <asp:TextBox ID="txtQtyDispatch" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Qty Dispatch" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
