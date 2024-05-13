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
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    GetLedgerList("ddlLedgerName");
    GetPartyList("ddlPartyName");
    GetBankList("ddlBank");
    GetTaxList("ddlTaxName");
    GetTaxList("ddlTax");
    $("#ddlTaxName").change();
    $("#txtBillDate, #txtIssueDate, #txtBDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtBillDate, #txtIssueDate, #txtBDate").datetimepicker({
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

            GetReceivedExpense(parseInt($.cookie("ExpenseID")));
            $("#hdnExpenseID").val(parseInt($.cookie("ExpenseID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("ExpenseID", null);
    }
    $("#ddlReceiptMode").val(5).change();
    pLoadingSetup(true);
    GetRecord();

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

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnExpenseID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#txtDescription").val("");
    $("#divTab").hide();
    $("#divOPBilling").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#ddlTaxName").change();
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#divOPBillingList").empty();

    $("#txtBillDate").focus();
    $("#txtBillNo").focus();
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
    $("#hdnExpenseID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});

function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#ddlPartyName").val(null).change();
    $("#txtTotalAmount").val("0");
    $("#txtGSTNo").val("");
    $("#txtBDate").val("");
    $("#txtExpenseNo").val("");
    $("#txtRoundoff").val("0");
    $("#txtDiscountPercent").val("0");
    $("#txtDisPer").val("0");
    $("#txtDiscountAmount").val("0");
    $("#txtOPDNo").val("");
    $("#txtChequeNo").val("");
    $("#txtIssueDate").val("");
    $("#ddlPaymentStatus").val("Cleared");
    $("#ddlReceiptMode").val(5).change();
    $("#txtDescription").val("");
    $("#divChequeDetails").hide();
    gOPBillingList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    GetLedgerList("ddlLedgerName");
    $("#txtBillNo").attr("disabled", false);
    $get("imgUpload2_view").src = "";
    $get("imgUpload3_view").src = "";
    $get("imgUpload4_view").src = "";
    $("[id*=imgUpload2_view]").css("visibility", "hidden");
    $("[id*=imgUpload3_view]").css("visibility", "hidden");
    $("[id*=imgUpload4_view]").css("visibility", "hidden");
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

function GetPartyList(ddlname) {
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

function GetLedgerList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetExpenseLedger",
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
    $("#txtAmount").val(parseFloat(iSubTotal).toFixed(2));
}

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


Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {
    CalculateTrans();
    if ($("#ddlLedgerName").val() == "0" || $("#ddlLedgerName").val() == undefined || $("#ddlLedgerName").val() == null) {
        $.jGrowl("Please select Ledger", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectLedgerName").addClass('has-error'); $("#ddlLedgerName").focus(); return false;
    }
    else { $("#divSelectLedgerName").removeClass('has-error'); }

    if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null || $("#txtAmount").val() <= 0) {
        $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
    } else { $("#divAmount").removeClass('has-error'); }

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

    var ObjData = new Object();
    ObjData.ExpenseID = 0;

    var oLedger = new Object();

    oLedger.LedgerID = $("#ddlLedgerName").val();
    oLedger.LedgerName = $("#ddlLedgerName option:selected").text();
    ObjData.Ledger = oLedger;

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
    ObjData.SGSTAmount = $("#hdnTransSGSTAmount").val().trim();
    ObjData.CGSTAmount = $("#hdnTransCGSTAmount").val().trim();
    ObjData.IGSTAmount = $("#hdnTransIGSTAmount").val().trim();

    ObjData.TaxAmount = parseFloat($("#txtTaxAmt").val());
    ObjData.DiscountPercent = parseFloat($("#txtDisPer").val());
    ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());

    ObjData.Amount = parseFloat($("#txtAmount").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.ExpenseID = 0;
        ObjData.StatusFlag = "I";
        AddOPBillingData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnExpenseID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.ExpenseID = $("#hdnExpenseID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.ExpenseID = 0;
        }
        Update_OPBilling(ObjData);
    }
    CalculateAmount();
    ClearOPBillingFields();
    $("#ddlLedgerName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});

function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlLedgerName").val(null).change();
    $("#txtAmount").val("0");
    $("#hdnOPSNo").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtDisPer").val("0");
    $("#txtTaxAmt").val("0");
    $("#txtDisAmt").val("0");

    if (parseFloat($("#txtDiscountPercent").val()) > 0) {
        $("#txtDisPer").val($("#txtDiscountPercent").val());
    }
    $("#ddlTax").val($("#ddlTaxName").val()).change();

    $("#ddlLedgerName").val(null).change();
    $("#divSelectLedgerName").show();
    $("#divLedgerName").removeClass('has-error');
    $("#divAmount").removeClass('has-error');
    $("#divRate").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
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

    if (gData.length >= 5)
    { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Ledger Name</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. %</th>";
        sTable += "<th class='" + sColorCode + "'>Disc. Amt</th>";
        sTable += "<th class='" + sColorCode + "'>Tax %</th>";
        sTable += "<th class='" + sColorCode + "'>Tax Amount</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Ledger.LedgerName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].DiscountPercent + "</td>";
                sTable += "<td>" + gData[i].DiscountAmount + "</td>";
                sTable += "<td>" + gData[i].Tax.TaxPercentage + "</td>";
                sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].Amount + "</td>";
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

function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gOPBillingList);
    return false;
}

function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlLedgerName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnOPSNo").val(ID);
            $("#hdnExpenseID").val(data[i].ExpenseID);
            $("#ddlLedgerName").val(data[i].Ledger.LedgerID).change();
            $("#ddlTax").val(data[i].Tax.TaxID).change();
            $("#txtQuantity").val(data[i].Quantity);
            $("#txtTaxAmt").val(data[i].TaxAmount);
            $("#txtRate").val(data[i].Rate);
            $("#txtDisPer").val(data[i].DiscountPercent);
            $("#txtDisAmt").val(data[i].DiscountAmount);
            $("#txtAmount").val(data[i].Amount);
        }
    }
    return false;
}

function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].ExpenseID = oData.ExpenseID;
            var oLedger = new Object();
            oLedger.LedgerID = oData.Ledger.LedgerID;
            oLedger.LedgerName = oData.Ledger.LedgerName;
            gOPBillingList[i].Ledger = oLedger;

            var oTransTax = new Object();
            oTransTax.TaxID = oData.Tax.TaxID;
            oTransTax.TaxPercentage = oData.Tax.TaxPercentage;
            oTransTax.IGSTPercent = oData.Tax.IGSTPercent;
            oTransTax.SGSTPercent = oData.Tax.SGSTPercent;
            oTransTax.CGSTPercent = oData.Tax.CGSTPercent;
            gOPBillingList[i].Tax = oTransTax;

            gOPBillingList[i].Quantity = oData.Quantity;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].TaxAmount = oData.TaxAmount;
            gOPBillingList[i].CGSTAmount = oData.CGSTAmount;
            gOPBillingList[i].SGSTAmount = oData.SGSTAmount;
            gOPBillingList[i].IGSTAmount = oData.IGSTAmount;
            gOPBillingList[i].DiscountPercent = oData.DiscountPercent;
            gOPBillingList[i].DiscountAmount = oData.DiscountAmount;
            gOPBillingList[i].Amount = oData.Amount;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayOPBillingList(gOPBillingList);
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    ClearOPBillingFields();
    $("#ddlLedgerName").focus();
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
        url: "WebServices/VHMSService.svc/GetExpense",
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "";
                                if (obj[index].BalanceAmount == 0) {
                                    table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].ExpenseID + "'>";
                                } else
                                    table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].ExpenseID + "'>";

                                // table = "<tr id='" + obj[index].ExpenseID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ExpenseNo + "</td>";
                                table += "<td>" + obj[index].sExpenseDate + "</td>";
                                table += "<td>" + obj[index].Party.LedgerName + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

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
                            { "sWidth": "20%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
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
        url: "WebServices/VHMSService.svc/SearchExpense",
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
                                var table = "";
                                if (obj[index].BalanceAmount == 0) {
                                    table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].ExpenseID + "'>";
                                } else
                                    table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].ExpenseID + "'>";

                                // table = "<tr id='" + obj[index].ExpenseID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ExpenseNo + "</td>";
                                table += "<td>" + obj[index].sExpenseDate + "</td>";
                                table += "<td>" + obj[index].Party.LedgerName + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].TaxAmount + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExpenseID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

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
                            $(".PrintOPBilling").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnExpenseID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("ExpenseID", AdmissionID);

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
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "20%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
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

$("#txtRoundoff").keypress(function (e) {
    if (e.which != 46 && e.which != 45 && e.which != 46 &&
        !(e.which >= 48 && e.which <= 57)) {
        return false;
    }
});

$("#btnCancel").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

$("#btnOK").click(function () {
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
                iSubtotal = gOPBillingList[i].Amount;
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

$("#ddlTaxName").change(function () {
    var iTaxID = $("#ddlTaxName").val();
    if (iTaxID != undefined && iTaxID > 0) {
        GetTaxByID(iTaxID);
        CalculateAmount();
    }
});

function CalculateAmountTrans() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iDiscountPer = parseFloat($("#txtDisPer").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDiscountPer)) iDiscountPer = 0;
    if (isNaN(iTax)) iTax = 0;
    var itotal = parseFloat(iRate) * parseFloat(iqty);
    var idisamt = itotal * parseFloat(iDiscountPer) / 100;

    var iTaxPercent = parseFloat(itotal) * parseFloat(iTax) / 100;
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));

    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(itotal) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(itotal) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(itotal) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    var iSubTotal = parseFloat(itotal) - parseFloat(idisamt);
    $("#txtDisAmt").val(parseFloat(idisamt).toFixed(2));
    $("#txtAmount").val(parseFloat(iSubTotal).toFixed(2));
}

$("#ddlReceiptMode").change(function () {
    $("#divChequeDetails").hide();
    var iReceiptMode = $("#ddlReceiptMode").val();
    if (iReceiptMode != undefined && iReceiptMode > 0) {
        if (iReceiptMode == 2 || iReceiptMode == 3)
            $("#divChequeDetails").show();
        if (iReceiptMode == 5 || iReceiptMode == 1) {
            $('#ddlBank').prop('disabled', 'disabled');
            $('#ddlBank').val(1).change();
        }
        else
            $('#ddlBank').prop('disabled', false);
    }
    else {
        $("#divChequeDetails").hide();
    }


    return false;
});

$("#txtDiscountPercent").change(function () {
    var iDisPercent = parseFloat($("#txtDiscountPercent").val());
    $("#txtDisPer").val($("#txtDiscountPercent").val()).change();
    if (isNaN(iDisPercent)) iDisPercent = 0;
    var iSubtotal = 0; var iDiscAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            gOPBillingList[i].DiscountPercent = (iDisPercent).toFixed(2);
            gOPBillingList[i].DiscountAmount = (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * iDisPercent / 100).toFixed(2);
            iSubtotal = (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate)) - parseFloat(gOPBillingList[i].DiscountAmount);
            gOPBillingList[i].Amount = parseFloat(iSubtotal).toFixed(2);
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

$("#ddlPartyName").change(function () {
    var iSupplierID = $("#ddlPartyName").val();
    if (iSupplierID != undefined && iSupplierID > 0)
        GetLedgerByID(iSupplierID);
});

function GetLedgerByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLedgerByID",
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
                            $("#txtGSTNo").val(obj.GSTIN);

                            for (var i = 0; i < gOPBillingList.length; i++) {
                                if (gOPBillingList[i].StatusFlag != "D") {
                                    iAmount = gOPBillingList[i].Amount;
                                    iTaxID = gOPBillingList[i].Tax.TaxID;
                                    GetTaxByID(iTaxID);

                                    gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());
                                    if ($("#hdnStateCode").val() == 33) {
                                        gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                                        gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                                        gOPBillingList[i].Tax.IGSTPercent = 0;
                                        gOPBillingList[i].CGSTAmount = (parseFloat(iAmount) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].SGSTAmount = (parseFloat(iAmount) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].IGSTAmount = 0;
                                    }
                                    else {
                                        gOPBillingList[i].Tax.CGSTPercent = 0;
                                        gOPBillingList[i].Tax.SGSTPercent = 0;
                                        gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                                        gOPBillingList[i].CGSTAmount = 0;
                                        gOPBillingList[i].SGSTAmount = 0;
                                        gOPBillingList[i].IGSTAmount = (parseFloat(iAmount) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
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
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].Amount);
            iBillingTaxAmt = iBillingTaxAmt + parseFloat(gOPBillingList[i].TaxAmount);
            gOPBillingList[i].CGSTAmount = (parseFloat(gOPBillingList[i].Amount) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
            gOPBillingList[i].SGSTAmount = (parseFloat(gOPBillingList[i].Amount) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
            gOPBillingList[i].IGSTAmount = (parseFloat(gOPBillingList[i].Amount) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
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
    
    $("#txtTotalQty").val((parseFloat(iBillingQty)).toFixed(0));
    $("#txtNetAmount").val((parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) + parseFloat(iround)).toFixed(2));
}
$("#txtRoundoff").change(function () {
    CalculateAmount();
});

$("#btnSave,#btnUpdate").click(function () {
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

    if ($("#ddlPartyName").val() == "0" || $("#ddlPartyName").val() == undefined || $("#ddlPartyName").val() == null) {
        $.jGrowl("Please select Party", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPartyName").addClass('has-error'); $("#ddlPartyName").focus(); return false;
    } else { $("#divPartyName").removeClass('has-error'); }

    if ($("#ddlTaxName").val() == "0" || $("#ddlTaxName").val() == undefined || $("#ddlTaxName").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divTaxName").addClass('has-error'); $("#ddlTaxName").focus(); return false;
    }
    else { $("#divTaxName").removeClass('has-error'); }

    if ($("#ddlBank").val() == "0" || $("#ddlBank").val() == undefined || $("#ddlBank").val() == null) {
        $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBank").addClass('has-error'); $("#ddlBank").focus(); return false;
    } else { $("#divBank").removeClass('has-error'); }

    if ($("#ddlReceiptMode").val() == "0" || $("#ddlReceiptMode").val() == undefined || $("#ddlReceiptMode").val() == null) {
        $.jGrowl("Please select Receipt Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divReceiptMode").addClass('has-error'); $("#ddlReceiptMode").focus(); return false;
    } else { $("#divReceiptMode").removeClass('has-error'); }

    if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3) {
        if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
            $.jGrowl("Please enter Cheque/DD No.", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
        } else { $("#divChequeNo").removeClass('has-error'); }

        if ($("#txtIssueDate").val().trim() == "" || $("#txtIssueDate").val().trim() == undefined) {
            $.jGrowl("Please select Issue Date", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divIssueDate").addClass('has-error'); $("#txtIssueDate").focus(); return false;
        } else { $("#divIssueDate").removeClass('has-error'); }

    }

    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Ledger has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    var ObjOPBilling = new Object();

    ObjOPBilling.ExpenseID = 0;
    ObjOPBilling.ExpenseNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sExpenseDate = $("#txtBillDate").val().trim();
    ObjOPBilling.sBillDate = $("#txtBDate").val().trim();
    ObjOPBilling.ExpenseTrans = gOPBillingList;

    var ObjSupplier = new Object();
    ObjSupplier.LedgerID = $("#ddlPartyName").val();
    ObjOPBilling.Party = ObjSupplier;

    var ObjExpenseOrder = new Object();
    ObjExpenseOrder.ExpenseOrderID = 0;
    ObjOPBilling.ExpenseOrder = ObjExpenseOrder;

    var objBank = new Object();
    objBank.LedgerID = $("#ddlBank").val();
    ObjOPBilling.Bank = objBank;

    var ObjTax = new Object();
    ObjTax.TaxID = $("#ddlTaxName").val();
    ObjOPBilling.Tax = ObjTax;
    ObjOPBilling.BillNo = $("#txtExpenseNo").val().trim();
    ObjOPBilling.GSTIN = $("#txtGSTNo").val().trim();
    ObjOPBilling.TaxPercent = $("#hdnTaxPercent").val().trim();
    ObjOPBilling.CGSTAmount = $("#txtCGST").val().trim();
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.IGSTAmount = $("#txtIGST").val().trim();
    ObjOPBilling.Roundoff = $("#txtRoundoff").val().trim();
    ObjOPBilling.TaxAmount = $("#txtTaxAmount").val().trim();
    ObjOPBilling.TotalAmount = $("#txtTotalAmount").val().trim();
    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.Description = $("#txtDescription").val().trim();
    ObjOPBilling.ReceiptModeID = $("#ddlReceiptMode").val();
    ObjOPBilling.ImagePath1 = $("[id*=imgUpload2_view]").attr("src");
    ObjOPBilling.ImagePath2 = $("[id*=imgUpload3_view]").attr("src");
    ObjOPBilling.ImagePath3 = $("[id*=imgUpload4_view]").attr("src");


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
    ObjOPBilling.AdditionalDiscountAmount = 0;
    ObjOPBilling.Status = $("#ddlPaymentStatus").val();
    if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3) {
        ObjOPBilling.ChequeNo = $("#txtChequeNo").val().trim();
        ObjOPBilling.sIssueDate = $("#txtIssueDate").val().trim();
    }
    ObjOPBilling.Narration = $("#txtDescription").val().trim();

    if ($("#hdnExpenseID").val() > 0) {
        if ($("#ddlReceiptMode").val() == 5) {
            ObjOPBilling.Status = "Pending";
            var Balance = parseFloat($("#txtNetAmount").val()) - parseFloat($("#hdnPaidAmt").val());
            ObjOPBilling.BalanceAmount = parseFloat(Balance).toFixed(2);
            ObjOPBilling.PaidAmount = parseFloat($("#hdnPaidAmt").val());
        }
        else {
            ObjOPBilling.Status = "Closed";
            ObjOPBilling.BalanceAmount = 0;
            ObjOPBilling.PaidAmount = parseFloat($("#txtNetAmount").val());

        }

        ObjOPBilling.ExpenseID = $("#hdnExpenseID").val();
        gOPBillingList.ExpenseID = ObjOPBilling.ExpenseID;
        ObjOPBilling.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdateExpense";
    }
    else {
        if ($("#ddlReceiptMode").val() == 5) {
            ObjOPBilling.Status = "Pending";
            ObjOPBilling.BalanceAmount = parseFloat($("#txtNetAmount").val());
            ObjOPBilling.PaidAmount = 0;
        }
        else {
            ObjOPBilling.Status = "Closed";
            ObjOPBilling.PaidAmount = parseFloat($("#txtNetAmount").val());
            ObjOPBilling.BalanceAmount = 0;
        }

        sMethodName = "AddExpense";
        ObjOPBilling.ExpenseID = 0;
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
                        if (sMethodName == "AddOPBilling") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnExpenseID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateOPBilling")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();

                        $("#hdnExpenseID").val("0");
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
        url: "WebServices/VHMSService.svc/GetExpenseByID",
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

                            $("#hdnExpenseID").val(obj.ExpenseID)
                            $("#txtBillNo").val(obj.ExpenseNo);
                            $("#txtBillDate").val(obj.sExpenseDate);
                            $("#txtBDate").val(obj.sBillDate);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtDescription").val(obj.Description);
                            $("#ddlPartyName").val(obj.Party.LedgerID).change();
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("#txtExpenseNo").val(obj.BillNo);
                            $("#txtGSTNo").val(obj.GSTIN);
                            $("#txtCGST").val(obj.CGSTAmount);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtSGST").val(obj.SGSTAmount);
                            $("#txtIGST").val(obj.IGSTAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscountAmount").val(obj.DiscountAmount);

                            $("#ddlBank").val(obj.Bank.LedgerID).change();
                            $("#ddlReceiptMode").val(obj.ReceiptModeID);
                            $("#ddlPaymentStatus").val(obj.Status);
                            if (obj.ReceiptModeID == 2 || obj.ReceiptModeID == 3) {
                                $("#divChequeDetails").show();
                                $("#txtChequeNo").val(obj.ChequeNo);
                                $("#txtIssueDate").val(obj.sIssueDate);
                            }
                            $("#txtDescription").val(obj.Description);
                            $("#hdnNetAmt").val(obj.NetAmount);

                            $("#hdnPaidAmt").val(obj.PaidAmount);
                            $("#hdnBalanceAmt").val(obj.BalanceAmount);

                            $("[id*=imgUpload2_view]").css("visibility", "visible");
                            $("[id*=imgUpload2_view]").attr("src", obj.ImagePath1);
                            $("[id*=imgUpload3_view]").css("visibility", "visible");
                            $("[id*=imgUpload3_view]").attr("src", obj.ImagePath2);
                            $("[id*=imgUpload4_view]").css("visibility", "visible");
                            $("[id*=imgUpload4_view]").attr("src", obj.ImagePath3);

                            gOPBillingList = [];
                            var ObjLedger = obj.ExpenseTrans;
                            for (var index = 0; index < ObjLedger.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                var objMagazine = new Object();
                                objMagazine.LedgerID = ObjLedger[index].Ledger.LedgerID;
                                objMagazine.LedgerName = ObjLedger[index].Ledger.LedgerName;
                                objTemp.Ledger = objMagazine;

                                var objTransTax = new Object();
                                objTransTax.TaxID = ObjLedger[index].Tax.TaxID;
                                objTransTax.TaxPercentage = ObjLedger[index].Tax.TaxPercentage;
                                objTransTax.CGSTPercent = ObjLedger[index].Tax.CGSTPercent;
                                objTransTax.SGSTPercent = ObjLedger[index].Tax.SGSTPercent;
                                objTransTax.IGSTPercent = ObjLedger[index].Tax.IGSTPercent;
                                objTemp.Tax = objTransTax;

                                objTemp.ExpenseTransID = ObjLedger[index].ExpenseTransID;
                                objTemp.ExpenseID = ObjLedger[index].ExpenseID;
                                objTemp.LedgerID = ObjLedger[index].Ledger.LedgerID;
                                objTemp.LedgerName = ObjLedger[index].Ledger.LedgerName;
                                objTemp.Amount = ObjLedger[index].Amount;
                                objTemp.SGSTAmount = ObjLedger[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjLedger[index].IGSTAmount;
                                objTemp.CGSTAmount = ObjLedger[index].CGSTAmount;
                                objTemp.TaxAmount = ObjLedger[index].TaxAmount;
                                objTemp.Quantity = ObjLedger[index].Quantity;
                                objTemp.Rate = ObjLedger[index].Rate;
                                objTemp.DiscountPercent = ObjLedger[index].DiscountPercent;
                                objTemp.DiscountAmount = ObjLedger[index].DiscountAmount;

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
    DeleteRecord(id, $("#txtReason").val());
    //$("#hdnID").val("");
    //$("#btnSave").show();
    //$("#btnUpdate").hide();
    //$(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    //$('#compose-modal').modal({ show: true, backdrop: true });
    //$("#txtID").val(id);
    //$("#txtReason").focus();
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
        url: "WebServices/VHMSService.svc/DeleteExpense",
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
