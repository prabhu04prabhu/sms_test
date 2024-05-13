var gBarcodeList = [];
var gStockCheckList = [];
var gExchangeList = [];

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#divTab").show();
    $("#divStockCheck").hide();
    $("#txtDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY',
    });

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtDate").val(d + "/" + m + "/" + y);

    // GetSettings();
    var _Tfunctionality;

    pLoadingSetup(true);
    GetRecord();

});
$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().length > 0) {
        GetBarcodeDetails($("#txtCode").val());
        $("#txtCode").val("");
        $("#txtCode").focus();
        $("#txtQuantity").val(1);
    }
});

$("#txtQuantity").change(function () {
    if ($("#txtCode").val().length > 0) {
        if ($("#txtQuantity").val().length > 0) {
            GetBarcodeDetails($("#txtCode").val());
            $("#txtQuantity").val(1);
            $("#txtCode").focus();
        }
    } else {
        if ($("#txtCode").val() == "" || $("#txtCode").val() == undefined || $("#txtCode").val() == null) {
            $.jGrowl("Please enter SMSCode", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divCode").addClass('has-error'); $("#txtCode").focus(); return false;
        }
        else { $("#divCode").removeClass('has-error'); }
    }
});

function GetBarcodeDetails(id) {
    dProgress(true);
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
                            var ObjData = new Object();
                            ObjData.sNO = gBarcodeList.max() + 1;
                            ObjData.SNo = ObjData.sNO;
                            ObjData.StockID = obj.StockID;
                            ObjData.ProductName = obj.Product.ProductName;
                            ObjData.ProductID = obj.Product.ProductID;
                            ObjData.SMSCode = obj.Product.SMSCode;
                            ObjData.Quantity = parseFloat($("#txtQuantity").val());
                            ObjData.StatusFlag = "I";
                            var Count = 0;
                            if (gBarcodeList.length > 0) {
                                for (var i = 0; i < gBarcodeList.length; i++) {
                                    if (gBarcodeList[i].SMSCode == $("#txtCode").val().toUpperCase()) {
                                        if ($("#txtQuantity").val() == 1) {
                                            gBarcodeList[i].Quantity = gBarcodeList[i].Quantity + parseFloat($("#txtQuantity").val());
                                        } else
                                            gBarcodeList[i].Quantity = parseFloat($("#txtQuantity").val());
                                        DisplayBarcodeList(gBarcodeList);
                                        $("#txtCode").val("");
                                        $("#txtQuantity").val(1);
                                        Count = 1;
                                    }
                                }
                                if (Count == 0)
                                    AddBarcodeData(ObjData);
                                else
                                    DisplayBarcodeList(gBarcodeList);
                            }
                            else
                                AddBarcodeData(ObjData);

                            ClearBarcodeFields();
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

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnStockCheckID").val("0");

    $("#divTab").hide();
    $("#divStockCheck").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gBarcodeList = [];
    gExchangeList = [];
    gStockCheckList = [];
    ClearStockCheckTab();
    $("#divStockCheckList").empty();

    $("#txtDate").focus();
    $("#txtCode").focus();
    return false;
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#divTab").show();
    $("#divStockCheck").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnStockCheckID").val("0");
    gStockCheckList = [];
    gBarcodeList = [];
    ClearStockCheckTab();
    $("#btnList").click();
    return false;
});



function ClearStockCheckTab() {
    gStockCheckList = [];
    gBarcodeList = [];
    $("#txtBillNo").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#txtQuantity").val(1);
    $("#txtBillNo").attr("disabled", false);
    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtDate").val(d + "/" + m + "/" + y);
    return false;
}

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {
    var ObjData = new Object();
    ObjData.StockCheckTransID = 0;
    var ObjStock = new Object();
    ObjStock.StockID = $("#hdnStockID").val();
    ObjData.Stock = ObjStock;
    ObjData.Quantity = 1;
    ObjData.ProductName = $("#txtProduct").val();
    ObjData.SMSCode = $("#txtCode").val();

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gStockCheckList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.StockCheckTransID = 0;
        ObjData.StatusFlag = "I";
        AddStockCheckData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnStockCheckID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.StockCheckID = $("#hdnStockCheckID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.StockCheckID = 0;
        }
        Update_StockCheck(ObjData);
    }
    CalculateAmount();
    ClearStockCheckFields();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});

function ClearBarcodeFields() {
    $("#txtCode").val("");
    return false;
}

function AddStockCheckData(oData) {
    gStockCheckList.push(oData);
    DisplayStockCheckList(gStockCheckList);
    // StockCheckData();
    return false;
}


function StockCheckData() {
    for (var i = 0; i < gBarcodeList.length; i++) {
        for (var j = 0; j < gStockCheckList.length; j++) {
            if (gBarcodeList[i].SMSCode == gStockCheckList[j].SMSCode) {
                var id = gBarcodeList[i].Quantity;
                var IID = gStockCheckList[j].Quantity;
                gStockCheckList[j].Quantity = gStockCheckList[j].Quantity - gBarcodeList[i].Quantity;
                DisplayStockCheckList(gStockCheckList);
            }

        }
    }
}

function AddBarcodeData(oData) {
    gBarcodeList.push(oData);
    DisplayBarcodeList(gBarcodeList);
    return false;
}
function DisplayBarcodeList(gData) {
    var sTable = "";
    var sCount = 1; var Quantity = 0;
    var sColorCode = "bg-info";

    if (gData.length >= 20)
    { $("#divBarcodeList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else
    { $("#divBarcodeList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblBarcodeList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>SMSCode</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "</tr></thead><tbody id='tblBarcodeList_body'>";
        sTable += "</tbody></table>";
        $("#divBarcodeList").html(sTable);
        for (var i = 0; i < gData.length; i++) {

            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].SMSCode + "</td>";
                sTable += "<td>" + gData[i].ProductName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OPBillingDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";

                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblBarcodeList_body").append(sTable);
            }
        }
    }
    else { $("#divBarcodeList").empty(); }

    return false;
}

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gBarcodeList);
    return false;
}

function Bind_OPBillingByID(ID, data) {
    //$("#btnAddMagazine").hide();
    // $("#btnUpdateMagazine").show();
    //$("#txtCode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].SNo == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnOPSNo").val(ID);

            //$("#txtSNo").val(data[i].SNo);
            $("#hdnStockCheckTransID").val(data[i].StockCheckTransID);
            //$("#ddlProductName").val(data[i].Product.ProductID).change();
            $("#txtCode").val(data[i].SMSCode);
            $("#txtQuantity").val(data[i].Quantity);
        }
    }
    return false;
}

function DisplayStockCheckList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divMissingBarcodeList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divMissingBarcodeList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblStockCheckList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>SMSCode</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "</tr></thead><tbody id='tblStockCheckList_body'>";
        sTable += "</tbody></table>";
        $("#divMissingBarcodeList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                // var Quantity = gData[i].Quantity - gBarcodeList[i].Quantity;
                //if (Quantity != 0) {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].SMSCode + "</td>";
                sTable += "<td>" + gData[i].ProductName + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblStockCheckList_body").append(sTable);

                // }
            }
        }
    }
    else { $("#divMissingBarcodeList").empty(); }

    return false;


}

$("#btnVerify").click(function () {
    var StockIDs = 0, Quantity = 0;
    //for (var i = 0; i < gBarcodeList.length; i++) {
    //    StockIDs = gBarcodeList[i].StockID;
    //    Quantity = gBarcodeList[i].Quantity;
    //    //if (i < gBarcodeList.length - 1)
    //    //    StockIDs = StockIDs + ",";
    GetMissingBarcodelist(StockIDs, Quantity);
    //}
    //if (StockIDs.length > 0)
    //    GetMissingBarcodelist(StockIDs);
});
//function GetMissingBarcodelist(StockIDs, Quantity) {
//    dProgress(true);
//    $.ajax({
//        type: "POST",
//        url: "WebServices/VHMSService.svc/GetMissingBarcode",
//        data: JSON.stringify({ StockIDs: StockIDs, Quantity: Quantity }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data) {
//            if (data.d != "") {
//                var objResponse = jQuery.parseJSON(data.d);
//                if (objResponse.Status == "Success") {
//                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
//                        var obj = jQuery.parseJSON(objResponse.Value);
//                        if (obj != null) {
//                            gStockCheckList = [];
//                            var Count = 0;
//                            var ObjProduct = obj;
//                            var ObjData = new Object();
//                            for (var index = 0; index < ObjProduct.length; index++) {
//                                for (var i = 0; i < gBarcodeList.length; i++) {
//                                    if (ObjProduct[index].Product.SMSCode == gBarcodeList[i].SMSCode) {
//                                        GetMissingBarcodelist(ObjProduct[index].StockID, gBarcodeList[i].Quantity)
//                                    }
//                                    else
//                                        GetMissingBarcodelist(ObjProduct[index].StockID , 0)
//                                }
//                            //    var objTemp = new Object();
//                            //    objTemp.sNO = index + 1;
//                            //    objTemp.SNo = objTemp.sNO;
//                            //    objTemp.StockID = ObjProduct[index].StockID;
//                            //    objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
//                            //    objTemp.ProductID = ObjProduct[index].Product.ProductID;
//                            //    objTemp.ProductName = ObjProduct[index].Product.ProductName;
//                            //    objTemp.Quantity = ObjProduct[index].Quantity;
//                            //    objTemp.StatusFlag = "I";

//                            //    var Count = 0;
//                            //    for (var i = 0; i < gBarcodeList.length; i++) {
//                            //        if (objTemp.SMSCode == gBarcodeList[i].SMSCode) {
//                            //            for (var j = 0; j < gStockCheckList.length; j++) {
//                            //                if (objTemp.SMSCode == gStockCheckList[j].SMSCode) {
//                            //                    gStockCheckList[j].Quantity = gBarcodeList[i].Quantity - ObjProduct[index].Quantity;
//                            //                    DisplayStockCheckList(gStockCheckList);
//                            //                    Count = 1;
//                            //                }
//                            //                else {
//                            //                    objTemp.Quantity = gBarcodeList[i].Quantity - ObjProduct[index].Quantity;
//                            //                    AddStockCheckData(objTemp);
//                            //                }
//                            //            }
//                            //        }
//                            //    }
//                            //    if (Count == 0)
//                            //        AddStockCheckData(objTemp);
//                            //    else
//                            //        DisplayStockCheckList(gStockCheckList);

//                            }
//                        }
//                        dProgress(false);
//                    }
//                    else if (objResponse.Value == "NoRecord") {
//                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
//                        dProgress(false);
//                    }
//                    else if (objResponse.Value == "Error") {
//                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
//                    }
//                }
//                else if (objResponse.Status == "Error") {
//                    if (objResponse.Value == "0") {
//                        window.location("frmLogin.aspx");
//                    }
//                    else if (objResponse.Value == "Error") {
//                        window.location = "frmErrorPage.aspx";
//                    }
//                    else if (objResponse.Value == "NoRecord") {
//                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
//                    }
//                }
//            }
//            else {
//                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
//                dProgress(false);
//            }
//        },
//        error: function () {
//            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
//            dProgress(false);
//        }
//    });
//    return false;
//}

function GetMissingBarcodelist(StockIDs, Quantity) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetMissingBarcode",
        data: JSON.stringify({ StockIDs: StockIDs, Quantity: Quantity }),
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
                            gStockCheckList = [];
                            var Count = 0;
                            var ObjProduct = obj;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StockID = ObjProduct[index].StockID;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.StatusFlag = "I";
                                AddStockCheckData(objTemp);
                            }
                            StockCheckData();
                            //var Count = 0;
                            //for (var i = 0; i < gBarcodeList.length; i++) {
                            //    if (objTemp.SMSCode == gBarcodeList[i].SMSCode) {
                            //        for (var j = 0; j < gStockCheckList.length; j++) {
                            //            if (objTemp.SMSCode == gStockCheckList[j].SMSCode) {
                            //                gStockCheckList[j].Quantity = gBarcodeList[i].Quantity - ObjProduct[index].Quantity;
                            //                DisplayStockCheckList(gStockCheckList);
                            //                Count = 1;
                            //            }
                            //            else {
                            //                objTemp.Quantity = gBarcodeList[i].Quantity - ObjProduct[index].Quantity;
                            //                AddStockCheckData(objTemp);
                            //            }
                            //        }
                            //    }
                            //}
                            //if (Count == 0)
                            //  AddStockCheckData(objTemp);
                            //else
                            //    DisplayStockCheckList(gStockCheckList);

                            // }
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
function Bind_BarcodeByID(ID, data) {
    $("#btnAddBarcode").hide();
    $("#btnUpdateBarcode").show();
    $("#ddlBarcode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnStockCheckID").val(data[i].StockCheckID);
            // $("#ddlBarcode").val(data[i].Register.RegisterID).change();
        }
    }
    return false;
}

function Bind_StockCheckByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#txtCode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            $("#hdnStockCheckID").val(data[i].StockCheckID);
            $("#txtCode").val(data[i].SMSCode).change();

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

            //iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iWeight) * parseFloat(iSelling)) + (parseFloat(iStoneWt) * parseFloat(iStoneAmt));
            //$("#txtTotal").val(parseFloat(iTotal).toFixed(2));
        }
    }
    return false;
}
function Update_Barcode(oData) {
    for (var i = 0; i < gBarcodeList.length; i++) {
        if (gBarcodeList[i].sNO == oData.sNO) {
            gBarcodeList[i].StockCheckID = oData.StockCheckID;
            var iRegister = new Object();
            iRegister.AccountNo = oData.Register.AccountNo
            iRegister.RegisterID = oData.Register.RegisterID
            iRegister.Barcode.BarcodeName = oData.Register.Barcode.BarcodeName
            gBarcodeList[i].Register = iRegister;

            gBarcodeList[i].StatusFlag = oData.StatusFlag;
            gBarcodeList[i].BarcodeAmount = oData.BarcodeAmount;
        }
    }
    DisplayBarcodeList(gBarcodeList);
    $("#btnAddBarcode").show();
    $("#btnUpdateBarcode").hide();
    ClearBarcodeFields();
    $("#ddlBarcode").focus();
    return false;
}
function Update_StockCheck(oData) {
    for (var i = 0; i < gStockCheckList.length; i++) {
        if (gStockCheckList[i].sNO == oData.sNO) {
            gStockCheckList[i].StockCheckID = oData.StockCheckID;
            gStockCheckList[i].ProductName = oData.ProductName;
            gStockCheckList[i].SMSCode = oData.SMSCode;
            gStockCheckList[i].StatusFlag = oData.StatusFlag;
            gStockCheckList[i].Quantity = oData.Quantity;
        }
    }
    DisplayStockCheckList(gStockCheckList);
    $("#btnAddStockCheck").show();
    $("#btnUpdateStockCheck").hide();
    ClearStockCheckFields();
    $("#txtCode").focus();
    return false;
}
function Delete_StockCheckDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gStockCheckList.length; i++) {
            if (gStockCheckList[i].SNo == ID) {
                var index = jQuery.inArray(gStockCheckList[i].valueOf("SNo"), gStockCheckList);
                if (gStockCheckList[i].SNo > 0) {
                    gStockCheckList[i].StatusFlag = "D";
                } else {
                    gStockCheckList.splice(index, 1);
                }
                $("#divStockCheckList").empty();
                DisplayStockCheckList(gStockCheckList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function Delete_BarcodeDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gBarcodeList.length; i++) {
            if (gBarcodeList[i].SNo == ID) {
                var index = jQuery.inArray(gBarcodeList[i].valueOf("SNo"), gBarcodeList);
                if (gBarcodeList[i].SNo > 0) {
                    gBarcodeList[i].StatusFlag = "D";
                } else {
                    gBarcodeList.splice(index, 1);
                }
                $("#divBarcodeList").empty();
                DisplayBarcodeList(gBarcodeList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gExchangeList);
    return false;
}

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopStockCheck",
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].StockCheckID + "'>";
                                table += "<td>" + (index + 1) + "</td>"; table += "<td class='hidden-xs'>" + obj[index].StockCheckNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sCheckDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockCheckID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockCheckID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockCheckID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
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
                            $(".PrintStockCheck").click(function () {
                                SetSessionValue("StockCheckID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintStockCheckInvoice.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
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
                            { "sWidth": "0%" },
                            { "sWidth": "0%" },
                            { "sWidth": "5%" },
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
        url: "WebServices/VHMSService.svc/SearchStockCheck",
        data: JSON.stringify({ PatientID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
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

                                var table = "<tr id='" + obj[index].StockCheckID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].StockCheckNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sCheckDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockCheckID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockCheckID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
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
                            $(".PrintStockCheck").click(function () {
                                SetSessionValue("StockCheckID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintStockCheckInvoice.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
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
                            { "sWidth": "60%" },
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

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }


    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    var d1 = Date.parse($("#hdnOpeningDate").val());
    var d2 = Date.parse($("#txtDate").val());
    if (d1 < d2) {
        $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
        return false;
    }

    if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
    } else { $("#divDate").removeClass('has-error'); }

    //if (gStockCheckList.length <= 0) {
    //    $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#txtBarcode").focus(); return false;
    //}

    //var gCount = 0;
    //for (var i = 0; i < gStockCheckList.length; i++) {
    //    if (gStockCheckList[i].StatusFlag != "D")
    //        gCount++;

    //}
    //if (gCount == 0) {
    //    $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#txtBarcode").focus(); return false;
    //}


    var gBarcodeCount = 0;
    for (var i = 0; i < gStockCheckList.length; i++) {
        if (gStockCheckList[i].StatusFlag != "D")
            gBarcodeCount++;
    }
    // }
    //if (gBarcodeCount == 0) {
    //    $.jGrowl("No Scheme has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#txtBarcode").focus(); return false;
    //}
    // }

    //var iStockCheckAmount = 0;
    //for (var i = 0; i < gStockCheckList.length; i++)
    //    iStockCheckAmount = iStockCheckAmount + parseFloat(gStockCheckList[i].Subtotal);

    var ObjStockCheck = new Object();

    if (gBarcodeCount > 0)
        ObjStockCheck.Status = "Missing";
    else
        ObjStockCheck.Status = "Closed";

    var ObjEmployee = new Object();
    ObjEmployee.UserID = $("#ddlEmployee").val();
    ObjStockCheck.Employee = ObjEmployee;

    ObjStockCheck.StockCheckID = 0;
    if ($("#txtBillNo").val().length > 0)
        ObjStockCheck.StockCheckNo = parseInt($("#txtBillNo").val());
    else
        ObjStockCheck.StockCheckNo = 0;
    ObjStockCheck.sCheckDate = $("#txtDate").val().trim();
    //ObjStockCheck.DiscountAmount = parseFloat($("#txtDiscount").val());

    ObjStockCheck.StockCheckTrans = gBarcodeList;

    if ($("#hdnStockCheckID").val() > 0) {
        ObjStockCheck.StockCheckID = $("#hdnStockCheckID").val();
        sMethodName = "UpdateStockCheck";
    }
    else {
        sMethodName = "AddStockCheck";
        ObjStockCheck.StockCheckID = 0
    }
    console.log(ObjStockCheck);

    SaveandUpdateStockCheck(ObjStockCheck, sMethodName);

});
function SaveandUpdateStockCheck(ObjStockCheck, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjStockCheck }),
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
                        $("#divStockCheck").hide();
                        if (sMethodName == "AddStockCheck") {

                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            window.open("frmStockCheckReport.aspx", "MsgWindow");
                            $("#hdnStockCheckID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateStockCheck") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            window.open("frmStockCheckReport.aspx", "MsgWindow");
                        }
                        ClearStockCheckTab();
                        location.reload();
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
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStockCheckByID",
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
                            //$("#btnVerify").click();

                            $("#txtBillNo").attr("disabled", true);
                            $("#txtBillNo").val(obj.StockCheckNo);
                            $("#hdnStockCheckID").val(obj.StockCheckID)
                            $("#txtDate").val(obj.sCheckDate);
                            gBarcodeList = [];
                            gStockCheckList = [];
                            var ObjProduct = obj.StockCheckTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";
                                var objStock = new Object();
                                objTemp.StockID = ObjProduct[index].StockID;
                                objTemp.StockCheckTransID = ObjProduct[index].StockCheckTransID;
                                objTemp.SMSCode = ObjProduct[index].SMSCode;
                                objTemp.ProductName = ObjProduct[index].ProductName;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                AddBarcodeData(objTemp);
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

function DeleteRecord(id) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteStockCheck",
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
                        ClearCancelData();

                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "StockCheck_R_01" || objResponse.Value == "StockCheck_D_01") {
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

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

