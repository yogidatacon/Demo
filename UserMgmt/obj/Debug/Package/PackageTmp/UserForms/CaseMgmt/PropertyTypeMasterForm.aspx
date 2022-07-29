<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PropertyTypeMasterForm.aspx.cs" Inherits="UserMgmt.PropertyTypeMasterForm" %>
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
                                <title>Property Type Master Form</title>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        if (document.getElementById('<%=txtPropertyTypeCode.ClientID%>').value == '') {
                                            alert("Enter  Property Type Code ");
                                              document.getElementById("<% =txtPropertyTypeCode.ClientID%>").focus();
                                            return false;
                                          
                                        }
                                        if (document.getElementById('<%=txtPropertyType.ClientID%>').value == '') {
                                            alert("Enter  Property Type");
                                             document.getElementById("<% =txtPropertyType.ClientID%>").focus();
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
                                            <h2>Property Type Master Form</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                            <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span>Property Type Code</label> <br />
                                            <asp:TextBox ID="txtPropertyTypeCode"  AutoComplete="off" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Property Type Code" Width="70%"  onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtPropertyTypeCode_TextChanged" Height="50%" runat="server" ></asp:TextBox>
                                                   </div>
                                              <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                        <label style="font-size: small; font-weight: bold;"><span style="color: red">*</span> Property Type</label>   <br />
                                                        <asp:TextBox ID="txtPropertyType"  AutoComplete="off" data-toggle="tooltip" CssClass="form-control" data-placement="right"  Width="70%" Height="50%"  title="Property Type" runat="server" MaxLength="50"></asp:TextBox>
                                                  </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <asp:HiddenField ID="txtid" runat="server" />
                                                <br />
                                              <p>&nbsp;</p>
                                                    <p>&nbsp;</p>
                                                       <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click"  />

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
