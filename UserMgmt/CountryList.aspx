<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="UserMgmt.CountryList" %>
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

         <li >  <asp:LinkButton  ID="StateMaster"  runat="server" OnClick="StateMaster_Click"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>
                       
        <li >     <asp:LinkButton ID="DivisionMaster"  runat="server" OnClick="DivisionMaster_Click"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
      <li  >  <asp:LinkButton  ID="DistrictMaster"   runat="server" OnClick="DistrictMaster_Click"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
        <li  >  <asp:LinkButton  ID="RoleLevelMaster"   runat="server" OnClick="RoleLevelMaster_Click"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li> 
                <li  >  <asp:LinkButton  ID="AccessTypeMaster"   runat="server" OnClick="AccessTypeMaster_Click"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
        <li  >  <asp:LinkButton  ID="RoleMaster"   runat="server" OnClick="RoleMaster_Click"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li> 
                   	   
        </ul>
        <br/>
      <a  ><asp:LinkButton runat="server" CssClass="myButton3 " ID="AddRecord" style="float:right" OnClick="AddRecord_Click"   ><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton></a>
      
      <div class="row">

                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <h2>Country List</h2>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="x_content">
                                                        <asp:GridView id="grdMF2List" runat="server" AutoGenerateColumns="false"   AllowPaging="true" 
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Country  Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCountryName" runat="server" Visible="true" Text="Datacon" ></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                                                                                       
                                                            <asp:TemplateField HeaderText="Action"     ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" >
                                                                <ItemTemplate>
                                                                        <asp:LinkButton Text="View" id="btnPrint"  CssClass="myButton"  runat="server"   CommandName="View" ><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                    <asp:LinkButton Text="Edit" id="LinkButton1"  CssClass="myButton"   runat="server"   CommandName="Edit" ><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                            </asp:LinkButton>
                                                                </ItemTemplate>
                                                                  <ItemStyle Width="10px" />
                                                            </asp:TemplateField>
                                                   </Columns>
                                                             </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>	


                                

            <!-- Datatables -->
            <script src="../common/theme/js/datatables/js/jquery.dataTables.js"></script>

            <script>
                $(document).ready(function () {
                    $('input.tableflat').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });
                });

                var asInitVals = new Array();
                $(document).ready(function () {
                    var oTable = $('#example').dataTable({
                        "oLanguage": {
                            "sSearch": "Search all columns:"
                        },
                        "aaSorting": [],                        
                        'iDisplayLength': 10,
                        "sPaginationType": "full_numbers",
                        "dom": 'T<"clear">lfrtip',
                        "tableTools": {
                            "sSwfPath": "<?php echo base_url('assets2/js/Datatables/tools/swf/copy_csv_xls_pdf.swf'); ?>"
                        }
                    });
                    $("tfoot input").keyup(function () {
                        /* Filter on the column based on the index of this element's parent <th> */
                        oTable.fnFilter(this.value, $("tfoot th").index($(this).parent()));
                    });
                    $("tfoot input").each(function (i) {
                        asInitVals[i] = this.value;
                    });
                    $("tfoot input").focus(function () {
                        if (this.className == "search_init") {
                            this.className = "";
                            this.value = "";
                        }
                    });
                    $("tfoot input").blur(function (i) {
                        if (this.value == "") {
                            this.className = "search_init";
                            this.value = asInitVals[$("tfoot input").index(this)];
                        }
                    });
                });
            </script>
               
        </body>
                 </html>
                                    </div>
                                </div>
                            </div>
                        </div>
                          </div>
        </div>

</asp:Content>
