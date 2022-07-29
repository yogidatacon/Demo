<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DesignationForm.aspx.cs" Inherits="UserMgmt.DesignationForm" %>

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
                                <title>Designation Form</title>
                            </head>
                            <body>
                                 <ul class="nav nav-tabs">
                                         
                                        <li >
                                            <asp:LinkButton ID="Designation_1" OnClick="Designation_1_Click" runat="server"><span style="color:#fff;font-size:14px;">Designation Types</span></asp:LinkButton></li>
                                        <li  class="active">
                                            <asp:LinkButton ID="Designation_2" OnClick="Designation_2_Click" runat="server"><span style="color:#fff;font-size:14px;">Designations</span></asp:LinkButton></li>
                                     <li >
                                            <asp:LinkButton ID="Employee_Details" OnClick="Employee_Details_Click" runat="server"><span style="color:#fff;font-size:14px;">Employee Details</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="UserRegistration" OnClick="UserRegistration_Click" runat="server"><span style="color:#fff;font-size:14px;">User Registration</span></asp:LinkButton></li>
                                    </ul>
                                <br />
                                <a>
                                <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" Style="float: right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Designation Form</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation Type</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDesignationtype" runat="server" Width="70%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Molasses Type"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation Code</label><br />
                                        <asp:TextBox ID="txtDesignationCode" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Designation Name</label><br />
                                        <asp:TextBox ID="txtDesignationName" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title=" Available Qty(LPL)" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    
                                    <div class="col-md-9 col-sm-9 col-xs-9 form-inline">

                                        <asp:HiddenField ID="txtid" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />


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
