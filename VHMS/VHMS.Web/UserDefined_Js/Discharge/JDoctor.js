$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    $("#btnAddNew").show();
    $("#btnSave").show();

    $("#btnList").hide();
    $("#btnUpdate").hide();

    $("#divRecords").show();
    $("#divDoctor").hide();
    pLoadingSetup(true);

    GetSpecializationList();
    GetDoctorTypeList();
    GetCountryList();
    GetDepartmentList();

    GetRecord();
    FillDisplayOrder();
});

$("#btnAddNew").click(function () {
    $("#btnAddNew").hide();
    $("#btnSave").show();

    $("#btnList").show();
    $("#btnUpdate").hide();

    $("#divRecords").hide();
    $("#divDoctor").show();
    ClearFields();

    $("#secHeader").addClass('hidden');
    document.title = "Add New Doctor";
    return false;
});
$("#btnList,#btnClose").click(function () {
    $("#btnAddNew").show();
    $("#btnSave").show();

    $("#btnList").hide();
    $("#btnUpdate").hide();

    $("#divRecords").show();
    $("#divDoctor").hide();

    GetRecord();
    document.title = "Doctor";
    $("#secHeader").removeClass('hidden');
    return false;
});

function ClearFields() {
    $("#hdnDoctorID").val("");
    $("#txtDoctorName").val("");
    $("#txtQualification").val("");
    $("#txtAddress").val("");
    $("#txtCity").val("");
    $("#txtPincode").val("");
    $("#txtPhoneNo1").val("");
    $("#txtPhoneNo2").val("");
    $("#txtPhoneNo3").val("");
    $("#txtMobileNo").val("");
    $("#txtFaxNo").val("");
    $("#txtEmail").val("");
    $("#txtDoctorNo").val("");

    $("#ddlSpecialization").val(null).change();
    $("#ddlDoctorType").val(null).change();
    $("#ddlCountry").val("1").change();
    $("#ddlState").val("1").change();
    $("#ddlDepartment").val(null).change();

    $("#chkIsRMODoctor").prop("checked", false);
    $("#chkIsExternalDoctor").prop("checked", false);
    $("#chkStatus").prop("checked", true);

    $("#divDoctorName").removeClass('has-error');
    $("#divSpecialization").removeClass('has-error');
    $("#divDoctorType").removeClass('has-error');
    $("#divCountry").removeClass('has-error');
    $("#divState").removeClass('has-error');
    $("#divDepartment").removeClass('has-error');
    $("#divDoctorNo").removeClass('has-error');
    return false;
}
function GetSpecializationList() {
    dProgress(true);
    $("#ddlSpecialization").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSpecialization",
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
                                    $("#ddlSpecialization").append('<option value=' + obj[index].SpecializationID + ' >' + obj[index].SpecializationName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlSpecialization").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlSpecialization").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
function GetDoctorTypeList() {
    dProgress(true);
    $("#ddlDoctorType").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctorType",
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
                                    $("#ddlDoctorType").append('<option value=' + obj[index].DoctorTypeID + ' >' + obj[index].DoctorTypeName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlDoctorType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlDoctorType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
function GetCountryList() {
    dProgress(true);
    $("#ddlCountry").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCountry",
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
                                    $("#ddlCountry").append('<option value=' + obj[index].CountryID + ' >' + obj[index].CountryName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlCountry").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
$("#ddlCountry").change(function () {
    var iCountryID = $("#ddlCountry").val();
    if (iCountryID != undefined && iCountryID > 0) {
        GetStateList(iCountryID);
        $("#ddlState").val(null).change();
    }
    else {
        $("#ddlState").empty();
        $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
    }
    return false;
});
function GetStateList(ID) {
    dProgress(true);
    $("#ddlState").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetState",
        data: JSON.stringify({ CountryID: ID }),
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
                                    $("#ddlState").append('<option value=' + obj[index].StateID + ' >' + obj[index].StateName + '</option>');
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
function GetDepartmentList() {
    dProgress(true);
    $("#ddlDepartment").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDepartment",
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
                                    $("#ddlDepartment").append('<option value=' + obj[index].DepartmentID + ' >' + obj[index].DepartmentName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlDepartment").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlDepartment").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

//Added on 31-07-2017
function FillDisplayOrder() {
    $("#ddlDisplayOrder").append("<option value='0' selected='selected'>--Select--</option>");
    for (var index = 1; index <= 20; index++)
        $("#ddlDisplayOrder").append("<option value=" + index + ">" + index + "</option>");
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
    if (this.id == "btnSave") {
        if (ActionAdd != "1") {
            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
            return false;
        }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") {
            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
            return false;
        }
    }

    if ($("#txtDoctorName").val().trim() == "" || $("#txtDoctorName").val().trim() == undefined) {
        $.jGrowl("Please enter Doctor Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDoctorName").addClass('has-error'); $("#txtDoctorName").focus(); return false;
    } else { $("#divDoctorName").removeClass('has-error'); }

    if ($("#ddlDoctorType").val() == "0" || $("#ddlDoctorType").val() == undefined || $("#ddlDoctorType").val() == null) {
        $.jGrowl("Please select Doctor Type", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDoctorType").addClass('has-error'); $("#ddlDoctorType").focus(); return false;
    } else { $("#divDoctorType").removeClass('has-error'); }

    if ($("#ddlSpecialization").val() == "0" || $("#ddlSpecialization").val() == undefined || $("#ddlSpecialization").val() == null) {
        $.jGrowl("Please select Specialization", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSpecialization").addClass('has-error'); $("#ddlSpecialization").focus(); return false;
    } else { $("#divSpecialization").removeClass('has-error'); }

    if ($("#ddlCountry").val() == "0" || $("#ddlCountry").val() == undefined || $("#ddlCountry").val() == null) {
        $.jGrowl("Please select Country", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCountry").addClass('has-error'); $("#ddlCountry").focus(); return false;
    } else { $("#divCountry").removeClass('has-error'); }

    if ($("#ddlState").val() == "0" || $("#ddlState").val() == undefined || $("#ddlState").val() == null) {
        $.jGrowl("Please select State", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divState").addClass('has-error'); $("#ddlState").focus(); return false;
    } else { $("#divState").removeClass('has-error'); }

    if ($("#ddlDepartment").val() == "0" || $("#ddlDepartment").val() == undefined || $("#ddlDepartment").val() == null) {
        $.jGrowl("Please select Department", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDepartment").addClass('has-error'); $("#ddlDepartment").focus(); return false;
    } else { $("#divDepartment").removeClass('has-error'); }

    if ($("#txtDoctorNo").val().trim() == "" || $("#txtDoctorNo").val().trim() == undefined) {
        $.jGrowl("Please enter DOH/MOH No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDoctorNo").addClass('has-error'); $("#txtDoctorNo").focus(); return false;
    } else { $("#divDoctorNo").removeClass('has-error'); }

    if ($("#ddlDisplayOrder").val() == "0" || $("#ddlDisplayOrder").val() == undefined || $("#ddlDisplayOrder").val() == null) {
        $.jGrowl("Please select Display Order", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDisplayOrder").addClass('has-error'); $("#ddlDisplayOrder").focus(); return false;
    } else { $("#divDisplayOrder").removeClass('has-error'); }

    if ($("#txtEmail").val().trim() != "" && $("#txtEmail").val().trim() != undefined) {
        if (IsEmail($("#txtEmail").val().trim()) == false) {
            $.jGrowl("Please enter Valid Email", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtEmail").focus(); return false;
        }
    } 

    var objDoctor = new Object();
    objDoctor.DoctorID = 0;
    objDoctor.DoctorName = $("#txtDoctorName").val().trim();
    objDoctor.IsRMODoctor = $("#chkIsRMODoctor").is(':checked') ? "1" : "0";
    objDoctor.IsExternalDoctor = $("#chkIsExternalDoctor").is(':checked') ? "1" : "0";
    objDoctor.Qualification = $("#txtQualification").val().trim();
    objDoctor.Address = $("#txtAddress").val().trim();
    objDoctor.City = $("#txtCity").val().trim();
    objDoctor.Pincode = $("#txtPincode").val().trim();
    objDoctor.PhoneNo1 = $("#txtPhoneNo1").val().trim();
    objDoctor.PhoneNo2 = $("#txtPhoneNo2").val().trim();
    objDoctor.PhoneNo3 = $("#txtPhoneNo3").val().trim();
    objDoctor.MobileNo = $("#txtMobileNo").val().trim();
    objDoctor.FaxNo = $("#txtFaxNo").val().trim();
    objDoctor.Email = $("#txtEmail").val().trim();
    objDoctor.DoctorNo = $("#txtDoctorNo").val().trim();
    objDoctor.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";

    var objDoctorType = new Object();
    objDoctorType.DoctorTypeID = $("#ddlDoctorType").val();
    objDoctor.DoctorType = objDoctorType;

    var objSpecialization = new Object();
    objSpecialization.SpecializationID = $("#ddlSpecialization").val();
    objDoctor.Specialization = objSpecialization;

    var objState = new Object();
    objState.StateID = $("#ddlState").val();
    objDoctor.State = objState;

    var objDepartment = new Object();
    objDepartment.DepartmentID = $("#ddlDepartment").val();
    objDoctor.Department = objDepartment;

    //Added on 31-07-2017
    objDoctor.DisplayOrder = $("#ddlDisplayOrder").val();

    var sMethodName;
    if ($("#hdnDoctorID").val() > 0) {
        objDoctor.DoctorID = $("#hdnDoctorID").val();
        sMethodName = "UpdateDoctor";
    }
    else { sMethodName = "AddDoctor"; }

    SaveandUpdateDoctor(objDoctor, sMethodName);
    return false;
});
function SaveandUpdateDoctor(ObjDoctor, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjDoctor }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        if (sMethodName == "AddDoctor")
                        { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                        else if (sMethodName == "UpdateDoctor")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Country_A_01" || objResponse.Value == "Country_U_01") {
                        $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
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
                                table += "<td>" + obj[index].DoctorType.DoctorTypeName + "</td>";
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
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDoctorByID",
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

                            document.title = "Edit Doctor";

                            $("#hdnDoctorID").val(obj.DoctorID);
                            $("#txtDoctorName").val(obj.DoctorName);
                            $("#ddlDoctorType").val(obj.DoctorType.DoctorTypeID).change();
                            $("#ddlSpecialization").val(obj.Specialization.SpecializationID).change();
                            $("#txtQualification").val(obj.Qualification);
                            $("#txtAddress").val(obj.Address);
                            $("#ddlCountry").val(obj.State.Country.CountryID).change();
                            GetStateList(obj.State.Country.CountryID);
                            $("#ddlState").val(obj.State.StateID).change();
                            $("#txtCity").val(obj.City);
                            $("#txtPincode").val(obj.Pincode);
                            $("#txtPhoneNo1").val(obj.PhoneNo1);
                            $("#txtPhoneNo2").val(obj.PhoneNo2);
                            $("#txtPhoneNo3").val(obj.PhoneNo3);
                            $("#txtMobileNo").val(obj.MobileNo);
                            $("#txtFaxNo").val(obj.FaxNo);
                            $("#txtEmail").val(obj.Email);
                            $("#ddlDepartment").val(obj.Department.DepartmentID).change();
                            $("#txtDoctorNo").val(obj.DoctorNo);

                            $("#chkIsRMODoctor").prop("checked", obj.IsRMODoctor ? true : false);
                            $("#chkIsExternalDoctor").prop("checked", obj.IsExternalDoctor ? true : false);
                            $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                            $("#ddlDisplayOrder").val(obj.DisplayOrder);
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