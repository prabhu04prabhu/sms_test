var gMagazineData = [];
var gOPBillingList = [];
var gSalesReturnList = [];

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    //ActionPurchaseDiscountID = _CMPurchaseDiscountID;

    $("#hdnSalesDiscountID").val("0");
    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    $("#ddlTaxName").hide();
    GetCustomerList("ddlCustomerName");
    GetTaxList("ddlTaxName");
    GetCustomerList("ddlCategoryName");
    GetTaxList("ddlTax");
    $("#ddlTaxName").change();
    $("#ddlOldInvoiceNo").change();
    $("#txtBillDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtSalesDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtEntryDate").attr("data-link-format", "dd/MM/yyyy");

    GetPassword();
    $("#txtBillDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    $("#txtSalesDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    $("#txtEntryDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    //var date = new Date()

    var _Tfunctionality;
    if ($.cookie("OPBilling") != undefined) {
        _Tfunctionality = $.cookie("OPBilling");

        if (_Tfunctionality == "AddNewOPBilling") {
            pLoadingSetup(true);
            $("#btnAddNew").click();
            //$("#ddlDoctor").val(parseInt($.cookie("DoctorID"))).change();
            //GetMagazineList(parseInt($.cookie("DoctorID")));

            //$("#ddlPatient").attr("disabled", true);

            GetReceivedSalesReturn(parseInt($.cookie("SalesDiscountID")));
            $("#hdnSalesDiscountID").val(parseInt($.cookie("SalesDiscountID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("SalesDiscountID", null);
    }

    pLoadingSetup(true);

    GetRecord();

    //    if (ActionPurchaseDiscountID > 0) {
    //        if (ActionView == "1") {
    //            EditRecord(ActionPurchaseDiscountID);
    //            $("#btnSave").hide();
    //            $("#btnUpdate").hide();
    //            $("#btnPrintbill").show();

    //            $("#btnAddMagazine").hide();
    //            $("#btnUpdateMagazine").hide();
    //        }

    //        else {
    //            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
    //            return false;
    //        }
    //    }
});
function GetCustomerList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopCustomerType",
        data: JSON.stringify({ Type: 'Sales' }),
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
                            //$("#ddlState").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $(sControlName).append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '</option>');
                            }
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}
function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}


$("#ddlCategoryName").change(function () {
    GetRecord();
});
//$("#ddlOldInvoiceNo").change(function () {
//    $("#txtSalesDisDate").val("");
//    if ($("#ddlOldInvoiceNo").val() > 0) {
//        dProgress(true);
//        $.ajax({
//            type: "POST",
//            url: "WebServices/VHMSService.svc/GetSalesEntryByID",
//            data: JSON.stringify({ ID: $("#ddlOldInvoiceNo").val(), IsOldBill: 1 }),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            async: false,
//            success: function (data) {
//                if (data.d != "") {
//                    var objResponse = jQuery.parseJSON(data.d);
//                    if (objResponse.Status == "Success") {
//                        if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
//                            var obj = jQuery.parseJSON(objResponse.Value);
//                            if (obj != null) {
//                                $("#txtSalesDisDate").val(obj.sBillDate);
//                            }
//                            dProgress(false);
//                        }
//                        else if (objResponse.Value == "NoRecord") {
//                            $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
//                            dProgress(false);
//                        }
//                        else if (objResponse.Value == "Error") {
//                            $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
//                        }
//                    }
//                    else if (objResponse.Status == "Error") {
//                        if (objResponse.Value == "0") {
//                            window.location("frmLogin.aspx");
//                        }
//                        else if (objResponse.Value == "Error") {
//                            window.location = "frmErrorPage.aspx";
//                        }
//                        else if (objResponse.Value == "NoRecord") {
//                            $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
//                        }
//                    }
//                }
//                else {
//                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
//                    dProgress(false);
//                }
//            },
//            error: function () {
//                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
//                dProgress(false);
//            }
//        });
//        return false;
//    }
//});
$("#ddlCustomerName").change(function () {
    if ($("#ddlCustomerName").val() > 0) {
        GetCustomerByID($("#ddlCustomerName").val());
        GetPendingSalesBillNo("ddlAdjBillNo");
        GetBillNo("ddlOldInvoiceNo");

        CalculateAmount();
    }
});
function GetPendingSalesBillNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPendingSalesDiscountBillNo",
        data: JSON.stringify({ CustomerID: $("#ddlCustomerName").val(), IsRetailBill: 1, SalesDiscountID: $("#hdnSalesDiscountID").val() }),
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
                                $(sControlName).append("<option value='" + obj[index].SalesEntryID + "'>" + obj[index].InvoiceNo + "</option>");
                            }
                            $(sControlName).val("0").change();
                        }

                        dProgress(false);

                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $(sControlName).val("0").change();
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occur/ed", { sticky: false, theme: 'danger', life: jGrowlLife });
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
            $.jGrowl("Error Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetCustomerByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomerByID",
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
                            $("#hdnStateCode").val(obj.State.StateCode);

                            for (var i = 0; i < gOPBillingList.length; i++) {
                                if (gOPBillingList[i].StatusFlag != "D") {
                                    iSubtotal = gOPBillingList[i].SubTotal;
                                    iTaxID = gOPBillingList[i].Tax.TaxID;
                                    GetTaxByID(iTaxID);

                                    gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());
                                    if ($("#hdnStateCode").val() == 33) {
                                        gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                                        gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                                        gOPBillingList[i].Tax.IGSTPercent = 0;
                                        gOPBillingList[i].Tax.CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].Tax.SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].Tax.IGSTAmount = 0;
                                    }
                                    else {
                                        gOPBillingList[i].Tax.CGSTPercent = 0;
                                        gOPBillingList[i].Tax.SGSTPercent = 0;
                                        gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                                        gOPBillingList[i].Tax.CGSTAmount = 0;
                                        gOPBillingList[i].Tax.SGSTAmount = 0;
                                        gOPBillingList[i].Tax.IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                                    }
                                    gOPBillingList[i].Tax.TaxAmount = (parseFloat(gOPBillingList[i].Tax.CGSTAmount) + parseFloat(gOPBillingList[i].Tax.SGSTAmount) + parseFloat(gOPBillingList[i].Tax.IGSTAmount)).toFixed(2);
                                }
                                // CalculateAmount();
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
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function saveimage(id) {
    pLoadingSetup(false);

    var images = $("#" + id + "").attr('src');
    var ImageSave = images.replace("data:image/jpeg;base64,", "");
    var submitval = JSON.stringify({ data: ImageSave });

    $.ajax({
        type: "POST",
        url: pageUrl + "/saveimage",
        data: submitval,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $get(id).src = "./" + r.d;
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    pLoadingSetup(true);
}


function GetSupplierByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSupplierByID",
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
                            $("#hdnStateCode").val(obj.State.StateCode);

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



$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().trim().length > 3) {
        GetProductByCodeList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();
        }
    }
    else if ($("#txtCode").val().length == 0) {
        GetProductList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();
        }
    }
});

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
        GetRate();
        GetProductTax();
    }
});

$("input[type=radio]").change(function () {
    GetProductList("ddlProductName");
    $("#ddlProductName").val(null).change();
    $("#txtPartyCode").val("");
});

function GetProductTax() {
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);

        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByID",
            data: JSON.stringify({ ID: $("#ddlProductName").val() }),
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
                                $("#ddlTax").val($("#ddlTaxName").val()).change();
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
                $.jGrowl("Error  Occred", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        });
        return false;
    }
}

function GetProductByCodeList(ddlname) {
    if ($("#txtCode").val().trim().length > 3) {
        var sControlName = "#" + ddlname;
        dProgress(true);
        $(sControlName).empty();
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByCodeByID",
            data: JSON.stringify({ ProductCode: $("#txtCode").val().trim(), SMSOnly: 0 }),
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
                                        $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                    }
                                }
                                $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
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
                            $.jGrowl("Error  Oc68 568cured", { sticky: false, theme: 'danger', life: jGrowlLife });
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
}

$("#txtRoundoff").keypress(function (e) {
    if (e.which != 46 && e.which != 45 && e.which != 46 &&
        !(e.which >= 48 && e.which <= 57)) {
        return false;
    }
});
$("#txtRoundoff").change(function () {
    CalculateAmount();
});

$("#btnAddNew").click(function () {
    $("#hdnID").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Barter Receipt");
    $('#compose-modaled').modal({ show: true, backdrop: true });

    $("#txtVoucherNo").focus();
    ClearOPBillingTab();
    return false;
});

$("#btnClearImage1").click(function () {
    $get("imgUploadSales_view").src = "";
    $("#imageSalesfile").val("");
});




$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#secHeader").removeClass('hidden');
    $("#divTab").show();
    $("#divOPBilling").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#hdnSalesDiscountID").val("0");
    $('#compose-modaled').modal('hide');
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});


$("#ddlOldInvoiceNo").change(function () {
    $("#txtSalesDate").val("");
    if ($("#ddlOldInvoiceNo").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetSalesEntryByID",
            data: JSON.stringify({ ID: $("#ddlOldInvoiceNo").val() }),
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
                                $("#txtSalesDate").val(obj.sInvoiceDate);
                            }
                            dProgress(false);
                        }
                        else if (objResponse.Value == "NoRecord") {
                            $.jGrowl("No Record1", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                            $.jGrowl("No Record1", { sticky: false, theme: 'warning', life: jGrowlLife });
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
});

function ClearOPBillingTab() {
    $("#hdnSalesDiscountID").val("0");
    $("#txtRefNo").val("");
    $("#txtBillDate").val("");
    $("#txtEntryDate").val("");
    $("#txtComments").val("");
    $("#txtDebitnote").val("");
    $("#txtDisAmount").val("0");
    $("#txtTaxAmt").val("0");
    $("#ddlCustomerName").val("0").change();
    $("#txtNetAmount").val("0");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    $("#ddlSupplierName").val("0").change();
    $("#ddlOldInvoiceNo").val("0").change();
    $("#ddlAdjBillNo").val("0").change();
    $("#txtBillNo").attr("disabled", false);
    $("#ddlTaxName").val(2).change();
    $get("imgUploadSales_view").src = "";
    $("[id*=imgUploadSales_view]").css("visibility", "hidden");
    return false;
}

function GetProductList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductList",
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
                                    if ($('#rdoSupplier').is(':checked')) {
                                        if (obj[index].Supplier.SupplierID == $("#ddlSupplierName").val())
                                            $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");

                                    } else {
                                        $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                    }
                                }
                            }
                            $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
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
                        $.jGrowl("Error  Occu57645red", { sticky: false, theme: 'danger', life: jGrowlLife });
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



//$("#ddlProductName").change(function () {
//    if ($("#ddlProductName").val() > 0)
//        GetRate();
//});

//$("#ddlProductName").change(function () {

//    if ($("#ddlProductName").val() > 0) {
//        var ProdID = $("#ddlProductName").val();
//        for (var i = 0; i < gSalesReturnList.length; i++) {
//            if (gSalesReturnList[i].PurchaseTransID == ProdID) {
//                $("#hdnProductID").val(gSalesReturnList[i].Product.ProductID);
//                $("#txtSMSCode").val(gSalesReturnList[i].Product.SMSCode);
//                $("#txtPartyCode").val(gSalesReturnList[i].Product.ProductCode);
//                $("#txtAvailableQty").val(gSalesReturnList[i].Quantity);
//                $("#txtQuantity").val(0);
//                $("#hdnTransTaxID").val(gSalesReturnList[i].Tax.TaxID);
//                $("#txtTax").val(gSalesReturnList[i].Tax.TaxPercentage);
//                $("#hdnTransTaxPercent").val(gSalesReturnList[i].Tax.TaxPercentage);
//                $("#hdnTransCGSTPercent").val(gSalesReturnList[i].Tax.CGSTPercent);
//                $("#hdnTransIGSTPercent").val(gSalesReturnList[i].Tax.IGSTPercent);
//                $("#hdnTransSGSTPercent").val(gSalesReturnList[i].Tax.SGSTPercent);
//                $("#txtTaxAmt").val(gSalesReturnList[i].TaxAmount);
//                //$("#txtTax").val(gSalesReturnList[i].Tax.TaxID);
//                $("#txtRate").val(gSalesReturnList[i].Rate);
//                $("#txtDisPer").val(gSalesReturnList[i].DiscountPercentage);
//                $("#txtDisAmt").val(gSalesReturnList[i].DiscountAmount);
//                $("#txtSubTotal").val(gSalesReturnList[i].SubTotal);
//                $("#txtBarcode").val(gSalesReturnList[i].Barcode);
//                $("#hdnPreQtyID").val(gSalesReturnList[i].Quantity);
//                CalculateTrans();
//            }
//        }
//    }

//});


function GetPendingSalesBillNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPendingSalesDiscountBillNo",
        data: JSON.stringify({ CustomerID: $("#ddlCustomerName").val(), IsRetailBill: 1, SalesEntryID: $("#hdnSalesDiscountID").val() }),
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
                                $(sControlName).append("<option value='" + obj[index].SalesEntryID + "'>" + obj[index].InvoiceNo + "</option>");
                            }
                            $(sControlName).val("0").change();
                        }

                        dProgress(false);

                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $(sControlName).val("0").change();
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occur/ed", { sticky: false, theme: 'danger', life: jGrowlLife });
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
            $.jGrowl("Error Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}
function GetBillNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesDiscountBillNo",
        data: JSON.stringify({ iCustomerID: $("#ddlCustomerName").val(), IsActive: 1, SalesDiscountID: $("#hdnSalesDiscountID").val() }),
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
                                $(sControlName).append("<option value='" + obj[index].SalesEntryID + "'>" + obj[index].InvoiceNo + "</option>");
                            }
                            $(sControlName).val("0").change();
                        }

                        dProgress(false);

                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $(sControlName).val("0").change();
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
            $.jGrowl("Error Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetTaxList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTax",
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
                                    $(sControlName).append("<option value='" + obj[index].TaxID + "'>" + obj[index].TaxName + "</option>");
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
                        $.jGrowl("Error  Occure7457457d", { sticky: false, theme: 'danger', life: jGrowlLife });
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



$("#ddlTaxName").change(function () {
    GetTaxTransByID($("#ddlTaxName").val());
    CalculateAmount();

});

$("#txtDisAmount").change(function () {
    var iqty = parseFloat($("#txtDisAmount").val());
    if (isNaN(iqty)) iqty = 0;
    CalculateAmount();

});
function CalculateAmount() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iqty = parseFloat($("#txtDisAmount").val());
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iTax)) iTax = 0;
    var iqty = parseFloat($("#txtDisAmount").val());
    if (isNaN(iqty)) iqty = 0;
    var iTaxPercent = parseFloat(iqty) * parseFloat(iTax) / 100;
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    var iNetAmount = parseFloat(iqty) + parseFloat(iTaxPercent) + parseFloat(iround);
    $("#txtTaxAmt").val((iTaxPercent).toFixed(2));
    $("#txtNetAmount").val((iNetAmount).toFixed(2));
}


Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

function GetTaxTransByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTaxByID",
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
                            $("#hdnTransTaxPercent").val(obj.TaxPercentage);
                            $("#hdnTransCGSTPercent").val(obj.CGSTPercent);
                            $("#hdnTransSGSTPercent").val(obj.SGSTPercent);
                            $("#hdnTransIGSTPercent").val(obj.IGSTPercent);
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



Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}



$("#btnProductImageCancel").click(function () {
    $('#composeProductImage').modal('hide');
    return false;
});


function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopSalesDiscount",
        data: JSON.stringify({ PublisherID: 0, iSupplierID: $("#ddlCategoryName").val() }),
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
                                if (obj[index].IsCancelled == "0") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].SalesDiscountID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].DiscountNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td>" + obj[index].SalesEntry.InvoiceNo + "</td>";
                                table += "<td>" + obj[index].DiscountAmount + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].TotalAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='PrintSalesDiscount' title='Click here to Print Sales Discount'></i><i class='fa fa-print text-green'/></a></td>";

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();
                                    $("#btnPrintbill").show();

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
                            $(".PrintSalesDiscount").click(function () {
                                SetSessionValue("SalesDiscountID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesDiscount.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?')) { ShowDeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "9%" },
                            { "sWidth": "10%" },
                            { "sWidth": "35%" },
                            { "sWidth": "10%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "1%" },
                            { "sWidth": "1%" },
                            { "sWidth": "1%" },
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

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSearchSalesDiscount",
        data: JSON.stringify({ PublisherID: iDetails, iSupplierID: $("#ddlCategoryName").val() }),
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
                                if (obj[index].IsCancelled == "0") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].SalesDiscountID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].DiscountNo + "</td>";
                                table += "<td>" + obj[index].sDiscountDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td>" + obj[index].SalesEntry.InvoiceNo + "</td>";
                                table += "<td>" + obj[index].DiscountAmount + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].TotalAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesDiscountID + " class='PrintSalesDiscount' title='Click here to Print Sales Discount'></i><i class='fa fa-print text-green'/></a></td>";

                                table += "</tr>";
                                $("#tblSearchResult_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();

                                    $("#btnPrintbill").show();
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
                            $(".PrintSalesDiscount").click(function () {
                                SetSessionValue("SalesDiscountID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesDiscount.aspx", "MsgWindow");
                            });
                            $(".PrintOPBilling").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnSalesDiscountID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("SalesDiscountID", AdmissionID);

                                var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?')) { ShowDeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "9%" },
                            { "sWidth": "10%" },
                            { "sWidth": "35%" },
                            { "sWidth": "10%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "1%" },
                            { "sWidth": "1%" },
                            { "sWidth": "1%" },
                            { "sWidth": "1%" }
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


$("#btnCancel").click(function () {
    $('#compose-modal').modal('hide');
    return false;

});

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});


function GetTaxByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTaxByID",
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
                            $("#hdnTaxPercent").val(obj.TaxPercentage);
                            $("#hdnCGSTPercent").val(obj.CGSTPercent);
                            $("#hdnSGSTPercent").val(obj.SGSTPercent);
                            $("#hdnIGSTPercent").val(obj.IGSTPercent);
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

$("#chkIsDiscount").change(function () {
    if ($("#chkIsDiscount").prop('checked') == true)
        $('#txtSubTotal').attr("readonly", false);
    else
        $('#txtSubTotal').attr("readonly", true)
});


$("#btnSave,#btnUpdate").click(function () {
    $("#ddlTax").change();
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
    }
    else { $("#divBillDate").removeClass('has-error'); }

    if ($("#txtEntryDate").val().trim() == "" || $("#txtEntryDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divEntryDate").addClass('has-error'); $("#txtEntryDate").focus(); return false;
    }
    else { $("#divEntryDate").removeClass('has-error'); }

    if ($("#ddlCustomerName").val() == "0" || $("#ddlCustomerName").val() == undefined || $("#ddlCustomerName").val() == null) {
        $.jGrowl("Please select Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSupplierName").addClass('has-error'); $("#ddlCustomerName").focus(); return false;
    }
    else { $("#divSupplierName").removeClass('has-error'); }

    if ($("#ddlTaxName").val() == "0" || $("#ddlTaxName").val() == undefined || $("#ddlTaxName").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxName").addClass('has-error'); $("#ddlTaxName").focus(); return false;
    }
    else { $("#divTaxName").removeClass('has-error'); }

    var ObjOPBilling = new Object();

    ObjOPBilling.SalesDiscountID = 0;
    ObjOPBilling.DiscountNo = $("#txtRefNo").val();
    ObjOPBilling.sDiscountDate = $("#txtBillDate").val().trim();
    ObjOPBilling.sBillDate = $("#txtEntryDate").val().trim();
    ObjOPBilling.sSalesDate = $("#txtSalesDate").val().trim();

    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#ddlCustomerName").val();
    ObjOPBilling.Customer = ObjCustomer;

    var ObjTax = new Object();
    ObjTax.TaxID = $("#ddlTaxName").val();
    ObjOPBilling.Tax = ObjTax;

    var ObjSales = new Object();
    ObjSales.SalesEntryID = $("#ddlOldInvoiceNo").val();
    ObjOPBilling.SalesEntry = ObjSales;

    var ObjAdjSales = new Object();
    ObjAdjSales.SalesEntryID = $("#ddlAdjBillNo").val();
    ObjOPBilling.AdjSales = ObjAdjSales;


    ObjOPBilling.TaxAmount = parseFloat($("#txtTaxAmt").val());
    ObjOPBilling.Roundoff = parseFloat($("#txtRoundoff").val());
    ObjOPBilling.TotalAmount = $("#txtNetAmount").val();
    ObjOPBilling.Comments = $("#txtComments").val().trim();
    ObjOPBilling.Notes = $("#txtDebitnote").val().trim();
    ObjOPBilling.ImagePath = $("[id*=imgUploadSales_view]").attr("src");

    var DiscountAmount = parseFloat($("#txtDisAmount").val());
    if (isNaN(DiscountAmount))
        ObjOPBilling.DiscountAmount = 0;
    else
        ObjOPBilling.DiscountAmount = $("#txtDisAmount").val();


    if ($("#hdnSalesDiscountID").val() > 0) {
        ObjOPBilling.SalesDiscountID = $("#hdnSalesDiscountID").val();
        sMethodName = "UpdateSalesDiscount";
    }
    else {
        sMethodName = "AddSalesDiscount";
        ObjOPBilling.SalesDiscountID = 0;

    }
    //console.log(ObjOPBilling);
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
                        if (sMethodName == "AddSalesDiscount") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnSalesDiscountID").val(objResponse.Value);
                            EditRecord($("#hdnSalesDiscountID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").hide();
                            SetSessionValue("SalesDiscountID", $("#hdnSalesDiscountID").val());
                            var myWindow = window.open("PrintSalesDiscount.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                        }
                        else if (sMethodName == "UpdateSalesDiscount") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#btnList").click();
                            $("#hdnSalesDiscountID").val("0");
                        }

                        $('#compose-modaled').modal('hide');
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

$("#btnPrintbill").click(function () {
    SetSessionValue("SalesDiscount", $("#hdnSalesDiscountID").val());
    var myWindow = window.open("PrintSalesDiscount.aspx", "MsgWindow");
});
$("#btnPrintbill").click(function () {
    SetSessionValue("SalesDiscount", $("#hdnSalesDiscountID").val());
    var myWindow = window.open("PrintSalesDiscount.aspx", "MsgWindow");
});

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesDiscountByID",
        data: JSON.stringify({ ID: id, Type: 1 }),
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

                            $("#txtRefNo").attr("disabled", true);
                            $("#txtDisAmount").val(obj.DiscountAmount)
                            $("#hdnSalesDiscountID").val(obj.SalesDiscountID)
                            $("#txtRefNo").val(obj.DiscountNo).change();
                            $("#txtBillDate").val(obj.sDiscountDate);
                            $("#txtEntryDate").val(obj.sBillDate);
                            $("#txtSalesDate").val(obj.sSalesDate);
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#ddlOldInvoiceNo").val(obj.SalesEntry.SalesEntryID).change();
                            $("#txtDiscountAmount").val(obj.DiscountAmount);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtTaxAmt").val(obj.TaxAmount);
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("#ddlAdjBillNo").val(obj.AdjSales.SalesEntryID).change();
                            $("#txtComments").val(obj.Comments);
                            $("#txtDebitnote").val(obj.Notes);
                            $("[id*=imgUploadSales_view]").css("visibility", "visible");
                            $("[id*=imgUploadSales_view]").attr("src", obj.ImagePath);
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
function GetPassword() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetUserPassword",
        data: JSON.stringify({ ID: 0 }),
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
                            $("#hdRS").val(obj.ConfirmPassword);
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
function ShowDeleteRecord(id) {
    DeleteRecord(id, $("#txtReason").val());
    //$("#hdnID").val("");
    //$("#btnSave").show();
    //$("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    $('#compose-modal').modal({ show: true, backdrop: true });
    $("#txtID").val(id);
    $("#txtReason").focus();
    return false;
}


$("#btnOK").click(function () {

    if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined || $("#txtPassword").val().trim() != $("#hdRS").val()) {
        $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPassword").addClass('has-error'); $("#txtPassword").focus(); return false;
    } else { $("#divPassword").removeClass('has-error'); }

    DeleteRecord($("#txtID").val(), '');

});
function ClearCancelData() {
    $("#txtID").val("");
    $("#txtReason").val("");
    $("#txtPassword").val("");
    $('#compose-modal').modal('hide');
}
function DeleteRecord(id, Reason) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteSalesDiscount",
        data: JSON.stringify({ ID: id, Reason: Reason }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        //ClearCancelData();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "SalesDiscount_R_01" || objResponse.Value == "SalesDiscount_D_01") {
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

$("#aGeneral").click(function () {
    $("#SearchResult").hide();
    GetRecord();
});

$("#aSearchResult").click(function () {
    $("#SearchResult").show();

});
