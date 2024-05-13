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
    GetProductList("ddlProductName");
    GetSupplierList("ddlSupplierName");
    $("#txtBillDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtBillDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    $("#txtDeliveryDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtDeliveryDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
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

            GetReceivedPurchaseOrder(parseInt($.cookie("PurchaseOrderID")));
            $("#hdnPurchaseOrderID").val(parseInt($.cookie("PurchaseOrderID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("PurchaseOrderID", null);
    }

    pLoadingSetup(true);
    GetRecord();

});

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnPurchaseOrderID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#ddlSupplierName").change();
    $("#divTab").hide();
    $("#divOPBilling").show();
    $("#txtComments").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    gOPBillingList = [];
    ClearOPBillingTab();
   
    $("#divOPBillingList").empty();
    $("#ddlSupplierName").val($("#ddlSupplierName option:first").val());
    $("#txtBillDate").focus();
    $("#txtBillNo").focus();
    ClearOPBillingFields();
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
    $("#hdnPurchaseOrderID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});
function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtDeliveryDate").val("");
    $("#ddlProductName").val(null).change();
    $("#txtTotalQuantity").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    gOPBillingList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    GetProductList("ddlProductName");   
    $("#txtBillNo").attr("disabled", false);
    return false;
}


$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
        GetRate();
        GetGetStockByID();
    }
});

function GetRate() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductRate",
        data: JSON.stringify({ ID: $("#ddlProductName").val(), type: "I", SupplierID: 0 }),
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

function GetGetStockByID() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCurrentStock",
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
                            $("#txtAvailableQty").val(obj.Rate);
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

$("#txtCode").blur(function () {
    $("#txtCode").val(($("#txtCode").val().split('|')[0]).trim());
    if ($("#txtCode").val().length > 2) {
        GetProductByCodeList("ddlProductName");
    }
    else if ($("#txtCode").val().length==0) 
    {
        GetProductList("ddlProductName");   
    }
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
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].SupplierID + "'>" + obj[index].SupplierName + "</option>");
                            }
                            $("#ddlSupplierName").val($("#ddlSupplierName option:first").val());
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
                                    if (obj[index].Supplier.SupplierID == $("#ddlSupplierName").val())
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
                                    if (obj[index].IsActive) {
                                        if (obj[index].Supplier.SupplierID == $("#ddlSupplierName").val())
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

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {
    if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    else { $("#divSelectProductName").removeClass('has-error'); }

    if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <=0 ) {
        $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    } else { $("#divQuantity").removeClass('has-error'); }

   
    var ObjData = new Object();
    ObjData.PurchaseOrderID = 0;

    var oProduct = new Object();

    oProduct.ProductID = $("#ddlProductName").val();
    oProduct.ProductName = $("#ddlProductName option:selected").text();
    oProduct.SMSCode = $("#txtSMSCode").val().toUpperCase();
    oProduct.ProductCode = $("#txtPartyCode").val();
    ObjData.Product = oProduct;

    ObjData.Quantity = parseFloat($("#txtQuantity").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.PurchaseOrderID = 0;
        var Count = 0; 
        ObjData.StatusFlag = "I";
        for (var i = 0; i < gOPBillingList.length; i++) {

            if ((gOPBillingList[i].Product.ProductID == $("#ddlProductName").val())) {
                gOPBillingList[i].Quantity = gOPBillingList[i].Quantity + parseFloat($("#txtQuantity").val());
                var iDisPercent = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].DiscountPercentage) / 100;
                gOPBillingList[i].DiscountAmount = parseFloat(iDisPercent);
                gOPBillingList[i].SubTotal = gOPBillingList[i].SubTotal + parseFloat($("#txtSubTotal").val());
                Count = 1;
            }
        }
        if (Count == 0)
            AddOPBillingData(ObjData);
        else
            DisplayOPBillingList(gOPBillingList);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnPurchaseOrderID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.PurchaseOrderID = $("#hdnPurchaseOrderID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.PurchaseOrderID = 0;
        }
        Update_OPBilling(ObjData);
    }
   // CalculateAmount();
    ClearOPBillingFields();
    $("#ddlProductName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    GetProductList("ddlProductName"); 
    $("#ddlProductName").val(null).change();
    $("#txtCode").val("");
    $("#txtQuantity").val("0");
    $("#txtAvailableQty").val("0");
    $("#txtSMSCode").val("");
    $("#txtPartyCode").val("");
    $("#hdnOPSNo").val("");
    //$("#hdnPurchaseOrderID").val("");
    $("#divSelectProductName").show();
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

    if (gData.length >= 12)
    { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else
    { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>SMSCode</th>";
        sTable += "<th class='" + sColorCode + "'>PartyCode</th>";
        sTable += "<th class='" + sColorCode + "'>Order Qty</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].Product.SMSCode + "</td>";
                sTable += "<td>" + gData[i].Product.ProductCode + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
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
    $("#ddlProductName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnOPSNo").val(ID);
            $("#hdnPurchaseOrderID").val(data[i].PurchaseOrderID);
            $("#ddlProductName").val(data[i].Product.ProductID).change();


            if (data[i].Product.ProductID == 0) {
                $("#divNewProductName").show();
                $("#divSelectProductName").hide();
                $("#rdbNewProductName").prop("checked", true);
                $("#rdbExistingProductName").prop("checked", false);
                $("#txtNewProductName").val(data[i].Product.ProductName);
            }
            else if (data[i].Product.ProductID > 0) {
                $("#divNewProductName").hide();
                $("#divSelectProductName").show();
                $("#rdbNewProductName").prop("checked", false);
                $("#rdbExistingProductName").prop("checked", true);
                $("#ddlProductName").val(data[i].Product.ProductID).change();
            }
            $("#txtSMSCode").val(data[i].Product.SMSCode);
            $("#txtPartyCode").val(data[i].Product.ProductCode);
            $("#txtQuantity").val(data[i].Quantity);
        }
    }
    return false;
}
function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].PurchaseOrderID = oData.PurchaseOrderID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            oProduct.ProductCode = oData.Product.ProductCode;
            oProduct.SMSCode = oData.Product.SMSCode;
            gOPBillingList[i].Product = oProduct;

            gOPBillingList[i].Quantity = oData.Quantity;
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
            }
        }
    }
    return false;
}

function CalculateNetPrice() {
    var iAmountPrice = parseFloat($("#txtQuantity").val());


    if (isNaN(iAmountPrice)) iNoofCopies = 0;
    
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
        url: "WebServices/VHMSService.svc/GetPurchaseOrder",
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

                                var table = "<tr id='" + obj[index].PurchaseOrderID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].PurchaseOrderNo + "</td>";
                                table += "<td>" + obj[index].sPurchaseOrderDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1") 
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }
                                
                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Print' title='Click here to Print PO'></i><i class='fa fa-print text-green'/></a></td>";
                                
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
                            $(".Print").click(function () {
                                SetSessionValue("PurchaseOrderID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseOrder.aspx", "MsgWindow");
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
                            { "sWidth": "20%" },
                            { "sWidth": "15%" },
                            { "sWidth": "40%" },
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
        url: "WebServices/VHMSService.svc/SearchPurchaseOrder",
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

                                var table = "<tr id='" + obj[index].PurchaseOrderID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].PurchaseOrderNo + "</td>";
                                table += "<td>" + obj[index].sPurchaseOrderDate + "</td>";
                                table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                                            

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PurchaseOrderID + " class='Print' title='Click here to Print PO'></i><i class='fa fa-print text-green'/></a></td>";

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
                                if (ActionUpdate == "1")
                                { EditRecord($(this).parent().parent()[0].id); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Print").click(function () {
                                SetSessionValue("PurchaseOrderID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintPurchaseOrder.aspx", "MsgWindow");
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
                            { "sWidth": "20%" },
                            { "sWidth": "15%" },
                            { "sWidth": "40%" },
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
});

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

function CalculateAmount() {
    var iOPBillingAmount = 0, iBillingQty = 0;
    //for (var i = 0; i < gOPBillingList.length; i++) {
    //    if (gOPBillingList[i].StatusFlag != "D") {
    //        iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);
    //        iBillingQty = iBillingQty + parseFloat(gOPBillingList[i].Quantity);
    //        }
    //}
 //   $("#txtTotalQuantity").val(parseInt(iBillingQty));
  //  $("#txtNetAmount").val(parseFloat(iOPBillingAmount).toFixed(2));
}

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    
    if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
        $.jGrowl("Please select Delivery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDeliveryDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
    }
    else { $("#divDeliveryDate").removeClass('has-error'); }

    if ($("#txtDeliveryDate").val().trim() == "" || $("#txtDeliveryDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBillDate").addClass('has-error'); $("#txtDeliveryDate").focus(); return false;
    }
    else { $("#divBillDate").removeClass('has-error'); }

    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    if ($("#ddlSupplierName").val() == "0" || $("#ddlSupplierName").val() == undefined || $("#ddlSupplierName").val() == null) {
        $.jGrowl("Please select Supplier", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSupplierName").addClass('has-error'); $("#ddlSupplierName").focus(); return false;
    }
    else { $("#divSupplierName").removeClass('has-error'); }


    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++)
        iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].Subtotal);

    var ObjOPBilling = new Object();
    var ObjSupplier = new Object();
    ObjSupplier.SupplierID = $("#ddlSupplierName").val();
    ObjOPBilling.Supplier = ObjSupplier;
    ObjOPBilling.PurchaseOrderID = 0;
    ObjOPBilling.PurchaseOrderNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sPurchaseOrderDate = $("#txtBillDate").val().trim();
    ObjOPBilling.sDeliveryDate = $("#txtDeliveryDate").val().trim();
    ObjOPBilling.Comments = $("#txtComments").val().trim();
    ObjOPBilling.PurchaseOrderTrans = gOPBillingList;

    if ($("#hdnPurchaseOrderID").val() > 0) {
        ObjOPBilling.PurchaseOrderID = $("#hdnPurchaseOrderID").val();
        gOPBillingList.PurchaseOrderID = ObjOPBilling.PurchaseOrderID;
        ObjOPBilling.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdatePurchaseOrder";
    }
    else {
        sMethodName = "AddPurchaseOrder";
        ObjOPBilling.PurchaseOrderID = 0
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
                        if (sMethodName == "AddPurchaseOrder") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPurchaseOrderID").val(objResponse.Value);
                            EditRecord($("#hdnPurchaseOrderID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            SetSessionValue("PurchaseOrderID", $("#hdnPurchaseOrderID").val());
                            var myWindow = window.open("PrintPurchaseOrder.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                        }
                        else if (sMethodName == "UpdatePurchaseOrder")
                        {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPurchaseOrderID").val(objResponse.Value);
                            EditRecord($("#hdnPurchaseOrderID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            SetSessionValue("PurchaseOrderID", $("#hdnPurchaseOrderID").val());
                            var myWindow = window.open("PrintPurchaseOrder.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
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

$("#btnPrintbill").click(function () {
    SetSessionValue("PurchaseOrderID", $("#hdnPurchaseOrderID").val());
    var myWindow = window.open("PrintPurchaseOrder.aspx", "MsgWindow");
});


$("#ddlSupplierName").change(function () {
    var iSupplierID = $("#ddlSupplierName").val();
    if (iSupplierID != undefined && iSupplierID > 0) {
        GetProductList("ddlProductName");
        ClearOPBillingFields();
        // $("#ddlTaxName").change();
    }
});
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPurchaseOrderByID",
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
                            $("#txtComments").val(obj.Comments);
                            $("#hdnPurchaseOrderID").val(obj.PurchaseOrderID)
                            $("#txtBillNo").val(obj.PurchaseOrderNo);
                            $("#txtBillDate").val(obj.sPurchaseOrderDate);
                            //$("#txtComments").val(obj.Comments);
                            $("#txtDeliveryDate").val(obj.sDeliveryDate);
                            $("#ddlSupplierName").val(obj.Supplier.SupplierID).change();

                            gOPBillingList = [];
                            var ObjProduct = obj.PurchaseOrderTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                var objMagazine = new Object();
                                objMagazine.ProductID = ObjProduct[index].Product.ProductID;
                                objMagazine.ProductName = ObjProduct[index].Product.ProductName;
                                objMagazine.SMSCode = ObjProduct[index].Product.SMSCode;
                                objMagazine.ProductCode = ObjProduct[index].Product.ProductCode;
                                objTemp.Product = objMagazine;

                                objTemp.PurchaseOrderTransID = ObjProduct[index].PurchaseOrderTransID;
                                objTemp.PurchaseOrderID = ObjProduct[index].PurchaseOrderID;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Quantity = ObjProduct[index].Quantity;

                                AddOPBillingData(objTemp);
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
        url: "WebServices/VHMSService.svc/DeletePurchaseOrder",
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
