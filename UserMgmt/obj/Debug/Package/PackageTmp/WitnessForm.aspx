<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="WitnessForm.aspx.cs" Inherits="UserMgmt.WitnessForm" %>
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
                                <title>Witness Form</title>
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
                                    function phoneValidate() {
                                        debugger;
                                        var mobileN = $('#BodyContent_NestedBodyContent_txtMobileNo').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Mobile No.");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').val("");
                                            $('#BodyContent_NestedBodyContent_txtMobileNo').focus();
                                        }
                                    }
                                    function emailValidate() {
                                        debugger;
                                        var emailId = $('#BodyContent_NestedBodyContent_txtEmailId').val();
                                        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                        if (!emailId.match(mailformat)) {
                                            alert("Enter Valid Email Id!");
                                            $('#BodyContent_NestedBodyContent_txtEmailId').val("");
                                            $('#BodyContent_NestedBodyContent_txtEmailId').focus();
                                            return false;
                                        }

                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                         if (document.getElementById('<%=rdDepartmentOfficer.ClientID%>').checked == false && document.getElementById('<%=rdIndependentPerson.ClientID%>').checked == false) {
                                             alert("Select DepartmentOfficer/IndependentPerson");
                                            document.getElementById("<% =rdDepartmentOfficer.ClientID%>").focus();
                                            return false;
                                        }  

                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter  Name");
                                            document.getElementById("<% =txtName.ClientID%>").focus();
                                            return false;

                                        }
                                        
                                     
                                         if (document.getElementById('<%=ddlDesignation.ClientID%>').value == 'Select') {
                                             alert("Enter Designation");
                                            document.getElementById("<% =ddlDesignation.ClientID%>").focus();
                                            return false;

                                         }
                                          if (document.getElementById('<%=ddlGender.ClientID%>').value == 'Select') {
                                             alert("Enter Gender");
                                            document.getElementById("<% =ddlGender.ClientID%>").focus();
                                            return false;

                                         }
                                        <%-- if (document.getElementById('<%=txtAge.ClientID%>').value == '') {
                                              alert("Enter Age");
                                            document.getElementById("<% =txtAge.ClientID%>").focus();
                                            return false;

                                        }
                                     
                                      
                                       if (document.getElementById('<%=txtFatherSpouseName.ClientID%>').value == '') {
                                             alert("Enter Father Spouse Name");
                                            document.getElementById("<% =txtFatherSpouseName.ClientID%>").focus();
                                            return false;

                                         }
                                       
                                       if ((document.getElementById('<%=txtMobileNo.ClientID%>').value== '') || (document.getElementById('<%=txtMobileNo.ClientID%>').value.length<"10")) {
                                             alert("Enter valid Mobile No");
                                            document.getElementById("<% =txtMobileNo.ClientID%>").focus();
                                            return false;

                                         }--%>
                                    

                                      <%--   if (document.getElementById('<%=txtPermanentAddress.ClientID%>').value == '') {
                                             alert("Enter PermanentAddress");
                                            document.getElementById("<% =txtPermanentAddress.ClientID%>").focus();
                                            return false;

                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtPermanentAddress").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <3)
                                             {
                                                 alert("PermanentAddress should be minimum 3 character");
                                                 document.getElementById("<% =txtPermanentAddress.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtPresentAddress.ClientID%>').value == '') {
                                             alert("Enter PresentAddress");
                                            document.getElementById("<% =txtPresentAddress.ClientID%>").focus();
                                            return false;
                                         }--%>
                                    }
                                </script>
                                  <script language="javascript" type="text/javascript">
                                    function CheckDiscription() {
                                        debugger;
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                            alert("Please Attach file");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                            alert("Enter Discription");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
                                
                                <script type="text/javascript">

                                    function validateExtraDocuments() {
                                       
                                        var fileInput = document.getElementById('<%= idupDocument.ClientID %>');
                                        var filePath = fileInput.value;
                                        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf|\.doc|\.docx)$/i;
                                        if (!allowedExtensions.exec(filePath)) {
                                            alert('Please upload file having extensions .jpeg/.jpg/.png/.pdf/.doc/.docx only.');
                                            fileInput.value = '';
                                            return false;
                                        }

                                        var uploadControl = document.getElementById('<%= idupDocument.ClientID %>');
                                        if (uploadControl.files[0].size > 2000000) {
                                            alert("Document size should be less than or eqaul to 2MB !!!!!")
                                            document.getElementById('<%= idupDocument.ClientID %>').value = "";

                                            return false;
                                        }
                                        else {
                                            return true;
                                        }
                                    }
                                </script>

                                <script type="text/javascript">
                                    function copyValue(Chk) {
                                        if(Chk.checked)
                                        {
                                             document.getElementById('<%=txtPresentAddress.ClientID%>').value  = document.getElementById('<%=txtPermanentAddress.ClientID%>').value;
                                        }
                                       
                                    }
                                </script>
                             <script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=grdWitnessView] input[type=checkbox]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=grdWitnessView] input[type=checkbox]").removeAttr("checked");
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
                                    <h2>Witness Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   
                                           <div id="searchid" runat="server">
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Name</label><br />
                                                <asp:TextBox ID="txtWitnessName" autocomplete="off" runat="server" Width="60%" onkeypress="return onlyAlphabets(this,event);" CssClass="form-control"></asp:TextBox>
                                            </div>
                                              <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red"></span>Father/Spouse Name</label><br />
                                                <asp:TextBox ID="txtFSName" autocomplete="off" runat="server" Width="60%" onkeypress="return onlyAlphabets(this,event);" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Mobile No</label><br />
                                                <asp:TextBox ID="txtMNo" autocomplete="off" runat="server" Width="80%" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Mobile No" MinLength="10" MaxLength="10" ></asp:TextBox>&nbsp;
                                               
                                            </div>
                                             <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span></label><br />
                                                  <asp:Button ID="btnWitnessSearch" runat="server" Width="60%"  Text="Witness Search" CssClass="btn btn primary" OnClick="btnSearch_Click" />
                                            </div>
                                             
                                   
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        <div style =" width:1000px; overflow:auto;"> 
                                    <asp:GridView ID="grdWitnessView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdWitnessView_PageIndexChanging" 
                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Witness Type" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWitnessType" runat="server" Visible="true" Text='<%#Eval("witnesstype") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Witness Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWitnessName" runat="server" Visible="true" Text='<%#Eval("witnessname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Visible="true" Text='<%#Eval("designation_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Gender" ItemStyle-Font-Bold="true"  Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGender" runat="server" Visible="false" Text='<%#Eval("gender_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Age" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAge" runat="server" Visible="false" Text='<%#Eval("witness_age") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="mobileno" ItemStyle-Font-Bold="true" Visible="True" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmobileno" runat="server" Visible="true" Text='<%#Eval("mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="designation_code" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation_code" runat="server" Visible="false" Text='<%#Eval("designation_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="landline" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllandline" runat="server" Visible="false" Text='<%#Eval("landline") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="witness_emailid" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwitness_emailid" runat="server" Visible="false" Text='<%#Eval("witness_emailid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="permanentaddress" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpermanentaddress" runat="server" Visible="false" Text='<%#Eval("permanentaddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="presentaddress" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpresentaddress" runat="server" Visible="false" Text='<%#Eval("presentaddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Father/ Spouse Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFatherSpouseName" runat="server" Visible="true" Text='<%#Eval("relativename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="View Document" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblViewDocument" runat="server" Visible="true" Text='<%#Eval("relativename") %>'></asp:Label>
                                                </ItemTemplate>--%>
                                           <%-- </asp:TemplateField>--%>
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
                                               </div>
                                      <div class="clearfix"></div>
                                    
                                       <div class="x_title"/>
                                        </div>
                                       </div>
                                   
                                       <div class="clearfix"></div>
                                 
                                      <div  class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <%--<label class="control-label" style="display:inline"><span style="color: red">*</span>Department</label>--%>
                                        <br />
                                          <asp:RadioButton ID="rdDepartmentOfficer" AutoPostBack="true" runat="server" GroupName="radio" OnCheckedChanged="rdIndependentPerson_OncheckedChange" /><label class="control-label" style="display:inline"><span style="color: red"></span>Department Officer</label>&nbsp;&nbsp;
                                          <asp:RadioButton ID="rdIndependentPerson" AutoPostBack="true" runat="server" GroupName="radio" OnCheckedChanged="rdIndependentPerson_OncheckedChange" /> <label class="control-label" style="display:inline"><span style="color: red"></span>Independent Person</label>&nbsp;&nbsp;
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Name</label>
                                        <br />
                                        <asp:TextBox ID="txtName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Name" onkeypress="return onlyAlphabets(this,event);" ></asp:TextBox>
                                    </div>
                                    <div  id="desg" runat="server">
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Designation</label>
                                        <br />     
                                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="60%" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList> 
                                       <%-- <asp:TextBox ID="txtDesignation" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Designation" ></asp:TextBox>--%>
                                    </div></div>
                                       <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Age</label>
                                        <br />
                                        <asp:TextBox ID="txtAge" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" MaxLength="3" onkeypress="return onlyDotsAndNumbers(this,event);" title="Age" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Gender</label>
                                        <br />
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Gender"></asp:DropDownList>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Father/Spouse Name</label>
                                        <br />
                                        <asp:TextBox ID="txtFatherSpouseName" autocomplete="off" CssClass="form-control" runat="server"  data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Father / Spouse Name" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Mobile No</label>
                                        <br />
                                        <asp:TextBox ID="txtMobileNo" autocomplete="off" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Mobile No" onchange="phoneValidate()" MinLength="10" MaxLength="10" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Landline No</label>
                                        <br />
                                        <asp:TextBox ID="txtLandlineNo" autocomplete="off" CssClass="form-control" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" data-toggle="tooltip" data-placement="right" title="Landline No" ></asp:TextBox>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Email Id</label>
                                        <br />
                                        <asp:TextBox ID="txtEmailId" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Email Id" onchange="emailValidate()"  ></asp:TextBox>
                                        

                                    </div>
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                       <div  class="col-md-6 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Permanent Address</label>
                                                &nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="chk" runat="server" onclick="copyValue(this)" /> Copy Address
                                        <br />
                                        <asp:TextBox ID="txtPermanentAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Width="85.5%" data-placement="right" title="Permanent Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-6 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="display:inline;font-size:small"><span style="color: red"></span>Present Address</label>
                                        <br />
                                        <asp:TextBox ID="txtPresentAddress" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" Width="85.5%" data-placement="right" title="Present Address" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                      <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" autocomplete="off" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
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
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="File Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" runat="server" Visible="true" Text='<%#Eval("Doc_name") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp; 
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" OnClick="DownloadFile" ImageUrl="~/img/download.png" runat="server" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" OnClick="btnRemove_Click" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
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
