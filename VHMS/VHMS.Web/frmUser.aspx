<%@ Page Title="User" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmUser.aspx.cs" Inherits="frmUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Users
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Accounts</a></li>
                <li class="active">Users</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">User Active</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">User InActive</a></li>
                </ul>
                <div class="tab-pane active" id="General">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Name
                                            </th>
                                            <th>Code
                                            </th>
                                            <th>Role
                                            </th>
                                            <th>User Name
                                            </th>
                                            <th>Password
                                            </th>
                                            <th>Special Password
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Reset
                                            </th>
                                            <th>Edit
                                            </th>
                                            <%-- <th>Delete
                                            </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane active" id="SearchResult">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Name
                                            </th>
                                            <th>Code
                                            </th>
                                            <th>Role
                                            </th>
                                            <th>User Name
                                            </th>
                                            <th>Password
                                            </th>
                                            <th>Special Password
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Reset
                                            </th>
                                            <th>Edit
                                            </th>
                                            <%-- <th>Delete
                                            </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="tblSearchResult_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divEmployeename">
                                    <label>
                                        Employee Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtEmployeename" placeholder="Employee Name" maxlength="250"
                                        tabindex="1" />
                                </div>
                                <div class="form-group col-md-4" id="divEmployeecode">
                                    <label>
                                        Employee Code</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtEmployeecode" placeholder="Employee Code" maxlength="50"
                                        tabindex="2" />
                                </div>
                                <div class="form-group col-md-4" id="divMobileno">
                                    <label>
                                        Mobile No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtmoblieno" placeholder="Moblie No" maxlength="10"
                                        tabindex="3" onkeypress="return isNumberKey(event)" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divUserName">
                                    <label>
                                        User Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtUserName" placeholder="User Name" maxlength="250"
                                        tabindex="4" />
                                </div>
                                <div class="form-group col-md-4" id="divPassword">
                                    <label>
                                        Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtPassword" placeholder="Password" maxlength="512"
                                        tabindex="5" />
                                </div>
                                <div class="form-group col-md-4" id="divCPassword">
                                    <label>
                                        Confirm Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtConfirmPassword" placeholder="Confirm Password" maxlength="512"
                                        tabindex="6" />
                                </div>
                            </div>
                            <div class="row">

                                <div class="form-group col-md-4" id="divUserRole">
                                    <label>
                                        Role</label><span class="text-danger">*</span>
                                    <select id="ddlUserRole" class="form-control" tabindex="7">
                                        <option selected="selected" value="0">--Select User Role--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divBranch">
                                    <label>
                                        Branch</label><span class="text-danger">*</span>
                                    <select id="ddlBranch" class="form-control" tabindex="8">
                                        <option selected="selected" value="0">--Select Branch--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divRegion">
                                    <label>
                                        Region</label><span class="text-danger">*</span>
                                    <select id="ddlRegion" class="form-control" tabindex="8">
                                        <option selected="selected" value="0">--Select Region--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divZone">
                                    <label>
                                        Zone</label><span class="text-danger">*</span>
                                    <select id="ddlZone" class="form-control" tabindex="8">
                                        <option selected="selected" value="0">--Select Zone--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divDesignation">
                                    <label>
                                        Designation</label><span class="text-danger">*</span>
                                    <select id="ddlDesignation" class="form-control" tabindex="9">
                                        <option selected="selected" value="0">--Select Designation--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divIDProof">
                                    <label>
                                        ID Proof</label>
                                    <input type="text" class="form-control" id="txtIDProof" placeholder="ID Proof" maxlength="500"
                                        tabindex="15" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divEmail">
                                    <label>
                                        Email</label>
                                    <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="512"
                                        tabindex="10" />
                                </div>
                                <div class="form-group col-md-4" id="divDOB">
                                    <label>
                                        DOB</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="11" id="txtDOB" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divDOJ">
                                    <label>
                                        DOJ</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOJ"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="12" id="txtDOJ" readonly />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divAddress">
                                    <label>
                                        Address</label>
                                    <textarea id="txtAddress" class="form-control" maxlength="2000" tabindex="13" rows="3"></textarea>
                                </div>
                                <div class="form-group col-md-4" id="divBasicpay">
                                    <label>
                                        Basic Pay</label>
                                    <input type="text" class="form-control" id="txtBasicPay" placeholder="Basic Pay"
                                        maxlength="15" tabindex="14" value="0" onkeypress="return IsNumeric(event)" />

                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divOtherPassword">
                                    <label>
                                        Special Password</label>
                                    <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" maxlength="512"
                                        tabindex="16" />
                                </div>
                                <div class="form-group col-md-4" id="divCOtherPassword">
                                    <label>
                                        Confirm Special Password</label>
                                    <input type="password" class="form-control" id="txtCOtherPassword" placeholder="Confirm Special Password" maxlength="512"
                                        tabindex="17" />
                                </div>

                                <div class="form-group col-md-4">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="18" />&nbsp;&nbsp; Active
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="21">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="19">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="20">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divResetPassword" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-6" id="divRPassword">
                                    <label>
                                        Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtResetPassword" placeholder="Password" maxlength="512"
                                        tabindex="1" />
                                </div>
                                <div class="form-group col-md-6" id="divResetCPassword">
                                    <label>
                                        Confirm Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtResetConfirmPassword" placeholder="Confirm Password" maxlength="512"
                                        tabindex="2" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnCloseResetWindow" tabindex="4">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnResetPassword" tabindex="3">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';
            // location.reload();
            pLoadingSetup(false);
            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            $("#txtDOB,#txtDOJ").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDOB,#txtDOJ").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            GetRoles();
            $("#SearchResult").hide();
            GetBranch();
            GetDesignation();
            pLoadingSetup(true);
            GetZoneList();
            GetRegionList();
            $("#ddlUserRole").change();
            GetRecordActive();
            GetRecordInActive();

        });
        function GetRegionList() {
            dProgress(true);
            $("#ddlRegion").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRegion",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlRegion").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlRegion").append('<option value=' + obj[index].RegionID + ' >' + obj[index].RegionName + '</option>');
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlRegion").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                                dProgress(false);
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            dProgress(false);
                        }
                    }
                    else {
                        $("#ddlRegion").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }
        function GetZoneList() {
            dProgress(true);
            $("#ddlZone").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetZone",
                data: JSON.stringify({ izoneid: 0, iregionid: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlZone").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlZone").append('<option value=' + obj[index].ZoneID + ' >' + obj[index].ZoneName + '</option>');
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlZone").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                                dProgress(false);
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            dProgress(false);
                        }
                    }
                    else {
                        $("#ddlZone").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }
        $("#ddlUserRole").change(function () {
            if ($("#ddlUserRole").val() == 7) {
                $("#divRegion").hide();
                $("#divZone").hide();
                $("#divBranch").hide();
            }
            else {
                $("#divRegion").hide();
                $("#divZone").hide();
                $("#divBranch").hide();
            }
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();

            $("#divPassword").show();
            $("#divCPassword").show();

            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add User");
            $('#compose-modal').modal({ show: true, backdrop: false });
            $("#txtName").focus();
            return false;
        });

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtUserName").val().trim() == "" || $("#txtUserName").val() == undefined) {
                $.jGrowl("Please Enter UserName", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divUserName").addClass('has-error'); $("#txtUserName").select(); return false;
            } else { $("#divUserName").removeClass('has-error'); }

            if ($("#txtEmail").val().trim() != "" && $("#txtEmail").val().trim() != undefined) {
                if (IsEmail($("#txtEmail").val()) == false) {
                    $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#txtEmail").focus(); return false;
                }
            }

            if ($("#txtmoblieno").val().trim() == "" || $("#txtmoblieno").val().trim() == undefined || $("#txtmoblieno").val().length != 10) {
                $.jGrowl("Please enter valid Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMobileno").addClass('has-error'); $("#txtmoblieno").focus(); return false;
            } else { $("#divMobileno").removeClass('has-error'); }

            if ($("#txtEmployeecode").val().trim() == "" || $("#txtEmployeecode").val() == undefined) {
                $.jGrowl("Please enter Employee Code", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmployeecode").addClass('has-error'); $("#txtEmployeecode").select(); return false;
            } else { $("#divEmployeecode").removeClass('has-error'); }

            if ($("#ddlUserRole").val() == "0" || $("#ddlUserRole").val() == undefined) {
                $.jGrowl("Please Select UserRole", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divUserRole").addClass('has-error'); $("#ddlUserRole").focus(); return false;
            } else { $("#divUserRole").removeClass('has-error'); }

            if ($("#txtEmployeename").val().trim() == "" || $("#txtEmployeename").val() == undefined) {
                $.jGrowl("Please enter Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divEmployeename").addClass('has-error'); $("#txtEmployeename").select(); return false;
            } else { $("#divEmployeename").removeClass('has-error'); }

            if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val() == undefined) {
                $.jGrowl("Please Enter Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPassword").addClass('has-error'); $("#txtPassword").select(); return false;
            } else { $("#divPassword").removeClass('has-error'); }

            if ($("#txtConfirmPassword").val().trim() == "" || $("#txtConfirmPassword").val() == undefined) {
                $.jGrowl("Please Enter Confirm Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCPassword").addClass('has-error'); $("#txtConfirmPassword").select(); return false;
            } else { $("#divCPassword").removeClass('has-error'); }

            var sPassword = $("#txtPassword").val().trim();
            var sCPassword = $("#txtConfirmPassword").val().trim();
            if (sPassword != sCPassword) {
                $.jGrowl("Password doesnt match", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCPassword").addClass('has-error'); $("#txtConfirmPassword").select(); return false;
            } else { $("#divCPassword").removeClass('has-error'); }

            //var sOtherPassword = $("#txtOtherPassword").val().trim();
            //var sCOtherPassword = $("#txtCOtherPassword").val().trim();
            //if (sOtherPassword != sCOtherPassword) {
            //    $.jGrowl("OtherPassword doesnt match", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divCOtherPassword").addClass('has-error'); $("#txtCOtherPassword").select(); return false;
            //} else { $("#divCOtherPassword").removeClass('has-error'); }

            var Obj = new Object();
            Obj.UserID = 0;
            Obj.UserName = $("#txtUserName").val().trim();
            Obj.Password = $("#txtPassword").val().trim();
            Obj.Email = $("#txtEmail").val();
            Obj.EmployeeName = $("#txtEmployeename").val().trim();
            Obj.EmployeeCode = $("#txtEmployeecode").val().trim();

            Obj.RoleID = $("#ddlUserRole").val();
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
            var ObjRegion = new Object();
            var ObjZone = new Object();
            var ObjBranch = new Object();

            ObjRegion.RegionID = 1;
            ObjZone.ZoneID = 1;
            ObjBranch.BranchID = 1;
            Obj.Branch = ObjBranch;
            Obj.Region = ObjRegion;
            Obj.Zone = ObjZone;

            var ObjDesignation = new Object();
            ObjDesignation.DesignationID = $("#ddlDesignation").val();
            Obj.Designation = ObjDesignation;

            Obj.MobileNo = $("#txtmoblieno").val().trim();
            Obj.Address = $("#txtAddress").val();
            Obj.BasicPay = $("#txtBasicPay").val();
            Obj.IDProof = $("#txtIDProof").val();
            Obj.sDOB = $("#txtDOB").val();
            Obj.sDOJ = $("#txtDOJ").val();
            Obj.ConfirmPassword = $("#txtOtherPassword").val().trim();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.UserID = $("#hdnID").val();
                sMethodName = "UpdateUser";
            }
            else { sMethodName = "AddUser"; }

            SaveandUpdateUser(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        //Added on 25-10-2017
        $("#btnCloseResetWindow").click(function () {
            $('#divResetPassword').modal('hide');
            return false;
        });

        $("#btnResetPassword").click(function () {
            if ($("#txtResetPassword").val().trim() == "" || $("#txtResetPassword").val() == undefined) {
                $.jGrowl("Please Enter Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRPassword").addClass('has-error'); $("#txtResetPassword").select(); return false;
            } else { $("#divRPassword").removeClass('has-error'); }

            if ($("#txtResetConfirmPassword").val().trim() == "" || $("#txtResetConfirmPassword").val() == undefined) {
                $.jGrowl("Please Enter Confirm Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divResetCPassword").addClass('has-error'); $("#txtResetConfirmPassword").select(); return false;
            } else { $("#divResetCPassword").removeClass('has-error'); }

            var sPassword = $("#txtResetPassword").val().trim();
            var sCPassword = $("#txtResetConfirmPassword").val().trim();
            if (sPassword != sCPassword) {
                $.jGrowl("Password doesn't match", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divResetCPassword").addClass('has-error'); $("#txtResetConfirmPassword").select(); return false;
            } else { $("#divResetCPassword").removeClass('has-error'); }

            ResetPassword($("#hdnID").val(), sPassword);
            return false;
        });

        function ResetPassword(ID, Pass) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/ResetPassword",
                data: JSON.stringify({ UserID: ID, sPassword: Pass }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Password Reset Successfully done", { sticky: false, theme: 'success', life: jGrowlLife });
                                $('#divResetPassword').modal('hide');
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
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
            $("#txtUserName").val("");
            $("#txtPassword").val("");
            $("#txtConfirmPassword").val("");
            $("#txtEmail").val("");
            $("#chkStatus").prop("checked", true);
            $("#ddlUserRole").val("0");
            $("#ddlBranch").val("0");
            $("#ddlZone").val("0");
            $("#ddlRegion").val("0");
            $("#ddlDesignation").val("0");
            $("#txtEmployeename").val("");
            $("#txtEmployeecode").val("");

            $("#txtmoblieno").val("");
            $("#txtAddress").val("");
            $("#txtBasicPay").val("0");
            $("#txtIDProof").val("");
            $("#txtDOB").val("");
            $("#txtDOJ").val("");
            $("#txtOtherPassword").val("");
            $("#txtCOtherPassword").val("");
            $("#divUserName").removeClass('has-error');
            $("#divEmployeename").removeClass('has-error');
            $("#divPassword").removeClass('has-error');
            $("#divCPassword").removeClass('has-error');
            $("#divUserRole").removeClass('has-error');
            $("#ddlUserRole").change();
            return false;
        }

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        function GetDesignation() {
            $("#ddlDesignation").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetDesignation",
                data: JSON.stringify({ RoleID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlDesignation").append('<option value="' + '0' + '">' + '--Select Designation--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlDesignation").append('<option value=' + obj[index].DesignationID + '>' + obj[index].DesignationName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlDesignation").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#ddlDesignation").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetBranch() {
            $("#ddlBranch").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetBranch",
                data: JSON.stringify({ RoleID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlBranch").append('<option value="' + '0' + '">' + '--Select Branch--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlBranch").append('<option value=' + obj[index].BranchID + '>' + obj[index].BranchName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlBranch").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#ddlBranch").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetRoles() {
            $("#ddlUserRole").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRole",
                data: JSON.stringify({ RoleID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlUserRole").append('<option value="' + '0' + '">' + '--Select Role--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $("#ddlUserRole").append('<option value=' + obj[index].RoleID + '>' + obj[index].RoleName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlUserRole").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#ddlUserRole").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetRecordInActive() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUserStatus",
                data: JSON.stringify({ ID: 0 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblSearchResult").dataTable();
                            try {
                                notetable.fnDestroy();
                            }
                            catch (err) {
                                $.fn.dataTableExt.sErrMode = 'throw';
                                //$.jGrowl("Eror" + err, { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblSearchResult_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }
                                        var table = "<tr id='" + obj[index].UserID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].EmployeeName + "</td>";
                                        table += "<td>" + obj[index].EmployeeCode + "</td>";
                                        table += "<td>" + obj[index].RoleName + "</td>";
                                        table += "<td>" + obj[index].UserName + "</td>";
                                        table += "<td>" + obj[index].Password + "</td>";
                                        table += "<td>" + obj[index].ConfirmPassword + "</td>";
                                        table += "<td>" + TypeStatus + "</td>";
                                        //Added on 25-10-2017
                                        table += "<td><a href='#' id=" + obj[index].UserID + " class='ResetPassword'><i class='fa fa-lg fa-unlock text-maroon'/></a></td>";
                                        if (ActionUpdate == "1") { table += "<td><a href='#' id=" + obj[index].UserID + " class=\"Edit\"><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        //if (ActionDelete == "1")
                                        //{ table += "<td><a href='#' id=" + obj[index].UserID + " class=\"Delete\"><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });

                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    //Added on 25-10-2017
                                    $(".ResetPassword").click(function () {
                                        var UserID = $(this).parent().parent()[0].id;
                                        $("#hdnID").val(UserID);
                                        $("#txtResetPassword").val("");
                                        $("#txtResetConfirmPassword").val("");
                                        $("#divRPassword").removeClass('has-error');
                                        $("#divResetCPassword").removeClass('has-error');

                                        $(".modal-title").html("<i class='fa fa-unlock'></i>&nbsp;&nbsp;Reset Password");
                                        $('#divResetPassword').modal({ show: true, backdrop: false });
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    //   { "sWidth": "3%" },
                                    //{ "sWidth": "3%" }
                                ]
                            });
                            $("#tblSearchResult_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblSearchResult_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetRecordActive() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUserStatus",
                data: JSON.stringify({ ID: 1 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            try {
                                notetable.fnDestroy();
                            }
                            catch (err) {
                                $.fn.dataTableExt.sErrMode = 'throw';
                                //$.jGrowl("Eror" + err, { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }
                                        var table = "<tr id='" + obj[index].UserID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].EmployeeName + "</td>";
                                        table += "<td>" + obj[index].EmployeeCode + "</td>";
                                        table += "<td>" + obj[index].RoleName + "</td>";
                                        table += "<td>" + obj[index].UserName + "</td>";
                                        table += "<td>" + obj[index].Password + "</td>";
                                        table += "<td>" + obj[index].ConfirmPassword + "</td>";
                                        table += "<td>" + TypeStatus + "</td>";
                                        //Added on 25-10-2017
                                        table += "<td><a href='#' id=" + obj[index].UserID + " class='ResetPassword'><i class='fa fa-lg fa-unlock text-maroon'/></a></td>";
                                        if (ActionUpdate == "1") { table += "<td><a href='#' id=" + obj[index].UserID + " class=\"Edit\"><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        //if (ActionDelete == "1")
                                        //{ table += "<td><a href='#' id=" + obj[index].UserID + " class=\"Delete\"><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });

                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    //Added on 25-10-2017
                                    $(".ResetPassword").click(function () {
                                        var UserID = $(this).parent().parent()[0].id;
                                        $("#hdnID").val(UserID);
                                        $("#txtResetPassword").val("");
                                        $("#txtResetConfirmPassword").val("");
                                        $("#divRPassword").removeClass('has-error');
                                        $("#divResetCPassword").removeClass('has-error');

                                        $(".modal-title").html("<i class='fa fa-unlock'></i>&nbsp;&nbsp;Reset Password");
                                        $('#divResetPassword').modal({ show: true, backdrop: false });
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblRecord_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblRecord").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    //   { "sWidth": "3%" },
                                    //{ "sWidth": "3%" }
                                ]
                            });
                            $("#tblRecord_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function SaveandUpdateUser(Obj, sMethodName) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/" + sMethodName,
                data: JSON.stringify({ Objdata: Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                ClearFields();
                                //GetRecord();

                                if (sMethodName == "AddUser") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateUser") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                                //window.location = "frmUser.aspx";
                                GetRecordActive();
                                GetRecordInActive();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "User_A_01" || objResponse.Value == "User_U_01") {
                                $.jGrowl("Username already exists in database. Please try different Username", { sticky: false, theme: 'danger', life: jGrowlLife });
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

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUserByID",
                data: JSON.stringify({ ID: id }),
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
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();

                                    //  $("#divPassword").hide();
                                    // $("#divCPassword").hide();

                                    $("#hdnID").val(obj.UserID);
                                    $("#txtUserName").val(obj.UserName);
                                    $("#txtPassword").val(obj.Password);
                                    $("#txtConfirmPassword").val(obj.Password);
                                    $("#txtEmail").val(obj.Email);
                                    $("#ddlUserRole").val(obj.RoleID).change();
                                    $("#ddlBranch").val(obj.Branch.BranchID);
                                    $("#ddlZone").val(obj.Zone.ZoneID);
                                    $("#ddlRegion").val(obj.Region.RegionID);
                                    $("#ddlDesignation").val(obj.Designation.DesignationID);
                                    $("#txtEmployeename").val(obj.EmployeeName);
                                    $("#txtEmployeecode").val(obj.EmployeeCode);

                                    $("#txtmoblieno").val(obj.MobileNo);
                                    $("#txtDOB").val(obj.sDOB);
                                    $("#txtDOJ").val(obj.sDOJ);
                                    $("#txtAddress").val(obj.Address);
                                    $("#txtBasicPay").val(obj.BasicPay);
                                    $("#txtIDProof").val(obj.IDProof);
                                    $("#txtOtherPassword").val(obj.ConfirmPassword);
                                    $("#txtCOtherPassword").val(obj.ConfirmPassword);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: false });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit User");
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteUser",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                ClearFields();
                                GetRecord();
                                $.jGrowl("Record Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "User_R_01" || objResponse.Value == "User_D_01") {
                                $.jGrowl(_CMDeleteError, { sticky: false, theme: 'danger', life: jGrowlLife });
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
        $("#aGeneral").click(function () {
            $("#General").show();
            $("#SearchResult").hide();
            //GetRecordActives();
        });

        $("#aSearchResult").click(function () {
            $("#General").hide();
            $("#SearchResult").show();

        });
    </script>
</asp:Content>
