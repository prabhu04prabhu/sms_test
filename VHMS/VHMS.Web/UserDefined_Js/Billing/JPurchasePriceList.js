var gData = [];
$(function () {
    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;

    pLoadingSetup(false);
    $("#tab-modal").show();

    $("#btnSave").show();

    var _Tfunctionality;
    if ($.cookie("Pricing") != undefined) {
        _Tfunctionality = $.cookie("Pricing");

        if (_Tfunctionality == "Add New Pricing") {
            pLoadingSetup(true);
            $("#btnAddNew").click();
        }
        $.cookie("Pricing", null);
    }
    GetSupplierList();
    GetProductType();
    GetCategoryList();
    GetSubCategoryList();
    GetProductList();

    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
    pLoadingSetup(true);
});

$("#ddlCategory").change(function () {
    //GetSubCategoryList();
    //GetProductList();
    //GetRecord();
    //$("#tblRecord_length").hide();
    //$("#tblRecord_filter").hide();
    //$("#tblRecord_info").hide();
    //$(".dataTables_paginate").hide();
});

$("#ddlSubCategory").change(function () {
    GetProductList();
    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
});

$("#ddlSupplier").change(function () {
    GetProductList();
    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
});


$("#ddlType").change(function () {
    GetProductList();
    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
});

$("#ddlProduct").change(function () {
    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
});
$("#txtCode").change(function () {
    GetRecord();
    $("#tblRecord_length").hide();
    $("#tblRecord_filter").hide();
    $("#tblRecord_info").hide();
    $(".dataTables_paginate").hide();
});


function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetPricing",
        data: JSON.stringify({ ProductID: $("#ddlProduct").val(), CategoryID: $("#ddlCategory").val(), SubCategoryID: $("#ddlSubCategory").val(), SupplierID: $("#ddlSupplier").val(), TypeID: $("#ddlType").val(), ProductCode: $("#txtCode").val().trim() }),
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
                            gData = [];
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1")
                                { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                else
                                { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                var table = "<tr id='" + obj[index].Product.ProductID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].Product.SMSCode + "</td>";
                                table += "<td>" + obj[index].Product.ProductName + "</td>";
                                table += "<td>" + obj[index].Product.Supplier.SupplierName + "</td>";
                                table += "<td>" + obj[index].Product.ProductCode + "</td>";
                                table += "<td style='text-align:right;'>" + obj[index].PurchasePrice + "</td>";
                                table += "<td><a href='#' id=" + obj[index].Product.ProductID + " onclick = 'GetProductByID(this.id)'><i class='fa fa-lg fa-file-image-o text-green' style='font-size: 14px;margin-left: 7px;'/></a></td>";

                                table += "</tr>";
                                $("#tblRecord_tbody").append(table);
                                var objAgent = new Object();
                                objAgent.ProductID = obj[index].Product.ProductID;
                                gData.push(objAgent);
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        gData = [];
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                    $("#tblRecord").dataTable({
                        "bPaginate": false,
                        "bFilter": true,
                        "bSort": true,
                        "iDisplayLength": 1000,
                        aoColumns: [
                            { "sWidth": "5%" },
                            { "sWidth": "15%" },
                            { "sWidth": "45%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
                            { "sWidth": "15%" },
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
            // $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}
$("#btnImageCancel").click(function () {
    $('#composeImage').modal('hide');
    return false;
});
Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

function GetProductByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductByID",
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
                            //$("[id*=imgUpload1]").css("visibility", "visible");
                            //$("[id*=imgUpload1]").attr("src", obj.ProductImage1);
                            //$("[id*=imgUpload5]").css("visibility", "visible");
                            //$("[id*=imgUpload5]").attr("src", obj.ProductImage2);
                            //$("[id*=imgUpload6]").css("visibility", "visible");
                            //$("[id*=imgUpload6]").attr("src", obj.ProductImage3);

                            //$(".modal-title").html("&nbsp;&nbsp; Product Image");
                            //$('#composeImage').modal({ show: true, backdrop: true });
                            $("#myImg").empty();
                            gImageupload = [];
                            var Objimg = obj.ProductImages;

                            for (var index = 0; index < Objimg.length; index++) {
                                var ObjData = new Object();
                                ObjData.sNO = gImageupload.max() + 1;
                                ObjData.SNo = ObjData.sNO;
                                ObjData.filename = Objimg[index].Filename;
                                ObjData.filepath = Objimg[index].Filepath;
                                $('#myImg').append('<img src="' + Objimg[index].Filepath + '" style="width: 200px;height: 200px;margin: 0px 15px 15px 0px;">');
                            }

                            $(".modal-title").html("&nbsp;&nbsp; Product Image");
                            $('#composeProductImage').modal({ show: true, backdrop: true });
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
$("#btnProductImageCancel").click(function () {
    $('#composeProductImage').modal('hide');
    return false;
});

function Edit_WholeSale(ID) {
    $("#hdnProductID").val(ID);
    $("#txtWholeSalePriceA").val($("#txtWholeSalePriceA_" + ID).val());
    $("#txtWholeSalePriceB").val($("#txtWholeSalePriceB_" + ID).val());
    $("#txtWholeSalePriceC").val($("#txtWholeSalePriceC_" + ID).val());

    if ($("#txtPricingA_" + ID).val() == "true")
        $("#divWholeSalePriceA").show();
    else
        $("#divWholeSalePriceA").hide();

    if ($("#txtPricingB_" + ID).val() == "true")
        $("#divWholeSalePriceB").show();
    else
        $("#divWholeSalePriceB").hide();

    if ($("#txtPricingC_" + ID).val() == "true")
        $("#divWholeSalePriceC").show();
    else
        $("#divWholeSalePriceC").hide();

    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Wholesale Price");
    $('#composeWholeSale').modal({ show: true, backdrop: true });
}

function Edit_Retail(ID) {
    $("#hdnProductID").val(ID);
    $("#txtRetailPriceA").val($("#txtRetailPriceA_" + ID).val());
    $("#txtRetailPriceB").val($("#txtRetailPriceB_" + ID).val());
    $("#txtRetailPriceC").val($("#txtRetailPriceC_" + ID).val());

    if ($("#txtPricingA_" + ID).val() == "true")
        $("#divRetailPriceA").show();
    else
        $("#divRetailPriceA").hide();

    if ($("#txtPricingB_" + ID).val() == "true")
        $("#divRetailPriceB").show();
    else
        $("#divRetailPriceB").hide();

    if ($("#txtPricingC_" + ID).val() == "true")
        $("#divRetailPriceC").show();
    else
        $("#divRetailPriceC").hide();

    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Retail Price");
    $('#composeRetail').modal({ show: true, backdrop: true });
}

$("#btnSaveRetail").click(function () {
    var ID = $("#hdnProductID").val();
    $("#txtRetailPriceA_" + ID).val($("#txtRetailPriceA").val());
    $("#txtRetailPriceB_" + ID).val($("#txtRetailPriceB").val());
    $("#txtRetailPriceC_" + ID).val($("#txtRetailPriceC").val());
    $('#composeRetail').modal('hide');
    return false;
});

$("#btnSaveWholeSale").click(function () {
    var ID = $("#hdnProductID").val();
    $("#txtWholeSalePriceA_" + ID).val($("#txtWholeSalePriceA").val());
    $("#txtWholeSalePriceB_" + ID).val($("#txtWholeSalePriceB").val());
    $("#txtWholeSalePriceC_" + ID).val($("#txtWholeSalePriceC").val());
    $('#composeWholeSale').modal('hide');
    return false;
});

$("#btnCancelWholeSale").click(function () {
    $('#composeWholeSale').modal('hide');
    return false;
});


$("#btnCancelRetail").click(function () {
    $('#composeRetail').modal('hide');
    return false;
});


$("#btnCancel").click(function () {
    $('#composeProductImage').modal('hide');
    return false;
});
function GetSupplierList() {
    $("#ddlSupplier").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSupplier",
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
                            $("#ddlSupplier").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1")
                                    $("#ddlSupplier").append('<option value=' + obj[index].SupplierID + ' >' + obj[index].SupplierName + '</option>');
                            }
                            $("#ddlSupplier").val(0).change();
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlSupplier").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlSupplier").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetProductType() {
    $("#ddlType").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductType",
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
                            $("#ddlType").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1")
                                    $("#ddlType").append('<option value=' + obj[index].ProductTypeID + ' >' + obj[index].ProductTypeName + '</option>');
                            }
                            $("#ddlType").val(0).change();
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlType").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetCategoryList() {
    $("#ddlCategory").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCategory",
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
                            $("#ddlCategory").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1")
                                    $("#ddlCategory").append('<option value=' + obj[index].CategoryID + ' >' + obj[index].CategoryName + '</option>');
                            }
                            $("#ddlCategory").val(0).change();
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetSubCategoryList() {
    $("#ddlSubCategory").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSubCategoryByCategoryID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: $("#ddlCategory").val() }),
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {

                                if (obj[index].IsActive == "1")
                                    $("#ddlSubCategory").append('<option value=' + obj[index].SubCategoryID + ' >' + obj[index].SubCategoryName + '</option>');
                            }
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetProductList() {
    $("#ddlProduct").empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: 0, CategoryID: $("#ddlCategory").val(), SubCategoryID: $("#ddlSubCategory").val(), SupplierID: $("#ddlSupplier").val(), TypeID: $("#ddlType").val() }),
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $("#ddlProduct").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1")
                                    $("#ddlProduct").append('<option value=' + obj[index].ProductID + ' >' + obj[index].ProductName + '</option>');
                            }
                            $("#ddlProduct").val(0).change();
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#ddlProduct").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        $("#ddlProduct").val(0).change();
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
                $("#ddlProduct").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            //$.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

$("#btnAddNew").click(function () {
    $("#divRecords").hide();
    $("#tab-modal").show();
    $("#btnList").show();
    $("#btnAddNew").hide();
    $("#hdnPricingID").val("");
    $("#btnSave").show();
    $("#aGeneral").click();
    ClearPricingTab();
    return false;
});
$("#btnList,#btnClose").click(function () {

    var myWindow = window.open("frmDefault.aspx", "_self");
    //$("#divRecords").show();
    //$("#tab-modal").hide();
    //$("#btnList").hide();
    //$("#btnAddNew").show();
    //$("#aGeneral").click();
    //$.cookie("Pricing", null);
    return false;
});


$("#btnSave").click(function () {
    if (this.id == "btnSave")
    { if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

    for (var index = 0; index < gData.length; index++) {
        var Obj = new Object();

        var ObjProduct = new Object();
        ObjProduct.ProductID = gData[index].ProductID;
        Obj.Product = ObjProduct;

        var sCtrlName = "txtPurchasePrice_" + gData[index].ProductID;
        var iPurchasePrice = $("#" + sCtrlName).val();

        sCtrlName = "txtMRP_" + gData[index].ProductID;
        var iMRP = $("#" + sCtrlName).val();

        sCtrlName = "txtWholeSalePrice_" + gData[index].ProductID;
        var iWholeSalePrice = $("#" + sCtrlName).val();

        sCtrlName = "txtWholeSalePriceA_" + gData[index].ProductID;
        var iWholeSalePriceA = $("#" + sCtrlName).val();

        sCtrlName = "txtWholeSalePriceB_" + gData[index].ProductID;
        var iWholeSalePriceB = $("#" + sCtrlName).val();

        sCtrlName = "txtWholeSalePriceC_" + gData[index].ProductID;
        var iWholeSalePriceC = $("#" + sCtrlName).val();

        sCtrlName = "txtRetailPrice_" + gData[index].ProductID;
        var iRetailPrice = $("#" + sCtrlName).val();

        sCtrlName = "txtRetailPriceA_" + gData[index].ProductID;
        var iRetailPriceA = $("#" + sCtrlName).val();

        sCtrlName = "txtRetailPriceB_" + gData[index].ProductID;
        var iRetailPriceB = $("#" + sCtrlName).val();

        sCtrlName = "txtRetailPriceC_" + gData[index].ProductID;
        var iRetailPriceC = $("#" + sCtrlName).val();

        sCtrlName = "txtRetailMargin_" + gData[index].ProductID;
        var iRetailMargin = $("#" + sCtrlName).val();

        sCtrlName = "txtWholeSaleMargin_" + gData[index].ProductID;
        var iWholeSaleMargin = $("#" + sCtrlName).val();

        sCtrlName = "txtMinDiscountPercent_" + gData[index].ProductID;
        var iMinDiscountPercent = $("#" + sCtrlName).val();

        sCtrlName = "txtMaxDiscountPercent_" + gData[index].ProductID;
        var iMaxDiscountPercent = $("#" + sCtrlName).val();



        if (!isNaN(iPurchasePrice) && iPurchasePrice != "" && iPurchasePrice != undefined)
            Obj.PurchasePrice = iPurchasePrice;
        else
            Obj.PurchasePrice = 0;

        if (!isNaN(iMRP) && iMRP != "" && iMRP != undefined)
            Obj.MRP = iMRP;
        else
            Obj.MRP = 0;

        if (!isNaN(iWholeSalePrice) && iWholeSalePrice != "" && iWholeSalePrice != undefined)
            Obj.WholeSalePrice = iWholeSalePrice;
        else
            Obj.WholeSalePrice = 0;

        if (!isNaN(iWholeSalePriceA) && iWholeSalePriceA != "" && iWholeSalePriceA != undefined)
            Obj.WholeSalePriceA = iWholeSalePriceA;
        else
            Obj.WholeSalePriceA = Obj.WholeSalePrice;

        if (!isNaN(iWholeSalePriceB) && iWholeSalePriceB != "" && iWholeSalePriceB != undefined)
            Obj.WholeSalePriceB = iWholeSalePriceB;
        else
            Obj.WholeSalePriceB = Obj.WholeSalePrice;

        if (!isNaN(iWholeSalePriceC) && iWholeSalePriceC != "" && iWholeSalePriceC != undefined)
            Obj.WholeSalePriceC = iWholeSalePriceC;
        else
            Obj.WholeSalePriceC = Obj.WholeSalePrice;

        if (!isNaN(iRetailMargin) && iPurchasePrice != "" && iRetailMargin != undefined)
            Obj.RetailMargin = iRetailMargin;
        else
            Obj.RetailMargin = 0;

        if (!isNaN(iRetailPrice) && iRetailPrice != "" && iRetailPrice != undefined)
            Obj.RetailPrice = iRetailPrice;
        else
            Obj.RetailPrice = 0;

        if (!isNaN(iRetailPriceA) && iRetailPriceA != "" && iRetailPriceA != undefined)
            Obj.RetailPriceA = iRetailPriceA;
        else
            Obj.RetailPriceA = Obj.RetailPrice;

        if (!isNaN(iRetailPriceB) && iRetailPriceB != "" && iRetailPriceB != undefined)
            Obj.RetailPriceB = iRetailPriceB;
        else
            Obj.RetailPriceB = Obj.RetailPrice;

        if (!isNaN(iRetailPriceC) && iRetailPriceC != "" && iRetailPriceC != undefined)
            Obj.RetailPriceC = iRetailPriceC;
        else
            Obj.RetailPriceC = Obj.RetailPrice;

        if (!isNaN(iWholeSaleMargin) && iWholeSaleMargin != "" && iWholeSaleMargin != undefined)
            Obj.WholeSaleMargin = iWholeSaleMargin;
        else
            Obj.WholeSaleMargin = 0;

        if (!isNaN(iMinDiscountPercent) && iMinDiscountPercent != "" && iMinDiscountPercent != undefined)
            Obj.MinDiscountPercent = iMinDiscountPercent;
        else
            Obj.MinDiscountPercent = 0;

        if (!isNaN(iMaxDiscountPercent) && iMaxDiscountPercent != "" && iMaxDiscountPercent != undefined)
            Obj.MaxDiscountPercent = iMaxDiscountPercent;
        else
            Obj.MaxDiscountPercent = 0;

        Obj.StatusFlag = "I";

        SaveandUpdatePricing(Obj);

    }
    $.jGrowl("Saved Successfully", { sticky: false, theme: 'success', life: jGrowlLife });

    return false;
});

function SaveandUpdatePricing(Obj) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/AddPricing",
        data: JSON.stringify({ Objdata: Obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {

                        // $("#btnList").click();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Pricing_A_01") {
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
