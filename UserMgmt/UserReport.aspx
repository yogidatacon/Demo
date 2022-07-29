<%@ Page Title="" Language="C#" EnableEventValidation="False" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeFile="UserReport.aspx.cs" Inherits="UserMgmt.UserReport" %>
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
        <title>User Management</title>
    </head>
    <body>
        <div >
        <ul class="nav nav-tabs">
       
                       
        <li class="active">     <asp:LinkButton ID="UserReport1" runat="server" OnClick="UserReport_Click"><span style="color:#fff;font-size:14px;">UserReport</span></asp:LinkButton></li>
                         
       
        </ul>
        <br/>
          <%--<a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords"  style="float:right"  ><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>--%>
             <div class="row">
            <div class="x_title"   >
                                    <h2>User Reports</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                    </ul><p>&nbsp;</p>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <form class="form-inline" accept-charset="UTF-8" method="post"  enctype="multipart/form-data">
                                                                                
                                                 
            
                                            
                                                                               
                                            
                                         
                                     <%--   <input type="hidden" id="lvlOfcr1" name="lvlOfcr11" value="State"> 
                                        <input type="hidden" id="stcode" name="stcode1" value="STATE-2">
                                        <input type="hidden" id="div111" name="div111" value="Bhagalpur">
                                        <input type="hidden" id="dist111" name="dist111" value="Banka">
                                         <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><a style="color:red;">*</a>Report Name</label>
                                            <select class="form-control validate[required]" name="reportName" id="reportName" onchange="showDivison1();">
                                                <option selected="" disabled="" value="">Select an option</option>
                                                <option value="userReport">User Report</option>
                                            </select>
                                        </div>
                                         <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><a style="color:red;">*</a>Format</label>

                                            <select class="form-control validate[required]" name="reportFormat" id="reportFormat">
                                                <option selected value="">Select an option</option>
                                                <option  value="HTML">HTML</option>
                                                <option  value="PDF">PDF</option>
                                                <option  value="EXCEL">EXCEL</option>
                                            </select>
                                            <span id="error2" style="color: red; font: bold; display: none; text-align: right;margin-left: 160px;">Please select district name...</span>
                                        </div>--%>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label class="control-label" style="font-size:12px"><a style="color:red;">*</a>Report Name</label>
                                                </td>
                                                 <td>&nbsp;</td>
                                                <td colspan="2">
                                                    <label class="control-label" style="font-size:12px"><a style="color:red;">*</a>Format</label>
                                                </td>
                                            </tr>

                                             <tr>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="ReportName" runat="server" class="form-control"  data-toggle="tooltip" data-placement="right" title="Report Name">
                                                        <asp:ListItem> Select an option</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td>&nbsp;</td>
                                                <td colspan="2">
                                                     <asp:DropDownList ID="Format" runat="server" class="form-control" data-toggle="tooltip" data-placement="right" title="Format" >
                                                        <asp:ListItem> Select an option</asp:ListItem>
                                                           <asp:ListItem  value="HTML">HTML</asp:ListItem>
                                                <asp:ListItem  value="PDF">PDF</asp:ListItem>
                                                <asp:ListItem value="EXCEL">EXCEL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                       

                                        <p>&nbsp;</p>
                                        <p>&nbsp;</p>
                                        <p>&nbsp;</p>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <a  class="btn btn-primary" style="" onclick="return validateForm();">Generate</a>
                                                <a href="../login/userReport.htm" class="btn btn-danger">Cancel</a>
                                            </div></div>
                                    </form>
                                                
                                                        
                                            
                                        </div>
                                       
                                        
                                   
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
</asp:Content>
