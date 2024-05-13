<%@ Page Title="Product" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmProduct.aspx.cs" Inherits="frmProduct" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .modal {
            overflow-y: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Product
            </h1>
            <div class="form-group  col-md-2" style="margin-left: 79px; margin-top: -34px;">
                <label>
                    Category Name</label>
                <select id="ddlCategoryName" class="form-control select2" data-placeholder="Select Category Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: -22px; margin-top: -34px;">
                <label>
                    SubCategory Name</label>
                <select id="ddlSubCategoryName" class="form-control select2" data-placeholder="Select SubCategory Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: -23px; margin-top: -34px;">
                <label>
                    Product Name</label>
                <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 0%; margin-top: -34px;">
                <label>
                    Supplier Name</label>
                <select id="ddlSupplierName" class="form-control select2" data-placeholder="Select Supplier Name" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: -1%; margin-top: -35px;">
                <label>
                    Product Type</label>
                <select id="ddlType" class="form-control select2" data-placeholder="Select Product Type" tabindex="-1"></select>
            </div>
            <div class="form-group  col-md-2" style="margin-left: 72%; margin-top: -53px;">
                <button type="button" style="background-color: #000000 !important;" class="btn btn-danger pull-right" id="btnView" tabindex="19">
                    View</button>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Product</li>
            </ol>
            <div class="pull-right" style="margin-top: -33px;">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs" id="navbars">
                    <li id="iGeneral" class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                    <li id="iSearch"><a id="aSearchResult" href="#SearchResult" data-toggle="tab">Search Result</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>SNo</th>
                                                <th>Category</th>
                                                <th>Product</th>
                                                <th>Code</th>
                                                <th>Supplier Name</th>
                                                <th>Party Code</th>
                                                <th>Entry Date</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tblRecord_tbody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="SearchResult">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group col-md-4" id="divSearchaname">
                                    <label>
                                        Search records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter search details"
                                        maxlength="150" />
                                </div>
                                <div class="form-group col-md-8"></div>
                                <div class="form-group col-md-12">
                                    <div class="table-responsive">
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>SNo
                                                    </th>
                                                    <th>Category
                                                    </th>
                                                    <th>Product
                                                    </th>
                                                    <th class="hidden-xs">Code
                                                    </th>
                                                    <th class="hidden-xs">Supplier Name
                                                    </th>
                                                    <th class="hidden-xs">Party Code
                                                    </th>
                                                    <th class="hidden-xs">Entry Date
                                                    </th>
                                                    <th class="hidden-xs">Status
                                                    </th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblSearchResult_tbody">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
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
                                <div class="form-group col-md-3" id="divName">
                                    <label>
                                        SMS Product Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Please enter Product Name"
                                        maxlength="150" tabindex="1" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divCode">
                                    <label>
                                        SMS Code</label>

                                    <%--<button type="button" class="btn btn-danger pull-right" id="btnLink" tabindex="-1" runat="server">
                                        Gener. SMSCode</button>--%>
                                    <input type="text" class="form-control" id="txtCode" style="text-transform: uppercase;" placeholder="Please enter Code"
                                        maxlength="15" tabindex="2" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3 " id="divHSNCode">
                                    <label>
                                        HSN Code</label>
                                <select id="ddlHSNCode" class="form-control select2" data-placeholder="Select HSNCode" tabindex="3">
                                    </select>
                                </div>
                                <div class="form-group  col-md-3" id="divSupplier">
                                    <label>
                                        Supplier</label><span class="text-danger">*</span>
                                    <select id="ddlSupplier" class="form-control select2" data-placeholder="Select Supplier" tabindex="4">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divSupplierProductName">
                                    <label>
                                        Party Product Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSupplierProductName" style="text-transform: uppercase" placeholder="Please enter Party Product Name"
                                        maxlength="150" tabindex="5" autocomplete="off" />
                                </div>
                                <div class="form-group  col-md-3" id="divSupplierCode">
                                    <label>
                                        Supplier Code</label>
                                    <input type="text" class="form-control" id="txtSupplierCode" style="text-transform: uppercase" placeholder="Please enter Code"
                                        maxlength="15" tabindex="6" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divPrintName">
                                    <label>
                                        Print Name</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtPrintName" style="text-transform: uppercase" placeholder="Please enter Print Name"
                                        maxlength="150" tabindex="7" autocomplete="off" />
                                </div>
                                <div class="form-group  col-md-3" id="divSection">
                                    <label>
                                        Section</label><span class="text-danger">*</span>
                                    <select id="ddlSection" class="form-control select2" data-placeholder="Select Section" tabindex="8">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divCategory">
                                    <label>
                                        Category</label><span class="text-danger">*</span>
                                    <select id="ddlCategory" class="form-control select2" data-placeholder="Select Category" tabindex="9">
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divSubCategory">
                                    <label>
                                        Subcategory</label><span class="text-danger">*</span>
                                    <select id="ddlSubCategory" class="form-control select2" data-placeholder="Select Subcategory" tabindex="10">
                                    </select>
                                </div>
                                <div class="form-group  col-md-3" id="divUnit">
                                    <label>
                                        Unit</label><span class="text-danger">*</span>
                                    <select id="ddlUnit" class="form-control select2" data-placeholder="Select Unit" tabindex="11">
                                    </select>
                                </div>
                                <div class="form-group  col-md-3" id="divProductType">
                                    <label>
                                        ProductType</label><span class="text-danger">*</span>
                                    <select id="ddlProductType" class="form-control select2" data-placeholder="Select ProductType" tabindex="12">
                                    </select>
                                </div>
                                <div class="form-group col-md-3 " id="divDesignNo" style="display: none">
                                    <label>
                                        Design No</label>
                                    <input type="text" class="form-control" id="txtDesignNo" placeholder="Please enter Design No"
                                        maxlength="10" tabindex="-1" onkeypress="return IsNumeric(event)" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group  col-md-2 " id="divMinimumStock">
                                    <label>
                                        Minimum Stock</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtMinimumStock" placeholder="Please enter minimum Stock"
                                        maxlength="4" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group  col-md-2 " id="divMaximumStock">
                                    <label>
                                        Maximum Stock</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtMaximumStock" placeholder="Please enter Maximum Stock"
                                        maxlength="4" tabindex="14" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divTax">
                                    <label>
                                        Tax</label><span class="text-danger">*</span>
                                    <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax" tabindex="15">
                                    </select>
                                </div>
                                <div class="checkbox col-md-1 " style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="16" />Active
                                    </label>
                                </div>
                                <div class="checkbox col-md-1 " style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkPricingA" tabindex="17" />A
                                    </label>
                                </div>
                                <div class="checkbox col-md-1 " style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkPricingB" tabindex="18" />B
                                    </label>
                                </div>
                                <div class="checkbox col-md-1 " style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkPricingC" tabindex="19" />C
                                    </label>
                                </div>
                            </div>
                            <div class="row">
                            </div>


                            <div class="row">
                                <div class="form-group col-md-6" id="divTrackerScreenshotM">
                                    <label>Image</label><span style="font-size: 10px; color: #ff0000;"> (upload maximum Size 5MB)
                                        <button id="btnViewimage" type="button" tabindex="-1" style="margin-right: 32%;" class="btn btn-info">
                                            VIEW IMAGE</button>
                                        <button id="btnDownloadAll" type="button" tabindex="-1" style="margin-right: 32%;" class="btn btn-info">
                                            DOWNLOAD ALL</button></span>
                                    <input type="file" id="Imageupload" multiple="multiple" />
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="TrackerList">
                                            <div class="box box-warning">
                                                <div class="box-body">
                                                    <div class="table-responsive">
                                                        <table id="Download" class="table table-striped table-bordered bg-info" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>File Name</th>
                                                                    <th>Download</th>
                                                                    <th></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="tblImageupload_tbody">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>
                                        Image 1</label>
                                    <button id="btnClearImage1" type="button" style="margin-top: -11px; display: none; color: deeppink;" class="btn btn-link">
                                        Clear</button>
                                    <input name="imagefile" type="file" id="imagefile" style="display: none;" data-image-src="imgUpload1_view" accept="image/*" onchange="ResizeImage('imagefile');" />
                                    <a href="#" data-fancybox="images">
                                        <img src="" id="imgUpload1_view" alt="" class="preview_img" style="width: 280px;" />
                                    </a>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Image 2</label>
                                    <button id="btnClearImage2" type="button" style="margin-top: -11px; display: none; color: deeppink;" class="btn btn-link">
                                        Clear</button>
                                    <input name="imagefile" type="file" id="imagefile2" style="display: none;" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />
                                    <a href="#" data-fancybox="images">
                                        <img src="" id="imgUpload2_view" alt="" class="preview_img" style="width: 280px;" />
                                    </a>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Image 3</label>
                                    <button id="btnClearImage3" type="button" style="margin-top: -11px; display: none; color: deeppink;" class="btn btn-link">
                                        Clear</button>
                                    <input name="imagefile" type="file" id="imagefile3" style="display: none;" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                                    <a href="#" data-fancybox="images">
                                        <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                                    </a>
                                </div>
                            </div>

                        </div>

                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="17">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="18">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button id="btnLink" type="button" tabindex="-1" style="margin-right: 36%; color: deeppink;" class="btn btn-link">
                                Gener. SMSCode</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="19">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeProductImage" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body" id="myImg">
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnProductImageCancel" tabindex="37">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmProduct.aspx") %>';
        var gImageupload = [];
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
            gImageupload = [];
            var Sup_Code = "";
            pLoadingSetup(false);
            pLoadingSetup(true);
            GetProductList("ddlProductName");
            GetProductTypeList("ddlProductType");
            GetCategoryList();
            GetSubCategoryList();
            GetUnitList();
            GetSupplierList();
            GetSupplierName();
            GetCategoryName();
            GetSubCategoryName();
            GetProductType();
            GetSectionList();
            GetHSNMasterList();
            GetRecord();
            GetTaxList("ddlTaxName");

            $("#Imageupload").change(function () {
                var fileUpload = $("#Imageupload").get(0);
                var files = fileUpload.files;

                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: "MultipleFileUploadHandler.ashx",
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        var objResponse = jQuery.parseJSON(result);
                        for (var index = 0; index < objResponse.length; index++) {
                            var ObjData = new Object();
                            ObjData.sNO = gImageupload.max() + 1;
                            ObjData.SNo = ObjData.sNO;
                            ObjData.filename = objResponse[index].filename;
                            ObjData.filepath = objResponse[index].filepath;
                            gImageupload.push(ObjData);
                        }
                        displaypostingTrackerlist(gImageupload);
                    },
                    error: function (err) {
                        //alert(err.statusText)
                        $.jGrowl("File cannot be uploaded", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                });
            });

            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0].trim()));
            });
            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':0,'IsAll':'A'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item.split('|')[0]
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
                        "width": ("700px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });

            $("[id$=txtName]").change(function () {
                $("[id$=txtName]").val($("[id$=txtName]").val().split('|')[0]);
            });
            $("[id$=txtName]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmProduct.aspx/GetBarcode") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item.split('|')[0]
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
                    $("[id$=txtName]").autocomplete("widget").css({
                        "width": ("800px"), "backgroundColor": ("#d7dde2")
                    });
                    //$("[id$=txtName]").autocomplete("widget").width(500)
                    //$("[id$=txtName]").autocomplete("widget").backgroundColor('#a52a2a')
                },
                select: function (e, i) {
                },
                minLength: 1
            });

        });
        Array.prototype.max = function () {
            var max = this.length > 0 ? this[0]["sNO"] : 0;
            var len = this.length;
            for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
            return max;
        }

        function saveimage(id) {
            pLoadingSetup(false);

            var images = $("#" + id + "").attr('src');
            var ImageSave = images.replace("data:image/jpeg;base64,", "");
            var submitval = JSON.stringify({ data: ImageSave });

            $.ajax({
                type: "POST",
                url: pageUrl + "/saveimage",
                data: submitval,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $get(id).src = "./" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
            pLoadingSetup(true);
        }

        $("#aGeneral").click(function () {
            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {
            $("#SearchResult").show();

        });


        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });

        $("#ddlCategory").change(function () {
            GetSubCategoryList();
        });

        //$("#ddlCategoryName").change(function () {
        //    GetSubCategoryName();
        //    if ($('ul#navbars li.active').attr("id") == "iGeneral")
        //        GetRecord();
        //    else
        //        $("#txtSearchName").change();
        //});

        //$("#ddlSubCategoryName").change(function () {
        //    GetRecord();
        //});

        //$("#ddlType").change(function () {
        //    if ($('ul#navbars li.active').attr("id") == "iGeneral")
        //        GetRecord();
        //    else
        //        $("#txtSearchName").change();
        //});

        //$("#ddlProductName").change(function () {
        //    if ($('ul#navbars li.active').attr("id") == "iGeneral")
        //        GetRecord();
        //    else
        //        $("#txtSearchName").change();
        //});

        //$("#ddlSupplierName").change(function () {
        //    if ($('ul#navbars li.active').attr("id") == "iGeneral")
        //        GetRecord();
        //    else
        //        $("#txtSearchName").change();
        //});

        function displaypostingTrackerlist(gData) {
            $("#tblImageupload_tbody").empty();
            for (var i = 0; i < gData.length; i++) {
                var table = "<tr id='" + gData[i].SNo + "'>";
                table += "<td>" + (i + 1) + "</td>";
                table += "<td>" + gData[i].filename + "</td>";
                table += "<td><a href='" + window.location.href.substring(0, window.location.href.lastIndexOf("/") + 1) + gData[i].filepath + "' target='_blank'>Download</a></td>";
                table += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_Imageupload(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                table += "</tr>";
                $("#tblImageupload_tbody").append(table);
            }
        }

        function Delete_Imageupload(ID) {
            if (ID == 0)
                return false;
            for (var i = 0; i < gImageupload.length; i++) {
                if (gImageupload[i].SNo == ID) {
                    var index = jQuery.inArray(gImageupload[i].valueOf("SNo"), gImageupload);
                    gImageupload.splice(index, 1);
                    displaypostingTrackerlist(gImageupload);
                }
            }
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

        //#region GetData
        function GetSectionList() {
            $("#ddlSection").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSection",
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
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSection").append('<option value=' + obj[index].SectionID + ' >' + obj[index].SectionName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlSection").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlSection").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSupplier").append('<option value=' + obj[index].SupplierID + ' >' + obj[index].SupplierName + '</option>');
                                    }
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

        function GetHSNMasterList() {
            $("#ddlHSNCode").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetHSNMaster",
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlHSNCode").append('<option value=' + obj[index].HSNCode + ' >' + obj[index].HSNCode + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlHSNCode").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlHSNCode").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlCategory").append('<option value=' + obj[index].CategoryID + ' >' + obj[index].CategoryName + '</option>');
                                    }
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

        $("#btnClearImage1").click(function () {
            $get("imgUpload1_view").src = "";
            $("#imagefile").val("");
        });

        $("#btnClearImage2").click(function () {
            $get("imgUpload2_view").src = "";
            $("#imagefile2").val("");
        });

        $("#btnClearImage3").click(function () {
            $get("imgUpload3_view").src = "";
            $("#imagefile3").val("");
        });

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
        function GetUnitList() {
            $("#ddlUnit").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUnit",
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
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlUnit").append('<option value=' + obj[index].UnitID + ' >' + obj[index].UnitName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlUnit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlUnit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetProductTypeList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductType",
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
                                            $(sControlName).append("<option value='" + obj[index].ProductTypeID + "'>" + obj[index].ProductTypeName + "</option>");
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
                                    $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSubCategory").append('<option value=' + obj[index].SubCategoryID + ' >' + obj[index].SubCategoryName + '</option>');
                                    }
                                    $("#ddlSubCategory").val(0).change();
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

        //#endregion GetData


        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Product");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#ddlTaxName").val(2).change();
            $("#txtName").focus();
            $("#imagefile").val("");
            $("#imagefile2").val("");
            $("#imagefile3").val("");
            return false;
        });

        $("#btnView").click(function () {
            GetRecord();
        });


        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate") { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Product", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            //if ($("#txtSupplierCode").val().trim() == "" || $("#txtSupplierCode").val().trim() == undefined) {
            //    $.jGrowl("Please enter Supplier Code", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divSupplierCode").addClass('has-error'); $("#txtSupplierCode").focus(); return false;
            //} else { $("#divSupplierCode").removeClass('has-error'); }



            if ($("#txtSupplierProductName").val().trim() == "" || $("#txtSupplierProductName").val().trim() == undefined) {
                $.jGrowl("Please enter Party Product Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSupplierProductName").addClass('has-error'); $("#txtSupplierProductName").focus(); return false;
            } else { $("#divSupplierProductName").removeClass('has-error'); }

            if ($("#txtPrintName").val().trim() == "" || $("#txtPrintName").val().trim() == undefined) {
                $.jGrowl("Please enter Print Name", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPrintName").addClass('has-error'); $("#txtPrintName").focus(); return false;
            } else { $("#divPrintName").removeClass('has-error'); }

            if ($("#txtMaximumStock").val().trim() == "" || $("#txtMaximumStock").val().trim() == undefined) {
                $.jGrowl("Please enter Maximum Stock", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMaximumStock ").addClass('has-error'); $("#txtMaximumStock").focus(); return false;
            } else { $("#divMaximumStock").removeClass('has-error'); }

            //if ($("#txtCode").val().trim() == "" || $("#txtCode").val().trim() == undefined) {
            //    $.jGrowl("Please enter SMS Code", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divCode").addClass('has-error'); $("#txtCode").focus(); return false;
            //} else { $("#divCode").removeClass('has-error'); }

            if ($("#ddlProductType").val() == "0" || $("#ddlProductType").val() == undefined) {
                $.jGrowl("Please Select Product Type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divProductType  ").addClass('has-error'); $("#ddlProductType").focus(); return false;
            } else { $("#divProductType").removeClass('has-error'); }

            if ($("#txtMinimumStock").val().trim() == "" || $("#txtMinimumStock").val().trim() == undefined) {
                $.jGrowl("Please enter Minimum Stock", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divMinimumStock").addClass('has-error'); $("#txtMinimumStock").focus(); return false;
            } else { $("#divMinimumStock").removeClass('has-error'); }

            if ($("#ddlCategory").val() == "0" || $("#ddlCategory").val() == undefined) {
                $.jGrowl("Please Select Category", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCategory").addClass('has-error'); $("#ddlCategory").focus(); return false;
            } else { $("#divCategory").removeClass('has-error'); }


            if ($("#ddlSubCategory").val() == "0" || $("#ddlSubCategory").val() == undefined) {
                $.jGrowl("Please Select Subcategory", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSubCategory").addClass('has-error'); $("#ddlSubCategory").focus(); return false;
            } else { $("#divSubCategory").removeClass('has-error'); }

            if ($("#ddlTaxName").val() == "0" || $("#ddlTaxName").val() == undefined) {
                $.jGrowl("Please Select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divTax").addClass('has-error'); $("#ddlTaxName").focus(); return false;
            } else { $("#divTax").removeClass('has-error'); }

            if ($("#txtCode").val().length > 0) {
                var Code = $("#txtCode").val().toUpperCase();
                var Supplier_Code = Sup_Code.toUpperCase();
                var res = Code.replace("SMS", "").includes(Supplier_Code);
                if (res == false) {
                    $.jGrowl("Please enter Coreect SMS Code", { sticky: false, theme: 'warning', life: jGrowlLife });
                    return false;
                }
            }

            var Obj = new Object();
            Obj.ProductID = 0;
            Obj.ProductName = $("#txtName").val().toUpperCase();
            Obj.ProductCode = $("#txtSupplierCode").val().toUpperCase();
            Obj.SupplierProductName = $("#txtSupplierProductName").val().toUpperCase();
            Obj.PrintName = $("#txtPrintName").val().toUpperCase();

            var ObjCategory = new Object();
            ObjCategory.CategoryID = $("#ddlCategory").val();
            Obj.Category = ObjCategory;

            var ObjSection = new Object();
            ObjSection.SectionID = $("#ddlSection").val();
            Obj.Section = ObjSection;

            var ObjSubCategory = new Object();
            ObjSubCategory.SubCategoryID = $("#ddlSubCategory").val();
            Obj.SubCategory = ObjSubCategory;

            var ObjSupplier = new Object();
            ObjSupplier.SupplierID = $("#ddlSupplier").val();
            Obj.Supplier = ObjSupplier;

            var ObjTax = new Object();
            ObjTax.TaxID = $("#ddlTaxName").val();
            Obj.Tax = ObjTax;

            var ObjUnit = new Object();
            ObjUnit.UnitID = $("#ddlUnit").val();
            Obj.Unit = ObjUnit;

            var ObjProductType = new Object();
            ObjProductType.ProductTypeID = $("#ddlProductType").val();
            Obj.ProductType = ObjProductType;

            Obj.HSNCode = $("#ddlHSNCode").val();
            Obj.DesignNo = $("#txtDesignNo").val();
            Obj.SMSCode = $("#txtCode").val().toUpperCase();
            Obj.MinimumStock = $("#txtMinimumStock").val();
            Obj.MaximumStock = $("#txtMaximumStock").val();
            Obj.ProductImage1 = "";//$("[id*=imgUpload1_view]").attr("src");
            Obj.ProductImage2 = "";//$("[id*=imgUpload2_view]").attr("src");
            Obj.ProductImage3 = "";//$("[id*=imgUpload3_view]").attr("src");
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
            Obj.PricingA = $("#chkPricingA").is(':checked') ? "1" : "0";
            Obj.PricingB = $("#chkPricingB").is(':checked') ? "1" : "0";
            Obj.PricingC = $("#chkPricingC").is(':checked') ? "1" : "0";

            var gImage = [];
            for (var i = 0; i < gImageupload.length; i++) {
                objimage = new Object();
                objimage.Filename = gImageupload[i].filename;
                objimage.Filepath = gImageupload[i].filepath;
                gImage.push(objimage);
            }
            Obj.ProductImages = gImage

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ProductID = $("#hdnID").val();
                sMethodName = "UpdateProduct";
            }
            else { sMethodName = "AddProduct"; }

            SaveandUpdateProduct(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#txtSupplierProductName").val("");
            $("#txtPrintName").val("");
            $("#txtSupplierCode").val("");
            $("#ddlHSNCode").val("0").change();
            $("#txtDesignNo").val("");

            $("#txtMinimumStock").val("0");
            $("#txtMaximumStock").val("0");
            $("#chkStatus").prop("checked", true);
            $("#chkPricingA").prop("checked", false);
            $("#chkPricingB").prop("checked", false);
            $("#chkPricingC").prop("checked", false);
            $("#ddlCategory").val(1).change();
            $("#ddlSection").val($("#ddlSection option:first").val()).change();
            $("#ddlSupplier").val($("#ddlSupplier option:first").val()).change();
            $("#ddlUnit").val($("#ddlUnit option:first").val()).change();
            $("#ddlProductType").val($("#ddlProductType option:first").val()).change();
            $("#ddlTaxName").val($("#ddlTaxName option:first").val()).change();
            $("#divName").removeClass('has-error');
            $("#divCategory").removeClass('has-error');
            $("#divSubCategory").removeClass('has-error');
            $("#divMinimumStock").removeClass('has-error');
            $("#divMaximumStock").removeClass('has-error');
            $("#divUnit").removeClass('has-error');
            $("#divProductType").removeClass('has-error');
            $("#divSection").removeClass('has-error');
            $("#divPrintName").removeClass('has-error');
            $("#divSupplier").removeClass('has-error');
            $("#divSupplierProductName").removeClass('has-error');
            $("#divSupplierCode").removeClass('has-error');
            $get("imgUpload1_view").src = "";
            $get("imgUpload2_view").src = "";
            $get("imgUpload3_view").src = "";
            $("[id*=imgUpload1_view]").css("visibility", "hidden");
            $("[id*=imgUpload2_view]").css("visibility", "hidden");
            $("[id*=imgUpload3_view]").css("visibility", "hidden");
            gImageupload = [];
            $("#tblImageupload_tbody").empty();
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAllProduct",
                data: JSON.stringify({ ID: $("#ddlProductName").val(), CategoryID: $("#ddlCategoryName").val(), SubCategoryID: $("#ddlSubCategoryName").val(), SupplierID: $("#ddlSupplierName").val(), TypeID: $("#ddlType").val() }),
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

                                        var table = "<tr id='" + obj[index].ProductID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Category.CategoryName + "</td>";
                                        table += "<td>" + obj[index].ProductName + "</td>";
                                        table += "<td>" + obj[index].SMSCode.toUpperCase() + "</td>";
                                        table += "<td>" + obj[index].Supplier.SupplierName + "</td>";
                                        table += "<td>" + obj[index].ProductCode + "</td>";
                                        table += "<td>" + obj[index].sCreatedOn + "</td>";
                                        table += "<td>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

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
                                    { "sWidth": "6%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "30%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
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
                url: "WebServices/VHMSService.svc/SearchProduct",
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
                                        if (obj[index].IsActive == "1") { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].ProductID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Category.CategoryName + "</td>";
                                        table += "<td>" + obj[index].ProductName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].SMSCode.toUpperCase() + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Supplier.SupplierName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].ProductCode + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sCreatedOn + "</td>";
                                        table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].ProductID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
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
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "6%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "30%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "20%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
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

                                if (sMethodName == "AddProduct") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateProduct") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Product_A_01" || objResponse.Value == "Product_U_01") {
                                $.jGrowl("SMS Code Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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
        function SupplierCode(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSupplierByID",
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
                                    Sup_Code = obj.SupplierCode;
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


        $("#btnLink").click(function () {
            GenerateSMSCode($("#ddlSupplier").val());
        });
        function GenerateSMSCode(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GenerateSMSCode",
                data: JSON.stringify({ SupplierID: id }),
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
                                    $("#txtCode").val(obj.GenerateSMSCode).change();
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
                                $.jGrowl("Supplier code not added in folio.", { sticky: false, theme: 'danger', life: jGrowlLife });
                                dProgress(false);
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
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();

                                    $("#hdnID").val(obj.ProductID);
                                    $("#txtName").val(obj.ProductName);
                                    $("#txtCode").val(obj.SMSCode);
                                    $("#txtSupplierProductName").val(obj.SupplierProductName);
                                    $("#txtPrintName").val(obj.PrintName);
                                    $("#ddlHSNCode").val(obj.HSNCode).change();
                                    $("#txtDesignNo").val(obj.DesignNo);
                                    $("#txtSupplierCode").val(obj.ProductCode);
                                    $("#ddlCategory").val(obj.Category.CategoryID).change();
                                    $("#ddlTaxName").val(obj.Tax.TaxID).change();
                                    $("#ddlSubCategory").val(obj.SubCategory.SubCategoryID).change();
                                    $("#ddlSupplier").val(obj.Supplier.SupplierID).change();
                                    $("#ddlUnit").val(obj.Unit.UnitID).change();
                                    $("#ddlProductType").val(obj.ProductType.ProductTypeID).change();
                                    $("#ddlSection").val(obj.Section.SectionID).change();
                                    $("#txtMinimumStock").val(obj.MinimumStock)
                                    $("#txtMaximumStock").val(obj.MaximumStock)
                                    $("[id*=imgUpload1_view]").css("visibility", "visible");
                                    $("[id*=imgUpload1_view]").attr("src", obj.ProductImage1);
                                    $("[id*=imgUpload2_view]").css("visibility", "visible");
                                    $("[id*=imgUpload2_view]").attr("src", obj.ProductImage2);
                                    $("[id*=imgUpload3_view]").css("visibility", "visible");
                                    $("[id*=imgUpload3_view]").attr("src", obj.ProductImage3);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);
                                    $("#chkPricingA").prop("checked", obj.PricingA ? true : false);
                                    $("#chkPricingB").prop("checked", obj.PricingB ? true : false);
                                    $("#chkPricingC").prop("checked", obj.PricingC ? true : false);

                                    gImageupload = [];
                                    var Objimg = obj.ProductImages;
                                    for (var index = 0; index < Objimg.length; index++) {
                                        var ObjData = new Object();
                                        ObjData.sNO = gImageupload.max() + 1;
                                        ObjData.SNo = ObjData.sNO;
                                        ObjData.filename = Objimg[index].Filename;
                                        ObjData.filepath = Objimg[index].Filepath;
                                        if (index == 0) {
                                            $("[id*=imgUpload1_view]").css("visibility", "visible");
                                            $("[id*=imgUpload1_view]").attr("src", Objimg[index].Filepath);
                                        }
                                        if (index == 1) {
                                            $("[id*=imgUpload2_view]").css("visibility", "visible");
                                            $("[id*=imgUpload2_view]").attr("src", Objimg[index].Filepath);
                                        }
                                        if (index == 2) {
                                            $("[id*=imgUpload3_view]").css("visibility", "visible");
                                            $("[id*=imgUpload3_view]").attr("src", Objimg[index].Filepath);
                                        }
                                        gImageupload.push(ObjData);
                                    }
                                    displaypostingTrackerlist(gImageupload);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Product");
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
        $("#btnViewimage").click(function () {

            $("#myImg").empty();

            for (var index = 0; index < gImageupload.length; index++) {
                var ObjData = new Object();
                ObjData.sNO = gImageupload.max() + 1;
                ObjData.SNo = ObjData.sNO;
                ObjData.filename = gImageupload[index].Filename;
                ObjData.filepath = gImageupload[index].Filepath;
                $('#myImg').append('<img src="' + gImageupload[index].filepath + '" style="width: 350px;height: 350px;margin: 0px 15px 15px 0px;">');
            }

            $(".modal-title").html("&nbsp;&nbsp; Product Image");
            $('#composeProductImage').modal({ show: true, backdrop: true });
        });
        $("#btnDownloadAll").click(function () {
            var zipFilename = $("#txtName").val() + ".zip";

            //var zip = new JSZip();
            //var count = 0;

            //for (var index = 0; index < gImageupload.length; index++) {
            //    var filename = gImageupload[index].filepath;
            //    //filename = filename.replace(/[\/\*\|\:\<\>\?\"\\]/gi, '').replace("httpsi.imgur.com", "");
            //    // loading a file and add it in a zip file
            //    // downloadImageAsZip(filename);
            //    //JSZipUtils.getBinaryContent(filename, function (err, data) {

            //    var zip = new JSZip();
            //    var img = new Image();
            //    img.crossOrigin = 'Anonymous';
            //    img.src = filename;
            //    img.onload = function () {

            //        var canvas = document.createElement('CANVAS');
            //        var ctx = canvas.getContext('2d');
            //        var dataURL;
            //        canvas.height = img.height;
            //        canvas.width = img.width;
            //        ctx.drawImage(this, 0, 0);
            //        ctx.enabled = false;
            //        dataURL = canvas.toDataURL("Canvas");
            //        canvas = null;
            //        //var base64String = dataURL.replace("/^data:image\/(png|jpg);base64,/", "");
            //        var base64String = dataURL.replace("data:image/png;base64,", "");
            //        zip.file("ImageName" + [index] + ".png", base64String, { base64: true });
            //        count++;
            //        if (count == gImageupload.length) {
            //            zip.generateAsync({ type: 'blob' }).then(function (content) {
            //                saveAs(content, zipFilename);
            //            });
            //        }

            //    }
            //}
            console.log('TEST');
            var zip = new JSZip();
            var count = 0;
            var zipFilename = "Pictures.zip";

            gImageupload.forEach(function (url, i) {
                var filename = gImageupload[i].filepath;
                filename = filename.replace("./images/ProductImages/", "");
                // loading a file and add it in a zip file
                JSZipUtils.getBinaryContent(url, function (err, data) {
                    if (err) {
                         // or handle the error
                    }
                    zip.file(filename, data, { binary: true });
                    count++;
                    if (count == gImageupload.length) {
                        zip.generateAsync({ type: 'blob' }).then(function (content) {
                            saveAs(content, zipFilename);
                        });
                    }
                });
            });

        });

        function downloadImageAsZip(imageUrl) {
            var zip = new JSZip();
            var img = new Image();
            img.crossOrigin = 'Anonymous';
            img.src = imageUrl;
            img.onload = function () {

                var canvas = document.createElement('CANVAS');
                var ctx = canvas.getContext('2d');
                var dataURL;
                canvas.height = img.height;
                canvas.width = img.width;
                ctx.drawImage(this, 0, 0);
                ctx.enabled = false;
                dataURL = canvas.toDataURL("Canvas");
                canvas = null;
                //var base64String = dataURL.replace("/^data:image\/(png|jpg);base64,/", "");
                var base64String = dataURL.replace("data:image/png;base64,", "");
                zip.file("ImageName", base64String, { base64: true });

                zip.generateAsync({ type: "blob" }).then(function (content) {
                    saveAs(content, "ZipFileName.zip");
                });

            }

        }
        $("#btnProductImageCancel").click(function () {
            $('#composeProductImage').modal('hide');
            $('#compose-modal').addClass('modal');
            return false;
        });

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
                                        $('#myImg').append('<img src="' + Objimg[index].Filepath + '" style="width: 350px;height: 350px;margin: 0px 15px 15px 0px;">');
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

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteProduct",
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
                                ClearFields();
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Product_R_01" || objResponse.Value == "Product_D_01") {
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
    </script>
    <script type="text/javascript" src="JS/fancybox/jquery.fancybox.js?v=2.1.4"></script>
    <link rel="stylesheet" type="text/css" href="JS/fancybox/jquery.fancybox.css?v=2.1.4" media="screen" />
    <script type="text/javascript">

        $('img.preview_img').on('load', function () {
            //console.log($(this).attr('src'));
            $(this).parent("a").attr("href", $(this).attr("src"));
        });

        function ResizeImage(img_id) {

            var filesToUpload = document.getElementById(img_id).files;
            var file = filesToUpload[0];

            // Create an image
            var img = document.createElement("img");
            // Create a file reader
            var reader = new FileReader();
            // Set the image once loaded into file reader
            reader.onload = function (e) {
                //img.src = e.target.result;
                var img = new Image();

                img.src = this.result;

                setTimeout(function () {
                    var canvas = document.createElement("canvas");

                    var MAX_WIDTH = 1500;
                    var MAX_HEIGHT = 1000;
                    var width = img.width;
                    var height = img.height;

                    if (width > height) {
                        if (width > MAX_WIDTH) {
                            height *= MAX_WIDTH / width;
                            width = MAX_WIDTH;
                        }
                    } else {
                        if (height > MAX_HEIGHT) {
                            width *= MAX_HEIGHT / height;
                            height = MAX_HEIGHT;
                        }
                    }
                    canvas.width = width;
                    canvas.height = height;
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(img, 0, 0, width, height);
                    var dataurl = canvas.toDataURL("image/jpeg");
                    var image_view = $("#" + img_id).attr("data-image-src");
                    document.getElementById(image_view).src = dataurl;
                    $("#" + image_view).css({ "visibility": "visible", "display": "block" });
                    saveimage(image_view);
                }, 100);
            }
            // Load files into file reader
            reader.readAsDataURL(file);
        }
    </script>
</asp:Content>


