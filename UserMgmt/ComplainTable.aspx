<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComplainTable.aspx.cs" Inherits="UserMgmt.ComplainTable" %>

<!DOCTYPE html>

<html>
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Complaint/ Grievance Redressal Form</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
<style>
body {font-family: Arial, Helvetica, sans-serif;}
* {box-sizing: border-box;}

/*input[type=text], select {
  width: 100%;
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  margin-top: 6px;
  margin-bottom: 16px;
  resize: vertical;
}
textarea {
  width: 100%;
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  margin-top: 6px;
  margin-bottom: 16px;
  resize: vertical;
}

input[type=submit], button {
  background-color: #04AA6D;
  color: white;
  padding: 12px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

input[type=submit]:hover {
  background-color: #45a049;
}*/

.container {
  border-radius: 5px;
  background-color: #f2f2f2;
  padding: 20px;
}
</style>
</head>
<body style="background-image:">
<div class="container" style="width:50%; margin-left:25%;margin-top:5%;margin-bottom:5%;margin-right:25%">
  <form runat="server">
    <%--  <h3>Complaint/ Grievance Redressal Form</h3>
    <p>Please send us details about the incident you would like to report.
     Our Complaint Center will analyze your complaint and take the appropriate measures in order that the reported situation will not occur at any other time in the future.</p>
    <label for="txtcomplainantname">Complainant Name</label>
    <input type="text" id="txtcomplainantname" name="firstname" runat="server" placeholder="Your name..">

     <label for="txtmobile">Contact Number</label>
    <input type="text" id="txtmobile" name="lastname" placeholder="Your Contact Number..">

    <label for="txtemail">Email ID</label>
    <input type="text" id="txtemail" name="lastname" placeholder="Your Email ID..">

    <label for="ddlcomplaintype">Complaint Type</label>
    <asp:DropDownList ID="ddlcomplaintype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Complaint Type">
                                            <asp:ListItem Value="LS">Liquior Sale</asp:ListItem>
                                              <asp:ListItem Value="CO">Complaint Against Officer</asp:ListItem>
                                            <asp:ListItem Value="CM">Consumption</asp:ListItem>
                                             <asp:ListItem Value="SA">Sale</asp:ListItem>
                                              <asp:ListItem Value="HD">Home Delivery</asp:ListItem>
                                             <asp:ListItem Value="MF">Manufacturing</asp:ListItem>
                                              <asp:ListItem Value="ST">Storage</asp:ListItem>
                                         
                                        </asp:DropDownList>

    <label for="txtAddress">Address</label>
    <textarea id="txtAddress" name="subject" placeholder="Write something.." style="height:200px"></textarea>

      <label for="txtcomplaintdetails">Complaint Details</label>
    <textarea id="txtcomplaintdetails" name="subject" placeholder="Write something.." style="height:200px"></textarea>
      
                                        <label for="ddlState">State</label>
                                      
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true"    data-toggle="tooltip" data-placement="right" title="State"></asp:DropDownList>
                                        <label >District</label>
                                           <asp:TextBox ID="txtdistrict" autocomplete="off"    Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="District"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDistrict" runat="server"   data-toggle="tooltip" AutoPostBack="true" data-placement="right" title="District" ></asp:DropDownList>
                                        <label >Thana</label>
                                           <asp:TextBox ID="txtthana" autocomplete="off" Visible="false" runat="server"  data-toggle="tooltip" data-placement="right" title="Thana"></asp:TextBox>
                                        <asp:DropDownList ID="ddlThana" runat="server"  data-toggle="tooltip" data-placement="right" AutoPostBack="true" title="Thana"></asp:DropDownList>
       
                        <label>Village Name</label>
                        <asp:TextBox ID="txtvillage" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" ></asp:TextBox>
                        <label >Land Mark</label>
                        <asp:TextBox ID="txtlandmark" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email ID" ></asp:TextBox>
                  
                                   
                                         
                                            <label for="idupDocument">Documents</label><br />
                                            <asp:FileUpload ID="idupDocument"  onchange="validateExtraDocuments();" runat="server" /><br />
                                  
                                        <label ></label><br />
                                            <label >Document Description</label><br />
                                            <asp:TextBox ID="txtDiscription" runat="server"  data-toggle="tooltip"  data-placement="right" title="Document Description" ></asp:TextBox>
                                              
       <input type="submit" value="upload" runat="server">
                               
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
                                                        <asp:ImageButton ID="ImageButton2" Width="30px" Height="20px" CommandArgument='<%#Eval("doc_path") %>' CommandName="Download" ImageUrl="~/img/download.png" runat="server"  />
                                                        &nbsp;&nbsp;&nbsp; &nbsp;  
                                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%#Eval("doc_path") %>' CommandName="Remove" ImageUrl="~/img/delete.gif" runat="server"  />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            
                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>

                                            <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True" />

                                            <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                          
                                        </asp:GridView>



      <p>&nbsp;</p>
       <p>&nbsp;</p>
       <p>&nbsp;</p>
       <p>&nbsp;</p>

    <input type="submit" value="Submit" runat="server" >--%>
       <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputEmail4">Complainant Name</label>
      <%--<input type="email" class="form-control" id="inputEmail4" placeholder="Email">--%>
         <asp:TextBox ID="txtcomplainantname" runat="server" CssClass="form-control" autocomplete="off" Width="250px" data-toggle="tooltip" data-placement="right" title="Complainant Name" ></asp:TextBox>
    </div>
    <div class="form-group col-md-6">
      <label for="inputPassword4">Contact Number</label>
      <%--<input type="password" class="form-control" id="inputPassword4" placeholder="Password">--%>
         <asp:TextBox ID="txtmobile" runat="server"  autocomplete="off" data-toggle="tooltip" data-placement="right" title="Contact Number" MaxLength="10"  class="form-control validate[custom[phone],required]" onchange="phoneValidate()" onkeypress="return onlyDotsAndNumbers(this,event);" ></asp:TextBox>
    </div>
  </div>
       <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputEmail4">Email ID</label>
      <%--<input type="email" class="form-control" id="inputEmail4" placeholder="Email">--%>
            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" autocomplete="off" data-toggle="tooltip" data-placement="right" title="Email ID" onchange="emailValidate(this);" ></asp:TextBox>
    </div>
    <div class="form-group col-md-6">
      <label for="inputPassword4">Complain Type</label>
      <%--<input type="password" class="form-control" id="inputPassword4" placeholder="Password">--%>
         <asp:DropDownList ID="ddlcomplaintype" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="right" title="Complaint Type">
                                            <asp:ListItem Value="LS">Liquior Sale</asp:ListItem>
                                              <asp:ListItem Value="CO">Complaint Against Officer</asp:ListItem>
                                            <asp:ListItem Value="CM">Consumption</asp:ListItem>
                                             <asp:ListItem Value="SA">Sale</asp:ListItem>
                                              <asp:ListItem Value="HD">Home Delivery</asp:ListItem>
                                             <asp:ListItem Value="MF">Manufacturing</asp:ListItem>
                                              <asp:ListItem Value="ST">Storage</asp:ListItem>
                                         
                                        </asp:DropDownList>
    </div>
  </div>
 <div class="form-group col-md-12" >
   <label><span style="color: red">*</span>Address</label>
     <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" data-toggle="tooltip"   data-placement="right" title="Address" TextMode="MultiLine"></asp:TextBox>
                                                 
  </div>
  <div class="form-group col-md-12">
      <label class="control-label"><span style="color: red">*</span>Complaint Details</label><br />
      <asp:TextBox ID="txtcomplaintdetails" runat="server" CssClass="form-control" data-toggle="tooltip"  data-placement="right" title="Complaint Details" TextMode="MultiLine"></asp:TextBox>
  </div>
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputCity">City</label>
      <input type="text" class="form-control" id="inputCity">
    </div>
    <div class="form-group col-md-4">
      <label for="inputState">State</label>
      <select id="inputState" class="form-control">
        <option selected>Choose...</option>
        <option>...</option>
      </select>
    </div>
    <div class="form-group col-md-2">
      <label for="inputZip">Zip</label>
      <input type="text" class="form-control" id="inputZip">
    </div>
  </div>
  <div class="form-group">
    <div class="form-check">
      <input class="form-check-input" type="checkbox" id="gridCheck">
      <label class="form-check-label" for="gridCheck">
        Check me out
      </label>
    </div>
  </div>
  <button type="submit" class="btn btn-primary">Sign in</button>

                                         
  </form>
    </div>

</body>
</html>
