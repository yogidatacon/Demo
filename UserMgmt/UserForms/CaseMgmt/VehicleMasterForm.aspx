<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="VehicleMasterForm.aspx.cs" Inherits="UserMgmt.VehicleMasterForm" %>
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
                                <title>Vehicle Master Form</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtVehicleTypeCode.ClientID%>').value == '') {
                                            alert("Enter  Vehicle Type Code");
                                              document.getElementById("<% =txtVehicleTypeCode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtVehicleType.ClientID%>').value == '') {
                                            alert("Enter  VehicleType");
                                             document.getElementById("<% =txtVehicleType.ClientID%>").focus();
                                            return false;
                                        }
                                    }
                                </script>
                                    <script language="javascript" type="text/javascript">
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
                            </head>
                            <body>
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecord_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Vehicle Master Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Vehicle Type Code</label> <br />
                                            <asp:TextBox ID="txtVehicleTypeCode" ReadOnly="true" AutoComplete="off" CssClass="form-control"  data-toggle="tooltip" data-placement="right" onkeypress="return onlyDotsAndNumbers(this,event);" title="Vehicle Code" Width="70%" Height="50%" AutoPostBack="true" OnTextChanged="txtVehicleTypeCode_TextChanged" runat="server"></asp:TextBox>
                                                   </div>
                                              <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Vehicle Type</label>   <br />
                                                        <asp:TextBox ID="txtVehicleType"  AutoComplete="off" data-toggle="tooltip" CssClass="form-control" data-placement="right"  Width="70%" Height="50%" title="Vehicle Type"  runat="server"></asp:TextBox>
                                                  </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <asp:HiddenField ID="txtid" runat="server" />
                                                <br />
                                              <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                       <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />

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
