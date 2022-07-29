<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComplaintGrievance.aspx.cs" Inherits="UserMgmt.ComplaintGrievance" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Complaint/ Grievance Redressal Form</title>

 <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style>
        /* Set height of the grid so .sidenav can be 100% (adjust if needed) */
        .row.content {
            height: 1500px;
        }

        /* Set gray background color and 100% height */
        .sidenav {
            background-color: #f1f1f1;
            height: 100%;
        }

        /* Set black background color, white text and some padding */
        footer {
            background-color: #555;
            color: white;
            padding: 15px;
        }

        /* On small screens, set height to 'auto' for sidenav and grid */
        @media screen and (max-width: 767px) {
            .sidenav {
                height: auto;
                padding: 15px;
            }

            .row.content {
                height: auto;
            }
        }

        .accordion .card-header:after {
            font-family: 'FontAwesome';
            content: "\f068";
            float: right;
        }

        .accordion .card-header.collapsed:after {
            /* symbol for "collapsed" panels */
            content: "\f067";
        }
    </style>
    <script>
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

   <script>
                                    function onlyDotsAndNumbers(txt, event) {
                                        debugger;
                                        var charCode = (event.which) ? event.which : event.keyCode
                                        if (charCode == 46) {
                                            if (txt.value.indexOf(".") < 0)
                                                return false;
                                            else
                                                return false;
                                        }
                                        //if (txt.value.indexOf(".") > 0) {
                                        //    var txtlen = txt.value.length;
                                        //    var dotpos = txt.value.indexOf(".");
                                        //    //Change the number here to allow more decimal points than 2
                                        //    if ((txtlen - dotpos) > 2)
                                        //        return false;
                                        //}
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
                                        if (document.getElementById("<%=txtemail.ClientID%>").value != "")
         {
            var filter = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!filter.test(document.getElementById("<%=txtemail.ClientID%>").value)) 
      {
        alert('Please Provide A Valid Email Id');
       document.getElementById("<%=txtemail.ClientID%>").value="";
       document.getElementById("<%=txtemail.ClientID%>").focus();
       return false;
                            }
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
                                      
                                        CheckIsRepeat();
                                    }
                                </script>
     <script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            debugger;
            if (++submit > 1) {
                //alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
    </script>
   <%-- <script language="javascript" type="text/javascript">
  function chkDuplicateState() {
                                        debugger;
                                        var User_id = $('#BodyContent_btnUpload').val();
                                        var jsondata = JSON.stringify($('#BodyContent_btnUpload').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "ComplaintGrievance.aspx/chkDuplicateState",
                                            data: '{statename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    alert("State Name is already exists");
                                                    $('#BodyContent_btnUpload').focus();
                                                }

                                            }
                                        });
  }
         </script>--%>
</head>
<body>
    <form runat="server">
        <div class="container-fluid">
                  <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
            <div class="row content">
                <h3 style="margin-left: 0%" class="control-label">Complaint/ Grievance Redressal Form</h3>
                <%--<asp:LinkButton ID="btnLogOut" Text="Log Out" CssClass="btn btn-danger right"  runat="server" OnClick="btnLogOut_Click"></asp:LinkButton>--%>
                <div style="height: 0.1%; background-color: #26b8b8;">
                </div>
                 
                <div class="col-sm-9">
                  
                    <div class="clearfix"></div>
                    <%--<h3 style="margin-left: 30%" class="control-label">Help Desk Tickets</h3>--%>

                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Complainant Name</label>
                        <br />
                        <asp:TextBox ID="txtcomplainantname" runat="server" CssClass="form-control" autocomplete="off" Width="250px" data-toggle="tooltip" data-placement="right" title="Complainant Name" ></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Contact Number</label>
                        <br />
                        <asp:TextBox ID="txtmobile" runat="server"  autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" MaxLength="10"  class="form-control validate[custom[phone],required]" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span> Email ID</label>
                        <br />
                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email ID" onchange="emailValidate(this);" ></asp:TextBox>
                    </div>
                      <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Complaint Type  </label>
                                        <br />
                                        <asp:DropDownList ID="ddlcomplaintype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Complaint Type" AutoPostBack="true">
                                            <asp:ListItem Value="LS">Liquior Sale</asp:ListItem>
                                              <asp:ListItem Value="CO">Complaint Against Officer</asp:ListItem>
                                            <asp:ListItem Value="CM">Consumption</asp:ListItem>
                                             <asp:ListItem Value="SA">Sale</asp:ListItem>
                                              <asp:ListItem Value="HD">Home Delivery</asp:ListItem>
                                             <asp:ListItem Value="MF">Manufacturing</asp:ListItem>
                                              <asp:ListItem Value="ST">Storage</asp:ListItem>
                                           <%-- <asp:ListItem Value="SG">Suggestion</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <%--<asp:TextBox ID="txtticketQuery" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" cols="20" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <h4 class="control-label">Comment:</h4>

                    <div class="form-group">
                        <asp:TextBox ID="txtComment" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" placeholder="Type Your Message" cols="20" Rows="7" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <asp:FileUpload ID="idupDocument" runat="server" onchange="validateExtraDocuments();" />--%>
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
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true"  Width="60%" CssClass="form-control" data-toggle="tooltip" data-placement="right" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" title="State"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>District</label><br />
                                           <asp:TextBox ID="txtdistrict" autocomplete="off" CssClass="form-control" Width="60%"  Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="District"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Width="60%"  data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="District" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Thana</label><br />
                                           <asp:TextBox ID="txtthana" autocomplete="off" CssClass="form-control" Width="60%" Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="Thana"></asp:TextBox>
                                        <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" Width="60%" data-toggle="tooltip" data-placement="right" AutoPostBack="true" title="Thana"></asp:DropDownList>
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

                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                
                                     <div id="docs" runat="server">
                                         
                                       
               <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color: red"></span>Documents</label><br />
                                            <asp:FileUpload ID="idupDocument" CssClass="form-control" onchange="validateExtraDocuments();" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="Document Description" ></asp:TextBox>
                                            <span>  <asp:Button ID="btnUpload" runat="server"  OnClientClick="javascript:return CheckDiscription()"   Text="Upload" OnClick="UploadFile" />  </span>
                                               </div>    
                

                                              
                                            
                                               <%-- <input type="button" class="btn btn-upload" OnClientClick="javascript:return CheckDiscription()" Text="Upload" onclick="UploadFile" />--%>
                                                   <%--<input id='buttonid' type='button' value='Upload MB' runat="server" onserverClick="UploadFile" />--%> 
                                         
                                        </div>
                                    
                                    <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                    <div id="dummytable" visible="false" runat="server" style="height: auto; width: 75%; border: 1px solid gray; margin-left: 10px; margin-right: 10px; background: #f5f6f7;">
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

                                                  <%--  <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnUpload" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                    </div>
                     <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                    <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" Visible="false" OnClick="btnSendOTP_Click"></asp:Button>

                      <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>

                   <%-- <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>--%>
                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" Visible="false" runat="server" class="btn btn-info pull-left" OnClick="btnSaveasDraft_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" CssClass="btn btn-primary" OnClick="btnSubmit_Click" >
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Visible="true"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click" >Cancel
                                            <span aria-hidden="true" > </span></asp:LinkButton>
                                        </div>

                </div>
               
            </div>
        </div>
    </form>
</body>
</html>
