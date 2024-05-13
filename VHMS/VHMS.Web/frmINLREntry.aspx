<%@ Page Title="IN LREntry" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmINLREntry.aspx.cs" Inherits="frmINLREntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>IN LREntry
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
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>LR #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Transport</th>
                                                <th>Invoice</th>
                                                <th>Status</th>
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
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search INLREntry records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>LR #</th>
                                                <th>Date</th>
                                                <th>Supplier</th>
                                                <th>Transport</th>
                                                <th>Invoice</th>
                                                <th>Status</th>
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
            <div class="box box-primary" id="divOPBilling">
                <div class="box-header with-border">
                    <div class="box-title">INLREntry Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2" id="divBillNo">
                            <label>
                                LR. No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="LR No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>

                        <div class="form-group col-md-2" id="divBillDate">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtBillDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divInvoiceNo">
                            <label>
                                Invoice No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtInvoiceNo" placeholder="InvoiceNo"
                                tabindex="3" />
                        </div>
                        <div class="form-group col-md-2" id="divCustomer">
                            <label>
                                Supplier Name</label><span class="text-danger">*</span>
                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="4"></select>
                        </div>
                        <div class="form-group col-md-2" id="divShippingAddressName" style="display: none;">
                            <label>
                                Shipping Address</label><span class="text-danger">*</span>
                            <select id="ddlShippingAddress" class="form-control select2" data-placeholder="Select Shipping Address" tabindex="5"></select>
                        </div>
                        <div class="form-group col-md-2" id="divTransportName">
                            <label>
                                Transport Name</label><span class="text-danger">*</span>
                            <select id="ddlTransport" class="form-control select2" data-placeholder="Select Transport Name" tabindex="6"></select>
                        </div>
                        <div class="form-group col-md-2" id="divBranch">
                            <label>
                                Transport place</label>
                            <input type="text" class="form-control" id="txtBranch" placeholder=""
                                maxlength="15" tabindex="7" />
                        </div>
                        <div class="form-group col-md-2" id="divVehicleNo">
                            <label>
                                Vehicle No</label>
                            <input type="text" class="form-control" id="txtVehicleNo" placeholder="Vehicle No"
                                maxlength="15" tabindex="8" />
                        </div>
                        <div class="form-group col-md-2" id="divVehicleType">
                            <label>
                                Vehicle Type</label>
                            <input type="text" class="form-control" id="txtVehicleType" placeholder="Vehicle Type"
                                maxlength="15" tabindex="9" />
                        </div>
                        <div class="form-group col-md-2" id="divEWayNo">
                            <label>
                                EWay No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtEWayNo" placeholder="EWayNo"
                                tabindex="10" />
                            <button id="btnLink" type="button" class="btn btn-link">
                                <i class="fa fa-link" aria-hidden="true"></i>&nbsp;&nbsp;
                              Eway link</button>
                        </div>
                        <div class="form-group col-md-2" id="divAWBNo">
                            <label>
                                LR No</label>
                            <input type="text" class="form-control" id="txtAWBNo" placeholder="LR No"
                                maxlength="15" tabindex="11" />
                        </div>
                        <div class="form-group col-md-2" id="divNoofbundles">
                            <label>
                                No of Bundles</label>
                            <input type="text" class="form-control" id="txtNoofbundles" placeholder="No of Bundles"
                                maxlength="15" tabindex="11" />
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divProduct">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtProduct" placeholder="Product"
                                        tabindex="12" />
                                </div>
                                <div class="form-group col-md-2" id="divQuantity">
                                    <label>
                                        KG</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" />
                                </div>


                                <div class="form-group col-md-2" id="divRate">
                                    <label>
                                        Charges</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="15" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-2" id="divSubTotal">
                                    <label>
                                        Subtotal</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="16" onkeypress="return IsNumeric(event)" />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="17">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="18">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive" style="min-height: 250px !Important;">
                            <div id="divOPBillingList">
                            </div>
                        </div>
                    </div>
                    <%-- <br />--%>
                    <div class="row">

                        <div class="form-group col-md-3">
                            <label>
                                Follow Person</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtFollwedPerson" placeholder="Followed Person"
                                tabindex="19" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Payment Mode</label><span class="text-danger">*</span>

                            <select id="ddlPayment" class="form-control select2" tabindex="20">
                                <option value="ToPay">To Pay</option>
                                <option value="ToPaid">Paid</option>
                                <option value="HandDelivery">HandDelivery</option>
                            </select>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Status</label><span class="text-danger">*</span>
                            <select id="ddlStatus" class="form-control select2" tabindex="21">
                                <option value="Pending">Pending</option>
                                <option value="Delivered">Delivered</option>
                            </select>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Description</label><span class="text-danger">*</span>
                            <textarea id="txtDescription" class="form-control TRSearch" maxlength="250" tabindex="22" rows="2"></textarea>
                        </div>
                    </div>
                    <div class="row">
                        <%--<div class="form-group col-md-7"></div>--%>
                    </div>
                    <div class="row">
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-3">
                        <label>
                            Image 1</label>
                        <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                            Clear</button>
                        <input name="imagefile" type="file" id="imagefile2" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />
                        <a href="#" data-fancybox="images">
                            <img src="" id="imgUpload2_view" alt="" class="preview_img" style="width: 280px;" />
                        </a>
                    </div>
                    <div class="form-group col-md-2">
                        <label>
                            Image 2</label>
                        <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                            Clear</button>
                        <input name="imagefile" type="file" id="imagefile3" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                        <a href="#" data-fancybox="images">
                            <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                        </a>
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-2">
                        <label>
                            Delivery Date</label><span class="text-danger">*</span>
                        <div class="input-group date form_date" data-date-format="dd/MM/yyyy" data-link-field="txtOPBillingDate"
                            data-link-format="dd/MM/yyyy">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-right" tabindex="23" id="txtDeliveryDate" readonly />
                        </div>
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-3">
                        <label>
                            Frieght Amt</label>
                        <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount"
                            maxlength="24" tabindex="22" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="25">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                    <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="26">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                    <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="27">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                </div>
            </div>
    </div>
    <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group" id="divReason">
                        <label>
                            Reason</label><span class="text-danger">*</span>
                        <input type="text" class="form-control" id="txtReason" placeholder="Please enter Reason"
                            maxlength="150" tabindex="26" />
                    </div>
                    <div class="form-group" id="divID">
                        <label>
                            ID</label><span class="text-danger">*</span>
                        <input type="text" class="form-control" id="txtID"
                            maxlength="150" tabindex="27" />
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="28">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                    <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="29">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                </div>
            </div>
        </div>
    </div>
    </section>

      </div>
    <input type="hidden" id="hdnINLREntryID" />
    <input type="hidden" id="hdnINLREntryTransID" />
    <input type="hidden" id="hdnPatientID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnPurchaseID" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jINLREntry.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jINLREntry.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var _CMDPurchaseID = '<%=Session["PurchaseID"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmINLREntry.aspx") %>';
        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
        });

        function DocumentuploadComplete(sender, args) {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#hdnImgupload1").val("./images/Documents/LR/" + r.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
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

