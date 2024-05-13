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

    $("#hdnPurchaseDiscountID").val("0");
    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    $("#ddlTaxName").hide();
    GetSupplierList("ddlSupplierName");
    GetTaxList("ddlTaxName");
    GetSupplierName("ddlCategoryName");
    GetTaxList("ddlTax");
    $("#ddlSupplierName").change();
    $("#ddlTaxName").change();
    $("#ddlBillNo").change();
    $("#txtBillDate").attr("data-link-format", "dd/MM/yyyy");

    GetPassword();
    $("#txtBillDate").datetimepicker({
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

            GetReceivedPurchaseReturn(parseInt($.cookie("PurchaseDiscountID")));
            $("#hdnPurchaseDiscountID").val(parseInt($.cookie("PurchaseDiscountID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("PurchaseDiscountID", null);
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

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}


$("#ddlCategoryName").change(function () {
    GetRecord();
});

$("#ddlSupplierName").change(function () {
    var iTaxID = $("#ddlSupplierName").val();

    if (iTaxID != undefined && iTaxID > 0) {
        GetBillNo("ddlBillNo");
        GetPendingPurchaseBillNo("ddlAdjBillNo");
        $("#ddlProductName").val(null).change();
        GetSupplierByID(iTaxID);
        CalculateAmount();

    }
});

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
    $get("imgUploadPurchase1_view").src = "";
    $("#imagePurchasefile").val("");
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
    $("#hdnPurchaseDiscountID").val("0");
    $('#compose-modaled').modal('hide');
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});
function ClearOPBillingTab() {
    $("#hdnPurchaseDiscountID").val("0");
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtComments").val("");
    $("#txtDisAmount").val("0");
    $("#txtTaxAmt").val("0");
    $("#txtNetAmount").val("0");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    $("#ddlSupplierName").val("0").change();
    $("#ddlBillNo").val("0").change();
    $("#ddlAdjBillNo").val("0").change();
    $("#txtBillNo").attr("disabled", false);
    $("#ddlTaxName").val(2).change();
    $get("imgUploadPurchase1_view").src = "";
    $("[id*=imgUploadPurchase1_view]").css("visibility", "hidden");
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

function GetRate() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductRate",
        data: JSON.stringify({ ID: $("#ddlProductName").val(), type: "P", SupplierID: $("#ddlSupplierName").val() }),
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
                            $("#txtRate").val(obj.Rate);
                            $("#txtSMSCode").val(obj.SMSCode);
                            $("#txtPartyCode").val(obj.ProductCode);
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

function GetPendingPurchaseBillNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPendingPurchaseDiscountBillNo",
        data: JSON.stringify({ SupplierID: $("#ddlSupplierName").val(), BillType: 1, PurchaseReturnID: $("#hdnPurchaseDiscountID").val() }),
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
                                $(sControlName).append("<option value='" + obj[index].PurchaseID + "'>" + obj[index].BillNo + "</option>");
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

function GetBillNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseDiscountBillNo",
        data: JSON.stringify({ SupplierID: $("#ddlSupplierName").val(), BillType: 1, PurchaseDiscountID: $("#hdnPurchaseDiscountID").val() }),
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
                                $(sControlName).append("<option value='" + obj[index].PurchaseID + "'>" + obj[index].BillNo + "</option>");
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


$("#ddlBillNo").change(function () {
    $("#txtPurchaseDate").val("");
    if ($("#ddlBillNo").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetPurchaseByID",
            data: JSON.stringify({ ID: $("#ddlBillNo").val(), BillType: 1 }),
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
                                $("#txtPurchaseDate").val(obj.sBillDate);
                                $("#txtDisAmount").val(obj.PurchaseDiscount);
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
});


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

function GetSupplierName(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSupplier",
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
                            $(sControlName).append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].SupplierID + "'>" + obj[index].SupplierName + "</option>");
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
                        $.jGrowl("Error  Occurcnced", { sticky: false, theme: 'danger', life: jGrowlLife });
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

function GetSupplierList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSupplier",
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
                                    $(sControlName).append("<option value='" + obj[index].SupplierID + "'>" + obj[index].SupplierName + "</option>");
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
                        $.jGrowl("Error  Occurncncvned", { sticky: false, theme: 'danger', life: jGrowlLife });
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

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    else { $("#divProductName").removeClass('has-error'); }

    //if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <= 0) {
    //    $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    //} else { $("#divQuantity").removeClass('has-error'); }

    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxTrans").addClass('has-error'); $("#ddlTax").focus(); return false;
    }
    else { $("#divTaxTrans").removeClass('has-error'); }

    if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null) {
        $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    } else { $("#divRate").removeClass('has-error'); }

    var iStockCount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].PurchaseTransID == $("#ddlProductName").val())
            iStockCount = iStockCount + 1;
    }

    if (this.id == "btnAddMagazine") {
        if (iStockCount > 0) {
            $.jGrowl("Product already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
        } else { $("#divProductName").removeClass('has-error'); }
    }
    else {
        if (iStockCount > 1) {
            $.jGrowl("Product already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
        } else { $("#divProductName").removeClass('has-error'); }
    }

    var ObjData = new Object();
    ObjData.PurchaseDiscountID = 0;

    var oProduct = new Object();

    oProduct.ProductID = $("#ddlProductName").val();
    oProduct.ProductName = $("#ddlProductName option:selected").text();
    oProduct.SMSCode = $("#txtSMSCode").val().toUpperCase();
    oProduct.ProductCode = $("#txtPartyCode").val();
    ObjData.Product = oProduct;

    var ObjData = new Object();
    ObjData.TaxID = $("#ddlTax").val();
    ObjData.TaxPercentage = $("#hdnTransTaxPercent").val().trim();
    ObjData.CGSTPercent = $("#hdnTransCGSTPercent").val().trim();
    ObjData.SGSTPercent = $("#hdnTransSGSTPercent").val().trim();
    ObjData.IGSTPercent = $("#hdnTransIGSTPercent").val().trim();
    ObjData.Tax = ObjData;
    $("#ddlTaxName").val($("#ddlTaxName").val());
    ObjData.SGSTAmount = $("#hdnTransSGSTAmount").val();
    ObjData.CGSTAmount = $("#hdnTransCGSTAmount").val();
    ObjData.IGSTAmount = $("#hdnTransIGSTAmount").val();

    ObjData.TaxAmount = parseFloat($("#txtTaxAmt").val());

    ObjData.PurchaseTransID = $("#ddlProductName").val();
    ObjData.Quantity = parseFloat($("#txtQuantity").val());
    ObjData.Rate = parseFloat($("#txtRate").val());
    ObjData.DiscountPercentage = parseFloat($("#txtDisPer").val());
    ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());
    ObjData.SubTotal = parseFloat($("#txtSubTotal").val());
    ObjData.Barcode = $("#txtBarcode").val();
    ObjData.Notes = $("#txtNotes").val();

    ClearOPBillingFields();
    $("#txtCode").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});

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

function GetProductByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductByID",
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
                            $("#myImg").empty();
                            gImageupload = [];
                            var Objimg = obj.ProductImages;

                            for (var index = 0; index < Objimg.length; index++) {
                                var ObjData = new Object();
                                ObjData.sNO = gImageupload.max() + 1;
                                ObjData.SNo = ObjData.sNO;
                                ObjData.filename = Objimg[index].Filename;
                                ObjData.filepath = Objimg[index].Filepath;
                                $('#myImg').append('<img src="' + Objimg[index].Filepath + '" style="width: 200px;height: 200px;margin: 0px 15px 15px 0px;">');
                            }

                            $(".modal-title").html("&nbsp;&nbsp; Product Image");
                            $('#composeProductImage').modal({ show: true, backdrop: true });
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


$("#btnProductImageCancel").click(function () {
    $('#composeProductImage').modal('hide');
    return false;
});


function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopPurchaseDiscount",
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

                                var table = "<tr id='" + obj[index].PurchaseDiscountID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].DiscountNo + "</td>";
                                table += "<td>" + obj[index].sDiscountDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                table += "<td>" + obj[index].Purchase.BillNo + "</td>";
                                table += "<td>" + obj[index].DiscountAmount + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='PrintPurchaseDiscount' title='Click here to Print PurchaseDiscount'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintPurchaseDiscount").click(function () {
                                SetSessionValue("PurchaseDiscountID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseDiscount.aspx", "MsgWindow");
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
        url: "WebServices/VHMSService.svc/GetSearchPurchaseDiscount",
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

                                var table = "<tr id='" + obj[index].PurchaseDiscountID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].DiscountNo + "</td>";
                                table += "<td>" + obj[index].sDiscountDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                table += "<td>" + obj[index].Purchase.BillNo + "</td>";
                                table += "<td>" + obj[index].DiscountAmount + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].Roundoff + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseDiscountID + " class='PrintPurchaseDiscount' title='Click here to Print PurchaseDiscount'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintPurchaseDiscount").click(function () {
                                SetSessionValue("PurchaseDiscountID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseDiscount.aspx", "MsgWindow");
                            });
                            $(".PrintOPBilling").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnPurchaseDiscountID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("PurchaseDiscountID", AdmissionID);

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

    if ($("#ddlSupplierName").val() == "0" || $("#ddlSupplierName").val() == undefined || $("#ddlSupplierName").val() == null) {
        $.jGrowl("Please select Supplier", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSupplierName").addClass('has-error'); $("#ddlSupplierName").focus(); return false;
    }
    else { $("#divSupplierName").removeClass('has-error'); }

    if ($("#ddlTaxName").val() == "0" || $("#ddlTaxName").val() == undefined || $("#ddlTaxName").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxName").addClass('has-error'); $("#ddlTaxName").focus(); return false;
    }
    else { $("#divTaxName").removeClass('has-error'); }

    var ObjOPBilling = new Object();

    ObjOPBilling.PurchaseDiscountID = 0;
    ObjOPBilling.DiscountNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sDiscountDate = $("#txtBillDate").val().trim();

    var ObjSupplier = new Object();
    ObjSupplier.SupplierID = $("#ddlSupplierName").val();
    ObjOPBilling.Supplier = ObjSupplier;

    var ObjTax = new Object();
    ObjTax.TaxID = $("#ddlTaxName").val();
    ObjOPBilling.Tax = ObjTax;

    var ObjPurchase = new Object();
    ObjPurchase.PurchaseID = $("#ddlBillNo").val();
    ObjOPBilling.Purchase = ObjPurchase;

    var ObjAdjPurchase = new Object();
    ObjAdjPurchase.PurchaseID = $("#ddlAdjBillNo").val();
    ObjOPBilling.AdjPurchase = ObjAdjPurchase;

    ObjOPBilling.TaxAmount = parseFloat($("#txtTaxAmt").val());
    ObjOPBilling.NetAmount = $("#txtNetAmount").val();
    ObjOPBilling.Roundoff = $("#txtRoundoff").val();
    ObjOPBilling.Notes = $("#txtComments").val().trim();
    ObjOPBilling.ImagePath = $("[id*=imgUploadPurchase1_view]").attr("src");

    var DiscountAmount = parseFloat($("#txtDisAmount").val());
    if (isNaN(DiscountAmount))
        ObjOPBilling.DiscountAmount = 0;
    else
        ObjOPBilling.DiscountAmount = $("#txtDisAmount").val();


    if ($("#hdnPurchaseDiscountID").val() > 0) {
        ObjOPBilling.PurchaseDiscountID = $("#hdnPurchaseDiscountID").val();
        sMethodName = "UpdatePurchaseDiscount";
    }
    else {
        sMethodName = "AddPurchaseDiscount";
        ObjOPBilling.PurchaseDiscountID = 0;

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
                        if (sMethodName == "AddPurchaseDiscount") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPurchaseDiscountID").val(objResponse.Value);
                            EditRecord($("#hdnPurchaseDiscountID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").hide();
                            SetSessionValue("PurchaseDiscountID", $("#hdnPurchaseDiscountID").val());
                            var myWindow = window.open("PrintPurchaseDiscount.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                        }
                        else if (sMethodName == "UpdatePurchaseDiscount") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#btnList").click();
                            $("#hdnPurchaseDiscountID").val("0");
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
    SetSessionValue("PurchaseDiscount", $("#hdnPurchaseDiscountID").val());
    var myWindow = window.open("PrintPurchaseDiscount.aspx", "MsgWindow");
});

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseDiscountByID",
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

                            $("#txtBillNo").attr("disabled", true);
                            $("#txtDisAmount").val(obj.DiscountAmount)
                            $("#hdnPurchaseDiscountID").val(obj.PurchaseDiscountID)
                            $("#txtBillNo").val(obj.DiscountNo).change();
                            $("#txtBillDate").val(obj.sDiscountDate);
                            $("#ddlSupplierName").val(obj.Supplier.SupplierID).change();
                            $("#txtDiscountAmount").val(obj.DiscountAmount);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtTaxAmt").val(obj.TaxAmount);
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("#ddlBillNo").val(obj.Purchase.PurchaseID).change();
                            $("#ddlAdjBillNo").val(obj.AdjPurchase.PurchaseID).change();
                            $("#txtComments").val(obj.Notes);
                            $("[id*=imgUploadPurchase1_view]").css("visibility", "visible");
                            $("[id*=imgUploadPurchase1_view]").attr("src", obj.ImagePath);
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
    //DeleteRecord(id, $("#txtReason").val());
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
        url: "WebServices/VHMSService.svc/DeletePurchaseDiscount",
        data: JSON.stringify({ ID: id, Reason: Reason }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearCancelData();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "PurchaseDiscount_R_01" || objResponse.Value == "PurchaseDiscount_D_01") {
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
