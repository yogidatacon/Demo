<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="DMH.aspx.cs" Inherits="UserMgmt.DMH_Action" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="BodyContent">
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
                                
                                <script type="text/javascript" src="/common/theme/js/flot/date.js"></script>
                                <title>Basic Information</title>
                                <script language="javascript" type="text/javascript">
                               
                                    function Selectdate(e) {
                                        debugger;
                                        var todayDate = e.get_selectedDate();
                                        var dd = todayDate.getDate();
                                        var mm = todayDate.getMonth() + 1; //January is 0!

                                        var yyyy = todayDate.getFullYear();
                                        if (dd < 10) {
                                            dd = '0' + dd;
                                        }
                                        if (mm < 10) {
                                            mm = '0' + mm;
                                        }
                                        todayDate = dd + '-' + mm + '-' + yyyy;
                                        $('#BodyContent_txtDATE').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                        $('#BodyContent_txtdob').val(todayDate);
                                    }
                                 
                                 </script>
                                 <script type="text/javascript">
                                     function blockSpecialChar(e) {
                                         var k;
                                         document.all ? k = e.keyCode : k = e.which;
                                         return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                                     }
                </script>
                 <script type="text/javascript" src="js/jquery2.min.js"> </script>               
                            <script type="text/javascript">
                                function ShowHideDiv() {
                                    var ddlActionPorposed = document.getElementById("BodyContent_ddlActionPorposed");
                                    var doh = document.getElementById("BodyContent_DOH");
                                    var caseclosed = document.getElementById("BodyContent_caseclosed");
                                    debugger;;
                                    if (ddlActionPorposed.value == "Next Hearing") {
                                        $("#BodyContent_DOH").show();
                                        $("#BodyContent_caseclosed").hide();
                                    }
                                    else {
                                        $("#BodyContent_DOH").hide();
                                        $("#BodyContent_caseclosed").show();
                                        //  caseclosed.style.display = "block";
                                    }


                                }
                 </script>
                 <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                      
                                        if (document.getElementById('<%=ddlActionPorposed.ClientID%>').value == 'Select') {
                                            alert("Select Action Porposed.");
                                            document.getElementById("<% =ddlActionPorposed.ClientID%>").focus();
                                            return false;
                                        }
                                        if (document.getElementById('<%=ddlActionPorposed.ClientID%>').value == 'Next Hearing') {
                                            if (document.getElementById('<%=txtDateofHearing.ClientID%>').value == '') {
                                                alert("Select Date of Hearing.");
                                                document.getElementById("<% =txtDateofHearing.ClientID%>").focus();
                                                return false;
                                            }
                                        }
                                        if (document.getElementById('<%=ddlActionPorposed.ClientID%>').value == 'Case Dispose')
                                        {
                                            debugger;;
                                            var val=true;
                                            if (document.getElementById('<%=txtConfiscationOrderNo.ClientID%>').value == '')
                                            {
                                                alert("Enter Confiscation Order No.");
                                                document.getElementById("<% =txtConfiscationOrderNo.ClientID%>").focus();
                                               val=false;
                                            }
                                            if (document.getElementById('<%=txtConfiscationOrderDate.ClientID%>').value == '')
                                            {
                                                alert("Enter Confiscation Order Date.");
                                                document.getElementById("<% =txtConfiscationOrderDate.ClientID%>").focus();
                                                val = false;
                                            }
                                            var obj = document.getElementById('<%=grdAdd.ClientID%>');
                                            if (obj === undefined || obj == null)
                                            {
                                                alert("Upload Atleast One Case Dispose Related Document.");
                                                document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                                val = false;
                                            }
                                            return val;
                                        }
                                        
                              
                                    }
              </script>
              <script>
                                    $(document).ready(function () {
                                        debugger;;
                                        if ($('#BodyContent_txtProposedLetterNo').val() == "")
                                            $('#BodyContent_txtProposedLetterNo').val($('#BodyContent_ProposedLetterNo').val());
                                        if ($('#BodyContent_txtProposedLetterDate').val() == "")
                                            $('#BodyContent_txtProposedLetterDate').val($('#BodyContent_ProposedLetterDate').val());
                                        if ($('#BodyContent_ddlCaseType').val() == ""||$('#BodyContent_ddlCaseType').val() == "Select")
                                            $('#BodyContent_ddlCaseType').val($('#BodyContent_CaseType').val());
                                        if ($('#BodyContent_txtCaseNo').val() == "")
                                            $('#BodyContent_txtCaseNo').val($('#BodyContent_CaseNo').val());
                                        if ($('#BodyContent_txtCaseRegisterDate').val() == "")
                                            $('#BodyContent_txtCaseRegisterDate').val($('#BodyContent_CaseRegisterDate').val());
                                        if ($('#BodyContent_ddlNameofCourt').val() == "" || $('#BodyContent_ddlNameofCourt').val() == "Select")
                                            $('#BodyContent_ddlNameofCourt').val($('#BodyContent_NameofCourt').val());
                                        ShowHideDiv();
                                     
                                    });

             </script>
                            </head>
                            <body>
                              
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Case Hearing at DM Office for Vehicle / Property under Prohibition Act</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Seizure By :</label>
                                            
                                                <asp:RadioButton ID="rdbExcise" runat="server" GroupName="radio1" AutoPostBack="true" />Excise&nbsp;
                                        <asp:RadioButton ID="rdbPolice" GroupName="radio1" AutoPostBack="true" runat="server"  />Police&nbsp;
                                        
                                     
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red;width:40%" >*</span>Confiscation Of :</label>&nbsp;
                                                
                                                <asp:RadioButton ID="rdVehicle" runat="server" GroupName="radio" AutoPostBack="true"  />Vehicle 
                                        <asp:RadioButton ID="rdProperty" GroupName="radio" AutoPostBack="true" runat="server"  />Property 
                                        
                                     
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Name Of District </label><br />
                                                
                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="false"
                                                    data-toggle="tooltip" data-placement="right" title="Name Of District" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Thana Name</label><br />
                                                
                                                <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" Enabled="false"
                                        data-toggle="tooltip" data-placement="right" title="Thana Name" AutoPostBack="true" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged">
                                    </asp:DropDownList>
                                            </div>  
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>PR / FIR No</label><br />
                                                
                                                <asp:DropDownList ID="ddlPrFirNo" Width="90%" runat="server" CssClass="form-control" Enabled="false"  AutoPostBack="true" data-toggle="tooltip" data-placement="right" title="PR / FIR No" ></asp:DropDownList>
                                            </div>
                                     
                                   <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline" runat="server" id="divpr" visible="false">
                                                <label class="control-label"><span style="color: red">*</span>FIR No</label><br />
                                                
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Thana"></asp:DropDownList>
                                            </div>--%>
                                    
                                    
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="width:60%"><span style="color: red">*</span>Proposed Letter No</label><br />
                                                
                                                <asp:TextBox ID="txtProposedLetterNo" CssClass="form-control" runat="server" ReadOnly data-toggle="tooltip" data-placement="right" 
                                                    onkeypress="return blockSpecialChar(event)" MaxLength="100" title="Proposed Letter No"></asp:TextBox>
                                             <asp:HiddenField ID="ProposedLetterNo" runat="server" />
                                            </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="width:60%"><span style="color: red">*</span>Proposed Letter Date</label><br />
                                                <cc1:CalendarExtender runat="server"  PopupButtonID="Image1" TargetControlID="txtProposedLetterDate" Format="dd-MM-yyyy" ID="CalendarExtender4"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtProposedLetterDate"   data-toggle="tooltip" data-placement="right" title="Proposed Letter Date" class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="ProposedLetterDate" runat="server" />
                                        <asp:HiddenField ID="dateofhearing" runat="server" />
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Case Type</label>&nbsp; &nbsp;&nbsp;<br />
                                              <asp:DropDownList ID="ddlCaseType" AutoPostBack="true"  runat="server" CssClass="form-control" Enabled="false"
                                                    data-toggle="tooltip" data-placement="right" title="Case Type">
                                                  <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                                    <asp:ListItem Text="Old" Value="Old"></asp:ListItem>
                                                   </asp:DropDownList>   
                                        <asp:HiddenField ID="CaseType" runat="server" />                  
                                            </div>
                                            
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Case No</label><br />
                                                
                                                <asp:TextBox ID="txtCaseNo" ReadOnly AutoComplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" MaxLength="20" title="Case No"></asp:TextBox>
                                         <asp:HiddenField ID="CaseNo" runat="server" /> 
                                            </div>
                                     
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="width:60%"><span style="color: red">*</span>Case Register Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtCaseRegisterDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtCaseRegisterDate"   data-toggle="tooltip" data-placement="right" title="Case Register Date" class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="CaseRegisterDate" runat="server" />
                                            </div>
                                     <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Name of Court</label><br />
                                                
                                                <asp:DropDownList ID="ddlNameofCourt"  AutoPostBack="true" runat="server" CssClass="form-control"  Enabled="false"
                                                    data-toggle="tooltip" data-placement="right" title="Name of Court">
                                                    

                                                </asp:DropDownList>
                                            <asp:HiddenField ID="NameofCourt" runat="server" />
                                            </div>
                                    
                                             
                                                <div class="col-md-5 col-sm-12 col-xs-12 form-inline ">
                                                   <label class="control-label" style="display: inline; font-size:small"><span style="color: red"></span>Case Registration Remarks</label>
                                                <asp:TextBox ID="txtRremarks" ReadOnly="true" CssClass="form-control" MaxLength="250" runat="server" Width="90%" data-toggle="tooltip" data-placement="right" title="Case Registration Remarks" onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                     <div class="x_title">
                                    <h2>Case Registery Attached Files</h2>
                                    <div class="clearfix"></div>
                                </div>        
                                    <div class="col-md-11 col-sm-12 col-xs-12 form-inline" >
                                        <asp:GridView ID="grdAdd1" runat="server" AutoGenerateColumns="false"
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
                                                <asp:TemplateField HeaderText="Document Type" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentType" runat="server" Visible="true" Text='<%#Eval("Doc_Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                      
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                    
                                    <div class="x_title">
                                         <h2>Case Hearing Details</h2>
                                    <div class="clearfix"></div>
                                      </div>   
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Action Proposed</label><br />
                                                
                                                <asp:DropDownList ID="ddlActionPorposed" runat="server" CssClass="form-control" onchange = "ShowHideDiv()" 
                                                    data-toggle="tooltip" data-placement="right" title="Action Proposed">
                                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="Case Dispose" Value="Case Dispose"></asp:ListItem>
                                                    <asp:ListItem Text="Next Hearing" Value="Next Hearing"></asp:ListItem>

                                                </asp:DropDownList>
                                         <asp:HiddenField ID="ActionPorposed" runat="server" />
                                            </div>
                                    <div id="caseclosed"  runat="server" style="display: none">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline" >
                                                <label class="control-label" style="width:60%"><span style="color:red">*</span>Confiscation Order No</label><br />
                                                
                                                <asp:TextBox ID="txtConfiscationOrderNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" MaxLength="100" title="Confiscation Order No"></asp:TextBox>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="width:60%"><span style="color:red">*</span>Confiscation Order Date</label><br />
                                                
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image3" TargetControlID="txtConfiscationOrderDate" Format="dd-MM-yyyy" ID="CalendarExtender3" ></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtConfiscationOrderDate"  data-toggle="tooltip" data-placement="right" title="Confiscation Order Date " class="form-control validate[required]" onkeypress="return blockAllChar(event)"  AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image3" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                            </div>
                                        </div>
                                    <div id="DOH" style="display: none" runat="server" >
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline" runat="server">
                                                <label class="control-label"><span style="color: red">*</span>Date of Hearing</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image4" TargetControlID="txtDateofHearing" Format="dd-MM-yyyy" ID="CalendarExtender5"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDateofHearing"  data-toggle="tooltip" data-placement="right" title="Proposed Letter Date" class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image4" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="hearing" runat="server">
                                        <div class="x_title">
                                            <h4>Hearing Summary</h4>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div style="color:gray" class="col-md-10 col-sm-12 col-xs-12 form-inline" class="x_title">
                                            <asp:GridView ID="grdHearingDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Hearings" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action Proposed" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionProposed" runat="server" Text='<%# Eval("case_action") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Next Hearing Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnexthearingdate" runat="server" Text='<%# Eval("next_hearing_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingComments" runat="server" Text='<%# Eval("hearing_remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("creation_date") %>'></asp:Label>
                                                        </ItemTemplate>
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
                                     
                                     <div id="docs" runat="server">
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline" id="Label1" runat="server"><span style="color: red"></span>Document Type</label><br />
                                           
                                            <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control" 
                                            AutoPostBack="true">
                                                <asp:ListItem Value="Select" Text="Select" Selected="true">                                                    
                                                </asp:ListItem>
                                                <asp:ListItem Value="FIR Document" Text="FIR Document">                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Seizure List" Text="Seizure List">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Chemical Test Report" Text="Chemical Test Report">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Vehicle / Property Owner Document" Text="Vehicle / Property Owner Document">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Driving Licence" Text="Driving Licence">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Vakalatnama" Text="Vakalatnama">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="DM Order" Text="DM Order">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Excise Com Order" Text="Excise Com Order">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                <asp:ListItem Value="Others" Text="Others">                                                   
                                                                                                   
                                                </asp:ListItem>
                                                
                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="width:60%"><span></span>Attached Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline" id="dn" runat="server"><span style="color: red"></span>Document Name</label><br />
                                            <asp:TextBox ID="txtDiscription" AutoComplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Name"></asp:TextBox>
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
                                         <div class="col-md-11 col-sm-12 col-xs-12 form-inline" >
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
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
                                                 <asp:TemplateField HeaderText="Document Type" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentType" runat="server" Visible="true" Text='<%#Eval("Doc_Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1"  CommandArgument='<%#Eval("doc_path") %>'  CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
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
                                            
                                             
                                                <div class="col-md-6 col-sm-12 col-xs-12 ">
                                                   <label class="control-label" style="display: inline; font-size:small"><span style="color: red"></span>Remarks</label>
                                                <asp:TextBox ID="txtRemarks" AutoComplete="off" CssClass="form-control" MaxLength="250" runat="server" Width="90%" data-toggle="tooltip" data-placement="right" title="Remarks" onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click"> <span aria-hidden="true" ></span>Save</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click" > <span aria-hidden="true"  ></span>Submit</asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel <span aria-hidden="true" ></span></asp:LinkButton>
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
