<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComplaintsStatusForm.aspx.cs" Inherits="UserMgmt.ComplaintsStatusForm" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Complaint Status Form</title>

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
                                 <script language="javascript" type="text/javascript">
                                    
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
                                        if (document.getElementById('<%=txtComplaintno.ClientID%>').value == '') {
                                            alert("Enter Complaint Number");
                                            document.getElementById("<% =txtComplaintno.ClientID%>").focus();
                                            return false;

                                        }
                                        if (document.getElementById('<%=ddlcomplaintype.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter Complain Status");
                                            document.getElementById("<% =ddlcomplaintype.ClientID%>").focus();
                                            return false;

                                        }
                                    
                                    }
                                    function validationMsg1() {
                                        if (document.getElementById('<%=txtComplaintno.ClientID%>').value == '') {
                                            alert("Enter Complaint Number");
                                            document.getElementById("<% =txtComplaintno.ClientID%>").focus();
                                            return false;

                                        }
                                    }
                                </script>
     <script>
                                    function chkDuplicateState() {
                                        debugger;
                                        var User_id = $('#BodyContent_txtComplaintno').val();
                                        var jsondata = JSON.stringify($('#BodyContent_txtComplaintno').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "ComplaintsStatusForm.aspx/chkDuplicateState",
                                            data: '{statename:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) <= 0) {
                                                    alert("Complaint number is not exists");
                                                    $('#BodyContent_txtComplaintno').val('');
                                                    $('#BodyContent_txtComplaintno').focus();
                                                }

                                            }
                                        });
                                    }
           </script>
     
   
</head>
<body>
    <form runat="server">
        <div class="container-fluid">
                  <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
            <div class="row content">
                <h3 style="margin-left: 0%" class="control-label">Complaint Status Form</h3>
                
                <div style="height: 0.1%; background-color: #26b8b8;">
                </div>
                 
                <div class="col-sm-9">
                  
                    <div class="clearfix"></div>
                    
                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Complaint Number</label>
                        <br />
                        <asp:TextBox ID="txtComplaintno" runat="server"  autocomplete="off" data-toggle="tooltip" data-placement="right" title="Complaint Number" MaxLength="10" AutoPostBack="true" OnTextChanged="txtcomplaint_no"  class="form-control validate[custom[phone],required]" onchange="chkDuplicateState();"  onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                          <span>
                                                <asp:Button ID="btncmn" runat="server" CssClass="btn btn-upload" OnClientClick="javascript:return validationMsg1()"  Text="Submit" OnClick="btncomplaint_no" />
                                                       
                                            </span>
                    </div>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>

                       <div class="col-md-10 col-sm-12 col-xs-12 form-inline">
                                       
                                        <asp:GridView ID="grdAdd" runat="server" AutoGenerateColumns="false"
                                            HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                           <Columns>
                                            <asp:TemplateField HeaderText="Complainant Name" ItemStyle-Font-Bold="true"  ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplainantName" runat="server"  Text='<%#Eval("complainant_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Complaint Details" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplaintDetails" runat="server" Visible="true" Text='<%#Eval("complaint_details") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile Number" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobileNumber" runat="server" Visible="true" Text='<%#Eval("mobile_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="complaint_id" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentId" runat="server" Visible="true" Text='<%#Eval("complaint_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? Eval("allotment_status").ToString() :Eval("record_status").ToString()=="R"? Eval("allotment_status").ToString():(Eval("record_status").ToString()=="Y"?Eval("allotment_status").ToString()=="N"?"Pending":Eval("allotment_status").ToString() :Eval("record_status").ToString()=="I"? "Alloted" : Eval("record_status").ToString()=="B"? Eval("allotment_status").ToString(): "Draft") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>--%>
                                         <%--   <asp:TemplateField HeaderText="Approval Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovalstatus" runat="server" Text='<%# Eval("allotment_status").ToString()== "N" ? "Pending":Eval("allotment_status").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" CommandName="View" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" Visible="false" CommandName="Edit" OnClick="btnEdit_Click"><i class="fa fa-pencil-square-o"> 
                                                    </i> 
                                                    </asp:LinkButton>
                                                     <asp:LinkButton Text="Issue" ID="btnEssue" CssClass="myButton2" runat="server" Visible=' false' CommandName="Issue" >I<i > 
                                                    </i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton Text="Print" ID="btnprint" CssClass="myButton11" runat="server" Visible='false'  CommandName="Print" ><i class="fa fa-print"> 
                                                                                    </i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                            
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                          
                                        </asp:GridView>

                                                
                                    </div>
                    <div id="CMPS" runat="server" visible="false">
                <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label"><span style="color: red">*</span>Complaint Status </label>
                                        <br />
                                        <asp:DropDownList ID="ddlcomplaintype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Complaint Type" AutoPostBack="true">
                                            <asp:ListItem Value="SA">Satisfied</asp:ListItem>
                                              <asp:ListItem Value="NS">Not Satisfied</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>

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
        </div>
    </form>
</body>
</html>
