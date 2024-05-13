<%@ Page Title="Barter Receipt" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmExchangeReceipt.aspx.cs" Inherits="frmExchangeReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Barter Receipt
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
                                            <th class="hidden-xs">Customer</th>
                                            <th class="hidden-xs">Product Name</th>
                                            <th class="hidden-xs">Quantity</th>
                                            <th class="hidden-xs">Rate</th>
                                            <th class="hidden-xs">Amount</th>
                                            <th></th>
                                            <th></th>
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
                                <div class="form-group col-md-4" id="divCustomer">
                                    <label>
                                        Customer</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlCustomer" class="form-control select2" data-placeholder="Select Customer" tabindex="3">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divProductName">
                                    <label>Product Name</label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtProductName" placeholder="Product Name"
                                        maxlength="500" tabindex="4" autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-4" id="divQuantity">
                                    <label>Quantity</label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control decimal" id="txtQuantity" placeholder="Quantity"
                                        maxlength="15" tabindex="5" autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-4" id="divRate">
                                    <label>Rate</label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control decimal" id="txtRate" placeholder="Rate"
                                        maxlength="15" tabindex="6" autocomplete="off"/>
                                </div>
                                <div class="form-group col-md-4" id="divAmount">
                                    <label>Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtAmount"style="font-size: 104%;font-weight: bold;" placeholder="Amount"
                                            maxlength="15" tabindex="7" autocomplete="off"/>
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divDescription">
                                    <label>
                                        Description</label>
                                    <textarea id="txtDescription" class="form-control" maxlength="250" tabindex="8" rows="3" aria-autocomplete="none"></textarea>
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="9">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="10">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="11">
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

            $("#txtIssueDate,#txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtIssueDate,#txtCollectionDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });


            $("#txtVoucherDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtVoucherDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            GetCustomerList();

            $("#divChequeDetails").hide();
            pLoadingSetup(true);

            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Barter Receipt");
            $('#compose-modal').modal({ show: true, backdrop: true });
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtVoucherDate").val(d + "/" + m + "/" + y);
            $("#txtVoucherNo").focus();
            return false;
        });

        $("#txtRate,#txtQuantity").change(function () {

            var iRate = parseFloat($("#txtRate").val());
            var iqty = parseFloat($("#txtQuantity").val());

            if (isNaN(iRate)) iRate = 0;
            if (isNaN(iqty)) iqty = 0;

            var iSubTotal = parseFloat(iRate) * parseFloat(iqty);
            $("#txtAmount").val(parseFloat(iSubTotal).toFixed(2));

        });

        $("#btnSave,#btnUpdate").click(function () {

            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate")
            { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            
            if ($("#txtVoucherDate").val().trim() == "" || $("#txtVoucherDate").val().trim() == undefined) {
                $.jGrowl("Please select Voucher Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divVoucherDate").addClass('has-error'); $("#txtVoucherDate").focus(); return false;
            } else { $("#divVoucherDate").removeClass('has-error'); }

            if ($("#ddlCustomer").val() == "0" || $("#ddlCustomer").val() == undefined || $("#ddlCustomer").val() == null) {
                $.jGrowl("Please select Voucher Type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCustomer").addClass('has-error'); $("#ddlCustomer").focus(); return false;
            } else { $("#divCustomer").removeClass('has-error'); }

            if ($("#txtProductName").val().trim() == "" || $("#txtProductName").val().trim() == undefined) {
                $.jGrowl("Please enter product name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divProductName").addClass('has-error'); $("#txtProductName").focus(); return false;
            } else { $("#divProductName").removeClass('has-error'); }

            if ($("#txtQuantity").val().trim() == "" || $("#txtQuantity").val().trim() == undefined) {
                $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
            } else { $("#divQuantity").removeClass('has-error'); }

            if ($("#txtRate").val().trim() == "" || $("#txtRate").val().trim() == undefined) {
                $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
            } else { $("#divRate").removeClass('has-error'); }

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
            
            var Obj = new Object();
            Obj.ExchangeReceiptID = 0;
            Obj.VoucherNo = $("#txtVoucherNo").val().trim();
            Obj.sVoucherDate = $("#txtVoucherDate").val();

            var objCustomer = new Object();
            objCustomer.CustomerID = $("#ddlCustomer").val();
            Obj.Customer = objCustomer;

            Obj.ProductName = $("#txtProductName").val().trim();
            Obj.Quantity = parseFloat($("#txtQuantity").val().trim());
            Obj.Rate = parseFloat($("#txtRate").val().trim());
            Obj.Amount = parseFloat($("#txtAmount").val().trim());
            Obj.Narration = $("#txtDescription").val().trim();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ExchangeReceiptID = $("#hdnID").val();
                sMethodName = "UpdateExchangeReceipt";
            }
            else { sMethodName = "AddExchangeReceipt"; }

            SaveandUpdateExchangeReceipt(Obj, sMethodName);

            return false;
        });
        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtVoucherNo").val("");
            $("#txtVoucherDate").val("");
            $("#ddlCustomer").val(null).change();
            $("#txtProductName").val("");
            $("#txtQuantity").val("0");
            $("#txtRate").val("0");
            $("#txtAmount").val("0");
            $("#txtDescription").val("");

            return false;
        }

        function GetCustomerList() {
            dProgress(true);
            $("#ddlCustomer").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomer",
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
                                            $("#ddlCustomer").append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '</option>');
                                        }
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

        function SaveandUpdateExchangeReceipt(Obj, sMethodName) {
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

                                if (sMethodName == "AddExchangeReceipt")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateExchangeReceipt")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "ExchangeReceipt_A_01" || objResponse.Value == "ExchangeReceipt_U_01") {
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
                url: "WebServices/VHMSService.svc/GetExchangeReceipt",
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
                                    var sExchangeReceiptMode = "";
                                    for (var index = 0; index < obj.length; index++) {

                                        var table = "<tr id='" + obj[index].ExchangeReceiptID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].VoucherNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sVoucherDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].ProductName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Quantity + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Rate + "</td>";
                                        table += "<td class='hidden-xs'>" + parseFloat(obj[index].Amount).toFixed(2) + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td><a href='#' id=" + obj[index].ExchangeReceiptID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td><a href='#' id=" + obj[index].ExchangeReceiptID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td ><a href='#' id=" + obj[index].ExchangeReceiptID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Barter Receipt");
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
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
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
        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetExchangeReceiptByID",
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

                                    $("#hdnID").val(obj.ExchangeReceiptID);
                                    $("#txtVoucherNo").val(obj.VoucherNo);
                                    $("#txtVoucherDate").val(obj.sVoucherDate);
                                    $("#ddlCustomer").val(obj.Customer.CustomerID).change();
                                    $("#txtProductName").val(obj.ProductName);
                                    $("#txtQuantity").val(obj.Quantity);
                                    $("#txtRate").val(obj.Rate);
                                    $("#txtAmount").val(obj.Amount);
                                    $("#txtDescription").val(obj.Narration);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Barter Receipt");
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
                url: "WebServices/VHMSService.svc/DeleteExchangeReceipt",
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
                            else if (objResponse.Value == "ExchangeReceipt_R_01" || objResponse.Value == "ExchangeReceipt_D_01") {
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

