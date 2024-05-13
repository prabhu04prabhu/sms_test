var ActionAdd = '0', ActionUpdate = '0', ActionDelete = '0', ActionView = '0', ActionAccess = '0';
var iRoleID = 0;
var jGrowlLife = 800;
var SESSIONVALUE = '';

var _CMDeleteError = 'Since this record is referred somewhere else you cannot delete it';
var _CMAlreadyExits = 'Name already exists';
var _CMAccessDeined = "You don't have permission. Please contact Administrator";

//Added on 21-06-2017
var _PrimaryConsultantID = 1;
var _CoConsultantID = 2;
var _RegistrarID = 3;
//Added on 22-09-2017
var _CheckedBy = 4;

var tableOpts25 = {
    destroy: true,
    searching: false,
    sorting: true,
    paging: false,
    "bAutoWidth": false,
    "iDisplayLength": 25
};

var tableOpts = {
    destroy: true,
    searching: true,
    sorting: true,
    paging: true,
    "bAutoWidth": false,
    "iDisplayLength": 25
};

var tableOpts50 = {
    destroy: true,
    searching: false,
    paging: false,
    "bAutoWidth": false,
    "iDisplayLength": 50
};

//Block Special Characters
function blockSpecialCharacters(event) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function Alphabets(event) {
    var regex = new RegExp("/^[a-zA-Z\s]+$/");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

// Allow Number Only
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//decimalPoint validator and allow two digit after "."
function IsNumeric(evt, btnid) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    else {
        return true;
    }
}
function isPrice(evt, value) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((value.indexOf('.') != -1) && (charCode != 45 && (charCode < 48 || charCode > 57)))
        return false;
    if (charCode != 45 && (charCode != 46 || $(this).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function GetSessionValue(SessionName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSessionName",
        data: JSON.stringify({ sSessionName: SessionName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                SESSIONVALUE = data.d;
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });

    return SESSIONVALUE;
}
function SetSessionValue(SessionName, Obj) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SetSessionValue",
        data: JSON.stringify({ sSessionName: SessionName, ObjValue: Obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {

            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });

    return SESSIONVALUE;
}

function pLoadingSetup(dFlag) {
    if (dFlag) {
        $('.container-wrapper').removeClass("hidden");
    }
    else {
        $('.container-wrapper').addClass("hidden");
    }
}
function dProgress(bFlag) {
    if (bFlag) {
        $("#divLoading > .overlay").height($(document).height());
        $("#divLoading").removeClass("hidden");
    }
    else {
        $("#divLoading").addClass("hidden");
    }
}

$('.two-digits').keyup(function () {
    if ($(this).val().indexOf('.') != -1) {
        if ($(this).val().split(".")[1].length > 2) {
            if (isNaN(parseFloat(this.value))) return;
            this.value = parseFloat(this.value).toFixed(2);
        }
    }
    return this; //for chaining
});

function FixTodayDate(count) {
    var today = new Date();
    var dd = today.getDate() + count;
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = "0" + mm;
    }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function GetDrugFrequencyByID(ID) {
    var sFrequency = "";
    if (ID == 1) sFrequency = "0-0-1";
    else if (ID == 2) sFrequency = "0-1-1";
    else if (ID == 3) sFrequency = "1-1-1";
    else if (ID == 4) sFrequency = "1-0-0";
    else if (ID == 5) sFrequency = "1-1-0";
    else if (ID == 6) sFrequency = "1-0-1";
    else if (ID == 7) sFrequency = "0-0-1";
    else if (ID == 8) sFrequency = "SOS";
    else if (ID == 9) sFrequency = "Others";
    else if (ID == 10) sFrequency = "1/2-1/2-1/2";
    else if (ID == 11) sFrequency = "1/2-0-1/2";
    else if (ID == 12) sFrequency = "1/2-0-0";
    else if (ID == 13) sFrequency = "0-1/2-0";
    else if (ID == 14) sFrequency = "0-0-1/2";

    return sFrequency;
}