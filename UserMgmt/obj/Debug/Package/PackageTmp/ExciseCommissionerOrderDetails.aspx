<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="ExciseCommissionerOrderDetails.aspx.cs" Inherits="UserMgmt.ExciseCommissionerOrderDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
                                 <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                                <title>Excise Commissioner Order Details</title>
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
                                </script>
                                <script type="text/javascript">
    function blockAllChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false; 
        }
    </script>
                                 <script language="javascript" type="text/javascript">
                                     function validationMsg() {

                                         if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                            alert("Enter FIR details!");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                        }

                                         if (document.getElementById('<%=txtLetterNo.ClientID%>').value == '') {
                                             alert("Enter Letter No");
                                             document.getElementById("<% =txtLetterNo.ClientID%>").focus();
                                             return false;

                                         }

                                         if (document.getElementById('<%=txtDate.ClientID%>').value == '') {
                                             debugger;
                                             alert("Select Letter Date");
                                             document.getElementById("<% =txtDate.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtOrderNo.ClientID%>').value == '') {
                                             debugger;
                                             alert("Enter OrderNo");
                                             document.getElementById("<% =txtOrderNo.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtOrderDate.ClientID%>').value == '') {
                                             debugger;
                                             alert("Enter Order Date");
                                             document.getElementById("<% =txtOrderDate.ClientID%>").focus();
                                             return false;
                                         }

                                       <%--  if (document.getElementById('<%=txtDMRemarks.ClientID%>').value == '') {
                                             alert("Enter DM Remarks");
                                             document.getElementById("<% =txtDMRemarks.ClientID%>").focus();
                                             return false;
                                         }--%>

                                         if (document.getElementById('<%=txtConfiscationCaseNo.ClientID%>').value == '') {
                                             alert("Enter Confiscation CaseNo");
                                             document.getElementById("<% =txtConfiscationCaseNo.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtNameofCommissioner.ClientID%>').value == '') {
                                             alert("Enter Name of Commissioner");
                                             document.getElementById("<% =txtNameofCommissioner.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtTotalReceivedAmount.ClientID%>').value == '') {
                                             alert("Enter Total Received Amount");
                                             document.getElementById("<% =txtTotalReceivedAmount.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txthigherdate.ClientID%>').value == '') {
                                             alert("Select Appeal Date");
                                             document.getElementById("<% =txthigherdate.ClientID%>").focus();
                                             return false;
                                         }

                                         if (document.getElementById('<%=txtHigherAuthorityName.ClientID%>').value == '') {
                                             alert("Enter Higher Authority Name");
                                             document.getElementById("<% =txtHigherAuthorityName.ClientID%>").focus();
                                             return false;
                                         }

                                        <%-- if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                             alert("Enter Remarks");
                                             document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                             return false;
                                         }--%>
                                     }
                                </script>
                             
                            </head>
                            <body>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Excise Commissioner Order Details</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    
                                    <a>
                                        <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="btnSeizure_Click"  BorderStyle="Outset"> Seizure</asp:LinkButton>
                                    </a>
                                   <%-- <a>
                                        <asp:LinkButton runat="server" ID="btnFIR" Height="100%" Width="12%" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View FIR" OnClick="btnFIR_Click"  BorderStyle="Outset"> FIR</asp:LinkButton>
                                    </a>
                                     <a>
                                       <asp:LinkButton runat="server" ID="btnChargeSheet" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnChargeSheet_Click"  BorderStyle="Outset">Charge Sheet </asp:LinkButton>
                                </a>
                                <a>
                                      <asp:LinkButton runat="server" ID="btnDm" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View DM">DM</asp:LinkButton>
                                     <asp:LinkButton runat="server" ID="btnBail" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnBail_Click"  BorderStyle="Outset">Bail</asp:LinkButton>
                                </a>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Application for confiscation</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                        <asp:DropDownList ID="ddlPRFIRNo" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="PR / FIR No"></asp:DropDownList>
                                    </div>--%>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red">*</span>PR / FIR No</label>
                                                <br />                                                
                                                <asp:TextBox ID="txtPRFIRNo"  AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                        <asp:TextBox ID="txtfirdate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>' Visible="false"  title="PR / FIR No"></asp:TextBox>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Letter No</label>
                                        <br />
                                        <asp:TextBox ID="txtLetterNo" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Letter No" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Letter Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="x_title">
                                    <h2>Order for Confiscation Release</h2>
                                    <div class="clearfix"></div> </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Order No</label>
                                        <br />
                                        <asp:TextBox ID="txtOrderNo" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Order No" ></asp:TextBox>
                                    </div>
                                     
                               
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Order Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtOrderDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtOrderDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Order Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Commissioner Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtDMRemarks" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="DM Remarks" Width="80%" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="x_title">
                                    <h2>In Case of Confiscation</h2>
                                    <div class="clearfix"></div> </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Confiscation Case No</label>
                                        <br />
                                        <asp:TextBox ID="txtConfiscationCaseNo" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Confiscation Case No" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Name of the Commissioner</label>
                                        <br />
                                        <asp:TextBox ID="txtNameofCommissioner" AutoComplete="off" runat="server" onkeypress="return onlyAlphabets(this,event);" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Name of the Magistrate" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Total Received Amount</label>
                                        <br />
                                        <asp:TextBox ID="txtTotalReceivedAmount" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Total Received Amount" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div class="x_title">
                                    <h2>Incase of Appeal to Higher Authority Details :</h2>
                                    <div class="clearfix"></div>
                                </div>
                                           <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Appeal Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txthigherdate" Format="dd-MM-yyyy" ID="CalendarExtender3"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txthigherdate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red">*</span>Higher Authority Name</label>
                                        <br />
                                        <asp:TextBox ID="txtHigherAuthorityName" AutoComplete="off" onkeypress="return onlyAlphabets(this,event);" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Higher Authority Name" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12  form-inline">
                                        <label class="control-label" style="font-size: small; display: inline"><span style="color: red"></span>Remarks</label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" runat="server" AutoComplete="off" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Remarks" Width="80%" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>EXCISABLE ARTICLE SEIZED :</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                    <div>
                                          <div class="col-md-6 col-sm-12 col-xs-12 form-inline"> 
                                        <asp:GridView ID="grdExcisableArticle" runat="server" AutoGenerateColumns="false" class="table table-striped responsive-utilities jambo_table" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="window"
                                            HeaderStyle-ForeColor="#ecf0f1" EmptyDataText="no records" OnRowDataBound="grdExcisableArticle_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Article Id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleId" runat="server" Text='<%#Eval("seizure_excisable_articles_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Category	" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleCategory" runat="server" Text='<%#Eval("article_category_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Sub-Category" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleSubCategory" runat="server" Text='<%#Eval("article_sub_category_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleName" runat="server" Text='<%#Eval("article_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Manufacturer	" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%#Eval("manufacturer_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit of Measure" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitofMeasure" runat="server" Text='<%#Eval("uom_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Packing Size" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackingSize" runat="server" Text='<%#Eval("packingsize_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAction" runat="server" Text='<%# Eval("actioncompleted") %>' Visible = "false" />
                                                        <asp:DropDownList ID="ddlAction" ReadOnly="true" class="form-control" runat="server">
                                                           
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate_of_destruction" runat="server" Text='<%# Eval("date_of_destruction") %>' Visible = "false" />
                                                      <asp:TextBox class="form-control" ReadOnly="true" type="date" id="AvalueDateFixedForDestruction" runat="server" />
                                                    </ItemTemplate>
                                               </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <input class="form-control" type="date"   id="DateOfDestructionAcquittal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div></div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>OTHER EXCISABLE ARTICLE SEIZED :</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   
                                     <div >
                                           <div style =" width:100%; overflow:auto;" class="col-md-6 col-sm-12 col-xs-12 form-inline"> 
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped responsive-utilities jambo_table"  PageSize="10" AllowPaging="true" EmptyDataText="No Records" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1"  OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Vehicle Id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleId" runat="server" Text='<%#Eval("seizure_vehicledetails_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle Type" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%#Eval("vehicle_type_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle Name" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleName" runat="server" Text='<%#Eval("vehiclename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Manufacturer	" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%#Eval("manufacturer_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Model" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("makemodel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chasis No" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChasisNo" runat="server" Text='<%#Eval("chasisno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reg.No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegNo" runat="server" Text='<%#Eval("registrationno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("ownername") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Present Address" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPresentAddress" runat="server" Text='<%#Eval("presentaddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Permanent Address" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPermanentAddress" runat="server" Text='<%#Eval("permanentaddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Contact No" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContactNo" runat="server" Text='<%#Eval("contactno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                          <asp:Label ID="lblAction" runat="server" Text='<%# Eval("actioncompleted") %>' Visible = "false" />
                                                        <asp:DropDownList ID="ddlAction" ReadOnly="true" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Fixed For Auction / Release" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblauction_or_releasedate" runat="server" Text='<%# Eval("auction_or_releasedate") %>' Visible = "false" />
                                                      <asp:TextBox class="form-control" ReadOnly="true" type="date" id="auction_or_releasedate" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date Of Auction Release" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <input class="form-control" type="date"   id="DateOfAuctionRelease" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Amount Recieved" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountRecieved" ReadOnly="true" runat="server" Text='<%#Eval("auctionreleaseamount")%>' CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In Favour Of" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtInFavourOf" ReadOnly="true" runat="server" Text='<%#Eval("infavourof")%>' CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Challan Date" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                           <asp:Label ID="lblChallanDate" runat="server" Text='<%# Eval("challan_date") %>' Visible = "false" />
                                                        <asp:TextBox class="form-control" ReadOnly="true" type="date" id="ChallanDate" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deposited Vide Challan No" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                      <asp:TextBox ID="txtDepositedVideChallanNo" ReadOnly="true" Text='<%#Eval("challan_no")%>' runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                              <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Apparatus</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                     
                                    <div>
                                          <div style =" width:100%; overflow:auto;" class="col-md-6 col-sm-12 col-xs-12 form-inline"> 
                                      
                                        <asp:GridView ID="grdApparatusView" runat="server" AutoGenerateColumns="false" class="table table-striped responsive-utilities jambo_table" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="window"
                                            HeaderStyle-ForeColor="#ecf0f1" EmptyDataText="no records" OnRowDataBound="grdApparatusView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Apparatus Id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApparatus" runat="server" Text='<%#Eval("seizure_apparatusdetails_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apparatus Type" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApparatusType" runat="server" Text='<%#Eval("apparatus_type_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apparatus Name" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApparatusName" runat="server" Text='<%#Eval("apparatus_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("ownername") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAction" runat="server" Text='<%# Eval("actioncompleted") %>' Visible = "false" />
                                                        <asp:DropDownList ID="ddlAction" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate_of_destruction" runat="server" Text='<%# Eval("date_of_destruction") %>' Visible = "false" />
                                                      <asp:TextBox class="form-control" type="date" id="AvalueDateFixedForDestruction"  runat="server" />
                                                    </ItemTemplate>
                                               </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <input class="form-control" type="date"   id="DateOfDestructionAcquittal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div></div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Propery</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                     
                                    <div>
                                          <div style =" width:100%; overflow:auto;" class="col-md-6 col-sm-12 col-xs-12 form-inline"> 
                                        <asp:GridView ID="grdPropertyView" runat="server" AutoGenerateColumns="false" class="table table-striped responsive-utilities jambo_table" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="window"
                                            HeaderStyle-ForeColor="#ecf0f1" EmptyDataText="no records" OnRowDataBound="grdPropertyView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Propery Id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProperyId" runat="server" Text='<%#Eval("seizure_propertydetails_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Propery Type	" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProperytype" runat="server" Text='<%#Eval("property_type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Circle Name" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCircleName" runat="server" Text='<%#Eval("propertycriclename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mouza Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMouzaName" runat="server" Text='<%#Eval("propertymauzaname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Khata No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKhataNo" runat="server" Text='<%#Eval("propertykhatano") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Khasra No" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKhasraNo" runat="server" Text='<%#Eval("propertykhasrano") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Thana Name" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThananame" runat="server" Text='<%#Eval("propertythanano") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("ownername") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                               
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAction" runat="server" Text='<%# Eval("actioncompleted") %>' Visible = "false" />
                                                        <asp:DropDownList ID="ddlAction" class="form-control" runat="server">
                                                           
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate_of_destruction" runat="server" Text='<%# Eval("date_of_destruction") %>' Visible = "false" />
                                                      <asp:TextBox class="form-control" type="date" id="AvalueDateFixedForDestruction"  runat="server" />
                                                    </ItemTemplate>
                                               </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <input class="form-control" type="date"   id="DateOfDestructionAcquittal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div></div>
                                              <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Money</h2>
                                    <div class="clearfix"></div>
                                </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                     
                                    <div>
                                          <div style =" width:100%; overflow:auto;" class="col-md-6 col-sm-12 col-xs-12 form-inline"> 
                                        <asp:GridView ID="grdMoneyListView" runat="server" AutoGenerateColumns="false" class="table table-striped responsive-utilities jambo_table" HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="window"
                                            HeaderStyle-ForeColor="#ecf0f1" EmptyDataText="no records" OnRowDataBound="grdMoneyListView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Money Id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMoney" runat="server" Text='<%#Eval("seizure_moneydetails_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount Seized" ItemStyle-Font-Bold="true" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmountSeized" runat="server" Text='<%#Eval("total_amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAction" runat="server" Text='<%# Eval("actioncompleted") %>' Visible = "false" />
                                                        <asp:DropDownList ID="ddlAction" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate_of_destruction" runat="server" Text='<%# Eval("date_of_destruction") %>' Visible = "false" />
                                                      <asp:TextBox class="form-control" type="date" id="AvalueDateFixedForDestruction"  runat="server" />
                                                    </ItemTemplate>
                                               </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date Of Destruction / Acquittal" ItemStyle-Font-Bold="true" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <input class="form-control" type="date"   id="DateOfDestructionAcquittal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div></div>
                                       
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                    </div>     </body>
                            </html>
                                </div>
                       
                        </div>
                    </div>
                </div>
            </div>
        </div>
   





</asp:Content>
