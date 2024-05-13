<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport' />
    <title>Sri Meenachi Silks PVT LTD | Sign in</title>
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/sLoginStyle.css" rel="stylesheet" />
    <link href="plugins/jgrowl/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
</head>
<body >
   <%-- <form id="form1" runat="server" >
        <div class="container">
            <div class="col-lg-4 col-md-3 col-sm-2"></div>
            <div class="col-lg-4 col-md-6 col-sm-8">
                <div class="logo">
                    <img src="images/default_user.png" alt="Logo" />
                </div>
                <div class="row loginbox">
                    <div id="divTitle" runat="server">Sri Meenachi Silks PVT LTD v 1.0.0.0</div>
                    <br />
                    <div class="col-lg-12">
                        <span class="singtext">Sign in </span>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <input type="text" class="form-control" id="txtUserName" runat="server" tabindex="1" placeholder="Please enter your user name" />
                    </div>
                    <div class="col-lg-12  col-md-12 col-sm-12">
                        <input type="password" class="form-control" id="txtPassword" runat="server" tabindex="2" placeholder="Please enter password" />
                    </div>
                    <div class="col-lg-12  col-md-12 col-sm-12">                     
                        <asp:Button ID="btnSignIn" runat="server" Text="Login" class="btn submitButton"
                            OnClick="btnSignIn_Click" />
                    </div>
                </div>
                <br/>
                <br/>
                <footer class="footer">                                   
                    <p>Copyrights ©2019 All rights reserved </p>
                </footer>

            </div>
            <div class="col-lg-4 col-md-3 col-sm-2"></div>
        </div>
    </form>--%>
    <form id="form1" runat="server" >
        <div class="container" style="width:100%;display:flex;height:100vh;padding:0px;">
            <div class="col-sm-6" style="background-image:url(images/meenakchi-amman-1.jpg);background-size:contain;background-repeat:no-repeat;background-position:center">
            </div>
            <div class="col-sm-1"></div>
            <div class="col-sm-3">
                <div class="logo">
                    <img src="images/default_user.png" alt="Logo" />
                </div>
                <div class="row loginbox">
                    <div id="divTitle" runat="server">Sri Meenachi Silks PVT LTD v 1.0.0.0</div>
                    <br />
                    <div class="col-lg-12">
                        <span class="singtext">Sign in </span>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <input type="text" class="form-control" id="txtUserName" runat="server" tabindex="1" placeholder="Please enter your user name" />
                    </div>
                    <div class="col-lg-12  col-md-12 col-sm-12">
                        <input type="password" class="form-control" id="txtPassword" runat="server" tabindex="2" placeholder="Please enter password" />
                    </div>
                    <div class="col-lg-12  col-md-12 col-sm-12">
                        <%--   <a href="#" class="btn submitButton" id="btnLogin">Submit </a>--%>
                        <%-- <button type="submit" class="btn submitButton" id="btnLogin" tabindex="3">
                            Login</button>--%>
                        <asp:Button ID="btnSignIn" runat="server" Text="Login" class="btn submitButton"
                            OnClick="btnSignIn_Click" />
                    </div>
                   <%-- <div class="checkbox " style="position: static !important">
                        <label>
                            <input type="checkbox" id="chkRememberMe" tabindex="3" runat="server" />Remember Me
                        </label>
                    </div>--%>
                </div>
                <br>
                <br>
                <footer class="footer">                                   
                    <p>Copyrights ©<span id="copyYear">2022</span> All rights reserved </p>
                </footer>
                <!--footer Section ends-->

            </div>
            <div class="col-sm-2"></div>
            <%--<div class="col-lg-4 col-md-3 col-sm-2"></div>--%>
        </div>
    </form>
    <footer>
<%--    <div class="container">
        <div class="col-md-10 col-md-offset-1 text-center">
            <h6 style="font-size:14px;font-weight:100;color: #fff;">Copyright <i class="fa fa-heart red" style="color: #BC0213;"></i> by <a href="http://hashif.com" style="color: #fff;" target="_blank">Hashif</a></h6>
        </div>   
    </div>--%>
</footer>
    <script src="js/jQuery-2.1.4.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="plugins/cookie/jquery.enhanced.cookie.js" type="text/javascript"></script>
    <script src="plugins/jgrowl/jquery.jgrowl.js" type="text/javascript"></script>
    <script src="UserDefined_Js/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#btnLogin").hide();

            //var _Tfunctionality;
            //if ($.cookie("UserName") != undefined && $.cookie("UserName") != null) {
            //    _Tfunctionality = $.cookie("UserName");
            //    alert(_Tfunctionality);
            //    $("#txtUserName").val(_Tfunctionality);               
            //}

            //if ($.cookie("Password") != undefined && $.cookie("Password") != null) {
            //    _Tfunctionality = $.cookie("Password");
            //    $("#txtPassword").val(_Tfunctionality);
            //}
            $("#copyYear").text(new Date().getFullYear());
            $("#txtUserName").focus();
            $("#btnSignIn").click(function () {
                if ($("#txtUserName").val().trim() == "" || $("#txtUserName").val().trim() == undefined) {
                    $.jGrowl("Please enter User Name", { sticky: false, theme: 'danger', life: jGrowlLife });
                    $("#txtUserName").focus(); return false;
                }
                if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined) {
                    $.jGrowl("Please enter Password", { sticky: false, theme: 'danger', life: jGrowlLife });
                    $("#txtPassword").focus(); return false;
                }
            });
        });

        function eAlert(msg) {
            $.jGrowl(msg, { sticky: false, theme: 'warning', life: jGrowlLife });
        }

        $("#btnLogin").click(function () {
            if ($("#txtUserName").val().trim() == "" || $("#txtUserName").val().trim() == undefined) {
                $.jGrowl("Please enter User Name", { sticky: false, theme: 'danger', life: jGrowlLife });
                $("#txtUserName").focus(); return false;
            }
            if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined) {
                $.jGrowl("Please enter Password", { sticky: false, theme: 'danger', life: jGrowlLife });
                $("#txtPassword").focus(); return false;
            }
            ValidateUserLogin();
            return false;
        });

        function ClearFields() {
            $("#txtUserName").val("");
            $("#txtPassword").val("");
            $("#txtUserName").focus();
        }

        function ValidateUserLogin() {
            var UserName = $("#txtUserName").val().trim();
            var Password = $("#txtPassword").val().trim();

            $.ajax({
                type: "POST",
                url: "WebServices/KudilMSService.svc/GetUserLogin",
                data: JSON.stringify({ sUserName: UserName, sPassword: Password }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {
                                    SetSessionValue("UserID", obj.UserID);
                                    SetSessionValue("UserName", obj.UserName);
                                    SetSessionValue("RoleID", obj.RoleID);
                                    //iRoleID = obj.RoleID;
                                    SetSessionValue("RoleName", obj.RoleName);
                                    var sdate = new Date();
                                    SetSessionValue("LogDateTime", sdate.getDate() + "-" + (sdate.getMonth() + 1) + "-" + sdate.getFullYear() + " " + sdate.getHours() + ":" + sdate.getMinutes() + ":" + sdate.getSeconds());
                                    window.location.href = obj.FileName;

                                    $.cookie("UserName", obj.UserName, { expires: 30 });
                                    $.cookie("Password", obj.Password, { expires: 30 });

                                    alert($.cookie("UserName"));
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("Invalid UserName/Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                                ClearFields();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "Error") {
                                $.jGrowl("Invalid UserName/Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                                ClearFields();
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
    </script>
</body>
</html>
