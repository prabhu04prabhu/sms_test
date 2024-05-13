<%@ Page Title="VendorPayment" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmVendorPayment.aspx.cs" Inherits="frmVendorPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>VendorPayment
            </h1>
            <small></small>
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
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
                                            <th>S.No</th>
                                            <th>Voucher #</th>
                                            <th>Voucher Dt</th>
                                            <th class="hidden-xs">Vendor</th>
                                            <th class="hidden-xs">Account</th>
                                            <th class="hidden-xs">Payment Mode</th>
                                            <th class="hidden-xs">Amount</th>
                                            <th></th>
                                            <%--<th></th>--%>
                                            <th></th>
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
                            <div class="row">
                                <div class="form-group col-md-4" id="divVoucherNo">
                                    <label>
                                        Voucher No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtVoucherNo" placeholder="Voucher No."
                                        maxlength="150" tabindex="1" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divVoucherDate">
                                    <label>
                                        Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtVoucherDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="2" id="txtVoucherDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divVendor">
                                    <label>
                                        Vendor</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlVendor" class="form-control select2" data-placeholder="Select Vendor" tabindex="3">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divBank">
                                    <label>
                                        Select A/c</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="4">
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divPaymentMode">
                                    <label>
                                        Payment Mode</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="5">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Cheque</option>
                                        <option value="3">NEFT/RTGS</option>
                                        <option value="4">Others</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divAmount">
                                    <label>Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtAmount" placeholder="Amount"
                                            maxlength="15" tabindex="6" autocomplete="off" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divChequeDetails">
                                <div class="form-group col-md-4" id="divChequeNo">
                                    <label>
                                        Cheque/DD #</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                        maxlength="150" tabindex="7" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divIssueDate">
                                    <label>
                                        Issued Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="8" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divCollectionDate">
                                    <label>
                                        Collection Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="9" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divIssuedBy">
                                    <label>
                                        Issued By</label>
                                    <input type="text" class="form-control" id="txtIssuedBy" placeholder="Issued By"
                                        maxlength="150" tabindex="10" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group" id="divDescription">
                                <label>
                                    Description</label>
                                <textarea id="txtDescription" class="form-control" maxlength="250" tabindex="11" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="12">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="13">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="14">
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
            pLoadingSetup(false);
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

            $(".decimal").inputmask("decimal", { digits: 2, radixPoint: "." });

            $("#txtVoucherDate,#txtIssueDate,#txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtVoucherDate,#txtIssueDate,#txtCollectionDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });

            GetBankList();
            GetVendorList("ddlVendor");

            $("#divChequeDetails").hide();
            pLoadingSetup(true);

            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add VendorPayment");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtVoucherNo").focus();
            return false;
        });
        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate") { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtVoucherDate").val().trim() == "" || $("#txtVoucherDate").val().trim() == undefined) {
                $.jGrowl("Please select Voucher Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divVoucherDate").addClass('has-error'); $("#txtVoucherDate").focus(); return false;
            } else { $("#divVoucherDate").removeClass('has-error'); }

            if ($("#ddlVendor").val() == "0" || $("#ddlVendor").val() == undefined || $("#ddlVendor").val() == null) {
                $.jGrowl("Please select Voucher Type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divVendor").addClass('has-error'); $("#ddlVendor").focus(); return false;
            } else { $("#divVendor").removeClass('has-error'); }

            if ($("#ddlBank").val() == "0" || $("#ddlBank").val() == undefined || $("#ddlBank").val() == null) {
                $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBank").addClass('has-error'); $("#ddlBank").focus(); return false;
            } else { $("#divBank").removeClass('has-error'); }

            if ($("#ddlPaymentMode").val() == "0" || $("#ddlPaymentMode").val() == undefined || $("#ddlPaymentMode").val() == null) {
                $.jGrowl("Please select VendorPayment Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPaymentMode").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
            } else { $("#divPaymentMode").removeClass('has-error'); }

            if ($("#txtAmount").val().trim() == "" || $("#txtAmount").val().trim() == undefined) {
                $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
            } else { $("#divAmount").removeClass('has-error'); }

            if (!isNaN($("#txtAmount").val())) {
                if ($("#txtAmount").val() == 0) {
                    $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
                }
            }

            if ($("#ddlPaymentMode").val() == 2 || $("#ddlPaymentMode").val() == 3) {
                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Cheque/DD No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }

                if ($("#txtIssueDate").val().trim() == "" || $("#txtIssueDate").val().trim() == undefined) {
                    $.jGrowl("Please select Issue Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divIssueDate").addClass('has-error'); $("#txtIssueDate").focus(); return false;
                } else { $("#divIssueDate").removeClass('has-error'); }

                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Collection Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                } else { $("#divCollectionDate").removeClass('has-error'); }
            }

            var Obj = new Object();
            Obj.VendorPaymentID = 0;
            Obj.VoucherNo = $("#txtVoucherNo").val().trim();
            Obj.sVoucherDate = $("#txtVoucherDate").val();

            var objVendor = new Object();
            objVendor.VendorID = $("#ddlVendor").val();
            Obj.Vendor = objVendor;

            var objBank = new Object();
            objBank.LedgerID = $("#ddlBank").val();
            Obj.Bank = objBank;

            Obj.PaymentModeID = $("#ddlPaymentMode").val();
            Obj.Amount = parseFloat($("#txtAmount").val().trim());
            if ($("#ddlPaymentMode").val() == 2 || $("#ddlPaymentMode").val() == 3) {
                Obj.ChequeNo = $("#txtChequeNo").val().trim();
                Obj.sIssueDate = $("#txtIssueDate").val();
                Obj.sCollectionDate = $("#txtCollectionDate").val();
                Obj.IssuedBy = $("#txtIssuedBy").val().trim();
            }
            Obj.Description = $("#txtDescription").val().trim();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.VendorPaymentID = $("#hdnID").val();
                sMethodName = "UpdateVendorPayment";
            }
            else { sMethodName = "AddVendorPayment"; }

            SaveandUpdateVendorPayment(Obj, sMethodName);

            return false;
        });
        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        $("#ddlPaymentMode").change(function () {
            $("#divChequeDetails").hide();
            var iPaymentMode = $("#ddlPaymentMode").val();
            if (iPaymentMode != undefined && iPaymentMode > 0) {
                if (iPaymentMode == 2 || iPaymentMode == 3)
                    $("#divChequeDetails").show();
            }
            else {
                $("#divChequeDetails").hide();
            }

            return false;
        });

        function ClearFields() {
            $("#txtVoucherNo").val("");
            $("#txtVoucherDate").val("");
            $("#ddlVendor").val(null).change();
            $("#ddlBank").val(null).change();
            $("#ddlPaymentMode").val(0);
            $("#txtAmount").val("0");
            $("#txtChequeNo").val("");
            $("#txtIssueDate").val("");
            $("#txtCollectionDate").val("");
            $("#txtIssuedBy").val("");
            $("#txtDescription").val("");

            $("#divChequeDetails").hide();
            return false;
        }
        function GetBankList() {
            dProgress(true);
            $("#ddlBank").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLedgerBank",
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive) {
                                            $("#ddlBank").append('<option value=' + obj[index].LedgerID + ' >' + obj[index].LedgerName + '</option>');
                                        }
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlBank").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlBank").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].VendorID + "'>" + obj[index].VendorName + "</option>");
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

        function SaveandUpdateVendorPayment(Obj, sMethodName) {
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

                                if (sMethodName == "AddVendorPayment") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateVendorPayment") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "VendorPayment_A_01" || objResponse.Value == "VendorPayment_U_01") {
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
        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetVendorPayment",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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
                                    var sPaymentMode = "";
                                    for (var index = 0; index < obj.length; index++) {

                                        if (obj[index].PaymentModeID == 1)
                                            sPaymentMode = "Cash";
                                        else if (obj[index].PaymentModeID == 2)
                                            sPaymentMode = "Cheque";
                                        else if (obj[index].PaymentModeID == 3)
                                            sPaymentMode = "NEFT/RTGS";
                                        else if (obj[index].PaymentModeID == 4)
                                            sPaymentMode = "Others";

                                        var table = "<tr id='" + obj[index].VendorPaymentID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].VoucherNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sVoucherDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Vendor.VendorName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Bank.LedgerName + "</td>";
                                        table += "<td class='hidden-xs'>" + sPaymentMode + "</td>";
                                        table += "<td class='hidden-xs'>" + parseFloat(obj[index].Amount).toFixed(2) + "</td>";

                                        if (ActionView == "1") { table += "<td><a href='#' id=" + obj[index].VendorPaymentID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        //if (ActionUpdate == "1")
                                        //{ table += "<td><a href='#' id=" + obj[index].VendorPaymentID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        //else
                                        //{ table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td ><a href='#' id=" + obj[index].VendorPaymentID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View VendorPayment");
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
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "12%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "1%" },
                                    //{ "sWidth": "1%" },
                                    { "sWidth": "1%" }
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
        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetVendorPaymentByID",
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
                                    ClearFields();

                                    $("#hdnID").val(obj.VendorPaymentID);
                                    $("#txtVoucherNo").val(obj.VoucherNo);
                                    $("#txtVoucherDate").val(obj.sVoucherDate);
                                    $("#ddlVendor").val(obj.Vendor.VendorID).change();
                                    $("#ddlBank").val(obj.Bank.LedgerID).change();
                                    $("#ddlPaymentMode").val(obj.PaymentModeID);
                                    $("#txtAmount").val(obj.Amount);

                                    if (obj.PaymentModeID == 2 || obj.PaymentModeID == 3) {
                                        $("#divChequeDetails").show();
                                        $("#txtChequeNo").val(obj.ChequeNo);
                                        $("#txtIssueDate").val(obj.sIssueDate);
                                        $("#txtCollectionDate").val(obj.sCollectionDate);
                                        $("#txtIssuedBy").val(obj.IssuedBy);
                                    }
                                    $("#txtDescription").val(obj.Description);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit VendorPayment");
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
                url: "WebServices/VHMSService.svc/DeleteVendorPayment",
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
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "VendorPayment_R_01" || objResponse.Value == "VendorPayment_D_01") {
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

