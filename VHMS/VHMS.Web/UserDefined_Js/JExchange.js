var gMagazineData = [];
var gExchangeList = [];

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
    $("#divExchange").hide();
    // GetDescriptionList("ddlDescriptionName");
    $("#txtExchangeDate").attr("data-link-format", "dd/MM/yyyy");

    $("#txtExchangeDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });
    //var date = new Date()
    GetCategory("ddlCategory");
   // GetProduct("ddlProduct");
    GetTaxList("ddlTax");

    //if ($("#hdnSalesID").val() >0) {
        //    pLoadingSetup(true);
        //    $("#btnAddNew").click();
        //    GetSalesDetails($("#hdnSalesID").val());
        //    $("#InvoiceNo").attr("disabled", true);

        //}
        //SetSessionValue("SalesID", 0);

    pLoadingSetup(true);
    GetRecord();

});
function GetSalesDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesByID",
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
                            
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#hdnCustomerID").val(obj.Customer.CustomerID);
                            $("#txtPhone").val(obj.Customer.MobileNo);
                            $("#txtAddress").val(obj.Customer.Address);
                            $("#hdnSalesID").val(obj.SalesID);
                            $("#txtInvoiceNo").val(obj.InvoiceNo);
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
function GetProduct(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    var CatID = $("#ddlCategory").val();
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductID",
        data: JSON.stringify({ CategoryID: CatID }),
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
                                    $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
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
function GetCategory(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCategory",
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
                                    $(sControlName).append("<option value='" + obj[index].CategoryID + "'>" + obj[index].CategoryName + "</option>");
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
function Edit_ExchangeDetail(ID) {
    Bind_ExchangeByID(ID, gExchangeList);
    return false;
}
$("#ddlCategory").change(function () {
    GetProduct("ddlProduct");
    var CatID = $("#ddlCategory").val();
    if (CatID == 2) {
        $("#ddlCategory").val();
        $("#txtKarat").val('-');
        $("#divKarat").hide();
    }
    else
        $("#divKarat").show();
});
$("#txtInvoiceNo").blur(function () {
    if ($("#txtInvoiceNo").val().length > 0) {
        GetCustomerDetails($("#txtInvoiceNo").val());
    }
});

$("#txtNetWeight,#txtMeltingWeight,#txtStoneWeight,#txtCurrentRate").change(function () {
    CalculateTrans();
});
function CalculateTrans()
{
    var iNetWt = 0;
    var iGrossWt = 0;
    var iMeltWt = 0;
    var iRate = 0;
    var iAmt = 0;
    var iStoneAmt = 0;
    if ($("#txtNetWeight").val() > 0)
        iNetWt = $("#txtNetWeight").val();    
    if ($("#txtMeltingWeight").val() > 0)
        iMeltWt = $("#txtMeltingWeight").val();
    if ($("#txtCurrentRate").val() > 0)
        iRate = $("#txtCurrentRate").val();
    if ($("#txtStoneWeight").val() > 0)
        iStoneAmt = $("#txtStoneWeight").val();

    iGrossWt = iNetWt - iMeltWt - iStoneAmt;
    iAmt = iGrossWt * iRate;

    $("#txtGrossWeight").val(iGrossWt);    
    $("#txtTotal").val(parseFloat(iAmt).toFixed(2));

}
function GetCustomerDetails(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesByInvoice",
        data: JSON.stringify({ InvoiceNo: id }),
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
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#hdnCustomerID").val(obj.Customer.CustomerID);
                            $("#txtPhone").val(obj.Customer.MobileNo);
                            $("#txtAddress").val(obj.Customer.Address);
                            $("#hdnSalesID").val(obj.SalesID);
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

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnExchangeID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").hide();
    $("#divExchange").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();

    gExchangeList = [];
    ClearExchangeTab();
    //$('#txtExchangeDate').datepicker('setDate', 'today');
    //ClearFields();
    $("#divExchangeList").empty();

    $("#txtExchangeDate").focus();
    $("#txtExchangeNo").focus();
    return false;
});

function PrintExchangeDetails() {
    SetSessionValue("AdmissionID", $("#hdnExchangeID").val());
    SetSessionValue("AdmissionNo", $("#hdnAdmissionNo").val());
    window.open("RepViewExchangeDetails.aspx", "_blank");
    return false;
}

$("#btnPrint").click(function () {

    SetSessionValue("ExchangeID", $("#hdnExchangeID").val());

    var myWindow = window.open("PrintOPBill.aspx", "MsgWindow");
    // PrintExchangeDetails();
});

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divExchange").hide();

    GetRecord();
    return false;
});
$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnExchangeID").val("0");
    gExchangeList = [];
    ClearExchangeTab();
    $("#btnList").click();
    return false;
});
function ClearExchangeTab() {
    $("#txtExchangeNo").val("");
    $("#txtExchangeDate").val("");
    $("#hdnCustomerID").val("");
    $("#txtSubtotal").val("0");
    $("#txtDiscount").val("0");
    $("#txtTotalAmount").val("0");
    $("#txtName").val("");
    $("#txtOPDNo").val("");
    gExchangeList = [];
    $("#btnSave").show();
    $("#btnUpdate").hide();

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();
    $("#txtExchangeDate").val(d + "/" + m + "/" + y);
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


Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#btnAddMagazine,#btnUpdateMagazine").click(function () {

    //if ($("#hdntransID").val() == "0" || $("#hdntransID").val() == undefined || $("#hdntransID").val() == null) {
    //    $.jGrowl("Please Enter Product", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divBarcode").addClass('has-error'); $("#txtBarcode").focus(); return false;
    //}
    //else { $("#divBarcode").removeClass('has-error'); }

    //if ($("#txtAmount").val() == "" || $("#txtAmount").val() == undefined || $("#txtAmount").val() == null) {
    //    $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
    //} else { $("#divAmount").removeClass('has-error'); }

    if ($("#ddlCategory").val() == "0" || $("#ddlCategory").val() == undefined || $("#ddlCategory").val() == null) {
        $.jGrowl("Please select Category", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCategory").addClass('has-error'); $("#ddlCategory").focus(); return false;
    } else { $("#divCategory").removeClass('has-error'); }

    if ($("#ddlProduct").val() == "0" || $("#ddlProduct").val() == undefined || $("#ddlProduct").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divProduct").addClass('has-error'); $("#ddlProduct").focus(); return false;
    } else { $("#divProduct").removeClass('has-error'); }

    if ($("#txtNetWeight").val().trim() == "" || $("#txtNetWeight").val().trim() == undefined) {
        $.jGrowl("Please enter Net Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divNetWeight").addClass('has-error'); $("#txtNetWeight").focus(); return false;
    } else { $("#divNetWeight").removeClass('has-error'); }

    if ($("#txtKarat").val().trim() == "" || $("#txtKarat").val().trim() == undefined) {
        $.jGrowl("Please enter Karat", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divKarat").addClass('has-error'); $("#txtKarat").focus(); return false;
    } else { $("#divKarat").removeClass('has-error'); }

    if ($("#txtMeltingWeight").val().trim() == "" || $("#txtMeltingWeight").val().trim() == undefined) {
        $.jGrowl("Please enter Melting Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divMeltingWeight").addClass('has-error'); $("#txtMeltingWeight").focus(); return false;
    } else { $("#divMeltingWeight").removeClass('has-error'); }

    if ($("#txtStoneWeight").val().trim() == "" || $("#txtStoneWeight").val().trim() == undefined) {
        $.jGrowl("Please enter Stone Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divStoneWeight").addClass('has-error'); $("#txtStoneWeight").focus(); return false;
    } else { $("#divStoneWeight").removeClass('has-error'); }

    if ($("#txtCurrentRate").val().trim() == "" || $("#txtCurrentRate").val().trim() == undefined) {
        $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCurrentRate").addClass('has-error'); $("#txtCurrentRate").focus(); return false;
    } else { $("#divCurrentRate").removeClass('has-error'); }



    var ObjData = new Object();
    ObjData.ExchangeTransID = 0;
    var ObjProduct = new Object();
    ObjProduct.ProductID = $("#ddlProduct").val();
    ObjProduct.ProductName = $("#ddlProduct option:selected").text();
    ObjData.Product = ObjProduct;

    var ObjCategory = new Object();
    ObjCategory.CategoryID = $("#ddlCategory").val();
    ObjCategory.CategoryName = $("#ddlCategory option:selected").text();
    ObjData.Category = ObjCategory;

    ObjData.Karat = $("#txtKarat").val();
    ObjData.CurrentRate = parseFloat($("#txtCurrentRate").val());
    ObjData.GrossWeight = parseFloat($("#txtGrossWeight").val());
    ObjData.StoneWeight = parseFloat($("#txtStoneWeight").val());
    ObjData.MeltingWeight = parseFloat($("#txtMeltingWeight").val());
    ObjData.NetWeight = parseFloat($("#txtNetWeight").val());
    ObjData.Amount = parseFloat($("#txtTotal").val());
    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gExchangeList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.ExchangeTransID = 0;
        ObjData.StatusFlag = "I";
        AddExchangeData(ObjData);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnExchangeID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.ExchangeID = $("#hdnExchangeID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.ExchangeID = 0;
        }
        Update_Exchange(ObjData);
    }
    CalculateAmount();
   ClearExchangeFields();
    $("#ddlddlCategory").focus();
    //$("#btnAddMagazine").show();
    //$("#btnUpdateMagazine").hide();
});
function ClearExchangeFields() {
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();

    $("#txtTotal").val("0");
    $("#txtCurrentRate").val("0");
    $("#txtGrossWeight").val("0");
    $("#txtMeltingWeight").val("0");
    $("#txtStoneWeight").val("0");
    $("#txtKarat").val("");
    $("#txtNetWeight").val("");
    $("#ddlCategory").val(0).change();
    $("#ddlProduct").val(0).change();
    $("#ddlProduct").empty();
    $("#divCategory").removeClass('has-error');
    $("#divProduct").removeClass('has-error');
    $("#divNetWeight").removeClass('has-error');
    $("#divKarat").removeClass('has-error');
    $("#divMeltingWeight").removeClass('has-error');
    $("#txtCurrentRate").removeClass('has-error');
    return false;
}
function AddExchangeData(oData) {
    gExchangeList.push(oData);
    DisplayExchangeList(gExchangeList);
    return false;
}
function DisplayExchangeList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 5)
    { $("#divExchangeList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
    else
    { $("#divExchangeList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblExchangeList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Category</th>";
        sTable += "<th class='" + sColorCode + "'>Product</th>";
        sTable += "<th class='" + sColorCode + "'>Net Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Stone Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Melting Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Gross Weight</th>";
        sTable += "<th class='" + sColorCode + "'>Karat</th>";
        sTable += "<th class='" + sColorCode + "'>Current Rate</th>";
        sTable += "<th class='" + sColorCode + "'>Amount</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblExchangeList_body'>";
        sTable += "</tbody></table>";
        $("#divExchangeList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                sTable += "<td>" + gData[i].Category.CategoryName + "</td>";
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].NetWeight + "</td>";
                sTable += "<td>" + gData[i].StoneWeight + "</td>";
                sTable += "<td>" + gData[i].MeltingWeight + "</td>";
                sTable += "<td>" + gData[i].GrossWeight + "</td>";
                sTable += "<td>" + gData[i].Karat + "</td>";
                sTable += "<td>" + gData[i].CurrentRate + "</td>";
                sTable += "<td>" + gData[i].Amount + "</td>";

                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_ExchangeDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_ExchangeDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblExchangeList_body").append(sTable);
            }
        }
    }
    else { $("#divExchangeList").empty(); }

    return false;
}
function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gExchangeList);
    return false;
}
function Bind_ExchangeByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlDescriptionName").focus();

    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            //$("#hdnOPSNo").val = null;
            $("#hdnOPSNo").val(ID);
            $("#hdnExchangeID").val(data[i].ExchangeID);
            //$("#ddlDescriptionName").val(data[i].Description.DescriptionID).change();
           
            $("#ddlCategory").val(data[i].Category.CategoryID).change();
            $("#ddlProduct").val(data[i].Product.ProductID).change();
            $("#txtKarat").val(data[i].Karat);
            $("#txtGrossWeight").val(data[i].GrossWeight);
            $("#txtStoneWeight").val(data[i].StoneWeight);
            $("#txtCurrentRate").val(data[i].CurrentRate);
            $("#txtMeltingWeight").val(data[i].MeltingWeight);
            $("#txtNetWeight").val(data[i].NetWeight);
            $("#txtTotal").val(data[i].Amount);
        }
    }
    return false;
}
function Update_Exchange(oData) {
    for (var i = 0; i < gExchangeList.length; i++) {
        if (gExchangeList[i].sNO == oData.sNO) {
            gExchangeList[i].ExchangeID = oData.ExchangeID;
            gExchangeList[i].ProductName = oData.Product.ProductName;
            gExchangeList[i].CategoryName = oData.Category.CategoryName;
            gExchangeList[i].ProductID = oData.Product.ProductID;
            gExchangeList[i].CategoryID = oData.Category.CategoryID;
            gExchangeList[i].Subtotal = oData.Subtotal;
            gExchangeList[i].StatusFlag = oData.StatusFlag;
            gExchangeList[i].NetWeight = oData.NetWeight;
            gExchangeList[i].Karat = oData.Karat;
            gExchangeList[i].MeltingWeight = oData.MeltingWeight;
            gExchangeList[i].StoneWeight = oData.StoneWeight;
            gExchangeList[i].GrossWeight = oData.GrossWeight;
            gExchangeList[i].CurrentRate = oData.CurrentRate;
            gExchangeList[i].Amount = oData.Amount;
        }
    }
    DisplayExchangeList(gExchangeList);
    $("#btnAddExchange").show();
    $("#btnUpdateExchange").hide();
    ClearExchangeFields();
    $("#txtBarcode").focus();
    return false;
}
function Delete_ExchangeDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gExchangeList.length; i++) {
            if (gExchangeList[i].SNo == ID) {
                var index = jQuery.inArray(gExchangeList[i].valueOf("SNo"), gExchangeList);
                if (gExchangeList[i].SNo > 0) {
                    gExchangeList[i].StatusFlag = "D";
                } else {
                    gExchangeList.splice(index, 1);
                }
                $("#divExchangeList").empty();
                DisplayExchangeList(gExchangeList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function CalculateNetPrice() {
    var iAmountPrice = parseFloat($("#txtAmount").val());


    if (isNaN(iAmountPrice)) iNoofCopies = 0;

    //var iDiscountAmount = parseFloat($("#txtSubtotal").val()) + parseFloat(iAmountPrice);
    //var iNetPrice = parseFloat(iCoverPrice * iNoofCopies) - parseFloat(iDiscountAmount * iNoofCopies);

    // $("#txtSubtotal").val(iNetPrice.toFixed(2));
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
        url: "WebServices/VHMSService.svc/GetTopExchange",
        data: JSON.stringify({ CustomerID: 0 }),
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

                                var table = "<tr id='" + obj[index].ExchangeID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ExchangeNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sExchangeDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Doctor.DoctorName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].NetAmount + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='PrintExchange' title='Click here to Print Exchange Invoice'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintExchange").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnExchangeID").val(AdmissionID);
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("ExchangeID", AdmissionID);

                                var myWindow = window.open("PrintExchangeInvoice.aspx", "MsgWindow");
                                //PrintExchangeDetails();
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
        url: "WebServices/VHMSService.svc/SearchExchange",
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

                                var table = "<tr id='" + obj[index].ExchangeID + "'>";
                                table += "<td>" + (index + 1) + "</td>";
                                table += "<td>" + obj[index].ExchangeNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].sExchangeDate + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Doctor.DoctorName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Customer.MobileNo + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].Branch.BranchName + "</td>";
                                table += "<td class='hidden-xs'>" + obj[index].NetAmount + "</td>";
                                //table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";


                                if (ActionView == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionUpdate == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                else
                                { table += "<td></td>"; }

                                if (ActionDelete == "1")
                                { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                else
                                { table += "<td></td>"; }
                                table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ExchangeID + " class='PrintExchange' title='Click here to Print Exchange Invoice'></i><i class='fa fa-print text-green'/></a></td>";

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
                            $(".PrintExchange").click(function () {
                                var AdmissionID = $(this).attr('id');
                                $("#hdnExchangeID").val(AdmissionID);
                                var JobCardID = parseInt($(this).parent().parent()[0].id);
                                SetSessionValue("ExchangeID", AdmissionID);

                                var myWindow = window.open("PrintExchangeInvoice.aspx", "MsgWindow");
                                //PrintExchangeDetails();
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
    var iExchangeAmount = 0
    var iTotal =0
    for (var i = 0; i < gExchangeList.length; i++) {
        if (gExchangeList[i].StatusFlag != "D")
            iExchangeAmount = iExchangeAmount + parseFloat(gExchangeList[i].Amount);
    }
    $("#txtSubtotal").val(iExchangeAmount);

    //if ($("#txtDiscountPercent").val() > 0)
    //    $("#txtDiscount").val((iExchangeAmount * parseFloat($("#txtDiscountPercent").val()) / 100).toFixed(2));
    //iExchangeAmount = iExchangeAmount - parseFloat($("#txtDiscount").val());
    
    $("#txtTaxAmount").val((iExchangeAmount * parseFloat($("#txtTaxPercent").text()) / 100).toFixed(2));
    $("#txtCGSTAmount").val((iExchangeAmount * parseFloat($("#txtCGSTPercent").text()) / 100).toFixed(2));
    $("#txtSGSTAmount").val((iExchangeAmount * parseFloat($("#txtSGSTPercent").text()) / 100).toFixed(2));
    $("#txtIGSTAmount").val((iExchangeAmount * parseFloat($("#txtIGSTPercent").text()) / 100).toFixed(2));
    iExchangeAmount = iExchangeAmount - parseFloat($("#txtTaxAmount").val());

    //if ($("#txtDiscount").val() > 0)
    //    $("#txtTotalAmount").val((parseFloat($("#txtSubtotal").val()) - parseFloat($("#txtDiscount").val())).toFixed(2));
    //else
    $("#txtTotalAmount").val(parseFloat(iExchangeAmount).toFixed(2));
}

$("#btnSave,#btnUpdate").click(function () {
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }

    //if ($("#txtExchangeNo").val().trim() == "" || $("#txtExchangeNo").val().trim() == undefined) {
    //    $.jGrowl("Please enter Exchange No", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divBillNo").addClass('has-error'); $("#txtExchangeNo").focus(); return false;
    //}
    //else { $("#divBillNo").removeClass('has-error'); }

    if ($("#txtExchangeDate").val().trim() == "" || $("#txtExchangeDate").val().trim() == undefined) {
        $.jGrowl("Please select Exchange Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divExchangeDate").addClass('has-error'); $("#txtExchangeDate").focus(); return false;
    }
    else { $("#divExchangeDate").removeClass('has-error'); }

    if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
        $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divtax").addClass('has-error'); $("#ddlTax").focus(); return false;
    } else { $("#divtax").removeClass('has-error'); }



    if ($("#hdnCustomerID").val() == "0" || $("#txtName").val() == undefined || $("#txtName").val() == null || $("#txtName").val().trim() == "") {
        $.jGrowl("Please Enter Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomer").addClass('has-error'); $("#txtOPDNo").focus(); return false;
    }
    else { $("#divCustomer").removeClass('has-error'); }

    if (gExchangeList.length <= 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    var gCount = 0;
    for (var i = 0; i < gExchangeList.length; i++) {
        if (gExchangeList[i].StatusFlag != "D")
            gCount++;

    }
    if (gCount == 0) {
        $.jGrowl("No description has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtBarcode").focus(); return false;
    }

    var iExchangeAmount = 0;
    for (var i = 0; i < gExchangeList.length; i++)
        iExchangeAmount = iExchangeAmount + parseFloat(gExchangeList[i].Subtotal);

    var ObjExchange = new Object();
    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#hdnCustomerID").val();
    ObjExchange.Customer = ObjCustomer;

    var Objtax = new Object();
    Objtax.TaxID = $("#ddlTax").val();
    ObjExchange.Tax = Objtax;

   
    ObjExchange.ExchangeID = 0;
    ObjExchange.SalesID = $("#hdnSalesID").val();
    ObjExchange.ExchangeNo = $("#txtExchangeNo").val().trim();
    ObjExchange.sExchangeDate = $("#txtExchangeDate").val().trim();
   
    ObjExchange.TotalAmount = parseFloat($("#txtSubtotal").val());
    ObjExchange.NetAmount = parseFloat($("#txtTotalAmount").val());
    ObjExchange.TaxAmount = parseFloat($("#txtTaxAmount").val());
    ObjExchange.TaxPercent = parseFloat($("#txtTaxPercent").text());
    ObjExchange.CGSTAmount = parseFloat($("#txtCGSTAmount").val());
    ObjExchange.SGSTAmount = parseFloat($("#txtSGSTAmount").val());
    ObjExchange.IGSTAmount = parseFloat($("#txtIGSTAmount").val());
    
    ObjExchange.ExchangeTrans = gExchangeList;
    //ObjExchange.ExchangeID = $("#hdnExchangeID").val();

    if ($("#hdnExchangeID").val() > 0) {
        ObjExchange.ExchangeID = $("#hdnExchangeID").val();
        sMethodName = "UpdateExchange";
    }
    else {
        sMethodName = "AddExchange";
        ObjExchange.ExchangeID = 0
    }

    SaveandUpdateExchange(ObjExchange, sMethodName);

});
function SaveandUpdateExchange(ObjExchange, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjExchange }),
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
                        $("#divExchange").hide();
                        if (sMethodName == "AddExchange") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnExchangeID").val(objResponse.Value);
                        }
                        else if (sMethodName == "UpdateExchange")
                        { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                        $("#btnList").click();
                        SetSessionValue("ExchangeID", $("#hdnExchangeID").val());
                        var myWindow = window.open("PrintExchangeInvoice.aspx", "MsgWindow");
                        $("#hdnExchangeID").val("0");
                        ClearExchangeTab();
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
        url: "WebServices/VHMSService.svc/GetExchangeByID",
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

                            $("#txtExchangeNo").attr("disabled", true);
                            // $("#ddlCustomer").attr("disabled", true);
                            $("#hdnSalesID").val(obj.SalesID)
                            $("#hdnExchangeID").val(obj.ExchangeID)
                            $("#hdnCustomerID").val(obj.Customer.CustomerID).change();
                            $("#ddlTax").val(obj.Tax.TaxID).change();
                            $("#txtExchangeNo").val(obj.ExchangeNo);
                            $("#txtExchangeDate").val(obj.sExchangeDate);
                            $("#txtSubtotal").val(obj.TotalAmount);
                            $("#txtTotalAmount").val(obj.NetAmount);
                            $("#txtTaxAmount").val(obj.TaxAmount);
                            $("#txtTaxPercent").text(obj.TaxPercent);
                            $("#txtCGSTAmount").val(obj.CGSTAmount);
                            $("#txtSGSTAmount").val(obj.SGSTAmount);
                            $("#txtIGSTAmount").val(obj.IGSTAmount);
                            $("#txtName").val(obj.Customer.CustomerName);
                            $("#txtPhone").val(obj.Customer.MobileNo);
                            $("#txtAddress").val(obj.Customer.Address);
                            $("#txtInvoiceNo").val(obj.InvoiceNo);

                            gExchangeList = [];
                            var ObjProduct = obj.ExchangeTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "";
                                var objCategory = new Object();
                                objCategory.CategoryID = ObjProduct[index].Category.CategoryID
                                objCategory.CategoryName = ObjProduct[index].Category.CategoryName;
                                objTemp.Category = objCategory;
                                var objProduct = new Object();
                                objProduct.ProductID = ObjProduct[index].Product.ProductID
                                objProduct.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.Product = objProduct;

                                objTemp.ExchangeID = ObjProduct[index].ExchangeID;
                                objTemp.ExchangeTransID = ObjProduct[index].ExchangeTransID;
                                objTemp.NetWeight = ObjProduct[index].NetWeight;
                                objTemp.StoneWeight = ObjProduct[index].StoneWeight;
                                objTemp.MeltingWeight = ObjProduct[index].MeltingWeight;
                                objTemp.GrossWeight = ObjProduct[index].GrossWeight;
                                objTemp.CurrentRate = ObjProduct[index].CurrentRate;
                                objTemp.Amount = ObjProduct[index].Amount;
                                objTemp.Karat = ObjProduct[index].Karat;
                                AddExchangeData(objTemp);
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
        url: "WebServices/VHMSService.svc/DeleteExchange",
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
                    else if (objResponse.Value == "Exchange_R_01" || objResponse.Value == "Exchange_D_01") {
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
