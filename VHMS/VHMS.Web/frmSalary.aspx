<%@ Page Title="Salary" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmSalary.aspx.cs" Inherits="frmSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Salary
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Salary</li>
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
                                            <th>Employee  Name
                                            </th>
                                            <th>MonthYear
                                            </th>
                                            <th>Salary Date
                                            </th>
                                            <th>BasicPay
                                            </th>
                                            <th>Deduction
                                            </th>
                                            <th>Incentives
                                            </th>
                                            <th>AdvanceDeduction
                                            </th>
                                            <th>AttendanceDeduction
                                            </th>
                                            <th>NetSalary
                                            </th>
                                            <th>PaymentMode
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

                            <div class="row">
                                <div class="form-group col-md-4" id="divMonth">
                                    <label>
                                        Month</label><span class="text-danger">*</span>
                                    <select id="ddlMonth" class="form-control select2" tabindex="1">
                                        <option value="January">January</option>
                                        <option value="February">February</option>
                                        <option value="March">March</option>
                                        <option value="April">April</option>
                                        <option value="May">May</option>
                                        <option value="June">June</option>
                                        <option value="July">July</option>
                                        <option value="August">August</option>
                                        <option value="September">September</option>
                                        <option value="October">October</option>
                                        <option value="November">November</option>
                                        <option value="December">December</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divYear">
                                    <label>
                                        Year</label><span class="text-danger">*</span>
                                    <select id="ddlYear" class="form-control select2" data-placeholder="" tabindex="2"></select>
                                </div>
                                <div class="form-group col-md-4" id="divDOB">
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
                            </div>
                            <div class="row">
                                <div class="form-group col-md-8" id="divConfirmedBy">
                                    <label>
                                        Employee Name</label><span class="text-danger">*</span>
                                    <select id="ddlConfirmedBy" class="form-control select2" data-placeholder="" tabindex="4"></select>
                                </div>
                                <div class="form-group col-md-4" id="divBasicSalary">
                                    <label>
                                        Basic Salary</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtBasicSalary" placeholder="Please enter BasicSalary"
                                        maxlength="150" tabindex="5" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                            </div>
                            <div class="row">

                                <div class="form-group col-md-4" id="divConveyance">
                                    <label>
                                        Deduction</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtDeduction" placeholder="Incentives" maxlength="13"
                                        tabindex="6" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divPhoneNo2">
                                    <label>
                                        Incentives</label>
                                    <input type="text" class="form-control" id="txtIncentives" placeholder="Incentives" maxlength="13"
                                        tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divOverTimeIncentive">
                                    <label>
                                        OverTimeIncentive</label>
                                    <input type="text" class="form-control" id="txtOverTimeIncentive" placeholder="OverTime Incentive" maxlength="13"
                                        tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divInAdvanceDeduction">
                                    <label>
                                        In Advance Deduction</label>
                                    <input type="text" class="form-control" id="txtInAdvanceDeduction" placeholder="IN Advance Deduction" maxlength="13"
                                        tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-4" id="divAdvanceDeduction">
                                    <label>
                                        Advance Deduction</label>
                                    <input type="text" class="form-control" id="txtAdvanceDeduction" placeholder="Advance Deduction" maxlength="13"
                                        tabindex="10" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divAttendanceDeduction">
                                    <label>
                                        Attendance Deduction</label>
                                    <input type="text" class="form-control" id="txtAttendanceDeduction" placeholder=" Attendance Deduction" maxlength="13"
                                        tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-12" id="divAddress1">
                            <label>
                                Comments</label>
                            <textarea id="txtComments" class="form-control" maxlength="255" tabindex="12" rows="3" aria-autocomplete="none"></textarea>
                        </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divPaymentMode">
                                    <label>Payment Mode</label>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="13">
                                        <option value="Cash" selected="selected">Cash</option>
                                        <option value="GPay">GPay</option>
                                        <option value="Paytm">Paytm</option>
                                        <option value="IMPS">IMPS</option>
                                        <option value="NEFT/RTGS">NEFT/RTGS</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divReferenceNo">
                                    <label>
                                        Reference No</label>
                                    <input type="text" class="form-control" id="txtReferenceNo" placeholder="Reference No" maxlength="13"
                                        tabindex="14" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divNetSalary" style="font-size: 24px;">
                                    <label>
                                        Net Salary</label>
                                    <input type="text" class="form-control" id="txtNetSalary" style="font-size: 24px; font-weight: bold;" placeholder="NetSalary" maxlength="13"
                                        tabindex="-1" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="15">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="16">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="17">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdnStartDate" />
    <input type="hidden" id="hdnEndDate" />
    <input type="hidden" id="hdnSalaryType" />
    <input type="hidden" id="hdnSalaryCount" />
    <script type="text/javascript">

        var TotalAdvanceAmt = 0;
        var TotalPaidAmount = 0;
        var TotalOvertimeCount = 0;
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
            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
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
            CalculateYear();
            GetShiftList("ddlShift");
            GetEmployeeList("ddlConfirmedBy");
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#hdnStartDate").val("");
            $("#hdnEndDate").val("");
            $("#ddlConfirmedBy").val("0").change();
            $("#txtDate").val("");
            $("#txtBasicSalary").val("0");
            $("#txtDeduction").val("0");
            $("#txtIncentives").val("0");
            $("#txtOverTimeIncentive").val("0");
            $("#txtAdvanceDeduction").val("0");
            $("#txtInAdvanceDeduction").val("0");
            $("#txtAttendanceDeduction").val("0");
            $("#ddlPaymentMode").val("Cash");
            $("#txtReferenceNo").val("");
            $("#txtNetSalary").val("0");
            $("#hdnSalaryCount").val("0");    
            $("#txtComments").val("");
            TotalAdvanceAmt = 0;
            TotalPaidAmount = 0;
            TotalOvertimeCount = 0;

            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Salary");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#ddlConfirmedBy").focus();
            return false;
        });

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
        function GetShiftList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetShift",
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
                                            $(sControlName).append("<option value='" + obj[index].ShiftID + "'>" + obj[index].ShiftName + "</option>");
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

        $("#txtBasicSalary,#txtDeduction, #txtIncentives, #txtAdvanceDeduction,#txtOverTimeIncentive, #txtInAdvanceDeduction,#txtAttendanceDeduction").change(function () {
            CalculateTrans();
        });



        function CalculateTrans() {
            var iBasicSalary = parseFloat($("#txtBasicSalary").val());
            var iDeduction = parseFloat($("#txtDeduction").val());
            var itxtIncentives = parseFloat($("#txtIncentives").val());
            var iAdvanceDeduction = parseFloat($("#txtAdvanceDeduction").val());
            var iInAdvanceDeduction = parseFloat($("#txtInAdvanceDeduction").val());
            var iAttendanceDeduction = parseFloat($("#txtAttendanceDeduction").val());
            var iOvertimeIncentive = parseFloat($("#txtOverTimeIncentive").val());

            if (isNaN(iBasicSalary)) iBasicSalary = 0;
            if (isNaN(iDeduction)) iDeduction = 0;
            if (isNaN(itxtIncentives)) itxtIncentives = 0;
            if (isNaN(iAdvanceDeduction)) iAdvanceDeduction = 0;
            if (isNaN(iInAdvanceDeduction)) iInAdvanceDeduction = 0;
            if (isNaN(iAttendanceDeduction)) iAttendanceDeduction = 0;
            if (isNaN(iOvertimeIncentive)) iOvertimeIncentive = 0;

            var iAddAmount = parseFloat(iBasicSalary) + parseFloat(itxtIncentives) + parseFloat(iOvertimeIncentive);
            var iNeagtiveAmount = parseFloat(iAdvanceDeduction) + parseFloat(iInAdvanceDeduction) + parseFloat(iAttendanceDeduction) + parseFloat(iDeduction);
            var iTotalAmount = parseFloat(iAddAmount) - parseFloat(iNeagtiveAmount);

            $("#txtNetSalary").val(parseFloat(iTotalAmount).toFixed(2));
        }

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#ddlConfirmedBy").val() == "0" || $("#ddlConfirmedBy").val() == undefined || $("#ddlConfirmedBy").val() == null) {
                $.jGrowl("Please select Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
            } else { $("#divConfirmedBy").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDOB").addClass('has-error'); $("#txtDate").focus(); return false;
            } else { $("#divDOB").removeClass('has-error'); }

            if ($("#txtBasicSalary").val() == "" || $("#txtBasicSalary").val() == undefined || $("#txtBasicSalary").val() == null) {
                $.jGrowl("Please enter Basic Salary", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBasicSalary").addClass('has-error'); $("#txtBasicSalary").focus(); return false;
            } else { $("#divBasicSalary").removeClass('has-error'); }

            if ($("#hdnSalaryCount").val() > 0)
            {
                $.jGrowl("Already Entry Salary Details", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
            }
            

            var ObjOPBilling = new Object();

            ObjOPBilling.SalaryID = 0;

            var ObjEmployee = new Object();
            ObjEmployee.EmployeeID = $("#ddlConfirmedBy").val();
            ObjOPBilling.Employee = ObjEmployee;

            ObjOPBilling.MonthYear = $("#ddlMonth").val() + "-" + $("#ddlYear").val();
            ObjOPBilling.sSalaryDate = $("#txtDate").val();
            ObjOPBilling.BasicPay = $("#txtBasicSalary").val().trim();
            ObjOPBilling.Deduction = $("#txtDeduction").val().trim();
            ObjOPBilling.Incentives = $("#txtIncentives").val().trim();
            ObjOPBilling.OvertimeIncentive = $("#txtOverTimeIncentive").val().trim();
            ObjOPBilling.NetSalary = $("#txtNetSalary").val().trim();
            ObjOPBilling.PaymentMode = $("#ddlPaymentMode").val().trim();
            ObjOPBilling.ReferenceNo = $("#txtReferenceNo").val().trim();
            ObjOPBilling.AdvanceDeduction = $("#txtAdvanceDeduction").val().trim();
            ObjOPBilling.InAdvanceDeduction = $("#txtInAdvanceDeduction").val().trim();
            ObjOPBilling.AttendanceDeduction = $("#txtAttendanceDeduction").val().trim();
            ObjOPBilling.Comments = $("#txtComments").val().trim();
            ObjOPBilling.Active = true;

            if ($("#hdnID").val() > 0) {
                ObjOPBilling.SalaryID = $("#hdnID").val();
                sMethodName = "UpdateSalary";
            }
            else { sMethodName = "AddSalary"; }

            SaveandUpdateEmployeeSalary(ObjOPBilling, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function CalculateYear() {
            var currentYear = new Date().getFullYear();
            for (var i = currentYear; i > currentYear - 3; i--) {
                $("#ddlYear").append('<option value="' + i.toString() + '">' + i.toString() + '</option>');
            }
        }

        $("#ddlConfirmedBy").change(function () {
            if ($("#ddlConfirmedBy").val() > 0) {
                $("#txtInAdvanceDeduction").val("0").change();
                $("#hdnSalaryCount").val("0");    
                CalculateMonthYear();
                GetEmployeeAdvance();
                GetPaidEmployeeAdvance();
                GetEmployeeAttendance();
                GetEmployeeAdvanceAmount();
                GetEmployeeSalaryCount();
                GetEmployeeByID();
            }
        });

        function GetEmployeeByID() {
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
                                    $("#txtBasicSalary").val(obj.NetSalary).change();
                                    var OvertimeAllowance = obj.OvertimeAllowance * TotalOvertimeCount;
                                    $("#txtOverTimeIncentive").val(OvertimeAllowance).change();
                                    if (TotalAdvanceAmt > 0) {
                                        //var BalanceDeduction = 0;
                                        //BalanceDeduction = TotalAdvanceAmt - TotalPaidAmount;
                                        //if (BalanceDeduction > obj.AdvanceDeduction) {
                                        if (TotalAdvanceAmt >= TotalPaidAmount)
                                            $("#txtAdvanceDeduction").val(obj.AdvanceDeduction).change();
                                        else {
                                            var Balancen = 0;
                                            Balance = TotalAdvanceAmt - TotalPaidAmount;
                                            if (Balance > 0)
                                                $("#txtAdvanceDeduction").val(Balance).change();
                                            else
                                                $("#txtAdvanceDeduction").val("0").change();
                                        }
                                        //}
                                        //else
                                        //    $("#txtAdvanceDeduction").val(BalanceDeduction).change();
                                    }
                                    else
                                        $("#txtAdvanceDeduction").val("0").change();
                                    dProgress(false);
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

        function GetEmployeeAttendance() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetEmployeeAttendanceLogByID",
                data: JSON.stringify({ EmployeeID: $("#ddlConfirmedBy").val(), FromDate: $("#hdnStartDate").val(), ToDate: $("#hdnEndDate").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var DeductionAmt = 0;
                        TotalOvertimeCount = 0;
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    for (var index = 0; index < obj.length; index++) {
                                        DeductionAmt += obj[index].DeductionAmt;
                                        TotalOvertimeCount = TotalOvertimeCount + obj[index].OvertimeCount;
                                    }
                                }

                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                // $.jGrowl("No Rregregeecord", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                        $("#txtAttendanceDeduction").val(DeductionAmt).change();
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

        function GetEmployeeAdvance() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdvance",
                data: JSON.stringify({ IsRetail: $("#ddlConfirmedBy").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);

                                if (obj.length > 0) {

                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].AdvanceType == "Out Salary")
                                            TotalAdvanceAmt = TotalAdvanceAmt + obj[index].Amount;
                                    }
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

        function GetEmployeeAdvanceAmount() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdvanceAmount",
                data: JSON.stringify({ FromDate: $("#hdnStartDate").val(), ToDate: $("#hdnEndDate").val(), EmployeeID: $("#ddlConfirmedBy").val() }),
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
                                    $("#txtInAdvanceDeduction").val(obj.Amount).change();
                                }

                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                //  $.jGrowl("No Resafsacord", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                                $.jGrowl("No Reasfsafcord", { sticky: false, theme: 'warning', life: jGrowlLife });
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

        function GetEmployeeSalaryCount() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetEmployeeSalaryCount",
                data: JSON.stringify({ MonthYear: $("#ddlMonth").val() + "-" + $("#ddlYear").val(), iEmployeeID: $("#ddlConfirmedBy").val() }),
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
                                    $("#hdnSalaryCount").val(obj.SalaryID);
                                }

                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                //  $.jGrowl("No Resafsacord", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                                $.jGrowl("No Reasfsafcord", { sticky: false, theme: 'warning', life: jGrowlLife });
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

        function GetPaidEmployeeAdvance() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSalary",
                data: JSON.stringify({ CountryID: $("#ddlConfirmedBy").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    for (var index = 0; index < obj.length; index++) {
                                        TotalPaidAmount = TotalPaidAmount + obj[index].AdvanceDeduction;
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                //   $.jGrowl("No fasdfaRecord", { sticky: false, theme: 'warning', life: jGrowlLife });
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


        $("#ddlMonth").change(function () {
            if ($("#ddlConfirmedBy").val() > 0) {
                $("#txtInAdvanceDeduction").val("0").change();
                $("#hdnSalaryCount").val("0");                
                CalculateMonthYear();
                GetEmployeeAdvance();
                GetPaidEmployeeAdvance();
                GetEmployeeAttendance();
                GetEmployeeAdvanceAmount();
                GetEmployeeSalaryCount();
                GetEmployeeByID();
            }

        });
        function CalculateMonthYear() {
            var MonthValue = 0;
            var year = $("#ddlYear").val();
            var month = $("#ddlMonth").val();

            if ($("#ddlMonth").val() == "January")
                MonthValue = 0;
            else if ($("#ddlMonth").val() == "February")
                MonthValue = 1;
            else if ($("#ddlMonth").val() == "March")
                MonthValue = 2;
            else if ($("#ddlMonth").val() == "April")
                MonthValue = 3;
            else if ($("#ddlMonth").val() == "May")
                MonthValue = 4;
            else if ($("#ddlMonth").val() == "June")
                MonthValue = 5;
            else if ($("#ddlMonth").val() == "July")
                MonthValue = 6;
            else if ($("#ddlMonth").val() == "August")
                MonthValue = 7;
            else if ($("#ddlMonth").val() == "September")
                MonthValue = 8;
            else if ($("#ddlMonth").val() == "October")
                MonthValue = 9;
            else if ($("#ddlMonth").val() == "November")
                MonthValue = 10;
            else if ($("#ddlMonth").val() == "December")
                MonthValue = 11;

            $("#hdnStartDate").val(new Date(year, MonthValue, 1).format("dd/MM/yyyy"));
            $("#hdnEndDate").val(new Date(year, MonthValue + 1, 0).format("dd/MM/yyyy"));

        }

        $("#ddlYear").change(function () {
            if ($("#ddlConfirmedBy").val() > 0) {
                $("#txtInAdvanceDeduction").val("0").change();
                $("#hdnSalaryCount").val("0");    
                CalculateMonthYear();
                GetEmployeeAdvance();
                GetPaidEmployeeAdvance();
                GetEmployeeAttendance();
                GetEmployeeAdvanceAmount();
                GetEmployeeSalaryCount();
                GetEmployeeByID();
            }
        });

        function ClearFields() {
            $("#ddlConfirmedBy").val("0").change();
            $("#txtBasicSalary").val("0");
            $("#txtConveyance").val("0");
            $("#txtSpecialAllowance").val("0");
            $("#txtMedicalAllowance").val("0");
            $("#txtPF").val("0");
            $("#txtHRA").val("0");
            $("#txtESI").val("0");
            $("#txtOvertimeAllowance").val("0");
            $("#txtFoodAllowance").val("0");
            $("#txtNetSalary").val("0");
            $("#txtPaidLeaves").val("0");
            $("#txtAdvanceDeduction").val("0");
            $("#txtInAdvanceDeduction").val("0");
            $("#txtInTime").val("");
            $("#txtOuttime").val("");

            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSalary",
                data: JSON.stringify({ CountryID: 0 }),
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
                                        if (obj[index].Active == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].SalaryID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Employee.EmployeeName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].MonthYear + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sSalaryDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].BasicPay + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Deduction + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Incentives + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AdvanceDeduction + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AttendanceDeduction + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].NetSalary + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].PaymentMode + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalaryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalaryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalaryID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Salary");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
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
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
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

        function SaveandUpdateEmployeeSalary(Obj, sMethodName) {
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

                                if (sMethodName == "AddSalary")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateSalary")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Salary_A_01" || objResponse.Value == "Salary_U_01") {
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
                url: "WebServices/VHMSService.svc/GetSalaryByID",
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

                                    $("#hdnID").val(obj.SalaryID);

                                    $("#ddlConfirmedBy").val(obj.Employee.EmployeeID).change();
                                    var arr = obj.MonthYear.split('-');
                                    $("#ddlMonth").val(arr[0]).change();
                                    $("#ddlYear").val(arr[1]).change();
                                    $("#txtDate").val(obj.sSalaryDate);
                                    $("#txtBasicSalary").val(obj.BasicPay);
                                    $("#txtDeduction").val(obj.Deduction);
                                    $("#txtIncentives").val(obj.Incentives);
                                    $("#txtOverTimeIncentive").val(obj.OvertimeIncentive);
                                    $("#txtAdvanceDeduction").val(obj.AdvanceDeduction);
                                    $("#txtInAdvanceDeduction").val(obj.InAdvanceDeduction);
                                    $("#txtAttendanceDeduction").val(obj.AttendanceDeduction);
                                    $("#ddlPaymentMode").val(obj.PaymentMode);
                                    $("#txtReferenceNo").val(obj.ReferenceNo);
                                    $("#txtNetSalary").val(obj.NetSalary);
                                    $("#txtComments").val(obj.Comments);


                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Salary");
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
                url: "WebServices/VHMSService.svc/DeleteSalary",
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
                            else if (objResponse.Value == "Salary_R_01" || objResponse.Value == "Salary_D_01") {
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
