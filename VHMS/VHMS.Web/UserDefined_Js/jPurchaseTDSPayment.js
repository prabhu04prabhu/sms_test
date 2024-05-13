var gMagazineData = [];
var gTDSPaymentList = [];
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
    $("#divTDSPayment").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#txtReturnDate,#txtSlipDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtReturnDate,#txtSlipDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    GetSupplierList("ddlCustomerName");
    var _Tfunctionality;
    //GetProductList("ddlProductName");
    pLoadingSetup(true);
    GetRecord();

});

function Edit_TDSPaymentDetail(ID) {
    Bind_TDSPaymentByID(ID, gOPBillingList);
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

$("#ddlManualPayment").change(function () {
    var iManualPayment = $("#ddlManualPayment").val();
    if (iManualPayment == 0) {
        $("#divOldSalesInvoice").show();
        $("#divOldSalesTotalAmt").show();
        $("#divRoundoffTrans").show();
        $("#txtDisPer").attr('readonly', true);
        $("#txtOldSalesTotalAmt").val(0);
        $("#txtDisPer").val(0);
        $("#ddlOldSalesInvoice").val(0).change();
        
    }
    else {
        $("#divOldSalesInvoice").hide();
        $("#divOldSalesTotalAmt").hide();
        $("#divRoundoffTrans").hide();
        $("#txtDisPer").attr('readonly', false);
        $("#txtOldSalesTotalAmt").val(0);
        $("#txtDisPer").val(0);
    }
    return false;
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
                            $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
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

$("#ddlOldSalesInvoice").change(function () {
    if ($("#ddlOldSalesInvoice").val() > 0) {
        if ($("#ddlBillType").val() == "Purchase")
            GetOldSalesInvoiceRate();
        else
            GetOldExpenseByID();
    }
});

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
        if ($("#ddlBillType").val() == "Purchase")
            GetRate();
        else
            GetExpenseByID();
    }
});

$("#txtOldSalesTotalAmt,#txtRate,#txtRoundoffTrans").change(function () {
    var iRate = parseFloat($("#txtOldSalesTotalAmt").val());
    var iqty = parseFloat($("#txtRate").val());
    var iRoundoffTrans = parseFloat($("#txtRoundoffTrans").val());
    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iRoundoffTrans)) iRoundoffTrans = 0;
    var iSubTotal = parseFloat(iRate) * parseFloat(iqty) / 100;
    var TotalTDS = (parseFloat(iSubTotal) + parseFloat(iRoundoffTrans)).toFixed(2);
    $("#txtDisPer").val(parseFloat(TotalTDS).toFixed(2));
    // CalculateAmount();
});


$("#ddlBillType").change(function () {
    $("#ddlCustomerName").empty();
    $("#ddlOldSalesInvoice").empty();
    $("#ddlProductName").empty();
    $("#ddlCustomerName").val("0").change();
    $("#ddlOldSalesInvoice").val("0").change();
    $("#ddlProductName").val("0").change();
   

    if ($("#ddlBillType").val() == "Purchase")
        GetSupplierList("ddlCustomerName");
    else
        GetPartyLedger("ddlCustomerName");
});


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
                            $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
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

function GetPartyLedger(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPartyLedger",
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

$("#ddlCustomerName").change(function () {
    if ($("#ddlCustomerName").val() > 0) {
        if ($("#ddlBillType").val() == "Purchase") {
            GetAdjustTDSPurchase();
            GetTDSPurchase();
        }
        else
        {
            GetAdjustTDSExpense();
            GetTDSExpense();
        }
    }
});



function GetTDSExpense() {
    var sControlName = "#ddlOldSalesInvoice";
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTDSExpense",
        data: JSON.stringify({ ipatientID:  $("#ddlCustomerName").val() }),
        contentType: "application/json; charset=utf-8",
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
                                $(sControlName).append("<option value='" + obj[index].ExpenseID + "'>" + obj[index].BillNo + " - " + (obj[index].sBillDate) + "</option>");
                            }
                            $(sControlName).val(0).change();
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

function GetAdjustTDSExpense() {
    var sControlName = "#ddlProductName";
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetAdjustTDSExpense",
        data: JSON.stringify({ ipatientID: $("#ddlCustomerName").val(), iTDSID: $("#hdnTDSPaymentID").val() }),
        contentType: "application/json; charset=utf-8",
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
                                $(sControlName).append("<option value='" + obj[index].ExpenseID + "'>" + obj[index].BillNo + " - " + (obj[index].sBillDate) + "</option>");
                            }
                            $(sControlName).val(0).change();
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


function GetTDSPurchase() {
    var sControlName = "#ddlOldSalesInvoice";
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTDSPurchase",
        data: JSON.stringify({ ipatientID: 0, iSupplierID: $("#ddlCustomerName").val() }),
        contentType: "application/json; charset=utf-8",
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
                                $(sControlName).append("<option value='" + obj[index].PurchaseID + "'>" + obj[index].BillNo + " - " + (obj[index].sBillDate) + "</option>");
                           }
                            $(sControlName).val(0).change();
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

function GetAdjustTDSPurchase() {
    var sControlName = "#ddlProductName";
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetAdjustTDSPurchase",
        data: JSON.stringify({ ipatientID: 0, iSupplierID: $("#ddlCustomerName").val(), iTDSID: $("#hdnTDSPaymentID").val()}),
        contentType: "application/json; charset=utf-8",
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
                                    $(sControlName).append("<option value='" + obj[index].PurchaseID + "'>" + obj[index].BillNo + " - " + (obj[index].sBillDate) + "</option>");
                            }
                            $(sControlName).val(0).change();
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

function GetExpenseByID() {
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetTDSExpenseByID",
            data: JSON.stringify({ iExpenseID: $("#ddlProductName").val() }),
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
                                $("#txtAvailableQty").val(obj.TotalAmount).change();
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

function GetOldExpenseByID() {
    if ($("#ddlOldSalesInvoice").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetTDSExpenseByID",
            data: JSON.stringify({ iExpenseID: $("#ddlOldSalesInvoice").val() }),
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
                                $("#txtOldSalesTotalAmt").val(obj.TotalAmount).change();
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


function GetRate() {
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetTDSPurchaseByID",
            data: JSON.stringify({ iPurchaseID: $("#ddlProductName").val(), BillType:1 }),
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
                                $("#txtAvailableQty").val(obj.TotalAmount).change();
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

function GetOldSalesInvoiceRate() {
    if ($("#ddlOldSalesInvoice").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetTDSPurchaseByID",
            data: JSON.stringify({ iPurchaseID: $("#ddlOldSalesInvoice").val(), BillType: 1 }),
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
                                $("#txtOldSalesTotalAmt").val(obj.TotalAmount).change();
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
                            $(sControlName).append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + ' - ' + obj[index].SMSCode + ' - ' + obj[index].ProductCode + "</option>");
                            }
                            $("#ddlProductName").val(0).change();
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

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#imgUpload1").val('');
    $get("imgUpload1_view").src = "";
    $("#hdnTDSPaymentID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#divTab").hide();
    $("#divTDSPayment").show();
    $("#txtInvoiceNo").attr("disabled", false);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    gOPBillingList = [];
    gTDSPaymentList = [];
    ClearTDSPaymentTab();
    $("input[type=radio]").change();
    $("#ddlTaxName").val(2).change();
    $("#ddlTax").val(2).change();
    $("#divOPBillingList").empty();
    $("#rdoSupplier").prop("checked", true);
    $("#txtReturnDate").focus();
    $("#txtBillNo").focus();
    $("#ddlCustomerName").val("0").change(); 
    $("#ddlOldSalesInvoice").val("0").change(); 
    $("#txtSNo").val("1");
    $("#hdnTDSPaymentTransID").val("0");
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
    $("#divTDSPayment").hide();

    GetRecord();
    return false;
});

$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnTDSPaymentID").val("0");
    gTDSPaymentList = [];
    gOPBillingList = [];
    ClearTDSPaymentTab();
    $("#btnList").click();
    return false;
});

function ClearTDSPaymentTab() {
    $("#txtReturnNo").val("");
    $("#txtReturnDate").val("");
    $("#txtSlipNo").val("");
    $("#ddlManualPayment").val(0).change(); 
    $("#txtSlipDate").val("");
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtInvoiceNo").val("");
    ClearTDSPaymentFields();
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
    $("#txtTotalQty").val("0");
    $("#txtAddress").val("");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    $("#txtPhone").val();
    $("#txtAddress").val();
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#ddlProductName").val(null).change();
    $("#txtSMSCode").val("");
    $("#txtDisPer").val("0");

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtReturnDate").val(d + "/" + m + "/" + y);

    $("#txtReturnNo").attr("disabled", false);
    $("#ddlDoctor").attr("disabled", false);
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
        $.jGrowl("Please select Sales Invoice No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    if ($("#ddlManualPayment").val() == 0) {
        if ($("#ddlOldSalesInvoice").val() == "0" || $("#ddlOldSalesInvoice").val() == undefined || $("#ddlOldSalesInvoice").val() == null) {
            $.jGrowl("Please select Old Sales Invoice No", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divOldSalesInvoice").addClass('has-error'); $("#ddlOldSalesInvoice").focus(); return false;
        }
    }

    if ($("#txtRate").val() == "0" || $("#txtRate").val() == undefined || $("#txtRate").val() == null) {
        $.jGrowl("Please enter TDS %", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    } else { $("#divRate").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.TDSPaymentTransID = 0;


    var oProduct = new Object();
    oProduct.PurchaseID = $("#ddlProductName").val();
    oProduct.BillNo = $("#ddlProductName option:selected").text();
    oProduct.NetAmount = parseFloat($("#txtAvailableQty").val());
    ObjData.PurchaseEntry = oProduct

    var OldPurchase = new Object();
    if ($("#ddlManualPayment").val() ==0) {
        OldPurchase.PurchaseID = $("#ddlOldSalesInvoice").val();
        OldPurchase.BillNo = $("#ddlOldSalesInvoice option:selected").text();
        OldPurchase.NetAmount = parseFloat($("#txtOldSalesTotalAmt").val());
    }
    else
    {
        OldPurchase.PurchaseID =0;
        OldPurchase.BillNo = "";
        OldPurchase.NetAmount = 0;
      
    }
    ObjData.oldPurchaseEntry = OldPurchase;

    ObjData.TDSPercent = parseFloat($("#txtRate").val());
    ObjData.Roundoff = parseFloat($("#txtRoundoffTrans").val());
    ObjData.TDSAmount = parseFloat($("#txtDisPer").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.TDSPaymentTransID = 0;
        ObjData.StatusFlag = "I";
        AddTDSPaymentData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnTDSPaymentID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.TDSPaymentID = $("#hdnTDSPaymentID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.TDSPaymentID = 0;
        }
        Update_TDSPayment(ObjData);
    }
    var scrollBottom = Math.max($('#tblOPBillingList').height());
    $('#divOPBillingList').scrollTop(scrollBottom);
    CalculateAmount();
    ClearTDSPaymentFields();
    $("#ddlProductName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearTDSPaymentFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlProductName").val(0).change();
    $("#ddlOldSalesInvoice").val(0).change();
    $("#txtOldSalesTotalAmt").val("0");
    $("#txtSMSCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0.10");
    $("#txtRoundoffTrans").val("0");
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
    $("#ddlOldSalesInvoice").val(0).change();
    $("#divSelectProductName").show();
    $("#divProductName").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
    $("#divRate").removeClass('has-error');
    return false;
}
function AddTDSPaymentData(oData) {
    gOPBillingList.push(oData);
    DisplayTDSPaymentList(gOPBillingList);
    return false;
}

function DisplayTDSPaymentList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 12) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Old Purchase Bill No</th>";
        sTable += "<th class='" + sColorCode + "'>Old Purchase TotalAmount</th>";
        sTable += "<th class='" + sColorCode + "'>Adjust Purchase Bill No</th>";
        sTable += "<th class='" + sColorCode + "'>Adjust Purchase TotalAmount</th>";
        sTable += "<th class='" + sColorCode + "'>TDS %</th>";
        sTable += "<th class='" + sColorCode + "'>RoundOff</th>";
        sTable += "<th class='" + sColorCode + "'>TDS Amount</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                $("#txtSNo").val(sCount + 1);
                sTable += "<td>" + gData[i].oldPurchaseEntry.BillNo + "</td>";
                sTable += "<td>" + gData[i].oldPurchaseEntry.NetAmount + "</td>";
                sTable += "<td>" + gData[i].PurchaseEntry.BillNo + "</td>";
                sTable += "<td>" + gData[i].PurchaseEntry.NetAmount + "</td>";
                sTable += "<td>" + gData[i].TDSPercent + "</td>";
                sTable += "<td>" + gData[i].Roundoff + "</td>";
                sTable += "<td>" + gData[i].TDSAmount + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_TDSPaymentDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
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

function Bind_TDSPaymentByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlProductName").focus();
    var sCount = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            if (data[i].StatusFlag != "D") {
                $("#hdnOPSNo").val(ID);
                $("#txtSNo").val(ID);
                // $("#hdnTDSPaymentID").val(data[i].TDSPaymentID);
                $("#hdnTDSPaymentTransID").val(data[i].TDSPaymentTransID);
                $("#ddlProductName").val(data[i].PurchaseEntry.PurchaseID).change();
                $("#ddlOldSalesInvoice").val(data[i].oldPurchaseEntry.PurchaseID).change();
                $("#txtRate").val(data[i].TDSPercent);
                $("#txtRoundoffTrans").val(data[i].Roundoff);
                $("#txtDisPer").val(data[i].TDSAmount);
            }
        }
    }
    return false;
}


function Update_TDSPayment(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].TDSPaymentID = oData.TDSPaymentID;
            var oProduct = new Object();
            oProduct.PurchaseID = oData.PurchaseEntry.PurchaseID;
            oProduct.InvoiceNo = oData.PurchaseEntry.InvoiceNo;
            oProduct.NetAmount = oData.PurchaseEntry.NetAmount;
            gOPBillingList[i].PurchaseEntry = oProduct;

            var OldSales = new Object();
            OldSales.PurchaseID = oData.oldPurchaseEntry.PurchaseID;
            OldSales.InvoiceNo = oData.oldPurchaseEntry.InvoiceNo;
            OldSales.NetAmount = oData.oldPurchaseEntry.NetAmount;
            gOPBillingList[i].OldPurchase = OldSales;

            // gOPBillingList[i].TDSPaymentTransID = oData.TDSPaymentTransID;
            gOPBillingList[i].TDSPercent = oData.TDSPercent;
            gOPBillingList[i].Roundoff = oData.Roundoff;
            gOPBillingList[i].TDSAmount = oData.TDSAmount;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayTDSPaymentList(gOPBillingList);
    $("#btnAddTDSPayment").show();
    $("#btnUpdateTDSPayment").hide();
    ClearTDSPaymentFields();
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
                DisplayTDSPaymentList(gOPBillingList);
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
        url: "WebServices/VHMSService.svc/GetTopPurchaseTDSPayment",
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
                            var TypeStatus = ""; var table = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].BillType == "Purchase")
                                    table = "<tr style='background-color: #f5a3a3;' id='" + obj[index].TDSPaymentID + "'>";
                                else
                                    table = "<tr style='background-color: #bcfddf;'  id='" + obj[index].TDSPaymentID + "'>";
                               // var table = "<tr id='" + obj[index].TDSPaymentID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].TDSPaymentNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sTDSPaymentDate + "</td>";
                                table += "<td>" + obj[index].SlipNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sSlipDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                // table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='PrintReturn' title='Click here to Print TDSPayment'></i><i class='fa fa-print text-green'/></a></td>";

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
                                SetSessionValue("TDSPaymentID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintTDSPaymentInvoice.aspx", "MsgWindow");
                            });
                            $(".PrintSales").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnTDSPaymentID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("TDSPaymentID", AdmissionID);

                                var myWindow = window.open("PrintTDSPayment.aspx", "MsgWindow");
                                //PrintTDSPaymentDetails();
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
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "35%" },
                            { "sWidth": "10%" },
                            { "sWidth": "5%" },
                            { "sWidth": "5%" },
                            // { "sWidth": "5%" },
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
        url: "WebServices/VHMSService.svc/SearchPurchaseTDSPayment",
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
                            var TypeStatus = ""; var table = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].BillType == "Purchase")
                                    table = "<tr style='background-color: #f5a3a3;' id='" + obj[index].TDSPaymentID + "'>";
                                else
                                    table = "<tr style='background-color: #bcfddf;'  id='" + obj[index].TDSPaymentID + "'>";
                                //var table = "<tr id='" + obj[index].TDSPaymentID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].TDSPaymentNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sTDSPaymentDate + "</td>";
                                table += "<td>" + obj[index].SlipNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sSlipDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }
                                // table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";
                                //   table += "<td style='text-align:center;'><a href='#' id=" + obj[index].TDSPaymentID + " class='PrintReturn' title='Click here to Print TDSPayment'></i><i class='fa fa-print text-green'/></a></td>";

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
                                SetSessionValue("TDSPaymentID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintTDSPaymentInvoice.aspx", "MsgWindow");
                            });
                            $(".PrintSales").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnTDSPaymentID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("TDSPaymentID", AdmissionID);

                                var myWindow = window.open("PrintTDSPayment.aspx", "MsgWindow");
                                //PrintTDSPaymentDetails();
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
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "55%" },
                            { "sWidth": "10%" },
                            { "sWidth": "5%" },
                            { "sWidth": "5%" },
                            //  { "sWidth": "5%" },
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
    var iTDSPaymentAmount = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0, iGrossTotal = 0, iQty = 0;
    var iTotal = 0;
    var DisPercent = 0;

    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iTDSPaymentAmount = iTDSPaymentAmount + parseFloat(gOPBillingList[i].TDSAmount);
        }
    }
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    $("#txtTotalQty").val((parseFloat(iTDSPaymentAmount) + parseFloat(iround)).toFixed(2));
}

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    if ($("#ddlCustomerName").val() == "0" || $("#ddlCustomerName").val() == undefined || $("#ddlCustomerName").val() == null) {
        $.jGrowl("Please select Customer Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomer").addClass('has-error'); $("#ddlCustomerName").focus(); return false;
    }

    if ($("#txtSlipDate").val().trim() == "" || $("#txtSlipDate").val().trim() == undefined) {
        $.jGrowl("Please select Slip Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSlipDate").addClass('has-error'); $("#txtSlipDate").focus(); return false;
    } else { $("#divSlipDate").removeClass('has-error'); }

    if ($("#txtSlipNo").val().trim() == "" || $("#txtSlipNo").val().trim() == undefined) {
        $.jGrowl("Please enter Slip No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSlipNo").addClass('has-error'); $("#txtSlipNo").focus(); return false;
    }
    else { $("#divSlipNo").removeClass('has-error'); }

    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Sales TDS has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#ddlProductName").focus(); return false;
    }

    var ObjTDSPayment = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#ddlCustomerName").val();
    ObjTDSPayment.Customer = ObjCustomer;

    ObjTDSPayment.TDSPaymentID = 0;
    ObjTDSPayment.TDSPaymentNo = $("#txtReturnNo").val().trim();
    ObjTDSPayment.PaymentType = $("#ddlManualPayment").val();
    ObjTDSPayment.BillType = $("#ddlBillType").val(); 
    ObjTDSPayment.sTDSPaymentDate = $("#txtReturnDate").val().trim();
    ObjTDSPayment.Roundoff = parseFloat($("#txtRoundoff").val());
    ObjTDSPayment.TotalAmount = parseFloat($("#txtTotalQty").val());
    ObjTDSPayment.Narration = $("#txtAddress").val().trim();
    ObjTDSPayment.SlipNo = $("#txtSlipNo").val().trim();
    ObjTDSPayment.sSlipDate = $("#txtSlipDate").val().trim();
    ObjTDSPayment.DocumentPath = $("#hdnImgupload1").val();

    ObjTDSPayment.PurchaseTDSPaymentTrans = gOPBillingList;
    //ObjTDSPayment.TDSPaymentID = $("#hdnTDSPaymentID").val();

    if ($("#hdnTDSPaymentID").val() > 0) {
        ObjTDSPayment.TDSPaymentID = $("#hdnTDSPaymentID").val();
        gOPBillingList.TDSPaymentID = ObjTDSPayment.TDSPaymentID;
        // ObjTDSPayment.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdatePurchaseTDSPayment";
    }
    else {
        sMethodName = "AddPurchaseTDSPayment";
        ObjTDSPayment.TDSPaymentID = 0
    }

    SaveandUpdateTDSPayment(ObjTDSPayment, sMethodName);

});
function SaveandUpdateTDSPayment(ObjTDSPayment, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjTDSPayment }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearTDSPaymentTab();
                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divTDSPayment").hide();
                        if (sMethodName == "AddPurchaseTDSPayment") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            // $("#hdnTDSPaymentID").val(objResponse.Value);
                            //SetSessionValue("TDSPaymentID", $("#hdnTDSPaymentID").val());
                            //var myWindow = window.open("PrintTDSPaymentInvoice.aspx", "MsgWindow");

                        }
                        else if (sMethodName == "UpdatePurchaseTDSPayment") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                        $("#hdnTDSPaymentID").val("0");
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
    SetSessionValue("TDSPaymentID", $("#hdnTDSPaymentID").val());
    var myWindow = window.open("PrintTDSPaymentInvoice.aspx", "MsgWindow");
});
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseTDSPaymentByID",
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
                            $("#txtInvoiceNo").attr("disabled", true);
                            $("#hdnTDSPaymentID").val(obj.TDSPaymentID)
                            $("#ddlManualPayment").val(obj.PaymentType).change();
                            $("#ddlBillType").val(obj.BillType).change();
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#txtTotalQty").val(obj.TotalAmount);
                            $("#txtAddress").val(obj.Narration);
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtReturnNo").val(obj.TDSPaymentNo);
                            $("#txtReturnDate").val(obj.sTDSPaymentDate);
                            $("#txtSlipNo").val(obj.SlipNo);
                            $("#txtSlipDate").val(obj.sSlipDate);
                            $("[id*=imgUpload1]").attr("src", obj.DocumentPath);

                            gOPBillingList = [];
                            var ObjProduct = obj.PurchaseTDSPaymentTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";
                                var objProduct = new Object();
                                objProduct.PurchaseID = ObjProduct[index].PurchaseEntry.PurchaseID;
                                objProduct.BillNo = ObjProduct[index].PurchaseEntry.BillNo;
                                objProduct.NetAmount = ObjProduct[index].PurchaseEntry.NetAmount;
                                objTemp.PurchaseEntry = objProduct;

                                var OldSalesInvoice = new Object();
                                OldSalesInvoice.PurchaseID = ObjProduct[index].oldPurchaseEntry.PurchaseID;
                                OldSalesInvoice.BillNo = ObjProduct[index].oldPurchaseEntry.BillNo;
                                OldSalesInvoice.NetAmount = ObjProduct[index].oldPurchaseEntry.NetAmount;
                                objTemp.oldPurchaseEntry = OldSalesInvoice;

                                objTemp.TDSPercent = ObjProduct[index].TDSPercent;
                                objTemp.Roundoff = ObjProduct[index].Roundoff;
                                objTemp.TDSAmount = ObjProduct[index].TDSAmount;
                                objTemp.TDSPaymentTransID = ObjProduct[index].TDSPaymentTransID;
                                objTemp.TDSPaymentID = ObjProduct[index].TDSPaymentID;

                                AddTDSPaymentData(objTemp);

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
        url: "WebServices/VHMSService.svc/DeletePurchaseTDSPayment",
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
                    else if (objResponse.Value == "TDSPayment_R_01" || objResponse.Value == "TDSPayment_D_01") {
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
