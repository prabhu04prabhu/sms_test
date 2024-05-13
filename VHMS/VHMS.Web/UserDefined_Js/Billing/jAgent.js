$(function () {
    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    var ClickCount = 0;

    pLoadingSetup(false);
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnAddNew").show();
    $("#btnList").hide();

    //General Tab
    $("#btnSave").show();
    $("#btnUpdate").hide();

    GetStateList();
    GetRecord();

    var _Tfunctionality;
    if ($.cookie("Agent") != undefined) {
        _Tfunctionality = $.cookie("Agent");

        if (_Tfunctionality == "Add New Agent") {
            pLoadingSetup(true);
            $("#btnAddNew").click();
        }
        $.cookie("Agent", null);
    }

    pLoadingSetup(true);


});

function GetStateList() {
    $("#ddlState").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetState",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CountryID: 0 }),
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            //$("#ddlState").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $("#ddlState").append('<option value=' + obj[index].StateID + ' >' + obj[index].StateName + '</option>');
                            }
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlState").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}
$("#btnAddNew").click(function () {
    $("#divRecords").hide();
    $("#tab-modal").show();
    $("#btnList").show();
    $("#btnAddNew").hide();
    $("#hdnAgentID").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#aGeneral").click();
    
    $('#divCommissionAmount').hide();
    ClearAgentTab();
    ClickCount = 0;
    return false;
});
$("#btnList,#btnClose").click(function () {
    $("#divRecords").show();
    $("#tab-modal").hide();
    $("#btnList").hide();
    $("#btnAddNew").show();
    $("#aGeneral").click();
    $.cookie("Agent", null);
    GetRecord();
    return false;
});

//$("#ddlCommissionType").change(function () {
//    if ($("#ddlCommissionType").val() == "Amount") {
//       // $("#txtCommissionPercentage").attr("visibility",true);
//        $('#divCommissionPercentage').show();
//        $('#divCommissionAmount').hide();
//        $("#txtCommissionAmount").val(0);
//    } else {
//        $('#divCommissionPercentage').hide();
//        $('#divCommissionAmount').show();
//        $("#txtCommissionPercentage").val(0);
//    }
//});

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetAgent",
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

                                var table = "<tr id='" + obj[index].AgentID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].AgentName + "</td>";
                                table += "<td>" + obj[index].AgentCode + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].PhoneNo1 + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].City + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].WhatsAppNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].EmailID + "</td>";
                                table += "<td class='hidden-xs'>" + TypeStatus + "</td>";
                                if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AgentID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else { table += "<td></td>"; }
                                if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AgentID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else { table += "<td></td>"; }

                                if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].AgentID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else { table += "<td></td>"; }
                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
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
                            { "sWidth": "20%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            // { "sWidth": "5%" },
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
function ClearAgentTab() {
    $("#txtName").val("");
    $("#txtCode").val("");
    $("#txtPhoneNo1").val("");
    $("#txtPhoneNo2").val("");
    $("#txtAddress").val("");
    $("#txtWhatsapp").val("");
    $("#txtEmail").val("");
    $("#ddlState").val(null).change();
    $("#txtCity").val("");
    $("#txtBankName").val("");
    $("#txtBranchName").val("");
    $("#txtIFSCCode").val("");
    $("#txtAccountNo").val("");
    $("#txtAccountHolderName").val("");
    $("#chkRateUpdate").prop("checked", false);
    $("#txtCommissionPercentage").val(0);
    $("#txtCommissionAmount").val(0);
    $("#txtAadharNo").val("");
    $("#txtPanNo").val("");
    $("#divName").removeClass('has-error');
    $("#divCode").removeClass('has-error');
    $("#divState").removeClass('has-error');
    $("#divPhoneNo1").removeClass('has-error');
}


$("#btnSave,#btnUpdate").click(function () {

    if (this.id == "btnSave")
    { if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
    else
    { if (ActionUpdate != "1") { $.Growl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

    if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
        $.jGrowl("Please enter Agent Name", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
    } else { $("#divName").removeClass('has-error'); }


    if ($("#txtAddress").val().trim() == "" || $("#txtAddress").val().trim() == undefined) {
        $.jGrowl("Please enter Address", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divAddress").addClass('has-error'); $("#txtAddress").focus(); return false;
    } else { $("#divAddress").removeClass('has-error'); }

    if ($("#ddlState").val() == "0" || $("#ddlState").val() == undefined) {
        $.jGrowl("Please Select State", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divState").addClass('has-error'); $("#ddlState").focus(); return false;
    } else { $("#divState").removeClass('has-error'); }

    if ($("#txtCity").val().trim() == "" || $("#txtCity").val().trim() == undefined) {
        $.jGrowl("Please enter City", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCity").addClass('has-error'); $("#txtCity").focus(); return false;
    } else { $("#divCity").removeClass('has-error'); }

    if ($("#ddlCommissionType").val() == "0" || $("#ddlCommissionType").val() == undefined) {
        $.jGrowl("Please select Commission Type", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCommissionType").addClass('has-error'); $("#ddlCommissionType").focus(); return false;
    } else { $("#divCommissionType").removeClass('has-error'); }

    if ($("#txtPhoneNo1").val().trim() == "" || $("#txtPhoneNo1").val().trim() == undefined) {
        $.jGrowl("Please enter Phone No", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPhoneNo1").addClass('has-error'); $("#txtPhoneNo1").focus(); return false;
    } else { $("#divPhoneNo1").removeClass('has-error'); }

    SaveandUpdateAgent();
    return false;
});
function SaveandUpdateAgent() {
    var Obj = new Object();
    Obj.AgentID = 0;
    Obj.AgentName = $("#txtName").val().toUpperCase();
    Obj.AgentCode = $("#txtCode").val().trim();
    Obj.PhoneNo1 = $("#txtPhoneNo1").val().trim();
    Obj.PhoneNo2 = $("#txtPhoneNo2").val();
    Obj.AgentAddress = $("#txtAddress").val();
    Obj.WhatsAppNo = $("#txtWhatsapp").val().trim();
    Obj.EmailID = $("#txtEmail").val().trim();
    Obj.City = $("#txtCity").val().trim();
    Obj.BankName = $("#txtBankName").val().trim();
    Obj.BranchName = $("#txtBranchName").val().trim();
    Obj.IFSCCode = $("#txtIFSCCode").val().trim();
    Obj.AccountNo = $("#txtAccountNo").val();
    Obj.AccountHolderName = $("#txtAccountHolderName").val();
    Obj.CommissionPercentage = $("#txtCommissionPercentage").val();
    Obj.CommissionAmount = $("#txtCommissionAmount").val();
    Obj.AadharNo = $("#txtAadharNo").val();
    Obj.PanNo = $("#txtPanNo").val();
    Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
    Obj.CommissionType = $("#ddlCommissionType").val();

    var ObjState = new Object();
    ObjState.StateID = $("#ddlState").val();
    Obj.State = ObjState;

    if ($("#hdnAgentID").val() > 0) {
        Obj.AgentID = $("#hdnAgentID").val();
        sMethodName = "UpdateAgent";
    }
    else { sMethodName = "AddAgent"; }

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
                        ClearAgentTab();
                        //GetRecord();
                        if (sMethodName == "AddAgent") {
                            $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnAgentID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateAgent")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        //EditRecord($("#hdnAgentID").val());
                        $("#btnList").click();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Agent_A_01") {
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

$("#txtFax").change(function () {
    ClickCount = 0;
});
function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetAgentByID",
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

                            ClearAgentTab();

                            $("#hdnAgentID").val(obj.AgentID);
                            $("#txtName").val(obj.AgentName);
                            $("#txtCode").val(obj.AgentCode);
                            $("#txtPhoneNo1").val(obj.PhoneNo1);
                            $("#txtPhoneNo2").val(obj.PhoneNo2);
                            $("#txtAddress").val(obj.AgentAddress);
                            $("#txtWhatsapp").val(obj.WhatsAppNo);
                            $("#txtEmail").val(obj.EmailID);
                            $("#ddlState").val(obj.State.StateID).change();
                            $("#txtCity").val(obj.City);
                            $("#txtBankName").val(obj.BankName);
                            $("#txtBranchName").val(obj.BranchName);
                            $("#txtIFSCCode").val(obj.IFSCCode);
                            $("#txtAccountNo").val(obj.AccountNo);
                            $("#txtAccountHolderName").val(obj.AccountHolderName);
                            $("#ddlCommissionType").val(obj.CommissionType).change();
                            $("#chkStatus").prop("checked", obj.IsActive ? true : false);
                            $("#txtCommissionPercentage").val(obj.CommissionPercentage);
                            $("#txtCommissionAmount").val(obj.CommissionAmount);
                            $("#txtAadharNo").val(obj.AadharNo);
                            $("#txtPanNo").val(obj.PanNo);
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
        url: "WebServices/VHMSService.svc/DeleteAgent",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearAgentTab();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Agent_R_01" || objResponse.Value == "Agent_D_01") {
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