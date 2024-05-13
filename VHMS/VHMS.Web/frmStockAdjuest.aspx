<%@ Page Title="Stock Adjust" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmStockAdjuest.aspx.cs" Inherits="frmStockAdjuest" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <%-- <style>
        .modal-lg {
            width: 446px !important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Stock Adjust
            </h1>
            <div class="form-group  col-md-2" style="margin-left: 148px; margin-top: -34px;">
                <label>
                    Category Name</label>
                <select id="ddlCategoryName" class="form-control select2" data-placeholder="Select Category Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: -5px; margin-top: -34px;">
                <label>
                    SubCategory Name</label>
                <select id="ddlSubCategoryName" class="form-control select2" data-placeholder="Select SubCategory Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 10px; margin-top: -34px;">
                <label>
                    Product Name</label>
                <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 61%; margin-top: -74px;">
                <label>
                    Supplier Name</label>
                <select id="ddlSupplierName" class="form-control select2" data-placeholder="Select Supplier Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 966px; margin-top: -75px; display: none;">
                <label>
                    Product Type</label>
                <select id="ddlType" class="form-control select2" data-placeholder="Select Product Type" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 67%; margin-top: -48px;">
                <button type="button" style="background-color: #000000 !important;" class="btn btn-danger pull-right" id="btnView" tabindex="19">
                    View</button>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Stock Adjust</li>
            </ol>
            <div class="pull-right" style="margin-top: -33px;">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Entry Date
                                            </th>
                                            <th>Barcode
                                            </th>
                                            <th>SMS Code
                                            </th>
                                            <th>Product Code
                                            </th>
                                            <th class="hidden-xs">Product Name
                                            </th>
                                            <th class="hidden-xs">Updated Qty
                                            </th>
                                            <th class="hidden-xs">P.Price
                                            </th>
                                            <th class="hidden-xs">W.S.Margin
                                            </th>
                                            <th class="hidden-xs">W.S.Price
                                            </th>
                                            <th class="hidden-xs">R.S.Margin
                                            </th>
                                            <th class="hidden-xs">R.S.Price
                                            </th>
                                            <th>Print
                                            </th>
                                            <%--<th>Edit
                                            </th>
                                            <th>Delete
                                            </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">                              
                                <div class="form-group col-md-3" id="divCode">
                                    <label>
                                        Search SMS/Party Code</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtCode" style="text-transform: uppercase" placeholder="Please enter Code"
                                        maxlength="15" tabindex="1" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-5" id="divProductName">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="2"></select>
                                </div>
                                <div class="form-group col-md-2" id="divSMSCode">
                                    <label>
                                        SMS Code</label>
                                    <input type="text" class="form-control" id="txtSMSCode" placeholder="SMS Code"
                                        maxlength="15" tabindex="3" autocomplete="off" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divPartyCode">
                                    <label>
                                        Party Code</label>
                                    <input type="text" class="form-control" id="txtPartyCode" placeholder="Party Code"
                                        maxlength="15" tabindex="4" autocomplete="off" readonly="true" />
                                </div>
                                 
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divBarCode">
                                    <label>
                                        Barcode</label>
                                    <input type="text" class="form-control" id="txtBarcode" placeholder="Barcode"
                                        maxlength="15" tabindex="5" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divAvailbleQty">
                                    <label>
                                        Available Qty</label>
                                    <input type="text" class="form-control" id="txtAvailbleQty" placeholder="Please enter HSN Code"
                                        maxlength="10" tabindex="6" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-3" id="divActualQty">
                                    <label>
                                        Actual Qty</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtActualQty" placeholder="Please enter ActualQty"
                                        maxlength="10" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divPurchasePrice">
                                    <label>
                                        Purchase Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtPurchasePrice" onkeypress="return IsNumeric(event)" placeholder="Please enter PurchasePrice"
                                        maxlength="150" tabindex="8" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divWholeSalesMargin">
                                    <label>
                                        WholeSales Margin</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtWholeSalesMargin" onkeypress="return IsNumeric(event)" placeholder="Please enter WholeSalesMargin"
                                        maxlength="150" tabindex="9" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divWholeSalesPrice">
                                    <label>
                                        WholeSales Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtWholeSalesPrice" onkeypress="return IsNumeric(event)" placeholder="Please enter WholeSalesPrice"
                                        maxlength="150" tabindex="10" autocomplete="off" />
                                </div>
                                <div class="form-group  col-md-3" id="divRetailSalesMargin">
                                    <label>
                                        RetailSales Margin</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtRetailSalesMargin" onkeypress="return IsNumeric(event)" placeholder="Please enter RetailSales Margin"
                                        maxlength="15" tabindex="11" autocomplete="off" />
                                </div>
                                <div class="form-group  col-md-3" id="divRetailSalesPrice">
                                    <label>
                                        RetailSales Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtRetailSalesPrice" onkeypress="return IsNumeric(event)" placeholder="Please enter RetailSalesPrice"
                                        maxlength="150" tabindex="12" autocomplete="off" />
                                </div>
                                 <div class="form-group col-md-6" id="divProductType" style="margin-top: 5px">
                                    <input type="radio" name="SupplierProduct" id="rdoAll" checked="checked" value="A" tabindex="-1" />
                                    Summary Stock 
                                    <span style="padding-left: 30px" />
                                    <input type="radio" name="SupplierProduct" id="rdobarcode" value="B" tabindex="-1" />
                                    Barcode Stock
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="13">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="14">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="15">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <input type="hidden" id="hdnProductID" />
    <script type="text/javascript">

       // var pageUrl = '<%=ResolveUrl("~/frmProduct.aspx") %>';

        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }
            var Sup_Code = "";
            pLoadingSetup(false);
            pLoadingSetup(true);
            GetProductList("ddlProduct");
            $("#txtBarcode").attr("disabled", "disabled");
            GetSupplierName();
            GetCategoryName();
            GetSubCategoryName();
            GetRecord();


            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0].trim()));
            });

            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
                        data: "{ 'prefix': '" + request.term + "', 'SupplierID':0,'IsAll':'A'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {
                                console.log(item);
                                return {
                                    label: item,
                                    val: item
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },

                open: function () {
                    $("[id$=txtCode]").autocomplete("widget").css({
                        "width": ("800px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#ddlProductName").val(null).change();
            $("#hdnID").val("");
            $("#txtBarcode").val("");
            $("#txtPartyCode").val("");
            $("#txtSMSCode").val("");
            $("#rdoAll").prop('checked', true);
            $("#rdobarcode").prop('checked', false);
            $("#txtBarcode").attr("disabled", "disabled");
            $("#txtCode").val("");
            $("#txtAvailbleQty").val(0);
            $("#txtActualQty").val(0);
            $("#txtPurchasePrice").val(0);
            $("#txtWholeSalesMargin").val(0);
            $("#txtWholeSalesPrice").val(0);
            $("#txtRetailSalesMargin").val(0);
            $("#txtRetailSalesPrice").val(0);
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Product");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtCode").focus();
            return false;
        });

        $("#txtCode").blur(function () {
            if ($("#txtCode").val().trim().length > 3) {
                GetProductByCodeList("ddlProductName");
                GetBarcodeBYID();
                if ($("#ddlProductName").val() > 0) {
                    GetQuantity();
                    GetPricingID();
                    GetSMSCodeByID();
                }
            }
            else if ($("#txtCode").val().length == 0) {
                GetProductList("ddlProductName");
                GetBarcodeBYID();
                if ($("#ddlProductName").val() > 0) {
                    GetQuantity();
                    GetPricingID();
                    GetSMSCodeByID();
                }
            }
        });

        $("#ddlProductName").change(function () {
            if ($("#ddlProductName").val() > 0) {
                GetBarcodeBYID();
                GetQuantity();
                GetPricingID();
                GetSMSCodeByID();
            }
        });


        $("#txtBarcode").change(function () {
            $("#txtCode").val("");
            if ($("#txtBarcode").val().length > 4) {
                GetProductByBarcodeList("ddlProductName");
                if ($("#ddlProductName").val() > 0) {
                    GetBarcodeBYID();
                    GetQuantity();
                    GetPricingID();
                    GetSMSCodeByID();
                }
            }
        });

        $("input[type=radio]").change(function () {
            if ($("input[name=SupplierProduct]:checked").val() == "A") {
                $("#txtBarcode").attr("disabled", "disabled");
                if ($("#ddlProductName").val() > 0) {
                    GetQuantity();
                }
            }
            else {
                $("#txtBarcode").removeAttr("disabled");
                if ($("#ddlProductName").val() > 0) 
                    GetQuantity();
                }
        });


        function GetSupplierName() {
            $("#ddlSupplierName").empty();
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
                                    $("#ddlSupplierName").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSupplierName").append('<option value=' + obj[index].SupplierID + ' >' + obj[index].SupplierName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlSupplierName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlSupplierName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetCategoryName() {
            $("#ddlCategoryName").empty();
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
                                    $("#ddlCategoryName").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlCategoryName").append('<option value=' + obj[index].CategoryID + ' >' + obj[index].CategoryName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCategoryName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCategoryName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetSubCategoryName() {

            $("#ddlSubCategoryName").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSubCategoryByCategoryID",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ID: $("#ddlCategoryName").val() }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#ddlSubCategoryName").append('<option value="' + '0' + '">' + '--All--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSubCategoryName").append('<option value=' + obj[index].SubCategoryID + ' >' + obj[index].SubCategoryName + '</option>');
                                    }
                                    $("#ddlSubCategoryName").val(0).change();
                                }

                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlSubCategoryName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlSubCategoryName").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetProductByBarcodeList(ddlname) {
            if ($("#txtBarcode").val().length > 4) {
                var sControlName = "#" + ddlname;
                dProgress(true);
                $(sControlName).empty();
                $.ajax({
                    type: "POST",
                    url: "WebServices/VHMSService.svc/GetProductByBarcode",
                    data: JSON.stringify({ ProductCode: $("#txtBarcode").val(), SMSOnly: 0 }),
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
                                                $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                        }
                                        $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
                                    }

                                }
                                else if (objResponse.Value == "NoRecord") {
                                    ClearFields();
                                    $(sControlName).append('<option value="' + '0' + '">' + '--No Recsords--' + '</option>');
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
        }


        function GetPricingID() {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetPricingByProductName",
                data: JSON.stringify({ ID: $("#ddlProductName").val() }),
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
                                    $("#txtPurchasePrice").val(obj.PurchasePrice);
                                    $("#txtRetailSalesMargin").val(obj.RetailMargin);
                                    $("#txtWholeSalesMargin").val(obj.WholeSaleMargin);
                                    $("#txtRetailSalesPrice").val(obj.RetailPrice);
                                    $("#txtWholeSalesPrice").val(obj.WholeSalePrice);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
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

        function GetSMSCodeByID() {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductByID",
                data: JSON.stringify({ ID: $("#ddlProductName").val() }),
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
                                    $("#txtSMSCode").val(obj.SMSCode);                                  
                                    $("#txtPartyCode").val(obj.ProductCode);
                                    //if ($("#txtCode").val().length == 0)
                                    //    $("#txtCode").val(obj.SMSCode);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
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

        function GetBarcodeBYID() {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetBarcodeBystock",
                data: JSON.stringify({ ID: $("#ddlProductName").val(), iBarcode: $("#txtBarcode").val() }),
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
                                    $("#txtBarcode").val(obj.Barcode);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
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

        function GetQuantity(ddlname) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetStockBarcodeByID",
                data: JSON.stringify({ ID: $("#ddlProductName").val(), iBarcode: $("#txtBarcode").val(), itype: $("input[name=SupplierProduct]:checked").val() }),
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
                                    $("#txtAvailbleQty").val(obj.Quantity);
                                    $("#txtActualQty").val(obj.Quantity);
                                    StockValue = obj.Quantity;
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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

        function GetProductList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductList",
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
                                    $(sControlName).append('<option value="' + '0' + '">' + '--All--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive) {
                                            $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                        }
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

        //$("#ddlCategoryName").change(function () {
        //    //  GetSubCategoryName();
        //    GetRecord();
        //});

        ////$("#ddlSubCategoryName").change(function () {
        ////    if ($('ul#navbars li.active').attr("id") == "iGeneral")
        ////        GetRecord();
        ////    else
        ////        $("#txtSearchName").change();
        ////});

        //$("#ddlProduct").change(function () {
        //    GetRecord();
        //});

        //$("#ddlSupplierName").change(function () {
        //    GetRecord();
        //});

        $("#btnView").click(function () {
            GetRecord();
        });

        function GetProductByCodeList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductByCodeByID",
                data: JSON.stringify({ ProductCode: $("#txtCode").val().trim(), SMSOnly: 0 }),
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
                                            $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                        }
                                    }
                                    $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
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
        $("#txtRetailSalesMargin").change(function () {
            calculateRetail();
        });

        $("#txtWholeSalesMargin").change(function () {
            calculateWholeSalesPrice();
        });

        function calculateRetail() {
            var iDiscountPer = $("#txtRetailSalesMargin").val();

            var iPurchasePrice = $("#txtPurchasePrice").val();

            if (isNaN(iDiscountPer)) iDiscountPer = 0;
            if (isNaN(iPurchasePrice)) iPurchasePrice = 0;

            var iDiscountAmount = parseFloat(iPurchasePrice * (iDiscountPer / 100));
            var iSellingPrice = parseFloat(iDiscountAmount) + parseFloat(iPurchasePrice);
            iSellingPrice = Math.ceil(iSellingPrice / 50) * 50;
            $("#txtRetailSalesPrice").val(iSellingPrice.toFixed(2));
        }


        function calculateWholeSalesPrice() {
            var iDiscountPer = $("#txtWholeSalesMargin").val();

            var iPurchasePrice = $("#txtPurchasePrice").val();

            if (isNaN(iDiscountPer)) iDiscountPer = 0;
            if (isNaN(iPurchasePrice)) iPurchasePrice = 0;

            var iDiscountAmount = parseFloat(iPurchasePrice * (iDiscountPer / 100));
            var iSellingPrice = parseFloat(iDiscountAmount) + parseFloat(iPurchasePrice);
            iSellingPrice = Math.ceil(iSellingPrice / 50) * 50;
            $("#txtWholeSalesPrice").val(iSellingPrice.toFixed(2));
        }

        $("#txtWholeSalesPrice").change(function () {
            calculateWholeSalesMargin();
        });

        function calculateWholeSalesMargin() {
            var iDiscountPer = $("#txtWholeSalesPrice").val();

            var iPurchasePrice = $("#txtPurchasePrice").val();

            if (isNaN(iDiscountPer)) iDiscountPer = 0;
            if (isNaN(iPurchasePrice)) iPurchasePrice = 0;

            var iDiscountAmount = parseFloat(iDiscountPer) - parseFloat(iPurchasePrice);
            var iSellingPrice = parseFloat(100 * (iDiscountAmount / iPurchasePrice));
            if (isNaN(iSellingPrice)) iSellingPrice = 0;
            iSellingPrice = Math.floor(iSellingPrice * 100) / 100;
            $("#txtWholeSalesMargin").val(iSellingPrice.toFixed(2));
            // $(".CalculateWholeSale").blur();
        }

        $("#txtRetailSalesPrice").change(function () {
            calculateRetailsMargin();
        });

        function calculateRetailsMargin() {
            var iDiscountPer = $("#txtRetailSalesPrice").val();;

            var iPurchasePrice = $("#txtPurchasePrice").val();

            if (isNaN(iDiscountPer)) iDiscountPer = 0;
            if (isNaN(iPurchasePrice)) iPurchasePrice = 0;

            var iDiscountAmount = parseFloat(iDiscountPer) - parseFloat(iPurchasePrice);
            var iSellingPrice = parseFloat(100 * (iDiscountAmount / iPurchasePrice));
            if (isNaN(iSellingPrice)) iSellingPrice = 0;
            iSellingPrice = Math.floor(iSellingPrice * 100) / 100;
            $("#txtRetailSalesMargin").val(iSellingPrice.toFixed(2));
            // $(".CalculateWholeSale").blur();
        }

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate") { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtSMSCode").val().trim() == "" || $("#txtSMSCode").val().trim() == undefined) {
                $.jGrowl("Please enter SMS Code", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSMSCode").addClass('has-error'); $("#txtSMSCode").focus(); return false;
            } else { $("#divSMSCode").removeClass('has-error'); }

            if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined) {
                $.jGrowl("Please Select Product Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divProductName  ").addClass('has-error'); $("#ddlProductName").focus(); return false;
            } else { $("#divProductName").removeClass('has-error'); }

            if ($("#txtActualQty").val() == "" || $("#txtActualQty").val() == undefined || $("#txtActualQty").val() == null) {
                $.jGrowl("Please enter Actual Qty", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divActualQty").addClass('has-error'); $("#txtActualQty").focus(); return false;
            } else { $("#divActualQty").removeClass('has-error'); }

            if ($("#txtPurchasePrice").val() == "" || $("#txtPurchasePrice").val() == undefined || $("#txtPurchasePrice").val() == null || $("#txtPurchasePrice").val() <= 0) {
                $.jGrowl("Please enter Purchase Price", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPurchasePrice").addClass('has-error'); $("#txtPurchasePrice").focus(); return false;
            } else { $("#divPurchasePrice").removeClass('has-error'); }

            if ($("#txtRetailSalesMargin").val() == "" || $("#txtRetailSalesMargin").val() == undefined || $("#txtRetailSalesMargin").val() == null || $("#txtRetailSalesMargin").val() <= 0) {
                $.jGrowl("Please enter Retails Sales Margin", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRetailSalesMargin").addClass('has-error'); $("#txtRetailSalesMargin").focus(); return false;
            } else { $("#divRetailSalesMargin").removeClass('has-error'); }

            if ($("#txtWholeSalesMargin").val() == "" || $("#txtWholeSalesMargin").val() == undefined || $("#txtWholeSalesMargin").val() == null || $("#txtWholeSalesMargin").val() <= 0) {
                $.jGrowl("Please enter Whole Sales Margin", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divWholeSalesMargin").addClass('has-error'); $("#txtWholeSalesMargin").focus(); return false;
            } else { $("#divWholeSalesMargin").removeClass('has-error'); }

            if ($("#txtRetailSalesPrice").val() == "" || $("#txtRetailSalesPrice").val() == undefined || $("#txtRetailSalesPrice").val() == null || $("#txtRetailSalesPrice").val() <= 0) {
                $.jGrowl("Please enter Retails Sales Price", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divRetailSalesPrice").addClass('has-error'); $("#txtRetailSalesPrice").focus(); return false;
            } else { $("#divRetailSalesPrice").removeClass('has-error'); }

            if ($("#txtWholeSalesPrice").val() == "" || $("#txtWholeSalesPrice").val() == undefined || $("#txtWholeSalesPrice").val() == null || $("#txtWholeSalesPrice").val() <= 0) {
                $.jGrowl("Please enter Whole Sales Price", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divWholeSalesPrice").addClass('has-error'); $("#txtWholeSalesPrice").focus(); return false;
            } else { $("#divWholeSalesPrice").removeClass('has-error'); }

            //var Code = $("#txtCode").val().toUpperCase();
            //var res = Code.replace("SMS", "").includes(Sup_Code);
            //if (res == false) {
            //    $.jGrowl("Please enter Correct SMS Code", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    return false;
            //}

            var Obj = new Object();

            var ObjProduct = new Object();
            ObjProduct.ProductID = $("#ddlProductName").val();
            ObjProduct.SMSCode = $("#txtCode").val();
            Obj.Product = ObjProduct;

            Obj.Barcode = $("#txtBarcode").val();
            Obj.SMSCode = $("#txtSMSCode").val();
            Obj.Available_Qty = $("#txtAvailbleQty").val();
            Obj.Updated_Qty = $("#txtActualQty").val();
            Obj.Purchase_Price = $("#txtPurchasePrice").val();
            Obj.RetailSalesMargin = $("#txtRetailSalesMargin").val();
            Obj.WholeSalesMargin = $("#txtWholeSalesMargin").val();
            Obj.RetailSalesPrice = $("#txtRetailSalesPrice").val();
            Obj.WholeSalesPrice = $("#txtWholeSalesPrice").val();
            Obj.AdjustType = $("input[name=SupplierProduct]:checked").val();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ProductID = $("#hdnID").val();
                sMethodName = "UpdateStockAdjuest";
            }
            else { sMethodName = "AddStockAdjuest"; }

            SaveandUpdateProduct(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtCode").val("");
            $("#txtBarcode").val("");
            $("#txtAvailbleQty").val("0");
            $("#ddlProductName").val(null).change();
            $("#txtActualQty").val("0");
            $("#txtPurchasePrice").val("0");
            $("#txtRetailSalesMargin").val("0");
            $("#txtWholeSalesMargin").val("0");
            $("#txtRetailSalesPrice").val("0");
            $("#txtWholeSalesPrice").val("0");

            GetProductList("ddlProductName");
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAllStockAdjust",
                data: JSON.stringify({ iProductID: $("#ddlProduct").val(), iCategoryID: $("#ddlCategoryName").val(), iSubCategoryID: $("#ddlSubCategoryName").val(), iSupplierID: $("#ddlSupplierName").val() }),
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
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].StockAdjustID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].sCreatedOn + "</td>";
                                        table += "<td>" + obj[index].Barcode + "</td>";
                                        table += "<td>" + obj[index].Product.SMSCode + "</td>";
                                        table += "<td>" + obj[index].Product.ProductCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Product.ProductName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Updated_Qty + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Purchase_Price + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WholeSalesMargin + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WholeSalesPrice + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].RetailSalesMargin + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].RetailSalesPrice + "</td>";

                                        //if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockAdjustID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        //else { table += "<td></td>"; }

                                        //if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockAdjustID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        //else { table += "<td></td>"; }

                                        //if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockAdjustID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        //else { table += "<td></td>"; }
                                        table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockAdjustID + " class='PrintPurchase' Barcode='" + obj[index].Barcode + "' BarcodeQty='" + obj[index].Updated_Qty + "' title='Click here to Print Purchase'></i><i class='fa fa-print text-green'/></a></td>";

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Product");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".PrintPurchase").click(function () {
                                        SetSessionValue("Barcode", $(this).attr('Barcode'));
                                        SetSessionValue("BarcodeQty", $(this).attr('BarcodeQty'));
                                        SetSessionValue("BarcodePurchaseID", $(this).attr('id'));
                                        SetSessionValue("ScreenName", "StockAdjuest");
                                        var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { DeleteRecord($(this).parent().parent()[0].id); }
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
                                    { "sWidth": "11%" },
                                    { "sWidth": "7%" },
                                    { "sWidth": "7%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "25%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "5%" }
                                    // { "sWidth": "3%" }
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

        function SaveandUpdateProduct(Obj, sMethodName) {
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
                                ClearFields();
                                GetRecord();

                                if (sMethodName == "AddStockAdjuest") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateStockAdjuest") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Product_A_01" || objResponse.Value == "Product_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "ProductC_A_01" || objResponse.Value == "ProductC_U_01") {
                                $.jGrowl("Party Code Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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

        $("#ddlSupplier").change(function () {
            if ($("#ddlSupplier").val() > 0) {
                SupplierCode($("#ddlSupplier").val());
            }
        });
    </script>
</asp:Content>


