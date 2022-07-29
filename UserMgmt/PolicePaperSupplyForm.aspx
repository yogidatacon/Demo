<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterCaseMgmt.Master" AutoEventWireup="true" CodeBehind="PolicePaperSupplyForm.aspx.cs" Inherits="UserMgmt.PolicePaperSupplyForm" %>

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
                                <title>Supply of Police Paper Form</title>
                               <script language="javascript" type="text/javascript">
                                    function validationMsg() {
                                       
                                       if (document.getElementById('<%=txtPRFIRNo.ClientID%>').value == '') {
                                            alert("Enter FIR details!");
                                            document.getElementById("<% =txtPRFIRNo.ClientID%>").focus();
                                            return false;
                                        }
                                        
                                          if (document.getElementById('<%=txtDateOfPolicepapersupply.ClientID%>').value == '') {
                                              alert("Enter Date of Police paper supply ");
                                            document.getElementById("<% =txtDateOfPolicepapersupply.ClientID%>").focus();
                                            return false;
                                          }
                                        if (document.getElementById('<%=txtNexthearingDate.ClientID%>').value == '') {
                                            alert("Enter Next hearing Date");
                                            document.getElementById("<% =txtNexthearingDate.ClientID%>").focus();
                                            return false;
                                        }
                                      <%--  var Date_Of_Policepaper_supply = document.getElementById('<%=txtDateOfPolicepapersupply.ClientID%>').value;
                                        var Nexthearing_Date = document.getElementById('<%=txtNexthearingDate.ClientID%>').value;
                                        //var cDate = new Date(Complaint_Date);
                                        //var fDate = new Date(FIR_Date);
                                        if (Date_Of_Policepaper_supply != '' && Nexthearing_Date != '' && Date_Of_Policepaper_supply > Nexthearing_Date) {
                                            alert("Please ensure that the Nexthearing Date  is greater than or equal to the Date Of Policepaper supply .");
                                            return false;
                                        }--%>
                                    }
                                </script>
                                <script type="text/javascript">
    function blockAllChar(e){
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return false; 
        }
    </script>
                            </head>
                            <body>
                                <a>
                                    <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecord_Click"   Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton></a>
                                <div class="x_title">
                                    <h2>Police Paper Supply  Form</h2>
                                    <div class="clearfix"></div>
                                </div>
                                  <div class="x_content">
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                     
                                <a>
                                     <asp:LinkButton runat="server" ID="btnSeizure" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Seizure" OnClick="btnSeizure_Click"  BorderStyle="Outset">Seizure</asp:LinkButton>
                                </a>
                                <%--<a>
                                       <asp:LinkButton runat="server" ID="btnFIR" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View FIR" OnClick="btnFIR_Click"  BorderStyle="Outset"> FIR</asp:LinkButton>
                                </a>
                                <a>
                                       <asp:LinkButton runat="server" ID="btnChargeSheet" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnChargeSheet_Click"  BorderStyle="Outset">Charge Sheet </asp:LinkButton>
                                </a>
                                 <a>
                                       <asp:LinkButton runat="server" ID="btnBail" Height="100%" Width="12%"  CssClass="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="View Charge Sheet" OnClick="btnBail_Click"  BorderStyle="Outset">Bail</asp:LinkButton>
                                </a>--%>
                                       <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                   
                                   <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                        <label class="control-label" style="font-size:small;display:inline"><span style="color: red">*</span>PR / FIR No</label>
                                        <br />
                                      <asp:TextBox ID="txtPRFIRNo" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>'  title="PR / FIR No"></asp:TextBox>
                                       <asp:TextBox ID="txtfirdate" CssClass="form-control" runat="server" data-toggle="tooltip" data-placement="right" ReadOnly="true" Text='<%#Eval("strPRFIRNo") %>' Visible="false"  title="PR / FIR No"></asp:TextBox>
                                    </div>
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span> Date of police paper supply</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image1" TargetControlID="txtDateOfPolicepapersupply" Format="dd-MM-yyyy" ID="CalendarExtender1"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtDateOfPolicepapersupply" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" ReadOnly="false" title="Date of police paper supply" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image1" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                     
                                     <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                                <label class="control-label" style="display:inline"><span style="color: red">*</span>Next Hearing Date</label><br />
                                                <cc1:CalendarExtender runat="server" PopupButtonID="Image2" TargetControlID="txtNexthearingDate" Format="dd-MM-yyyy" ID="CalendarExtender2"></cc1:CalendarExtender>
                                                <asp:TextBox ID="txtNexthearingDate" onchange="chkDuplicateDates();" data-toggle="tooltip" data-placement="right" title="Next Hearing date" ReadOnly="false" class="form-control validate[required]" onkeypress="return blockAllChar(event)" AutoComplete="off" runat="server" Font-Size="14px">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="Image2" class="control-label" runat="server" Height="30px" Width="30px" ImageUrl="common/theme/image1/calendar-512.png" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                        <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                            <div class="clearfix"></div>
                                        <p>&nbsp;</p>
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-inline">
                                            <asp:LinkButton ID="btnSaveasDraft" OnClientClick="javascript:return validationMsg()" runat="server" OnClick="btnSaveasDraft_Click" class="btn btn-info pull-left">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> *</span>Save as Draft</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg();" OnClick ="btnSubmit_Click"  CssClass="btn btn-primary">
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
