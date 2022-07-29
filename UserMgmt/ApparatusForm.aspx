<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="ApparatusForm.aspx.cs" Inherits="UserMgmt.ApparatusForm" %>
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
                                <title>Apparatus Form</title>
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
                                        if (document.getElementById('<%=ddlApparatusType.ClientID%>').value == 'Select') {
                                            alert("Select Apparatus Type");
                                            document.getElementById("<% =ddlApparatusType.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtApparatusName.ClientID%>').value == '') {
                                             alert("Enter Apparatus Name");
                                            document.getElementById("<% =txtApparatusName.ClientID%>").focus();
                                            return false;

                                         }
                                         <%--else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtApparatusName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("Apparatus Name should be minimum 3 character");
                                                 document.getElementById("<% =txtApparatusName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>
                                        <%-- if (document.getElementById('<%=txtManufacturer.ClientID%>').value == '') {
                                             alert("Enter Manufacturer");
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
                                             alert("Enter  Model");
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
                                                 alert("Owner Name should be minimum 3 character");
                                                 document.getElementById("<% =txtOwnerName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if ((document.getElementById('<%=txtMobileNo.ClientID%>').value<"1") || (document.getElementById('<%=txtMobileNo.ClientID%>').value.length<"10")) {
                                            debugger;
                                            alert("Enter valid Mobile No");
                                            document.getElementById("<% =txtMobileNo.ClientID%>").focus();
                                            return false;
                                        }  --%>

                                        

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

                                        if (document.getElementById('<%=txtOwnerPresentAddress.ClientID%>').value<"1") {
                                            debugger;
                                            alert("Enter Owner Present Address");
                                            document.getElementById("<% =txtOwnerPresentAddress.ClientID%>").focus();
                                            return false;
                                        }
                                        else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtOwnerPresentAddress").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <10)
                                             {
                                                 alert("Owner Present Address should be minimum 10 character");
                                                 document.getElementById("<% =txtOwnerPresentAddress.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>
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
        $("[id*=grdApparatusView] input[type=checkbox]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=grdApparatusView] input[type=checkbox]").removeAttr("checked");
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
                                    <h2>Apparatus Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                
                                <div class="x_content">
                              <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                      <div id ="serchid" runat="server">
                                  <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Apparatus Name </label><br />
                                                <asp:TextBox ID="txtAName" autocomplete="off" runat="server" Width="90%" onkeypress="return onlyAlphabets(this,event);" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span></label><br />
                                                  <asp:Button ID="btnApparatusSearch"  runat="server" Width="40%"  Text="Apparatus Search" OnClick="btnSearch_Click" CssClass="btn btn primary" />
                                            </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                          <div style =" width:1000px; overflow:auto;"> 
                                    <asp:GridView ID="grdApparatusView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdApparatusView_PageIndexChanging"
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Apparatus Type Code" Visible="false" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApparatusTypeCode" runat="server" Visible="true" Text='<%#Eval("apparatus_type_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apparatus Type" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApparatusType" runat="server" Visible="true" Text='<%#Eval("apparatus_type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apparatus Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApparatusName" runat="server" Visible="true" Text='<%#Eval("apparatus_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Owner Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerName" runat="server" Visible="true" Text='<%#Eval("ownername") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobileNo" runat="server" Visible="true" Text='<%#Eval("contactno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           --%>
                                              <%--<asp:TemplateField HeaderText="Owner Permanent Address" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOwnerPermanentAddress" runat="server" Visible="true" Text='<%#Eval("permanentaddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                             <asp:CheckBox ID="chselect" AutoPostBack="true" OnCheckedChanged="chselect_CheckedChanged" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                               <%-- <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server"  CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />
                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                    </asp:GridView>
                                    </div>

                                        </div>
                                        <div class="clearfix"></div>
                                    
                                        <div class="x_title"></div>
                                        </div>     
                                    
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Apparatus Type </label>
                                        <br />
                                        <asp:DropDownList ID="ddlApparatusType" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Apparatus Type" ></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Apparatus Name </label>
                                        <br />
                                         <asp:TextBox ID="txtApparatusName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Apparatus Name" ></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Manufacturer </label>
                                        <br />  <asp:TextBox ID="txtManufacturer" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Manufacturer" ></asp:TextBox>
                                       
                                    </div>
                                    
                                      
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Model </label>
                                        <br />
                                        <asp:TextBox ID="txtMakeModel" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Model" ></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>IMEI No </label>
                                        <br />
                                        <asp:TextBox ID="txtIMEI" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="IMEI No" MaxLength="20" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Owner Name</label>
                                        <br />
                                        <asp:TextBox ID="txtOwnerName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Owner Name" ></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Mobile No </label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" autocomplete="off" onchange="phoneValidate()"  CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right"  MaxLength="10" title="Mobile No" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Permanent Address </label>
                                        &nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="chk" runat="server" onclick="copyValue(this)" /> Copy Address
                                        <br />
                                        <asp:TextBox ID="txtOwnerPermanentAddress" autocomplete="off" CssClass="form-control" runat="server" Height="10%" Width="77.5%" data-toggle="tooltip" data-placement="right" title="Owner Permanent Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Owner Present Address</label>
                                        <br />
                                         <asp:TextBox ID="txtOwnerPresentAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Width="77.5%" Height="10%"  data-placement="right" title="Owner Present Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" onclick="btnSaveasDraft_Click" runat="server" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                           <%-- <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
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
