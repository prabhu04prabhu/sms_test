<%@ Page Title="User Profile" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmProfile.aspx.cs" Inherits="frmProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-3">
                    <div class="box box-primary">
                        <div class="box-body">
                            <img class="profile-user-img img-responsive img-circle" style="border-radius:35%" src="images/default_user.png" alt="User profile picture">
                            <h3 class="profile-username text-center"><%= Session["EmployeeName"].ToString().ToUpper()%></h3>
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">Role <a id="aDisplayRole" title="Click here to view details" class="pull-right"><%= Session["RoleName"].ToString().ToUpper()%></a>
                                </li>
                                <li class="list-group-item">Logged On <a href="frmLog.aspx" target="_blank" class="pull-right"><%= Session["LogDateTime"].ToString()%></a>
                                </li>
                            </ul>
                            <a id="aChangePassword" href="#" class="btn btn-primary btn-block"><b>Change Password</b></a>
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="box" id="divChangePassword">
                        <div class="box-header with-border">
                            <div class="box-title">Change Password</div>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label>
                                    Old Password<span class="text-danger">*</span></label>
                                <input type="password" class="form-control" id="txtOldPassword" placeholder="Please enter old password" maxlength="100"
                                    tabindex="1" />
                            </div>
                            <div class="form-group" id="">
                                <label>
                                    New Password<span class="text-danger">*</span></label>
                                <input type="password" class="form-control" id="txtNewPassword" placeholder="Please enter New password" maxlength="100"
                                    tabindex="1" />
                            </div>
                            <div class="form-group" id="">
                                <label>
                                    Confirm New Password<span class="text-danger">*</span></label>
                                <input type="password" class="form-control" id="txtNewCPassword" placeholder="Please enter Confirm New Password" maxlength="100"
                                    tabindex="1" />
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 col-md-offset-9">
                                    <button id="btnChangePassword" type="button" class="btn btn-info margin pull-right" tabindex="3">
                                        <i class="fa fa-save"></i>&nbsp;&nbsp;Change Password</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="box" id="divRole">
                        <div class="box-header with-border">
                            <div class="box-title">Role Resources</div>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times text-red"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <div id="divModules"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <script type="text/javascript">
        var _CMRoleID = '<%=Session["RoleID"]%>';
        $(document).ready(function () {
            pLoadingSetup(false);
            $("#divChangePassword").hide();
            $("#divRole").hide();
           // $("#aChangePassword").hide();
            pLoadingSetup(true);
        });

        $("#aChangePassword").click(function () {
            ClearFields();
            $("#divChangePassword").show();
            $("#divRole").hide();
            $("#txtOldPassword").focus();
            return false;
        });

        $("#btnChangePassword").click(function () {
            if ($("#txtOldPassword").val() == "") { $.jGrowl("Please Enter Old Password", { sticky: false, theme: 'danger', life: jGrowlLife }); $("#txtOldPassword").focus(); return false; }
            else if ($("#txtNewPassword").val() == "") { $.jGrowl("Please Enter New Password", { sticky: false, theme: 'danger', life: jGrowlLife }); $("#txtNewPassword").focus(); return false; }
            else if ($("#txtNewCPassword").val() == "") { $.jGrowl("Please Enter Confirm New Password", { sticky: false, theme: 'danger', life: jGrowlLife }); $("#txtConfirmNewPassword").focus(); return false; }
            var sOldPassword = $("#txtOldPassword").val();
            var sNewPassword = $("#txtNewPassword").val();
            var sConfirmNewPassword = $("#txtNewCPassword").val();
            if (sNewPassword == sConfirmNewPassword) {
                ChangePassword(sNewPassword, sOldPassword);
            }
            else {
                $.jGrowl("Password Not Match", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }

            ClearFields();
            $("#txtOldPassword").focus();
            return false;
        });

        function ChangePassword(sNewPass, sOldPass) {
            var Obj = new Object();
            Obj.Password = sNewPass;
            Obj.OldPassword = sOldPass;

            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/ChangePassword",
                data: JSON.stringify({ Objdata: Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Password Changed Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                $("#divChangePassword").hide();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "ChangePassword_U_01") {
                                $.jGrowl("Old password doesn't match", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function ClearFields() {
            $("#txtOldPassword").val("");
            $("#txtNewPassword").val("");
            $("#txtNewCPassword").val("");
            return false;
        }

        $("#aDisplayRole").click(function () {
            if (_CMRoleID > 0)
            { DisplayRoleConfiguration(_CMRoleID); $("#divChangePassword").hide(); $("#divRole").show(); }
            else
            { $.jGrowl("Invalid Role Access", { sticky: true, theme: 'danger', life: jGrowlLife }); }

            return false;
        });

        function DisplayRoleConfiguration(id) {
            $("#divModules").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRoleConfiguration",
                data: JSON.stringify({ iRoleID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var objdata = $.parseJSON(objResponse.Value);
                                if (objdata.length > 0) {
                                    gSourceModule = objdata;
                                    var sTable = "", sColorCode = "bg-blue";
                                    sTable = "<table id='tblModule' class='table no-margin table-hover table-condensed'>";
                                    sTable += "<thead><tr><th class='" + sColorCode + "'>Menu</th>";
                                    sTable += "<th class='" + sColorCode + "'>Module</th>";
                                    sTable += "<th style='text-align:center;' class='" + sColorCode + "'>Access</th>";
                                    sTable += "<th style='text-align:center;' class='" + sColorCode + "'>View</th>";
                                    sTable += "<th style='text-align:center;' class='" + sColorCode + "'>Add</th>";
                                    sTable += "<th style='text-align:center;' class='" + sColorCode + "'>Edit</th>";
                                    sTable += "<th style='text-align:center;' class='" + sColorCode + "'>Delete</th>";
                                    sTable += "</tr></thead><tbody>";
                                    var sIsAccess = '', sIsView = '', sIsAdd = '', sIsEdit = '', sIsDelete = '';
                                    for (var index = 0; index < objdata.length; index++) {
                                        sIsAccess = ''; sIsView = ''; sIsAdd = ''; sIsEdit = ''; sIsDelete = '';
                                        sTable += "<tr id='Row" + (index + 1) + "'><td>" + objdata[index].MenuName + "</td>";
                                        sTable += "<td>" + objdata[index].ModuleName + "</td>";

                                        sIsAccess = objdata[index].IsAccess ? "fa fa-check-square text-green" : "fa fa-times text-red";
                                        sIsView = objdata[index].IsView ? "fa fa-check-square text-green" : "fa fa-times text-red";
                                        sIsAdd = objdata[index].IsAdd ? "fa fa-check-square text-green" : "fa fa-times text-red";
                                        sIsEdit = objdata[index].IsEdit ? "fa fa-check-square text-green" : "fa fa-times text-red";
                                        sIsDelete = objdata[index].IsDelete ? "fa fa-check-square text-green" : "fa fa-times text-red";

                                        sTable += "<td style='text-align:center;'><i class='" + sIsAccess + "'></i></td>";
                                        sTable += "<td style='text-align:center;'><i class='" + sIsView + "'></i></td>";
                                        sTable += "<td style='text-align:center;'><i class='" + sIsAdd + "'></i></td>";
                                        sTable += "<td style='text-align:center;'><i class='" + sIsEdit + "'></i></td>";
                                        sTable += "<td style='text-align:center;'><i class='" + sIsDelete + "'></i></td>";
                                        sTable += "</tr>";
                                    }
                                    sTable += ('</tbody></table>');
                                    $("#divModules").show();
                                    $("#divModules").html(sTable);

                                    if (objdata.length >= 13)
                                    { $("#divModules").css({ 'height': '0px', 'min-height': '500px', 'overflow': 'auto' }); }
                                    else
                                    { $("#divModules").css({ 'height': '', 'min-height': '' }); }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#divModules").empty();
                                dProgress(false);
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            dProgress(false);
                        }
                    }
                    else {
                        $("#divModules").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        $("#aVisitLog").click(function () {
            window.open('frmLog.aspx', '_blank');
        });
    </script>
</asp:Content>
