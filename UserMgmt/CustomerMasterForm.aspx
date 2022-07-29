<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerMasterForm.aspx.cs" Inherits="UserMgmt.CustomerMasterForm" %>


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
                                <title>Distillery Customer Form</title>
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
                                            $('#BodyContent_txtMobileNumber').val('');
                                            $('#BodyContent_txtMobileNumber').focus();
                                        }
                                    }
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_txtEmailID').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_txtEmailID').val('');
                                            $('#BodyContent_txtEmailID').focus();
                                            return false;
                                        }

                                    }
                                    function Validate()
                                    {
                                        var mobileN = $('#BodyContent_txtPINCode').val().length;

                                        if (mobileN != 6) {
                                            alert("Invalid Pincode.");
                                            $('#BodyContent_txtPINCode').val('');
                                            $('#BodyContent_txtPINCode').focus();
                                        }
                                    }
                                    function Validate1(e) {
                                        var keyCode = e.keyCode || e.which;
                                        var regex = /^[a-zA-Z ]+$/; //^[A-Za-z]*$/;
                                        var isValid = regex.test(String.fromCharCode(keyCode));
                                        return isValid;
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                      

                                         
                                        if (document.getElementById('<%=txtCustomerName.ClientID%>').value == '') {
                                            alert("Enter Customer Name");
                                            document.getElementById("<% =txtCustomerName.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                        if (document.getElementById('<%=txtMobileNumber.ClientID%>').value == '') {
                                            alert("Enter MobileNumber");
                                            document.getElementById("<% =txtMobileNumber.ClientID%>").focus();
                                            return false;
                                        }
                                       
                                        if (document.getElementById('<%=txtEmailID.ClientID%>').value == '') {
                                            alert("Enter Email ID");
                                            document.getElementById("<% =txtEmailID.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlState.ClientID%>').value == 'Select')
                                        {
                                            alert("Select State");
                                            document.getElementById("<% =ddlState.ClientID%>").focus();
                                            return false;

                                         }
                                        if (document.getElementById('<%=txtdistrict.ClientID%>').value == '')
                                        {
                                            alert("Select District");
                                            document.getElementById("<% =txtdistrict.ClientID%>").focus();
                                            return false;
                                        }
                                     
                                        if (document.getElementById('<%=txtThana.ClientID%>').value == '') {
                                        alert("Select Thana");
                                            document.getElementById("<% =txtThana.ClientID%>").focus();
                                            return false;
                                     }
                                        if (document.getElementById('<%=txtPINCode.ClientID%>').value == '') {
                                            alert("Enter PINCode");
                                            document.getElementById("<% =txtPINCode.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtCustomerAddress.ClientID%>').value == '') {
                                            alert("Enter Customer Address");
                                            document.getElementById("<% =txtCustomerAddress.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                             
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>NOC Customer Form</h2>
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
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Customer Name</label>
                                        <br />
                                        <asp:TextBox ID="txtCustomerName" runat="server" style="text-transform:capitalize" Width="67%" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return Validate1(event);" title="Customer Name"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Mobile Number</label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNumber" CssClass="form-control" Width="40%" MaxLength="10"  runat="server" data-toggle="tooltip" data-placement="right" onchange="phoneValidate()" title="Mobile Number"  onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                                    </div>
                                     
                                      
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Email ID</label>
                                        <br />
                                        <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" Width="67%"   data-toggle="tooltip" data-placement="right"  title="Email ID" onchange="emailValidate(this)" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>State</label>
                                        <br />
                                      <asp:DropDownList ID="ddlState" Width="67%" AutoPostBack="true" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" title="State"></asp:DropDownList>
                                        
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>District</label>
                                        <br />
                                       <asp:TextBox ID="txtdistrict" runat="server" style="text-transform:capitalize" Width="67%" CssClass="form-control" data-toggle="tooltip" onkeypress="return Validate1(event);" data-placement="right" title="District"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Thana</label>
                                        <br />
                                    <asp:TextBox ID="txtThana" runat="server" style="text-transform:capitalize" Width="67%" CssClass="form-control" data-toggle="tooltip" onkeypress="return Validate1(event);" data-placement="right" title="thana"></asp:TextBox>
                 
                                     
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>PIN Code</label>
                                        <br />
                                        <asp:TextBox ID="txtPINCode" CssClass="form-control" Width="40%" MaxLength="6"  runat="server" data-toggle="tooltip" onchange="Validate()" data-placement="right" title="PIN Code" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                                    </div>
                                    
                                         
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline;font-size:small"><span style="color: red">*</span>Customer Address</label>
                                        <br />
                                        <asp:TextBox ID="txtCustomerAddress" CssClass="form-control" Width="67%" Height="100px" runat="server" data-toggle="tooltip" MaxLength="100"  data-placement="right"  title="Customer Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:HiddenField ID="party_code" runat="server" />
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"  OnClick="btnCancel_Click"
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
