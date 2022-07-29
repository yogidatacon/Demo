<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DispatchTypeMaster.aspx.cs" Inherits="UserMgmt.DispatchTypeMaster" %>

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
                                <title>Dispatch Type Master</title>
                                <script  type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtDispatchCode.ClientID%>').value == '') {
                                            alert("Enter  Dispatch Code");
                                            return false;
                                            document.getElementById("<% =txtDispatchCode.ClientID%>").focus();
                                        }
                                          if (document.getElementById('<%=txtDispatchname.ClientID%>').value == '') {
                                              alert("Enter  Dispatch Name");
                                            return false;
                                            document.getElementById("<% =txtDispatchname.ClientID%>").focus();
                                        }
                                    }
                                </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                     <li>
                                         <asp:LinkButton ID="partytypemaster" OnClick="partytypemaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="partyfinancialyears" OnClick="partyfinancialyears_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Type Financial Year</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="partymaster" OnClick="partymaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Party Master</span></asp:LinkButton></li>
                                         <li>
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
                                        <li class="active">
                                         <asp:LinkButton ID="DispatchTypeMaster1" OnClick="DispatchTypeMaster1_Click" runat="server"><span style="color:#fff;font-size:14px;">Dispatch Type Master</span></asp:LinkButton></li>

                                       </ul> 
                                    <br />
                                    <a>
                                        <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right" ><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                    <div class="row">
                                        <div class="x_title">
                                            <h2>Dispatch Type Form</h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                             <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Dispatch Type Code</label><br />
                                                  <asp:TextBox ID="txtDispatchCode" AutoComplete="off" runat="server" Height="30px" Width="250px"  MaxLength="3" style="text-transform:uppercase" data-toggle="tooltip" data-placement="right" title="Dispatch Type Code"  class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-6 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span> Dispatch Type Name</label><br />
                                                <asp:TextBox ID="txtDispatchname"  runat="server" Height="30px" Width="250px" style="text-transform:capitalize" data-toggle="tooltip" data-placement="right" title="Dispatch Type Name"   class="form-control"></asp:TextBox>
                                            </div>
                                                <div class="clearfix"></div>
                                                  <p>&nbsp;</p>
                                            </div>
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="javascript:return validationMsg()" OnClick="btnSave_Click"  />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
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
        </div></div>
</asp:Content>
