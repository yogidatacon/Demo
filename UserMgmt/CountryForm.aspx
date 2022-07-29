<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeFile="CountryForm.aspx.cs" Inherits="UserMgmt.CountryForm" %>
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
            <li class="active">  <asp:LinkButton  ID="Country"  runat="server" OnClick="Country_Click" ><span style="color:#fff;font-size:14px;">Country Master</span></asp:LinkButton></li>


         <li >  <asp:LinkButton  ID="StateMaster"  runat="server" OnClick="StateMaster_Click"
><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>
                       
        <li >     <asp:LinkButton ID="DivisionMaster"  runat="server" OnClick="DivisionMaster_Click"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
      <li  >  <asp:LinkButton  ID="DistrictMaster"   runat="server" OnClick="DistrictMaster_Click"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
        <li  >  <asp:LinkButton  ID="RoleLevelMaster"   runat="server" OnClick="RoleLevelMaster_Click"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li> 
                <li  >  <asp:LinkButton  ID="AccessTypeMaster"   runat="server" OnClick="AccessTypeMaster_Click"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
        <li  >  <asp:LinkButton  ID="RoleMaster"   runat="server" OnClick="RoleMaster_Click"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li> 
                   	      
       
        </ul>
        <br/>
          <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" style="float:right" OnClick="ShowRecords_Click"><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
              <div class="row">
           <div class="x_title">
                                    <h2>Country Form</h2>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <br />

                                    <form class="form-inline" id="stateForm" accept-charset="UTF-8" method="post" action="../login/stateMasterList.htm" enctype="multipart/form-data" commandName="stateForm">

                                      

                                        </div> 
                                        <table width="100%">
                                            <tr>
                                                <td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td><td width="10%"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                     <label class="control-label" style="font-size:12px"><span style="color:red">*</span>Country Name</label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td colspan="2">
                                                      <label class="control-label" style="font-size:12px"><span style="color:red">*</span> Country Code</label>
                                                 </td>
                                            </tr>
                                                                                           
                                            <tr>
                                                <td colspan="2">
                                                    <input type="text" id="CountryName" data-toggle="tooltip" data-placement="top" title="Country Name" class="form-control" name="Country Name" maxlength="50" >
                                                </td>
                                                <td >&nbsp;</td>
                                                <td colspan="2">
                                                     <input type="text" data-toggle="tooltip" data-placement="top" title="Country Code" id="CountryCode" class="form-control"  name="Country Code"  maxlength="2">
                                                </td>
                                            </tr>

                                                     
                                         
                                                   
                                      </table>

                                        <p>&nbsp;</p>
                                        
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <input type="submit"  class="btn btn-primary" style="" value="Submit" onclick="validationMsg();"/>                                              
                                                    <a href="../login/stateMasterList.htm" class="btn btn-danger">Cancel</a>                          
                                                </div> 
                                            </div> 
                                         </form>
            </div>
        </body>
                 </html>
                                    </div>
                                </div>
                            </div>
                        </div>
                          </div>




</asp:Content>
