$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#divRecords").show();  

    if (ActionAdd != "1") {
        $("#btnAddNew").remove();
        $("#btnSave").remove();
    }
    $("#SearchResult").hide();
    if (ActionUpdate != "1") {
        $("#btnUpdate").remove();
    }
    pLoadingSetup(true);
    GetRecord();
});

$("#btnAddNew").click(function () {
    SetSessionValue("PatientID", "");
    var myWindow = window.open("frmPatient.aspx", "_self");
    return false;
    return false;
});
function ClearFields() {
    $("#divTitle").html("<h3>Patient</h3>");
    return false;
}
$("#btnList").click(function () {
    ClearFields();
    $("#hdnPatientID").val("");
    $("#btnAddNew").show();

    $("#divRecords").show();
    $("#tabmodal").hide();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    GetRecord();
    return false;
});

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSearchpatient",
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
                                if (obj[index].IsActive == "1")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }
                                $("#LastOPDNo").val(obj[index].LastOPDNo);
                                var table = "<tr id='" + obj[index].patientID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Place + "</td>";
                                table += "<td>" + obj[index].OPDNo + "</td>";
                                table += "<td>" + obj[index].HName + "</td>";
                                table += "<td>" + obj[index].WName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].HMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].WMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Print' title='Click here to Print'><i class='fa fa-lg fa-print text-green'/></a></td>";
                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblSearchResult_tbody").append(table);
                            }
                            $(".Edit").click(function () {
                                if (ActionUpdate == "1") {
                                    var AdmissionID = $(this).attr('id');
                                    $("#hdnPatientID").val(AdmissionID);
                                    EditRecord(AdmissionID);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Print").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnPatientID").val(AdmissionID);
                                SetSessionValue("PatientID", AdmissionID);
                                var myWindow = window.open("PrintPatient.aspx", "MsgWindow");

                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to delete the selected record ?')) {
                                        var AdmissionID = $(this).attr('id');
                                        $("#hdnPatientID").val(AdmissionID);
                                        DeleteRecord(AdmissionID);
                                    }
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
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
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


function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPatientID",
        data: JSON.stringify({ iPatientID: 0 }),
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
                                $("#LastOPDNo").val(obj[index].LastOPDNo);
                                var table = "<tr id='" + obj[index].patientID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Place + "</td>";
                                table += "<td>" + obj[index].OPDNo+ "</td>";
                                table += "<td>" + obj[index].HName + "</td>";
                                table += "<td>" + obj[index].WName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].HMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].WMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                //if (ActionView == "1")
                                //{ table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                //else
                                //{ table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Print' title='Click here to Print'><i class='fa fa-lg fa-print text-green'/></a></td>";
                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            //$(".View").click(function () {
                            //    if (ActionView == "1") {
                            //        var AdmissionID = $(this).attr('id');
                            //        $("#hdnPatientID").val(AdmissionID);
                            //        EditRecord(AdmissionID);
                            //       // $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Patient");
                            //        //$("#btnSave").hide();
                            //       // $("#btnUpdate").hide();
                            //    }
                            //    else {
                            //        $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                            //        return false;
                            //    }
                            //});
                            $(".Edit").click(function () {
                                if (ActionUpdate == "1")
                                {
                                    var AdmissionID = $(this).attr('id');
                                    $("#hdnPatientID").val(AdmissionID);
                                    EditRecord(AdmissionID);
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Print").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnPatientID").val(AdmissionID);
                                SetSessionValue("PatientID", AdmissionID);
                                var myWindow = window.open("PrintPatient.aspx", "MsgWindow");
                                
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to delete the selected record ?'))
                                    {
                                        var AdmissionID = $(this).attr('id');
                                        $("#hdnPatientID").val(AdmissionID);
                                        DeleteRecord(AdmissionID);
                                    }
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
                          //{ "sWidth": "15%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
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


function SaveandUpdatePatient(ObjPatient, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        enctype: 'multipart/form-data',
        data: JSON.stringify({ Objdata: ObjPatient }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {

                        SetSessionValue("PatientID", $("#hdnPatientID").val());
                        var myWindow = window.open("PrintPatient.aspx", "MsgWindow");

                        ClearFields();

                        if (sMethodName == "AddPatient") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPatientID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdatePatient")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

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
    SetSessionValue("PatientID", id);
    //var myWindow = window.open("frmPatient.aspx", "MsgWindow");
    var myWindow = window.open("frmPatient.aspx", "_self");
    return false;
}

$("#aGeneral").click(function () {
    $("#SearchResult").hide();
    GetRecord();
});

$("#aSearchResult").click(function () {
    $("#SearchResult").show();

});
function DeleteRecord(id) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeletePatient",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearFields();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Patient_R_01" || objResponse.Value == "Patient_D_01") {
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
