<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeFile="AccesstypeMasterform.aspx.cs" Inherits="UserMgmt.AccesstypeMasterform" %>
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
         <li >  <asp:LinkButton  ID="StateMaster"  runat="server" OnClick="StateMaster_Click"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>
                       
        <li >     <asp:LinkButton ID="DivisionMaster"  runat="server" OnClick="DivisionMaster_Click"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
      <li  >  <asp:LinkButton  ID="DistrictMaster"   runat="server" OnClick="DistrictMaster_Click"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
        <li   >  <asp:LinkButton  ID="RoleLevelMaster"   runat="server" OnClick="RoleLevelMaster_Click"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li> 
                <li  class="active">  <asp:LinkButton  ID="AccessTypeMaster"   runat="server" OnClick="AccessTypeMaster_Click"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
        <li  >  <asp:LinkButton  ID="RoleMaster"   runat="server" OnClick="RoleMaster_Click"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li> 
                   	      
       
        </ul>
        <br/>
          <a  ><asp:LinkButton runat="server" CssClass="myButton3" ID="ShowRecords" style="float:right" OnClick="ShowRecords_Click" ><i class="fa fa-list "> SHOW RECORD LIST</i></asp:LinkButton></a>
              <div class="row">
           <div class="x_title">
                                    <h2>Access Type Form</h2>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">

                                    <form class="form-inline" id="access" accept-charset="UTF-8" method="post" action="../login/accessTypeSubmit.htm" enctype="multipart/form-data" commandName="access">
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"><span style="color:red">*</span>Access Type Name</label>
                                                    <input type="text" id="accessTypName" name="accessTypName" data-toggle="tooltip" data-placement="right" title="Access Type Name"  class="form-control capitalize validate[required]" onchange="uniqueValid();">
                                                    <span id="error1" style="color: red; font: bold; display: none; text-align: right;margin-left: 160px;"data-toggle="tooltip" data-placement="right" title="Access Type Name">Please enter Level Name...</span>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                            <label class="control-label"style="margin-bottom: 30px">Description</label>
                                            
                                                    <textarea name="accessTypDesc" class="form-control" style="resize: none" maxlength="98"data-toggle="tooltip" data-placement="right" title="Description"></textarea>
                                        </div>

                                        <p>&nbsp;</p>
                                        <p>&nbsp;</p>
                                        <p>&nbsp;</p>
                                        
                                            <div class="col-md-9 col-sm-9 col-xs-9 form-inline">
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <button type="submit" id="billbutton" class="btn btn-primary" style="">Submit</button>
                                                    <a href="../login/accessTypeList.htm" class="btn btn-danger">Cancel</a>
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
        </div>


</asp:Content>
