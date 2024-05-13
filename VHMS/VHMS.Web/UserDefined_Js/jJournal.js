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
    GetBankList("ddlBank");
    GetLedgerList("ddlLedgerName");
    $("#txtBillDate, #txtIssueDate, #txtBDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtBillDate, #txtIssueDate, #txtBDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    
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
    $("#hdnJournalID").val("0");
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
    $("#hdnJournalID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});
function ClearOPBillingTab() {
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtBDate").val("");
    $("#txtExpenseNo").val("");
    $("#txtRoundoff").val("0");
    $("#txtChequeNo").val("");
    $("#txtIssueDate").val("");
    $("#ddlPaymentStatus").val("Cleared");
    $("#ddlReceiptMode").val(0);
    $("#divChequeDetails").hide();
    $("#txtNarration").val("");
    $("#txtDebit").val("0");
    $("#txtCredit").val("0");
    $("#ddlLedgerName").val(null).change();
    $("#txtTotalAmount").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    gOPBillingList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    GetLedgerList("ddlLedgerName");
    $("#txtBillNo").attr("disabled", false);
    GetSettings();
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
                            $("#ddlReceiptMode").val(obj.OtherReceiptModeID).change();
                            $("#ddlBank").val(obj.OtherExpenseBank.LedgerID).change();
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

function GetLedgerList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLedgerExpense",
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
                            $("#ddlLedgerName").val($("#ddlLedgerName option:first").val()).change();
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
    if ($("#ddlLedgerName").val() == "0" || $("#ddlLedgerName").val() == undefined || $("#ddlLedgerName").val() == null) {
        $.jGrowl("Please select Ledger", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectLedgerName").addClass('has-error'); $("#ddlLedgerName").focus(); return false;
    }
    else { $("#divSelectLedgerName").removeClass('has-error'); }

    if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null || $("#txtAmount").val() <= 0) {
        $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
    } else { $("#divAmount").removeClass('has-error'); }



    var ObjData = new Object();
    ObjData.JournalID = 0;


    ObjData.LedgerID = $("#ddlLedgerName").val();
    ObjData.LedgerName = $("#ddlLedgerName option:selected").text();
    ObjData.PartyType = "";
    ObjData.CreditOrDebit = $("#ddlType").val();
    ObjData.Notes = $("#txtNotes").val();
    if ($("#ddlType").val() == "Cr") {
        ObjData.CreditAmount = parseFloat($("#txtAmount").val());
        ObjData.DebitAmount = 0;
    }
    else {
        ObjData.CreditAmount = 0;
        ObjData.DebitAmount = parseFloat($("#txtAmount").val());
    }

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.JournalID = 0;
        ObjData.StatusFlag = "I";
        AddOPBillingData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnJournalID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.JournalID = $("#hdnJournalID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.JournalID = 0;
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
    $("#ddlType").val("Cr").change();
    $("#txtAmount").val("0");
    $("#txtNotes").val("");
    $("#hdnOPSNo").val("");
    GetLedgerList("ddlLedgerName");
    $("#ddlLedgerName").val(null).change();
    $("#divSelectLedgerName").show();
    $("#divLedgerName").removeClass('has-error');
    $("#divAmount").removeClass('has-error');
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

    if (gData.length >= 5)
    { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Ledger Name</th>";
        sTable += "<th class='" + sColorCode + "'>Amount</th>";
        sTable += "<th class='" + sColorCode + "'>Notes</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].LedgerName + "</td>";
                sTable += "<td>" + gData[i].CreditAmount + "</td>";
                sTable += "<td>" + gData[i].Notes + "</td>";
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
            $("#hdnJournalID").val(data[i].JournalID);
            $("#ddlLedgerName").val(data[i].LedgerID).change();
            $("#ddlType").val(data[i].CreditOrDebit).change();
            $("#txtNotes").val(data[i].Notes);
            //if (data[i].LedgerID == 0) {
            //    $("#divNewLedgerName").show();
            //    $("#divSelectLedgerName").hide();
            //    $("#rdbNewLedgerName").prop("checked", true);
            //    $("#rdbExistingLedgerName").prop("checked", false);
            //    $("#txtNewLedgerName").val(data[i].LedgerName);
            //}
            //else if (data[i].LedgerID > 0) {
            //    $("#divNewLedgerName").hide();
            //    $("#divSelectLedgerName").show();
            //    $("#rdbNewLedgerName").prop("checked", false);
            //    $("#rdbExistingLedgerName").prop("checked", true);
            //    $("#ddlLedgerName").val(data[i].LedgerID).change();
            //}
            if ($("#ddlType").val() == "Cr")
                $("#txtAmount").val(data[i].CreditAmount);
            else
                $("#txtAmount").val(data[i].DebitAmount);
        }
    }
    return false;
}
function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].JournalID = oData.JournalID;
            var oLedger = new Object();
            gOPBillingList[i].LedgerID = oData.LedgerID;
            gOPBillingList[i].LedgerName = oData.LedgerName;
            gOPBillingList[i].CreditOrDebit = oData.CreditOrDebit;
            gOPBillingList[i].Ledger = oLedger;

            gOPBillingList[i].CreditAmount = oData.CreditAmount;
            gOPBillingList[i].Notes = oData.Notes;
            gOPBillingList[i].DebitAmount = oData.DebitAmount;
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

function CalculateNetPrice() {
    var iAmountPrice = parseFloat($("#txtAmount").val());
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
        url: "WebServices/VHMSService.svc/GetJournal",
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

                                var table = "<tr id='" + obj[index].JournalID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].JournalNo + "</td>";
                                table += "<td>" + obj[index].sJournalDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";
                                table += "<td>" + obj[index].Narration + "</td>";
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
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
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "25%" },
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
        url: "WebServices/VHMSService.svc/SearchJournal",
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

                                var table = "<tr id='" + obj[index].JournalID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].JournalNo + "</td>";
                                table += "<td>" + obj[index].sJournalDate + "</td>";
                                table += "<td>" + obj[index].BillNo + "</td>";
                                table += "<td>" + obj[index].sBillDate + "</td>";
                                table += "<td>" + obj[index].NetAmount + "</td>";
                                table += "<td>" + obj[index].Narration + "</td>";

                               
                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].JournalID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
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
                                $("#hdnJournalID").val(AdmissionID);
                                var JobCardNo = $(this).attr('BillNo');
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("JournalID", AdmissionID);

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
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "25%" },
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

//function CalculateAmount() {
//    var iOPBillingAmount = 0, iBillingQty = 0;
//    for (var i = 0; i < gOPBillingList.length; i++) {
//        if (gOPBillingList[i].StatusFlag != "D") {
//            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].CreditAmount);
//            iBillingQty = iBillingQty + parseFloat(gOPBillingList[i].DebitAmount);
//        }
//    }
//    $("#txtCredit").val(parseFloat(iOPBillingAmount).toFixed(2));
//    $("#txtDebit").val(parseFloat(iBillingQty).toFixed(2));
//}

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

    if (parseFloat($("#txtCredit").val()) != parseFloat($("#txtDebit").val()))
    {
        $.jGrowl("Credit Amount and Debit Amount must tally", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCredit").addClass('has-error'); $("#txtCredit").focus(); return false;
    } 
   
    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++)
        iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].Subtotal);

    var ObjOPBilling = new Object();
    ObjOPBilling.JournalID = 0;
    ObjOPBilling.JournalNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sJournalDate = $("#txtBillDate").val().trim();
    ObjOPBilling.BillNo = $("#txtExpenseNo").val().trim();
    ObjOPBilling.sBillDate = $("#txtBDate").val().trim();
    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.Roundoff = $("#txtRoundoff").val().trim();
    ObjOPBilling.Status = $("#ddlPaymentStatus").val();
    if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3) {
        ObjOPBilling.ChequeNo = $("#txtChequeNo").val().trim();
        ObjOPBilling.sIssueDate = $("#txtIssueDate").val().trim();
    }
    var objBank = new Object();
    objBank.LedgerID = $("#ddlBank").val();
    ObjOPBilling.Bank = objBank;
    ObjOPBilling.ReceiptModeID = $("#ddlReceiptMode").val();

    ObjOPBilling.JournalTrans = gOPBillingList;
    ObjOPBilling.Narration = $("#txtNarration").val().trim();
    if ($("#hdnJournalID").val() > 0) {
        ObjOPBilling.JournalID = $("#hdnJournalID").val();
        gOPBillingList.JournalID = ObjOPBilling.JournalID;
        ObjOPBilling.OPBillingTrans = gOPBillingList;
        sMethodName = "UpdateJournal";
    }
    else {
        sMethodName = "AddJournal";
        ObjOPBilling.JournalID = 0
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
                            $("#hdnJournalID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateOPBilling")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();

                        $("#hdnJournalID").val("0");
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
        url: "WebServices/VHMSService.svc/GetJournalByID",
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

                            $("#hdnJournalID").val(obj.JournalID)
                            $("#txtBillNo").val(obj.JournalNo);
                            $("#txtBDate").val(obj.sBillDate);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#txtBillDate").val(obj.sJournalDate);
                            $("#txtNarration").val(obj.Narration);
                            $("#txtExpenseNo").val(obj.BillNo);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#ddlBank").val(obj.Bank.LedgerID).change();
                            $("#ddlReceiptMode").val(obj.ReceiptModeID).change();
                            $("#ddlPaymentStatus").val(obj.Status);
                            if (obj.ReceiptModeID == 2 || obj.ReceiptModeID == 3) {
                                $("#divChequeDetails").show();
                                $("#txtChequeNo").val(obj.ChequeNo);
                                $("#txtIssueDate").val(obj.sIssueDate);
                            }

                            gOPBillingList = [];
                            var ObjLedger = obj.JournalTrans;
                            for (var index = 0; index < ObjLedger.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                var objMagazine = new Object();
                                objTemp.LedgerID = ObjLedger[index].LedgerID;
                                objTemp.LedgerName = ObjLedger[index].LedgerName;
                                objTemp.CreditOrDebit = ObjLedger[index].CreditOrDebit;
                                objTemp.PartyType = ObjLedger[index].PartyType;

                                objTemp.JournalTransID = ObjLedger[index].JournalTransID;
                                objTemp.JournalID = ObjLedger[index].JournalID;
                                objTemp.DebitAmount = ObjLedger[index].DebitAmount;
                                objTemp.Notes = ObjLedger[index].Notes;
                                objTemp.CreditAmount = ObjLedger[index].CreditAmount;
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


function CalculateAmount() {
    var iOPBillingAmount = 0, TotalAmount = 0, iBillingQty = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].CreditAmount);
        }
    }
  
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    
    $("#txtNetAmount").val((parseFloat(iOPBillingAmount) + parseFloat(iround)).toFixed(2));
}

$("#txtRoundoff").change(function () {
    CalculateAmount();
});

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
        url: "WebServices/VHMSService.svc/DeleteJournal",
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
