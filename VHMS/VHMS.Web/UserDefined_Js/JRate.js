$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    
    $("#btnUpdate").show();

    $("#divDoctor").show();
    pLoadingSetup(true);
   
    GetRecord();
});


function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

$("#btnUpdate").click(function () {
        if (ActionAdd != "1") {
            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
            return false;

            if (confirm('Are you sure to Update?')) {

            }
            else {
                return false;
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

    SaveandUpdateDoctor(objDoctor, "AddRate");
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
                         $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); 
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Rate_A_01" || objResponse.Value == "Rate_U_01") {
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
        url: "WebServices/VHMSService.svc/GetCurrentRate",
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
