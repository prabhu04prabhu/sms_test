<%@ Page Title="Expense" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmExpense.aspx.cs" Inherits="frmExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Expense
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
                                                <th>Expense #</th>
                                                <th>Date</th>
                                                <th>Party</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Tax Amount</th>
                                                <th>Net Amount</th>
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
                                        Search Expense records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Expense #</th>
                                                <th>Date</th>
                                                <th>Party</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Tax Amount</th>
                                                <th>Net Amount</th>
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
                    <div class="box-title">Expense Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2" id="divBillNo">
                            <label>
                                Expense No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Exp. No"
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
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divParty">
                            <label>
                                Party</label><span class="text-danger">*</span>
                            <select id="ddlPartyName" class="form-control select2" data-placeholder="Select Party Name" tabindex="3"></select>
                        </div>
                        <div class="form-group col-md-2" id="divExpenseNo">
                            <label>
                                Bill No</label>
                            <input type="text" class="form-control" id="txtExpenseNo" placeholder="Bill No"
                                maxlength="50" tabindex="4" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divBDate">
                            <label>
                                Bill Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtBDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="5" id="txtBDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divGSTNo">
                            <label>
                                GST No</label>
                            <input type="text" class="form-control" id="txtGSTNo" placeholder="GST No"
                                maxlength="15" tabindex="6" autocomplete="off" readonly="true" />
                        </div>

                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-3" id="divLedgerName">
                                    <label>
                                        Ledger</label><span class="text-danger">*</span>
                                    <div id="divSelectLedgerName">
                                        <select id="ddlLedgerName" class="form-control select2" data-placeholder="Select Ledger Name" tabindex="7"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-1" id="divQuantity" style="margin-left: -21px;">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divRate" style="margin-left: -21px;">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divTaxTrans" style="margin-left: -21px;">
                                    <label>
                                        Tax</label><span class="text-danger">*</span>
                                    <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax " tabindex="10"></select>
                                </div>
                                <div class="form-group col-md-1" id="divTaxAmt">
                                    <label>
                                        Tax Amt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer" style="margin-left: -21px;">
                                    <label>
                                        Disc %</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divDisAmt" style="margin-left: -21px;">
                                    <label>
                                        Disc. Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divAmount">
                                    <label>
                                        Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtAmount" placeholder="Amount"
                                        maxlength="12" tabindex="12" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="14">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive" style="min-height: 100px !Important;">
                            <div id="divOPBillingList">
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-8"></div>

                        <div class="form-group col-md-2" style="text-align: right">
                            <label>
                                Total Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">

                            <input type="text" class="form-control" id="txtTotalAmount" style="font-weight: bold; font-size: 18px;" placeholder="Total Amount"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountPercent">

                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="15" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Disc. Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountAmount">

                            <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="16" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divTaxName">

                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="17"></select>
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                CGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divCGST">

                            <input type="text" class="form-control" id="txtCGST" placeholder="CGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                SGST</label>
                        </div>
                        <div class="form-group col-md-1 " id="divSGST">

                            <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                IGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divIGST">

                            <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTaxAmount">

                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount" style="font-weight: bold; font-size: 18px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Round off</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff"
                                maxlength="15" tabindex="18" value="0" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">

                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 25px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Pay Mode</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divReceiptMode">
                            <select id="ddlReceiptMode" class="form-control" disabled="disabled" tabindex="19">
                                <option value="0" selected="selected">--Select--</option>
                                <option value="1">Cash</option>
                                <option value="5">Credit</option>
                                <option value="2">Cheque</option>
                                <option value="3">NEFT/RTGS</option>
                                <option value="4">Others</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Select A/c</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBank">
                            <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="20">
                            </select>
                        </div>
                        <div id="divChequeDetails">
                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    Trans #</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divChequeNo">
                                <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                    maxlength="150" tabindex="21" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    Issued Date</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divIssueDate">
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input class="form-control pull-right" tabindex="22" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                </div>
                            </div>

                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    Status</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divStatus">
                                <select id="ddlPaymentStatus" class="form-control" tabindex="23">
                                    <option value="Cleared">Cleared</option>
                                    <option value="Pending">Pending</option>
                                    <option value="Bounced">Bounced</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Description</label>
                        </div>
                        <div class="form-group col-md-3" id="divDescription">
                            <textarea id="txtDescription" class="form-control" maxlength="250" tabindex="24" rows="2" aria-autocomplete="none" style="width: 395%"></textarea>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile2" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload2_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile3" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 3</label>
                            <button id="btnClearImage3" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile4" data-image-src="imgUpload4_view" accept="image/*" onchange="ResizeImage('imagefile4');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload4_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
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
                                    maxlength="150" tabindex="28" autocomplete="off" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="29" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="30">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="31">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnExpenseID" />
    <input type="hidden" id="hdnExpenseTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnTransTaxPercent" />
    <input type="hidden" id="hdnTransCGSTPercent" />
    <input type="hidden" id="hdnTransSGSTPercent" />
    <input type="hidden" id="hdnTransIGSTPercent" />
    <input type="hidden" id="hdnTransCGSTAmount" />
    <input type="hidden" id="hdnTransSGSTAmount" />
    <input type="hidden" id="hdnTransIGSTAmount" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnBalanceAmt" />
    <input type="hidden" id="hdnPaidAmt" />
    <input type="hidden" id="hdnNetAmt" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jExpense.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jExpense.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
            var pageUrl = '<%=ResolveUrl("~/frmExpense.aspx") %>';

        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
        });


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

