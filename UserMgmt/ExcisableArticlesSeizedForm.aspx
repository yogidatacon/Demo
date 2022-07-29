<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="ExcisableArticlesSeizedForm.aspx.cs" Inherits="UserMgmt.ExcisableArticlesSeizedForm" %>
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
                                <title>Excisable Articles Seized Form</title>
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
                                </script>
                                <script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=grdExcisableArticlesView] input[type=checkbox]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=grdExcisableArticlesView] input[type=checkbox]").removeAttr("checked");
                $(this).attr("checked", "checked");
            }
        });
    });
</script>
                                <script language="javascript" type="text/javascript">
                                    function validationMsg() {

                                        
                                        if (document.getElementById('<%=ddlArticleCategory.ClientID%>').value == 'Select') {
                                            alert("Select ArticleCategory");
                                            document.getElementById("<% =ddlArticleCategory.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlArticleSubCategory.ClientID%>').value == 'Select') {
                                            alert("Select ArticleSubCategory");
                                            document.getElementById("<% =ddlArticleSubCategory.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=ddlarticlename.ClientID%>').value == 'Select') {
                                             alert("Select Article Name");
                                            document.getElementById("<% =ddlarticlename.ClientID%>").focus();
                                            return false;
                                        }
                                        <%--if (document.getElementById('<%=txtArticleName.ClientID%>').value == '') {
                                            alert("Enter ArticleName");
                                            document.getElementById("<% =txtArticleName.ClientID%>").focus();
                                            return false;

                                        }

                                     if (document.getElementById('<%=txtManufacturer.ClientID%>').value < "1") {
                                            debugger;
                                            alert("Enter Manufacturer");
                                            document.getElementById("<% =txtManufacturer.ClientID%>").focus();
                                            return false;
                                        }
                                        else
                                         {
                                            var ref_code = $("#BodyContent_NestedBodyContent_txtManufacturer").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code < 3)
                                             {
                                                 alert("Manufacturer should be minimum 3 character");
                                                 document.getElementById("<% =txtManufacturer.ClientID%>").focus();
                                                 return false;
                                             }
                                         }--%>

                                        if (document.getElementById('<%=ddlUnitOfMeasurement.ClientID%>').value == 'Select') {
                                            alert("Select Unit Of Measurement");
                                            document.getElementById("<% =ddlUnitOfMeasurement.ClientID%>").focus();
                                            return false;
                                        }

                                        if (document.getElementById('<%=txtQuantity.ClientID%>').value == '') {
                                            alert("Enter Quantity");
                                            document.getElementById("<% =txtQuantity.ClientID%>").focus();
                                            return false;
                                        }
                                       

                                        <%-- if (document.getElementById('<%=txtRemarks.ClientID%>').value == '') {
                                            alert("Enter Remarks");
                                            document.getElementById("<% =txtRemarks.ClientID%>").focus();
                                            return false;
                                         }
                                         else
                                         {
                                             var ref_code = $("#BodyContent_NestedBodyContent_txtRemarks").val().length;
                                             //const textbox = document.getElementById("txtRaidLocation");
                                             if (ref_code <5)
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
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click" Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Excisable Articles Seized Form</h2>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">

                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                           <div id ="serchid" runat="server">
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" ><span style="color: red"></span>Article Name </label><br />
                                                <asp:TextBox ID="txtArticle" autocomplete="off"  runat="server" Width="90%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label"><span style="color: red"></span></label><br />
                                                  <asp:Button ID="btnSearch" runat="server" Width="60%"  Text="Article Search" CssClass="btn btn primary" OnClick="btnSearch_Click" />
                                            </div>
                                                  
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-6 col-sm-12 col-xs-12 form-inline">
                                        
                                        
                                            <asp:GridView ID="grdExcisableArticlesView" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdExcisableArticlesView_PageIndexChanging"
                                                HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Edit Gif to edit the record" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEditGif" runat="server" Visible="false" Text='<%#Eval("seizure_excisable_articles_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article Category" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArticleCategory" runat="server" Visible="true" Text='<%#Eval("article_category_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article Sub Category" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArticleSubCategory" runat="server" Visible="true" Text='<%#Eval("article_sub_category_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article Code" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArticleNameCode" runat="server" Visible="true" Text='<%#Eval("article_name_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article Name" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArticleName" runat="server" Visible="true" Text='<%#Eval("article_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Visible="true" Text='<%#Eval("quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit of Measurement" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitofMeasurement" runat="server" Visible="false" Text='<%#Eval("uom_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                             <asp:CheckBox ID="chselect" runat="server" OnCheckedChanged="chselect_CheckedChanged"  AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                            </asp:GridView>

                                            </div>
                                       
                                   
                                       <div class="x_title"></div>
                                                </div>
                                    <div style="height: 0.8%; background-color: #26b8b8;">
                                        <span style="font-size: x-small; color: white; margin-left: 1%"></span>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>                                

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Article Category </label>
                                        <br />
                                        <asp:DropDownList ID="ddlArticleCategory" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Article Category"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Article Sub-Category </label>
                                        <br />
                                        <asp:DropDownList ID="ddlArticleSubCategory" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Article Sub-Category "></asp:DropDownList>
                                    </div>

                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Article Name </label>
                                        <br />
                                 <asp:DropDownList ID="ddlarticlename" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Article Name "></asp:DropDownList>
                                        <%--<asp:TextBox ID="txtArticleName" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Article Name"></asp:TextBox>--%>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Manufacturer </label>
                                        <br />
                                        <asp:TextBox ID="txtManufacturer" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Manufacturer"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                      <label class="control-label" style="display:inline"><span style="color: red"></span>Manufacturing Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtManufacturingDate" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtManufacturingDate" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Bail Request Date" class="form-control validate[required]" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Batch No </label>
                                        <br />
                                        <asp:TextBox ID="txtBatch" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip"  data-placement="right" MaxLength="20" title="Batch No"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Prod State Code </label>
                                        <br />
                                        <asp:TextBox ID="txtProdStateCode" autocomplete="off"  CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" MaxLength="2" title="Prod State Code "></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Sale State Code</label>
                                        <br />
                                        <asp:TextBox ID="txtSaleStateCode" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" MaxLength="2" title="Sale State Code"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red">*</span>Unit Of Measurement </label>
                                        <br />
                                        <asp:DropDownList ID="ddlUnitOfMeasurement" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Unit Of Measurement"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Quantity </label>
                                        <br />
                                        <asp:TextBox ID="txtQuantity" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Quantity"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Packing Size </label>
                                        <br />
                                        <asp:TextBox ID="txtPackingSize" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" onkeypress="return onlyDotsAndNumbers(this,event);" data-placement="right" title="Packing Size"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Bottle No</label>
                                        <br />
                                        <asp:TextBox ID="txtAreaSize" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip"  data-placement="right" title="Bottle No"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red"></span>Different Liquor</label>
                                        <br />
                                        <asp:TextBox ID="txtDifferentLiquor" autocomplete="off" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" title="Different Liquor"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12">
                                        <label class="control-label" style="display: inline; font-size: small"><span style="color: red"></span>Remarks </label>
                                        <br />
                                        <asp:TextBox ID="txtRemarks" autocomplete="off" CssClass="form-control" runat="server" Height="10%" data-toggle="tooltip" data-placement="right" title="Remarks" TextMode="MultiLine"></asp:TextBox>
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
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click"> <span aria-hidden="true" class="fa fa-plus-circle">*</span>Save as Draft</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click"> <span aria-hidden="true" ></span>Submit</asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="btnApprove" AutoPostback="false" CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" Style="width: 85px;" Text="Approve" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" />--%>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel <span aria-hidden="true" ></span></asp:LinkButton>
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
