var gDoctorsList = [];
var gSelectedDoctorsList = [];
var gHipSurgeryList = [];
var gKneeSurgeryList = [];
var gOtherSurgeryList = [];
var gPrescriptionList = [];
var gPatientOwnDrug = [];
var inFocus = false;

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").hide();
    $("#btnSave").show();
    $("#btnSavePrint").show();

    $("#btnList").hide();
    $("#btnUpdate").hide();
    $("#btnUpdatePrint").hide();

    //Controls
    $("#btnAddHipSurgery,#btnAddKneeSurgery,#btnAddOtherSurgery,#btnAddPrescription").show();
    $("#btnUpdateHipSurgery,#btnUpdateKneeSurgery,#btnUpdateOtherSurgery,#btnUpdatePrescription").hide();

    $("#txtAdmissionDate,#txtSurgeryDate,#txtDischargeDate,#txtHipSurgeryDate,#txtKneeSurgeryDate,#txtOtherSurgeryDate,#txtReviewAppointmentDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtAdmissionDate,#txtSurgeryDate,#txtDischargeDate,#txtHipSurgeryDate,#txtKneeSurgeryDate,#txtOtherSurgeryDate,#txtReviewAppointmentDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        format: 'DD/MM/YYYY'
    });

    $("#txtReviewAppointmentTime,#txtAdmissionTime,#txtDischargeTime").attr("data-link-format", "HH:mm");
    $("#txtReviewAppointmentTime,#txtAdmissionTime,#txtDischargeTime").datetimepicker({
        pickTime: true,
        pickDate: false,
        useCurrent: true,
        format: 'HH:mm'
    });

    $("#divDischargeEdit").hide();
    $("#divDischargeEntry").show();
    $("#tab-modal").show();
    //Hip
    $("#divLeftHip").hide();
    $("#divRightHip").hide();
    //Knee
    $("#divLeftKnee").hide();
    $("#divRightKnee").hide();

    $("#btnAddNew").click();
    $("#divNewOtherProcedure").hide();

    $("#txtDosage").keyup(function () {
        var SearchText = $("#txtDosage").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnDosageID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchDosage";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });
    $("#txtDuration").keyup(function () {
        var SearchText = $("#txtDuration").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnDurationID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchDuration";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });
    $("#txtFrequency").keyup(function () {
        var SearchText = $("#txtFrequency").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnFrequencyID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchFrequency";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });

    $("#txtPatientDosage").keyup(function () {
        var SearchText = $("#txtPatientDosage").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnPatientDosageID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchDosage";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });
    $("#txtPatientDuration").keyup(function () {
        var SearchText = $("#txtPatientDuration").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnPatientDurationID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchDuration";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });
    $("#txtPatientFrequency").keyup(function () {
        var SearchText = $("#txtPatientFrequency").val();
        var obj = new Object();
        obj.key = SearchText;
        obj.OutPutID = "hdnPatientFrequencyID";
        obj.SearchLength = 10;
        obj.Method = "WebServices/VHMSService.svc/GetSearchFrequency";
        var objSer = JSON.stringify(obj);
        TRSearch(objSer);
    });

    var _Tfunctionality;
    if ($.cookie("Discharge") != undefined) {
        _Tfunctionality = $.cookie("Discharge");

        if (_Tfunctionality == "EditEntry") {
            pLoadingSetup(true);
            $("#divDischargeEdit").show();
            $("#divDischargeEntry").hide();
            $("#tab-modal").hide();
            document.title = "Edit Discharge Entry";
        }
        $.cookie("Discharge", null);
    }

    pLoadingSetup(true);

    //Added on 04-09-2017
    $("#aTab9").hide(); //Cause of Death
    $("#divOtherFrqeuency").hide();

    //Added on 08-09-2017
    $("#divOtherDoctorList").hide();
    $("#divPatientOtherFrqeuency").hide();

    //Added on 12-09-2017
    $("#divNewDrugName").hide();
    $("#divNewPatientDrugName").hide();
});

$("#btnAddNew,#btnClose").click(function () {
    $("#btnSave").show();
    $("#btnSavePrint").show();
    $("#btnUpdate").hide();
    $("#btnUpdatePrint").hide();
    GetPrimaryConsultant();
    GetCoConsultantDoctorList();
    GetRegistrarDoctorList();
    GetDoctorList();
    GetDrugList("ddlDrugName");
    GetDrugList("ddlPatientDrugName");
    GetOtherProcedureList();
    GetCheckedByDoctorList();

    ClearAdmissionFields();
    ClearDischargeFields();
    ClearHipSurgeryFields();
    ClearKneeSurgeryFields();
    ClearOtherSurgeryFields();
    ClearPrescriptionFields();
    ClearPatientPrescriptionFields();

    $("#divDischargeEdit").hide();
    $("#divDischargeEntry").show();
    $("#tab-modal").show();
    document.title = "Add Discharge Entry";
    return false;
});
function ClearDischargeFields() {
    gDoctorsList = [];
    gSelectedDoctorsList = [];
    gHipSurgeryList = [];
    gKneeSurgeryList = [];
    gOtherSurgeryList = [];
    gPrescriptionList = [];
    gPatientOwnDrug = [];

    $("#hdnDoctorID").val("");
    $("#hdnDischargeEntryID").val("");
    $("#hdnHipSNo").val("");
    $("#hdnHipID").val("");

    $("#hdnKneeSNo").val("");
    $("#hdnKneeID").val("");

    $("#hdnOtherSNo").val("");
    $("#hdnOtherID").val("");

    $("#hdnPrescriptionSNo").val("");
    $("#hdnPrescriptionID").val("");

    $("#txtSurgeryDate").val("");
    $("#txtDrugAllergy").val("");
    $("#txtDiagnosis").val("");
    $("#txtCourseDuringStay").val("");
    $("#txtInvestigation").val("");
    $("#txtPastHistory").val("");
    $("#txtGeneralExamination").val("");
    $("#txtLocalExamination").val("");
    $("#txtAdviseonDischarge").val("");
    $("#txtCauseofDeath").val("");

    $("#ddlCoConsultant").val(null).change();
    //$("#ddlRegistrar").val(null).change();
    $("#ddlDrugName").val(null).change();
    $("#ddlOtherProcedure").val(null).change();

    DisplayHipSurgeryList(gHipSurgeryList);
    DisplayKneeSurgeryList(gKneeSurgeryList);
    DisplayOtherSurgeryList(gOtherSurgeryList);
    DisplayPrescriptionList(gPrescriptionList);
    DisplayPatientPrescriptionList(gPatientOwnDrug);

    //Added on 04-09-2017
    var sGeneralExamination = "PATIENT CONSCIOUS / ORIENTED / AFEBRILE";
    sGeneralExamination += "\nCVS   : S1,S2 +\nRS   : NVBS\nP/A  : SOFT\nB.P  : 130/80mm of Hg\nP.R  : 84/min\nR.R  : 22/min\nTemp : 98.6ºF";
    $("#txtGeneralExamination").val(sGeneralExamination);
    $("#txtInvestigation").val("Reports Enclosed");

    //Added on 07-09-2017
    var sAdviseonDischarge = "1.Please bring the discharge summary without fail on review or for any enquires.\n";
    sAdviseonDischarge += "2.Change review appointment.\n";
    sAdviseonDischarge += "3.Come before 30min appointment time.\n";
    sAdviseonDischarge += "4.Appointment time 03:00 pm to 05:00 pm only.\n";
    sAdviseonDischarge += "5.After discharge first consultation only free.";
    $("#txtAdviseonDischarge").val(sAdviseonDischarge);

    $("#aTab9").hide(); //Cause of Death
    $("#divOtherFrqeuency").hide();

    //Added on 05-09-2017
    $("#ddlCheckedBy").val(null).change();
    //$(".WeekDay").prop("checked", false);
    $("input[name='WeekDay']:checked").prop("checked", false);

    $("#txtWrittenBy").val(GetSessionValue("EmployeeName"));

    //Added on 08-09-2017
    $("#txtDrugAllergy").val("-NIL-");

    //Added on 14-09-2017
    $("#ddlSummaryType").val(0);

    //Added on 22-09-2017
    $("#txtReviewAppointmentDate,#txtReviewAppointmentTime").prop("disabled", true);
    $("#chkAdvStatus").prop("checked", false);
    return false;
}

//Discharge & Review Appointment controls
$("#chkAdvStatus").click(function () {
    if ($("#chkAdvStatus").is(':checked'))
        $("#txtReviewAppointmentDate,#txtReviewAppointmentTime").prop("disabled", false);
    else
        $("#txtReviewAppointmentDate,#txtReviewAppointmentTime").prop("disabled", true);
});

function GetPrimaryConsultant() {
    dProgress(true);
    $("#ddlPrimaryConsultant").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: _PrimaryConsultantID }),
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
                                    $("#ddlPrimaryConsultant").append("<option value=" + obj[index].DoctorID + " selected='selected'>" + obj[index].DoctorName + "</option>");
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlPrimaryConsultant").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlPrimaryConsultant").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
function GetCoConsultantDoctorList() {
    dProgress(true);
    $("#ddlCoConsultant").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: _CoConsultantID }),
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
                                    $("#ddlCoConsultant").append('<option value=' + obj[index].DoctorID + ' >' + obj[index].DoctorName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlCoConsultant").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlCoConsultant").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
function GetRegistrarDoctorList() {
    dProgress(true);
    $("#ddlRegistrar").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: _RegistrarID }),
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
                                    $("#ddlRegistrar").append("<option value=" + obj[index].DoctorID + " selected='selected'>" + obj[index].DoctorName + "</option>");
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlRegistrar").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlRegistrar").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
function GetDoctorList() {
    dProgress(true);
    $("#ddlDoctor").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: 0 }),
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
                                if (obj[index].IsActive && obj[index].DoctorType.DoctorTypeID != _RegistrarID && obj[index].DoctorType.DoctorTypeID != _CoConsultantID)
                                    $("#ddlDoctor").append('<option value=' + obj[index].DoctorID + ' >' + obj[index].DoctorName + '</option>');
                            }
                            DisplayDoctorList(obj);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlDoctor").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlDoctor").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

//===Co-Consultant & Registrar Doctors Details
function DisplayDoctorList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divDoctorsList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else
    { $("#divDoctorsList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblDoctorList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Doctor Name</th>";
        //sTable += "<th class='" + sColorCode + "'>Specialization</th>";
        sTable += "<th class='" + sColorCode + "'>Department</th>";
        sTable += "<th class='" + sColorCode + "'>Type</th>";
        //sTable += "<th class='" + sColorCode + "'>Mobile No</th>";
        sTable += "</tr></thead><tbody id='tblDoctorList_body'>";
        sTable += "</tbody></table>";
        $("#divDoctorsList").html(sTable);
        for (var i = 0; i < gData.length; i++) {

            if (gData[i].IsActive && gData[i].DoctorType.DoctorTypeID != _PrimaryConsultantID && gData[i].IsActive && gData[i].DoctorType.DoctorTypeID != _RegistrarID && gData[i].DoctorType.DoctorTypeID != _CoConsultantID) {
                sTable = "<tr><td id='" + gData[i].DoctorID + "'>" + sCount + "</td>";
                //sTable += "<td>" + gData[i].DoctorName + "</td>";
                sTable += "<td><label class='checkbox-inline'><input id='chkDoctor_" + gData[i].DoctorID + "' type='checkbox' value='" + gData[i].DoctorID + "' DoctorName='" + gData[i].DoctorName + "'  Class='SelectDoctor' name='Doctor'/>&nbsp;" + gData[i].DoctorName + "</label></td>";
                //sTable += "<td>" + gData[i].Specialization.SpecializationName + "</td>";
                sTable += "<td>" + gData[i].Department.DepartmentName + "</td>";
                sTable += "<td>" + gData[i].DoctorType.DoctorTypeName + "</td>";
                //sTable += "<td>" + gData[i].PhoneNo1 + "</td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblDoctorList_body").append(sTable);
            }
        }
        $("#tblDoctorList").dataTable({
            "bPaginate": false,
            "bFilter": true,
            "bSort": true,
            aoColumns: [
                { "sWidth": "5%" },
                { "sWidth": "0%" },
                { "sWidth": "0%" },
            //{ "sWidth": "0%" },
            //{ "sWidth": "0%" },
                { "sWidth": "0%" }
            ]
        });
        $("#tblDoctorList_filter").addClass('pull-right');
        $(".pagination").addClass('pull-right');
        $(".SelectDoctor").click(function () {
            gSelectedDoctorsList = [];
            var DoctorID = 0;
            $.each($("input[name='Doctor']:checked"), function () {
                var objDoctor = new Object();
                DoctorID = parseInt($(this).val());
                var DoctorName = $(this).attr('DoctorName');

                objDoctor.DoctorID = DoctorID;
                objDoctor.DoctorName = DoctorName;
                gSelectedDoctorsList.push(objDoctor);
            });

            DisplaySelectedDoctorList(gSelectedDoctorsList);
        });
    }
    else { $("#divDoctorsList").empty(); }

    return false;
}
function DisplaySelectedDoctorList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length > 0) {
        sTable = "<table id='tblSelectedDoctorList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Doctor Name</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblSelectedDoctorList_body'>";
        sTable += "</tbody></table>";
        $("#divSelectedDoctorsList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].DoctorID + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].DoctorName + "</td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].DoctorID + " onclick = 'Delete_SelectDoctorDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                //sTable += "<td><label class='checkbox-inline'><input id='chkDoctor_" + gData[i].DoctorID + "' type='checkbox' value='" + gData[i].DoctorID + "' Class='SelectDoctor' name='Doctor'/>&nbsp;" + gData[i].DoctorName + "</label></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblSelectedDoctorList_body").append(sTable);
            }
        }

        $("#tblSelectedDoctorList").dataTable({
            "bPaginate": false,
            "bFilter": true,
            "bSort": true,
            aoColumns: [
                { "sWidth": "5%" },
                { "sWidth": "0%" },
                { "sWidth": "0%" },
            ]
        });
        $("#tblSelectedDoctorList_filter").addClass('pull-right');
        $(".pagination").addClass('pull-right');
    }
    else { $("#divSelectedDoctorsList").empty(); }

    return false;
}
function Delete_SelectDoctorDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gSelectedDoctorsList.length; i++) {
            if (gSelectedDoctorsList[i].DoctorID == ID) {
                var index = jQuery.inArray(gSelectedDoctorsList[i].valueOf("DoctorID"), gSelectedDoctorsList);
                if (gSelectedDoctorsList[i].DoctorID > 0) {
                    gSelectedDoctorsList[i].StatusFlag = "D";
                    $("#chkDoctor_" + ID).prop("checked", false);
                } else {
                    gSelectedDoctorsList.splice(index, 1);
                }
                $("#divSelectedDoctorsList").empty();
                DisplaySelectedDoctorList(gSelectedDoctorsList);
            }
        }
    }
    return false;
}
//===END - Co-Consultant & Registrar Doctors Details

//==============Hip Replacement Surgery Details=================
$("#ddlHipSurgeryType").change(function () {
    var iHipSurgeryType = $("#ddlHipSurgeryType").val();
    $("#divLeftHip").hide();
    $("#divRightHip").hide();
    if (iHipSurgeryType == 3) {
        $("#divLeftHip").show();
        $("#divRightHip").show();
    }
    else if (iHipSurgeryType == 1)
        $("#divLeftHip").show();
    else if (iHipSurgeryType == 2)
        $("#divRightHip").show();
});
$("#btnAddHipSurgery,#btnUpdateHipSurgery").click(function () {
    if ($("#ddlHipSurgeryType").val() == "0" || $("#ddlHipSurgeryType").val() == undefined) {
        $.jGrowl("Please Select Type", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divHipSurgeryType").addClass('has-error'); $("#ddlHipSurgeryType").focus(); return false;
    } else { $("#divHipSurgeryType").removeClass('has-error'); }

    if ($("#txtHipSurgeryName").val() == "" || $("#txtHipSurgeryName").val() == undefined || $("#txtHipSurgeryName").val() == null) {
        $.jGrowl("Please Enter Surgery Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divHipSurgeryName").addClass('has-error'); $("#txtHipSurgeryName").focus(); return false;
    } else { $("#divHipSurgeryName").removeClass('has-error'); }

    if ($("#txtHipSurgeryDate").val() == "" || $("#txtHipSurgeryDate").val() == undefined || $("#txtHipSurgeryDate").val() == null) {
        $.jGrowl("Please Select Surgery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divHipSurgeryDate").addClass('has-error'); $("#txtHipSurgeryDate").focus(); return false;
    } else { $("#divHipSurgeryDate").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.HipReplacementID = 0;
    ObjData.SurgeryID = 0;
    ObjData.HipSurgeryTypeID = $("#ddlHipSurgeryType").val();
    ObjData.HipSurgeryTypeName = $("#ddlHipSurgeryType option:selected").text();

    ObjData.HipSurgeryName = $("#txtHipSurgeryName").val();
    ObjData.sHipSurgeryDate = $("#txtHipSurgeryDate").val();
    //Modified on 20-07-2017 changed column as LEFT and RIGHT
    ObjData.LHipImplant = $("#txtLHipImplant").val();
    ObjData.LAcetabulumCup = $("#txtLAcetabulum").val();
    ObjData.LLiner = $("#txtLLiner").val();
    ObjData.LFemoralStem = $("#txtLFemoralStem").val();
    ObjData.LFemoralHead = $("#txtLFemoralHead").val();

    ObjData.RHipImplant = $("#txtRHipImplant").val();
    ObjData.RAcetabulumCup = $("#txtRAcetabulum").val();
    ObjData.RLiner = $("#txtRLiner").val();
    ObjData.RFemoralStem = $("#txtRFemoralStem").val();
    ObjData.RFemoralHead = $("#txtRFemoralHead").val();

    //LEFT Title
    ObjData.LAcetabulumCupTitle = $("#txtLAcetabulumTitle").val();
    ObjData.LLinerTitle = $("#txtLLinerTitle").val();
    ObjData.LFemoralStemTitle = $("#txtLFemoralStemTitle").val();
    ObjData.LFemoralHeadTitle = $("#txtLFemoralHeadTitle").val();

    //RIGHT Title
    ObjData.RAcetabulumCupTitle = $("#txtRAcetabulumTitle").val();
    ObjData.RLinerTitle = $("#txtRLinerTitle").val();
    ObjData.RFemoralStemTitle = $("#txtRFemoralStemTitle").val();
    ObjData.RFemoralHeadTitle = $("#txtRFemoralHeadTitle").val();

    if ($("#hdnDischargeEntryID").val() > 0)
    { ObjData.DischargeEntryID = $("#hdnDischargeEntryID").val(); }
    else
    { ObjData.DischargeEntryID = 0; }

    if (this.id == "btnAddHipSurgery") {
        ObjData.sNO = gHipSurgeryList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.StatusFlag = "I";
        AddHipSurgeryData(ObjData);
    }
    else if (this.id == "btnUpdateHipSurgery") {
        ObjData.sNO = $("#hdnHipSNo").val();
        if ($("#hdnHipID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.HipReplacementID = $("#hdnHipID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.HipReplacementID = 0;
        }
        Update_HipSurgery(ObjData);
    }
    GenerateLocalExamination($("#ddlHipSurgeryType").val(), "HIP");
    ClearHipSurgeryFields();
    $("#ddlHipSurgeryType").focus();
});
function ClearHipSurgeryFields() {
    $("#btnAddHipSurgery").show();
    $("#btnUpdateHipSurgery").hide();
    $("#ddlHipSurgeryType").val(0);
    $("#txtHipSurgeryName").val("");
    $("#txtHipSurgeryDate").val("");

    $("#txtLHipImplant").val("");
    $("#txtLAcetabulum").val("");
    $("#txtLLiner").val("");
    $("#txtLFemoralStem").val("");
    $("#txtLFemoralHead").val("");

    $("#txtRHipImplant").val("");
    $("#txtRAcetabulum").val("");
    $("#txtRLiner").val("");
    $("#txtRFemoralStem").val("");
    $("#txtRFemoralHead").val("");

    $("#hdnHipSNo").val("");
    $("#hdnHipID").val("");

    $("#divLeftHip").hide();
    $("#divRightHip").hide();

    $("#divHipSurgeryType").removeClass('has-error');
    $("#divHipSurgeryName").removeClass('has-error');
    $("#divHipSurgeryDate").removeClass('has-error');
    ResetHipReplacementTitle();
    return false;
}
function AddHipSurgeryData(oData) {
    gHipSurgeryList.push(oData);
    DisplayHipSurgeryList(gHipSurgeryList);
    return false;
}
function DisplayHipSurgeryList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divHipSurgeryList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divHipSurgeryList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblHipSurgeryList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr class='" + sColorCode + "'><th style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th>Type</th>";
        sTable += "<th>Surgery Name</th>";
        sTable += "<th>Surgery Date</th>";
        sTable += "<th>Implant</th>";
        sTable += "<th>Acetabulum Cup</th>";
        sTable += "<th>Liner</th>";
        sTable += "<th class='" + sColorCode + "'>Femoral Stem</th>";
        sTable += "<th class='" + sColorCode + "'>Femoral Head</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblHipSurgeryList_body'>";
        sTable += "</tbody></table>";
        $("#divHipSurgeryList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].HipSurgeryTypeName + "</td>";
                sTable += "<td>" + gData[i].HipSurgeryName + "</td>";
                sTable += "<td>" + gData[i].sHipSurgeryDate + "</td>";
                if (gData[i].HipSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LHipImplant + "<BR>R: " + gData[i].RHipImplant + "</td>";
                else sTable += "<td>" + gData[i].LHipImplant + "</td>";

                if (gData[i].HipSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LAcetabulumCup + "<BR>R: " + gData[i].RAcetabulumCup + "</td>";
                else sTable += "<td>" + gData[i].LAcetabulumCup + "</td>";

                if (gData[i].HipSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LLiner + "<BR>R: " + gData[i].RLiner + "</td>";
                else sTable += "<td>" + gData[i].LLiner + "</td>";

                if (gData[i].HipSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LFemoralStem + "<BR>R: " + gData[i].RFemoralStem + "</td>";
                else sTable += "<td>" + gData[i].LFemoralStem + "</td>";

                if (gData[i].HipSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LFemoralHead + "<BR>R: " + gData[i].RFemoralHead + "</td>";
                else sTable += "<td>" + gData[i].LFemoralHead + "</td>";

                //sTable += "<td>" + gData[i].LAcetabulumCup + "</td>";
                //sTable += "<td>" + gData[i].LLiner + "</td>";
                //sTable += "<td>" + gData[i].LFemoralStem + "</td>";
                //sTable += "<td>" + gData[i].LFemoralHead + "</td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_HipSurgeryDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_HipReplacementDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblHipSurgeryList_body").append(sTable);
            }
        }
    }
    else { $("#divHipSurgeryList").empty(); }

    return false;
}
function Edit_HipSurgeryDetail(ID) {
    Bind_HipSurgeryByID(ID, gHipSurgeryList);
    return false;
}
function Bind_HipSurgeryByID(ID, data) {

    $("#btnAddHipSurgery").hide();
    $("#btnUpdateHipSurgery").show();
    $("#txtHipSurgeryName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnHipSNo").val(ID);
            $("#hdnHipID").val(data[i].HipReplacementID);
            $("#ddlHipSurgeryType").val(data[i].HipSurgeryTypeID);
            $("#ddlHipSurgeryType").change();
            $("#txtHipSurgeryName").val(data[i].HipSurgeryName);
            $("#txtHipSurgeryDate").val(data[i].sHipSurgeryDate);
            //Left
            $("#txtLHipImplant").val(data[i].LHipImplant);
            $("#txtLAcetabulum").val(data[i].LAcetabulumCup);
            $("#txtLLiner").val(data[i].LLiner);
            $("#txtLFemoralStem").val(data[i].LFemoralStem);
            $("#txtLFemoralHead").val(data[i].LFemoralHead);
            //Right
            $("#txtRHipImplant").val(data[i].RHipImplant);
            $("#txtRAcetabulum").val(data[i].RAcetabulumCup);
            $("#txtRLiner").val(data[i].RLiner);
            $("#txtRFemoralStem").val(data[i].RFemoralStem);
            $("#txtRFemoralHead").val(data[i].RFemoralHead);

            //LEFT Title
            $("#txtLAcetabulumTitle").val(data[i].LAcetabulumCupTitle);
            $("#txtLLinerTitle").val(data[i].LLinerTitle);
            $("#txtLFemoralStemTitle").val(data[i].LFemoralStemTitle);
            $("#txtLFemoralHeadTitle").val(data[i].LFemoralHeadTitle);

            //RIGHT Title
            $("#txtRAcetabulumTitle").val(data[i].RAcetabulumCupTitle);
            $("#txtRLinerTitle").val(data[i].RLinerTitle);
            $("#txtRFemoralStemTitle").val(data[i].RFemoralStemTitle);
            $("#txtRFemoralHeadTitle").val(data[i].RFemoralHeadTitle);
        }
    }
    $("#Tab31").scrollTop();
    return false;
}
function Update_HipSurgery(oData) {
    for (var i = 0; i < gHipSurgeryList.length; i++) {
        if (gHipSurgeryList[i].sNO == oData.sNO) {
            gHipSurgeryList[i].HipReplacementID = oData.HipReplacementID;
            gHipSurgeryList[i].HipSurgeryTypeID = oData.HipSurgeryTypeID;
            gHipSurgeryList[i].HipSurgeryTypeName = oData.HipSurgeryTypeName;

            gHipSurgeryList[i].HipSurgeryName = oData.HipSurgeryName;
            gHipSurgeryList[i].sHipSurgeryDate = oData.sHipSurgeryDate;
            //Left
            gHipSurgeryList[i].LHipImplant = oData.LHipImplant;
            gHipSurgeryList[i].LAcetabulumCup = oData.LAcetabulumCup;
            gHipSurgeryList[i].LLiner = oData.LLiner;
            gHipSurgeryList[i].LFemoralStem = oData.LFemoralStem;
            gHipSurgeryList[i].LFemoralHead = oData.LFemoralHead;
            //Right
            gHipSurgeryList[i].RHipImplant = oData.RHipImplant;
            gHipSurgeryList[i].RAcetabulumCup = oData.RAcetabulumCup;
            gHipSurgeryList[i].RLiner = oData.RLiner;
            gHipSurgeryList[i].RFemoralStem = oData.RFemoralStem;
            gHipSurgeryList[i].RFemoralHead = oData.RFemoralHead;
            gHipSurgeryList[i].StatusFlag = oData.StatusFlag;

            //LEFT Title
            gHipSurgeryList[i].LAcetabulumCupTitle = oData.LAcetabulumCupTitle;
            gHipSurgeryList[i].LLinerTitle = oData.LLinerTitle;
            gHipSurgeryList[i].LFemoralStemTitle = oData.LFemoralStemTitle;
            gHipSurgeryList[i].LFemoralHeadTitle = oData.LFemoralHeadTitle;

            //RIGHT Title
            gHipSurgeryList[i].RAcetabulumCupTitle = oData.RAcetabulumCupTitle;
            gHipSurgeryList[i].RLinerTitle = oData.RLinerTitle;
            gHipSurgeryList[i].RFemoralStemTitle = oData.RFemoralStemTitle;
            gHipSurgeryList[i].RFemoralHeadTitle = oData.RFemoralHeadTitle;
        }
    }
    DisplayHipSurgeryList(gHipSurgeryList);
    $("#btnAddHipSurgery").show();
    $("#btnUpdateHipSurgery").hide();
    ClearHipSurgeryFields();
    $("#txtHipSurgeryName").focus();
    return false;
}
function Delete_HipReplacementDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gHipSurgeryList.length; i++) {
            if (gHipSurgeryList[i].SNo == ID) {
                var index = jQuery.inArray(gHipSurgeryList[i].valueOf("SNo"), gHipSurgeryList);
                if (gHipSurgeryList[i].SNo > 0) {
                    gHipSurgeryList[i].StatusFlag = "D";
                } else {
                    gHipSurgeryList.splice(index, 1);
                }
                $("#divHipSurgeryList").empty();
                DisplayHipSurgeryList(gHipSurgeryList);
            }
        }
    }
    return false;
}
//Added on 15-09-2017
function ResetHipReplacementTitle() {
    $("#txtLAcetabulumTitle").val("Acetabulum cup");
    $("#txtLLinerTitle").val("Liner");
    $("#txtLFemoralStemTitle").val("Femoral stem");
    $("#txtLFemoralHeadTitle").val("Femoral head");

    $("#txtRAcetabulumTitle").val("Acetabulum cup");
    $("#txtRLinerTitle").val("Liner");
    $("#txtRFemoralStemTitle").val("Femoral stem");
    $("#txtRFemoralHeadTitle").val("Femoral head");
    return false;
}
//=======Hip Replacement Details - END===========================

//==============Knee Replacement Surgery Details=================
$("#ddlKneeSurgeryType").change(function () {
    var iKneeSurgeryType = $("#ddlKneeSurgeryType").val();
    $("#divLeftKnee").hide();
    $("#divRightKnee").hide();
    if (iKneeSurgeryType == 3) {
        $("#divLeftKnee").show();
        $("#divRightKnee").show();
    }
    else if (iKneeSurgeryType == 1)
        $("#divLeftKnee").show();
    else if (iKneeSurgeryType == 2)
        $("#divRightKnee").show();
});
$("#btnAddKneeSurgery,#btnUpdateKneeSurgery").click(function () {
    if ($("#ddlKneeSurgeryType").val() == "0" || $("#ddlKneeSurgeryType").val() == undefined || $("#ddlKneeSurgeryType").val() == null) {
        $.jGrowl("Please Select Type", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSurgeryType").addClass('has-error'); $("#ddlKneeSurgeryType").focus(); return false;
    } else { $("#divSurgeryType").removeClass('has-error'); }

    if ($("#txtKneeSurgeryName").val() == "" || $("#txtKneeSurgeryName").val() == undefined || $("#txtKneeSurgeryName").val() == null) {
        $.jGrowl("Please Enter Surgery Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divKneeSurgeryName").addClass('has-error'); $("#txtKneeSurgeryName").focus(); return false;
    } else { $("#divKneeSurgeryName").removeClass('has-error'); }

    if ($("#txtKneeSurgeryDate").val() == "" || $("#txtKneeSurgeryDate").val() == undefined || $("#txtKneeSurgeryDate").val() == null) {
        $.jGrowl("Please Select Surgery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divKneeSurgeryDate").addClass('has-error'); $("#txtKneeSurgeryDate").focus(); return false;
    } else { $("#divKneeSurgeryDate").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.KneeReplacementID = 0;
    ObjData.SurgeryID = 0;
    ObjData.KneeSurgeryTypeID = $("#ddlKneeSurgeryType").val();
    ObjData.KneeSurgeryType = $("#ddlKneeSurgeryType option:selected").text();
    ObjData.KneeSurgeryName = $("#txtKneeSurgeryName").val();
    ObjData.sKneeSurgeryDate = $("#txtKneeSurgeryDate").val();
    //Left
    ObjData.LKneeImplant = $("#txtLKneeImplant").val();
    ObjData.LFemur = $("#txtLFemur").val();
    ObjData.LTibia = $("#txtLTibia").val();
    ObjData.LPoly = $("#txtLPoly").val();
    ObjData.LStem = $("#txtLStem").val();
    //Right
    ObjData.RKneeImplant = $("#txtRKneeImplant").val();
    ObjData.RFemur = $("#txtRFemur").val();
    ObjData.RTibia = $("#txtRTibia").val();
    ObjData.RPoly = $("#txtRPoly").val();
    ObjData.RStem = $("#txtRStem").val();

    //LEFT Title
    ObjData.LFemurTitle = $("#txtLFemurTitle").val();
    ObjData.LTibiaTitle = $("#txtLTibiaTitle").val();
    ObjData.LPolyTitle = $("#txtLPolyTitle").val();
    ObjData.LStemTitle = $("#txtLStemTitle").val();

    //RIGHT Title
    ObjData.RFemurTitle = $("#txtRFemurTitle").val();
    ObjData.RTibiaTitle = $("#txtRTibiaTitle").val();
    ObjData.RPolyTitle = $("#txtRPolyTitle").val();
    ObjData.RStemTitle = $("#txtRStemTitle").val();

    if ($("#hdnDischargeEntryID").val() > 0)
    { ObjData.DischargeEntryID = $("#hdnDischargeEntryID").val(); }
    else
    { ObjData.DischargeEntryID = 0; }

    if (this.id == "btnAddKneeSurgery") {
        ObjData.sNO = gKneeSurgeryList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.StatusFlag = "I";
        //ObjData.DoctorID = $("#ddlDoctor").val();
        AddKneeSurgeryData(ObjData);
    }
    else if (this.id == "btnUpdateKneeSurgery") {
        ObjData.sNO = $("#hdnKneeSNo").val();
        if ($("#hdnKneeID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.KneeReplacementID = $("#hdnKneeID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.KneeReplacementID = 0;
        }
        Update_KneeSurgery(ObjData);
    }
    ClearKneeSurgeryFields();
    GenerateLocalExamination($("#ddlKneeSurgeryType").val(), "KNEE");
    $("#ddlKneeSurgeryType").focus();
});
function ClearKneeSurgeryFields() {
    $("#btnAddKneeSurgery").show();
    $("#btnUpdateKneeSurgery").hide();
    $("#ddlKneeSurgeryType").val('0');
    $("#txtKneeSurgeryName").val("");
    $("#txtKneeSurgeryDate").val("");

    $("#txtLKneeImplant").val("");
    $("#txtLFemur").val("");
    $("#txtLTibia").val("");
    $("#txtLPoly").val("");
    $("#txtLStem").val("");
    $("#hdnKneeSNo").val("");
    $("#hdnKneeID").val("");

    $("#txtRKneeImplant").val("");
    $("#txtRFemur").val("");
    $("#txtRTibia").val("");
    $("#txtRPoly").val("");
    $("#txtRStem").val("");

    $("#divLeftKnee").hide();
    $("#divRightKnee").hide();
    $("#divSurgeryType").removeClass('has-error');
    $("#divKneeSurgeryName").removeClass('has-error');
    $("#divKneeSurgeryDate").removeClass('has-error');
    ResetKneeReplacementTitle();
    return false;
}
function AddKneeSurgeryData(oData) {
    gKneeSurgeryList.push(oData);
    DisplayKneeSurgeryList(gKneeSurgeryList);
    return false;
}
function DisplayKneeSurgeryList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divKneeSurgeryList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divKneeSurgeryList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblKneeSurgeryList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Type</th>";
        sTable += "<th class='" + sColorCode + "'>Surgery Name</th>";
        sTable += "<th class='" + sColorCode + "'>Surgery Date</th>";
        sTable += "<th class='" + sColorCode + "'>Implant</th>";
        sTable += "<th class='" + sColorCode + "'>Femur</th>";
        sTable += "<th class='" + sColorCode + "'>Tibia</th>";
        sTable += "<th class='" + sColorCode + "'>Poly</th>";
        sTable += "<th class='" + sColorCode + "'>Stem</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblKneeSurgeryList_body'>";
        sTable += "</tbody></table>";
        $("#divKneeSurgeryList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].KneeSurgeryType + "</td>";
                sTable += "<td>" + gData[i].KneeSurgeryName + "</td>";
                sTable += "<td>" + gData[i].sKneeSurgeryDate + "</td>";

                if (gData[i].KneeSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LKneeImplant + "<BR>R: " + gData[i].RKneeImplant + "</td>";
                else sTable += "<td>" + gData[i].LKneeImplant + "</td>";

                if (gData[i].KneeSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LFemur + "<BR>R: " + gData[i].RFemur + "</td>";
                else sTable += "<td>" + gData[i].LFemur + "</td>";

                if (gData[i].KneeSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LTibia + "<BR>R: " + gData[i].RTibia + "</td>";
                else sTable += "<td>" + gData[i].LTibia + "</td>";

                if (gData[i].KneeSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LPoly + "<BR>R: " + gData[i].RPoly + "</td>";
                else sTable += "<td>" + gData[i].LPoly + "</td>";

                if (gData[i].KneeSurgeryTypeID == 3) sTable += "<td>L: " + gData[i].LStem + "<BR>R: " + gData[i].RStem + "</td>";
                else sTable += "<td>" + gData[i].LStem + "</td>";

                //sTable += "<td>" + gData[i].LFemur + "</td>";
                //sTable += "<td>" + gData[i].LTibia + "</td>";
                //sTable += "<td>" + gData[i].LPoly + "</td>";
                //sTable += "<td>" + gData[i].LStem + "</td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_KneeSurgeryDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_KneeReplacementDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblKneeSurgeryList_body").append(sTable);
            }
        }
    }
    else { $("#divKneeSurgeryList").empty(); }

    return false;
}
function Edit_KneeSurgeryDetail(ID) {
    Bind_KneeSurgeryByID(ID, gKneeSurgeryList);
    return false;
}
function Bind_KneeSurgeryByID(ID, data) {

    $("#btnAddKneeSurgery").hide();
    $("#btnUpdateKneeSurgery").show();
    $("#txtKneeSurgeryName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnKneeSNo").val(ID);
            $("#hdnKneeID").val(data[i].KneeReplacementID);
            $("#ddlKneeSurgeryType").val(data[i].KneeSurgeryTypeID);
            $("#ddlKneeSurgeryType").change();
            $("#txtKneeSurgeryName").val(data[i].KneeSurgeryName);
            $("#txtKneeSurgeryDate").val(data[i].sKneeSurgeryDate);
            //Left
            $("#txtLKneeImplant").val(data[i].LKneeImplant);
            $("#txtLFemur").val(data[i].LFemur);
            $("#txtLTibia").val(data[i].LTibia);
            $("#txtLPoly").val(data[i].LPoly);
            $("#txtLStem").val(data[i].LStem);
            //Left
            $("#txtRKneeImplant").val(data[i].RKneeImplant);
            $("#txtRFemur").val(data[i].RFemur);
            $("#txtRTibia").val(data[i].RTibia);
            $("#txtRPoly").val(data[i].RPoly);
            $("#txtRStem").val(data[i].RStem);

            //LEFT Title
            $("#txtLFemurTitle").val(data[i].LFemurTitle);
            $("#txtLTibiaTitle").val(data[i].LTibiaTitle);
            $("#txtLPolyTitle").val(data[i].LPolyTitle);
            $("#txtLStemTitle").val(data[i].LStemTitle);

            //RIGHT Title
            $("#txtRFemurTitle").val(data[i].RFemurTitle);
            $("#txtRTibiaTitle").val(data[i].RTibiaTitle);
            $("#txtRPolyTitle").val(data[i].RPolyTitle);
            $("#txtRStemTitle").val(data[i].RStemTitle);
        }
    }
    return false;
}
function Update_KneeSurgery(oData) {
    for (var i = 0; i < gKneeSurgeryList.length; i++) {
        if (gKneeSurgeryList[i].sNO == oData.sNO) {
            gKneeSurgeryList[i].KneeReplacementID = oData.KneeReplacementID;
            gKneeSurgeryList[i].KneeSurgeryType = oData.KneeSurgeryType;
            gKneeSurgeryList[i].KneeSurgeryName = oData.KneeSurgeryName;
            gKneeSurgeryList[i].sKneeSurgeryDate = oData.sKneeSurgeryDate;
            //Left
            gKneeSurgeryList[i].LKneeImplant = oData.LKneeImplant;
            gKneeSurgeryList[i].LFemur = oData.LFemur;
            gKneeSurgeryList[i].LTibia = oData.LTibia;
            gKneeSurgeryList[i].LPoly = oData.LPoly;
            gKneeSurgeryList[i].LStem = oData.LStem;
            //Right
            gKneeSurgeryList[i].RKneeImplant = oData.RKneeImplant;
            gKneeSurgeryList[i].RFemur = oData.RFemur;
            gKneeSurgeryList[i].RTibia = oData.RTibia;
            gKneeSurgeryList[i].RPoly = oData.RPoly;
            gKneeSurgeryList[i].RStem = oData.RStem;
            gKneeSurgeryList[i].StatusFlag = oData.StatusFlag;

            //LEFT Title
            gKneeSurgeryList[i].LFemurTitle = oData.LFemurTitle;
            gKneeSurgeryList[i].LTibiaTitle = oData.LTibiaTitle;
            gKneeSurgeryList[i].LPolyTitle = oData.LPolyTitle;
            gKneeSurgeryList[i].LStemTitle = oData.LStemTitle;

            //RIGHT Title
            gKneeSurgeryList[i].RFemurTitle = oData.RFemurTitle;
            gKneeSurgeryList[i].RTibiaTitle = oData.RTibiaTitle;
            gKneeSurgeryList[i].RPolyTitle = oData.RPolyTitle;
            gKneeSurgeryList[i].RStemTitle = oData.RStemTitle;
        }
    }
    DisplayKneeSurgeryList(gKneeSurgeryList);
    $("#btnAddKneeSurgery").show();
    $("#btnUpdateKneeSurgery").hide();
    ClearKneeSurgeryFields();
    $("#ddlKneeSurgeryType").focus();
    return false;
}
function Delete_KneeReplacementDetail(ID) {
    alert(ID);
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gKneeSurgeryList.length; i++) {
            if (gKneeSurgeryList[i].SNo == ID) {
                var index = jQuery.inArray(gKneeSurgeryList[i].valueOf("SNo"), gKneeSurgeryList);
                if (gKneeSurgeryList[i].SNo > 0) {
                    gKneeSurgeryList[i].StatusFlag = "D";
                } else {
                    gKneeSurgeryList.splice(index, 1);
                }
                $("#divKneeSurgeryList").empty();
                DisplayKneeSurgeryList(gKneeSurgeryList);
            }
        }
    }
    return false;
}
//Added on 15-09-2017
function ResetKneeReplacementTitle() {
    $("#txtLFemurTitle").val("Femur");
    $("#txtLTibiaTitle").val("Tibia");
    $("#txtLPolyTitle").val("Poly");
    $("#txtLStemTitle").val("Stem");

    $("#txtRFemurTitle").val("Femur");
    $("#txtRTibiaTitle").val("Tibia");
    $("#txtRPolyTitle").val("Poly");
    $("#txtRStemTitle").val("Stem");
    return false;
}
//==========Knee Replacement Details - END=======================

//==============Other Surgery Details=================
$("#rdbExistingOtherProcedure,#rdbNewOtherProcedure").click(function () {
    if (this.id == "rdbNewOtherProcedure") {
        $("#divSelectOtherProcedure").hide();
        $("#divNewOtherProcedure").show();
    }
    else if (this.id == "rdbExistingOtherProcedure") {
        $("#divSelectOtherProcedure").show();
        $("#divNewOtherProcedure").hide();
    }
});
function GetOtherProcedureList() {
    dProgress(true);
    $("#ddlOtherProcedure").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetOtherProcedure",
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
                                    $("#ddlOtherProcedure").append('<option value=' + obj[index].OtherProcedureID + ' >' + obj[index].OtherProcedureName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlOtherProcedure").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlOtherProcedure").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
$("#ddlOtherProcedure").change(function () {
    var ID = $("#ddlOtherProcedure").val();
    if (ID != undefined && ID > 0)
        GetOtherProcedureByID(ID);
    else $("#txtDetailProcedure").val("");
    return false;
});
function GetOtherProcedureByID(id) {
    $("#txtDetailProcedure").val("");
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetOtherProcedureByID",
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
                            $("#txtDetailProcedure").val(obj.OtherProcedureDescription);
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
$("#btnAddOtherSurgery,#btnUpdateOtherSurgery").click(function () {
    //if ($("#txtOtherSurgeryName").val() == "" || $("#txtOtherSurgeryName").val() == undefined || $("#txtOtherSurgeryName").val() == null) {
    //    $.jGrowl("Please Enter Surgery Name", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divOtherSurgeryName").addClass('has-error'); $("#txtOtherSurgeryName").focus(); return false;
    //} else { $("#divOtherSurgeryName").removeClass('has-error'); }

    if ($("#txtOtherSurgeryDate").val() == "" || $("#txtOtherSurgeryDate").val() == undefined || $("#txtOtherSurgeryDate").val() == null) {
        $.jGrowl("Please Select Surgery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divOtherSurgeryDate").addClass('has-error'); $("#txtOtherSurgeryDate").focus(); return false;
    } else { $("#divOtherSurgeryDate").removeClass('has-error'); }

    if ($("#rdbNewOtherProcedure").is(':checked')) {
        if ($("#txtNewOtherProcedure").val() == "" || $("#txtNewOtherProcedure").val() == undefined || $("#txtNewOtherProcedure").val() == null) {
            $.jGrowl("Please enter Procedure", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtNewOtherProcedure").focus(); return false;
        }
    }
    else if ($("#rdbExistingOtherProcedure").is(':checked')) {
        if ($("#ddlOtherProcedure").val() == "0" || $("#ddlOtherProcedure").val() == undefined || $("#ddlOtherProcedure").val() == null) {
            $.jGrowl("Please Select Procedure", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#ddlOtherProcedure").focus(); return false;
        }
    }

    var ObjData = new Object();
    ObjData.OtherSurgeryID = 0;
    ObjData.SurgeryID = 0;
    ObjData.OtherSurgeryName = $("#txtOtherSurgeryName").val();
    ObjData.sOtherSurgeryDate = $("#txtOtherSurgeryDate").val();

    var oOtherProcedure = new Object();
    if ($("#rdbNewOtherProcedure").is(':checked')) {
        oOtherProcedure.OtherProcedureID = 0;
        oOtherProcedure.OtherProcedureName = $("#txtNewOtherProcedure").val().trim();
    }
    else if ($("#rdbExistingOtherProcedure").is(':checked')) {
        oOtherProcedure.OtherProcedureID = $("#ddlOtherProcedure").val();
        oOtherProcedure.OtherProcedureName = $("#ddlOtherProcedure option:selected").text();
    }
    oOtherProcedure.OtherProcedureDescription = $("#txtDetailProcedure").val();
    ObjData.OtherProcedure = oOtherProcedure;

    if ($("#hdnDischargeEntryID").val() > 0)
    { ObjData.DischargeEntryID = $("#hdnDischargeEntryID").val(); }
    else
    { ObjData.DischargeEntryID = 0; }

    if (this.id == "btnAddOtherSurgery") {
        ObjData.sNO = gOtherSurgeryList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.StatusFlag = "I";
        AddOtherSurgeryData(ObjData);
    }
    else if (this.id == "btnUpdateOtherSurgery") {
        ObjData.sNO = $("#hdnOtherSNo").val();
        if ($("#hdnOtherID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.OtherSurgeryID = $("#hdnOtherID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.OtherSurgeryID = 0;
        }
        Update_OtherSurgery(ObjData);
    }
    ClearOtherSurgeryFields();
    $("#txtOtherSurgeryName").focus();
});
function ClearOtherSurgeryFields() {
    $("#btnAddOtherSurgery").show();
    $("#btnUpdateOtherSurgery").hide();
    $("#txtOtherSurgeryName").val("");
    $("#txtOtherSurgeryDate").val("");
    $("#hdnOtherSNo").val("");
    $("#hdnOtherID").val("");

    $("#ddlOtherProcedure").val(null).change();
    $("#divNewOtherProcedure").hide();
    $("#divSelectOtherProcedure").show();
    $("#rdbNewOtherProcedure").prop("checked", false);
    $("#rdbExistingOtherProcedure").prop("checked", true);

    $("#txtNewOtherProcedure").val("");
    $("#divOtherSurgeryDate").removeClass('has-error');
    return false;
}
function AddOtherSurgeryData(oData) {
    gOtherSurgeryList.push(oData);
    DisplayOtherSurgeryList(gOtherSurgeryList);
    return false;
}
function DisplayOtherSurgeryList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    $("#divOtherSurgeryList").empty();

    if (gData.length >= 5)
    { $("#divOtherSurgeryList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divOtherSurgeryList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOtherSurgeryList' class='table no-margin table-hover table-condensed'>";
        sTable += "<thead><tr class='" + sColorCode + "'><th style='width:3px;text-align: center'>S.No</th>";
        //sTable += "<th>Surgery Name</th>";
        sTable += "<th>Surgery Date</th>";
        sTable += "<th>Procedure</th>";
        sTable += "<th>Detail Procedure</th>";
        sTable += "<th style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOtherSurgeryList_body'>";
        sTable += "</tbody></table>";
        $("#divOtherSurgeryList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                // sTable += "<td>" + gData[i].OtherSurgeryName + "</td>";
                sTable += "<td>" + gData[i].sOtherSurgeryDate + "</td>";
                sTable += "<td>" + gData[i].OtherProcedure.OtherProcedureName + "</td>";
                sTable += "<td>" + gData[i].OtherProcedure.OtherProcedureDescription + "</td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OtherSurgeryDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td style='width:3px;text-align: center'><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OtherSurgeryDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblOtherSurgeryList_body").append(sTable);
            }
        }
    }
    else { $("#divOtherSurgeryList").empty(); }

    return false;
}
function Edit_OtherSurgeryDetail(ID) {
    Bind_OtherSurgeryByID(ID, gOtherSurgeryList);
    return false;
}
function Bind_OtherSurgeryByID(ID, data) {
    $("#btnAddOtherSurgery").hide();
    $("#btnUpdateOtherSurgery").show();
    $("#txtOtherSurgeryName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnOtherSNo").val(ID);
            $("#hdnOtherID").val(data[i].OtherSurgeryID);
            $("#txtOtherSurgeryName").val(data[i].OtherSurgeryName);
            $("#txtOtherSurgeryDate").val(data[i].sOtherSurgeryDate);
            if (data[i].OtherProcedure.OtherProcedureID == 0) {
                $("#divNewOtherProcedure").show();
                $("#divSelectOtherProcedure").hide();
                $("#rdbNewOtherProcedure").prop("checked", true);
                $("#rdbExistingOtherProcedure").prop("checked", false);
                $("#txtNewOtherProcedure").val(data[i].OtherProcedure.OtherProcedureName);
                $("#txtDetailProcedure").val(data[i].OtherProcedure.OtherProcedureDescription);
            }
            else if (data[i].OtherProcedure.OtherProcedureID > 0) {
                $("#divNewOtherProcedure").hide();
                $("#divSelectOtherProcedure").show();
                $("#rdbNewOtherProcedure").prop("checked", false);
                $("#rdbExistingOtherProcedure").prop("checked", true);
                $("#ddlOtherProcedure").val(data[i].OtherProcedure.OtherProcedureID).change();
                $("#txtDetailProcedure").val(data[i].OtherProcedure.OtherProcedureDescription);
            }
        }
    }
    return false;
}
function Update_OtherSurgery(oData) {
    for (var i = 0; i < gOtherSurgeryList.length; i++) {
        if (gOtherSurgeryList[i].sNO == oData.sNO) {
            gOtherSurgeryList[i].OtherSurgeryID = oData.OtherSurgeryID;
            gOtherSurgeryList[i].OtherSurgeryName = oData.OtherSurgeryName;
            gOtherSurgeryList[i].sOtherSurgeryDate = oData.sOtherSurgeryDate;
            gOtherSurgeryList[i].StatusFlag = oData.StatusFlag;

            var oOtherProcedure = new Object();
            oOtherProcedure.OtherProcedureID = oData.OtherProcedure.OtherProcedureID;
            oOtherProcedure.OtherProcedureName = oData.OtherProcedure.OtherProcedureName;
            oOtherProcedure.OtherProcedureDescription = oData.OtherProcedure.OtherProcedureDescription;
            gOtherSurgeryList[i].OtherProcedure = oOtherProcedure;
        }
    }
    DisplayOtherSurgeryList(gOtherSurgeryList);
    $("#btnAddOtherSurgery").show();
    $("#btnUpdateOtherSurgery").hide();
    ClearOtherSurgeryFields();
    $("#ddlOtherSurgeryType").focus();
    return false;
}
function Delete_OtherSurgeryDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gOtherSurgeryList.length; i++) {
            if (gOtherSurgeryList[i].SNo == ID) {
                var index = jQuery.inArray(gOtherSurgeryList[i].valueOf("SNo"), gOtherSurgeryList);
                if (gOtherSurgeryList[i].SNo > 0) {
                    gOtherSurgeryList[i].StatusFlag = "D";
                } else {
                    gOtherSurgeryList.splice(index, 1);
                }
                DisplayOtherSurgeryList(gOtherSurgeryList);
            }
        }
    }
    return false;
}
//=====Other Surgery Details - END=========

//==============Advice Medications (VITO)=================
$("#btnAddPrescription,#btnUpdatePrescription").click(function () {
    if ($("#rdbNewDrugName").is(':checked')) {
        if ($("#txtNewDrugName").val() == "" || $("#txtNewDrugName").val() == undefined || $("#txtNewDrugName").val() == null) {
            $.jGrowl("Please enter Drug Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divNewDrugName").addClass('has-error'); $("#txtNewDrugName").focus(); return false;
        } else { $("#divNewDrugName").removeClass('has-error'); }
    }
    else if ($("#rdbExistingDrugName").is(':checked')) {
        if ($("#ddlDrugName").val() == "0" || $("#ddlDrugName").val() == undefined || $("#ddlDrugName").val() == null) {
            $.jGrowl("Please Select Drug Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divDrugName").addClass('has-error'); $("#ddlDrugName").focus(); return false;
        } else { $("#divDrugName").removeClass('has-error'); }
    }

    if ($("#txtDosage").val() == "" || $("#txtDosage").val() == undefined || $("#txtDosage").val() == null) {
        $.jGrowl("Please enter Dosage", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDosage").addClass('has-error'); $("#txtDosage").focus(); return false;
    } else { $("#divDosage").removeClass('has-error'); }

    if ($("#txtFrequency").val() == "" || $("#txtFrequency").val() == undefined || $("#txtFrequency").val() == null) {
        $.jGrowl("Please enter Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divFrequency").addClass('has-error'); $("#txtFrequency").focus(); return false;
    } else { $("#divFrequency").removeClass('has-error'); }

    //if ($("#ddlFrequency").val() == "0" || $("#ddlFrequency").val() == undefined) {
    //    $.jGrowl("Please select Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divFrequency").addClass('has-error'); $("#ddlFrequency").focus(); return false;
    //} else { $("#divFrequency").removeClass('has-error'); }

    //if ($("#ddlFrequency").val() == "9") {
    //    if ($("#txtOtherFrqeuency").val() == "" || $("#txtOtherFrqeuency").val() == undefined || $("#txtOtherFrqeuency").val() == null) {
    //        $.jGrowl("Please enter Other Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divOtherFrqeuency").addClass('has-error'); $("#txtOtherFrqeuency").focus(); return false;
    //    } else { $("#divOtherFrqeuency").removeClass('has-error'); }
    //}

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
    if ($("#rdbNewDrugName").is(':checked')) {
        oDrug.DrugID = 0;
        oDrug.DrugName = $("#txtNewDrugName").val();
    }
    else if ($("#rdbExistingDrugName").is(':checked')) {
        oDrug.DrugID = $("#ddlDrugName").val();
        oDrug.DrugName = $("#ddlDrugName option:selected").text();
    }
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
    ObjData.OtherFrequency = $("#txtOtherFrqeuency").val();

    if ($("#hdnDischargeEntryID").val() > 0)
    { ObjData.DischargeEntryID = $("#hdnDischargeEntryID").val(); }
    else
    { ObjData.DischargeEntryID = 0; }

    if (this.id == "btnAddPrescription") {
        ObjData.sNO = gPrescriptionList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.PrescriptionID = 0;
        ObjData.StatusFlag = "I";
        AddPrescriptionData(ObjData);
    }
    else if (this.id == "btnUpdatePrescription") {
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
    $("#txtPrescriptionDate").val("");
    $("#ddlFrequency").val("0");
    $("#txtDuration").val("");
    $("#txtFrequency").val("");
    $("#ddlInstruction").val('0');
    $("#txtIngredient").val("");

    $("#hdnPrescriptionSNo").val("");
    $("#hdnPrescriptionID").val("");
    $("#hdnDosageID").val("");
    $("#hdnDurationID").val("");
    $("#hdnFrequencyID").val("");

    $("#divOtherFrqeuency").hide();
    $("#txtOtherFrqeuency").val("");
    $("#divOtherFrqeuency").removeClass('has-error');

    $("#ddlDrugName").val(null).change();
    $("#divNewDrugName").hide();
    $("#divSelectDrugName").show();
    $("#rdbNewDrugName").prop("checked", false);
    $("#rdbExistingDrugName").prop("checked", true);

    $("#txtNewDrugName").val("");

    $("#divNewDrugName").removeClass('has-error');
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

                if (gData[i].FrequencyID == 9)
                    sTable += "<td>" + gData[i].OtherFrequency + "</td>";
                else
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
function Edit_PrescriptionDetail(ID) {
    Bind_PrescriptionByID(ID, gPrescriptionList);
    return false;
}
function Bind_PrescriptionByID(ID, data) {
    $("#btnAddPrescription").hide();
    $("#btnUpdatePrescription").show();
    $("#ddlDrugName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnPrescriptionSNo").val(ID);
            $("#hdnPrescriptionID").val(data[i].PrescriptionID);
            $("#ddlDrugName").val(data[i].Drug.DrugID).change();
            $("#txtDosage").val(data[i].Dosage);
            $("#hdnDosageID").val(data[i].DosageID);
            $("#hdnFrequencyID").val(data[i].FrequencyID);
            $("#txtFrequency").val(data[i].Frequency);
            //$("#txtOtherFrqeuency").val(data[i].OtherFrequency);
            $("#txtDuration").val(data[i].Duration);
            $("#hdnDurationID").val(data[i].DurationID);
            $("#ddlInstruction").val(data[i].InstructionType);
            $("#txtIngredient").val(data[i].Ingredient);

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
            //gPrescriptionList[i].OtherFrequency = oData.OtherFrequency;
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
//==========Advice Medications (VITO) - END======

//==============Save and Update All Discharge Details=================
function ClearAdmissionFields() {
    $("#txtAdmissionNo").val("");
    $("#txtUHIDNo").val("");
    $("#txtRoomNo").val("");
    $("#txtContactNo").val("");
    $("#txtPatientName").val("");
    $("#txtAge").val("");
    $("#ddlGender").val("0");
    $("#txtPrimaryConsultant").val("");
    $("#txtAdmissionDate").val("");
    $("#txtAdmissionTime").val("");
    $("#txtSurgeryDate").val("");
    $("#txtDischargeDate").val("");
    $("#txtDischargeTime").val("");
    $("#txtMLCNo").val("");
    $("#txtPatientAddress").val("");

    $("#divAdmissionNo").removeClass('has-error');
    $("#divUHIDNo").removeClass('has-error');
    $("#divRoomNo").removeClass('has-error');
    $("#divPatientName").removeClass('has-error');
    $("#divAdmissionDate").removeClass('has-error');
    $("#divAdmissionTime").removeClass('has-error');
    $("#divDischargeDate").removeClass('has-error');
    $("#divDischargeTime").removeClass('has-error');

    return false;
}

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave")
    { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
    else if (this.id == "btnUpdate")
    { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

    //Modified on 03-04-2018 - If Day Case Summary Not required validation
    if ($("#ddlSummaryType").val() != 5) {
        //Validate Admission Details
        //if ($("#txtAdmissionNo").val().trim() == "" || $("#txtAdmissionNo").val().trim() == undefined) {
        //    $.jGrowl("Please enter Admission No", { sticky: false, theme: 'warning', life: jGrowlLife });
        //    $("#divAdmissionNo").addClass('has-error'); $("#txtAdmissionNo").focus(); return false;
        //} else { $("#divAdmissionNo").removeClass('has-error'); }

        //if ($("#txtUHIDNo").val().trim() == "" || $("#txtUHIDNo").val().trim() == undefined) {
        //    $.jGrowl("Please enter UHID No", { sticky: false, theme: 'warning', life: jGrowlLife });
        //    $("#divUHIDNo").addClass('has-error'); $("#txtUHIDNo").focus(); return false;
        //} else { $("#divUHIDNo").removeClass('has-error'); }

        //if ($("#txtRoomNo").val().trim() == "" || $("#txtRoomNo").val().trim() == undefined) {
        //    $.jGrowl("Please enter Room No", { sticky: false, theme: 'warning', life: jGrowlLife });
        //    $("#divRoomNo").addClass('has-error'); $("#txtRoomNo").focus(); return false;
        //} else { $("#divRoomNo").removeClass('has-error'); }
    }

    if ($("#txtPatientName").val().trim() == "" || $("#txtPatientName").val().trim() == undefined) {
        $.jGrowl("Please enter Patient Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatientName").addClass('has-error'); $("#txtPatientName").focus(); return false;
    } else { $("#divPatientName").removeClass('has-error'); }


    //if ($("#txtAdmissionDate").val().trim() == "" || $("#txtAdmissionDate").val().trim() == undefined) {
    //    $.jGrowl("Please select Admission Date", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divAdmissionDate").addClass('has-error'); $("#txtAdmissionDate").focus(); return false;
    //} else { $("#divAdmissionDate").removeClass('has-error'); }

    //if ($("#txtAdmissionTime").val().trim() == "" || $("#txtAdmissionTime").val().trim() == undefined) {
    //    $.jGrowl("Please select Admission Time", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divAdmissionTime").addClass('has-error'); $("#txtAdmissionTime").focus(); return false;
    //} else { $("#divAdmissionTime").removeClass('has-error'); }

    //if ($("#txtSurgeryDate").val().trim() == "" || $("#txtSurgeryDate").val().trim() == undefined) {
    //    $.jGrowl("Please select Surgery Date", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divSurgeryDate").addClass('has-error'); $("#txtSurgeryDate").focus(); return false;
    //} else { $("#divSurgeryDate").removeClass('has-error'); }

    //if ($("#txtDischargeDate").val().trim() == "" || $("#txtDischargeDate").val().trim() == undefined) {
    //    $.jGrowl("Please select Discharge Date", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divDischargeDate").addClass('has-error'); $("#txtDischargeDate").focus(); return false;
    //} else { $("#divDischargeDate").removeClass('has-error'); }

    //if ($("#txtDischargeTime").val().trim() == "" || $("#txtDischargeTime").val().trim() == undefined) {
    //    $.jGrowl("Please select Discharge Time", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divDischargeTime").addClass('has-error'); $("#txtAdmissionTime").focus(); return false;
    //} else { $("#divDischargeTime").removeClass('has-error'); }

    //Discharge Validation
    //if ($("#ddlCoConsultant").val() == "0" || $("#ddlCoConsultant").val() == undefined || $("#ddlCoConsultant").val() == null) {
    //    $.jGrowl("Please select Co-Consultant", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divCoConsultant").addClass('has-error'); $("#ddlCoConsultant").focus(); return false;
    //}
    //else { $("#divCoConsultant").removeClass('has-error'); }

    //Commented on 03-04-2018 Removed Registrar Mandatory
    //if ($("#ddlRegistrar").val() == "0" || $("#ddlRegistrar").val() == undefined || $("#ddlRegistrar").val() == null) {
    //    $.jGrowl("Please select Registrar", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divRegistrar").addClass('has-error'); $("#ddlRegistrar").focus(); return false;
    //} else { $("#divRegistrar").removeClass('has-error'); }

    //ADMISSION
    var objAdmission = new Object();
    objAdmission.AdmissionID = 0;
    objAdmission.AdmissionNo = $("#txtAdmissionNo").val();
    objAdmission.UHIDNo = $("#txtUHIDNo").val();
    objAdmission.RoomNo = $("#txtRoomNo").val();
    objAdmission.ContactNo = $("#txtContactNo").val();
    objAdmission.PatientName = $("#txtPatientName").val();
    objAdmission.PatientAge = $("#txtAge").val();
    objAdmission.PatientSex = $("#ddlGender").val();
    //objAdmission.PrimaryConsultant = $("#txtPrimaryConsultant").val();
    var sTemp = "", sID = $("#ddlPrimaryConsultant").val();
    for (var i = 0; i < sID.length; i++)
        sTemp = sTemp + sID[i] + ",";
    objAdmission.PrimaryConsultantID = sTemp.slice(0, -1);
    objAdmission.sDateofAdmissionDate = $("#txtAdmissionDate").val();
    objAdmission.sDateofAdmissionTime = $("#txtAdmissionTime").val();
    objAdmission.MLCNo = $("#txtMLCNo").val();
    objAdmission.PatientAddress = $("#txtPatientAddress").val();

    //DISCHARGE
    var objDischarge = new Object();
    objDischarge.DischargeEntryID = 0;
    objDischarge.Admission = objAdmission;
    objDischarge.sSurgeryDate = $("#txtSurgeryDate").val().trim();
    objDischarge.sDischargeDate = $("#txtDischargeDate").val().trim();
    objDischarge.sDischargeTime = $("#txtDischargeTime").val().trim();

    sTemp = "", sID = $("#ddlCoConsultant").val();
    if (sID != null) {
        for (var i = 0; i < sID.length; i++)
            sTemp = sTemp + sID[i] + ",";
        objDischarge.CoConsultantID = sTemp.slice(0, -1);
    }
    else objDischarge.CoConsultantID = "";

    sTemp = "", sID = $("#ddlRegistrar").val();
    if (sID != null) {
        for (var i = 0; i < sID.length; i++)
            sTemp = sTemp + sID[i] + ",";
        if (sTemp != "" || sTemp != undefined) objDischarge.RegistrarID = sTemp.slice(0, -1);
    }
    else objDischarge.RegistrarID = "";

    //External Doctor
    var sExternalDoctorID = "";
    for (var index = 0; index < gSelectedDoctorsList.length; index++)
        sExternalDoctorID = sExternalDoctorID + gSelectedDoctorsList[index].DoctorID + ",";
    sExternalDoctorID = sExternalDoctorID.slice(0, -1);
    objDischarge.ExternalDoctor = sExternalDoctorID;

    objDischarge.DrugAllergy = $("#txtDrugAllergy").val().trim();
    objDischarge.Diagnosis = $("#txtDiagnosis").val().trim();
    objDischarge.CourseDuringStay = $("#txtCourseDuringStay").val().trim();
    objDischarge.Investigation = $("#txtInvestigation").val().trim();
    objDischarge.PastHistory = $("#txtPastHistory").val().trim();
    objDischarge.GeneralExamination = $("#txtGeneralExamination").val().trim();
    objDischarge.LocalExamination = $("#txtLocalExamination").val().trim();
    objDischarge.AdviseonDischarge = $("#txtAdviseonDischarge").val().trim();

    //Modified on 22-09-2017 (Added if condition)
    if ($("#chkAdvStatus").is(':checked')) {
        objDischarge.sReviewAppointmentDate = $("#txtReviewAppointmentDate").val().trim();
        objDischarge.sReviewAppointmentTime = $("#txtReviewAppointmentTime").val().trim();
    }
    else {
        objDischarge.sReviewAppointmentDate = "";
        objDischarge.sReviewAppointmentTime = "";
    }

    objDischarge.HipReplacement = gHipSurgeryList;
    objDischarge.KneeReplacement = gKneeSurgeryList;
    objDischarge.OtherSurgery = gOtherSurgeryList;
    objDischarge.Prescription = gPrescriptionList;
    //Added on 01-08-2017
    objDischarge.SummaryTypeID = $("#ddlSummaryType").val();
    //Added on 04-09-2017
    objDischarge.CauseofDeath = $("#txtCauseofDeath").val();

    //Added on 05-09-2017
    objDischarge.WrittenBy = $("#txtWrittenBy").val();

    var CheckedByID = 0;
    if ($("#ddlCheckedBy").val() == "0" || $("#ddlCheckedBy").val() == undefined || $("#ddlCheckedBy").val() == null)
        CheckedByID = 0;
    else
        CheckedByID = $("#ddlCheckedBy").val();

    objDischarge.CheckedBy = CheckedByID;

    var sWeekDay = "";
    $.each($("input[name='WeekDay']:checked"), function () {
        sWeekDay = sWeekDay + $(this).val() + ',';
    });
    if (sWeekDay.length > 0) sWeekDay = sWeekDay.slice(0, -1);
    objDischarge.WeekDays = sWeekDay;

    //Added on 08-09-2017
    objDischarge.PatientOwnDrug = gPatientOwnDrug;

    var sMethodName;
    if ($("#hdnDischargeEntryID").val() > 0) {
        objDischarge.DischargeEntryID = $("#hdnDischargeEntryID").val();
        objAdmission.AdmissionID = $("#hdnAdmissionID").val();
        objDischarge.Admission = objAdmission;
        sMethodName = "UpdateDischargeEntry";
    }
    else { sMethodName = "AddDischargeEntry"; }

    SaveandUpdateDischargeEntry(objDischarge, sMethodName);

    return false;
});
function SaveandUpdateDischargeEntry(objDischarge, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: objDischarge }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        if (sMethodName == "AddDischargeEntry") {
                            $("#ddlSummaryType option:selected").text()
                            $.jGrowl($("#ddlSummaryType option:selected").text() + " Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            //$("#btnAddNew").click();
                            EditRecord(objResponse.Value, 0);
                        }
                        else if (sMethodName == "UpdateDischargeEntry") {
                            $.jGrowl($("#ddlSummaryType option:selected").text() + " Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            EditRecord(objDischarge.DischargeEntryID, 0);
                        }
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Admission_A_01" || objResponse.Value == "Admission_U_01") {
                        $.jGrowl("Admission # already exists in Database", { sticky: false, theme: 'danger', life: jGrowlLife });
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
        error: function (xhr, ajaxOptions, thrownError) {
            $.jGrowl("Error : " + xhr.responseText, { sticky: true, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}
function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
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

                                var table = "<tr id='" + obj[index].DoctorID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].DoctorName + "</td>";
                                table += "<td>" + obj[index].Specialization.SpecializationName + "</td>";
                                table += "<td>" + obj[index].State.StateName + "</td>";
                                table += "<td>" + obj[index].City + "</td>";
                                table += "<td>" + obj[index].MobileNo + "</td>";
                                table += "<td>" + obj[index].Department.DepartmentName + "</td>";
                                table += "<td>" + obj[index].DoctorNo + "</td>";
                                table += "<td>" + TypeStatus + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].DoctorID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].DoctorID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].DoctorID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
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

                                    document.title = "View Doctor";
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
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "5%" },
                          { "sWidth": "3%" },
                          { "sWidth": "3%" },
                          { "sWidth": "3%" }
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
function EditRecord(id, iAdmissionID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDischargeEntryByID",
        data: JSON.stringify({ ID: id, AdmissionID: iAdmissionID }),
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
                            $("#btnSavePrint").hide();
                            $("#btnUpdate").show();
                            $("#btnUpdatePrint").show();

                            document.title = "Edit Discharge Entry";

                            //Admission Details
                            var objAdmission = obj.Admission;
                            $("#hdnAdmissionID").val(objAdmission.AdmissionID);
                            $("#txtAdmissionNo").val(objAdmission.AdmissionNo);
                            $("#hdnAdmissionNo").val(objAdmission.AdmissionNo); //Added on 10-09-2017
                            $("#txtUHIDNo").val(objAdmission.UHIDNo);
                            $("#txtRoomNo").val(objAdmission.RoomNo);
                            $("#txtContactNo").val(objAdmission.ContactNo);
                            $("#txtPatientName").val(objAdmission.PatientName);
                            $("#txtAge").val(objAdmission.PatientAge);
                            $("#ddlGender").val(objAdmission.PatientSex);
                            $("#ddlPrimaryConsultant").val(objAdmission.PrimaryConsultantID.split(',')).change();
                            $("#txtAdmissionDate").val(objAdmission.sDateofAdmissionDate);
                            $("#txtAdmissionTime").val(objAdmission.sDateofAdmissionTime);
                            $("#txtMLCNo").val(objAdmission.MLCNo);
                            $("#txtPatientAddress").val(objAdmission.PatientAddress);

                            //Discharge Entry
                            var objDischarge = obj;
                            $("#hdnDischargeEntryID").val(objDischarge.DischargeEntryID);
                            $("#txtSurgeryDate").val(objDischarge.sSurgeryDate);
                            $("#txtDischargeDate").val(objDischarge.sDischargeDate);
                            $("#txtDischargeTime").val(objDischarge.sDischargeTime);
                            $("#ddlCoConsultant").val(objDischarge.CoConsultantID.split(',')).change();
                            $("#ddlRegistrar").val(objDischarge.RegistrarID.split(',')).change();
                            //objDischarge.ExternalDoctor
                            $("#txtDrugAllergy").val(objDischarge.DrugAllergy);
                            $("#txtDiagnosis").val(objDischarge.Diagnosis);
                            $("#txtCourseDuringStay").val(objDischarge.CourseDuringStay);
                            $("#txtInvestigation").val(objDischarge.Investigation);
                            $("#txtPastHistory").val(objDischarge.PastHistory);
                            $("#txtGeneralExamination").val(objDischarge.GeneralExamination);
                            $("#txtLocalExamination").val(objDischarge.LocalExamination);
                            $("#txtAdviseonDischarge").val(objDischarge.AdviseonDischarge);
                            $("#txtReviewAppointmentDate").val(objDischarge.sReviewAppointmentDate);
                            $("#txtReviewAppointmentTime").val(objDischarge.sReviewAppointmentTime);

                            if (objDischarge.sReviewAppointmentDate.length > 0)
                                $("#chkAdvStatus").prop("checked", true);
                            else
                                $("#chkAdvStatus").prop("checked", false);

                            $("#ddlSummaryType").val(objDischarge.SummaryTypeID);
                            $("#ddlSummaryType").change();
                            //Added on 04-09-2017
                            $("#txtCauseofDeath").val(objDischarge.CauseofDeath);
                            //Added on 05-09-2017
                            $("#txtWrittenBy").val(objDischarge.WrittenBy);
                            $("#ddlCheckedBy").val(objDischarge.CheckedBy).change();

                            var sWeekDays = objDischarge.WeekDays.split(',');
                            for (var i = 0; i < sWeekDays.length; i++) {
                                $("#chkWeekDay_" + sWeekDays[i]).prop("checked", true);
                            }

                            //Hip Replacement
                            var objHipReplacement = obj.HipReplacement;
                            gHipSurgeryList = [];
                            for (var i = 0; i < objHipReplacement.length; i++) {
                                var ObjData = new Object();
                                ObjData.HipReplacementID = objHipReplacement[i].HipReplacementID;
                                ObjData.DischargeEntryID = objHipReplacement[i].DischargeEntryID;
                                ObjData.HipSurgeryTypeID = objHipReplacement[i].HipSurgeryTypeID;
                                ObjData.HipSurgeryTypeName = objHipReplacement[i].HipSurgeryTypeID == 1 ? "Left" : objHipReplacement[i].HipSurgeryTypeID == 2 ? "Right" : "Both";
                                ObjData.HipSurgeryName = objHipReplacement[i].HipSurgeryName;
                                ObjData.sHipSurgeryDate = objHipReplacement[i].sHipSurgeryDate;
                                //Left
                                ObjData.LHipImplant = objHipReplacement[i].LHipImplant;
                                ObjData.LAcetabulumCup = objHipReplacement[i].LAcetabulumCup;
                                ObjData.LLiner = objHipReplacement[i].LLiner;
                                ObjData.LFemoralStem = objHipReplacement[i].LFemoralStem;
                                ObjData.LFemoralHead = objHipReplacement[i].LFemoralHead;
                                //Right
                                ObjData.RHipImplant = objHipReplacement[i].RHipImplant;
                                ObjData.RAcetabulumCup = objHipReplacement[i].RAcetabulumCup;
                                ObjData.RLiner = objHipReplacement[i].RLiner;
                                ObjData.RFemoralStem = objHipReplacement[i].RFemoralStem;
                                ObjData.RFemoralHead = objHipReplacement[i].RFemoralHead;

                                //LEFT Title
                                ObjData.LAcetabulumCupTitle = objHipReplacement[i].LAcetabulumCupTitle;
                                ObjData.LLinerTitle = objHipReplacement[i].LLinerTitle;
                                ObjData.LFemoralStemTitle = objHipReplacement[i].LFemoralStemTitle;
                                ObjData.LFemoralHeadTitle = objHipReplacement[i].LFemoralHeadTitle;

                                //RIGHT Title
                                ObjData.RAcetabulumCupTitle = objHipReplacement[i].RAcetabulumCupTitle;
                                ObjData.RLinerTitle = objHipReplacement[i].RLinerTitle;
                                ObjData.RFemoralStemTitle = objHipReplacement[i].RFemoralStemTitle;
                                ObjData.RFemoralHeadTitle = objHipReplacement[i].RFemoralHeadTitle;

                                ObjData.sNO = (i + 1);
                                ObjData.SNo = ObjData.sNO;
                                ObjData.StatusFlag = "";
                                gHipSurgeryList.push(ObjData);
                            }
                            DisplayHipSurgeryList(gHipSurgeryList);

                            //Knee Replacement
                            var objKneeReplacement = obj.KneeReplacement;
                            gKneeSurgeryList = [];
                            for (var i = 0; i < objKneeReplacement.length; i++) {
                                var ObjData = new Object();
                                ObjData.KneeReplacementID = objKneeReplacement[i].KneeReplacementID;
                                ObjData.DischargeEntryID = objKneeReplacement[i].DischargeEntryID;
                                ObjData.KneeSurgeryTypeID = objKneeReplacement[i].KneeSurgeryTypeID;
                                ObjData.KneeSurgeryType = (ObjData.KneeSurgeryTypeID == 1 ? "Left" : ObjData.KneeSurgeryTypeID == 2 ? "Right" : "Both");
                                ObjData.KneeSurgeryName = objKneeReplacement[i].KneeSurgeryName;
                                ObjData.sKneeSurgeryDate = objKneeReplacement[i].sKneeSurgeryDate;
                                //Left
                                ObjData.LKneeImplant = objKneeReplacement[i].LKneeImplant;
                                ObjData.LFemur = objKneeReplacement[i].LFemur;
                                ObjData.LTibia = objKneeReplacement[i].LTibia;
                                ObjData.LPoly = objKneeReplacement[i].LPoly;
                                ObjData.LStem = objKneeReplacement[i].LStem;

                                //Right
                                ObjData.RKneeImplant = objKneeReplacement[i].RKneeImplant;
                                ObjData.RFemur = objKneeReplacement[i].RFemur;
                                ObjData.RTibia = objKneeReplacement[i].RTibia;
                                ObjData.RPoly = objKneeReplacement[i].RPoly;
                                ObjData.RStem = objKneeReplacement[i].RStem;

                                //LEFT Title
                                ObjData.LFemurTitle = objKneeReplacement[i].LFemurTitle;
                                ObjData.LTibiaTitle = objKneeReplacement[i].LTibiaTitle;
                                ObjData.LPolyTitle = objKneeReplacement[i].LPolyTitle;
                                ObjData.LStemTitle = objKneeReplacement[i].LStemTitle;

                                //RIGHT Title
                                ObjData.RFemurTitle = objKneeReplacement[i].RFemurTitle;
                                ObjData.RTibiaTitle = objKneeReplacement[i].RTibiaTitle;
                                ObjData.RPolyTitle = objKneeReplacement[i].RPolyTitle;
                                ObjData.RStemTitle = objKneeReplacement[i].RStemTitle;

                                ObjData.sNO = (i + 1);
                                ObjData.SNo = ObjData.sNO;
                                ObjData.StatusFlag = "";
                                gKneeSurgeryList.push(ObjData);
                            }
                            DisplayKneeSurgeryList(gKneeSurgeryList);

                            //Other Surgery
                            var objOtherSurgery = obj.OtherSurgery;
                            gOtherSurgeryList = [];
                            for (var i = 0; i < objOtherSurgery.length; i++) {
                                var ObjData = new Object();
                                ObjData.OtherSurgeryID = objOtherSurgery[i].OtherSurgeryID;
                                ObjData.DischargeEntryID = objOtherSurgery[i].DischargeEntryID;
                                ObjData.OtherSurgeryName = objOtherSurgery[i].OtherSurgeryName;
                                ObjData.sOtherSurgeryDate = objOtherSurgery[i].sOtherSurgeryDate;

                                var oOtherProcedure = new Object();
                                oOtherProcedure.OtherProcedureID = objOtherSurgery[i].OtherProcedure.OtherProcedureID;
                                oOtherProcedure.OtherProcedureName = objOtherSurgery[i].OtherProcedure.OtherProcedureName;
                                oOtherProcedure.OtherProcedureDescription = objOtherSurgery[i].OtherProcedure.OtherProcedureDescription;
                                ObjData.OtherProcedure = oOtherProcedure;

                                ObjData.sNO = (i + 1);
                                ObjData.SNo = ObjData.sNO;
                                ObjData.StatusFlag = "";
                                gOtherSurgeryList.push(ObjData);
                            }
                            DisplayOtherSurgeryList(gOtherSurgeryList);

                            //Advise Mediciations (VITO)
                            var objPrescription = obj.Prescription;
                            gPrescriptionList = [];
                            for (var i = 0; i < objPrescription.length; i++) {
                                var ObjData = new Object();
                                ObjData.PrescriptionID = objPrescription[i].PrescriptionID;
                                ObjData.DischargeEntryID = objPrescription[i].DischargeEntryID;

                                var oDrug = new Object();
                                oDrug.DrugID = objPrescription[i].Drug.DrugID;
                                oDrug.DrugName = objPrescription[i].Drug.DrugName;
                                ObjData.Drug = oDrug;

                                ObjData.FrequencyID = objPrescription[i].FrequencyID;
                                ObjData.Frequency = objPrescription[i].Frequency; // GetDrugFrequencyByID(objPrescription[i].FrequencyID);
                                ObjData.OtherFrequency = objPrescription[i].OtherFrequency;
                                ObjData.Duration = objPrescription[i].Duration;
                                ObjData.Dosage = objPrescription[i].Dosage;
                                ObjData.InstructionType = objPrescription[i].InstructionType;
                                ObjData.Instruction = objPrescription[i].Instruction;
                                ObjData.Ingredient = objPrescription[i].Ingredient;
                                ObjData.sNO = (i + 1);
                                ObjData.SNo = ObjData.sNO;
                                ObjData.StatusFlag = "";
                                gPrescriptionList.push(ObjData);
                            }
                            DisplayPrescriptionList(gPrescriptionList);

                            //Added on 08-09-2017
                            //Patient Own Drug
                            var objPatientOwnDrug = obj.PatientOwnDrug;
                            gPatientOwnDrug = [];
                            for (var i = 0; i < objPatientOwnDrug.length; i++) {
                                var ObjData = new Object();
                                ObjData.PatientOwnDrugID = objPatientOwnDrug[i].PatientOwnDrugID;
                                ObjData.DischargeEntryID = objPatientOwnDrug[i].DischargeEntryID;

                                var oDrug = new Object();
                                oDrug.DrugID = objPatientOwnDrug[i].Drug.DrugID;
                                oDrug.DrugName = objPatientOwnDrug[i].Drug.DrugName;
                                ObjData.Drug = oDrug;

                                ObjData.FrequencyID = objPatientOwnDrug[i].FrequencyID;
                                ObjData.Frequency = objPatientOwnDrug[i].Frequency; //GetDrugFrequencyByID(objPrescription[i].FrequencyID);
                                ObjData.OtherFrequency = objPatientOwnDrug[i].OtherFrequency;
                                ObjData.Duration = objPatientOwnDrug[i].Duration;
                                ObjData.Dosage = objPatientOwnDrug[i].Dosage;
                                ObjData.InstructionType = objPatientOwnDrug[i].InstructionType;
                                ObjData.Instruction = objPatientOwnDrug[i].Instruction;
                                ObjData.Ingredient = objPatientOwnDrug[i].Ingredient;
                                ObjData.sNO = (i + 1);
                                ObjData.SNo = ObjData.sNO;
                                ObjData.StatusFlag = "";
                                gPatientOwnDrug.push(ObjData);
                            }
                            DisplayPatientPrescriptionList(gPatientOwnDrug);
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
        url: "WebServices/VHMSService.svc/DeleteDoctor",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                        GetRecord();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Doctor_R_01" || objResponse.Value == "Doctor_D_01") {
                        $.jGrowl(_CMDeleteError, { sticky: false, theme: 'danger', life: jGrowlLife });
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
//==========Save and Update All Discharge Details - END============

//Date: 13-07-20171
//Edit Discharge Entry
$("#txtSearchAdmissionNo").keyup(function () {
    var sKey = $("#txtSearchAdmissionNo").val().trim();
    GetAdmissionDetails(sKey);
});
function GetAdmissionDetails(sKey) {
    //dProgress(true);
    $("#divAdmissionResult").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetAdmission",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ key: sKey, RowCount: 100 }),
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            var sTable = "";
                            var sCount = 1;
                            var sColorCode = "bg-info"; //"bg-teal-active color-palette";

                            sTable = "<table id='tblAdmissionList' class='table no-margin table-hover'>";
                            sTable += "<tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'></th>";
                            //Added on 09-09-2017
                            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'></th>";
                            sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'></th>";
                            sTable += "<th class='" + sColorCode + "'>Admission No</th>";
                            sTable += "<th class='" + sColorCode + "'>UHID #</th>";
                            sTable += "<th class='" + sColorCode + "'>Room No</th>";
                            sTable += "<th class='" + sColorCode + "'>Patient Name</th>";
                            sTable += "<th class='" + sColorCode + "'>Age</th>";
                            sTable += "<th class='" + sColorCode + "'>Primary Consultant</th>";
                            sTable += "<th class='" + sColorCode + "'>Admission Dt</th>";
                            sTable += "<th class='" + sColorCode + "'>Time</th>";
                            sTable += "<th class='" + sColorCode + "'>Summary Type</th>";
                            sTable += "</tr>";
                            for (var index = 0; index < obj.length; index++) {
                                sTable += "<tr><td id='" + (index + 1) + "' style='text-align:left;width:3%;'>" + (index + 1) + "</td>";
                                sTable += "<td style='text-align:center;'><a href='#' AdmissionID='" + obj[index].AdmissionID + "' AdmissionNo='" + obj[index].AdmissionNo + "' class='EditDischarge' title='Click here to Edit'><i class='fa fa-lg fa-edit text-blue'/></a></td>";
                                //Added on 09-09-2017
                                sTable += "<td style='text-align:center;'><a href='#' AdmissionID='" + obj[index].AdmissionID + "' AdmissionNo='" + obj[index].AdmissionNo + "'  class='PrintDischarge' title='Click here to Print'><i class='fa fa-lg fa-print text-yellow'/></a></td>";
                                sTable += "<td style='text-align:center;'><a href='#' AdmissionID='" + obj[index].AdmissionID + "' AdmissionNo='" + obj[index].AdmissionNo + "'  class='PrintPrescription' title='Click here to Print Prescription'><i class='fa fa-lg fa-list text-green'/></a></td>";
                                sTable += "<td>" + obj[index].AdmissionNo + "</td>";
                                sTable += "<td>" + obj[index].UHIDNo + "</td>";
                                sTable += "<td>" + obj[index].RoomNo + "</td>";
                                sTable += "<td>" + obj[index].PatientName + "</td>";
                                sTable += "<td>" + obj[index].PatientAge + "/" + (obj[index].PatientSex == 1 ? "M" : obj[index].PatientSex == 2 ? "F" : "T") + "</td>";
                                sTable += "<td>" + obj[index].PrimaryConsultant + "</td>";
                                sTable += "<td>" + obj[index].sDateofAdmissionDate + "</td>";
                                sTable += "<td>" + obj[index].sDateofAdmissionTime + "</td>";
                                sTable += "<td>" + obj[index].SummaryType + "</td>";
                                sTable += "</tr>";
                            }
                            sTable += "</table>";
                            $("#divAdmissionResult").html(sTable);

                            if (obj.length >= 10)
                            { $("#divAdmissionResult").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
                            else
                            { $("#divAdmissionResult").css({ 'height': '', 'min-height': '' }); }

                            $(".EditDischarge").click(function () {
                                var AdmissionID = $(this).attr('AdmissionID');
                                EditRecord(0, AdmissionID);
                            });
                            //Added on 09-09-2017
                            $(".PrintDischarge").click(function () {
                                var AdmissionID = $(this).attr('AdmissionID');
                                var AdmissionNo = $(this).attr('AdmissionNo');
                                $("#hdnAdmissionID").val(AdmissionID);
                                $("#hdnAdmissionNo").val(AdmissionNo);
                                PrintDischargeReport();
                            });
                            $(".PrintPrescription").click(function () {
                                var AdmissionID = $(this).attr('AdmissionID');
                                var AdmissionNo = $(this).attr('AdmissionNo');
                                $("#hdnAdmissionID").val(AdmissionID);
                                $("#hdnAdmissionNo").val(AdmissionNo);
                                PrintPrescriptionDetails();
                            });
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#divAdmissionResult").empty();
                        //$.jGrowl("No Records", { sticky: false, theme: 'warning', life: jGrowlLife });
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
                $("#divAdmissionResult").empty();
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
$("#btnSearch").click(function () {
    var sKey = $("#txtSearchAdmissionNo").val().trim();
    GetAdmissionDetails(sKey);
    return false;
});
$("#btnClearSearch").click(function () {
    $("#divAdmissionResult").empty();
    $("#txtSearchAdmissionNo").val("");
    $("#txtSearchAdmissionNo").focus();
    $("#divAdmissionResult").css({ 'height': '', 'min-height': '' });
    return false;
});
$("#btnAddDischargeEntry").click(function () {
    $("#btnAddNew").click();
    return false;
});

//Added on 01-09-2017
$("#ddlDrugName").change(function () {
    var iDrugID = $("#ddlDrugName").val();
    if (iDrugID != undefined && iDrugID > 0) {
        GetDrugByID(iDrugID);
    }
    else {
        $("#hdnDosageID").val("");
        $("#hdnDurationID").val("");
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
                            $("#hdnDosageID").val(obj.Dosage.DosageID);
                            $("#txtDosage").val(obj.Dosage.DosageName);
                            $("#ddlFrequency").val(obj.FrequencyID);
                            $("#hdnDurationID").val(obj.Duration.DurationID);
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

//Added on 04-09-2017
$("#ddlSummaryType").change(function () {
    var iSummaryType = $("#ddlSummaryType").val();
    if (iSummaryType != undefined && iSummaryType == 4) {
        $("#aTab6").hide(); //Added on 25-10-2017
        $("#aTab7").hide();
        $("#aTab8").hide();
        $("#aTab9").show();
    }
    else {
        $("#aTab6").show(); //Added on 25-10-2017
        $("#aTab7").show();
        $("#aTab8").show();
        $("#aTab9").hide();
    }
});
$("#ddlFrequency").change(function () {
    var iFrequencyID = $("#ddlFrequency").val();
    if (iFrequencyID != undefined && iFrequencyID == 9)
        $("#divOtherFrqeuency").show();
    else
        $("#divOtherFrqeuency").hide();
});

//Added on 05-09-2017
function GetCheckedByDoctorList() {
    dProgress(true);
    $("#ddlCheckedBy").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctor",
        data: JSON.stringify({ DoctorTypeID: 0 }),
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
                                if (obj[index].IsActive && (obj[index].DoctorType.DoctorTypeID == _RegistrarID || obj[index].DoctorType.DoctorTypeID == _CheckedBy))
                                    $("#ddlCheckedBy").append("<option value='" + obj[index].DoctorID + "'>" + obj[index].DoctorName + "</option>");
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlCheckedBy").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlCheckedBy").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
$("#aTab81").click(function ()
{ GetDrugList("ddlDrugName"); });

//Added on 08-09-2017
//==============Patient Own Drug=================
$("#btnAddPatientPrescription,#btnUpdatePatientPrescription").click(function () {
    if ($("#rdbNewPatientDrugName").is(':checked')) {
        if ($("#txtNewPatientDrugName").val() == "" || $("#txtNewPatientDrugName").val() == undefined || $("#txtNewPatientDrugName").val() == null) {
            $.jGrowl("Please enter Drug Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divNewPatientDrugName").addClass('has-error'); $("#txtNewPatientDrugName").focus(); return false;
        } else { $("#divNewPatientDrugName").removeClass('has-error'); }
    }
    else if ($("#rdbExistingPatientDrugName").is(':checked')) {
        if ($("#ddlPatientDrugName").val() == "0" || $("#ddlPatientDrugName").val() == undefined || $("#ddlPatientDrugName").val() == null) {
            $.jGrowl("Please Select Drug Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divSelectPatientDrugName").addClass('has-error'); $("#ddlPatientDrugName").focus(); return false;
        } else { $("#divSelectPatientDrugName").removeClass('has-error'); }
    }

    if ($("#txtPatientDosage").val() == "" || $("#txtPatientDosage").val() == undefined || $("#txtPatientDosage").val() == null) {
        $.jGrowl("Please enter Dosage", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatientDosage").addClass('has-error'); $("#txtPatientDosage").focus(); return false;
    } else { $("#divPatientDosage").removeClass('has-error'); }

    if ($("#txtPatientFrequency").val() == "" || $("#txtPatientFrequency").val() == undefined || $("#txtPatientFrequency").val() == null) {
        $.jGrowl("Please enter Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatientFrequency").addClass('has-error'); $("#txtPatientFrequency").focus(); return false;
    } else { $("#divPatientFrequency").removeClass('has-error'); }

    //if ($("#ddlPatientFrequency").val() == "0" || $("#ddlPatientFrequency").val() == undefined) {
    //    $.jGrowl("Please select Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divPatientFrequency").addClass('has-error'); $("#ddlPatientFrequency").focus(); return false;
    //} else { $("#divPatientFrequency").removeClass('has-error'); }

    //if ($("#ddlPatientFrequency").val() == "9") {
    //    if ($("#txtPatientOtherFrqeuency").val() == "" || $("#txtPatientOtherFrqeuency").val() == undefined || $("#txtPatientOtherFrqeuency").val() == null) {
    //        $.jGrowl("Please enter Other Frequency", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divPatientOtherFrqeuency").addClass('has-error'); $("#txtPatientOtherFrqeuency").focus(); return false;
    //    } else { $("#divPatientOtherFrqeuency").removeClass('has-error'); }
    //}

    if ($("#txtPatientDuration").val() == "" || $("#txtPatientDuration").val() == undefined || $("#txtPatientDuration").val() == null) {
        $.jGrowl("Please enter Duration", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatientDuration").addClass('has-error'); $("#txtPatientDuration").focus(); return false;
    } else { $("#divPatientDuration").removeClass('has-error'); }

    if ($("#ddlPatientInstruction").val() == "0" || $("#ddlPatientInstruction").val() == undefined || $("#ddlPatientInstruction").val() == null) {
        $.jGrowl("Please Select Instruction", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPatientInstruction").addClass('has-error'); $("#ddlPatientInstruction").focus(); return false;
    } else { $("#divPatientInstruction").removeClass('has-error'); }

    var DurationID = 0, DosageID = 0;
    if ($("#txtPatientDuration").val() != null) {
        if ($("#hdnPatientDurationID").val() != null && $("#hdnPatientDurationID").val() > 0 && $("#hdnPatientDurationID").val() != undefined)
            DurationID = $("#hdnPatientDurationID").val();
        else
            $("#hdnPatientDurationID").val(DurationID);
    }
    if ($("#txtPatientDosage").val() != null) {
        if ($("#hdnPatientDosageID").val() != null && $("#hdnPatientDosageID").val() > 0 && $("#hdnPatientDosageID").val() != undefined)
            DosageID = $("#hdnPatientDosageID").val();
        else
            $("#hdnPatientDosageID").val(DosageID);
    }
    if ($("#txtPatientFrequency").val() != null) {
        if ($("#hdnPatientFrequencyID").val() != null && $("#hdnPatientFrequencyID").val() > 0 && $("#hdnPatientFrequencyID").val() != undefined)
            FrequencyID = $("#hdnPatientFrequencyID").val();
        else
            $("#hdnPatientFrequencyID").val(FrequencyID);
    }
    var ObjData = new Object();
    ObjData.PrescriptionID = 0;

    var oDrug = new Object();
    if ($("#rdbNewPatientDrugName").is(':checked')) {
        oDrug.DrugID = 0;
        oDrug.DrugName = $("#txtNewPatientDrugName").val();
    }
    else if ($("#rdbExistingPatientDrugName").is(':checked')) {
        oDrug.DrugID = $("#ddlPatientDrugName").val();
        oDrug.DrugName = $("#ddlPatientDrugName option:selected").text();
    }
    ObjData.Drug = oDrug;

    ObjData.Duration = $("#txtPatientDuration").val();
    ObjData.DurationID = $("#hdnPatientDurationID").val();

    ObjData.InstructionType = $("#ddlPatientInstruction").val();
    ObjData.Instruction = $("#ddlPatientInstruction option:selected").text();
    ObjData.Ingredient = $("#txtPatientIngredient").val();

    ObjData.Dosage = $("#txtPatientDosage").val();
    ObjData.DosageID = $("#hdnPatientDosageID").val();

    ObjData.FrequencyID = $("#hdnPatientFrequencyID").val();
    ObjData.Frequency = $("#txtPatientFrequency").val();
    ObjData.OtherFrequency = $("#txtPatientOtherFrqeuency").val();

    if ($("#hdnDischargeEntryID").val() > 0)
    { ObjData.DischargeEntryID = $("#hdnDischargeEntryID").val(); }
    else
    { ObjData.DischargeEntryID = 0; }

    if (this.id == "btnAddPatientPrescription") {
        ObjData.sNO = gPatientOwnDrug.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.PatientOwnDrugID = 0;
        ObjData.StatusFlag = "I";
        AddPatientPrescriptionData(ObjData);
    }
    else if (this.id == "btnUpdatePatientPrescription") {
        ObjData.sNO = $("#hdnPatientPrescriptionSNo").val();
        if ($("#hdnPatientPrescriptionID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.PatientOwnDrugID = $("#hdnPatientPrescriptionID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.PatientOwnDrugID = 0;
        }
        Update_PatientPrescription(ObjData);
    }
    ClearPatientPrescriptionFields();
    $("#ddlPatientDrugName").focus();
});
function ClearPatientPrescriptionFields() {
    $("#btnAddPatientPrescription").show();
    $("#btnUpdatePatientPrescription").hide();
    $("#ddlPatientDrugName").val(null).change();
    $("#txtPatientDosage").val("");
    $("#txtPatientPrescriptionDate").val("");
    $("#txtPatientFrequency").val("");
    $("#txtPatientDuration").val("");
    $("#ddlPatientInstruction").val('0');
    $("#txtPatientIngredient").val("");

    $("#hdnPatientPrescriptionSNo").val("");
    $("#hdnPatientPrescriptionID").val("");
    $("#hdnPatientDosageID").val("");
    $("#hdnPatientDurationID").val("");
    $("#hdnPatientFrequencyID").val("");

    $("#divPatientOtherFrqeuency").hide();
    $("#txtPatientOtherFrqeuency").val("");
    $("#divPatientOtherFrqeuency").removeClass('has-error');

    $("#ddlPatientDrugName").val(null).change();
    $("#divNewPatientDrugName").hide();
    $("#divSelectPatientDrugName").show();
    $("#rdbNewPatientDrugName").prop("checked", false);
    $("#rdbExistingPatientDrugName").prop("checked", true);

    $("#txtNewPatientDrugName").val("");
    $("#divNewPatientDrugName").removeClass('has-error');
    $("#divSelectPatientDrugName").removeClass('has-error');
    $("#divPatientDosage").removeClass('has-error');
    $("#divPatientFrequency").removeClass('has-error');
    $("#divPatientOtherFrqeuency").removeClass('has-error');
    $("#divPatientDuration").removeClass('has-error');
    $("#divPatientInstruction").removeClass('has-error');
    return false;
}
function AddPatientPrescriptionData(oData) {
    gPatientOwnDrug.push(oData);
    DisplayPatientPrescriptionList(gPatientOwnDrug);
    return false;
}
function DisplayPatientPrescriptionList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divPatientPrescriptionList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divPatientPrescriptionList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblPatientPrescriptionList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Drug Name</th>";
        sTable += "<th class='" + sColorCode + "'>Dosage</th>";
        sTable += "<th class='" + sColorCode + "'>Frequency</th>";
        sTable += "<th class='" + sColorCode + "'>Duration</th>";
        sTable += "<th class='" + sColorCode + "'>Instruction</th>";
        sTable += "<th class='" + sColorCode + "'>Ingredient</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblPatientPrescriptionList_body'>";
        sTable += "</tbody></table>";
        $("#divPatientPrescriptionList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Drug.DrugName + "</td>";
                sTable += "<td>" + gData[i].Dosage + "</td>";

                if (gData[i].FrequencyID == 9)
                    sTable += "<td>" + gData[i].OtherFrequency + "</td>";
                else
                    sTable += "<td>" + gData[i].Frequency + "</td>";

                sTable += "<td>" + gData[i].Duration + "</td>";
                sTable += "<td>" + gData[i].Instruction + "</td>";
                sTable += "<td>" + gData[i].Ingredient + "</td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_PatientPrescriptionDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_PatientPrescriptionDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblPatientPrescriptionList_body").append(sTable);
            }
        }
    }
    else { $("#divPatientPrescriptionList").empty(); }

    return false;
}
function Edit_PatientPrescriptionDetail(ID) {
    Bind_PatientPrescriptionByID(ID, gPatientOwnDrug);
    return false;
}
function Bind_PatientPrescriptionByID(ID, data) {

    $("#btnAddPatientPrescription").hide();
    $("#btnUpdatePatientPrescription").show();
    $("#ddlPatientDrugName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnPatientPrescriptionSNo").val(ID);
            $("#hdnPatientPrescriptionID").val(data[i].PatientOwnDrugID);
            $("#txtPatientDosage").val(data[i].Dosage);
            $("#hdnPatientDosageID").val(data[i].DosageID);
            $("#hdnPatientFrequencyID").val(data[i].FrequencyID);
            $("#txtPatientFrequency").val(data[i].Frequency);
            $("#txtPatientOtherFrqeuency").val(data[i].OtherFrequency);
            $("#txtPatientDuration").val(data[i].Duration);
            $("#hdnPatientDurationID").val(data[i].DurationID);
            $("#ddlPatientInstruction").val(data[i].InstructionType);
            $("#txtPatientIngredient").val(data[i].Ingredient);

            if (data[i].Drug.DrugID == 0) {
                $("#divNewPatientDrugName").show();
                $("#divSelectPatientDrugName").hide();
                $("#rdbNewPatientDrugName").prop("checked", true);
                $("#rdbExistingPatientDrugName").prop("checked", false);
                $("#txtNewPatientDrugName").val(data[i].Drug.DrugName);
            }
            else if (data[i].Drug.DrugID > 0) {
                $("#divNewPatientDrugName").hide();
                $("#divSelectPatientDrugName").show();
                $("#rdbNewPatientDrugName").prop("checked", false);
                $("#rdbExistingPatientDrugName").prop("checked", true);
                $("#ddlPatientDrugName").val(data[i].Drug.DrugID).change();
            }
        }
    }
    return false;
}
function Update_PatientPrescription(oData) {
    for (var i = 0; i < gPatientOwnDrug.length; i++) {
        if (gPatientOwnDrug[i].sNO == oData.sNO) {
            gPatientOwnDrug[i].PatientOwnDrugID = oData.PatientOwnDrugID;
            var oDrug = new Object();
            oDrug.DrugID = oData.Drug.DrugID;
            oDrug.DrugName = oData.Drug.DrugName;
            gPatientOwnDrug[i].Drug = oDrug;

            gPatientOwnDrug[i].DosageID = oData.DosageID;
            gPatientOwnDrug[i].Dosage = oData.Dosage;
            gPatientOwnDrug[i].Frequency = oData.Frequency;
            gPatientOwnDrug[i].FrequencyID = oData.FrequencyID;
            gPatientOwnDrug[i].OtherFrequency = oData.OtherFrequency;
            gPatientOwnDrug[i].DurationID = oData.DurationID;
            gPatientOwnDrug[i].Duration = oData.Duration;
            gPatientOwnDrug[i].Ingredient = oData.Ingredient;
            gPatientOwnDrug[i].InstructionType = oData.InstructionType;
            gPatientOwnDrug[i].Instruction = oData.Instruction;
            gPatientOwnDrug[i].StatusFlag = oData.StatusFlag;
        }
    }
    DisplayPatientPrescriptionList(gPatientOwnDrug);
    $("#btnAddPatientPrescription").show();
    $("#btnUpdatePatientPrescription").hide();
    ClearPatientPrescriptionFields();
    $("#ddlPatientDrugName").focus();
    return false;
}
function Delete_PatientPrescriptionDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gPatientOwnDrug.length; i++) {
            if (gPatientOwnDrug[i].SNo == ID) {
                var index = jQuery.inArray(gPatientOwnDrug[i].valueOf("SNo"), gPatientOwnDrug);
                if (gPatientOwnDrug[i].SNo > 0) {
                    gPatientOwnDrug[i].StatusFlag = "D";
                } else {
                    gPatientOwnDrug.splice(index, 1);
                }
                $("#divPatientPrescriptionList").empty();
                DisplayPatientPrescriptionList(gPatientOwnDrug);
            }
        }
    }
    return false;
}
//==========Patient Own Drug - END======

$("#aTab82").click(function ()
{ GetDrugList("ddlPatientDrugName"); });

//Added on 01-09-2017
$("#ddlPatientDrugName").change(function () {
    var iDrugID = $("#ddlPatientDrugName").val();
    if (iDrugID != undefined && iDrugID > 0) {
        GetPatientDrugByID(iDrugID);
    }
    else {
        $("#hdnPatientDosageID").val("");
        $("#hdnPatientDurationID").val("");
        $("#txtPatientDosage").val("");
        $("#txtPatientDuration").val("");
        $("#txtPatientIngredient").val("");
    }
});
function GetPatientDrugByID(id) {
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
                            $("#hdnPatientDosageID").val(obj.Dosage.DosageID);
                            $("#txtPatientDosage").val(obj.Dosage.DosageName);
                            $("#ddlPatientFrequency").val(obj.FrequencyID);
                            $("#hdnPatientDurationID").val(obj.Duration.DurationID);
                            $("#txtPatientDuration").val(obj.Duration.DurationName);
                            $("#ddlPatientInstruction").val(obj.InstructionID);
                            $("#txtPatientIngredient").val(obj.Ingredient);
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
$("#ddlPatientFrequency").change(function () {
    var iFrequencyID = $("#ddlPatientFrequency").val();
    if (iFrequencyID != undefined && iFrequencyID == 9)
        $("#divPatientOtherFrqeuency").show();
    else
        $("#divPatientOtherFrqeuency").hide();
});

function GenerateLocalExamination(ID, Type) {
    var sLocalExamination = "";
    if (Type == "HIP") {
        if (ID == 1)
            sLocalExamination = "LEFT HIP";
        else if (ID == 2)
            sLocalExamination = "RIGHT HIP";
        else if (ID == 3)
            sLocalExamination = "BOTH HIP";
    }
    if (Type == "KNEE") {
        if (ID == 1)
            sLocalExamination = "LEFT LEG";
        else if (ID == 2)
            sLocalExamination = "RIGHT LEG";
        else if (ID == 3)
            sLocalExamination = "BOTH LEG";
    }
    sLocalExamination += "\nSWELLING+\nTENDERNESS+\nREDNESS+";

    $("#txtLocalExamination").val(sLocalExamination);
    return false;
}

//Added on 09-09-2017
function PrintDischargeReport() {
    SetSessionValue("AdmissionID", $("#hdnAdmissionID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewDischargeSummary.aspx", "_blank");
    return false;
}
function PrintPrescriptionDetails() {
    SetSessionValue("AdmissionID", $("#hdnAdmissionID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewPrescriptionDetails.aspx", "_blank");
    return false;
}
$("#btnSavePrint,#btnUpdatePrint").click(function () {
    $("#btnSave").click();

    if ($("#hdnAdmissionID").val() != null && $("#hdnAdmissionID").val() > 0 && $("#hdnAdmissionID").val() != undefined)
        PrintDischargeReport();

    return false;
});

//Added on 12-09-2017
$("#rdbExistingDrugName,#rdbNewDrugName").click(function () {
    if (this.id == "rdbNewDrugName") {
        $("#divSelectDrugName").hide();
        $("#divNewDrugName").show();
        $("#txtNewDrugName").val("");
        $("#txtNewDrugName").focus();
    }
    else if (this.id == "rdbExistingDrugName") {
        $("#divSelectDrugName").show();
        $("#divNewDrugName").hide();
    }
});
$("#rdbExistingPatientDrugName,#rdbNewPatientDrugName").click(function () {
    if (this.id == "rdbNewPatientDrugName") {
        $("#divSelectPatientDrugName").hide();
        $("#divNewPatientDrugName").show();
        $("#txtNewPatientDrugName").val("");
        $("#txtNewPatientDrugName").focus();
    }
    else if (this.id == "rdbExistingPatientDrugName") {
        $("#divSelectPatientDrugName").show();
        $("#divNewPatientDrugName").hide();
    }
});

//Added on 15-09-2017
$("#divClearAdmissionDate").click(function ()
{ $("#txtAdmissionDate").val(""); });
$("#divClearAdmissionTime").click(function ()
{ $("#txtAdmissionTime").val(""); });
$("#divClearSurgeryDate").click(function ()
{ $("#txtSurgeryDate").val(""); });
$("#divClearDischargeDate").click(function ()
{ $("#txtDischargeDate").val(""); });
$("#divClearDischargeTime").click(function ()
{ $("#txtDischargeTime").val(""); });

//Added on 22-09-2017
$("#divClearReviewAppointmentDate").click(function ()
{ $("#txtReviewAppointmentDate").val(""); });
$("#divClearReviewAppointmentTime").click(function ()
{ $("#txtReviewAppointmentTime").val(""); });

//Added on 25-10-2017
$("#btnChangeCase").click(function () {
    var upperCase = new RegExp('[A-Z]');

    if (inFocus != false) {
        var sel = $("#" + inFocus).getSelection();
        var oSelectedText = sel.text;
        if (oSelectedText.match(upperCase))
            oSelectedText = oSelectedText.toLowerCase();
        else
            oSelectedText = oSelectedText.toUpperCase();

        $("#" + inFocus).replaceSelectedText(oSelectedText, 'select');
    }
    return false;
});

$('textarea').focus(function () {
    inFocus = $(this).attr('id');
});