<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="FinalArgumentForm.aspx.cs" Inherits="UserMgmt.FinalArgumentForm" %>

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
                                <title>Final Argument Form</title>
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
                                    function validationMsg() {
                                       
                                       
                                         if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                            alert("Enter FIR details!");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                        }
                                          if (document.getElementById('<%=txtFinalArgumentDate.ClientID%>').value == '') {
                                              alert("Enter FinalArgument Date");
                                            document.getElementById("<% =txtFinalArgumentDate.ClientID%>").focus();
                                            return false;
                                          }
                                        if (document.getElementById('<%=txtNexthearingDate.ClientID%>').value == '') {
                                            alert("Enter Next hearing Date");
                                            document.getElementById("<% =txtNexthearingDate.ClientID%>").focus();
                                            return false;
                                        }
                                     <%--   var Final_Argument_Date = document.getElementById('<%=txtFinalArgumentDate.ClientID%>').value;
                                        var Nexthearing_Date = document.getElementById('<%=txtNexthearingDate.ClientID%>').value;
                                        //var cDate = new Date(Complaint_Date);
                                        //var fDate = new Date(FIR_Date);
                                        if (Final_Argument_Date != '' && Nexthearing_Date != '' && Final_Argument_Date > Nexthearing_Date) {
                                            alert("Please ensure that the Nexthearing Date  is greater than or equal to the Final Argument Date .");
                                            return false;
                                        }--%>

                                    }
                                </script>

                                <%-- <script language="javascript" type="text/javascript">
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
                                </script>--%>
                                
                                <%--<script type="text/javascript">

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
                                </script>--%>
                            </head>
                            <body>
                                   <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                            
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Final Argument Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                           
                                              </ContentTemplate>
                                    </asp:UpdatePanel> 
                                
                                <a>
                                     <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="btnSeizure_Click"  BorderStyle="Outset"> Seizure</asp:LinkButton>
                                </a>
                               <%-- <a>
                                       <asp:LinkButton runat="server" ID="btnFIR" Height="100%" Width="12%" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View FIR" OnClick="btnFIR_Click"  BorderStyle="Outset"> FIR</asp:LinkButton>
                                </a>
                                <a>
                                       <asp:LinkButton runat="server" ID="btnChargeSheet" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnChargeSheet_Click"  BorderStyle="Outset">Charge Sheet </asp:LinkButton>
                                </a>
                                 <a>
                                       <asp:LinkButton runat="server" ID="btnBail" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnBail_Click"  BorderStyle="Outset">Bail</asp:LinkButton>
                                </a>--%>
                                       <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                   
                                   <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                      <asp:TextBox ID="txtPRFIRNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                       <asp:TextBox ID="txtfirdate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>' Visible="false"  title="PR / FIR No"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span> Final Argument Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtFinalArgumentDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtFinalArgumentDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false"   title="Final Argument Date" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Next Hearing Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtNexthearingDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtNexthearingDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false"   title="Next Hearing date" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>

                                          <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <%--<div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red">*</span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red">*</span>Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" OnClick="UploadFile" />
                                            </span>
                                        </div>
                                    </div>--%>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <%--<div id="dummytable" runat="server" style="height: auto; width: 90%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
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
                                    </div>--%>
                                    <div class="col-md-11 col-sm-12 col-xs-12 form-inline">
                                      <%--  <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
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
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove"  OnClick="btnRemove_Click" ImageUrl="~/img/delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>


                                        </asp:GridView>--%>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick ="btnSubmit_Click" CssClass="btn btn-primary">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
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
