<%@ Page Title="Employee Salary" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmEmployeeSalary.aspx.cs" Inherits="frmEmployeeSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Employee Salary
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Employee Salary</li>
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
                                            <th>PhoneNo1
                                            </th>
                                            <th>Gender
                                            </th>
                                            <th>BasicPay
                                            </th>
                                            <th>Special Allowance
                                            </th>
                                            <th>MedicalAllowance
                                            </th>
                                            <th>PF
                                            </th>
                                            <th>NetSalary
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
                <div class="modal-dialog modal-lg">
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
                            <div class="row">
                                <div class="form-group col-md-4" id="divShift">
                                    <label>
                                        Shift</label><span class="text-danger">*</span>
                                    <select id="ddlShift" class="form-control select2" data-placeholder="" tabindex="2"></select>
                                </div>
                                <div class="form-group  col-md-4" id="divInTime">
                                    <label>
                                        In Time</label>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtInTime"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="3" id="txtInTime" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divOutTime">
                                    <label>
                                        Out Time</label>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOuttime"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="4" id="txtOuttime" readonly="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divBasicSalary">
                                    <label>
                                        Basic Salary</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtBasicSalary" placeholder="Please enter BasicSalary"
                                        maxlength="150" tabindex="5" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divPF">
                                    <label>
                                        PF</label>
                                    <input type="text" class="form-control" id="txtPF" placeholder="PF" maxlength="13"
                                        tabindex="6" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-4" id="divOvertimeAllowance">
                                    <label>
                                        Overtime Allowance</label>
                                    <input type="text" class="form-control" id="txtOvertimeAllowance" placeholder="Overtime Allowance" maxlength="13"
                                        tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-4" id="divMedicalAllowance">
                                    <label>
                                        Medical Allowance</label>
                                    <input type="text" class="form-control" id="txtMedicalAllowance" placeholder="Medical Allowance" maxlength="13"
                                        tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divESI">
                                    <label>
                                        ESI</label>
                                    <input type="text" class="form-control" id="txtESI" placeholder="ESI" maxlength="13"
                                        tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-4" id="divAdvanceDeduction">
                                    <label>
                                        AdvanceDeduction</label>
                                    <input type="text" class="form-control" id="txtAdvanceDeduction" placeholder="AdvanceDeduction" maxlength="13"
                                        tabindex="10" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divHRA">
                                    <label>
                                        HRA (House Rent)</label>
                                    <input type="text" class="form-control" id="txtHRA" placeholder="HRA" maxlength="13"
                                        tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divConveyance">
                                    <label>
                                        Conveyance</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtConveyance" placeholder="Conveyance" maxlength="13"
                                        tabindex="12" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divFoodAllowance">
                                    <label>
                                        Food Allowance</label>
                                    <input type="text" class="form-control" id="txtFoodAllowance" placeholder="Food Allowance" maxlength="13"
                                        tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divPaidLeaves" style="display: none;">
                                    <label>
                                        PaidLeaves</label>
                                    <input type="text" class="form-control" id="txtPaidLeaves" placeholder="Paid Leaves" maxlength="13"
                                        tabindex="14" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divPhoneNo2">
                                    <label>
                                        Special Allowance</label>
                                    <input type="text" class="form-control" id="txtSpecialAllowance" placeholder="Special Allowance" maxlength="13"
                                        tabindex="15" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" style="font-size: 24px; margin-top: 22px;">
                                    <label>
                                        Net Salary</label>
                                </div>
                                <div class="form-group col-md-4" id="divNetSalary" style="font-size: 24px; margin-top: 22px;">
                                    <input type="text" class="form-control" id="txtNetSalary" style="font-size: 24px; font-weight: bold;" placeholder="NetSalary" maxlength="13"
                                        tabindex="-1" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="16">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="17">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="18">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
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

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("#txtInTime,#txtOuttime").attr("data-link-format", "dd/MM/yyyy");

            $("#txtInTime,#txtOuttime").datetimepicker({
                pickTime: true,
                pickDate: false,
                useCurrent: true,
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
            GetShiftList("ddlShift");
            GetEmployeeList("ddlConfirmedBy");
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#ddlConfirmedBy").val("0").change();
            $("#ddlShift").val("0").change();
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
            $("#txtAdvanceDeduction").val("0");
            $("#txtPaidLeaves").val("0");

            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Employee Salary");
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

        $("#txtBasicSalary,#txtConveyance, #txtSpecialAllowance, #txtMedicalAllowance").change(function () {
            CalculateTrans();
        });

        $("#txtPF,#txtHRA, #txtESI,#txtFoodAllowance").change(function () {
            CalculateTrans();
        });


        $("#txtNetSalary").change(function () {
            var perdaysalary = $("#txtNetSalary").val() / 30;
            var RoundOff = Math.round(perdaysalary);
            $("#txtOvertimeAllowance").val(parseFloat(RoundOff));
        });

        function CalculateTrans() {
            var iBasicSalary = parseFloat($("#txtBasicSalary").val());
            var iConveyance = parseFloat($("#txtConveyance").val());
            var iSpecialAllowance = parseFloat($("#txtSpecialAllowance").val());
            var iMedicalAllowance = parseFloat($("#txtMedicalAllowance").val());
            var iPF = parseFloat($("#txtPF").val());
            var iHRA = parseFloat($("#txtHRA").val());
            var iESI = parseFloat($("#txtESI").val());
            // var iOvertimeAllowance = parseFloat($("#txtOvertimeAllowance").val());
            var iFoodAllowance = parseFloat($("#txtFoodAllowance").val());

            if (isNaN(iBasicSalary)) iBasicSalary = 0;
            if (isNaN(iConveyance)) iConveyance = 0;
            if (isNaN(iSpecialAllowance)) iSpecialAllowance = 0;
            if (isNaN(iMedicalAllowance)) iMedicalAllowance = 0;
            if (isNaN(iPF)) iPF = 0;
            if (isNaN(iHRA)) iHRA = 0;
            if (isNaN(iESI)) iESI = 0;
            //if (isNaN(iOvertimeAllowance)) iOvertimeAllowance = 0;
            if (isNaN(iFoodAllowance)) iFoodAllowance = 0;

            var iAddAmount = parseFloat(iBasicSalary) + parseFloat(iConveyance) + parseFloat(iSpecialAllowance) + parseFloat(iMedicalAllowance) + parseFloat(iHRA) + parseFloat(iFoodAllowance);
            var iNeagtiveAmount = parseFloat(iPF) + parseFloat(iESI);
            var iTotalAmount = parseFloat(iAddAmount) - parseFloat(iNeagtiveAmount);

            $("#txtNetSalary").val(parseFloat(iTotalAmount).toFixed(2)).change();
        }

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#ddlConfirmedBy").val() == "0" || $("#ddlConfirmedBy").val() == undefined || $("#ddlConfirmedBy").val() == null) {
                $.jGrowl("Please select Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
            }
            else { $("#divConfirmedBy").removeClass('has-error'); }

            if ($("#txtBasicSalary").val() == "" || $("#txtBasicSalary").val() == undefined || $("#txtBasicSalary").val() == null) {
                $.jGrowl("Please enter Basic Salary", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBasicSalary").addClass('has-error'); $("#txtBasicSalary").focus(); return false;
            } else { $("#divBasicSalary").removeClass('has-error'); }

            var ObjOPBilling = new Object();

            ObjOPBilling.EmployeeID = 0;

            var ObjShift = new Object();
            ObjShift.ShiftID = $("#ddlShift").val();
            ObjOPBilling.Shift = ObjShift;

            ObjOPBilling.EmployeeInTime = $("#txtInTime").val();
            ObjOPBilling.EmployeeOutTime = $("#txtOuttime").val();
            ObjOPBilling.EmployeeID = $("#ddlConfirmedBy").val();
            ObjOPBilling.BasicPay = $("#txtBasicSalary").val().trim();
            ObjOPBilling.Conveyance = $("#txtConveyance").val().trim();
            ObjOPBilling.SpecialAllowance = $("#txtSpecialAllowance").val().trim();
            ObjOPBilling.MedicalAllowance = $("#txtMedicalAllowance").val().trim();
            ObjOPBilling.PF = $("#txtPF").val().trim();
            ObjOPBilling.HRA = $("#txtHRA").val().trim();
            ObjOPBilling.ESI = $("#txtESI").val().trim();
            ObjOPBilling.OvertimeAllowance = $("#txtOvertimeAllowance").val().trim();
            ObjOPBilling.FoodAllowance = $("#txtFoodAllowance").val().trim();
            ObjOPBilling.PaidLeaves = $("#txtPaidLeaves").val().trim();
            ObjOPBilling.AdvanceDeduction = $("#txtAdvanceDeduction").val().trim();
            ObjOPBilling.NetSalary = $("#txtNetSalary").val().trim();

            var sMethodName;
            sMethodName = "UpdateEmployeeAmount";

            SaveandUpdateEmployeeSalary(ObjOPBilling, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        $("#ddlShift").change(function () {
            if ($("#ddlShift").val() > 0) {
                GetShiftByID();
            }
        });

        function GetShiftByID() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetShiftByID",
                data: JSON.stringify({ ID: $("#ddlShift").val() }),
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
                                    $("#txtInTime").val(obj.InTime);
                                    $("#txtOuttime").val(obj.OutTime);
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

        function ClearFields() {
            $("#ddlConfirmedBy").val("0").change();
            $("#ddlShift").val("0").change();
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
            $("#txtInTime").val("");
            $("#txtOuttime").val("");

            $("#divName").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
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
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].Shift.ShiftID > 0) {
                                            if (obj[index].IsActive == "1")
                                            { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                            else
                                            { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                            var table = "<tr id='" + obj[index].EmployeeID + "'>";
                                            table += "<td>" + (index + 1) + "</td>";
                                            table += "<td>" + obj[index].EmployeeName + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].PhoneNo1 + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].Gender + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].BasicPay + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].SpecialAllowance + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].MedicalAllowance + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].PF + "</td>";
                                            table += "<td class='hidden-xs'>" + obj[index].NetSalary + "</td>";

                                            if (ActionView == "1")
                                            { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                            else { table += "<td></td>"; }

                                            if (ActionUpdate == "1")
                                            { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                            else
                                            { table += "<td></td>"; }

                                            if (ActionDelete == "1")
                                            { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].EmployeeID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                            else
                                            { table += "<td></td>"; }

                                            table += "</tr>";
                                            $("#tblRecord_tbody").append(table);
                                        }
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Employee Salary");
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
                                    { "sWidth": "35%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
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

                                if (sMethodName == "AddEmployee Salary")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateEmployeeAmount")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Employee Salary_A_01" || objResponse.Value == "Employee Salary_U_01") {
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
                url: "WebServices/VHMSService.svc/GetEmployeeByID",
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

                                    $("#hdnID").val(obj.EmployeeID);

                                    $("#ddlConfirmedBy").val(obj.EmployeeID).change();
                                    $("#ddlShift").val(obj.Shift.ShiftID).change();
                                    $("#txtBasicSalary").val(obj.BasicPay);
                                    $("#txtInTime").val(obj.EmployeeInTime);
                                    $("#txtOuttime").val(obj.EmployeeOutTime);
                                    $("#txtConveyance").val(obj.Conveyance);
                                    $("#txtSpecialAllowance").val(obj.SpecialAllowance);
                                    $("#txtMedicalAllowance").val(obj.MedicalAllowance);
                                    $("#txtPF").val(obj.PF);
                                    $("#txtHRA").val(obj.HRA);
                                    $("#txtESI").val(obj.ESI);
                                    $("#txtOvertimeAllowance").val(obj.OvertimeAllowance);
                                    $("#txtFoodAllowance").val(obj.FoodAllowance);
                                    $("#txtNetSalary").val(obj.NetSalary);
                                    $("#txtAdvanceDeduction").val(obj.AdvanceDeduction);
                                    $("#txtPaidLeaves").val(obj.PaidLeaves);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Employee Salary");
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
                url: "WebServices/VHMSService.svc/DeleteEmployee",
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
                            else if (objResponse.Value == "Employee Salary_R_01" || objResponse.Value == "Employee Salary_D_01") {
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
