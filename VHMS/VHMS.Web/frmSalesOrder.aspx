<%@ Page Title="SalesOrder" Language="C#" MasterPageFile="~/VHMSMasterPage.master"
    AutoEventWireup="true" CodeFile="frmSalesOrder.aspx.cs" Inherits="frmSalesOrder" %>

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
                <h3>SalesOrder
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
                                                <th>SalesOrder #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Tax Amount</th>
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
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search Sales Order records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>SalesOrder #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Tax Amount</th>
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
            <div class="box box-primary" id="divOPBilling">
                <div class="box-header with-border">
                    <div class="box-title">SalesOrder Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Order No</label>
                        </div>
                        <div class="form-group col-md-2" id="divBillNo">
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Order No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Date</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-4" id="divCustomer">

                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="3"></select>
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
                                        SMSCode</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code"
                                        maxlength="12" tabindex="4" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divProductName">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <div id="divSelectProductName">
                                        <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="5"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-1" id="divAvailQuantity">
                                    <label>
                                        Avail. Qty</label>
                                    <input type="text" class="form-control TRSearch" id="txtAvailQuantity" placeholder="Avail. Qty"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divQuantity">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="6" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divRate">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divFrequency">
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
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="8">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="9">
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
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1" id="divTaxName">

                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="10"></select>
                        </div>

                        <div class="form-group col-md-1">
                            <label>
                                Tax Amount</label>
                        </div>
                        <div class="form-group col-md-1" id="divTaxAmount">

                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount"style="font-size: 104%;font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountPercent">

                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="11" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountAmount">

                            <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount"style="font-size: 104%;font-weight: bold;"
                                maxlength="15" tabindex="12" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" style="text-align:right">
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
                       
                        <div class="form-group col-md-3"></div>
                        <div class="form-group col-md-1" style="text-align:right" >
                            <label>
                                Roundoff</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">

                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff"style="font-size: 104%;font-weight: bold;"
                                maxlength="15" tabindex="13" value="0" autocomplete="off" />
                        </div>
                        
                        </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                         <div class="form-group col-md-2" style="text-align: right">
                            <label style="font-size: 20px;">
                                Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">

                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 20px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="form-group col-md-8" id="divAddress">
                            <label>
                                Comments</label>
                            <textarea id="txtComments" class="form-control" maxlength="1000" tabindex="22" rows="3" aria-autocomplete="none" style="width:154%"></textarea>
                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="14">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save & Print</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update & Print</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="16">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="17">
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
                                    maxlength="150" tabindex="18" autocomplete="off" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="19" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="20">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="21">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnSalesOrderID" />
    <input type="hidden" id="hdnSalesOrderTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jSalesOrder.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jSalesOrder.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
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
    </script>
</asp:Content>
