<%@ Page Title="Sales Discount" Language="C#" MasterPageFile="~/VHMSMasterPage.master"
    AutoEventWireup="true" CodeFile="frmSalesDiscount.aspx.cs" Inherits="frmSalesDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .btnPrint, .btnPrintbill {
            background-color: #ef00bc !important;
            margin-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Sales Discount
                </h3>
            </div>
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>
            </div>
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">Search Result</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Ref. No</th>
                                                <th>Entry Date</th>
                                                <th>Customer</th>

                                                <th>Bill No</th>
                                                <th>Dis Amt</th>
                                                <th>Tax %</th>
                                                <th>Net Amount</th>
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
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info">
                                            <thead>
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>Ref. No</th>
                                                    <th>Entry Date</th>
                                                    <th>Customer</th>
                                                    <th>Bill No</th>
                                                    <th>Dis Amt</th>
                                                    <th>Tax %</th>
                                                    <th>Net Amount</th>
                                                    <th></th>
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
            <div class="modal fade" id="compose-modaled" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="box-header with-border">
                            <div class="box-title">Sales DisCount</div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-3" id="divReturnNo">
                                    <label>
                                        Sales Disc.No</label>

                                    <input type="text" class="form-control" id="txtRefNo" placeholder="Bill No"
                                        maxlength="15" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divEntryDate">
                                    <label>Entry Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon"><i class="fa fa-calendar glyphicon glyphicon-calendar"></i></div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtEntryDate" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-6" id="divCustomer">
                                    <label>
                                        Customer</label>
                                    <input type="text" class="form-control" id="txtName" placeholder="Customer Name"
                                        maxlength="15" tabindex="-1" readonly="true" style="display: none;" />
                                    <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="4"></select>
                                </div>
                                <div class="form-group col-md-3" id="divOldInvoiceNo">
                                    <label>
                                        Old Invoice No</label>
                                    <select id="ddlOldInvoiceNo" class="form-control select2" data-placeholder="Select Old Invoice No" tabindex="4"></select>

                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Bill  Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy" data-link-field="txtPurcDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon"><i class="fa fa-calendar glyphicon glyphicon-calendar"></i></div>
                                        <input type="text" class="form-control pull-right" tabindex="-1" id="txtSalesDate" readonly="true" disabled="disabled" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divOPDNo">
                                    <label>
                                        Invoice No(Adjust)</label><span class="text-danger">*</span>
                                    <select id="ddlAdjBillNo" class="form-control select2" data-placeholder="Select Invoice No" tabindex="4"></select>
                                </div>
                            </div>


                            <div class="row">
                                <div class="form-group col-md-3" id="divBillDate">
                                    <label>Debit note Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy" data-link-field="txtOPBillingDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divNote">
                                    <label>
                                        Debit note</label>
                                    <input type="text" class="form-control" id="txtDebitnote" placeholder="Debit note"
                                        maxlength="15" tabindex="-1" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-8" id="divAddress">
                                    <label>
                                        Notes</label>
                                    <textarea id="txtComments" class="form-control" maxlength="1000" tabindex="6" rows="6" aria-autocomplete="none"></textarea>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label>
                                            Discount Amount</label>
                                    </div>
                                    <div class="form-group col-md-2" id="divDisAmount" style="margin-left: -10px;">
                                        <input type="text" class="form-control TRSearch" id="txtDisAmount" placeholder=" Discount Amount"
                                            maxlength="12" tabindex="5" autocomplete="off" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Tax</label><span class="text-danger">*</span>
                                    </div>
                                    <div class="form-group col-md-2" id="divTaxName" style="margin-left: -10px;">
                                        <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="4"></select>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Tax Amount</label>
                                    </div>
                                    <div class="form-group col-md-2" id="divTaxAmount" style="margin-left: -10px;">
                                        <input type="text" class="form-control" id="txtTaxAmt" placeholder="Tax Amount"
                                            maxlength="12" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Roundoff</label>
                                    </div>
                                    <div class="form-group col-md-2" id="divRoundoff" style="margin-left: -10px;">
                                        <input type="text" class="form-control TRSearch" id="txtRoundoff"
                                            maxlength="12" tabindex="8" autocomplete="off" value="0" />
                                    </div>
                                    <div class="form-group col-md-8"></div>
                                    <div class="form-group col-md-1">
                                        <label>
                                            NetAmount</label>
                                    </div>
                                    <div class="form-group col-md-2" id="divNetAmount" style="margin-left: 65px;">
                                        <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 25px;"
                                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <label>
                                    Image 1</label>
                                <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                    Clear</button>
                                <input name="imageSalesfile" type="file" id="imageSalesfile" data-image-src="imgUploadSales_view" accept="image/*" onchange="ResizeImage('imageSalesfile');" />
                                <img src="" id="imgUploadSales_view" alt="" style="width: 280px;" />
                            </div>
                            <div class="form-group col-md-6">
                                <div class="form-group col-md-1" style="display: none;">
                                    <label>
                                        IGST</label>
                                </div>
                                <div class="form-group col-md-2" id="divIGST" style="display: none;">
                                    <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                                        maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-1" style="display: none;">
                                    <label>
                                        SGST</label>
                                </div>
                                <div class="form-group col-md-2" id="divSGST" style="display: none;">
                                    <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                                        maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-2"></div>
                                <div class="form-group col-md-1" style="display: none;">
                                    <label>
                                        CGST</label>
                                </div>
                                <div class="form-group col-md-2" id="divCGST" style="display: none;">
                                    <input type="text" class="form-control" id="txtCGST" placeholder="CGST"
                                        maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-12"></div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="16">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                            <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="17">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="18">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                            <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="19">
                                <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="composeImage" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title"></h4>
                            </div>
                            <div class="modal-body">
                                <div>
                                    <img src="" id="imgUpload1" alt="" style="visibility: hidden; display: block; margin-right: auto; width: 280px; height: 280px" />
                                </div>
                                <div>
                                    <img src="" id="imgUpload5" alt="" style="visibility: hidden; display: block; margin-top: -280px; margin-left: 297px; width: 280px; height: 280px" />
                                </div>
                                <div>
                                    <img src="" id="imgUpload6" alt="" style="visibility: hidden; display: block; margin-right: auto; margin-left: 593px; margin-top: -280px; width: 280px; height: 280px" />
                                </div>
                            </div>
                            <div class="modal-footer clearfix">
                                <button type="button" class="btn btn-danger pull-left" id="btnImageCancel" tabindex="37">
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
            </div>
    </div>
    </section>
    </div>
    <input type="hidden" id="hdnSalesDiscountID" />
    <input type="hidden" id="hdnSalesReturnTransID" />
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnSalesID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnSMSCode" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnProductID" />
    <input type="hidden" id="hdnPreQtyID" />
    <input type="hidden" id="hdnTransTaxID" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnTransTaxPercent" />
    <input type="hidden" id="hdnTransCGSTPercent" />
    <input type="hidden" id="hdnTransSGSTPercent" />
    <input type="hidden" id="hdnTransIGSTPercent" />
    <input type="hidden" id="hdnTransCGSTAmount" />
    <input type="hidden" id="hdnTransSGSTAmount" />
    <input type="hidden" id="hdnTransIGSTAmount" />
    <input type="hidden" id="hdnOpeningDate" />
    <script src="UserDefined_Js/jSalesDiscount.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jSalesDiscount.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmSalesDiscount.aspx") %>';
        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0]).trim());
            });
            $("[id$=txtCode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':0,'IsAll':'" + $('input[name="SupplierProduct"]:checked').val() + "'}",
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
                        "width": ("600px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });

        });

        $("input[name=SupplierProduct]:radio").click(function () {
            if ($("input[name=SupplierProduct]:checked").val() == "S") {
                $('#txtInvoiceNo').prop('readonly', false);
                $('#ddlCustomerName').prop('disabled', true);
            }
            else if ($("input[name=SupplierProduct]:checked").val() == "A") {
                //$('#txtInvoiceNo').prop('readonly', true);
                $('#ddlCustomerName').prop('disabled', false)
            }
        });


    </script>
    <script type="text/javascript">

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
