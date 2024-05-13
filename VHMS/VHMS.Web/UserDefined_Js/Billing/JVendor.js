var gMagazineData = [];
var gOPBillingList = [];
$(function () {
    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    pLoadingSetup(false);
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    //General Tab
    $("#btnSave").show();
    $("#btnUpdate").hide();
    GetWorkList("ddlWorkName");
    GetRecord();

    var _Tfunctionality;
    if ($.cookie("Vendor") != undefined) {
        _Tfunctionality = $.cookie("Vendor");

        if (_Tfunctionality == "Add New Vendor") {
            pLoadingSetup(true);
            $("#btnAddNew").click();
        }
        $.cookie("Vendor", null);
    }

    pLoadingSetup(true);
});

$("#btnAddNew").click(function () {
    $("#divRecords").hide();
    $("#tab-modal").show();
    $("#btnList").show();
    $("#btnAddNew").hide();
    $("#hdnVendorID").val("");
    $("#ddlWorkName").val(null).change();
    $("#txtSubTotal").val(0);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#aGeneral").click();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    gOPBillingList = [];
    ClearVendorTab();
    $("#divOPBillingList").empty();
    $("#txtComments").val("");
    return false;
});
$("#btnList").click(function () {
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnList").hide();
    $("#btnAddNew").show();
    $("#aGeneral").click();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $.cookie("Vendor", null);
    GetRecord();
    return false;
});

$("#btnClose").click(function () {
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnList").hide();
    $("#btnAddNew").show();
    $("#aGeneral").click();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $.cookie("Vendor", null);
    gOPBillingList = [];
    GetRecord();
    return false;
});

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetVendor",
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
                                if (obj[index].IsActive == "1")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                var table = "<tr id='" + obj[index].VendorID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].VendorName + "</td>";
                                table += "<td>" + obj[index].VendorCode + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].PhoneNo1 + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].VendorAddress + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";
                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SupplierID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SupplierID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SupplierID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }
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
                                    $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });

                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to delete the selected record ?'))
                                    { DeleteRecord($(this).parent().parent()[0].id); }
                                }
                                else {
                                    $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife });
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
                            { "sWidth": "30%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "25%" },
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
function ClearVendorTab() {
    $("#txtName").val("");
    $("#txtCode").val("");
    $("#txtAddress").val("");
    $("#txtPhoneNo1").val("");
    $("#txtPhoneNo2").val("");
    $("#txtPANNo").val("");
    $("#chkStatus").prop("checked", true);

    gOPBillingList = [];
    $("#divName").removeClass('has-error');
    $("#divCode").removeClass('has-error');
    $("#divPhoneNo1").removeClass('has-error');
}

function GetWorkList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetWork",
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
                                    $(sControlName).append("<option value='" + obj[index].WorkID + "'>" + obj[index].WorkName + "</option>");
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


$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    if ($("#ddlWorkName").val() == "0" || $("#ddlWorkName").val() == undefined || $("#ddlWorkName").val() == null) {
        $.jGrowl("Please select Work", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWork").addClass('has-error'); $("#ddlWorkName").focus(); return false;
    } else { $("#divWork").removeClass('has-error'); }

    if ($("#txtSubTotal").val() == "" || $("#txtSubTotal").val() == undefined || $("#txtSubTotal").val() == null || $("#txtSubTotal").val() <= 0) {
        $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSubTotal").addClass('has-error'); $("#txtSubTotal").focus(); return false;
    } else { $("#divSubTotal").removeClass('has-error'); }


    var ObjData = new Object();
    ObjData.VendorID = 0;

    ObjData.WorkID = $("#ddlWorkName").val();
    ObjData.Amount = $("#txtSubTotal").val();;
    ObjData.WorkName = $("#ddlWorkName option:selected").text();

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.length + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.VendorID = 0;
        ObjData.StatusFlag = "I";
        AddOPBillingData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnVendorEntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.VendorID = $("#hdnVendorEntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.VendorID = 0;
        }
        Update_OPBilling(ObjData);
    }
    ClearOPBillingFields();
    $("#ddlWorkName").focus();
});

function ClearOPBillingFields() {
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#txtSubTotal").val("0");
    $("#ddlWorkName").val(null).change();
    $("#divWork").removeClass('has-error');
    $("#divSubTotal").removeClass('has-error');
    return false;
}

function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlWorkName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOPSNo").val(ID);
            $("#hdnVendorEntryID").val(data[i].VendorID);
            $("#ddlWorkName").val(data[i].WorkID).change();
            $("#txtSubTotal").val(data[i].Amount);

        }
    }
    return false;
}

function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].VendorID = oData.VendorID;
            gOPBillingList[i].WorkID = oData.WorkID;
            gOPBillingList[i].WorkName = oData.WorkName;
            gOPBillingList[i].Amount = oData.Amount;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayOPBillingList(gOPBillingList);
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
            }
        }
    }
    return false;
}


function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
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
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:5px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "' style='width:250px;'>Job Work</th>";
        sTable += "<th class='" + sColorCode + "' style='width:100px;'>Amount</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].WorkName + "</td>";
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

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave")
    { if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
    else
    { if (ActionUpdate != "1") { $.Growl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

    if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
        $.jGrowl("Please enter Vendor Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
    } else { $("#divName").removeClass('has-error'); }

    if ($("#txtCode").val().trim() == "" || $("#txtCode").val().trim() == undefined) {
        $.jGrowl("Please enter Vendor Code", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCode").addClass('has-error'); $("#txtCode").focus(); return false;
    } else { $("#divCode").removeClass('has-error'); }

    if ($("#txtPhoneNo1").val().trim() == "" || $("#txtPhoneNo1").val().trim() == undefined) {
        $.jGrowl("Please enter Phone No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPhoneNo1").addClass('has-error'); $("#txtPhoneNo1").focus(); return false;
    } else { $("#divPhoneNo1").removeClass('has-error'); }

    if ($("#txtPANNo").val().trim() == "" || $("#txtPANNo").val().trim() == undefined) {
        $.jGrowl("Please enter PAN No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPANNo").addClass('has-error'); $("#txtPANNo").focus(); return false;
    } else { $("#divPANNo").removeClass('has-error'); }

    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCode").focus(); return false;
    }

    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            iOPBillingAmount = iOPBillingAmount + 1;
    }
    if (iOPBillingAmount <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#ddlWorkName").focus(); return false;
    }

    SaveandUpdateVendor();
    return false;
});
function SaveandUpdateVendor() {
    var Obj = new Object();
    Obj.VendorID = 0;
    Obj.VendorName = $("#txtName").val().toUpperCase();
    Obj.VendorCode = $("#txtCode").val().trim();
    Obj.VendorAddress = $("#txtAddress").val().trim();
    Obj.PhoneNo1 = $("#txtPhoneNo1").val().trim();
    Obj.PhoneNo2 = $("#txtPhoneNo2").val().trim();
    Obj.PANNo = $("#txtPANNo").val().trim();
    Obj.Comments = $("#txtComments").val().trim();
    Obj.VendorWorkTrans = gOPBillingList;
    Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";

    var ObjState = new Object();
    ObjState.StateID = $("#ddlState").val();
    Obj.State = ObjState;

    if ($("#hdnVendorID").val() > 0) {
        Obj.VendorID = $("#hdnVendorID").val();
        sMethodName = "UpdateVendor";
    }
    else { sMethodName = "AddVendor"; }

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: Obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearVendorTab();
                        //GetRecord();
                        if (sMethodName == "AddVendor") {
                            $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnVendorID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateVendor")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        //EditRecord($("#hdnVendorID").val());
                        $("#btnList").click();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Vendor_A_01") {
                        $.jGrowl("Name Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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
        url: "WebServices/VHMSService.svc/GetVendorByID",
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
                            $("#btnAddNew").hide();
                            $("#btnList").show();
                            $("#btnSave").hide();
                            $("#btnUpdate").show();

                            $("#tab-modal").show();
                            $("#aGeneral").click();
                            $("#divRecords").hide();

                            ClearVendorTab();

                            $("#hdnVendorID").val(obj.VendorID);
                            $("#txtName").val(obj.VendorName);
                            $("#txtCode").val(obj.VendorCode);
                            $("#txtAddress").val(obj.VendorAddress);
                            $("#txtPhoneNo1").val(obj.PhoneNo1);
                            $("#txtPhoneNo2").val(obj.PhoneNo2);
                            $("#txtPANNo").val(obj.PANNo);
                            $("#txtComments").val(obj.Comments);

                            $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                            gOPBillingList = [];

                            var ObjProduct = obj.VendorWorkTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                objTemp.VendorWorkTransID = ObjProduct[index].VendorWorkTransID;
                                objTemp.VendorID = ObjProduct[index].VendorID;
                                objTemp.WorkID = ObjProduct[index].WorkID;
                                objTemp.WorkName = ObjProduct[index].WorkName;
                                objTemp.Amount = ObjProduct[index].Amount;

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
$("#ddlWorkName").change(function () {
    if ($("#ddlWorkName").val() > 0)
        GetWorkRate($("#ddlWorkName").val());
});

function GetWorkRate(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetWorkRate",
        data: JSON.stringify({ ID: id, VendorID: $("#ddlVendorName").val() }),
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
                            $("#txtSubTotal").val(obj.Amount);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#txtSubTotal").val("0");
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

function DeleteRecord(id) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteVendor",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearVendorTab();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Vendor_R_01" || objResponse.Value == "Vendor_D_01") {
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