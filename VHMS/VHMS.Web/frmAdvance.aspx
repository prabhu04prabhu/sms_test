<%@ Page Title="Advance" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmAdvance.aspx.cs" Inherits="frmAdvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Advance
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Advance</li>
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
                                            <th>Type
                                            </th>
                                            <th>Type Name
                                            </th>
                                            <th>Date
                                            </th>
                                            <th>Salary Type
                                            </th>
                                            <th>Advance Type
                                            </th>
                                            <th>Amount
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
                            <div class="form-group col-md-6"" id="divAdvancetype">
                                <label>
                                   Type</label><span class="text-danger">*</span>
                                <select id="ddlAdvance" class="form-control" tabindex="3">
                                    <option value="Employee" selected="selected">Employee</option>
                                    <option value="Vendor">Vendor</option>
                                    <option value="Ledger">Ledger</option>
                                </select>
                            </div>
                            <div class="form-group col-md-6"" id="divConfirmedBy">
                                <label>
                                    Employee Name</label><span class="text-danger">*</span>
                                <select id="ddlConfirmedBy" class="form-control select2" data-placeholder="" tabindex="1"></select>
                            </div>
                               <div class="form-group col-md-6"" id="divVendor">
                                <label>
                                   Vendor Name</label><span class="text-danger">*</span>
                                <select id="ddlvendor" class="form-control select2" data-placeholder="" tabindex="1"></select>
                            </div>
                              <div class="form-group col-md-6"" id="divledger">
                                <label>
                                    Ledger Name</label><span class="text-danger">*</span>
                                <select id="ddlledger" class="form-control select2" data-placeholder="" tabindex="1"></select>
                            </div>
                            <div class="form-group col-md-6" id="divBillDate">
                                <label>
                                    Date of Given</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="1" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                            <div class="form-group col-md-6" id="divCategory">
                                <label>
                                    Salary Type</label><span class="text-danger">*</span>
                                <select id="ddlAdvanceType" class="form-control" tabindex="3">
                                    <option value="In Salary" selected="selected">In Salary</option>
                                    <option value="Out Salary">Out Salary</option>
                                </select>
                            </div>
                            <div class="form-group  col-md-6" id="divAmount">
                                <label>
                                    Amount</label>
                                <input type="text" class="form-control" id="txtAmount" placeholder="Amount" style="font-size: 104%; font-weight: bold;"
                                    maxlength="15" tabindex="4" value="0" onkeypress="return IsNumeric(event)" />
                            </div>
                              <div class="form-group col-md-6" id="divType">
                                <label>
                                    Advance Type</label><span class="text-danger">*</span>
                                <select id="ddlType" class="form-control" tabindex="3">
                                    <option value="Given" selected="selected">Given</option>
                                    <option value="Received">Received</option>
                                </select>
                            </div>
                                        <div class="form-group  col-md-6" id="divBalanceAmount">
                                <label>
                                    Balance Amount</label>
                                <input type="text" class="form-control" id="txtBalanceAmount" placeholder="Balance Amount" style="font-size: 104%; font-weight: bold;"
                                    maxlength="15" tabindex="4" value="0" onkeypress="return IsNumeric(event)" readonly />
                            </div>
                             <div style="clear:both">  </div>
                            <div class="form-group" id="divAddress1">
                                <label>
                                    Comments</label>
                                <textarea id="txtComments" class="form-control" maxlength="255" tabindex="5" rows="3" aria-autocomplete="none"></textarea>
                            </div>

                            <%--<div class="checkbox">
                                <label>
                                    <input type="checkbox" id="chkStatus" checked="checked" tabindex="4" />Active
                                </label>
                            </div>--%>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="6">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="7">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="8">
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

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            pLoadingSetup(false);
            $("#txtBillDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtBillDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            pLoadingSetup(true);
            GetEmployeeList("ddlConfirmedBy");
            GetVendorList("ddlvendor");
            GetLedgerList("ddlledger");
            GetRecord();
        });



        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#ddlConfirmedBy").val(0).change();
            $("#txtBillDate").focus();
            $("#ddlAdvanceType").val("In Salary");
            $("#ddlAdvance").val("Employee").change();
            $("#ddlConfirmedBy").val("0").change();
            $("#ddlvendor").val("0").change();
            $("#ddlledger").val("0").change();
            $("#txtAmount").val("0");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Advance");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            $("#txtComments").val("");


            return false;
        });



        $("#ddlAdvance").change(function () {
            if ($(this).val() == 'Employee') {
                $('#divledger').hide();
                $('#divVendor').hide();
                $('#divConfirmedBy').show();
            }
            else if ($(this).val() == 'Vendor') {
                $('#divVendor').show();
                $('#divConfirmedBy').hide();
                $('#divledger').hide();
            }
            else if ($(this).val() == 'Ledger') {
                $('#divledger').show();
                $('#divConfirmedBy').hide();
                $('#divVendor').hide();
            }
        });
        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            //if ($("#ddlConfirmedBy").val() == "0" || $("#ddlConfirmedBy").val() == undefined || $("#ddlConfirmedBy").val() == null) {
            //    $.jGrowl("Please select Employee Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
            //}
            //else { $("#divConfirmedBy").removeClass('has-error'); }

            if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null) {
                $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
            } else { $("#divAmount").removeClass('has-error'); }

            if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
                $.jGrowl("Please select Date of Given", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
            } else { $("#divBillDate").removeClass('has-error'); }

            var ObjOPBilling = new Object();

            ObjOPBilling.AdvanceID = 0;
            //  Obj.AdvanceName = $("#txtName").val().trim();

            var ObjEmployee = new Object();
            ObjEmployee.EmployeeID = $("#ddlConfirmedBy").val();
            ObjOPBilling.Employee = ObjEmployee;

            ObjOPBilling.sDateofGiven = $("#txtBillDate").val().trim();
            ObjOPBilling.AdvanceType = $("#ddlAdvanceType").val();
            ObjOPBilling.Advances = $("#ddlAdvance").val();
            ObjOPBilling.Amount = $("#txtAmount").val().trim();
            ObjOPBilling.Comments = $("#txtComments").val().trim();


            objLedger = new Object();
            objLedger.LedgerID = $("#ddlledger").val();
            ObjOPBilling.Ledger = objLedger;

            objVendor = new Object();
            objVendor.VendorID = $("#ddlvendor").val();
            ObjOPBilling.Vendor = objVendor;

            ObjOPBilling.Type = $("#ddlType").val();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                ObjOPBilling.AdvanceID = $("#hdnID").val();
                sMethodName = "UpdateAdvance";
            }
            else { sMethodName = "AddAdvance"; }

            SaveandUpdateAdvance(ObjOPBilling, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        $("#ddlConfirmedBy,#ddlAdvanceType").change(function () {
            if ($("#ddlConfirmedBy").val() > 0) {
                $("#txtBalanceAmount").val("0").change();
                if ($("#ddlAdvanceType").val() == 'Out Salary') {
                    GetAdvanceOutSalary($("#ddlConfirmedBy").val());
                }
                else if ($("#ddlAdvanceType").val() == 'In Salary') {
                    GetAdvanceInSalary($("#ddlConfirmedBy").val());
                }

            }
        });

        function GetAdvanceInSalary(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdvanceInSalary",
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
                                    $("#txtBalanceAmount").val(obj.Amount);
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
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetAdvanceOutSalary(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdvanceOutSalary",
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
                                    $("#txtBalanceAmount").val(obj.Amount);
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
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtOutTime").val("");
            $("#chkStatus").prop("checked", true);

            $("#divName").removeClass('has-error');
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtBillDate").val(d + "/" + m + "/" + y);
            return false;
        }

        function GetVendorList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetVendor",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].VendorID + "'>" + obj[index].VendorName + "</option>");
                                    }
                                    $("#ddlvendor").val($("#ddlvendor option:first").val()).change();
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



        function GetLedgerList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLedger",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive && (obj[index].LedgerType.LedgerTypeID == 18 || obj[index].LedgerType.LedgerTypeID == 17))
                                            $(sControlName).append("<option value='" + obj[index].LedgerID + "'>" + obj[index].LedgerName + "</option>");
                                    }
                                    $("#ddlledger").val($("#ddlledger option:first").val()).change();
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

        //function GetLedgerList() {
        //    dProgress(true);
        //    $("#ddlledger").empty();
        //    $.ajax({
        //        type: "POST",
        //        url: "WebServices/VHMSService.svc/GetLedger",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (data) {
        //            if (data.d != "") {
        //                var objResponse = jQuery.parseJSON(data.d);
        //                if (objResponse.Status == "Success") {
        //                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
        //                        var obj = $.parseJSON(objResponse.Value);
        //                        if (obj.length > 0) {
        //                            for (var index = 0; index < obj.length; index++) {
        //                                if (obj[index].IsActive)
        //                                    $("#ddlledger").append('<option value=' + obj[index].LedgerID + ' >' + obj[index].LedgerName + '</option>');
        //                            }
        //                        }
        //                        dProgress(false);
        //                    }
        //                    else if (objResponse.Value == "NoRecord") {
        //                        $("#ddlledger").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
        //                        dProgress(false);
        //                    }
        //                }
        //                else if (objResponse.Status == "Error") {
        //                    if (objResponse.Value == "0") {
        //                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        //                        window.location = "frmLogin.aspx";
        //                    }
        //                    else if (objResponse.Value == "Error") {
        //                        window.location = "frmErrorPage.aspx";
        //                    }
        //                    dProgress(false);
        //                }
        //            }
        //            else {
        //                $("#ddlledger").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
        //                dProgress(false);
        //            }
        //        },
        //        error: function (e) {
        //            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        //            dProgress(false);
        //        }
        //    });
        //    return false;
        //}

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAdvance",
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

                                        var table = "<tr id='" + obj[index].AdvanceID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Advances + "</td>";
                                        table += "<td>" + obj[index].PartyName + "</td>";
                                        table += "<td>" + obj[index].sDateofGiven + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].AdvanceType + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Type + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Amount + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AdvanceID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AdvanceID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AdvanceID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Advance");
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
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "40%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
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

                                if (sMethodName == "AddAdvance") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateAdvance") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

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
                url: "WebServices/VHMSService.svc/GetAdvanceByID",
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

                                    $("#hdnID").val(obj.AdvanceID);
                                    $("#txtAmount").val(obj.Amount);
                                    $("#txtBillDate").val(obj.sDateofGiven);
                                    $("#ddlConfirmedBy").val(obj.Employee.EmployeeID).change();
                                    $("#ddlledger").val(obj.Ledger.LedgerID).change();
                                    $("#ddlvendor").val(obj.Vendor.VendorID).change();
                                    $("#ddlAdvanceType").val(obj.AdvanceType);
                                    $("#ddlAdvance").val(obj.Advances);
                                    $("#ddlType").val(obj.Type);
                                    $("#txtComments").val(obj.Comments);
                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Advance");
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
                url: "WebServices/VHMSService.svc/DeleteAdvance",
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

        //$("#ddlvendor").change(function () {
        //    if ($("#ddlvendor").val() > 0) {
        //        GetVendorByID($("#ddlvendor").val());
        //    }
        //});

        //function GetVendorByID(id) {
        //    dProgress(true);
        //    $.ajax({
        //        type: "POST",
        //        url: "WebServices/VHMSService.svc/GetVendorByID",
        //        data: JSON.stringify({ ID: id }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (data) {
        //            if (data.d != "") {
        //                var objResponse = jQuery.parseJSON(data.d);
        //                if (objResponse.Status == "Success") {
        //                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
        //                        var obj = jQuery.parseJSON(objResponse.Value);
        //                        if (obj != null) {
        //                            $("#hdnStateCode").val(obj.State.StateCode);
        //                        }
        //                        dProgress(false);
        //                    }
        //                    else if (objResponse.Value == "NoRecord") {
        //                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
        //                        dProgress(false);
        //                    }
        //                    else if (objResponse.Value == "Error") {
        //                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
        //                    }
        //                }
        //                else if (objResponse.Status == "Error") {
        //                    if (objResponse.Value == "0") {
        //                        window.location("frmLogin.aspx");
        //                    }
        //                    else if (objResponse.Value == "Error") {
        //                        window.location = "frmErrorPage.aspx";
        //                    }
        //                    else if (objResponse.Value == "NoRecord") {
        //                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
        //                    }
        //                }
        //            }
        //            else {
        //                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        //                dProgress(false);
        //            }
        //        },
        //        error: function (e) {
        //            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        //            dProgress(false);
        //        }
        //    });
        //    return false;
        //}

    </script>
</asp:Content>
