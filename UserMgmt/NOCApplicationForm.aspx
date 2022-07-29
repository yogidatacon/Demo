<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="NOCApplicationForm.aspx.cs" Inherits="UserMgmt.NOCApplicationForm" %>

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
                                <title>NOC Application Form</title>
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


                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_txtMobileNumber').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Mobile Number.");
                                            $('#' + BodyContent_txtMobileNumber).val("");
                                            $('#' + BodyContent_txtMobileNumber).focus();
                                        }
                                    }
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtEmailID').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtEmailID').val("");
                                            $('#BodyContent_txtEmailID').focus();
                                            return false;
                                        }

                                    }
                                    function SelectDate(e) {
                                        debugger;
                                        var dat1e = $('#BodyContent_txtNOCDate').val();
                                        $('#BodyContent_txtdob').val(dat1e);
                                    }
                                </script>
                                <script type="text/javascript">
                                    function validationMsg() {
                                         if (document.getElementById('<%=ddlNOCFor.ClientID%>').value == 'Select') {
                                            alert("Select NOCFor");
                                            document.getElementById("<% =ddlNOCFor.ClientID%>").focus();
                                            return false;
                                        }
                                     
                                   <%--  if (document.getElementById('<%=txtNOCDate.ClientID%>').value == '') {
                                         alert("Enter NOC Date");
                                            document.getElementById("<% =txtNOCDate.ClientID%>").focus();
                                            return false;
                                     }--%>

                                         if (document.getElementById('<%=ddlNumberTypes.ClientID%>').value == 'Select') {
                                            alert("Select Number Type");
                                            document.getElementById("<% =ddlNumberTypes.ClientID%>").focus();
                                            return false;
                                         }

                                       if (document.getElementById('<%=txtTenderNumber.ClientID%>').value == '') {
                                            alert("Enter TenderNumber");
                                            document.getElementById("<% =txtTenderNumber.ClientID%>").focus();
                                            return false;
                                       }
                                        if (document.getElementById('<%=txtissuedate.ClientID%>').value == '') {
                                         alert("Enter Issue Date");
                                            document.getElementById("<% =txtissuedate.ClientID%>").focus();
                                            return false;
                                     }
                                        if (document.getElementById('<%=ddlCustomerName.ClientID%>').value == 'Select') {
                                            alert("Select Customer Name");
                                            document.getElementById("<% =ddlCustomerName.ClientID%>").focus();
                                            return false;
                                        }
                                      
                                      <%-- if (document.getElementById('<%=txtPONumber.ClientID%>').value == '') {
                                            alert("Enter PO number");
                                            document.getElementById("<% =txtPONumber.ClientID%>").focus();
                                            return false;
                                        }--%>
                                       
                                        if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                    }
                                </script>


                                 <script type="text/javascript">
                                    function CheckDepot() {
                                        debugger;
                                        if (document.getElementById('<%=txtDepotName.ClientID%>').value == '') {
                                            alert("Enter Depot Name");
                                            document.getElementById("<% =txtDepotName.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtQuantity.ClientID%>').value == '') {
                                            alert("Enter Quantity");
                                            document.getElementById("<% =txtQuantity.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>

                                <script type="text/javascript">
                                    function CheckDiscription() {
                                        debugger;
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                            alert("Please Attach file");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                            alert("Enter Document Name");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                    function validationMsg2() {
                                        if (document.getElementById('<%=currentlevel.ClientID%>').value == '2')
                                        {
                                            if (document.getElementById('<%=txtValiedDate.ClientID%>').value == '')
                                            {
                                                alert("Select Valid Date");
                                                document.getElementById("<% =txtValiedDate.ClientID%>").focus();
                                                return false;
                                            }
                                            if (document.getElementById('<%=txtApproverComment.ClientID%>').value == '')
                                            {
                                                alert("Enter Approver Remarks");
                                                document.getElementById("<% =txtApproverComment.ClientID%>").focus();
                                                return false;
                                            }
                                            //if (GetConformation() === false) {
                                            //    alert("Please Check Digilock Password");
                                            //    return false;
                                            //}
                                        }
                                        else
                                        {
                                             if (document.getElementById('<%=txtApproverComment.ClientID%>').value == '') {
                                                alert("Enter Approver Remarks");
                                                document.getElementById("<% =txtApproverComment.ClientID%>").focus();
                                                return false;
                                             }
                                            //if (GetConformation() === false) {
                                            //    alert("Please Check Digilock Password");
                                            //    return false;
                                            //}
                                        }
                                    }
                                    function validationMsg22() {
                                       
                                            if (document.getElementById('<%=txtApproverComment.ClientID%>').value == '') {
                                                alert("Enter Approver Remarks");
                                                document.getElementById("<% =txtApproverComment.ClientID%>").focus();
                                                return false;

                                            }
                                        //if(GetConformation()===false)
                                        //{
                                        //    alert("Please Check Digilock Password");
                                        //    return false;
                                        //}
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
                                        else {
                                            return true;
                                        }
                                    }
                                    function SelectValiedDate(e) {
                                       
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
                                        $('#BodyContent_txtValiedDate').val(todayDate);
                                        $('#BodyContent_txtvalieddate1').val(todayDate);
                                    }
                                    function SelectValiedDate1(e) {

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
                                        $('#BodyContent_txtissuedate').val(todayDate);
                                        $('#BodyContent_issuedate').val(todayDate);
                                    }
                                    function GetConformation() {
                                      
                                        var retVal = prompt("Record cannot be changed after submission \n \n Confirm Password to Submit : ", "your Password here");
                                        if (retVal == $('#BodyContent_conformation').val())
                                        {
                                            return true;
                                        }
                                        else if (retVal == $('#BodyContent_conformation').val()) {
                                            return true;
                                        }
                                        else if (retVal == $('#BodyContent_conformation').val()) {
                                            return true     ;
                                        }
                                      else
                                      {
                                         
                                          return false;

                                      }
                                    }
                                </script>

                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton ID="btnNOCApplication" runat="server" OnClick="btnNOCApplication_Click" ><span style="color:#fff;font-size:14px;">NOC Application</span></asp:LinkButton></li>
                                       
                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>NOC Application Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:HiddenField ID="conformation" runat="server" />
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>NOC For</label>
                                        <br />
                                         <asp:HiddenField ID="noc_id" runat="server" />
                                        <asp:DropDownList ID="ddlNOCFor" runat="server" CssClass="form-control" Width="70%" data-toggle="tooltip" data-placement="right" title="NOC For"></asp:DropDownList>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>NOC Application Number</label>
                                        <br />
                                        <asp:TextBox ID="txtNOCNumber" Width="80%" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="NOC Number" ReadOnly="true"></asp:TextBox>
                                    </div>
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>NOC Date</label>
                                                <br />
                                                <%--<cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtNOCDate" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>--%>
                                                <asp:TextBox ID="txtNOCDate"  data-toggle="tooltip"  data-placement="right" title="NOC Date " class="form-control validate[required]"  AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <%--<asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />--%>
                                                <%--<asp:HiddenField ID="txtdob" runat="server" />--%>
                                            </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                      <div  runat="server" id="gender" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Number Type</label>
                                        <br />
                                        <asp:DropDownList ID="ddlNumberTypes" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Number Type" OnSelectedIndexChanged="ddlNumberTypes_SelectedIndexChanged" >
                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Text="Tender Number" Value="T"></asp:ListItem> 
                                            <asp:ListItem Text="Permit Number" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="PO No" Value="O"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                      <%--  <label class="control-label" style="display: inline" runat="server"  id="tender"><span style="color: red">*</span>Tender Number</label>--%>
                                                 <span style="color: red">*</span><asp:Label ID="tender" runat="server" Font-Bold="true" CssClass="control-label"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtTenderNumber" CssClass="form-control" Width="80%"  runat="server" data-toggle="tooltip" data-placement="right" title="Tender Number"></asp:TextBox>
                                    </div>

                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="Div1" runat="server">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Issue Date </label>
                                                <br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txtissuedate" OnClientDateSelectionChanged="SelectValiedDate1" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtissuedate"  data-toggle="tooltip" data-placement="right" title="Issue Date" Cssclass="form-control" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="issuedate" runat="server" />
                                            </div>
                                        
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                              
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Customer Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" Width="70%" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Customer Name"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Mobile Number</label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNumber" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" title="Mobile Number" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Email ID</label>
                                        <br />
                                        <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server"   data-toggle="tooltip" data-placement="right"  title="Email ID" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <%--<label class="control-label" style="display: inline" runat="server" id="pono"><span style="color: red"></span>PO Number</label>--%>
                                          <span style="color: red"></span><asp:Label ID="pono" runat="server" Font-Bold="true" CssClass="control-label"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtPONumber" CssClass="form-control"  runat="server" data-toggle="tooltip" data-placement="right" title="PO Number"></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="col-md-11 col-sm-11 col-xs-12">
                                        <label class="control-label" style="display: inline;font-size:small"><span style="color: red"></span>Customer Address</label>
                                        <br />
                                        <asp:TextBox ID="txtCustomerAddress" CssClass="form-control" runat="server" data-toggle="tooltip" Width="100%" data-placement="right"  title="Customer Address" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>District</label>
                                        <br />
                                        
                                        <asp:TextBox ID="txtDistrict" CssClass="form-control" runat="server" Width="60%" data-toggle="tooltip" data-placement="right" title="District" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Thana</label>
                                        <br />
                                     
                                        <asp:TextBox ID="txtThana" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Thana" ReadOnly="true"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>State</label>
                                        <br />
                                         
                                  <asp:TextBox ID="txtState" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="State" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>PIN Code</label>
                                        <br />
                                        <asp:TextBox ID="txtPINCode" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="PIN Code" ReadOnly="true"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                               

                                       

                                        
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                    <div class="x_title"> </div>
                                   <div class="clearfix"></div>
                                    <div id="depotdiv" runat="server">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <%--<label class="control-label" style="display: inline"><span style="color: red">*</span>Depot Name</label>--%>
                                    <span style="color: red">*</span><asp:Label ID="lblDepot" runat="server" Font-Bold="true" CssClass="control-label"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtDepotName" CssClass="form-control" runat="server" Width="70%" data-toggle="tooltip" data-placement="right" title="Depot Name"></asp:TextBox>
                                    </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Quantity</label>
                                        <br />
                                        <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" Width="60%" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Quantity"></asp:TextBox>
                                              <span>
                                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDepot()" Text="Add" OnClick="DepotAdd" />
                                            </span>
                                    </div>
                                   </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div id="dummyDepotDatatable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Depot">
                                            <thead>
                                                <tr>
                                                    <th>Depot Name</th>
                                                    <th>Quantity</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="DepotDatatable">
                                            </tbody>
                                        </table>
                                    </div>
                                    <div id="EnaTable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Depot">
                                            <thead>
                                                <tr>
                                                    <th>Product Name</th>
                                                    <th>Quantity</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody id="DepotDatatable">
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="gridDepotData" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" CssClass="table table-striped responsive-utilities jambo_table" runat="server"  AutoGenerateColumns="false">
                                            <Columns>
                                              
                                                <asp:TemplateField HeaderText="Depot Name" ControlStyle-Width="380px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepotName" runat="server"  Text='<%#Eval("Depot_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Requested Quantity(BL)"  ControlStyle-Width="380px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server"  Text='<%#Eval("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved Quantity(BL)" ControlStyle-Width="380px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQTY" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" Text='<%#Eval("AppQTY") %>' AutoPostBack="true" OnTextChanged="txtQTY_TextChanged" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" CommandArgument='<%#Eval("depot_id") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="DepotRemove_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" CssClass="paginationClass" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Document Name</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Name"></asp:TextBox>
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
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
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" data-toggle="tooltip" Width="90%" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline" id="valieddate12" runat="server">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Valid Date Upto</label>
                                                <br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtValiedDate" OnClientDateSelectionChanged="SelectValiedDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtValiedDate"  data-toggle="tooltip" data-placement="right" title="Valid Date Upto" Cssclass="form-control" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtvalieddate1" runat="server" />
                                            </div>
                                            </div>
                                    <div id="approverremaks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Approver Comments</label><br />
                                        <asp:TextBox ID="txtApproverComment" runat="server" CssClass="form-control" data-toggle="tooltip" Height="5%" Width="95%" data-placement="right" title="Approver Comments" TextMode="MultiLine"></asp:TextBox>
                                        <asp:HiddenField ID="currentlevel" runat="server" />
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <asp:HiddenField ID="fisicalyear" runat="server" />
                                    <asp:HiddenField ID="partycode" runat="server" />
                                    <asp:HiddenField ID="rolename" runat="server" />
                                   
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" Cssclass="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg2();" runat="server" Style="width: 85px;" Text="Approve" OnClick="btnApprove_Click" />
                                        <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg22();" OnClick="btnReject_Click" class="fa fa-cut" />
                                         <asp:LinkButton ID="btnReferBack" Text="Refer Back" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return validationMsg22();" OnClick="btnReferBack_Click" class="fa fa-backward" />
                                        <asp:LinkButton ID="btnupdate" runat="server" Visible="false" OnClientClick="javascript:return validationMsg2();" CssClass="btn btn-primary" OnClick="btnupdate_Click"><span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                         <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                            CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                    </div>
                                    <p>&nbsp;</p>
                                    <div id="approv" runat="server">
                                        <div class="x_title">
                                            <h4>Approval Summary</h4>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div style="color:gray" class="col-md-10 col-sm-12 col-xs-12 form-inline" class="x_title">
                                            <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" >
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
