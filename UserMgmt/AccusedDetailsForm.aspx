<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="AccusedDetailsForm.aspx.cs" Inherits="UserMgmt.AccusedDetailsForm" %>

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
                                <title>Accused Details Form</title>
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
                                    function filladd() {
                                        if (filltoo.checked == true) {
                                            var tal11 = document.getElementById("txtPermanentAddress").value;
                                            document.getElementById("txtPresentAddress").value = tal11
                                        }
                                    }
                                    function copyAddress(aThis) {
                                        var ischecked = document.getElementById("Chaddress").onselect;

                                        alert("is checked " + ischecked + "" + aThis.value);
                                        if (ischecked) {
                                            document.getElementById('txtPresentAddress').innerHTML = document.getElementById('txtPermanentAddress').innerHTML
                                        }
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
                                    function phoneValidate1() {
                                        debugger;
                                        var mobileN = $('#BodyContent_NestedBodyContent_txtAlternate').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Alternate Mobile No.");
                                            $('#BodyContent_NestedBodyContent_txtAlternate').val("");
                                            $('#BodyContent_NestedBodyContent_txtAlternate').focus();
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        debugger;;
                                        if (document.getElementById('<%=ddlAccusedStatus.ClientID%>').value == 'Select') {
                                            alert("Select Accused Status");
                                            document.getElementById("<% =ddlAccusedStatus.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlAccusedStatus.ClientID%>').value != 'UNR')
                                        {
                                            var val = true;
                                            if (document.getElementById('<%=txtAccusedName.ClientID%>').value == '') {
                                                alert("Enter Accused Name");
                                                document.getElementById("<% =txtAccusedName.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                      
                                          <%--  if (document.getElementById('<%=txtSpouseName.ClientID%>').value == '') {
                                                alert("Enter Father Spouse Name");
                                                document.getElementById("<% =txtSpouseName.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }--%>
                                            
                                            if (document.getElementById('<%=ddlDescriptionofoffence.ClientID%>').value == 'Select') {
                                                alert("Select Description of offence");
                                                document.getElementById("<% =ddlDescriptionofoffence.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                             if (document.getElementById('<%=ddlGender.ClientID%>').value == 'Select') {
                                                alert("Select Gender");
                                                document.getElementById("<% =ddlGender.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }    
                                            <%--if (document.getElementById('<%=txtAge.ClientID%>').value == '') {
                                                alert("Enter Age");
                                                document.getElementById("<% =txtAge.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            
                                            if (document.getElementById('<%=ddReligion.ClientID%>').value == 'Select') {
                                                alert("Select Religion");
                                                document.getElementById("<% =ddReligion.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            if (document.getElementById('<%=ddlCasteCategory.ClientID%>').value == 'Select') {
                                                alert("Select Caste Category");
                                                document.getElementById("<% =ddlCasteCategory.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            if (document.getElementById('<%=ddlCaste.ClientID%>').value == 'Select') {
                                                alert("Select Caste");
                                                document.getElementById("<% =ddlCaste.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            } 
                                            if (document.getElementById('<%=ddlAccusedIdProof.ClientID%>').value == 'Select') {
                                                alert("Select Accused Id Proof");
                                                document.getElementById("<% =ddlAccusedIdProof.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            debugger;;
                                            if (document.getElementById('<%=ddlAccusedIdProof.ClientID%>').value != 'Oth') {
                                                if (document.getElementById('<%=txtIDNo.ClientID%>').value == '') {
                                                    alert("Enter  IDNo");
                                                    document.getElementById("<% =txtIDNo.ClientID%>").focus();
                                                    val = false;
                                                    return false;
                                                }
                                            }
                                            if (document.getElementById('<%=ddlSDR_CAF.ClientID%>').value == 'Select') {
                                                alert("Select SDR/CAF");
                                                document.getElementById("<% =ddlSDR_CAF.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            if (document.getElementById('<%=txtMobileNo.ClientID%>').value == '') {
                                                alert("Enter Mobile No");
                                                document.getElementById("<% =txtMobileNo.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }--%>
                                          
                                           <%-- if (document.getElementById('<%=txtPermanentAddress.ClientID%>').value == '') {
                                                alert("Enter Permanent Address");
                                                document.getElementById("<% =txtPermanentAddress.ClientID%>").focus();
                                               
                                                val = false;
                                                return false;
                                            }
                                            if (document.getElementById('<%=txtPresentAddress.ClientID%>').value == '') {
                                               
                                                alert("Enter  Present Address");
                                                document.getElementById("<% =txtPresentAddress.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                           
                                            if (document.getElementById('<%=txtBloodTestResult.ClientID%>').value == '' && document.getElementById('<%=RadioButton1.ClientID%>').checked == true) {
                                                debugger;
                                                alert("Enter Blood Test Result");
                                                document.getElementById("<% =txtBloodTestResult.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }
                                            if (document.getElementById('<%=txtBreathAnalyzer.ClientID%>').value == '') {
                                                alert("Enter Breath Analyzer");
                                                document.getElementById("<% =txtBreathAnalyzer.ClientID%>").focus();
                                                val = false;
                                                return false;
                                            }--%>
                                            return val;
                                        }
                                    }
                                    $(document).ready(function () {
                                        debugger;;

                                        ShowHideDiv();

                                    });
                                </script>
                                <script type="text/javascript">
                                    function copyValue(Chk) {
                                        if (Chk.checked) {
                                            document.getElementById('<%=txtPresentAddress.ClientID%>').value = document.getElementById('<%=txtPermanentAddress.ClientID%>').value;
                                        }
                                    }
                                    function ShowHideDiv() {
                                        var ddlAccusedStatus = document.getElementById("BodyContent_NestedBodyContent_ddlAccusedStatus");
                                      
                                        debugger;;
                                        if (ddlAccusedStatus.value == "UNR")
                                        {
                                            $("#BodyContent_NestedBodyContent_d0").hide();
                                            $("#BodyContent_NestedBodyContent_d00").hide();
                                            $("#BodyContent_NestedBodyContent_d000").hide();
                                            $("#BodyContent_NestedBodyContent_d1").hide();
                                          
                                        }
                                        else
                                        {
                                            $("#BodyContent_NestedBodyContent_d0").show();
                                            $("#BodyContent_NestedBodyContent_d00").show();
                                            $("#BodyContent_NestedBodyContent_d000").show();
                                            $("#BodyContent_NestedBodyContent_d1").show();
                                        }


                                    }
                                   
                                </script>
                             <script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=grdAccusedDetailsView] input[type=checkbox]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=grdAccusedDetailsView] input[type=checkbox]").removeAttr("checked");
                $(this).attr("checked", "checked");
            }
        });
    });
</script>
                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Accused Details Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                
                                <div class="x_content">
                                   <div id="search1" runat="server"> 
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>--%>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Accused Name </label>
                                                <br />
                                                <asp:TextBox ID="txtAccusName" autocomplete="off" runat="server" Width="80%" onkeypress="return onlyAlphabets(this,event);" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <%--<div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span> ID Proof </label>
                                                <br />
                                                <asp:TextBox ID="txtIDProof" runat="server" Width="90%" CssClass="form-control"></asp:TextBox>
                                            </div>--%>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Father/Spouse Name</label>
                                                <br />
                                                <asp:TextBox ID="txtFatherSpouseName" autocomplete="off" runat="server" Width="80%" onkeypress="return onlyAlphabets(this,event);"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Mobile No</label><br />
                                                <asp:TextBox ID="txtMobiNo" runat="server" autocomplete="off"  MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                        <label class="control-label"><span style="color: red"></span></label><br />
                                                  <asp:Button ID="btnSearch" runat="server" Width="50%" Text="Accused Search" CssClass="btn btn primary" OnClick="btnSearch_Click" />
                                            </div>
                                        
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <div >
                                            <asp:GridView ID="grdAccusedDetailsView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdAccusedDetailsView_PageIndexChanging"
                                                HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                <Columns>
                                                     
                                                    <%--<asp:TemplateField HeaderText="Description of offence" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescriptionofoffence" runat="server" Visible="true" Text='<%#Eval("") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Accused Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccusedName" runat="server" Visible="true" Text='<%#Eval("accusedname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accused Father/Spouse Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrelativename" runat="server" Visible="true" Text='<%#Eval("relativename") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accused IDProof" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDProof" runat="server" Visible="true" Text='<%#Eval("idproof_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accused IDNo" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Visible="true" Text='<%#Eval("idno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobileNo" runat="server" Visible="true" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OffenceCode" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOffenceCode" runat="server" Visible="false" Text='<%#Eval("offence_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Permanent Address" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPermanentAddress" runat="server" Visible="true" Text='<%#Eval("permanentaddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="adid" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladid" runat="server" Visible="true" Text='<%#Eval("seizure_accused_details_id") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chselect" AutoPostBack="true" OnCheckedChanged="chselect_CheckedChanged" runat="server" />
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
                                    
                                     <%--   </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                       <div class="clearfix"></div>
                                    
                                       <div class="x_title"/>
                                        </div>
                                       </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Accused Status </label>
                                        <br />

                                          <asp:DropDownList ID="ddlAccusedStatus" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Status" onchange = "ShowHideDiv()"></asp:DropDownList>
                                        <%--<asp:RadioButton ID="rdArrested" runat="server" GroupName="radio" />Arrested&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdAbsconding" runat="server" GroupName="radio" />Absconding&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdUnrecognized" runat="server" GroupName="radio" />Unrecognized&nbsp;&nbsp;--%>
                                    </div>
                                    <div id="d0" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline; font-size: small"><span style="color: red">*</span>Accused Name </label>
                                        <br />
                                        <asp:TextBox ID="txtAccusedName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Accused Name"></asp:TextBox>
                                    </div>
                                <div id="d00" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Father/Spouse Name</label>
                                        <br />
                                        <asp:TextBox ID="txtSpouseName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Father/Spouse Name"></asp:TextBox>
                                    </div>
                                    <div id="d000" runat="server" class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Description of offence </label>
                                        <br />
                                        <asp:DropDownList ID="ddlDescriptionofoffence" autocomplete="off" runat="server" Width="70%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Description of offence"></asp:DropDownList>
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <div id="d1" runat="server">
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Gender</label>
                                        <br />
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Gender">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Transgender</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Age</label>
                                        <br />
                                        <asp:TextBox ID="txtAge" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" MaxLength="3" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Age"></asp:TextBox>
                                    </div>
                                   
                                                       
                                   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Religion</label>
                                        <br />
                                        <asp:DropDownList ID="ddReligion"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Religion"></asp:DropDownList>
                                    </div>
                                 
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Caste Category</label>
                                        <br />
                                        <asp:DropDownList ID="ddlCasteCategory"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Caste Category"></asp:DropDownList>
                                    </div>
                                  <%--  </div>--%>
                                <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                               <%-- <div id="d2" runat="server">--%>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Caste </label>
                                        <br />
                                        <asp:DropDownList ID="ddlCaste"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Caste"></asp:DropDownList>
                                    </div>
                              <%--      <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Caste</label>
                                        <br />
                                        <asp:DropDownList ID="ddlCaste" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Caste"></asp:DropDownList>
                                    </div>--%>
                                   <%-- <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Caste</label>
                                        <br />
                                        <asp:TextBox ID="txtCaste" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Caste"></asp:TextBox>
                                    </div>--%>
                                 <%--  <div class="clearfix"></div>
                                    <p>&nbsp;</p>--%>
                                  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Caste Details</label>
                                        <br />
                                        <asp:TextBox ID="txtCasteDetails" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Caste Details"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Accused Id Proof </label>
                                        <br />
                                        <asp:DropDownList ID="ddlAccusedIdProof"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Accused Id Proof"></asp:DropDownList>
                                    </div>
                                 
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>ID No</label>
                                        <br />
                                        <asp:TextBox ID="txtIDNo" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" MaxLength="20" title="ID No"></asp:TextBox>
                                    </div>
                                   <%-- </div>--%>
                                 <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                <%--<div id="d3" runat="server">--%>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Identification Mark</label>
                                        <br />
                                        <asp:TextBox ID="txtMarksOfIdentification" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Marks Of Identification"></asp:TextBox>
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
                                   
                                  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Mobile No </label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" autocomplete="off" onchange="phoneValidate()" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" MaxLength="10" title="Mobile No"></asp:TextBox>
                                    </div>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Alternate Mobile No </label>
                                        <br />
                                        <asp:TextBox ID="txtAlternate" autocomplete="off" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" onchange="phoneValidate1()" MaxLength="10" title="Alternate Mobile No"></asp:TextBox>
                                    </div>
                                 <%-- //  </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Occupation</label>
                                        <br />
                                        <asp:TextBox ID="txtOccupation" autocomplete="off" CssClass="form-control" runat="server" onkeypress="return onlyAlphabets(this,event);" data-toggle="tooltip" data-placement="right" title="Occupation"></asp:TextBox>
                                    </div>  
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                   <%--<div id="d4" runat="server">--%>
                                    <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span> Permanent Address </label>
                                        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk" runat="server" onclick="copyValue(this)" />Copy Address 
                                        <br />
                                        <asp:TextBox ID="txtPermanentAddress" autocomplete="off" CssClass="form-control" runat="server" Height="10%" Width="77.5%" data-toggle="tooltip" data-placement="right" title="Permanent Address" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>State</label><br />
                                        <%--<asp:TextBox ID="txtstate" autocomplete="off" CssClass="form-control" runat="server"  data-toggle="tooltip" data-placement="right" title="State"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" title="State"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>District</label><br />
                                           <asp:TextBox ID="txtdistrict" autocomplete="off" CssClass="form-control"  Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="District"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"  data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="District" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Thana</label><br />
                                           <asp:TextBox ID="txtthana" autocomplete="off" CssClass="form-control" Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="Thana"></asp:TextBox>
                                        <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" AutoPostBack="true" title="Thana"></asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span> Present Address</label>
                                        <br />
                                        <asp:TextBox ID="txtPresentAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Height="10%" Width="77.5%" data-placement="right" title="Present Address" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="x_title"></div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Breathe Analyze</label>
                                        <br />
                                        <asp:RadioButton ID="rdPositive" runat="server" GroupName="radio0" />Positive&nbsp;&nbsp;
                                           <asp:RadioButton ID="rdNegative" runat="server" GroupName="radio0" />Negative&nbsp;&nbsp;
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Breath Analyzer</label>
                                        <br />
                                        <asp:TextBox ID="txtBreathAnalyzer" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" MaxLength="5" data-placement="right" title="Breath Analyzer"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Blood Test</label>
                                        <br />
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="radio1" />Yes&nbsp;&nbsp;
                                           <asp:RadioButton ID="RadioButton2" runat="server" GroupName="radio1" />No&nbsp;&nbsp;
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Blood Test Result</label>
                                        <br />
                                        <asp:TextBox ID="txtBloodTestResult" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" MaxLength="20" data-placement="right" title="Blood Test Result"></asp:TextBox>
                                    </div>
                                       </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
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
