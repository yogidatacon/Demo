<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="OtherExcisableArticlesForm.aspx.cs" Inherits="UserMgmt.OtherExcisableArticlesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedBodyContent" runat="server">
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
                                <title>Vehicle Form</title>
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
                                  <script>
                                    function onlyAlphabets(e, t) {
                                        try {
                                            if (window.event) {
                                                var charCode = window.event.keyCode;
                                            }
                                            else if (e) {
                                                var charCode = e.which;
                                            }
                                            else { return true; }
                                            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32)
                                                return true;
                                            else
                                                return false;
                                        }
                                        catch (err) {
                                            alert(err.Description);
                                        }
                                    }
                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_NestedBodyContent_txtMobileNo').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Mobile No.");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').val("");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').focus();
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlVehicleType.ClientID%>').value == 'Select') {
                                            alert("Select Vehicle Type");
                                            document.getElementById("<% =ddlVehicleType.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtVehicleName.ClientID%>').value == '') {
                                             alert("Enter VehicleName");
                                            document.getElementById("<% =txtVehicleName.ClientID%>").focus();
                                            return false;

                                         }
                                      <%-- else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtVehicleName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Vehicle Name should be minimum 3 character");
                                                 document.getElementById("<% =txtVehicleName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>
                                         <%--if (document.getElementById('<%=txtManufacturer.ClientID%>').value == '') {
                                             alert("Select Manufacturer");
                                            document.getElementById("<% =txtManufacturer.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtManufacturer").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Manufacturer should be minimum 3 character");
                                                 document.getElementById("<% =txtManufacturer.ClientID%>").focus();
                                                 return false;
                                             }
                                         }


                                         if (document.getElementById('<%=txtMakeModel.ClientID%>').value == '') {
                                             alert("Enter Model");
                                            document.getElementById("<% =txtMakeModel.ClientID%>").focus();
                                            return false;

                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtMakeModel").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Model should be minimum 3 character");
                                                 document.getElementById("<% =txtMakeModel.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>

                                        <%-- if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                                             alert("Enter Vehicle No");
                                            document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                            return false;

                                         }
                                      else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtVehicleNo").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Vehicle No should be minimum 3 character");
                                                 document.getElementById("<% =txtVehicleNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>
                                        <%-- if (document.getElementById('<%=txtChassisNo.ClientID%>').value == '') {
                                             alert("Enter Chassis No");
                                            document.getElementById("<% =txtChassisNo.ClientID%>").focus();
                                            return false;

                                         }
                                         if (document.getElementById('<%=txtRCNo.ClientID%>').value == '') {
                                             alert("Enter RC No");
                                            document.getElementById("<% =txtRCNo.ClientID%>").focus();
                                            return false;
                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtRCNo").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <5)
                                             {
                                                 alert("RC No should be minimum 5 character");
                                                 document.getElementById("<% =txtRCNo.ClientID%>").focus();
                                                 return false;
                                             }
                                         }

                                         if (document.getElementById('<%=txtOwnerName.ClientID%>').value == '') {
                                             alert("Enter Owner Name");
                                            document.getElementById("<% =txtOwnerName.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtOwnerName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Owner Name should be minimum 10 character");
                                                 document.getElementById("<% =txtOwnerName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if ((document.getElementById('<%=txtMobileNo.ClientID%>').value<"1") || (document.getElementById('<%=txtMobileNo.ClientID%>').value.length<"10")) {
                                            debugger;
                                            alert("Enter valid Mobile No");
                                            document.getElementById("<% =txtMobileNo.ClientID%>").focus();
                                            return false;

                                          } --%>

                                         <%-- if (document.getElementById('<%=txtOwnerPermanentAddress.ClientID%>').value == '') {
                                             alert("Enter Owner Permanent Address");
                                            document.getElementById("<% =txtOwnerPermanentAddress.ClientID%>").focus();
                                            return false;

                                          }
                                         else
                                         {
                                              var ref_code = $("#BodyContent_NestedBodyContent_txtOwnerPermanentAddress").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <10)
                                             {
                                                 alert("Owner Permanent Address should be minimum 10 character");
                                                 document.getElementById("<% =txtOwnerPermanentAddress.ClientID%>").focus();
                                                 return false;
                                             }
                                         }

                                        if (document.getElementById('<%=txtOwnerPresentAddress.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter Owner Present Address");
                                            document.getElementById("<% =txtOwnerPresentAddress.ClientID%>").focus();
                                            return false;

                                        }
                                         --%>
                                       
                                         
                                    }
                                </script>
                                   <script type="text/javascript">
                                    function copyValue(Chk) {
                                        if(Chk.checked)
                                        {
                                             document.getElementById('<%=txtOwnerPresentAddress.ClientID%>').value  = document.getElementById('<%=txtOwnerPermanentAddress.ClientID%>').value;
                                        }
                                       
                                    }
                                </script>
                              <script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=grdExcisableArticlesView] input[type=checkbox]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=grdExcisableArticlesView] input[type=checkbox]").removeAttr("checked");
                $(this).attr("checked", "checked");
            }
        });
    });
</script>   
                               
                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Vehicle Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                
                                <div class="x_content">
                                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   <%-- <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Vehicle Type</label><br />
                                                <asp:DropDownList ID="drpVtype" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Vehicle Type"></asp:DropDownList>
                                              </div>--%>
                                    <div id ="serchid" runat="server">
                                 <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Vehicle No</label><br />
                                                <asp:TextBox ID="txtVNo"  autocomplete="off" runat="server"   CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                           
                                             <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                 <label class="control-label"><span style="color: red"></span></label><br />
                                            <span> <asp:Button ID="btnVehicleSearch" runat="server"  Text="Vehicle Search" CssClass="btn btn primary" OnClick="btnVehicleSearch_Click" /></span>
                                                 </div>
                                    
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                          
                                    <asp:GridView ID="grdExcisableArticlesView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdExcisableArticlesView_PageIndexChanging" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Vehicle Type" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleType" runat="server" Visible="true" Text='<%#Eval("vehicle_type") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle Type" ItemStyle-Font-Bold="true" Visible="false"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleTypeCode" runat="server" Visible="true" Text='<%#Eval("vehicle_type_code") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleName" runat="server" Visible="true" Text='<%#Eval("vehiclename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleNo" runat="server" Visible="true" Text='<%#Eval("vehicle_number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Model No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModel" runat="server" Visible="true" Text='<%#Eval("makemodel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manufacturer" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManufacturer" runat="server" Visible="true" Text='<%#Eval("manufacturer_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chassis No" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNo" runat="server" Visible="true" Text='<%#Eval("chasisno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration Number" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegistrationNumber" runat="server" Visible="true" Text='<%#Eval("registrationno") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerName" runat="server" Visible="true" Text='<%#Eval("ownername") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Owner Permanent Address" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerPermanentAddress" runat="server" Visible="true" Text='<%#Eval("permanentaddress") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobileNo" runat="server" Visible="true" Text='<%#Eval("contactno") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                     <asp:CheckBox ID="chselect" OnCheckedChanged="chselect_CheckedChanged" AutoPostBack="true" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                    

                                        </div>
                                      <div  class="x_title"></div>
                                     </div>
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Vehicle Type </label>
                                        <br />
                                        <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Vehicle Type"></asp:DropDownList>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Vehicle Name </label>
                                        <br />
                                         <asp:TextBox ID="txtVehicleName"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return onlyAlphabets(this,event);" title="Vehicle Name" MaxLength="50" ></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Manufacturer </label>
                                        <br />
                                        <asp:TextBox ID="txtManufacturer"  autocomplete="off" runat="server"  CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Manufacturer"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span> Model </label>
                                        <br />
                                        <asp:TextBox ID="txtMakeModel"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Model" MaxLength="50" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Vehicle No</label>
                                        <br />
                                        <asp:TextBox ID="txtVehicleNo"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Vehicle No" MaxLength="10" ></asp:TextBox>
                                    </div>

                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Chassis No</label>
                                        <br />
                                        <asp:TextBox ID="txtChassisNo"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Chassis No" MaxLength="50" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Engine No </label>
                                        <br />
                                        <asp:TextBox ID="txtengno"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Registration Number" MaxLength="50" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>RC No </label>
                                        <br />
                                        <asp:TextBox ID="txtRCNo"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Registration Number" MaxLength="50" ></asp:TextBox>
                                    </div>
                                    
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>GPS Company</label>
                                        <br />
                                        <asp:TextBox ID="txtGPSCompany"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="GPS Company" MaxLength="150" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>IMEI No </label>
                                        <br />
                                        <asp:TextBox ID="txtIMEI"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="IMEI No" MaxLength="20" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>SIM No </label>
                                        <br />
                                        <asp:TextBox ID="txtSIM"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="SIM No" MaxLength="30" ></asp:TextBox>
                                    </div>
                                    
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Owner Name</label>
                                        <br />
                                        <asp:TextBox ID="txtOwnerName"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Owner Name" MaxLength="50" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Mobile No </label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo"  autocomplete="off" onchange="phoneValidate()"  CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" MaxLength="10" title="Mobile No" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>SDR/CAF</label>
                                        <br />
                                        <asp:DropDownList ID="ddlSDR_CAF" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="SDR/CAF">
                                             <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                             <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                             <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-5 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtremarks" Width="85%"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Remarks" MaxLength="150" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Permanent Address </label>
                                        &nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="chk" runat="server" onclick="copyValue(this)" /> Copy Address
                                        <br />
                                        <asp:TextBox ID="txtOwnerPermanentAddress"  autocomplete="off" CssClass="form-control" runat="server" Height="10%" Width="86%" data-toggle="tooltip" data-placement="right" title="Owner Permanent Address" TextMode="MultiLine" MaxLength="250" ></asp:TextBox>
                                    </div>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Present Address</label>
                                        <br />
                                         <asp:TextBox ID="txtOwnerPresentAddress"  autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Height="10%" Width="86%" data-placement="right" title="Owner Present Address" TextMode="MultiLine" MaxLength="250" ></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" onclick="btnSaveasDraft_Click" runat="server" class="btn btn-info pull-left">
                                                    <span aria-hidden="true"  class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger"  OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                    </div></body>
                            </html>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  

</asp:Content>
