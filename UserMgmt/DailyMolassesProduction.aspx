<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DailyMolassesProduction.aspx.cs" Inherits="UserMgmt.SCMDailyMolassesProduction" %>

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
                                <script src="common/theme/js/flot/date.js"></script> 
                                <title>Daily Molasses Production</title>
                                <script  type="text/javascript">
                                    function validationMsg() {
                                        if (document.getElementById('<%=ddlpartyname.ClientID%>').value == 'Select') {
                                            alert("Select  Party Name");
                                            return false;
                                            document.getElementById("<% =ddlpartyname.ClientID%>").focus();
                                        }

                                        if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                            alert("Enter  Date");
                                            document.getElementById("<%=txtDATE.ClientID%>").focus();
                                            return false;
                                        }
                                         if (document.getElementById('<%=txtRemarks1.ClientID%>').value == '') {
                                            alert("Enter  Remarks");
                                            document.getElementById("<%=txtRemarks1.ClientID%>").focus();
                                            return false;
                                         }
                                        debugger;
                                      
                                      if (document.getElementById('<%=vattotal.ClientID%>').value=='' || document.getElementById('<%=vattotal.ClientID%>').value=="0"||document.getElementById('<%=vattotal.ClientID%>').value=="0.0")
                                        {
                                           alert("Enter atleast one vat details");
                                           $('#BodyContent_grdDailyMolassesProduction_txtTodaysProd_0').focus();
                                           return false;
                                       }
                                       
                                        
                                    }
                                </script>
                                <script type="text/javascript">
                                    function validationMsg1() {
                                        if (document.getElementById('<%= txtapproverremarks.ClientID%>').value == '')
                                        {
                                            alert("Enter  Approval Comments ");
                                            return false;
                                            document.getElementById("<% =txtapproverremarks.ClientID%>").focus();
                                        }
                                    }
                                </script>
                                <script>
                                    $(document).ready(function () {
                                      
                                        if ($('#BodyContent_textTotal').val() == "") {
                                            $('#BodyContent_textTotal').val($('#BodyContent_txtTotal').val());
                                        }
                                        if ($('#BodyContent_txtDATE').val() == "")
                                        {
                                            $('#BodyContent_txtDATE').val($('#BodyContent_txtdob1').val());
                                            $('#BodyContent_txtdob').val($('#BodyContent_txtdob1').val());
                                        }
                                       
                                    });
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
                                    function GetTotal() {
                                      
                                        var pfg = $('#BodyContent_textpfg').val();
                                        var pfs = $('#BodyContent_textpfs').val();
                                        var foe = $('#BodyContent_textFOE').val();
                                        var total = $('#BodyContent_textTotal').val();
                                        if (pfg == "")
                                            pfg = 0;
                                        if (pfs == "")
                                            pfs = 0;
                                        if (foe == "")
                                            foe = 0;
                                        if (total == "")
                                            total = 0;
                                        var result = parseInt(pfg) + parseInt(pfs) + parseInt(foe);
                                        //  $('#texttotal').val(result);
                                        $('#BodyContent_textTotal').val(result);
                                        $('#BodyContent_txtTotal').val(result);
                                       
                                    }
                                    function CheckWithTotal() {

                                        var total = $('#BodyContent_textTotal').val();
                                        var crushed = $('#BodyContent_textcanecrushed').val();
                                        if (parseInt(total) < parseInt(crushed)) {
                                            alert("Cane Crushed Total should be less than or eqaul to Total!!!!!")
                                            $('#BodyContent_textcanecrushed').val("");
                                            $('#BodyContent_textcanecrushed').focus();
                                            return false;
                                        }

                                    }
                                    function onlyDotsAndNumbers(txt, event) {
                                       
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
                                    function CheckDiscription() {
                                      
                                        if (document.getElementById('<%=idupDocument.ClientID%>').value == '') {
                                            alert("Please Attach file");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=txtDiscription.ClientID%>').value == '') {
                                            alert("Enter Document Name");
                                            document.getElementById("<% =txtDiscription.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
                                <script >
                                    function CheckValues(obj) {
                                    
                                      //  var id = document.getElementById(obj.id);
                                        var row = obj.parentNode.parentNode;
                                        var n=(row.rowIndex)-1;
                                        var today = $('#BodyContent_grdDailyMolassesProduction_txtTodaysProd_' + n).val();
                                        var tot = $('#BodyContent_grdDailyMolassesProduction_txtAvailable_' + n).val();
                                        debugger;
                                        var availble = parseFloat(tot) + parseFloat(today);
                                        var total = $('#BodyContent_grdDailyMolassesProduction_txttotalcapacity_' + n).val();
                                        
                                        $('#BodyContent_vattotal').val(today);
                                        //vattotal
                                        $('#BodyContent_val1').val("1");
                                        if (parseFloat(total) < parseFloat(availble)) {
                                            alert("Production cannot be greater than VAT storage capacity");
                                            $('#BodyContent_grdDailyMolassesProduction_txtTodaysProd_' + n).val("");
                                            $('#BodyContent_grdDailyMolassesProduction_txtTodaysProd_' + n).focus();
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                     function Calcutate() {

                                        var total = 0;
                                      
                                        var gv = document.getElementById("<%= grdDailyMolassesProduction.ClientID %>");
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
                                              
                                                //var NetWeight = parseFloat($('#BodyContent_NetWeight').val()).toFixed(2);
                                                //if (NetWeight < total)
                                                //{
                                                //    total -= parseFloat(sub);
                                                //    alert("Qty not Matched with Net Weight");
                                                //    $('#BodyContent_grdRawMaterial_lblTotal').text(total);
                                                //    tb[i].value = "";
                                                //    tb[i].focus();
                                                //    return false;
                                                //}
                                                //i++;
                                            }
                                        }
                                        $('#BodyContent_vattotal').val(total);
                                       
                                       
                                    }
                                </script>
                                <script>

                                    function chkDuplicateDates(date1) {
                                        debugger;
                                        var todayDate =date1;
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
                                       
                                        $('#BodyContent_txtdob1').val(todayDate);

                                      //  var etrydate = $('#BodyContent_txtdob1').val();
                                     
                                        var party_code = $('#BodyContent_ddlpartyname').val();
                                        var jsondata = JSON.stringify(todayDate + "_" + $('#BodyContent_ddlpartyname').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DailyMolassesProduction.aspx/chkDuplicateDates",
                                            data: '{scpdate:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {
                                                if (parseInt(msg.d) > 0) {
                                                    alert("Production Date is already exists");
                                                    $('#BodyContent_txtDATE').val($('#BodyContent_txtdob').val());
                                                    $('#BodyContent_txtDATE').focus();
                                                    $('#BodyContent_txtdob').val($('#BodyContent_txtdob').val());
                                                    $('#BodyContent_txtdob1').val($('#BodyContent_txtdob').val());
                                                  
                                                }
                                               

                                            }
                                        });
                                    }
                                    
                                </script>
                                <script>
                                 function SelectDate(e) {
                                     debugger;
                                     chkDuplicateDates(e.get_selectedDate())
                                       
                                       
                                 }
                                     </script>

                            </head>
                            <body>
                               
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnRG4" OnClick="btnRG4_Click">
                                        <span style="color: #fff; font-size: 14px;">SugarCane Purchase Form R.G-4</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton runat="server" ID="btnDMP" OnClick="btnDMP_Click">
                                        <span style="color: #fff; font-size: 14px;">Daily Molasses Production</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnMIR" OnClick="btnMIR_Click">
                                        <span style="color: #fff; font-size: 14px;">Molasses Issue Register</span></asp:LinkButton></li>
                                            <li>
                                            <asp:LinkButton runat="server" ID="btnVATtansfers" OnClick="btnVATtansfers_Click" Text="Seizure List">
                                        <span style="color: #fff; font-size: 14px;">VAT Transfer Form</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnOpeningBalance" OnClick="btnOpeningBalance_Click">
                                        <span style="color: #fff; font-size: 14px;">Opening Balance</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                   </div>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="btnShowRecords" OnClick="ShowRecords_Click" Style="float: right" Text="ShowRecords"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Daily Molasses Production</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div style="float: right">
                                </div>
                                <div class="x_content">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:HiddenField ID="val1" runat="server" />
                                    <asp:HiddenField ID="vattotal" runat="server" />
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Party Name</label><br />
                                        <asp:DropDownList ID="ddlpartyname" Width="90%"  CssClass="form-control"  data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="txtDate" title="party name" runat="server" AutoPostBack="true" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Date</label><br />
                                        <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDATE" Format="dd-MM-yyyy" OnClientDateSelectionChanged="SelectDate" ID="CalendarExtender"></cc1:CalendarExtender>
                                        <asp:TextBox ID="txtDATE" onchange="chkDuplicateDates()" data-toggle="tooltip" ReadOnly="true"  data-placement="right"  title="Date" Cssclass="form-control" runat="server" Font-Size="14px">
                                         </asp:TextBox>
                                        <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                        <asp:HiddenField ID="txtdob"  runat="server" />
                                         <asp:HiddenField ID="txtdob1"  runat="server" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                        <asp:GridView ID="grdDailyMolassesProduction" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"  EmptyDataText="No Records" 
                                                       HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                            <Columns>
                                                <asp:TemplateField HeaderText=" VAT Code"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvatcode" runat="server" Text='<%#Eval("vat_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" VAT Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvatname" runat="server" Text='<%#Eval("vat_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Storage Content" ItemStyle-Font-Bold="true" ItemStyle-Width="20%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstorage_content" runat="server" Text='<%#Eval("storage_content") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOMCode" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="1%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluom_code" runat="server" Text='<%#Eval("uom_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Font-Bold="true" ItemStyle-Width="10%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluom_name" runat="server" Text='<%#Eval("uom_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                               
                                                    <ItemStyle Font-Bold="True"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Capacity" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                      
                                                        <asp:TextBox ID="txttotalcapacity" CssClass="form-control" runat="server" Text='<%#Eval("vat_totalcapacity") %>' ReadOnly="true">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Font-Bold="True" ></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Available QTY" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAvailable"  CssClass="form-control" runat="server" Text='<%#Eval("vat_availablecapacity") %>' ReadOnly="true" >
                                                          
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Today's Production" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTodaysProd"  CssClass="form-control" class="calculate" runat="server" AutoPostBack="true" Text='<%#Eval("dailyproduction") %>' onchange="return CheckValues(this);Calcutate();"
                                                             onkeypress="return onlyDotsAndNumbers(this,event);">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                     
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brix" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTodaysBrix" onkeypress="return onlyDotsAndNumbers(this,event);"  CssClass="form-control" Text='<%#Eval("brix") %>' runat="server" MaxLength="50">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="record_status" Visible="false" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblrecord_status" CssClass="form-control" Text='<%#Eval("record_status") %>' runat="server" MaxLength="50">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="dailymolassesproduction_id" Visible="false" ItemStyle-Font-Bold="true" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldailymolassesproduction_id" runat="server" Text='<%#Eval("dailymolassesproduction_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>

                                        </asp:GridView>
                                   
                                    <br />
                                    <div class="clearfix"></div>
                                    <div id="docs" runat="server">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Document Name</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Document Name"></asp:TextBox>
                                            <span>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-upload" OnClick="UploadFile" OnClientClick="javascript:return CheckDiscription()" Text="Upload" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
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
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false" AllowPaging="true"
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
                                                <asp:TemplateField HeaderText="Discription" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscriptione" runat="server" Visible="true" Text='<%#Eval("Description") %>'></asp:Label>
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" OnClick="DownloadFile" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" OnClick="btnRemove_Click" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server" />
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
                                       <div class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Remarks</label><br />
                                        <asp:TextBox TextMode="MultiLine" id="txtRemarks1" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                        </div>
                                    <div class="clearfix"></div>
                                            <p>&nbsp;</p>
                                             <div id ="approverremarks" runat="server" class="col-md-11 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small"><span style="color: red">*</span>Approver Comments</label><br />
                                        <asp:TextBox TextMode="MultiLine" id="txtapproverremarks" data-toggle="tooltip" data-placement="right" title="Remarks" height="50px" width="90%" runat="server" class="form-control" name="size"></asp:TextBox>
                                        </div>
                                 <div class="clearfix"></div> 
                                     <p>&nbsp;</p>
                                    <div class="clearfix"> </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                           <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> </span>Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton id="btnApprove" AutoPostback = false CssClass="btn btn-primary" OnClientClick="javascript:return validationMsg1()" runat="server" style="width:85px;"  Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:LinkButton ID="btnReject" Text="Reject" runat="server" CssClass="btn btn-danger right" OnClientClick="javascript:return validationMsg1()" class="fa fa-cut" OnClick="btnReject_Click" />
                                        <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                            CssClass="btn btn-danger" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>
                                    <p>&nbsp;</p>
                                            <div class="x_title">
                                                <h4>Approval Summary</h4>
                                                <div class="clearfix"></div>
                                            </div>
                                        <div class="x_title">
                                        <asp:GridView ID="grdApprovalDetails" runat="server" class="table table-striped responsive-utilities jambo_table"
                                                    HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                                    HeaderStyle-ForeColor="#ECF0F1" AutoGenerateColumns="false" EmptyDataText="No Approvals" Width="1218px">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Transaction Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Transaction_Date") %>'></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approver Role" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approver Comments" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblApproverComments" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Transaction_state") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Documents_id") %>' ForeColor="Black" runat="server" OnClick="DeleteFiles" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                             <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                        </asp:GridView>
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
