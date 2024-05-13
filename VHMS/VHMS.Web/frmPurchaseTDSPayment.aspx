<%@ Page Title="TDS Payment" Language="C#" MasterPageFile="~/VHMSMasterPage.master"
    AutoEventWireup="true" CodeFile="frmPurchaseTDSPayment.aspx.cs" Inherits="frmPurchaseTDSPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                <h3>TDS Payment
                </h3>
                  <div class="box box-warning box-solid" id="divViewDamage" style="width: 25%; border-radius: 23px; position: absolute; top: 23%; left: 35%; font-weight: bold;">
                    <div class="box-body">
                        <div class="row">
                            <div class="form-group col-md-1" style="width: 30%; left: 10%; margin-bottom: 0px;">
                                <input type="text" class="form-control" style="height: 15px; width: 15px; background-color: #f5a3a3;" />
                                Purchase
                            </div>
                            <div class="form-group col-md-1" style="width: 30%; left: 30%; margin-bottom: 0px;">
                                <input type="text" class="form-control" style="height: 15px; width: 15px; background-color: #bcfddf;" />
                                Expense
                            </div>
                        </div>
                    </div>
                </div>
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
                                                <th>TDS. No</th>
                                                <th>TDS Date</th>
                                                <th>Slip No</th>
                                                <th>Slip Date</th>
                                                <th>Customer</th>
                                                <th>Total Amount</th>
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
                                                    <th>TDS. No</th>
                                                    <th>TDS Date</th>
                                                    <th>Slip No</th>
                                                    <th>Slip Date</th>
                                                    <th>Customer</th>
                                                    <th>Total Amount</th>
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
            <div class="box box-primary" id="divTDSPayment">
                <div class="box-header with-border">
                    <div class="box-title">TDSPayment Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1" id="divReturnNo">
                            <label>
                                TDS. No</label>
                            <input type="text" class="form-control" id="txtReturnNo" placeholder="Ref. No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divReturnDate">
                            <label>
                                Entry Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtReturnDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="-1" id="txtReturnDate" readonly="true" disabled="disabled" />
                            </div>
                        </div>
                        <div class="form-group col-md-1" id="divBillType">
                            <label>
                             Bill Type</label>
                            <select id="ddlBillType" class="form-control select2" data-placeholder="Select Type" tabindex="1">
                                <option value="Purchase">Purchase</option>
                                <option value="Expense">Expense</option>
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divCustomer">
                            <label>
                                Customer Name</label>
                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="2"></select>
                        </div>
                        <div class="form-group col-md-2" id="divSlipNo">
                            <label>
                                Slip No</label>
                            <input type="text" class="form-control" id="txtSlipNo" placeholder="Slip No"
                                maxlength="30" tabindex="3" />
                        </div>
                        <div class="form-group col-md-2" id="divSlipDate">
                            <label>
                                Slip Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtSlipDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="4" id="txtSlipDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-1" id="divManualPayment">
                            <label>
                                Type</label>
                            <select id="ddlManualPayment" class="form-control select2" data-placeholder="Select Type" tabindex="5">
                                <option value="0">Bill Wise</option>
                                <option value="1">Manual</option>
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
                                        maxlength="30" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divOldSalesInvoice">
                                    <label>
                                        Old Bill No</label><span class="text-danger">*</span>
                                    <div id="divSelectOldProductName">
                                        <select id="ddlOldSalesInvoice" class="form-control select2" data-placeholder="Select SalesInvoice" tabindex="6"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divOldSalesTotalAmt">
                                    <label>
                                        Old Total Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtOldSalesTotalAmt" placeholder="Old Sales TotalAmt"
                                        maxlength="12" tabindex="-1" readonly="true" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divProductName">
                                    <label>
                                        Adjust Bill No</label><span class="text-danger">*</span>
                                    <div id="divSelectProductName">
                                        <select id="ddlProductName" class="form-control select2" data-placeholder="Select Adjust Bill" tabindex="7"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divAvailableQty">
                                    <label>
                                        Adjust Total Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtAvailableQty" placeholder="Billed Qty "
                                        maxlength="12" tabindex="-1" readonly="true" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-1" id="divRate">
                                    <label>
                                        TDS %</label>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="TDS %"
                                        maxlength="12" tabindex="8" />
                                </div>
                                <div class="form-group col-md-1"id="divRoundoffTrans">
                                    <label>
                                        Roundoff</label>
                                    <input type="text" class="form-control" id="txtRoundoffTrans" placeholder="Roundoff"
                                        maxlength="15" tabindex="9" value="0" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer">
                                    <label>
                                        TDS Amount</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="TDS Amount"
                                        maxlength="10" onkeypress="return IsNumeric(event)" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="11">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
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
                        <div class="form-group col-md-8" id="divAddress">
                            <label>
                                Narration</label><span class="text-danger">*</span>
                            <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="13" rows="5" aria-autocomplete="none"></textarea>
                        </div>
                        <div class="form-group col-md-2">
                            <label>
                                Roundoff</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="14" value="0" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2">
                            <label style="font-size: 17px;">
                                TDS Total Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalQty">
                            <input type="text" class="form-control" id="txtTotalQty" placeholder="Total Qty" style="font-weight: bold; font-size: 17px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12">
                            <label>Upload</label>
                            <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                             runat="server" ID="imgUpload1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="214px"
                             OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                            <input type="hidden" id="hdnImgupload1" />
                            <img src="" id="imgUpload1_view" alt="" class="preview_img" style="width: 280px;" />
                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="16">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="17">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="18">
                            <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnTDSPaymentID" />
    <input type="hidden" id="hdnTDSPaymentTransID" />
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
    <script src="UserDefined_Js/jPurchaseTDSPayment.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jPurchaseTDSPayment.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmTDSPayment.aspx") %>';
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmTDSPayment.aspx") %>';
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
                    $("#hdnImgupload1").val("/images/Documents/TDSPayment/" + r.d);
                    $("#imgUpload1_view").val("/images/Documents/TDSPayment/" + r.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


    </script>

</asp:Content>
