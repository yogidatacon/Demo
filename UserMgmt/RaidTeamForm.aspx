<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.master" AutoEventWireup="true" CodeBehind="RaidTeamForm.aspx.cs" Inherits="UserMgmt.RaidTeamForm" %>
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
                                <title>Raid Team Form</title>
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
                                        var mobileN = $('#BodyContent_NestedBodyContent_txtMobile').val().length;

                                        if (mobileN != 10) {
                                            alert("Invalid Mobile No.");
                                            $('#BodyContent_NestedBodyContent_txtMobile').val("");
                                            $('#BodyContent_NestedBodyContent_txtMobile').focus();
                                        }
                                    }
                                </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=txtOfficerName.ClientID%>').value == '') {
                                            alert("Enter Officer Name");
                                            document.getElementById("<% =txtOfficerName.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=ddlDesignation.ClientID%>').value == 'Select') {
                                             alert("Select Designation");
                                            document.getElementById("<% =ddlDesignation.ClientID%>").focus();
                                            return false;

                                         }
                                         if (document.getElementById('<%=ddlRaidTeamLeadBy.ClientID%>').value == 'Select') {
                                             alert("Enter Raid Team Lead By");
                                            document.getElementById("<% =ddlRaidTeamLeadBy.ClientID%>").focus();
                                            return false;

                                         }
                                        <%--  if (document.getElementById('<%=txtMobile.ClientID%>').value == '') {
                                              alert("Enter Mobile No");
                                            document.getElementById("<% =txtMobile.ClientID%>").focus();
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
                                
                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Raid Team Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   
                                     
                                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Officer Name</label>
                                        <br />
                                        <asp:TextBox ID="txtOfficerName" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyAlphabets(this,event);" data-placement="right" title="Officer Name" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Designation  </label>
                                        <br />
                                        <asp:DropDownList ID="ddlDesignation" Width="60%"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Designation"></asp:DropDownList>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red">*</span>Raid Team Lead By </label>
                                        <br />
                                        <asp:DropDownList ID="ddlRaidTeamLeadBy" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Raid Team Lead By"></asp:DropDownList>
                                    </div>
                                    <div  class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display:inline"><span style="color: red"></span>Mobile No</label>
                                        <br />
                                        <asp:TextBox ID="txtMobile" autocomplete="off" CssClass="form-control" MaxLength="10" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" onchange="phoneValidate()" data-toggle="tooltip" data-placement="right" title="Mobile No" ></asp:TextBox>
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
                                                <asp:TemplateField HeaderText="Description" ItemStyle-Font-Bold="true"  Visible="true" ItemStyle-Width="20px">
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
                                                         <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" OnClick="DownloadFile"  CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;    
                                                        <%--<asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />--%>
                                                         <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' OnClick="btnRemove_Click" CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView></div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                           <%-- <asp:LinkButton ID="btnSubmit" Visible="false" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true" OnClick="btnCancel_Click"
                                                CssClass="btn btn-danger">Cancel
                                            <span aria-hidden="true"> </span></asp:LinkButton>
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
