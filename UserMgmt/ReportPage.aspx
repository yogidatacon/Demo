<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="UserMgmt.ReportPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title></title>
    <script src="js/library/jNotify.jquery.js"></script>
    <script src="js/library/jquery-3.2.1.js"></script>
    <script src="js/library/jquery-ui.js"></script>
   <%-- <script src="js/library/jquery.accordion.js"></script>
    <script src="js/library/print.min.js"></script>--%>
    <script>
        
        $(document).ready(function () {
            debugger;
            var getInput = document.getElementById('txtpdf').value;
            localStorage.setItem("storageName", getInput);
            localStorage.setItem("dtime", document.getElementById('dtime').value);
            localStorage.setItem("dkey", document.getElementById('dkey').value);
            localStorage.setItem("xy", document.getElementById('xy').value);
            localStorage.setItem("HW", document.getElementById('HW').value);


        });
        </script>
    
       <script>
        function CallPKI() {
            debugger;
           // var reqxml = GetRequestData();
            data = document.getElementById("txtpdf").value;
            reqxml = data;
            const Http = new XMLHttpRequest();
            const url = 'http://127.0.0.1:1620';
            Http.open("POST", url);
            Http.send(reqxml);
            Http.onreadystatechange = (e) =>
            {
                var xml = Http.responseText;
                document.getElementById("txtResponse").innerHTML = Http.responseText;
                console.log(Http.responseText)
            }
        }

    </script>


   
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%-- <div align="center" class="col-md-9 col-sm-9 col-xs-9">--%>
             <%--<asp:Button ID="btnBack" BackColor="RoyalBlue" ForeColor="White" Height="30px" Width="50px" runat="server" class="btn btn-primary" OnClick="btnBack_Click" Text="Back" />--%>
       <%-- <asp:LinkButton ID="btnBack" BackColor="RoyalBlue"  runat="server">Back</asp:LinkButton>--%>
             <%--</div>--%>
         <div align="center">
        <%--<rsweb:ReportViewer ID="ReportViewer1" Width="75%" runat="server" ShowPageNavigationControls="False" ShowPrintButton="True" Height="75%"  AsyncRendering="true" SizeToReportContent="true" Font-Names="Verdana" Font-Size="8pt" ></rsweb:ReportViewer>--%>
             <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowReportBody="true" ShowPrintButton="true" ShowRefreshButton="false" ShowPageNavigationControls="False" ShowFindControls="false" InteractivityPostBackMode="SynchronousOnDrillthrough" ZoomMode="FullPage" ProcessingMode="Remote" SizeToReportContent="True" />
             <asp:HiddenField ID="txtpdf" runat="server" />
              <asp:HiddenField ID="dtime" runat="server" />
              <asp:HiddenField ID="dkey" runat="server" />
              <asp:HiddenField ID="xy" runat="server" />
              <asp:HiddenField ID="HW" runat="server" />
              <asp:HiddenField ID="txtResponse" runat="server" />
    </div>
       
    </form>
</body>
</html>
