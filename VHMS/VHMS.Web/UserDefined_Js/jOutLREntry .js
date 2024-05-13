var gMagazineData = [];
var gOPBillingList = [];
var RecordAvailable = 0;
$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    SalesEntryID = _CMDSalesEntryID;

    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    GetCustomerList("ddlCustomerName");
    GetTransportName();
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
    if (SalesEntryID != undefined && SalesEntryID.length > 0) {
        GetLREntrySalesID(SalesEntryID)

        if ($("#hdnSalesID").val() > 0)

            EditRecord($("#hdnSalesID").val());
        else {
            $("#btnAddNew").click();
            GetSalesEntryID(SalesEntryID);
        }
        SetSessionValue("SalesEntryID", "");
    }
    pLoadingSetup(true);
    GetRecord();

});

function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}
$("#btnAddNew").click(function () {
    $("#imagefile2").val("");
    return false;
});

$("#btnClearImage1").click(function () {
    $get("imgUpload2_view").src = "";
    $("#imagefile2").val("");
});

$("#txtInvoiceNo").change(function () {
    if ($("#txtInvoiceNo").val().length > 0) {
        GetSalesDetails($("#txtInvoiceNo").val());
    }
});



function GetLREntrySalesID(SalesEntryID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLREntrySalesID",
        data: JSON.stringify({ ID: SalesEntryID }),
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
                            $("#hdnSalesID").val(parseInt(obj.LREntryID));
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
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}


function GetSalesEntryID(SalesEntryID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByID",
        data: JSON.stringify({ ID: SalesEntryID }),
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
                            $("#txtInvoiceNo").val(obj.InvoiceNo).change();
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

function GetTransportName() {
    dProgress(true);
    $("#ddlTransport").empty();
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
                                    $("#ddlTransport").append("<option value='" + obj[index].TransportID + "'>" + obj[index].TransportName + "</option>");
                            }
                            $("#ddlTransport").val($("#ddlTransport option:first").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlTransport").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlTransport").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

function GetSalesDetails(id) {
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
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#hdnSalesID").val(obj.SalesEntryID);
                            $("#ddlShippingAddress").val(obj.ShippingAddress.ShippingAddressID).change();
                            $("#ddlTransport").val(obj.Transport.TransportID).change();
                            $("#txtVehicleNo").val(obj.VehicleNo);
                            $("#txtEWayNo").val(obj.EWayNo);
                            $("#txtNoofbundles").val(obj.NoofBags);
                            // $("#txtBillNo").val(obj.LRNo);
                            // $("#txtBillDate").val(obj.sLRDate);

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

$("#btnLink").click(function () {
    var myWindow = window.open("https://docs.ewaybillgst.gov.in/", "MsgWindow");
});


$("#txtRate,#txtQuantity").change(function () {

    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    var iSubTotal = parseFloat(iRate) * parseFloat(iqty);
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
    //var iRate = parseFloat(iSubTotal) / parseFloat(iqty);
    //$("#txtRate").val(parseFloat(iRate).toFixed(2));
});

$("#txtSubTotal,#txtQuantity").change(function () {

    var iSubTotal1 = parseFloat($("#txtSubTotal").val());
    var iqty1 = parseFloat($("#txtQuantity").val());

    if (isNaN(iSubTotal1)) iSubTotal1 = 0;
    if (isNaN(iqty1)) iqty1 = 0;
    //var iSubTotal = parseFloat(iRate) * parseFloat(iqty);
    //$("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
    var iRate = parseFloat(iSubTotal1) / parseFloat(iqty1);
    $("#txtRate").val(parseFloat(iRate).toFixed(2));
});

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnLREntryID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").hide();
    $("#divOPBilling").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

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
    $("#hdnLREntryID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});
function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtNetAmount").val("0");
    $("#ddlCustomerName").val(null).change();
    $("#ddlShippingAddress").val(null).change();
    $("#ddlTransport").val(null).change();
    $("#txtFollwedPerson").val("");
    $("#txtDeliveryDate").val("");
    $("#txtName").val("");
    $("#txtInvoiceNo").val("");
    $("#txtVehicleNo").val("");
    $("#txtNoofbundles").val("");
    $("#txtOPDNo").val("");
    $("#txtAWBNo").val("");

    gOPBillingList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#txtBillNo").attr("disabled", false);
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
                            var ObjProduct = obj.ShippingAddress;
                            $("#ddlShippingAddress").empty();
                            for (var index = 0; index < ObjProduct.length; index++) {
                                $("#ddlShippingAddress").append("<option value='" + ObjProduct[index].ShippingAddressID + "'>" + ObjProduct[index].BranchName + "</option>");
                            }
                            $("#ddlShippingAddress").val($("#ddlShippingAddress option:first").val()).change();
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

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    if ($("#txtProduct").val() == "" || $("#txtProduct").val() == undefined || $("#txtProduct").val() == null) {
        $.jGrowl("Please enter Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divProduct").addClass('has-error'); $("#txtProduct").focus(); return false;
    } else { $("#divProduct").removeClass('has-error'); }

    if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <= 0) {
        $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    } else { $("#divQuantity").removeClass('has-error'); }

    if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null || $("#txtRate").val() <= 0) {
        $.jGrowl("Please enter Charges", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    } else { $("#divRate").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.LREntryID = 0;

    ObjData.InvoiceNo = "";
    ObjData.ProductDescription = $("#txtProduct").val();
    ObjData.Rate = parseFloat($("#txtRate").val());
    ObjData.Quantity = parseFloat($("#txtQuantity").val());
    ObjData.SubTotal = parseFloat($("#txtSubTotal").val());


    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.LREntryID = 0;
        ObjData.StatusFlag = "I";
        AddOPBillingData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnLREntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.LREntryID = $("#hdnLREntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.LREntryID = 0;
        }
        Update_OPBilling(ObjData);
    }
    CalculateAmount();
    ClearOPBillingFields();
    $("#ddlProductName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlProductName").val(null).change();

    $("#txtCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtSubTotal").val("0");
    $("#txtProduct").val("");

    $("#hdnOPSNo").val("");
    $("#ddlProductName").val(null).change();
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

    if (gData.length >= 5) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity / KG</th>";
        sTable += "<th class='" + sColorCode + "'>Charges</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].ProductDescription + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
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

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            $("#hdnLREntryID").val(data[i].LREntryID);
            $("#txtProduct").val(data[i].ProductDescription);
            $("#txtQuantity").val(data[i].Quantity);
            $("#txtRate").val(data[i].Rate);
            $("#txtSubTotal").val(data[i].SubTotal);
        }
    }
    return false;
}
function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].LREntryID = oData.LREntryID;
            gOPBillingList[i].InvoiceNo = oData.InvoiceNo;
            gOPBillingList[i].ProductDescription = oData.ProductDescription;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].SubTotal = oData.SubTotal;

            gOPBillingList[i].Quantity = oData.Quantity;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayOPBillingList(gOPBillingList);
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    ClearOPBillingFields();
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
        url: "WebServices/VHMSService.svc/GetLREntry",
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
                                if (obj[index].IsCancelled == "0") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].LREntryID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].LREntryNo + "</td>";
                                table += "<td>" + obj[index].sLREntryDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td>" + obj[index].Transport.TransportName + "</td>";
                                table += "<td>" + obj[index].InvoiceNo + "</td>";
                                table += "<td>" + obj[index].Status + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='WhatsApp' title='Click here to send WhatsApp SMS'><i class='fa fa-lg fa-whatsapp text-green'/></a></td>";
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
                                if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
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
                            $(".WhatsApp").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to send WhatsApp SMS ?')) { SendWhatsAppSMS($(this).parent().parent()[0].id); }
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
                            { "sWidth": "15%" },
                            { "sWidth": "20%" },
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
        url: "WebServices/VHMSService.svc/SearchLREntry",
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

                                var table = "<tr id='" + obj[index].LREntryID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].LREntryNo + "</td>";
                                table += "<td>" + obj[index].sLREntryDate + "</td>";
                                table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                table += "<td>" + obj[index].Transport.TransportName + "</td>";
                                table += "<td>" + obj[index].InvoiceNo + "</td>";
                                table += "<td>" + obj[index].Status + "</td>";

                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='WhatsApp' title='Click here to send WhatsApp SMS'><i class='fa fa-lg fa-whatsapp text-green'/></a></td>";
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
                                if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintOPBilling").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnLREntryID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("LREntryID", AdmissionID);

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
                            $(".WhatsApp").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to send WhatsApp SMS ?')) { SendWhatsAppSMS($(this).parent().parent()[0].id); }
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
                            { "sWidth": "15%" },
                            { "sWidth": "20%" },
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
});

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

function CalculateAmount() {
    var iOPBillingAmount = 0, iBillingQty = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);
        }
    }
    $("#txtNetAmount").val(parseFloat(iOPBillingAmount).toFixed(2));
}

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

    //if ($("#txtDeliveryDate").val().trim() == "" || $("#txtDeliveryDate").val().trim() == undefined) {
    //    $.jGrowl("Please select Delivery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divDeliveryDate").addClass('has-error'); $("#txtDeliveryDate").focus(); return false;
    //}
    //else { $("#divDeliveryDate").removeClass('has-error'); }

    var gCount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            gCount++;

    }
    if (gCount == 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }


    if ($("#ddlCustomerName").val() == "0" || $("#ddlCustomerName").val() == undefined || $("#ddlCustomerName").val() == null) {
        $.jGrowl("Please select Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomerName").addClass('has-error'); $("#ddlCustomerName").focus(); return false;
    }
    else { $("#divCustomerName").removeClass('has-error'); }

    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++)
        iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);

    var ObjOPBilling = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#ddlCustomerName").val();
    ObjOPBilling.Customer = ObjCustomer;

    var ObjTransport = new Object();
    ObjTransport.TransportID = $("#ddlTransport").val();
    ObjOPBilling.Transport = ObjTransport;

    var ObjShippingAddress = new Object();
    ObjShippingAddress.ShippingAddressID = $("#ddlShippingAddress").val();
    ObjOPBilling.ShippingAddress = ObjShippingAddress;

    ObjOPBilling.LREntryID = 0;
    ObjOPBilling.LREntryNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sLREntryDate = $("#txtBillDate").val().trim();
    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.FrieghtCharges = $("#txtNetAmount").val().trim();
    ObjOPBilling.Description = $("#txtDescription").val().trim();
    ObjOPBilling.VehicleType = $("#txtVehicleType").val().trim();
    ObjOPBilling.VehicleNo = $("#txtVehicleNo").val().trim();
    ObjOPBilling.Status = $("#ddlStatus").val();
    ObjOPBilling.Payment = $("#ddlPayment").val();
    ObjOPBilling.LREntryTrans = gOPBillingList;
    ObjOPBilling.TransportBranch = $("#txtBranch").val();
    ObjOPBilling.EWayNo = $("#txtEWayNo").val();
    ObjOPBilling.AWBNo = $("#txtAWBNo").val();

    ObjOPBilling.NoofBags = $("#txtNoofbundles").val();
    ObjOPBilling.sDeliveryDate = $("#txtDeliveryDate").val().trim();
    ObjOPBilling.FollowedPerson = $("#txtFollwedPerson").val();
    ObjOPBilling.SalesEntryID = $("#hdnSalesID").val();
    if ($("#hdnLREntryID").val() > 0) {
        ObjOPBilling.LREntryID = $("#hdnLREntryID").val();
        gOPBillingList.LREntryID = ObjOPBilling.LREntryID;
        ObjOPBilling.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdateLREntry";
    }
    else {
        sMethodName = "AddLREntry";
        ObjOPBilling.LREntryID = 0
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
                            $("#hdnLREntryID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateOPBilling") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();

                        $("#hdnLREntryID").val("0");
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
            //console.log(e);
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLREntryByID",
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

                            $("#txtInvoiceNo").val(obj.InvoiceNo).change();
                            $("#hdnLREntryID").val(obj.LREntryID)
                            $("#txtBillNo").val(obj.LREntryNo);
                            $("#txtBillDate").val(obj.sLREntryDate);
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#ddlTransport").val(obj.Transport.TransportID).change();
                            $("#ddlShippingAddress").val(obj.ShippingAddress.ShippingAddressID).change();
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtDescription").val(obj.Description);
                            $("#txtVehicleType").val(obj.VehicleType);
                            $("#txtVehicleNo").val(obj.VehicleNo);
                            $("#ddlStatus").val(obj.Status).change();
                            $("#ddlPayment").val(obj.Payment).change();
                            $("#txtAWBNo").val(obj.AWBNo);
                            $("#txtNoofbundles").val(obj.NoofBags);
                            $("#txtBranch").val(obj.TransportBranch);
                            $("#txtEWayNo").val(obj.EWayNo);
                            $("#hdnSalesID").val(obj.SalesEntryID);
                            $("#txtDeliveryDate").val(obj.sDeliveryDate);
                            $("#txtFollwedPerson").val(obj.FollowedPerson);

                            gOPBillingList = [];
                            var ObjProduct = obj.LREntryTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                var objMagazine = new Object();
                                objTemp.LREntryTransID = ObjProduct[index].LREntryTransID;
                                objTemp.LREntryID = ObjProduct[index].LREntryID;
                                objTemp.InvoiceNo = ObjProduct[index].InvoiceNo;
                                objTemp.ProductDescription = ObjProduct[index].ProductDescription;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
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
        url: "WebServices/VHMSService.svc/DeleteLREntry",
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

function SendWhatsAppSMS(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLREntryByID",
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

                            var Message = (obj.TemplateMessage)

                            if (Message.match(/#Customer_Name#/)) {
                                var CustomerName = obj.Customer.CustomerName
                                var result = CustomerName.replace("&", "AND");
                                Message = Message.replace("#Customer_Name#", result)
                            };
                            if (Message.match(/#InvoiceNo#/))
                                Message = Message.replace("#InvoiceNo#", obj.InvoiceNo ) ;
                            if (Message.match(/#Date#/))
                                Message = Message.replace("#Date#", obj.sInvoiceDate);
                            if (Message.match(/#Total_Qty#/))
                                Message = Message.replace("#Total_Qty#", obj.TotalQty);
                            if (Message.match(/#No_of_Bundles#/))
                                Message = Message.replace("#No_of_Bundles#", obj.NoofBags);
                            if (Message.match(/#Transport#/))
                                Message = Message.replace("#Transport#", obj.Transport.TransportName);
                            if (Message.match(/#LR_No#/))
                                Message = Message.replace("#LR_No#", obj.LREntryNo);
                            if (Message.match(/#LR_Date#/))
                                Message = Message.replace("#LR_Date#", obj.sLREntryDate);


                            if (obj.Customer.WhatsAppNo.length == 10) {
                                var myWindow = window.open("https://wa.me/91" + obj.Customer.WhatsAppNo + "?text=‎" + Message, "MsgWindow");
                                //var myWindow = window.open("`whatsapp://send?text=${myWindow}%0a‎Hello%0aWorld`" + obj.Customer.WhatsAppNo  + "?text="+ Message, "MsgWindow");
                            } else {
                                $.jGrowl("Invalid WhatsApp No", { sticky: false, theme: 'warning', life: jGrowlLife });
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
