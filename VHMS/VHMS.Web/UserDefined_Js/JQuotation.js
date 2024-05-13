var gMagazineData = [];
var gQuotationList = [];

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
    $("#divQuotation").hide();
    // GetDescriptionList("ddlDescriptionName");
    $("#txtQuotationDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtQuotationDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    //var date = new Date()

    GetTaxList("ddlTax");
   GetEmployeeList("ddlEmployee");

    var _Tfunctionality;
    //if ($.cookie("Quotation") != undefined) {
    //    _Tfunctionality = $.cookie("Quotation");

    //    if (_Tfunctionality == "AddNewQuotation") {
    //        pLoadingSetup(true);
    //        $("#btnAddNew").click();
    //        $("#ddlDoctor").val(parseInt($.cookie("DoctorID"))).change();
    //        GetMagazineList(parseInt($.cookie("DoctorID")));

    //        $("#ddlPatient").attr("disabled", true);

    //        GetReceivedInward(parseInt($.cookie("QuotationID")));
    //        $("#hdnQuotationID").val(parseInt($.cookie("QuotationID")));
    //    }
    //    $.cookie("Quotation", null);
    //    $.cookie("DoctorID", null);
    //    $.cookie("QuotationID", null);
    //}
    GetSettings();
    pLoadingSetup(true);
    GetRecord();

});

function Edit_QuotationDetail(ID) {
    Bind_QuotationByID(ID, gQuotationList);
    //alert($("#hdnQuotationSNo").val())
    return false;
}

$("#txtCustomerCode").blur(function () {
    if ($("#txtCustomerCode").val().length > 0) {
        GetCustomerDetails($("#txtCustomerCode").val());
       // LoadRegister("ddlChit");
    }
});

function LoadRegister(ddlname) {
    var sControlName = "#" + ddlname;
    var hidcusID = $("#hdnCustomerID").val();
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRegister",
        data: JSON.stringify({ RegisterID: hidcusID }),
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
                            $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].RegisterID + "'>" + obj[index].AccountNo + "</option>");
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

//$("#txtDiscountPercent").change(function () {
//    CalculateAmount();
//});


$("#txtDiscountPercent").change(function () {

    if (parseFloat($("#hdnMaxDiscount").val()) >= parseFloat($("#txtDiscountPercent").val())) {
        CalculateAmount();
        $("#btnSave").attr("disabled", false);
    }
    else {
        $.jGrowl("You cannot give discount percentage greater than " + $("#hdnMaxDiscount").val() + "%", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#btnSave").attr("disabled", true); $("#txtDiscountPercent").focus();
    }
});

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
                            $("#hdnMaxDiscount").val(obj.MaxDiscountPercent);
                            $("#hdnOpeningDate").val(obj.sOpeningDate);
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
function GetCustomerDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomerByCode",
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
                            $("#txtName").val(obj.CustomerName);
                            $("#hdnCustomerID").val(obj.CustomerID);
                            $("#txtPhone").val(obj.MobileNo);
                            $("#txtAddress").val(obj.Address);
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

$("#ddlTax").change(function () {
    dProgress(true);
    var id = $("#ddlTax").val();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTaxByID",
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

                            $("#txtTaxPercent").text(obj.TaxPercentage);
                            $("#txtCGSTPercent").text(obj.CGSTPercent);
                            $("#txtSGSTPercent").text(obj.SGSTPercent);
                            $("#txtIGSTPercent").text(obj.IGSTPercent);
                            CalculateAmount();
                            //$("#txtSellingPrice").val(obj.SellingPrice);
                            //$("#txtNetWeight").val(obj.TotalWeight);
                            //$("#txtStoneAmount").val(obj.StonePrice);
                            //$("#txtTotal").val(obj.StonePrice);
                            //$("#hdntransID").val(obj.StockID)
                            //$("#txtBarcode").val(obj.Barcode);
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

});

$("#txtBarcode").change(function () {
    var iStatus;
    dProgress(true);
    var id = $("#txtBarcode").val();
    if ($("#hdnSalesID").val() > 0)
        iStatus = null;
    else
        iStatus = "IN"
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStockByBarcodeQuotation",
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
                                $("#hdnStockID").val(obj.StockID)
                                $("#txtProduct").val(obj.Product.ProductName);
                                $("#txtCategory").val(obj.Category.CategoryName);
                                $("#txtGrossWeight").val(obj.NetWeight);
                                $("#txtSellingPrice").val(obj.SellingPrice);
                                $("#txtMaking").val(obj.Making);
                                $("#txtNetWeight").val(parseFloat(obj.NetWeight) + parseFloat(obj.StoneWeight));
                                $("#txtStoneAmount").val(parseFloat(obj.StonePrice) * parseFloat(obj.StoneQuantity));
                                $("#hdntransID").val(obj.StockID)
                                $("#txtBarcode").val(obj.Barcode);
                                $("#txtStoneWeight").val(obj.StoneWeight);
                                $("#txtWastePercent").val(obj.WastagePercent);
                            //  $("#txtWastage").val(obj.Wastage);

                                var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0; var iWastage = 0; var iGross = 0;
                                if ($("#txtSellingPrice").val() > 0)
                                    iSelling = $("#txtSellingPrice").val();

                                if ($("#txtMaking").val() > 0)
                                    iMaking = $("#txtMaking").val();

                                if ($("#txtStoneAmount").val() > 0)
                                    iStoneAmt = $("#txtStoneAmount").val();

                                if ($("#txtStoneWeight").val() > 0)
                                    iStoneWt = $("#txtStoneWeight").val();

                                if ($("#txtNetWeight").val() > 0)
                                    iWeight = $("#txtNetWeight").val();

                                $("#txtWastage").val(parseFloat(iSelling) * obj.Wastage);

                                if ($("#txtWastage").val() > 0)
                                    iWastage = $("#txtWastage").val();

                                if ($("#txtGrossWeight").val() > 0)
                                    iGross = $("#txtGrossWeight").val();

                                iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iGross) * parseFloat(iSelling)) + (parseFloat(iStoneAmt)) + (parseFloat(iWastage));


                                $("#txtTotal").val(parseFloat(iTotal).toFixed(2));

                                //var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0;
                                //if ($("#txtSellingPrice").val() > 0)
                                //    iSelling = $("#txtSellingPrice").val();

                                //if ($("#txtMaking").val() > 0)
                                //    iMaking = $("#txtMaking").val();

                                //if ($("#txtStoneAmount").val() > 0)
                                //    iStoneAmt = $("#txtStoneAmount").val();

                                //if ($("#txtStoneWeight").val() > 0)
                                //    iStoneWt = $("#txtStoneWeight").val();

                                //if (obj.TotalWeight > 0)
                                //    iWeight = obj.TotalWeight;

                                //iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iWeight) * parseFloat(iSelling)) + (parseFloat(iStoneWt) * parseFloat(iStoneAmt));
                          
                                //$("#txtTotal").val(parseFloat(iTotal).toFixed(2));
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

});
$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnQuotationID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").hide();
    $("#divQuotation").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gQuotationList = [];
    ClearQuotationTab();
    //$('#txtQuotationDate').datepicker('setDate', 'today');
    //ClearFields();
    $("#divQuotationList").empty();

    $("#txtQuotationDate").focus();
    $("#txtQuotationNo").focus();
    return false;
});

function PrintQuotationDetails() {
    SetSessionValue("AdmissionID", $("#hdnQuotationID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewQuotationDetails.aspx", "_blank");
    return false;
}

$("#btnPrint").click(function () {

    SetSessionValue("QuotationID", $("#hdnQuotationID").val());

    var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
    // PrintQuotationDetails();
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divQuotation").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnQuotationID").val("0");
    gQuotationList = [];
    ClearQuotationTab();
    $("#btnList").click();
    return false;
});
function ClearQuotationTab() {
    $("#txtQuotationNo").val("");
    $("#txtQuotationDate").val("");
    $("#hdnCustomerID").val("0");
    $("#txtSubtotal").val("0");
    $("#txtDiscount").val("0");
    $("#txtTotalAmount").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    gQuotationList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();
    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtQuotationDate").val(d + "/" + m + "/" + y);
    $("#txtQuotationNo").attr("disabled", false);
    $("#ddlDoctor").attr("disabled", false);
    return false;
}

function GetTaxList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTax",
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
                            $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    if (obj[index].IsActive == "1")
                                    $(sControlName).append("<option value='" + obj[index].TaxID + "'>" + obj[index].TaxName + "</option>");
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

function GetEmployeeList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetUser",
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
                            $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].UserID + "'>" + obj[index].EmployeeCode + "</option>");
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

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    //if ($("#hdntransID").val() == "0" || $("#hdntransID").val() == undefined || $("#hdntransID").val() == null) {
    //    $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    //}
    //else { $("#divBarcode").removeClass('has-error'); }

    if ($("#hdnStockID").val() == "0" || $("#hdnStockID").val() == undefined || $("#hdnStockID").val() == "" || $("#hdnStockID").val() == null) {
        $.jGrowl("Please Enter Correct barcode", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    }
    else { $("#divBarcode").removeClass('has-error'); }

    var ObjData = new Object();
    ObjData.QuotationTransID = 0;
    var ObjStock = new Object();
    ObjStock.StockID = $("#hdnStockID").val();
    ObjData.Stock = ObjStock;
    ObjData.Quantity = 1;
    ObjData.Rate = parseFloat($("#txtSellingPrice").val());
    ObjData.Making = parseFloat($("#txtMaking").val());
    ObjData.ProductName = $("#txtProduct").val();
    ObjData.CategoryName = $("#txtCategory").val();
    ObjData.Barcode = $("#txtBarcode").val();
    ObjData.Subtotal = parseFloat($("#txtTotal").val()); 
    ObjData.StonePrice = parseFloat($("#txtStoneAmount").val());
    ObjData.TotalWeight = parseFloat($("#txtGrossWeight").val()) + parseFloat($("#txtWastage").val());
    ObjData.NetWeight = parseFloat($("#txtGrossWeight").val());
    ObjData.StoneWeight = parseFloat($("#txtStoneWeight").val());
    ObjData.WastagePercent = parseFloat($("#txtWastePercent").val());
    ObjData.Wastage = parseFloat($("#txtWastage").val());

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gQuotationList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.QuotationTransID = 0;
        ObjData.StatusFlag = "I";
        AddQuotationData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnQuotationID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.QuotationID = $("#hdnQuotationID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.QuotationID = 0;
        }
        Update_Quotation(ObjData);
    }
    CalculateAmount();
    ClearQuotationFields();
    $("#ddlDescriptionName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
});
function ClearQuotationFields() {
    $("#btnAddQuotation").show();
    $("#btnUpdateQuotation").hide();

    $("#txtTotal").val("0");
    $("#txtStoneAmount").val("0");
    $("#txtNetWeight").val("0");
    $("#txtSellingPrice").val("0");
    $("#txtMaking").val("0");
    $("#txtGrossWeight").val("0");
    $("#txtProduct").val("");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");
    $("#txtCategory").val("");
    $("#txtStoneWeight").val("");
    $("#txtWastage").val("");
    $("#txtWastePercent").val("");
    $("#hdnStockID").val("");

   // $("#ddlDescriptionName").val(null).change();
    $("#divSelectDescriptionName").show();
    $("#divDescriptionName").removeClass('has-error');
    $("#divAmount").removeClass('has-error');
    return false;
}
function AddQuotationData(oData) {
    gQuotationList.push(oData);
    DisplayQuotationList(gQuotationList);
    return false;
}
function DisplayQuotationList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divQuotationList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divQuotationList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblQuotationList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Barcode</th>";
        sTable += "<th class='" + sColorCode + "'>Category</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Gold Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Stone Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Stone Price</th>";
        sTable += "<th class='" + sColorCode + "'>Wastage %</th>";
        sTable += "<th class='" + sColorCode + "'>Wastage</th>";
        sTable += "<th class='" + sColorCode + "'>Net Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblQuotationList_body'>";
        sTable += "</tbody></table>";
        $("#divQuotationList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Barcode + "</td>";
                sTable += "<td>" + gData[i].CategoryName + "</td>";
                sTable += "<td>" + gData[i].ProductName + "</td>";
                sTable += "<td>" + gData[i].NetWeight + "</td>";
                sTable += "<td>" + gData[i].StoneWeight + "</td>";
                sTable += "<td>" + gData[i].StonePrice + "</td>";
                sTable += "<td>" + gData[i].WastagePercent + "</td>";
                sTable += "<td>" + gData[i].Wastage + "</td>";
                sTable += "<td>" + (parseFloat(gData[i].NetWeight) + parseFloat(gData[i].StoneWeight)) + "</td>";
                sTable += "<td>" + gData[i].Subtotal + "</td>";

                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_QuotationDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_QuotationDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblQuotationList_body").append(sTable);
            }
        }
    }
    else { $("#divQuotationList").empty(); }

    return false;
}
function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gQuotationList);
    return false;
}
function Bind_QuotationByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#txtBarcode").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnOPSNo").val(ID);
            $("#hdnQuotationID").val(data[i].QuotationID);
            $("#txtBarcode").val(data[i].Barcode).change();
            $("#txtSellingPrice").val(data[i].Rate);

            var iTotal = 0; var iSelling = 0; var iMaking = 0; var iWeight = 0; var iStoneWt = 0; var iStoneAmt = 0; var iWastage = 0; var iGross = 0;
            if ($("#txtSellingPrice").val() > 0)
                iSelling = $("#txtSellingPrice").val();

            if ($("#txtMaking").val() > 0)
                iMaking = $("#txtMaking").val();

            if ($("#txtStoneAmount").val() > 0)
                iStoneAmt = $("#txtStoneAmount").val();

            if ($("#txtStoneWeight").val() > 0)
                iStoneWt = $("#txtStoneWeight").val();

            if ($("#txtNetWeight").val() > 0)
                iWeight = $("#txtNetWeight").val();

            $("#txtWastage").val(parseFloat(iSelling) * obj.Wastage);

            if ($("#txtWastage").val() > 0)
                iWastage = $("#txtWastage").val();

            if ($("#txtGrossWeight").val() > 0)
                iGross = $("#txtGrossWeight").val();

            iTotal = (parseFloat(iMaking) * parseFloat(iWeight)) + (parseFloat(iGross) * parseFloat(iSelling)) + (parseFloat(iStoneAmt)) + (parseFloat(iWastage));


            $("#txtTotal").val(parseFloat(iTotal).toFixed(2));
        }
    }
    return false;
}
function Update_Quotation(oData) {
    for (var i = 0; i < gQuotationList.length; i++) {
        if (gQuotationList[i].sNO == oData.sNO) {
            gQuotationList[i].QuotationID = oData.QuotationID;
            gQuotationList[i].ProductName = oData.ProductName;
            gQuotationList[i].CategoryName = oData.CategoryName;
           
            gQuotationList[i].Subtotal = oData.Subtotal;
            gQuotationList[i].Barcode = oData.Barcode;
            gQuotationList[i].StatusFlag = oData.StatusFlag;
            gQuotationList[i].StonePrice = oData.StonePrice;
            gQuotationList[i].TotalWeight = oData.TotalWeight;
            gQuotationList[i].NetWeight = oData.NetWeight;
            gQuotationList[i].StoneWeight = oData.StoneWeight;
            gQuotationList[i].WastagePercent = oData.WastagePercent;
            gQuotationList[i].Wastage = oData.Wastage;
        }
    }
    DisplayQuotationList(gQuotationList);
    $("#btnAddQuotation").show();
    $("#btnUpdateQuotation").hide();
    ClearQuotationFields();
    $("#txtBarcode").focus();
    return false;
}
function Delete_QuotationDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gQuotationList.length; i++) {
            if (gQuotationList[i].SNo == ID) {
                var index = jQuery.inArray(gQuotationList[i].valueOf("SNo"), gQuotationList);
                if (gQuotationList[i].SNo > 0) {
                    gQuotationList[i].StatusFlag = "D";
                } else {
                    gQuotationList.splice(index, 1);
                }
                $("#divQuotationList").empty();
                DisplayQuotationList(gQuotationList);
                CalculateAmount();
            }
        }
    }
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
        url: "WebServices/VHMSService.svc/GetTopQuotation",
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
                                if (obj[index].IsCancelled == "0")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                var table = "<tr id='" + obj[index].QuotationID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].QuotationNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sQuotationDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Doctor.DoctorName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceAmount + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1" )
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='PrintQuotation' title='Click here to Print'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintQuotation").click(function () {
                                SetSessionValue("QuotationID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintQuotation.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
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
                            //{ "sWidth": "10%" },
                          { "sWidth": "5%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          //{ "sWidth": "0%" },
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

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SearchQuotation",
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

                                var table = "<tr id='" + obj[index].QuotationID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].QuotationNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sQuotationDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Doctor.DoctorName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].InvoiceAmount + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].QuotationID + " class='PrintQuotation' title='Click here to Print'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintQuotation").click(function () {
                                SetSessionValue("QuotationID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintQuotation.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?'))
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
                          { "sWidth": "10%" },
                          { "sWidth": "10%" },
                            //{ "sWidth": "10%" },
                          { "sWidth": "5%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          { "sWidth": "0%" },
                          //{ "sWidth": "0%" },
                          { "sWidth": "1%" },
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
//$("#ddlDescriptionName").change(function () {
//    var iDescriptionID = $("#ddlDescriptionName").val();
//    if (iDescriptionID != undefined && iDescriptionID > 0) {
//        GetDescriptionByID(iDescriptionID);
//    }
//    else {
//        $("#txtamount").val("0");
//    }
//});

$("#btnCancel").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

$("#btnOK").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

function CalculateAmount() {
    var iQuotationAmount = 0;
    var iTotal = 0;
    var iDiscount = 0;
    for (var i = 0; i < gQuotationList.length; i++) {
        if (gQuotationList[i].StatusFlag != "D")
            iQuotationAmount = iQuotationAmount + parseFloat(gQuotationList[i].Subtotal);
    }
    $("#txtSubtotal").val(iQuotationAmount);

    if ($("#txtDiscountPercent").val() > 0)
        iDiscount = $("#txtDiscountPercent").val();
    $("#txtDiscount").val((iQuotationAmount * parseFloat(iDiscount) / 100).toFixed(2));
    iQuotationAmount = iQuotationAmount - parseFloat($("#txtDiscount").val());
    
    $("#txtTaxAmount").val((iQuotationAmount * parseFloat($("#txtTaxPercent").text()) / 100).toFixed(2));
    $("#txtCGSTAmount").val((iQuotationAmount * parseFloat($("#txtCGSTPercent").text()) / 100).toFixed(2));
    $("#txtSGSTAmount").val((iQuotationAmount * parseFloat($("#txtSGSTPercent").text()) / 100).toFixed(2));
    $("#txtIGSTAmount").val((iQuotationAmount * parseFloat($("#txtIGSTPercent").text()) / 100).toFixed(2));
    iQuotationAmount = iQuotationAmount + parseFloat($("#txtTaxAmount").val());

    //if ($("#txtDiscount").val() > 0)
    //    $("#txtTotalAmount").val((parseFloat($("#txtSubtotal").val()) - parseFloat($("#txtDiscount").val())).toFixed(2));
    //else
    $("#txtTotalAmount").val(parseFloat(iQuotationAmount).toFixed(2));
}

function GetDescriptionByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetDescriptionByID",
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
                            $("#txtAmount").val(obj.Amount);
                            //$("#ddlInstruction").val(obj.InstructionID);
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
        if (confirm('Are you sure to Save?')) {

        }
        else {
            return false;
        }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
        if (confirm('Are you sure to Update?')) {

        }
        else {
            return false;
        }
    }

    if ($("#txtQuotationDate").val().trim() == "" || $("#txtQuotationDate").val().trim() == undefined) {
        $.jGrowl("Please select Quotation Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuotationDate").addClass('has-error'); $("#txtQuotationDate").focus(); return false;
    }
    else { $("#divQuotationDate").removeClass('has-error'); }

    var d1 = Date.parse($("#hdnOpeningDate").val());
    var d2 = Date.parse($("#txtQuotationDate").val());
    if (d1 < d2) {
        $.jGrowl("Date not updated yet. Contact Administrator", { sticky: false, theme: 'warning', life: jGrowlLife });
        return false;
    }

    //if ($("#hdnCustomerID").val() == "0" || $("#txtName").val() == undefined || $("#txtName").val() == null || $("#txtName").val().trim() == "") {
    //    $.jGrowl("Please Enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divCustomer").addClass('has-error'); $("#txtOPDNo").focus(); return false;
    //}
    //else { $("#divCustomer").removeClass('has-error'); }

    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divtax").addClass('has-error'); $("#ddlTax").focus(); return false;
    } else { $("#divtax").removeClass('has-error'); }

    if ($("#ddlEmployee").val() == "0" || $("#ddlEmployee").val() == undefined || $("#ddlEmployee").val() == null) {
        $.jGrowl("Please select Employee", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divEmployee").addClass('has-error'); $("#ddlEmployee").focus(); return false;
    } else { $("#divEmployee").removeClass('has-error'); }

    if (gQuotationList.length <= 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }
    var gCount=0;
    for (var i = 0; i < gQuotationList.length; i++)
    {
        if (gQuotationList[i].StatusFlag != "D")
            gCount++;
        
    }
    if (gCount == 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }
    var iQuotationAmount = 0;
    for (var i = 0; i < gQuotationList.length; i++)
        iQuotationAmount = iQuotationAmount + parseFloat(gQuotationList[i].Subtotal);

    var ObjQuotation = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#hdnCustomerID").val();
    ObjQuotation.Customer = ObjCustomer;

    var Objtax = new Object();
    Objtax.TaxID = $("#ddlTax").val();
    ObjQuotation.Tax = Objtax;

    var ObjEmployee = new Object();
    ObjEmployee.UserID = $("#ddlEmployee").val();
    ObjQuotation.Employee = ObjEmployee;

    ObjQuotation.QuotationID = 0;
    ObjQuotation.QuotationNo = $("#txtQuotationNo").val().trim();
    ObjQuotation.sQuotationDate = $("#txtQuotationDate").val().trim();
    ObjQuotation.DiscountAmount = parseFloat($("#txtDiscount").val());
    ObjQuotation.DiscountPercent = parseFloat($("#txtDiscountPercent").val());
    ObjQuotation.TotalAmount = parseFloat($("#txtSubtotal").val());
    ObjQuotation.InvoiceAmount = parseFloat($("#txtTotalAmount").val());
    ObjQuotation.TaxAmount = parseFloat($("#txtTaxAmount").val());
    ObjQuotation.TaxPercent = parseFloat($("#txtTaxPercent").text());
    ObjQuotation.CGSTAmount = parseFloat($("#txtCGSTAmount").val());
    ObjQuotation.SGSTAmount = parseFloat($("#txtSGSTAmount").val());
    ObjQuotation.IGSTAmount = parseFloat($("#txtIGSTAmount").val());

    
    ObjQuotation.QuotationTrans = gQuotationList;
    //ObjQuotation.QuotationID = $("#hdnQuotationID").val();

    if ($("#hdnQuotationID").val() > 0) {
        ObjQuotation.QuotationID = $("#hdnQuotationID").val();
        sMethodName = "UpdateQuotation";
    }
    else {
        sMethodName = "AddQuotation";
        ObjQuotation.QuotationID = 0
    }

    SaveandUpdateQuotation(ObjQuotation, sMethodName);

});
function SaveandUpdateQuotation(ObjQuotation, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjQuotation }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                       
                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divQuotation").hide();
                        if (sMethodName == "AddQuotation") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnQuotationID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateQuotation")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                        SetSessionValue("QuotationID", $("#hdnQuotationID").val());
                            var myWindow = window.open("PrintQuotation.aspx", "MsgWindow");
                       
                        $("#hdnQuotationID").val("0");
                        ClearQuotationTab();
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
        url: "WebServices/VHMSService.svc/GetQuotationByID",
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

                            $("#txtQuotationNo").attr("disabled", true);
                            // $("#ddlPatient").attr("disabled", true);

                            $("#hdnQuotationID").val(obj.QuotationID)
                            $("#hdnCustomerID").val(obj.Customer.CustomerID).change();
                            $("#ddlTax").val(obj.Tax.TaxID).change();
                            $("#txtQuotationNo").val(obj.QuotationNo);
                            $("#ddlEmployee").val(obj.Employee.UserID).change();
                            $("#txtQuotationDate").val(obj.sQuotationDate);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscount").val(obj.DiscountAmount);
                            $("#txtTotalAmount").val(obj.InvoiceAmount);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtTaxAmount").val(obj.TaxAmount);
                          //  $("#txtTaxPercent").text(obj.TaxPercent);
                            $("#txtCGSTAmount").val(obj.CGSTAmount);
                            $("#txtSGSTAmount").val(obj.SGSTAmount);
                            $("#txtIGSTAmount").val(obj.IGSTAmount);                           
                            $("#txtAddress").val(obj.Customer.Address)
                            $("#txtPhone").val(obj.Customer.MobileNo)
                            $("#txtCustomerCode").val(obj.Customer.MobileNo)
                            $("#txtName").val(obj.Customer.CustomerName)

                            gQuotationList = [];
                            var ObjProduct = obj.QuotationTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                var objStock = new Object();
                                objStock.StockID = ObjProduct[index].StockID;
                                objTemp.Stock = objStock;
                                objTemp.QuotationID = ObjProduct[index].QuotationID;
                                objTemp.QuotationTransID = ObjProduct[index].QuotationTransID;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.Subtotal = ObjProduct[index].Subtotal;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.WastagePercent = ObjProduct[index].WastagePercent;
                                objTemp.Wastage = ObjProduct[index].Wastage;
                                objTemp.NetWeight = ObjProduct[index].NetWeight;
                                objTemp.WastageAmt = ObjProduct[index].Wastage * ObjProduct[index].Rate;
                                objTemp.TotalWeight = ObjProduct[index].TotalWeight;
                                objTemp.StonePrice = ObjProduct[index].StonePrice * ObjProduct[index].StoneQuantity;
                                objTemp.Making = ObjProduct[index].Making;
                                objTemp.StoneWeight = ObjProduct[index].StoneWeight;
                                objTemp.CategoryName = ObjProduct[index].CategoryName;
                                objTemp.ProductName = ObjProduct[index].ProductName;
                                AddQuotationData(objTemp);
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
function ShowDeleteRecord(id) {
    $("#hdnID").val("");
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    $('#compose-modal').modal({ show: true, backdrop: true });
    $("#txtID").val(id);
    $("#txtReason").focus();
    return false;
}


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
        url: "WebServices/VHMSService.svc/DeleteQuotation",
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
                    else if (objResponse.Value == "Quotation_R_01" || objResponse.Value == "Quotation_D_01") {
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
