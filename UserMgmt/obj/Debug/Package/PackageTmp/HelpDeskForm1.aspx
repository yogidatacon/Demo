<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDeskForm1.aspx.cs" Inherits="UserMgmt.HelpDeskForm1" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <title>Tickets </title>
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

    <script type="text/javascript">
        function validationMsg1() {
            if (document.getElementById('<%=ddlPriority.ClientID%>').value == 'Select') {
                                             alert("Select Priority");
                                             document.getElementById("<% =ddlPriority.ClientID%>").focus();
                                             return false;
                                         }
                                     <%--}
                                     function validationMsg() {
                                         if (document.getElementById('<%=txtDATE.ClientID%>').value == '') {
                                             alert("Select Gauged & Proved Date ");
                                             document.getElementById("<% =txtDATE.ClientID%>").focus();
                                             return false;
                                         }--%>
                                         if (document.getElementById('<%=ddlTicketStatus.ClientID%>').value == 'Select') {
                                             alert("Select TicketStatus ");
                                             document.getElementById("<% =ddlTicketStatus.ClientID%>").focus();
                                             return false;
                                         }
                                     }
    </script>
</head>
<body>
    <form runat="server">
        <div class="container-fluid">
            <div class="row content">
                <h3 style="margin-left: 0%" class="control-label">Help Desk Tickets</h3>
                <%--<asp:LinkButton ID="btnLogOut" Text="Log Out" CssClass="btn btn-danger right"  runat="server" OnClick="btnLogOut_Click"></asp:LinkButton>--%>
                <div style="height: 0.1%; background-color: #26b8b8;">
                </div>
                  <marquee direction="right">
                    <asp:Label ID="lblresolvetime" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label></marquee>
                <div class="col-sm-9">
                  
                    <div class="clearfix"></div>
                    <%--<h3 style="margin-left: 30%" class="control-label">Help Desk Tickets</h3>--%>

                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Error Page Name</label>
                        <br />
                        <asp:TextBox ID="txtpagename" runat="server" CssClass="form-control" autocomplete="off" Width="250px" data-toggle="tooltip" data-placement="right" title="Page Name" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>Raised By</label>
                        <br />
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Name" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>User Contact Number</label>
                        <br />
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-inline">
                        <label class="control-label" style="display: inline"><span style="color: red"></span>User Email ID</label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <asp:TextBox ID="txtticketQuery" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" cols="20" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <h4 class="control-label">Comment:</h4>

                    <div class="form-group">
                        <asp:TextBox ID="txtComment" autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" placeholder="Type Your Message" cols="20" Rows="7" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <asp:FileUpload ID="idupDocument" runat="server" onchange="validateExtraDocuments();" />
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>


                    <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return validationMsg()" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" > </span>Submit</asp:LinkButton>

                    <br>
                    <br>
                    <%--   <p><span class="badge"></span> Comments:</p>--%>
                    <br>

                    <div class="row">
                    </div>
                    <%--<asp:TextBox ID="txthistory"  autocomplete="off" runat="server" data-toggle="tooltip" data-placement="right" Height="100%" Width="100%" cols="20" rows="10" TextMode="MultiLine"></asp:TextBox>
                    --%>
                    <div style="height: 8%; background-color: #26b8b8;">
                        <span style="font-size: medium; color: white; margin-left: 40%">Ticket History</span>
                    </div>
                    <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records" OnPageIndexChanging="grdHistory_PageIndexChanging"
                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                        <Columns>
                            <asp:TemplateField HeaderText="tickethistory" ItemStyle-Font-Bold="true" Visible="false" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Label ID="lblticket_history" runat="server" Visible="true" Text='<%#Eval("hd_ticket_history_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Visible="true" Text='<%#Eval("creation_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User" ItemStyle-Font-Bold="true" Visible="true" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Visible="true" Text='<%#Eval("user_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--  <asp:TemplateField HeaderText="Setup Time " ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetupTime" runat="server" Visible="true" Text='<%#Eval("setup_time") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Fermenter VAT" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFermenterVAT" runat="server" Visible="true" Text='<%#Eval("vat_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Comment" ItemStyle-Font-Bold="true" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblhistory" runat="server" Visible="true" Text='<%#Eval("remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Visible='<%#Eval("path").ToString()==null? false:true%>' Text='<%#Eval("path")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--    <asp:TemplateField HeaderText="Fermenter Status" ItemStyle-Font-Bold="true"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("record_status").ToString() == "A" ? "Approved" :Eval("record_status").ToString()=="R"? "Rejected":(Eval("record_status").ToString()=="Y"? "Pending":"Draft") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>
                                                <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true"
                                ItemStyle-Width="90px" HeaderStyle-Font-Underline="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" OnClick="DownloadFile" Visible='<%#Eval("path").ToString()==null? false:Eval("path").ToString()==""?false:true%>' CommandArgument='<%#Eval("path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server" />
                                    <%-- <asp:LinkButton ID="btnView" runat="server" CssClass="myButton" OnClick="btnView_Click"><i class="fa fa-search-plus">
                                                </i>      
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="myButton1" Visible='<%# Eval("record_status").ToString() == "A" ? false:Eval("record_status").ToString() == "Y" ? false : true %>' OnClick="btnEdit_Click"><i class="fa fa-pencil-square-o">
                                                                                </i>  
                                                    </asp:LinkButton> --%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" CssClass="paginationClass" />

                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>


                    </asp:GridView>

                </div>
                <div class="col-sm-3 sidenav">
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <div style="align-self: center">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:return validationMsg1()" CssClass="btn btn-primary" OnClick="ShowRecords_Click">
                                                    <span aria-hidden="true" > </span>SHOW RECORD LIST</asp:LinkButton>
                    </div>
                    <%--<asp:Button ID="Button1" runat="server"  Text="SHOW RECORD LIST" OnClick="ShowRecords_Click" />--%>

                    <p>&nbsp;</p>




                    <%--   <asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" OnClick="ShowRecords_Click"  Style="float: right"><i class="fa fa-list"> SHOW RECORD LIST</i></asp:LinkButton>--%>
                    <%-- <ul class="nav nav-pills nav-stacked">
        <li class="active"><a href="#section1">Home</a></li>
        <li><a href="#section2">Friends</a></li>
        <li><a href="#section3">Family</a></li>
        <li><a href="#section3">Photos</a></li>
      </ul><br>--%>
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Ticket Category</label><br />
                    <asp:DropDownList ID="ddlTicketCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Priority</label><br />
                    <asp:DropDownList ID="ddlPriority" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Ticket Status</label><br />
                    <asp:DropDownList ID="ddlTicketStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Assigned To Developer</label><br />
                    <asp:DropDownList ID="ddldeveloper" CssClass="form-control" runat="server"></asp:DropDownList>
                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <label class="control-label" style="display: inline"><span style="color: red"></span>Assigned To Tester</label><br />
                    <asp:DropDownList ID="ddlAssignedTotester" CssClass="form-control" runat="server"></asp:DropDownList>

                    <div class="clearfix"></div>
                    <p>&nbsp;</p>
                    <%--  <div class="input-group">
                   <input type="text" class="form-control" placeholder="Search Blog..">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>--%>
                    <%-- <div class="col-sm-2 well">
      <div class="thumbnail">
        <p>Upcoming Events:</p>
        <img src="paris.jpg" alt="Paris" width="400" height="300">
        <p><strong>Paris</strong></p>
        <p>Fri. 27 November 2015</p>
        <button class="btn btn-primary">Info</button>
      </div>      
      <div class="well">
        <p>ADS</p>
      </div>
      <div class="well">
        <p>ADS</p>
      </div>--%>

                    <%--  <div class="col-sm-3 ">--%>

                    <div class="container">
                        <div id="accordion" class="accordion">
                            <div class="card mb-0">
                                <div class="card-header collapsed" data-toggle="collapse" href="#collapseOne">
                                    <a style="margin-left: 0%" class="card-title">
                                        <h4 style="color: black">Ticket Details</h4>
                                    </a>
                                </div>
                                <div id="collapseOne" class="card-body collapse" data-parent="#accordion">
                                    <div>
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Tracking ID</label>&nbsp: &nbsp&nbsp;&nbsp;&nbsp;<span><asp:Label ID="lblTracking" runat="server"></asp:Label></span>
                                    </div>
                                    <%-- <div class="col-sm-1 well">
                                            
                                            <asp:Label ID="lblTracking" runat="server"></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                    <div>
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Ticket number</label>: &nbsp;&nbsp;<span><asp:Label ID="lblTicketno" runat="server"></asp:Label></span>
                                    </div>
                                    <%--<div class="col-sm-1 well">
                                           
                                            <asp:Label ID="lblTicketno" runat="server" ></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                    <div>
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Created on</label>&nbsp: &nbsp&nbsp;&nbsp;&nbsp;<span>
                                            <asp:Label ID="lblCreated" runat="server"></asp:Label></span>
                                    </div>
                                    <%--<div class="col-sm-1 well">
                                          
                                            <asp:Label ID="lblCreated" runat="server" ></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                    <div>
                                        <label class="control-label" style="display: inline">Created By</label>&nbsp:&nbsp &nbsp&nbsp;&nbsp;&nbsp;<span><asp:Label ID="lblTicketstatus" runat="server"></asp:Label></span>
                                    </div>
                                    <%--<div>
                                           
                                            <asp:Label ID="lblTicketstatus" runat="server" ></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                    <div>
                                        <label class="control-label" style="display: inline"><span style="color: red"></span>Updated</label>&nbsp&nbsp:&nbsp &nbsp&nbsp&nbsp;&nbsp;&nbsp;<span>
                                            <asp:Label ID="lblUpdated" runat="server"></asp:Label></span>
                                    </div>
                                    <%-- <div class="col-sm-1 well">
                                            
                                            <asp:Label ID="lblUpdated" runat="server" ></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                      <div>
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Time Taken By Developer </label>: <span><asp:Label ID="lblDeveloper" runat="server" ></asp:Label></span>
                                        </div>
                                        <%-- <div class="col-sm-1 well">
                                            
                                            <asp:Label ID="lblPriority" runat="server" ></asp:Label>
                                        </div>--%>
                                    <br />
                                    <div class="clearfix"></div>
                                      <div>
                                            <label class="control-label" style="display: inline"><span style="color: red"></span>Time Taken By Tester </label>: <span><asp:Label ID="lblTester" runat="server" ></asp:Label></span>
                                        </div>
                                    <div class="clearfix"></div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </div>
        </div>

        <%--<footer class="container-fluid">
            <p>Footer Text</p>
        </footer>--%>
    </form>
</body>
</html>

