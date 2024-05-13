
var gOPBillingList = [];
var PreviousRate = 0;
var RecordAvailable = 0;
var RateCount = 0;
var StockCheck = 0;
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
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();

    GetTaxList("ddlTax");
    GetPassword();
    GetTransportList("ddlTransport");
    GetSupplierList("ddlSupplierName");
    GetEmployeeList("ddlVerifiedBy");
    GetProductList("ddlProductName");
    GetEmployeeList("ddlConfirmedBy");
    $("#btnPurchaseBarcode").hide();
    $("#ddlVerifiedBy").val(0).change();
    $("#ddlConfirmedBy").val(0).change();
    GetTaxList("ddlTaxName");
    $("input[name=SupplierProduct]:checked").val("S");
    $("#txtBillDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtDate").attr("data-link-format", "dd/MM/yyyy");


    $("#txtBillDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    $("#txtDate").datetimepicker({
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

            GetReceivedPurchase(parseInt($.cookie("PurchaseID")));
            $("#hdnDCPurchaseID").val(parseInt($.cookie("PurchaseID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("PurchaseID", null);
    }
    GetSupplierName("ddlCategoryName");
    pLoadingSetup(true);
    GetRecord();

});

function GetTransportList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTransport",
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
                                    $(sControlName).append("<option value='" + obj[index].TransportID + "'>" + obj[index].TransportName + "</option>");
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

function GetSalesProductDetails(id, smscode, value, SID, SalesID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseNewProductDetails",
        data: JSON.stringify({ ID: id, code: smscode, type: value, SupplierID: SID, SalesEntryID: SalesID }),
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
                                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Supplier</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Bill No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Disc. Amt</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "</tr></thead><tbody id='tblDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td style='line-height:0.5;' id='" + obj[i].PurchaseTransID + "'>" + sCount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SupplierName + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Rate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].sBillDate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].BillNo + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Quantity + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SubTotal + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblDetailsList_body").append(sTable);
                                    // if ($('#hdnIsAllProduct').val() == 1)
                                    // $(".modal-title").html("&nbsp;&nbsp; All Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                    // else
                                    if ($('#hdnIsAllProduct').val() == 0) {
                                        $(".modal-title").html("&nbsp;&nbsp; This Supplier - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        $("#Allcomposedetails").modal('hide');
                                        $('#composedetails').modal({ show: true, backdrop: true });

                                    }
                                }
                                RecordAvailable = obj.length;

                            }
                            else { $("#divDetailsList").empty(); }

                            return false;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

                        RecordAvailable = 0;
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
    dProgress(false);
    return false;

}

function GetAllProductDetails(id, smscode, value, SID, SalesID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseNewProductDetails",
        data: JSON.stringify({ ID: id, code: smscode, type: value, SupplierID: SID, SalesEntryID: SalesID }),
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

                            if (obj.length >= 5) { $("#divAllDetailsList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
                            else { $("#divAllDetailsList").css({ 'height': '', 'min-height': '' }); }

                            if (obj.length > 0) {
                                sTable = "<table id='tblAllDetailsList' class='table no-margin table-condensed table-hover'>";
                                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Supplier</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Bill No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Disc. Amt</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "</tr></thead><tbody id='tblAllDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divAllDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td style='line-height:0.5;' id='" + obj[i].PurchaseTransID + "'>" + sCount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SupplierName + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Rate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].sBillDate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].BillNo + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Quantity + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SubTotal + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblAllDetailsList_body").append(sTable);
                                    if ($('#hdnIsAllProduct').val() == 1) {
                                        $(".Allmodal-title").html("&nbsp;&nbsp; All supplier - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        // else
                                        //    $(".modal-title").html("&nbsp;&nbsp; This Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        $("#composedetails").modal('hide');
                                        $("#Allcomposedetails").modal({ show: true, backdrop: true });
                                    }
                                }
                                RecordAvailable = obj.length;

                            }
                            else { $("#divAllDetailsList").empty(); }

                            return false;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

                        RecordAvailable = 0;
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
    dProgress(false);
    return false;

}

$("#ddlCategoryName").change(function () {
    GetRecord();
});

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
                            $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
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

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}

$("#btnClearImage1").click(function () {
    $get("imgUploadDCPurchase1_view").src = "";
    $("#imagePurchasefile").val("");
});

$("#btnClearImage2").click(function () {
    $get("imgUploadDCPurchase2_view").src = "";
    $("#imagePurchasefile2").val("");
});

$("#btnClearImage3").click(function () {
    $get("imgUploadDCPurchase3_view").src = "";
    $("#imagePurchasefile3").val("");
});

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
    $("#hdnDCPurchaseID").val("0");
    $("#hdnDCPurcID").val("0");
    $("#txtComments").val("");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnPurchaseBarcode").hide();
    $("#divTab").hide();
    $("#divOPBilling").show();
    $("#txtSNo").val("1");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#ddlTaxName").change();
    $("#ddlSupplierName").change();
    $("#ddlTax").change();
    gOPBillingList = [];
    ClearOPBillingTab();
    ClearOPBillingFields();
    $("#divOPBillingList").empty();
    $("#ddlTaxName").val(2).change();
    $("#ddlTax").val(2).change();
    $("#txtBillDate").focus();
    $("#divOtherPasswordlbl").hide();
    $("#divOtherPassword").hide();
    $("#txtDate").focus();
    $("#txtNo").focus();
    $("#imagePurchasefile").val("");
    $("#imagePurchasefile2").val("");
    $("#imagePurchasefile3").val("");

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
    $("#hdnDCPurchaseID").val("0");
    $("#hdnDCPurcID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});

function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtNo").val("");
    $("#txtDate").val("");
    $("#ddlTaxName").val($("#ddlTaxName option:first").val());
    $("#chkDC").prop("checked", true);
    $("#chkYarn").prop("checked", false);
    $("#txtTotalQuantity").val("0");
    $("#txtName").val("");
    $("#txtPaymentDiscount").val("0");
    $("#txtOPDNo").val("");
    gOPBillingList = [];
    ClearOPBillingFields();
    $get("imgUpload1").src = "";
    $("#ddlTax").val($("#ddlTax option:first").val());

    $("#ddlVerifiedBy").val(0).change();
    $("#ddlConfirmedBy").val(0).change();
    $("#ddlDis_amt_Typ").val("NetAmount").change();
    $("#txtPaymentDiscountPercent").val(0);
    $("#txtOtherCharges").val(0);
    $("#txtCourierCharges").val(0);
    $("#txtPaymentDiscount").val(0);
    GetTaxList("ddlTaxName");
    $("#txtDiscountPercent").val("0");
    $("#txtDiscountAmount").val("0");
    $("#txtTCSPercent").val("0");
    $("#txtTCSAmount").val("0");
    $("#chk").prop("checked", false);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    GetProductList("ddlProductName");
    $get("imgUploadDCPurchase1_view").src = "";
    $get("imgUploadDCPurchase2_view").src = "";
    $get("imgUploadDCPurchase3_view").src = "";
    $("[id*=imgUploadDCPurchase1_view]").css("visibility", "hidden");
    $("[id*=imgUploadDCPurchase2_view]").css("visibility", "hidden");
    $("[id*=imgUploadDCPurchase3_view]").css("visibility", "hidden");
    $("#ddlProductName").val(null).change();
    $("#txtBillNo").attr("disabled", false);
    return false;
}


//$("#imgUploadDCPurchase1_view").click(function () {
//    var $img = $('#imgUploadDCPurchase1_view'),
//        imageWidth = $img[0].width, //need the raw width due to a jquery bug that affects chrome
//        imageHeight = $img[0].height, //need the raw height due to a jquery bug that affects chrome
//        maxWidth = $(window).width(),
//        maxHeight = $(window).height(),
//        widthRatio = maxWidth / imageWidth,
//        heightRatio = maxHeight / imageHeight;

//    var ratio = widthRatio; //default to the width ratio until proven wrong

//    if (widthRatio * imageHeight > maxHeight) {
//        ratio = heightRatio;
//    }

//    //now resize the image relative to the ratio
//    $img.attr('width', "109%")
//        .attr('height', imageHeight * ratio);

//    //and center the image vertically and horizontally
//    $img.css({
//        margin: 'auto',
//        position: 'absolute',
//        top: 0,
//        bottom: 0,
//        left: 0,
//        right: 0
//    });
//});

$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().trim().length > 3) {
        GetProductByCodeList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();
            GetProductTax();
        }
    }
    else if ($("#txtCode").val().length == 0) {
        GetProductList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();
            GetProductTax();
        }

        //ClearOPBillingFields();
    }
});

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
        GetRate();
        GetProductTax();
    }
});


$('#ddlProductName').on('select2:close', function (e) {
    GetProductTaxwithModel();
});

$("input[type=radio]").change(function () {
    GetProductList("ddlProductName");
    //GetProductByCodeList("ddlProductName");
    $("#txtCode").val("");
    $("#ddlProductName").val(null).change();
    ClearOPBillingFields();

});

function GetProductDetailsPurchase(id, value, SID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductDetailsPurchase",
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
                            RecordAvailable = obj.length;
                            PreviousRate = obj[0].Rate;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        RecordAvailable = 0;
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


function GetRate() {
    if ($("#ddlProductName").val() > 0 && $("#ddlSupplierName").val() > 0) {
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
                                // $("#txtCode").val(obj.SMSCode);
                                //$("#ddlTax").val(obj.Tax.TaxID);
                                PreviousRate = obj.Rate;

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
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        });
        return false;
    }
}

function GetProductTaxwithModel() {
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
                                $("#ddlTax").val(obj.Tax.TaxID).change();
                                if (obj.IsRateUpdated == 1) {
                                    $(".modal-title").html("&nbsp;&nbsp;Rate has been changed by Supplier");
                                    $('#composedialog').modal({ show: true, backdrop: 'static', keyboard: false });
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
                                if (obj[index].IsActive) {
                                    if ($("input[name=SupplierProduct]:checked").val() == "S") {
                                        if (obj[index].Supplier.SupplierID == $("#ddlSupplierName").val())
                                            $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");

                                    } else {
                                        $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                    }
                                }
                            }
                            //  $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
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
                                        if ($("input[name=SupplierProduct]:checked").val() == "S") {
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
    CalculateTrans();
});

function CalculateTrans() {
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
    var iTaxPercent = parseFloat($("#hdnTransCGSTAmount").val()) + parseFloat($("#hdnTransSGSTAmount").val()) + parseFloat($("#hdnTransIGSTAmount").val());
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

$("#txtRate").change(function () {
    if (PreviousRate == parseFloat($("#txtRate").val())) {
        PreviousRate = $("#txtRate").val();
        RateCount = 0;
    }
    else {
        if (PreviousRate != 0) {
            $(".modal-title").html("&nbsp;&nbsp;Rate has been changed");
            $('#composedialog').modal({ show: true, backdrop: 'static', keyboard: false });
            if (PreviousRate < parseFloat($("#txtRate").val())) {
                RateCount = 1;
            }
            else
                RateCount = 2;

        }
    }
});

$("#btnCloseDialog").click(function () {
    $('#composedialog').modal('hide');
    return false;
});

$("#txtDisPer,#txtRate,#txtQuantity").change(function () {
    var iDisAmt = parseFloat($("#txtDisPer").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    var iDisPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iDisAmt) / 100;
    $("#txtDisAmt").val(parseFloat(iDisPercent).toFixed(2));

    CalculateTrans();

});


$("#txtDiscountAmount").change(function () {
    calculateDiscount();
});

function calculateDiscount() {
    var iOPBillingAmount = 0, iBillingDiscount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate));
        }
    }

    var iBillingDiscount = parseFloat($("#txtDiscountAmount").val());
    if (isNaN(iBillingDiscount)) iBillingDiscount = 0;
    // $("#txtDiscountPercent").val((parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).toFixed(2)).change();

    var DiscountPercent = (parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).toFixed(2);

    $("#txtDiscountPercent").val(parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount)).change();
    $("#txtDiscountPercent").val((parseFloat(DiscountPercent)).toFixed(2));
}


//#region Purchase Trans
$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    CalculateTrans();
    if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    else { $("#divProductName").removeClass('has-error'); }

    if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <= 0) {
        $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    } else { $("#divQuantity").removeClass('has-error'); }

    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxTrans").addClass('has-error'); $("#ddlTax").focus(); return false;
    }
    else { $("#divTaxTrans").removeClass('has-error'); }

    if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null) {
        $.jGrowl("Please enter rate", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    } else { $("#divRate").removeClass('has-error'); }

    if ($("#txtDisPer").val() == "" || $("#txtDisPer").val() == undefined || $("#txtDisPer").val() == null) {
        $.jGrowl("Please enter Disc. Percent", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDisPer").addClass('has-error'); $("#txtDisPer").focus(); return false;
    } else { $("#divDisPer").removeClass('has-error'); }

    if ($("#txtDisAmt").val() == "" || $("#txtDisAmt").val() == undefined || $("#txtDisAmt").val() == null) {
        $.jGrowl("Please enter Disc. Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDisAmt").addClass('has-error'); $("#txtDisAmt").focus(); return false;
    } else { $("#divDisAmt").removeClass('has-error'); }

    if ($("#hdnDCPurchaseTransID").val() > 0)
        CheckdeleteStockByProductID($("#hdnDCPurchaseTransID").val(), $("#ddlProductName").val(), parseFloat($("#txtQuantity").val()));

    if (StockCheck == 0) {
        var ObjData = new Object();
        ObjData.PurchaseID = 0;

        var oProduct = new Object();

        oProduct.ProductID = $("#ddlProductName").val();
        oProduct.ProductName = $("#ddlProductName option:selected").text();
        oProduct.SMSCode = $("#txtSMSCode").val().toUpperCase();
        oProduct.ProductCode = $("#txtPartyCode").val();
        ObjData.Product = oProduct;

        var oTaxTrans = new Object();

        oTaxTrans.TaxID = $("#ddlTax").val();
        oTaxTrans.TaxName = $("#ddlTax option:selected").text();
        oTaxTrans.TaxPercentage = $("#hdnTransTaxPercent").val().trim();

        if ($("#hdnStateCode").val() == 33) {
            oTaxTrans.CGSTPercent = $("#hdnTransCGSTPercent").val().trim();
            oTaxTrans.SGSTPercent = $("#hdnTransSGSTPercent").val().trim();
            oTaxTrans.IGSTPercent = 0;
        }
        else {
            oTaxTrans.CGSTPercent = 0;
            oTaxTrans.SGSTPercent = 0;
            oTaxTrans.IGSTPercent = $("#hdnTransIGSTPercent").val().trim();
        }
        ObjData.Tax = oTaxTrans;

        ObjData.Quantity = parseFloat($("#txtQuantity").val());
        ObjData.Rate = parseFloat($("#txtRate").val());
        if (RateCount == 0) {
            ObjData.RateupdateFlag = false;
            ObjData.RateDecreaseFlag = false;
        }
        else if (RateCount == 1) {
            ObjData.RateupdateFlag = true;
            ObjData.RateDecreaseFlag = false;
        }
        else {
            ObjData.RateupdateFlag = false;
            ObjData.RateDecreaseFlag = true;
        }

        ObjData.SGSTAmount = $("#hdnTransSGSTAmount").val().trim();
        ObjData.CGSTAmount = $("#hdnTransCGSTAmount").val().trim();
        ObjData.IGSTAmount = $("#hdnTransIGSTAmount").val().trim();

        ObjData.TaxAmount = parseFloat($("#txtTaxAmt").val());
        ObjData.DiscountPercentage = parseFloat($("#txtDisPer").val());
        ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());
        ObjData.SubTotal = parseFloat($("#txtSubTotal").val());
        ObjData.Barcode = $("#txtBarcode").val();
        console.log(ObjData.Product.ProductID);
        GetProductDetailsPurchase(ObjData.Product.ProductID, $("#hdnDCPurchaseID").val(), $("#ddlSupplierName").val());
        if (RecordAvailable == 0)
            ObjData.NewProductFlag = 1;
        else
            ObjData.NewProductFlag = 0;
        if (this.id == "btnAddMagazine") {
            ObjData.sNO = gOPBillingList.max() + 1;
            ObjData.SNo = ObjData.sNO;
            ObjData.PurchaseID = 0;
            var Count = 0;
            ObjData.StatusFlag = "I";
            for (var i = 0; i < gOPBillingList.length; i++) {
                if (gOPBillingList[i].StatusFlag != "D") {
                    if ((gOPBillingList[i].Product.ProductID == $("#ddlProductName").val()) && (gOPBillingList[i].Rate == parseFloat($("#txtRate").val())) && (gOPBillingList[i].Tax.TaxID == $("#ddlTax").val()) && (gOPBillingList[i].DiscountPercentage == parseFloat($("#txtDisPer").val()))) {
                        gOPBillingList[i].Quantity = gOPBillingList[i].Quantity + parseFloat($("#txtQuantity").val());
                        var iDisPercent = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].DiscountPercentage) / 100;
                        gOPBillingList[i].DiscountAmount = parseFloat(iDisPercent);
                        gOPBillingList[i].SubTotal = gOPBillingList[i].SubTotal + parseFloat($("#txtSubTotal").val());
                        if ($("#hdnStateCode").val() == 33) {
                            gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnTransCGSTPercent").val());
                            gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnTransSGSTPercent").val());
                            gOPBillingList[i].Tax.IGSTPercent = 0;
                            gOPBillingList[i].CGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                            gOPBillingList[i].SGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                            gOPBillingList[i].IGSTAmount = 0;
                        }
                        else {

                            gOPBillingList[i].Tax.CGSTPercent = 0;
                            gOPBillingList[i].Tax.SGSTPercent = 0;
                            gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnTransIGSTPercent").val());
                            gOPBillingList[i].CGSTAmount = 0;
                            gOPBillingList[i].SGSTAmount = 0;
                            gOPBillingList[i].IGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                        }
                        gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);

                        Count = 1;
                    }
                }
            }
            if (Count == 0)
                AddOPBillingData(ObjData);
            else
                DisplayOPBillingList(gOPBillingList);
        }
        else if (this.id == "btnUpdateMagazine") {
            ObjData.sNO = $("#hdnOPSNo").val();
            if ($("#hdnDCPurchaseID").val() > 0) {
                ObjData.StatusFlag = "U";
                ObjData.PurchaseID = $("#hdnDCPurchaseID").val();
            }
            else {
                ObjData.StatusFlag = "I";
                ObjData.PurchaseID = 0;
            }
            Update_OPBilling(ObjData);
        }
        if (RecordAvailable == 0) {
            $.jGrowl("No data New design Kindly Check...", { sticky: false, theme: 'warning', life: jGrowlLife });
            RecordAvailable = 0;
        }
        var scrollBottom = Math.max($('#tblOPBillingList').height());
        $('#divOPBillingList').scrollTop(scrollBottom);
        RateCount = 0;
        PreviousRate = 0;
        CalculateAmount();
        ClearOPBillingFields();
        $("#txtCode").focus();
        $("#btnAddMagazine").show();
        $("#btnUpdateMagazine").hide();
    }
});

function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#txtCode").val("");
    $("#ddlProductName").val(null).change();
    $("#txtSMSCode").val("");
    $("#txtPartyCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtDisPer").val("0");
    $("#txtTaxAmt").val("0");
    $("#txtDisAmt").val("0");
    $("#txtSubTotal").val("0.00");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");

    if (parseFloat($("#txtDiscountPercent").val()) > 0) {
        $("#txtDisPer").val($("#txtDiscountPercent").val());
    }
    $("#ddlTax").val($("#ddlTaxName").val()).change();

    $("#ddlProductName").val(null).change();

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
    var StockQty = 0;
    if (gData.length >= 12)
    { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else
    { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>PartyCode</th>";
        sTable += "<th class='" + sColorCode + "'>SMSCode</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. %</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. Amt</th>";
        sTable += "<th class='" + sColorCode + "'>Tax %</th>";
        sTable += "<th class='" + sColorCode + "'>Tax Amount</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "'>Barcode</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Image</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Print</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                //if (gData[i].PurchaseID == 0)
                //    GetProductDetailsPurchase(gData[i].Product.ProductID, 0, $("#ddlSupplierName").val());
                //else
                //    GetProductDetailsPurchase(gData[i].Product.ProductID, gData[i].PurchaseID, $("#ddlSupplierName").val());
                if (gData[i].NewProductFlag == 0)
                    sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                else
                    sTable = "<tr style='background-color:#f3c8c8;'><td id='" + gData[i].SNo + "'>" + sCount + "</td>";


                if (gData[i].RateupdateFlag == true) {
                    sTable = "<tr style='background-color:#b0ecb8;'><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                }
                if (gData[i].RateDecreaseFlag == true) {
                    sTable = "<tr style='background-color:#cc76dc;'><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                }
                $("#txtSNo").val(sCount + 1);
                sTable += "<td>" + gData[i].Product.ProductCode + "</td>";
                sTable += "<td>" + gData[i].Product.SMSCode + "</td>";
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                //if (RecordAvailable > 0)
                //    sTable += "<tr style='background-color:chartreuse;'><td>" + gData[i].Rate + "</td>";
                //else
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].DiscountPercentage + "</td>";
                sTable += "<td>" + gData[i].DiscountAmount + "</td>";
                sTable += "<td>" + gData[i].Tax.TaxPercentage + "</td>";
                sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
                sTable += "<td>" + gData[i].Barcode + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OPBillingDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OPBillingDetail(this.id," + gData[i].Product.ProductID + "," + gData[i].Quantity + ")'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].Product.ProductID + " onclick = 'GetProductByID(this.id)'><i class='fa fa-lg fa-file-image-o text-green'/></a></td>";
                if ($("#hdnDCPurchaseID").val() > 0)
                    sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Print_OPBillingDetail(this.id)'><i class='fa fa-print text-green'/></a></td>";
                else
                    sTable += "<td></td>";

                sTable += "</tr>";
                sCount = sCount + 1;

                $("#tblOPBillingList_body").append(sTable);
            }
        }
    }
    else { $("#divOPBillingList").empty(); }

    return false;
}
$("#btnImageCancel").click(function () {
    $('#composeImage').modal('hide');
    return false;
});

$("#txtPaymentDiscountPercent,#ddlDis_amt_Typ").change(function () {
    var itotal = 0, idisPercent = 0, idisamt = 0;
    idisPercent = parseFloat($("#txtPaymentDiscountPercent").val());
    if (isNaN(idisPercent)) idisPercent = 0;
    if ($("#ddlDis_amt_Typ").val() == "NetAmount")
        itotal = parseFloat($("#txtNetAmount").val());
    else
        itotal = parseFloat($("#txtTotalAmount").val());
    if (isNaN(itotal)) itotal = 0;
    idisamt = parseFloat(itotal) * parseFloat(idisPercent) / 100;
    $("#txtPaymentDiscount").val(idisamt.toFixed(2));
});

function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlProductName").focus();
    // GetProductList();
    var sCount = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            if (data[i].StatusFlag != "D") {
                //$("#hdnOPSNo").val = null;
                $("#hdnOPSNo").val(ID);
                $("#txtSNo").val(ID);
                $("#hdnDCPurchaseID").val(data[i].PurchaseID);
                $("#txtCode").val(data[i].Product.SMSCode).change();
                $("#ddlProductName").val(data[i].Product.ProductID).change();
                console.log($("#ddlProductName").val());

                var id = data[i].Tax.TaxID;
                if (data[i].Tax.TaxID == 0) {
                    $("#divNewProductName").show();
                    $("#divTaxTrans").hide();
                }
                else if (data[i].Tax.TaxID > 0) {
                    $("#divNewProductName").hide();
                    $("#divTaxTrans").show();
                    $("#rdbNewProductName").prop("checked", false);
                    $("#rdbExistingProductName").prop("checked", true);
                    $("#ddlTax").val(data[i].Tax.TaxID).change();
                }


                $("#txtPartyCode").val(data[i].Product.ProductCode);
                $("#txtQuantity").val(data[i].Quantity);
                $("#txtTaxAmt").val(data[i].TaxAmount);
                $("#txtRate").val(data[i].Rate);
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
            gOPBillingList[i].PurchaseID = oData.PurchaseID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            oProduct.SMSCode = oData.Product.SMSCode;
            oProduct.ProductCode = oData.Product.ProductCode;
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
            gOPBillingList[i].RateupdateFlag = oData.RateupdateFlag;
            gOPBillingList[i].RateDecreaseFlag = oData.RateDecreaseFlag;
            gOPBillingList[i].NewProductFlag = oData.NewProductFlag;
            gOPBillingList[i].TaxAmount = oData.TaxAmount;
            gOPBillingList[i].CGSTAmount = oData.CGSTAmount;
            gOPBillingList[i].SGSTAmount = oData.SGSTAmount;
            gOPBillingList[i].IGSTAmount = oData.IGSTAmount;
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

function Delete_OPBillingDetail(ID, ProductID, Qty) {
    if (ID == 0)
        return false;
    CheckdeleteStockByProductID(0, ProductID, Qty);

    if (StockCheck == 0) {
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
            }
        }
    }
    }
    return false;
}


function CheckdeleteStockByProductID(PurchaseTransID, ProductID, Qty) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/CheckdeleteStockByProductID",
        data: JSON.stringify({ iProductID: ProductID, iQty: Qty, iPurchaseTransID: PurchaseTransID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function(data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Error") {
                    StockCheck = 1;
                    $(".modal-titleStock").html("&nbsp;&nbsp;Stock Not Available");
                    $('#composedialogStock').modal({ show: true, backdrop: 'static', keyboard: false });
                }
                else {
                    StockCheck = 0;
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            }
        },
        error: function(e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}
$("#btnCloseDialogStock").click(function() {
    $('#composedialogStock').modal('hide');
    return false;
});
function Print_OPBillingDetail(ID) {
    if (ID == 0)
        return false;

    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].SNo == ID) {
            SetSessionValue("Barcode", gOPBillingList[i].Barcode);
            SetSessionValue("BarcodeQty", gOPBillingList[i].Quantity);
            SetSessionValue("ScreenName", "Purchase");
            var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
        }
    }
    return false;
}

//#endregion Purchase Trans

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
        url: "WebServices/VHMSService.svc/GetTopPurchaseSupplierWise",
        data: JSON.stringify({ iSupplierID: $("#ddlCategoryName").val(), BillType: 1, DC: 1 }),
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }
                                var table = "";
                                if (obj[index].BalanceAmount > 0) {
                                    if (obj[index].DueDays == obj[index].Supplier.Days)
                                        table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].PurchaseID + "'>";
                                    else
                                        table += "<tr id='" + obj[index].PurchaseID + "'>";
                                }
                                else
                                    table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].PurchaseID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].PurchaseNo + "</td>";
                                table += "<td>" + obj[index].sPurchaseDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                table += "<td>" + obj[index].TotalQty + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";
                                table += "<td>" + obj[index].CEmployeeName + "</td>";
                                table += "<td>" + obj[index].MEmployeeName + "</td>";
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='PrintPurchase' title='Click here to Print Purchase'></i><i class='fa fa-print text-green'/></a></td>";

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
                                if (ActionUpdate == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#divOtherPasswordlbl").show();
                                    $("#divOtherPassword").show();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintPurchase").click(function () {
                                SetSessionValue("PurchaseID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseEntry.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
                                    { ShowDeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "7%" },
                            { "sWidth": "7%" },
                            { "sWidth": "26%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
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
        url: "WebServices/VHMSService.svc/SearchPurchase",
        data: JSON.stringify({ ID: iDetails, BillType: 1, DC: 1 }),
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "";
                                if (obj[index].BalanceAmount > 0) {
                                    if (obj[index].DueDays == obj[index].Supplier.Days)
                                        table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].PurchaseID + "'>";
                                    else
                                        table += "<tr id='" + obj[index].PurchaseID + "'>";
                                }
                                else
                                    table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].PurchaseID + "'>";
                                //var table = "<tr id='" + obj[index].PurchaseID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].PurchaseNo + "</td>";
                                table += "<td>" + obj[index].sPurchaseDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                table += "<td>" + obj[index].TotalQty + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";
                                table += "<td>" + obj[index].CEmployeeName + "</td>";
                                table += "<td>" + obj[index].MEmployeeName + "</td>";
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseID + " class='PrintPurchase' title='Click here to Print Purchase'></i><i class='fa fa-print text-green'/></a></td>";

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
                                if (ActionUpdate == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#divOtherPasswordlbl").show();
                                    $("#divOtherPassword").show();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintPurchase").click(function () {
                                SetSessionValue("PurchaseID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseEntry.aspx", "MsgWindow");
                            });
                            $(".PrintOPBilling").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnDCPurchaseID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("PurchaseID", AdmissionID);

                                var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
                                    { ShowDeleteRecord($(this).parent().parent()[0].id); }
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
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "7%" },
                            { "sWidth": "7%" },
                            { "sWidth": "26%" },
                            { "sWidth": "8%" },
                            { "sWidth": "8%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
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

$("#ddlSupplierName").change(function () {
    var iSupplierID = $("#ddlSupplierName").val();
    if (iSupplierID != undefined && iSupplierID > 0) {

        GetSupplierByID(iSupplierID);
        GetProductList("ddlProductName");
        GetProductByCodeList("ddlProductName");
        $("#ddlProductName").val(null).change();
        ClearOPBillingFields();
        // $("#ddlTaxName").change();
    }
});

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

$("#txtRoundoff").change(function () {
    CalculateAmount();
});

$("#txtDiscountPercent").change(function () {
    var iDisPercent = parseFloat($("#txtDiscountPercent").val());
    $("#txtDisPer").val($("#txtDiscountPercent").val()).change();
    if (isNaN(iDisPercent)) iDisPercent = 0;
    var iSubtotal = 0; var iDiscAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            gOPBillingList[i].DiscountPercentage = (iDisPercent).toFixed(2);
            gOPBillingList[i].DiscountAmount = (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * iDisPercent / 100).toFixed(2);
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

$("#txtTCSPercent").change(function () {
    var iTCS = parseFloat($("#txtTCSPercent").val());
    if (isNaN(iTCS)) iTCS = 0;

    $("#txtTCSAmount").val((parseFloat($("#txtTotalAmount").val()) * iTCS / 100).toFixed(2));
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
                            $("#hdnDays").val(obj.Days);

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

function CalculateAmount() {
    var iOPBillingAmount = 0, TotalAmount = 0, iBillingQty = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);
            iBillingTaxAmt = iBillingTaxAmt + parseFloat(gOPBillingList[i].TaxAmount);
            gOPBillingList[i].CGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
            gOPBillingList[i].SGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
            gOPBillingList[i].IGSTAmount = (parseFloat(gOPBillingList[i].SubTotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
            iBillingCGST = iBillingCGST + parseFloat(gOPBillingList[i].CGSTAmount);
            iBillingSGST = iBillingSGST + parseFloat(gOPBillingList[i].SGSTAmount);
            iBillingIGST = iBillingIGST + parseFloat(gOPBillingList[i].IGSTAmount);
            iBillingDiscount = iBillingDiscount + parseFloat(gOPBillingList[i].DiscountAmount);
            iBillingQty = iBillingQty + parseFloat(gOPBillingList[i].Quantity);
            TotalAmount = TotalAmount + (parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].Quantity));
        }
    }
    $("#txtTotalAmount").val(parseFloat(iOPBillingAmount).toFixed(2));
    $("#txtAmount").val(parseFloat(TotalAmount).toFixed(2));

    $("#txtTaxAmount").val(parseFloat(iBillingTaxAmt).toFixed(2));
    $("#txtCGST").val(parseFloat(iBillingCGST).toFixed(2));
    $("#txtSGST").val(parseFloat(iBillingSGST).toFixed(2));
    $("#txtIGST").val(parseFloat(iBillingIGST).toFixed(2));
    var DiscountAmt = Math.round(iBillingDiscount);

    $("#txtDiscountAmount").val(parseFloat(DiscountAmt).toFixed(2));

    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;

    var iTCS_Amt = parseFloat($("#txtTCSAmount").val());
    if (isNaN(iTCS_Amt)) iTCS_Amt = 0;
    $("#txtTotalQty").val((parseFloat(iBillingQty)).toFixed(0));
    $("#txtNetAmount").val((parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) + parseFloat(iround) + parseFloat(iTCS_Amt)).toFixed(2));

}
$("#btnPurchaseBarcode").click(function () {
    SetSessionValue("BarcodePurchaseID", $("#hdnDCPurchaseID").val());
    SetSessionValue("ScreenName", "Purchase");
    var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
});



$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    } else if (this.id == "btnUpdate") {
        if (ActionUpdate == "1") {
            if ($("#txtOtherPassword").val().trim() == "" || $("#txtOtherPassword").val().trim() == undefined || $("#txtOtherPassword").val().trim() != $("#hdRS").val()) {
                $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherPassword").addClass('has-error'); $("#txtOtherPassword").focus(); return false;
            } else { $("#divOtherPassword").removeClass('has-error'); }
        }
        if (ActionUpdate != "1") {
            $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false;
        }
    }

    if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
    } else { $("#divBillDate").removeClass('has-error'); }

    if ($("#txtNo").val().trim() == "" || $("#txtNo").val().trim() == undefined) {
        $.jGrowl("Please enter Bill No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divNo").addClass('has-error'); $("#txtNo").focus(); return false;
    }
    else { $("#divNo").removeClass('has-error'); }

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

    if ($("#ddlVerifiedBy").val() == "0" || $("#ddlVerifiedBy").val() == undefined || $("#ddlVerifiedBy").val() == null) {
        $.jGrowl("Please select Verified By", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divVerifiedBy").addClass('has-error'); $("#ddlVerifiedBy").focus(); return false;
    }
    else { $("#divVerifiedBy").removeClass('has-error'); }

    if ($("#ddlConfirmedBy").val() == "0" || $("#ddlConfirmedBy").val() == undefined || $("#ddlConfirmedBy").val() == null) {
        $.jGrowl("Please select Confirmed By", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divConfirmedBy").addClass('has-error'); $("#ddlConfirmedBy").focus(); return false;
    }
    else { $("#divConfirmedBy").removeClass('has-error'); }

    //if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
    //    $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divTax").addClass('has-error'); $("#ddlTax").focus(); return false;
    //}
    //else { $("#divTax").removeClass('has-error'); }
    if ($("#ddlTransport").val() == "0" || $("#ddlTransport").val() == undefined || $("#ddlTransport").val() == null) {
        $.jGrowl("Please select Supplier", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTransport").addClass('has-error'); $("#ddlTransport").focus(); return false;
    }
    else { $("#divTransport").removeClass('has-error'); }
    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            iOPBillingAmount = iOPBillingAmount + 1;
    }
    if (iOPBillingAmount <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCode").focus(); return false;
    }

    if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
        $.jGrowl("Please select Bill Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
    }
    else { $("#divDate").removeClass('has-error'); }

    var ObjOPBilling = new Object();

    ObjOPBilling.PurchaseID = 0;
    ObjOPBilling.PurchaseNo = $("#txtBillNo").val().toUpperCase();
    ObjOPBilling.sPurchaseDate = $("#txtBillDate").val().trim();
    ObjOPBilling.BillNo = $("#txtNo").val().trim();
    ObjOPBilling.sBillDate = $("#txtDate").val().trim();
    ObjOPBilling.PurchaseTrans = gOPBillingList;

    var ObjSupplier = new Object();
    ObjSupplier.SupplierID = $("#ddlSupplierName").val();
    ObjOPBilling.Supplier = ObjSupplier;

    var ObjPurchaseOrder = new Object();
    ObjPurchaseOrder.PurchaseOrderID = 0;
    ObjOPBilling.PurchaseOrder = ObjPurchaseOrder;

    var objTransport = new Object();
    objTransport.TransportID = $("#ddlTransport").val();
    ObjOPBilling.Transport = objTransport;

    var ObjTax = new Object();
    ObjTax.TaxID = $("#ddlTaxName").val();
    ObjOPBilling.Tax = ObjTax;
    ObjOPBilling.BillType = true;
    ObjOPBilling.ConfirmedBy = $("#ddlConfirmedBy").val();
    ObjOPBilling.VerifiedBy = $("#ddlVerifiedBy").val();

    ObjOPBilling.DocumentPath = $("[id*=imgUpload1]").attr("src");

    ObjOPBilling.ImagePath1 = $("[id*=imgUploadDCPurchase1_view]").attr("src");
    ObjOPBilling.ImagePath2 = $("[id*=imgUploadDCPurchase2_view]").attr("src");
    ObjOPBilling.ImagePath3 = $("[id*=imgUploadDCPurchase3_view]").attr("src");
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.TaxPercent = $("#hdnTaxPercent").val().trim();
    ObjOPBilling.CGSTAmount = $("#txtCGST").val().trim();
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.IGSTAmount = $("#txtIGST").val().trim();
    ObjOPBilling.PaymentDiscount = $("#txtPaymentDiscount").val();
    ObjOPBilling.PaymentDiscountPercent = $("#txtPaymentDiscountPercent").val();
    ObjOPBilling.OtherCharges = $("#txtOtherCharges").val();
    ObjOPBilling.CourierCharges = $("#txtCourierCharges").val();
    ObjOPBilling.Dis_amt_Type = $("#ddlDis_amt_Typ").val();

    ObjOPBilling.TaxAmount = $("#txtTaxAmount").val().trim();
    ObjOPBilling.TotalAmount = $("#txtTotalAmount").val().trim();
    var Roundoff = parseFloat($("#txtRoundoff").val());
    if (isNaN(Roundoff))
        ObjOPBilling.Roundoff = 0;
    else
        ObjOPBilling.Roundoff = $("#txtRoundoff").val().trim();
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
    ObjOPBilling.TCSPercent = $("#txtTCSPercent").val().trim();
    ObjOPBilling.TCSAmount = $("#txtTCSAmount").val().trim();
    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.Comments = $("#txtComments").val().trim();
    ObjOPBilling.OPBillingTrans = gOPBillingList;
    ObjOPBilling.IsDC = $("#chkDC").is(':checked') ? "1" : "0";
    ObjOPBilling.IsYarn = $("#chkYarn").is(':checked') ? "1" : "0";

    if ($("#hdnDCPurcID").val() > 0) {
        ObjOPBilling.PurchaseID = $("#hdnDCPurcID").val();
        gOPBillingList.PurchaseID = ObjOPBilling.PurchaseID;
        ObjOPBilling.PaidAmount = parseFloat($("#hdnPaidAmt").val());
        ObjOPBilling.BalanceAmount = parseFloat($("#txtNetAmount").val()) - (parseFloat($("#hdnNetAmt").val()) - parseFloat($("#hdnBalanceAmt").val()));
        sMethodName = "UpdatePurchase";
    }
    else {
        sMethodName = "AddPurchase";
        ObjOPBilling.PurchaseID = 0;
        ObjOPBilling.PaidAmount = 0;
        ObjOPBilling.BalanceAmount = parseFloat($("#txtNetAmount").val());
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
                        if (sMethodName == "AddPurchase") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnDCPurchaseID").val(objResponse.Value);
                            EditRecord($("#hdnDCPurchaseID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            //SetSessionValue("PurchaseID", $("#hdnDCPurchaseID").val());
                            //var myWindow = window.open("PrintPurchaseEntry.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                        }
                        else if (sMethodName == "UpdatePurchase") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#btnList").click();
                            $("#hdnDCPurchaseID").val("0");
                        }


                        //$("#btnAddNew").show();
                        //$("#btnList").hide();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Purchase_A_01" || objResponse.Value == "Purchase_U_01") {
                        $.jGrowl("Bill No Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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
        url: "WebServices/VHMSService.svc/GetPurchaseByID",
        data: JSON.stringify({ ID: id, BillType: 1 }),
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
                            $("#btnPurchaseBarcode").show();
                            $("#txtBillNo").attr("disabled", true);
                            $("#txtOtherPassword").val("");
                            $("#hdnDCPurchaseID").val(obj.PurchaseID);
                            $("#hdnDCPurcID").val(obj.PurchaseID);
                            $("#txtBillNo").val(obj.PurchaseNo);
                            $("#txtBillDate").val(obj.sPurchaseDate);
                            $("#txtNo").val(obj.BillNo);
                            $("#txtDate").val(obj.sBillDate);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtComments").val(obj.Comments);
                            $("#ddlSupplierName").val(obj.Supplier.SupplierID).change();
                            $("#ddlTransport").val(obj.Transport.TransportID).change();
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("[id*=imgUpload1]").attr("src", obj.DocumentPath);
                            $("#ddlConfirmedBy").val(obj.ConfirmedBy).change();
                            $("#ddlVerifiedBy").val(obj.VerifiedBy).change();
                            $("#txtCGST").val(obj.CGSTAmount);
                            $("#txtSGST").val(obj.SGSTAmount);
                            $("#txtIGST").val(obj.IGSTAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscountAmount").val(obj.DiscountAmount);
                            $("#txtTCSPercent").val(obj.TCSPercent);
                            $("#txtTCSAmount").val(obj.TCSAmount);
                            $("#ddlDis_amt_Typ").val(obj.Dis_amt_Type).change();
                            $("#txtPaymentDiscountPercent").val(obj.PaymentDiscountPercent);
                            $("#txtOtherCharges").val(obj.OtherCharges);
                            $("#txtCourierCharges").val(obj.CourierCharges);
                            $("#txtPaymentDiscount").val(obj.PaymentDiscount);

                            $("#hdnPaidAmt").val(obj.PaidAmount);
                            $("#hdnNetAmt").val(obj.NetAmount);
                            $("#hdnBalanceAmt").val(obj.BalanceAmount);
                            $("[id*=imgUploadDCPurchase1_view]").css("visibility", "visible");
                            $("[id*=imgUploadDCPurchase1_view]").attr("src", obj.ImagePath1);
                            $("[id*=imgUploadDCPurchase2_view]").css("visibility", "visible");
                            $("[id*=imgUploadDCPurchase2_view]").attr("src", obj.ImagePath2);
                            $("[id*=imgUploadDCPurchase3_view]").css("visibility", "visible");
                            $("[id*=imgUploadDCPurchase3_view]").attr("src", obj.ImagePath3);
                            $("#chkDC").prop("checked", obj.IsDC ? true : false);
                            $("#chkYarn").prop("checked", obj.IsYarn ? true : false);

                            gOPBillingList = [];
                            var ObjProduct = obj.PurchaseTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                var objMagazine = new Object();
                                objMagazine.ProductID = ObjProduct[index].Product.ProductID;
                                objMagazine.ProductName = ObjProduct[index].Product.ProductName;
                                objMagazine.SMSCode = ObjProduct[index].Product.SMSCode;
                                objMagazine.ProductCode = ObjProduct[index].Product.ProductCode;
                                objTemp.Product = objMagazine;

                                var objTransTax = new Object();
                                objTransTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTransTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTransTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTransTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTransTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTransTax;

                                objTemp.PurchaseTransID = ObjProduct[index].PurchaseTransID;
                                objTemp.PurchaseID = ObjProduct[index].PurchaseID;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.RateupdateFlag = ObjProduct[index].RateupdateFlag;
                                objTemp.RateDecreaseFlag = ObjProduct[index].RateDecreaseFlag;
                                objTemp.NewProductFlag = ObjProduct[index].NewProductFlag;
                                objTemp.SGSTAmount = ObjProduct[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjProduct[index].IGSTAmount;
                                objTemp.CGSTAmount = ObjProduct[index].CGSTAmount;
                                objTemp.TaxAmount = ObjProduct[index].TaxAmount;

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

function ShowDeleteRecord(id) {
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    $('#compose-modal').modal({ show: true, backdrop: true });
    $("#txtID").val(id);
    $("#txtPassword").focus();
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
    $('#compose-modal').modal('hide');
}

function DeleteRecord(id, Reason) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeletePurchase",
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

$("#chk").change(function () {
    if ($(this).is(":checked"))
        $("#txtTCSPercent").val(0.075);
    else
        $("#txtTCSPercent").val(0);

    var iTCS = parseFloat($("#txtTCSPercent").val());
    if (isNaN(iTCS)) iTCS = 0;
    $("#txtTCSAmount").val((parseFloat($("#txtTotalAmount").val()) * iTCS / 100).toFixed(2));
    CalculateAmount();
});
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