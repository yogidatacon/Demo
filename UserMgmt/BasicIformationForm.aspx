<%@ Page Title="" Language="C#"  MasterPageFile="~/NestedMasterCaseMgmt.Master"  AutoEventWireup="true" CodeBehind="BasicIformationForm.aspx.cs" Inherits="UserMgmt.BasicIformationForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NestedBodyContent">
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
                                <title>Basic Information</title>
                                   <script language="javascript" type="text/javascript">
                                 $(document).ready(function () {
                                        debugger;
                                        if ($('#BodyContent_txtDATE').val() == "") {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                        }
                                 });
</script>
                                <script language="javascript" type="text/javascript">
                                //function Selectdate(e) {
                                //        debugger;
                                //        var todayDate = e.get_selectedDate();
                                //        var dd = todayDate.getDate();
                                //        var mm = todayDate.getMonth() + 1; //January is 0!

                                //        var yyyy = todayDate.getFullYear();
                                //        if (dd < 10) {
                                //            dd = '0' + dd;
                                //        }
                                //        if (mm < 10) {
                                //            mm = '0' + mm;
                                //        }
                                //        todayDate = dd + '-' + mm + '-' + yyyy;
                                     
                                //        $('#BodyContent_txtDATE').val(todayDate);
                                //        $('#BodyContent_txtdob').val(todayDate);
                                //}
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
                                    $('#BodyContent_NestedBodyContent_txtDATE').val(todayDate);
                                        //var date1 = $('#BodyContent_txtDATE').val();
                                    if (todayDate!=null)
                                        $('#BodyContent_NestedBodyContent_txtdob').val(todayDate);
                                }
                                 </script>
                                 <script type="text/javascript">
    function blockSpecialChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
       
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }
    function blockAllChar(e) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false;
    }
    </script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        debugger;;
                                         if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Select Raid Date");
                                            document.getElementById("<% =txtDATE.ClientID%>").focus();
                                            return false;
                                        }
                                       <%-- if (document.getElementById('<%=raidtime.ClientID%>').value == '') {
                                            alert("Select Raid time");
                                            document.getElementById("<% =raidtime.ClientID%>").focus();
                                             return false;
                                         }--%>
                                         if (document.getElementById('<%=txtRaidLocation.ClientID%>').value == '') {
                                             alert("Enter Raid Location");
                                            document.getElementById("<% =txtRaidLocation.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtRaidLocation").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Raid Location should be minimum 3 character");
                                                 document.getElementById("<% =txtRaidLocation.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                       
                                        if (document.getElementById('<%=ddlThana.ClientID%>').value == 'Select') {
                                            alert("Select Thana Name");
                                            document.getElementById("<% =ddlThana.ClientID%>").focus();
                                             return false;
                                        }
                                        if (document.getElementById('<%=txtName.ClientID%>').value == '') {
                                            alert("Enter Recovery Based on Complaint details");
                                            document.getElementById("<% =txtName.ClientID%>").focus();
                                            return false;
                                        }
                                          else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtName").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Recovery Based on Complaint details should be minimum 3 character");
                                                 document.getElementById("<% =txtName.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                        <%--if (document.getElementById('<%=txtManualBookSeizure.ClientID%>').value == '') {
                                            alert("Enter ManualBook Seizure");
                                            document.getElementById("<% =txtManualBookSeizure.ClientID%>").focus();
                                             return false;
                                        }
                                        else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtManualBookSeizure").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("ManualBook Seizure should be minimum 3 character");
                                                 document.getElementById("<% =txtManualBookSeizure.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                         if (document.getElementById('<%=txtLatitude.ClientID%>').value == '') {
                                             alert("Enter Latitude");
                                            document.getElementById("<% =txtLatitude.ClientID%>").focus();
                                            return false;
                                         }
                                        else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtLatitude").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Latitude should be minimum 3 character");
                                                 document.getElementById("<% =txtLatitude.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                        if (document.getElementById('<%=txtLongitude.ClientID%>').value == '') {
                                            alert("Enter Longitude");
                                            document.getElementById("<% =txtLongitude.ClientID%>").focus();
                                             return false;
                                        }
                                         else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtLongitude").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Longitude should be minimum 3 character");
                                                 document.getElementById("<% =txtLongitude.ClientID%>").focus();
                                                 return false;
                                             }
                                         }
                                        if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                        }
                                         else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtRemarks").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Remarks should be minimum 5 character");
                                                 document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                                 return false;
                                             }
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
                               <%-- <div>
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnBasicInformation">
                                        <span style="color: #fff; font-size: 14px;">Basic Information</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnEAS" OnClick="btnbtnEAS_Click">
                                        <span style="color: #fff; font-size: 14px;">Excise Articles Seized</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnOtherExisable" OnClick="btnOtherExisable_Click">
                                        <span style="color: #fff; font-size: 14px;">Other Exisable Articles</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnAccusedDetails" OnClick="btnAccusedDetails_Click">
                                        <span style="color: #fff; font-size: 14px;">Accused Details</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnRaidTeam" OnClick="btnRaidTeam_Click">
                                        <span style="color: #fff; font-size: 14px;">Raid Team  </span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                </div>--%>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Basic Information</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                    <%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Date of Raid </label>
                                                <br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDate" Format="dd-MM-yyyy" ID="CalendarExtender" OnClientDateSelectionChanged="Selectdate"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDATE"   data-toggle="tooltip" data-placement="right" title="Date of Raid " class="form-control validate[required]" ReadOnly="false" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="txtdob" runat="server" />
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Raid Time </label>
                                                <br />
                                                <input id="raidtime" type="time" runat="server" class="form-control" data-toggle="tooltip" data-placement="right" title="Raid Time" />
                                            </div>
                                              <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Raid Location </label>
                                                <br />
                                                <asp:TextBox ID="txtRaidLocation" AutoComplete="off" Width="90%" CssClass="form-control" TextMode="MultiLine" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" MaxLength="100" title="Raid Location"></asp:TextBox>
                                            </div>
                                           
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red">*</span>Thana </label>
                                                <br />
                                                <asp:DropDownList ID="ddlThana"  runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Thana"></asp:DropDownList>
                                            </div>

                                              <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red">*</span>Recovery Based on Complaint </label>
                                                <br />
                                                <asp:RadioButton ID="rdPersonally" runat="server" GroupName="radio" AutoPostBack="true" OnCheckedChanged="rdPersonally_OncheckedChange" />Personally Detected  &nbsp; &nbsp;
                                        <asp:RadioButton ID="rdInformationFromControl" GroupName="radio" AutoPostBack="true" runat="server" OnCheckedChanged="rdInformationFromControl_OncheckedChange" />Information From Control Room Complaint  &nbsp; &nbsp;
                                        <asp:RadioButton ID="rdInformationFromOther" GroupName="radio" AutoPostBack="true" runat="server" OnCheckedChanged="rdInformationFromOther_OncheckedChange" />Information From Other Sources &nbsp; &nbsp;
                                     
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                                <span style="color: red">*</span>
                                                <asp:Label ID="lblname" CssClass="control-label" Font-Size="Small" Font-Bold="true" runat="server" Text=""></asp:Label><br />
                                                <asp:TextBox ID="txtName" AutoComplete="off" CssClass="form-control" MaxLength="50" runat="server" Width="36%" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" title="Name"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                           <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display: inline"><span style="color: red"></span>Station Diary Entry No</label>
                                                <br />
                                                <asp:TextBox ID="txtManualBookSeizure" AutoComplete="off" CssClass="form-control" MaxLength="20" runat="server" data-toggle="tooltip" data-placement="right" title="Station Diary Entry No"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Latitude </label>
                                                <br />
                                                <asp:TextBox ID="txtLatitude" AutoComplete="off" CssClass="form-control" MaxLength="20" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" title="Latitude"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span>Longitude </label>
                                                <br />
                                                <asp:TextBox ID="txtLongitude" AutoComplete="off" CssClass="form-control" MaxLength="20" runat="server" data-toggle="tooltip" data-placement="right" onkeypress="return blockSpecialChar(event)" title="Longitude"></asp:TextBox>
                                            </div>

                                            
                                             <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                                <div class="col-md-6 col-sm-12 col-xs-12 ">
                                                   <label class="control-label" style="display: inline; font-size:small"><span style="color: red"></span>Remarks</label>
                                                <asp:TextBox ID="txtRemarks" AutoComplete="off" CssClass="form-control" MaxLength="250" runat="server" Width="90%" data-toggle="tooltip" data-placement="right" title="Remarks" onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
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
                                  <%--  <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                           <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                             <label class="control-label"><span style="color: red"></span>Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Description"></asp:TextBox>--%>
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" OnClick="DownloadFile"  CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' OnClick="btnRemove_Click" CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
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
                                            
                                        
                                    </div>


                                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                        <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click"> <span aria-hidden="true" class="fa fa-plus-circle">*</span>Save as Draft</asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click"> <span aria-hidden="true" ></span>Submit</asp:LinkButton>--%>
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
