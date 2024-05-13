<%@ Page Title="Vendor Entry" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmVendorEntry.aspx.cs" Inherits="frmVendorEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Vendor Entry
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
                                                <th>Vendor Name</th>
                                                <th>Month Year</th>
                                                <th>PhoneNo</th>
                                                <th>Total In Qty</th>
                                                <th>Total Out Qty</th>
                                                <th>Balance Qty</th>
                                                <th>Status</th>
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
                                        Search Vendor Entry records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Vendor Name</th>
                                                <th>Month Year</th>
                                                <th>PhoneNo</th>
                                                <th>Total In Qty</th>
                                                <th>Total Out Qty</th>
                                                <th>Balance Qty</th>
                                                <th>Status</th>
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
                    <div class="box-title">Vendor Entry Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2">
                            <label>
                                Vendor</label><span class="text-danger">*</span>
                            <div id="divVendor">
                                <select id="ddlVendorName" class="form-control select2" data-placeholder="Select Vendor Name" tabindex="1"></select>
                            </div>
                        </div>
                        <div class="form-group col-md-2">
                            <label>
                                Month</label><span class="text-danger">*</span>

                            <div id="divMonth">
                                <select id="ddlMonth" class="form-control" tabindex="5">
                                    <option value="January">January</option>
                                    <option value="February">February</option>
                                    <option value="March">March</option>
                                    <option value="April">April</option>
                                    <option value="May">May</option>
                                    <option value="June">June</option>
                                    <option value="July">July</option>
                                    <option value="August">August</option>
                                    <option value="September">September</option>
                                    <option value="October">October</option>
                                    <option value="November">November</option>
                                    <option value="December">December</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divYear">
                            <label>
                                Year</label><span class="text-danger">*</span>
                            <select id="ddlYear" class="form-control select2" data-placeholder="" tabindex="2"></select>
                        </div>
                        <div class="form-group col-md-2">
                            <label>
                                Opening Qty</label>
                            <div id="divOpeningQty">
                                <input type="text" class="form-control" id="txtOpeningQty" placeholder="0"
                                    maxlength="15" tabindex="2" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                            </div>
                        </div>
                        <div class="form-group col-md-2">
                            <label>
                                Opening Balance</label>
                            <div id="divOpeningBalance">
                                <input type="text" class="form-control" id="txtOpeningBalance" placeholder="0"
                                    maxlength="15" tabindex="3" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                            </div>
                        </div>

                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divEntryDate">
                                    <label>
                                        Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtEntryDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="4" id="txtEntryDate" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divType">
                                    <label>
                                        Type</label><span class="text-danger">*</span>
                                    <select id="ddlType" class="form-control" tabindex="5">
                                        <option value="Outward" selected="selected">Outward</option>
                                        <option value="Inward">Inward</option>
                                        <option value="Damage">Damage</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-2" id="divWork">
                                    <label>
                                        Job Work</label><span class="text-danger">*</span>
                                    <select id="ddlWorkName" class="form-control select2" data-placeholder="Select Work" tabindex="6"></select>
                                </div>
                                <div class="form-group col-md-1" id="divQuantity">
                                    <label id="lblQuantity">
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="0"
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divRepolish">
                                    <label id="lblRepolish">
                                        Repolish</label>
                                    <input type="text" class="form-control TRSearch" id="txtRepolish" placeholder="0"
                                        maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divReturnQuantity">
                                    <label id="lblReturnQuantity">
                                        Rtn Qty</label>
                                    <input type="text" class="form-control TRSearch" id="txtReturnQuantity" placeholder="0"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divRate">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="10" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divSubTotal">
                                    <label id="lblAmount">
                                        Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Amount"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <%-- <div class="form-group col-md-2" id="divPaymentMode">
                                    <label>
                                        Payment Mode</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="5">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="3">NEFT/RTGS</option>
                                        <option value="4">IMPS</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Cheque</option>
                                        <option value="5">Others</option>
                                    </select>
                                </div>--%>
                                <div class="form-group col-md-3" id="divBank">
                                    <label>
                                        Select A/c</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="4">
                                    </select>
                                </div>
                                <div class="form-group col-md-2" id="divChequeNo">

                                    <label id="lblCollection1">Reference No #</label>
                                    <asp:Label ID="Label1" Text="Reference No #" runat="server" Style="font-weight: bold; display: none;"></asp:Label>
                                    <span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                        maxlength="150" tabindex="9" autocomplete="off" />
                                </div>
                                <%-- <div class="form-group col-md-2" id="divCollectionDate">
                                    <label id="lblCollection">Payment Date</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="8" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>--%>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
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

                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Payment mode
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divPaymentMode">
                                    <label>Payment Mode</label>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="32">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Credit</option>
                                        <option value="3">Debit Card</option>
                                        <option value="4">Credit Card</option>
                                        <option value="5">GPay</option>
                                        <option value="6">Paytm</option>
                                        <option value="7">IMPS</option>
                                        <option value="8">NEFT/RTGS</option>
                                        <option value="9">Cheque</option>
                                        <option value="10">Advance</option>
                                        <option value="11">Others</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-2" id="divBankName">
                                    <label>Bank</label>
                                    <select id="ddlBankName" class="form-control select2" data-placeholder="Select Bank Name" tabindex="33"></select>
                                </div>
                                <div class="form-group col-md-2" id="divCardNo">
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
                                <div class="form-group col-md-2" id="divPayableAmount">
                                    <label>Payable Amount</label>
                                    <input type="text" class="form-control" id="txtPayableAmount" placeholder="Payable Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divTDS">
                                    <label>TDS%</label>
                                    <input type="text" class="form-control" id="txtTDS" placeholder="Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-2" id="divTDSAmounts">
                                    <label>TDS Amount</label>
                                    <input type="text" class="form-control" id="txtTDSAmounts" placeholder="Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-2" id="divRoundOff">
                                    <label>RoundOff</label>
                                    <input type="text" class="form-control" id="txtRoundOff" placeholder="Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divPaidAmounts">
                                    <label>Amount</label>
                                    <input type="text" class="form-control" id="txtPaidAmounts" placeholder="Amount"
                                        maxlength="15" tabindex="36" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
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
                        <div class="table-responsive" style="min-height: 100px !Important;">
                            <div id="divPaymentList">
                            </div>
                        </div>

                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="form-group col-md-2"></div>
                        <div class="form-group col-md-2" id="divDamageAmount">
                            <label>
                                Damage Amount</label>
                            <input type="text" class="form-control" id="txtDamageAmount" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <%-- <div class="form-group col-md-2" id="divPaidAmount">
                            <label>
                                Amount Paid</label>
                            <input type="text" class="form-control" id="txtPaidAmount" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>--%>
                        <div class="form-group col-md-2" id="divBillingAmount">
                            <label>
                                Bill Value</label>
                            <input type="text" class="form-control" id="txtBillingAmount" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2" id="divTotalOutQuantity">
                            <label>
                                Total Out Qty</label>
                            <input type="text" class="form-control" id="txtTotalOutQuantity" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2" id="divTotalInQuantity">
                            <label>
                                Total In Qty</label>
                            <input type="text" class="form-control" id="txtTotalInQuantity" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2" id="divTotalPayment">
                            <label>
                              Total Payable Amt</label>
                            <input type="text" class="form-control" id="txtTotalPayment" style="font-size: 104%; font-weight: bold;" placeholder="0"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-2"></div>
                        <div class="form-group col-md-4" id="divNarration">
                            <label>
                                Narration</label>
                            <textarea id="txtNarration" class="form-control" maxlength="255" tabindex="13" rows="2" aria-autocomplete="none"></textarea>
                        </div>
                        <div class="form-group col-md-2" id="divStatus">
                            <label>
                                Status</label><span class="text-danger">*</span>
                            <select id="ddlStatus" class="form-control" tabindex="14">
                                <option value="Open" selected="selected">Open</option>
                                <option value="Closed">Closed</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divBalanceQuantity">
                            <label style="font-size: 20px;">
                                Balance Qty</label>
                            <input type="text" class="form-control" id="txtBalanceQuantity" placeholder="0" maxlength="15" tabindex="-1"
                                style="font-weight: bold; font-size: 25px;" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2" id="divBalanceAmount">
                            <label style="font-size: 20px;">
                                Balance Amount</label>
                            <input type="text" class="form-control" id="txtBalanceAmount" placeholder="0" maxlength="15" tabindex="-1"
                                style="font-weight: bold; font-size: 25px;" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>

                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="16">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="17">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>

                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnVendorEntryID" />
    <input type="hidden" id="hdnPreviousVendorEntryID" />
    <input type="hidden" id="hdnVendorTransID" />
    <input type="hidden" id="hdnVendorID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnStartDate" />
    <input type="hidden" id="hdnEndDate" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jVendorEntry.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jVendorEntry.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

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
</asp:Content>

