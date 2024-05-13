<%@ Page Title="Attendance Entry" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmManageAttandance.aspx.cs" Inherits="frmManageAttandance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Attendance Entry
            </h1>
            <div class="form-group col-md-2" id="divFrom">
                <label>
                    FormDate</label><span class="text-danger">*</span>
                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                    data-link-format="dd/MM/yyyy">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                    </div>
                    <input type="text" class="form-control pull-right" tabindex="3" id="txtForm" readonly="true" />
                </div>
            </div>
            <div class="form-group col-md-2" id="divTo">
                <label>
                    ToDate</label><span class="text-danger">*</span>
                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                    data-link-format="dd/MM/yyyy">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                    </div>
                    <input type="text" class="form-control pull-right" tabindex="3" id="txtTo" readonly="true" />
                </div>
            </div>
            <div class="form-group col-md-4" id="divEmployee">
                <label>
                    Employee Name</label><span class="text-danger">*</span>
                <select id="ddlEmployee" class="form-control select2" data-placeholder="" tabindex="-1"></select>
            </div>

            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Attendance Entry</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Employee Name
                                            </th>
                                            <th>Punch Date
                                            </th>
                                            <th>In time
                                            </th>
                                            <th>Out time
                                            </th>
                                            <th>Special Status
                                            </th>
                                            <th>View
                                            </th>
                                            <th>Edit
                                            </th>
                                            <th>Delete
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group" id="divConfirmedBy">
                                <label>
                                    Employee Name</label><span class="text-danger">*</span>
                                <select id="ddlConfirmedBy" class="form-control select2" data-placeholder="" tabindex="1"></select>
                            </div>
                            <div class="form-group" id="divDate">
                                <label>
                                    Date</label><span class="text-danger">*</span>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="3" id="txtDate" readonly="true" />
                                </div>
                            </div>

                            <div class="form-group" id="divInTime">
                                <label>
                                    In Time</label>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="4" id="txtInTime" readonly="true" />
                                </div>
                            </div>
                            <div class="form-group" id="divOutTime">
                                <label>
                                    Out Time</label>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="5" id="txtOuttime" readonly="true" />
                                </div>
                            </div>
                            <div class="form-group" id="divCategory">
                                <label>
                                    Special Status</label><span class="text-danger">*</span>
                                <select id="ddlAdvanceType" class="form-control" tabindex="6">
                                    <option value="Present" selected="selected">Present</option>
                                    <option value="Permission">Permission</option>
                                    <option value="Half Day">Half Day</option>
                                    <option value="Absent">Absent</option>
                                    <option value="Leave">Leave</option>
                                    <option value="Paid leave">Paid leave</option>
                                </select>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="7">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="8">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="9">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdnBasicPay" />
    <input type="hidden" id="hdnInTime" />
    <input type="hidden" id="hdnOutTime" />
    <input type="hidden" id="hdnShiftID" />
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("#txtDate,#txtForm,#txtTo").attr("data-link-format", "dd/MM/yyyy");
            $("#txtDate,#txtForm,#txtTo").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            $("#txtInTime,#txtOuttime").attr("data-link-format", "dd/MM/yyyy");
            $("#txtInTime,#txtOuttime").datetimepicker({
                pickTime: true,
                pickDate: false,
                format: 'hh:mm A'
            });
            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }
            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            pLoadingSetup(false);
            pLoadingSetup(true);
            GetEmployeeList("ddlConfirmedBy");
            GetEmployeeList("ddlEmployee");
            $("#txtTo").focus();
            var d = new Date().getDate();
            var m = new Date().getMonth() - 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtForm").val(d + "/" + m + "/" + y);
            $("#txtForm").focus();
            $("#ddlEmployee").focus();
            GetRecord();
        });

        $("#txtTo,#txtForm,#ddlEmployee").change(function () {
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#ddlConfirmedBy").val(0).change();
            $("#ddlEmployee").val(0).change();
            $("#txtDate").val("");
            $("#ddlAdvanceType").val("Present");
            $("#txtInTime").val("");
            $("#txtOuttime").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Attendance");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            return false;
        });

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#ddlConfirmedBy").val() == "0" || $("#ddlConfirmedBy").val() == undefined || $("#ddlConfirmedBy").val() == null) {
                $.jGrowl("Please select Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
            }
            else { $("#divConfirmedBy").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Punch Date ", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            } else { $("#divDate").removeClass('has-error'); }

            var ObjOPBilling = new Object();
            ObjOPBilling.AttendanceLogID = 0;
            var ObjEmployee = new Object();
            ObjEmployee.EmployeeID = $("#ddlConfirmedBy").val();
            ObjOPBilling.Employee = ObjEmployee;
            ObjOPBilling.sPunchDate = $("#txtDate").val().trim();
            ObjOPBilling.SpecialStatus = $("#ddlAdvanceType").val();
            var Days = moment(ObjOPBilling.sPunchDate, "DD/MM/YYYY").daysInMonth();
            var perdaysalary = $("#hdnBasicPay").val() / Days;
            if (ObjOPBilling.SpecialStatus == "Half Day") {
                ObjOPBilling.DeductionAmt = perdaysalary / 2;
            }
            else if (ObjOPBilling.SpecialStatus == "Absent") {
                ObjOPBilling.DeductionAmt = perdaysalary;
            }
            else {
                ObjOPBilling.DeductionAmt = 0;
            }
            var ILateMin = "30";
            ObjOPBilling.PunchInTime = $("#txtInTime").val();
            ObjOPBilling.PunchOutTime = $("#txtOuttime").val();

            var StartTime = moment(ObjOPBilling.PunchInTime, 'hh:mm A').format('HH:mm');
            var endtime = moment(ObjOPBilling.PunchOutTime, 'hh:mm A').format('HH:mm');
            var diff = (new Date("1970-1-1 " + endtime) - new Date("1970-1-1 " + StartTime)) / 1000 / 60;

            var hours = Math.floor(diff / 60);
            var TotalMinutes = (diff % 60);
            ObjOPBilling.Duration = hours + ":" + TotalMinutes;
            ObjOPBilling.Status = "OUT";
            var WorkingTime = $("#hdnInTime").val();
            var WorkingendTime = $("#hdnOutTime").val();
            var WorkStartTime = moment($("#hdnInTime").val(), 'hh:mm A').format('HH:mm');
            var WorkEndTime = moment($("#hdnOutTime").val(), 'hh:mm A').format('HH:mm');
            var WorkingDifference = (new Date("1970-1-1 " + WorkEndTime) - new Date("1970-1-1 " + WorkStartTime)) / 1000 / 60;
            if (WorkingDifference > diff) {
                //var diff1 = (new Date("1970-1-1 " + StartTime) - new Date("1970-1-1 " + WorkStartTime)) / 1000 / 60;
                ObjOPBilling.LateMinutes = WorkingDifference - diff;
            }
            else
                ObjOPBilling.LateMinutes = 0;

            var Overtime = diff + 60;
            if (WorkingDifference < Overtime) {
                ObjOPBilling.OvertimeCount = 1;
            }
            else
                ObjOPBilling.OvertimeCount = 0;

            //ObjOPBilling.LateMinutes = WorkStartTime - (Convert.ToInt32(tMinutes) + iLateMinutes);
            ObjOPBilling.Active = true;
            ObjOPBilling.Edit = 1;
            var sMethodName;
            if ($("#hdnID").val() > 0) {
                ObjOPBilling.AttendanceLogID = $("#hdnID").val();
                sMethodName = "UpdateAttendanceLog";
                ObjOPBilling.Edit = 1;
            }
            else { sMethodName = "AddAttendanceLog"; }

            SaveandUpdateAdvance(ObjOPBilling, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtOutTime").val("");
            $("#chkStatus").prop("checked", true);

            $("#divName").removeClass('has-error');
            return false;
        }

        $("#ddlConfirmedBy").change(function () {
            if ($("#ddlConfirmedBy").val() > 0) {
                GetRateByProduct();
            }
        });

        function GetRateByProduct() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetEmployeeByID",
                data: JSON.stringify({ ID: $("#ddlConfirmedBy").val() }),
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
                                    $("#hdnBasicPay").val(obj.NetSalary);
                                    $("#hdnShiftID").val(obj.Shift.ShiftID);
                                    $("#hdnInTime").val(obj.EmployeeInTime);
                                    $("#hdnOutTime").val(obj.EmployeeOutTime);
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



        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAttendanceLog",
                data: JSON.stringify({ FromDate: $("#txtForm").val(), ToDate: $("#txtTo").val(), iEmployeeID: $("#ddlEmployee").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";

                                    for (var index = 0; index < obj.length; index++) {

                                        var table = "";
                                        if (obj[index].SpecialStatus == 'Absent')
                                            table += "<tr style='background-color:#d97168;' id='" + obj[index].AttendanceLogID + "'>";
                                        else if (obj[index].SpecialStatus = 'Present')
                                            table = "<tr id='" + obj[index].AttendanceLogID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Employee.EmployeeName + "</td>";
                                        table += "<td>" + obj[index].sPunchDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].PunchInTime + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].PunchOutTime + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].SpecialStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AttendanceLogID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AttendanceLogID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AttendanceLogID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Attendance");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
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
                                dom: 'Bfrtip',
                                buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "45%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "7%" },
                                    { "sWidth": "8%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" }
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
        function GetEmployeeList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetEmployee",
                data: JSON.stringify({ CountryID: 0 }),
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
                                    $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].EmployeeID + "'>" + obj[index].EmployeeName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

        function SaveandUpdateAdvance(Obj, sMethodName) {
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
                                GetRecord();

                                if (sMethodName == "AddAttendanceLog") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateAttendanceLog") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Advance_A_01" || objResponse.Value == "Advance_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
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
                url: "WebServices/VHMSService.svc/GetAttendanceLogByID",
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
                                    $("#hdnID").val(obj.AttendanceLogID);
                                    $("#txtDate").val(obj.sPunchDate);
                                    $("#ddlConfirmedBy").val(obj.Employee.EmployeeID).change();
                                    $("#ddlAdvanceType").val(obj.SpecialStatus);
                                    $("#txtInTime").val(obj.PunchInTime);
                                    $("#txtOuttime").val(obj.PunchOutTime);
                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Attendance");
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
                url: "WebServices/VHMSService.svc/DeleteAttendanceLog",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                ClearFields();
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Advance_R_01" || objResponse.Value == "Advance_D_01") {
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
    </script>
</asp:Content>
