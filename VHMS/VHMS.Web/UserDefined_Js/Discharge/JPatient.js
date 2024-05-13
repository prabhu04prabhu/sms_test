$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
   
    $("#hdnHImage").val("");
    $("#hdnWPhoto").val("");
    $('#txtHReferredBy').hide();
    $('#txtWReferredBy').hide();
    $('#txtDocMobileNo').hide();
    $('#divWDOB1').hide();
    $('#divHDOB1').hide();
    $("#btnSave").show();
    $("#btnUpdate").hide();

    $(".decimal").inputmask("decimal", { digits: 2, radixPoint: "." });

    if (ActionAdd != "1") {
        $("#btnAddNew").remove();
        $("#btnSave").remove();
    }

    if (ActionUpdate != "1") {
        $("#btnUpdate").remove();
    }
    $("#txtHDOB,#txtWDOB").attr("data-link-format", "dd/MM/yyyy");

    $("#txtHDOB").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    $("#divPatient").show();
    $("#txtWDOB").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    pLoadingSetup(true);
    GetCountryList();
    //GetLastOPDNo();
    if ($("#hdnPatientID").val() == "") {

        //GetLastOPDNo();
        AddNewPatient();        
    }
    else
        EditRecord($("#hdnPatientID").val())
});

function GetLastOPDNo() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPatient",
        data: JSON.stringify({ iPatientID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            for (var index = 0; index < obj.length; index++) {                               
                                $("#LastOPDNo").val(obj[index].LastOPDNo);
                                
                            }
                            SetSessionValue("LastOPDNo", $("#LastOPDNo").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {                        
                        dProgress(false);
                    }
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

function AddNewPatient() {
    $("#divPatient").show();
    document.title = "Manage Patient";

    $("#hdnPatientID").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#txtName").focus();
    $(".divTitle").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Patient");
    ClearFields();
    
    //var test = $("#LastOPDNo").val();
    //test = (test.slice(3));
    //$("#LastOPDNo").val(dformat);
    SetSessionValue("LastOPDNo", $("#LastOPDNo").val());
    return false;
};
function ClearFields() {
    $("#divTitle").html("<h3>Patient</h3>");
    $("#txtHName").val("");
    $("#txtHAddress").val("");
    $("#txtHMobileNo").val("");
    $("#txtHEmail").val("");
    $('.IWProof').attr("src", "");
    $('.IWimage').attr("src", "");
    $('.IHProof').attr("src", "");
    $('.IHPhoto').attr("src", "");
    $("#txtHBloodGroup").val("");
    $("#txtHDOB").val("");
    $("#txtHReferredBy").val("");
    $("#txtHPincode").val("");
    $("#txtHCity").val("");
    $("#txtHCountry").val("");
    $("#txtHNationality").val("Indian");
    $("#txtWName").val("");
    $("#txtWAddress").val("");
    $("#txtWMobileNo").val("");
    $("#txtWEmail").val("");
    $("#txtWBloodGroup").val("");
    $("#txtWDOB").val("");
    $("#txtHAge").val("");
    $("#txtWAge").val("");
    $("#txtWReferredBy").val("");
    $("#txtDocMobileNo").val("");
    $("#txtWPincode").val("");
    $("#txtWCity").val("");
    $("#txtWCountry").val("");
    $("#txtWNationality").val("Indian");
    $("#chkStatus").prop("checked", true);
    $("#ddlWCountry")[0].selectedIndex = 0;
    $("#ddlHCountry")[0].selectedIndex = 0;
    $("#txtWProfession").val("");
    $("#txtHProfession").val("");
    return false;
}
$("#btnList").click(function () {
    ClearFields();
    $("#hdnPatientID").val("");
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#divRecords").show();
    $("#tabmodal").hide();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    GetRecord();
    return false;
});

function GetStateList() {
    dProgress(true);
    $("#ddlState").empty();
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
                            //$("#ddlState").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive) {
                                    $("#ddlState").append('<option value=' + obj[index].StateID + ' >' + obj[index].StateName + '</option>');
                                }
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
$("#ddlState").change(function () {
    var iStateID = $("#ddlState").val();
    if (iStateID != undefined && iStateID > 0) {
        GetDistrictList(iStateID);
        $("#ddlDistrict").val(null).change();
    }
    else {
        $("#ddlDistrict").empty();
        $("#ddlDistrict").val(null).change();
    }
    return false;
});



$("#chkCopyDetails").click(function () {
    if ($(this).prop("checked") == true) {
        $("#txtHReferredBy").val($("#txtWReferredBy").val());
        $("#ddlHRefer").val($("#ddlRefer").val());
        $("#txtHEmail").val($("#txtWEmail").val());
        $("#txtHAddress").val($("#txtWAddress").val());
        //$("#txtHMobileNo").val($("#txtWMobileNo").val());
        $("#txtHPincode").val($("#txtWPincode").val());
        $("#txtHNationality").val($("#txtWNationality").val());
        $("#txtHCountry").val($("#txtWCountry").val());
        $("#txtHCity").val($("#txtWCity").val());
    }

});

$("#ddlHRefer").change(function () {
    var iStateID = $("#ddlHRefer").val();
    if (iStateID != undefined && iStateID == 'Doctor' || iStateID == 'Others') {
        $('#txtHReferredBy').show();
    }
    else {
        $('#txtHReferredBy').hide();
    }
    return false;
});
$("#ddlRefer").change(function () {
    var iStateID = $("#ddlRefer").val();
    if (iStateID != undefined && iStateID == 'Doctor' || iStateID == 'Others') {
        $('#txtWReferredBy').show();
        if (iStateID == 'Doctor')
            $('#txtDocMobileNo').show();
        else
            $('#txtDocMobileNo').hide();

    }
    else {
        $('#txtWReferredBy').hide();
        $('#txtDocMobileNo').hide();
    }
    return false;
});
function GetCountryList() {
    dProgress(true);
    $("#ddlWCountry").empty();
    $("#ddlHCountry").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCountry",
       // data: JSON.stringify({ iStateID: ID }),
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
                                    $("#ddlWCountry").append('<option value=' + obj[index].CountryID + ' >' + obj[index].CountryName + '</option>');
                                    $("#ddlHCountry").append('<option value=' + obj[index].CountryID + ' >' + obj[index].CountryName + '</option>');
                                  
                                }
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlWCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $("#ddlHCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlWCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                $("#ddlHCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPatient",
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
                                var table = "<tr id='" + obj[index].PatientID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Place + "</td>";
                                table += "<td>" + obj[index].OPDNo + "</td>";
                                table += "<td>" + obj[index].HName + "</td>";
                                table += "<td>" + obj[index].WName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].HMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].WMobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PatientID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PatientID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].patientID + " class='Print' title='Click here to Print'><i class='fa fa-lg fa-print text-green'/></a></td>";
                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PatientID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].PatientID);
                                    $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Patient");
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
                                { EditRecord($(this).parent().parent()[0].PatientID); }
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
                                    { DeleteRecord($(this).parent().parent()[0].PatientID); }
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

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

$("#btnSave,#btnUpdate").click(function () {

    if ($("#ddlCategory").val() == "Fertility") {
        if ($("#txtHName").val().trim() == "" || $("#txtHName").val().trim() == undefined) {
            $.jGrowl("Please enter Husband Name", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHName").addClass('has-error'); $("#txtHName").focus(); return false;
        } else { $("#divHName").removeClass('has-error'); }
        if ($("#txtHMobileNo").val().trim() == "" || $("#txtHMobileNo").val().trim() == undefined) {
            $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHMobileNo").addClass('has-error'); $("#txtHMobileNo").focus(); return false;
        } else { $("#divHMobileNo").removeClass('has-error'); }
        if ($("#txtHAddress").val().trim() == "" || $("#txtHAddress").val().trim() == undefined) {
            $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHAddress").addClass('has-error'); $("#txtHAddress").focus(); return false;
        } else { $("#divHAddress").removeClass('has-error'); }       
        if ($("#txtHCity").val().trim() == "" || $("#txtHCity").val().trim() == undefined) {
            $.jGrowl("Please enter City", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHCity").addClass('has-error'); $("#txtHCity").focus(); return false;
        } else { $("#divHCity").removeClass('has-error'); }
        if ($("#txtHNationality").val().trim() == "" || $("#txtHNationality").val().trim() == undefined) {
            $.jGrowl("Please enter Nationality", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHNationality").addClass('has-error'); $("#txtHNationality").focus(); return false;
        } else { $("#divHNationality").removeClass('has-error'); }
        if ($("#ddlHRefer").val() == "0" || $("#ddlHRefer").val() == undefined || $("#ddlHRefer").val() == null) {
            $.jGrowl("Please select Reference", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHRefer").addClass('has-error'); $("#ddlHRefer").focus(); return false;
        } else { $("#divHRefer").removeClass('has-error'); }
        if ($("#txtHAge").val().trim == "0" || $("#txtHAge").val() == undefined || $("#txtHAge").val() == null) {
            $.jGrowl("Please Enter Age", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divHDOB").addClass('has-error'); $("#txtHAge").focus(); return false;
        } else { $("#divHDOB").removeClass('has-error'); }
        //if ($("#txtHDOB").val().trim() == "" || $("#txtHDOB").val().trim() == undefined) {
        //    $.jGrowl("Please enter DOB", { sticky: false, theme: 'warning', life: jGrowlLife });
        //    $("#divHDOB").addClass('has-error'); $("#txtHDOB").focus(); return false;
        //} else { $("#divHDOB").removeClass('has-error'); }
        //if ($("#txtHEmail").val().trim() != "" || $("#txtHEmail").val().trim() != undefined) {
        //    if (IsEmail($("#txtHEmail").val().trim()) == false) {
        //        $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
        //        $("#divHEmail").addClass('has-error'); $("#txtHEmail").focus(); return false;
        //    }
        //} else { $("#divHEmail").removeClass('has-error'); }

    }
    if ($("#txtWName").val().trim() == "" || $("#txtWName").val().trim() == undefined) {
        $.jGrowl("Please enter Wife Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWName").addClass('has-error'); $("#txtWName").focus(); return false;
    } else { $("#divWName").removeClass('has-error'); }
    if ($("#txtWMobileNo").val().trim() == "" || $("#txtWMobileNo").val().trim() == undefined) {
        $.jGrowl("Please enter Mobile No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWMobileNo").addClass('has-error'); $("#txtWMobileNo").focus(); return false;
    } else { $("#divWMobileNo").removeClass('has-error'); }
    if ($("#txtWAddress").val().trim() == "" || $("#txtWAddress").val().trim() == undefined) {
        $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWAddress").addClass('has-error'); $("#txtWAddress").focus(); return false;
    } else { $("#divWAddress").removeClass('has-error'); }   
    if ($("#txtWCity").val().trim() == "" || $("#txtWCity").val().trim() == undefined) {
        $.jGrowl("Please enter City", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWCity").addClass('has-error'); $("#txtWCity").focus(); return false;
    } else { $("#divWCity").removeClass('has-error'); }
    if ($("#txtWNationality").val().trim() == "" || $("#txtWNationality").val().trim() == undefined) {
        $.jGrowl("Please enter Nationality", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWNationality").addClass('has-error'); $("#txtWNationality").focus(); return false;
    } else { $("#divWNationality").removeClass('has-error'); }
    if ($("#ddlRefer").val() == "0" || $("#ddlRefer").val() == undefined || $("#ddlRefer").val() == null) {
        $.jGrowl("Please select Reference", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRefer").addClass('has-error'); $("#ddlRefer").focus(); return false;
    } else { $("#divRefer").removeClass('has-error'); }
    if ($("#txtWAge").val().trim == "0" || $("#txtWAge").val() == undefined || $("#txtWAge").val() == null) {
        $.jGrowl("Please Enter Age", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divWDOB").addClass('has-error'); $("#txtWAge").focus(); return false;
    } else { $("#divWDOB").removeClass('has-error'); }
    //if ($("#txtWDOB").val().trim() == "" || $("#txtWDOB").val().trim() == undefined) {
    //    $.jGrowl("Please enter DOB", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divWDOB").addClass('has-error'); $("#txtWDOB").focus(); return false;
    //} else { $("#divWDOB").removeClass('has-error'); }
    //var email = $("#txtWEmail").val();
    //if ($("#txtWEmail").val().trim() != "" || $("#txtWEmail").val().trim() != undefined) {
    //    if (IsEmail($("#txtWEmail").val().trim()) == false) {
    //        $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
    //        $("#divWEmail").addClass('has-error'); $("#txtWEmail").focus(); return false;
    //    }
    //} else { $("#divWEmail").removeClass('has-error'); }

    if ($("#txtHAge").val() == undefined || $("#txtHAge").val() == null || $("#txtHAge").val().length <=0) {
        $("#txtHAge").val("0");    }

    var ObjPatient = new Object();
    ObjPatient.PatientID = 0;
    ObjPatient.HName = $("#txtHName").val().trim();
    ObjPatient.WName = $("#txtWName").val().trim();
    ObjPatient.HBloodGroup = $("#txtHBloodGroup").val().trim();
    ObjPatient.WBloodGroup = $("#txtWBloodGroup").val().trim();
    ObjPatient.HReferredDetails = $("#txtHReferredBy").val().trim();
    ObjPatient.WReferredDetails = $("#txtWReferredBy").val().trim();
    ObjPatient.RefDoctorMobileNo = $("#txtDocMobileNo").val().trim();
    ObjPatient.HMobileNo = $("#txtHMobileNo").val().trim();
    ObjPatient.WMobileNo = $("#txtWMobileNo").val().trim();
    ObjPatient.HEmail = $("#txtHEmail").val().trim();
    ObjPatient.WEmail = $("#txtWEmail").val().trim();
    ObjPatient.HAddress = $("#txtHAddress").val().trim();
    ObjPatient.WAddress = $("#txtWAddress").val().trim();
    ObjPatient.HPincode = $("#txtHPincode").val().trim();
    ObjPatient.WPincode = $("#txtWPincode").val().trim();
    ObjPatient.HCity = $("#txtHCity").val().trim();
    ObjPatient.WCity = $("#txtWCity").val().trim();
    ObjPatient.HAge = $("#txtHAge").val().trim();
    ObjPatient.WAge = $("#txtWAge").val().trim();
    ObjPatient.HCountryID = $("#ddlHCountry").val().trim();
    ObjPatient.WCountryID = $("#ddlWCountry").val().trim();
    ObjPatient.HNationality = $("#txtHNationality").val().trim();
    ObjPatient.WNationality = $("#txtWNationality").val().trim();
    ObjPatient.sHDOB = $("#txtHDOB").val().trim();
    ObjPatient.sWDOB = $("#txtWDOB").val().trim();
    ObjPatient.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
    ObjPatient.HReferredBy = $("#ddlHRefer").val();
    ObjPatient.WReferredBy = $("#ddlRefer").val();
    ObjPatient.Category = $("#ddlCategory").val();
    ObjPatient.HImage = $("#hdnHImage").val();
    ObjPatient.WImage = $("#hdnWPhoto").val();
    ObjPatient.HProfession = $("#txtHProfession").val();
    ObjPatient.WProfession = $("#txtWProfession").val();

    if ($("#hdnHProof").val().length > 3)
        ObjPatient.HIDProof = "images/PatientPhotos/" + $("#hdnHProof").val();

    if ($("#hdnWProof").val().length > 3)
        ObjPatient.WIDProof = "images/PatientPhotos/" + $("#hdnWProof").val();

    //if ($("#hdnHImage").val().length > 3)
    //    ObjPatient.HImage = $("#hdnHImage").val();
    
    //if ($("#hdnWPhoto").val().length > 3)
    //    ObjPatient.WImage = $("#hdnWPhoto").val();

    ObjPatient.HImage = $("[id*=HimgCapture]").attr("src");    
    ObjPatient.WImage = $("[id*=WimgCapture]").attr("src");

    var sMethodName;
    if ($("#hdnPatientID").val() > 0) {
        ObjPatient.patientID = $("#hdnPatientID").val();

        sMethodName = "UpdatePatient";
    }
    else {
        sMethodName = "AddPatient";
    }
    //SubmitButtonOnclick();
    SaveandUpdatePatient(ObjPatient, sMethodName);
    $('#compose-modal').modal('hide');
    return false;
});

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
                        ClearFields();
                        if (sMethodName == "AddPatient") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnPatientID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdatePatient")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        SetSessionValue("PatientID", $("#hdnPatientID").val());
                        var myWindow = window.open("frmManagePatient.aspx", "_self");
                        var myWindow = window.open("PrintPatient.aspx", "MsgWindow");
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
        url: "WebServices/VHMSService.svc/GetPatientByID",
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

                            $("#btnSave").hide();
                            $("#btnUpdate").show();
                            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Edit Patient");
                            $("#hdnPatientID").val(obj.patientID);
                            $("#txtHName").val(obj.HName);
                            $("#txtWName").val(obj.WName);
                            $("#ddlCategory").val(obj.Category);

                            $("#txtWBloodGroup").val(obj.WBloodGroup);
                            $("#txtHBloodGroup").val(obj.HBloodGroup);
                            $("#txtHReferredBy").val(obj.HReferredDetails);
                            $("#txtWReferredBy").val(obj.WReferredDetails);
                            $("#txtDocMobileNo").val(obj.RefDoctorMobileNo);
                            $("#ddlRefer").val(obj.WReferredBy);
                            $("#ddlHRefer").val(obj.HReferredBy);
                            $("#txtHEmail").val(obj.HEmail);
                            $("#txtWEmail").val(obj.WEmail);
                            $("#txtHAddress").val(obj.HAddress);
                            $("#txtWAddress").val(obj.WAddress);
                            $("#txtHMobileNo").val(obj.HMobileNo);
                            $("#txtWMobileNo").val(obj.WMobileNo);
                            $("#txtHPincode").val(obj.HPincode);
                            $("#txtWPincode").val(obj.WPincode);
                            $("#txtHAge").val(obj.HAge);
                            $("#txtWAge").val(obj.WAge);
                            $("#txtHNationality").val(obj.HNationality);
                            $("#txtWNationality").val(obj.WNationality);
                            $("#ddlHCountry").val(obj.HCountryID).change();
                            $("#ddlWCountry").val(obj.WCountryID).change();
                            $("#txtHCity").val(obj.HCity);
                            $("#txtWCity").val(obj.WCity);
                            $("#txtHDOB").val(obj.sHDOB);
                            $("#txtWDOB").val(obj.sWDOB);
                            $("#txtHProfession").val(obj.HProfession);
                            $("#txtWProfession").val(obj.WProfession);
                            $('.IWProof').attr("src", obj.WIDProof);
                           // $('.IWimage').attr("src", obj.WImage);
                            $('.IHProof').attr("src", obj.HIDProof);
                           // $('.IHPhoto').attr("src", obj.HImage);
                            $("[id*=HimgCapture]").css("visibility", "visible");
                            $("[id*=HimgCapture]").attr("src", obj.HImage);
                            $("[id*=WimgCapture]").css("visibility", "visible");
                            $("[id*=WimgCapture]").attr("src", obj.WImage);
                            $("[id*=Wlink]").attr("href", obj.WIDProof);
                            $('[id*=Hlink]').attr("href", obj.HIDProof);
                            $("#chkStatus").prop("checked", obj.IsActive ? true : false);
                            //var test = obj.LastOPDNo;
                            //$("#LastOPDNo").val(obj.LastOPDNo);
                           // SetSessionValue("LastOPDNo", $("#LastOPDNo").val());
                            //$("#divTitle").html("<h3 class='text-blue'><i class='fa fa-user'></i>&nbsp;&nbsp;" + obj.PatientName + "&nbsp;&nbsp;<i class='fa fa-phone text-black'></i>&nbsp;&nbsp;" + obj.MobileNo + "</h3>");
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
//$("#btnClose").click(function () {
//    $('#compose-modal').modal('hide');
//    return false;
//});

$("#btnList,#btnClose").click(function () {
    var myWindow = window.open("frmManagePatient.aspx", "_self");
    SetSessionValue("PatientID", "");
});
