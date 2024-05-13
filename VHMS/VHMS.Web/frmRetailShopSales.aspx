<%@ Page Title="Retail Sales" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRetailShopSales.aspx.cs" Inherits="frmRetailShopSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .btnPrint, .btnPrintbill {
            background-color: #ef00bc !important;
            margin-top: 0px !important;
        }

        .TableTrans > tbody > tr > td {
            padding: 2px !important;
        }

        /*.ui-corner-all {
            color: #000 !important;
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Retail Sales
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
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Customer</th>
                                                <th>Mobile No</th>
                                                <th>Area</th>
                                                <th>Payment Mode</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
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
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search Sales records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Customer</th>
                                                <th>Mobile No</th>
                                                <th>Area</th>
                                                <th>Payment Mode</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
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
            <div class="box box-primary" id="divOPBilling">
                <div class="box-header with-border">
                    <div class="box-title">Retail Sales Information</div>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="form-group col-md-2" id="divBillNo">
                            <label>
                                Bill No</label>

                            <input type="text" class="form-control" id="txtBillNo" placeholder="Bill No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <label>
                                Bill Date</label><span class="text-danger">*</span>

                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divMobileNo">
                            <label>
                                Mobile No</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtMobileNo" placeholder="Mobile No"
                                maxlength="10" tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divCustomer">
                            <label>
                                Customer</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtCustomer" placeholder="Name"
                                maxlength="500" tabindex="4" style="text-transform: uppercase;" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divCustomerType">
                            <label>
                                Customer Type</label><span class="text-danger">*</span>
                            <select id="ddlCustomerType" class="form-control select2" data-placeholder="Select CustomerType" tabindex="5"></select>

                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-6" id="divAddress">
                            <label>
                                Address</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtAddress" placeholder="Address"
                                maxlength="1000" tabindex="6" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divArea" style="display: none;">
                            <label>
                                Area</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtArea" placeholder="Area"
                                maxlength="500" tabindex="7" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divState">
                            <label>
                                State</label><span class="text-danger">*</span>
                            <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="8"></select>

                        </div>
                        <div class="form-group col-md-1" id="divchkExchange">
                            <input type="checkbox" id="chkExchange" style="margin-top: 30px" tabindex="-1" />
                            Exchange
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>

                        <table class="table table-hover TableTrans" id="participantTable">
                            <thead>
                                <tr>
                                    <th>S.No</th>
                                    <th>Barcode</th>
                                    <th>Product</th>
                                    <th>Quantity</th>
                                    <th>Rate</th>
                                    <th>Disc %</th>
                                    <th>Disc. Amt</th>
                                    <th>Tax %</th>
                                    <th>Tax Amt</th>
                                    <th>SubTotal</th>
                                </tr>
                            </thead>
                            <tr class="participantRow">
                                <td>
                                    <%-- <select id="participants" class="input-mini required-entry"  style="height: 32px; width: 44px;"  disabled="disabled">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                        <option value="11">11</option>
                                        <option value="12">12</option>
                                        <option value="13">13</option>
                                        <option value="14">14</option>
                                        <option value="15">15</option>
                                    </select>--%>
                                    <input type="text" class="form-control TRSearch" id="participants" placeholder="S.no"
                                        maxlength="12" tabindex="9" autocomplete="off" value="1" style="width: 50px;" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Barcode"
                                        maxlength="12" tabindex="9" autocomplete="off" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtProduct" placeholder="Product" style="width: 300px;"
                                        maxlength="12" tabindex="12" autocomplete="off" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="12" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="14" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtTaxPer" placeholder="TaxAmt"
                                        maxlength="12" tabindex="16" onkeypress="return IsNumeric(event)" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="TaxPer"
                                        maxlength="12" tabindex="16" onkeypress="return IsNumeric(event)" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="TaxAmt"
                                        maxlength="12" tabindex="16" onkeypress="return IsNumeric(event)" readonly="true" />
                                </td>
                            </tr>
                        </table>
                        <div class="table-responsive" style="min-height: 200px !Important;">
                            <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right add" tabindex="17">
                                <i class="fa fa-plus-square"></i>
                                <div id="divOPBillingList">
                                </div>
                        </div>
                    </div>
                    <div class="box box-primary box-solid" id="divExchange">
                        <div class="box-header">
                            Exchange
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divExchangeBarcode">
                                    <label>
                                        Bill No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeBillNo" placeholder="Bill No"
                                        maxlength="12" tabindex="19" />
                                </div>
                                <div class="form-group col-md-2" id="divExchangeProductName">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlExchangeProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="20"></select>
                                </div>
                                <div class="form-group col-md-1" id="divExchangeQuantity">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="21" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divExchangeRate">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeRate" placeholder="Rate"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1" id="divExchangeTaxAmt">
                                    <label>
                                        Tax Amt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeTaxAmt" placeholder="Tax Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divExchangeDisPer">
                                    <label>
                                        Disc %</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divExchangeDisAmt">
                                    <label>
                                        Disc. Amt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divExchangeSubTotal">
                                    <label>
                                        Subtotal</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtExchangeSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddExchange" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="22">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateExchange" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="23">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <div id="divExchangeList">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1" id="divTaxName">

                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="24"></select>
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Gift</label>
                        </div>
                        <div class="form-group col-md-1" id="divGift" style="display: none;">

                            <select id="ddlGift" class="form-control select2" data-placeholder="Select Gift" tabindex="25"></select>
                        </div>
                        <div class="form-group col-md-1">
                            <label style="font-size: 17px;">
                                Total Qty</label>
                        </div>
                        <div class="form-group col-md-1" id="divTotalQty">

                            <input type="text" class="form-control" id="txtTotalQty" style="font-weight: bold; font-size: 17px;" placeholder="Total Qty"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountPercent">

                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="26" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Disc Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountAmount">
                            <input type="text" class="form-control" id="txtDiscountAmount" style="font-size: 104%; font-weight: bold;" placeholder="Discount Amount"
                                maxlength="15" tabindex="27" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Total Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divAmount">

                            <input type="text" class="form-control" id="txtAmount" placeholder="Total Amount" style="font-size: 104%; font-weight: bold;"
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
                        <div class="form-group col-md-1">
                            <label>
                                SGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divSGST">

                            <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                IGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divIGST">

                            <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Tax Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divTaxAmount">

                            <input type="text" class="form-control" style="font-size: 104%; font-weight: bold;" id="txtTaxAmount" placeholder="Tax Amount"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Taxable Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">

                            <input type="text" class="form-control" style="font-size: 104%; font-weight: bold;" id="txtTotalAmount" placeholder="Total Amount"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none;">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2" id="divAdditionalDiscount">
                            <label style="font-size: 20px;">
                                Additional  Dis. Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divAdditionalDiscountAmount">
                            <input type="text" class="form-control" id="txtAdditionalDiscountAmount" placeholder="Additional Discount Amount"
                                maxlength="15" tabindex="28" value="0" onkeypress="return IsNumeric(event)" style="font-weight: bold; font-size: 20px;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Narration</label>
                        </div>
                        <div class="form-group col-md-3" id="divNarration">
                            <input type="text" class="form-control" id="txtNarration" placeholder="Narration"
                                maxlength="50" tabindex="29" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="font-size: 13px;">
                            <label>
                                Sales Points</label>
                        </div>
                        <div class="form-group col-md-1" id="divSalesPoints">
                            <input type="text" class="form-control" id="txtSalesPoints" placeholder="Sales Points"
                                maxlength="15" tabindex="-1" value="0" readonly="true" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Roundoff</label>
                        </div>
                        <div class="form-group col-md-1" id="divRoundoff">
                            <input type="text" class="form-control" style="font-size: 104%; font-weight: bold;" id="txtRoundoff" placeholder="Roundoff"
                                maxlength="15" tabindex="30" value="0" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Exchange Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divExchangeAmount">

                            <input type="text" class="form-control" style="font-size: 104%; font-weight: bold;" id="txtExchangeAmount" placeholder="ExchangeAmount"
                                maxlength="15" tabindex="-1" value="0" readonly="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-2" style="text-align: right">
                            <label style="font-size: 20px;">
                                Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">
                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 20px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                TenderAmount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTenderAmount">

                            <input type="text" class="form-control" id="txtTenderAmount" placeholder="TenderAmount"
                                maxlength="15" tabindex="31" value="0" onkeypress="return IsNumeric(event)" style="font-size: 20px;" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                BalanceGiven</label>
                        </div>
                        <div class="form-group col-md-2" id="divBalanceGiven">

                            <input type="text" class="form-control" id="txtBalanceGiven" placeholder="BalanceGiven"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" style="font-size: 25px;" autocomplete="off" />
                        </div>
                    </div>
                    <div class="box box-primary box-solid" id="divPayment">
                        <div class="box-header">
                            Payment mode
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divPaymentMode">
                                    <label>Payment Mode</label>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="32">
                                        <option value="Cash" selected="selected">Cash</option>
                                        <option value="Credit">Credit</option>
                                        <option value="DebitCard">Debit Card</option>
                                        <option value="CreditCard">Credit Card</option>
                                        <option value="GPay">GPay</option>
                                        <option value="Paytm">Paytm</option>
                                        <option value="IMPS">IMPS</option>
                                        <option value="NEFT/RTGS">NEFT/RTGS</option>
                                        <option value="Cheque">Cheque</option>
                                        <option value="Advance">Advance</option>
                                        <option value="Others">Others</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-2" id="divBankName">
                                    <label>Bank</label>
                                    <select id="ddlBankName" class="form-control select2" data-placeholder="Select Bank Name" tabindex="33"></select>
                                </div>
                                <div class="form-group col-md-1" id="divCardNo">
                                    <label>Ref. No</label>
                                    <input type="text" class="form-control" id="txtCardNo" placeholder="Ref. No"
                                        maxlength="16" tabindex="34" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divCollectionDate">
                                    <label>Collection Date</label>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="35" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-1" id="divPaidAmount">
                                    <label>Amount</label>
                                    <input type="text" class="form-control" id="txtPaidAmount" placeholder="Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divCardCharges">
                                    <label>Charges</label>
                                    <input type="text" class="form-control" id="txtCardCharges" placeholder="Charges"
                                        maxlength="15" tabindex="37" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divNotes">
                                    <label>Note</label>
                                    <input type="text" class="form-control" id="txtNotes" placeholder="Notes"
                                        maxlength="255" tabindex="38" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddPayment" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="39">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdatePayment" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="40">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <div id="divPaymentList">
                            </div>
                        </div>

                    </div>
                    <div class="row" style="display: none;">
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
                        <div class="form-group col-md-3">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile3" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Image 3</label>
                            <button id="btnClearImage3" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile4" data-image-src="imgUpload4_view" accept="image/*" onchange="ResizeImage('imagefile4');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload4_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-1" style="text-align: right" style="display: none;">
                            <label>
                                Bill Status</label>
                        </div>
                        <div class="form-group col-md-2" id="divBillStatus" style="display: none;">
                            <select id="ddlBillStatus" class="form-control" tabindex="32">
                                <option value="Completed Bill" selected="selected">Completed Bill</option>
                                <option value="Booking Bill">Booking Bill</option>
                            </select>
                        </div>

                        <div class="form-group col-md-1" style="text-align: right" style="display: none;">
                            <label>
                                Delivery Date</label>
                        </div>
                        <div class="form-group col-md-2" id="divDeliveryDate" style="display: none;">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtDeliveryDate" readonly />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="41">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                    <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="42">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                    <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="43">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                    <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="44">
                        <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>

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
                            maxlength="150" tabindex="20" autocomplete="off" />
                    </div>
                    <div class="form-group" id="divPassword">
                        <label>
                            Password</label><span class="text-danger">*</span>
                        <input type="text" class="form-control" id="txtPassword" placeholder="Please enter Password"
                            maxlength="150" tabindex="20" autocomplete="off" />
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
    <div class="modal fade" id="composedetails" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-body">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <div id="divDetailsList">
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btndetailsCancel" tabindex="22">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                </div>
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
    <div class="modal fade" id="composeRate" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group col-md-6" id="divSearchCode">
                            <label>
                                SMSCode</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtSearchCode" placeholder="Code"
                                maxlength="12" tabindex="7" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-6" id="divProduct">
                            <label>
                                Product</label><span class="text-danger">*</span>
                            <div id="divSelectProduct">
                                <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product Name" tabindex="6"></select>
                            </div>
                        </div>
                        <div class="form-group col-md-6" id="divSMSCode">
                            <label>
                                SMSCode</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtSMSCode" placeholder="Code"
                                maxlength="12" tabindex="-1" readonly />
                        </div>
                        <div class="form-group col-md-6" id="divPartyCode">
                            <label>
                                PartyCode</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtPartyCode" placeholder="Code"
                                maxlength="12" tabindex="-1" readonly />
                        </div>
                        <div class="form-group col-md-6" id="divWholeSalePrice">
                            <label>
                                Rate</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtWholeSalePrice" placeholder="Rate"
                                maxlength="12" tabindex="7" readonly="true" />
                        </div>
                        <div class="form-group col-md-6" id="divRetailPrice">
                            <label>
                                Retail Price</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtRetailPrice" placeholder="Retail Price"
                                maxlength="12" tabindex="7" readonly="true" />
                        </div>

                        <div class="form-group col-md-4" id="divRetailSalePriceA">
                            <label>
                                Retail Price A</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtRetailSalePriceA" placeholder="Retail Price A"
                                maxlength="12" tabindex="7" readonly="true" />
                        </div>
                        <div class="form-group col-md-4" id="divRetailSalePriceB">
                            <label>
                                Retail Price B</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtRetailSalePriceB" placeholder="Retail Price B"
                                maxlength="12" tabindex="7" readonly="true" />
                        </div>
                        <div class="form-group col-md-4" id="divRetailSalePriceC">
                            <label>
                                Retail Price C</label><span class="text-danger">*</span>
                            <input type="text" class="form-control TRSearch" id="txtRetailSalePriceC" placeholder="Retail Price C"
                                maxlength="12" tabindex="7" readonly="true" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btnRateCancel" tabindex="22">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="composedialog" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content" style="width: 165%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" style="text-align: center; color: #ff0000;"></h4>
                    <div class="form-group" id="divPasswords">
                        <label>
                            Password</label><span class="text-danger">*</span>
                        <input type="text" class="form-control" id="txtPasswords" placeholder="Please enter Password"
                            maxlength="150" tabindex="29" autocomplete="off" />
                    </div>
                </div>
                <div class="modal-footer clearfix" style="text-align: center">
                    <button type="button" class="btn btn-warning" id="btnCloseDialog" tabindex="33">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                OK</button>
                </div>
            </div>
        </div>
    </div>
    </section>
    </div>
    <input type="hidden" id="hdnSalesEntryID" />
    <input type="hidden" id="hdnSalesEntryTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnSMSCode" />
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnCGSTAmount" />
    <input type="hidden" id="hdnSGSTAmount" />
    <input type="hidden" id="hdnIGSTAmount" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnMaxDiscount" />
    <input type="hidden" id="hdnTransTaxPercent" />
    <input type="hidden" id="hdnTransCGSTPercent" />
    <input type="hidden" id="hdnTransSGSTPercent" />
    <input type="hidden" id="hdnTransIGSTPercent" />
    <input type="hidden" id="hdnTransCGSTAmount" />
    <input type="hidden" id="hdnTransSGSTAmount" />
    <input type="hidden" id="hdnTransIGSTAmount" />
    <input type="hidden" id="hdnExchangeTaxPercent" />
    <input type="hidden" id="hdnExchangeCGSTPercent" />
    <input type="hidden" id="hdnExchangeSGSTPercent" />
    <input type="hidden" id="hdnExchangeIGSTPercent" />
    <input type="hidden" id="hdnExchangeCGSTAmount" />
    <input type="hidden" id="hdnExchangeSGSTAmount" />
    <input type="hidden" id="hdnExchangeIGSTAmount" />
    <input type="hidden" id="hdnExchangeBarcode" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnPreQtyID" />
    <input type="hidden" id="hdnExchangeTaxID" />
    <input type="hidden" id="hdnDiscountAmount" />
    <input type="hidden" id="hdnAddDiscountAmount" />
    <input type="hidden" id="hdnExchangeCode" />
    <input type="hidden" id="hdnBalanceAmt" />
    <input type="hidden" id="hdnPaidAmt" />
    <input type="hidden" id="hdnNetAmt" />
    <input type="hidden" id="hdnStatus" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnPaymentSNo" />
    <input type="hidden" id="hdnExchangeSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jRetailShopSales.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jRetailShopSales.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmRetailSales.aspx") %>';
        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("[id$=txtBarcode]").change(function () {
                $("[id$=txtBarcode]").val(($("[id$=txtBarcode]").val().split('|')[0]).trim());
            });
            $("[id$=txtBarcode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeList") %>',
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
                    $("[id$=txtBarcode]").autocomplete("widget").css({
                        "width": ("200px"), "backgroundColor": ("#eac9c2"), "-webkit-text-fill-color": ("#000")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
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
                        "width": ("800px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });


            $("[id$=txtCustomer]").change(function () {
                var MobileNo = $("[id$=txtCustomer]").val().split('|')[2].trim();
                $("[id$=txtCustomer]").val(($("[id$=txtCustomer]").val().split('|')[0].trim()));
                $("#txtMobileNo").val(MobileNo).change();
            });
            $("[id$=txtCustomer]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetRetailSalesCustomer") %>',
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
                    $("[id$=txtCustomer]").autocomplete("widget").css({
                        "width": ("800px"), "backgroundColor": ("#f3f9d2"), "-webkit-text-fill-color": ("#000")

                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });


        });
            //$(document).ready(function () {
            //    SearchText();
            //});

    </script>

    <script type="text/javascript" src="JS/fancybox/jquery.fancybox.js?v=2.1.4"></script>
    <link rel="stylesheet" type="text/css" href="JS/fancybox/jquery.fancybox.css?v=2.1.4" media="screen" />
    <script type="text/javascript">
        document.onkeydown = function () {


            //    if (event.keyCode == 112 && event.altKey) {
            //        GetProductDetails($("#ddlProductName").val(), 0, $("#ddlCustomerName").val());
            //        $(".modal-title").html("&nbsp;&nbsp; Last 10 sales details of this Product to this Customer");
            //        $('#composedetails').modal({ show: true, backdrop: true });
            //    }
            //    if (event.keyCode == 113 && event.altKey) {
            //        if ($("#ddlProductName").val() > 0) {
            //            if ($("#txtCode").val() != "")
            //                GetProductDetails($("#ddlProductName").val(), 2, $("#ddlCustomerName").val());
            //            $(".modal-title").html("&nbsp;&nbsp; Last 10 sales details of this Product to all Customers");
            //            $('#composedetails').modal({ show: true, backdrop: true });
            //        }
            //    }
            //    if (event.keyCode == 114 && event.altKey) {
            //        GetRateByProduct();
            //        $(".modal-title").html("&nbsp;&nbsp; Search Rate");
            //        $('#composeRate').modal({ show: true, backdrop: true });
            //        $("#ddlProduct").val($("#ddlProductName").val()).change();
            //    }
        };

    </script>

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

