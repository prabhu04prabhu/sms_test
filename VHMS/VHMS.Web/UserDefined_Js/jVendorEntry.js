var gMagazineData = [];
var gOPBillingList = [];
$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    //  $("#divPayment").show();
    GetVendorList("ddlVendorName");
    GetBankList("ddlBankNameName");
    GetWorkList("ddlWorkName");
    $("#txtEntryDate,#txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtYear").attr("data-link-format", "dd/MM/yyyy");

    $("#txtEntryDate,#txtCollectionDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    pLoadingSetup(true);
    GetRecord();
    GetWorkRate();
    CalculateYear();
    GetEmployeeSalaryCount();

});

function GetBankList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
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
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].LedgerID + "'>" + obj[index].LedgerName + "</option>");
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

function CalculateYear() {
    var currentYear = new Date().getFullYear();
    for (var i = currentYear; i > currentYear - 2; i--) {
        $("#ddlYear").append('<option value="' + i.toString() + '">' + i.toString() + '</option>');
    }
}

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnVendorEntryID").val("0");
    $("#hdnPreviousVendorEntryID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#hdnStartDate").val("");
    $("#hdnEndDate").val("");
    $("#divTab").hide();
    $("#divOPBilling").show();
    $("#txtNarration").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gOPBillingList = [];
    ClearOPBillingTab();
    DisplayPaymentList(gOPBillingList);
    $("#divOPBillingList").empty();
    $("#divPayment").empty();

    $("#txtEntryDate").focus();
    $("#ddlVendorName").focus();
    return false;
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divOPBilling").hide();

    GetRecord();
    return false;
});

$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnVendorEntryID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});

function ClearOPBillingTab() {
    $("#txtEntryDate").val("");
    $("#ddlVendorName").val($("#ddlVendorName option:first").val()).change();
    $("#ddlWorkName").val("").change();
    //$("#txtComments").val("")
    $("#txtDamageAmount").val("0");
    $("#txtBillingAmount").val("0");
    $("#txtTotalOutQuantity").val("0");
    $("#txtTotalInQuantity").val("0");
    $("#txtBalanceAmount").val("0");
    $("#txtOpeningBalance").val("0");
    $("#txtOpeningQty").val("0");
    $("#txtBalanceQuantity").val("0");
    $("#ddlStatus").val("Open").change();
    gOPBillingList = [];
    ClearOPBillingFields();
    $("#btnSave").show();
    $("#btnUpdate").hide();
    return false;
}
//$("#ddlPaymentMode").change(function () {
//    var iPaymentMode = $("#ddlPaymentMode").val();
//    $("#txtCardNo").val("");
//    $("#divChequeNo").hide();
//    if (iPaymentMode != undefined && iPaymentMode > 0) {
//        if (iPaymentMode == 2 || iPaymentMode == 3 || iPaymentMode == 4)
//            $("#divChequeNo").show();
//        if (iPaymentMode == 2) {
//            $("#lblCollection").text("Collection Date");
//            $("#lblCollection1").text("Cheque No #");
//            $("#txtCardNo").attr("placeholder", "Cheque No");
//        }
//        else {
//            $("#lblCollection").text("Payment Date");
//            $("#lblCollection1").text("Reference No #");
//            $("#txtCardNo").attr("placeholder", "Reference No");
//        }
//    }

//    return false;
//});


$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendorEntry",
        data: JSON.stringify({ PublisherID: 0 }),
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
                            var TypeStatus = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].Status == "Open") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Closed</span>"; }

                                var table = "<tr id='" + obj[index].VendorEntryID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].Vendor.VendorName + "</td>";
                                table += "<td>" + obj[index].MonthYear + "</td>";
                                table += "<td>" + obj[index].Vendor.PhoneNo1 + "</td>";
                                table += "<td>" + obj[index].TotalInQty + "</td>";
                                table += "<td>" + obj[index].TotalOutQty + "</td>";
                                table += "<td>" + obj[index].BalanceQty + "</td>";
                                table += "<td>" + TypeStatus + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1" && obj[index].Status == "Open") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();

                                    $("#btnAddMagazine").hide();
                                    $("#btnUpdateMagazine").hide();
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
                                    if (confirm('Are you sure to cancel the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "45%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
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

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SearchVendorEntry",
        data: JSON.stringify({ ID: iDetails }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    var notetable = $("#tblSearchResult").dataTable();
                    notetable.fnDestroy();
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $("#tblSearchResult_tbody").empty();
                            var TypeStatus = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].Status == "Open") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Closed</span>"; }

                                var table = "<tr id='" + obj[index].VendorEntryID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].Vendor.VendorName + "</td>";
                                table += "<td>" + obj[index].MonthYear + "</td>";
                                table += "<td>" + obj[index].Vendor.PhoneNo1 + "</td>";
                                table += "<td>" + obj[index].TotalInQty + "</td>";
                                table += "<td>" + obj[index].TotalOutQty + "</td>";
                                table += "<td>" + obj[index].BalanceQty + "</td>";
                                table += "<td>" + TypeStatus + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                //if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                //else { table += "<td></td>"; }

                                //if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].VendorEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                //else { table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblSearchResult_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();

                                    $("#btnAddMagazine").hide();
                                    $("#btnUpdateMagazine").hide();
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
                                    if (confirm('Are you sure to cancel the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "45%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "5%" },
                            //{ "sWidth": "5%" },
                            //{ "sWidth": "5%" },
                            { "sWidth": "5%" }
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

$("#aGeneral").click(function () {
    $("#SearchResult").hide();
    GetRecord();
});

$("#aSearchResult").click(function () {
    $("#SearchResult").show();

});

function GetWorkListName() {
    $("#ddlWorkName").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendorWorkID",
        data: JSON.stringify({ VendorID: $("#ddlVendorName").val() }),
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
                            $("#ddlWorkName").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $("#ddlWorkName").append("<option value='" + obj[index].WorkID + "'>" + obj[index].WorkName + "</option>");
                            }
                            $("#ddlWorkName").val($("#ddlWorkName option:first").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlWorkName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlWorkName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

$("#ddlWorkName").change(function () {

    if ($("#ddlWorkName").val() > 0) {

        GetWorkRate($("#ddlWorkName").val());
    }
});

function GetWorkRate(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetWorkRatebyVendor",
        data: JSON.stringify({ ID: id, VendorID: $("#ddlVendorName").val() }),
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
                            $("#txtRate").val(obj.Amount);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#txtSubTotal").val("0");
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
//#endregion Manage Screen

//#region Master
//$("#ddlPaymentMode").change(function () {
//    if ($("#ddlPaymentMode").val() == "Cash" || $("#ddlPaymentMode").val() == "Credit") {
//        $("#ddlBankNameName").attr("disabled", "disabled");
//        $("#ddlBankNameName").val(1).change();
//        $("#txtCollectionDate").attr("disabled", "disabled");
//        $("#txtCardCharges").attr("disabled", "disabled");
//        $("#txtCardNo").attr("disabled", "disabled");
//    }
//    else {
//        //if ($('txtCardNo').prop('disabled'))
//        $("#txtCardNo").removeAttr("disabled");
//        $("#txtCardCharges").removeAttr("disabled");
//        $("#txtCollectionDate").removeAttr("disabled");
//        $("#ddlBankNameName").removeAttr("disabled");
//    }
//    return false;
//});

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
                            $("#ddlVendorName").val($("#ddlVendorName option:first").val()).change();
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

function DeleteRecord(id) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteVendorEntry",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "VendorEntry_D_01" || objResponse.Value == "VendorEntry_D_01") {
                        $.jGrowl("Since this record is referred somewhere else you cannot delete it", { sticky: false, theme: 'danger', life: jGrowlLife });
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

function GetWorkList(ddlname) {
    //    var sControlName = "#" + ddlname;
    //    dProgress(true);
    //    $(sControlName).empty();
    //    $.ajax({
    //        type: "POST",
    //        url: "WebServices/VHMSService.svc/GetWork",
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
    //                                    $(sControlName).append("<option value='" + obj[index].WorkID + "'>" + obj[index].WorkName + "</option>");
    //                            }
    //                        }
    //                        dProgress(false);
    //                    }
    //                    else if (objResponse.Value == "NoRecord") {
    //                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
    //                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
    //                dProgress(false);
    //            }
    //        },
    //        error: function (e) {
    //            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
    //            dProgress(false);
    //        }
    //    });
    //    return false;
}

$("#ddlVendorName").change(function () {
    if ($("#ddlVendorName").val() > 0) {
        txtTDS
        GetVendorEntryByStatus($("#ddlVendorName").val());
        GetVendorEntryByOpeningQty($("#ddlVendorName").val());
        GetWorkListName();
        CalculateAmount();
        $("#ddlWorkName").val(0).change();
    }
});

function GetVendorEntryByStatus(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendorEntryByStatus",
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

                            $("#divOPBillingList").empty();
                            $("#divPayment").empty();
                            gOPBillingList = [];
                            $("#hdnVendorEntryID").val(obj.VendorEntryID);
                            var arrs = obj.MonthYear.split("-");
                            $("#ddlMonth").val(arrs[0]).change();
                            $("#ddlYear").val(arrs[1]).change();
                            var ObjProduct = obj.VendorTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                objTemp.VendorTransID = ObjProduct[index].VendorTransID;
                                objTemp.VendorEntryID = ObjProduct[index].VendorEntryID;
                                objTemp.sEntryDate = ObjProduct[index].sEntryDate;
                                objTemp.EntryType = ObjProduct[index].EntryType;
                                objTemp.WorkID = ObjProduct[index].WorkID;
                                objTemp.WorkName = ObjProduct[index].WorkName;
                                objTemp.InQty = ObjProduct[index].InQty;
                                objTemp.RePolishQty = ObjProduct[index].RePolishQty;
                                objTemp.RePolish = ObjProduct[index].RePolish;
                                objTemp.OutQty = ObjProduct[index].OutQty;
                                objTemp.ReturnQty = ObjProduct[index].ReturnQty;
                                objTemp.DamageAmount = ObjProduct[index].DamageAmount;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.PaidAmount = ObjProduct[index].PaidAmount;
                                objTemp.PaymentModeID = ObjProduct[index].PaymentModeID;
                                objTemp.PaymentMode = ObjProduct[index].PaymentMode;
                                objTemp.LedgerName = ObjProduct[index].LedgerName;
                                objTemp.BankID = ObjProduct[index].BankID;
                                objTemp.ChequeNo = ObjProduct[index].ChequeNo;
                                objTemp.sCollectionDate = ObjProduct[index].sCollectionDate;
                                objTemp.PayableAmount = ObjProduct[index].PayableAmount;
                                objTemp.TDSPercent = ObjProduct[index].TDSPercent;
                                objTemp.TDSAmount = ObjProduct[index].TDSAmount;
                                objTemp.RoundOff = ObjProduct[index].RoundOff;

                                AddOPBillingData(objTemp);
                            }


                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#divOPBillingList").empty();
                        $("#divPayment").empty();
                        $("#txtOpeningQty").val("0");
                        $("#txtOpeningBalance").val("0");
                        $("#hdnVendorEntryID").val("0");
                        gOPBillingList = [];
                        CalculateAmount();
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


function GetVendorEntryByOpeningQty(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendorEntryByOpeningQty",
        data: JSON.stringify({ ID: id, iVendorEntryID: $("#hdnPreviousVendorEntryID").val() }),
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
                            $("#txtOpeningQty").val(obj.OpeningQty);
                            $("#txtOpeningBalance").val(obj.OpeningBalance).change();

                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

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

//#endregion Master

//#region Trans

$("#ddlType").change(function () {
    $("#divWork").hide();
    $("#divRepolish").hide();
    $("#divQuantity").hide();
    $("#divRate").hide();
    $("#divSubTotal").hide();
    $("#divReturnQuantity").hide();
    $("#divBank").hide();
    $("#divChequeNo").hide();
    $("#txtSubTotal").val("0");
    $("#txtRate").val("0");
    $("#txtReturnQuantity").val("0");
    $("#txtRepolish").val("0");
    $("#txtQuantity").val("0");
    // $("#txtPayableAmount").val("0");
    $("#txtTDS").val("1");
    //$("#txtTDSAmounts").val("0");
    //$("#txtRoundOff").val("0");
    //$("#txtPaidAmounts").val("0");
    $("#txtCollectionDate").val("");
    $("#txtCardNo").val("");
    $("#ddlBankName").val("0").change();
    $("#ddlPaymentMode").val("0").change();
    $("#divChequeNo").hide();

    if ($("#ddlType").val() == "Outward") {
        $("#divWork").show();
        $("#divQuantity").show();
        $("#divRepolish").show();
        $("#lblQuantity").text("Out Qty");
        $("#lblReturnQuantity").text("Repolish");
        $("#txtSubTotal").removeAttr("readonly");
    }
    else if ($("#ddlType").val() == "Damage") {
        $("#divSubTotal").show();
        $("#lblAmount").text("Damage");
        $("#txtSubTotal").removeAttr("readonly");
    }
    else if ($("#ddlType").val() == "Inward") {
        $("#divWork").show();
        $("#divQuantity").show();
        $("#divSubTotal").show();
        $("#txtSubTotal").attr("readonly", "readonly");
        $("#divRate").show();
        $("#divReturnQuantity").show();
        $("#lblQuantity").text("In Qty");
        $("#lblReturnQuantity").text("Return Qty");
        $("#lblAmount").text("Subtotal");
        $("#divRepolish").show();
    }
});

$("#txtRate,#txtQuantity").change(function () {
    CalculateTrans();
});

function CalculateTrans() {
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    var iSubTotal = parseFloat(iRate) * parseFloat(iqty);
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

$("#btnAddMagazine,#btnUpdateMagazine,#btnAddPayment,#btnUpdatePayment").click(function () {

    //if ($("#ddlType").val() == "0" || $("#ddlType").val() == undefined || $("#ddlType").val() == null) {
    //    $.jGrowl("Please select Type", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divType").addClass('has-error'); $("#ddlType").focus(); return false;
    //} else { $("#divType").removeClass('has-error'); }
    if ($("#ddlVendorName").val() == "0" || $("#ddlVendorName").val() == undefined || $("#ddlVendorName").val() == null) {
        $.jGrowl("Please select Vendor", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divVendorName").addClass('has-error'); $("#ddlVendorName").focus(); return false;
    } else { $("#divVendorName").removeClass('has-error'); }


    if ($("#txtEntryDate").val() == "" || $("#txtEntryDate").val() == undefined || $("#txtEntryDate").val() == null) {
        $.jGrowl("Please enter Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divEntryDate").addClass('has-error'); $("#txtEntryDate").focus(); return false;
    } else { $("#divEntryDate").removeClass('has-error'); }


    if ((this.id == "btnAddMagazine" || this.id == "btnUpdateMagazine") && ($("#ddlType").val() == "Outward" || $("#ddlType").val() == "Inward")) {
        if ($("#ddlWorkName").val() == "0" || $("#ddlWorkName").val() == undefined || $("#ddlType").val() == null) {
            $.jGrowl("Please select Work", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divWork").addClass('has-error'); $("#ddlWorkName").focus(); return false;
        } else { $("#divWork").removeClass('has-error'); }
    }


    if ($("#ddlType").val() != "Payment" && $("#ddlType").val() != "Damage") {
        if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() == null) {
            $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
        } else { $("#divQuantity").removeClass('has-error'); }
    }

    if ($("#ddlType").val() == "Inward") {
        if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null || $("#txtRate").val() <= 0) {
            $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
        } else { $("#divRate").removeClass('has-error'); }
    }

    if ($("#ddlType").val() == "Payment") {
        if ($("#ddlPaymentMode").val() == "0" || $("#ddlPaymentMode").val() == undefined || $("#ddlPaymentMode").val() == null) {
            $.jGrowl("Please select Payment Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divPaymentMode").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
        } else { $("#divPaymentMode").removeClass('has-error'); }

        if ($("#ddlBankName").val() == "0" || $("#ddlBankName").val() == undefined || $("#ddlBankName").val() == null) {
            $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divBank").addClass('has-error'); $("#ddlBankName").focus(); return false;
        } else { $("#divBank").removeClass('has-error'); }
    }

    if ($("#ddlType").val() == "Outward" || $("#ddlType").val() == "Inward") {
        var StockValue = 0; var Re_StockValue = 0;
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetVendorStockCheck",
            data: JSON.stringify({
                iVendorID: $("#ddlVendorName").val(), iWorkID: $("#ddlWorkName").val(), iVendorEntryID: $("#hdnVendorEntryID").val()
            }),
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
                                StockValue = obj.Quantity;
                                Re_StockValue = obj.RePolishQuantity;
                            }
                            dProgress(false);
                        }
                        else if (objResponse.Value == "NoRecord") {
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
        var StockQty = 0; var Re_StockQty = 0;
        for (var i = 0; i < gOPBillingList.length; i++) {
            var hdnval = $("#hdnOPSNo").val();
            if (gOPBillingList[i].StatusFlag != "D" && gOPBillingList[i].WorkID == $("#ddlWorkName").val() && gOPBillingList[i].sNO != $("#hdnOPSNo").val()) {
                StockQty = parseInt(StockQty) + parseInt(gOPBillingList[i].OutQty) - (parseInt(gOPBillingList[i].InQty) + parseInt(gOPBillingList[i].ReturnQty));
                Re_StockQty = parseInt(Re_StockQty) + parseInt(gOPBillingList[i].RePolishQty) - parseInt(gOPBillingList[i].RePolish);
            }
        }
        var currentQty = 0; var re_currentQty = 0;
        if ($("#ddlType").val() == "Outward") {
            currentQty = parseInt($("#txtQuantity").val());
            re_currentQty = parseInt($("#txtRepolish").val());

            if (StockQty + currentQty + StockValue < 0) {
                $.jGrowl("Stock Mismatch", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }
            if (Re_StockQty + re_currentQty + Re_StockValue < 0) {
                $.jGrowl("Repolish Stock Mismatch", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }
        }
        if ($("#ddlType").val() == "Inward") {
            currentQty = parseInt($("#txtQuantity").val()) + parseInt($("#txtReturnQuantity").val());
            re_currentQty = parseInt($("#txtRepolish").val());
            if (StockQty + StockValue - currentQty < 0) {
                $.jGrowl("Stock Mismatch", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }
            if (Re_StockQty - re_currentQty + Re_StockValue < 0) {
                $.jGrowl("Repolish Stock Mismatch", { sticky: false, theme: 'warning', life: jGrowlLife });
                return false;
            }
        }


    }

    var ObjData = new Object();
    ObjData.VendorEntryID = 0;

    if (this.id == "btnAddMagazine" || this.id == "btnUpdateMagazine") { ObjData.sEntryDate = $("#txtEntryDate").val(); }
    else if (this.id == "btnAddPayment" || this.id == "btnUpdatePayment") {
        ObjData.sEntryDate = "01/06/2023";
    }

    if (this.id == "btnAddMagazine" || this.id == "btnUpdateMagazine") { ObjData.EntryType = $("#ddlType").val(); }
    else if (this.id == "btnAddPayment" || this.id == "btnUpdatePayment")
        ObjData.EntryType = "Payment";
    ObjData.PaidAmount = $("#txtPaidAmounts").val();
    ObjData.BankID = $("#ddlBankName").val();
    ObjData.LedgerName = $("#ddlBankName option:selected").text();
    ObjData.ChequeNo = $("#txtCardNo").val();
    ObjData.PaymentModeID = $("#ddlPaymentMode").val();
    ObjData.PaymentMode = $("#ddlPaymentMode option:selected").text();

    ObjData.sCollectionDate = $("#txtCollectionDate").val();

    ObjData.WorkID = 0;
    ObjData.WorkName = "";
    ObjData.InQty = 0;
    ObjData.RePolishQty = 0;
    ObjData.OutQty = 0;
    ObjData.ReturnQty = 0;
    ObjData.Rate = 0;
    ObjData.SubTotal = 0;
    ObjData.DamageAmount = 0;
    ObjData.BalanceQty = 0;
    ObjData.RePolish = 0;

    if (this.id == "btnUpdatePayment" || this.id == "btnAddPayment") {
        ObjData.PayableAmount = $("#txtPayableAmount").val();
    }
    else {
        ObjData.PayableAmount = 0;
    }

    ObjData.TDSPercent = $("#txtTDS").val();
    ObjData.TDSAmount = $("#txtTDSAmounts").val();
    ObjData.RoundOff = $("#txtRoundOff").val();

    if ($("#ddlType").val() == "Outward") {
        ObjData.OutQty = $("#txtQuantity").val();
        ObjData.WorkID = $("#ddlWorkName").val();
        ObjData.WorkName = $("#ddlWorkName option:selected").text();
        ObjData.RePolishQty = $("#txtRepolish").val();
    }
    else if ($("#ddlType").val() == "Damage") {
        ObjData.DamageAmount = $("#txtSubTotal").val();
    }
    else if ($("#ddlType").val() == "Inward") {
        ObjData.InQty = $("#txtQuantity").val();
        ObjData.Rate = $("#txtRate").val();
        ObjData.SubTotal = $("#txtSubTotal").val();
        ObjData.ReturnQty = $("#txtReturnQuantity").val();
        ObjData.WorkID = $("#ddlWorkName").val();
        ObjData.WorkName = $("#ddlWorkName option:selected").text();
        //ObjData.RePolishQty = $("#txtReturnQuantity").val();
        ObjData.RePolish = $("#txtRepolish").val();
    }
    // var iInQty = 0; var iOutQty = 0; var iRepolishQty = 0; var iReturnQty = 0;

    if (this.id == "btnAddMagazine" || this.id == "btnAddPayment") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.VendorEntryID = 0;
        ObjData.StatusFlag = "I";
        AddOPBillingData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine" || this.id == "btnUpdatePayment") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnVendorEntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.VendorEntryID = $("#hdnVendorEntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.VendorEntryID = 0;
        }
        Update_OPBilling(ObjData);
    }
    CalculateAmount();
    ClearOPBillingFields();
    if (this.id == "btnUpdatePayment" || this.id == "btnAddPayment") {
        ClearPayment();
    }
    $("#ddlType").focus();
});
function ClearOPBillingFields() {
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    $("#ddlType").val("Outward").change();
    $("#ddlWorkName").val("0").change();
    $("#txtRate").val("0");
    $("#txtSubTotal").val("0");
    $("#txtQuantity").val("0");
    $("#txtReturnQuantity").val("0");
    $("txtRepolish").val("0");
    $("#hdnOPSNo").val("");
    $("#divChequeNo").hide();
    $("#divRate").removeClass('has-error');
    $("#divRate").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
    $("#divWork").removeClass('has-error');
    $("#divEntryDate").removeClass('has-error');
    $("#divSubTotal").removeClass('has-error');
    return false;
}

function ClearPayment() {
$("#txtPayableAmount").val("0");
$("#txtTDS").val("1");
$("#txtTDSAmounts").val("0");
$("#txtRoundOff").val("0");
$("#txtPaidAmounts").val("0");
return false;
}
function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}
function Edit_PaymentDetail(ID) {
    Bind_PaymentID(ID, gOPBillingList);
    return false;
}
function AddOPBillingData(oData) {
    gOPBillingList.push(oData);
    DisplayOPBillingList(gOPBillingList);
    DisplayPaymentList(gOPBillingList);
    return false;
}

function DisplayOPBillingList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";
    if (gData.length >= 5) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }
    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:5px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Date</th>";
        sTable += "<th class='" + sColorCode + "' style='width:120px;'>Type</th>";
        sTable += "<th class='" + sColorCode + "' style='width:250px;'>Job Work</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Out</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>In</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Repolish</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Return</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Balance Qty</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Damage Amt</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Rate</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Bill Amount</th>";
        //sTable += "<th class='" + sColorCode + "' style='width:100px;'>Payable Amt</th>";
        //sTable += "<th class='" + sColorCode + "' style='width:100px;'>TDS %</th>";
        //sTable += "<th class='" + sColorCode + "' style='width:100px;'>TDS Amt</th>";
        //sTable += "<th class='" + sColorCode + "' style='width:100px;'>RoundOff</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Paid Amt</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        var iInQty = 0; var iOutQty = 0; var iRepolishQty = 0; var iReturnQty = 0; var iBalanceQty = 0; var iRepolish = 0;
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].EntryType != "Payment" && gData[i].StatusFlag != "D") {
                iInQty += parseFloat(gData[i].InQty);
                iOutQty += parseFloat(gData[i].OutQty);
                iRepolishQty += parseFloat(gData[i].RePolishQty);
                iRepolish += parseFloat(gData[i].RePolish);
                iReturnQty += parseFloat(gData[i].ReturnQty);
                gData[i].BalanceQty = (parseFloat(iOutQty) + parseFloat(iRepolishQty)) - (parseFloat(iInQty) + parseFloat(iReturnQty) + parseFloat(iBalanceQty) + parseFloat(iRepolish));

                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].sEntryDate + "</td>";
                sTable += "<td>" + gData[i].EntryType + "</td>";
                sTable += "<td>" + gData[i].WorkName + "</td>";
                if (gData[i].OutQty > 0)
                    sTable += "<td>" + gData[i].OutQty + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].InQty > 0)
                    sTable += "<td>" + gData[i].InQty + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].RePolishQty > 0)
                    sTable += "<td>" + gData[i].RePolishQty + "</td>";
                else
                    sTable += "<td>" + gData[i].RePolish + "</td>";
                if (gData[i].ReturnQty > 0)
                    sTable += "<td>" + gData[i].ReturnQty + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].BalanceQty >= 0)
                    sTable += "<td>" + gData[i].BalanceQty + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].DamageAmount > 0)
                    sTable += "<td>" + gData[i].DamageAmount + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].Rate > 0)
                    sTable += "<td>" + gData[i].Rate + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].SubTotal > 0)
                    sTable += "<td>" + gData[i].SubTotal + "</td>";
                else
                    sTable += "<td></td>";
                //if (gData[i].PayableAmount > 0)
                //    sTable += "<td>" + gData[i].PayableAmount + "</td>";
                //else
                //    sTable += "<td></td>";
                //if (gData[i].TDSPercent > 0)
                //    sTable += "<td>" + gData[i].TDSPercent + "</td>";
                //else
                //    sTable += "<td></td>";
                //if (gData[i].TDSAmount > 0)
                //    sTable += "<td>" + gData[i].TDSAmount + "</td>";
                //else
                //    sTable += "<td></td>";
                //if (gData[i].RoundOff > 0)
                //    sTable += "<td>" + gData[i].RoundOff + "</td>";
                //else
                //    sTable += "<td></td>";
                if (gData[i].PaidAmount > 0)
                    sTable += "<td>" + gData[i].PaidAmount + "</td>";
                else
                    sTable += "<td></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OPBillingDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OPBillingDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblOPBillingList_body").append(sTable);
            }
        }
    }
    else { $("#divOPBillingList").empty(); }
    return false;
}


function DisplayPaymentList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";
    if (gData.length >= 5) { $("#divPaymentList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else { $("#divPaymentList").css({ 'height': '', 'min-height': '' }); }
    if (gData.length > 0) {
        sTable = "<table id='tblPaymentList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:5px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Date</th>";
        sTable += "<th class='" + sColorCode + "' style='width:120px;'>Type</th>";
        sTable += "<th class='" + sColorCode + "' style='width:120px;'>PaymentMode</th>";
        sTable += "<th class='" + sColorCode + "' style='width:120px;'>Bank</th>";
        sTable += "<th class='" + sColorCode + "' style='width:120px;'>ChequeNo</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Payable Amt</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>TDS %</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>TDS Amt</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>RoundOff</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Paid Amt</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblPaymentList_body'>";
        sTable += "</tbody></table>";
        $("#divPaymentList").html(sTable);
        var iInQty = 0; var iOutQty = 0; var iRepolishQty = 0; var iReturnQty = 0; var iBalanceQty = 0; var iRepolish = 0;
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].EntryType == "Payment" && gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].sCollectionDate + "</td>";
                sTable += "<td>" + gData[i].EntryType + "</td>";
                sTable += "<td>" + gData[i].PaymentMode + "</td>";
                sTable += "<td>" + gData[i].LedgerName + "</td>";
                sTable += "<td>" + gData[i].ChequeNo + "</td>";
                if (gData[i].PayableAmount > 0)
                    sTable += "<td>" + gData[i].PayableAmount + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].TDSPercent > 0)
                    sTable += "<td>" + gData[i].TDSPercent + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].TDSAmount > 0)
                    sTable += "<td>" + gData[i].TDSAmount + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].RoundOff > 0)
                    sTable += "<td>" + gData[i].RoundOff + "</td>";
                else
                    sTable += "<td></td>";
                if (gData[i].PaidAmount > 0)
                    sTable += "<td>" + gData[i].PaidAmount + "</td>";
                else
                    sTable += "<td></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_PaymentDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_PaymentDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblPaymentList_body").append(sTable);
            }
        }
    }
    else { $("#divPaymentList").empty(); }
    return false;
}
function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlType").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            //$("#hdnVendorEntryID").val(data[i].VendorEntryID);
            $("#txtEntryDate").val(data[i].sEntryDate);
            $("#ddlType").val(data[i].EntryType).change();
            $("#ddlWorkName").val(data[i].WorkID).change();
            if (data[i].EntryType == "Outward") {
                $("#txtQuantity").val(data[i].OutQty);
                $("#txtReturnQuantity").val(data[i].RePolishQty);
            }
            else if (data[i].EntryType == "Inward") {
                $("#txtQuantity").val(data[i].InQty);
                $("#txtRate").val(data[i].Rate);
                $("#txtSubTotal").val(data[i].SubTotal);
                $("#txtReturnQuantity").val(data[i].ReturnQty);
                $("#txtRepolish").val(data[i].RePolish);
            } else if (data[i].EntryType == "Damage")
                $("#txtSubTotal").val(data[i].DamageAmount);
        }
    }
    return false;
}


function Bind_PaymentID(ID, data) {
    $("#btnAddPayment").hide();
    $("#btnUpdatePayment").show();
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            //$("#hdnVendorEntryID").val(data[i].VendorEntryID);
            $("#ddlType").val(data[i].EntryType).change();
            $("#ddlWorkName").val(data[i].WorkID).change();
            $("#txtEntryDate").val(data[i].sEntryDate);
            if (data[i].EntryType == "Payment") {
                $("#txtTDS").val(data[i].TDSPercent);
                $("#txtTDSAmounts").val(data[i].TDSAmount);
                $("#txtRoundOff").val(data[i].RoundOff);
                $("#txtPayableAmount").val(data[i].PayableAmount);

                $("#txtPaidAmounts").val(data[i].PaidAmount);
                $("#ddlPaymentMode").val(data[i].PaymentModeID).change();
                $("#ddlBankName").val(data[i].BankID).change();
                $("#txtCardNo").val(data[i].ChequeNo);
                $("#txtCollectionDate").val(data[i].sCollectionDate);
            }
        }
    }
    return false;
}

function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].VendorEntryID = oData.VendorEntryID;
            gOPBillingList[i].sEntryDate = oData.sEntryDate;
            gOPBillingList[i].EntryType = oData.EntryType;
            gOPBillingList[i].WorkID = oData.WorkID;
            gOPBillingList[i].WorkName = oData.WorkName;
            gOPBillingList[i].InQty = oData.InQty;
            gOPBillingList[i].OutQty = oData.OutQty;
            gOPBillingList[i].ReturnQty = oData.ReturnQty;
            gOPBillingList[i].RePolishQty = oData.RePolishQty;
            gOPBillingList[i].RePolish = oData.RePolish;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].DamageAmount = oData.DamageAmount;
            gOPBillingList[i].PaidAmount = oData.PaidAmount;
            gOPBillingList[i].PaymentModeID = oData.PaymentModeID;
            gOPBillingList[i].PaymentMode = oData.PaymentMode;
            gOPBillingList[i].BankID = oData.BankID;
            gOPBillingList[i].LedgerName = oData.LedgerName;
            gOPBillingList[i].ChequeNo = oData.ChequeNo;
            gOPBillingList[i].sCollectionDate = oData.sCollectionDate;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;

            gOPBillingList[i].PayableAmount = oData.PayableAmount;

            gOPBillingList[i].TDSPercent = oData.TDSPercent;

            gOPBillingList[i].TDSAmount = oData.TDSAmount;

            gOPBillingList[i].RoundOff = oData.RoundOff;
        }
    }
    DisplayOPBillingList(gOPBillingList);
    DisplayPaymentList(gOPBillingList);
    return false;
}

function Delete_OPBillingDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].SNo == ID) {
                var index = jQuery.inArray(gOPBillingList[i].valueOf("SNo"), gOPBillingList);
                if (gOPBillingList[i].SNo > 0) {
                    gOPBillingList[i].StatusFlag = "D";
                } else {
                    gOPBillingList.splice(index, 1);
                }
                $("#divOPBillingList").empty();
                DisplayOPBillingList(gOPBillingList);
                DisplayPaymentList(gOPBillingList);
                CalculateAmount();
            }
        }
    }
    return false;
}


function Delete_PaymentDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].SNo == ID) {
                var index = jQuery.inArray(gOPBillingList[i].valueOf("SNo"), gOPBillingList);
                if (gOPBillingList[i].SNo > 0) {
                    gOPBillingList[i].StatusFlag = "D";
                } else {
                    gOPBillingList.splice(index, 1);
                }
                $("#divPayment").empty();
                DisplayOPBillingList(gOPBillingList);
                DisplayPaymentList(gOPBillingList);
                CalculateAmount();
            }
        }
    }
    return false;
}

$("#txtPayableAmount,#txtTDS,#txtRoundOff").change(function () {

    CalculateTotlaAmount();

});
function CalculateTotlaAmount() {
    var itPayableAmount = parseFloat($("#txtPayableAmount").val());
    var iTDSPercent = parseFloat($("#txtTDS").val());
    var iRoundOff = parseFloat($("#txtRoundOff").val());

    if (isNaN(itPayableAmount)) itPayableAmount = 0;
    if (isNaN(iTDSPercent)) iTDSPercent = 0;
    if (isNaN(iTDSAmounts)) iTDSAmounts = 0;
    if (isNaN(iRoundOff)) iRoundOff = 0;


    var iTDSAmounts = parseFloat(itPayableAmount) * parseFloat(iTDSPercent) / 100;
    if (isNaN(iTDSAmounts)) iTDSAmounts = 0;
    $("#txtTDSAmounts").val((iTDSAmounts).toFixed(2));

    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    var iNetAmount = parseFloat(itPayableAmount) - parseFloat(iTDSAmounts) + parseFloat(iRoundOff);

    $("#txtPaidAmounts").val((iNetAmount).toFixed(2));
}

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}
//#endregion Trans

//#region Save& Update
$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    var gCount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            gCount++;

    }
    if (gCount == 0) {
        $.jGrowl("No entries has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    if ($("#ddlVendorName").val() == "0" || $("#ddlVendorName").val() == undefined || $("#ddlVendorName").val() == null) {
        $.jGrowl("Please select Vendor", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divVendorName").addClass('has-error'); $("#ddlVendorName").focus(); return false;
    } else { $("#divVendorName").removeClass('has-error'); }

    var ObjOPBilling = new Object();
    var ObjVendor = new Object();
    ObjVendor.VendorID = $("#ddlVendorName").val();
    ObjOPBilling.Vendor = ObjVendor;
    ObjOPBilling.MonthYear = $("#ddlMonth").val() + "-" + $("#ddlYear").val();
    ObjOPBilling.VendorEntryID = 0;
    ObjOPBilling.TotalInQty = $("#txtTotalInQuantity").val();
    ObjOPBilling.TotalOutQty = $("#txtTotalOutQuantity").val();
    ObjOPBilling.BalanceQty = $("#txtBalanceQuantity").val();
    ObjOPBilling.ClosingBalance = $("#txtBalanceAmount").val();
    ObjOPBilling.Status = $("#ddlStatus").val();
    ObjOPBilling.Comments = $("#txtNarration").val();
    ObjOPBilling.VendorTrans = gOPBillingList;

    if ($("#hdnVendorEntryID").val() > 0) {
        ObjOPBilling.VendorEntryID = $("#hdnVendorEntryID").val();
        gOPBillingList.VendorEntryID = ObjOPBilling.VendorEntryID;
        sMethodName = "UpdateVendorEntry";
    }
    else {
        sMethodName = "AddVendorEntry";
        ObjOPBilling.VendorEntryID = 0
    }

    SaveandUpdateOPBilling(ObjOPBilling, sMethodName);
 
});

function SaveandUpdateOPBilling(ObjOPBilling, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjOPBilling }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearOPBillingTab();
                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divOPBilling").hide();
                        if (sMethodName == "AddVendorEntry") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnVendorEntryID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateVendorEntry") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();

                        $("#hdnVendorEntryID").val("0");
                        //$("#btnAddNew").show();
                        //$("#btnList").hide();
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

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendorEntryByID",
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
                            $("#btnAddNew").click();
                            $("#btnSave").hide();
                            $("#btnUpdate").show();
                            $("#txtBillNo").attr("disabled", true);
                            $("#hdnPreviousVendorEntryID").val(obj.VendorEntryID);
                            $("#hdnVendorEntryID").val(obj.VendorEntryID);
                            $("#ddlVendorName").val(obj.Vendor.VendorID).change();
                            $("#ddlStatus").val(obj.Status).change();
                            $("#txtNarration").val(obj.Comments);
                            var arr = obj.MonthYear.split('-');
                            $("#ddlMonth").val(arr[0]).change();
                            $("#ddlYear").val(arr[1]).change();

                            gOPBillingList = [];

                            var ObjProduct = obj.VendorTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                objTemp.VendorTransID = ObjProduct[index].VendorTransID;
                                objTemp.VendorEntryID = ObjProduct[index].VendorEntryID;
                                objTemp.sEntryDate = ObjProduct[index].sEntryDate;
                                objTemp.EntryType = ObjProduct[index].EntryType;
                                objTemp.WorkID = ObjProduct[index].WorkID;
                                objTemp.WorkName = ObjProduct[index].WorkName;
                                objTemp.InQty = ObjProduct[index].InQty;
                                objTemp.RePolishQty = ObjProduct[index].RePolishQty;
                                objTemp.RePolish = ObjProduct[index].RePolish;
                                objTemp.OutQty = ObjProduct[index].OutQty;
                                objTemp.ReturnQty = ObjProduct[index].ReturnQty;
                                objTemp.DamageAmount = ObjProduct[index].DamageAmount;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.PaidAmount = ObjProduct[index].PaidAmount;
                                objTemp.PaymentModeID = ObjProduct[index].PaymentModeID;
                                objTemp.PaymentMode = ObjProduct[index].PaymentMode;
                                objTemp.BankID = ObjProduct[index].BankID;
                                objTemp.LedgerName = ObjProduct[index].LedgerName;
                                objTemp.ChequeNo = ObjProduct[index].ChequeNo;
                                objTemp.sCollectionDate = ObjProduct[index].sCollectionDate;

                                objTemp.PayableAmount = ObjProduct[index].PayableAmount;
                                objTemp.TDSPercent = ObjProduct[index].TDSPercent;
                                objTemp.TDSAmount = ObjProduct[index].TDSAmount;
                                objTemp.RoundOff = ObjProduct[index].RoundOff;

                                AddOPBillingData(objTemp);
                            }
                            CalculateAmount();
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

function CalculateAmount() {
    var iINQty = 0, iOutQty = 0, iPayableAmount = 0, iPayableAmt = 0, iDamageAmt = 0, iPaidAmount = 0, iSubTotal = 0; iTOTAL = 0, iRePolish = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {

            iOutQty = iOutQty + parseFloat(gOPBillingList[i].OutQty) + parseFloat(gOPBillingList[i].RePolishQty);
            iINQty = iINQty + (parseFloat(gOPBillingList[i].InQty) + parseFloat(gOPBillingList[i].RePolish) + parseFloat(gOPBillingList[i].ReturnQty));
            iDamageAmt = iDamageAmt + parseFloat(gOPBillingList[i].DamageAmount);
            iPaidAmount = iPaidAmount + parseFloat(gOPBillingList[i].PaidAmount);
            iSubTotal = iSubTotal + parseFloat(gOPBillingList[i].SubTotal);
            iPayableAmount = iPayableAmount + parseFloat(gOPBillingList[i].PayableAmount);

        }
    }
    iTOTAL = parseFloat($("#txtOpeningQty").val());
    iTOTALAmt = parseFloat($("#txtOpeningBalance").val());


    iTol = ((parseFloat(iSubTotal) - parseFloat(iDamageAmt)) + (parseFloat(iTOTALAmt)));
    $("#txtBillingAmount").val(parseFloat(iTol).toFixed(2));
    $("#txtDamageAmount").val(parseFloat(iDamageAmt).toFixed(2));
    $("#txtTotalPayment").val(parseFloat(iPayableAmount).toFixed(2));
    $("#txtPayableAmount").val(parseFloat(iTol)).change();

    $("#txtTotalOutQuantity").val(parseFloat(iOutQty));
    $("#txtTotalInQuantity").val(parseFloat(iINQty));
    $("#txtBalanceQuantity").val(parseFloat(iOutQty) - parseFloat(iINQty) + iTOTAL);

    iBilling = parseFloat($("#txtBillingAmount").val());
    iPay = parseFloat($("#txtTotalPayment").val());
    iPaidAmount = (parseFloat(iBilling) - parseFloat(iPay));

    $("#txtBalanceAmount").val(parseFloat(iPaidAmount).toFixed(2));

}
function GetBankList() {
    dProgress(true);
    $("#ddlBankName").empty();
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

                            $("#ddlBankName").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive) {
                                    $("#ddlBankName").append('<option value=' + obj[index].LedgerID + ' >' + obj[index].LedgerName + '</option>');
                                }
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlBankName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlBankName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
//#endregion Save& Update


