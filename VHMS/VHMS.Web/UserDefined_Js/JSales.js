var gChitList = [];
var gSalesList = [];
var gExchangeList = [];

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    SdSMS = _SendSMS;
    SMSpassword = _SMSpassword;
    SMSsendername = _SMSsendername;
    SMSurl = _SMSurl;
    SMSusername = _SMSusername;
    useridd = _userID;
    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#btnAddChit").show();
    $("#btnUpdateChit").hide();
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    $("#SearchResult").hide();
    // $("#divChitDetails").hide();
    //$("#divExchangeDetails").hide();
    $("#divTab").show();
    $("#divSales").hide();
    $("#txtInvoiceDate").attr("data-link-format", "dd/MM/yyyy");
   // GetCategory("ddlCategory");
    $("#txtInvoiceDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY',

    });

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtInvoiceDate").val(d + "/" + m + "/" + y);
    //$("[id$=txtInvoiceDate]").datetimepicker({ weekStart: 1, dateFormat: 'dd/mm/yyyy' });

    GetTaxList("ddlTax");
    GetEmployeeList("ddlEmployee");
    GetEmployeeListsalesMan("ddlSalesMan");
    LoadRegister("ddlChit");
    GetSettings();
    var _Tfunctionality;


    pLoadingSetup(true);
    GetRecord();

});
function Edit_ExchangeDetail(ID) {
    Bind_ExchangeByID(ID, gExchangeList);
    return false;
}
$("#ddlCategory").change(function () {
    GetProduct("ddlProduct");
});
$("#txtENetWeight,#txtMeltingWeight,#txtCurrentRate").change(function () {
    CalculateTrans();
});
function CalculateTrans() {
    var iNetWt = 0;
    var iGrossWt = 0;
    var iMeltWt = 0;
    var iRate = 0;
    var iAmt = 0;
    if ($("#txtENetWeight").val() > 0)
        iNetWt = $("#txtENetWeight").val();
    if ($("#txtMeltingWeight").val() > 0)
        iMeltWt = $("#txtMeltingWeight").val();
    if ($("#txtCurrentRate").val() > 0)
        iRate = $("#txtCurrentRate").val();

    iGrossWt = iNetWt - iMeltWt;
    iAmt = iGrossWt * iRate;

    $("#txtEGrossWeight").val(iGrossWt);
    $("#txtETotal").val(parseFloat(iAmt).toFixed(2));

}
function GetProduct(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    var CatID = $("#ddlCategory").val();
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductID",
        data: JSON.stringify({ CategoryID: CatID }),
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
function GetCategory(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCategory",
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
                                    $(sControlName).append("<option value='" + obj[index].CategoryID + "'>" + obj[index].CategoryName + "</option>");
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
function Edit_SalesDetail(ID) {
    Bind_SalesByID(ID, gSalesList);
    return false;
}
function Edit_ChitDetail(ID) {
    Bind_ChitByID(ID, gChitList);
    return false;
}
//$("#chkExchange").change(function () {
//    if ($("#chkExchange").is(':checked')) {
//        $("#divExchangeDetails").show();

//    }
//    else
//        $("#divExchangeDetails").hide();
//});

$("#txtCustomerCode").blur(function () {
    if ($("#txtCustomerCode").val().length > 0) {
        GetCustomerDetails($("#txtCustomerCode").val());

    }
});

function LoadRegister(ddlname) {
    var sControlName = "#" + ddlname;
    var hidcusID = $("#hdnCustomerID").val();
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetClosedRegisterByStatus",
        data: JSON.stringify({ ID: 0, Status: "Closed" }),
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
                                //  if (obj[index].IsActive)
                                $(sControlName).append("<option value='" + obj[index].RegisterID + "'>" + obj[index].AccountNo + "</option>");
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

$("#txtDiscountPercent,#txtReturnAmount,#txtRoundoffAmount,#txtCardAmount,#txtExchangeAmount").change(function () {
    CalculateAmount();
});
$("#txtDiscountPercent").change(function () {

    if (parseFloat($("#hdnMaxDiscount").val()) >= parseFloat($("#txtDiscountPercent").val())) {
        CalculateAmount();
        $("#btnSave").attr("disabled", false);
    }
    else {
        $.jGrowl("You cannot give discount percentage greater than " + $("#hdnMaxDiscount").val() + "%", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#btnSave").attr("disabled", true); $("#txtDiscountPercent").focus();
    }
});
function GetCustomerDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomerByCode",
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
                            $("#txtName").val(obj.CustomerName);
                            $("#hdnCustomerID").val(obj.CustomerID);
                            $("#txtPhone").val(obj.MobileNo);
                            $("#txtAddress").val(obj.Address);


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
$("#ddlTax").change(function () {
    dProgress(true);
    var id = $("#ddlTax").val();
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

                            $("#txtTaxPercent").text(obj.TaxPercentage);
                            $("#txtCGSTPercent").text(obj.CGSTPercent);
                            $("#txtSGSTPercent").text(obj.SGSTPercent);
                            $("#txtIGSTPercent").text(obj.IGSTPercent);
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

});

$("#ddlPaymentMode").change(function () {
    if ($("#ddlPaymentMode").val() == "Cash") {
        $("#divCardNo").hide();
        $("#divCardAmount").hide();
        $("#txtCardAmount").val(0).change();
    }
    else {
        $("#divCardNo").show();
        $("#divCardAmount").show();
    }
});
$("#txtBarcode").change(function () {
    var iStatus;
    dProgress(true);
    var id = $("#txtBarcode").val();
    if ($("#hdnSalesID").val() > 0)
        iStatus = null;
    else
        iStatus = "IN"
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStockByBarcode",
        data: JSON.stringify({ ID: id, Status: iStatus }),
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
                            $("#hdnStockID").val(obj.StockID)
                            $("#txtProduct").val(obj.Product.ProductName);
                            $("#txtCategory").val(obj.Category.CategoryName);
                            $("#txtGrossWeight").val(obj.NetWeight);
                            $("#txtSellingPrice").val(obj.SellingPrice);
                            $("#txtMaking").val(obj.Making);
                            $("#txtNetWeight").val(parseFloat(obj.NetWeight) + parseFloat(obj.StoneWeight));
                            $("#txtStoneAmount").val( parseFloat(obj.StonePrice) *  parseFloat(obj.StoneQuantity));
                            $("#hdntransID").val(obj.StockID)
                            $("#txtBarcode").val(obj.Barcode);
                            $("#txtQuantity").val(obj.Quantity);
                            $("#txtStoneWeight").val(obj.StoneWeight);
                            $("#txtWastePercent").val(obj.WastagePercent);
                            // $("#txtWastage").val(obj.Wastage);

                            var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0; var iWastage = 0; var iGross = 0;
                            if ($("#txtSellingPrice").val() > 0)
                                iSelling = $("#txtSellingPrice").val();

                            if ($("#txtMaking").val() > 0)
                                iMaking = $("#txtMaking").val();

                            if ($("#txtStoneAmount").val() > 0)
                                iStoneAmt = $("#txtStoneAmount").val();

                            if ($("#txtStoneWeight").val() > 0)
                                iStoneWt = $("#txtStoneWeight").val();

                            if ($("#txtNetWeight").val() > 0)
                                iWeight = $("#txtNetWeight").val();

                            $("#txtWastage").val(parseFloat(iSelling) * obj.Wastage);

                            if ($("#txtWastage").val() > 0)
                                iWastage = $("#txtWastage").val();

                            if ($("#txtGrossWeight").val() > 0)
                                iGross = $("#txtGrossWeight").val();

                            iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iGross) * parseFloat(iSelling)) + (parseFloat(iStoneAmt)) + (parseFloat(iWastage));

                           
                            $("#txtTotal").val(parseFloat(iTotal).toFixed(2));

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

});
$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnSalesID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();


    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    //$("#divChitAmount").hide();

    $("#divTab").hide();
    $("#divSales").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gSalesList = [];
    gChitList = [];
    gExchangeList = [];
    ClearSalesTab();
    //$('#txtInvoiceDate').datepicker('setDate', 'today');
    //ClearFields();
    $("#divSalesList").empty();

    $("#txtInvoiceDate").focus();
    $("#txtInvoiceNo").focus();
    return false;
});

function PrintSalesDetails() {
    SetSessionValue("AdmissionID", $("#hdnSalesID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewSalesDetails.aspx", "_blank");
    return false;
}

$("#btnPrint").click(function () {

    SetSessionValue("SalesID", $("#hdnSalesID").val());

    var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divSales").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnSalesID").val("0");
    gSalesList = [];
    gChitList = [];
    ClearSalesTab();
    $("#btnList").click();
    return false;
});

//$("#chkSales,#chkChit").change(function () {
//    if ($("#chkSales").prop("checked") == true) {
//        $("#divChitDetails").hide();
//        $("#btnAddChit").hide();
//        $("#btnUpdateChit").hide();
//        var gChitList = [];
//    }
//    else {
//        $("#divChitDetails").show();
//        $("#btnAddChit").show();
//        $("#btnUpdateChit").hide();
//    }
//    return false;
//});

$("#ddlChit").change(function () {
    var iStatus;
    dProgress(true);
    var id = $("#ddlChit").val();
    if ($("#hdnSalesID").val() > 0)
        iStatus = "All";
    else
        iStatus = "All"
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRegisterByID",
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
                            $("#txtChitName").val(obj.Chit.ChitName);
                            $("#txtChitAmount").val(parseFloat(obj.ChitAmount) + parseFloat(obj.BonusAmount));
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

});

function ClearSalesTab() {
    $("#txtInvoiceNo").val("");
    $("#txtInvoiceDate").val("");
    $("#hdnPatientID").val("");
    $("#txtSubtotal").val("0");
    $("#txtDiscount").val("0");
    $("#divCardAmount").hide();
    $("#txtTotalAmount").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    //$("#divChit").hide();
    // $("#divChitDetails").hide();
    // $("#divExchangeDetails").hide();
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    $("#btnAddChit").show();
    $("#btnUpdateChit").hide();
    gSalesList = [];
    gChitList = [];
    gExchangeList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#divCardNo").hide();
    $("#txtCustomerCode").val("");
    $("#txtName").val("");
    $("#txtPhone").val("");
    $("#txtAddress").val("");
    $("#txtCardAmount").val("0");
    $("#txtReturnAmount").val("0");
    $("#txtExchangeAmount").val("0");
    $("#txtBalanceAmount").val("0");
    $("#txtSchemeAmount").val("0");

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtInvoiceDate").val(d + "/" + m + "/" + y);
    $("#txtInvoiceNo").attr("disabled", false);
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


function GetEmployeeListsalesMan(ddlname) {
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
                                    //$(sControlName).append("<option value='" + obj[index].UserID + "'>" + obj[index].SalesManCode + "</option>");
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

    if ($("#hdntransID").val() == "0" || $("#hdntransID").val() == undefined || $("#hdntransID").val() == null) {
        $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    }
    else { $("#divBarcode").removeClass('has-error'); }

    if ($("#hdnStockID").val() == "0" || $("#hdnStockID").val() == undefined || $("#hdnStockID").val() == "" || $("#hdnStockID").val() == null) {
        $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    }
    else { $("#divBarcode").removeClass('has-error'); }


    var iStockCount = 0;
    for (var i = 0; i < gSalesList.length; i++) {
        if (gSalesList[i].Stock.StockID == $("#hdnStockID").val())
            iStockCount = iStockCount + 1;
    }
    if (this.id == "btnAddMagazine") {
        if (iStockCount > 0) {
            $.jGrowl("Product already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
        } else { $("#divBarcode").removeClass('has-error'); }
    }
    else {
        if (iStockCount > 1) {
            $.jGrowl("Product already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
        } else { $("#divBarcode").removeClass('has-error'); }
    }

    var ObjData = new Object();
    ObjData.SalesTransID = 0;
    var ObjStock = new Object();
    ObjStock.StockID = $("#hdnStockID").val();
    ObjData.Stock = ObjStock;
    ObjData.Quantity = parseInt($("#txtQuantity").val());
    ObjData.Rate = parseFloat($("#txtSellingPrice").val());
    ObjData.Making = parseFloat($("#txtMaking").val());
    ObjData.ProductName = $("#txtProduct").val();
    ObjData.CategoryName = $("#txtCategory").val();
    ObjData.Barcode = $("#txtBarcode").val();
    ObjData.Subtotal = parseFloat($("#txtTotal").val());
    ObjData.StonePrice = parseFloat($("#txtStoneAmount").val());
    ObjData.TotalWeight = parseFloat($("#txtGrossWeight").val()) + parseFloat($("#txtWastage").val());
    ObjData.NetWeight = parseFloat($("#txtGrossWeight").val());
    ObjData.StoneWeight = parseFloat($("#txtStoneWeight").val());
    ObjData.WastagePercent = parseFloat($("#txtWastePercent").val());
    ObjData.Wastage = parseFloat($("#txtGrossWeight").val()) * parseFloat($("#txtWastePercent").val()) / 100;
    ObjData.WastageAmt = parseFloat($("#txtWastage").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gSalesList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.SalesTransID = 0;
        ObjData.StatusFlag = "I";
        AddSalesData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnSalesID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesID = $("#hdnSalesID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesID = 0;
        }
        Update_Sales(ObjData);
    }
    CalculateAmount();
    ClearSalesFields();
    $("#ddlDescriptionName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});

$("#btnAddChit,#btnUpdateChit").click(function () {

    if ($("#ddlChit").val() == "0" || $("#ddlChit").val() == undefined || $("#ddlChit").val() == null) {
        $.jGrowl("Please select Scheme", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divChit").addClass('has-error'); $("#ddlChit").focus(); return false;
    } else { $("#divChit").removeClass('has-error'); }

    var iStockCount = 0;
    for (var i = 0; i < gChitList.length; i++) {
        if (gChitList[i].Register.RegisterID == $("#ddlChit").val())
            iStockCount = iStockCount + 1;
    }
    if (this.id == "btnAddChit") {
        if (iStockCount > 0) {
            $.jGrowl("Scheme account already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divChit").addClass('has-error'); $("#ddlChit").focus(); return false;
        } else { $("#divChit").removeClass('has-error'); }
    }
    else {
        if (iStockCount > 1) {
            $.jGrowl("Scheme account already added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divChit").addClass('has-error'); $("#ddlChit").focus(); return false;
        } else { $("#divChit").removeClass('has-error'); }
    }

    var ObjData = new Object();
    ObjData.SalesChitID = 0;
    var ObjRegister = new Object();
    ObjRegister.RegisterID = $("#ddlChit").val();
    ObjRegister.AccountNo = $("#ddlChit option:selected").text();
    var objChit = new Object();
    objChit.ChitName = $("#txtChitName").val();
    ObjRegister.Chit = objChit;
    ObjData.Register = ObjRegister;
    ObjData.ChitAmount = parseFloat($("#txtChitAmount").val());

    if (this.id == "btnAddChit") {
        ObjData.sNO = gChitList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.SalesChitTransID = 0;
        ObjData.StatusFlag = "I";
        AddChitData(ObjData);
    }
    else if (this.id == "btnUpdateChit") {
        ObjData.sNO = $("#hdnCTSNo").val();
        if ($("#hdnSalesID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesID = $("#hdnSalesID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesID = 0;
        }
        Update_Chit(ObjData);
    }
    CalculateAmount();
    ClearChitFields();
    $("#ddlChit").focus();
    $("#btnAddChit").show();
    $("#btnUpdateChit").hide();
});

function ClearChitFields() {
    $("#btnAddChit").show();
    $("#btnUpdateChit").hide();
    $("#ddlChit").val(0).change();
    $("#txtChitName").val(0).change();
    $("#txtChitAmount").val("0");
    $("#divChit").removeClass('has-error');
    return false;
}

function ClearSalesFields() {
    $("#btnAddSales").show();
    $("#btnUpdateSales").hide();

    $("#txtTotal").val("0");
    $("#txtStoneAmount").val("0");
    $("#txtNetWeight").val("0");
    $("#txtSellingPrice").val("0");
    $("#txtMaking").val("0");
    $("#txtGrossWeight").val("0");
    $("#txtProduct").val("");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");
    $("#txtCategory").val("");
    $("#txtStoneWeight").val("");
    $("#txtWastage").val("");
    $("#txtWastePercent").val("");
    $("#hdnStockID").val("");
    $("#divSelectDescriptionName").show();
    $("#divDescriptionName").removeClass('has-error');
    $("#divAmount").removeClass('has-error');
    $("#divBarcode").removeClass('has-error');
    return false;
}
function AddSalesData(oData) {
    gSalesList.push(oData);
    DisplaySalesList(gSalesList);
    return false;
}

function AddChitData(oData) {
    gChitList.push(oData);
    DisplayChitList(gChitList);
    return false;
}
function DisplayChitList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divChitList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divChitList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblChitList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Account No</th>";
        sTable += "<th class='" + sColorCode + "'>Chit Name</th>";
        sTable += "<th class='" + sColorCode + "'>Amount</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblChitList_body'>";
        sTable += "</tbody></table>";
        $("#divChitList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Register.AccountNo + "</td>";
                sTable += "<td>" + gData[i].Register.Chit.ChitName + "</td>";
                sTable += "<td>" + gData[i].ChitAmount + "</td>";

                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_ChitDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_ChitDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblChitList_body").append(sTable);
            }
        }
    }
    else { $("#divChitList").empty(); }

    return false;
}
function DisplaySalesList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divSalesList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divSalesList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblSalesList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Barcode</th>";
        sTable += "<th class='" + sColorCode + "'>Category</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Gross Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Stone Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Net Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Stone Price</th>";
        sTable += "<th class='" + sColorCode + "'>Wastage %</th>";
        sTable += "<th class='" + sColorCode + "'>Wastage</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblSalesList_body'>";
        sTable += "</tbody></table>";
        $("#divSalesList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Barcode + "</td>";
                sTable += "<td>" + gData[i].CategoryName + "</td>";
                sTable += "<td>" + gData[i].ProductName + "</td>";
                sTable += "<td>" + gData[i].NetWeight + "</td>";
                sTable += "<td>" + gData[i].StoneWeight + "</td>";
                sTable += "<td>" + (parseFloat(gData[i].NetWeight) + parseFloat(gData[i].StoneWeight)) + "</td>";
                sTable += "<td>" + gData[i].StonePrice + "</td>";
                sTable += "<td>" + gData[i].WastagePercent + "</td>";
                sTable += "<td>" + gData[i].WastageAmt + "</td>";

                sTable += "<td>" + gData[i].Subtotal + "</td>";

                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_SalesDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_SalesDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblSalesList_body").append(sTable);
            }
        }
    }
    else { $("#divSalesList").empty(); }

    return false;
}

function Bind_ChitByID(ID, data) {
    $("#btnAddChit").hide();
    $("#btnUpdateChit").show();
    $("#ddlChit").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnSalesID").val(data[i].SalesID);
            $("#ddlChit").val(data[i].Register.RegisterID).change();

            //$("#hdnStockID").val(data[i].StockID)
            //$("#txtProduct").val(data[i].ProductName);
            //$("#txtCategory").val(data[i].CategoryName);
            //$("#txtGrossWeight").val(data[i].NetWeight);
            //$("#txtSellingPrice").val(data[i].SellingPrice);
            //$("#txtMaking").val(data[i].Making);
            //$("#txtNetWeight").val(data[i].TotalWeight);
            //$("#txtStoneAmount").val(data[i].StonePrice);
            //$("#hdntransID").val(data[i].StockID)

            //$("#txtStoneWeight").val(data[i].StoneWeight);
            //$("#txtWastePercent").val(data[i].WastagePercent);
            //$("#txtWastage").val(data[i].Wastage);
            // $("#txtTotal").val(obj.SellingPrice);

            //var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0;
            //if ($("#txtSellingPrice").val() > 0)
            //    iSelling = $("#txtSellingPrice").val();

            //if ($("#txtMaking").val() > 0)
            //    iMaking = $("#txtMaking").val();

            //if ($("#txtNetWeight").val() > 0)
            //    iWeight = $("#txtNetWeight").val();

            //iTotal = parseFloat(iMaking) + (parseFloat(iWeight) * parseFloat(iSelling));

            //$("#txtTotal").val(parseFloat(iTotal).toFixed(2));
        }
    }
    return false;
}

function Bind_SalesByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#txtBarcode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            $("#hdnSalesID").val(data[i].SalesID);
            $("#txtBarcode").val(data[i].Barcode).change();
            $("#txtSellingPrice").val(data[i].Rate);


            var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0; var iWastage = 0; var iGross = 0;
            if ($("#txtSellingPrice").val() > 0)
                iSelling = $("#txtSellingPrice").val();

            if ($("#txtMaking").val() > 0)
                iMaking = $("#txtMaking").val();

            if ($("#txtStoneAmount").val() > 0)
                iStoneAmt = $("#txtStoneAmount").val();

            if ($("#txtStoneWeight").val() > 0)
                iStoneWt = $("#txtStoneWeight").val();

            if ($("#txtNetWeight").val() > 0)
                iWeight = $("#txtNetWeight").val();

            //$("#txtWastage").val(parseFloat(iSelling) * obj.Wastage);

            if ($("#txtWastage").val() > 0)
                iWastage = $("#txtWastage").val();

            if ($("#txtGrossWeight").val() > 0)
                iGross = $("#txtGrossWeight").val();

            iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iGross) * parseFloat(iSelling)) + (parseFloat(iStoneAmt)) + (parseFloat(iWastage));


            //var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0;
            //if ($("#txtSellingPrice").val() > 0)
            //    iSelling = $("#txtSellingPrice").val();

            //if ($("#txtMaking").val() > 0)
            //    iMaking = $("#txtMaking").val();

            //if ($("#txtStoneAmount").val() > 0)
            //    iStoneAmt = $("#txtStoneAmount").val();

            //if ($("#txtStoneWeight").val() > 0)
            //    iStoneWt = $("#txtStoneWeight").val();

            //if (data[i].TotalWeight > 0)
            //    iWeight = data[i].TotalWeight;

          //  iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iWeight) * parseFloat(iSelling)) + (parseFloat(iStoneWt) * parseFloat(iStoneAmt));
            $("#txtTotal").val(parseFloat(iTotal).toFixed(2));
        }
    }
    return false;
}
function Update_Chit(oData) {
    for (var i = 0; i < gChitList.length; i++) {
        if (gChitList[i].sNO == oData.sNO) {
            gChitList[i].SalesID = oData.SalesID;
            var iRegister = new Object();
            iRegister.AccountNo = oData.Register.AccountNo
            iRegister.RegisterID = oData.Register.RegisterID
            iRegister.Chit.ChitName = oData.Register.Chit.ChitName
            gChitList[i].Register = iRegister;

            gChitList[i].StatusFlag = oData.StatusFlag;
            gChitList[i].ChitAmount = oData.ChitAmount;
        }
    }
    DisplayChitList(gChitList);
    $("#btnAddChit").show();
    $("#btnUpdateChit").hide();
    ClearChitFields();
    $("#ddlChit").focus();
    return false;
}
function Update_Sales(oData) {
    for (var i = 0; i < gSalesList.length; i++) {
        if (gSalesList[i].sNO == oData.sNO) {
            gSalesList[i].SalesID = oData.SalesID;
            gSalesList[i].ProductName = oData.ProductName;
            gSalesList[i].CategoryName = oData.CategoryName;

            gSalesList[i].Subtotal = oData.Subtotal;
            gSalesList[i].Barcode = oData.Barcode;
            gSalesList[i].StatusFlag = oData.StatusFlag;
            gSalesList[i].StonePrice = oData.StonePrice;
            gSalesList[i].TotalWeight = oData.TotalWeight;
            gSalesList[i].NetWeight = oData.NetWeight;
            gSalesList[i].StoneWeight = oData.StoneWeight;
            gSalesList[i].WastagePercent = oData.WastagePercent;
            gSalesList[i].Wastage = oData.Wastage;
            gSalesList[i].WastageAmt = oData.WastageAmt;
            gSalesList[i].Rate = oData.Rate;
            gSalesList[i].Making = oData.Making;
        }
    }
    DisplaySalesList(gSalesList);
    $("#btnAddSales").show();
    $("#btnUpdateSales").hide();
    ClearSalesFields();
    $("#txtBarcode").focus();
    return false;
}
function Delete_SalesDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gSalesList.length; i++) {
            if (gSalesList[i].SNo == ID) {
                var index = jQuery.inArray(gSalesList[i].valueOf("SNo"), gSalesList);
                if (gSalesList[i].SNo > 0) {
                    gSalesList[i].StatusFlag = "D";
                } else {
                    gSalesList.splice(index, 1);
                }
                $("#divSalesList").empty();
                DisplaySalesList(gSalesList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function Delete_ChitDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gChitList.length; i++) {
            if (gChitList[i].SNo == ID) {
                var index = jQuery.inArray(gChitList[i].valueOf("SNo"), gChitList);
                if (gChitList[i].SNo > 0) {
                    gChitList[i].StatusFlag = "D";
                } else {
                    gChitList.splice(index, 1);
                }
                $("#divChitList").empty();
                DisplayChitList(gChitList);
                CalculateAmount();
            }
        }
    }
    return false;
}

//$("#btnAddExchange,#btnUpdateExchange").click(function () {

//    //if ($("#hdntransID").val() == "0" || $("#hdntransID").val() == undefined || $("#hdntransID").val() == null) {
//    //    $.jGrowl("Please Enter Product", { sticky: false, theme: 'warning', life: jGrowlLife });
//    //    $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
//    //}
//    //else { $("#divBarcode").removeClass('has-error'); }

//    //if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null) {
//    //    $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
//    //    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
//    //} else { $("#divAmount").removeClass('has-error'); }

//    if ($("#ddlCategory").val() == "0" || $("#ddlCategory").val() == undefined || $("#ddlCategory").val() == null) {
//        $.jGrowl("Please select Category", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divCategory").addClass('has-error'); $("#ddlCategory").focus(); return false;
//    } else { $("#divCategory").removeClass('has-error'); }

//    if ($("#ddlProduct").val() == "0" || $("#ddlProduct").val() == undefined || $("#ddlProduct").val() == null) {
//        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divProduct").addClass('has-error'); $("#ddlProduct").focus(); return false;
//    } else { $("#divProduct").removeClass('has-error'); }

//    if ($("#txtENetWeight").val().trim() == "" || $("#txtENetWeight").val().trim() == undefined) {
//        $.jGrowl("Please enter Net Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divENetWeight").addClass('has-error'); $("#txtENetWeight").focus(); return false;
//    } else { $("#divENetWeight").removeClass('has-error'); }

//    if ($("#txtEKarat").val().trim() == "" || $("#txtEKarat").val().trim() == undefined) {
//        $.jGrowl("Please enter Karat", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divEKarat").addClass('has-error'); $("#txtEKarat").focus(); return false;
//    } else { $("#divEKarat").removeClass('has-error'); }

//    if ($("#txtMeltingWeight").val().trim() == "" || $("#txtMeltingWeight").val().trim() == undefined) {
//        $.jGrowl("Please enter Melting Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divMeltingWeight").addClass('has-error'); $("#txtMeltingWeight").focus(); return false;
//    } else { $("#divMeltingWeight").removeClass('has-error'); }

//    if ($("#txtCurrentRate").val().trim() == "" || $("#txtCurrentRate").val().trim() == undefined) {
//        $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
//        $("#divCurrentRate").addClass('has-error'); $("#txtCurrentRate").focus(); return false;
//    } else { $("#divCurrentRate").removeClass('has-error'); }



//    var ObjData = new Object();
//    ObjData.ExchangeTransID = 0;
//    var ObjProduct = new Object();
//    ObjProduct.ProductID = $("#ddlProduct").val();
//    ObjProduct.ProductName = $("#ddlProduct option:selected").text();
//    ObjData.Product = ObjProduct;

//    var ObjCategory = new Object();
//    ObjCategory.CategoryID = $("#ddlCategory").val();
//    ObjCategory.CategoryName = $("#ddlCategory option:selected").text();
//    ObjData.Category = ObjCategory;

//    ObjData.Karat = $("#txtEKarat").val();
//    ObjData.CurrentRate = parseFloat($("#txtCurrentRate").val());
//    ObjData.GrossWeight = parseFloat($("#txtEGrossWeight").val());
//    ObjData.MeltingWeight = parseFloat($("#txtMeltingWeight").val());
//    ObjData.NetWeight = parseFloat($("#txtENetWeight").val());
//    ObjData.Amount = parseFloat($("#txtETotal").val());
//    if (this.id == "btnAddExchange") {
//        ObjData.sNO = gExchangeList.max() + 1;
//        ObjData.SNo = ObjData.sNO;
//        ObjData.ExchangeTransID = 0;
//        ObjData.StatusFlag = "I";
//        AddExchangeData(ObjData);
//    }
//    else if (this.id == "btnUpdateExchange") {
//        ObjData.sNO = $("#hdnExchangeSNo").val();
//        if ($("#hdnExchangeID").val() > 0) {
//            ObjData.StatusFlag = "U";
//            ObjData.ExchangeID = $("#hdnExchangeID").val();
//        }
//        else {
//            ObjData.StatusFlag = "I";
//            ObjData.ExchangeID = 0;
//        }
//        Update_Exchange(ObjData);
//    }
//    CalculateAmount();
//    ClearExchangeFields();
//    $("#ddlddlCategory").focus();
//    //$("#btnAddMagazine").show();
//    //$("#btnUpdateMagazine").hide();
//});
//function ClearExchangeFields() {
//    $("#btnAddExchange").show();
//    $("#btnUpdateExchange").hide();

//    $("#txtETotal").val("0");
//    $("#txtCurrentRate").val("0");
//    $("#txtEGrossWeight").val("0");
//    $("#txtMeltingWeight").val("0");
//    $("#txtEKarat").val("");
//    $("#txtENetWeight").val("");
//    $("#ddlCategory").val(0).change();
//    $("#ddlProduct").val(0).change();
//    $("#ddlProduct").empty();
//    $("#divCategory").removeClass('has-error');
//    $("#divProduct").removeClass('has-error');
//    $("#divENetWeight").removeClass('has-error');
//    $("#divEKarat").removeClass('has-error');
//    $("#divMeltingWeight").removeClass('has-error');
//    $("#divCurrentRate").removeClass('has-error');
//    return false;
//}
//function AddExchangeData(oData) {
//    gExchangeList.push(oData);
//    DisplayExchangeList(gExchangeList);
//    return false;
//}
//function DisplayExchangeList(gData) {
//    var sTable = "";
//    var sCount = 1;
//    var sColorCode = "bg-info";

//    if (gData.length >= 5)
//    { $("#divExchangeList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
//    else
//    { $("#divExchangeList").css({ 'height': '', 'min-height': '' }); }

//    if (gData.length > 0) {
//        sTable = "<table id='tblExchangeList' class='table no-margin table-condensed table-hover'>";
//        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
//        sTable += "<th class='" + sColorCode + "'>Category</th>";
//        sTable += "<th class='" + sColorCode + "'>Product</th>";
//        sTable += "<th class='" + sColorCode + "'>Net Weight</th>";
//        sTable += "<th class='" + sColorCode + "'>Karat</th>";
//        sTable += "<th class='" + sColorCode + "'>Melting Weight</th>";
//        sTable += "<th class='" + sColorCode + "'>Gross Weight</th>";
//        sTable += "<th class='" + sColorCode + "'>Current Rate</th>";
//        sTable += "<th class='" + sColorCode + "'>Amount</th>";
//        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
//        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
//        sTable += "</tr></thead><tbody id='tblExchangeList_body'>";
//        sTable += "</tbody></table>";
//        $("#divExchangeList").html(sTable);
//        for (var i = 0; i < gData.length; i++) {
//            if (gData[i].StatusFlag != "D") {
//                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
//                sTable += "<td>" + gData[i].Category.CategoryName + "</td>";
//                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
//                sTable += "<td>" + gData[i].NetWeight + "</td>";
//                sTable += "<td>" + gData[i].Karat + "</td>";
//                sTable += "<td>" + gData[i].MeltingWeight + "</td>";
//                sTable += "<td>" + gData[i].GrossWeight + "</td>";
//                sTable += "<td>" + gData[i].CurrentRate + "</td>";
//                sTable += "<td>" + gData[i].Amount + "</td>";

//                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_ExchangeDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
//                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_ExchangeDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
//                sTable += "</tr>";
//                sCount = sCount + 1;
//                $("#tblExchangeList_body").append(sTable);
//            }
//        }
//    }
//    else { $("#divExchangeList").empty(); }

//    return false;
//}
function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gExchangeList);
    return false;
}
//function Bind_ExchangeByID(ID, data) {
//    $("#btnAddExchange").hide();
//    $("#btnUpdateExchange").show();
//    $("#ddlCategory").focus();

//    for (var i = 0; i < data.length; i++) {
//        if (data[i].sNO == ID) {
//            //$("#hdnOPSNo").val = null;
//            $("#hdnExchangeSNo").val(ID);
//            $("#hdnSalesID").val(data[i].SalesID);
//            //$("#ddlDescriptionName").val(data[i].Description.DescriptionID).change();

//            $("#ddlCategory").val(data[i].Category.CategoryID).change();
//            $("#ddlProduct").val(data[i].Product.ProductID).change();
//            $("#txtEKarat").val(data[i].Karat);
//            $("#txtEGrossWeight").val(data[i].GrossWeight);
//            $("#txtCurrentRate").val(data[i].CurrentRate);
//            $("#txtMeltingWeight").val(data[i].MeltingWeight);
//            $("#txtENetWeight").val(data[i].NetWeight);
//            $("#txtETotal").val(data[i].Amount);
//        }
//    }
//    return false;
//}
//function Update_Exchange(oData) {
//    for (var i = 0; i < gExchangeList.length; i++) {
//        if (gExchangeList[i].sNO == oData.sNO) {
//            gExchangeList[i].ExchangeID = oData.ExchangeID;
//            gExchangeList[i].ProductName = oData.Product.ProductName;
//            gExchangeList[i].CategoryName = oData.Category.CategoryName;
//            gExchangeList[i].ProductID = oData.Product.ProductID;
//            gExchangeList[i].CategoryID = oData.Category.CategoryID;
//            gExchangeList[i].Subtotal = oData.Subtotal;
//            gExchangeList[i].StatusFlag = oData.StatusFlag;
//            gExchangeList[i].NetWeight = oData.NetWeight;
//            gExchangeList[i].Karat = oData.Karat;
//            gExchangeList[i].MeltingWeight = oData.MeltingWeight;
//            gExchangeList[i].GrossWeight = oData.GrossWeight;
//            gExchangeList[i].CurrentRate = oData.CurrentRate;
//            gExchangeList[i].Amount = oData.Amount;
//        }
//    }
//    DisplayExchangeList(gExchangeList);
//    $("#btnAddExchange").show();
//    $("#btnUpdateExchange").hide();
//    ClearExchangeFields();
//    $("#ddlCategory").focus();
//    return false;
//}
//function Delete_ExchangeDetail(ID) {
//    if (ID == 0)
//        return false;

//    if (confirm('Are you sure to delete the selected record ?')) {
//        for (var i = 0; i < gExchangeList.length; i++) {
//            if (gExchangeList[i].SNo == ID) {
//                var index = jQuery.inArray(gExchangeList[i].valueOf("SNo"), gExchangeList);
//                if (gExchangeList[i].SNo > 0) {
//                    gExchangeList[i].StatusFlag = "D";
//                } else {
//                    gExchangeList.splice(index, 1);
//                }
//                $("#divExchangeList").empty();
//                DisplayExchangeList(gExchangeList);
//                CalculateAmount();
//            }
//        }
//    }
//    return false;
//}

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetRecord() {
    dProgress(true);
    //alert(useridd);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopSales",
        data: JSON.stringify({ PatientID: 11 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
           // alert(" data : " + data);
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

                                var table = "<tr id='" + obj[index].SalesID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].InvoiceNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sInvoiceDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceAmount + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                //if (ActionUpdate == "1")
                                //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                //else
                                //{ table += "<td></td>"; }

                                if (ActionDelete == "1" && obj[index].IsCancelled == "0")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";

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
                                if (ActionUpdate == "1")
                                { EditRecord($(this).parent().parent()[0].id); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintSales").click(function () {
                                SetSessionValue("SalesID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesInvoice.aspx", "MsgWindow");
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
                          { "sWidth": "10%" },
                          { "sWidth": "10%" },
                          { "sWidth": "5%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "1%" },
                          //{ "sWidth": "1%" },
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
        url: "WebServices/VHMSService.svc/SearchSales",
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].SalesID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].InvoiceNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sInvoiceDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceAmount + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                //if (ActionUpdate == "1")
                                //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                //else
                                //{ table += "<td></td>"; }

                                if (ActionDelete == "1" && obj[index].IsCancelled == "0")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";


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
                                if (ActionUpdate == "1")
                                { EditRecord($(this).parent().parent()[0].id); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintSales").click(function () {
                                SetSessionValue("SalesID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesInvoice.aspx", "MsgWindow");
                                //PrintSalesDetails();
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
                          { "sWidth": "10%" },
                          { "sWidth": "10%" },
                          { "sWidth": "5%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "1%" },
                          //{ "sWidth": "1%" },
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

function CalculateExchange() {


}
function CalculateAmount() {
    var iSalesAmount = 0; var iExchangeAmount = 0;
    var iTotal = 0; var iChitAmount = 0; var iCardAmount = 0;
    var iDiscount = 0; var iReturnAmt = 0; var iRoundoffAmt = 0; var iExchange=0 ;

    for (var i = 0; i < gSalesList.length; i++) {
        if (gSalesList[i].StatusFlag != "D")
            iSalesAmount = iSalesAmount + parseFloat(gSalesList[i].Subtotal);
    }
    $("#txtSubtotal").val(iSalesAmount);

    //for (var i = 0; i < gExchangeList.length; i++) {
    //    if (gExchangeList[i].StatusFlag != "D")
    //        iExchangeAmount = iExchangeAmount + parseFloat(gExchangeList[i].Amount);
    //}
    //$("#txtExchangeAmount").val(iExchangeAmount);

    for (var i = 0; i < gChitList.length; i++) {
        if (gChitList[i].StatusFlag != "D")
            iChitAmount = iChitAmount + parseFloat(gChitList[i].ChitAmount);
    }
    $("#txtSchemeAmount").val(iChitAmount);

    if ($("#txtRoundoffAmount").val() > 0)
        iRoundoffAmt = $("#txtRoundoffAmount").val();

    if ($("#txtReturnAmount").val() > 0)
        iReturnAmt = $("#txtReturnAmount").val();

    if ($("#txtExchangeAmount").val() > 0)
        iExchange = $("#txtExchangeAmount").val()

    if ($("#txtCardAmount").val() > 0)
        iCardAmount = $("#txtCardAmount").val();


    if ($("#txtDiscountPercent").val() > 0)
        iDiscount = $("#txtDiscountPercent").val();

    $("#txtDiscount").val((iSalesAmount * parseFloat(iDiscount) / 100).toFixed(2));
    iSalesAmount = iSalesAmount - parseFloat($("#txtDiscount").val());

    $("#txtTaxAmount").val((iSalesAmount * parseFloat($("#txtTaxPercent").text()) / 100).toFixed(2));
    $("#txtCGSTAmount").val((iSalesAmount * parseFloat($("#txtCGSTPercent").text()) / 100).toFixed(2));
    $("#txtSGSTAmount").val((iSalesAmount * parseFloat($("#txtSGSTPercent").text()) / 100).toFixed(2));
    $("#txtIGSTAmount").val((iSalesAmount * parseFloat($("#txtIGSTPercent").text()) / 100).toFixed(2));
    iSalesAmount = parseFloat(iSalesAmount) + parseFloat($("#txtTaxAmount").val()) - (parseFloat(iChitAmount) + parseFloat(iReturnAmt) + parseFloat(iRoundoffAmt)+ parseFloat(iExchange));

    $("#txtTotalAmount").val(parseFloat(iSalesAmount).toFixed(2));

    $("#txtBalanceAmount").val(parseFloat(iSalesAmount - iCardAmount).toFixed(2));
}

function CalculateChitAmount() {
    var iSalesAmount = 0;
    var iTotal = 0;
    var iDiscount = 0;
    for (var i = 0; i < gChitList.length; i++) {
        if (gChitList[i].StatusFlag != "D")
            iSalesAmount = iSalesAmount + parseFloat(gChitList[i].Subtotal);
    }
    //$("#txtSubtotal").val(iSalesAmount);

    //if ($("#txtDiscountPercent").val() > 0)
    //    iDiscount = $("#txtDiscountPercent").val();
    //$("#txtDiscount").val((iSalesAmount * parseFloat(iDiscount) / 100).toFixed(2));
    //iSalesAmount = iSalesAmount - parseFloat($("#txtDiscount").val());

    //$("#txtTaxAmount").val((iSalesAmount * parseFloat($("#txtTaxPercent").text()) / 100).toFixed(2));
    //$("#txtCGSTAmount").val((iSalesAmount * parseFloat($("#txtCGSTPercent").text()) / 100).toFixed(2));
    //$("#txtSGSTAmount").val((iSalesAmount * parseFloat($("#txtSGSTPercent").text()) / 100).toFixed(2));
    //$("#txtIGSTAmount").val((iSalesAmount * parseFloat($("#txtIGSTPercent").text()) / 100).toFixed(2));
    //iSalesAmount = iSalesAmount + parseFloat($("#txtTaxAmount").val());

    $("#txtTotalChitAmount").val(parseFloat(iSalesAmount).toFixed(2));
}

$("#btnSave,#btnUpdate").click(function () {
    
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

        if (confirm('Are you sure to save?')) { }
        else { return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }

        if (confirm('Are you sure to Update?')) { }
        else { return false; }
    }

    var d1 = Date.parse($("#hdnOpeningDate").val());
    var d2 = Date.parse($("#txtInvoiceDate").val());
    if (d1 < d2) {
        $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
        return false;
    }

    if ($("#txtInvoiceDate").val().trim() == "" || $("#txtInvoiceDate").val().trim() == undefined) {
        $.jGrowl("Please select Sales Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divInvoiceDate").addClass('has-error'); $("#txtInvoiceDate").focus(); return false;
    }
    else { $("#divInvoiceDate").removeClass('has-error'); }

    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divtax").addClass('has-error'); $("#ddlTax").focus(); return false;
    } else { $("#divtax").removeClass('has-error'); }

    if ($("#hdnCustomerID").val() == "0" || $("#txtName").val() == undefined || $("#txtName").val() == null || $("#txtName").val().trim() == "") {
        $.jGrowl("Please Enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomer").addClass('has-error'); $("#txtOPDNo").focus(); return false;
    }
    else { $("#divCustomer").removeClass('has-error'); }

    if (gSalesList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    if ($("#ddlEmployee").val() == "0" || $("#ddlEmployee").val() == undefined || $("#ddlEmployee").val() == null) {
        $.jGrowl("Please select Employee", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divEmployee").addClass('has-error'); $("#ddlEmployee").focus(); return false;
    } else { $("#divEmployee").removeClass('has-error'); }

    if ($("#ddlSalesMan").val() == "0" || $("#ddlSalesMan").val() == undefined || $("#ddlSalesMan").val() == null) {
        $.jGrowl("Please select SalenMan Code", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSalesMan").addClass('has-error'); $("#ddlSalesMan").focus(); return false;
    } else { $("#divSalesMan").removeClass('has-error'); }

    var gCount = 0;
    for (var i = 0; i < gSalesList.length; i++) {
        if (gSalesList[i].StatusFlag != "D")
            gCount++;

    }
    if (gCount == 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    if ($("#chkChit").prop("checked") == true) {
        var gChitCount = 0;
        for (var i = 0; i < gChitList.length; i++) {
            if (gChitList[i].StatusFlag != "D")
                gChitCount++;

        }
        if (gChitCount == 0) {
            $.jGrowl("No Scheme has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtBarcode").focus(); return false;
        }
    }

    var iSalesAmount = 0;
    for (var i = 0; i < gSalesList.length; i++)
        iSalesAmount = iSalesAmount + parseFloat(gSalesList[i].Subtotal);

    var ObjSales = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#hdnCustomerID").val();
    ObjSales.Customer = ObjCustomer;

    var Objtax = new Object();
    Objtax.TaxID = $("#ddlTax").val();
    ObjSales.Tax = Objtax;

    var ObjChit = new Object();
    ObjChit.RegisterID = $("#ddlChit").val();
    ObjSales.Register = ObjChit;

    var ObjEmployee = new Object();
    ObjEmployee.UserID = $("#ddlEmployee").val();
    ObjSales.Employee = ObjEmployee;

    ObjEmployee.UserID1 = $("#ddlSalesMan").val();
    ObjSales.SalesManID= ObjEmployee;
    

    ObjSales.SalesID = 0;
    ObjSales.InvoiceNo = $("#txtInvoiceNo").val().trim();
    ObjSales.sInvoiceDate = $("#txtInvoiceDate").val().trim();
    ObjSales.DiscountAmount = parseFloat($("#txtDiscount").val());
    ObjSales.DiscountPercent = parseFloat($("#txtDiscountPercent").val());
    ObjSales.TotalAmount = parseFloat($("#txtSubtotal").val());
    ObjSales.InvoiceAmount = parseFloat($("#txtTotalAmount").val());
    ObjSales.TaxAmount = parseFloat($("#txtTaxAmount").val());
    ObjSales.TaxPercent = parseFloat($("#txtTaxPercent").text());
    ObjSales.CGSTAmount = parseFloat($("#txtCGSTAmount").val());
    ObjSales.SGSTAmount = parseFloat($("#txtSGSTAmount").val());
    ObjSales.IGSTAmount = parseFloat($("#txtIGSTAmount").val());

    ObjSales.PaidAmount = ObjSales.InvoiceAmount;
    ObjSales.BalanceAmount = 0;
    ObjSales.Status = "Closed";
    ObjSales.CardNo = $("#txtCardNo").val();
    ObjSales.CardAmount = $("#txtCardAmount").val();
    ObjSales.ReturnAmount = $("#txtReturnAmount").val();
    ObjSales.Exchange = $("#txtExchangeAmount").val();
    ObjSales.Roundoff = $("#txtRoundoffAmount").val();
    ObjSales.PaymentMode = $("#ddlPaymentMode option:selected").text();
    ObjSales.SalesTrans = gSalesList;
    ObjSales.SalesChitTrans = gChitList;
    //ObjSales.ExchangeTrans = gExchangeList;
    if ($("#hdnSalesID").val() > 0) {
        ObjSales.SalesID = $("#hdnSalesID").val();
        sMethodName = "UpdateSales";
    }
    else {
        sMethodName = "AddSales";
        ObjSales.SalesID = 0
    }

    SaveandUpdateSales(ObjSales, sMethodName);

});
function SaveandUpdateSales(ObjSales, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjSales }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {

                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divSales").hide();
                        if (sMethodName == "AddSales") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnSalesID").val(objResponse.Value);
                            GetSMSRecord(objResponse.Value, "");
                        }
                        else if (sMethodName == "UpdateSales")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                        SetSessionValue("SalesID", $("#hdnSalesID").val());
                        //var r = confirm("Do You Want to Add Exchange for this Invoice?");
                        //if (r == true) {
                        //    SetSessionValue("SalesID", $("#hdnSalesID").val());
                        //    var myWindow = window.open("frmExchange.aspx", "MsgWindow");
                        //}
                        //else {
                        SetSessionValue("SalesID", $("#hdnSalesID").val());
                        var myWindow = window.open("PrintSalesInvoice.aspx", "MsgWindow");
                        //}
                        $("#hdnSalesID").val("0");
                        ClearSalesTab();
                        $("#btnList").click();
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

function SendSMS(MobileNo, APLUrl) {
    
    var url = APLUrl;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        success: function (res) {
            console.log(res);
        }
    });
}

function GetSMSRecord(id, messageType) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesByID",
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

                            var SMSMsg = "Dear " + obj.Customer.CustomerName + ", Thank you very much for your valuable purchase of Rs." + obj.InvoiceAmount + " against Invoice no: "+ obj.InvoiceNo +", Dated "+ obj.sInvoiceDate +". Visit again - SVS Jewellers.";
                            //SMSMsg = SMSMsg.replace("#jobcardno#", obj.JobCardNo);
                            ////var test = ;
                            //if (SMSMsg.match(/#brandmodel#/))
                            //    SMSMsg = SMSMsg.replace("#brandmodel#", obj.Model.Brand.BrandName + " - " + obj.Model.ModelName);

                            //var sComplaints = obj.sComplaints.split(',');
                            //var Complaint = "";
                            //for (var i = 0; i < sComplaints.length; i++)
                            //    Complaint += CapitalizeFirstLetterEachWord(sComplaints[i]);
                            ////if (SMSMsg.includes("#complaints#"))
                            //if (SMSMsg.match(/#complaints#/))
                            //    SMSMsg = SMSMsg.replace("#complaints#", Complaint);
                            //if (SMSMsg.match(/#companyname#/))
                            //    SMSMsg = SMSMsg.replace("#companyname#", CompanyName);
                            //if (SMSMsg.match(/#phone#/))
                            //    SMSMsg = SMSMsg.replace("#phone#", PhoneNo1);
                            ////if (SMSMsg.includes("#servicecost#"))
                            //if (SMSMsg.match(/#servicecost#/))
                            //    SMSMsg = SMSMsg.replace("#servicecost#", obj.InvoiceAmount);
                            ////var SMSUsername = SMS;
                            ////var SMSPassword = Session["SMSPassword"];
                            var APILink = SMSurl;
                           
                            if (APILink.match(/#message#/))
                                APILink = APILink.replace("#message#", SMSMsg);
                            if (APILink.match(/#uname#/))
                                APILink = APILink.replace("#uname#", SMSusername);
                            if (APILink.match(/#pwd#/))
                                APILink = APILink.replace("#pwd#", SMSpassword);
                            if (APILink.match(/#mobile#/))
                                APILink = APILink.replace("#mobile#", obj.Customer.MobileNo);
                            if (APILink.match(/#sendername#/))
                                APILink = APILink.replace("#sendername#", SMSsendername);

                            SendSMS(obj.Customer.MobileNo, APILink);
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

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesByID",
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

                            $("#txtInvoiceNo").attr("disabled", true);
                            // $("#ddlPatient").attr("disabled", true);

                            $("#hdnSalesID").val(obj.SalesID)
                            $("#hdnCustomerID").val(obj.Customer.CustomerID).change();
                            $("#ddlTax").val(obj.Tax.TaxID).change();
                            $("#ddlEmployee").val(obj.Employee.UserID).change();
                            $("#ddlSalesMan").val(obj.Employee.UserID1).change();
                            $("#txtInvoiceNo").val(obj.InvoiceNo);
                            $("#txtInvoiceDate").val(obj.sInvoiceDate);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscount").val(obj.DiscountAmount);
                            $("#txtTotalAmount").val(obj.InvoiceAmount);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtTaxAmount").val(obj.TaxAmount);
                            // $("#txtTaxPercent").text(obj.TaxPercent);
                            $("#txtCGSTAmount").val(obj.CGSTAmount);
                            $("#txtSGSTAmount").val(obj.SGSTAmount);
                            $("#txtIGSTAmount").val(obj.IGSTAmount);
                            $("#ddlPaymentMode").val(obj.PaymentMode).change();
                            $("#txtCardNo").val(obj.CardNo);
                            $("#txtAddress").val(obj.Customer.Address)
                            $("#txtPhone").val(obj.Customer.MobileNo)
                            $("#txtCustomerCode").val(obj.Customer.MobileNo).blur();
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtRoundoffAmount").val(obj.Roundoff);
                            $("#txtReturnAmount").val(obj.ReturnAmount);
                            $('#txtExchangeAmount').val(obj.Exchange);
                            $("#txtCardAmount").val(obj.CardAmount);
                            // $("#ddlChit").val(obj.Register.RegisterID).change();

                            //if (obj.SalesChitTrans.length > 0) {
                            //    $("#chkChit").prop("checked", true).change();
                            //    $("#chkSales").prop("checked", false).change();
                            //}
                            //else {
                            //    $("#chkChit").prop("checked", false).change();
                            //    $("#chkSales").prop("checked", true).change();
                            //}

                            gSalesList = [];
                            var ObjProduct = obj.SalesTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                var objStock = new Object();
                                objStock.StockID = ObjProduct[index].StockID;
                                objTemp.Stock = objStock;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Subtotal = ObjProduct[index].Subtotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.SalesID = ObjProduct[index].SalesID;
                                objTemp.SalesTransID = ObjProduct[index].SalesTransID;
                                objTemp.WastagePercent = ObjProduct[index].WastagePercent;
                                objTemp.Wastage = ObjProduct[index].Wastage;
                                objTemp.WastageAmt = ObjProduct[index].Wastage * ObjProduct[index].Rate;
                                objTemp.NetWeight = ObjProduct[index].NetWeight;
                                objTemp.TotalWeight = ObjProduct[index].TotalWeight;
                                objTemp.StonePrice = ObjProduct[index].StonePrice * ObjProduct[index].StoneQuantity;
                                objTemp.StoneWeight = ObjProduct[index].StoneWeight;
                                objTemp.Making = ObjProduct[index].Making;
                                objTemp.CategoryName = ObjProduct[index].CategoryName;
                                objTemp.ProductName = ObjProduct[index].ProductName;
                                AddSalesData(objTemp);
                            }

                            gChitList = [];
                            var ObjProduct1 = obj.SalesChitTrans;
                            for (var index = 0; index < ObjProduct1.length; index++) {
                                var objTemp1 = new Object();
                                objTemp1.sNO = index + 1;
                                objTemp1.SNo = objTemp1.sNO;
                                objTemp1.StatusFlag = "";
                                var objRegister = new Object();
                                objRegister.RegisterID = ObjProduct1[index].Register.RegisterID;
                                objRegister.AccountNo = ObjProduct1[index].Register.AccountNo;
                                var objChit = new Object();
                                objChit.ChitName = ObjProduct1[index].Register.Chit.ChitName;
                                objRegister.Chit = objChit;
                                objTemp1.Register = objRegister;
                                objTemp1.ChitAmount = parseFloat(ObjProduct1[index].ChitAmount);
                                objTemp1.SalesID = ObjProduct1[index].SalesID;
                                objTemp1.SalesChitTransID = ObjProduct1[index].SalesChitTransID;
                                AddChitData(objTemp1);
                            }

                            //gExchangeList = [];
                            //var ObjProduct2 = obj.ExchangeTrans;
                            //for (var index = 0; index < ObjProduct2.length; index++) {
                            //    var objTemp2 = new Object();
                            //    objTemp2.sNO = index + 1;
                            //    objTemp2.SNo = objTemp2.sNO;
                            //    objTemp2.StatusFlag = "";
                            //    var objCategory = new Object();
                            //    objCategory.CategoryID = ObjProduct2[index].Category.CategoryID
                            //    objCategory.CategoryName = ObjProduct2[index].Category.CategoryName;
                            //    objTemp2.Category = objCategory;
                            //    var objProduct = new Object();
                            //    objProduct.ProductID = ObjProduct2[index].Product.ProductID
                            //    objProduct.ProductName = ObjProduct2[index].Product.ProductName;
                            //    objTemp2.Product = objProduct;

                            //    objTemp2.ExchangeID = ObjProduct2[index].ExchangeID;
                            //    objTemp2.ExchangeTransID = ObjProduct2[index].ExchangeTransID;
                            //    objTemp2.NetWeight = ObjProduct2[index].NetWeight;
                            //    objTemp2.MeltingWeight = ObjProduct2[index].MeltingWeight;
                            //    objTemp2.GrossWeight = ObjProduct2[index].GrossWeight;
                            //    objTemp2.CurrentRate = ObjProduct2[index].CurrentRate;
                            //    objTemp2.Amount = ObjProduct2[index].Amount;
                            //    objTemp2.Karat = ObjProduct2[index].Karat;
                            //    AddExchangeData(objTemp2);
                            //}
                            CalculateAmount();
                            $("#txtReturnAmount").change();
                            $('#txtExchangeAmount').chnage();
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
        url: "WebServices/VHMSService.svc/DeleteSales",
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
                    else if (objResponse.Value == "Sales_R_01" || objResponse.Value == "Sales_D_01") {
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
