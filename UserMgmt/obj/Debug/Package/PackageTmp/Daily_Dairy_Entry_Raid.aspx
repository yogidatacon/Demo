<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Daily_Dairy_Entry_Raid.aspx.cs" Inherits="UserMgmt.Daily_Dairy_Entry_Raid" %>
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
                                <title>Daily Dairy Entry Raid</title>
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
    <script type="text/javascript">
    function blockAllChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false; 
    }
    function ShowHideDiv() {
        var ddlRecovery = document.getElementById("BodyContent_ddlRecovery");
        var doh = document.getElementById("BodyContent_DOH");

        debugger;;
        if (ddlRecovery.value == "Yes") {
            $("#BodyContent_DOH").show();

        }
        else {
            $("#BodyContent_DOH").hide();

            //  caseclosed.style.display = "block";
        }


    }
    function blockSpecialChar(e) {
        var k;
        document.all ? k = e.keyCode : k = e.which;

        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    </script>
                               <script language="javascript" type="text/javascript">
                                   function validationMsg() {
                                      <%-- if (document.getElementById('<%=ddlGender.ClientID%>').value == 'Select') {
                                           alert("Select Gender");
                                           document.getElementById("<% =ddlGender.ClientID%>").focus();
                                           return false;
                                       }--%>
                                       if (document.getElementById('<%=txtraiddate.ClientID%>').value == '') {
                                           alert("Select Raid Date!");
                                           document.getElementById("<% =txtraiddate.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtraidlocation.ClientID%>').value == '') {
                                           alert("Enter Raid Location");
                                           document.getElementById("<% =txtraidlocation.ClientID%>").focus();
                                           return false;
                                       }  
                                       if (document.getElementById('<%=txtdistance.ClientID%>').value == '') {
                                           alert("Enter Raid Distance");
                                           document.getElementById("<% =txtdistance.ClientID%>").focus();
                                           return false;
                                       }

                                       if (document.getElementById('<%=txtteamleader.ClientID%>').value == '') {
                                           alert("enter Team Leader");
                                           document.getElementById("<% =txtteamleader.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlRecovery.ClientID%>').value == 'Select') {
                                           alert("Select Recovery");
                                           document.getElementById("<% =ddlRecovery.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=ddlRecovery.ClientID%>').value == 'Yes')
                                       {
                                           var obj = document.getElementById('<%=grdrecovery.ClientID%>');
                                           if (obj === undefined || obj == null) {
                                              
                                               if (document.getElementById('<%=ddlExcisablegood.ClientID%>').value == 'Select') {
                                                   alert("select Excisable Goods");
                                                   document.getElementById("<% =ddlExcisablegood.ClientID%>").focus();
                                                   return false;
                                               }
                                               if (document.getElementById('<%=ddlExcisablegood.ClientID%>').value == 'E') {
                                                   if (document.getElementById('<%=ddVehicletype.ClientID%>').value == 'Select') {
                                                       alert("Select Article Category");
                                                       document.getElementById("<% =ddVehicletype.ClientID%>").focus();
                                                       return false;
                                                   }

                                                   if (document.getElementById('<%=ddlUOM.ClientID%>').value == 'Select') {
                                                       alert("Select  UOM");
                                                       document.getElementById("<% =ddlUOM.ClientID%>").focus();
                                                       return false;
                                                   }
                                                   if (document.getElementById('<%=txtvolumesno.ClientID%>').value == '') {
                                                       alert("Enter Volumes No");
                                                       document.getElementById("<% =txtvolumesno.ClientID%>").focus();
                                                       return false;
                                                   }
                                               }
                                               if (document.getElementById('<%=ddlExcisablegood.ClientID%>').value == 'V') {
                                                   if (document.getElementById('<%=ddVehicletype.ClientID%>').value == 'Select') {
                                                       alert("Select Vehicle Type");
                                                       document.getElementById("<% =ddVehicletype.ClientID%>").focus();
                                                       return false;
                                                   }


                                                   if (document.getElementById('<%=txtvolumesno.ClientID%>').value == '') {
                                                       alert("Enter Vehicle No");
                                                       document.getElementById("<% =txtvolumesno.ClientID%>").focus();
                                                       return false;
                                                   }
                                               }
                                               if (document.getElementById('<%=ddlExcisablegood.ClientID%>').value == 'D') {

                                                   if (document.getElementById('<%=txtvolumesno.ClientID%>').value == '') {
                                                       alert("Enter No of Members Drunken");
                                                       document.getElementById("<% =txtvolumesno.ClientID%>").focus();
                                                       return false;
                                                   }
                                               }
                                           }
                                       }  
                                         if (document.getElementById('<%=txtnoarresting.ClientID%>').value == '') {
                                                       alert("Enter No of Arrested");
                                                       document.getElementById("<% =txtnoarresting.ClientID%>").focus();
                                                       return false;
                                         }
                                        if (document.getElementById('<%=txtAbsconding.ClientID%>').value == '') {
                                                       alert("Enter No of Absconding");
                                                       document.getElementById("<% =txtAbsconding.ClientID%>").focus();
                                                       return false;
                                        }
                                        if (document.getElementById('<%=txtnoofcaseinstituted.ClientID%>').value == '') {
                                            alert("Enter No of Case Instituted");
                                                       document.getElementById("<% =txtnoofcaseinstituted.ClientID%>").focus();
                                                       return false;
                                        }
                                       
                                   }

                                   function validationMsg1() {
                                       if (document.getElementById('<%=ddlGender.ClientID%>').value == 'Select') {
                                           alert("Select Gender");
                                           document.getElementById("<% =ddlGender.ClientID%>").focus();
                                           return false;
                                       }
                                       if (document.getElementById('<%=txtArresting.ClientID%>').value == '') {
                                           alert("Enter Arresting");
                                           document.getElementById("<% =txtArresting.ClientID%>").focus();
                                           return false;
                                       }
                                   }
                                </script>

                                  <script type="text/javascript">
                                    function Calcutate() {
                                        debugger;
                                        var total = 0;
                                      
                                        var gv = document.getElementById("<%=grdArresting.ClientID %>");
                                        var tb = gv.getElementsByTagName("input");
                                        var sub = 0;
                                        var total = 0;
                                        var indexQ = 1;
                                        var indexP = 0;
                                        for (var i = 0; i < tb.length; i++)
                                        {
                                            if (tb[i].type == "text" )
                                            {
                                                sub = parseFloat(tb[i].value);
                                                if (isNaN(sub)) {
                                                    // lb[i + indexQ].innerHTML = "";
                                                    sub = 0;
                                                }
                                               
                                                total += parseFloat(sub);
                                               
                                                var noarresting = parseFloat($('#BodyContent_txtnoarresting').val()).toFixed(2);
                                                if (noarresting < total)
                                                {
                                                    total -= parseFloat(sub);
                                                    alert("Qty not Matched with Net Weight");
                                                    //$('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                                    tb[i].value = "";
                                                    tb[i].focus();
                                                    return false;
                                                }
                                                i++;
                                            }
                                        }
                                        debugger;
                                        //$('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                       
                                    }
                                      </script>
                                <script>
                                    $(document).ready(function () {
                                        debugger;;
                                      
                                        ShowHideDiv();
                                     
                                    });

             </script>
                            </head>
                            <body>
                                <ul class="nav nav-tabs">
                                    <li Class="active">
                                        <asp:LinkButton ID="DDER" OnClick="DDER_Click"  runat="server"><span style="color:#fff;font-size:14px;">Raid Entry</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="DDEOR" OnClick="DDEOR_Click"  runat="server"><span style="color:#fff;font-size:14px;">Other than Raid Entry</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="EVT" OnClick="EVT_Click" Visible="false"  runat="server"><span style="color:#fff;font-size:14px;">Events</span></asp:LinkButton></li>
                                    </ul>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Daily Dairy Entry Raid</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="clearfix"></div>
                                   
                                      </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span>Name of Officer</label>
                                                <br />                                                
                                                <asp:TextBox ID="txtnameofofficer" CssClass="form-control" Enabled="false" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("user_name") %>'  title="Name of Officer"></asp:TextBox>
                                            </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Designation</label>
                                        <br />
                                      <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" Enabled="false" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                    </div>
                                 
                                   
                                       <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>District</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false" data-toggle="tooltip" data-placement="right" title="District"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red"></span>Mobile No</label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" Enabled="false"  data-toggle="tooltip" data-placement="right" title="Mobile No"></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Raid Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtraiddate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtraiddate" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Raid Date" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Place of Raid</label>
                                        <br />
                                        <asp:TextBox ID="txtraidlocation" CssClass="form-control" runat="server"   data-toggle="tooltip" MaxLength="150" data-placement="right" title="Place of Raid"></asp:TextBox>
                                    </div>                                                   
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Enter Distance Travelled</label>
                                        <br />          
                                        <asp:TextBox ID="txtdistance" CssClass="form-control" runat="server"   data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" MaxLength="10" title="Enter Distance Travelled"></asp:TextBox>
                                    </div>
                                 
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline ">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>Team Leader</label>
                                        <br />
                                        <asp:TextBox ID="txtteamleader" CssClass="form-control" runat="server"   data-toggle="tooltip" MaxLength="100" data-placement="right" title="Team Leader"></asp:TextBox>
                                    </div>
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Recovery</label>
                                        <br />
                                        <asp:DropDownList ID="ddlRecovery" runat="server" CssClass="form-control" AutoPostBack="true" onchange = "ShowHideDiv()"  data-toggle="tooltip" data-placement="right" title="Recovery">
                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="YES" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="NO" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                <div id="DOH" runat="server">
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Recovery Details</h2>
                                    <div class="clearfix"></div>
                                </div>
                                 
                                 <div id="rec" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Type of Recovery</label>
                                        <br />
                                        <asp:DropDownList ID="ddlExcisablegood" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Excisable Goods" OnSelectedIndexChanged="ddlExcisablegood_SelectedIndexChanged">
                                                      <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="Excisable Goods" Value="E"></asp:ListItem>
                                                    <asp:ListItem Text="Vehicle" Value="V"></asp:ListItem>
                                     <asp:ListItem Text="Drunken" Value="D"></asp:ListItem>  </asp:DropDownList>
                                    </div>
                                 <div id="rtype" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" runat="server" id="v" style="display:inline"><span style="color: red">*</span>Vehicle Type</label>
                                       <label class="control-label" runat="server" id="a" style="display:inline"><span style="color: red">*</span>Artical Category Name</label>
                                        <br />
                                        <asp:DropDownList ID="ddVehicletype" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Vehicle Type"></asp:DropDownList>
                                    </div>
                                 <div  id="utype" runat="server" class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" runat="server" id="u" style="display:inline"><span style="color: red">*</span>UOM</label>
                                        <br />
                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="UOM"></asp:DropDownList>
                                    </div>
                                 <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" runat="server" id="a1" style="display:inline"><span style="color: red">*</span>Volumes in No</label>
                                     <label class="control-label" runat="server" id="v1" style="display:inline"><span style="color: red">*</span>Vehicle No</label>
                                      <label class="control-label" runat="server" id="n" style="display:inline"><span style="color: red">*</span>No of Members Drunken</label>
                                        <br />
                                     <asp:TextBox ID="txtvolumesno" CssClass="form-control" runat="server"   data-toggle="tooltip" data-placement="right" title="Volumes in No"></asp:TextBox>    
                                    &nbsp;&nbsp;    
                                               <asp:LinkButton ID="btnAdd" runat="server"   OnClick="btnAdd_Click"
                                                                CssClass="btn btn-primary" OnClientClick="javascript:return Validate1();">
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >   </i>ADD</asp:LinkButton>
                                         
                                    </div>
                               
                                  <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                 <div id="Div1" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="membertable1">
                                            <thead>
                                                <tr>
                                                    <th>Type of Recovery</th>
                                                    <th>Recovery Description</th>
                                                    <th>UOM</th>
                                                    <th>Volumes in No</th>
                                                </tr>
                                            </thead>
                                            <tbody id="resourcetable1">
                                            </tbody>

                                        </table>
                                    </div>
                                 <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdrecovery" runat="server" AutoGenerateColumns="false" EmptyDataText="No Attached Files"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                  <asp:TemplateField HeaderText="Type of Recovery" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype_of_recovery" runat="server" Visible="true"  Text='<%#Eval("type_of_recovery_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TypeofRecovery" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltypeofrecovery" runat="server" Visible="true" Text='<%#Eval("type_of_recovery") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Recovery Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrecovery_description" runat="server" Visible="true" Text='<%#Eval("recovery_description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Recoveryid" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrecovery_particulars_id" runat="server" Visible="true" Text='<%#Eval("recovery_particulars_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluom_name" runat="server" Visible="true" Text='<%#Eval("uom_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluom_code" runat="server" Visible="true" Text='<%#Eval("uom_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Volumes in No/Vehicle No/Drunken Name" Visible="true" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrecovery_qty" runat="server" Visible="true" Text='<%#Eval("recovery_qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="daily_dairy_recovery_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldaily_dairy_recovery_id" runat="server" Visible="true" Text='<%#Eval("daily_dairy_recovery_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldaily_dairy_id" runat="server" Visible="true" Text='<%#Eval("daily_dairy_raid_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="btnDelete" CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnDelete_Click"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                     </div>
                                 <div class="clearfix"></div>
                                        <p>&nbsp;</p>         
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                 <label class="control-label" style="display:inline"><span style="color: red">*</span>No of Arresting</label>
                                        <br />
                                     <asp:TextBox ID="txtnoarresting" CssClass="form-control" runat="server"   data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtnoarresting_TextChanged" data-placement="right" title="No of Arresting"></asp:TextBox>    
                                    </div>

                                 <div id="DivDetails" runat="server">
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Arresting Details</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div  runat="server" id="gender" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Gender</label>
                                        <br />
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Gender">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Transgender</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                 <div runat="server" id="Arrst" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                         <label class="control-label" style="display:inline"><span style="color: red">*</span>Arresting</label>
                                        <br />
                                     <asp:TextBox ID="txtArresting" CssClass="form-control" runat="server"   data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Arresting"></asp:TextBox>    
                                    </div>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                       <label class="control-label" style="display:inline"></label>
                                   <asp:LinkButton ID="btnGAdd" runat="server"   OnClick="btnAddgender_Click"
                                                                CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1();">
                                                        <i class="fa fa-plus-circle" style="font-size: 15px" >   </i>ADD</asp:LinkButton>
                                        <asp:HiddenField ID="txttotalarrest" runat="server" />
                                     </div>

                                      <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                 <div id="ArrestingDiv" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
                                        <table class="table table-striped responsive-utilities jambo_table" id="Arrestingtable">
                                            <thead>
                                                <tr>
                                                    <th>Gender</th>
                                                    <th>Arresting</th>
                                                   <%-- <th>UOM</th>
                                                    <th>Volumes in No</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="resourcetable12">
                                            </tbody>

                                        </table>
                                    </div>
                                 <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                        <asp:GridView ID="grdArresting" runat="server" AutoGenerateColumns="false" EmptyDataText="No Attached Files"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Gender Code" Visible="false" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGendercode" runat="server" Visible="true"  Text='<%#Eval("gender_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Gender" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGender" runat="server" Visible="true"  Text='<%#Eval("gender_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Arresting" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArresting" runat="server" Visible="true" Text='<%#Eval("arresting") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="doc_id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGenderid" runat="server" Visible="true" Text='<%#Eval("Doc_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="btnDelete" CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnDelete_Click1"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                     </div>




                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                         <label class="control-label" style="display:inline"><span style="color: red">*</span>No of Absconding</label>
                                        <br />
                                     <asp:TextBox ID="txtAbsconding" CssClass="form-control" runat="server"   data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="No of Absconding"></asp:TextBox>    
                                    </div>
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                     <label class="control-label" style="display:inline"><span style="color: red">*</span>No of Case Instituted</label>
                                        <br />
                                     <asp:TextBox ID="txtnoofcaseinstituted" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);"  data-toggle="tooltip" data-placement="right" title="No of Case Instituted"></asp:TextBox>    
                                    </div>
                                          
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                     <label class="control-label" style="display:inline"><span style="color: red"></span>Other Recovery</label>
                                        <br />
                                     <asp:TextBox ID="txtotherrecovery" CssClass="form-control" runat="server" onkeypress="return blockSpecialChar(event)"   data-toggle="tooltip" data-placement="right" title="Otehr Recovery"></asp:TextBox>    
                                    </div>
                                <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            
                                             <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline" id="dn" runat="server"><span style="color: red"></span>Document Name</label><br />
                                            <asp:TextBox ID="txtDiscription" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" title="Document Name"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload"  OnClick="UploadFile"  />
                                            </span>
                                        </div>
                                    </div>
                                            <div class="clearfix"></div>
                                    <p>&nbsp;</p>                                          
                                            <div id="dummytable" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
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
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" EmptyDataText="No Attached Files"
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
                                                <asp:TemplateField HeaderText="Document Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_name") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_name") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
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

                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                              <asp:HiddenField ID="hdocsfrom" runat="server" />
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click" >
                                                    <span aria-hidden="true" class="fa fa-plus-circle"></span>Save as Draft</asp:LinkButton>
                                           <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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

