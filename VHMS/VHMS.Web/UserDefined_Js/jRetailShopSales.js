var gMagazineData = [];
var gOPBillingList = [];
var gSalesReturnList = [];
var gExchangeList = [];
var gPaymentList = [];
var BalanceAmount = 0;
var SerialNo = 0;
var row = 0;
$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#divParticular").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    GetProductList("ddlProductName");

    GetProductList("ddlProduct");
    GetCustomerList("ddlCustomerName");
    GetTaxList("ddlTaxName");
    GetTaxList("ddlTax");
    GetCustomerType();
    GetPassword();
    GetStateList();
    GetBankList("ddlBankName");
    $("#ddlTaxName").change();
    $("#txtBillDate, #txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtBillDate, #txtCollectionDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    $("#txtDeliveryDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtDeliveryDate").datetimepicker({
        pickTime: false,
        useCurrent: false,
        //maxDate: moment(),
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

            GetReceivedSalesEntry(parseInt($.cookie("SalesEntryID")));
            $("#hdnSalesEntryID").val(parseInt($.cookie("SalesEntryID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("SalesEntryID", null);
    }
    $("#divCardDetails").hide();
    pLoadingSetup(true);
    GetSettings();
    SerialNo = $("#participants").val();
     row = $(".participantRow");
    GetRecord();

});

function getP() {
    SerialNo = $("#participants").val();
}

function addRow() {
    row.clone(true, true).appendTo("#participantTable");
}

//$("#participants").change(function () {
//    var i = 0;
//    SerialNo = $("#participants").val();
//    var rowCount = $("#participantTable tr").length - 2;
//    if (SerialNo > rowCount) {
//        for (i = rowCount; i < SerialNo; i += 1) {
//            addRow();
//        }
//        $("#participantTable #addButtonRow").appendTo("#participantTable");
//    } else if (SerialNo < rowCount) {
//    }
//});

$(".add").on('click', function () {
    getP();
    if ($("#participantTable tr").length < 50) {
       
        var J = Number(SerialNo) + 1;
        addRow();
        
        //$("#participants").val(i);
        
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


function GetPassword(id) {
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

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}

$("#txtRoundoff").keypress(function (e) {
    if (e.which != 46 && e.which != 45 && e.which != 46 &&
        !(e.which >= 48 && e.which <= 57)) {
        return false;
    }
});

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    console.log(BalanceAmount);
    $("#hdnSalesEntryID").val("0");
    $("#txtPasswords").val("");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    $("#divTab").hide();
    $("#divExchange").hide();
    $("#composedialog").hide();
    $("#divOPBilling").show();
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    $("#ddlTaxName").change();
    $("#txtSNo").val("1");
    gOPBillingList = [];
    gSalesReturnList = [];
    gExchangeList = [];
    gPaymentList = [];
    ClearOPBillingTab();
    $("#ddlTaxName,#ddlGift").prop('disabled', false);
    $("#txtDiscountPercent,#txtDiscountAmount").prop('disabled', false);
    $("#txtAdditionalDiscountAmount,#divAdditionalDiscount").hide();
    $("#divOPBillingList").empty();
    $("#divPaymentList").empty();
    $("#ddlTax").change();
    $("#txtDiscountPercent").val(0);
    $("#txtDiscountAmount").val(0);
    $("#txtDisPer").val(0);
    $("#txtDisAmt").val(0);
    $("#txtBillDate").focus();
    $("#txtBillNo").focus();
    //$("#ddlTaxName").val(0).change();
    //$("#ddlTax").val(0).change();
    $("#imagefile2").val("");
    $("#imagefile3").val("");
    $("#imagefile4").val("");
    return false;
});


$("#btnClearImage1").click(function () {
    $get("imgUpload2_view").src = "";
    $("#imagefile2").val("");
});

$("#btnClearImage2").click(function () {
    $get("imgUpload3_view").src = "";
    $("#imagefile3").val("");
});

$("#btnClearImage3").click(function () {
    $get("imgUpload4_view").src = "";
    $("#imagefile4").val("");
});


$("#chkExchange").change(function () {
    if (this.checked)
        $("#divExchange").show();
    else
        $("#divExchange").hide();
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
    $("#hdnSalesEntryID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});
function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    var currentdate = new Date();
    var nowMonth = currentdate.getDate() + '-' + ((currentdate.getMonth() + 1) < 10 ? '0' : '') + (currentdate.getMonth() + 1) + '-' + currentdate.getFullYear();
    var ThisDate = nowMonth.toString();
    $("#txtDeliveryDate").val(ThisDate);

    $("#txtDeliveryDate").attr("disabled", "disabled");
    $("#txtArea").val("");
    $("#txtMobileNo").val("");
    $("#txtCustomerType").val("");
    $("#txtCustomer").val("");
    $("#txtAddress").val("");
    $("#ddlCustomerType").val(null).change();
    $("#ddlState").val(null).change();
    $("#ddlProductName").val(null).change();
    $("#ddlPaymentMode").val("Cash").change();
    $("#txtTotalQuantity").val("0");
    $("#txtAdditionalDiscountAmount").val("0");
    $("#txtName").val("");
    $("#txtExchangeAmount").val("0")
    $("#txtNarration").val("");
    $("#divBank").hide();
    $("#txtOPDNo").val("");
    gOPBillingList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#ddlTax").val($("#ddlTax option:first").val());
    $("#ddlTaxName").val($("#ddlTaxName option:first").val());
    $get("imgUpload2_view").src = "";
    $get("imgUpload3_view").src = "";
    $get("imgUpload4_view").src = "";
    $("[id*=imgUpload2_view]").css("visibility", "hidden");
    $("[id*=imgUpload3_view]").css("visibility", "hidden");
    $("[id*=imgUpload4_view]").css("visibility", "hidden");
    ClearOPBillingFields();
    GetProductList("ddlProductName");

    $("#ddlProductName").val(null).change();
    $("#txtBillNo").attr("disabled", false);
    $("#txtCode").val("");
    $("#divCardDetails").hide();
    return false;
}

$("#ddlPaymentMode").change(function () {
    if ($("#ddlPaymentMode").val() == "Card") {
        $("#divBank").show();
        $("#divCardDetails").show();
    }
    else {
        $("#divBank").hide();
        $("#divCardDetails").hide();
    }
});

function GetStateList() {
    $("#ddlState").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetState",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CountryID: 0 }),
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
                                $("#ddlState").append('<option value=' + obj[index].StateID + ' >' + obj[index].StateName + '</option>');
                            }
                            $("#ddlState").val($("#ddlState option:first").val());
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetCustomerType() {
    $("#ddlCustomerType").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomertypeName",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ TypeNames: "Retail" }),
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
                                $("#ddlCustomerType").append('<option value=' + obj[index].CustomertypeID + ' >' + obj[index].CustomerTypeName + '</option>');
                            }
                            $("#ddlCustomerType").val($("#ddlCustomerType option:first").val());
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlCustomerType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlCustomerType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}
function GetGiftList(value) {
    var sControlName = "#ddlGift";
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetGiftAmount",
        data: JSON.stringify({ Amount: value }),
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
                                    if ($("#txtNetAmount").val() > 0)
                                        $(sControlName).append("<option value='" + obj[index].GiftID + "'>" + obj[index].GiftName + "</option>");
                                    else {

                                        $(sControlName).append("<option value='" + obj[index].GiftID + "'>" + obj[index].GiftName + "</option>");

                                    }
                            }
                            $(sControlName).val($("#ddlGift option:first").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $(sControlName).val(0).change();
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

$("#txtBarcode").change(function () {
    if ($("#txtBarcode").val().length > 4) {
        $("#txtCode").val("");
        GetProductByBarcodeList("ddlProductName");
        //ClearOPBillingFields();
        if ($("#ddlProductName").val() > 0)
            GetRate();
    }
    else
        ClearOPBillingFields();
});


$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().length > 2) {
        GetProductByCodeList("ddlProductName");
        if ($("#ddlProductName").val() > 0)
            GetRate();
    }
    else if ($("#txtCode").val().length == 0) {
        GetProductList("ddlProductName");
        ClearOPBillingFields();
        if ($("#ddlProductName").val() > 0)
            GetRate();
    }
});
$("#ddlCustomerName").change(function () {
    if ($("#ddlCustomerName").val() > 0) {
        GetCustomerByID($("#ddlCustomerName").val());
        GetRate();
    }
});

$("#ddlBillStatus").change(function () {
    if ($("#ddlBillStatus").val() == "Booking Bill")
        $("#txtDeliveryDate").removeAttr("disabled");
    else
        $("#txtDeliveryDate").attr("disabled", "disabled");
});

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0)
        GetRate();
});

$("#txtDiscountAmount").change(function () {
    calculateDiscount();
});

$("#txtAdditionalDiscountAmount").change(function () {
    calculateAdditionalDiscount();
    $("#txtPasswords").val("");

});


function calculateAdditionalDiscount() {
    $("#txtDiscountAmount").val(parseFloat($("#hdnDiscountAmount").val()) - parseFloat($("#hdnAddDiscountAmount").val())).change();
    var iOPBillingAmount = 0, iBillingDiscount = 0, iABillingDiscount = 0;

    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate));
        }
    }

    var iABillingDiscount = parseFloat($("#txtAdditionalDiscountAmount").val());
    if (isNaN(iABillingDiscount)) iABillingDiscount = 0;
    var iBillingDiscount = parseFloat($("#txtDiscountAmount").val());
    if (isNaN(iBillingDiscount)) iBillingDiscount = 0;
    iBillingDiscount = parseFloat(iBillingDiscount) + parseFloat(iABillingDiscount);
    $("#txtDiscountPercent").val(parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).change();
    $("#txtDiscountPercent").val((parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).toFixed(2));
}

function calculateDiscount() {
    var iOPBillingAmount = 0, iBillingDiscount = 0, iABillingDiscount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate));
        }
    }

    var iBillingDiscount = parseFloat($("#txtDiscountAmount").val());
    if (isNaN(iBillingDiscount)) iBillingDiscount = 0;
    $("#txtDiscountPercent").val(parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).change();
    $("#txtDiscountPercent").val((parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).toFixed(2));
}

function GetRate() {
    if ($("#hdnCustomerID").val().length <= 0)
        $("#hdnCustomerID").val("0");
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetRetailProductRate",
            data: JSON.stringify({ ID: $("#ddlProductName").val(), type: $("#txtCode").val().trim(), SupplierID: $("#hdnCustomerID").val() }),
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
                                if ($("#txtCode").val().length > 0)
                                    $("#hdnSMSCode").val(obj.SMSCode);
                                else
                                    $("#txtCode").val(obj.SMSCode);
                                if (obj.Type == "Y")
                                    $.jGrowl("Rate has been updated", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                            $("#ddlTax").val($("#ddlTax option:first").val());
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

function GetCustomerList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
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
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].CustomerID + "'>" + obj[index].CustomerName + "</option>");
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

function GetProductByBarcodeList(ddlname) {
    if ($("#txtBarcode").val().length > 3) {
        var sControlName = "#" + ddlname;
        dProgress(true);
        $(sControlName).empty();
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByBarcode",
            data: JSON.stringify({ ProductCode: $("#txtBarcode").val(), SMSOnly: 0 }),
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

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#txtRate,#txtQuantity, #txtDisAmt, #txtDisPer").change(function () {
    CalculateSubtotal();
});

$("#txtDisPer").change(function () {

    CalculateSubtotal();
});

$("#txtExchangeBillNo").blur(function () {
    if ($("#txtExchangeBillNo").val().length > 0) {
        GetSalesReturnDetails($("#txtExchangeBillNo").val());
    }
});

$("#ddlExchangeProductName").change(function () {

    if ($("#ddlExchangeProductName").val() > 0) {
        var ProdID = $("#ddlExchangeProductName").val();
        for (var i = 0; i < gSalesReturnList.length; i++) {
            if (gSalesReturnList[i].Product.ProductID == ProdID) {
                $("#hdnExchangeCode").val(gSalesReturnList[i].Product.SMSCode);
                $("#txtExchangeQuantity").val(0);
                $("#txtExchangeRate").val(gSalesReturnList[i].Rate);
                $("#hdnExchangeTaxID").val(gSalesReturnList[i].Tax.TaxID);
                $("#hdnExchangeTaxPercent").val(gSalesReturnList[i].Tax.TaxPercentage);
                $("#hdnExchangeCGSTPercent").val(gSalesReturnList[i].Tax.CGSTPercent);
                $("#hdnExchangeSGSTPercent").val(gSalesReturnList[i].Tax.SGSTPercent);
                $("#hdnExchangeIGSTPercent").val(gSalesReturnList[i].Tax.IGSTPercent);
                $("#txtExchangeDisPer").val(gSalesReturnList[i].DiscountPercentage);
                $("#hdnExchangeBarcode").val(gSalesReturnList[i].Barcode);
                $("#hdnPreQtyID").val(gSalesReturnList[i].Quantity);

                CalculateExchange();
            }
        }
    }

});

$("#txtExchangeQuantity").change(function () {
    CalculateExchange();
});
function CalculateExchange() {

    var iTax = parseFloat($("#hdnExchangeTaxPercent").val());
    var iRate = parseFloat($("#txtExchangeRate").val());
    var iqty = parseFloat($("#txtExchangeQuantity").val());
    var iCGST = parseFloat($("#hdnExchangeCGSTPercent").val());
    var iSGST = parseFloat($("#hdnExchangeSGSTPercent").val());
    var iIGST = parseFloat($("#hdnExchangeIGSTPercent").val());
    var iDiscountPercent = parseFloat($("#txtExchangeDisPer").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDiscountPercent)) iDiscountPercent = 0;
    if (isNaN(iTax)) iTax = 0;
    var iDisAmt = (parseFloat(iRate) * parseFloat(iqty) * parseFloat(iDiscountPercent) / 100);
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty)) - iDisAmt;

    var Id = $("#hdnStateCode").val();
    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iSubTotal) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iSubTotal) * parseFloat(iSGST) / 100;
        $("#hdnExchangeSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnExchangeCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnExchangeIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iSubTotal) * parseFloat(iIGST) / 100;
        $("#hdnExchangeSGSTAmount").val(0);
        $("#hdnExchangeCGSTAmount").val(0);
        $("#hdnExchangeIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    var iTaxPercent = parseFloat($("#hdnExchangeSGSTAmount").val()) + parseFloat($("#hdnExchangeCGSTAmount").val()) + parseFloat($("#hdnExchangeIGSTAmount").val());
    $("#txtExchangeDisAmt").val(parseFloat(iDisAmt).toFixed(2));
    $("#txtExchangeTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));
    $("#txtExchangeSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

function GetSalesReturnDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByInvoice",
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
                            $("#hdnCustomerID").val(obj.Customer.CustomerID);
                            $("#hdnSalesID").val(obj.SalesEntryID);
                            gSalesReturnList = [];
                            var ObjProduct = obj.SalesEntryTrans;
                            $("#ddlExchangeProductName").empty();
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                var objTax = new Object();
                                objTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTax;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                $("#ddlExchangeProductName").append("<option value='" + ObjProduct[index].Product.ProductID + "'>" + ObjProduct[index].Product.ProductName + "</option>");
                                var objProducts = new Object();
                                objProducts.ProductID = ObjProduct[index].Product.ProductID;
                                objProducts.ProductName = ObjProduct[index].Product.ProductName;
                                objProducts.ProductCode = ObjProduct[index].Product.ProductCode;
                                objProducts.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objProducts;

                                gSalesReturnList.push(objTemp);
                            }
                            $("#ddlExchangeProductName").val($("#ddlExchangeProductName option:first").val()).change();
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

function CalculateSubtotal() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());
    var iDiscountPercent = parseFloat($("#txtDisPer").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDiscountPercent)) iDiscountPercent = 0;
    if (isNaN(iTax)) iTax = 0;
    var iDisAmt = (parseFloat(iRate) * parseFloat(iqty) * parseFloat(iDiscountPercent) / 100);
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty)) - iDisAmt;
    var Id = $("#hdnStateCode").val();
    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iSubTotal) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iSubTotal) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iSubTotal) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    $("#txtDisAmt").val(parseFloat(iDisAmt).toFixed(2));
    var iTaxPercent = parseFloat($("#hdnTransCGSTAmount").val()) + parseFloat($("#hdnTransSGSTAmount").val()) + parseFloat($("#hdnTransIGSTAmount").val());
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}


$("#txtSearchCode").change(function () {
    if ($("#txtSearchCode").val().length > 2) {
        GetProductByCode("ddlProduct");
        if ($("#ddlProduct").val() > 0)
            GetRateByProduct();
    }
    else if ($("#txtSearchCode").val().length == 0) {
        GetProductList("ddlProduct");
        if ($("#ddlProduct").val() > 0)
            GetRateByProduct();
    }
});

$("#ddlProduct").change(function () {
    if ($("#ddlProduct").val() > 0)
        GetRateByProduct();
});

$("#txtMobileNo").change(function () {
    if ($("#txtMobileNo").val().length == 10) {
        GetCustomerByCode();
        var ID = $("#hdnCustomerID").val();
        if (ID > 0)
            GetCustomerByID($("#hdnCustomerID").val());
    }
    else {
        $("#txtCustomer").val("");
        $("#txtAddress").val("")
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
                                        gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                                    }
                                    gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
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

function GetCustomerByCode(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomerByCode",
        data: JSON.stringify({ ID: $("#txtMobileNo").val() }),
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
                            $("#hdnCustomerID").val(obj.CustomerID);
                            $("#txtCustomer").val(obj.CustomerName);
                            $("#txtAddress").val(obj.Address);
                            $("#ddlCustomerType").val(obj.CustomerTypes.CustomertypeID).change();
                            $("#ddlState").val(obj.State.StateID).change();
                            $("#txtArea").val(obj.Area);
                        }
                        else {
                            $("#ddlCustomerType").val($("#ddlCustomerType option:first").val()).change();
                            $("#ddlState").val(33).change();
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("NoRecord", { sticky: true, theme: 'danger', life: jGrowlLife });
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
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
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

function GetRateByProduct() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRateByProduct",
        data: JSON.stringify({ ID: $("#ddlProduct").val() }),
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
                            $("#txtWholeSalePrice").val(obj.Rate);
                            $("#txtRetailPrice").val(obj.RetailRate);
                            $("#txtRetailSalePriceA").val(obj.RetailPriceA);
                            $("#txtRetailSalePriceB").val(obj.RetailPriceB);
                            $("#txtRetailSalePriceC").val(obj.RetailPriceC);
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

function GetProductByCode(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductByCode",
        data: JSON.stringify({ ProductCode: $("#txtSearchCode").val(), SMSOnly: 0 }),
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

$("#ddlPaymentMode").change(function () {
    if ($("#ddlPaymentMode").val() == "Cash" || $("#ddlPaymentMode").val() == "Credit") {
        $("#ddlBankName").attr("disabled", "disabled");
        $("#ddlBankName").val(1).change();
        $("#txtCollectionDate").attr("disabled", "disabled");
        $("#txtCardCharges").attr("disabled", "disabled");
        $("#txtCardNo").attr("disabled", "disabled");
    }
    else {
        //if ($('txtCardNo').prop('disabled'))
        $("#txtCardNo").removeAttr("disabled");
        $("#txtCardCharges").removeAttr("disabled");
        $("#txtCollectionDate").removeAttr("disabled");
        $("#ddlBankName").removeAttr("disabled");
    }
    return false;
});

//#region PaymentTrans
$("#btnAddPayment,#btnUpdatePayment").click(function () {
    if ($("#ddlPaymentMode").val() != "Cash" && $("#ddlPaymentMode").val() != "Credit") {
        if ($("#ddlBankName").val() == "0" || $("#ddlBankName").val() == undefined || $("#ddlBankName").val() == null) {
            $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divBankName").addClass('has-error'); $("#ddlBankName").focus(); return false;
        } else { $("#divBankName").removeClass('has-error'); }

        if ($("#txtPaidAmount").val().trim() == "" || $("#txtPaidAmount").val().trim() == undefined) {
            $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divPaidAmount").addClass('has-error'); $("#txtPaidAmount").focus(); return false;
        } else { $("#divPaidAmount").removeClass('has-error'); }

        if (!isNaN($("#txtPaidAmount").val())) {
            if ($("#txtPaidAmount").val() == 0) {
                $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPaidAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
            }
        }
    }

    var iStockCount = 0; var StockValue = 0; var StockQty = 0;
    for (var i = 0; i < gPaymentList.length; i++) {
        if (gPaymentList[i].StatusFlag != "D") {
            if (gPaymentList[i].PaymentMode == $("#ddlPaymentMode").val()) {
                iStockCount = iStockCount + 1;
            }
        }
    }

    if (this.id == "btnAddPayment") {
        if (iStockCount > 0) {
            $.jGrowl("Payment Mode already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divPaymentMode").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
        } else { $("#divPaymentMode").removeClass('has-error'); }
    }
    else {
        if (iStockCount > 1) {
            $.jGrowl("Payment Mode already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divPaymentMode").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
        } else { $("#divPaymentMode").removeClass('has-error'); }
    }

    var ObjData = new Object();
    ObjData.RetailPaymentModeID = 0;

    ObjData.PaymentMode = $("#ddlPaymentMode").val();
    ObjData.Amount = parseFloat($("#txtPaidAmount").val());
    ObjData.Charges = parseFloat($("#txtCardCharges").val());
    ObjData.ChequeNo = $("#txtCardNo").val().trim();
    ObjData.IssuedBy = $("#txtNotes").val();
    ObjData.BankID = $("#ddlBankName").val();



    if ($("#ddlPaymentMode").val() == "Cash") {
        var currentdate = new Date();
        var nowMonth = currentdate.getDate() + '-' + ((currentdate.getMonth() + 1) < 10 ? '0' : '') + (currentdate.getMonth() + 1) + '-' + currentdate.getFullYear();
        var ThisDate = nowMonth.toString();
        ObjData.sCollectionDate = ThisDate;
        ObjData.BankName = "";
    }
    else {
        ObjData.BankName = $("#ddlBankName option:selected").text();
        ObjData.sCollectionDate = $("#txtCollectionDate").val();
    }

    ObjData.sIssueDate = "";

    if (ObjData.PaymentMode == "Credit") {
        BalanceAmount = BalanceAmount + ObjData.Amount;
    }

    if (ObjData.PaymentMode == "DebitCard" || ObjData.PaymentMode == "CreditCard" || ObjData.PaymentMode == "Cheque") {
        ObjData.Status = "Pending";
    } else {
        ObjData.Status = "Closed";
    }

    // console.log(BalanceAmount);
    if (this.id == "btnAddPayment") {
        ObjData.sNO = gPaymentList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.RetailPaymentModeID = 0;
        ObjData.StatusFlag = "I";

        AddPayment(ObjData);
    }
    else if (this.id == "btnUpdatePayment") {
        ObjData.sNO = $("#hdnPaymentSNo").val();
        if ($("#hdnSalesEntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesEntryID = $("#hdnSalesEntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesEntryID = 0;
        }
        Update_Payment(ObjData);
    }
    CalculateAmount();
    ClearPaymentFields();
    $("#ddlExchangeProductName").focus();
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
});

function AddPayment(oData) {
    gPaymentList.push(oData);
    DisplayPaymentList(gPaymentList);
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
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Mode</th>";
        sTable += "<th class='" + sColorCode + "'>Bank</th>";
        sTable += "<th class='" + sColorCode + "'>Ref. No</th>";
        sTable += "<th class='" + sColorCode + "'>Collection Date</th>";
        sTable += "<th class='" + sColorCode + "'>Amount</th>";
        sTable += "<th class='" + sColorCode + "'>Charges</th>";
        sTable += "<th class='" + sColorCode + "'>Note</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblPaymentList_body'>";
        sTable += "</tbody></table>";
        $("#divPaymentList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].PaymentMode + "</td>";
                sTable += "<td>" + gData[i].BankName + "</td>";
                sTable += "<td>" + gData[i].ChequeNo + "</td>";
                if (gData[i].PaymentMode != "Credit" && gData[i].sCollectionDate != "01-01-1900")
                    sTable += "<td>" + gData[i].sCollectionDate + "</td>";
                else
                    sTable += "<td></td>";
                sTable += "<td>" + gData[i].Amount + "</td>";
                sTable += "<td>" + gData[i].Charges + "</td>";
                sTable += "<td>" + gData[i].IssuedBy + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_Payment(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_Payment(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblPaymentList_body").append(sTable);
            }
        }
    }
    else { $("#divPaymentList").empty(); }

    return false;
}

function Edit_Payment(ID) {
    Bind_PaymentByID(ID, gPaymentList);
    return false;
}

function Bind_PaymentByID(ID, data) {
    $("#btnAddPayment").hide();
    $("#btnUpdatePayment").show();
    $("#ddlPaymentMode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnPaymentSNo").val(ID);
            $("#hdnSalesEntryID").val(data[i].SalesEntryID);
            $("#ddlPaymentMode").val(data[i].PaymentMode).change();
            $("#ddlBankName").val(data[i].BankID).change();
            $("#txtCardNo").val(data[i].ChequeNo);
            $("#txtNotes").val(data[i].IssuedBy);
            $("#txtNotes").val(data[i].IssuedBy);
            $("#txtCardCharges").val(data[i].Charges);
            $("#txtPaidAmount").val(data[i].Amount);
            //if (data[i].PaymentMode != 'Credit')
            //{
            //    $("#txtCollectionDate").val(data[i].sCollectionDate);
            //}

        }
    }
    return false;
}

function Update_Payment(oData) {
    for (var i = 0; i < gPaymentList.length; i++) {
        if (gPaymentList[i].sNO == oData.sNO) {
            gPaymentList[i].SalesEntryID = oData.SalesEntryID;
            gPaymentList[i].BankID = oData.BankID;
            gPaymentList[i].BankName = oData.BankName;
            gPaymentList[i].ChequeNo = oData.ChequeNo;
            gPaymentList[i].IssuedBy = oData.IssuedBy;
            gPaymentList[i].sIssueDate = oData.sIssueDate;
            gPaymentList[i].Amount = oData.Amount;
            gPaymentList[i].Charges = oData.Charges;
            gPaymentList[i].PaymentMode = oData.PaymentMode;
            gPaymentList[i].sCollectionDate = oData.sCollectionDate;
            gPaymentList[i].StatusFlag = oData.StatusFlag;
            gPaymentList[i].Status = oData.Status;
        }
    }
    DisplayPaymentList(gPaymentList);
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    ClearPaymentFields();
    $("#ddlPaymentMode").focus();
    return false;
}

function Delete_Payment(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gPaymentList.length; i++) {
            if (gPaymentList[i].SNo == ID) {
                var index = jQuery.inArray(gPaymentList[i].valueOf("SNo"), gPaymentList);
                if (gPaymentList[i].SNo > 0) {
                    gPaymentList[i].StatusFlag = "D";
                } else {
                    gPaymentList.splice(index, 1);
                }
                $("#divPaymentList").empty();
                DisplayPaymentList(gPaymentList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function ClearPaymentFields() {
    $("#btnAddPayment").show();
    $("#btnUpdatePayment").hide();
    $("#ddlPaymentMode").val("Cash").change();
    $("#ddlBankName").val($("#ddlBankName option:first").val());
    //$("#ddlBankName").val(0).change();
    $("#txtCardNo").val("");
    $("#txtCollectionDate").val("");
    //$("#txtPaidAmount").val("0");
    $("#txtCardCharges").val("0");
    $("#txtNotes").val("");

    $("#divBankName").removeClass('has-error');
    $("#divPaidAmount").removeClass('has-error');

    return false;
}


$("#btnCloseDialog").click(function () {
    GetPassword();

    if ($("#txtPasswords").val().trim() == "" || $("#txtPasswords").val().trim() == undefined || $("#txtPasswords").val().trim() != $("#hdRS").val()) {
        $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPasswords").addClass('has-error'); $("#txtPasswords").focus(); return false;
    } else { $("#divPasswords").removeClass('has-error'); }

    $('#composedialog').modal('hide');
    return false;
});

//#endregion PaymentTrans

//#region SalesTrans

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {
    var ig = $("#participants").val();
    for (var i = 0; i < ig; i++) {
        $("#participants").val(ig + 1);
    }
    //TaxCalculate();
    //if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
    //    $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    //}
    //else { $("#divProductName").removeClass('has-error'); }

    //if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <= 0) {
    //    $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    //} else { $("#divQuantity").removeClass('has-error'); }

    //if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null) {
    //    $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    //} else { $("#divRate").removeClass('has-error'); }

    //if ($("#txtDisPer").val() == "" || $("#txtDisPer").val() == undefined || $("#txtDisPer").val() == null) {
    //    $.jGrowl("Please enter Disc. Percent", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divDisPer").addClass('has-error'); $("#txtDisPer").focus(); return false;
    //} else { $("#divDisPer").removeClass('has-error'); }

    //if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
    //    $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divTaxTrans").addClass('has-error'); $("#ddlTax").focus(); return false;
    //}
    //else { $("#divTaxTrans").removeClass('has-error'); }

    //if ($("#txtDisAmt").val() == "" || $("#txtDisAmt").val() == undefined || $("#txtDisAmt").val() == null) {
    //    $.jGrowl("Please enter Disc. Percent", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divDisAmt").addClass('has-error'); $("#txtDisAmt").focus(); return false;
    //} else { $("#divDisAmt").removeClass('has-error'); }


    //if ($("#hdnMaxDiscount").val() > 0) {
    //    if (parseFloat($("#hdnMaxDiscount").val()) < parseFloat($("#txtDisPer").val())) {
    //        $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#txtDisPer").focus(); return false;
    //    }
    //}


    ////if ($("#hdnMaxDiscount").val() > 0) {
    ////    if (parseFloat($("#hdnMaxDiscount").val()) < parseFloat($("#txtDisPer").val())) {
    ////        console.log($("#txtPasswords").val());
    ////        if ($("#txtPasswords").val() >= 5) {
    ////            $(".modal-title").html("&nbsp;&nbsp;Discount Percentage Exceeding Maximum discount");
    ////            $('#composedialog').modal({ show: true, backdrop: 'static', keyboard: false });
    ////            $("#txtPasswords").focus(); return false;
    ////        }
    ////    }
    ////}

    //var iStockCount = 0; var StockValue = 0; var StockQty = 0; var WholesaleMinPrice = 0;
    //for (var i = 0; i < gOPBillingList.length; i++) {
    //    if (gOPBillingList[i].Product.ProductID == $("#ddlProductName").val()) {
    //        iStockCount = iStockCount + 1;
    //        StockQty = gOPBillingList[i].Quantity;
    //    }
    //}

    //$.ajax({
    //    type: "POST",
    //    url: "WebServices/VHMSService.svc/GetStockByID",
    //    data: JSON.stringify({ ID: $("#ddlProductName").val() }),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    async: false,
    //    success: function (data) {
    //        if (data.d != "") {
    //            var objResponse = jQuery.parseJSON(data.d);
    //            if (objResponse.Status == "Success") {
    //                if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
    //                    var obj = jQuery.parseJSON(objResponse.Value);
    //                    if (obj != null) {
    //                        StockValue = obj.Quantity;
    //                        WholesaleMinPrice = obj.RetailMinPrice;
    //                    }
    //                    dProgress(false);
    //                }
    //                else if (objResponse.Value == "NoRecord") {
    //                    dProgress(false);
    //                }
    //                else if (objResponse.Value == "Error") {
    //                    $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
    //                }
    //            }
    //            else if (objResponse.Status == "Error") {
    //                if (objResponse.Value == "0") {
    //                    window.location("frmLogin.aspx");
    //                }
    //                else if (objResponse.Value == "Error") {
    //                    window.location = "frmErrorPage.aspx";
    //                }
    //                else if (objResponse.Value == "NoRecord") {
    //                }
    //            }
    //        }
    //        else {
    //            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
    //            dProgress(false);
    //        }
    //    },
    //    error: function () {
    //        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
    //        dProgress(false);
    //    }
    //});

    //if (this.id == "btnAddMagazine" || $("#hdnSalesEntryID").val() == 0) {
    //    if (StockValue < $("#txtQuantity").val()) {
    //        $.jGrowl("Stock Not Available", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    //    }
    //}
    //else {
    //    if ((parseFloat(StockValue) + parseFloat(StockQty)) < $("#txtQuantity").val()) {
    //        $.jGrowl("Stock Not Available", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    //    }
    //}

    //if (parseFloat(WholesaleMinPrice) > parseFloat($("#txtRate").val())) {
    //    $.jGrowl("Rate is less than minimum margin", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    //}

    //var ObjData = new Object();
    //ObjData.SalesEntryID = 0;

    //var oProduct = new Object();

    //oProduct.ProductID = $("#ddlProductName").val();
    //oProduct.ProductName = $("#ddlProductName option:selected").text();
    //if ($("#txtCode").val() != "")
    //    oProduct.SMSCode = $("#txtCode").val().toUpperCase();
    //else
    //    oProduct.SMSCode = $("#hdnSMSCode").val().toUpperCase();
    //ObjData.Product = oProduct;

    //var oTaxTrans = new Object();

    //oTaxTrans.TaxID = $("#ddlTax").val();
    //oTaxTrans.TaxName = $("#ddlTax option:selected").text();
    //oTaxTrans.TaxPercentage = $("#hdnTransTaxPercent").val().trim();
    //if ($("#hdnStateCode").val() == 33) {
    //    oTaxTrans.CGSTPercent = $("#hdnTransCGSTPercent").val().trim();
    //    oTaxTrans.SGSTPercent = $("#hdnTransSGSTPercent").val().trim();
    //    oTaxTrans.IGSTPercent = 0;
    //}
    //else {
    //    oTaxTrans.CGSTPercent = 0;
    //    oTaxTrans.SGSTPercent = 0;
    //    oTaxTrans.IGSTPercent = $("#hdnTransIGSTPercent").val().trim();
    //}
    //ObjData.Tax = oTaxTrans;

    //ObjData.SGSTAmount = $("#hdnTransSGSTAmount").val().trim();
    //ObjData.CGSTAmount = $("#hdnTransCGSTAmount").val().trim();
    //ObjData.IGSTAmount = $("#hdnTransIGSTAmount").val().trim();

    //ObjData.TaxAmount = parseFloat($("#txtTaxAmt").val());

    //ObjData.Quantity = parseFloat($("#txtQuantity").val());
    //ObjData.Rate = parseFloat($("#txtRate").val());
    //ObjData.DiscountPercentage = parseFloat($("#txtDisPer").val());
    //ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());
    //ObjData.SubTotal = parseFloat($("#txtSubTotal").val());
    //ObjData.Barcode = $("#txtBarcode").val();
    //ObjData.NewProductFlag = 0;
    //if (this.id == "btnAddMagazine") {
    //    ObjData.sNO = gOPBillingList.max() + 1;
    //    ObjData.SNo = ObjData.sNO;
    //    ObjData.SalesEntryID = 0;
    //    ObjData.StatusFlag = "I";
    //    var Count = 0;
    //    for (var i = 0; i < gOPBillingList.length; i++) {
    //        if (gOPBillingList[i].StatusFlag != "D") {
    //            if ((gOPBillingList[i].Product.SMSCode == oProduct.SMSCode) && (gOPBillingList[i].Product.ProductID == $("#ddlProductName").val()) && (gOPBillingList[i].Rate == parseFloat($("#txtRate").val())) && (gOPBillingList[i].Tax.TaxID == $("#ddlTax").val()) && (gOPBillingList[i].DiscountPercentage == parseFloat($("#txtDisPer").val()))) {
    //                gOPBillingList[i].Quantity = gOPBillingList[i].Quantity + parseFloat($("#txtQuantity").val());
    //                var iDisPercent = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].DiscountPercentage) / 100;
    //                gOPBillingList[i].DiscountAmount = parseFloat(iDisPercent);
    //                gOPBillingList[i].SubTotal = gOPBillingList[i].SubTotal + parseFloat($("#txtSubTotal").val());

    //                if ($("#hdnStateCode").val() == 33) {
    //                    gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnTransCGSTPercent").val());
    //                    gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnTransSGSTPercent").val());
    //                    gOPBillingList[i].Tax.IGSTPercent = 0;
    //                    gOPBillingList[i].CGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
    //                    gOPBillingList[i].SGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
    //                    gOPBillingList[i].IGSTAmount = 0;
    //                }
    //                else {

    //                    gOPBillingList[i].Tax.CGSTPercent = 0;
    //                    gOPBillingList[i].Tax.SGSTPercent = 0;
    //                    gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnTransIGSTPercent").val());
    //                    gOPBillingList[i].CGSTAmount = 0;
    //                    gOPBillingList[i].SGSTAmount = 0;
    //                    gOPBillingList[i].IGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
    //                }
    //                gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);

    //                Count = 1;
    //            }
    //        }
    //    }
    //    if (Count == 0)
    //        AddOPBillingData(ObjData);
    //    else
    //        DisplayOPBillingList(gOPBillingList);
    //}
    //else if (this.id == "btnUpdateMagazine") {
    //    ObjData.sNO = $("#hdnOPSNo").val();
    //    if ($("#hdnSalesEntryID").val() > 0) {
    //        ObjData.StatusFlag = "U";
    //        ObjData.SalesEntryID = $("#hdnSalesEntryID").val();
    //    }
    //    else {
    //        ObjData.StatusFlag = "I";
    //        ObjData.SalesEntryID = 0;
    //    }
    //    Update_OPBilling(ObjData);
    //}
    //CalculateAmount();
    //CalculateBalance();
    //ClearOPBillingFields();
    //console.log(BalanceAmount);
    //var scrollBottom = Math.max($('#tblOPBillingList').height());
    //$('#divOPBillingList').scrollTop(scrollBottom);
    //GetGiftList($("#txtNetAmount").val());
    //$("#ddlProductName").focus();
    //$("#btnAddMagazine").show();
    //$("#btnUpdateMagazine").hide();
    //$("#txtBarcode").focus();

   
});



function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlProductName").val(null).change();
    $("#txtCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtDisPer").val("0");
    $("#txtDisAmt").val("0");
    $("#txtSubTotal").val("0.00");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");

    if (parseFloat($("#txtDiscountPercent").val()) > 0) {
        $("#txtDisPer").val($("#txtDiscountPercent").val());
    }

    $("#ddlTax").val($("#ddlTaxName").val()).change();
    $("#ddlProductName").val(null).change();
    $("#divProductName").show();
    $("#divProductName").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
    $("#divRate").removeClass('has-error');

    return false;
}

function AddOPBillingData(oData) {
    gOPBillingList.push(oData);
    DisplayOPBillingList(gOPBillingList);
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
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>SMS Code</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>Qty</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Total</th>";
        sTable += "<th style='text-align: center' colspan='2' class='" + sColorCode + "'>Discount</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th style='text-align: center' colspan='2' class='" + sColorCode + "'>Tax</th>";
        sTable += "<th class='" + sColorCode + "'>Barcode</th>";
        if ($("#hdnSalesEntryID").val() > 0) { }
        else {
            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        }
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Image</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                $("#txtSNo").val(sCount + 1);
                sTable += "<td>" + gData[i].Product.SMSCode + "</td>";
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].Rate * gData[i].Quantity + "</td>";
                sTable += "<td style='text-align: right'>" + gData[i].DiscountPercentage.toFixed(2) + " %</td>";
                sTable += "<td>" + gData[i].DiscountAmount.toFixed(2) + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
                sTable += "<td style='text-align: right'>" + gData[i].Tax.TaxPercentage + " %</td>";
                sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].Barcode + "</td>";
                if ($("#hdnSalesEntryID").val() > 0) { }
                else {
                    sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OPBillingDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                    sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OPBillingDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                }
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

function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gOPBillingList);
    return false;
}

function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlProductName").focus();
    var sCount = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            if (data[i].StatusFlag != "D") {
                $("#hdnOPSNo").val(ID);
                $("#txtSNo").val(sCount + 1);
                $("#hdnSalesEntryID").val(data[i].SalesEntryID);

                $("#txtCode").val(data[i].Product.SMSCode).blur();
                var Id = data[i].Product.ProductID;
                $("#ddlProductName").val(data[i].Product.ProductID).change();

                $("#ddlTax").val(data[i].Tax.TaxID).change();
                //$("#txtCode").val(data[i].Product.SMSCode);
                $("#txtQuantity").val(data[i].Quantity);
                $("#txtRate").val(data[i].Rate);
                $("#txtTaxAmt").val(data[i].TaxAmount);
                $("#txtDisPer").val(data[i].DiscountPercentage);
                $("#txtDisAmt").val(data[i].DiscountAmount);
                $("#txtSubTotal").val(data[i].SubTotal);
                $("#txtBarcode").val(data[i].Barcode);
            }
        }
    }
    return false;
}

function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].SalesEntryID = oData.SalesEntryID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            oProduct.SMSCode = oData.Product.SMSCode;
            gOPBillingList[i].Product = oProduct;

            var oTransTax = new Object();

            oTransTax.TaxID = oData.Tax.TaxID;
            oTransTax.TaxPercentage = oData.Tax.TaxPercentage;
            oTransTax.IGSTPercent = oData.Tax.IGSTPercent;
            oTransTax.SGSTPercent = oData.Tax.SGSTPercent;
            oTransTax.CGSTPercent = oData.Tax.CGSTPercent;
            gOPBillingList[i].Tax = oTransTax;

            gOPBillingList[i].Quantity = oData.Quantity;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].IGSTAmount = oData.IGSTAmount;
            gOPBillingList[i].TaxAmount = oData.TaxAmount;
            gOPBillingList[i].SGSTAmount = oData.SGSTAmount;
            gOPBillingList[i].CGSTAmount = oData.CGSTAmount;
            gOPBillingList[i].SMSCode = oData.Product.SMSCode;
            gOPBillingList[i].DiscountPercentage = oData.DiscountPercentage;
            gOPBillingList[i].DiscountAmount = oData.DiscountAmount;
            gOPBillingList[i].SubTotal = oData.SubTotal;
            gOPBillingList[i].Barcode = oData.Barcode;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayOPBillingList(gOPBillingList);
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    ClearOPBillingFields();
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
                DisplayOPBillingList(gOPBillingList);
                CalculateAmount();

                GetGiftList($("#txtNetAmount").val());
                // $("#ddlGift").val(0);
                // $("#ddlGift").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                // $('#ddlGift').empty()
            }
        }
    }
    return false;
}

//#endregion SalesTrans

//#region ExchangeTrans

$("#btnAddExchange,#btnUpdateExchange").click(function () {

    if ($("#ddlExchangeProductName").val() == "0" || $("#ddlExchangeProductName").val() == undefined || $("#ddlExchangeProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divExchangeProductName").addClass('has-error'); $("#ddlExchangeProductName").focus(); return false;
    }
    else { $("#divExchangeProductName").removeClass('has-error'); }

    if ($("#txtExchangeQuantity").val() == "" || $("#txtExchangeQuantity").val() == undefined || $("#txtExchangeQuantity").val() == null || $("#txtExchangeQuantity").val() <= 0) {
        $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divExchangeQuantity").addClass('has-error'); $("#txtExchangeQuantity").focus(); return false;
    } else { $("#divExchangeQuantity").removeClass('has-error'); }

    if (parseFloat($("#hdnPreQtyID").val()) < parseFloat($("#txtExchangeQuantity").val())) {
        $.jGrowl("Please enter quantity less then sold quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divExchangeQuantity").addClass('has-error'); $("#txtExchangeQuantity").focus(); return false;
    } else { $("#divExchangeQuantity").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.ExchangeTransID = 0;

    var oProduct = new Object();

    oProduct.ProductID = $("#ddlExchangeProductName").val();
    oProduct.ProductName = $("#ddlExchangeProductName option:selected").text();

    ObjData.Product = oProduct;
    ObjData.Quantity = parseFloat($("#txtExchangeQuantity").val());
    ObjData.Rate = parseFloat($("#txtExchangeRate").val());

    var oTaxTrans = new Object();

    oTaxTrans.TaxID = $("#hdnExchangeTaxID").val();
    oTaxTrans.TaxPercentage = $("#hdnExchangeTaxPercent").val().trim();
    oTaxTrans.CGSTPercent = $("#hdnExchangeCGSTPercent").val().trim();
    oTaxTrans.SGSTPercent = $("#hdnExchangeSGSTPercent").val().trim();
    oTaxTrans.IGSTPercent = $("#hdnExchangeIGSTPercent").val().trim();
    ObjData.Tax = oTaxTrans;

    ObjData.CGSTAmount = $("#hdnExchangeCGSTAmount").val().trim();
    ObjData.SGSTAmount = $("#hdnExchangeSGSTAmount").val().trim();
    ObjData.IGSTAmount = $("#hdnExchangeIGSTAmount").val().trim();
    ObjData.TaxAmount = parseFloat($("#txtExchangeTaxAmt").val());

    ObjData.DiscountPercentage = parseFloat($("#txtExchangeDisPer").val());
    ObjData.DiscountAmount = parseFloat($("#txtExchangeDisAmt").val());
    ObjData.SubTotal = parseFloat($("#txtExchangeSubTotal").val());
    ObjData.Barcode = $("#hdnExchangeBarcode").val();

    if (this.id == "btnAddExchange") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.ExchangeTransID = 0;
        ObjData.StatusFlag = "I";
        AddExchange(ObjData);
    }
    else if (this.id == "btnUpdateExchange") {
        ObjData.sNO = $("#hdnExchangeSNo").val();
        if ($("#hdnSalesEntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesEntryID = $("#hdnSalesEntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesEntryID = 0;
        }
        Update_Exchange(ObjData);
    }
    CalculateAmount();
    ClearSalesReturnFields();

    var scrollBottom = Math.max($('#tblExchangeList').height());
    $('#divExchangeList').scrollTop(scrollBottom);

    $("#ddlExchangeProductName").focus();
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
});

function AddExchange(oData) {
    gExchangeList.push(oData);
    DisplayExchangeList(gExchangeList);
    return false;
}

function DisplayExchangeList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5) { $("#divExchangeList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else { $("#divExchangeList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblExchangeList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. %</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. Amt</th>";
        sTable += "<th class='" + sColorCode + "'>Tax Amt</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        if ($("#hdnSalesEntryID").val() > 0) { }
        else {
            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        }
        sTable += "</tr></thead><tbody id='tblExchangeList_body'>";
        sTable += "</tbody></table>";
        $("#divExchangeList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].DiscountPercentage + "</td>";
                sTable += "<td>" + gData[i].DiscountAmount + "</td>";
                sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
                if ($("#hdnSalesEntryID").val() > 0) { }
                else {
                    sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_Exchange(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                    sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_Exchange(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                } sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblExchangeList_body").append(sTable);
            }
        }
    }
    else { $("#divExchangeList").empty(); }

    return false;
}

function Edit_Exchange(ID) {
    Bind_ExchangeByID(ID, gExchangeList);
    return false;
}

function Bind_ExchangeByID(ID, data) {
    $("#btnAddExchange").hide();
    $("#btnUpdateExchange").show();
    $("#ddlExchangeProductName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnExchangeSNo").val(ID);
            $("#SalesEntryID").val(data[i].SalesEntryID);
            $("#ddlExchangeProductName").val(data[i].Product.ProductID).change();
            $("#hdnProductID").val(data[i].Product.ProductID);
            $("#txtExchangeQuantity").val(data[i].Quantity);
            $("#txtExchangeRate").val(data[i].Rate);

            $("#hdnExchangeTaxID").val(data[i].TaxID);
            $("#hdnExchangeTaxPercent").val(data[i].TaxPercentage);
            $("#hdnExchangeTaxPercent").val(data[i].CGSTPercent);
            $("#hdnExchangeSGSTPercent").val(data[i].SGSTPercent);
            $("#hdnExchangeIGSTPercent").val(data[i].IGSTPercent);
            $("#hdnExchangeBarcode").val(data[i].Barcode);
            $("#hdnExchangeCGSTAmount").val(data[i].CGSTAmount);
            $("#hdnExchangeSGSTAmount").val(data[i].SGSTAmount);
            $("#hdnExchangeIGSTAmount").val(data[i].IGSTAmount);
            $("#txtExchangeTaxAmt").val(data[i].TaxAmount);
            $("#txtExchangeDisPer").val(data[i].DiscountPercentage);
            $("#txtExchangeDisAmt").val(data[i].DiscountAmount);
            $("#txtExchangeSubTotal").val(data[i].SubTotal);
        }
    }
    return false;
}

function Update_Exchange(oData) {
    for (var i = 0; i < gExchangeList.length; i++) {
        if (gExchangeList[i].sNO == oData.sNO) {
            gExchangeList[i].SalesReturnID = oData.SalesReturnID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            gExchangeList[i].Product = oProduct;
            gExchangeList[i].SalesEntryTransID = oData.SalesEntryTransID;
            gExchangeList[i].Quantity = oData.Quantity;
            gExchangeList[i].Rate = oData.Rate;
            gOPBillingList[i].Tax.TaxID = oData.Tax.TaxID;
            gOPBillingList[i].Tax.TaxPercent = oData.Tax.TaxPercent;
            gOPBillingList[i].Tax.IGSTPercent = oData.Tax.IGSTPercent;
            gOPBillingList[i].Tax.SGSTPercent = oData.Tax.SGSTPercent;
            gOPBillingList[i].Tax.CGSTPercent = oData.Tax.CGSTPercent;
            gOPBillingList[i].IGSTAmount = oData.IGSTAmount;
            gOPBillingList[i].SGSTAmount = oData.SGSTAmount;
            gOPBillingList[i].CGSTAmount = oData.CGSTAmount;
            gExchangeList[i].DiscountPercentage = oData.DiscountPercentage;
            gExchangeList[i].DiscountAmount = oData.DiscountAmount;
            gExchangeList[i].SubTotal = oData.SubTotal;
            gExchangeList[i].Barcode = oData.Barcode;
            gExchangeList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayExchangeList(gExchangeList);
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    ClearSalesReturnFields();
    $("#ddlExchangeProductName").focus();
    return false;
}

function Delete_Exchange(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gExchangeList.length; i++) {
            if (gExchangeList[i].SNo == ID) {
                var index = jQuery.inArray(gExchangeList[i].valueOf("SNo"), gOPBillingList);
                if (gExchangeList[i].SNo > 0) {
                    gExchangeList[i].StatusFlag = "D";
                } else {
                    gExchangeList.splice(index, 1);
                }
                $("#divExchangeList").empty();
                DisplayExchangeList(gExchangeList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function ClearSalesReturnFields() {
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    $("#ddlExchangeProductName").val(null).change();
    $("#txtExchangeQuantity").val("0");
    $("#txtExchangeRate").val("0");
    $("#txtExchangeDisPer").val("0");
    $("#txtExchangeDisAmt").val("0");
    $("#txtExchangeTaxAmt").val("0");
    $("#txtExchangeSubTotal").val("0.00");
    $("#hdnExchangeSNo").val("");
    $("#divExchangeProductName").removeClass('has-error');
    $("#divExchangeQuantity").removeClass('has-error');

    return false;
}

//#endregion ExchangeTrans

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetProductDetails(id, value, SID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductDetails",
        data: JSON.stringify({ ID: id, type: value, SupplierID: SID }),
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
                            var sTable = "";
                            var sCount = 1;
                            var sColorCode = "bg-info";

                            if (obj.length >= 5) { $("#divDetailsList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
                            else { $("#divDetailsList").css({ 'height': '', 'min-height': '' }); }

                            if (obj.length > 0) {
                                sTable = "<table id='tblDetailsList' class='table no-margin table-condensed table-hover'>";
                                sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th class='" + sColorCode + "'>Customer</th>";
                                sTable += "<th class='" + sColorCode + "'>Invoice No</th>";
                                sTable += "<th class='" + sColorCode + "'>Invoice Date</th>";
                                sTable += "<th class='" + sColorCode + "'>Product Name</th>";
                                sTable += "<th class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th class='" + sColorCode + "'>Disc. Amt</th>";
                                sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "<th class='" + sColorCode + "'>Barcode</th>";
                                sTable += "</tr></thead><tbody id='tblDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td id='" + obj[i].PK_SalesEntryTransID + "'>" + sCount + "</td>";
                                    sTable += "<td>" + obj[i].CustomerName + "</td>";
                                    sTable += "<td>" + obj[i].InvoiceNo + "</td>";
                                    sTable += "<td>" + obj[i].sInvoiceDate + "</td>";
                                    sTable += "<td>" + obj[i].Product.ProductName + "</td>";
                                    sTable += "<td>" + obj[i].Quantity + "</td>";
                                    sTable += "<td>" + obj[i].Rate + "</td>";
                                    sTable += "<td>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td>" + obj[i].SubTotal + "</td>";
                                    sTable += "<td>" + obj[i].Barcode + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblDetailsList_body").append(sTable);
                                }

                            }
                            else { $("#divDetailsList").empty(); }

                            return false;
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
    dProgress(false);
    return false;

}

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopSalesEntry",
        data: JSON.stringify({ PublisherID: 0, IsRetail: 1, IsYarnBill: 0 }),
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
                                if (obj[index].IsActive == "1") {
                                    //{ TypeStatus = "<span class='label label-success'>Active</span>"; }
                                    //else
                                    //{ TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }
                                    //var InvoiceNo1 = obj[index].RetailsPaymentMode;
                                    //var result = InvoiceNo1.split(',').join('');
                                    //var result1 = result.split('.00').join('<br />');
                                    var table = "";
                                    if (obj[index].BalanceAmount > 0) {
                                        if (obj[index].DueDays == obj[index].Customer.Days)
                                            table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].SalesEntryID + "'>";
                                        else
                                            table += "<tr id='" + obj[index].SalesEntryID + "'>";
                                    } else
                                        table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].SalesEntryID + "'>";
                                    table += "<td>" + (index + 1) + "</td>";
                                    table += "<td>" + obj[index].InvoiceNo + "</td>";
                                    table += "<td>" + obj[index].sInvoiceDate + "</td>";
                                    table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                    table += "<td>" + obj[index].Customer.MobileNo + "</td>";
                                    table += "<td>" + obj[index].Customer.Area + "</td>";
                                    table += "<td>" + obj[index].RetailsPaymentMode + "</td>";
                                    table += "<td>" + obj[index].TotalQty + "</td>";
                                    table += "<td>" + obj[index].NetAmount + "</td>";

                                    if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Print' title='Click here to Print'><i class='fa fa-print text-green'></i></a></td>";

                                    table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].Customer.CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].Customer.CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                    table += "</tr>";
                                    $("#tblRecord_tbody").append(table);
                                }
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
                                if (ActionUpdate == "1") {
                                    BalanceAmount = 0;
                                    EditRecord($(this).parent().parent()[0].id);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Print").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintRetailsalesEntry.aspx", "MsgWindow");
                            });
                            $(".Address").click(function () {
                                SetSessionValue("SalesID", $(this).attr('Accountno'));
                                SetSessionValue("Table", "Customer");
                                var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
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
                            { "sWidth": "7%" },
                            { "sWidth": "15%" },
                            { "sWidth": "20%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "18%" },
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "1%" },
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

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SearchSalesEntry",
        data: JSON.stringify({ ID: iDetails, IsRetail: 1, IsYarnBill: 0 }),
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
                                if (obj[index].IsActive == "1") {
                                    //{ TypeStatus = "<span class='label label-success'>Active</span>"; }
                                    //else
                                    //{ TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }
                                    var table = "";
                                    if (obj[index].BalanceAmount > 0) {
                                        if (obj[index].DueDays == obj[index].Customer.Days)
                                            table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].SalesEntryID + "'>";
                                        else
                                            table += "<tr id='" + obj[index].SalesEntryID + "'>";
                                    } else
                                        table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].SalesEntryID + "'>";
                                    // var table = "<tr id='" + obj[index].SalesEntryID + "'>";
                                    table += "<td>" + (index + 1) + "</td>";
                                    table += "<td>" + obj[index].InvoiceNo + "</td>";
                                    table += "<td>" + obj[index].sInvoiceDate + "</td>";
                                    table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                    table += "<td>" + obj[index].Customer.MobileNo + "</td>";
                                    table += "<td>" + obj[index].Customer.Area + "</td>";
                                    table += "<td>" + obj[index].RetailsPaymentMode + "</td>";
                                    table += "<td>" + obj[index].TotalQty + "</td>";
                                    table += "<td>" + obj[index].NetAmount + "</td>";

                                    //if (ActionView == "1")
                                    //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                    //else
                                    //{ table += "<td></td>"; }

                                    if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Print' title='Click here to Print RSE'></i><i class='fa fa-print text-green'/></a></td>";

                                    table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].Customer.CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].Customer.CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                    table += "</tr>";
                                    $("#tblSearchResult_tbody").append(table);
                                }
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
                                if (ActionUpdate == "1") {
                                    BalanceAmount = 0;
                                    EditRecord($(this).parent().parent()[0].id);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Print").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintRetailsalesEntry.aspx", "MsgWindow");
                            });
                            $(".Address").click(function () {
                                SetSessionValue("SalesID", $(this).attr('Accountno'));
                                SetSessionValue("Table", "Customer");
                                var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
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
                            { "sWidth": "7%" },
                            { "sWidth": "13%" },
                            { "sWidth": "20%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "15%" },
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "1%" },
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


//$("#ddlTaxName").change(function () {
//    var iTaxID = $("#ddlTaxName").val();
//    if (iTaxID != undefined && iTaxID > 0) {
//        GetTaxByID(iTaxID);
//        CalculateAmount();
//    }
//});


$("#ddlTaxName").change(function () {
    var iTaxID = $("#ddlTaxName").val();
    $("#ddlTax").val($("#ddlTaxName").val()).change();
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
                    gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                }
                gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
            }
        }
        DisplayOPBillingList(gOPBillingList);
        CalculateAmount();
    }
});

$("#ddlTax").change(function () {
    var iTax = $("#ddlTax").val();
    if (iTax != undefined && iTax > 0) {
        GetTaxTransByID(iTax);
        CalculateAmountTrans();
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

function CalculateAmountTrans() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iTax)) iTax = 0;
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
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

//$("#txtDiscountAmount").change(function () {
//    CalculateAmount();
//});

$("#txtTenderAmount").change(function () {
    CalculateBalance();
});
//$("#txtNetAmount").change(function () {
//    if ($("#txtNetAmount").val() > 0)
//        GetGiftList($("#txtNetAmount").val());
//});

function CalculateBalance() {
    var iNet = parseFloat($("#txtNetAmount").val());
    if (isNaN(iNet)) iNet = 0;
    var iTender = parseFloat($("#txtTenderAmount").val());
    if (isNaN(iTender)) iTender = 0;
    if (iTender > 0)
        $("#txtBalanceGiven").val((parseFloat(iNet) - parseFloat(iTender)).toFixed(2));
    else
        $("#txtBalanceGiven").val("0");
}

$("#txtRoundoff").change(function () {
    CalculateAmount();
});
$("#txtDiscountPercent").change(function () {
    //if ($("#hdnMaxDiscount").val() > 0) {
    //    if (parseFloat($("#hdnMaxDiscount").val()) < parseFloat($("#txtDiscountPercent").val())) {
    //        $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#txtDiscountPercent").focus(); return false;
    //    }
    //}
    $("#txtPasswords").val("");
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
            gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
        }
    }
    DisplayOPBillingList(gOPBillingList);
    CalculateAmount();
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

function CalculateAmount() {
    var iOPBillingAmount = 0, TotalAmount = 0, iBillingQty = 0, RoundOff = 0, Amount = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0;
    var iExchangeAmount = 0, iExchangeTax = 0, iPaidAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);
            iBillingTaxAmt = iBillingTaxAmt + parseFloat(gOPBillingList[i].TaxAmount);
            iBillingCGST = iBillingCGST + parseFloat(gOPBillingList[i].CGSTAmount);
            iBillingSGST = iBillingSGST + parseFloat(gOPBillingList[i].SGSTAmount);
            iBillingIGST = iBillingIGST + parseFloat(gOPBillingList[i].IGSTAmount);
            iBillingDiscount = iBillingDiscount + parseFloat(gOPBillingList[i].DiscountAmount);
            iBillingQty = iBillingQty + parseFloat(gOPBillingList[i].Quantity);
            TotalAmount = TotalAmount + (parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].Quantity));
        }
    }

    for (var i = 0; i < gExchangeList.length; i++) {
        if (gExchangeList[i].StatusFlag != "D") {
            iExchangeAmount = iExchangeAmount + parseFloat(gExchangeList[i].SubTotal);
            iExchangeTax = iExchangeTax + parseFloat(gExchangeList[i].TaxAmount);
        }
    }

    for (var i = 0; i < gPaymentList.length; i++) {
        if (gPaymentList[i].StatusFlag != "D") {
            iPaidAmt = iPaidAmt + gPaymentList[i].Amount;
        }
    }
    $("#txtExchangeAmount").val((parseFloat(iExchangeAmount) + parseFloat(iExchangeTax)).toFixed(2));

    $("#txtAmount").val(parseFloat(TotalAmount).toFixed(2));
    $("#txtTotalAmount").val(parseFloat(iOPBillingAmount).toFixed(2));

    $("#txtTaxAmount").val(parseFloat(iBillingTaxAmt).toFixed(2));
    $("#txtCGST").val(parseFloat(iBillingCGST).toFixed(2));
    $("#txtSGST").val(parseFloat(iBillingSGST).toFixed(2));
    $("#txtIGST").val(parseFloat(iBillingIGST).toFixed(2));
    $("#txtDiscountAmount").val(parseFloat(iBillingDiscount).toFixed(2));
    var iTCS_Amt = parseFloat($("#txtTCSAmount").val());
    if (isNaN(iTCS_Amt)) iTCS_Amt = 0;
    //var iround = parseFloat($("#txtRoundoff").val());
    //if (isNaN(iround)) iround = 0;
    Amount = (parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) - parseFloat(iExchangeAmount) - parseFloat(iExchangeTax) + parseFloat(iTCS_Amt)).toFixed(2);
    RoundOff = Math.round(Amount);
    $("#txtRoundoff").val((RoundOff - Amount).toFixed(2));
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;

    $("#txtTotalQty").val((parseFloat(iBillingQty)).toFixed(0));
    $("#txtNetAmount").val((parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) - parseFloat(iExchangeAmount) - parseFloat(iExchangeTax) + parseFloat(iround) + parseFloat(iTCS_Amt)).toFixed(2));
    $("#txtPaidAmount").val((parseFloat($("#txtNetAmount").val()) - parseFloat(iPaidAmt)).toFixed(2));
    $("#txtSalesPoints").val(((parseFloat($("#txtNetAmount").val())) / 100).toFixed(2));

    var iNet = parseFloat($("#txtNetAmount").val());
    if (isNaN(iNet)) iNet = 0;
    $("#txtTenderAmount").val(iPaidAmt);
    var iTender = parseFloat($("#txtTenderAmount").val());
    if (isNaN(iTender)) iTender = 0;
    if (iTender > 0)
        $("#txtBalanceGiven").val((parseFloat(iTender) - parseFloat(iNet)).toFixed(2));
    else
        $("#txtBalanceGiven").val("0");
}
$("#btnSave,#btnUpdate").click(function () {
    TaxCalculate();
    //if (this.id == "btnSave") {
    //    if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    //}
    //else if (this.id == "btnUpdate") {
    //    if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    //}

    if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
    }
    else { $("#divBillDate").removeClass('has-error'); }

    if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined || $("#txtMobileNo").val().length != 10) {
        $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
    }
    else { $("#divMobileNo").removeClass('has-error'); }

    if ($("#txtCustomer").val().trim() == "" || $("#txtCustomer").val().trim() == undefined) {
        $.jGrowl("Please enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomer").addClass('has-error'); $("#txtCustomer").focus(); return false;
    }
    else { $("#divCustomer").removeClass('has-error'); }

    if ($("#ddlTaxName").val() == "0" || $("#ddlTaxName").val() == undefined || $("#ddlTaxName").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxName").addClass('has-error'); $("#ddlTaxName").focus(); return false;
    }
    else { $("#divTaxName").removeClass('has-error'); }

    if ($("#ddlCustomerType").val() == "0" || $("#ddlCustomerType").val() == undefined || $("#ddlCustomerType").val() == null) {
        $.jGrowl("Please select Customer Type", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomerType").addClass('has-error'); $("#ddlCustomerType").focus(); return false;
    }
    else { $("#divCustomerType").removeClass('has-error'); }

    if ($("#ddlState").val() == "0" || $("#ddlState").val() == undefined || $("#ddlState").val() == null) {
        $.jGrowl("Please select state", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divState").addClass('has-error'); $("#ddlState").focus(); return false;
    }
    else { $("#divState").removeClass('has-error'); }

    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    var iOPBillingAmount = 0, iPaymentAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            iOPBillingAmount = iOPBillingAmount + 1;
    }
    if (iOPBillingAmount <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCode").focus(); return false;
    }

    //if ($("#hdnMaxDiscount").val() > 0) {
    //    if (parseFloat($("#hdnMaxDiscount").val()) < parseFloat($("#txtDiscountPercent").val())) {
    //        $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#txtDiscountPercent").focus(); return false;
    //    }
    //}

    if ($("#hdnMaxDiscount").val() > 0) {
        var Id = parseFloat($("#hdnMaxDiscount").val());
        var Amt = parseFloat($("#txtDiscountPercent").val());
        if (parseFloat($("#hdnMaxDiscount").val()) < parseFloat($("#txtDiscountPercent").val())) {
            if ($("#txtPasswords").val() == 0) {
                $(".modal-title").html("&nbsp;&nbsp;Discount Percentage Exceeding Maximum discount");
                $('#composedialog').modal({ show: true, backdrop: 'static', keyboard: false });
                $("#txtPasswords").focus(); return false;
            }
        }
    }


    var creditAmt = 0;
    for (var i = 0; i < gPaymentList.length; i++) {
        if (gPaymentList[i].StatusFlag != "D") {
            iPaymentAmount = iPaymentAmount + gPaymentList[i].Amount;
            if (gPaymentList[i].PaymentMode == 'Credit')
                creditAmt = creditAmt + gPaymentList[i].Amount;
        }
    }
    if (parseFloat($("#txtNetAmount").val()) > parseFloat(iPaymentAmount)) {
        $.jGrowl("Net Amount and Paid Amount must tally", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#ddlPaymentMode").focus(); return false;
    }

    if (parseFloat($("#txtBalanceGiven").val()) > 0) {
        if ($("#chkBalanceGiven").is(':checked')) {
        }
        else {
            $.jGrowl("please verify Balance given or not and Click Balance given box", { sticky: false, theme: 'warning', life: jGrowlLife });
            ("#ddlPaymentMode").focus(); return false;
        }
    }
    if ($("#ddlBillStatus").val() == "Booking Bill") {
        if ($("#txtDeliveryDate").val().trim() == "" || $("#txtDeliveryDate").val().trim() == undefined) {
            $.jGrowl("Please select Delivery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divDeliveryDate").addClass('has-error'); $("#txtDeliveryDate").focus(); return false;
        }
        else { $("#divDeliveryDate").removeClass('has-error'); }
    }
    var ObjOPBilling = new Object();

    ObjOPBilling.SalesEntryID = 0;
    ObjOPBilling.InvoiceNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sInvoiceDate = $("#txtBillDate").val().trim();
    ObjOPBilling.SalesEntryTrans = gOPBillingList;
    ObjOPBilling.SalesExchangeTrans = gExchangeList;
    ObjOPBilling.RetailPaymentMode = gPaymentList;

    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = 0;
    ObjCustomer.MobileNo = $("#txtMobileNo").val();
    ObjCustomer.CustomerName = $("#txtCustomer").val().toUpperCase();
    ObjCustomer.Address = $("#txtAddress").val();
    ObjCustomer.Area = $("#txtArea").val();

    ObjOPBilling.Customer = ObjCustomer;

    var ObjTax = new Object();
    ObjTax.TaxID = $("#ddlTaxName").val();
    ObjOPBilling.Tax = ObjTax;

    var ObjGift = new Object();
    ObjGift.GiftID = $("#ddlGift").val();
    ObjOPBilling.Gift = ObjGift;

    var ObjTransport = new Object();
    ObjTransport.TransportID = 0;
    ObjOPBilling.Transport = ObjTransport;

    var ObjShippingAddress = new Object();
    ObjShippingAddress.ShippingAddressID = 0;
    ObjOPBilling.ShippingAddress = ObjShippingAddress;

    var ObjBank = new Object();
    ObjBank.LedgerID = 0;
    ObjOPBilling.Bank = ObjBank;

    var ObjSalesOrder = new Object();
    ObjSalesOrder.SalesOrderID = 0;
    ObjOPBilling.SalesOrder = ObjSalesOrder;

    var ObjAgent = new Object();
    ObjAgent.AgentID = $("#ddlAgent").val();
    ObjOPBilling.Agent = ObjAgent;

    var oCustomerType = new Object();
    oCustomerType.CustomertypeID = $("#ddlCustomerType").val();
    oCustomerType.CustomerTypeName = $("#ddlCustomerType option:selected").text();
    ObjOPBilling.CustomerType = oCustomerType;

    var ostate = new Object();
    ostate.StateID = $("#ddlState").val();
    ostate.State = $("#ddlState option:selected").text();
    ObjOPBilling.State = ostate;
    ObjOPBilling.ImagePath1 = $("[id*=imgUpload2_view]").attr("src");
    ObjOPBilling.ImagePath2 = $("[id*=imgUpload3_view]").attr("src");
    ObjOPBilling.ImagePath3 = $("[id*=imgUpload4_view]").attr("src");
    ObjOPBilling.TaxPercent = $("#hdnTaxPercent").val().trim();
    ObjOPBilling.CGSTAmount = $("#txtCGST").val().trim();
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.IGSTAmount = $("#txtIGST").val().trim();
    ObjOPBilling.TaxAmount = $("#txtTaxAmount").val().trim();
    ObjOPBilling.TotalAmount = $("#txtTotalAmount").val().trim();

    var Roundoff = parseFloat($("#txtRoundoff").val());
    if (isNaN(Roundoff))
        ObjOPBilling.Roundoff = 0;
    else
        ObjOPBilling.Roundoff = $("#txtRoundoff").val().trim();

    ObjOPBilling.AdditionalDiscount = $("#txtAdditionalDiscountAmount").val().trim();

    var DiscountAmount = parseFloat($("#txtDiscountAmount").val());
    if (isNaN(DiscountAmount))
        ObjOPBilling.DiscountAmount = 0;
    else
        ObjOPBilling.DiscountAmount = $("#txtDiscountAmount").val().trim();

    var DiscountPercent = parseFloat($("#txtDiscountPercent").val());
    if (isNaN(DiscountPercent))
        ObjOPBilling.DiscountPercent = 0;
    else
        ObjOPBilling.DiscountPercent = $("#txtDiscountPercent").val().trim();

    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.IsActive = 1;
    ObjOPBilling.ExchangeAmount = $("#txtExchangeAmount").val();
    ObjOPBilling.TransportCharges = 0;
    ObjOPBilling.OtherCharges = 0;
    ObjOPBilling.UsedPoints = 0;
    ObjOPBilling.CancelReason = "";
    ObjOPBilling.Narration = $("#txtNarration").val();
    ObjOPBilling.PaymentMode = $("#ddlPaymentMode").val();
    ObjOPBilling.BillStatus = $("#ddlBillStatus").val();
    // if ($("#ddlBillStatus").val() == "Booking Bill")
    ObjOPBilling.sDeliveryDate = $("#txtDeliveryDate").val().trim();
    //else {
    //    var currentdate = new Date();
    //    var nowMonth = ((currentdate.getMonth().toString().length) == 1) ? '0' + (currentdate.getMonth() + 1) : (currentdate.getMonth() + 1);
    //    var ThisDay = currentdate.getDate();
    //    var ThisYear = currentdate.getFullYear();
    //    var ThisDate = ThisDay.toString() + "/" + nowMonth.toString() + "/" + ThisYear.toString();
    //    ObjOPBilling.sDeliveryDate = ThisDate;
    //}

    ObjOPBilling.IsYarnBill = 0;
    ObjOPBilling.IsRetailBill = 1;
    if (ObjOPBilling.PaymentMode == "Card") {
        ObjOPBilling.CardNo = $("#txtCardNo").val().trim();
        ObjOPBilling.CardCharges = $("#txtCardCharges").val().trim();
    }
    else {
        ObjOPBilling.CardNo = "";
        ObjOPBilling.CardCharges = "0";
    }

    ObjOPBilling.TenderAmount = $("#txtTenderAmount").val().trim();
    ObjOPBilling.BalanceGiven = $("#txtBalanceGiven").val().trim();
    ObjOPBilling.SalesPoints = $("#txtSalesPoints").val().trim();
    ObjOPBilling.EWayNo = "";
    ObjOPBilling.LRNo = "";
    ObjOPBilling.TransportName = "";
    ObjOPBilling.VehicleNo = "";
    ObjOPBilling.sLRDate = "";

    if ($("#hdnSalesEntryID").val() > 0) {
        ObjOPBilling.SalesEntryID = $("#hdnSalesEntryID").val();
        gOPBillingList.SalesEntryID = ObjOPBilling.SalesEntryID;
        ObjOPBilling.PaidAmount = parseFloat($("#txtNetAmount").val()) - BalanceAmount;
        ObjOPBilling.BalanceAmount = parseFloat($("#txtNetAmount").val()) - ObjOPBilling.PaidAmount;
        if ($("#ddlPaymentMode").val() == "Credit")
            ObjOPBilling.Status = $("#hdnStatus").val().trim();
        else
            ObjOPBilling.Status = "Closed";

        sMethodName = "UpdateSalesEntry";
    }
    else {
        sMethodName = "AddSalesEntry";
        ObjOPBilling.SalesEntryID = 0;
        if (parseFloat(creditAmt) > 0) {
            ObjOPBilling.Status = "Pending";
            ObjOPBilling.BalanceAmount = parseFloat(BalanceAmount).toFixed(2);
            ObjOPBilling.PaidAmount = ObjOPBilling.NetAmount - ObjOPBilling.BalanceAmount;
        }
        else {
            ObjOPBilling.Status = "Closed";
            ObjOPBilling.BalanceAmount = 0;
            ObjOPBilling.PaidAmount = ObjOPBilling.NetAmount;
        }
    }
    console.log(ObjOPBilling);
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
                        if (sMethodName == "AddSalesEntry") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnSalesEntryID").val(objResponse.Value);
                            EditRecord($("#hdnSalesEntryID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            //SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            //var myWindow = window.open("PrintRetailsalesEntry.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                            //SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            //var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                        }
                        else if (sMethodName == "UpdateSalesEntry") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            EditRecord($("#hdnSalesEntryID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            //SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            //var myWindow = window.open("PrintRetailsalesEntry.aspx", "MsgWindow");
                            $("#btnList").click();
                            $("#hdnSalesEntryID").val("0");
                        }

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

function TaxCalculate() {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            gOPBillingList[i].DiscountAmount = parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].DiscountPercentage) / 100;
            gOPBillingList[i].SubTotal = (parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].Quantity)) - parseFloat(gOPBillingList[i].DiscountAmount);
            iSubtotal = gOPBillingList[i].SubTotal;
            iTaxID = gOPBillingList[i].Tax.TaxID;
            GetTaxByID(iTaxID);

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
                gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
            }
            gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
        }
        CalculateAmount();
    }
}
$("#ddlState").change(function () {
    var iSupplierID = $("#ddlState").val();
    if (iSupplierID != undefined && iSupplierID > 0) {
        GetStateByID(iSupplierID);
    }
});

function GetStateByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStateByID",
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
                            $("#hdnStateCode").val(obj.StateCode);

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
                                        gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                                    }
                                    gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
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
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}


$("#btnPrintbill").click(function () {
    SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
    var myWindow = window.open("PrintRetailsalesEntry.aspx", "MsgWindow");
});
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByID",
        data: JSON.stringify({ ID: id, IsRetail: 1, IsYarnBill: 0 }),
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
                            GetGiftList(obj.NetAmount);
                            $("#txtOtherPassword").val("");
                            $("#txtBillNo").attr("disabled", true);

                            $("#hdnSalesEntryID").val(obj.SalesEntryID)
                            $("#txtBillNo").val(obj.InvoiceNo);
                            $("#txtBillDate").val(obj.sInvoiceDate);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("#ddlCustomerType").val(obj.CustomerType.CustomertypeID).change();

                            $("#ddlState").val(obj.State.StateID).change();
                            $("#txtMobileNo").val(obj.Customer.MobileNo).change();
                            $("#txtCustomer").val(obj.Customer.CustomerName);
                            $("#txtAddress").val(obj.Customer.Address);

                            $("#txtCGST").val(obj.CGSTAmount);
                            $("#txtSGST").val(obj.SGSTAmount);
                            $("#txtIGST").val(obj.IGSTAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscountAmount").val(obj.DiscountAmount);
                            $("#hdnDiscountAmount").val(obj.DiscountAmount);
                            $("#hdnAddDiscountAmount").val(obj.AdditionalDiscount);
                            $("#txtAdditionalDiscountAmount").val(obj.AdditionalDiscount);
                            $("#hdnPaidAmt").val(obj.PaidAmount);
                            $("#hdnNetAmt").val(obj.NetAmount);
                            $("#hdnBalanceAmt").val(obj.BalanceAmount);
                            $("#hdnStatus").val(obj.Status);
                            $("#txtNarration").val(obj.Narration);
                            $("#ddlPaymentMode").val(obj.PaymentMode);
                            $("#ddlBillStatus").val(obj.BillStatus);
                            if (obj.PaymentMode == "Card") {
                                $("#divCardDetails").show();
                                $("#txtCardNo").val(obj.CardNo);
                                $("#txtCardCharges").val(obj.CardCharges);
                            }

                            $("#txtDeliveryDate").val(obj.sDeliveryDate);
                            $("#ddlGift").val(obj.Gift.GiftID).change();
                            $("#txtCardNo").val(obj.CardNo);
                            $("#txtCardCharges").val(obj.CardCharges);
                            $("#txtTenderAmount").val(obj.TenderAmount);
                            $("#txtBalanceGiven").val(obj.BalanceGiven);
                            $("#txtSalesPoints").val(obj.SalesPoints);
                            $("[id*=imgUpload2_view]").css("visibility", "visible");
                            $("[id*=imgUpload2_view]").attr("src", obj.ImagePath1);
                            $("[id*=imgUpload3_view]").css("visibility", "visible");
                            $("[id*=imgUpload3_view]").attr("src", obj.ImagePath2);
                            $("[id*=imgUpload4_view]").css("visibility", "visible");
                            $("[id*=imgUpload4_view]").attr("src", obj.ImagePath3);
                            $("#ddlTaxName,#ddlGift").prop('disabled', true);
                            $("#txtDiscountPercent,#txtDiscountAmount").prop('disabled', true);
                            $("#txtAdditionalDiscountAmount,#divAdditionalDiscount").show();
                            $("#btnAddMagazine,#btnAddExchange").hide();
                            gOPBillingList = [];
                            var ObjProduct = obj.SalesEntryTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                var objMagazine = new Object();
                                objMagazine.ProductID = ObjProduct[index].Product.ProductID;
                                objMagazine.ProductName = ObjProduct[index].Product.ProductName;
                                objMagazine.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objMagazine;

                                var objTransTax = new Object();
                                objTransTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTransTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTransTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTransTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTransTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTransTax;

                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                objTemp.SalesEntryID = ObjProduct[index].SalesEntryID;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.SGSTAmount = ObjProduct[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjProduct[index].IGSTAmount;
                                objTemp.CGSTAmount = ObjProduct[index].CGSTAmount;
                                objTemp.TaxAmount = ObjProduct[index].TaxAmount;
                                // objTemp.NewProductFlag = ObjProduct[index].NewProductFlag;

                                AddOPBillingData(objTemp);
                            }

                            gExchangeList = [];
                            var ObjProduct = obj.SalesExchangeTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                var objMagazine = new Object();
                                objMagazine.ProductID = ObjProduct[index].Product.ProductID;
                                objMagazine.ProductName = ObjProduct[index].Product.ProductName;
                                objMagazine.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objMagazine;

                                var objTransTax = new Object();
                                objTransTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTransTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTransTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTransTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTransTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTransTax;

                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                objTemp.SalesEntryID = ObjProduct[index].SalesEntryID;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.SGSTAmount = ObjProduct[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjProduct[index].IGSTAmount;
                                objTemp.CGSTAmount = ObjProduct[index].CGSTAmount;
                                objTemp.TaxAmount = ObjProduct[index].TaxAmount;

                                AddExchange(objTemp);
                            }

                            gPaymentList = [];
                            var ObjProduct = obj.RetailPaymentMode;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                objTemp.RetailPaymentModeID = ObjProduct[index].RetailPaymentModeID;
                                objTemp.SalesEntryID = ObjProduct[index].SalesEntryID;
                                objTemp.BankID = ObjProduct[index].BankID;
                                objTemp.PaymentMode = ObjProduct[index].PaymentMode;

                                if (objTemp.PaymentMode == "Cash")
                                    objTemp.BankName = "";
                                else
                                    objTemp.BankName = ObjProduct[index].BankName;

                                objTemp.Amount = ObjProduct[index].Amount;
                                objTemp.ChequeNo = ObjProduct[index].ChequeNo;
                                objTemp.sIssueDate = ObjProduct[index].sIssueDate;
                                objTemp.sCollectionDate = ObjProduct[index].sCollectionDate;
                                objTemp.IssuedBy = ObjProduct[index].IssuedBy;
                                objTemp.Charges = ObjProduct[index].Charges;
                                objTemp.Status = ObjProduct[index].Status;
                                if (objTemp.PaymentMode == "Credit")
                                    BalanceAmount = BalanceAmount + ObjProduct[index].Amount;
                                AddPayment(objTemp);
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
function ShowDeleteRecord(id) {
    // DeleteRecord(id, $("#txtReason").val());
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

    if ($("#txtReason").val() == undefined || $("#txtReason").val() == null || $("#txtReason").val().trim() == "") {
        $.jGrowl("Please enter reason for cancelling", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divReason").addClass('has-error'); $("#txtReason").focus(); return false;
    }
    else { $("#divReason").removeClass('has-error'); }

    if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined || $("#txtPassword").val().trim() != $("#hdRS").val()) {
        $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPassword").addClass('has-error'); $("#txtPassword").focus(); return false;
    } else { $("#divPassword").removeClass('has-error'); }

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
        url: "WebServices/VHMSService.svc/DeleteSalesEntry",
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
                    else if (objResponse.Value == "OPBilling_R_01" || objResponse.Value == "OPBilling_D_01") {
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

$("#btnRateCancel").click(function () {
    $('#composeRate').modal('hide');
});


$("#btnImageCancel").click(function () {
    $('#composeImage').modal('hide');
    return false;
});

$("#btndetailsCancel").click(function () {
    $('#composedetails').modal('hide');
});

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
                            $("[id*=imgUpload1]").css("visibility", "visible");
                            $("[id*=imgUpload1]").attr("src", obj.ProductImage1);
                            $("[id*=imgUpload5]").css("visibility", "visible");
                            $("[id*=imgUpload5]").attr("src", obj.ProductImage2);
                            $("[id*=imgUpload6]").css("visibility", "visible");
                            $("[id*=imgUpload6]").attr("src", obj.ProductImage3);

                            $(".modal-title").html("&nbsp;&nbsp; Product Image");
                            $('#composeImage').modal({ show: true, backdrop: true });
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
