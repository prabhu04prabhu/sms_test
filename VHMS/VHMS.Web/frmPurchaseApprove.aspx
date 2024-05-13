<%@ Page Title="Purchase Approved" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPurchaseApprove.aspx.cs" Inherits="frmPurchaseApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Purchase Approved
                </h3>
            </div>
            <div class="breadcrumb" style="display: none;">
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
                                                <th>Purchase No</th>
                                                <th>Entry Date</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Supplier</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th>Created Emp.Name</th>
                                                <th>Modified Emp.Name</th>
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
                                        Search Purchase records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive" style="min-height: 10px !important">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                               <th>S.No</th>
                                                <th>Purchase No</th>
                                                <th>Entry Date</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Supplier</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th>Created Emp.Name</th>
                                                <th>Modified Emp.Name</th>
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
                    <div class="box-title">Purchase Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Purchase No</label>
                        </div>
                        <div class="form-group col-md-1" id="divBillNo">
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Purchase No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Entry Dt</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="1" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Bill No</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divNo">
                            <input type="text" class="form-control" id="txtNo" placeholder="Bill No"
                                maxlength="15" tabindex="2" autocomplete="off" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Bill Date</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divDate">
                            <%--  <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy" >--%>
                            <%-- <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>--%>
                            <input type="text" class="form-control pull-right" tabindex="3" id="txtDate" readonly="true" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-1">
                        <label>
                            Supplier</label><span class="text-danger">*</span>
                    </div>
                    <div class="form-group col-md-4" id="divSupplier">

                        <select id="ddlSupplierName" class="form-control select2" data-placeholder="Select Supplier Name" tabindex="4" readonly="true"></select>
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Tax</label><span class="text-danger">*</span>
                    </div>
                    <div class="form-group col-md-2" id="divTaxName">

                        <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="5" readonly="true"></select>
                    </div>
                    <div class="form-group col-md-4" id="divProductType">
                        <input type="radio" name="SupplierProduct" id="rdoSupplier" checked="checked" value="S" tabindex="-1" readonly="true" />Supplier Products 
                        <span style="padding-left: 30px" />
                        <input type="radio" name="SupplierProduct" id="rdoAll" value="A" tabindex="-1" readonly="true" />All Products
                    </div>
                </div>
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        Particulars
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="form-group col-md-2" id="divCode">
                                <label>
                                    Search Code</label>
                                <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code"
                                    maxlength="12" tabindex="6" autocomplete="off" readonly="true" />
                            </div>
                            <div class="form-group col-md-2" id="divProductName">
                                <label>
                                    Product</label><span class="text-danger">*</span>
                                <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="7" readonly="true"></select>
                            </div>
                            <div class="form-group col-md-1" id="divSMSCode" style="display: none">
                                <label>
                                    SMS Code</label>
                                <input type="text" class="form-control TRSearch" id="txtSMSCode" placeholder="Code"
                                    maxlength="12" tabindex="-1" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divPartyCode">
                                <label>
                                    Party Code</label>
                                <input type="text" class="form-control TRSearch" id="txtPartyCode" placeholder="Code"
                                    maxlength="12" tabindex="-1" readonly="true" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divQuantity">
                                <label>
                                    Quantity</label><span class="text-danger">*</span>
                                <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                    maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divRate">
                                <label>
                                    Rate</label><span class="text-danger">*</span>
                                <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                    maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divTaxTrans">
                                <label>
                                    Tax</label><span class="text-danger">*</span>
                                <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax " tabindex="10" readonly="true"></select>
                            </div>
                            <div class="form-group col-md-1" id="divTaxAmt" style="display: none">
                                <label>
                                    Tax Amt</label><span class="text-danger">*</span>
                                <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Disc. Amt"
                                    maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divDisPer">
                                <label>
                                    Disc %</label>
                                <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                    maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divDisAmt">
                                <label>
                                    Disc. Amt</label>
                                <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc.Amt"
                                    maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" readonly="true" />
                            </div>
                            <div class="form-group col-md-1" id="divFrequency">
                                <label>
                                    Subtotal</label>
                                <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                    maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" readonly="true" />
                            </div>
                            <div class="form-group col-md-2" id="divBarcode" style="display: none">
                                <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Subtotal"
                                    maxlength="12" tabindex="12" onkeypress="return IsNumeric(event)" readonly="true" />
                            </div>

                            <div class="form-group col-md-1 pull-right" style="display: none;">
                                <div class="margin">
                                    <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                    <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="14">
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
                <br />
                <br />
                <div class="row">
                    <div class="form-group col-md-1">
                        <label>
                            Upload</label>
                    </div>
                    <div class="form-group col-md-2">
                        <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                            runat="server" ID="imgupload1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="214px"
                            OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                        <input type="hidden" id="hdnImgupload1" readonly="true" />

                    </div>
                    <div class="form-group col-md-1" style="display: none">
                        <img src="" id="imgUpload1" alt="" style="visibility: hidden;" />
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Discount %</label>
                    </div>
                    <div class="form-group col-md-1" id="divDiscountPercent">
                        <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                            maxlength="15" tabindex="15" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" readonly="true" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            Disc. Amt</label>
                    </div>
                    <div class="form-group col-md-1" id="divDiscountAmount">
                        <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount"
                            maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                    </div>
                    <div class="form-group col-md-2" style="text-align: right">
                        <label>
                            Total Amount</label>
                    </div>
                    <div class="form-group col-md-2" id="divTotalAmount">
                        <input type="text" class="form-control" id="txtTotalAmount" placeholder="Total Amount"style="font-size: 104%;font-weight: bold;"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-1">
                        <label>
                            CGST</label>
                    </div>
                    <div class="form-group col-md-1" id="divCGST">
                        <input type="text" class="form-control" id="txtCGST" placeholder="CGST"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            SGST</label>
                    </div>
                    <div class="form-group col-md-1" id="divSGST">
                        <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            IGST</label>
                    </div>
                    <div class="form-group col-md-1" id="divIGST">
                        <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            Tax Amount</label>
                    </div>
                    <div class="form-group col-md-2" id="divTaxAmount">
                        <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount"style="font-size: 104%;font-weight: bold;"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-2">
                        <input type="checkbox" id="chk" value="value" />
                        <label for="chk" style="font-size: 15px;" readonly="true">Calculate TCS </label>
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            TCS %</label>
                    </div>
                    <div class="form-group col-md-1" id="divTCSPercent">
                        <input type="text" class="form-control" id="txtTCSPercent" placeholder="TCS Percent"
                            maxlength="15" tabindex="16" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            TCS Amt</label>
                    </div>
                    <div class="form-group col-md-1" id="divTCSAmount">
                        <input type="text" class="form-control" id="txtTCSAmount" placeholder="TCS Amount"
                            maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                    </div>
                    <div class="form-group col-md-1"></div>
                    <div class="form-group col-md-1">
                        <label>
                            Roundoff</label>
                        <span class="text-danger">*</span>
                    </div>
                    <div class="form-group col-md-2" id="divRoundoff">
                        <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff"style="font-size: 104%;font-weight: bold;"
                            maxlength="15" tabindex="17" value="0" autocomplete="off" readonly="true" />
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-md-1">
                        <label>
                            Verified By</label><span class="text-danger">*</span>
                    </div>
                    <div class="form-group col-md-2" id="divVerifiedBy">

                        <select id="ddlVerifiedBy" class="form-control select2" data-placeholder="" tabindex="5" readonly="true"></select>
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Confirm By</label><span class="text-danger">*</span>
                    </div>
                    <div class="form-group col-md-2" id="divConfirmedBy">
                        <select id="ddlConfirmedBy" class="form-control select2" data-placeholder="" tabindex="5" readonly="true"></select>
                    </div>
                    <div class="form-group col-md-1">
                        <label style="font-size: 20px;">
                            Total Qty</label>
                    </div>
                    <div class="form-group col-md-1" id="divTotalQty">

                        <input type="text" class="form-control" id="txtTotalQty" style="font-weight: bold; font-size: 20px;" placeholder="Total Qty"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                    </div>
                    <div class="form-group col-md-2" style="text-align: right">
                        <label style="font-size: 20px;">Net Amount</label>
                    </div>
                    <div class="form-group col-md-2" id="divNetAmount">

                        <input type="text" class="form-control" id="txtNetAmount" style="font-weight: bold; font-size: 20px;" placeholder="Net Amount"
                            maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-12" id="divAddress">
                        <label>
                            Comments</label>
                        <textarea id="txtComments" class="form-control" maxlength="255" tabindex="5" rows="3" aria-autocomplete="none"></textarea>
                    </div>
                </div>
                 <div class="row">
                        <div class="form-group col-md-2" id="divOtherCharges">
                             <label>Other Charges</label>
                            <input type="text" class="form-control" id="txtOtherCharges" placeholder="Other Charges"
                                maxlength="15" tabindex="24" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divCourierCharges">
                            <label>Courier Charges</label>
                            <input type="text" class="form-control" id="txtCourierCharges" placeholder="Courier Charges"
                                maxlength="15" tabindex="24" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>                       
                        <div class="form-group col-md-2" id="divPaymentDiscountPercent">
                            <label>
                                Payment Disc.%</label>
                            <input type="text" class="form-control" id="txtPaymentDiscountPercent" placeholder="Payment Discount %"
                                maxlength="15" tabindex="24" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                         <div class="form-group col-md-2" id="divDis_amt_Type">
                            <label>Calculate for</label>
                            <select id="ddlDis_amt_Typ" class="form-control select2" data-placeholder="Select Type" tabindex="24">
                                <option value="NetAmount">Net Amount</option>
                                <option value="TaxableAmount">Taxable Amount</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divPaymentDiscount">
                             <label>
                                Payment Disc. Amt</label>
                            <input type="text" class="form-control" id="txtPaymentDiscount" placeholder="Payment Discount"
                                maxlength="15" tabindex="24" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                <div class="modal-footer clearfix">
                    <button id="btnSave" type="button" class="btn btn-info pull-left" tabindex="19" style="display: none;">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save & Print</button>
                    <button id="btnUpdate" type="button" class="btn btn-info pull-left" tabindex="20">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Verified</button>
                    <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="21">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
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
                                    maxlength="150" tabindex="20" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="20" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="22">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composedialog" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title" style="text-align: center"></h4>
                        </div>
                        <div class="modal-footer clearfix" style="text-align: center">
                            <button type="button" class="btn btn-warning" id="btnCloseDialog" tabindex="6">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnPurchaseID" />
    <input type="hidden" id="hdnPurchaseTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnCGSTAmount" />
    <input type="hidden" id="hdnSGSTAmount" />
    <input type="hidden" id="hdnIGSTAmount" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnTransTaxPercent" />
    <input type="hidden" id="hdnTransCGSTPercent" />
    <input type="hidden" id="hdnTransSGSTPercent" />
    <input type="hidden" id="hdnTransIGSTPercent" />
    <input type="hidden" id="hdnTransCGSTAmount" />
    <input type="hidden" id="hdnTransSGSTAmount" />
    <input type="hidden" id="hdnTransIGSTAmount" />
    <input type="hidden" id="hdnBalanceAmt" />
    <input type="hidden" id="hdnPaidAmt" />
    <input type="hidden" id="hdnNetAmt" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jPurchaseApprove.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jPurchaseApprove.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmProduct.aspx") %>';
        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });


            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val($("[id$=txtCode]").val().split('|')[0]);
            });
            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
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
                    $("[id$=txtCode]").autocomplete("widget").css({
                        "width": ("600px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
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
                    $("#hdnImgupload1").val(r.d);
                    $get("imgUpload1").src = "./images/Documents/Purchase/" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }



    </script>
    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 113) {
                var myWindow = window.open("frmDPurchase.aspx", "_self");
            }

        };
    </script>
    <%--  <style>
        .fullscreen-container {
            display: none;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(90, 90, 90, 0.5);
            z-index: 9999;
        }

        #popdiv {
            height: 300px;
            width: 420px;
            background-color: #97ceaa;
            position: center;
            top: 50px;
            left: 50px;
        }

        
    </style>--%>
</asp:Content>

