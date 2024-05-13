var gMagazineData = [];
var gSalesReturnList = [];
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
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divSalesReturn").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#txtReturnDate,#txtBillDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtReturnDate,#txtBillDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    GetTaxList("ddlTax");
    GetTaxList("ddlTaxName");
    GetCustomerList("ddlCustomerName");
    var _Tfunctionality;
    GetProductList("ddlProductName");
    pLoadingSetup(true);
    GetSettings();
    GetRecord();

});
function GetSettings() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSettings",
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
                            $("#hdnMaxDiscount").val(obj.MaxDiscountPercent);
                            $("#hdnOpeningDate").val(obj.sOpeningDate);
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
function Edit_SalesReturnDetail(ID) {
    Bind_SalesReturnByID(ID, gOPBillingList);
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

$("#txtOldInvoiceNo").change(function () {
    if ($("#txtOldInvoiceNo").val().length > 0) {
        if ($("input[name=SupplierProduct]:checked").val() == "S") {
            $("#divAvailableQty").show();
            //$("#divCode").hide();
            //GetSalesReturnDetails($("#txtOldInvoiceNo").val());
        }
        else {
           // GetSalesReturnDetailsByInvoice($("#txtOldInvoiceNo").val());
        }
    }
    else {
        $("#ddlProductName").html("");
        $("#ddlProductName").val(0).change();
    }
    ClearSalesReturnFields();
});


$("#txtInvoiceNo").change(function () {
    if ($("#txtInvoiceNo").val().length > 0) {
        if ($("input[name=SupplierProduct]:checked").val() == "S") {
            //GetSalesReturnAdjustDetails($("#txtInvoiceNo").val());
        }
        else {
            //GetSalesReturnDetailsByAdjustInvoice($("#txtInvoiceNo").val());
        }
    }
    else {
        $("#ddlProductName").html("");
        $("#ddlProductName").val(0).change();
    }
    ClearSalesReturnFields();
});

$("#txtRoundoff").keypress(function (e) {
    if (e.which != 46 && e.which != 45 && e.which != 46 &&
        !(e.which >= 48 && e.which <= 57)) {
        return false;
    }
});

$("#txtRoundoff").change(function () {
    CalculateAmount();
});

$("input[type=radio]").change(function () {
    if ($("input[name=SupplierProduct]:checked").val() == "A") {
        $("#divAvailableQty").hide();
        //  $("#divCode").show();
        //GetProductList("ddlProductName");
    }
    else {
        $("#ddlProductName").html("");
        $("#ddlProductName").val(0).change();
        $("#txtSMSCode").val("");
        $("#txtRate").val(0);
        $("#divAvailableQty").show();
        // $("#divCode").hide();
        if ($("#txtInvoiceNo").val().length > 0) {
            GetSalesReturnAdjustDetails($("#txtInvoiceNo").val());
        }
    }
    ClearSalesReturnFields();
});

function getList() {
    if ($("input[name=SupplierProduct]:checked").val() == "S")
    {
        if ($("#ddlProductName").val() > 0) {
            var ProdID = $("#ddlProductName").val();
            for (var i = 0; i < gSalesReturnList.length; i++) {
                if (gSalesReturnList[i].SalesEntryTransID == ProdID) {
                    // $("#txtCode").val(gSalesReturnList[i].Product.SMSCode).blur();
                    $("#hdnProductID").val(gSalesReturnList[i].Product.ProductID);
                    $("#txtSMSCode").val(gSalesReturnList[i].Product.SMSCode);
                    $("#txtAvailableQty").val(gSalesReturnList[i].Quantity);
                    $("#txtQuantity").val(0);
                    $("#hdnTransTaxID").val(gSalesReturnList[i].Tax.TaxID);
                    $("#hdnTransTaxPercent").val(gSalesReturnList[i].Tax.TaxPercentage);
                    $("#hdnTransCGSTPercent").val(gSalesReturnList[i].Tax.CGSTPercent);
                    $("#hdnTransSGSTPercent").val(gSalesReturnList[i].Tax.SGSTPercent);
                    $("#hdnTransIGSTPercent").val(gSalesReturnList[i].Tax.IGSTPercent);
                    $("#txtRate").val(gSalesReturnList[i].Rate);
                    $("#txtDisPer").val(gSalesReturnList[i].DiscountPercentage);
                    $("#txtTax").val(gSalesReturnList[i].Tax.TaxPercentage);
                    $("#txtDisAmt").val(gSalesReturnList[i].DiscountAmount);
                    $("#txtSubTotal").val(gSalesReturnList[i].SubTotal);
                    $("#txtBarcode").val(gSalesReturnList[i].Barcode);
                    $("#hdnPreQtyID").val(gSalesReturnList[i].Quantity);

                }
            }
        }
    }
    else
    {
        $("#hdnCustomerID").val(0);
        $("#hdnProductID").val($("#ddlProductName").val());
    }
    GetRate();

}

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


$("#txtDiscountPercent").change(function () {

    var iDisPercent = parseFloat($("#txtDiscountPercent").val());
    $("#txtDisPer").val($("#txtDiscountPercent").val()).change();
    if (isNaN(iDisPercent)) iDisPercent = 0;
    var iSubtotal = 0; var iDiscAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            gOPBillingList[i].DiscountPercentage = iDisPercent;
            gOPBillingList[i].DiscountAmount = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * iDisPercent / 100;
            iSubtotal = (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate)) - parseFloat(gOPBillingList[i].DiscountAmount);
            gOPBillingList[i].SubTotal = parseFloat(iSubtotal).toFixed(2);
            if ($("#hdnStateCode").val() == 33) {
                gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].IGSTAmount = 0;
            }
            else {
                gOPBillingList[i].CGSTAmount = 0;
                gOPBillingList[i].SGSTAmount = 0;
                gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
            }
            gOPBillingList[i].Tax.TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
            gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
        }
    }
    DisplaySalesReturnList(gOPBillingList);
    CalculateAmount();
});

$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().length > 2) {
       // GetProductByCodeList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();

            getList();
        }
    }
    else if ($("#txtCode").val().length == 0) {
        GetProductList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();

            getList();
        }
    }
});

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
        var InvoiceNo1 = $("#ddlProductName option:selected").text();
        //if ($("#txtCode").val().length <= 0) {
        //    var result = InvoiceNo1.split('-');
        //    console.log(result[1]);
        //    var resultasa = result;
        //    $("#txtCode").val(result[1]).blur();
        //}
        getList();
        GetRate();
    }
});

function GetProductByCodeList(ddlname) {
    if ($("#txtCode").val().length > 3) {
        var sControlName = "#" + ddlname;
        dProgress(true);
        $(sControlName).empty();
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByCode",
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
                                    if (obj[index].IsActive)
                                        $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
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
}

$("#ddlTaxName").change(function () {
    var iTax = $("#ddlTaxName").val();
    if (iTax != undefined && iTax > 0) {
        GetTaxTransByID(iTax);
        CalculateSubtotal();
    }
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
                            $("#txtTaxPercent").val(obj.TaxPercentage);
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

$("#txtDisPer,#txtRate,#txtQuantity").change(function () {
    var iDisAmt = parseFloat($("#txtDisPer").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    var iSubTotal = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iDisAmt) / 100;
    $("#txtDisAmt").val(parseFloat(iSubTotal).toFixed(2));
    CalculateSubtotal();
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

$("#ddlCustomerName").change(function () {
    if ($("#ddlCustomerName").val() > 0) {
        GetCustomerByID($("#ddlCustomerName").val());
    }
});

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
                                CalculateAmount();
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

function CalculateSubtotal() {

    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());
    var iDisAmt = parseFloat($("#txtDisAmt").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    if (isNaN(iTax)) iTax = 0;
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty)) - iDisAmt;
    var iTaxPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iTax) / 100;
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));

    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }

    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
    CalculateAmount();
}
//$("#txtDisAmt").change(function () {
//    var iDisAmt = parseFloat($("#txtDisAmt").val());
//    var iRate = parseFloat($("#txtRate").val());
//    var iqty = parseFloat($("#txtQuantity").val());
//    if (isNaN(iRate)) iRate = 0;
//    if (isNaN(iqty)) iqty = 0;
//    if (isNaN(iDisAmt)) iDisAmt = 0;
//    var iSubTotal = (parseFloat(iDisAmt) * 100) / (parseFloat(iRate) * parseFloat(iqty));
//    $("#txtDisPer").val(parseFloat(iSubTotal).toFixed(2));
//    CalculateSubtotal();
//});


function GetRate() {
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetWholeSaleProductRate",
            data: JSON.stringify({ ID: $("#ddlProductName").val(), type: $("#txtSMSCode").val().trim(), SupplierID: $("#hdnCustomerID").val() }),
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
                               // $("#txtRate").val(obj.Rate);
                                $("#hdnOriginalRate").val(obj.Rate);
                                $("#txtSMSCode").val(obj.SMSCode);
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
        return false;
    }
}


function GetSalesReturnDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByInvoiceReturn",
        data: JSON.stringify({ InvoiceNo: id }),
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
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtInvoiceDate").val(obj.sInvoiceDate);
                            $("#hdnCustomerID").val(obj.Customer.CustomerID);
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            GetCustomerByID(obj.Customer.CustomerID);
                            gSalesReturnList = [];
                            var ObjProduct = obj.SalesEntryTrans;
                            $("#ddlProductName").empty();
                            $("#ddlProductName").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                var objTax = new Object();
                                objTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTax;

                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                $("#ddlProductName").append("<option value='" + ObjProduct[index].SalesEntryTransID + "'>" + ObjProduct[index].Product.ProductName + ' - ' + ObjProduct[index].Product.SMSCode + ' - ' + ObjProduct[index].Product.ProductCode + "</option>");
                                var objProducts = new Object();
                                objProducts.ProductID = ObjProduct[index].Product.ProductID;
                                objProducts.ProductName = ObjProduct[index].Product.ProductName;
                                objProducts.ProductCode = ObjProduct[index].Product.ProductCode;
                                objProducts.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objProducts;
                                // AddSalesReturnData(objTemp);

                                gSalesReturnList.push(objTemp);
                            }
                            if ($("#hdnSalesReturnTransID").val() > 0)
                                $("#ddlProductName").val($("#hdnSalesReturnTransID").val()).change();
                            else
                                $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
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

function GetSalesReturnAdjustDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByInvoiceReturn",
        data: JSON.stringify({ InvoiceNo: id }),
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
                            $("#hdnSalesID").val(obj.SalesEntryID);
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

function GetSalesReturnDetailsByInvoice(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByInvoiceReturn",
        data: JSON.stringify({ InvoiceNo: id, SalesReturnID: $("#hdnSalesReturnID").val() }),
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
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtInvoiceDate").val(obj.sInvoiceDate);
                            $("#hdnCustomerID").val(obj.Customer.CustomerID);
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            GetCustomerByID(obj.Customer.CustomerID);
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

function GetSalesReturnDetailsByAdjustInvoice(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByInvoiceReturn",
        data: JSON.stringify({ InvoiceNo: id, SalesReturnID: $("#hdnSalesReturnID").val() }),
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
                            $("#hdnSalesID").val(obj.SalesEntryID);
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

$("#ddlTax").change(function () {
    var iTaxID = $("#ddlTax").val();
    //$("#ddlTaxName").val($("#ddlTax").val()).change();
    if (iTaxID != undefined && iTaxID > 0) {
        GetTaxByID(iTaxID);
        var iSubtotal = 0;
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].StatusFlag != "D") {
                iSubtotal = gOPBillingList[i].SubTotal;
                gOPBillingList[i].Tax.TaxID = iTaxID;
                gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());

                if ($("#hdnStateCode").val() == 33) {
                    gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                    gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                    gOPBillingList[i].Tax.IGSTPercent = 0;
                    gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                    gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                    gOPBillingList[i].IGSTAmount = 0;
                }
                else {
                    gOPBillingList[i].Tax.CGSTPercent = 0;
                    gOPBillingList[i].Tax.SGSTPercent = 0;
                    gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                    gOPBillingList[i].CGSTAmount = 0;
                    gOPBillingList[i].SGSTAmount = 0;
                    var IGST = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                    gOPBillingList[i].IGSTAmount = IGST;
                }
                gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
                gOPBillingList[i].Tax.TaxAmount = (parseFloat(gOPBillingList[i].Tax.CGSTAmount) + parseFloat(gOPBillingList[i].Tax.SGSTAmount) + parseFloat(gOPBillingList[i].Tax.IGSTAmount)).toFixed(2);
            }
        }
        DisplaySalesReturnList(gOPBillingList);
        CalculateAmount();
    }

});

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnSalesReturnID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#divTab").hide();
    $("#divSalesReturn").show();
    $("#txtInvoiceNo").attr("disabled", false);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    gOPBillingList = [];
    gSalesReturnList = [];
    ClearSalesReturnTab();
    $("input[type=radio]").change();
    $("#ddlTaxName").val(2).change();
    $("#ddlTax").val(2).change();
    $("#divOPBillingList").empty();
    $("#rdoSupplier").prop("checked", true);
    $("#txtReturnDate").focus();
    $("#txtBillNo").focus();
    $("#imagePurchasefile").val("");
    $("#imagePurchasefile2").val("");
    $("#imagePurchasefile3").val("");
    $("#txtSNo").val("1");
    $("#hdnSalesReturnTransID").val("0");
    $("#txtComments").val("");
    return false;
});


$("#btnClearImage1").click(function () {
    $get("imgUploadPurchase1_view").src = "";
    $("#imagePurchasefile").val("");
});

$("#btnClearImage2").click(function () {
    $get("imgUploadPurchase2_view").src = "";
    $("#imagePurchasefile2").val("");
});

$("#btnClearImage3").click(function () {
    $get("imgUploadPurchase3_view").src = "";
    $("#imagePurchasefile3").val("");
});

$("#btndetailsCancel").click(function () {
    $('#composedetails').modal('hide');
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divSalesReturn").hide();

    GetRecord();
    return false;
});

$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnSalesReturnID").val("0");
    gSalesReturnList = [];
    gOPBillingList = [];
    ClearSalesReturnTab();
    $("#btnList").click();
    return false;
});

function ClearSalesReturnTab() {
    $("#txtReturnNo").val("");
    $("#txtReturnDate").val("");
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtInvoiceNo").val("");
    ClearSalesReturnFields();
    $("#hdnPatientID").val("");
    $("#txtSubtotal").val("0");
    $("#txtDiscountPercent").val("0");
    $("#ddlTax").val(0).change();
    $("#txtCGST").val("0");
    $("#txtSGST").val("0");
    $("#txtIGST").val("0");
    $("#txtTaxAmount").val("0");
    $("#txtDiscount").val("0");
    $("#txtRoundoff").val("0");
    $("#txtDiscount").val("0");
    $("#txtGrossTotal").val("0");
    $("#txtInvoiceDate").val("");
    $("#txtOldInvoiceNo").val("");
    $("#txtTotalAmount").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    $("#txtPhone").val();
    $("#txtAddress").val();
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#ddlProductName").val(null).change();
    $("#txtSMSCode").val("");
    $("#txtDisPer").val("0");
    $get("imgUploadPurchase1_view").src = "";
    $get("imgUploadPurchase2_view").src = "";
    $get("imgUploadPurchase3_view").src = "";
    $("[id*=imgUploadPurchase1_view]").css("visibility", "hidden");
    $("[id*=imgUploadPurchase2_view]").css("visibility", "hidden");
    $("[id*=imgUploadPurchase3_view]").css("visibility", "hidden");
    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtReturnDate").val(d + "/" + m + "/" + y);

    $("#txtReturnNo").attr("disabled", false);
    $("#ddlDoctor").attr("disabled", false);
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
                            $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
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

function GetEmployeeList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetUser",
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
                                    $(sControlName).append("<option value='" + obj[index].UserID + "'>" + obj[index].EmployeeCode + "</option>");
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

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    else { $("#divSelectProductName").removeClass('has-error'); }
    if ($("input[name=SupplierProduct]:checked").val() == "S") {
        if (parseFloat($("#hdnPreQtyID").val()) < parseFloat($("#txtQuantity").val())) {
            $.jGrowl("Please enter quantity less then sold quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
        } else { $("#divQuantity").removeClass('has-error'); }
    }
    if ($("#txtQuantity").val() == "0" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null) {
        $.jGrowl("Please enter quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    } else { $("#divQuantity").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.SalesReturnTransID = 0;

    var oProduct = new Object();
    oProduct.ProductID = $("#hdnProductID").val();
    oProduct.ProductName = $("#ddlProductName option:selected").text();
    oProduct.SMSCode = $("#txtSMSCode").val();
    ObjData.Product = oProduct;
    ObjData.SalesEntryTransID = $("#ddlProductName").val();
    ObjData.Quantity = parseFloat($("#txtQuantity").val());
    ObjData.Rate = parseFloat($("#txtRate").val());
    ObjData.DiscountPercentage = parseFloat($("#txtDisPer").val());
    ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());
    ObjData.SubTotal = parseFloat($("#txtSubTotal").val());
    ObjData.Barcode = $("#txtBarcode").val();
    ObjData.Notes = $("#txtNotes").val();

    $("#hdnTransTaxID").val($("#ddlTaxName").val());
    var oTaxTrans = new Object();

    oTaxTrans.TaxID = $("#ddlTaxName").val();
    oTaxTrans.TaxPercentage = $("#hdnTransTaxPercent").val().trim();
    oTaxTrans.CGSTPercent = $("#hdnTransCGSTPercent").val().trim();
    oTaxTrans.SGSTPercent = $("#hdnTransSGSTPercent").val().trim();
    oTaxTrans.IGSTPercent = $("#hdnTransIGSTPercent").val().trim();
    oTaxTrans.TaxAmount = parseFloat($("#txtTaxAmt").val());

    ObjData.Tax = oTaxTrans;
    ObjData.SGSTAmount = $("#hdnTransSGSTAmount").val().trim();
    ObjData.CGSTAmount = $("#hdnTransCGSTAmount").val().trim();
    ObjData.IGSTAmount = $("#hdnTransIGSTAmount").val().trim();
    ObjData.TaxAmount = parseFloat($("#txtTaxAmt").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.SalesReturnTransID = 0;
        ObjData.StatusFlag = "I";
        AddSalesReturnData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnSalesReturnID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesReturnID = $("#hdnSalesReturnID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesReturnID = 0;
        }
        Update_SalesReturn(ObjData);
    }
    var scrollBottom = Math.max($('#tblOPBillingList').height());
    $('#divOPBillingList').scrollTop(scrollBottom);
    CalculateAmount();
    ClearSalesReturnFields();
    $("#ddlProductName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearSalesReturnFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlProductName").val(0).change();
    $("#txtSMSCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtDisPer").val("0");
    $("#txtNotes").val("");
    $("#txtCode").val("");
    $("#txtTaxAmt").val("0");
    $("#txtDisAmt").val("0");
    $("#txtSubTotal").val("0.00");
    $("#txtAvailableQty").val("0");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");
    if (parseFloat($("#txtDiscountPercent").val()) > 0) {
        $("#txtDisPer").val($("#txtDiscountPercent").val());
    }
    $("#ddlTaxName").val($("#ddlTax").val()).change();
    $("#ddlProductName").val("0").change();
    $("#divSelectProductName").show();
    $("#divProductName").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
    $("#divRate").removeClass('has-error');
    return false;
}
function AddSalesReturnData(oData) {
    gOPBillingList.push(oData);
    DisplaySalesReturnList(gOPBillingList);
    return false;
}

function DisplaySalesReturnList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 12) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>SMSCode</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. %</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. Amt</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th style='text-align: center' colspan='2' class='" + sColorCode + "'>Tax</th>";
        sTable += "<th class='" + sColorCode + "'>Notes</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Image</th>";
        
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                $("#txtSNo").val(sCount + 1);
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].Product.SMSCode + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].DiscountPercentage + "</td>";
                sTable += "<td>" + gData[i].DiscountAmount + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
                sTable += "<td style='text-align: center'>" + gData[i].Tax.TaxPercentage + " %</td>";
                sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].Notes + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_SalesReturnDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OPBillingDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].Product.ProductID + " onclick = 'GetProductByID(this.id)'><i class='fa fa-lg fa-file-image-o text-green'/></a></td>";

                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblOPBillingList_body").append(sTable);
            }
        }
    }
    else { $("#divOPBillingList").empty(); }

    return false;
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

function Bind_SalesReturnByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
  //  $("#txtBarcode").focus();
    var sCount = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            if (data[i].StatusFlag != "D") {
                $("#hdnOPSNo").val(ID);
                $("#txtSNo").val(ID);
                $("#hdnSalesReturnID").val(data[i].SalesReturnID);
                $("#hdnSalesReturnTransID").val(data[i].SalesEntryTransID);
               
                //GetProductList("ddlProductName");
                if ($("input[name=SupplierProduct]:checked").val() == "A")
                    $("#ddlProductName").val(data[i].Product.ProductID).change();
                else
                    $("#ddlProductName").val(data[i].SalesEntryTransID).change();
                $("#txtCode").val(data[i].Product.SMSCode).blur();
                $("#hdnProductID").val(data[i].Product.ProductID);
                $("#txtQuantity").val(data[i].Quantity);
                $("#txtRate").val(data[i].Rate);
                $("#txtNotes").val(data[i].Notes);
                $("#txtDisPer").val(data[i].DiscountPercentage);
                $("#txtDisAmt").val(data[i].DiscountAmount);
                $("#ddlTaxName").val(data[i].Tax.TaxID).change();
                $("#txtTaxAmt").val(data[i].TaxAmount);
                $("#hdnTransTaxPercent").val(data[i].Tax.TaxPercentage);
                $("#hdnTransCGSTPercent").val(data[i].Tax.CGSTPercent);
                $("#hdnTransSGSTPercent").val(data[i].Tax.SGSTPercent);
                $("#hdnTransIGSTPercent").val(data[i].Tax.IGSTPercent);
                $("#hdnTransSGSTAmount").val(data[i].SGSTAmount);
                $("#hdnTransCGSTAmount").val(data[i].CGSTAmount);
                $("#hdnTransIGSTAmount").val(data[i].IGSTAmount);

                $("#txtSubTotal").val(data[i].SubTotal);
                $("#txtBarcode").val(data[i].Barcode);
                $("#txtCode").val("");
            }
        }
    }
    return false;
}

function Update_SalesReturn(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].SalesReturnID = oData.SalesReturnID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            oProduct.SMSCode = oData.Product.SMSCode;
            gOPBillingList[i].Product = oProduct;
            gOPBillingList[i].SalesEntryTransID = oData.SalesEntryTransID;
            gOPBillingList[i].Quantity = oData.Quantity;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].Notes = oData.Notes;
            gOPBillingList[i].Tax.TaxID = oData.Tax.TaxID;
            gOPBillingList[i].Tax.TaxPercentage = oData.Tax.TaxPercentage;
            gOPBillingList[i].Tax.IGSTPercent = oData.Tax.IGSTPercent;
            gOPBillingList[i].Tax.SGSTPercent = oData.Tax.SGSTPercent;
            gOPBillingList[i].Tax.CGSTPercent = oData.Tax.CGSTPercent;
            gOPBillingList[i].Tax.IGSTAmount = oData.Tax.IGSTAmount;
            gOPBillingList[i].Tax.SGSTAmount = oData.Tax.SGSTAmount;
            gOPBillingList[i].Tax.CGSTAmount = oData.Tax.CGSTAmount;
            gOPBillingList[i].TaxAmount = oData.TaxAmount;
            gOPBillingList[i].DiscountPercentage = oData.DiscountPercentage;
            gOPBillingList[i].DiscountAmount = oData.DiscountAmount;
            gOPBillingList[i].SubTotal = oData.SubTotal;
            gOPBillingList[i].Barcode = oData.Barcode;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplaySalesReturnList(gOPBillingList);
    $("#btnAddSalesReturn").show();
    $("#btnUpdateSalesReturn").hide();
    ClearSalesReturnFields();
    $("#ddlProductName").focus();
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
                DisplaySalesReturnList(gOPBillingList);
                CalculateAmount();
            }
        }
    }
    return false;
}


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
        url: "WebServices/VHMSService.svc/GetTopSalesReturn",
        data: JSON.stringify({ PatientID: 0 }),
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

                                var table = "<tr id='" + obj[index].SalesReturnID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ReturnNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sReturnDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                if (obj[index].sBillDate == "01-01-1900")
                                    table += "<td class='hidden-xs'></td>";
                                else
                                    table += "<td class='hidden-xs'>" + obj[index].sBillDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].ReturnAmount + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalQty + "</td>";
                                // table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='PrintReturn' title='Click here to Print SalesReturn'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintReturn").click(function () {
                                SetSessionValue("SalesReturnID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesReturnInvoice.aspx", "MsgWindow");
                            });
                            $(".PrintSales").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnSalesReturnID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("SalesReturnID", AdmissionID);

                                var myWindow = window.open("PrintSalesReturn.aspx", "MsgWindow");
                                //PrintSalesReturnDetails();
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
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "7%" },
                            { "sWidth": "7%" },
                            { "sWidth": "25%" },
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
        url: "WebServices/VHMSService.svc/SearchSalesReturn",
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
                                if (obj[index].IsCancelled == "0") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].SalesReturnID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ReturnNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sReturnDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                if (obj[index].sBillDate == "01-01-1900")
                                    table += "<td class='hidden-xs'></td>";
                                else
                                    table += "<td class='hidden-xs'>" + obj[index].sBillDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].ReturnAmount + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalQty + "</td>";


                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }
                                // table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesReturnID + " class='PrintReturn' title='Click here to Print SalesReturn'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintReturn").click(function () {
                                SetSessionValue("SalesReturnID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesReturnInvoice.aspx", "MsgWindow");
                            });
                            $(".PrintSales").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnSalesReturnID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("SalesReturnID", AdmissionID);

                                var myWindow = window.open("PrintSalesReturn.aspx", "MsgWindow");
                                //PrintSalesReturnDetails();
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
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "7%" },
                            { "sWidth": "7%" },
                            { "sWidth": "25%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "5%" },
                            { "sWidth": "5%" },
                            { "sWidth": "5%" },
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
$("#btnCancel").click(function () {
    $('#compose-modal').modal('hide');
    return false;

    pLoadingSetup(true);
});

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});


function CalculateAmount() {
    var iSalesReturnAmount = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0, iGrossTotal = 0, iQty = 0;
    var iTotal = 0;
    var DisPercent = 0;

    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iSalesReturnAmount = iSalesReturnAmount + parseFloat(gOPBillingList[i].SubTotal);
            iBillingTaxAmt = iBillingTaxAmt + parseFloat(gOPBillingList[i].TaxAmount);
            iBillingCGST = iBillingCGST + parseFloat(gOPBillingList[i].CGSTAmount);
            iBillingSGST = iBillingSGST + parseFloat(gOPBillingList[i].SGSTAmount);
            iBillingIGST = iBillingIGST + parseFloat(gOPBillingList[i].IGSTAmount);
            iBillingDiscount = iBillingDiscount + parseFloat(gOPBillingList[i].DiscountAmount);
            iGrossTotal = parseFloat(iGrossTotal) + (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate));
            iQty = parseInt(iQty) + parseFloat(gOPBillingList[i].Quantity);
        }
    }

    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    $("#txtGrossTotal").val(parseFloat(iGrossTotal).toFixed(2));
    $("#txtSubtotal").val(parseFloat(iSalesReturnAmount).toFixed(2));

    $("#txtDiscount").val(parseFloat(iBillingDiscount).toFixed(2));

    $("#txtTaxAmount").val(parseFloat(iBillingTaxAmt).toFixed(2));
    $("#txtCGST").val(parseFloat(iBillingCGST).toFixed(2));
    $("#txtSGST").val(parseFloat(iBillingSGST).toFixed(2));
    $("#txtIGST").val(parseFloat(iBillingIGST).toFixed(2));
    iSalesReturnAmount = parseFloat(iSalesReturnAmount) + parseFloat(iBillingTaxAmt);

    $("#txtTotalQty").val(iQty);
    $("#txtTotalAmount").val((parseFloat(iSalesReturnAmount) + parseFloat(iround)).toFixed(2));
}

$("#btnSave,#btnUpdate").click(function () {
    $("#ddlTax").change();
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    var d1 = Date.parse($("#hdnOpeningDate").val());
    var d2 = Date.parse($("#txtReturnDate").val());
    if (d1 < d2) {
        $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
        return false;
    }
    if ($("#txtReturnDate").val().trim() == "" || $("#txtReturnDate").val().trim() == undefined) {
        $.jGrowl("Please select SalesReturn Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divReturnDate").addClass('has-error'); $("#txtReturnDate").focus(); return false;
    }
    else { $("#divReturnDate").removeClass('has-error'); }

    if ($("#txtInvoiceNo").val() == "0" || $("#txtInvoiceNo").val() == "" ||  $("#txtInvoiceNo").val() == undefined || $("#txtInvoiceNo").val() == null) {
        $.jGrowl("Please enter Adjust InvoiceNo", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divOPDNo").addClass('has-error'); $("#txtInvoiceNo").focus(); return false;
    } else { $("#divOPDNo").removeClass('has-error'); }

    //if ($("#txtOldInvoiceNo").val() == "0" || $("#txtOldInvoiceNo").val() == "" || $("#txtOldInvoiceNo").val() == undefined || $("#txtOldInvoiceNo").val() == null) {
    //    $.jGrowl("Please enter Old InvoiceNo", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divOldInvoiceNo").addClass('has-error'); $("#txtOldInvoiceNo").focus(); return false;
    //} else { $("#divOldInvoiceNo").removeClass('has-error'); }

    //if ($("#hdnCustomerID").val() == "0" || $("#txtName").val() == undefined || $("#txtName").val() == null || $("#txtName").val().trim() == "") {
    //    $.jGrowl("Please Enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divCustomer").addClass('has-error'); $("#txtOPDNo").focus(); return false;
    //}
    //else { $("#divCustomer").removeClass('has-error'); }


    //    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
    //        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divtax").addClass('has-error'); $("#ddlTax").focus(); return false;
    //    } else { $("#divtax").removeClass('has-error'); }


    //var Count=0
    //$.each($("input[name='CheckTrans']:checked"), function () {
    //    Count = Count +1;
    //});
    //if (Count == 0) {
    //    $.jGrowl("Select returned product", { sticky: false, theme: 'warning', life: jGrowlLife });
    //     return false;
    //}

    var iSalesReturnAmount = 0;

    //$.each($("input[name='CheckTrans']:checked"), function () {
    //    var objTemp = new Object();
    //    objTemp.sNO =  1;
    //    objTemp.SNo = 0;
    //    objTemp.StatusFlag = "I";
    //    objTemp.SalesRetunID = 0;
    //    var objProduct = new Object();
    //    objProduct.ProductID = $(this).attr('ProductID');
    //    objTemp.Product = objProduct;
    //    objTemp.SalesRetunTransID = 0;
    //    objTemp.SalesEntryTransID = $(this).attr('SalesEntryTransID');
    //    objTemp.Rate = $(this).attr('Rate');
    //    objTemp.Quantity = $(this).attr('Quantity');
    //    objTemp.Subtotal = $(this).attr('Subtotal');
    //    objTemp.Barcode = $(this).attr('Barcode');
    //});


    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    var ObjSalesReturn = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#ddlCustomerName").val();
    ObjSalesReturn.Customer = ObjCustomer;

    var Objtax = new Object();
    Objtax.TaxID = $("#ddlTax").val();
    ObjSalesReturn.Tax = Objtax;


    var ObjEmployee = new Object();
    ObjEmployee.UserID = $("#ddlEmployee").val();
    ObjSalesReturn.Employee = ObjEmployee;
    var Id = $("#hdnSalesID").val();
    if ($("#hdnSalesID").val() != "")
        ObjSalesReturn.SalesEntryID = $("#hdnSalesID").val();
    else
        ObjSalesReturn.SalesEntryID = 0;

    ObjSalesReturn.SalesReturnID = 0;
    ObjSalesReturn.ReturnNo = $("#txtReturnNo").val().trim();
    ObjSalesReturn.sReturnDate = $("#txtReturnDate").val().trim();
    ObjSalesReturn.DiscountAmount = parseFloat($("#txtDiscount").val());
    ObjSalesReturn.DiscountPercent = parseFloat($("#txtDiscountPercent").val());
    ObjSalesReturn.OldInvoiceNo = $("#txtOldInvoiceNo").val();
    ObjSalesReturn.TotalAmount = parseFloat($("#txtSubtotal").val());
    ObjSalesReturn.ReturnAmount = parseFloat($("#txtTotalAmount").val());
    ObjSalesReturn.TaxAmount = parseFloat($("#txtTaxAmount").val());
    ObjSalesReturn.TaxPercent = parseFloat($("#txtTaxPercent").text());
    ObjSalesReturn.CGSTAmount = parseFloat($("#txtCGST").val());
    ObjSalesReturn.SGSTAmount = parseFloat($("#txtSGST").val());
    ObjSalesReturn.IGSTAmount = parseFloat($("#txtIGST").val());
    ObjSalesReturn.BillNo = $("#txtBillNo").val();
    ObjSalesReturn.sBillDate = $("#txtBillDate").val();
    ObjSalesReturn.Comments = $("#txtComments").val().trim();
    ObjSalesReturn.Roundoff = $("#txtRoundoff").val().trim();
    ObjSalesReturn.ImagePath1 = $("[id*=imgUploadPurchase1_view]").attr("src");
    ObjSalesReturn.ImagePath2 = $("[id*=imgUploadPurchase2_view]").attr("src");
    ObjSalesReturn.ImagePath3 = $("[id*=imgUploadPurchase3_view]").attr("src");
    if ($("input[name=SupplierProduct]:checked").val() == "S") {
        ObjSalesReturn.InvoiceBillType = true;
        ObjSalesReturn.CustomerBillType = false;
    }
    else {
        ObjSalesReturn.CustomerBillType = true;
        ObjSalesReturn.InvoiceBillType = false;
    }

    ObjSalesReturn.SalesReturnTrans = gOPBillingList;
    //ObjSalesReturn.SalesReturnID = $("#hdnSalesReturnID").val();

    if ($("#hdnSalesReturnID").val() > 0) {
        ObjSalesReturn.SalesReturnID = $("#hdnSalesReturnID").val();
        gOPBillingList.SalesReturnID = ObjSalesReturn.SalesReturnID;
        ObjSalesReturn.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdateSalesReturn";
    }
    else {
        sMethodName = "AddSalesReturn";
        ObjSalesReturn.SalesReturnID = 0
    }

    SaveandUpdateSalesReturn(ObjSalesReturn, sMethodName);

});
function SaveandUpdateSalesReturn(ObjSalesReturn, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjSalesReturn }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearSalesReturnTab();
                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divSalesReturn").hide();
                        if (sMethodName == "AddSalesReturn") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            // $("#hdnSalesReturnID").val(objResponse.Value);
                            //SetSessionValue("SalesReturnID", $("#hdnSalesReturnID").val());
                            //var myWindow = window.open("PrintSalesReturnInvoice.aspx", "MsgWindow");

                        }
                        else if (sMethodName == "UpdateSalesReturn") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                        $("#hdnSalesReturnID").val("0");
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
    SetSessionValue("SalesReturnID", $("#hdnSalesReturnID").val());
    var myWindow = window.open("PrintSalesReturnInvoice.aspx", "MsgWindow");
});
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesReturnByID",
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

                            $("#txtReturnNo").attr("disabled", true);
                            //$("#txtInvoiceNo").attr("disabled", true);
                            $("input[type=radio]").change();
                            $("#hdnSalesReturnID").val(obj.SalesReturnID)
                            $("#hdnCustomerID").val(obj.Customer.CustomerID).change();
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#ddlTax").val(obj.Tax.TaxID).change();
                            $("#txtReturnNo").val(obj.ReturnNo);
                            $("#txtReturnDate").val(obj.sReturnDate);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscount").val(obj.DiscountAmount);
                            $("#txtTotalAmount").val(obj.ReturnAmount);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtTaxAmount").val(obj.TaxAmount);
                            $("#txtTaxPercent").text(obj.TaxPercent);
                            $("#txtCGST").val(obj.CGSTAmount);
                            $("#txtSGST").val(obj.SGSTAmount);
                            $("#txtIGST").val(obj.IGSTAmount);
                            $("#txtAddress").val(obj.Customer.Address);
                            $("#txtPhone").val(obj.Customer.MobileNo);
                            $("#txtInvoiceNo").val(obj.InvoiceNo).change();
                            $("#hdnSalesID").val(obj.SalesEntryID);
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtBillNo").val(obj.BillNo);
                            $("#txtComments").val(obj.Comments);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("[id*=imgUploadPurchase1_view]").css("visibility", "visible");
                            $("[id*=imgUploadPurchase1_view]").attr("src", obj.ImagePath1);
                            $("[id*=imgUploadPurchase2_view]").css("visibility", "visible");
                            $("[id*=imgUploadPurchase2_view]").attr("src", obj.ImagePath2);
                            $("[id*=imgUploadPurchase3_view]").css("visibility", "visible");
                            $("[id*=imgUploadPurchase3_view]").attr("src", obj.ImagePath3);
                            if (obj.InvoiceBillType == true) {
                                $("#rdoSupplier").prop("checked", true);
                                $("#rdoAll").prop("checked", false);
                            }
                            else {
                                $("#rdoAll").prop("checked", true);
                                $("#rdoSupplier").prop("checked", false);
                            }
                            $("input[type=radio]").change();
                            $("#txtOldInvoiceNo").val(obj.OldInvoiceNo).change();
                          
                            $("#txtBillDate").val(obj.sBillDate);
                            gOPBillingList = [];
                            var ObjProduct = obj.SalesReturnTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";
                                var objProduct = new Object();
                                objProduct.ProductID = ObjProduct[index].Product.ProductID;
                                objProduct.ProductName = ObjProduct[index].Product.ProductName;
                                objProduct.ProductCode = ObjProduct[index].Product.ProductCode;
                                objProduct.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objProduct;

                                var objTax = new Object();
                                objTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;

                                objTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;

                                objTemp.Tax = objTax;

                                objTemp.TaxAmount = ObjProduct[index].TaxAmount;
                                objTemp.CGSTAmount = ObjProduct[index].CGSTAmount;
                                objTemp.SGSTAmount = ObjProduct[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjProduct[index].IGSTAmount;

                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                objTemp.SalesReturnTransID = ObjProduct[index].SalesReturnTransID;
                                objTemp.SalesReturnID = ObjProduct[index].SalesReturnID;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.ProductName = ObjProduct[index].ProductName;
                                objTemp.Notes = ObjProduct[index].Notes;


                                AddSalesReturnData(objTemp);

                                // gOPBillingList.push(objTemp);
                            }
                            CalculateAmount();
                            // location.reload();
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
    $("#hdnID").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    $('#compose-modal').modal({ show: true, backdrop: true });
    $("#txtID").val(id);
    $("#txtReason").focus();
    return false;
}


$("#btnOK").click(function () {

    if ($("#txtReason").val() == undefined || $("#txtReason").val() == null || $("#txtReason").val().trim() == "") {
        $.jGrowl("Please enter reason for cancelling", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divReason").addClass('has-error'); $("#txtReason").focus(); return false;
    }
    else { $("#divReason").removeClass('has-error'); }

    DeleteRecord($("#txtID").val(), $("#txtReason").val());

});
function ClearCancelData() {
    $("#txtID").val("");
    $("#txtReason").val("");
    $('#compose-modal').modal('hide');
}
function DeleteRecord(id, Reason) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteSalesReturn",
        data: JSON.stringify({ ID: id }),
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
                    else if (objResponse.Value == "SalesReturn_R_01" || objResponse.Value == "SalesReturn_D_01") {
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
