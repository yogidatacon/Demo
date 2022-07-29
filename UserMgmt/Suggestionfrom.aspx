<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suggestionfrom.aspx.cs" Inherits="UserMgmt.Suggestionfrom" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Suggestion Form</title>

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
                                            alert("Enter Name");
                                            document.getElementById("<% =txtcomplainantname.ClientID%>").focus();
                                            return false;

                                        }
                                         if (document.getElementById('<%=txtmobile.ClientID%>').value == '') {
                                             alert("Enter Contact Number");
                                            document.getElementById("<% =txtmobile.ClientID%>").focus();
                                            return false;

                                         }
                                       <%--  if (document.getElementById('<%=txtemail.ClientID%>').value == '') {
                                            alert("Enter Email");
                                            document.getElementById("<% =txtemail.ClientID%>").focus();
                                            return false;

                                        }

                                        if (document.getElementById('<%=ddlcomplaintype.ClientID%>').value=='') {
                                            debugger;
                                            alert("Enter Complain Type");
                                            document.getElementById("<% =ddlcomplaintype.ClientID%>").focus();
                                            return false;

                                        }--%>

                                         if (document.getElementById('<%=txtAddress.ClientID%>').value == '') {
                                            alert("Enter Suggestion");
                                            document.getElementById("<% =txtAddress.ClientID%>").focus();
                                            return false;

                                        }

                                    
                                    
                                    }
                                </script>
     
   
</head>
<body>
    <form runat="server">
        <div class="container-fluid">
                  <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
            <div class="row content">
                <h3 style="margin-left: 0%" class="control-label">Suggestion Form</h3>
                
                <div style="height: 0.1%; background-color: #26b8b8;">
                </div>
                 
                <div class="col-sm-9">
                  
                    <div class="clearfix"></div>
                    
                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span> Name</label>
                        <br />
                        <asp:TextBox ID="txtcomplainantname" runat="server" CssClass="form-control" autocomplete="off" Width="250px" data-toggle="tooltip" data-placement="right" title="Complainant Name" ></asp:TextBox>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Contact Number</label>
                        <br />
                        <asp:TextBox ID="txtmobile" runat="server"  autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" MaxLength="10"  class="form-control validate[custom[phone],required]" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
                    </div>
                   <%-- <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span> Email ID</label>
                        <br />
                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email ID" onchange="emailValidate(this);" ></asp:TextBox>
                    </div>--%>
                      <%--<div class="col-md-3 col-sm-12 col-xs-12 form-inline">
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
                                           <%-- <asp:ListItem Value="SG">Suggestion</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                       <div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  id="approverremaks" runat="server" class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red">*</span>Suggestion</label><br />
                                  <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" data-toggle="tooltip"  height="5%" width="95%" data-placement="right" title="Address" TextMode="MultiLine"></asp:TextBox>
                                                 
                                    </div>

                                    <%--<div class="clearfix"></div>
                                    <p>&nbsp;</p>
                                      <div  id="Div1" runat="server" class="col-md-12 col-sm-12 col-xs-12 ">
                                        <label class="control-label" style="font-size: small;display:inline"><span style="color: red">*</span>Complaint Details</label><br />
                                  <asp:TextBox ID="txtcomplaintdetails" runat="server" CssClass="form-control" data-toggle="tooltip"  height="5%" width="95%" data-placement="right" title="Complaint Details" TextMode="MultiLine"></asp:TextBox>
                                    </div>--%>
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
    </form>
</body>
</html>
