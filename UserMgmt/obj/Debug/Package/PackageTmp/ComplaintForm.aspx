<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ComplaintForm.aspx.cs" Inherits="UserMgmt.ComplaintForm" %>
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
                                <title>Complaint Form</title>
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
                                 <script language="javascript" type="text/javascript">
                                     function validationMsg1() {

                                         if (document.getElementById('<%=txtAddress.ClientID%>').value == '') {
                                             alert("Enter Approver Remarks Name");
                                             document.getElementById("<% =txtAddress.ClientID%>").focus();
                                             return false;
                                         }
                                     }

                                     function phoneValidate() {
                                         debugger;
                                         var mobileN = $('#BodyContent_txtmobile').val().length;

                                         if (mobileN != 10) {
                                             alert("Invalid phone number.");
                                             $('#' + BodyContent_txtmobile).val("");
                                             $('#' + BodyContent_txtmobile).focus();
                                         }
                                     }

                                     function emailValidate() {
                                         debugger;
                                         var emailId = $('#BodyContent_txtemail').val();
                                         var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                                         if (!emailId.match(mailformat)) {
                                             alert("Enter Valid Email Id!");
                                             $('#BodyContent_txtemail').val("");
                                             $('#BodyContent_txtemail').focus();
                                             return false;
                                         }

                                     }

                                        </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtcomplainantname.ClientID%>').value == '') {
                                            alert("Enter Complainant Name");
                                            document.getElementById("<% =txtcomplainantname.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtmobile.ClientID%>').value == '') {
                                             alert("Enter Contact Number");
                                            document.getElementById("<% =txtmobile.ClientID%>").focus();
                                            return false;

                                         }
                                         if (document.getElementById('<%=txtemail.ClientID%>').value == '') {
                                            alert("Enter Email");
                                            document.getElementById("<% =txtemail.ClientID%>").focus();
                                            return false;

                                        }

                                        if (document.getElementById('<%=ddlcomplaintype.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter Complain Type");
                                            document.getElementById("<% =ddlcomplaintype.ClientID%>").focus();
                                            return false;

                                        }

                                         if (document.getElementById('<%=txtAddress.ClientID%>').value == '') {
                                            alert("Enter Address");
                                            document.getElementById("<% =txtAddress.ClientID%>").focus();
                                            return false;

                                        }

                                     if (document.getElementById('<%=txtcomplaintdetails.ClientID%>').value == '') {
                                            alert("Enter Complaint Details");
                                            document.getElementById("<% =txtcomplaintdetails.ClientID%>").focus();
                                            return false;

                                        }
                                    
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
                                
                            </head>
                            <body>
                              <%--  <div>
                                    <ul class="nav nav-tabs">
                                        <li >
                                            <asp:LinkButton runat="server" ID="btnIndent" OnClick="btnIndent_Click">
                                        <span style="color: #fff; font-size: 14px;">Indent for Molasses</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnARM" OnClick="btnARM_Click">
                                        <span style="color: #fff; font-size: 14px;">Allocation Request for Molasses</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>--%>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Complaint Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                  <%--  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Allotment Request Date </label>
                                        <br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDate" Format="dd-MM-yyyy" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="Indent Date" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                        </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob" runat="server" />
                                    </div>--%>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red; display:inline">*</span>Complainant Name </label><br />
                                     
                                        <asp:TextBox ID="txtcomplainantname" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Complainant Name" ></asp:TextBox>
                                    </div>
                                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Contact Number</label><br />
                                                <asp:TextBox ID="txtmobile" AutoComplete="off" Height="30px" width="60%" runat="server" name="mobile" MaxLength="10" data-toggle="tooltip" data-placement="right" title="Contact Number" class="form-control validate[custom[phone],required]" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                     </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Email Address</label><br />
                                                <asp:TextBox ID="txtemail" AutoComplete="off" Height="30px" width="60%" class="form-control" data-toggle="tooltip" data-placement="right" title="Email Address" runat="server" onchange="emailValidate(this);"></asp:TextBox>
                                            </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Complaint Type  </label>
                                        <br />
                                        <asp:DropDownList ID="ddlcomplaintype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Complaint Type">
                                            <asp:ListItem Value="LS">Liquior Sale</asp:ListItem>
                                              <asp:ListItem Value="CO">Complaint Against Officer</asp:ListItem>
                                            <asp:ListItem Value="SG">Suggestion</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  id="approverremaks" runat="server" class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red">*</span>Address</label><br />
                                  <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" data-toggle="tooltip"  height="5%" width="95%" data-placement="right" title="Address" TextMode="MultiLine"></asp:TextBox>
                                                 
                                    </div>

                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  id="Div1" runat="server" class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red">*</span>Complaint Details</label><br />
                                  <asp:TextBox ID="txtcomplaintdetails" runat="server" CssClass="form-control" data-toggle="tooltip"  height="5%" width="95%" data-placement="right" title="Complaint Details" TextMode="MultiLine"></asp:TextBox>
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
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Village Name</label>
                        <br />
                        <asp:TextBox ID="txtvillage" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ></asp:TextBox>
                    </div>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Land Mark</label>
                        <br />
                        <asp:TextBox ID="txtlandmark" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email ID" ></asp:TextBox>
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
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                                       
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
                                     <div class="col-md-10 col-sm-12 col-xs-12 form-inline">
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" OnClick="DownloadFile" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" OnClick="btnRemove_Click" />
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
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click" >
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click" >Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
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
