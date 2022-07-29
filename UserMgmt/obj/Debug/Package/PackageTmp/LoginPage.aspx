<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="UserMgmt.LoginPage" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>IEMS</title>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <link href="common/sc.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="common/theme/css2/style.css">
    <link rel="stylesheet" href="common/theme/css2/bjqs.css">
    <link href="common/theme/css/bootstrap.min.css" rel="stylesheet">
    <!-- some pretty fonts for this demo page - not required for the slider -->
    <link href='http://fonts.googleapis.com/css?family=Source+Code+Pro|Open+Sans:300'
        integrity="sha384-DhDFXnFkhz4HJ28nHl/bKjJbJuMy/tn6c6a4n5GC41x92cyJ9x7cN7lgI3QP7hK6" crossorigin="anonymous"
        rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="common/theme/css2/demo.css">

    <!-- load jQuery and the plugin -->
    <script src="http://code.jquery.com/jquery-1.7.1.min.js"
        integrity="sha384-npxfGiG5C/F5X72RqcKFYSfzr1AXsDiu1YC/ydsOrS+jL554Jh4zFAx9GpQi4lXQ" crossorigin="anonymous"></script>
    <script src="common/theme/js2/bjqs-1.3.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css"
        integrity="sha384-i/ZLCOBtDmoxztrtShNvc3vGe1+IbOGDzkZNC4KLXurv/BT7QInnM2AsPnvbgXH/" crossorigin="anonymous">
    <link rel='stylesheet prefetch' href='https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css'
        integrity="sha384-yNuQMX46Gcak2eQsUzmBYgJ3eBeWYNKhnjyiBqLd1vvtE9kuMtgw6bjwN8J0JauQ" crossorigin="anonymous">
    <link rel="stylesheet" href="css/style.css">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css"
        integrity="sha384-i/ZLCOBtDmoxztrtShNvc3vGe1+IbOGDzkZNC4KLXurv/BT7QInnM2AsPnvbgXH/" crossorigin="anonymous">

    <link rel='stylesheet prefetch' href='https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css'
        integrity="sha384-yNuQMX46Gcak2eQsUzmBYgJ3eBeWYNKhnjyiBqLd1vvtE9kuMtgw6bjwN8J0JauQ" crossorigin="anonymous">
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'
        integrity="sha384-CgeP3wqr9h5YanePjYLENwCTSSEz42NJkbFpAFgHWQz7u3Zk8D00752ScNpXqGjS" crossorigin="anonymous"></script>
    <script src="common/theme/js_demo/index.js"></script>
    <link rel="stylesheet" href="common/theme/css_demo/style.css">
    <link rel="stylesheet" href="common/theme/scss_demo/style.css">
    <link rel="stylesheet" href="css/responsive.css">


    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500"
        integrity="sha384-CgeP3wqr9h5YanePjYLENwCTSSEz42NJkbFpAFgHWQz7u3Zk8D00752ScNpXqGjS" crossorigin="anonymous">
    <link rel="stylesheet" href="common/theme/assets/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="common/theme/assets/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="common/theme/assets/css/form-elements.css">
    <link rel="stylesheet" href="common/theme/assets/css/style.css">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"
        integrity="sha384-nvAa0+6Qg9clwYCGGPpDQLVpLNn0fRaROjHqs13t4Ggj3Ez50XnGQqc/r8MhnRDZ" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <!-- Jquery Validation Engine-->
    <link rel="stylesheet" href="common/validate/validationEngine.jquery.css" type="text/css" />
    <script src="common/validate/jquery.validationEngine-en.js" type="text/javascript" charset="utf-8">
    </script>
    <script src="common/validate/jquery.validationEngine.js" type="text/javascript" charset="utf-8">
    </script>


    <script type="text/javascript">
        (function (global) {

            if (typeof (global) === "undefined") {
                throw new Error("window is undefined");
            }

            var _hash = "!";
            var noBackPlease = function () {
                global.location.href += "#";

                // making sure we have the fruit available for juice (^__^)
                global.setTimeout(function () {
                    global.location.href += "!";
                }, 50);
            };

            global.onhashchange = function () {
                if (global.location.hash !== _hash) {
                    global.location.hash = _hash;
                }
            };

            global.onload = function () {
                noBackPlease();

                // disables backspace on page except on input fields and textarea..
                document.body.onkeydown = function (e) {
                    var elm = e.target.nodeName.toLowerCase();
                    if (e.which === 8 && (elm !== 'input' && elm !== 'textarea')) {
                        e.preventDefault();
                    }
                    // stopping event bubbling up the DOM tree..
                    e.stopPropagation();
                };
            }

        })(window);

        $(document).ready(function () {
            $("#myTab a").click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });

            $('#forgot').validationEngine();
        });
    </script>

    <link rel="shortcut icon" href="common/theme/assets/ico/fevicon.png">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="common/theme/assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="common/theme/assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="common/theme/assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="common/theme/assets/ico/apple-touch-icon-57-precomposed.png">
</head>
<style>
    html, body {
        width: 100%;
        height: 100%;
        margin: 0px;
        padding: 0px;
        overflow-x: hidden;
    }

    .top-content {
        background-image: url("common/theme/assets/img/backgrounds/back_ground_ A.jpg");
        height: -webkit-fill-available;
        height: 130%;
        background-size: cover;
        background-position: center;
        background-repeat: no-repeat;
    }

    body {
        overflow: hidden;
    }

    .middle-border {
        border-right: 0px solid #19b9e7;
    }

    #header-bg {
        background-image: url(common/theme/images/Webtemplate_03.png);
        background-repeat: no-repeat;
        background-repeat: no-repeat;
        background-color: #26b8b8;
        background-attachment: inherit;
        background-position: top center;
        background-size: cover;
        position: relative;
    }

    .log {
        float: left;
        position: relative;
        margin-top: -104px;
    }

    @media screen and (max-width: 480px) {

        .nav > li > a {
            position: relative;
            display: block;
            padding: 5px 6px;
            text-align: center;
        }

        .form-top {
            font-size: 11px;
        }

        #form_id {
            margin-top: -56px;
        }

        #form_id2 {
            margin-top: -55px;
        }

        .loginfooter {
            /*//margin-top: -82px;*/
        }

        body {
            overflow: scroll;
        }

        .logo_img {
            display: none;
        }

        .log {
            display: none;
            display: block;
            margin-top: -67px;
            margin-left: 20px;
            margin-bottom: 30px;
        }

        input[type="button"], input[type="button"] {
            font-size: 15px;
            float: right;
            position: relative;
            top: -34px;
        }

        .loginfooter p {
            font-size: 10px;
            line-height: 1em;
        }

        .compatibility {
            text-align: start;
            color: #252727;
            font-size: 12px;
        }
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
    /* .fixed-header{
             width: 100%;
             position: fixed;
             padding: 10px 0;
         }*/
    .fixed-header {
        top: 0;
    }

    .compatibility {
        text-align: start;
        color: #252727;
    }

        .compatibility a {
            text-align: start;
            color: #252727;
            text-decoration: underline;
        }
</style>
<body>

    <div class="fixed-header">
        <div class="loginheader" id="header-bg" align="center" style="background-image: url(common/theme/images/Webtemplate_03.png);">
            <div class="btn_img" style="margin-right: 40px;">
                <input type="button" class="btn btn-info" style="background-color: #ffb400; margin: 0 0 0 80%; margin-top: 34px;" value="Visit Website" onclick="window.open('http://117.254.110.7');">
            </div>
            <div class="logo_img" style="">
                <img src="common/theme/images/keonics-logo (1).png" style="margin-top: -34px; float: right; width: 90px; margin-right: 10px;">
            </div>

        </div>
    </div>

    <div class="log" style="">
        <img style="float: right; margin-right: 23px; position: relative; margin-top: -2px;" src="common/theme/images/Logo_d-1.png">
    </div>


    <div class="top-content">

        <div class="inner-bg">

            <div class="container">



                <div class="row">
                    <div class="col-sm-5">

                        <div class="form-box" id="form_id">
                            <div class="form-top">
                                <ul class="nav nav-tabs" id="myTab">
                                    <li class="active" style="width: 100%;"><a data-toggle="tab" href="#Login">Login</a></li>

                                    
                                </ul>

                            </div>
                            <div class="tab-content">
                                <div id="Login" class="tab-pane fade in active">
                                    <div class="form-bottom">
                                        <form runat="server" id="form1">
                                            <div class="form-group">
                                                <label class="sr-only" for="form-username">Username</label>
                                                <%--<input type="text" name="userId" placeholder="Username" class="form-username form-control" id="username" runat="server">--%>
                                                <input type="text" name="userId" placeholder="Username" autocomplete="off" class="form-username form-control" id="txtusername" runat="server"/> 
                                            </div>
                                            <div class="form-group">
                                                <label class="sr-only" for="form-password">Password</label>
                                                <%--<input type="password" name="password" placeholder="Password" autocomplete="off" class="form-password form-control" id="userpassword" runat="server">--%>
                                                <%--<input type="password" name="password" placeholder="Password" autocomplete="off" class="form-password form-control" id="Password1" runat="server">--%>
                                                <input type="password" name="password" placeholder="Password" autocomplete="off" class="form-password form-control" id="txtuserpassword" runat="server"/> 
                                            </div>

                                            <div class="form-group">

                                                <input type="hidden" name="active_status">
                                            </div>

                                            <div>
                                                <!--                                                   <div id="recaptcha" class="g-recaptcha" data-sitekey="6LcxaD8UAAAAALDM1Os3BqCYjYJMApQItQ2_20_-"></div>-->


                                            </div>
                                            <div id="cs1" runat="server"></div>

                                            <span id="recaptchaerr" style="color: rgb(242, 115, 142);">
                                                <asp:Label runat="server" ID="loginerror" Visible="false"></asp:Label></span>
                                            <div style="color: rgb(242, 115, 142);"></div>

                                            <!--                                                                                             
                                                <div style="color:rgb(242, 115, 142);"></div>                                               
-->
                                            <button type="submit" class="btn btn-primary" runat="server" onserverclick="Unnamed_ServerClick">Sign In</button><br /><br />
                                            <button type="submit" class="btn btn-primary" id="btnForgotPassword" runat="server" onserverclick="btnForgotPassword_ServerClick">Change / Forgot Password</button>
                                        </form>
                                    </div>
                                </div>

                                <div id="ForgotPassword" class="tab-pane fade" runat="server" visible="false">
                                    <div class="form-bottom">
                                        <form role="form" id="forgot" action="login/recoveryPassword.htm" method="post" class="registration-form">
                                            <div class="form-group">
                                                <label class="sr-only" for="form-first-name">Enter Valid User ID:</label>
                                                <input type="text" name="userId" id="userId" autocomplete="off" placeholder="Enter Valid User Id" class="form-email-addr form-control" id="form-email-addr">
                                            </div>
                                            <button type="submit" class="btn" style="width: 100%; height: 45px; padding: 0 20px; background: #cba750; border: 1px solid #0b5f61; font-size: 16px; font-weight: 300">Get New Password</button>
                                        </form>
                                    </div>
                                </div>
                            </div>


                        </div>



                    </div>

                    <div class="col-sm-1 middle-border"></div>

                    <div class="col-sm-1"></div>



                    <div class="form-box" id="form_id2">



                        <img src="common/theme/assets/img/slide (1).png">
                    </div>


                </div>

                <div class="compatibility">
                    <p style="margin: 0px;"><a style="font-size: 14px; font-family: verdana; font-weight: 600;">Note:</a></p>
                    <ul style="font-size: 12px; font-family: verdana; font-weight: 600; line-height: 1.8em;">
                        <li>Site Best Viewed In Google Chrome Version 55+, Mozilla Firefox Version 50+.For Report view use PDF Reader Version 10+ and Microsoft Office 2003 and above for reports.</li>
                        <li>Delete cookies for the proper performance.</li>
                        <li>Please use latest updated versions for better view.</li>
                    </ul>

                </div>

            </div>
        </div>






        <span class="resp-info"></span>
        <div class="fixed-footer">
            <div class="loginfooter" style="text-align: center;">

                <p style="color: #FFF; float: left; margin-left: 10px; font-size: 14px;">© Department of Prohibition, Excise and Registration , Govt. of Bihar</p>
                <p style="color: #FFF; float: right; margin-right: 10px; font-size: 14px;">Designed, Developed and Maintained by KEONICS</p>
            </div>
        </div>
    </div>
    <script src="common/theme/assets/js/jquery-1.11.1.min.js"></script>
    <script src="common/theme/assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="common/theme/assets/js/jquery.backstretch.min.js"></script>
    <script src="common/theme/assets/js/scripts.js"></script>
    <script>
        $("#logsumbit").click(function () {
            var $captcha = $('#recaptcha'),
                    response = $('#recaptcha').val();
            if (response.length === 0) {
                $('#recaptchaerr').text("It is mandatory to ensure not a robot.");
                if (!$captcha.hasClass("error")) {
                    $captcha.addClass("error");
                }
            } else {
                $('#recaptchaerr').text('');
                $captcha.removeClass("error");
                //                            alert('reCAPTCHA marked');
                $.ajax({
                    type: "POST",
                    url: 'Transactional/validateTextCaptcha.htm',
                    data: {
                        recaptcharesponse: response
                    },
                    success: function (data) {
                        if (data == '0') {
                            $('#recaptchaerr').text("Invalid captcha..");
                        }
                        else {
                            $("#loginForm").submit();
                        }
                    }

                });
            }
        });

        $("#refreshCapt").click(function () {
            $('#recaptcha').val('');
            var newSrc = "simpleCaptcha.png?Id=" + Math.random();
            $("#captchaImage").attr("src", newSrc);
        });


        function changeDivImage() {
            //change the image path to a string   
            var imgPath = new String();
            imgPath = document.getElementById("header-bg").style.backgroundImage;

            //get screen res of customer
            var custHeight = screen.height;
            var custWidth = screen.width;

            //if their screen width is less than or equal to 640 then use the 640 pic url
            if (custWidth <= 480) {
                document.getElementById("header-bg").style.backgroundImage = "url(common/theme/images/mobile_logo.png)";



            }
        }

        $("#userId").change(function () {
            var userId = document.getElementById("userId").value;
            $.ajax
               ({
                   type: "POST",
                   url: 'login/getUserId.htm',
                   data: {
                       userId: userId
                   },
                   success: function (data) {
                       if (data === 'No Data') {
                           alert("Enter valid UserID");
                           document.getElementById("userId").value = "";
                       }
                   }
               });


        });

    </script>
</body>
</html>


