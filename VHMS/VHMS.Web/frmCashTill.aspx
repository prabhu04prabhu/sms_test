<%@ Page Title="Cash Till" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmCashTill.aspx.cs" Inherits="frmCashTill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Balance Closing
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li class="active">Balance Closing</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
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
                                                <th>SNo
                                                </th>
                                                <th>Date
                                                </th>

                                                <th class="hidden-xs">Total Amount
                                                </th>
                                                <th>View
                                                </th>
                                                <th>Edit
                                                </th>
                                                <th>Delete
                                                </th>
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
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>SNo
                                                    </th>
                                                    <th>Date
                                                    </th>

                                                    <th class="hidden-xs">Total Amount
                                                    </th>
                                                    <th>View
                                                    </th>
                                                    <th>Edit
                                                    </th>
                                                    <th>Delete
                                                    </th>
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
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-content">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="row">
                            <div class="form-Cash col-md-3"></div>
                            <div class="form-group col-md-1">
                                <label>
                                    Date</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divDate">
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="-1" id="txtDate" disabled />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-Cash col-md-1"></div>
                            <div class="form-Cash col-md-3" id="divRecordsPettyCash">

                                <div class="col-xs-12 DaybookHeader">
                                    <h4 class="headername">Petty Cash</h4>
                                    <div class="box box-warning tableboxShadow">
                                        <div class="box-body">
                                            <div class="table-responsive">
                                                <table id="tblRecordPettyCash" class="table table-striped table-bordered table-hover bg-info" width="20%">
                                                    <thead>
                                                        <tr>
                                                            <th>Value
                                                            </th>
                                                            <th>Count
                                                            </th>
                                                            <th>Total 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tblRecordPettyCash_tbody">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-Cash col-md-3" id="divRecordsTally">
                                <div class="form-Cash col-md-1"></div>
                                <div class="col-xs-12 DaybookHeader">
                                    <h4 class="headername">Today Payment Details
                                    </h4>
                                    <div class="box box-warning tableboxShadow">
                                        <div class="box-body">
                                            <div class="table-responsive">
                                                <table id="tblRecordTally" class="table table-striped table-bordered table-hover bg-info" width="20%">
                                                    <thead>
                                                        <tr style="background-color: aquamarine;">
                                                            <th>Particular
                                                            </th>
                                                            <th>Value
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tblRecordTally_tbody">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-Cash col-md-3" id="divRecordsCash">
                                <div class="col-xs-12 DaybookHeader">
                                    <h4 class="headername">Sales Details</h4>
                                    <div class="box box-warning tableboxShadow">
                                        <div class="box-body">
                                            <div class="table-responsive">
                                                <table id="tblRecordCash" class="table table-striped table-bordered table-hover bg-info" width="20%">
                                                    <tbody id="tblRecordCash_tbody">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-group col-md-4" id="divAddress">
                                <label>
                                    Notes</label><span class="text-danger">*</span>
                                <textarea id="txtNotes" class="form-control" maxlength="4000" tabindex="3" rows="15"></textarea>
                            </div>
                        </div>
                        <div class="modal-body" style="display: none;">

                            <div class="row">

                                <div class="form-group col-md-3"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Cash
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divCash">
                                    <input type="text" class="form-control TRSearch" id="txtCash" placeholder="Cash"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>

                            </div>
                            <div class="row" style="display: none;">
                                <div class="form-group col-md-3">
                                    <label>
                                        Rs. 2000
                                    </label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-3" id="divTwoThousand">
                                    <input type="text" class="form-control TRSearch" id="txtTwoThousand" placeholder="2000"
                                        maxlength="12" tabindex="2" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divTwoThousandAmt">
                                    <input type="text" class="form-control TRSearch" id="txtTwoThousandAmt" placeholder="0"
                                        maxlength="12" tabindex="2" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-md-1" style="display: none;">
                                    <label>
                                        Rs.1000</label>
                                </div>
                                <div class="form-group col-md-1" style="display: none;">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divThousand" style="display: none;">
                                    <input type="text" class="form-control TRSearch" id="txtThousand" placeholder="1000"
                                        maxlength="12" tabindex="3" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divThousandAmt" style="display: none;">
                                    <input type="text" class="form-control TRSearch" id="txtThousandAmt" placeholder="0"
                                        maxlength="12" tabindex="-1" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 500</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X   
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divFiveHundred">
                                    <input type="text" class="form-control TRSearch" id="txtFiveHundred" placeholder="500"
                                        maxlength="12" tabindex="4" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divFiveHundredAmt">
                                    <input type="text" class="form-control TRSearch" id="txtFiveHundredAmt" placeholder="0"
                                        maxlength="12" tabindex="4" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Credit
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divCredit">
                                    <input type="text" class="form-control TRSearch" id="txtCredit" placeholder="Credit"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 200</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divTwoHundred">
                                    <input type="text" class="form-control TRSearch" id="txtTwoHundred" placeholder="200"
                                        maxlength="12" tabindex="5" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divTwoHundredAmt">
                                    <input type="text" class="form-control TRSearch" id="txtTwoHundredAmt" placeholder="0"
                                        maxlength="12" tabindex="5" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>

                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Debit Card
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divDebitCard">
                                    <input type="text" class="form-control TRSearch" id="txtDebitCard" placeholder="Debit Card"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 100</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divHundred">
                                    <input type="text" class="form-control TRSearch" id="txtHundred" placeholder="100"
                                        maxlength="12" tabindex="6" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divHundredAmt">
                                    <input type="text" class="form-control TRSearch" id="txtHundredAmt" placeholder="0"
                                        maxlength="12" tabindex="6" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Credit Card</label>
                                </div>
                                <div class="form-group col-md-2" id="divCreditCard">
                                    <input type="text" class="form-control TRSearch" id="txtCreditCard" placeholder="Credit Card"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 50</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divFifty">
                                    <input type="text" class="form-control TRSearch" id="txtFifty" placeholder="50"
                                        maxlength="12" tabindex="7" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divFiftyAmt">
                                    <input type="text" class="form-control TRSearch" id="txtFiftyAmt" placeholder="0"
                                        maxlength="12" tabindex="7" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Gpay</label>
                                </div>
                                <div class="form-group col-md-2" id="divGpay">
                                    <input type="text" class="form-control TRSearch" id="txtGpay" placeholder="Gpay"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 20</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divTwenty">
                                    <input type="text" class="form-control TRSearch" id="txtTwenty" placeholder="20"
                                        maxlength="12" tabindex="8" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divTwentyAmt">
                                    <input type="text" class="form-control TRSearch" id="txtTwentyAmt" placeholder="0"
                                        maxlength="12" tabindex="8" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Paytm</label>
                                </div>
                                <div class="form-group col-md-2" id="divPaytm">
                                    <input type="text" class="form-control TRSearch" id="txtPaytm" placeholder="Paytm"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 10</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divTen">
                                    <input type="text" class="form-control TRSearch" id="txtTen" placeholder="10"
                                        maxlength="12" tabindex="9" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divTenAmt">
                                    <input type="text" class="form-control TRSearch" id="txtTenAmt" placeholder="0"
                                        maxlength="12" tabindex="9" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        IMPS</label>
                                </div>
                                <div class="form-group col-md-2" id="divIMPS">
                                    <input type="text" class="form-control TRSearch" id="txtIMPS" placeholder="IMPS"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 5</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divFive">
                                    <input type="text" class="form-control TRSearch" id="txtFive" placeholder="5"
                                        maxlength="12" tabindex="10" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divFiveAmt">
                                    <input type="text" class="form-control TRSearch" id="txtFiveAmt" placeholder="0"
                                        maxlength="12" tabindex="10" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        NEFT/RTGS</label>
                                </div>
                                <div class="form-group col-md-2" id="divNEFT">
                                    <input type="text" class="form-control TRSearch" id="txtNEFT" placeholder="NEFT/RTGS"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 2</label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divTwo">
                                    <input type="text" class="form-control TRSearch" id="txtTwo" placeholder="2"
                                        maxlength="12" tabindex="11" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divTwoAmt">
                                    <input type="text" class="form-control TRSearch" id="txtTwoAmt" placeholder="0"
                                        maxlength="12" tabindex="11" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Cheque</label>
                                </div>
                                <div class="form-group col-md-2" id="divCheque">
                                    <input type="text" class="form-control TRSearch" id="txtCheque" placeholder="Cheque"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-1">
                                    <label>
                                        Rs. 1
                                    </label>
                                </div>
                                <div class="form-group col-md-1">
                                    <label>
                                        X
                                    </label>
                                </div>
                                <div class="form-group col-md-2" id="divone">
                                    <input type="text" class="form-control TRSearch" id="txtOne" placeholder="1"
                                        maxlength="12" tabindex="12" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divoneAmt">
                                    <input type="text" class="form-control TRSearch" id="txtOneAmt" placeholder="0"
                                        maxlength="12" tabindex="12" onkeypress="return isNumberKey(event)" disabled="disabled" />
                                </div>
                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Advance</label>
                                </div>
                                <div class="form-group col-md-2" id="divAdvance">
                                    <input type="text" class="form-control TRSearch" id="txtAdvance" placeholder="Advance"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-2">
                                    <label>
                                        Total Amount</label><span class="text-danger">*</span>
                                </div>
                                <div class="form-group col-md-4" id="divTotalAmount">
                                    <input type="text" class="form-control TRSearch" id="txtTotalAmount" placeholder="Total Amount"
                                        maxlength="12" tabindex="14" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1"></div>
                                <div class="form-group col-md-2" style="text-align: right;">
                                    <label>
                                        Others</label>
                                </div>
                                <div class="form-group col-md-2" id="divOthers">
                                    <input type="text" class="form-control TRSearch" id="txtOthers" placeholder="Others"
                                        maxlength="12" tabindex="13" onkeypress="return IsNumeric(event)" autocomplete="off" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="17">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="15">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="16">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }


            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });
            pLoadingSetup(false);
            pLoadingSetup(true);
            GetPettyCash();
            GetTallyofAccounts();
            GetCashDetails();
            GetRecord();
        });

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Balance Closing");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtDate").focus();
            $("#txtTwoThousand").focus();
            GetRetailSalesPaymentAmount();
            return false;
        });


        function GetTallyofAccounts() {
            dProgress(true);
            $("#tblRecordTally_tbody").empty();

            var table = "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Cash</td><td style='text-align:left;width:16%;'><label id='lblCash' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Credit</td><td style='text-align:left;width:16%;'><label id='lblCredit' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>DebitCard</td><td style='text-align:left;width:16%;'><label id='lblDebitCard' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>CreditCard</td><td style='text-align:left;width:16%;'><label id='lblCreditCard' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>GPay</td><td style='text-align:left;width:16%;'><label id='lblGPay' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Paytm</td><td style='text-align:left;width:16%;'><label id='lblPaytm' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>IMPS</td><td style='text-align:left;width:16%;'><label id='lblIMPS' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>NEFT</td><td style='text-align:left;width:16%;'><label id='lblNEFT' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Cheque</td><td style='text-align:left;width:16%;'><label id='lblCheque' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Advance</td><td style='text-align:left;width:16%;'><label id='lblAdvance' style='font-size: 15px  !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Others</td><td style='text-align:left;width:16%;'><label id='lblOthers' style='font-size: 15px  !important;'>0</label></td></tr>";
            //table += "<tr style='background-color: #fff;'><td style='text-align:left; color:blue; font-size:17px;font-weight:bold;'>Total</td><td style='text-align:left;width:8%;color:blue; font-size:17px;font-weight:bold;'><label id='lblTotalCash' style='font-size: 18px !important;'>0</label></td></tr>";

            $("#tblRecordTally_tbody").append(table);
            return false;
        }



        function GetCashDetails() {
            dProgress(true);
            $("#tblRecordCash_tbody").empty();

            var table = "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Today Sales </td><td style='text-align:left;width:16%;'><label id='lblTotalSalesAmount' style='font-size: 15px !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Today Payment</td><td style='text-align:left;width:16%;'><label id='lblTodayPayment' style='font-size: 15px !important;'>0</label></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='text-align:left; color:blue; font-size:17px;font-weight:bold;'>Difference</td><td style='text-align:left;width:8%;color:blue; font-size:17px;font-weight:bold;'><label id='lblTotalBalance' style='font-size: 18px !important;'>0</label></td></tr>";
            // table += "<tr style='background-color: #fff;'><td style='text-align:left;width:8%;'>Today Receipt Amount</td><td style='text-align:left;width:16%;'><label id='lblTotalReceiptAmount' style='font-size: 15px !important;'>0</label></td></tr>";

            $("#tblRecordCash_tbody").append(table);
            return false;
        }

        function GetPettyCash() {
            dProgress(true);
            $("#tblRecordPettyCash_tbody").empty();

            // table = "<tr style='display:none;'><td>2000</td><td style='text-align:left;width:6%;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_2000' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_2000' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            // table += "<tr><td>1000</td><td style='text-align:left;width:6%;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_1000' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_1000' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            var table = "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.500</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_500' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_500' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.200</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_200' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_200' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.100</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_100' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_100' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.50</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_50' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_50' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.20</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_20' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_20' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.10</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_10' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_10' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.5</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_5' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_5' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.2</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_2' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_2' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'>RS.1</td><td style='text-align:left;'><input type='text' style='width:80px;' maxlength='10' class='form-control-sm Calculate' value='0' placeholder='Purchase Price' id='txtPettyCash_1' onkeypress='return IsNumeric(event)'/></td><td style='text-align:left;width:6%;'><input type='text' style='width:90px;' maxlength='10' class='form-control-sm' value='0' placeholder='Purchase Price' id='txtPettytotal_1' onkeypress='return IsNumeric(event)' readonly/></td></tr>";
            table += "<tr style='background-color: #fff;'><td style='font-weight: bold;'></td><td style='text-align:left; color:blue; font-size:17px;font-weight:bold;'>Total</td><td style='text-align:left;width:8%;color:blue; font-size:17px;font-weight:bold;'><label id='lblTotalPettyCash' style='font-size: 18px !important;'>0</label></td></tr>";

            // table += "</tr>";
            $("#tblRecordPettyCash_tbody").append(table);

            $(".Calculate").blur(function () {
                var sCtrlName2000 = "txtPettyCash_2000";
                var iSubtotal2000 = 2000 * + $("#" + sCtrlName2000).val();
                $("#txtPettytotal_2000").val(iSubtotal2000);

                var sCtrlName1000 = "txtPettyCash_1000";
                var iSubtotal1000 = 1000 * + $("#" + sCtrlName1000).val();
                $("#txtPettytotal_1000").val(iSubtotal1000);

                var sCtrlName500 = "txtPettyCash_500";
                var iSubtotal500 = 500 * + $("#" + sCtrlName500).val();
                $("#txtPettytotal_500").val(iSubtotal500);

                var sCtrlName200 = "txtPettyCash_200";
                var iSubtotal200 = 200 * + $("#" + sCtrlName200).val();
                $("#txtPettytotal_200").val(iSubtotal200);

                var sCtrlName100 = "txtPettyCash_100";
                var iSubtotal100 = 100 * + $("#" + sCtrlName100).val();
                $("#txtPettytotal_100").val(iSubtotal100);

                var sCtrlName50 = "txtPettyCash_50";
                var iSubtotal50 = 50 * + $("#" + sCtrlName50).val();
                $("#txtPettytotal_50").val(iSubtotal50);

                var sCtrlName20 = "txtPettyCash_20";
                var iSubtotal20 = 20 * + $("#" + sCtrlName20).val();
                $("#txtPettytotal_20").val(iSubtotal20);

                var sCtrlName10 = "txtPettyCash_10";
                var iSubtotal10 = 10 * + $("#" + sCtrlName10).val();
                $("#txtPettytotal_10").val(iSubtotal10);

                var sCtrlName5 = "txtPettyCash_5";
                var iSubtotal5 = 5 * + $("#" + sCtrlName5).val();
                $("#txtPettytotal_5").val(iSubtotal5);

                var sCtrlName2 = "txtPettyCash_2";
                var iSubtotal2 = 2 * + $("#" + sCtrlName2).val();
                $("#txtPettytotal_2").val(iSubtotal2);

                var sCtrlName1 = "txtPettyCash_1";
                var iSubtotal1 = 1 * + $("#" + sCtrlName1).val();
                $("#txtPettytotal_1").val(iSubtotal1);

                $("#lblTotalPettyCash").text(parseFloat(iSubtotal500) + parseFloat(iSubtotal200) + parseFloat(iSubtotal100) + parseFloat(iSubtotal50) + parseFloat(iSubtotal20) + parseFloat(iSubtotal10) + parseFloat(iSubtotal5) + parseFloat(iSubtotal2) + parseFloat(iSubtotal1));


            });

            return false;
        }

        function GetRetailSalesPaymentAmount() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetRetailSalesPaymentAmount",
                data: JSON.stringify({ iDate: $("#txtDate").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {

                                    $("#lblCash").text(obj.Cash);
                                    $("#lblCredit").text(obj.Credit);
                                    $("#lblDebitCard").text(obj.DebitCard);
                                    $("#lblCreditCard").text(obj.CreditCard);
                                    $("#lblGPay").text(obj.GPay);
                                    $("#lblPaytm").text(obj.Paytm);
                                    $("#lblIMPS").text(obj.IMPS);
                                    $("#lblNEFT").text(obj.NEFT);
                                    $("#lblCheque").text(obj.Cheque);
                                    $("#lblAdvance").text(obj.Advance);
                                    $("#lblOthers").text(obj.Others);
                                    $("#lblTodayPayment").text(obj.TodayPayment);
                                    $("#lblTotalSalesAmount").text(obj.TotalSalesAmount);
                                    $("#lblTotalBalance").text(parseFloat(obj.TodayPayment) - parseFloat(obj.TotalSalesAmount));

                                    // $("#lblTotalReceiptAmount").text(obj.ReceiptAmount);

                                    // $("#lblTotalCash").text(parseFloat(obj.Cash) + parseFloat(obj.Credit) + parseFloat(obj.DebitCard) + parseFloat(obj.CreditCard) + parseFloat(obj.GPay) + parseFloat(obj.Paytm) + parseFloat(obj.IMPS) + parseFloat(obj.NEFT) + parseFloat(obj.Cheque) + parseFloat(obj.Advance) + parseFloat(obj.Others));
                                    dProgress(false);
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function ClearFields() {
            $("#txtTwoThousand").val("");
            $("#txtThousand").val("");
            $("#txtFiveHundred").val("");
            $("#txtTwoHundred").val("");
            $("#txtHundred").val("");
            $("#txtFifty").val("");
            $("#txtTwenty").val("");
            $("#txtTen").val("");
            $("#txtFive").val("");
            $("#txtOne").val("");
            $("#txtTwo").val("");
            $("#txtCardAmount").val("");
            $("#txtTotalAmount").val(0);
            $("#txtTwoThousandAmt").val(0);
            $("#txtThousandAmt").val(0);
            $("#txtFiveHundredAmt").val(0);
            $("#txtTwoHundredAmt").val(0);
            $("#txtHundredAmt").val(0);
            $("#txtFiftyAmt").val(0);
            $("#txtTwentyAmt").val(0);
            $("#txtTenAmt").val(0);
            $("#txtFiveAmt").val(0);
            $("#txtOneAmt").val(0);
            $("#txtTwoAmt").val(0);

            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtDate").val(d + "/" + m + "/" + y);

            $("#chkStatus").prop("checked", true);
            $("#divName").removeClass('has-error');
            return false;
        }
        $("#txtTwoThousand,#txtThousand,#txtFiveHundred,#txtTwoHundred,#txtHundred,#txtFifty,#txtTwenty,#txtTen,#txtFive,#txtTwo,#txtOne,#txtCardAmount").change(function () {
            var Twok = 0; var onek = 0; var fiveh = 0; var twoh = 0; var oneh = 0; var fifty = 0; var twenty = 0;
            var ten = 0; var five = 0; var two = 0; var one = 0; var card = 0; var Total = 0;

            if ($("#txtTwoThousand").val() > 0)
                Twok = $("#txtTwoThousand").val();
            if ($("#txtThousand").val() > 0)
                onek = $("#txtThousand").val();
            if ($("#txtFiveHundred").val() > 0)
                fiveh = $("#txtFiveHundred").val();
            if ($("#txtTwoHundred").val() > 0)
                twoh = $("#txtTwoHundred").val();
            if ($("#txtHundred").val() > 0)
                oneh = $("#txtHundred").val();
            if ($("#txtFifty").val() > 0)
                fifty = $("#txtFifty").val();
            if ($("#txtTwenty").val() > 0)
                twenty = $("#txtTwenty").val();
            if ($("#txtTen").val() > 0)
                ten = $("#txtTen").val();
            if ($("#txtFive").val() > 0)
                five = $("#txtFive").val();
            if ($("#txtTwo").val() > 0)
                two = $("#txtTwo").val();
            if ($("#txtOne").val() > 0)
                one = $("#txtOne").val();
            if ($("#txtCardAmount").val() > 0)
                card = $("#txtCardAmount").val();

            $("#txtTwoThousandAmt").val(parseFloat(Twok) * 2000);
            $("#txtThousandAmt").val(parseFloat(onek) * 1000);
            $("#txtFiveHundredAmt").val(parseFloat(fiveh) * 500);
            $("#txtTwoHundredAmt").val(parseFloat(twoh) * 200);
            $("#txtHundredAmt").val(parseFloat(oneh) * 100);
            $("#txtFiftyAmt").val(parseFloat(fifty) * 50);
            $("#txtTwentyAmt").val(parseFloat(twenty) * 20);
            $("#txtTenAmt").val(parseFloat(ten) * 10);
            $("#txtFiveAmt").val(parseFloat(five) * 5);
            $("#txtTwoAmt").val(parseFloat(two) * 2);
            $("#txtOneAmt").val(parseFloat(one));

            Total = parseFloat(Twok) * 2000 + parseFloat(onek) * 1000 + parseFloat(fiveh) * 500 + parseFloat(twoh) * 200 + parseFloat(oneh) * 100 +
                parseFloat(fifty) * 50 + parseFloat(twenty) * 20 + parseFloat(ten) * 10 + parseFloat(five) * 5 + parseFloat(two) * 2 + parseFloat(one) + parseFloat(card);

            $("#txtTotalAmount").val(parseFloat(Total).toFixed(2));
        });

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            }
            else { $("#divDate").removeClass('has-error'); }

            //if ($("#txtTotalAmount").val().trim() == "" || $("#txtTotalAmount").val().trim() == "0" || $("#txtTotalAmount").val().trim() == undefined) {
            //    $.jGrowl("Please Enter Data", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divTotalAmount").addClass('has-error'); $("#txtTotalAmount").focus(); return false;
            //}
            //else { $("#divTotalAmount").removeClass('has-error'); }

            //if ($("#txtTwoThousand").val().trim() == "" || $("#txtTwoThousand").val().trim() == undefined) {
            //    $("#txtTwoThousand").val(0);
            //}
            //if ($("#txtThousand").val().trim() == "" || $("#txtThousand").val().trim() == undefined) {
            //    $("#txtThousand").val(0);
            //}
            //if ($("#txtFiveHundred").val().trim() == "" || $("#txtFiveHundred").val().trim() == undefined) {
            //    $("#txtFiveHundred").val(0);
            //}
            //if ($("#txtTwoHundred").val().trim() == "" || $("#txtTwoHundred").val().trim() == undefined) {
            //    $("#txtTwoHundred").val(0);
            //}
            //if ($("#txtHundred").val().trim() == "" || $("#txtHundred").val().trim() == undefined) {
            //    $("#txtHundred").val(0);
            //}
            //if ($("#txtFifty").val().trim() == "" || $("#txtFifty").val().trim() == undefined) {
            //    $("#txtFifty").val(0);
            //}
            //if ($("#txtTwenty").val().trim() == "" || $("#txtTwenty").val().trim() == undefined) {
            //    $("#txtTwenty").val(0);
            //}
            //if ($("#txtTen").val().trim() == "" || $("#txtTen").val().trim() == undefined) {
            //    $("#txtTen").val(0);
            //}
            //if ($("#txtFive").val().trim() == "" || $("#txtFive").val().trim() == undefined) {
            //    $("#txtFive").val(0);
            //}
            //if ($("#txtTwo").val().trim() == "" || $("#txtTwo").val().trim() == undefined) {
            //    $("#txtTwo").val(0);
            //}
            //if ($("#txtOne").val().trim() == "" || $("#txtOne").val().trim() == undefined) {
            //    $("#txtOne").val(0);
            //}
            //if ($("#txtCardAmount").val().trim() == "" || $("#txtCardAmount").val().trim() == undefined) {
            //    $("#txtCardAmount").val(0);
            //}

            var Obj = new Object();
            Obj.CashTillID = 0;
            Obj.sTillDate = $("#txtDate").val().trim();

            var iPettytotal_2000 = $("#txtPettytotal_2000").val();
            var iPettytotal_1000 = $("#txtPettytotal_1000").val();
            var iPettytotal_500 = $("#txtPettytotal_500").val();
            var iPettytotal_200 = $("#txtPettytotal_200").val();
            var iPettytotal_100 = $("#txtPettytotal_100").val();
            var iPettytotal_50 = $("#txtPettytotal_50").val();
            var iPettytotal_20 = $("#txtPettytotal_20").val();
            var iPettytotal_10 = $("#txtPettytotal_10").val();
            var iPettytotal_5 = $("#txtPettytotal_5").val();
            var iPettytotal_2 = $("#txtPettytotal_2").val();
            var iPettytotal_1 = $("#txtPettytotal_1").val();
            var iTotalPettyCash = parseFloat($("#lblTotalPettyCash").text());


            if (!isNaN(iPettytotal_2000) && iPettytotal_2000 != "" && iPettytotal_2000 != undefined)
                Obj.TwoThousandRs = iPettytotal_2000;
            else
                Obj.TwoThousandRs = 0;

            if (!isNaN(iPettytotal_1000) && iPettytotal_1000 != "" && iPettytotal_1000 != undefined)
                Obj.ThousandRs = iPettytotal_1000;
            else
                Obj.ThousandRs = 0;

            if (!isNaN(iPettytotal_500) && iPettytotal_500 != "" && iPettytotal_500 != undefined)
                Obj.FiveHundredRs = iPettytotal_500;
            else
                Obj.FiveHundredRs = 0;

            if (!isNaN(iPettytotal_200) && iPettytotal_200 != "" && iPettytotal_200 != undefined)
                Obj.TwoHundredRs = iPettytotal_200;
            else
                Obj.TwoHundredRs = 0;

            if (!isNaN(iPettytotal_100) && iPettytotal_100 != "" && iPettytotal_100 != undefined)
                Obj.HundredRs = iPettytotal_100;
            else
                Obj.HundredRs = 0;

            if (!isNaN(iPettytotal_50) && iPettytotal_50 != "" && iPettytotal_50 != undefined)
                Obj.FiftyRs = iPettytotal_50;
            else
                Obj.FiftyRs = 0;

            if (!isNaN(iPettytotal_20) && iPettytotal_20 != "" && iPettytotal_20 != undefined)
                Obj.TwentyRs = iPettytotal_20;
            else
                Obj.TwentyRs = 0;

            if (!isNaN(iPettytotal_10) && iPettytotal_10 != "" && iPettytotal_10 != undefined)
                Obj.TenRs = iPettytotal_10;
            else
                Obj.TenRs = 0;

            if (!isNaN(iPettytotal_5) && iPettytotal_5 != "" && iPettytotal_5 != undefined)
                Obj.FiveRs = iPettytotal_5;
            else
                Obj.FiveRs = 0;

            if (!isNaN(iPettytotal_2) && iPettytotal_2 != "" && iPettytotal_2 != undefined)
                Obj.TwoRs = iPettytotal_2;
            else
                Obj.TwoRs = 0;

            if (!isNaN(iPettytotal_1) && iPettytotal_1 != "" && iPettytotal_1 != undefined)
                Obj.OneRs = iPettytotal_1;
            else
                Obj.OneRs = 0;

            if (!isNaN(iTotalPettyCash) && iTotalPettyCash != "" && iTotalPettyCash != undefined)
                Obj.TotalAmount = iTotalPettyCash;
            else
                Obj.TotalAmount = 0;

            //Obj.OneRs = $("#txtOne").val();
            //Obj.TwoRs = $("#txtTwo").val();
            //Obj.FiveRs = $("#txtFive").val();
            //Obj.TenRs = $("#txtTen").val();
            //Obj.TwentyRs = $("#txtTwenty").val();
            //Obj.FiftyRs = $("#txtFifty").val();
            //Obj.HundredRs = $("#txtHundred").val();
            //Obj.TwoHundredRs = $("#txtTwoHundred").val();
            //Obj.FiveHundredRs = $("#txtFiveHundred").val();
            //Obj.ThousandRs = $("#txtThousand").val();
            //Obj.TwoThousandRs = $("#txtTwoThousand").val();
            //Obj.CardAmount = $("#txtCardAmount").val();
            //Obj.TotalAmount = $("#txtTotalAmount").val();
            Obj.Notes = $("#txtNotes").val();
            Obj.IsActive = true;

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.CashTillID = $("#hdnID").val();
                sMethodName = "UpdateCashTill";
            }
            else { sMethodName = "AddCashTill"; }

            SaveandUpdateCashTill(Obj, sMethodName);

            return false;
        });


        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });


        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetTopCashTill",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].CashTillID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].sTillDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";
                                        //table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Balance Closing");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblRecord_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblRecord").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "40%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "5%" },
                                    { "sWidth": "2%" },
                                    { "sWidth": "2%" },
                                    { "sWidth": "2%" }
                                ]
                            });
                            $("#tblRecord_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetSearchRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchCashTill",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblSearchResult").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblSearchResult_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].CashTillID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].sTillDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalAmount + "</td>";
                                        //table += "<td class='hidden-xs'>" + TypeStatus + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].CashTillID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Balance Closing");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "40%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "0%" },
                                    //{ "sWidth": "5%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" }
                                ]
                            });
                            $("#tblSearchResult_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblSearchResult_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function SaveandUpdateCashTill(Obj, sMethodName) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/" + sMethodName,
                data: JSON.stringify({ Objdata: Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                ClearFields();
                                GetRecord();

                                if (sMethodName == "AddCashTill")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateCashTill")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "CashTill_A_01" || objResponse.Value == "CashTill_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCashTillByID",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();

                                    $("#hdnID").val(obj.CashTillID);
                                    $("#txtDate").val(obj.sTillDate);
                                    // $("#txtTwoThousand").val(obj.TwoThousandRs);
                                    //$("#txtThousand").val(obj.ThousandRs);
                                    $("#txtPettyCash_500").val(obj.FiveHundredRs / 500);
                                    $("#txtPettyCash_200").val(obj.TwoHundredRs / 200);
                                    $("#txtPettyCash_100").val(obj.HundredRs / 100);
                                    $("#txtPettyCash_50").val(obj.FiftyRs / 50);
                                    $("#txtPettyCash_20").val(obj.TwentyRs / 20);
                                    $("#txtPettyCash_10").val(obj.TenRs / 10);
                                    $("#txtPettyCash_5").val(obj.FiveRs / 5);
                                    $("#txtPettyCash_2").val(obj.TwoRs / 2);
                                    $("#txtPettyCash_1").val(obj.OneRs / 1);
                                    $("#lblTotalPettyCash").val(obj.TotalAmount);
                                    $("#txtNotes").val(obj.Notes);

                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Balance Closing");
                                    GetRetailSalesPaymentAmount();
                                    $(".Calculate").blur();
                                    //$("#txtTwoThousand").change();
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteCashTill",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                ClearFields();
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "CashTill_R_01" || objResponse.Value == "CashTill_D_01") {
                                $.jGrowl(_CMDeleteError, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });

        $("#aGeneral").click(function () {
            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {
            $("#SearchResult").show();

        });
    </script>
</asp:Content>




