var gMagazineData = [];
var gPrescriptionList = [];

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divPrescription").hide();
    GetDrugList("ddlDrugName");
    $("#txtIssueDateTime,#txtPrescriptionDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtIssueDateTime").datetimepicker({
        pickTime: false,
        useCurrent: true,
        format: 'DD/MM/YYYY'
    });

    $("#txtPrescriptionDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    GetPatientList();

    var _Tfunctionality;
    if ($.cookie("Prescription") != undefined) {
        _Tfunctionality = $.cookie("Prescription");

        if (_Tfunctionality == "AddNewPrescription") {
            pLoadingSetup(true);
            $("#btnAddNew").click();
            $("#ddlPatient").val(parseInt($.cookie("PatientID"))).change();
            GetMagazineList(parseInt($.cookie("PatientID")));

            $("#ddlPatient").attr("disabled", true);

            GetReceivedInward(parseInt($.cookie("PrescriptionOrderID")));
            $("#hdnPrescriptionOrderID").val(parseInt($.cookie("PrescriptionOrderID")));
        }
        $.cookie("Prescription", null);
        $.cookie("PatientID", null);
        $.cookie("PrescriptionOrderID", null);
    }

    pLoadingSetup(true);
    GetRecord();
});

function Edit_PrescriptionDetail(ID) {
    Bind_PrescriptionByID(ID, gPrescriptionList);
    //alert($("#hdnPrescriptionSNo").val())
    return false;
}

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").hide();
    $("#divPrescription").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gPrescriptionList = [];
    ClearPrescriptionTab();
    //ClearFields();
    $("#divPrescriptionList").empty();

    $("#txtPrescriptionDate").focus();
    return false;
});

function PrintPrescriptionDetails() {
    SetSessionValue("AdmissionID", $("#hdnPrescriptionID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewPrescriptionDetails.aspx", "_blank");
    return false;
}

$("#btnPrint").click(function () {
    
    PrintPrescriptionDetails();
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divPrescription").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#btnList").click();
    return false;
});
function ClearPrescriptionTab() {
    $("#txtPrescriptionNo").val("");
    $("#txtPrescriptionDate").val("");
    $("#ddlPatient").val(null).change();
    $("#hdnPatientID").val("");
    $("#hdnPrescriptionID").val("");

    gPrescriptionList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();

    $("#txtPrescriptionNo").attr("disabled", false);
    $("#ddlPatient").attr("disabled", false);
    return false;
}

function GetDrugList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDrug",
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
                                    $(sControlName).append("<option value='" + obj[index].DrugID + "'>" + obj[index].DrugName + "</option>");
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
function GetPatientList() {
    dProgress(true);
    $("#ddlPatient").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPatient",
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
                            //$("#ddlPatient").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive) {
                                    $("#ddlPatient").append('<option value=' + obj[index].patientID + ' >' + obj[index].HName + " - " + obj[index].WName + '</option>');
                                }
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlPatient").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlPatient").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
    if ($("#ddlDrugName").val() == "0" || $("#ddlDrugName").val() == undefined || $("#ddlDrugName").val() == null) {
        $.jGrowl("Please select Drug", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectDrugName").addClass('has-error'); $("#ddlDrugName").focus(); return false;
    }
    else { $("#divSelectDrugName").removeClass('has-error'); }

    if ($("#txtDosage").val() == "" || $("#txtDosage").val() == undefined || $("#txtDosage").val() == null) {
        $.jGrowl("Please enter Dosage", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDosage").addClass('has-error'); $("#txtDosage").focus(); return false;
    } else { $("#divDosage").removeClass('has-error'); }

    if ($("#txtFrequency").val() == "" || $("#txtFrequency").val() == undefined || $("#txtFrequency").val() == null) {
        $.jGrowl("Please enter Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divFrequency").addClass('has-error'); $("#txtFrequency").focus(); return false;
    } else { $("#divFrequency").removeClass('has-error'); }

    if ($("#txtDuration").val() == "" || $("#txtDuration").val() == undefined || $("#txtDuration").val() == null) {
        $.jGrowl("Please enter Duration", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDuration").addClass('has-error'); $("#txtDuration").focus(); return false;
    } else { $("#divDuration").removeClass('has-error'); }

    if ($("#ddlInstruction").val() == "0" || $("#ddlInstruction").val() == undefined || $("#ddlInstruction").val() == null) {
        $.jGrowl("Please Select Instruction", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divInstruction").addClass('has-error'); $("#ddlInstruction").focus(); return false;
    } else { $("#divInstruction").removeClass('has-error'); }

    var DurationID = 0, DosageID = 0, FrequencyID = 0;
    if ($("#txtDuration").val() != null) {
        if ($("#hdnDurationID").val() != null && $("#hdnDurationID").val() > 0 && $("#hdnDurationID").val() != undefined)
            DurationID = $("#hdnDurationID").val();
        else
            $("#hdnDurationID").val(DurationID);
    }
    if ($("#txtDosage").val() != null) {
        if ($("#hdnDosageID").val() != null && $("#hdnDosageID").val() > 0 && $("#hdnDosageID").val() != undefined)
            DosageID = $("#hdnDosageID").val();
        else
            $("#hdnDosageID").val(DosageID);
    }
    if ($("#txtFrequency").val() != null) {
        if ($("#hdnFrequencyID").val() != null && $("#hdnFrequencyID").val() > 0 && $("#hdnFrequencyID").val() != undefined)
            FrequencyID = $("#hdnFrequencyID").val();
        else
            $("#hdnFrequencyID").val(FrequencyID);
    }
    var ObjData = new Object();
    ObjData.PrescriptionID = 0;

    var oDrug = new Object();
    //if ($("#rdbNewDrugName").is(':checked')) {
    //    oDrug.DrugID = 0;
    //    oDrug.DrugName = $("#txtNewDrugName").val();
    //}
    //else if ($("#rdbExistingDrugName").is(':checked')) {
    //    oDrug.DrugID = $("#ddlDrugName").val();
    //    oDrug.DrugName = $("#ddlDrugName option:selected").text();
    //}
    oDrug.DrugID = $("#ddlDrugName").val();
    oDrug.DrugName = $("#ddlDrugName option:selected").text();
    ObjData.Drug = oDrug;

    ObjData.Duration = $("#txtDuration").val();
    ObjData.DurationID = $("#hdnDurationID").val();

    ObjData.InstructionType = $("#ddlInstruction").val();
    ObjData.Instruction = $("#ddlInstruction option:selected").text();
    ObjData.Ingredient = $("#txtIngredient").val();

    ObjData.Dosage = $("#txtDosage").val();
    ObjData.DosageID = $("#hdnDosageID").val();

    ObjData.FrequencyID = $("#hdnFrequencyID").val();
    ObjData.Frequency = $("#txtFrequency").val();

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gPrescriptionList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.PrescriptionID = 0;
        ObjData.StatusFlag = "I";
        AddPrescriptionData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnPrescriptionSNo").val();
        if ($("#hdnPrescriptionID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.PrescriptionID = $("#hdnPrescriptionID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.PrescriptionID = 0;
        }
        Update_Prescription(ObjData);
    }
    ClearPrescriptionFields();
    $("#ddlDrugName").focus();
});
function ClearPrescriptionFields() {
    $("#btnAddPrescription").show();
    $("#btnUpdatePrescription").hide();
    $("#ddlDrugName").val(null).change();
    $("#txtDosage").val("");
   // $("#txtPrescriptionDate").val("");
    $("#ddlFrequency").val("0");
    $("#txtDuration").val("");
    $("#txtFrequency").val("");
    $("#ddlInstruction").val('0');
    $("#txtIngredient").val("");

    $("#hdnPrescriptionSNo").val("");
   //$("#hdnPrescriptionID").val("");
    $("#hdnDosageID").val("");
    $("#hdnDurationID").val("");
    $("#hdnFrequencyID").val("");

    $("#ddlDrugName").val(null).change();
    $("#divSelectDrugName").show();

    $("#divDrugName").removeClass('has-error');
    $("#divDosage").removeClass('has-error');
    $("#divFrequency").removeClass('has-error');
    $("#divOtherFrqeuency").removeClass('has-error');
    $("#divDuration").removeClass('has-error');
    $("#divInstruction").removeClass('has-error');
    return false;
}
function AddPrescriptionData(oData) {
    gPrescriptionList.push(oData);
    DisplayPrescriptionList(gPrescriptionList);
    return false;
}
function DisplayPrescriptionList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divPrescriptionList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divPrescriptionList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblPrescriptionList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Drug Name</th>";
        sTable += "<th class='" + sColorCode + "'>Dosage</th>";
        sTable += "<th class='" + sColorCode + "'>Frequency</th>";
        sTable += "<th class='" + sColorCode + "'>Duration</th>";
        sTable += "<th class='" + sColorCode + "'>Instruction</th>";
        sTable += "<th class='" + sColorCode + "'>Ingredient</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblPrescriptionList_body'>";
        sTable += "</tbody></table>";
        $("#divPrescriptionList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Drug.DrugName + "</td>";
                sTable += "<td>" + gData[i].Dosage + "</td>";
                sTable += "<td>" + gData[i].Frequency + "</td>";
                sTable += "<td>" + gData[i].Duration + "</td>";
                sTable += "<td>" + gData[i].Instruction + "</td>";
                sTable += "<td>" + gData[i].Ingredient + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_PrescriptionDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_PrescriptionDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblPrescriptionList_body").append(sTable);
            }
        }
    }
    else { $("#divPrescriptionList").empty(); }

    return false;
}
function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gPrescriptionList);
    return false;
}
function Bind_PrescriptionByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlDrugName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnPrescriptionSNo").val = null;
            $("#hdnPrescriptionSNo").val(ID);
            $("#hdnPrescriptionID").val(data[i].PrescriptionID);
            $("#ddlDrugName").val(data[i].Drug.DrugID).change();
            $("#txtDosage").val(data[i].Dosage);
            $("#hdnDosageID").val(data[i].DosageID);
            $("#hdnFrequencyID").val(data[i].FrequencyID);
            $("#txtFrequency").val(data[i].Frequency);
            $("#txtDuration").val(data[i].Duration);
            $("#hdnDurationID").val(data[i].DurationID);
            $("#ddlInstruction").val(data[i].InstructionType);
            $("#txtIngredient").val(data[i].Ingredient);
            //alert($("#hdnPrescriptionSNo").val);
            if (data[i].Drug.DrugID == 0) {
                $("#divNewDrugName").show();
                $("#divSelectDrugName").hide();
                $("#rdbNewDrugName").prop("checked", true);
                $("#rdbExistingDrugName").prop("checked", false);
                $("#txtNewDrugName").val(data[i].Drug.DrugName);
            }
            else if (data[i].Drug.DrugID > 0) {
                $("#divNewDrugName").hide();
                $("#divSelectDrugName").show();
                $("#rdbNewDrugName").prop("checked", false);
                $("#rdbExistingDrugName").prop("checked", true);
                $("#ddlDrugName").val(data[i].Drug.DrugID).change();
            }
        }
    }
    return false;
}
function Update_Prescription(oData) {
    for (var i = 0; i < gPrescriptionList.length; i++) {
        if (gPrescriptionList[i].sNO == oData.sNO) {
            gPrescriptionList[i].PrescriptionID = oData.PrescriptionID;
            var oDrug = new Object();
            oDrug.DrugID = oData.Drug.DrugID;
            oDrug.DrugName = oData.Drug.DrugName;
            gPrescriptionList[i].Drug = oDrug;

            gPrescriptionList[i].DosageID = oData.DosageID;
            gPrescriptionList[i].Dosage = oData.Dosage;
            gPrescriptionList[i].Frequency = oData.Frequency;
            gPrescriptionList[i].FrequencyID = oData.FrequencyID;
            gPrescriptionList[i].DurationID = oData.DurationID;
            gPrescriptionList[i].Duration = oData.Duration;
            gPrescriptionList[i].Ingredient = oData.Ingredient;
            gPrescriptionList[i].InstructionType = oData.InstructionType;
            gPrescriptionList[i].Instruction = oData.Instruction;
            gPrescriptionList[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayPrescriptionList(gPrescriptionList);
    $("#btnAddPrescription").show();
    $("#btnUpdatePrescription").hide();
    ClearPrescriptionFields();
    $("#ddlDrugName").focus();
    return false;
}
function Delete_PrescriptionDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gPrescriptionList.length; i++) {
            if (gPrescriptionList[i].SNo == ID) {
                var index = jQuery.inArray(gPrescriptionList[i].valueOf("SNo"), gPrescriptionList);
                if (gPrescriptionList[i].SNo > 0) {
                    gPrescriptionList[i].StatusFlag = "D";
                } else {
                    gPrescriptionList.splice(index, 1);
                }
                $("#divPrescriptionList").empty();
                DisplayPrescriptionList(gPrescriptionList);
            }
        }
    }
    return false;
}

function CalculateNetPrice() {
    var iCoverPrice = parseFloat($("#txtCoverPrice").val());
    var iDiscountPer = parseFloat($("#txtDiscountPer").val());
    var iWastePaperValue = parseFloat($("#txtWastePapervalue").val());
    var iNoofCopies = parseInt($("#txtNoofCopies").val());

    if (isNaN(iNoofCopies)) iNoofCopies = 0;
    if (isNaN(iDiscountPer)) iDiscountPer = 0;
    if (isNaN(iWastePaperValue)) iWastePaperValue = 0;
    if (isNaN(iCoverPrice)) iCoverPrice = 0;

    var iDiscountAmount = parseFloat(iCoverPrice * (iDiscountPer / 100));
    var iNetPrice = parseFloat(iCoverPrice * iNoofCopies) - parseFloat(iDiscountAmount * iNoofCopies);

    $("#txtNetPrice").val(iNetPrice.toFixed(2));
    return false;
}

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPrescription",
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
                                if (obj[index].IsActive == "1")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                var table = "<tr id='" + obj[index].PrescriptionID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].PrescriptionNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sPrescriptionDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Patient.patientID + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Patient.HName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Patient.WName + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PrescriptionID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                
                               

                                if (ActionUpdate == "1") {
                                    if (!obj[index].IsPrescriptioned)
                                    { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PrescriptionID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                    else
                                    { table += "<td></td>"; }
                                }
                                else { table += "<td></td>"; }

                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PrescriptionID + " class='PrintPrescription' title='Click here to Print Prescription'><i class='fa fa-lg fa-print text-green'/></a></td>";
                                 //table += "<td style='text-align:center;'><a href='#' AdmissionID='" + obj[index].PrescriptionID + "' AdmissionNo='" + obj[index].AdmissionNo + "'  class='PrintPrescription' title='Click here to Print Prescription'><i class='fa fa-lg fa-list text-green'/></a></td>";
                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PrescriptionID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
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
                            $(".PrintPrescription").click(function () {
                                var AdmissionID = $(this).attr('id');
                               // var AdmissionNo = $(this).attr('AdmissionNo');
                                $("#hdnPrescriptionID").val(AdmissionID);
                               // $("#hdnAdmissionNo").val(AdmissionNo);
                                PrintPrescriptionDetails();
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to delete the selected record ?'))
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
                          { "sWidth": "10%" },
                          { "sWidth": "10%" },
                          { "sWidth": "5%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
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
$("#ddlDrugName").change(function () {
    var iDrugID = $("#ddlDrugName").val();
    if (iDrugID != undefined && iDrugID > 0) {
        GetDrugByID(iDrugID);
    }
    else {
        $("#txtDosage").val("");
        $("#txtDuration").val("");
        $("#txtIngredient").val("");
    }
});

function GetDrugByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDrugByID",
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
                            $("#txtDosage").val(obj.Dosage.DosageName);
                            $("#txtDuration").val(obj.Duration.DurationName);
                            $("#ddlInstruction").val(obj.InstructionID);
                            $("#txtIngredient").val(obj.Ingredient);
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

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    //if ($("#txtPrescriptionNo").val().trim() == "" || $("#txtPrescriptionNo").val().trim() == undefined) {
    //    $.jGrowl("Please enter Prescription No", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divPrescriptionNo").addClass('has-error'); $("#txtPrescriptionNo").focus(); return false;
    //}
    //else { $("#divPrescriptionNo").removeClass('has-error'); }

    if ($("#txtPrescriptionDate").val().trim() == "" || $("#txtPrescriptionDate").val().trim() == undefined) {
        $.jGrowl("Please select Prescription Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPrescriptionDate").addClass('has-error'); $("#txtPrescriptionDate").focus(); return false;
    }
    else { $("#divPrescriptionDate").removeClass('has-error'); }

    if ($("#ddlPatient").val() == "0" || $("#ddlPatient").val() == undefined || $("#ddlPatient").val() == null) {
        $.jGrowl("Please select Patient", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatient").addClass('has-error'); $("#ddlPatient").focus(); return false;
    }
    else { $("#divPatient").removeClass('has-error'); }

    if (gPrescriptionList.length <= 0) {
        $.jGrowl("No Magazine has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtMagazineName").focus(); return false;
    }

    var iPrescriptionAmount = 0;
    for (var i = 0; i < gPrescriptionList.length; i++)
        iPrescriptionAmount = iPrescriptionAmount + parseFloat(gPrescriptionList[i].NetPrice);

    var ObjPrescription = new Object();
    var ObjPatient = new Object();
    ObjPatient.patientID = $("#ddlPatient").val();
    ObjPrescription.Patient = ObjPatient;

    ObjPrescription.PrescriptionID = 0;
    ObjPrescription.PrescriptionNo = $("#txtPrescriptionNo").val().trim();
    ObjPrescription.sPrescriptionDate = $("#txtPrescriptionDate").val().trim();
    ObjPrescription.PrescriptionAmount = iPrescriptionAmount;
    ObjPrescription.PrescriptionTrans = gPrescriptionList;
    ObjPrescription.PrescriptionOrderID = $("#hdnPrescriptionOrderID").val();

    if ($("#hdnPrescriptionID").val() > 0) {
        ObjPrescription.PrescriptionID = $("#hdnPrescriptionID").val();
        sMethodName = "UpdatePrescription";
    }
    else { sMethodName = "AddPrescription"; }

    SaveandUpdatePrescription(ObjPrescription, sMethodName);
});
function SaveandUpdatePrescription(ObjPrescription, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjPrescription }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        if (sMethodName == "AddPrescription") {
                            $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPrescriptionID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdatePrescription")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        //EditRecord($("#hdnPrescriptionID").val());

                        $("#btnClose").click();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Prescription_A_01" || objResponse.Value == "Prescription_U_01") {
                        $.jGrowl("Prescription No already exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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
        url: "WebServices/VHMSService.svc/GetPrescriptionByID",
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

                            $("#txtPrescriptionNo").attr("disabled", true);
                            $("#ddlPatient").attr("disabled", true);

                            $("#hdnPrescriptionID").val(obj.PrescriptionID)
                            $("#ddlPatient").val(obj.Patient.patientID).change();
                            //GetMagazineList(obj.Patient.PatientID);
                            $("#txtPrescriptionNo").val(obj.PrescriptionNo);
                            $("#txtPrescriptionDate").val(obj.sPrescriptionDate);
                            //$("#txtPatientName").val(obj.Patient.PatientName);

                            gPrescriptionList = [];
                            var ObjProduct = obj.PrescriptionTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";

                                var objMagazine = new Object();
                                objMagazine.DrugID = ObjProduct[index].DrugID;
                                objMagazine.DrugName = ObjProduct[index].DrugName;
                                objTemp.Drug = objMagazine;

                                objTemp.PrescriptionTransID = ObjProduct[index].PrescriptionTransID;
                                objTemp.PrescriptionID = ObjProduct[index].PrescriptionID;
                                objTemp.DrugID = ObjProduct[index].DrugID;
                                objTemp.DrugName = ObjProduct[index].DrugName;
                                objTemp.Dosage = ObjProduct[index].Dosage;                                
                                objTemp.Frequency = ObjProduct[index].Frequency;
                                objTemp.Duration = ObjProduct[index].Duration;
                                objTemp.Instruction = ObjProduct[index].Instruction;
                                objTemp.InstructionType = ObjProduct[index].InstructionType;
                                objTemp.Ingredient = ObjProduct[index].Ingredient;

                                AddPrescriptionData(objTemp);
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
        url: "WebServices/VHMSService.svc/DeletePrescription",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearPrescriptionTab();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Prescription_R_01" || objResponse.Value == "Prescription_D_01") {
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
