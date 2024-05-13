var gMagazineData = [];
var gBranchMoveList = [];
var iBranchID = 0;
$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    iBranchID = _BranchID

    if (ActionAdd != "1") {
        $("#btnAddNew").remove();
        $("#btnSave").remove();
    }

    if (ActionUpdate != "1") {
        $("#btnUpdate").remove();
    }

    $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    //var date = new Date()

    GetBranch("ddlFromBranch");
    GetBranch("ddlToBranch");
    GetSettings();
    pLoadingSetup(true);
    GetPassword();
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
function GetBranch(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetMainBranch",
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
                                    $(sControlName).append("<option value='" + obj[index].BranchID + "'>" + obj[index].BranchName + "</option>");
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

$("#ddlFromBranch").change(function () {
    dProgress(true);
    var id = $("#ddlFromBranch").val();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetBranchByID",
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

                            $("#txtFromAddress").text(obj.Address);

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

$("#ddlToBranch").change(function () {
    dProgress(true);

    var id = $("#ddlToBranch").val();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetBranchByID",
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

                            $("#txtToAddress").text(obj.Address);

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

function Edit_BranchMoveDetail(ID) {
    Bind_BranchMoveByID(ID, gBranchMoveList);
    return false;
}

$("#txtBarcode").change(function () {
    var iStatus;
    dProgress(true);
    var id = $("#txtBarcode").val();
    if ($("#hdnBranchMoveID").val() > 0)
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
                            $("#txtGross").val(obj.NetWeight);
                            $("#txtQuantity").val(1);
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
    $("#hdnBranchMoveID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#ddlFromBranch").val(iBranchID).change();

    $("#btnSave").show();
    $("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Branch Move");
    $('#compose-modal').modal({ show: true, backdrop: true });

    gBranchMoveList = [];
    ClearBranchMoveTab();
    //$('#txtBranchMoveDate').datepicker('setDate', 'today');
    //ClearFields();
    $("#divBranchMoveList").empty();

    $("#txtDate").focus();
    $("#ddlFromBranch").focus();
    return false;
});

function PrintBranchMoveDetails() {
    SetSessionValue("AdmissionID", $("#hdnBranchMoveID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewBranchMoveDetails.aspx", "_blank");
    return false;
}

$("#btnPrint").click(function () {

    SetSessionValue("BranchMoveID", $("#hdnBranchMoveID").val());

    var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
    // PrintBranchMoveDetails();
});

$("#btnList").click(function () {

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divBranchMove").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $('#compose-modal').modal('hide');
    $("#hdnBranchMoveID").val("0");
    gBranchMoveList = [];
    ClearBranchMoveTab();
    return false;
});
function ClearBranchMoveTab() {
    $("#txtMoveNo").val("");
    $("#txtDate").val("");
    $("#hdnStockID").val("");
    $("#txtGrossWeight").val("0");
    $("#txtOtherPassword").val("");
    $("#txtTotalQuantity").val("0");
    gBranchMoveList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();

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

    if ($("#hdnStockID").val() == "0" || $("#hdnStockID").val() == undefined || $("#hdnStockID").val() == null) {
        $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    }
    else { $("#divBarcode").removeClass('has-error'); }

    if ($("#hdnStockID").val() == "0" || $("#hdnStockID").val() == undefined || $("#hdnStockID").val() == "" || $("#hdnStockID").val() == null) {
        $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    }
    else { $("#divBarcode").removeClass('has-error'); }

    //if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null) {
    //    $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
    //} else { $("#divAmount").removeClass('has-error'); }

    var iStockCount = 0;
    for (var i = 0; i < gBranchMoveList.length; i++) {
        if (gBranchMoveList[i].Stock.StockID == $("#hdnStockID").val())
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
    ObjData.BranchMoveTransID = 0;
    var ObjStock = new Object();
    ObjStock.StockID = $("#hdnStockID").val();
    ObjData.Stock = ObjStock;
    ObjData.Quantity = 1;
    ObjData.Barcode = $("#txtBarcode").val();
    ObjData.NetWeight = $("#txtGross").val();
    ObjData.ProductName = $("#txtProduct").val();
    ObjData.CategoryName = $("#txtCategory").val();
    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gBranchMoveList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.BranchMoveTransID = 0;
        ObjData.StatusFlag = "I";
        AddBranchMoveData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnBranchMoveID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.BranchMoveID = $("#hdnBranchMoveID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.BranchMoveID = 0;
        }
        Update_BranchMove(ObjData);
    }
    CalculateAmount();
    ClearBranchMoveFields();
    $("#ddlDescriptionName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearBranchMoveFields() {
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#txtQuantity").val("0");
    $("#txtProduct").val("");
    $("#txtBarcode").val("");
    $("#txtGross").val(""); 
    $("#hdnStockID").val("");
    $("#txtCategory").val("");

    $("#divBarcode").removeClass('has-error');
    return false;
}
function AddBranchMoveData(oData) {
    gBranchMoveList.push(oData);
    DisplayBranchMoveList(gBranchMoveList);
    return false;
}
function DisplayBranchMoveList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divBranchMoveList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divBranchMoveList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblBranchMoveList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Barcode</th>";
        sTable += "<th class='" + sColorCode + "'>Category</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Gross Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Quantity</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblBranchMoveList_body'>";
        sTable += "</tbody></table>";
        $("#divBranchMoveList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Barcode + "</td>";
                sTable += "<td>" + gData[i].CategoryName + "</td>";
                sTable += "<td>" + gData[i].ProductName + "</td>";
                sTable += "<td>" + gData[i].NetWeight + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_BranchMoveDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_BranchMoveDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblBranchMoveList_body").append(sTable);
            }
        }
    }
    else { $("#divBranchMoveList").empty(); }

    return false;
}
function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gBranchMoveList);
    return false;
}
function Bind_BranchMoveByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#txtBarcode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnStockID").val(ID);
            $("#hdnBranchMoveID").val(data[i].BranchMoveID);
            $("#txtBarcode").val(data[i].Barcode).change();
        }
    }
    return false;
}
function Update_BranchMove(oData) {
    for (var i = 0; i < gBranchMoveList.length; i++) {
        if (gBranchMoveList[i].sNO == oData.sNO) {
            gBranchMoveList[i].BranchMoveID = oData.BranchMoveID;
            gBranchMoveList[i].ProductName = oData.ProductName;
            gBranchMoveList[i].CategoryName = oData.CategoryName;
            gBranchMoveList[i].NetWeight = oData.NetWeight;
            gBranchMoveList[i].Quantity = oData.Quantity;
            gBranchMoveList[i].Barcode = oData.Barcode;
            gBranchMoveList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayBranchMoveList(gBranchMoveList);
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    ClearBranchMoveFields();
    $("#txtBarcode").focus();
    return false;
}
function Delete_BranchMoveDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gBranchMoveList.length; i++) {
            if (gBranchMoveList[i].SNo == ID) {
                var index = jQuery.inArray(gBranchMoveList[i].valueOf("SNo"), gBranchMoveList);
                if (gBranchMoveList[i].SNo > 0) {
                    gBranchMoveList[i].StatusFlag = "D";
                } else {
                    gBranchMoveList.splice(index, 1);
                }
                $("#divBranchMoveList").empty();
                DisplayBranchMoveList(gBranchMoveList);
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
        url: "WebServices/VHMSService.svc/GetTopBranchMove",
        data: JSON.stringify({ BranchMoveID: 0 }),
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
                                if (obj[index].Status == "Approved")
                                {TypeStatus = "<span class='label label-success'>Approved</span>"; }
                                if (obj[index].Status == "Waiting for Approval")
                                { TypeStatus = "<span class='label label-warning'>Waiting for Approval</span>"; }
                                else if (obj[index].Status == "Cancelled")
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }
                                else if (obj[index].Status == "Received")
                                { TypeStatus = "<span class='label label-info'>Received</span>"; }

                                var table = "<tr id='" + obj[index].BranchMoveID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].MoveNo + "</td>";
                                table += "<td>" + obj[index].sMoveDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].FromBranch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].ToBranch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalWeight + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalQuantity + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1" && obj[index].Status == "Waiting for Approval")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1" && obj[index].Status == "Waiting for Approval")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (obj[index].Status == "Waiting for Approval" && ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Approve' style='color:green;' title='Click here to Approve'>Approve</a></td>";
                                }
                                else if (obj[index].Status == "Approved" && obj[index].ToBranch.BranchID == iBranchID)
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Receive' style='color:green;' title='Click here to Approve'>Receive</i></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (obj[index].Status == "Waiting for Approval" && ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Cancel' style='color:red;' title='Click here to Cancel'>Cancel</a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Branch");
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();
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
                            $(".Approve").click(function () {
                                if (ActionUpdate == "1")
                                { //StockStatusUpdate($(this).parent().parent()[0].id, "Approved");
                                    ShowReceiptNo($(this).parent().parent()[0].id);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Receive").click(function () {
                                if (ActionUpdate == "1")
                                { StockStatusUpdate($(this).parent().parent()[0].id, "Received"); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Cancel").click(function () {
                                if (ActionUpdate == "1")
                                { StockStatusUpdate($(this).parent().parent()[0].id, "Cancelled"); }
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
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "1%" },
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

$("#btnSubmit").click(function () {
    
        if ($("#txtOtherPassword").val().trim() == "" || $("#txtOtherPassword").val().trim() == undefined || $("#txtOtherPassword").val().trim() != $("#hdRS").val()) {
            $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divOtherPassword").addClass('has-error'); $("#txtOtherPassword").focus(); return false;
        } else { $("#divOtherPassword").removeClass('has-error'); }
    
   
        $('#Renewalmodal').modal('hide');
        StockStatusUpdate($('#hdnIDS').val(), "Approved");
        // ShowReceiptNo($(this).parent().parent()[0].id);
      
        GetRecord();
    
        return false;
    

});


function ShowReceiptNo(id) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopBranchMove",
        data: JSON.stringify({ BranchMoveID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $('#Renewalmodal').modal({ show: true, backdrop: true });
                            $('#hdnIDS').val(id);
                            $(".modal-title").html("<i class='fa fa-info-circle'></i>&nbsp;&nbsp;Branch Move Details");
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



function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SearchBranchMove",
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
                                if (obj[index].Status == "Approved")
                                { TypeStatus = "<span class='label label-success'>Approved</span>"; }
                                else if (obj[index].Status == "Waiting for Approval")
                                { TypeStatus = "<span class='label label-warning'>Waiting for Approval</span>"; }
                                else if (obj[index].Status == "Cancelled")
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }
                                else if (obj[index].Status == "Received")
                                { TypeStatus = "<span class='label label-info'>Received</span>"; }

                                var table = "<tr id='" + obj[index].BranchMoveID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].MoveNo + "</td>";
                                table += "<td>" + obj[index].sMoveDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].FromBranch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].ToBranch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalQuantity + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].TotalWeight + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1" && obj[index].Status == "Waiting for Approval")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1" && obj[index].Status == "Waiting for Approval")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (obj[index].Status == "Waiting for Approval" && ActionDelete == "1") {
                                    table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Approve' style='color:green;' title='Click here to Approve'>Approve</a></td>";
                                }
                                else if (obj[index].Status == "Approved" && obj[index].ToBranch.BranchID == iBranchID)
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Receive' style='color:green;' title='Click here to Approve'>Receive</i></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (obj[index].Status == "Waiting for Approval" && ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].BranchMoveID + " class='Cancel' style='color:red;' title='Click here to Cancel'>Cancel</a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "</tr>";
                                $("#tblSearchResult_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Branch");
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();
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
                            $(".Approve").click(function () {
                                if (ActionUpdate == "1")
                                { //StockStatusUpdate($(this).parent().parent()[0].id, "Approved");
                                    ShowReceiptNo($(this).parent().parent()[0].id);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Receive").click(function () {
                                if (ActionUpdate == "1")
                                { StockStatusUpdate($(this).parent().parent()[0].id, "Received"); }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Cancel").click(function () {
                                if (ActionUpdate == "1")
                                { StockStatusUpdate($(this).parent().parent()[0].id, "Cancelled"); }
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
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "1%" },
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

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

function CalculateAmount() {
    var iBranchMoveAmount = 0
    var iBranchMoveGross= 0

    var iTotal = 0
    for (var i = 0; i < gBranchMoveList.length; i++) {
        if (gBranchMoveList[i].StatusFlag != "D")
            iBranchMoveAmount = iBranchMoveAmount + parseFloat(gBranchMoveList[i].Quantity);
        iBranchMoveGross = iBranchMoveGross+parseFloat(gBranchMoveList[i].NetWeight)
    }
    $("#txtTotalQuantity").val(iBranchMoveAmount);
    $("#txtGrossWeight").val(iBranchMoveGross);
}

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    //if ($("#txtBranchMoveNo").val().trim() == "" || $("#txtBranchMoveNo").val().trim() == undefined) {
    //    $.jGrowl("Please enter BranchMove No", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divBillNo").addClass('has-error'); $("#txtBranchMoveNo").focus(); return false;
    //}
    //else { $("#divBillNo").removeClass('has-error'); }

    var d1 = Date.parse($("#hdnOpeningDate").val());
    var d2 = Date.parse($("#txtDate").val());
    if (d1 < d2) {
        $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
        return false;
    }

    if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
        $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
    }
    else { $("#divDate").removeClass('has-error'); }

    if ($("#ddlFromBranch").val() == "0" || $("#ddlFromBranch").val() == undefined) {
        $.jGrowl("Please Select From Branch", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divFromBranch").addClass('has-error'); $("#ddlFromBranch").focus(); return false;
    } else { $("#divFromBranch").removeClass('has-error'); }
    if ($("#ddlToBranch").val() == "0" || $("#ddlToBranch").val() == undefined) {
        $.jGrowl("Please Select To Branch", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divToBranch").addClass('has-error'); $("#ddlToBranch").focus(); return false;
    } else { $("#divToBranch").removeClass('has-error'); }

    if ($("#ddlToBranch").val() == iBranchID) {
        $.jGrowl("Please select different Branch", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divToBranch").addClass('has-error'); $("#ddlToBranch").val(0);
        $("#ddlToBranch").focus(); return false;
    }
    else { $("#divToBranch").removeClass('has-error'); }

    if (gBranchMoveList.length <= 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    var gCount = 0;
    for (var i = 0; i < gBranchMoveList.length; i++) {
        if (gBranchMoveList[i].StatusFlag != "D")
            gCount++;

    }
    if (gCount == 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    var ObjBranchMove = new Object();
    var objFromBranch = new Object();
    objFromBranch.BranchID = $("#ddlFromBranch").val();
    ObjBranchMove.FromBranch = objFromBranch;

    var objToBranch = new Object();
    objToBranch.BranchID = $("#ddlToBranch").val();
    ObjBranchMove.ToBranch = objToBranch;

    ObjBranchMove.BranchMoveID = 0;
    ObjBranchMove.BranchMoveNo = $("#txtMoveNo").val().trim();
    ObjBranchMove.sMoveDate = $("#txtDate").val().trim();
    ObjBranchMove.TotalQuantity = parseFloat($("#txtTotalQuantity").val());
    ObjBranchMove.TotalWeight = parseFloat($("#txtGrossWeight").val());

    ObjBranchMove.BranchMoveTrans = gBranchMoveList;

    if ($("#hdnBranchMoveID").val() > 0) {
        ObjBranchMove.BranchMoveID = $("#hdnBranchMoveID").val();
        sMethodName = "UpdateBranchMove";
    }
    else {
        sMethodName = "AddBranchMove";
        ObjBranchMove.BranchMoveID = 0
    }

    SaveandUpdateBranchMove(ObjBranchMove, sMethodName);

});
function SaveandUpdateBranchMove(ObjBranchMove, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjBranchMove }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearBranchMoveTab();
                        GetRecord();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divBranchMove").hide();
                        if (sMethodName == "AddBranchMove") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnBranchMoveID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateBranchMove")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                        //var AdmissionID = $(this).attr('id');
                        //var r = confirm("Do You Want to Add Exchange for this Invoice?");
                        //if (r == true) {
                        //    SetSessionValue("BranchMoveID", $("#hdnBranchMoveID").val());
                        //    var myWindow = window.open("frmExchange.aspx", "MsgWindow");
                        //}
                        //else
                        //{
                        //    SetSessionValue("BranchMoveID", $("#hdnBranchMoveID").val());
                        //    var myWindow = window.open("frmBranchMove.aspx", "MsgWindow");
                        //}
                        $("#hdnBranchMoveID").val("0");
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
        url: "WebServices/VHMSService.svc/GetBranchMoveByID",
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
                            $("#btnUpdate").show();
                            $("#btnSave").hide();
                            $("#txtBranchMoveNo").attr("disabled", true);
                            // $("#ddlPatient").attr("disabled", true);

                            $("#hdnBranchMoveID").val(obj.BranchMoveID)
                            $("#ddlFromBranch").val(obj.FromBranch.BranchID).change();
                            $("#ddlToBranch").val(obj.ToBranch.BranchID).change();
                            $("#txtMoveNo").val(obj.MoveNo);
                            $("#txtDate").val(obj.sMoveDate);
                            $("#txtTotalQuantity").val(obj.TotalQuantity);
                            $("#txtGrossWeight").val(obj.TotalWeight);

                            gBranchMoveList = [];
                            var ObjProduct = obj.BranchMoveTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                var objStock = new Object();
                                objStock.StockID = ObjProduct[index].StockID;
                                objTemp.Stock = objStock;
                                objTemp.BranchMoveID = ObjProduct[index].BranchMoveID;
                                objTemp.BranchMoveTransID = ObjProduct[index].BranchMoveTransID;
                                objTemp.NetWeight = ObjProduct[index].NetWeight;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.CategoryName = ObjProduct[index].CategoryName;
                                objTemp.ProductName = ObjProduct[index].ProductName;
                                AddBranchMoveData(objTemp);
                            }

                            $('#compose-modal').modal({ show: true, backdrop: true });
                            $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Branch Move");
                            $("#btnUpdateMagazine").hide();
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

function StockStatusUpdate(id, iStatus) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/UpdateMoveStatus",
        data: JSON.stringify({ ID: id, Status: iStatus }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearBranchMoveTab();
                        GetRecord();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divBranchMove").hide();
                        //if (sMethodName == "AddBranchMove") {
                        //    $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                        //    $("#hdnBranchMoveID").val(objResponse.Value);
                        //}
                        //else if (sMethodName == "UpdateBranchMove")
                        //{ $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();

                        $("#hdnBranchMoveID").val("0");
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
        url: "WebServices/VHMSService.svc/DeleteBranchMove",
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
                    else if (objResponse.Value == "BranchMove_R_01" || objResponse.Value == "BranchMove_D_01") {
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
