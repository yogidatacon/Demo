<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="UserMgmt.ForgotPassword" %>

<!DOCTYPE html>

<html>
<head>
    <script src="common/theme/js/jquery.min.js"></script>
    <script src="common/theme/js2/jquery.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Seizure Form</title>
    <link rel="shortcut icon" href="common/theme/assets/ico/fevicon.png">
    <link href="common/theme/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="common/datepicker/datetimepicker_css.js?ver=1"></script>
    <script src="common/validate/form_field_validator.js"></script>
    <script src="common/validate/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="common/validate/ex-automation.js" type="text/javascript"></script>
    <script src="common/datepicker/timepicker.min.js" type="text/javascript"></script>
    <link href="common/datepicker/timepicker.min.css" rel="stylesheet" type="text/css" />
    <style>
        input[type="radio"] {
            width: 30px;
        }
    </style>
    <style>
        .formErrorArrow {
            display: none !important;
        }

        .formErrorContent {
            display: none !important;
        }
    </style>

    <script>
        function validationMsg() {
            if (jQuery("#caseSeizureForm").validationEngine('validate') === false) {
                jQuery(".formErrorContent").css('display', 'none');
                jQuery(".formErrorArrow").css('display', 'none');
                alert("Please fill all the mandatory fields");
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function Validatearticle() {
            if (document.getElementById('<%=txtUserId.ClientID%>').value == '') {
                alert("Enter USER ID");
                document.getElementById("<%=txtUserId.ClientID%>").focus();
                return false;
            }


        }

    </script>

    <script language="javascript" type="text/javascript">
        function Validatearticle() {
            if (document.getElementById('<%=txtSMSOtp.ClientID%>').value == '') {
                alert("Enter OTP sent to phone");
                document.getElementById("<%=txtSMSOtp.ClientID%>").focus();
                return false;
            }

            if (document.getElementById('<%=txtEmailOtp.ClientID%>').value == '') {
                alert("Enter OTP sent to Email-Id");
                document.getElementById("<%=txtEmailOtp.ClientID%>").focus();
                return false;
            }


            if (document.getElementById('<%=txtNewPassword.ClientID%>').value == '') {
                alert("Enter New Password");
                document.getElementById("<%=txtNewPassword.ClientID%>").focus();
                return false;
            }

            if (document.getElementById('<%=txtConfirmPassword.ClientID%>').value == '') {
                alert("Enter Confirm Password");
                document.getElementById("<%=txtConfirmPassword.ClientID%>").focus();
                return false;
            }




        }

    </script>

    <script>
        $(document).ready(function () {
            if ('Yes' === 'Yes') {
                $("#articles_yes").show();
                $("#clear").show();
                $("#articles_no").hide();

            } else if ('Yes' === 'No') {
                $("#articles_yes").hide();
                $("#clear").hide();
                $("#articles_no").show();
            }
            $(".articleStatus").click(function () {
                if ($(".articleStatus:checked").val() === 'Yes') {
                    $("#articles_yes").show();
                    $("#clear").show();
                    $("#articles_no").hide();
                } else if ($(".articleStatus:checked").val() === 'No') {
                    $("#articles_yes").hide();
                    $("#clear").hide();
                    $("#articles_no").show();
                }
            });
            $("#hiddenCNo").hide();
            if ($(".recoveryComplaint:checked").val() === 'ICR') {
                $("#rbComplaint").show();
                $("#hiddenCNo").hide();
                //                        $("#personalOther_label").text("Name").hide();
                //$("#articles_no").hide();
            }
        });


    </script>
    <script>
        $(document).ready(function () {
            $("#hiddenCNo").hide();
            if ($(".recoveryComplaint:checked").val() === 'ICR') {
                $("#rbComplaint").show();
                $("#hiddenCNo").hide();
                $("#hiddenCNo").val("");
                $("#personalOther_label").text("Name").hide();
                //$("#articles_no").hide();
            } else if ($(".recoveryComplaint:checked").val() === 'PD') {
                $("#rbComplaint").hide();
                $("#hiddenCNo").show();
                $("#controlComplaintNo").val("");
                $("#personalOther_label").html("<span style='color:red'>*</span>Name").show();
            } else if ($(".recoveryComplaint:checked").val() === 'IOS') {
                $("#rbComplaint").hide();
                $("#hiddenCNo").show();
                $("#controlComplaintNo").val("");
                $("#personalOther_label").html("<span style='color:red'>*</span>Source Name").show();
            }

            $(".recoveryComplaint").click(function () {
                if ($(".recoveryComplaint:checked").val() === 'ICR') {
                    $("#rbComplaint").show();
                    $("#hiddenCNo").hide();
                    $("#personalOther_label").hide();
                    //$("#articles_no").hide();
                } else if ($(".recoveryComplaint:checked").val() === 'PD') {
                    $("#rbComplaint").hide();
                    $("#hiddenCNo").show();
                    $("#personalOther_label").html("<span style='color:red'>*</span>Name").show();
                } else if ($(".recoveryComplaint:checked").val() === 'IOS') {
                    $("#rbComplaint").hide();
                    $("#hiddenCNo").show();
                    $("#personalOther_label").html("<span style='color:red'>*</span>Source Name").show();
                }
            });
        });
    </script>
    <script>
        // WRITE THE VALIDATION SCRIPT.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))

                return false;


            return true;
        }
    </script>
    <script>
        var next = 1;
        $(document).on("click", ".add-more", function (e) {
            e.preventDefault();
            var addto = "#adiv" + next;
            next = next + 1;
            var newIn = '<div id="adiv' + (next) + '" class="col-md-4 col-sm-12 "><button type="button" style="display: inline!important;" id="remove' + (next) + '" class="btn btn-danger remove-me">-</button><input style="display: inline!important;margin-left: 5px;margin-right: 3px;" class="form-control" id="attachname' + next + '" name="attachname" type="text" onkeypress="return attachMand(\'field' + next + '\',this.id)" placeholder="Attachment Name" data-toggle="tooltip" data-placement="right" title="Attachment" ><input style="display: none;" id="field' + next + '" class="filecls" onkeypress="return attachMand(\'field\', this.id)" name="attachfield" type="file" onchange="readURL3(this);setAttachName(\'' + next + '\');" ><input type="button" id="btnb' + next + '" class="btn btn-primary" value="Browse..." onclick="browseFile(\'' + next + '\');" /><p id="field' + next + 'Error" style="color: red; font: bold; display: none; text-align: right;">Upload your valid attachment</p></div>';
            var newInput = $(newIn);
            $(addto).after(newInput);
            // $("#field" + next).attr('data-source', $(addto).attr('data-source'));
            $("#count").val(next);

        });

        $(document).on("click", '.remove-me', function (e) {
            var rmvId = '' + this.id;
            var fieldNum = rmvId.substring(6);
            var fieldID = "#field" + (fieldNum);
            var nameFieldID = "#attachname" + (fieldNum);
            var btnb = "#btnb" + (fieldNum);
            var errpgh = "#field" + (fieldNum) + "Error";
            var adiv = "#adiv" + (fieldNum);
            $(this).remove();
            $(fieldID).remove();
            $(nameFieldID).remove();
            $(btnb).remove();
            $(errpgh).remove();
            $(adiv).remove();
            next = next - 1;
        });

        function browseFile(slno) {
            document.getElementById('field' + slno).click();
        }

        function setAttachName(slno) {
            var filenm = document.getElementById('field' + slno).value;
            var filename = filenm.replace(/^.*[\\\/]/, '');
            var imgTextArr = filename.split(".");
            var imgTxtFTb = imgTextArr[0];
            if (parseInt(imgTxtFTb.length) > 45) {
                imgTxtFTb = imgTxtFTb.substring(0, 45);
            }
            document.getElementById('attachname' + slno).value = imgTxtFTb;

        }
    </script>

    <script type="text/javascript">

        function yesnoCheck() {
            if (document.getElementById('rdbBloodY').checked) {
                document.getElementById('txtAccusedBloodTestResult').disabled = false;
            } else if (document.getElementById('rdbBloodN').checked) {
                document.getElementById('txtAccusedBloodTestResult').disabled = true;
            }
        }

    </script>
    <script type="text/javascript">
        function addSection(sectionValue) {
            var list = $("#sectionList").val();
            if (sectionValue !== "") {
                var res = list.split(",");
                $.each(res, function (idx2, val2) {
                    if (sectionValue === val2.trim()) {
                        sectionValue = "";
                    }
                });
                if (sectionValue !== "") {
                    list = sectionValue + " , " + list;
                } else {
                    alert("This Section is already added");
                }
                $("#sectionList").val(list);
            } else {
                alert("Please enter section.")
            }
            $('#offencesc').val("");
            $('#applicableSection').val("");
        }

    </script>


    <%--Validate Excise Items--%>


    <%--Validate Save & Next--%>






    <link rel="shortcut icon" type="image/x-icon" href="gobsymbol.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>IEMS</title>

    <!-- chat style -->
    <link href="cssres/homestyle.css" rel="stylesheet" type="text/css" />
    <link href="cssres/chatstyle.css" rel="stylesheet" type="text/css" />

    <link href="common/theme/css/bootstrap.min.css" rel="stylesheet">

    <link href="common/theme/fonts/css/font-awesome.min.css" rel="stylesheet">
    <link href="common/theme/css/animate.min.css" rel="stylesheet">

    <!-- Custom styling plus plugins -->
    <link href="common/theme/css/custom.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="common/theme/css/maps/jquery-jvectormap-2.0.1.css" />
    <link href="common/theme/css/icheck/flat/green.css" rel="stylesheet" />
    <link href="common/theme/css/floatexamples.css" rel="stylesheet" type="text/css" />

    <script src="common/theme/js/jquery.min.js"></script>
    <script src="common/theme/js/nprogress.js"></script>

    <script src="common/theme/js/jquery.min.js"></script>
    <script src="common/theme/js/nprogress.js"></script>
    <script src="common/datepicker/jquery-1.10.2.js"></script>
    <script src="common/datepicker/jquery-ui.js"></script>
    <link rel="stylesheet" href="common/datepicker/jquery-ui.css">
    <script src="common/datepicker/MonthPicker.min.js"></script>
    <script src="common/datepicker/jquery.maskedinput.min.js"></script>
    <link rel="stylesheet" href="common/datepicker/test.css">

    <!-- Jquery Validation Engine-->
    <link rel="stylesheet" href="common/validate/validationEngine.jquery.css" type="text/css" />
    <script src="common/validate/jquery.validationEngine-en.js" type="text/javascript" charset="utf-8">
    </script>
    <script src="common/validate/jquery.validationEngine.js" type="text/javascript" charset="utf-8">
    </script>

    <link rel="stylesheet" type="text/css" href="common/Timepicker/jquery.timepicker.css" />
    <script type="text/javascript" src="common/Timepicker/jquery.timepicker.js"></script>

    <link href="common/css1/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="common/js/chosen1.5.1.jquery.min.js" type="text/javascript"></script>

    <script>

        NProgress.start();
        $(document).ready(function () {
            var role = '" + userid + "';
            if (role == 'Applicant') {//alert();
                $("#chat1").hide();
            }
            $(".right_col").click(function () {
                $(".child_menu").css("display", "none");
            });
        });
    </script>
    <script>

        NProgress.start();
        $(document).ready(function () {
            $('.main-chatbox').hide();
            $(".right_col").click(function () {
                $(".child_menu").css("display", "none");
            });

            $('#chat').hide();
            $("#runchat").click(function () {
                $('#chat').show();
            });




        });
        function showChatBox() {
            $('.main-chatbox').show();
            $('#onlineChatBox').show();

        }
    </script>
    <style>
        .nav_menu {
            /*//background-image:  url(common/theme/images/Webtemplate_03.png);*/
            background-repeat: no-repeat;
            background-color: #26b8b8;
            background-attachment: inherit;
            background-position: top center;
            background-size: cover;
            position: relative;
            height: 100px;
            border-bottom: 5px solid #ffb400;
        }

        .dropdown-menu {
            list-style: none;
            background-color: #ffffff;
            border: 1px solid #ccc;
            border: 1px solid rgba(0, 0, 0, 0.2);
            -webkit-border-radius: 5px;
            /*//-moz-border-radius: 5px;*/
            border-radius: 0.5em;
            -webkit-box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            /*//-moz-box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);*/
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            -webkit-background-clip: padding-box;
            /*//-moz-background-clip: padding;*/
            background-clip: padding-box;
            /*width: 300px;
                height: 200px;
                padding-top: 10px;
                padding-left: 10px;
                padding-right: 10px;*/
            margin-right: 5px;
            border-top-left-radius: 0.5em;
            border-top-right-radius: 0.5em;
        }

        .site_title p {
            width: 125px;
            margin-top: -85px;
            line-height: 0.9em;
            margin-left: 88px;
            text-align: center;
        }


        .fixed-footer {
            width: 100%;
            position: fixed;
            background: #333;
            padding: 10px 0;
            color: #fff;
        }

        .fixed-footer {
            bottom: 0;
        }

        /*chat box*/
        #oulist a {
            color: rgba(82, 179, 217, 0.9);
        }

        #chat1 {
            bottom: 50px;
            /*//float: left;*/
            right: 10px;
            position: fixed;
            z-index: 100001;
            color: #066061;
        }

        .fa-wechat:hover {
            color: #26b8b8;
        }

        .chat li:nth-child(2) {
            /*//margin-top: 60px;*/
        }

        #onlineChatBox {
            overflow-y: auto;
            height: 65%;
            width: 230px;
            background: #fff;
            margin-top: 67px;
            float: right;
            border-radius: 2px;
            margin-right: 0px;
            overflow-y: scroll;
            border: 1px solid #dbd8d8;
        }

        #chatbox {
            /*overflow-y: scroll; 
                height: auto; 
                width: 350px; 
                margin-right: 10px; 
                margin-top: 280px; 
                background: #e5e5e5; 
                float: right;
                bottom: 0;
                max-height: 200px;*/
            overflow-y: scroll;
            height: 38%;
            width: 333px;
            margin-right: 10px;
            /*//margin-top: 265px;*/
            background: rgb(229, 229, 229) none repeat scroll 0% 0%;
            float: right;
            display: block;
            position: fixed;
            /*//bottom: -325px;*/
            right: 225px;
            bottom: 60px;
        }

            #chatbox .menu1 {
                padding-top: 4px;
                padding-left: 10px;
                background-color: #26b8b8;
                position: fixed;
                width: 320px;
                margin-top: -2px;
            }

        .text_area {
            height: 100px;
            bottom: 60px;
            position: fixed;
            z-index: 9;
        }

        @media screen and (max-width: 480px) {
            .nav > li > a.dash_board {
                display: none;
            }

            .nav > li {
                width: fit-content;
                /*margin-left: 65px;*/
            }

            .open, .profile {
                margin-top: -90px;
            }

            .toggle {
                width: 50px;
                margin-top: 10px;
            }

            .switch_menu {
                margin-top: 12px;
                margin-left: 70px;
            }

            .top_nav .navbar-right li a {
                margin-top: -3px;
            }

            .site_title img {
                margin-top: 2px;
                margin-left: -17px;
            }

            #onlineChatBox {
                height: 200px;
                width: 230px;
            }

            #chatbox {
                width: 260px;
                margin-right: -210px;
                /*//margin-top: 10px;*/
                max-height: 210px;
                bottom: 105px;
            }

                #chatbox .menu1 {
                    width: 250px;
                }

            #chat1 {
                bottom: 100px;
                right: 20px;
            }

            .textarea {
                width: 190px;
                bottom: 0px;
                margin-left: 5px;
            }

            .sendbtn {
                position: absolute;
                padding-left: 197px;
                bottom: -3px;
            }

            .main-chatbox {
                height: 75%;
                margin-top: 25%;
            }

            .text_area {
                height: 100px;
                bottom: 110px;
                position: fixed;
            }
        }
    </style>

    <script>
        setTimeout(function () {
            $('#successMessage').fadeOut('fast');
        }, 5000);

    </script>

    <script>
        var websocket1 = new WebSocket("ws://" + window.location.host + "/Excise_ERP/OnlineUsers");
        var intervalID = setInterval(function () {
            // 	console.log("user"+userName);

            websocket1.send("Get Users");
        }, 3000);
        $(document).ready(
                function () {
                    // 				usersList();
                    document.getElementById("chatbox").style.display = 'none';

                    // 				$(".menu1").click(function(){
                    // 					websocket1.send("click...");
                    // 				}); 

                });

        websocket1.onmessage = function processMessage(message) {
            var jsonData = JSON.parse(message.data);
            getOnlineUsers(jsonData.message);
        }
    </script>
    <script type="text/javascript">
        var websocket = new WebSocket("ws://" + window.location.host
                + "/Excise_ERP/ChatSrv");
        websocket.onmessage = function processMessage(message) {
            var jsonData = JSON.parse(message.data);
            var msgData;
            if (jsonData.message != null) {
                msgData = jsonData.message;
                var res = msgData.split(":-");
                var today = new Date();
                var time = today.getHours() + ":" + today.getMinutes() + ":"
                        + today.getSeconds();
                var cuser = document.getElementById("cuser").value;
                if (res[0] === cuser || res[0] == cuser) {
                    $("#chatboxol")
                            .append(
                                    '<li class="self"><div class="avatar"><img src="img/HYcn9xO.png" draggable="false" /></div><div class="msg"><p style="font-weight: bold;">'
                                    + res[0]
                                    + '...</p><p style="word-wrap: break-word;">'
                                    + res[1]
                                    + '</p><time>'
                                    + time
                                    + '</time></div></li>');
                } else {
                    $("#chatboxol")
                            .append(
                                    '<li class="other"><div class="avatar"><img src="img/DY6gND0.png" draggable="false" /></div><div class="msg"><p style="font-weight: bold;">'
                                    + res[0]
                                    + '!</p><p style="word-wrap: break-word;">'
                                    + res[1]
                                    + '</p><time>'
                                    + time
                                    + '</time></div></li>');
                    document.getElementById("chatwith").innerHTML = res[0];
                    $(".main-chatbox").show();
                    $("#chatbox").show();
                }
                var objDiv = document.getElementById("chatbox");
                objDiv.scrollTop = objDiv.scrollHeight;
            }
        }

        function closeChatBox(divid) {
            document.getElementById(divid).style.display = 'none';
        }

        function getOnlineUsers(userlist) {
            document.getElementById("oulist").innerHTML = '';
            var userlistArr = userlist.split(":-");
            var text;
            var i;
            for (i = 0; i < userlistArr.length; i++) {
                var cuser = document.getElementById("cuser").value;
                // 			alert(userlistArr[i]);
                if (cuser != userlistArr[i] || cuser !== userlistArr[i]) {
                    if (text) {
                        text += "<img src='img/ezgif.com-crop.gif' draggable='false' style='width: 16px;height: 15px;margin-left: 20px;'/>&nbsp&nbsp&nbsp<a id='"
                                + userlistArr[i]
                                + "' style='cursor:pointer;' onclick='openChat(this);'>"
                                + userlistArr[i] + "</a><br><br>";
                    } else {
                        text = "<img src='img/ezgif.com-crop.gif' draggable='false' style='width: 16px;height: 15px;margin-left: 20px;'/>&nbsp&nbsp&nbsp<a id='"
                                + userlistArr[i]
                                + "' style='cursor:pointer;' onclick='openChat(this);'>"
                                + userlistArr[i] + "</a><br><br>";
                    }
                }
            }
            if (text) {
                document.getElementById("oulist").innerHTML = text;
            }
        }
        function openChat(obj) {
            var cuser = document.getElementById("cuser").value;
            var chatWith = obj.id;
            document.getElementById("chatwith").innerHTML = chatWith;
            document.getElementById("chatbox").style.display = 'block';
        }

        function sendMsg() {
            var msgTxt = sendtext.value;
            // 		websocket.send(msgTxt);
            var cuser = document.getElementById("cuser").value;
            if (msgTxt.trim() != "") {
                var chatwith = document.getElementById("chatwith").innerHTML;
                websocket.send(JSON.stringify({
                    from: cuser,
                    to: chatwith,
                    data: msgTxt
                }));
                sendtext.value = "";
            }

        }
    </script>

    <script type="text/javascript">

        function yesnoChecko() {
            if (document.getElementById('rdbOtherInfoY').checked) {
                document.getElementById('Div2').style.visibility = 'visible';
                document.getElementById('btnSkip').style.visibility = 'hidden'
                document.getElementById('btnSubmit').style.visibility = 'visible'
            } else {
                document.getElementById('Div2').style.visibility = 'hidden';
                document.getElementById('btnSkip').style.visibility = 'visible'
                document.getElementById('btnSubmit').style.visibility = 'hidden'
            }
        }

    </script>




</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container body">
            <div class="main_container">
                <div class="col-md-3 left_col">
                    <div class="left_col scroll-view">
                        <div class="navbar nav_title" style="border: 0;">
                            <a href="login/fdashboard.htm?orgCode=8-Case Management" class="site_title">
                                <img style=""
                                    src="common/theme/assets/img/Logo_circle.png" alt="" />
                                <br />
                                <p style="width: 100px;">
                                    <span style="font-size: 13px"><b>Integrated Excise Management System</b></span>
                                </p>
                            </a>


                        </div>
                        <div class="clearfix"></div>

                        <br />

                        <!-- sidebar menu -->
                        <div id="sidebar-menu" class="main_menu_side hidden-print main_menu" style="margin-top: 23px;">

                            <div class="menu_section">
                                <ul class="nav side-menu">
                                    <!--                            
                                
                                    <li><a href="Case/seizureList.htm">Seizure List<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu" style="display: block">
                                    
                                        
                                            <li><a href="Case/seizureList.htm">Seizure List</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/seizureForm.htm">Add Seizure</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/articleCatagoryList.htm  ">Case Management Master</a></li>
                                        
                                    
                            </ul>
                        </li>
                                
                                    <li><a href="Case/seizureForm.htm">Add Seizure<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu" style="display: block">
                                    
                                        
                                            <li><a href="Case/seizureList.htm">Seizure List</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/seizureForm.htm">Add Seizure</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/articleCatagoryList.htm  ">Case Management Master</a></li>
                                        
                                    
                            </ul>
                        </li>
                                
                                    <li><a href="Case/articleCatagoryList.htm  ">Case Management Master<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu" style="display: block">
                                    
                                        
                                            <li><a href="Case/seizureList.htm">Seizure List</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/seizureForm.htm">Add Seizure</a></li>
                                        
                                    
                                        
                                            <li><a href="Case/articleCatagoryList.htm  ">Case Management Master</a></li>
                                        
                                    
                            </ul>
                        </li>
                                    
                              -->





                                </ul>
                                </li>


                                </ul>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="top_nav" style="font-size: 15px;">

                    <div class="nav_menu">
                        <nav class="" role="navigation" style="margin-top: 30px;">
                            <div class="nav toggle">
                                <a id="menu_toggle"><i class="fa fa-bars"></i></a>
                            </div>
                            <ul class="nav navbar-nav" style="margin-left: -25px;">
                                <%--<li class="switch_menu">
                            <a href="login/selectDivision.htm" >Switch Menu </a>
                        </li>--%>

                                <!--                        <li>
                                                     
                                                    <img src="common/theme/images/logo_1.png" alt=""/>
                                                </li>-->
                            </ul>



                        </nav>
                    </div>

                </div>

                <%--        <a class="" onclick="showChatBox()"  id="chat1" style=""><i class="fa fa-wechat" style="font-size: 50px;"></i></a>--%>
                <div class="fixed-footer" style="z-index: 11;">
                    <div class="loginfooter" style="text-align: center;">





                        <p style="color: #FFF; float: left; margin-left: 10px; font-size: 14px;">© Department of Prohibition, Excise and Registration , Govt. of Bihar</p>
                        <p style="color: #FFF; float: right; margin-right: 10px; font-size: 14px;">Designed, Developed and Maintained by KEONICS</p>
                    </div>
                </div>

                <div class="main-chatbox" style="display: none">
                    <div id="onlineChatBox"
                        style="">
                        <div class="menu1" style="padding-top: 4px; padding-left: 10px;">
                            <span style="color: white; font-weight: bold;">Online User List</span>
                            <img
                                src="img/40-delete-button.png" onclick="closeChatBox('onlineChatBox');"
                                style="width: 16px; height: 20px; float: right; margin-right: 5px; margin-top: 2px; cursor: pointer;" />
                        </div>
                        <div id="oulist" style="margin-top: 7px;">
                        </div>
                    </div>

                    <div id="chatbox"
                        style="">
                        <ol class="chat" id="chatboxol">
                            <div class="menu1" style="">
                                <span id="chatwith" style="color: white;"></span>
                                <img
                                    src="img/40-delete-button.png" onclick="closeChatBox('chatbox');"
                                    style="width: 16px; height: 20px; float: right; margin-right: 5px; margin-top: 2px; cursor: pointer;" />
                            </div>
                        </ol>

                        <div class="text_area" style="">
                            <div class=""></div>
                            <!-- 			<input class="textarea" type="text" rows="1" placeholder="Type here!" /> -->
                            <textarea class="textarea" id="sendtext" rows="1" cols="30"
                                placeholder="Type here...!"></textarea>
                            <div class="sendbtn">
                                <img src="img/sendbtn.png"
                                    style="height: 53px; width: 53px; cursor: pointer"
                                    onclick="sendMsg();" />
                            </div>

                        </div>

                    </div>

                    <script type="text/javascript">

                        function sourcename() {
                            if (document.getElementById('rdbPersonally').checked) {
                                document.getElementById('lblSourceName').innerText = '* Name'
                                document.getElementById('lblSourceName').style.color = "Red"

                            }

                            else if (document.getElementById('rdbControlRoom').checked) {
                                document.getElementById('lblSourceName').innerText = '* Control Room Complaint No.';
                            }

                            else if (document.getElementById('rdbOtherSources').checked) {
                                document.getElementById('lblSourceName').innerText = '* Source Name';
                            }
                        }

                    </script>

                </div>
                <input type="hidden" id="cuser" value="Superuser" />

                <script src="common/theme/js/bootstrap.min.js"></script>

                <!-- gauge js -->
                <script type="text/javascript" src="common/theme/js/gauge/gauge.min.js"></script>
                <script type="text/javascript" src="common/theme/js/gauge/gauge_demo.js"></script>
                <!-- chart js -->
                <script src="common/theme/js/chartjs/chart.min.js"></script>
                <!-- bootstrap progress js -->
                <script src="common/theme/js/progressbar/bootstrap-progressbar.min.js"></script>
                <script src="common/theme/js/nicescroll/jquery.nicescroll.min.js"></script>
                <!-- icheck -->
                <script src="common/theme/js/icheck/icheck.min.js"></script>
                <!-- daterangepicker -->
                <script type="text/javascript" src="common/theme/js/moment.min.js"></script>
                <script type="text/javascript" src="common/theme/js/datepicker/daterangepicker.js"></script>

                <script src="common/theme/js/custom.js"></script>

                <!-- flot js -->
                <!--[if lte IE 8]><script type="text/javascript" src="common/theme/js/excanvas.min.js"></script><![endif]-->
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.pie.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.orderBars.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.time.min.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/date.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.spline.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.stack.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/curvedLines.js"></script>
                <script type="text/javascript" src="common/theme/js/flot/jquery.flot.resize.js"></script>

                <script type="text/javascript" src="common/theme/js/maps/jquery-jvectormap-2.0.1.min.js"></script>
                <script type="text/javascript" src="common/theme/js/maps/gdp-data.js"></script>
                <script type="text/javascript" src="common/theme/js/maps/jquery-jvectormap-world-mill-en.js"></script>
                <script type="text/javascript" src="common/theme/js/maps/jquery-jvectormap-us-aea-en.js"></script>


                <div class="right_col" role="main">
                    <div class="">
                        <div class="clearfix"></div>
                        <div class="x_panel">





                            <!DOCTYPE html>



                            <div class="x_title">
                                <h2>FORGOT PASSWORD</h2>
                                <div class="clearfix"></div>
                            </div>



                            <div class="x_content">






                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" runat="server" visible="true" id="lblUserId"><span style="color: red">*</span>Enter User Id </label>
                                    <input type="text" id="txtUserId" runat="server" class="form-control" name="size">
                                    &nbsp;<asp:LinkButton ID="btnGenerateOtp" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnOtp_Click">Generate OTP
                                            <span aria-hidden="true" class="fa fa-spin"> </span></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="btnBack" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="btnBack_Click">Back
                                            <span aria-hidden="true" class="fa fa-spin"> </span></asp:LinkButton>
                                </div>




                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" runat="server" id="lblSMSOtp" visible="false"><span style="color: red">*</span>Enter OTP sent to phone </label>
                                    <input type="text" id="txtSMSOtp" runat="server" class="form-control" name="size" visible="false">
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" runat="server" id="lblEmailOtp" visible="false"><span style="color: red">*</span>Enter OTP sent to Email-Id </label>
                                    <input type="text" id="txtEmailOtp" runat="server" class="form-control" name="size" visible="false">
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>

                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" runat="server" id="lblNewPassword" visible="false"><span style="color: red">*</span>Enter New Password </label>
                                    <input type="password" id="txtNewPassword" runat="server" class="form-control" name="size" visible="false">
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <label class="control-label" runat="server" id="lblConfirmPassword" visible="false"><span style="color: red">*</span>Confirm New Password </label>
                                    <input type="password" id="txtConfirmPassword" runat="server" class="form-control" name="size" visible="false">
                                </div>
                                <div class="clearfix"></div>
                                <p>&nbsp;</p>


                                <div class="clearfix"></div>
                                <p>&nbsp;</p>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-inline">
                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:return Validatearticle1();" Visible="false" class="btn btn-primary pull-left" OnClick="btnSubmit_Click">
                                                    <span aria-hidden="true" class="fa fa-plus-circle"> </span>Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Visible="false" CssClass="btn btn-danger pull-left" OnClick="btnCancel_Click">Cancel
                                            <span aria-hidden="true" class="fa fa-cut"> </span></asp:LinkButton>
                                    <asp:LinkButton ID="btnLogin" runat="server" Visible="false" CssClass="btn btn-danger pull-left" OnClick="btnLogin_Click">Click here to login
                                            <span aria-hidden="true" class="fa fa-cut"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12 ">
                                    <input type="hidden" name="seizureNo" value="">
                                    <input type="hidden" name="sNoCaseSeizure" id="seizureSrNo" value="">



                                    <hr />
                                    <%--<div style="float: right; width: 500px;">Search all columns:<asp:TextBox ID="txtSearch" runat="server"   AutoPostBack="true" onkeyup="__doPostBack(this.name,'OnKeyPress');"></asp:TextBox></div> </div>--%>


                                    <div class="x_content">
                                        <asp:GridView ID="grdArticleList" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                            OnPageIndexChanging="grdArticleList_PageIndexChanging" PagerStyle-ForeColor="Black" PagerStyle-Font-Underline="true"
                                            PagerStyle-HorizontalAlign="Center"
                                            class="table table-striped responsive-utilities jambo_table" Style="overflow-y: scroll;"
                                            HeaderStyle-BackColor="#26b8b8" RowStyle-BackColor="Window"
                                            HeaderStyle-ForeColor="#ECF0F1" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdArticleList_RowCommand">

                                            <Columns>






                                                <%--<asp:TemplateField HeaderText="Article No." ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleNo" runat="server" Text='<%#Eval("sr_no_artical_catagory_master") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>

                                                    <ItemStyle Font-Bold="True" Width="5px"></ItemStyle>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Article Category Name"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleName" runat="server" Text='<%#Eval("artical_catagory_name") %>'></asp:Label>
                                                        <%--<asp:LinkButton ID="lblArticleName"     OnClick="lblSeizureNo_Click1"
                                                                runat="server"
                                                                 CausesValidation="False" 
                                                                 Text='<%#Eval("artical_catagory_name") %>'></asp:LinkButton>--%>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>

                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Sub-Category Name"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubArticleName" runat="server" Text='<%#Eval("sub_cat_name") %>'></asp:Label>
                                                        <%--<asp:LinkButton ID="lblArticleName"     OnClick="lblSeizureNo_Click1"
                                                                runat="server"
                                                                 CausesValidation="False" 
                                                                 Text='<%#Eval("artical_catagory_name") %>'></asp:LinkButton>--%>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>

                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Brand"
                                                    ItemStyle-Font-Bold="true" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArticleBrand" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                        <%--<asp:LinkButton ID="lblArticleName"     OnClick="lblSeizureNo_Click1"
                                                                runat="server"
                                                                 CausesValidation="False" 
                                                                 Text='<%#Eval("artical_catagory_name") %>'></asp:LinkButton>--%>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>

                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
                                                        <%--<asp:LinkButton ID="lblFIR" runat="server" CausesValidation="False" Text='<%#Eval("status") %>' 
                                                                PostBackUrl= '<%# Eval("seizuretempno","~/AddFIR.aspx?seizuretempno={0}") %>'></asp:LinkButton>--%>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="False"></HeaderStyle>

                                                    <ItemStyle Font-Bold="True" Width="40px"></ItemStyle>
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true"
                                                    ItemStyle-Width="90px" HeaderStyle-Font-Underline="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"
                                                            CssClass="myButton" CommandArgument='<%# Bind("s_no_article") %>' CommandName="View"><i class="fa fa-search-plus"  ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server"
                                                            CssClass="myButton1" CommandArgument='<%# Bind("s_no_article") %>' CommandName="Editt"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                             
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>

                                            <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" HorizontalAlign="Center"></HeaderStyle>

                                            <RowStyle BackColor="Window"></RowStyle>
                                        </asp:GridView>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>

                    <div class="col-md-12 col-sm-12 col-xs-12 form-inline">

                        <%-- <button type="submit" class="btn btn-primary pull-right" id="articles_yes" onclick="validationMsg();
                                                    return checkGrid();">--%>

                        <%--<button type="submit" class="btn btn-primary pull-right" id="articles_no" onclick="validationMsg();">Save & Next</button>--%>
                    </div>
                </div>



            </div>
        </div>
        </div>
                </div>
            </div>
        </div>
        <script>

            function removefile(filename) {
                var seizureSrNo = document.getElementById("seizureSrNo").value;
                var t = confirm("Are you sure want to delete this file   " + filename);
                if (t == true) {
                    $.ajax
                            ({
                                type: "POST",
                                url: 'Case/delDoc.htm',
                                data: {
                                    filename: filename,
                                    seizureSrNo: seizureSrNo
                                },
                                success: function (data) {
                                    alert("file deleted");
                                    window.location.reload();
                                    return false;
                                }
                            });
                } else {
                    //alert("u not agreed!!! "+filename);
                }
            }


        </script>
        <script>
            NProgress.done();
            $(document).ready(function () {
                jQuery("#caseSeizureForm").validationEngine();
                var timepicker = new TimePicker('time', {
                    lang: 'en',
                    theme: 'dark'
                });
                timepicker.on('change', function (evt) {

                    var value = (evt.hour || '00') + ':' + (evt.minute || '00');
                    evt.element.value = value;

                });
            });
            function isNumberKey(evt) {

                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    alert("Please Enter Numeric value");
                    return false;
                }


                return true;

            }
            function nameValidate(name, id) {
                var letterRegex = /^[a-zA-Z_-\s]+$/;

                if (!letterRegex.test(name)) {
                    alert("Letters only");
                    $("#" + id).val("");
                    return false;
                }

            }
            function nameAlphanumeric(name, id) {
                var letterRegex = /^[A-Za-z0-9]+$/;

                if (!letterRegex.test(name)) {
                    alert("Please enter alphanumeric's only");
                    $("#" + id).val("");
                    return false;
                }

            }
            $("#division").change(function () {
                $("#district").empty();
                var division = document.getElementById("division").value;
                var divcode = division.split("@");
                $.ajax
                        ({
                            type: "POST",
                            url: 'ExciseAutoMationReport/getDistrict.htm',
                            data: {
                                division: divcode[0]
                            },
                            success: function (data) {
                                data = data.split(",");
                                var len = data.length;
                                $("#district").append('<option value="">Select an option</option>');
                                if (data != 'a') {
                                    for (var i = 0; i <= (len - 2) ; i++) {
                                        var id = data[i];
                                        var name = data[i + 1];
                                        i += 1;
                                        $("#district").append("<option value='" + id + "'>" + name + "</option>");

                                    }
                                }
                            }
                        });
            });

            $("#district").change(function () {
                $("#thana").empty();
                var district = document.getElementById("district").value;
                $.ajax
                        ({
                            type: "POST",
                            url: 'CSML/getThana.htm',
                            data: {
                                district: district
                            },
                            success: function (data) {
                                data = data.split(",");
                                var len = data.length;
                                $("#thana").append('<option value="">Select An Option</option>');

                                if (data != 'a') {
                                    for (var i = 0; i <= (len - 2) ; i++) {
                                        var id = data[i];
                                        var name = data[i + 1];
                                        i += 1;
                                        $("#thana").append("<option value='" + id + "'>" + name + "</option>");

                                    }
                                }
                            }
                        });
            });
            function findSubCategory(category, subCatId) {
                $("#productSubCategory").empty();
                $("#productName").empty();
                //                var category = document.getElementById("productCategory").value;
                $.ajax
                        ({
                            type: "POST",
                            url: 'Case/getSubCategory.htm',
                            data: {
                                category: category
                            },
                            success: function (data) {
                                data = data.split(",");
                                var len = data.length;
                                $("#productSubCategory").append('<option value="">Select An Option</option>');
                                $("#productName").append('<option value="">Select An Option</option>');
                                if (data !== 'a') {
                                    jQuery.each(data, function (i, val) {
                                        var data2 = val.split("+");
                                        var len2 = data2.length;
                                        if (data2 !== "" && len2 > 0) {
                                            for (var j = 0; j <= (len2 - 2) ; j++) {
                                                var id = data2[j + 1];
                                                var name = data2[j];
                                                i += 1;
                                                if (subCatId !== null) {
                                                    if (subCatId === id) {
                                                        $("#productSubCategory").append("<option value='" + id + "' selected=''>" + name + "</option>");
                                                    } else {
                                                        $("#productSubCategory").append("<option value='" + id + "'>" + name + "</option>");
                                                    }
                                                } else {
                                                    $("#productSubCategory").append("<option value='" + id + "'>" + name + "</option>");
                                                }
                                            }
                                        }
                                    });
                                }
                            }
                        });
            }
            function findArtName(productSubCategory, articleId) {
                $("#productName").empty();
                //                var productSubCategory = document.getElementById("productSubCategory").value;
                $.ajax
                        ({
                            type: "POST",
                            url: 'Case/getProductName.htm',
                            data: {
                                productSubCategory: productSubCategory
                            },
                            success: function (data) {
                                data = data.split(",");
                                var len = data.length;
                                $("#productName").append('<option value="">Select An Option</option>');

                                if (data !== 'a') {
                                    jQuery.each(data, function (i, val) {
                                        var data2 = val.split("+");
                                        var len2 = data2.length;
                                        if (data2 !== "" && len2 > 0) {
                                            for (var j = 0; j <= (len2 - 2) ; j++) {
                                                var id = data2[j + 1];
                                                var name = data2[j];
                                                i += 1;
                                                if (articleId !== null) {
                                                    if (articleId === id) {
                                                        $("#productName").append("<option value='" + id + "' selected=''>" + name + "</option>");
                                                    } else {
                                                        $("#productName").append("<option value='" + id + "'>" + name + "</option>");
                                                    }
                                                } else {
                                                    $("#productName").append("<option value='" + id + "'>" + name + "</option>");
                                                }
                                            }
                                        }
                                    });
                                }
                            }
                        });
            }
            //            $("#productSubCategory").change(function () {
            //               
            //            });

            function ComplaintNoCheck() {

                var controlComplaintNo = document.getElementById("controlComplaintNo").value;
                if (controlComplaintNo !== '' && controlComplaintNo !== null) {
                    $.ajax({
                        type: "POST",
                        url: 'Case/complaintNoCheck.htm',
                        data: {
                            controlComplaintNo: controlComplaintNo
                        },
                        success: function (data) {

                            if ($.trim(data) == 'Already Exists.Enter another') {
                                alert("Control Room Complaint Number is Already Exist");
                                document.getElementById("controlComplaintNo").value = "";
                                return false;
                            }

                        }
                    });
                }
            }


            function ManualBookNoCheck() {
                var manualBookNo = document.getElementById("manualBookNo").value;
                $.ajax
                        ({
                            type: "POST",
                            url: 'Case/manualBookNoCheck.htm',
                            data: {
                                manualBookNo: manualBookNo
                            },
                            success: function (data) {

                                if ($.trim(data) == 'Already Exists.Enter another') {
                                    alert("Manual Book Seizure List Number is Already Exist");
                                    document.getElementById("manualBookNo").value = "";
                                    return false;
                                }

                            }
                        });
            }
            $(document).ready(function () {
                var district = document.getElementById("district").value;

                $.ajax
                        ({
                            type: "POST",
                            url: 'CSML/getThana.htm',
                            data: {
                                district: district
                            },
                            success: function (data) {
                                data = data.split(",");
                                var len = data.length;
                                $("#thana").append('<option value="">Select An Option</option>');

                                if (data != 'a') {
                                    for (var i = 0; i <= (len - 2) ; i++) {
                                        var id = data[i];
                                        var name = data[i + 1];
                                        i += 1;
                                        $("#thana").append("<option value='" + id + "'>" + name + "</option>");

                                    }
                                }
                            }
                        });
            });
        </script>
    </form>
</body>
</html>
