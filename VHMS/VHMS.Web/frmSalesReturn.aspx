<%@ Page Title="Sales Return" Language="C#" MasterPageFile="~/VHMSMasterPage.master"
    AutoEventWireup="true" CodeFile="frmSalesReturn.aspx.cs" Inherits="frmSalesReturn" %>

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
                <h3>Sales Return
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
                                                <th>Credit Note(Ref.No)</th>
                                                <th>CreditNote(Date)</th>
                                                <th>Debit No</th>
                                                <th>Debit Date</th>
                                                <th>Customer</th>
                                                <th>Invoice No</th>
                                                <th>Return Amount</th>
                                                <th>Total Qty</th>
                                                <th></th>
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
                                                    <th>Credit Note(Ref.No)</th>
                                                    <th>CreditNote(Date)</th>
                                                    <th>Customer</th>
                                                    <th>Invoice No</th>
                                                    <th>Return Amount</th>
                                                    <th>Total Qty</th>
                                                    <th></th>
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
            <div class="box box-primary" id="divSalesReturn">
                <div class="box-header with-border">
                    <div class="box-title">SalesReturn Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2" id="divReturnNo">
                            <label>
                                Credit Note(Ref.No)</label>

                            <input type="text" class="form-control" id="txtReturnNo" placeholder="Ref. No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divReturnDate">
                            <label>
                                CreditNote(Date)</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtReturnDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="-1" id="txtReturnDate" readonly="true" disabled="disabled" />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divBillNo">
                            <label>Debit note #</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Debit note #" maxlength="30" tabindex="1" />
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <label>Debit note Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtBillDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon"><i class="fa fa-calendar glyphicon glyphicon-calendar"></i></div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divCustomer">
                            <label>
                                Customer</label>
                            <input type="text" class="form-control" id="txtName" placeholder="Customer Name"
                                maxlength="15" tabindex="-1" readonly="true" style="display: none;" />
                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="4" disabled="disabled"></select>
                        </div>
                        <div class="form-group col-md-2" id="divOPDNo">
                            <label>
                                Invoice No(Adjust)</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtInvoiceNo" placeholder=""
                                maxlength="15" tabindex="4" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divOldInvoiceNo">
                            <label>
                                Old Invoice No</label>
                            <input type="text" class="form-control" id="txtOldInvoiceNo" placeholder=""
                                maxlength="15" tabindex="4" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divInvoiceDate">
                            <label>
                                Invoice Date</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtInvoiceDate" placeholder=""
                                maxlength="15" tabindex="-1" autocomplete="off" readonly="true" />
                        </div>
                        <div class="form-group col-md-3" id="divProductType" style="margin-top: 25px">
                            <input type="radio" name="SupplierProduct" id="rdoSupplier" checked="checked" value="S" tabindex="-1" />
                            Invoice Products 
                        <span style="padding-left: 30px" />
                            <input type="radio" name="SupplierProduct" id="rdoAll" value="A" tabindex="-1" />
                            All Products
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label id="txtTaxPercent">0</label><span>%</span>
                        </div>
                        <div class="form-group col-md-2" id="divtax">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                            <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax" tabindex="4">
                            </select>
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-1" id="divSno" style="width: 6%;">
                                    <label>
                                        S.No</label>
                                    <input type="text" class="form-control TRSearch" id="txtSNo" placeholder="Sno"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divCode" style="display: none;">
                                    <label>
                                        Search Code</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code"
                                        maxlength="12" tabindex="5" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divProductName">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <div id="divSelectProductName">
                                        <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="6"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-1" id="divSMSCode">
                                    <label>
                                        SMSCode</label>
                                    <input type="text" class="form-control TRSearch" id="txtSMSCode" placeholder="Code"
                                        maxlength="12" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divAvailableQty">
                                    <label>
                                        Billed Qty</label>
                                    <input type="text" class="form-control TRSearch" id="txtAvailableQty" placeholder="Billed Qty "
                                        maxlength="12" tabindex="-1" readonly="true" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divQuantity">
                                    <label>
                                        Return Qty</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Return Qty"
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divRate">
                                    <label>
                                        Rate</label>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="8" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer">
                                    <label>
                                        Disc %</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" onkeypress="return IsNumeric(event)" tabindex="9" />
                                </div>
                                <div class="form-group col-md-1" id="divDisAmt">
                                    <label>
                                        Disc. Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divTaxTrans">
                                    <label>
                                        Tax</label><span class="text-danger">*</span>
                                    <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax " tabindex="10"></select>
                                </div>
                                <div class="form-group col-md-1" id="divTaxAmt">
                                    <label>
                                        Tax Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Tax. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divFrequency">
                                    <label>
                                        Subtotal</label>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divBarcode" style="display: none">
                                    <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divNotes">
                                    <label>
                                        Notes</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtNotes" placeholder="Notes"
                                        maxlength="500" tabindex="11" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive" style="min-height: 400px !Important;">
                            <div id="divOPBillingList">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12"></div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                CGST</label>
                        </div>
                        <div class="form-group col-md-2" id="divCGST">

                            <input type="text" class="form-control" id="txtCGST" placeholder="CGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-5"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Gross total</label>
                        </div>
                        <div class="form-group col-md-2" id="divSubtotal">
                            <input type="text" class="form-control" id="txtGrossTotal" placeholder="Gross Total" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                            <input type="text" class="form-control" id="txtSubtotal" placeholder="Subtotal"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" style="display: none; font-size: 104%; font-weight: bold;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                SGST</label>
                        </div>
                        <div class="form-group col-md-2" id="divSGST">

                            <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountPercent">
                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="14" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Discount Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscount">
                            <input type="text" class="form-control" id="txtDiscount" placeholder="Reduction" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                IGST</label>
                        </div>
                        <div class="form-group col-md-2" id="divIGST">

                            <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                        <div class="form-group col-md-5"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Return Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTaxAmount">
                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                    </div>
                    <div class="row" hidden="hidden">
                        <div class="form-group col-md-2">
                            <label>
                                CGST Amount</label>
                        </div>
                        <label id="txtCGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divCGSTAmount">
                            <input type="text" class="form-control" id="txtCGSTAmount" placeholder="CGST Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                SGST Amount</label>
                        </div>
                        <label id="txtSGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divSGSTAmount">
                            <input type="text" class="form-control" id="txtSGSTAmount" placeholder="SGST Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                IGST Amount</label>
                        </div>
                        <label id="txtIGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divIGSTAmount">
                            <input type="text" class="form-control" id="txtIGSTAmount" placeholder="IGST Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                      <div class="form-group col-md-1">
                            <label>
                                IRN No</label>
                        </div>
                        <div class="form-group col-md-7" id="divIRNNo">
                            <input type="text" class="form-control" id="txtIRNNo" placeholder="IRN No"
                                maxlength="1000" tabindex="14"  autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2">
                            <label>
                                Roundoff</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="15" value="0" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                      <div class="form-group col-md-1">
                            <label>
                                ACK No</label>
                        </div>
                        <div class="form-group col-md-5" id="divACKNo">
                            <input type="text" class="form-control" id="txtACKNo" placeholder="ACK No"
                                maxlength="1000" tabindex="16"  autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1">
                            <label style="font-size: 17px;">
                                Total Qty</label>
                        </div>
                        <div class="form-group col-md-1" id="divTotalQty">
                            <input type="text" class="form-control" id="txtTotalQty" placeholder="Total Qty" style="font-weight: bold; font-size: 17px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2">
                            <label style="font-size: 20px;">
                                Return Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">
                            <input type="text" class="form-control" id="txtTotalAmount" placeholder="Return Amount" style="font-weight: bold; font-size: 20px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile" data-image-src="imgUploadPurchase1_view" accept="image/*" onchange="ResizeImage('imagePurchasefile');" />
                            <img src="" id="imgUploadPurchase1_view" alt="" style="width: 280px;" />
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile2" data-image-src="imgUploadPurchase2_view" accept="image/*" onchange="ResizeImage('imagePurchasefile2');" />
                            <img src="" id="imgUploadPurchase2_view" alt="" style="width: 280px;" />
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 3</label>
                            <button id="btnClearImage3" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile3" data-image-src="imgUploadPurchase3_view" accept="image/*" onchange="ResizeImage('imagePurchasefile3');" />
                            <img src="" id="imgUploadPurchase3_view" alt="" style="width: 280px;" />
                        </div>
                        <div class="form-group col-md-8" id="divAddress">
                            <label>
                                Comments</label>
                            <textarea id="txtComments" class="form-control" maxlength="1000" tabindex="17" rows="3" aria-autocomplete="none" style="width: 152%"></textarea>
                        </div>
                    </div>

                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="18">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="19">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="20">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="21">
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
                            <button type="button" class="btn btn-danger pull-left" id="btnImageCancel" tabindex="22">
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
                            <button type="button" class="btn btn-danger pull-left" id="btnProductImageCancel" tabindex="23">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnSalesReturnID" />
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
    <script src="UserDefined_Js/JSalesReturn.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JSalesReturn.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmSalesReturn.aspx") %>';
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
