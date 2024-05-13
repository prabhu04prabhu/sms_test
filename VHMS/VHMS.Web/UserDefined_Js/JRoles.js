var gRoleConfiguration = [];

$(function () {
    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    // $("#btnAddNew").remove();
    if (ActionAdd != "1") {
        $("#btnAddNew").remove();
        $("#btnSave").remove();
    }

    if (ActionUpdate != "1") {
        $("#btnUpdate").remove();
    }

    pLoadingSetup(false);

    $("#btnUpdate").hide();
    $("#btnSave").show();

    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnList").hide();
    $("#btnAddNew").show();

    $("#btnAddModule").show();
    $("#btnUpdateModule").hide();
    $("#divViewDamage").hide();
    GetMenuList();
    GetRecord();
    pLoadingSetup(true);
});

$("#btnAddNew").click(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();
    $("#divRecords").hide();
    $("#tab-modal").show();
    $("#btnList").show();
    $("#btnAddNew").hide();
    $("#hdnRoleID").val("");
    $("#divClone").hide();
    ClearFields();
    $("#aGeneral").click();

    $("#divViewDamage").hide();
    return false;
});
$("#btnList").click(function () {
    $("#btnUpdate").hide();
    $("#btnSave").show();
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnList").hide();
    $("#btnAddNew").show();
    $("#hdnRoleID").val("");

    $("#divViewDamage").hide();
    GetRecord();

    return false;
});

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRole",
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
                            $("#ddlRole").empty();
                            var TypeStatus = "";
                            for (var index = 0; index < obj.length; index++) {

                                $("#ddlRole").append('<option value=' + obj[index].RoleID + ' >' + obj[index].RoleName + '</option>');
                                $("#ddlRole").val(0).change();
                                if (obj[index].IsActive == "1")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                var table = "<tr id='" + obj[index].RoleID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].RoleName + "</td>";
                                table += "<td>" + obj[index].Description + "</td>";
                                table += "<td>" + TypeStatus + "</td>";

                                if (ActionUpdate == "1")
                                { table += "<td><a href='#' id=" + obj[index].RoleID + " class=\"Edit\"><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                //if (ActionDelete == "1")
                                //{ table += "<td><a href='#' id=" + obj[index].RoleID + " class=\"Delete\"><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                //else
                                //{ table += "<td></td>"; }

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
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
                            { "sWidth": "50%" },
                            { "sWidth": "35%" },
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

$("#btnCopy").click(function () {
    if (confirm('Previous Permissions will be replaced by selected role privileges. Are you sure want to continue?')) {
        ClearPrivilegeFields();
        DeletePermissionByRoleID($('#hdnRoleID').val());
        GetRoleConfigurationClone($("#ddlRole").val());
    }
});

function GetRoleConfigurationClone(ID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRoleConfiguration",
        data: JSON.stringify({ iRoleID: ID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            gRoleConfiguration = [];
                            for (var index = 0; index < obj.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.RoleID = $('#hdnRoleID').val();
                                objTemp.RoleConfigurationID = 0;
                                objTemp.MenuID = obj[index].MenuID;
                                objTemp.MenuName = obj[index].MenuName;
                                objTemp.ModuleID = obj[index].ModuleID;
                                objTemp.ModuleName = obj[index].ModuleName;
                                objTemp.IsAccess = obj[index].IsAccess;
                                objTemp.IsView = obj[index].IsView;
                                objTemp.IsAdd = obj[index].IsAdd;
                                objTemp.IsEdit = obj[index].IsEdit;
                                objTemp.IsDelete = obj[index].IsDelete;

                                objTemp.StatusFlag = "I";
                                gRoleConfiguration.push(objTemp);
                            }
                            DisplayPrivilegeDataTable(gRoleConfiguration);
                            $("#btnSaveRoleConfiguration").click();

                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#divModules").empty();
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
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function DeletePermissionByRoleID(ID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeletePermissionByRoleID",
        data: JSON.stringify({ iRoleID: ID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    dProgress(false);
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
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
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

    if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
        $.jGrowl("Please enter Role Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divName").addClass('has-error'); return false;
    } else { $("#divName").removeClass('has-error'); }

    var Obj = new Object();
    Obj.RoleID = 0;
    Obj.RoleName = $("#txtName").val().trim();
    Obj.Description = $("#txtDescription").val();
    Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
    Obj.MarginView = $("#chkMarginView").is(':checked') ? "1" : "0";
    Obj.CommonMargin = $("#chkCommonMargin").is(':checked') ? "1" : "0";

    var sMethodName;
    if ($("#hdnRoleID").val() > 0) {
        Obj.RoleID = $("#hdnRoleID").val();
        sMethodName = "UpdateRole";
    }
    else { sMethodName = "AddRole"; }

    SaveandUpdateRole(Obj, sMethodName);

    return false;
});
function ClearFields() {
    $("#txtName").val("");
    $("#txtDescription").val("");
    $("#chkStatus").prop("checked", true);

    $("#divName").removeClass('has-error');
    return false;
}
function SaveandUpdateRole(Obj, sMethodName) {
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
                        if (sMethodName == "AddRole")
                        { $("#hdnRoleID").val(objResponse.Value); $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                        else if (sMethodName == "UpdateRole")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        EditRecord($("#hdnRoleID").val());
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Role_A_01" || objResponse.Value == "Role_U_01") {
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

function GetRoleByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRoleByID",
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
                            $("#lblRoleName").text("Role : " + obj.RoleName);
                            if (obj.RoleConfig_Count > 0)
                                $("#btnCopy").prop('disabled', true);
                            else
                                $("#btnCopy").prop('disabled', false);

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

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRoleByID",
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
                            $("#divRecords").hide();
                            $("#tab-modal").show();
                            $("#btnList").show();
                            $("#btnAddNew").hide();
                            $("#hdnRoleID").val("");
                            ClearFields();
                            $("#divClone").show();
                            $("#aGeneral").click();

                            if (obj.RoleConfig_Count > 0)
                                $("#btnCopy").prop('disabled', true);
                            else
                                $("#btnCopy").prop('disabled', false);

                            $("#hdnRoleID").val(obj.RoleID);
                            $("#txtName").val(obj.RoleName);
                            $("#txtDescription").val(obj.Description);
                            $("#chkCommonMargin").prop("checked", obj.CommonMargin ? true : false);
                            $("#chkMarginView").prop("checked", obj.MarginView ? true : false);
                            $("#chkStatus").prop("checked", obj.IsActive ? true : false);
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
        url: "WebServices/VHMSService.svc/DeleteRole",
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
                        $.jGrowl("Record Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Role_R_01" || objResponse.Value == "Role_D_01") {
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



$("#aGeneral").click(function () {
    $("#divViewDamage").hide();
});


$("#aRoleResources").click(function () {
    if ($("#hdnRoleID").val() == "" || $("#hdnRoleID").val() == undefined || $("#hdnRoleID").val() == "0") {
        return false;
    }
    ClearPrivilegeFields();
    gRoleConfiguration = [];
    GetRoleConfiguration($("#hdnRoleID").val());
    DisplayPrivilegeDataTable(gRoleConfiguration);
    GetRoleByID($("#hdnRoleID").val());
    $("#divViewDamage").show();
});

function GetMenuList() {
    dProgress(true);
    $("#ddlMenu").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetMenu",
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
                            //$("#ddlMenu").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $("#ddlMenu").append('<option value=' + obj[index].PK_MenuID + ' >' + obj[index].MenuName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlMenu").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlMenu").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

$("#ddlMenu").change(function () {
    var iMenuID = $("#ddlMenu").val();
    if (iMenuID != undefined && iMenuID > 0) {
        GetModuleList(iMenuID);
        $("#ddlModule").val(null).change();
    }
    else {
        $("#ddlModule").empty();
        //$("#ddlModule").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
    }
    return false;
});

function GetModuleList(MenuID) {
    dProgress(true);
    $("#ddlModule").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetModule",
        data: JSON.stringify({ iMenuID: MenuID }),
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
                            //$("#ddlModule").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $("#ddlModule").append('<option value=' + obj[index].PK_ModuleID + ' >' + obj[index].ModuleName + '</option>');
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlModule").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlModule").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

$("#btnAddModule,#btnUpdateModule").click(function () {
    if ($("#ddlMenu").val() == "0" || $("#ddlMenu").val() == undefined || $("#ddlMenu").val() == null) {
        $.jGrowl("Please select Menu", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divMenu").addClass('has-error'); $("#ddlMenu").focus(); return false;
    } else { $("#divMenu").removeClass('has-error'); }

    if ($("#ddlModule").val() == "0" || $("#ddlModule").val() == undefined || $("#ddlModule").val() == null) {
        $.jGrowl("Please select Module", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divModule").addClass('has-error'); $("#ddlModule").focus(); return false;
    } else { $("#divModule").removeClass('has-error'); }

    if (this.id == "btnAddModule") {
        var scount = 0;

        for (var i = 0; i < gRoleConfiguration.length; i++) {
            if (gRoleConfiguration[i].StatusFlag != 'D') {
                if (gRoleConfiguration[i].ModuleID == $("#ddlModule").val())
                    scount = scount + 1;
            }
        }

        if (scount > 0) {
            $.jGrowl(" Module/Screen Already Added", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#ddlModule").focus(); return false;
        }
    }

    var ObjData = new Object();
    ObjData.RoleID = $("#hdnRoleID").val();
    ObjData.MenuID = $("#ddlMenu").val();
    ObjData.MenuName = $("#ddlMenu option:selected").text();
    ObjData.ModuleID = $("#ddlModule").val();
    ObjData.ModuleName = $("#ddlModule option:selected").text();
    ObjData.IsAccess = $("#chkAccess").is(':checked') ? true : false;
    ObjData.IsView = $("#chkView").is(':checked') ? true : false;
    ObjData.IsAdd = $("#chkAdd").is(':checked') ? true : false;
    ObjData.IsEdit = $("#chkEdit").is(':checked') ? true : false;
    ObjData.IsDelete = $("#chkDelete").is(':checked') ? true : false;

    if (this.id == "btnAddModule") {
        ObjData.sNO = gRoleConfiguration.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.StatusFlag = "I";
        ObjData.RoleConfigurationID = 0;
        Add_Privilege(ObjData);
    }
    else if (this.id == "btnUpdateModule") {
        ObjData.sNO = $("#hdnSNo").val();

        if ($("#hdnRoleConfigurationID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.RoleConfigurationID = $("#hdnRoleConfigurationID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.RoleConfigurationID = 0;
        }

        Update_Privilege(ObjData);
    }
    ClearPrivilegeFields();
    $("#ddlMenu").focus();
});

function ClearPrivilegeFields() {
    $("#ddlMenu").val(null).change();
    $("#chkAccess").prop("checked", false);
    $("#chkView").prop("checked", false);
    $("#chkAdd").prop("checked", false);
    $("#chkEdit").prop("checked", false);
    $("#chkDelete").prop("checked", false);

    $("#btnAddModule").show();
    $("#btnUpdateModule").hide();

    $("#ddlModule").empty();
    $("#ddlModule").val(null).change();
    $("#ddlModule").val('0');
    $("#hdnSNo").val("");
    $("#hdnRoleConfigurationID").val("");

    $("#divMenu").removeClass('has-error');
    $("#divModule").removeClass('has-error');
    return false;
}

function Add_Privilege(oData) {
    gRoleConfiguration.push(oData);
    DisplayPrivilegeDataTable(gRoleConfiguration);
    return false;
}

function DisplayPrivilegeDataTable(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-aqua-active color-palette";

    if (gData.length >= 10)
    { $("#divModules").css({ 'height': '0px', 'min-height': '350px', 'overflow': 'auto' }); }
    else
    { $("#divModules").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblRecordPrivilege' class='table table-condensed table-hover'>";
        sTable += "<thead><tr class='" + sColorCode + "'><th>S.No</th>";
        sTable += "<th>Menu</th>";
        sTable += "<th>Module</th>";
        sTable += "<th style='text-align:center;'>Access</th>";
        sTable += "<th style='text-align:center;'>Add</th>";
        sTable += "<th style='text-align:center;'>Edit</th>";
        sTable += "<th style='text-align:center;'>Delete</th>";
        sTable += "<th style='text-align:center;'>View</th>";
        sTable += "<th style='width:5%;text-align:center;'>Edit</th>";
        sTable += "<th style='width:5%;text-align:center;'>Delete</th></tr></thead><tbody>";
        var sIsAccess = '', sIsView = '', sIsAdd = '', sIsEdit = '', sIsDelete = '';
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable += "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].MenuName + "</td>";
                sTable += "<td>" + gData[i].ModuleName + "</td>";

                sIsAccess = gData[i].IsAccess ? "fa fa-check-square text-green" : "fa fa-times text-red";
                sIsView = gData[i].IsView ? "fa fa-check-square text-green" : "fa fa-times text-red";
                sIsAdd = gData[i].IsAdd ? "fa fa-check-square text-green" : "fa fa-times text-red";
                sIsEdit = gData[i].IsEdit ? "fa fa-check-square text-green" : "fa fa-times text-red";
                sIsDelete = gData[i].IsDelete ? "fa fa-check-square text-green" : "fa fa-times text-red";

                sTable += "<td style='text-align:center;'><i class='" + sIsAccess + "'></i></td>";
                sTable += "<td style='text-align:center;'><i class='" + sIsAdd + "'></i></td>";
                sTable += "<td style='text-align:center;'><i class='" + sIsEdit + "'></i></td>";
                sTable += "<td style='text-align:center;'><i class='" + sIsDelete + "'></i></td>";
                sTable += "<td style='text-align:center;'><i class='" + sIsView + "'></i></td>";
                sTable += "<td style='width:5%;text-align:center;'><a href='#' id=" + gData[i].sNO + " onclick = 'Edit_PrivilegeDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td style='width:5%;text-align:center;'><a href='#' id=" + gData[i].sNO + " onclick = 'Delete_PrivilegeDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
            }
        }
        sTable += ('</tbody></table>');

        $("#divModules").empty();
        $("#divModules").html(sTable);
        //$("#tblRecordPrivilege").dataTable({ pagination: false });
        $("#tblRecordPrivilege").dataTable({ "bPaginate": false, });
    }
    else { $("#divModules").empty(); }

    return false;
}

function Edit_PrivilegeDetail(ID) {
    Bind_PrivilegByID(ID, gRoleConfiguration);
    return false;
}

function Bind_PrivilegByID(ID, data) {
    $("#btnAddModule").hide();
    $("#btnUpdateModule").show();
    $("#ddlMenu").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            $("#hdnSNo").val(ID);
            $("#hdnRoleConfigurationID").val(data[i].RoleConfigurationID);
            $("#ddlMenu").val(data[i].MenuID).change();
            GetModuleList(data[i].MenuID);
            $("#ddlModule").val(data[i].ModuleID).change();
            $("#chkAccess").prop("checked", data[i].IsAccess);
            $("#chkView").prop("checked", data[i].IsView);
            $("#chkAdd").prop("checked", data[i].IsAdd);
            $("#chkEdit").prop("checked", data[i].IsEdit);
            $("#chkDelete").prop("checked", data[i].IsDelete);
        }
    }
    return false;
}

function Update_Privilege(oData) {
    for (var i = 0; i < gRoleConfiguration.length; i++) {
        if (gRoleConfiguration[i].sNO == oData.sNO) {
            gRoleConfiguration[i].RoleID = oData.RoleID;
            gRoleConfiguration[i].RoleConfigurationID = oData.RoleConfigurationID;
            gRoleConfiguration[i].MenuID = oData.MenuID;
            gRoleConfiguration[i].MenuName = oData.MenuName;
            gRoleConfiguration[i].ModuleID = oData.ModuleID;
            gRoleConfiguration[i].ModuleName = oData.ModuleName;
            gRoleConfiguration[i].IsAccess = oData.IsAccess;
            gRoleConfiguration[i].IsView = oData.IsView;
            gRoleConfiguration[i].IsAdd = oData.IsAdd;
            gRoleConfiguration[i].IsEdit = oData.IsEdit;
            gRoleConfiguration[i].IsDelete = oData.IsDelete;
            gRoleConfiguration[i].StatusFlag = oData.StatusFlag;
        }
    }

    DisplayPrivilegeDataTable(gRoleConfiguration);
    $("#btnAddModule").show();
    $("#btnUpdateModule").hide();
    ClearPrivilegeFields();
    $("#ddlMenu").focus();
    return false;
}

function Delete_PrivilegeDetail(ID) {
    $("#btnAddModule").show();
    $("#btnUpdateModule").hide();

    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gRoleConfiguration.length; i++) {
            if (gRoleConfiguration[i].sNO == ID) {
                var index = jQuery.inArray(gRoleConfiguration[i].valueOf("sNO"), gRoleConfiguration);
                if (gRoleConfiguration[i].RoleConfigurationID > 0) {
                    gRoleConfiguration[i].StatusFlag = "D";
                } else {
                    gRoleConfiguration.splice(index, 1);
                }
                DisplayPrivilegeDataTable(gRoleConfiguration);
            }
        }
    }
    ClearPrivilegeFields();
    $("#ddlMenu").focus();
    return false;
}

$("#btnSaveRoleConfiguration").click(function () {
    SaveandUpdateRoleConfigration(gRoleConfiguration);
    return false;
});

function SaveandUpdateRoleConfigration(gRoleConfiguration) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SaveRoleConfiguration",
        data: JSON.stringify({ objList: gRoleConfiguration }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                        GetRoleConfiguration($("#hdnRoleID").val());
                        DisplayPrivilegeDataTable(gRoleConfiguration);
                        GetRoleByID($("#hdnRoleID").val());
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
}

function GetRoleConfiguration(ID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRoleConfiguration",
        data: JSON.stringify({ iRoleID: ID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            gRoleConfiguration = [];
                            for (var index = 0; index < obj.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.RoleID = obj[index].RoleID;
                                objTemp.RoleConfigurationID = obj[index].RoleConfigurationID;
                                objTemp.MenuID = obj[index].MenuID;
                                objTemp.MenuName = obj[index].MenuName;
                                objTemp.ModuleID = obj[index].ModuleID;
                                objTemp.ModuleName = obj[index].ModuleName;
                                objTemp.IsAccess = obj[index].IsAccess;
                                objTemp.IsView = obj[index].IsView;
                                objTemp.IsAdd = obj[index].IsAdd;
                                objTemp.IsEdit = obj[index].IsEdit;
                                objTemp.IsDelete = obj[index].IsDelete;

                                objTemp.StatusFlag = "";
                                Add_Privilege(objTemp);
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#divModules").empty();
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
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}