<%@ Page Title="Outward" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmOutward.aspx.cs" Inherits="frmOutward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .btnPrint, .btnPrintbill {
            background-color: #ef00bc !important;
            margin-top: 0px !important;
        }

        table.formatHTML5 tr.selected {
            background-color: #e92929 !important;
            color: #fff;
            vertical-align: middle;
            padding: 1.5em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Outward
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
                                                <th>Invoice #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <%--        <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>--%>
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
                                        Search Whole Outwards records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Invoice #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <%--  <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>--%>
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
                    <div class="box-title">Whole Outwards Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1" id="divBillNo">
                            <label>
                                Invoice No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Invoice No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <%-- <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCustomer" style="width: 282px; display: none;">

                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="3"></select>
                        </div>
                        <div class="form-group col-md-3" id="divShippingAddressName1" style="width: 156px; display: none;">
                            <label>
                                Shipping Address</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divShippingAddressName">
                            <select id="ddlShippingAddress" class="form-control select2" data-placeholder="Select Shipping Address" tabindex="4"></select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Cust.Type</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCustomerType" style="display: none;">
                            <input type="text" class="form-control" id="txtCustomertype" placeholder="Customer Type"
                                maxlength="10" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Mobile No</label>
                        </div>
                        <div class="form-group col-md-2" id="divMobileNo">
                            <input type="text" class="form-control" id="txtMobileNo" placeholder="MobileNo"
                                maxlength="10" tabindex="-1" readonly="true" />
                        </div>--%>
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
                        <div class="form-group col-md-4" id="divAddress">
                            <label>
                                Address</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtAddress" placeholder="Address"
                                maxlength="1000" tabindex="6" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divArea">
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
                        <div class="form-group col-md-2" id="divGsTNo" style="display: none;">
                            <label>
                                GST No</label>
                            <input type="text" class="form-control" id="txtGSTNo" placeholder="GSTNo"
                                maxlength="10" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divTaxName">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="5"></select>
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
                                <div class="form-group col-md-2" id="divCode" style="margin-left: -21px;">
                                    <label>
                                        SMSCode</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code"
                                        maxlength="12" tabindex="6" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divProductName" style="margin-left: -21px;">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <div id="divSelectProductName">
                                        <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="7"></select>
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
                                <div class="form-group col-md-1" id="divTaxAmt" style="margin-left: -21px;">
                                    <label>
                                        Tax Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Tax. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer" style="margin-left: -21px;">
                                    <label>
                                        Disc %</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divDisAmt" style="margin-left: -21px;">
                                    <label>
                                        Disc. Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divFrequency" style="margin-left: -21px;">
                                    <label>
                                        Subtotal</label>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divBarcode" style="display: none">
                                    <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
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
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2" style="text-align: right;">
                            <label>
                                Total Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divAmount">
                            <input type="text" class="form-control" id="txtAmount" placeholder="Total Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountPercent">
                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="14" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" style="text-align: right;">
                            <label>
                                Discount Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountAmount">
                            <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="15" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-2" style="display: none;">
                            <input type="checkbox" id="chk" value="value" style="display: none;" />
                            <label for="chk">Calculate TCS </label>

                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                TCS %</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSPercent" style="display: none;">
                            <input type="text" class="form-control" id="txtTCSPercent" placeholder="TCS Percent"
                                maxlength="15" tabindex="16" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-5" style="display: none;">
                            <label>
                                TCS Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSAmount" style="display: none;">
                            <input type="text" class="form-control" id="txtTCSAmount" placeholder="TCS Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label style="font-size: 17px;">
                                Total Qty</label>
                        </div>
                        <div class="form-group col-md-1" id="divTotalQty">
                            <input type="text" class="form-control" id="txtTotalQty" style="font-weight: bold; font-size: 17px;" placeholder="Total Qty"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-8" style="text-align: right;">
                            <label>
                                Taxable Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">
                            <input type="text" class="form-control" id="txtTotalAmount" placeholder="Total Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-2">
                        </div>
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
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-1" style="text-align: right;">
                            <label>
                                Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTaxAmount">
                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                After Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalTaxAmount">

                            <input type="text" class="form-control" id="txtTotalTaxAmount" placeholder="Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="21" value="0" autocomplete="off" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9">
                        </div>
                        <div class="form-group col-md-1" style="text-align: right;">
                            <label>
                                Other Charges</label>
                        </div>
                        <div class="form-group col-md-2" id="divOtherCharges">
                            <input type="text" class="form-control" id="txtOtherCharges" placeholder="Other Charges" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-2" id="divRCustomerToatlAmountall" style="display: none;">
                            <input type="text" class="form-control" id="txtCustomerToatlAmountall" placeholder="ToatlAmount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                        <div class="form-group col-md-2" id="divRCustomerToatlAmount" style="display: none;">
                            <input type="text" class="form-control" id="txtCustomerToatlAmount" placeholder="ToatlAmount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right;">
                            <label>
                                Roundoff</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Agent Name</label>
                        </div>
                        <div class="form-group col-md-2" id="divAgent" style="display: none;">
                            <select id="ddlAgent" class="form-control select2" data-placeholder="Select Agent Name" tabindex="17"></select>
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                IRN No</label>
                        </div>
                        <div class="form-group col-md-2" id="divIRNNo" style="display: none;">
                            <input type="text" class="form-control" id="txtIRNNo" placeholder=" IRN No"
                                maxlength="50" tabindex="18" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                ACK No</label>
                        </div>
                        <div class="form-group col-md-2" id="divACKno" style="display: none;">
                            <input type="text" class="form-control" id="txtACKno" placeholder="ACK No"
                                maxlength="50" tabindex="19" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-10" style="text-align: right">
                            <label style="font-size: 20px;">
                                Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">
                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 20px;"
                                maxlength="15" tabindex="-1" readonly="true" style="width: 192px;" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Transport Name</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportName" style="display: none;">
                            <select id="ddlTransport" class="form-control select2" data-placeholder="Select Transport Name" tabindex="20"></select>
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Transport GSTNo</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportGSTNo" style="display: none;">
                            <input type="text" class="form-control" id="txtTransportGSTNo" placeholder="Transport GSTNo"
                                maxlength="50" tabindex="-1" autocomplete="off" readonly="true" />
                        </div>

                        <div class="form-group col-md-2" style="display: none;">
                            <button id="btnLink" type="button" class="btn btn-link">
                                <i class="fa fa-link" aria-hidden="true"></i>&nbsp;&nbsp;
                              Eway link</button>
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                VehicleNo</label>
                        </div>
                        <%--<div class="form-group col-md-2" id="divVehicleNo" style="display: none;">

                            <input type="text" class="form-control" id="txtVehicleNo" placeholder=" Vehicle No"
                                maxlength="50" tabindex="21" autocomplete="off" />
                        </div>--%>
                        <div class="form-group col-md-10" style="text-align: right;">
                            <label>
                                Transport Charges</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportCharge">
                            <input type="text" class="form-control" id="txtTransportCharge" placeholder="Transport Charges" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="22" value="0" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-10" id="divOtherPasswordlbl" style="text-align: right;">
                            <label>
                                Password</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divOtherPassword">
                            <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" autocomplete="off" maxlength="512"
                                tabindex="26" />
                        </div>

                        <div class="form-group col-md-2"></div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                TenderAmount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTenderAmount" style="display: none;">

                            <input type="text" class="form-control" id="txtTenderAmount" placeholder="TenderAmount"
                                maxlength="15" tabindex="-1" value="0" style="font-size: 20px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                BalanceGiven</label>
                        </div>
                        <div class="form-group col-md-2" id="divBalanceGiven" style="display: none;">

                            <input type="text" class="form-control" id="txtBalanceGiven" placeholder="BalanceGiven"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" style="font-size: 25px;" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="margin-left: -11px; display: none;">
                            <label>
                                Payment Mode</label>
                        </div>
                        <div class="form-group col-md-2" style="display: none;">
                            <select id="ddlPaymentMode" class="form-control" style="width: 192px;" tabindex="-1">
                                <option value="Credit" selected="selected">Credit</option>
                                <option value="Cash">Cash</option>
                                <option value="Card">Card</option>
                                <option value="NEFT/RTGS">NEFT/RTGS</option>
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                LR No</label>
                        </div>
                        <div class="form-group col-md-1" id="divLRNo" style="display: none;">
                            <input type="text" class="form-control" id="txtLRNo" placeholder=" LR No"
                                maxlength="50" tabindex="-1" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                LR Date</label>
                        </div>
                        <div class="form-group col-md-2" id="divLRDate" style="display: none;">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="-1" id="txtLRDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-1"></div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                EWayNo</label>
                        </div>
                        <div class="form-group col-md-2" id="divEWayNo" style="display: none;">
                            <input type="text" class="form-control" id="txtEWayNo" placeholder="EWayNo"
                                maxlength="50" tabindex="23" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Noof Bages</label>
                        </div>
                        <div class="form-group col-md-2" id="divNoofBages" style="display: none;">
                            <input type="text" class="form-control" id="txtNoofBages" placeholder=" Noof Bages"
                                maxlength="50" tabindex="24" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Narration</label>
                        </div>
                        <div class="form-group col-md-2" id="divNarration" style="display: none;">
                            <input type="text" class="form-control" id="txtNarration" placeholder="Narration"
                                maxlength="500" tabindex="25" autocomplete="off" />
                        </div>

                    </div>
                    <div class="form-group col-md-12" id="divAddress">
                        <label>
                            Notes</label>
                        <textarea id="txtComments" class="form-control" maxlength="255" tabindex="27" rows="3" aria-autocomplete="none" style="width: 102%"></textarea>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4" style="display: none;">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile2" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload2_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4" style="display: none;">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile3" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4" style="display: none;">
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
                </div>
                <div class="row" style="display: none" id="divBank">
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="margin-left: -11px;">
                            <label>
                                Card No</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCardNo">

                            <input type="text" class="form-control" id="txtCardNo" placeholder="CardNo"
                                maxlength="16" tabindex="29" style="width: 192px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="margin-left: -11px;">
                            <label>
                                Card Charges</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCardCharges">

                            <input type="text" class="form-control" id="txtCardCharges" placeholder="CardCharges"
                                maxlength="15" tabindex="30" value="0" style="width: 192px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                    <div class="form-group col-md-9"></div>
                    <div class="form-group col-md-1">
                        <label>
                            Bank</label>
                    </div>
                    <div class="form-group col-md-2">
                        <select id="ddlBankName" class="form-control select2" data-placeholder="Select Bank Name" tabindex="31"></select>
                    </div>

                </div>

                <div class="modal-footer clearfix">
                    <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="32">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                    <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="33">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                    <button type="button" class="btn btn-danger margin pull-right" id="btnClose" tabindex="34">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                    <button id="btnPrintbill" type="button" class="btn btn-info btnPrint pull-left" style="margin-top: 10px !important;" tabindex="32">
                        <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>
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
                                    maxlength="150" tabindex="35" />
                            </div>
                            <div class="form-group" id="divPassword">
                                <label>
                                    Password</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtPassword" placeholder="Please enter Password"
                                    maxlength="150" tabindex="36" autocomplete="off" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="37" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="38">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="39">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composedetails" tabindex="-1" role="dialog" aria-hidden="true" style="margin-top: -25px;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 10px; padding: 0px;">
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
                        <div class="modal-footer clearfix" style="display: none;">
                            <button type="button" class="btn btn-danger pull-left" id="btndetailsCancel" tabindex="40">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="Allcomposedetails" tabindex="-1" role="dialog" aria-hidden="true" style="margin-top: -25px;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="width: 105%;">
                        <div class="modal-header" style="height: 10px; padding: 0px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="Allmodal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div id="divAllDetailsList">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix" style="display: none;">
                            <button type="button" class="btn btn-danger pull-left" id="btnAlldetailsCancel" tabindex="41">
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
                            <button type="button" class="btn btn-danger pull-left" id="btnImageCancel" tabindex="42">
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
                                        maxlength="12" tabindex="43" />
                                </div>
                                <div class="form-group col-md-6" id="divProduct">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <div id="divSelectProduct">
                                        <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product Name" tabindex="44"></select>
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
                                <div class="form-group col-md-4" id="divPurchasePrice">
                                    <label>
                                        Purchase Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPurchasePrice" placeholder="Purchase Price"
                                        maxlength="12" tabindex="45" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePrice">
                                    <label>
                                        WholeSale Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePrice" placeholder="WholeSale Price"
                                        maxlength="12" tabindex="46" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divRetailPrice">
                                    <label>
                                        Retail Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRetailPrice" placeholder="Retail Price"
                                        maxlength="12" tabindex="47" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceA">
                                    <label>
                                        WholeSale Price A</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceA" placeholder="WholeSale Price A"
                                        maxlength="12" tabindex="48" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceB">
                                    <label>
                                        WholeSale Price B</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceB" placeholder="WholeSale Price B"
                                        maxlength="12" tabindex="49" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceC">
                                    <label>
                                        WholeSale Price C</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceC" placeholder="WholeSale Price C"
                                        maxlength="12" tabindex="50" readonly="true" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnRateCancel" tabindex="51">
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
                            <button type="button" class="btn btn-danger pull-left" id="btnProductImageCancel" tabindex="52">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnOutwardID" />
    <input type="hidden" id="hdnOutwardsID" />
    <input type="hidden" id="hdnLastinvoiceDate" />
    <input type="hidden" id="hdnOutwardTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnSMSCode" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnCGSTAmount" />
    <input type="hidden" id="hdnSGSTAmount" />
    <input type="hidden" id="hdnIGSTAmount" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnstateID" />
    <input type="hidden" id="hdnOutwardsLimit" />
    <input type="hidden" id="hdnCustomerBalanceAmount" />
    <input type="hidden" id="hdnOutwardsDiscountPercent" />
    <input type="hidden" id="hdnMaxOutwardsDiscount" />
    <input type="hidden" id="hdnCustomerTypeID" />
    <input type="hidden" id="hdnCustomerID" />
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
    <input type="hidden" id="hdnStatus" />
    <input type="hidden" id="hdnIsAllProduct" />
    <input type="hidden" id="hdnOriginalRate" />
    <input type="hidden" id="hdnRateChanged" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jOutward.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jOutward.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmOutward.aspx") %>';
        $(document).ready(function () {

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
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
        });
           $("[id$=txtCustomer]").change(function () {
                var MobileNo = $("[id$=txtCustomer]").val().split('|')[2].trim();
                $("[id$=txtCustomer]").val(($("[id$=txtCustomer]").val().split('|')[0].trim()));
                $("#txtMobileNo").val(MobileNo).change();
            });
            $("[id$=txtCustomer]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetWholeSalesCustomer") %>',         
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

    </script>
    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 67 && event.altKey) {
                $('#hdnIsAllProduct').val(0);
                if ($("#ddlProductName").val() > 0) {
                    if ($("#txtCode").val() != "")
                        GetOutwardsProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 0, $("#ddlCustomerName").val());
                    else
                        GetOutwardsProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 0, $("#ddlCustomerName").val());
                    //$(".modal-title").html("&nbsp;&nbsp; Last 10 Outwards details of this Product to this Customer");

                    //$('#composedetails').modal({ show: true, backdrop: true });
                }
            }

            if (event.keyCode == 86 && event.altKey) {
                $('#hdnIsAllProduct').val(1);
                if ($("#ddlProductName").val() > 0) {
                    if ($("#txtCode").val() != "")
                        GetAllProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 1, $("#ddlCustomerName").val());
                    else
                        GetAllProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 1, $("#ddlCustomerName").val());
                }
            }
            if (event.keyCode == 81 && event.altKey) {
                GetRateByProduct();
                $(".modal-title").html("&nbsp;&nbsp; Search Rate");
                $('#composeRate').modal({ show: true, backdrop: true });
                $("#ddlProduct").val($("#ddlProductName").val()).change();
            }
        };



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

